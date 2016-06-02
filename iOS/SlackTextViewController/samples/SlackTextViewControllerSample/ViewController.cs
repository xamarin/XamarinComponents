using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

using SlackHQ;

namespace SlackTextViewControllerSample
{
	public partial class ViewController : SlackTextViewController
	{
		private const string MessengerCellIdentifier = "MessengerCell";
		private const string AutoCompletionCellIdentifier = "AutoCompletionCell";

		private readonly static Random random;

		private readonly static string CurrentUsername;
		private readonly static UIColor CurrentProfileColor;

		private readonly static UIColor[] colors;
		private readonly static string[] users;
		private readonly static string[] channels;
		private readonly static string[] emojis;
		private readonly static string[] words;

		static ViewController ()
		{
			random = new Random ();

			users = new [] { "Allen", "Anna", "Alicia", "Arnold", "Armando", "Antonio", "Brad", "Catalaya", "Christoph", "Emerson", "Eric", "Everyone", "Steve" };
			colors = new UIColor [users.Length];
			channels = new [] { "General", "Random", "iOS", "Bugs", "Sports", "Android", "UI", "SSB" };
			emojis = new [] { "m", "man", "machine", "block-a", "block-b", "bowtie", "boar", "boat", "book", "bookmark", "neckbeard", "metal", "fu", "feelsgood" };
			words = new [] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

			CurrentUsername = "Xamarin";
			CurrentProfileColor = RandomColor (null);
		}

		private List<Message> messages = new List<Message> ();
		private string[] searchResult = new string[0];

		private	NSObject contentSizeObserver;

		public ViewController (IntPtr handle)
			: base (handle)
		{
			contentSizeObserver = NSNotificationCenter.DefaultCenter.AddObserver (UIApplication.ContentSizeCategoryChangedNotification, note => TableView.ReloadData ());

			// initial data
			for (int i = 0; i < 100; i++) {
				var username = RandomUser ();
				messages.Add (new Message {
					Username = username,
					Text = LoremIpsum (),
					ProfileColor = RandomColor (username)
				});
			}

			// Register a SlackTextView subclass, if you need any special appearance and/or behavior customisation.
			RegisterClassForTextView<MessageTextView> ();
		}

		[Preserve]
		[Export("tableViewStyleForCoder:")]
		private static UITableViewStyle GetTableViewStyleForCoder (NSCoder decoder)
		{
			return UITableViewStyle.Plain;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Bounces = true;
			ShakeToClearEnabled = true;
			KeyboardPanningEnabled = true;
			ShouldScrollToBottomAfterKeyboardShows = false;
			Inverted = true;

			LeftButton.SetImage (UIImage.FromBundle ("upload.png"), UIControlState.Normal);
			LeftButton.TintColor = UIColor.Gray;

			RightButton.SetTitle ("Send", UIControlState.Normal);

			TextInputbar.AutoHideRightButton = true;
			TextInputbar.MaxCharCount = 256;
			TextInputbar.CounterStyle = CounterStyle.Split;
			TextInputbar.CounterPosition = CounterPosition.Top;

			TextInputbar.EditorTitle.TextColor = UIColor.DarkGray;
			TextInputbar.EditorLeftButton.TintColor = UIColor.FromRGB (0, 122, 255);
			TextInputbar.EditorRightButton.TintColor = UIColor.FromRGB (0, 122, 255);

			TypingIndicatorView.CanResignByTouch = true;

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RegisterClassForCellReuse (typeof(MessageTableViewCell), MessengerCellIdentifier);

			AutoCompletionView.RegisterClassForCellReuse (typeof(MessageTableViewCell), AutoCompletionCellIdentifier);

			RegisterPrefixesForAutoCompletion (new []{ "@", "#", ":", "+:" });
		}

		partial void OnSimulateTyping (UIBarButtonItem sender)
		{
			if (CanShowTypingIndicator) {
				TypingIndicatorView.InsertUsername (RandomUser ());
			}
		}

		public override bool ForceTextInputbarAdjustmentForResponder (UIResponder responder)
		{
			// On iOS 9, returning YES helps keeping the input view visible when the keyboard if 
			// presented from another app when using multi-tasking on iPad.
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
		}

