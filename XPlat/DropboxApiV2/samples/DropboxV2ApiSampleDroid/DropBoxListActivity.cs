using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DropboxV2ApiSampleDroid.Adapter;
using Xamarin.Dropbox.Api.Android;
using Xamarin.Dropbox.Api.Enums;

namespace DropboxV2ApiSampleDroid
{
    [Activity(Label = "DropBoxListActivity")]
	public class DropBoxListActivity : ListActivity
	{

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            string currentPath = Intent.GetStringExtra("DBPath") ?? "";

            this.Title = currentPath;

			ListView.Adapter = new DropboxListAdapter(this, currentPath);
			ListView.FastScrollEnabled = true;

            try
            {
                if (DropBoxHelper.IsAuthenticated)
                {
                    //already authenticated so no need to do anything at this point
                    await ((DropboxListAdapter)ListView.Adapter).LoadFolderAsync();

                    //authenticated so refresh the adapter
                    ((DropboxListAdapter)ListView.Adapter).NotifyDataSetChanged();
                }
                else
                {
                    //setup a new dropbox helper and set a handler for after being authenticated
                    var authHelp = new DropBoxHelper(async () =>
                    {
                        await ((DropboxListAdapter)ListView.Adapter).LoadFolderAsync();

                        //authenticated so refresh the adapter
                        ((DropboxListAdapter)ListView.Adapter).NotifyDataSetChanged();
                    });

                    //show the authenticator
                    authHelp.PresentAuthController(this);
                }
            }
            catch (System.Exception ex)
            {

            }


		}

		protected override void OnListItemClick(ListView l, Android.Views.View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			var ars = ((DropboxListAdapter)ListView.Adapter).FileNames[position];

			if (ars.ItemType == DropBoxItemType.Folder)
			{
				var activity2 = new Intent(this, typeof(DropBoxListActivity));
				activity2.PutExtra("DBPath", ars.Path);
				StartActivity(activity2);
			}

		}
	}
}
