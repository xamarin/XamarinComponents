using System;
using System.Drawing;
using MonoTouch.UIKit;

using AnimatedButtons;

namespace AnimatedShapeButtonSampleClassic
{
    partial class ViewController : UIViewController, IUIViewControllerTransitioningDelegate
    {
        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var width = (View.Frame.Width - 44f) / 4f;
            var x = width / 2f;
            var y = View.Frame.Height / 2f - 22f;

            // star button
            var starButton = new AnimatedShapeButton(new RectangleF(x, y, 44, 44));
            starButton.Image = UIImage.FromBundle("star");
            View.AddSubview(starButton);
            x += width;

            // heart button
            var heartButton = new AnimatedShapeButton(new RectangleF(x, y, 44, 44));
            heartButton.Image = UIImage.FromBundle("heart");
            heartButton.Color = UIColor.FromRGB(254, 110, 111);
            heartButton.CircleColor = UIColor.FromRGB(254, 110, 111);
            heartButton.LinesColor = UIColor.FromRGB(226, 96, 96);
            View.AddSubview(heartButton);
            x += width;

            // like button
            var likeButton = new AnimatedShapeButton(new RectangleF(x, y, 44, 44));
            likeButton.Image = UIImage.FromBundle("like");
            likeButton.Color = UIColor.FromRGB(52, 152, 219);
            likeButton.CircleColor = UIColor.FromRGB(52, 152, 219);
            likeButton.LinesColor = UIColor.FromRGB(41, 128, 185);
            View.AddSubview(likeButton);
            x += width;

            // smile button
            var smileButton = new AnimatedShapeButton(new RectangleF(x, y, 44, 44));
            smileButton.Image = UIImage.FromBundle("smile");
            smileButton.Color = UIColor.FromRGB(45, 204, 112);
            smileButton.CircleColor = UIColor.FromRGB(45, 204, 112);
            smileButton.LinesColor = UIColor.FromRGB(45, 195, 106);
            View.AddSubview(smileButton);
        }
    }
}
