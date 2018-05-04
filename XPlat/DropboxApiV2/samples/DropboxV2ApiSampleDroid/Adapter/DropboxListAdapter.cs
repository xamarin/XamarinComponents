using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using Dropbox.Api;
using Xamarin.Dropbox.Api;
using Xamarin.Dropbox.Api.Android;
using Xamarin.Dropbox.Api.Enums;

namespace DropboxV2ApiSampleDroid.Adapter
{
	public class DropboxListAdapter : BaseAdapter<string>
	{
		List<DropBoxItem> fileNames = new List<DropBoxItem>();
		Activity context;
        string currentPath;

        public List<DropBoxItem> FileNames
        {
            get 
            {
                return fileNames;
            }
        }
		public DropboxListAdapter(Activity context, string currentPath = "") : base() {
            this.context = context;
            this.currentPath = currentPath;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position]
		{
			get { return fileNames[position].Name; }
		}

		public override int Count
		{
			get { return fileNames.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = fileNames[position].Name;
			return view;
		}

        public async Task LoadFolderAsync()
        {
			fileNames.Clear();

			try
			{
                using (var client = DropBoxHelper.CreateDropboxClient())
				{
					var someFiles = await client.Files.ListFolderAsync(new Dropbox.Api.Files.ListFolderArg(currentPath)
					{

					});

					var sList = someFiles.Entries.OrderBy(x => x.Name);

					foreach (var item in sList)
					{
						var ffold = item.AsFolder;

						fileNames.Add(new DropBoxItem()
						{
							Name = item.Name,
							ItemType = (item.IsFolder) ? DropBoxItemType.Folder : DropBoxItemType.File,
							Path = item.PathLower,
						});

					}
				}


			}
			catch (HttpException e)
			{
				Console.WriteLine("Exception reported from RPC layer");
				Console.WriteLine("    Status code: {0}", e.StatusCode);
				Console.WriteLine("    Message    : {0}", e.Message);
				if (e.RequestUri != null)
				{
					Console.WriteLine("    Request uri: {0}", e.RequestUri);
				}
			}
        }
	}
}
