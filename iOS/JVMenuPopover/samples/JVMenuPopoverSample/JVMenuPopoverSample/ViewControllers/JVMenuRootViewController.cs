using System;
using UIKit;
using JVMenuPopover;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;

namespace JVMenuPopoverSample.ViewControllers
{
	public class JVMenuRootViewController : JVMenuViewController
	{
		#region Properties

		public UILabel Label {get; set;}
		public UIImageView ImageView {get; set;}
		public UIImage Image {get; set;}
		public CAGradientLayer Gradient {get; set;}
		public UIView ContainerView { get; set;}
	
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopoverSample.ViewControllers.JVMenuRootViewController"/> class.
		/// </summary>
		public JVMenuRootViewController() : base()
		{
			
		}

		#endregion

		#region Methods

		/// <summary>
		/// ViewDidLoad
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = UIColor.Clear;
		
			this.View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			this.View.AutosizesSubviews = true;

			ContainerView =  new UIView(this.View.Frame);
			ContainerView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			ContainerView.AutosizesSubviews = true;

			// gradient background color
			Gradient = (CAGradientLayer)CAGradientLayer.Create(); 
			Gradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("52EDC7");
			var secondColor = JVMenuHelper.ColorWithHexString("5AC8FB");

			Gradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(Gradient,0);

			Image = JVMenuHelper.ChangeImageColor(JVMenuPopoverConfig.SharedInstance.MenuImage, UIColor.Black);
			ImageView = new UIImageView(new CGRect(this.View.Frame.Size.Width/2-this.Image.Size.Width/2, this.View.Frame.Size.Height/2-30, this.Image.Size.Width, this.Image.Size.Height));
			ImageView.Image = Image;

			ContainerView.Add(ImageView);
		    
			Label = new UILabel(new CGRect(this.View.Frame.Size.Width/2-110, this.View.Frame.Size.Height/2-20, 220, 60));
			Label.TextColor = UIColor.Black.ColorWithAlpha(0.6f);
		    Label.TextAlignment = UITextAlignment.Center;
			Label.Font = UIFont.FromName("HelveticaNeue", 20);
			Label.TextColor = UIColor.Black;
		    Label.Text = @"Home";

			ContainerView.Add(Label);

			this.View.Add(ContainerView);

		}

		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NavigationItem.LeftBarButtonItem.TintColor = UIColor.Black;

			this.View.Frame = this.View.Bounds;
			Gradient.Frame = this.View.Frame;
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			this.View.Frame = this.View.Bounds;
			Gradient.Frame = this.View.Frame;
		}
		#endregion


	
	}


}

