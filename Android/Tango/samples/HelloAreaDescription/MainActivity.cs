using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

using ProjectTango.Service;

namespace HelloAreaDescription
{
    /// <summary>
    /// Start Activity for Area Description example. Gives the ability to choose a particular
    /// configuration and also Manage Area Description Files(ADF).
    /// </summary>
    [Activity(Label = "@string/app_name", MainLauncher = true, ScreenOrientation = ScreenOrientation.Nosensor)]
    public class MainActivity : Activity
    {
        // The unique key string for storing user's input.
        public const string USE_AREA_LEARNING = "com.projecttango.examples.java.helloareadescription.usearealearning";
        public const string LOAD_ADF = "com.projecttango.examples.java.helloareadescription.loadadf";

        // Permission request action.
        public const int REQUEST_CODE_TANGO_PERMISSION = 0;

        // UI elements.
        private ToggleButton mLearningModeToggleButton;
        private ToggleButton mLoadAdfToggleButton;

        private bool mIsUseAreaLearning;
        private bool mIsLoadAdf;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_start);

            mLearningModeToggleButton = FindViewById<ToggleButton>(Resource.Id.learning_mode);
            mIsUseAreaLearning = mLearningModeToggleButton.Checked;
            mLearningModeToggleButton.Click += (sender, e) => mIsUseAreaLearning = mLearningModeToggleButton.Checked;

            mLoadAdfToggleButton = FindViewById<ToggleButton>(Resource.Id.load_adf);
            mIsLoadAdf = mLoadAdfToggleButton.Checked;
            mLoadAdfToggleButton.Click += (sender, e) => mIsLoadAdf = mLoadAdfToggleButton.Checked;

            FindViewById<Button>(Resource.Id.start).Click += (sender, e) => StartAreaDescriptionActivity();

            FindViewById<Button>(Resource.Id.AdfListView).Click += (sender, e) => startAdfListView();

            StartActivityForResult(Tango.GetRequestPermissionIntent(Tango.PermissiontypeAdfLoadSave), REQUEST_CODE_TANGO_PERMISSION);
        }

        /// <summary>
        /// Start the main area description activity and pass in user's configuration.
        /// </summary>
        private void StartAreaDescriptionActivity()
        {
            var startAdIntent = new Intent(this, typeof(HelloAreaDescriptionActivity));
            startAdIntent.PutExtra(USE_AREA_LEARNING, mIsUseAreaLearning);
            startAdIntent.PutExtra(LOAD_ADF, mIsLoadAdf);
            StartActivity(startAdIntent);
        }

        /// <summary>
        /// Start the ADF list activity.
        /// </summary>
        private void startAdfListView()
        {
            var startAdfListViewIntent = new Intent(this, typeof(AdfUuidListViewActivity));
            StartActivity(startAdfListViewIntent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // The result of the permission activity.
            //
            // Note that when the permission activity is dismissed, the HelloAreaDescriptionActivity's
            // onResume() callback is called. As the TangoService is connected in the onResume()
            // function, we do not call connect here.
            //
            // Check which request we're responding to
            if (requestCode == REQUEST_CODE_TANGO_PERMISSION)
            {
                // Make sure the request was successful
                if (resultCode == Result.Canceled)
                {
                    Toast.MakeText(this, Resource.String.arealearning_permission, ToastLength.Short).Show();
                    Finish();
                }
            }
        }
    }
}
