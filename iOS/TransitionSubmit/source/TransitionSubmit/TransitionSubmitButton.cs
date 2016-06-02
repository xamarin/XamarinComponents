using System;
using System.ComponentModel;
using System.Threading.Tasks;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("TransitionSubmitButton")]
    public class TransitionSubmitButton : UIButton, IUIViewControllerTransitioningDelegate
    {
        private static UIColor Pink = UIColor.FromRGBA(0.992157f, 0.215686f, 0.403922f, 1.0f);
        private static UIColor DarkPink = UIColor.FromRGBA(0.798012f, 0.171076f, 0.321758f, 1.0f);

        private CAMediaTimingFunction springGoEase = CAMediaTimingFunction.FromControlPoints(0.45f, -0.36f, 0.44f, 0.92f);
        private CAMediaTimingFunction shrinkCurve = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
        private CAMediaTimingFunction expandCurve = CAMediaTimingFunction.FromControlPoints(0.95f, 0.02f, 1.0f, 0.05f);
        private double shrinkDuration = 0.1;

        private string cachedTitle = null;
        private bool isAnimating = false;
        private SpinnerLayer spinner = null;
        private UIColor highlightedBackgroundColor = DarkPink;
        private UIColor normalBackgroundColor = Pink;

        public TransitionSubmitButton()
        {
            Setup();
        }

        public TransitionSubmitButton(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        public TransitionSubmitButton(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        public TransitionSubmitButton(IntPtr handle)
            : base(handle)
        {
            Setup();
        }

        private void Setup()
        {
            Layer.CornerRadius = Frame.Height / 2.0f;
            ClipsToBounds = true;
            SetBackgroundColor();
        }

        [Browsable(true)]
        [Export("HighlightedBackgroundColor")]
        public UIColor HighlightedBackgroundColor
        {
            get { return highlightedBackgroundColor; }
            set
            {
                highlightedBackgroundColor = value;
                SetBackgroundColor();
            }
        }

        [Browsable(true)]
        [Export("NormalBackgroundColor")]
        public UIColor NormalBackgroundColor
        {
            get { return normalBackgroundColor; }
            set
            {
                normalBackgroundColor = value;
                SetBackgroundColor();
            }
        }

        public override bool Highlighted
        {
            get { return base.Highlighted; }
            set
            {
                base.Highlighted = value;
                SetBackgroundColor();
            }
        }

        private SpinnerLayer Spinner
        {
            get
            {
                if (spinner == null)
                {
                    spinner = new SpinnerLayer(Frame);
                    Layer.AddSublayer(spinner);
                }
                return spinner;
            }
        }

        public event EventHandler AnimationComplete;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            Layer.CornerRadius = Frame.Height / 2.0f;
        }

        private void SetBackgroundColor()
        {
            if (Highlighted)
            {
                BackgroundColor = HighlightedBackgroundColor;
            }
            else
            {
                BackgroundColor = NormalBackgroundColor;
            }
        }

        public void StartLoadingAnimation()
        {
            if (!isAnimating)
            {
                isAnimating = true;

                cachedTitle = Title(UIControlState.Normal);
                SetTitle("", UIControlState.Normal);
                Shrink();

                CreateScheduledTimer(shrinkDuration - 0.25, () =>
                {
                    Spinner.Animation();
                });
            }
        }

        public void StartFinishAnimation(Action completion)
        {
            StartFinishAnimation(0, completion);
        }

        public void StartFinishAnimation(double delay, Action completion)
        {
            CreateScheduledTimer(delay, () => 
            {
                Expand(completion);
                Spinner.StopAnimation();
            });
        }

        public Task StartFinishAnimationAsync(double delay)
        {
            var tcs = new TaskCompletionSource<bool>();
            StartFinishAnimation(delay, () =>
            {
                tcs.SetResult(true);
            });
            return tcs.Task;
        }

        public Task StartFinishAnimationAsync()
        {
            return StartFinishAnimationAsync(0);
        }

        public Task<T> AnimateAsync<T>(Func<Task<T>> function)
        {
            return AnimateAsync(function());
        }

        public async Task<T> AnimateAsync<T>(Task<T> task)
        {
            StartLoadingAnimation();
            var result = await task;
            await StartFinishAnimationAsync();
            return result;
        }

        public Task AnimateAsync(Func<Task> function)
        {
            return AnimateAsync(function());
        }

        public async Task AnimateAsync(Task task)
        {
            StartLoadingAnimation();
            await task;
            await StartFinishAnimationAsync();
        }

        public new void Animate(double duration, Action completion)
        {
            StartLoadingAnimation();
            StartFinishAnimation(duration, completion);
        }

        protected virtual void OnAnimationComplete()
        {
            var handler = AnimationComplete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void Shrink()
        {
            var shrinkAnim = CABasicAnimation.FromKeyPath("bounds.size.width");
            shrinkAnim.From = NSNumber.FromDouble(Frame.Width);
            shrinkAnim.To = NSNumber.FromDouble(Frame.Height);
            shrinkAnim.Duration = shrinkDuration;
            shrinkAnim.TimingFunction = shrinkCurve;
            shrinkAnim.FillMode = CAFillMode.Forwards;
            shrinkAnim.RemovedOnCompletion = false;

            Layer.AddAnimation(shrinkAnim, shrinkAnim.KeyPath);
        }

        private void Expand(Action completion)
        {
            var expandAnim = CABasicAnimation.FromKeyPath("transform.scale");
            expandAnim.From = NSNumber.FromFloat(1.0f);
            expandAnim.To = NSNumber.FromFloat(26.0f);
            expandAnim.TimingFunction = expandCurve;
            expandAnim.Duration = 0.3;
            expandAnim.FillMode = CAFillMode.Forwards;
            expandAnim.RemovedOnCompletion = false;
            expandAnim.AnimationStopped += delegate
            {
                OnAnimationComplete();
                if (completion != null)
                {
                    completion();
                }
                CreateScheduledTimer(1.0, () =>
                {
                    ReturnToOriginalState();
                    
                    isAnimating = false;
                });
            };

            Layer.AddAnimation(expandAnim, expandAnim.KeyPath);
        }

        private void ReturnToOriginalState()
        {
            Layer.RemoveAllAnimations();
            SetTitle(cachedTitle, UIControlState.Normal);
        }

        private static void CreateScheduledTimer(double delay, Action action)
        {
#if __UNIFIED__
            NSTimer.CreateScheduledTimer(delay, timer =>
            {
                action();
            });
#else
            NSTimer.CreateScheduledTimer(delay, () =>
            {
                action();
            });
#endif
        }
    }
}
