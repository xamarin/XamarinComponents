using System;
using System.Linq;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class InspectorViewController : UITableViewController
	{
		private NSUuid inspectionUuid;
		private NSDictionary<NSString, TIPImagePipelineInspectionResult> results;
		private string[] pipelines;

		public InspectorViewController()
		{
			NavigationItem.Title = "Cache Inspector";
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var id = new NSUuid();
			inspectionUuid = id;

			TableView.ReloadData();

			var r = await TIPGlobalConfiguration.SharedInstance.InspectAsync();
			if (inspectionUuid == id)
			{
				inspectionUuid = null;
				results = r;
				pipelines = results.Keys.Select(k => k.ToString()).OrderBy(k => k).ToArray();

				TableView.ReloadData();
			}
		}

		public override nint NumberOfSections(UITableView tableView) => 2; // 1 for the pipelines, 1 for the data usages

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (0 == section)
			{
				return (nint)Math.Max(1, results?.Count ?? 0);
			}
			else
			{
				return 3; // disk, memory, rendered
			}
		}

		public override string TitleForHeader(UITableView tableView, nint section)
			=> (0 == section) ? "Pipelines" : "Cache Usage (tap to clear)";

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (0 == indexPath.Section)
			{
				if (results == null)
				{
					return CreatePlainTextCell("Loading...");
				}
				else if (results.Count == 0)
				{
					return CreatePlainTextCell("No Pipelines");
				}
				else
				{
					return CreatePipelineCell(indexPath.Row);
				}
			}
			else
			{
				return CreateCacheCell(indexPath.Row);
			}
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			TableView.DeselectRow(indexPath, true);

			if (0 == indexPath.Section)
			{
				if (results.Count > 0)
				{
					PipelineSelected(indexPath.Row);
				}
			}
			else
			{
				CacheSelected(indexPath.Row);
			}
		}

		private UITableViewCell CreatePlainTextCell(string text)
		{
			var cell = TableView.DequeueReusableCell("TextCell") ?? new UITableViewCell(UITableViewCellStyle.Default, "TextCell");
			cell.TextLabel.Text = text;
			return cell;
		}

		private UITableViewCell CreateChevronTextCell(string text)
		{
			var cell = TableView.DequeueReusableCell("ChevronCell") ?? new UITableViewCell(UITableViewCellStyle.Default, "ChevronCell");
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			cell.TextLabel.Text = text;
			return cell;
		}

		private UITableViewCell CreatePipelineCell(int index)
		{
			return CreateChevronTextCell(pipelines[index]);
		}

		private UITableViewCell CreateCacheCell(int index)
		{
			string text = null;
			if (0 == index)
			{
				text = string.Format(
					"Rendered Cache: {0} / {1}",
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.TotalBytesForAllRenderedCaches, NSByteCountFormatterCountStyle.Binary),
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.MaxBytesForAllRenderedCaches, NSByteCountFormatterCountStyle.Binary));
			}
			else if (1 == index)
			{
				text = string.Format(
					"Memory Cache: {0} / {1}",
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.TotalBytesForAllMemoryCaches, NSByteCountFormatterCountStyle.Binary),
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.MaxBytesForAllMemoryCaches, NSByteCountFormatterCountStyle.Binary));
			}
			else
			{
				text = string.Format(
					"Disk Cache: {0} / {1}",
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.TotalBytesForAllDiskCaches, NSByteCountFormatterCountStyle.Binary),
					NSByteCountFormatter.Format(TIPGlobalConfiguration.SharedInstance.MaxBytesForAllDiskCaches, NSByteCountFormatterCountStyle.Binary));
			}
			return CreateChevronTextCell(text);
		}

		private void CacheSelected(int index)
		{
			var cacheTypeName = (0 == index || 1 == index) ? "rendered & memory" : "disk";
			var alertVC = UIAlertController.Create(
				$"Clear {cacheTypeName} caches?",
				$"Would you like to remove all cached entries from all {cacheTypeName} caches?",
				UIAlertControllerStyle.ActionSheet);
			alertVC.AddAction(UIAlertAction.Create("Clear them!", UIAlertActionStyle.Destructive, delegate
			{
				if (0 == index || 1 == index)
				{
					TIPGlobalConfiguration.SharedInstance.ClearAllMemoryCaches();
				}
				else
				{
					TIPGlobalConfiguration.SharedInstance.ClearAllDiskCaches();
				}
				TableView.ReloadData();
			}));
			alertVC.AddAction(UIAlertAction.Create("Never mind", UIAlertActionStyle.Cancel, null));
			PresentViewController(alertVC, true, null);
		}

		private void PipelineSelected(int index)
		{
			var result = (TIPImagePipelineInspectionResult)results[pipelines[index]];
			if (result != null)
			{
				var vc = new PipelineInspectorViewController(result);
				NavigationController.PushViewController(vc, true);
			}
		}
	}
}
