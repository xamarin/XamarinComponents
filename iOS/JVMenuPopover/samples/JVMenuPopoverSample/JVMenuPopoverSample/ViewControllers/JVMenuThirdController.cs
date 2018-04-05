using System;
using JVMenuPopover;
using CoreAnimation;
using UIKit;

namespace JVMenuPopoverSample.ViewControllers
{
	public class JVMenuThirdController : JVMenuRootViewController
	{
		public JVMenuThirdController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			JVMenuHelper.RemoveLayerFromView(ContainerView);

			// gradient background color
			var newGradient = (CAGradientLayer)CAGradientLayer.Create(); 
			newGradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("FB2B69");
			var secondColor = JVMenuHelper.ColorWithHexString("FF5B37");

			newGradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(newGradient,0);

			Image = JVMenuHelper.ChangeImageColor(UIImage.FromBundle("settings-48"), UIColor.Black);
			ImageView.Image = Image;

			Label.Text = @"Our Services";
		}
	}
}

