using System;
using UIKit;
using JVMenuPopover;
using CoreAnimation;
using CoreGraphics;

namespace JVMenuPopoverSample.ViewControllers
{
	public class JVMenuSecondController : JVMenuRootViewController
	{
		public JVMenuSecondController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			JVMenuHelper.RemoveLayerFromView(ContainerView);
		
			// gradient background color
			var newGradient = (CAGradientLayer)CAGradientLayer.Create(); 
			newGradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("87FC70");
			var secondColor = JVMenuHelper.ColorWithHexString("0BD318");

			newGradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(newGradient,0);

			Image = JVMenuHelper.ChangeImageColor(UIImage.FromBundle("about-48"), UIColor.Black);
			ImageView.Image = Image;

			Label.Text = @"About Us";
		}
	}
}

