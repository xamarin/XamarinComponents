
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
    
## Custom Animations

We can implement the `IJazzyEffect` interface to create a custom animation:

    public interface IJazzyEffect
    {
        void Animate(
            JazzyViewPager viewPager, 
            View left, 
            View right, 
            float positionOffset, 
            float positionOffsetPixels);
    }

As an example, to create a fade animation:

    public class FadeEffect : JazzyEffect
    {
        public override void Animate(
            JazzyViewPager viewPager,
            View left, 
            View right, 
            float positionOffset, 
            float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var translate = positionOffsetPixels;
                    left.Alpha = 1 - effectOffset;
                    left.TranslationX = translate;
                }
                if (right != null)
                {
                    right.Alpha = effectOffset;
                    var translate = 
                        positionOffsetPixels - 
                        viewPager.Width - 
                        viewPager.PageMargin;
                    right.TranslationX = translate;
                }
            }
        }
    }
    
Then, we can assign this animation to the `JazzyViewPager` using the 
`TransitionEffect` property:

    jazzy.TransitionEffect = new FadeEffect();
    
