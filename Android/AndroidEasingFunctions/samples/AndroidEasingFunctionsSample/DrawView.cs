using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace AndroidEasingFunctionsSample
{
	public class DrawView : View
	{
		private Paint pathPaint;
		private Paint boxPaint;
		private Path path;

		public DrawView (Context context)
			: base (context)
		{
			Setup ();
		}

		public DrawView (Context context, IAttributeSet attrs)
			: base (context, attrs)
		{
			Setup ();
		}

		public DrawView (Context context, IAttributeSet attrs, int defStyleAttr)
			: base (context, attrs, defStyleAttr)
		{
			Setup ();
		}

		public DrawView (Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
			: base (context, attrs, defStyleAttr, defStyleRes)
		{
			Setup ();
		}

		private void Setup ()
		{
			path = new Path ();

			// Xamarin blue
			boxPaint = new Paint {
				Color = Color.ParseColor ("#3498db"),
				AntiAlias = true,
				StrokeWidth = 3
			};
			boxPaint.SetStyle (Paint.Style.Stroke);

			// Xamarin green
			pathPaint = new Paint {
				Color = Color.ParseColor ("#77d065"),
				AntiAlias = true,
				StrokeWidth = 3
			};
			pathPaint.SetStyle (Paint.Style.Stroke);
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			float l = 0;
			float t = Height - PaddingBottom - DipToPixels (Context, 217);
			float r = Width - PaddingRight;
			float b = Height - DipToPixels (Context, 60);

			canvas.DrawRect (l, t, r, b, boxPaint);
			canvas.DrawPath (path, pathPaint);
		}

		public static float DipToPixels (Context context, float dipValue)
		{
			DisplayMetrics metrics = context.Resources.DisplayMetrics;
			return TypedValue.ApplyDimension (ComplexUnitType.Dip, dipValue, metrics);
		}

		public void DrawPoint (float time, float duration, float y)
		{
			float p = time / duration;
			float x = p * Width;
			float z = Height + y;
			if (path.IsEmpty) {
				path.MoveTo (x, z);
			}
			path.LineTo (x, z);
			Invalidate ();
		}

		public void Clear ()
		{
			path.Reset ();
			Invalidate ();
		}
	}
}
