using System;
using System.CodeDom.Compiler;
using Foundation;
using UIKit;

using JDStatusBarNotification;

namespace JDStatusBarNotificationSample
{
	partial class MainViewController : UITableViewController
	{
		private const string CustomStyle1 = "style-one";
		private const string CustomStyle2 = "style-two";

		private const double ProgressInterval = 0.03;
		private const float ProgressStep = 0.01f;
		private const double DismissalDelay = 3.0;

		private bool autoDismiss = false;
		private NSTimer timer;
		private UIActivityIndicatorViewStyle indicatorStyle = UIActivityIndicatorViewStyle.Gray;

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// presenting a notification, before a keyWindow is set
			StatusBarNotification.Show ("Hello World!", 2.0, StatusBarStyles.Matrix);

			// create custom styles
			StatusBarNotification.AddStyle (CustomStyle1, style => {
				style.BarColor = new UIColor (0.797f, 0.000f, 0.662f, 1.000f);
				style.TextColor = UIColor.White;
				style.AnimationType = StatusBarAnimationType.Fade;
				style.Font = UIFont.FromName ("SnellRoundhand-Bold", 17.0f);
				style.ProgressBarColor = new UIColor (0.986f, 0.062f, 0.598f, 1.000f);
				style.ProgressBarHeight = 20.0f;
				return style;
			});
			StatusBarNotification.AddStyle (CustomStyle2, style => {
				style.BarColor = UIColor.Cyan;
				style.TextColor = new UIColor (0.056f, 0.478f, 0.478f, 1.000f);
				style.AnimationType = StatusBarAnimationType.Bounce;
				style.ProgressBarColor = style.TextColor;
				style.ProgressBarHeight = 5.0f;
				style.ProgressBarPosition = StatusBarProgressBarPosition.Top;
				if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
					style.Font = UIFont.FromName ("DINCondensed-Bold", 17.0f);
					style.TextVerticalPositionAdjustment = 2.0f;
				} else {
					style.Font = UIFont.FromName ("HelveticaNeue-CondensedBold", 17.0f);
				}
				return style;
			});
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);

			// cancel progress if we are going to show new or dismiss old
			if (indexPath.Section != 0 || indexPath.Row == 2) {
				CancelTimer ();
			}

			if (indexPath.Section == 0) {
				// current notification controls
				if (indexPath.Row == 0) {
					// show progress
					if (!StatusBarNotification.IsVisible) {
						indicatorStyle = UIActivityIndicatorViewStyle.Gray;
						ShowStatus (StatusBarStyles.Default, "Some Progress...");
					}
					StartTimer ();
				} else if (indexPath.Row == 1) {
					// show activity
					if (!StatusBarNotification.IsVisible) {
						indicatorStyle = UIActivityIndicatorViewStyle.Gray;
						ShowStatus (StatusBarStyles.Default, "Some Activity...");
					}
					StatusBarNotification.ShowActivityIndicator (true, indicatorStyle);
				} else if (indexPath.Row == 2) {
					// dismiss
					StatusBarNotification.Dismiss (true);
				} else if (indexPath.Row == 3) {
					// auto dismiss
					autoDismiss = !autoDismiss;
					var cell = tableView.CellAt (indexPath);
					cell.Accessory = autoDismiss 
						? UITableViewCellAccessory.Checkmark 
						: UITableViewCellAccessory.None;
				}
			} else if (indexPath.Section == 1) {
				// default notification styles
				indicatorStyle = UIActivityIndicatorViewStyle.Gray;
				var style = StatusBarStyles.Default;
				var message = "Better call Saul!";
				if (indexPath.Row == 0) {
					// default style
				} else if (indexPath.Row == 1) {
					// success style
					indicatorStyle = UIActivityIndicatorViewStyle.White;
					style = StatusBarStyles.Success;
					message = "That's how we roll!";
				} else if (indexPath.Row == 2) {
					// error style
					indicatorStyle = UIActivityIndicatorViewStyle.White;
					style = StatusBarStyles.Error;
					message = "No, I don't have the money...";
				} else if (indexPath.Row == 3) {
					// warning style
					indicatorStyle = UIActivityIndicatorViewStyle.Gray;
					style = StatusBarStyles.Warning;
					message = "You know who I am!";
				} else if (indexPath.Row == 4) {
					// dark style
					indicatorStyle = UIActivityIndicatorViewStyle.White;
					style = StatusBarStyles.Dark;
					message = "Don't mess with me!";
				} else if (indexPath.Row == 5) {
					// matrix style
					indicatorStyle = UIActivityIndicatorViewStyle.White;
					style = StatusBarStyles.Matrix;
					message = "Wake up Neo...";
				}
				// show
				ShowStatus (style, message);
			} else if (indexPath.Section == 2) {
				// custom notification styles
				if (indexPath.Row == 0) {
					// custom style 1
					indicatorStyle = UIActivityIndicatorViewStyle.White;
					ShowStatus (CustomStyle1, "Oh, I love it!");
				} else if (indexPath.Row == 1) {
					// custom style 2
					indicatorStyle = UIActivityIndicatorViewStyle.Gray;
					ShowStatus (CustomStyle2, "Level up!");
				}
			}
		}

		private void ShowStatus (string style, string message)
		{
			if (autoDismiss) {
				StatusBarNotification.Show (message, DismissalDelay, style);
			} else {
				StatusBarNotification.Show (message, style);
			}
		}

		private void StartTimer ()
		{
			var progress = 0.0f;
			timer = NSTimer.CreateRepeatingScheduledTimer (ProgressInterval, t => {
				if (progress < 1.0) {
					progress += ProgressStep;
					StatusBarNotification.ShowProgress (progress);
				} else {
					Invoke (() => StatusBarNotification.ShowProgress (0.0f), 1.0);
					t.Invalidate ();
				}
			});
		}

		private void CancelTimer ()
		{
			if (timer != null && timer.IsValid) {
				timer.Invalidate ();
			}
		}
	}
}
