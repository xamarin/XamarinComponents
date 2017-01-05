using System;
using System.Drawing;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGPoint = global::System.Drawing.PointF;
#endif

using LoginScreen.Utils;
using LoginScreen.Views;

namespace LoginScreen.ViewControllers
{
	abstract class BaseViewController : UIViewController
	{
		UIScrollView scrollView;
		UIAlertView activityAlert;
		BaseFormView formView;

		protected BaseViewController (string title)
		{
			Title = title;
			UIKeyboard.Notifications.ObserveWillChangeFrame (KeyboardWillChangeFrame);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View = new UIScrollView (View.Frame);

			var gradientLayer = new CAGradientLayer ();
			gradientLayer.Colors = new [] {
				UIColor.FromRGB (241, 246, 250).CGColor,
				UIColor.FromRGB (222, 231, 240).CGColor
			};
			View.Layer.InsertSublayer (gradientLayer, 0);

			View.BackgroundColor = UIColor.FromRGB (230, 236, 245);

			var endEditingGestureRecognizer = new UITapGestureRecognizer (() => View.EndEditing (true))
			{
				ShouldBegin = IsTouchOutsideOfAnyButton
			};

			View.AddGestureRecognizer (endEditingGestureRecognizer);

			formView = CreateFormView ();
			#if __UNIFIED__
			formView.BackClick += (sender, e) => NavigationController.PopViewController (true);
			#else
			formView.BackClick += (sender, e) => NavigationController.PopViewControllerAnimated (true);
			#endif

			scrollView = new UIScrollView
			{
				Frame = View.Bounds,
				ContentSize = formView.Bounds.Size,
				AutoresizingMask = UIViewAutoresizing.FlexibleDimensions
			};

			scrollView.AddSubview (formView);

			View.AddSubview (scrollView);
		}

		protected abstract BaseFormView CreateFormView ();

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			formView.Center = scrollView.GetContentCenter ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			formView.Clear ();
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return AutorotateHelper.GetSupportedInterfaceOrientations ();
		}

#pragma warning disable 0672
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
#pragma warning restore 0672
		{
			return AutorotateHelper.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation);
		}

		protected bool IsEmailValid (string email)
		{
			try {
				new System.Net.Mail.MailAddress (email);
			} catch (FormatException) {
				return false;
			}
			return true;
		}

		protected void StartActivityAnimation (string title)
		{
			if (activityAlert != null) {
				StopActivityAnimation ();
			}

			activityAlert = new UIAlertView
			{
				Title = title
			};

			var indicator = new UIActivityIndicatorView
			{
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White,
				Center = new CGPoint(activityAlert.Bounds.GetMidX(), activityAlert.Bounds.GetMidY()),
				AutoresizingMask = UIViewAutoresizing.FlexibleMargins
			};
			indicator.StartAnimating ();

			activityAlert.AddSubview (indicator);
			activityAlert.Show ();
		}

		protected void StopActivityAnimation ()
		{
			if (activityAlert != null) {
				activityAlert.DismissWithClickedButtonIndex (0, true);
				activityAlert = null;
			}
		}

		protected void ShowAlert (string title, string text, string cancelButtonTitle)
		{
			var alert = new UIAlertView
			{
				Title = title,
				Message = text
			};
			alert.AddButton (cancelButtonTitle);
			alert.Show ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			View.EndEditing (true);
		}

#pragma warning disable 0672
		public override void ViewDidUnload ()
#pragma warning restore 0672
		{
#pragma warning disable 0618
			base.ViewDidUnload ();
#pragma warning restore 0618

			ReleaseOutlet (ref scrollView);
			ReleaseOutlet (ref formView);
		}

		protected void ReleaseOutlet<T> (ref T view)
			where T : UIView
		{
			if (view != null) {
				view.Dispose ();
				view = null;
			}
		}

		void KeyboardWillChangeFrame (object sender, UIKeyboardEventArgs e)
		{
			if (IsViewLoaded && View.Window != null) {
				scrollView.Frame = View.GetAreaAboveKeyboard (e.FrameBegin);
				formView.Center = scrollView.GetContentCenter ();
				UIView.Animate (e.AnimationDuration, 0, e.AnimationCurve.ToAnimationOptions (), () => {
					scrollView.Frame = View.GetAreaAboveKeyboard (e.FrameEnd);
					formView.Center = scrollView.GetContentCenter ();
				}, null);
			}
		}

		bool IsTouchOutsideOfAnyButton (UIGestureRecognizer recognizer)
		{
			var location = recognizer.LocationInView (recognizer.View);
			var viewWithTouchInside = recognizer.View.HitTest (location, null);
			return !(viewWithTouchInside is UIButton);
		}
	}
}