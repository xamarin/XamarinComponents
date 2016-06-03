
<iframe src="https://appetize.io/embed/r23uhhjp7gt8h37xerdh1jn8cm?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

An easy to use `ViewPager` that adds an awesome set of custom swiping animations. 

## Usage

There are several animations that can be used, and many more can be created:

 * Standard (deafult animation)
 * Accordion
 * Cube
 * Fade
 * Flip
 * FlipAway
 * RotateUp
 * RotateDown
 * Stack
 * Tablet
 * Zoom

The `JazzyViewPager` is a small extension to the support `ViewPager`
and functions very much the same:

    JazzyViewPager jazzy = ...
    jazzy.TransitionEffect = JazzyEffects.Tablet;
    jazzy.Adapter = new MyJazzyAdapter(jazzy);
    
The adapter is also an extension, but with the additional support for tracking 
the pages using the `SetObjectForPosition` and `FindViewFromObject` methods:

    private class MyJazzyAdapter : JazzyPagerAdapter
    {
		public MainAdapter(JazzyViewPager jazzy)
			: base(jazzy)
		{
		}

		public override Java.Lang.Object InstantiateItem(
            ViewGroup container, int position)
		{
            // create and add the view
			var view = ...
			container.AddView(
                view, 
                ViewGroup.LayoutParams.MatchParent, 
                ViewGroup.LayoutParams.MatchParent);
                
            // let the jazzy bit know about the object
			SetObjectForPosition(view, position);
            
            // continue as normal
			return view;
		}

		public override void DestroyItem(
            ViewGroup container, int position, Java.Lang.Object obj)
		{
            // get the real view from the jazzy bit
            var view = FindViewFromObject(position);
            
            // continue as normal
			container.RemoveView(view);
		}
	}
    