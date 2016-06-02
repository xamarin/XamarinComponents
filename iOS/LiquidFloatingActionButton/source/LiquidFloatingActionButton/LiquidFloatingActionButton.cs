using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

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
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("LiquidFloatingActionButton")]
    public class LiquidFloatingActionButton : UIControl
    {
        private nfloat internalRadiusRatio = 20.0f / 56.0f;

        private bool _enableShadow = true;
        private UIColor color = UIColor.FromRGB(82, 112, 235);

        private CAShapeLayer plusLayer = new CAShapeLayer();
        private CAShapeLayer circleLayer = new CAShapeLayer();

        private bool touching = false;
        private nfloat plusRotation = 0.0f;

        private CircleLiquidBaseView baseView = new CircleLiquidBaseView();
        private UIView liquidView = new UIView();

        public LiquidFloatingActionButton()
        {
            Setup();
        }

        public LiquidFloatingActionButton(IntPtr handle)
            : base(handle)
        {
        }

        public LiquidFloatingActionButton(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        public LiquidFloatingActionButton(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Setup();
        }

        [Browsable(true)]
        [Export("CellRadiusRatio")]
        public nfloat CellRadiusRatio { get; set; }

        [Browsable(true)]
        [Export("AnimateStyle")]
        public AnimateStyle AnimateStyle
        {
            get { return baseView.AnimateStyle; }
            set { baseView.AnimateStyle = value; }
        }

        [Browsable(true)]
        [Export("EnableShadow")]
        public bool EnableShadow
        {
            get { return _enableShadow; }
            set
            {
                _enableShadow = value;
                SetNeedsDisplay();
            }
        }

        public event EventHandler<CellSelectedEventArgs> CellSelected;

        public IEnumerable<LiquidFloatingCell> Cells { get; set; }

        [Browsable(true)]
        [Export("Responsible")]
        public bool Responsible { get; set; }

        public bool IsClosed
        {
            get { return plusRotation == 0; }
            set
            {
                if (value)
                {
                    Close();
                }
                else
                {
                    Open();
                }
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
                baseView.Color = color;
            }
        }

        private void InsertCell(LiquidFloatingCell cell)
        {
            var radius = Frame.Width * CellRadiusRatio;
            var offset = (Frame.Width - radius) / 2f;
            cell.Frame = new CGRect(offset, offset, radius, radius);
            cell.Color = Color;
            cell.ActionButton = this;
            InsertSubviewAbove(cell, baseView);
        }

        private LiquidFloatingCell[] CellArray()
        {
            if (Cells != null)
            {
                return Cells.ToArray();
            }
            return new LiquidFloatingCell[0];
        }

        /// <summary>
        /// Opens all cells.
        /// </summary>
        public void Open()
        {
            // rotate plus icon
            plusLayer.AddAnimation(PlusKeyFrame(true), "plusRot");
            plusRotation = CoreGraphicsExtensions.PI * 0.25f; // 45 degree

            var cells = CellArray();
            foreach (var cell in cells)
            {
                InsertCell(cell);
            }

            baseView.Open(cells);
            SetNeedsDisplay();
        }

        /// <summary>
        /// Closes all cells.
        /// </summary>
        public void Close()
        {
            // rotate plus icon
            plusLayer.AddAnimation(PlusKeyFrame(false), "plusRot");
            plusRotation = 0;

            baseView.Close(CellArray());
            SetNeedsDisplay();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            DrawCircle();
            DrawShadow();
            DrawPlus(plusRotation);
        }

        private void DrawCircle()
        {
            circleLayer.Frame = new CGRect(CGPoint.Empty, Frame.Size);
            circleLayer.CornerRadius = Frame.Width * 0.5f;
            circleLayer.MasksToBounds = true;
            if (touching && Responsible)
            {
                circleLayer.BackgroundColor = Color.White(0.5f).CGColor;
            }
            else
            {
                circleLayer.BackgroundColor = Color.CGColor;
            }
        }

        private void DrawPlus(nfloat rotation)
        {
            plusLayer.Frame = new CGRect(CGPoint.Empty, Frame.Size);
            plusLayer.LineCap = CAShapeLayer.CapRound;
            plusLayer.StrokeColor = UIColor.White.CGColor; // TODO: customizable
            plusLayer.LineWidth = 3.0f;

            plusLayer.Path = PathPlus(rotation).CGPath;
        }

        private void DrawShadow()
        {
            if (EnableShadow)
            {
                circleLayer.AppendShadow();
            }
        }

        private UIBezierPath PathPlus(nfloat rotation)
        {
            var radius = Frame.Width * internalRadiusRatio * 0.5f;
            var center = Center.Minus(Frame.Location);
            var points = new[] {
                CoreGraphicsExtensions.CirclePoint(center, radius,  CoreGraphicsExtensions.PI2 * 0f + rotation),
                CoreGraphicsExtensions.CirclePoint(center, radius, CoreGraphicsExtensions.PI2 * 1f + rotation),
                CoreGraphicsExtensions.CirclePoint(center, radius, CoreGraphicsExtensions.PI2 * 2f + rotation),
                CoreGraphicsExtensions.CirclePoint(center, radius, CoreGraphicsExtensions.PI2 * 3f + rotation)
            };
            var path = new UIBezierPath();
            path.MoveTo(points[0]);
            path.AddLineTo(points[2]);
            path.MoveTo(points[1]);
            path.AddLineTo(points[3]);
            return path;
        }

        private CAKeyFrameAnimation PlusKeyFrame(bool closed)
        {
            var paths = closed ? new[] {
                    PathPlus(CoreGraphicsExtensions.PI * 0f),
                    PathPlus(CoreGraphicsExtensions.PI * 0.125f),
                    PathPlus(CoreGraphicsExtensions.PI * 0.25f),
            } : new[] {
                    PathPlus(CoreGraphicsExtensions.PI* 0.25f),
                    PathPlus(CoreGraphicsExtensions.PI* 0.125f),
                    PathPlus(CoreGraphicsExtensions.PI* 0f),
            };
            var anim = CAKeyFrameAnimation.GetFromKeyPath("path");
            anim.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
            anim.SetValues(paths.Select(x => x.CGPath).ToArray());
            anim.Duration = 0.5f;
            anim.RemovedOnCompletion = true;
            anim.FillMode = CAFillMode.Forwards;
            return anim;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            touching = true;
            SetNeedsDisplay();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            touching = false;
            SetNeedsDisplay();
            DidTapped();
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            touching = false;
            SetNeedsDisplay();
        }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            foreach (var cell in CellArray())
            {
                var pointForTargetView = cell.ConvertPointFromView(point, this);
                if (cell.Bounds.Contains(pointForTargetView))
                {
                    if (cell.UserInteractionEnabled)
                    {
                        return cell.HitTest(pointForTargetView, uievent);
                    }
                }
            }
            return base.HitTest(point, uievent);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var frame = new CGRect(CGPoint.Empty, Frame.Size);
            liquidView.Frame = frame;
            baseView.Frame = frame;
        }

        private void Setup()
        {
            CellRadiusRatio = 0.75f;
            Cells = new List<LiquidFloatingCell>();
            BackgroundColor = UIColor.Clear;
            ClipsToBounds = false;
            Responsible = true;

            baseView.Setup(this);
            AddSubview(baseView);

            liquidView.UserInteractionEnabled = false;
            AddSubview(liquidView);

            liquidView.Layer.AddSublayer(circleLayer);
            circleLayer.AddSublayer(plusLayer);
        }

        private void DidTapped()
        {
            if (IsClosed)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        protected internal virtual void OnCellSelected(LiquidFloatingCell target)
        {
            var handler = CellSelected;
            if (handler != null)
            {
                var cells = CellArray();
                var index = Array.IndexOf(cells, target);
                handler(this, new CellSelectedEventArgs(target, index));
            }
        }
    }
}
