using Android.Graphics.Drawables;
using Android.Graphics;
using Java.Interop;

namespace NineOldAndroidsSample.ApiDemos
{
    /// <summary>
    /// A data structure that holds a Shape and various properties that can be used to define
    /// how the shape is drawn.
    /// </summary>
    public class ShapeHolder : Java.Lang.Object
    {
        public ShapeHolder(ShapeDrawable s)
        {
            Shape = s;
        }

        public Paint Paint { get; set; }

        public float X {[Export("getX")] get;[Export("setX")] set; }

        public float Y {[Export("getY")] get;[Export("setY")] set; }

        public ShapeDrawable Shape { get; set; }

        public Color Color
        {
            get { return Shape.Paint.Color; }
            set { Shape.Paint.Color = value; }
        }

        public int ColorInt
        {
            [Export("getColor")] get { return Color; }
            [Export("setColor")] set { Color = new Color(value); }
        }

        public RadialGradient Gradient { get; set; }

        public float Alpha
        {
            [Export("getAlpha")] get { return Shape.Alpha / 255f; }
            [Export("setAlpha")] set { Shape.Alpha = (int)(value * 255); }
        }

        public float Width
        {
            [Export("getWidth")] get { return Shape.Shape.Width; }
            [Export("setWidth")]
            set
            {
                var s = Shape.Shape;
                s.Resize(value, s.Height);
            }
        }

        public float Height
        {
            [Export("getHeight")] get { return Shape.Shape.Height; }
            [Export("setHeight")]
            set
            {
                var s = Shape.Shape;
                s.Resize(s.Width, value);
            }
        }
    }
}