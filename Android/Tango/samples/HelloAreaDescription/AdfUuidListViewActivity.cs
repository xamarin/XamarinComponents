using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using ProjectTango.Service;

namespace HelloAreaDescription
{
    /// <summary>
    /// This class lets you manage ADFs between this class's Application Package folder and API private
    /// space. This show cases mainly three things: Import, Export, Delete an ADF file from API private
    /// space to any known and accessible file path.
    /// </summary>
    public class AdfUuidListViewActivity : Activity, SetAdfNameDialog.ICallbackListener
    {

        private ListView mTangoSpaceAdfListView;
        private ListView mAppSpaceAdfListView;
        private AdfUuidArrayAdapter mTangoSpaceAdfListAdapter;
        private AdfUuidArrayAdapter mAppSpaceAdfListAdapter;
        private List<AdfData> mTangoSpaceAdfDataList;
        private List<AdfData> mAppSpaceAdfDataList;
        private string[] mTangoSpaceMenuStrings;
        private string[] mAppSpaceMenuStrings;
        private string mAppSpaceAdfFolder;
        private Tango mTango;
        private volatile bool mIsTangoReady = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.uuid_list_view);
            mTangoSpaceMenuStrings = Resources.GetStringArray(Resource.Array.set_dialog_menu_items_api_space);
            mAppSpaceMenuStrings = Resources.GetStringArray(Resource.Array.set_dialog_menu_items_app_space);

            // Get API ADF ListView Ready
            mTangoSpaceAdfListView = FindViewById<ListView>(Resource.Id.uuid_list_view_tango_space);
            mTangoSpaceAdfDataList = new List<AdfData>();
            mTangoSpaceAdfListAdapter = new AdfUuidArrayAdapter(this, mTangoSpaceAdfDataList);
            mTangoSpaceAdfListView.Adapter = mTangoSpaceAdfListAdapter;
            RegisterForContextMenu(mTangoSpaceAdfListView);

