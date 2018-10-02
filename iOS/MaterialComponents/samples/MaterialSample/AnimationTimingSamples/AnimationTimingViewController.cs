using System;
using System.Collections.Generic;
using System.Linq;

using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

using MaterialComponents;

namespace MaterialSample {
	public partial class AnimationTimingViewController : UIViewController {
		#region Class Variables

		static readonly nfloat offset = 16;
		static readonly nfloat sphereWidth = 48;
		const double animationTimeInterval = 1;
		const double animationTimeDelay = 0.5;

		UIBarButtonItem btnAnimate;

		UIScrollView scrollView;
		UILabel lblLinear;
		UIView linearView;
		UILabel lblMaterialStandard;
		UIView materialStandardView;
		UILabel lblMaterialDeceleration;
		UIView materialDecelerationView;
		UILabel lblMaterialAcceleration;
		UIView materialAccelerationView;
		UILabel lblMaterialSharp;
		UIView materialSharpView;

		nfloat lineSpace;

		#endregion

		#region Constructors

		public AnimationTimingViewController (string title)
		{
			Title = title;
		}

		#endregion

		#region Controller Life Cycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			InitializeComponents ();
		}

		#endregion

		#region User Interactions

		void BtnAnimate_Clicked (object sender, EventArgs e)
		{
			btnAnimate.Enabled = false;

			PlayAnimations (HandleAction);

			void HandleAction (bool obj)
			{
				btnAnimate.Enabled = true;
			}
		}


		#endregion

		#region Internal Functionality

		void InitializeComponents ()
		{
			View.BackgroundColor = UIColor.White;

			btnAnimate = new UIBarButtonItem ("Animate", UIBarButtonItemStyle.Done, BtnAnimate_Clicked);
			NavigationItem.RightBarButtonItem = btnAnimate;

			scrollView = new UIScrollView (View.Bounds) { 
				ClipsToBounds = true,
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0))
				scrollView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Always;

			View.AddSubview (scrollView);

			lblLinear = CreateLabel ("Linear");
			linearView = CreateSphere ();

			lblMaterialStandard = CreateLabel ("AnimationTimingFunction.Standard");
			materialStandardView = CreateSphere ();

			lblMaterialDeceleration = CreateLabel ("AnimationTimingFunction.Deceleration");
			materialDecelerationView = CreateSphere ();

			lblMaterialAcceleration = CreateLabel ("AnimationTimingFunction.Acceleration");
			materialAccelerationView = CreateSphere ();

			lblMaterialSharp = CreateLabel ("AnimationTimingFunction.Sharp");
			materialSharpView = CreateSphere ();

			scrollView.AddSubviews (lblLinear, linearView,
			                        lblMaterialStandard, materialStandardView,
			                        lblMaterialDeceleration, materialDecelerationView,
			                        lblMaterialAcceleration, materialAccelerationView,
			                        lblMaterialSharp, materialSharpView);

			lineSpace = (View.Frame.Height - 50) / 5 - sphereWidth - lblLinear.Frame.Height;

