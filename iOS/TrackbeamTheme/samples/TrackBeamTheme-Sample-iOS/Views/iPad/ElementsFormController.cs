using System;
using System.CodeDom.Compiler;
using Xamarin.Themes.Core;
using Foundation;
using UIKit;

namespace TrackBeamTheme_Sample_iOS
{
	partial class ElementsFormController : UIViewController
	{
		public ElementsFormController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "Elements";

			//setup the theme
			ThemeManager.Current.Apply(View)
				.Apply(loginButton)
				.Apply(registerButton)
				.Apply(textField);

			//set up the switches
			onSwitch.IsOn = true;
			offSwitch.IsOn = false;

			//
			var itemBtn = new UIBarButtonItem ("Close", UIBarButtonItemStyle.Plain, null);
			itemBtn.Clicked += (object sender, EventArgs e) => 
			{
				this.DismissViewController(true, null);
			};
			NavigationItem.SetRightBarButtonItem (itemBtn, false);

			//
			textField.ShouldReturn = TextFieldShouldReturn;
			sliderView.ValueChanged += (object sender, EventArgs e) => 
			{
				progressView.Progress = sliderView.Value;
			};
		}


		bool TextFieldShouldReturn (UITextField aTextField)
		{
			aTextField.ResignFirstResponder ();
			return true;
		}
	}
}
