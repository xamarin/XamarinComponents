using System;
using System.Drawing;

using Foundation;
using UIKit;

using Facebook.Pop;

namespace PopSample
{
    public partial class PopSampleViewController : UIViewController
    {
        public PopSampleViewController (IntPtr handle)
            : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
			
            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            PopButton.TouchUpInside += (object sender, EventArgs e) => {
                var springAnimation = POPSpringAnimation.AnimationWithPropertyNamed (POPAnimation.ViewCenter);
                var newCenter = PopButton.Center;

                newCenter.Y = newCenter.Y + 200;

                springAnimation.ToValue = NSValue.FromCGPoint (newCenter);
                springAnimation.CompletionAction = (POPAnimation anim, bool arg2) => {

                    var decayAnimation = POPDecayAnimation.AnimationWithPropertyNamed (POPAnimation.LayerPositionY);
                    decayAnimation.Velocity = new NSNumber (-800);
                    decayAnimation.AutoReverse = true;
                    decayAnimation.RepeatForever = true;
                    decayAnimation.ClampMode = POPAnimationClampFlags.Both;

                    ImageMonkey.AddAnimation (decayAnimation, "Decay");
                };

                ImageMonkey.AddAnimation (springAnimation, "Center");
            };
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
        }

        public override void ViewWillDisappear (bool animated)
        {
            base.ViewWillDisappear (animated);
        }

        public override void ViewDidDisappear (bool animated)
        {
            base.ViewDidDisappear (animated);
        }

        #endregion
    }
}

