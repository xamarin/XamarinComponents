
<iframe src="https://appetize.io/embed/mxt9y9a0rzdem9mvpvn7u4nxcc?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

`VideoViewController` is a `UIViewController` with a background which is
video.

##  Using VideoViewController

The easiest way to make use of `VideoViewController` is by simply 
specifying a URL to a video:

    public class ViewController : VideoViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        
            var bundle = NSBundle.MainBundle;
            var resource = bundle.PathForResource("splash", "mp4");
            
            // set the video
            VideoUrl = new NSUrl(resource, false);
            
            // optionally crop the video
            StartTime = 12.0f;
            Duration = 4.0f;
            
            // make the video translucent
            Alpha = 0.7f;
        }
    }

## Properties

There are several properties that can be set to control how the video 
is rendered:

 * **VideoUrl**  
   Specifies the URL to the video file
 * **StartTime**  
   Specifies the start offest (in seconds) for the video clip
 * **Duration**  
   Specifies the length (in seconds) of the video clip
 * **BackgroundColor**  
   Specifies the background color behind the video
 * **Sound**  
   Specifies whether the sound is enabled or not
 * **Alpha**  
   Specifies the transparency of the video 
 * **Repeat**  
   Specifies whether to loop the video
 * **FillMode**  
   Specifies how the video will be displayed

## Methods

In addition to properties, there are several methods that can be used
to manage the video playback:

 * **PlayVideo()**  
   Starts/resumes video playback
 * **PauseVideo()**  
   Stops/pauses video playback

There are also several methods that will be invoked during playback, 
and can be overridden:

 * **OnVideoReady()**  
   Invoked when the video is loaded and about to start
 * **OnVideoComplete()**  
   Invoked when the video is finished playijng and about to loop
