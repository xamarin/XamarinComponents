using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
using CoreGraphics;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CGRect = global::System.Drawing.RectangleF;
#endif

using LoginScreen.Utils;

namespace LoginScreen.Views
{
	class BaseFormView : UIView
	{
		Dictionary<UITextField, BubbleView> bubbleViews;
		UIImageView backgroundView;
		UIButton backButton;
		UILabel titleView;
		UIImageView titleDividerView;
		UIImage fieldBackground;

		public BaseFormView ()
		{
			bubbleViews = new Dictionary<UITextField, BubbleView> ();

			backgroundView = new UIImageView (UIImage.FromFile ("Images/background.png").StretchableImage (13, 13));

			backButton = new UIButton ();
			backButton.SetImage (UIImage.FromFile ("Images/backarrow.png"), UIControlState.Normal);
			backButton.SetImage (UIImage.FromFile ("Images/backarrow_pressed.png"), UIControlState.Highlighted);
			backButton.TouchUpInside += (sender, e) => OnBackClick (EventArgs.Empty);

			titleView = new UILabel
			{
				BackgroundColor = UIColor.Clear,
				Font = Fonts.HelveticaNeueMedium (18),
				TextColor = UIColor.FromRGB(65, 100, 140),
				ShadowColor = UIColor.White.ColorWithAlpha(0.5f),
				ShadowOffset = new SizeF(0, 1),
				TextAlignment = UITextAlignment.Center
			};

			titleDividerView = new UIImageView (UIImage.FromFile ("Images/titledivider.png").StretchableImage (4, 0));

			AddSubviews (backgroundView, backButton, titleView, titleDividerView);

			fieldBackground = UIImage.FromFile ("Images/textfield.png").StretchableImage (5, 0);
		}

		public event EventHandler BackClick;

		public string Title {
			get { return titleView.Text; }
			set { titleView.Text = value; }
		}

		public bool BackButtonHidden {
			get { return backButton.Hidden; }
			set { backButton.Hidden = value; }
		}

		public virtual void Clear ()
		{
			foreach (var field in Subviews.OfType<UITextField>()) {
				field.Text = String.Empty;
			}
			foreach (var field in bubbleViews.Keys.ToArray ()) {
				HideBubbleFor (field);
			}
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			backgroundView.Frame = Bounds;
			backButton.Frame = new CGRect (Bounds.X + 13, Bounds.Y + 4, 43, 39);
			titleView.Frame = new CGRect (Bounds.X + 56, Bounds.Y + 4, Bounds.Width - 112, 34);
			titleDividerView.Frame = new CGRect (Bounds.X + 3, Bounds.Y + 38, Bounds.Width - 6, 4);
		}

		protected CustomTextField CreateField ()
		{
			return new CustomTextField
			{
				Background = fieldBackground,
				TextColor = UIColor.FromRGB (134, 155, 170),
				Font = Fonts.HelveticaNeueMedium(14f),
				AutocorrectionType = UITextAutocorrectionType.No,
				TextInset = new UIEdgeInsets (6, 10, 6, 10),
				PlaceholderFont = Fonts.HelveticaNeueMedium(14f),
				PlaceholderColor = UIColor.FromRGB (134, 155, 170).ColorWithAlpha (0.7f)
			};
		}

		protected UIButton CreateButton ()
		{
			var result = new UIButton
			{
				Font = Fonts.HelveticaNeueMedium(16f),
				TitleShadowOffset = new SizeF (0f, -1f),
				TitleEdgeInsets = new UIEdgeInsets (0f, 5f, 0f, 5f),
				ExclusiveTouch = true
			};
			result.TitleLabel.AdjustsFontSizeToFitWidth = true;
			result.SetTitleColor (UIColor.White, UIControlState.Normal);
			result.SetTitleShadowColor (UIColor.Black.ColorWithAlpha (0.2f), UIControlState.Normal);
			return result;
		}

		protected void ApplyFieldsNavigation (Action submitAction, params UITextField[] fields)
		{
			if (fields != null && fields.Length > 0) {
				for (int i = 0; i < fields.Length - 1; i++) {
					var nextField = fields [i + 1];
					fields [i].ReturnKeyType = UIReturnKeyType.Next;
					fields [i].ShouldReturn = x => {
						nextField.BecomeFirstResponder ();
						return false;
					};
				}
				fields [fields.Length - 1].ReturnKeyType = UIReturnKeyType.Done;
				fields [fields.Length - 1].ShouldReturn = x => {
					submitAction.Invoke ();
					return false;
				};
			}
		}

		protected void ShowBubbleFor (UITextField field, string text)
		{
			if (!bubbleViews.ContainsKey (field)) {
				field.ShouldBeginEditing = HideBubbleFor;
				bubbleViews.Add (field, null);
			}

			if (bubbleViews [field] == null) {
				var bubbleView = new BubbleView
				{
					Frame = new CGRect (field.Frame.Left + 5f, field.Frame.Bottom - 14f, field.Frame.Width, 44f),
					Text = text
				};
				bubbleViews[field] = bubbleView;
				AddSubview (bubbleView);
			} else {
				bubbleViews [field].Text = text;
			}
		}

		private bool HideBubbleFor (UITextField textFild)
		{
			var bubbleView = bubbleViews [textFild];
			if (bubbleView != null) {
				bubbleView.RemoveFromSuperview ();
				bubbleView.Dispose ();
			}
			bubbleViews [textFild] = null;

			return true;
		}

		protected virtual void OnBackClick (EventArgs e)
		{
			var handler = BackClick;
			if (handler != null)
				handler (this, e);
		}
	}
}

