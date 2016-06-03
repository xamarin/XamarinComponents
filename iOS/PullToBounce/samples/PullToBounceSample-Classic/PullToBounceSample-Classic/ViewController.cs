using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using PullToBounce;

namespace PullToBounceSampleClassic
{
    partial class ViewController : UIViewController, IUIViewControllerTransitioningDelegate
    {
        private static UIColor blue = UIColor.FromRGBA(0.421593f, 0.657718f, 0.972549f, 1.0f);
        private static UIColor lightBlue = UIColor.FromRGBA(0.700062f, 0.817345f, 0.972549f, 1.0f);
        
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = blue;
            var bodyView = new UIView();
            var bodyFrame = View.Frame;
            bodyFrame.Y += 20f + 44f;
            bodyView.Frame = bodyFrame;
            View.AddSubview(bodyView);

            var tableViewWrapper = new PullToBounceWrapper(View.Frame);
            tableViewWrapper.BallColor = UIColor.White;
            bodyView.AddSubview(tableViewWrapper);

            var tableView = new SampleTableView(View.Frame, UITableViewStyle.Plain);
            tableView.BackgroundColor = UIColor.Clear;
            tableViewWrapper.AddSubview(tableView);

            tableViewWrapper.RefreshStarted += async delegate
            {
                await Task.Delay(2000);
                tableViewWrapper.StopLoadingAnimation();
            };

            MakeMock();
        }

        private void MakeMock()
        {
            var headerView = new UIView();
            headerView.Frame = new RectangleF(0, 0, View.Frame.Width, 64);
            headerView.BackgroundColor = lightBlue;
            View.AddSubview(headerView);

            var headerLine = new UIView();
            headerLine.Frame = new RectangleF(0, 0, 120, 8);
            headerLine.Layer.CornerRadius = headerLine.Frame.Height / 2f;
            headerLine.BackgroundColor = UIColor.White.ColorWithAlpha(0.8f);
            headerLine.Center = new PointF(headerView.Frame.GetMidX(), 20 + 44 / 2);
            headerView.AddSubview(headerLine);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        private class SampleTableView : UITableView, IUITableViewDelegate, IUITableViewDataSource
        {
            public SampleTableView(RectangleF frame, UITableViewStyle style)
                    : base(frame, style)
            {
                WeakDelegate = this;
                WeakDataSource = this;
                RegisterClassForCellReuse(typeof(SampleCell), "SampleCell");
                SeparatorStyle = UITableViewCellSeparatorStyle.None;
            }

            [Export("tableView:numberOfRowsInSection:")]
            public int RowsInSection(UITableView tableView, int section)
            {
                return 30;
            }

            [Export("tableView:cellForRowAtIndexPath:")]
            public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                return tableView.DequeueReusableCell("SampleCell", indexPath);
            }

            [Export("tableView:heightForRowAtIndexPath:")]
            public virtual float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 92;
            }

            private class SampleCell : UITableViewCell
            {
                public SampleCell(IntPtr handle)
                    : base(handle)
                {
                    var iconMock = new UIView();
                    iconMock.BackgroundColor = lightBlue;
                    iconMock.Frame = new RectangleF(16, 16, 60, 60);
                    iconMock.Layer.CornerRadius = 10;
                    AddSubview(iconMock);

                    var lineLeft = iconMock.Frame.Right + 16.0f;
                    var lineMargin = 12.0f;

                    var line1 = new RectangleF(lineLeft, 12 + lineMargin, 100, 6);
                    var line2 = new RectangleF(lineLeft, line1.Bottom + lineMargin, 160, 5);
                    var line3 = new RectangleF(lineLeft, line2.Bottom + lineMargin, 180, 5);
                    AddLine(line1);
                    AddLine(line2);
                    AddLine(line3);

                    var sepalator = new UIView();
                    sepalator.Frame = new RectangleF(0, 92 - 1, Frame.Width, 1);
                    sepalator.BackgroundColor = UIColor.Gray.ColorWithAlpha(0.2f);
                    AddSubview(sepalator);
                }

                private void AddLine(RectangleF frame)
                {
                    var line = new UIView(frame);
                    line.Layer.CornerRadius = frame.Height / 2.0f;
                    line.BackgroundColor = lightBlue;
                    AddSubview(line);
                }
            }
        }
    }
}
