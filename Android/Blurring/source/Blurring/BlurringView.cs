using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V8.Renderscript;
using Android.Util;
using Android.Views;

namespace Blurring
{
    /// <summary>
    /// A custom view for presenting a dynamically blurred version of another view's content.
    /// </summary>
    public class BlurringView : View
    {
        /// <summary>
        /// The default blur radius
        /// </summary>
        public readonly static int DefaultBlurRadius = 6;
        /// <summary>
        /// The default downsample factor
        /// </summary>
        public readonly static int DefaultDownsampleFactor = 4;
        /// <summary>
        /// The default overlay color
        /// </summary>
        public readonly static Color DefaultOverlayColor = Color.Argb(100, 0, 0, 0);
        /// <summary>
        /// The default overlay background color
        /// </summary>
        public readonly static Color DefaultOverlayBackgroundColor = Color.Transparent;

        private int downsampleFactor;
        private bool downsampleFactorChanged;
        private int blurRadius;
        private int blurredViewWidth;
        private int blurredViewHeight;
        private Bitmap bitmapToBlur;
        private Bitmap blurredBitmap;
        private Canvas blurringCanvas;

        private RenderScript renderScript;
        private ScriptIntrinsicBlur blurScript;
        private Allocation blurInput;
        private Allocation blurOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlurringView"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BlurringView(Context context)
            : base(context)
        {
            Init(context, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlurringView"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attribute set.</param>
        public BlurringView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            // set up the script
            try
            {
                renderScript = RenderScript.Create(context);
                blurScript = ScriptIntrinsicBlur.Create(renderScript, Element.U8_4(renderScript));
            }
            catch (Java.Lang.RuntimeException ex)
            {
                if (ex.Class.Name == "android.support.v8.renderscript.RSRuntimeException")
                {
                    Console.WriteLine(ex);
                }
                else
                {
                    throw;
                }
            }

            // set the default values
            BlurRadius = DefaultBlurRadius;
            DownsampleFactor = DefaultDownsampleFactor;
            OverlayColor = DefaultOverlayColor;
            OverlayBackgroundColor = DefaultOverlayBackgroundColor;
            BlurredView = null;

            if (attrs != null)
            {
                // read any XML values
                var a = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.BlurringView, 0, 0);
                try
                {
                    BlurRadius = a.GetInteger(Resource.Styleable.BlurringView_blurRadius, DefaultBlurRadius);
                    DownsampleFactor = a.GetInteger(Resource.Styleable.BlurringView_downsampleFactor, DefaultDownsampleFactor);
                    OverlayColor = a.GetColor(Resource.Styleable.BlurringView_overlayColor, DefaultOverlayColor);
                    OverlayBackgroundColor = a.GetColor(Resource.Styleable.BlurringView_overlayBackgroundColor, DefaultOverlayBackgroundColor);
                }
                finally
                {
                    a.Recycle();
                }
            }
        }

        /// <summary>
        /// Gets or sets the view to be blurred.
        /// </summary>
        /// <value>The view to be blurred.</value>
        public View BlurredView { get; set; }

        /// <summary>
        /// Gets or sets the downsample factor.
        /// </summary>
        /// <value>The downsample factor.</value>
        /// <exception cref="ArgumentException">Downsample factor must be greater than 0.</exception>
        public int DownsampleFactor
        {
            get { return downsampleFactor; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Downsample factor must be greater than 0.");
                }

                if (downsampleFactor != value)
                {
                    downsampleFactor = value;
                    downsampleFactorChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the blur radius.
        /// </summary>
        /// <value>The blur radius.</value>
        /// <exception cref="System.ArgumentException">
        /// Blur radius factor must be greater than 0.
        /// or
        /// Blur radius must be less than 25.
        /// </exception>
        public int BlurRadius
        {
            get { return blurRadius; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Blur radius factor must be greater than 0.");
                }

                if (value > 25)
                {
                    throw new ArgumentException("Blur radius must be less than 25.");
                }

                if (blurRadius != value)
                {
                    blurRadius = value;
                    if (blurScript != null)
                    {
                        blurScript.SetRadius(value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the overlay.
        /// </summary>
        /// <value>The color of the overlay.</value>
        public Color OverlayColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the overlay before blurring.
        /// </summary>
        /// <value>The color of the overlay before blurring.</value>
        public Color OverlayBackgroundColor { get; set; }

        /// <summary>
        /// Handles the drawing of the blurred view.
        /// </summary>
        /// <param name="canvas">The canvas on which to draw.</param>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (BlurredView != null)
            {
                if (Prepare())
                {
                    bitmapToBlur.EraseColor(OverlayBackgroundColor);

                    BlurredView.Draw(blurringCanvas);

                    if (renderScript != null)
                    {
                        blurInput.CopyFrom(bitmapToBlur);
                        blurScript.SetInput(blurInput);
                        blurScript.ForEach(blurOutput);
                        blurOutput.CopyTo(blurredBitmap);
                    }
                    else
                    {
                        StackBlur.Blur(bitmapToBlur, blurredBitmap, BlurRadius);
                    }

                    canvas.Save();
                    float x = 0;
                    float y = 0;
                    if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb)
                    {
                        x = BlurredView.Left - Left;
                        y = BlurredView.Top - Top;
                    }
                    else
                    {
                        x = BlurredView.GetX() - GetX();
                        y = BlurredView.GetY() - GetY();
                    }
                    canvas.Translate(x, y);
                    canvas.Scale(downsampleFactor, downsampleFactor);
                    canvas.DrawBitmap(blurredBitmap, 0, 0, null);
                    canvas.Restore();
                }

                canvas.DrawColor(OverlayColor);
            }
        }

        private bool Prepare()
        {
            int width = BlurredView.Width;
            int height = BlurredView.Height;

            if (blurringCanvas == null || downsampleFactorChanged || blurredViewWidth != width || blurredViewHeight != height)
            {
                downsampleFactorChanged = false;

                blurredViewWidth = width;
                blurredViewHeight = height;

                int scaledWidth = width / downsampleFactor;
                int scaledHeight = height / downsampleFactor;

                scaledWidth = scaledWidth - scaledWidth % 4 + 4;
                scaledHeight = scaledHeight - scaledHeight % 4 + 4;

                if (blurredBitmap == null || blurredBitmap.Width != scaledWidth || blurredBitmap.Height != scaledHeight)
                {
                    bitmapToBlur = Bitmap.CreateBitmap(scaledWidth, scaledHeight, Bitmap.Config.Argb8888);
                    if (bitmapToBlur == null)
                    {
                        return false;
                    }

                    blurredBitmap = Bitmap.CreateBitmap(scaledWidth, scaledHeight, Bitmap.Config.Argb8888);
                    if (blurredBitmap == null)
                    {
                        return false;
                    }
                }

                blurringCanvas = new Canvas(bitmapToBlur);
                blurringCanvas.Scale(1f / downsampleFactor, 1f / downsampleFactor);
                if (renderScript != null)
                {
                    blurInput = Allocation.CreateFromBitmap(renderScript, bitmapToBlur, Allocation.MipmapControl.MipmapNone, Allocation.UsageScript);
                    blurOutput = Allocation.CreateTyped(renderScript, blurInput.Type);
                }
            }
            return true;
        }

        /// <summary>
        /// This is called when the view is detached from a window.
        /// </summary>
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();

            if (renderScript != null)
            {
                renderScript.Destroy();
            }
        }
    }
}
