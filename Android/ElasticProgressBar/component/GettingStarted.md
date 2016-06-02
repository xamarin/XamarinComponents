
**Elastic Progress Bar** is a loading view that displays progress 
using a cool, springy animation on Android 3.0+.

## Usage

The `ElasticDownloadView` can be added to a layout file:

    <it.michelelacorte.elasticprogressbar.ElasticDownloadView
        android:id="@+id/elastic"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:layout_alignParentBottom="true"
        android:layout_centerInParent="true" />

And then in the code, we can control the progress:

    var elastic = FindViewById<ElasticDownloadView> (Resource.id.elastic);
    
    // initiate the intro animation
    elastic.StartIntro ();
    
    // set the progress percentage
    elastic.Progress = 10;
    
    // set the progress to complete
    elastic.Success ();
    
    // the progress can also fail
    elastic.Fail ();

### Customization

The `OptionView` type has several members that can be used to control the 
appearance of the progress bar and background view. The properties must be set 
before the view is inflated, or constructed:

 - **BackgroundColorSquare** 
 - **ColorCloud** 
 - **ColorFail**  
 - **ColorSuccess**  
 - **ColorProgressBar**  
 - **ColorProgressBarInProgress**  
 - **ColorProgressBarText** 
 - **NoBackground** 
 - **NoIntro** 

To prevent a customization from being used, the value can be reset, or 
switched off: 

 - IsBackgroundColorSquareSet
 - IsColorCloudSet
 - IsColorFailSet
 - IsColorSuccessSet
 - IsColorProgressBarSet
 - IsColorProgressBarInProgressSet
 - IsColorProgressBarTextSet

You can set color background with (default is `colorPrimary`), make sure to 
call this before `SetContentView()` of your activity:

    OptionView.BackgroundColorSquare = Color.Green;

To have no background, set the `NoBackground` property to true. Make sure to
do this without first `BackgroundColorSquare`. `BackgroundColorSquare` takes 
priority over NoBackground:

    OptionView.NoBackground = true;
