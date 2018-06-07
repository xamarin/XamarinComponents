using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using DropboxV2ApiSampleiOS.Data;
using DropboxV2ApiSampleiOS.Helpers;
using Foundation;
using UIKit;
using Xamarin.Dropbox.Api.iOS;

namespace DropboxV2ApiSampleiOS.DataSources
{
	public class DropBoxFolderSource : UITableViewSource
	{
		private string currentPath;

		string CellIdentifier = "TableCell";

		List<DropBoxItem> fileNames = new List<DropBoxItem>();

		public Action<DropBoxItem> HandleSelection = delegate { };

		public DropBoxFolderSource(string path = "")
		{
			currentPath = path;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			var item = fileNames[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

			cell.Accessory = (item.ItemType == DropBoxItemType.Folder) ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.None;
			cell.TextLabel.Text = item.Name;

			return cell;

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return fileNames.Count;
		}

		public async Task FetchFolderItems()
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

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);

			var item = fileNames[indexPath.Row];

			HandleSelection(item);


		}
	}
}