			AddConstraints ();
		}

		UILabel CreateLabel (string text)
		{
			var label = new UILabel (CGRect.Empty) {
				Font = Typography.CaptionFont,
				Alpha = Typography.CaptionFontOpacity,
				Text = text,
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			label.SizeToFit ();

			return label;
		}

		UIView CreateSphere ()
		{
			var view = new UIView (new CGRect (0, 0, sphereWidth, sphereWidth)) {
				BackgroundColor = UIColor.DarkGray,
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			view.Layer.CornerRadius = sphereWidth / 2;

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				view.WidthAnchor.ConstraintEqualTo (sphereWidth).Active = true;
				view.HeightAnchor.ConstraintEqualTo (sphereWidth).Active = true;
			} else {
				var widthAnchor = NSLayoutConstraint.Create (view, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1, sphereWidth);
				var heightAnchor = NSLayoutConstraint.Create (view, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, sphereWidth);
				NSLayoutConstraint.ActivateConstraints (new [] { widthAnchor, heightAnchor });
			}

			return view;
		}

		void AddConstraints ()
		{
			var views = new Dictionary<object, object> {
				{ nameof (lblLinear), lblLinear },
				{ nameof (linearView), linearView },
				{ nameof (lblMaterialStandard), lblMaterialStandard },
				{ nameof (materialStandardView), materialStandardView },
				{ nameof (lblMaterialDeceleration), lblMaterialDeceleration },
				{ nameof (materialDecelerationView), materialDecelerationView },
				{ nameof (lblMaterialAcceleration), lblMaterialAcceleration },
				{ nameof (materialAccelerationView), materialAccelerationView },
				{ nameof (lblMaterialSharp), lblMaterialSharp },
				{ nameof (materialSharpView), materialSharpView }
			};
			var nsViews = NSDictionary<NSString, UIView>.FromObjectsAndKeys (views.Values.ToArray (), views.Keys.ToArray (), views.Keys.Count);

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				scrollView.TopAnchor.ConstraintEqualTo (View.TopAnchor, 0).Active = true;
				scrollView.LeadingAnchor.ConstraintEqualTo (View.LeadingAnchor, 0).Active = true;
				scrollView.TrailingAnchor.ConstraintEqualTo (View.TrailingAnchor, 0).Active = true;
				scrollView.BottomAnchor.ConstraintEqualTo (View.BottomAnchor, 0).Active = true;

				foreach (var view in nsViews.Values)
					view.LeadingAnchor.ConstraintEqualTo (scrollView.LeadingAnchor, offset).Active = true;
			} else {
				var topAnchor = NSLayoutConstraint.Create (scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0);
				var leadingAnchor = NSLayoutConstraint.Create (scrollView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.Leading, 1, 0);
				var trailingAnchor = NSLayoutConstraint.Create (scrollView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.Trailing, 1, 0);
				var bottomAnchor = NSLayoutConstraint.Create (scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0);
				NSLayoutConstraint.ActivateConstraints (new [] { topAnchor, leadingAnchor, trailingAnchor, bottomAnchor });

				foreach (var view in nsViews.Values) {
					var viewLeadingAnchor = NSLayoutConstraint.Create (view, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Leading, 1, offset);
					NSLayoutConstraint.ActivateConstraints (new [] { viewLeadingAnchor });
				}
			}

			var keys = views.Keys.ToArray ();
			var vfl = $"V:|-{offset}";

			for (int i = 0; i < keys.Length; i++)
				if (i == keys.Length - 1) 
					vfl += $"[{keys [i]}]";
				else if (i % 2 == 0) 
					vfl += $"-[{keys [i]}]";
				else 
					vfl += $"[{keys [i]}]-{lineSpace}";

			vfl += $"-{offset}-|";

			NSLayoutConstraint.ActivateConstraints (NSLayoutConstraint.FromVisualFormat (vfl, 0, null, nsViews));
		}

		void PlayAnimations (Action<bool> completion)
		{
			var linearTimingCurve = CAMediaTimingFunction.FromName (CAMediaTimingFunction.Linear);
			ApplyAnimation (linearView, linearTimingCurve, completion);

			var materialStandardCurve = CAMediaTimingFunctionAnimationTiming.GetFunction (AnimationTimingFunction.Standard);
			ApplyAnimation (materialStandardView, materialStandardCurve, null);

			var materialDecelerationCurve = CAMediaTimingFunctionAnimationTiming.GetFunction (AnimationTimingFunction.Deceleration);
			ApplyAnimation (materialDecelerationView, materialDecelerationCurve, null);

			var materialAccelerationCurve = CAMediaTimingFunctionAnimationTiming.GetFunction (AnimationTimingFunction.Acceleration);
			ApplyAnimation (materialAccelerationView, materialAccelerationCurve, null);

			var materialSharpCurve = CAMediaTimingFunctionAnimationTiming.GetFunction (AnimationTimingFunction.Sharp);
			ApplyAnimation (materialSharpView, materialSharpCurve, null);
		}

		void ApplyAnimation (UIView view, CAMediaTimingFunction timingFunction, Action<bool> completion)
		{
			var animationWidth = View.Frame.Width - view.Frame.Width - (offset * 2);

			if (UIDevice.CurrentDevice.CheckSystemVersion (11, 0))
				animationWidth -= View.SafeAreaInsets.Left + View.SafeAreaInsets.Right;

			var transform = CGAffineTransform.MakeTranslation (animationWidth, 0);
			UIViewMDCTimingFunction.MdcAnimate (timingFunction, 
			                                    animationTimeInterval, 
			                                    animationTimeDelay, 
			                                    0, 
			                                    () => view.Transform = transform, 
			                                    HandleAction);

			void HandleAction (bool obj)
			{
				UIViewMDCTimingFunction.MdcAnimate (timingFunction,
				                                    animationTimeInterval,
				                                    animationTimeDelay,
				                                    0,
				                                    () => view.Transform = CGAffineTransform.MakeIdentity (),
				                                    completion);
			}
		}

		#endregion
	}
}