            // Get App Space ADF List View Ready
            mAppSpaceAdfListView = FindViewById<ListView>(Resource.Id.uuid_list_view_application_space);
            mAppSpaceAdfFolder = GetAppSpaceAdfFolder();
            mAppSpaceAdfDataList = new List<AdfData>();
            mAppSpaceAdfListAdapter = new AdfUuidArrayAdapter(this, mAppSpaceAdfDataList);
            mAppSpaceAdfListView.Adapter = mAppSpaceAdfListAdapter;
            RegisterForContextMenu(mAppSpaceAdfListView);
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Initialize Tango Service as a normal Android Service, since we call
            // mTango.disconnect() in onPause, this will unbind Tango Service, so
            // everytime when onResume gets called, we should create a new Tango object.
            mTango = new Tango(this, () =>
            {
                // Pass in an Action to be called from UI thread when Tango is ready,
                // this Action will be running on a new thread.
                // When Tango is ready, we can call Tango functions safely here only
                // when there is no UI thread changes involved.
                mIsTangoReady = true;
                RunOnUiThread(() =>
                {
                    lock (this)
                    {
                        UpdateList();
                    }
                });
            });
        }

        protected override void OnPause()
        {
            base.OnPause();
            lock (this)
            {
                // Unbinds Tango Service
                mTango.Disconnect();
            }
            mIsTangoReady = false;
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
            if (v.Id == Resource.Id.uuid_list_view_tango_space)
            {
                menu.SetHeaderTitle(mTangoSpaceAdfDataList[info.Position].Uuid);
                menu.Add(mTangoSpaceMenuStrings[0]);
                menu.Add(mTangoSpaceMenuStrings[1]);
                menu.Add(mTangoSpaceMenuStrings[2]);
            }

            if (v.Id == Resource.Id.uuid_list_view_application_space)
            {
                menu.SetHeaderTitle(mAppSpaceAdfDataList[info.Position].Uuid);
                menu.Add(mAppSpaceMenuStrings[0]);
                menu.Add(mAppSpaceMenuStrings[1]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (!mIsTangoReady)
            {
                Toast.MakeText(this, Resource.String.tango_not_ready, ToastLength.Short).Show();
                return false;
            }

            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var itemName = item.TitleFormatted.ToString();
            int index = info.Position;

            // Rename the ADF from API storage
            if (itemName == mTangoSpaceMenuStrings[0])
            {
                // Delete the ADF from Tango space and update the Tango ADF Listview.
                ShowSetNameDialog(mTangoSpaceAdfDataList[index].Uuid);
            }
            else if (itemName == mTangoSpaceMenuStrings[1])
            {
                // Delete the ADF from Tango space and update the Tango ADF Listview.
                DeleteAdfFromTangoSpace(mTangoSpaceAdfDataList[index].Uuid);
            }
            else if (itemName == mTangoSpaceMenuStrings[2])
            {
                // Export the ADF into application package folder and update the Listview.
                ExportAdf(mTangoSpaceAdfDataList[index].Uuid);
            }
            else if (itemName == mAppSpaceMenuStrings[0])
            {
                // Delete an ADF from App space and update the App space ADF Listview.
                DeleteAdfFromAppSpace(mAppSpaceAdfDataList[index].Uuid);
            }
            else if (itemName == mAppSpaceMenuStrings[1])
            {
                // Import an ADF from app space to Tango space.
                ImportAdf(mAppSpaceAdfDataList[index].Uuid);
            }

            UpdateList();
            return true;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // Check which request we're responding to
            if (requestCode == Tango.TangoIntentActivitycode)
            {
                // Make sure the request was successful
                if (resultCode == Result.Canceled)
                {
                    Toast.MakeText(this, Resource.String.no_permissions, ToastLength.Long).Show();
                }
            }
            UpdateList();
        }

        void SetAdfNameDialog.ICallbackListener.OnAdfNameOk(string name, string uuid)
        {
            if (!mIsTangoReady)
            {
                Toast.MakeText(this, Resource.String.tango_not_ready, ToastLength.Short).Show();
                return;
            }
            var metadata = mTango.LoadAreaDescriptionMetaData(uuid);
            var adfNameBytes = metadata.Get(TangoAreaDescriptionMetaData.KeyName);
            if (adfNameBytes != Encoding.Default.GetBytes(name))
            {
                metadata.Set(TangoAreaDescriptionMetaData.KeyName, Encoding.Default.GetBytes(name));
            }
            mTango.SaveAreaDescriptionMetadata(uuid, metadata);
            UpdateList();
        }

        void SetAdfNameDialog.ICallbackListener.OnAdfNameCancelled()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Import an ADF from app space to Tango space.
        /// </summary>
        private void ImportAdf(string uuid)
        {
            try
            {
                mTango.ImportAreaDescriptionFile(Path.Combine(mAppSpaceAdfFolder, uuid));
            }
            catch (TangoErrorException ex)
            {
                Toast.MakeText(this, Resource.String.adf_exists_api_space, ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Export an ADF from Tango space to app space.
        /// </summary>
        private void ExportAdf(string uuid)
        {
            try
            {
                mTango.ExportAreaDescriptionFile(uuid, mAppSpaceAdfFolder);
            }
            catch (TangoErrorException ex)
            {
                Toast.MakeText(this, Resource.String.adf_exists_app_space, ToastLength.Short).Show();
            }
        }

        public void DeleteAdfFromTangoSpace(string uuid)
        {
            try
            {
                mTango.DeleteAreaDescription(uuid);
            }
            catch (TangoErrorException ex)
            {
                Toast.MakeText(this, Resource.String.no_uuid_tango_error, ToastLength.Short).Show();
            }
        }

        private void DeleteAdfFromAppSpace(string uuid)
        {
            File.Delete(Path.Combine(mAppSpaceAdfFolder, uuid));
        }

        /// <summary>
        /// Returns maps storage location in the App package folder. Creates a folder called Maps, if it
        /// does not exist.
        /// </summary>
        private string GetAppSpaceAdfFolder()
        {
            var mapsFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Maps");
            if (!Directory.Exists(mapsFolder))
            {
                Directory.CreateDirectory(mapsFolder);
            }
            return mapsFolder;
        }

        /// <summary>
        /// Updates the list of AdfData corresponding to the App space.
        /// </summary>
        private void UpdateAppSpaceAdfList()
        {
            var adfFileList = Directory.GetFiles(mAppSpaceAdfFolder);
            mAppSpaceAdfDataList.Clear();

            for (int i = 0; i < adfFileList.Length; ++i)
            {
                mAppSpaceAdfDataList.Add(new AdfData(Path.GetFileName(adfFileList[i]), ""));
            }
        }

        /// <summary>
        /// Updates the list of AdfData corresponding to the Tango space.
        /// </summary>
        private void UpdateTangoSpaceAdfList()
        {
            var metadata = new TangoAreaDescriptionMetaData();

            try
            {
                // Get all ADF UUIDs.
                var fullUuidList = mTango.ListAreaDescriptions();
                // Get the names from the UUIDs.
                mTangoSpaceAdfDataList.Clear();
                foreach (var uuid in fullUuidList)
                {
                    try
                    {
                        metadata = mTango.LoadAreaDescriptionMetaData(uuid);
                    }
                    catch (TangoErrorException ex)
                    {
                        Toast.MakeText(this, Resource.String.tango_error, ToastLength.Short).Show();
                    }
                    var name = Encoding.Default.GetString(metadata.Get(TangoAreaDescriptionMetaData.KeyName));
                    mTangoSpaceAdfDataList.Add(new AdfData(uuid, name));
                }
            }
            catch (TangoErrorException ex)
            {
                Toast.MakeText(this, Resource.String.tango_error, ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Updates the list of AdfData from Tango and App space, and sets it to the adapters.
        /// </summary>
        private void UpdateList()
        {
            // Update App space ADF Listview.
            UpdateAppSpaceAdfList();
            mAppSpaceAdfListAdapter.SetAdfData(mAppSpaceAdfDataList);
            mAppSpaceAdfListAdapter.NotifyDataSetChanged();

            // Update Tango space ADF Listview.
            UpdateTangoSpaceAdfList();
            mTangoSpaceAdfListAdapter.SetAdfData(mTangoSpaceAdfDataList);
            mTangoSpaceAdfListAdapter.NotifyDataSetChanged();
        }

        private void ShowSetNameDialog(string mCurrentUuid)
        {
            if (!mIsTangoReady)
            {
                Toast.MakeText(this, Resource.String.tango_not_ready, ToastLength.Short).Show();
                return;
            }

            var bundle = new Bundle();
            var metaData = mTango.LoadAreaDescriptionMetaData(mCurrentUuid);
            var adfNameBytes = metaData.Get(TangoAreaDescriptionMetaData.KeyName);
            if (adfNameBytes != null)
            {
                var fillDialogName = Encoding.Default.GetString(adfNameBytes);
                bundle.PutString(TangoAreaDescriptionMetaData.KeyName, fillDialogName);
            }
            bundle.PutString(TangoAreaDescriptionMetaData.KeyUuid, mCurrentUuid);
            var setAdfNameDialog = new SetAdfNameDialog();
            setAdfNameDialog.Arguments = bundle;
            setAdfNameDialog.Show(FragmentManager, "ADFNameDialog");
        }
    }
}
