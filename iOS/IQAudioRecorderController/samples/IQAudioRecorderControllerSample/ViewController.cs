using System;

using UIKit;
using IQAudioRecorderController;

namespace IQAudioRecorderControllerSample
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
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		/// <summary>
		/// Raises the show clicked event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void OnShowClicked (UIButton sender)
		{
			var controller = new IQAudioRecorderViewController();

			controller.NormalTintColor = UIColor.Green;
			controller.RecordingTintColor = UIColor.Red;
			controller.PlayingTintColor = UIColor.Orange;

			controller.OnCancel += AudioRecorderControllerDidCancel;
			controller.OnRecordingCompleted += AudioRecorderControllerCompleted;
			this.PresentViewController(controller,true,null);

		}

		/// <summary>
		/// Raises the show dialog task event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		async partial void OnShowDialogTask (UIButton sender)
		{
			var colors = new UIColor[]{UIColor.Green, UIColor.Red, UIColor.Orange};

			var result = await IQAudioRecorderViewController.ShowDialogTask(this, colors);

			if (!string.IsNullOrWhiteSpace(result))
			{
				ShowMessage("File recorded", result);
			}
			else
			{
				ShowMessage("Record cancelled", "Recording was canelled");
			}
		}


		#region IIQAudioRecorderControllerDelegate implementation

		public void AudioRecorderControllerCompleted (IQAudioRecorderController.IQAudioRecorderViewController controller, string filePath)
		{
			controller.OnCancel -= AudioRecorderControllerDidCancel;
			controller.OnRecordingCompleted -= AudioRecorderControllerCompleted;


			ShowMessage("File recorded", filePath);

		}

		public void AudioRecorderControllerDidCancel (IQAudioRecorderController.IQAudioRecorderViewController controller)
		{			
			controller.OnCancel -= AudioRecorderControllerDidCancel;
			controller.OnRecordingCompleted -= AudioRecorderControllerCompleted;

			ShowMessage("Record cancelled", "Recording was canelled");

		}

		public void ShowMessage(String title, String message)
		{
			var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);

			alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (obj)=>
				{
					alert.DismissViewController(true,null);

				}));
			
			this.PresentViewController (alert, true, null);
		}
			
		#endregion
	}
}

