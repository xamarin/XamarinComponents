using System;
using JVMenuPopover;
using CoreAnimation;
using UIKit;


namespace JVMenuPopoverSample.ViewControllers
{
	public class JVMenuFifthController : JVMenuRootViewController
	{
		public JVMenuFifthController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			JVMenuHelper.RemoveLayerFromView(ContainerView);

			// gradient background color
			var newGradient = (CAGradientLayer)CAGradientLayer.Create(); 
			newGradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("EF4DB6");
			var secondColor = JVMenuHelper.ColorWithHexString("C643FC");

			newGradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(newGradient,0);

			Image = JVMenuHelper.ChangeImageColor(UIImage.FromBundle("ask_question-48"), UIColor.Black);
			ImageView.Image = Image;

			Label.Text = @"Help?";
		}
	}
}

