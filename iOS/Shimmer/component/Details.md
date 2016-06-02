
<iframe src="https://appetize.io/embed/kpvgwbg060a40huf6pgqaahdn8?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

`ShimmeringView` is a container `UIView` that will add a shmmering effect to any view that it
contains. It's useful as an unobtrusive loading indicator.

##  Using ShimmeringView

The easiest way to make use of `ShimmeringView` is by simply adding it to your view 
hierachy and adding a sub view to shimmer:

    // create the shimmering view
    shimmeringView = new ShimmeringView(View.Bounds);
    View.AddSubview(shimmeringView);
    
    // create the view to shimmer
    logoLabel = new UILabel(View.Bounds);
    logoLabel.Text = "Shimmer";
    logoLabel.TextColor = UIColor.White;
    logoLabel.Font = UIFont.FromName("HelveticaNeue-UltraLight", 60.0f);
    
    // add the view to shimmering view
    shimmeringView.AddSubview(logoLabel);
    
    // start the shimmering
    shimmeringView.Shimmering = true;

## Using Storyboards

`ShimmeringView` is fully supported in storyboards and the storyboard designer. 
There are several properties that are available in the designer andin the code.
