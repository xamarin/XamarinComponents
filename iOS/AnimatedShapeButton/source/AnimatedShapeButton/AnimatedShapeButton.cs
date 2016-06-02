using System;
using System.ComponentModel;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("AnimatedShapeButton")]
    public class AnimatedShapeButton : UIControl
    {
        private static readonly nfloat fPI = (nfloat)Math.PI;

        private UIColor color = UIColor.FromRGB(255, 172, 51);
        private UIColor skeletonColor = UIColor.FromRGB(136, 153, 166);
        private UIColor circleColor = UIColor.FromRGB(255, 172, 51);
        private UIColor linesColor = UIColor.FromRGB(250, 120, 68);

        private CAKeyFrameAnimation circleTransform = CAKeyFrameAnimation.GetFromKeyPath("transform");
        private CAKeyFrameAnimation circleMaskTransform = CAKeyFrameAnimation.GetFromKeyPath("transform");
        private CAKeyFrameAnimation lineStrokeStart = CAKeyFrameAnimation.GetFromKeyPath("strokeStart");
        private CAKeyFrameAnimation lineStrokeEnd = CAKeyFrameAnimation.GetFromKeyPath("strokeEnd");
        private CAKeyFrameAnimation lineOpacity = CAKeyFrameAnimation.GetFromKeyPath("opacity");
        private CAKeyFrameAnimation imageTransform = CAKeyFrameAnimation.GetFromKeyPath("transform");

        private UIImage image;
        private CAShapeLayer imageShape;
        private CAShapeLayer circleShape;
        private CAShapeLayer circleMaskShape;
        private CAShapeLayer[] lineShapes;

        private bool isChecked = false;
        private double duration = 1.0;

        public AnimatedShapeButton()
        {
            Setup();
        }

        public AnimatedShapeButton(IntPtr handle)
            : base(handle)
        {
        }

        public AnimatedShapeButton(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        public AnimatedShapeButton(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Setup();
        }

        private void Setup()
        {
            // set up events
            TouchDown += OnTouchDown;
            TouchUpInside += OnTouchUpInside;
            TouchDragExit += OnTouchDragExit;
            TouchDragEnter += OnTouchDragEnter;
            TouchCancel += OnTouchCancel;
        }

        [Browsable(true)]
        [Export("Image")]
        public UIImage Image
        {
            get { return image; }
            set
            {
                CreateLayers(value);

                // set afterwards as this is used as a "is set up?" flag
                image = value;
            }
        }

        [Browsable(true)]
        [Export("Color")]
        public UIColor Color
        {
            get { return color; }
            set
            {
                color = value;

                if (isChecked && imageShape != null)
                {
                    imageShape.FillColor = color.CGColor;
                }
            }
        }

        [Browsable(true)]
        [Export("SkeletonColor")]
        public UIColor SkeletonColor
        {
            get { return skeletonColor; }
            set
            {
                skeletonColor = value;

                if (!isChecked && imageShape != null)
                {
                    imageShape.FillColor = skeletonColor.CGColor;
                }
            }
        }

        [Browsable(true)]
        [Export("CircleColor")]
        public UIColor CircleColor
        {
            get { return circleColor; }
            set
            {
                circleColor = value;

                if (circleShape != null)
                {
                    circleShape.FillColor = circleColor.CGColor;
                }
            }
        }

        [Browsable(true)]
        [Export("LinesColor")]
        public UIColor LinesColor
        {
            get { return linesColor; }
            set
            {
                linesColor = value;

                if (lineShapes != null)
                {
                    foreach (var line in lineShapes)
                    {
                        line.StrokeColor = linesColor.CGColor;
                    }
                }
            }
        }

        [Browsable(true)]
        [Export("Duration")]
        public double Duration
        {
            get { return duration; }
            set
            {
                duration = value;

                circleTransform.Duration = 0.333 * duration; // 0.0333 * 10
                circleMaskTransform.Duration = 0.333 * duration; // 0.0333 * 10
                lineStrokeStart.Duration = 0.6 * duration; //0.0333 * 18
                lineStrokeEnd.Duration = 0.6 * duration; //0.0333 * 18
                lineOpacity.Duration = 1.0 * duration; //0.0333 * 30
                imageTransform.Duration = 1.0 * duration; //0.0333 * 30
            }
        }

        [Browsable(true)]
        [Export("Checked")]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                if (isChecked == value)
                    return;

                isChecked = value;

                // make sure we are set up
                if (image == null)
                    return;

                if (isChecked)
                {
                    imageShape.FillColor = Color.CGColor;

                    // start animating
                    CATransaction.Begin();
                    circleShape.AddAnimation(circleTransform, "transform");
                    circleMaskShape.AddAnimation(circleMaskTransform, "transform");
                    imageShape.AddAnimation(imageTransform, "transform");
                    foreach (var line in lineShapes)
                    {
                        line.AddAnimation(lineStrokeStart, "strokeStart");
                        line.AddAnimation(lineStrokeEnd, "strokeEnd");
                        line.AddAnimation(lineOpacity, "opacity");
                    }
                    CATransaction.Commit();
                }
                else
                {
                    imageShape.FillColor = SkeletonColor.CGColor;

                    // remove all animations
                    circleShape.RemoveAllAnimations();
                    circleMaskShape.RemoveAllAnimations();
                    imageShape.RemoveAllAnimations();
                    foreach (var line in lineShapes)
                    {
                        line.RemoveAllAnimations();
                    }
                }

                OnCheckedChanged();
            }
        }

        public event EventHandler CheckedChanged;

        protected virtual void OnCheckedChanged()
        {
            var handler = CheckedChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected override void Dispose(bool disposing)
        {
            TouchDown -= OnTouchDown;
            TouchUpInside -= OnTouchUpInside;
            TouchDragExit -= OnTouchDragExit;
            TouchDragEnter -= OnTouchDragEnter;
            TouchCancel -= OnTouchCancel;

            base.Dispose(disposing);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            CreateLayers(image);
        }

        private void OnTouchDown(object sender, EventArgs e)
        {
            Layer.Opacity = 0.4f;
        }

        private void OnTouchUpInside(object sender, EventArgs e)
        {
            Layer.Opacity = 1.0f;

            Checked = !Checked;
        }

        private void OnTouchDragExit(object sender, EventArgs e)
        {
            Layer.Opacity = 1.0f;
        }

        private void OnTouchDragEnter(object sender, EventArgs e)
        {
            Layer.Opacity = 0.4f;
        }

        private void OnTouchCancel(object sender, EventArgs e)
        {
            Layer.Opacity = 1.0f;
        }

        private void CreateLayers(UIImage image)
        {
            if (image == null)
                return;

            Layer.Sublayers = new CALayer[0];

            var imageFrame = new CGRect(
                Frame.Width / 2f - Frame.Width / 4f,
                Frame.Height / 2f - Frame.Height / 4f,
                Frame.Width / 2f,
                Frame.Height / 2f);
            var imgCenterPoint = new CGPoint(imageFrame.GetMidX(), imageFrame.GetMidY());
            var lineFrame = new CGRect(
                imageFrame.X - imageFrame.Width / 4f,
                imageFrame.Y - imageFrame.Height / 4f,
                imageFrame.Width * 1.5f,
                imageFrame.Height * 1.5f);

            //===============
            // circle layer
            //===============
            circleShape = new CAShapeLayer();
            circleShape.Bounds = imageFrame;
            circleShape.Position = imgCenterPoint;
            circleShape.Path = UIBezierPath.FromOval(imageFrame).CGPath;
            circleShape.FillColor = circleColor.CGColor;
            circleShape.Transform = CATransform3D.MakeScale(0.0f, 0.0f, 1.0f);
            Layer.AddSublayer(circleShape);

            circleMaskShape = new CAShapeLayer();
            circleMaskShape.Bounds = imageFrame;
            circleMaskShape.Position = imgCenterPoint;
            circleMaskShape.FillRule = CAShapeLayer.FillRuleEvenOdd;
            circleShape.Mask = circleMaskShape;

            var maskPath = UIBezierPath.FromRect(imageFrame);
            maskPath.AddArc(imgCenterPoint, 0.1f, 0.0f, fPI * 2f, true);
            circleMaskShape.Path = maskPath.CGPath;

            //===============
            // line layer
            //===============
            lineShapes = new CAShapeLayer[5];
            for (int i = 0; i < 5; i++)
            {
                var line = new CAShapeLayer();
                line.Bounds = lineFrame;
                line.Position = imgCenterPoint;
                line.MasksToBounds = true;
                line.Actions = NSDictionary.FromObjectsAndKeys(
                    new[] { NSNull.Null, NSNull.Null },
                    new[] { (NSString)"strokeStart", (NSString)"strokeEnd" });
                line.StrokeColor = linesColor.CGColor;
                line.LineWidth = 1.25f;
                line.MiterLimit = 1.25f;
                var path = new CGPath();
                path.MoveToPoint(lineFrame.GetMidX(), lineFrame.GetMidY());
                path.AddLineToPoint(lineFrame.X + lineFrame.Width / 2f, lineFrame.Y);
                line.Path = path;
                line.LineCap = CAShapeLayer.CapRound;
                line.LineJoin = CAShapeLayer.JoinRound;
                line.StrokeStart = 0.0f;
                line.StrokeEnd = 0.0f;
                line.Opacity = 0.0f;
                line.Transform = CATransform3D.MakeRotation(fPI / 5f * (i * 2f + 1f), 0.0f, 0.0f, 1.0f);
                Layer.AddSublayer(line);
                lineShapes[i] = line;
            }

            //===============
            // image layer
            //===============
            imageShape = new CAShapeLayer();
            imageShape.Bounds = imageFrame;
            imageShape.Position = imgCenterPoint;
            imageShape.Path = UIBezierPath.FromRect(imageFrame).CGPath;
            imageShape.FillColor = Checked ? Color.CGColor : SkeletonColor.CGColor;
            imageShape.Actions = NSDictionary.FromObjectAndKey(NSNull.Null, (NSString)"fillColor");
            Layer.AddSublayer(imageShape);

            imageShape.Mask = new CALayer();
            imageShape.Mask.Contents = image.CGImage;
            imageShape.Mask.Bounds = imageFrame;
            imageShape.Mask.Position = imgCenterPoint;

            //==============================
            // circle transform animation
            //==============================
            circleTransform.Values = new[]
            {
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.0f,  0.0f,  1.0f)),    //  0/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.5f,  0.5f,  1.0f)),    //  1/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.0f,  1.0f,  1.0f)),    //  2/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.2f,  1.2f,  1.0f)),    //  3/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.3f,  1.3f,  1.0f)),    //  4/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.37f, 1.37f, 1.0f)),    //  5/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.4f,  1.4f,  1.0f)),    //  6/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.4f,  1.4f,  1.0f))     // 10/10
            };
            circleTransform.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/10
                NSNumber.FromDouble(0.1),    //  1/10
                NSNumber.FromDouble(0.2),    //  2/10
                NSNumber.FromDouble(0.3),    //  3/10
                NSNumber.FromDouble(0.4),    //  4/10
                NSNumber.FromDouble(0.5),    //  5/10
                NSNumber.FromDouble(0.6),    //  6/10
                NSNumber.FromDouble(1.0)     // 10/10
            };

            circleMaskTransform.Values = new[]
            {
                NSValue.FromCATransform3D(CATransform3D.Identity),                                                                 //  0/10
                NSValue.FromCATransform3D(CATransform3D.Identity),                                                                 //  2/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 1.25f,  imageFrame.Height * 1.25f,  1.0f)),   //  3/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 2.688f, imageFrame.Height * 2.688f, 1.0f)),   //  4/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 3.923f, imageFrame.Height * 3.923f, 1.0f)),   //  5/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 4.375f, imageFrame.Height * 4.375f, 1.0f)),   //  6/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 4.731f, imageFrame.Height * 4.731f, 1.0f)),   //  7/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 5.0f,   imageFrame.Height * 5.0f,   1.0f)),   //  9/10
                NSValue.FromCATransform3D(CATransform3D.MakeScale(imageFrame.Width * 5.0f,   imageFrame.Height * 5.0f,   1.0f))    // 10/10
            };
            circleMaskTransform.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/10
                NSNumber.FromDouble(0.2),    //  2/10
                NSNumber.FromDouble(0.3),    //  3/10
                NSNumber.FromDouble(0.4),    //  4/10
                NSNumber.FromDouble(0.5),    //  5/10
                NSNumber.FromDouble(0.6),    //  6/10
                NSNumber.FromDouble(0.7),    //  7/10
                NSNumber.FromDouble(0.9),    //  9/10
                NSNumber.FromDouble(1.0)     // 10/10
            };

            //==============================
            // line stroke animation
            //==============================
            lineStrokeStart.Values = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/18
                NSNumber.FromDouble(0.0),    //  1/18
                NSNumber.FromDouble(0.18),   //  2/18
                NSNumber.FromDouble(0.2),    //  3/18
                NSNumber.FromDouble(0.26),   //  4/18
                NSNumber.FromDouble(0.32),   //  5/18
                NSNumber.FromDouble(0.4),    //  6/18
                NSNumber.FromDouble(0.6),    //  7/18
                NSNumber.FromDouble(0.71),   //  8/18
                NSNumber.FromDouble(0.89),   // 17/18
                NSNumber.FromDouble(0.92)    // 18/18
            };
            lineStrokeStart.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/18
                NSNumber.FromDouble(0.056),  //  1/18
                NSNumber.FromDouble(0.111),  //  2/18
                NSNumber.FromDouble(0.167),  //  3/18
                NSNumber.FromDouble(0.222),  //  4/18
                NSNumber.FromDouble(0.278),  //  5/18
                NSNumber.FromDouble(0.333),  //  6/18
                NSNumber.FromDouble(0.389),  //  7/18
                NSNumber.FromDouble(0.444),  //  8/18
                NSNumber.FromDouble(0.944),  // 17/18
                NSNumber.FromDouble(1.0),    // 18/18
            };

            lineStrokeEnd.Values = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/18
                NSNumber.FromDouble(0.0),    //  1/18
                NSNumber.FromDouble(0.32),   //  2/18
                NSNumber.FromDouble(0.48),   //  3/18
                NSNumber.FromDouble(0.64),   //  4/18
                NSNumber.FromDouble(0.68),   //  5/18
                NSNumber.FromDouble(0.92),   // 17/18
                NSNumber.FromDouble(0.92)    // 18/18
            };
            lineStrokeEnd.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/18
                NSNumber.FromDouble(0.056),  //  1/18
                NSNumber.FromDouble(0.111),  //  2/18
                NSNumber.FromDouble(0.167),  //  3/18
                NSNumber.FromDouble(0.222),  //  4/18
                NSNumber.FromDouble(0.278),  //  5/18
                NSNumber.FromDouble(0.944),  // 17/18
                NSNumber.FromDouble(1.0),    // 18/18
            };

            lineOpacity.Values = new[]
            {
                NSNumber.FromDouble(1.0),    //  0/30
                NSNumber.FromDouble(1.0),    // 12/30
                NSNumber.FromDouble(0.0)     // 17/30
            };
            lineOpacity.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/30
                NSNumber.FromDouble(0.4),    // 12/30
                NSNumber.FromDouble(0.567)   // 17/30
            };

            //==============================
            // image transform animation
            //==============================
            imageTransform.Values = new[]
            {
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.0f,   0.0f,   1.0f)),  //  0/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.0f,   0.0f,   1.0f)),  //  3/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.2f,   1.2f,   1.0f)),  //  9/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.25f,  1.25f,  1.0f)),  // 10/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.2f,   1.2f,   1.0f)),  // 11/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.9f,   0.9f,   1.0f)),  // 14/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.875f, 0.875f, 1.0f)),  // 15/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.875f, 0.875f, 1.0f)),  // 16/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.9f,   0.9f,   1.0f)),  // 17/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.013f, 1.013f, 1.0f)),  // 20/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.025f, 1.025f, 1.0f)),  // 21/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(1.013f, 1.013f, 1.0f)),  // 22/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.96f,  0.96f,  1.0f)),  // 25/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.95f,  0.95f,  1.0f)),  // 26/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.96f,  0.96f,  1.0f)),  // 27/30
                NSValue.FromCATransform3D(CATransform3D.MakeScale(0.99f,  0.99f,  1.0f)),  // 29/30
                NSValue.FromCATransform3D(CATransform3D.Identity)                          // 30/30
            };
            imageTransform.KeyTimes = new[]
            {
                NSNumber.FromDouble(0.0),    //  0/30
                NSNumber.FromDouble(0.1),    //  3/30
                NSNumber.FromDouble(0.3),    //  9/30
                NSNumber.FromDouble(0.333),  // 10/30
                NSNumber.FromDouble(0.367),  // 11/30
                NSNumber.FromDouble(0.467),  // 14/30
                NSNumber.FromDouble(0.5),    // 15/30
                NSNumber.FromDouble(0.533),  // 16/30
                NSNumber.FromDouble(0.567),  // 17/30
                NSNumber.FromDouble(0.667),  // 20/30
                NSNumber.FromDouble(0.7),    // 21/30
                NSNumber.FromDouble(0.733),  // 22/30
                NSNumber.FromDouble(0.833),  // 25/30
                NSNumber.FromDouble(0.867),  // 26/30
                NSNumber.FromDouble(0.9),    // 27/30
                NSNumber.FromDouble(0.967),  // 29/30
                NSNumber.FromDouble(1.0)     // 30/30
            };

            // re-set the durations
            Duration = duration;
        }
    }
}
