using System;

using UIKit;
using HDNotification;

namespace HDNotificationDemo
{
	public partial class ViewController : UIViewController
	{
		string [] messages = new string[] {
			"Welcome to HDNotification Demo !!!",
			"This is a push notificaiton message!",
			"You can add an icon to this notification view on the left!",
			"You have a message with long text which will show in two lines of notification view!",
			"Feed Monkeys!"
		};

		Random random = new Random();

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void ShowNotiViewTouchUpInside (UIButton sender)
		{
			int randomNumber = random.Next(0, 1000) % messages.Length;

			HDNotificationView.ShowNotification (
				image: UIImage.FromBundle ("sampleIcon"),
				title: "HDNotification Demo",
				message: messages[randomNumber],
				isAutoHide: true,
				onTouch: () => {
					HDNotificationView.HideNotification ();
				}
			);
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}

