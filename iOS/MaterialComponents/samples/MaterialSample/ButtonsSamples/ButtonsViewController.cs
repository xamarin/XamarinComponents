using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using MaterialComponents;
using System.Threading.Tasks;

namespace MaterialSample {
	public partial class ButtonsViewController : UITableViewController {
		#region Class Variables

		UIBarButtonItem btnEnable;

		SemanticColorScheme semanticColorScheme;
		TypographyScheme typographyScheme;
		ButtonScheme buttonScheme;

		Dictionary<string, Button> buttons;
		string [] buttonsKeys;

		enum ButtonType {
			Contained,
			Text,
			Outlined,
			FloatingAction
		}

		#endregion

		#region Constructors

		public ButtonsViewController (string title) : base (UITableViewStyle.Plain) => Title = title;

		#endregion

		#region Controller Life Cycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			TableView.RegisterClassForCellReuse (typeof (ButtonTableViewCell), ButtonTableViewCell.Id);
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.AllowsSelection = false;
			TableView.RowHeight = 56;

			InitializeComponents ();
		}

		#endregion

		#region User Interactions

		void Button_TouchUpInside (object sender, EventArgs e)
		{
			AppDelegate.LogMessage ("Auch!!! >.<", nameof (ButtonsViewController));

			if (sender is FloatingButton floatingButton)
				floatingButton.Collapse (true, HandleAction);

			async void HandleAction ()
			{
				await Task.Delay (1000);
				InvokeOnMainThread (() => floatingButton.Expand (true, null));
			}
		}

		void BtnEnable_Clicked (object sender, EventArgs e)
		{
			btnEnable.Title = btnEnable.Title == "Disable Buttons" ? "Enable Buttons" : "Disable Buttons";

			foreach (var button in buttons)
				button.Value.Enabled = !button.Value.Enabled;
		}

		#endregion

		#region Internal Functionality

		void InitializeComponents ()
		{
			btnEnable = new UIBarButtonItem ("Disable Buttons", UIBarButtonItemStyle.Done, BtnEnable_Clicked);
			NavigationItem.RightBarButtonItem = btnEnable;

			semanticColorScheme = new SemanticColorScheme ();
			typographyScheme = new TypographyScheme ();
			buttonScheme = new ButtonScheme {
				ColorScheme = semanticColorScheme,
				TypographyScheme = typographyScheme
			};

			buttons = new Dictionary<string, Button> {
				{ "Contained", CreateButton ("Button", ButtonType.Contained) },
				{ "Text", CreateButton ("Button", ButtonType.Text) },
				{ "Outlined", CreateButton ("Button", ButtonType.Outlined) },
				{ "Floating Action", CreateButton ("+", ButtonType.FloatingAction) }
			};

			buttonsKeys = buttons.Keys.ToArray ();
		}

		Button CreateButton (string title, ButtonType type, bool enabled = true)
		{
			var button = type != ButtonType.FloatingAction ? new Button { Enabled = true } : new FloatingButton ();
			button.TranslatesAutoresizingMaskIntoConstraints = false;
			button.SetTitle (title, UIControlState.Normal);
			button.SizeToFit ();
			button.TouchUpInside += Button_TouchUpInside;

			switch (type) {
			case ButtonType.Contained:
				ContainedButtonThemer.ApplyScheme (buttonScheme, button);
				break;
			case ButtonType.Text:
				TextButtonThemer.ApplyScheme (buttonScheme, button);
				break;
			case ButtonType.Outlined:
				OutlinedButtonThemer.ApplyScheme (buttonScheme, button);
				break;
			case ButtonType.FloatingAction:
				FloatingActionButtonThemer.ApplyScheme (buttonScheme, (FloatingButton)button);
				break;
			}

			return button;
		}

		#endregion

		#region UITableView Data Source

		public override nint NumberOfSections (UITableView tableView) => 1;
		public override nint RowsInSection (UITableView tableView, nint section) => buttons.Count;

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var title = buttonsKeys [indexPath.Row];
			var button = buttons [title];

			var cell = tableView.DequeueReusableCell (ButtonTableViewCell.Id, indexPath) as ButtonTableViewCell;
			cell.TextLabel.Text = title;
			cell.TextLabel.Font = Typography.CaptionFont;
			cell.TextLabel.Alpha = Typography.CaptionFontOpacity;
			cell.Button = button;

			return cell;
		}

		#endregion
	}

	public class CustomSwitch : UISwitch {
		public NSIndexPath IndexPath { get; set; }
	}

	public class ButtonTableViewCell : UITableViewCell {
		#region Cell Identifier

		public static NSString Id { get; } = new NSString (nameof (ButtonTableViewCell));

		#endregion

		#region Class Variables

		static readonly nfloat offset = 16; 

		#endregion

		#region Properties

		Button button;
		public Button Button {
			get => button;
			set {
				RemoveButton ();
				button = value;
				ContentView.AddSubview (button);
				AddButtonConstraints ();
			}
		}

		#endregion

		#region Constructors

		public ButtonTableViewCell (IntPtr handle) : base (handle) { }

		#endregion

		#region Cell Life Cycle

		public override void PrepareForReuse ()
		{
			base.PrepareForReuse ();

			RemoveButton ();
		}

		#endregion

		#region Internal Functionality

		void AddButtonConstraints ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				button.CenterYAnchor.ConstraintEqualTo (ContentView.CenterYAnchor, 0).Active = true;
				button.TrailingAnchor.ConstraintEqualTo (ContentView.TrailingAnchor, -offset).Active = true;
			} else {
				var centerYAnchor = NSLayoutConstraint.Create (button, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0);
				var trailingAnchor = NSLayoutConstraint.Create (button, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Trailing, 1, -offset);
				NSLayoutConstraint.ActivateConstraints (new [] { centerYAnchor, trailingAnchor });
			}
		}

		void RemoveButton ()
		{
			button?.RemoveFromSuperview ();
		}

		#endregion
	}
}

