using System;
using UIKit;
using XamDialogs;
using Foundation;
using System.Collections.Generic;

namespace XamDialogsSample
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			this.View.BackgroundColor = UIColor.White;

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
			
		partial void ShowDatePicker (UIButton sender)
		{

			var dialog = new XamDatePickerDialog(UIDatePickerMode.DateAndTime)
			{
				Title = "Date Picker",
				Message = "Please Pick a date and time",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Cancel",
				ConstantUpdates = false,
			};
				
			dialog.SelectedDate = new DateTime(1969,7,20,20,18,00,00);

			dialog.ButtonMode = ButtonMode.OkAndCancel;

			dialog.ValidateSubmit = (DateTime data)=>
			{
				return true;
			};

			dialog.OnSelectedDateChanged += (object s, DateTime e) => 
			{
				Console.WriteLine(e);
			};

			dialog.Show();

			// Static methods
//			var result = await XamDatePickerDialog.ShowDialogAsync(UIDatePickerMode.DateAndTime,"Date of Birth","Select your Date of Birth", new DateTime(1969,7,20,20,18,00,00) );
//
//			Console.WriteLine(result);
		}

		partial void ShowSimplePicker (UIButton sender)
		{
			var dialog = new XamSimplePickerDialog(new List<String>(){"Ringo","John","Paul", "George"})
			{
				Title = "Favorite Beatle",
				Message = "Pick your favorite beatle",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Cancel",
				ConstantUpdates = false,
			};
				
			dialog.OnSelectedItemChanged += (object s, string e) => 
			{
				Console.WriteLine(e);
			};

			dialog.SelectedItem = "John";

			dialog.Show();

			// Static methods
//			var result = await XamSimplePickerDialog.ShowDialogAsync("Who are you?","Select your name", new List<String>(){"Dave","Rob","Jamie"}, "Rob");
//			Console.WriteLine(result);
		}
	}
}

