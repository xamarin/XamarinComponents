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
    internal class CircleLiquidBaseView : UIView
    {
        private bool opening = false;
        private nfloat openDuration = 0.6f;
        private nfloat closeDuration = 0.2f;
        private nfloat viscosity = 0.65f;
        public AnimateStyle AnimateStyle = AnimateStyle.Up;
        private UIColor _color = UIColor.FromRGB(82, 112, 235);

        private LiquittableCircle baseLiquid;
        private SimpleCircleLiquidEngine engine;
        private SimpleCircleLiquidEngine bigEngine;
        private bool enableShadow = true;

        private List<LiquidFloatingCell> openingCells = new List<LiquidFloatingCell>();
        private nfloat keyDuration = 0;
        private CADisplayLink displayLink;

        public bool IsOpeningOrClosing
        {
            get { return openingCells.Count > 0; }
        }

        public UIColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (engine != null) engine.Color = _color;
                if (bigEngine != null) bigEngine.Color = _color;
            }
        }

        public void Setup(LiquidFloatingActionButton actionButton)
        {
            Frame = actionButton.Frame;
            Center = actionButton.Center.Minus(actionButton.Frame.Location);
            AnimateStyle = actionButton.AnimateStyle;
            ClipsToBounds = false;
            Layer.MasksToBounds = false;

            engine = new SimpleCircleLiquidEngine();
            engine.Viscosity = viscosity;
            engine.Color = actionButton.Color;
            bigEngine = new SimpleCircleLiquidEngine();
            bigEngine.Viscosity = viscosity;
            bigEngine.Color = actionButton.Color;

            baseLiquid = new LiquittableCircle();
            baseLiquid.Color = actionButton.Color;
            baseLiquid.ClipsToBounds = false;
            baseLiquid.Layer.MasksToBounds = false;
            AddSubview(baseLiquid);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var radius = (nfloat)Math.Min(Frame.Width, Frame.Height) * 0.5f;
            engine.RadiusThreshold = radius * 0.73f;
            engine.AngleThreshold = 0.45f;
            bigEngine.RadiusThreshold = radius;
            bigEngine.AngleThreshold = 0.55f;

            baseLiquid.Frame = new CGRect(CGPoint.Empty, Frame.Size);
        }

        public void Open(LiquidFloatingCell[] cells)
        {
            Stop();
            displayLink = CADisplayLink.Create(DidDisplayRefresh);
            displayLink.AddToRunLoop(NSRunLoop.Current, NSRunLoopMode.Common);
            opening = true;
            foreach (var cell in cells)
            {
                cell.Layer.RemoveAllAnimations();
                cell.Layer.EraseShadow();
                openingCells.Add(cell);
            }
        }

        public void Close(LiquidFloatingCell[] cells)
        {
            Stop();
            opening = false;
            displayLink = CADisplayLink.Create(DidDisplayRefresh);
            displayLink.AddToRunLoop(NSRunLoop.Current, NSRunLoopMode.Common);
            foreach (var cell in cells)
            {
                cell.Layer.RemoveAllAnimations();
                cell.Layer.EraseShadow();
                openingCells.Add(cell);
                cell.UserInteractionEnabled = false;
            }
        }

        private void DidFinishUpdate()
        {
            if (opening)
            {
                foreach (var cell in openingCells)
                {
                    cell.UserInteractionEnabled = true;
                }
            }
            else
            {
                foreach (var cell in openingCells)
                {
                    cell.RemoveFromSuperview();
                }
            }
        }

        private void Update(nfloat delay, nfloat duration, Action<LiquidFloatingCell, int, nfloat> f)
        {
            if (openingCells.Count == 0)
            {
                return;
            }

            var maxDuration = duration + openingCells.Count * delay;
            var t = keyDuration;
            var allRatio = EaseInEaseOut(t / maxDuration);

            if (allRatio >= 1.0f)
            {
                DidFinishUpdate();
                Stop();
                return;
            }

            engine.Clear();
            bigEngine.Clear();
            for (var i = 0; i < openingCells.Count; i++)
            {
                var liquidCell = openingCells[i];
                var cellDelay = delay * i;
                var ratio = EaseInEaseOut((t - cellDelay) / duration);
                f(liquidCell, i, ratio);
            }

            var firstCell = openingCells.FirstOrDefault();
            if (firstCell != null)
            {
                bigEngine.Push(baseLiquid, firstCell);
            }

            for (var i = 1; i < openingCells.Count; i++)
            {
                var prev = openingCells[i - 1];
                var cell = openingCells[i];
                engine.Push(prev, cell);
            }
            engine.Draw(baseLiquid);
            bigEngine.Draw(baseLiquid);
        }

        private void UpdateOpen()
        {
            Update(0.1f, openDuration, (cell, i, ratio) =>
            {
                var posRatio = ratio > (nfloat)i / (nfloat)openingCells.Count ? ratio : 0;
                var distance = (cell.Frame.Height * 0.5f + (i + 1f) * cell.Frame.Height * 1.5f) * posRatio;
                cell.Center = Center.Plus(DifferencePoint(distance));
                cell.Update(ratio, true);
            });
        }

        private void UpdateClose()
        {
            Update(0.0f, closeDuration, (cell, i, ratio) =>
            {
                var distance = (cell.Frame.Height * 0.5f + (i + 1f) * cell.Frame.Height * 1.5f) * (1f - ratio);
                cell.Center = Center.Plus(DifferencePoint(distance));
                cell.Update(ratio, false);
            });
        }

        private CGPoint DifferencePoint(nfloat distance)
        {
            switch (AnimateStyle)
            {
                default:
                case AnimateStyle.Up:
                    return new CGPoint(x: 0, y: -distance);
                case AnimateStyle.Right:
                    return new CGPoint(x: distance, y: 0);
                case AnimateStyle.Left:
                    return new CGPoint(x: -distance, y: 0);
                case AnimateStyle.Down:
                    return new CGPoint(x: 0, y: distance);
            }
        }

        private void Stop()
        {
            foreach (var cell in openingCells)
            {
                if (enableShadow)
                {
                    cell.Layer.AppendShadow();
                }
            }
            openingCells.Clear();
            keyDuration = 0;
            if (displayLink != null)
            {
                displayLink.Invalidate();
            }
        }

        private nfloat EaseInEaseOut(nfloat t)
        {
            if (t >= 1.0f)
            {
                return 1.0f;
            }
            if (t < 0f)
            {
                return 0f;
            }
            var t2 = t * 2f;
            return -1f * t * (t - 2f);
        }

        private void DidDisplayRefresh()
        {
            keyDuration += (nfloat)displayLink.Duration;
            if (opening)
            {
                UpdateOpen();
            }
            else
            {
                UpdateClose();
            }
        }
    }
}
