using System;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class PipelineInspectorViewController : UITableViewController
	{
		private readonly TIPImagePipelineInspectionResult result;
		private bool shouldAutoPop;
		private PipelineCacheInspectionResultsViewController presentedResults;

		public PipelineInspectorViewController(TIPImagePipelineInspectionResult inspectionResult)
		{
			result = inspectionResult;

			NavigationItem.Title = result.ImagePipeline.Identifier;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (presentedResults != null)
			{
				shouldAutoPop = presentedResults.DidClearAnyEntries;
				presentedResults = null;
			}
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (shouldAutoPop)
			{
				NavigationController.PopToRootViewController(true);
			}
		}

		public override nint NumberOfSections(UITableView tableView) => 1;

		public override nint RowsInSection(UITableView tableView, nint section) => 4;

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("EntryGroupCell");
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, "EntryGroupCell");
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}

			switch (indexPath.Row)
			{
				case 0:
					cell.TextLabel.Text = $"Rendered Entries ({result.CompleteRenderedEntries.Length})";
					break;
				case 1:
					cell.TextLabel.Text = $"Memory Entries ({result.CompleteMemoryEntries.Length})";
					break;
				case 2:
					cell.TextLabel.Text = $"Incomplete Disk Entries ({result.PartialDiskEntries.Length})";
					break;
				case 3:
				default:
					cell.TextLabel.Text = $"Complete Disk Entries ({result.CompleteDiskEntries.Length})";
					break;
			}

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			TableView.DeselectRow(indexPath, true);

			ITIPImagePipelineInspectionResultEntry[] entries = null;
			string name = null;
			switch (indexPath.Row)
			{
				case 0:
					entries = result.CompleteRenderedEntries;
					name = @"Rendered";
					break;
				case 1:
					entries = result.CompleteMemoryEntries;
					name = @"Memory";
					break;
				case 2:
					entries = result.PartialDiskEntries;
					name = @"Incomplete Disk";
					break;
				case 3:
				default:
					entries = result.CompleteDiskEntries;
					name = @"Complete Disk";
					break;
			}

			presentedResults = new PipelineCacheInspectionResultsViewController(entries, result.ImagePipeline);
			presentedResults.NavigationItem.Title = name;
			NavigationController.PushViewController(presentedResults, true);
		}
	}
}
