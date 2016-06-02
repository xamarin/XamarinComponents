
`CircularProgress` is a `UIView` subclass with circular `UIProgressView` 
properties.

## Usage

It is easy to use `CircularProgressView`:

    var rect = new CGRect(140.0f, 30.0f, 40.0f, 40.0f);
	
    // create the control
    var progressView = new CircularProgressView(rect);
    progressView.RoundedCorners = true;
    progressView.TrackTintColor = UIColor.Clear;
    
    View.AddSubview(progressView);

In addition to the traditional circular progress view, 
`LabeledCircularProgressView` provides a `UILabel` to allow a textual 
representation of the current progress:

    // create the control
    var progressView = new LabeledCircularProgressView(rect);
    progressView.RoundedCorners = true;
    progressView.TrackTintColor = UIColor.Clear;
    
    // set the text
    progressView.ProgressLabel.Text = "10%";

## Members

Both progress views provide numerous properties and methods that can be
used to control the appearance of the progress view:

 * **TrackTintColor**  
   Represents the color of the track

 * **ProgressTintColor**  
   Represents the color of the progress bar

 * **InnerTintColor**  
   Represents the fill color of the circle

 * **RoundedCorners**  
   Indicates whether to use rounding for the progress bar

 * **ThicknessRatio**  
   Represents the ratio of the track to overall size (0.0 - 1.0)

 * **ClockwiseProgress**  
   Indicates whether the progress is clockwise or counter-clockwise

 * **Progress**  
   Represents the percent progress (0.0 - 1.0)

 * **IndeterminateDuration**  
   Represents the speed of the indeterminate animation

 * **Indeterminate**  
   Indicates whether the progress is indeterminate

 * **SetProgress()**  
   Provides a way to set the progress, along with an animation and other values
