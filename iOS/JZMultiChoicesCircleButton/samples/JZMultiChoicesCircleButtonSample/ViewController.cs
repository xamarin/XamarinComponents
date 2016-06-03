using System;

using UIKit;
using JZMultiChoice;
using CoreGraphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using JZMultiChoice.Collections;

namespace JZMultiChoicesCircleButtonSample
{
	public partial class ViewController : UIViewController
	{
		private JZMultiChoicesCircleButton mCircleButton;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.


			var menuItems = new ChoiceItemCollection () {

				new ChoiceItem()
				{
					Title = "Send",
					Icon = UIImage.FromBundle (@"SendRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 1 Selected"); 
						ShowSimple();
					},
					DisableActionAnimation = true,
				},
				new ChoiceItem()
				{
					Title = "Complete",
					Icon = UIImage.FromBundle (@"CompleteRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 2 Selected"); 
						ShowSuccess(true, "YES!!");
					},
				},
				new ChoiceItem()
				{
					Title = "Calender",
					Icon = UIImage.FromBundle (@"CalenderRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 3 Selected"); 
						ShowSuccess(false, "NO!!");
					},
				},
				new ChoiceItem()
				{
					Title = "Mark",
					Icon = UIImage.FromBundle (@"MarkRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 4 Selected"); 
						ShowSuccess(false, "NO!!");
					},
				},
			};

			mCircleButton = new JZMultiChoicesCircleButton (new CGPoint (this.View.Frame.Size.Width / 2, this.View.Frame.Size.Height / 2)
				, UIImage.FromBundle (@"send")
				, 30.0f
				, 120.0f
				, menuItems
				, true
				, 100
				,this
			);

			this.View.Add (mCircleButton);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public void ShowSimple()
		{
			Console.WriteLine ("Do something without show the success/fail screen");
		}

		public void ShowSuccess(bool success, string message)
		{
			mCircleButton.ShowCompleteScreen (message, success);

		}

	}
}

