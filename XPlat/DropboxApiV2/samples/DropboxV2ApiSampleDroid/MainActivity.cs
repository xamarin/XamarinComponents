using Android.App;
using Android.Widget;
using Android.OS;
using DropboxV2ApiSampleDroid.Adapter;
using Android.Content;
using Xamarin.Dropbox.Api.Android;
using Xamarin.Dropbox.Api.Enums;
using Xamarin.Dropbox.Api.Core.Data;

namespace DropboxV2ApiSampleDroid
{
	[Activity(Label = "Dropbox v2 API Sample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : ListActivity
	{

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            DropBoxConfig.Configure()
                         .SetAppName("SimpleTestApp")
                         .SetAuthorizeUrl("https://www.dropbox.com/1/oauth2/authorize")
                         .SetApiKey("")
                         .SetRedirectUri("https://xamarin.com")
                         .SetTokenAppName("DropBox");

            //you can access directly as well
            //DropBoxConfig.Instance.ApiKey = "";

            try
            {
                if (string.IsNullOrWhiteSpace((DropBoxConfig.Instance.ApiKey)))
                    throw new System.Exception("You must enter an ApiKey");

                ListView.Adapter = new DropboxListAdapter(this);
                ListView.FastScrollEnabled = true;

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
                        //once authenticated load the folder contents
                        await ((DropboxListAdapter)ListView.Adapter).LoadFolderAsync();

                        //authenticated so refresh the adapter
                        ((DropboxListAdapter)ListView.Adapter).NotifyDataSetChanged();
                    });

                    authHelp.PresentAuthController(this);
                }
            }
            catch (System.Exception ex)
            {
                ShowMessage("Error", ex.Message);
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

        protected void ShowMessage(string title, string message, bool toast = false)
        {
            if (toast == true)
            {
                Toast.MakeText
                        (
                            this,
                            message,
                            ToastLength.Long
                        ).Show();
            }
            else
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle(title);
                builder.SetMessage(message);
                builder.SetPositiveButton("Ok", (o, e) => { });
                builder.Create().Show();
            }

        }
	}
}

