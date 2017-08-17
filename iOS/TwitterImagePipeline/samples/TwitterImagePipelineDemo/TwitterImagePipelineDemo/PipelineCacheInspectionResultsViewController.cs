using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class PipelineCacheInspectionResultsViewController : UITableViewController
	{
		private readonly List<ITIPImagePipelineInspectionResultEntry> results;
		private readonly TIPImagePipeline pipeline;

		public PipelineCacheInspectionResultsViewController(ITIPImagePipelineInspectionResultEntry[] entries, TIPImagePipeline imagePipeline)
			: base(UITableViewStyle.Grouped)
		{
			results = entries.ToList();
			pipeline = imagePipeline;
		}

		public bool DidClearAnyEntries { get; private set; }

		public override string TitleForHeader(UITableView tableView, nint section)
			=> $"{results.Count} Entries (tap to remove)";

		public override nint NumberOfSections(UITableView tableView) => 1;

		public override nint RowsInSection(UITableView tableView, nint section) => results.Count;

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell("EntryCell");
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "EntryCell");
				cell.TextLabel.Lines = 1;
				cell.TextLabel.LineBreakMode = UILineBreakMode.HeadTruncation;
				cell.DetailTextLabel.Lines = 3;
			}

			var entry = results[indexPath.Row];
			cell.ImageView.Image = entry.Image;
			cell.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

			var percent = (entry.Progress < 1f) ? $"({entry.Progress * 100}%) " : string.Empty;
			cell.TextLabel.Text = entry.Identifier;
			cell.DetailTextLabel.Text =
				$"{entry.Dimensions.Width}x{entry.Dimensions.Height}\n" +
				$"{percent}{NSByteCountFormatter.Format((long)entry.BytesUsed, NSByteCountFormatterCountStyle.Binary)}";

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => 100;

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			TableView.DeselectRow(indexPath, true);

			var index = indexPath.Row;
			var entry = results[index];

			var alertVC = UIAlertController.Create("Clear entry?", entry.Identifier, UIAlertControllerStyle.ActionSheet);
			alertVC.AddAction(UIAlertAction.Create("Clear", UIAlertActionStyle.Destructive, delegate
			{
				DidClearAnyEntries = true;
				pipeline.ClearImage(entry.Identifier);
				results.RemoveAt(index);
				TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
				TableView.GetHeaderView(0).TextLabel.Text = TitleForHeader(TableView, 0);
			}));
			alertVC.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

			PresentViewController(alertVC, true, null);
		}
	}
}