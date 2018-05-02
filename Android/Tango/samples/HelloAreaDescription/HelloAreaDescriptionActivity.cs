using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using ProjectTango.Service;

namespace HelloAreaDescription
{
    /// <summary>
    /// Main Activity class for the Area Description example.Handles the connection to the Tango service
    /// and propagation of Tango pose data to Layout view.
    /// </summary>
    public class HelloAreaDescriptionActivity : Activity, SetAdfNameDialog.ICallbackListener, Tango.IOnTangoUpdateListener
    {
        private const int SECS_TO_MILLISECS = 1000;
        private const double UPDATE_INTERVAL_MS = 100.0;

        private Tango mTango;
        private TangoConfig mConfig;
        private TextView mUuidTextView;
        private TextView mRelocalizationTextView;

        private Button mSaveAdfButton;

        private double mPreviousPoseTimeStamp;
        private double mTimeToNextUpdate = UPDATE_INTERVAL_MS;

        private bool mIsRelocalized;
        private bool mIsLearningMode;
        private bool mIsConstantSpaceRelocalize;

        private object mSharedLock = new object();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_area_learning);

            var intent = Intent;
            mIsLearningMode = intent.GetBooleanExtra(MainActivity.USE_AREA_LEARNING, false);
            mIsConstantSpaceRelocalize = intent.GetBooleanExtra(MainActivity.LOAD_ADF, false);
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Initialize Tango Service as a normal Android Service, since we call
            // mTango.disconnect() in onPause, this will unbind Tango Service, so
            // everytime when onResume gets called, we should create a new Tango object.
            mTango = new Tango(this, () =>
            {
                lock (this)
                {
                    // Pass in an Acgtion to be called from UI thread when Tango is ready,
                    // this Action will be running on a new thread.
                    // When Tango is ready, we can call Tango functions safely here only
                    // when there is no UI thread changes involved.
                    mConfig = SetTangoConfig(mTango, mIsLearningMode, mIsConstantSpaceRelocalize);

                    // Re-attach listeners.
                    try
                    {
                        SetUpTangoListeners();
                    }
                    catch (TangoErrorException ex)
                    {
                        Console.WriteLine(GetString(Resource.String.tango_error), ex);
                    }
                    catch (Java.Lang.SecurityException ex)
                    {
                        Console.WriteLine(GetString(Resource.String.no_permissions), ex);
                    }

                    // Connect to the tango service (start receiving pose updates).
                    try
                    {
                        mTango.Connect(mConfig);
                    }
                    catch (TangoOutOfDateException ex)
                    {
                        Console.WriteLine(GetString(Resource.String.tango_out_of_date_exception), ex);
                    }
                    catch (TangoErrorException ex)
                    {
                        Console.WriteLine(GetString(Resource.String.tango_error), ex);
                    }
                    catch (TangoInvalidException ex)
                    {
                        Console.WriteLine(GetString(Resource.String.tango_invalid), ex);
                    }
                }

                RunOnUiThread(() =>
                {
                    lock (this)
                    {
                        SetupTextViewsAndButtons(mTango, mIsLearningMode, mIsConstantSpaceRelocalize);
                    }
                });
            });
        }

        protected override void OnPause()
        {
            base.OnPause();

            // Clear the relocalization state: we don't know where the device will be since our app
            // will be paused.
            mIsRelocalized = false;
            lock (this)
            {
                try
                {
                    mTango.Disconnect();
                }
                catch (TangoErrorException ex)
                {
                    Console.WriteLine(GetString(Resource.String.tango_error), ex);
                }
            }
        }

        /// <summary>
        /// Sets Texts views to display statistics of Poses being received. This also sets the buttons
        /// used in the UI. Please note that this needs to be called after TangoService and Config
        /// objects are initialized since we use them for the SDK related stuff like version number
        /// etc.
        /// </summary>
        private void SetupTextViewsAndButtons(Tango tango, bool isLearningMode, bool isLoadAdf)
        {
            mSaveAdfButton = FindViewById<Button>(Resource.Id.save_adf_button);
            mUuidTextView = FindViewById<TextView>(Resource.Id.adf_uuid_textview);
            mRelocalizationTextView = FindViewById<TextView>(Resource.Id.relocalization_textview);

            mSaveAdfButton.Click += (sender, e) => ShowSetAdfNameDialog();

            if (isLearningMode)
            {
                // Disable save ADF button until Tango relocalizes to the current ADF.
                mSaveAdfButton.Enabled = false;
            }
            else
            {
                // Hide to save ADF button if leanring mode is off.
                mSaveAdfButton.Visibility = ViewStates.Gone;
            }

            if (isLoadAdf)
            {
                // Returns a list of ADFs with their UUIDs
                var fullUuidList = tango.ListAreaDescriptions();
                if (fullUuidList.Count == 0)
                {
                    mUuidTextView.SetText(Resource.String.no_uuid);
                }
                else
                {
                    mUuidTextView.Text =
                        GetString(Resource.String.number_of_adfs) + fullUuidList.Count +
                        GetString(Resource.String.latest_adf_is) + fullUuidList[fullUuidList.Count - 1];
                }
            }
        }

        /// <summary>
        /// Sets up the tango configuration object. Make sure mTango object is initialized before
        /// making this call.
        /// </summary>
        private TangoConfig SetTangoConfig(Tango tango, bool isLearningMode, bool isLoadAdf)
        {
            var config = tango.GetConfig(TangoConfig.ConfigTypeDefault);
            // Check if learning mode
            if (isLearningMode)
            {
                // Set learning mode to config.
                config.PutBoolean(TangoConfig.KeyBooleanLearningmode, true);

            }
            // Check for Load ADF/Constant Space relocalization mode.
            if (isLoadAdf)
            {
                // Returns a list of ADFs with their UUIDs.
                var fullUuidList = tango.ListAreaDescriptions();
                // Load the latest ADF if ADFs are found.
                if (fullUuidList.Count > 0)
                {
                    config.PutString(TangoConfig.KeyStringAreadescription, fullUuidList[fullUuidList.Count - 1]);
                }
            }
            return config;
        }

        /// <summary>
        /// Set up the callback listeners for the Tango service, then begin using the Motion
        /// Tracking API. This is called in response to the user clicking the 'Start' Button.
        /// </summary>
        private void SetUpTangoListeners()
        {
            // Set Tango Listeners for Poses Device wrt Start of Service, Device wrt
            // ADF and Start of Service wrt ADF.
            var framePairs = new List<TangoCoordinateFramePair>
            {
                new TangoCoordinateFramePair(TangoPoseData.CoordinateFrameStartOfService, TangoPoseData.CoordinateFrameDevice),
                new TangoCoordinateFramePair(TangoPoseData.CoordinateFrameAreaDescription, TangoPoseData.CoordinateFrameDevice),
                new TangoCoordinateFramePair(TangoPoseData.CoordinateFrameAreaDescription, TangoPoseData.CoordinateFrameStartOfService)
            };

            mTango.ConnectListener(framePairs, this);
        }

        void Tango.IOnTangoUpdateListener.OnPoseAvailable(TangoPoseData pose)
        {
            // Make sure to have atomic access to Tango Data so that UI loop doesn't interfere
            // while Pose call back is updating the data.
            lock (mSharedLock)
            {
                // Check for Device wrt ADF pose, Device wrt Start of Service pose, Start of
                // Service wrt ADF pose (This pose determines if the device is relocalized or
                // not).
                if (pose.BaseFrame == TangoPoseData.CoordinateFrameAreaDescription && pose.TargetFrame == TangoPoseData.CoordinateFrameStartOfService)
                {
                    if (pose.StatusCode == TangoPoseData.PoseValid)
                    {
                        mIsRelocalized = true;
                    }
                    else
                    {
                        mIsRelocalized = false;
                    }
                }
            }

            var deltaTime = (pose.Timestamp - mPreviousPoseTimeStamp) * SECS_TO_MILLISECS;
            mPreviousPoseTimeStamp = pose.Timestamp;
            mTimeToNextUpdate -= deltaTime;

            if (mTimeToNextUpdate < 0.0)
            {
                mTimeToNextUpdate = UPDATE_INTERVAL_MS;

                RunOnUiThread(() =>
                {
                    lock (mSharedLock)
                    {
                        mSaveAdfButton.Enabled = mIsRelocalized;
                        mRelocalizationTextView.Text = mIsRelocalized ? GetString(Resource.String.localized) : GetString(Resource.String.not_localized);
                    }
                });
            }
        }

        void Tango.IOnTangoUpdateListener.OnXyzIjAvailable(TangoXyzIjData xyzij)
        {
            // We are not using TangoXyzIjData for this application.
        }

        void Tango.IOnTangoUpdateListener.OnTangoEvent(TangoEvent evnt)
        {
            // Ignoring TangoEvents.
        }

        void Tango.IOnTangoUpdateListener.OnFrameAvailable(int cameraId)
        {
            // We are not using onFrameAvailable for this application.
        }

        void SetAdfNameDialog.ICallbackListener.OnAdfNameOk(string name, string uuid)
        {
            SaveAdf(name);
        }

        void SetAdfNameDialog.ICallbackListener.OnAdfNameCancelled()
        {
            // Continue running.
        }

        /// <summary>
        /// Save the current Area Description File.
        /// Performs saving on a background thread and displays a progress dialog.
        /// </summary>
        private async void SaveAdf(string adfName)
        {
            var mProgressDialog = new SaveAdfDialog(this);
            mProgressDialog.Show();

            var adfUuid = await Task.Run(() =>
            {
                try
                {
                    // Save the ADF.
                    var uuid = mTango.SaveAreaDescription();

                    // Read the ADF Metadata, set the desired name, and save it back.
                    TangoAreaDescriptionMetaData metadata = mTango.LoadAreaDescriptionMetaData(uuid);
                    metadata.Set(TangoAreaDescriptionMetaData.KeyName, Encoding.Default.GetBytes(adfName));
                    mTango.SaveAreaDescriptionMetadata(uuid, metadata);

                    return uuid;
                }
                catch (TangoErrorException ex)
                {
                    // There's currently no additional information in the exception.
                }
                catch (TangoInvalidException ex)
                {
                    // There's currently no additional information in the exception.
                }
                return null;
            });
            mProgressDialog.Dismiss();
            if (adfUuid == null)
            {
                var toastMessage = string.Format(Resources.GetString(Resource.String.save_adf_failed_toast_format), adfName);
                Toast.MakeText(this, toastMessage, ToastLength.Long).Show();
            }
            else
            {
                var toastMessage = string.Format(Resources.GetString(Resource.String.save_adf_success_toast_format), adfName, adfUuid);
                Toast.MakeText(this, toastMessage, ToastLength.Long).Show();
                Finish();
            }
        }

        /// <summary>
        /// Shows a dialog for setting the ADF name.
        /// </summary>
        private void ShowSetAdfNameDialog()
        {
            var bundle = new Bundle();
            bundle.PutString(TangoAreaDescriptionMetaData.KeyName, "New ADF");
            // UUID is generated after the ADF is saved.
            bundle.PutString(TangoAreaDescriptionMetaData.KeyUuid, "");

            var setAdfNameDialog = new SetAdfNameDialog();
            setAdfNameDialog.Arguments = bundle;
            setAdfNameDialog.Show(FragmentManager, "ADFNameDialog");
        }
    }
}
