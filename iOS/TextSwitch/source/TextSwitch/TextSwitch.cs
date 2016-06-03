using System;
using System.ComponentModel;
using System.Threading.Tasks;

#if __UNIFIED__
using AVFoundation;
using AVKit;
using CoreAnimation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;
using ObjCClass = ObjCRuntime.Class;
#else
using MonoTouch.AVFoundation;
using MonoTouch.CoreAnimation;
using MonoTouch.AVKit;
using MonoTouch.CoreMedia;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ObjCClass = MonoTouch.ObjCRuntime.Class;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    internal class RoundedLayer : CALayer
    {
        public override CGRect Frame
        {
            get { return base.Frame; }
            set
            {
                base.Frame = value;
                CornerRadius = Bounds.Height / 2.0f;
            }
        }
    }

    internal class RoundedView : UIView
    {
        [Export("layerClass")]
        private static ObjCClass LayerClass()
        {
            return new ObjCClass(typeof(RoundedLayer));
        }
    }

    [DesignTimeVisible(true), Category("Controls")]
    [Register("TextSwitch")]
    public class TextSwitch : UIControl
    {
        private bool on;
        private nfloat selectedBackgroundInset;

        private UIView titleLabelsContentView = new UIView();
        private UILabel leftTitleLabel = new UILabel();
        private UILabel rightTitleLabel = new UILabel();

        private UIView selectedTitleLabelsContentView = new UIView();
        private UILabel selectedLeftTitleLabel = new UILabel();
        private UILabel selectedRightTitleLabel = new UILabel();

        private UIView selectedBackgroundView = new RoundedView();
        private UIView titleMaskView = new RoundedView();

        private UITapGestureRecognizer tapGesture;
        private UIPanGestureRecognizer panGesture;

        private CGRect initialSelectedBackgroundViewFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSwitch"/> class.
        /// </summary>
        public TextSwitch()
        {
            Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSwitch"/> class when creating managed 
        /// representations of unmanaged objects. Called by the runtime.
        /// </summary>
        /// <param name="handle">Pointer (handle) to the unmanaged object.</param>
        public TextSwitch(IntPtr handle)
            : base(handle)
        {
            Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSwitch"/> class from the data stored in 
        /// the unarchiver object.
        /// </summary>
        /// <param name="coder">The unarchiver object.</param>
        public TextSwitch(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSwitch"/> class with the specified frame.
        /// </summary>
        /// <param name="frame">Frame used by the view, expressed in iOS points.</param>
        public TextSwitch(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        public TextSwitch(string leftTitle, string rightTitle, CGRect frame)
            : base(frame)
        {
            LeftTitle = leftTitle;
            RightTitle = rightTitle;

            Setup();
        }

        public TextSwitch(string leftTitle, string rightTitle)
        {
            LeftTitle = leftTitle;
            RightTitle = rightTitle;

            Setup();
        }

        private void Setup()
        {
            AnimationDuration = 0.3;
            AnimationSpringDamping = 0.75f;
            AnimationInitialSpringVelocity = 0.0f;
            SelectedBackgroundInset = 2.0f;

            // Setup views
            leftTitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            rightTitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;

            titleLabelsContentView.AddSubview(leftTitleLabel);
            titleLabelsContentView.AddSubview(rightTitleLabel);
            AddSubview(titleLabelsContentView);

            AddSubview(selectedBackgroundView);

            selectedTitleLabelsContentView.AddSubview(selectedLeftTitleLabel);
            selectedTitleLabelsContentView.AddSubview(selectedRightTitleLabel);
            AddSubview(selectedTitleLabelsContentView);

            leftTitleLabel.TextAlignment = UITextAlignment.Center;
            rightTitleLabel.TextAlignment = UITextAlignment.Center;
            selectedLeftTitleLabel.TextAlignment = UITextAlignment.Center;
            selectedRightTitleLabel.TextAlignment = UITextAlignment.Center;

            titleMaskView.BackgroundColor = UIColor.Black;
            selectedTitleLabelsContentView.Layer.Mask = titleMaskView.Layer;

            // Setup default colors
            BackgroundColor = BackgroundColor ?? UIColor.Black;
            SelectedBackgroundColor = SelectedBackgroundColor ?? UIColor.White;
            TitleColor = TitleColor ?? UIColor.White;
            SelectedTitleColor = SelectedTitleColor ?? UIColor.Black;

            // Gestures
            tapGesture = new UITapGestureRecognizer(gesture =>
            {
                var location = gesture.LocationInView(this);
                if (location.X < Bounds.Width / 2.0f)
                {
                    SetState(false, true);
                }
                else
                {
                    SetState(true, true);
                }
            });
            AddGestureRecognizer(tapGesture);

            panGesture = new UIPanGestureRecognizer(gesture =>
            {
                if (gesture.State == UIGestureRecognizerState.Began)
                {
                    initialSelectedBackgroundViewFrame = selectedBackgroundView.Frame;
                }
                else if (gesture.State == UIGestureRecognizerState.Changed)
                {
                    var frame = initialSelectedBackgroundViewFrame;
                    frame.X += gesture.TranslationInView(this).X;
                    frame.X = (nfloat)Math.Max(Math.Min(frame.X, Bounds.Width - SelectedBackgroundInset - frame.Width), SelectedBackgroundInset);
                    selectedBackgroundView.Frame = frame;
                    titleMaskView.Frame = selectedBackgroundView.Frame;
                }
                else if (gesture.State == UIGestureRecognizerState.Ended || gesture.State == UIGestureRecognizerState.Failed || gesture.State == UIGestureRecognizerState.Cancelled)
                {
                    var velocityX = gesture.VelocityInView(this).X;
                    if (velocityX >= 500.0f)
                    {
                        SetState(true,  true);
                    }
                    else if (velocityX < -500.0f)
                    {
                        SetState(false,  true);
                    }
                    else if (selectedBackgroundView.Center.X >= Bounds.Width / 2.0f)
                    {
                        SetState(true,  true);
                    }
                    else if (selectedBackgroundView.Center.X < Bounds.Size.Width / 2.0f)
                    {
                        SetState(false,  true);
                    }
                }
            });
            panGesture.ShouldBegin = gesture => selectedBackgroundView.Frame.Contains(gesture.LocationInView(this));
            AddGestureRecognizer(panGesture);
        }

        [Browsable(true)]
        [Export("AnimationDuration")]
        public double AnimationDuration { get; set; }

        [Browsable(true)]
        [Export("AnimationSpringDamping")]
        public nfloat AnimationSpringDamping { get; set; }

        [Browsable(true)]
        [Export("AnimationInitialSpringVelocity")]
        public nfloat AnimationInitialSpringVelocity { get; set; }

        [Browsable(true)]
        [Export("LeftTitle")]
        public string LeftTitle
        {
            get { return leftTitleLabel.Text; }
            set
            {
                leftTitleLabel.Text = value;
                selectedLeftTitleLabel.Text = value;
            }
        }

        [Browsable(true)]
        [Export("RightTitle")]
        public string RightTitle
        {
            get { return rightTitleLabel.Text; }
            set
            {
                rightTitleLabel.Text = value;
                selectedRightTitleLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TextSwitch"/> is on.
        /// </summary>
        /// <value><c>true</c> if on; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Export("On")]
        public bool On
        {
            get { return on; }
            set { SetState(value, true); }
        }

        [Browsable(true)]
        [Export("SelectedBackgroundInset")]
        public nfloat SelectedBackgroundInset
        {
            get { return selectedBackgroundInset; }
            set
            {
                selectedBackgroundInset = value;
                SetNeedsLayout();
            }
        }

        [Browsable(true)]
        [Export("SelectedBackgroundColor")]
        public UIColor SelectedBackgroundColor {
            get { return selectedBackgroundView.BackgroundColor; }
            set { selectedBackgroundView.BackgroundColor = value; }
        }

        [Browsable(true)]
        [Export("TitleColor")]
        public UIColor TitleColor
        {
            set
            {
                leftTitleLabel.TextColor = value;
                rightTitleLabel.TextColor = value;
            }
            get { return leftTitleLabel.TextColor; }
        }

        [Browsable(true)]
        [Export("SelectedTitleColor")]
        public UIColor SelectedTitleColor
        {
            set
            {
                selectedLeftTitleLabel.TextColor = value;
                selectedRightTitleLabel.TextColor = value;
            }
            get { return selectedLeftTitleLabel.TextColor; }
        }

        [Browsable(true)]
        [Export("TitleFont")]
        public UIFont TitleFont
        {
            set
            {
                leftTitleLabel.Font = value;
                rightTitleLabel.Font = value;
                selectedLeftTitleLabel.Font = value;
                selectedRightTitleLabel.Font = value;
            }
            get { return leftTitleLabel.Font; }
        }
        
        /// <summary>
        /// This method changes the state of the switch, and will animate the change if specified.
        /// </summary>
        /// <param name="newState">The new value of the switch.</param>
        /// <param name="animated">If the change in state should be animated.</param>
        public virtual void SetState(bool newState, bool animated)
        {
            on = newState;
            if (animated)
            {
                UIView.AnimateNotify(
                    AnimationDuration,
                    0.0,
                    AnimationSpringDamping,
                    AnimationInitialSpringVelocity,
                    UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseOut,
                    () => LayoutSubviews(),
                    finished =>
                    {
                        if (finished)
                        {
                            SendActionForControlEvents(UIControlEvent.ValueChanged);
                        }
                    });
            }
            else
            {
                LayoutSubviews();
                SendActionForControlEvents(UIControlEvent.ValueChanged);
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat selectedIndex = on ? 1 : 0;

            var selectedBackgroundWidth = Bounds.Width / 2.0f - selectedBackgroundInset * 2.0f;
            selectedBackgroundView.Frame = new CGRect(
                selectedBackgroundInset + selectedIndex * (selectedBackgroundWidth + selectedBackgroundInset * 2.0f),
                selectedBackgroundInset,
                selectedBackgroundWidth,
                Bounds.Height - selectedBackgroundInset * 2.0f);
            titleMaskView.Frame = selectedBackgroundView.Frame;

            titleLabelsContentView.Frame = Bounds;
            selectedTitleLabelsContentView.Frame = Bounds;

            var titleLabelMaxWidth = selectedBackgroundWidth;
            var titleLabelMaxHeight = Bounds.Height - selectedBackgroundInset * 2.0f;

            var leftTitleLabelSize = leftTitleLabel.SizeThatFits(new CGSize(titleLabelMaxWidth, titleLabelMaxHeight));
            leftTitleLabelSize.Width = (nfloat)Math.Min(leftTitleLabelSize.Width, titleLabelMaxWidth);

            var leftTitleLabelOrigin = new CGPoint(
                (nfloat)Math.Floor((Bounds.Width / 2.0f - leftTitleLabelSize.Width) / 2.0f),
                (nfloat)Math.Floor((Bounds.Height - leftTitleLabelSize.Height) / 2.0f));
            var leftTitleLabelFrame = new CGRect(leftTitleLabelOrigin, leftTitleLabelSize);
            leftTitleLabel.Frame = leftTitleLabelFrame;
            selectedLeftTitleLabel.Frame = leftTitleLabelFrame;

            var rightTitleLabelSize = rightTitleLabel.SizeThatFits(new CGSize(titleLabelMaxWidth, titleLabelMaxHeight));
            rightTitleLabelSize.Width = (nfloat)Math.Min(rightTitleLabelSize.Width, titleLabelMaxWidth);

            var rightTitleLabelOrigin = new CGPoint(
                (nfloat)Math.Floor(Bounds.Size.Width / 2.0f + (Bounds.Width / 2.0f - rightTitleLabelSize.Width) / 2.0f),
                (nfloat)Math.Floor((Bounds.Height - rightTitleLabelSize.Height) / 2.0f));
            var rightTitleLabelFrame = new CGRect(rightTitleLabelOrigin, rightTitleLabelSize);
            rightTitleLabel.Frame = rightTitleLabelFrame;
            selectedRightTitleLabel.Frame = rightTitleLabelFrame;
        }

        [Export("layerClass")]
        private static ObjCClass LayerClass()
        {
            return new ObjCClass(typeof(RoundedLayer));
        }
    }
}
