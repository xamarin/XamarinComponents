using System;
using System.Drawing;
using MonoTouch.UIKit;

using AnimatedButtons;

namespace TextSwitchSampleClassic
{
    partial class ViewController : UIViewController
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

            var runkeeperSwitch = new TextSwitch("Feed", "Leaderboard", new RectangleF(50, 20, View.Bounds.Width - 100, 30));
            runkeeperSwitch.BackgroundColor = UIColor.FromRGB(229, 163, 48);
            runkeeperSwitch.SelectedBackgroundColor = UIColor.White;
            runkeeperSwitch.TitleColor = UIColor.White;
            runkeeperSwitch.SelectedTitleColor = UIColor.FromRGB(255, 196, 92);
            runkeeperSwitch.TitleFont = UIFont.FromName("HelveticaNeue-Medium", 13);
            runkeeperSwitch.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            runkeeperSwitch.ValueChanged += delegate
            {
                Console.WriteLine(runkeeperSwitch.On ? "Using the leaderboard" : "Using the feed");
            };
            View.AddSubview(runkeeperSwitch);

            var runkeeperSwitch2 = new TextSwitch(new RectangleF(50, 70, View.Bounds.Width - 100, 30));
            runkeeperSwitch2.LeftTitle = "Weekly";
            runkeeperSwitch2.RightTitle = "Monthly";
            runkeeperSwitch2.BackgroundColor = UIColor.FromRGB(239, 95, 49);
            runkeeperSwitch2.SelectedBackgroundColor = UIColor.White;
            runkeeperSwitch2.TitleColor = UIColor.White;
            runkeeperSwitch2.SelectedTitleColor = UIColor.FromRGB(239, 95, 49);
            runkeeperSwitch2.TitleFont = UIFont.FromName("HelveticaNeue-Medium", 13);
            runkeeperSwitch2.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            runkeeperSwitch2.ValueChanged += delegate
            {
                Console.WriteLine(runkeeperSwitch2.On ? "Every month" : "Every week");
            };
            View.AddSubview(runkeeperSwitch2);

            var runkeeperSwitch3 = new TextSwitch();
            runkeeperSwitch3.LeftTitle = "Super long left title";
            runkeeperSwitch3.RightTitle = "Super long right title";
            runkeeperSwitch3.BackgroundColor = UIColor.FromRGB(239, 95, 49);
            runkeeperSwitch3.SelectedBackgroundColor = UIColor.White;
            runkeeperSwitch3.TitleColor = UIColor.White;
            runkeeperSwitch3.SelectedTitleColor = UIColor.FromRGB(239, 95, 49);
            runkeeperSwitch3.TitleFont = UIFont.FromName("HelveticaNeue-Medium", 13);
            runkeeperSwitch3.Frame = new RectangleF(50, 120, View.Bounds.Width - 100, 30);
            runkeeperSwitch3.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            runkeeperSwitch3.ValueChanged += delegate
            {
                Console.WriteLine(runkeeperSwitch3.On ? "On the right" : "On the left");
            };
            View.AddSubview(runkeeperSwitch3);
        }
    }
}