		public override void DidPressRightButton (NSObject sender)
		{
			// Notifies the view controller when the right button's action has been triggered, manually or by using the keyboard return key.
			
			// This little trick validates any pending auto-correction or auto-spelling just after hitting the 'Send' button
			TextView.RefreshFirstResponder ();

			var message = new Message {
				Username = CurrentUsername,
				Text = TextView.Text,
				ProfileColor = CurrentProfileColor
			};

			var indexPath = NSIndexPath.FromRowSection (0, 0);
			var rowAnimation = Inverted ? UITableViewRowAnimation.Bottom : UITableViewRowAnimation.Top;
			var scrollPosition = Inverted ? UITableViewScrollPosition.Bottom : UITableViewScrollPosition.Top;
			
			TableView.BeginUpdates ();
			messages.Insert (0, message);
			TableView.InsertRows (new []{ indexPath }, rowAnimation);
			TableView.EndUpdates ();
			
			TableView.ScrollToRow (indexPath, scrollPosition, true);
			
			// Fixes the cell from blinking (because of the transform, when using translucent cells)
			// See https://github.com/slackhq/SlackTextViewController/issues/94#issuecomment-69929927
			TableView.ReloadRows (new []{ indexPath }, UITableViewRowAnimation.Automatic);

			base.DidPressRightButton (sender);
		}

		public override void DidPressArrowKey (UIKeyCommand sender)
		{
			base.DidPressArrowKey (sender);
			
			var keyCommand = (UIKeyCommand)sender;
			if (keyCommand.Input == UIKeyCommand.UpArrow && TextView.Text.Length == 0) {
				var lastMessage = messages [0];
				EditText (lastMessage.Text);

				var indexPath = NSIndexPath.FromRowSection (0, 0);
				TableView.ScrollToRow (indexPath, UITableViewScrollPosition.Bottom, true);
			}
		}

		public override string KeyForTextCaching {
			get {
				return NSBundle.MainBundle.BundleIdentifier;
			}
		}

		public override void DidPasteMediaContent (NSDictionary userInfo)
		{
			// Notifies the view controller when the user has pasted a media (image, video, etc) inside of the text view.
			base.DidPasteMediaContent (userInfo);

			var mediaType = userInfo [SlackTextView.PastedItemMediaType];
			var contentType = userInfo [SlackTextView.PastedItemContentType];
			var data = userInfo [SlackTextView.PastedItemData];
			
			Console.WriteLine ("{0} (type = {1}) | data : {2}", contentType, mediaType, data);
		}

		public override void DidCommitTextEditing (NSObject sender)
		{
			// Notifies the view controller when tapped on the right "Accept" button for commiting the edited text
			
			var message = new Message {
				Username = CurrentUsername,
				Text = TextView.Text,
				ProfileColor = CurrentProfileColor
			};
			
			messages.RemoveAt (0);
			messages.Insert (0, message);
			TableView.ReloadData ();
			
			base.DidCommitTextEditing (sender);
		}

		public override void DidChangeAutoCompletionPrefix (string prefix, string word)
		{
			searchResult = new string[0];
			if (prefix == "@") {
				if (word.Length > 0) {
					searchResult = users.Where (u => u.StartsWith (word, StringComparison.CurrentCultureIgnoreCase)).ToArray ();
				} else {
					searchResult = users.ToArray ();
				}
			} else if (prefix == "#" && word.Length > 0) {
				searchResult = channels.Where (c => c.StartsWith (word, StringComparison.CurrentCultureIgnoreCase)).ToArray ();
			} else if ((prefix == ":" || prefix == "+:") && word.Length > 1) {
				searchResult = emojis.Where (e => e.StartsWith (word, StringComparison.CurrentCultureIgnoreCase)).ToArray ();
			}
			Array.Sort (searchResult, StringComparer.CurrentCultureIgnoreCase);
			
			var show = (searchResult.Length > 0);
			ShowAutoCompletionView (show);
		}

