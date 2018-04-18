using System;
using JVMenuPopover;
using CoreAnimation;
using UIKit;

namespace JVMenuPopoverSample.ViewControllers
{
	public class JVMenuFourthController : JVMenuRootViewController
	{
		public JVMenuFourthController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			JVMenuHelper.RemoveLayerFromView(ContainerView);

			// gradient background color
			var newGradient = (CAGradientLayer)CAGradientLayer.Create(); 
			newGradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("1AD6FD");
			var secondColor = JVMenuHelper.ColorWithHexString("1D62F0");

			newGradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(newGradient,0);

			Image = JVMenuHelper.ChangeImageColor(UIImage.FromBundle("business_contact-48"), UIColor.Black);
			ImageView.Image = Image;

			Label.Text = @"Contact Us";
		}
	}
}

