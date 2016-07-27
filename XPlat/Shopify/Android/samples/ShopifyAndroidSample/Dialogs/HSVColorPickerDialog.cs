using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ShopifyAndroidSample.Dialogs
{
	public class HSVColorPickerDialog : AlertDialog
	{
		private const int PADDING_DP = 20;

		private const int CONTROL_SPACING_DP = 20;
		private const int SELECTED_COLOR_HEIGHT_DP = 50;
		private const int BORDER_DP = 1;
		private static Color BORDER_COLOR = Color.Black;

		private Action<Color> listener;
		private Color selectedColor;

		private HSVColorWheel colorWheel;
		private HSVValueSlider valueSlider;

		private View selectedColorView;

		public HSVColorPickerDialog(Context context, Color initialColor, Action<Color> listener)
			: base(context)
		{
			this.selectedColor = initialColor;
			this.listener = listener;

			colorWheel = new HSVColorWheel(context);
			valueSlider = new HSVValueSlider(context);
			var padding = (int)(context.Resources.DisplayMetrics.Density * PADDING_DP);
			var borderSize = (int)(context.Resources.DisplayMetrics.Density * BORDER_DP);
			var layout = new RelativeLayout(context);

			var lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
			lp.BottomMargin = (int)(context.Resources.DisplayMetrics.Density * CONTROL_SPACING_DP);
			colorWheel.setListener((color) => valueSlider.SetColor(color, true));
			colorWheel.setColor(initialColor);
			colorWheel.Id = (1);
			layout.AddView(colorWheel, lp);

			int selectedColorHeight = (int)(context.Resources.DisplayMetrics.Density * SELECTED_COLOR_HEIGHT_DP);

			var valueSliderBorder = new FrameLayout(context);
			valueSliderBorder.SetBackgroundColor(BORDER_COLOR);
			valueSliderBorder.SetPadding(borderSize, borderSize, borderSize, borderSize);
			valueSliderBorder.Id = (2);
			lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, selectedColorHeight + 2 * borderSize);
			lp.BottomMargin = (int)(context.Resources.DisplayMetrics.Density * CONTROL_SPACING_DP);
			lp.AddRule(LayoutRules.Below, 1);
			layout.AddView(valueSliderBorder, lp);

			valueSlider.SetColor(initialColor, false);
			valueSlider.SetListener((color) =>
			{
				selectedColor = color;
				selectedColorView.SetBackgroundColor(color);
			});
			valueSliderBorder.AddView(valueSlider);

			var selectedColorborder = new FrameLayout(context);
			selectedColorborder.SetBackgroundColor(BORDER_COLOR);
			lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, selectedColorHeight + 2 * borderSize);
			selectedColorborder.SetPadding(borderSize, borderSize, borderSize, borderSize);
			lp.AddRule(LayoutRules.Below, 2);
			layout.AddView(selectedColorborder, lp);

			selectedColorView = new View(context);
			selectedColorView.SetBackgroundColor(selectedColor);
			selectedColorborder.AddView(selectedColorView);

			SetButton((int)DialogButtonType.Negative, context.GetString(Android.Resource.String.Cancel), ClickListener);
			SetButton((int)DialogButtonType.Positive, context.GetString(Android.Resource.String.Ok), ClickListener);

			SetView(layout, padding, padding, padding, padding);
		}

		private void ClickListener(object sender, DialogClickEventArgs e)
		{
			switch ((DialogButtonType)e.Which)
			{
				case DialogButtonType.Negative:
					((AlertDialog)sender).Dismiss();
					break;
				case DialogButtonType.Neutral:
					((AlertDialog)sender).Dismiss();
					listener(default(Color));
					break;
				case DialogButtonType.Positive:
					listener(selectedColor);
					break;
			}
		}

		/**
	     * Adds a button to the dialog that allows a user to select "No color",
	     * which will call the listener's {@link OnColorSelectedListener#colorSelected(Integer) colorSelected(Integer)} callback
	     * with null as its parameter
	     * @param res A string resource with the text to be used on this button
	     */
		public void SetNoColorButton(int res)
		{
			SetButton((int)DialogButtonType.Neutral, Context.GetString(res), ClickListener);
		}

		private class HSVColorWheel : View
		{
			private const float SCALE = 2f;
			private const float FADE_OUT_FRACTION = 0.03f;

			private const int POINTER_LINE_WIDTH_DP = 2;
			private const int POINTER_LENGTH_DP = 10;

			private Context context;

			private Action<Color> listener;

			private int scale;
			private int pointerLength;
			private int innerPadding;
			private Paint pointerPaint = new Paint();
			private float[] colorHsv = { 0f, 0f, 1f };

			private Rect rect;
			private Bitmap bitmap;

			private int[] pixels;
			private float innerCircleRadius;
			private float fullCircleRadius;

			private int scaledWidth;
			private int scaledHeight;
			private int[] scaledPixels;

			private float scaledInnerCircleRadius;
			private float scaledFullCircleRadius;
			private float scaledFadeOutSize;

			private Point selectedPoint = new Point();

			public HSVColorWheel(Context context, IAttributeSet attrs, int defStyle)
				: base(context, attrs, defStyle)
			{
				this.context = context;
				init();
			}

			public HSVColorWheel(Context context, IAttributeSet attrs)
				: base(context, attrs)
			{
				this.context = context;
				init();
			}

			public HSVColorWheel(Context context)
				: base(context)
			{
				this.context = context;
				init();
			}

			private void init()
			{
				float density = context.Resources.DisplayMetrics.Density;
				scale = (int)(density * SCALE);
				pointerLength = (int)(density * POINTER_LENGTH_DP);
				pointerPaint.StrokeWidth = (int)(density * POINTER_LINE_WIDTH_DP);
				innerPadding = pointerLength / 2;
			}

			public void setListener(Action<Color> listener)
			{
				this.listener = listener;
			}

			public void setColor(Color color)
			{
				Color.ColorToHSV(color, colorHsv);
				Invalidate();
			}

			protected override void OnDraw(Canvas canvas)
			{
				if (bitmap != null)
				{
					canvas.DrawBitmap(bitmap, null, rect, null);
					float hueInPiInterval = colorHsv[0] / 180f * (float)Math.PI;

					selectedPoint.X = rect.Left + (int)(-Math.Cos(hueInPiInterval) * colorHsv[1] * innerCircleRadius + fullCircleRadius);
					selectedPoint.Y = rect.Top + (int)(-Math.Sin(hueInPiInterval) * colorHsv[1] * innerCircleRadius + fullCircleRadius);

					canvas.DrawLine(selectedPoint.X - pointerLength, selectedPoint.Y, selectedPoint.X + pointerLength, selectedPoint.Y, pointerPaint);
					canvas.DrawLine(selectedPoint.X, selectedPoint.Y - pointerLength, selectedPoint.X, selectedPoint.Y + pointerLength, pointerPaint);
				}
			}

			protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
			{
				base.OnSizeChanged(w, h, oldw, oldh);

				rect = new Rect(innerPadding, innerPadding, w - innerPadding, h - innerPadding);
				bitmap = Bitmap.CreateBitmap(w - 2 * innerPadding, h - 2 * innerPadding, Bitmap.Config.Argb8888);

				fullCircleRadius = Math.Min(rect.Width(), rect.Height()) / 2;
				innerCircleRadius = fullCircleRadius * (1 - FADE_OUT_FRACTION);

				scaledWidth = rect.Width() / scale;
				scaledHeight = rect.Height() / scale;
				scaledFullCircleRadius = Math.Min(scaledWidth, scaledHeight) / 2;
				scaledInnerCircleRadius = scaledFullCircleRadius * (1 - FADE_OUT_FRACTION);
				scaledFadeOutSize = scaledFullCircleRadius - scaledInnerCircleRadius;
				scaledPixels = new int[scaledWidth * scaledHeight];
				pixels = new int[rect.Width() * rect.Height()];

				CreateBitmap();
			}

			private void CreateBitmap()
			{
				int w = rect.Width();
				int h = rect.Height();

				float[] hsv = new float[] { 0f, 0f, 1f };
				int alpha = 255;

				int x = (int)-scaledFullCircleRadius, y = (int)-scaledFullCircleRadius;
				for (int i = 0; i < scaledPixels.Length; i++)
				{
					if (i % scaledWidth == 0)
					{
						x = (int)-scaledFullCircleRadius;
						y++;
					}
					else {
						x++;
					}

					double centerDist = Math.Sqrt(x * x + y * y);
					if (centerDist <= scaledFullCircleRadius)
					{
						hsv[0] = (float)(Math.Atan2(y, x) / Math.PI * 180f) + 180;
						hsv[1] = (float)(centerDist / scaledInnerCircleRadius);
						if (centerDist <= scaledInnerCircleRadius)
						{
							alpha = 255;
						}
						else {
							alpha = 255 - (int)((centerDist - scaledInnerCircleRadius) / scaledFadeOutSize * 255);
						}
						scaledPixels[i] = Color.HSVToColor(alpha, hsv);
					}
					else {
						scaledPixels[i] = 0x00000000;
					}
				}

				int scaledX, scaledY;
				for (x = 0; x < w; x++)
				{
					scaledX = x / scale;
					if (scaledX >= scaledWidth) scaledX = scaledWidth - 1;
					for (y = 0; y < h; y++)
					{
						scaledY = y / scale;
						if (scaledY >= scaledHeight) scaledY = scaledHeight - 1;
						pixels[x * h + y] = scaledPixels[scaledX * scaledHeight + scaledY];
					}
				}

				bitmap.SetPixels(pixels, 0, w, 0, 0, w, h);

				Invalidate();
			}

			protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
			{
				int maxWidth = View.MeasureSpec.GetSize(widthMeasureSpec);
				int maxHeight = View.MeasureSpec.GetSize(heightMeasureSpec);

				int width, height;
				/*
				 * Make the view quadratic, with height and width equal and as large as possible
				 */
				width = height = Math.Min(maxWidth, maxHeight);

				SetMeasuredDimension(width, height);
			}

			public Color GetColorForPoint(float x, float y, float[] hsv)
			{
				x -= fullCircleRadius;
				y -= fullCircleRadius;
				double centerDist = Math.Sqrt(x * x + y * y);
				hsv[0] = (float)(Math.Atan2(y, x) / Math.PI * 180f) + 180;
				hsv[1] = Math.Max(0f, Math.Min(1f, (float)(centerDist / innerCircleRadius)));
				return Color.HSVToColor(hsv);
			}

			public override bool OnTouchEvent(MotionEvent evt)
			{
				var action = evt.ActionMasked;
				switch (action)
				{
					case MotionEventActions.Down:
					case MotionEventActions.Move:
						if (listener != null)
						{
							listener(GetColorForPoint(evt.GetX(), evt.GetY(), colorHsv));
						}
						Invalidate();
						return true;
				}
				return base.OnTouchEvent(evt);
			}
		}

		private class HSVValueSlider : View
		{
			private Action<Color> listener;
			float[] colorHsv = { 0f, 0f, 1f };
			private Rect srcRect;
			private Rect dstRect;
			private Bitmap bitmap;
			private int[] pixels;

			public HSVValueSlider(Context context, IAttributeSet attrs, int defStyle)
				: base(context, attrs, defStyle)
			{
			}

			public HSVValueSlider(Context context, IAttributeSet attrs)
				: base(context, attrs)
			{
			}

			public HSVValueSlider(Context context)
				: base(context)
			{
			}

			public void SetListener(Action<Color> listener)
			{
				this.listener = listener;
			}

			public void SetColor(Color color, bool keepValue)
			{
				float oldValue = colorHsv[2];
				Color.ColorToHSV(color, colorHsv);
				if (keepValue)
				{
					colorHsv[2] = oldValue;
				}
				if (listener != null)
				{
					listener(Color.HSVToColor(colorHsv));
				}

				CreateBitmap();
			}

			protected override void OnDraw(Canvas canvas)
			{
				if (bitmap != null)
				{
					canvas.DrawBitmap(bitmap, srcRect, dstRect, null);
				}
			}

			protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
			{
				base.OnSizeChanged(w, h, oldw, oldh);

				srcRect = new Rect(0, 0, w, 1);
				dstRect = new Rect(0, 0, w, h);
				bitmap = Bitmap.CreateBitmap(w, 1, Bitmap.Config.Argb8888);
				pixels = new int[w];

				CreateBitmap();
			}

			private void CreateBitmap()
			{
				if (bitmap == null)
				{
					return;
				}
				int w = Width;

				float[] hsv = new float[] { colorHsv[0], colorHsv[1], 1f };

				int selectedX = (int)(colorHsv[2] * w);

				float value = 0;
				float valueStep = 1f / w;
				for (int x = 0; x < w; x++)
				{
					value += valueStep;
					if (x >= selectedX - 1 && x <= selectedX + 1)
					{
						int intVal = 0xFF - (int)(value * 0xFF);
						long color = intVal * 0x010101 + 0xFF000000;
						pixels[x] = (int)color;
					}
					else {
						hsv[2] = value;
						pixels[x] = Color.HSVToColor(hsv);
					}
				}

				bitmap.SetPixels(pixels, 0, w, 0, 0, w, 1);

				Invalidate();
			}

			public override bool OnTouchEvent(MotionEvent evt)
			{
				var action = evt.ActionMasked;
				switch (action)
				{
					case MotionEventActions.Down:
					case MotionEventActions.Move:
						int x = Math.Max(0, Math.Min(bitmap.Width - 1, (int)evt.GetX()));
						float value = x / (float)bitmap.Width;
						if (colorHsv[2] != value)
						{
							colorHsv[2] = value;
							if (listener != null)
							{
								listener(Color.HSVToColor(colorHsv));
							}
							CreateBitmap();
							Invalidate();
						}
						return true;
				}
				return base.OnTouchEvent(evt);
			}
		}
	}
}