		public override nfloat HeightForAutoCompletionView {
			get {
				var cellHeight = AutoCompletionView.Delegate.GetHeightForRow (AutoCompletionView, NSIndexPath.FromRowSection (0, 0));
				return cellHeight * searchResult.Length;
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			if (this.TableView == tableView) {
				return messages.Count;
			} else {
				return searchResult.Length;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (this.TableView == tableView) {
				var cell = (MessageTableViewCell)TableView.DequeueReusableCell (MessengerCellIdentifier);

				if (cell.TextLabel.Text == null) {
					cell.AddGestureRecognizer (new UILongPressGestureRecognizer (gesture => {
						if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
							var alertController = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
							alertController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
							if (alertController.PopoverPresentationController != null) {
								alertController.PopoverPresentationController.SourceView = gesture.View.Superview;
								alertController.PopoverPresentationController.SourceRect = gesture.View.Frame;
							}
							alertController.AddAction (UIAlertAction.Create ("Edit Message", UIAlertActionStyle.Default, (action) => {
								EditCellMessage (gesture);
							}));
							alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
							NavigationController.PresentViewController (alertController, true, null);
						} else {
							EditCellMessage (gesture);
						}
					}));
				}

				var message = messages [indexPath.Row];

				cell.TitleLabel.Text = message.Username;
				cell.BodyLabel.Text = message.Text;
				cell.ThumbnailView.BackgroundColor = message.ProfileColor;

				cell.IndexPath = indexPath;
				cell.UsedForMessage = true;

				// Cells must inherit the table view's transform
				// This is very important, since the main table view may be inverted
				cell.Transform = TableView.Transform;

				return cell;
			} else {
				var cell = (MessageTableViewCell)AutoCompletionView.DequeueReusableCell (AutoCompletionCellIdentifier);
				cell.IndexPath = indexPath;

				var item = searchResult [indexPath.Row];
				if (FoundPrefix == "#") {
					item = "# " + item;
				} else if (FoundPrefix == ":" || FoundPrefix == "+:") {
					item = ":" + item;
				}

				cell.TitleLabel.Text = item;
				cell.TitleLabel.Font = UIFont.SystemFontOfSize (14.0f);
				cell.SelectionStyle = UITableViewCellSelectionStyle.Default;

				return cell;
			}
		}

		private void EditCellMessage (UIGestureRecognizer gesture)
		{
			var cell = (MessageTableViewCell)gesture.View;
			var message = messages [cell.IndexPath.Row];

			EditText (message.Text);

			TableView.ScrollToRow (cell.IndexPath, UITableViewScrollPosition.Bottom, true);
		}

		[Export ("tableView:heightForRowAtIndexPath:")]
		public nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (this.TableView == tableView) {
				var message = messages [indexPath.Row];
			
				var paragraphStyle = new NSMutableParagraphStyle ();
				paragraphStyle.LineBreakMode = UILineBreakMode.WordWrap;
				paragraphStyle.Alignment = UITextAlignment.Left;

				var pointSize = MessageTableViewCell.DefaultFontSize;
				var attributes = new UIStringAttributes {
					Font = UIFont.SystemFontOfSize (pointSize),
					ParagraphStyle = paragraphStyle
				};
			
				var width = tableView.Frame.Width - MessageTableViewCell.AvatarHeight;
				width -= 25.0f;
			
				var titleBounds = ((NSString)message.Username).GetBoundingRect (new CGSize (width, nfloat.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, attributes, null);
				var bodyBounds = ((NSString)message.Text).GetBoundingRect (new CGSize (width, nfloat.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, attributes, null);
			
				if (message.Text.Length == 0) {
					return 0.0f;
				}
			
				var height = titleBounds.Height;
				height += bodyBounds.Height;
				height += 40.0f;
			
				if (height < MessageTableViewCell.MinimumHeight) {
					height = MessageTableViewCell.MinimumHeight;
				}
			
				return height;
			} else {
				return MessageTableViewCell.MinimumHeight;
			}
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (this.AutoCompletionView == tableView) {
				var item = searchResult [indexPath.Row];
				if (FoundPrefix == "@" && FoundPrefixRange.Location == 0) {
					item += ":";
				} else if (FoundPrefix == ":" || FoundPrefix == "+:") {
					item += ":";
				}
				item += " ";
				AcceptAutoCompletion (item, true);
			}
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			contentSizeObserver.Dispose ();
		}

		private static UIColor RandomColor (string username)
		{
			var index = Array.IndexOf (users, username);

			if (index != -1 && colors [index] != null) {
				return colors [index];
			}
			
			var color = UIColor.FromRGB (
				random.Next (127) + 127, 
				random.Next (127) + 127, 
				random.Next (127) + 127);

			if (index != -1) {
				colors [index] = color;
			}

			return color;
		}

		private static string RandomUser ()
		{
			var idx = random.Next (users.Length);
			var username = users [idx];
			return username;
		}

		private static string LoremIpsum ()
		{
			var numSentences = random.Next (4) + 3;
			var numWords = random.Next (8) + 4;
			var result = new StringBuilder ();
			for (int s = 0; s < numSentences; s++) {
				for (int w = 0; w < numWords; w++) {
					if (w > 0) {
						result.Append (" ");
					}
					result.Append (words [random.Next (words.Length)]);
				}
				result.Append (". ");
			}
			return result.ToString ();
		}
	}
}
