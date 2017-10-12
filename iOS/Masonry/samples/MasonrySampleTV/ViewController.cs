using System;
using CoreGraphics;
using Foundation;
using UIKit;

using Masonry;

namespace MasonrySampleTV
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		private CGSize buttonSize = new CGSize(200, 200);
		private UIButton growingButton;

		private bool topLeft = true;
		private UIButton movingButton;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;


			// create the sub views

			var greenView = new UIView();
			greenView.BackgroundColor = UIColor.Green;
			greenView.Layer.BorderColor = UIColor.Black.CGColor;
			greenView.Layer.BorderWidth = 2;
			View.AddSubview(greenView);

			var redView = new UIView();
			redView.BackgroundColor = UIColor.Red;
			redView.Layer.BorderColor = UIColor.Black.CGColor;
			redView.Layer.BorderWidth = 2;
			View.AddSubview(redView);

			var blueView = new UIView();
			blueView.BackgroundColor = UIColor.Blue;
			blueView.Layer.BorderColor = UIColor.Black.CGColor;
			blueView.Layer.BorderWidth = 2;
			View.AddSubview(blueView);


			// create the constraints

			var padding = 10;
			var superview = View;

			// basic constraints

			greenView.MakeConstraints(make =>
			{
				make.Top.And.Left.EqualTo(superview).Offset(padding); // chain .Top and .Left
				make.Bottom.EqualTo(blueView.Top()).Offset(-padding);
				make.Right.EqualTo(redView.Left()).Offset(-padding);

				make.Width.EqualTo(redView.Width());

				make.Height.EqualTo(redView.Height());
				make.Height.EqualTo(blueView.Height());
			});

			// .With and .And are semantic and optional
			redView.MakeConstraints(make =>
			{
				make.Top.EqualTo(superview.Top()).With.Offset(padding); // with .Width
				make.Left.EqualTo(greenView.Right()).Offset(padding); // no .Width
				make.Bottom.EqualTo(blueView.Top()).And.Offset(-padding); // with .And
				make.Right.EqualTo(superview.Right()).Offset(-padding); // no .And
				make.Width.EqualTo(greenView.Width());

				make.Height.EqualTo(NSArray.FromNSObjects(new[] { greenView, blueView })); // can pass array of views
			});

			blueView.MakeConstraints(make =>
			{
				make.Top.EqualTo(greenView.Bottom()).Offset(padding);
				make.Left.EqualTo(superview.Left()).Offset(padding);
				make.Bottom.EqualTo(superview.Bottom()).Offset(-padding);
				make.Right.EqualTo(superview.Right()).Offset(-padding);
				make.Height.EqualTo(NSArray.FromNSObjects(new[] { greenView.Height(), redView.Height() })); // can pass array of attributes
			});


			// demonstrate update

			growingButton = new UIButton(UIButtonType.System);
			growingButton.SetTitle("Grow Me!", UIControlState.Normal);
			growingButton.BackgroundColor = UIColor.White;
			growingButton.Layer.BorderColor = UIColor.Green.CGColor;
			growingButton.Layer.BorderWidth = 3;
			View.AddSubview(growingButton);

			growingButton.PrimaryActionTriggered += delegate
			{
				buttonSize = new CGSize(buttonSize.Width * 1.3f, buttonSize.Height * 1.3f);

				// tell constraints they need updating
				View.SetNeedsUpdateConstraints();

				// update constraints now so we can animate the change
				View.UpdateConstraintsIfNeeded();

				UIView.Animate(0.4, () => View.LayoutIfNeeded());
			};


			// demonstrate remake

			movingButton = new UIButton(UIButtonType.System);
			movingButton.SetTitle("Move Me!", UIControlState.Normal);
			movingButton.BackgroundColor = UIColor.White;
			movingButton.Layer.BorderColor = UIColor.Green.CGColor;
			movingButton.Layer.BorderWidth = 3;
			View.AddSubview(movingButton);

			movingButton.PrimaryActionTriggered += delegate
			{
				topLeft = !topLeft;

				// tell constraints they need updating
				View.SetNeedsUpdateConstraints();

				// update constraints now so we can animate the change
				View.UpdateConstraintsIfNeeded();

				UIView.Animate(0.4, () => View.LayoutIfNeeded());
			};
		}

		public override void UpdateViewConstraints()
		{
			// update is used only when updating the constant value
			growingButton.UpdateConstraints(make =>
			{
				make.Center.EqualTo(View);
				make.Width.EqualTo(NSNumber.FromNFloat(buttonSize.Width)).PriorityLow();
				make.Height.EqualTo(NSNumber.FromNFloat(buttonSize.Height)).PriorityLow();
				make.Width.LessThanOrEqualTo(View);
				make.Height.LessThanOrEqualTo(View);
			});

			// remake is used when changing constraints
			movingButton.RemakeConstraints(make =>
			{
				make.Height.EqualTo(NSNumber.FromNFloat(250));
				make.Width.EqualTo(movingButton.Height()).MultipliedBy(2.0f);

				if (topLeft)
				{
					make.Left.EqualTo(View.LeftMargin());
					make.Top.EqualTo(this.TopLayoutGuide());
				}
				else
				{
					make.Right.EqualTo(View.RightMargin());
					make.Bottom.EqualTo(this.BottomLayoutGuide());
				}
			});

			base.UpdateViewConstraints();
		}
	}
}
