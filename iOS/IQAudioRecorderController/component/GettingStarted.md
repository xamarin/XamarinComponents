# IQAudioRecorderController  

IQAudioRecorderController is a drop-in library allows you to record audio within the app with a nice User Interface. The Audio Recorder produces an .m4a file and returns the path so you can integrate it into your application.  
  
Usage  
----
  
### IQAudioRecorderController ###
  
`IQAudioRecorderController` is the pre-built ViewController that handles the recording and playback.  It uses `AVAudioPlayer` and `AVAudioRecorder` elements from the `AV Foundation` framework.  

To show the view controller create a new instance of `IQAudioRecorderController` and present it using an existing `UIViewController`.

			var controller = new IQAudioRecorderViewController();

			this.PresentViewController(controller,true,null);
  
Once the `IQAudioRecorderController` instance has been presented you can click the microphone to record, the play button to hear what you have recorded and delete to remove the recording.

Then click `Done` to keep the recording or cancel to delete it, and return to the previous view.

#### Properties ####

There are a number of properties on `IQAudioRecorderController` to help configure the appearance of the record view.

 - NormalTintColor
  - The color of the wave line when it is neither playing or recording
 - RecordingTintColor
  - The color of the wave line when it is recording
 - PlayingTintColor
  - The color of the wave line when it is playing

			var controller = new IQAudioRecorderViewController();

			controller.NormalTintColor = UIColor.Green;
			controller.RecordingTintColor = UIColor.Red;
			controller.PlayingTintColor = UIColor.Orange;

			this.PresentViewController(controller,true,null);

#### Events ####

Two events are provided on `IQAudioRecorderController` to respond to the results of the ViewController


 - OnCancel
  - Called when the `Cancel` button is pressed
 - OnRecordingCompleted
  - Called then the `Done` button is pressed.  Returns the path to the recording

			var controller = new IQAudioRecorderViewController();
			
			controller.OnCancel += AudioRecorderControllerDidCancel;
			controller.OnRecordingCompleted += AudioRecorderControllerCompleted;
			
			this.PresentViewController(controller,true,null);
			
### Async/Await Support ###

`IQAudioRecorderController` also provides an awaitable task static method which allows you to `await` the result of the recorder view controller.

`IQAudioRecorderViewController.ShowDialogTask` takes a `UIViewController` parameter and a `UIColor` array to set the wave line colors.


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

Attribution  
----
  
This component is a port to C# from the original Objective-C repo created by [Mohd Iftekhar Qurashi](https://github.com/hackiftekhar/IQAudioRecorderController)  
	
 

