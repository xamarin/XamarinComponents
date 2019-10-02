using System;

using UIKit;
using Foundation;

using MaterialComponents;
using CoreGraphics;

namespace MaterialSample {
	public partial class ActivityIndicatorViewController : UITableViewController, IActivityIndicatorDelegate {
		#region Cell Identifier

		static readonly NSString cellKey = new NSString (nameof (UITableViewCell));

		#endregion

		#region Class Variables

		ActivityIndicator activityIndicator;
		UISlider sldProgress;
		UISwitch swtIndeterminate;
		UISwitch swtHide;
		UISwitch swtColors;

		SemanticColorScheme semanticColorScheme;
		UIColor [] cycleColors;
		float initialProgressValue = 0.6f;

		#endregion

		#region Constructors

		public ActivityIndicatorViewController (string title) : base (UITableViewStyle.Plain) => Title = title;

		#endregion

		#region Controller Life Cycle

		public override void ViewWillAppear (bool animated)
		{
			InitializeComponents ();

			base.ViewWillAppear (animated);
		}

		#endregion

		#region User Interactions

		void SldProgress_ValueChanged (object sender, EventArgs e)
		{
			activityIndicator.Progress = sldProgress.Value;
			var cell = TableView.CellAt (NSIndexPath.FromRowSection (1, 0));
			cell.TextLabel.Text = sldProgress.Value.ToString ("P");
		}

		void SwtIndeterminate_ValueChanged (object sender, EventArgs e)
		{
			activityIndicator.IndicatorMode = swtIndeterminate.On ? ActivityIndicatorMode.Indeterminate : ActivityIndicatorMode.Determinate;
			swtColors.Enabled = swtIndeterminate.On;
		}

		void SwtHide_ValueChanged (object sender, EventArgs e)
		{
			if (swtHide.On) activityIndicator.StopAnimating ();
			else activityIndicator.StartAnimating ();
		}

		void SwtColors_ValueChanged (object sender, EventArgs e)
		{
			activityIndicator.CycleColors = swtColors.On ? cycleColors : new [] { UIColor.Black };
		}

		#endregion

		#region Internal Functionality

		void InitializeComponents ()
		{

			TableView.RegisterClassForCellReuse (typeof (UITableViewCell), cellKey);

			semanticColorScheme = new SemanticColorScheme ();

			activityIndicator = new ActivityIndicator {
				Delegate = this,
				IndicatorMode = ActivityIndicatorMode.Determinate,
				CycleColors = new [] { UIColor.Black },
				Progress = initialProgressValue,
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			activityIndicator.SizeToFit ();
			activityIndicator.StartAnimating ();
			ActivityIndicatorColorThemer.ApplySemanticColorScheme (semanticColorScheme, activityIndicator);

			sldProgress = new UISlider (new CGRect (0, 0, 160, 27)) { Value = initialProgressValue };
			sldProgress.ValueChanged += SldProgress_ValueChanged;

			swtIndeterminate = new UISwitch { On = false };
			swtIndeterminate.ValueChanged += SwtIndeterminate_ValueChanged;

			swtHide = new UISwitch { On = false };
			swtHide.ValueChanged += SwtHide_ValueChanged;

			swtColors = new UISwitch { On = false, Enabled = false };
			swtColors.ValueChanged += SwtColors_ValueChanged;

			cycleColors = new [] { UIColor.Red, UIColor.Green, UIColor.Blue, UIColor.Yellow };
		}

		void AddConstraints (UIView parent)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				activityIndicator.CenterXAnchor.ConstraintEqualTo (parent.CenterXAnchor).Active = true;
				activityIndicator.CenterYAnchor.ConstraintEqualTo (parent.CenterYAnchor).Active = true;
			} else {
				var centerXAnchor = NSLayoutConstraint.Create (activityIndicator, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, parent, NSLayoutAttribute.CenterX, 1, 0);
				var centerYAnchor = NSLayoutConstraint.Create (activityIndicator, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, parent, NSLayoutAttribute.CenterY, 1, 0);
				NSLayoutConstraint.ActivateConstraints (new [] { centerXAnchor, centerYAnchor });
			}
		}

		#endregion

		#region UITableView Data Source

		public override nint NumberOfSections (UITableView tableView) => 1;
		public override nint RowsInSection (UITableView tableView, nint section) => 5;

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (cellKey, indexPath);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			switch (indexPath.Row) {
			case 0:
				cell.AccessoryView = null;
				cell.TextLabel.Text = null;
				cell.ContentView.AddSubview (activityIndicator);
				AddConstraints (cell.ContentView);
				break;
			case 1:
				cell.TextLabel.Text = "Progress";
				cell.AccessoryView = sldProgress;
				break;
			case 2:
				cell.TextLabel.Text = "Indeterminate";
				cell.AccessoryView = swtIndeterminate;
				break;
			case 3:
				cell.TextLabel.Text = "Hide";
				cell.AccessoryView = swtHide;
				break;
			case 4:
				cell.TextLabel.Text = "Colors";
				cell.AccessoryView = swtColors;
				break;
			}

			return cell;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return indexPath.Row == 0 ? 100 : 56;
		}

		#endregion

		#region ActivityIndicator Delegate

		[Export ("activityIndicatorAnimationDidFinish:")]
		public void AnimationDidFinish (ActivityIndicator activityIndicator)
		{
			AppDelegate.LogMessage ("Animation finished.", nameof (ActivityIndicatorViewController));
		}

		[Export ("activityIndicatorModeTransitionDidFinish:")]
		void ModeTransitionDidFinish (ActivityIndicator activityIndicator)
		{
			AppDelegate.LogMessage ("Mode Transition finished.", nameof (ActivityIndicatorViewController));
		}

		#endregion
	}
}

