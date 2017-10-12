using System;
using AppKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

using Masonry;

namespace MasonrySampleMac
{
	public partial class ViewController : NSViewController
	{
		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		private CGSize buttonSize = new CGSize(100, 100);
		private NSButton growingButton;

		private bool topLeft = true;
		private NSButton movingButton;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.Layer = new CALayer();
			View.Layer.BackgroundColor = NSColor.White.CGColor;


			// create the sub views

			var greenView = new NSView();
			greenView.Layer = new CALayer();
			greenView.Layer.BackgroundColor = NSColor.Green.CGColor;
			greenView.Layer.BorderColor = NSColor.Black.CGColor;
			greenView.Layer.BorderWidth = 2;
			View.AddSubview(greenView);

			var redView = new NSView();
			redView.Layer = new CALayer();
			redView.Layer.BackgroundColor = NSColor.Red.CGColor;
			redView.Layer.BorderColor = NSColor.Black.CGColor;
			redView.Layer.BorderWidth = 2;
			View.AddSubview(redView);

			var blueView = new NSView();
			blueView.Layer = new CALayer();
			blueView.Layer.BackgroundColor = NSColor.Blue.CGColor;
			blueView.Layer.BorderColor = NSColor.Black.CGColor;
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

			growingButton = new NSButton();
			growingButton.Title = "Grow Me!";
			growingButton.Layer = new CALayer();
			growingButton.Layer.BackgroundColor = NSColor.White.CGColor;
			growingButton.Layer.BorderColor = NSColor.Green.CGColor;
			growingButton.Layer.BorderWidth = 3;
			View.AddSubview(growingButton);

			growingButton.AddGestureRecognizer(new NSClickGestureRecognizer(_ =>
			{
				buttonSize = new CGSize(buttonSize.Width * 1.3f, buttonSize.Height * 1.3f);

				// tell constraints they need updating
				View.NeedsUpdateConstraints = true;

				// update constraints now
				View.UpdateConstraintsForSubtreeIfNeeded();
				View.LayoutSubtreeIfNeeded();
			}));


			// demonstrate remake

			movingButton = new NSButton();
			movingButton.Title = "Move Me!";
			movingButton.Layer = new CALayer();
			movingButton.Layer.BackgroundColor = NSColor.White.CGColor;
			movingButton.Layer.BorderColor = NSColor.Green.CGColor;
			movingButton.Layer.BorderWidth = 3;
			View.AddSubview(movingButton);

			movingButton.AddGestureRecognizer(new NSClickGestureRecognizer(_ =>
			{
				topLeft = !topLeft;

				// tell constraints they need updating
				View.NeedsUpdateConstraints = true;

				// update constraints now
				View.UpdateConstraintsForSubtreeIfNeeded();
				View.LayoutSubtreeIfNeeded();
			}));
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
				make.Height.EqualTo(NSNumber.FromNFloat(100));
				make.Width.EqualTo(movingButton.Height()).MultipliedBy(2.0f);

				if (topLeft)
				{
					make.Left.EqualTo(View.Left());
					make.Top.EqualTo(View.Top());
				}
				else
				{
					make.Right.EqualTo(View.Right());
					make.Bottom.EqualTo(View.Bottom());
				}
			});

			base.UpdateViewConstraints();
		}
	}
}
