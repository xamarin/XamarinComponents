using System;

using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Util;

namespace RadialProgress
{
	public class RadialProgressView : View
	{
		const float borderWidthPercentage = 0.018691589f;
		const float lineWidthPercentage = 0.056074766f;
		const float progressWidthPercentage = 0.228971963f;
		const float textFontSizePercentage = 0.66f;
		const float textHeightPercentage = 0.744680851f;
		const float outerLineRadiusPercentage = 1 - borderWidthPercentage - lineWidthPercentage / 2;
		const float progressBackgroundRadiusPercentage = 1 - borderWidthPercentage - lineWidthPercentage - progressWidthPercentage / 2;
		const float innerLineRadiusPercentage = 1 - borderWidthPercentage - lineWidthPercentage / 2 - lineWidthPercentage - progressWidthPercentage;
		const float progressRadiusPercentage = 1 - borderWidthPercentage - lineWidthPercentage;

		public static readonly Color DefaultColor = Color.Rgb (51, 181, 229);
		static readonly Color CircleBackgroundColor = Color.Argb (128, 0, 0, 0);
		static readonly Color ProgressBackgroundColor = Color.Argb (51, 255, 255, 255);
		static readonly Color BorderColor = Color.Argb (105, 0, 0, 0);

		RadialProgressViewStyle progressType;

		float minValue, maxValue, currentValue;
		string valueText = "";

		Paint textPaint;
		Paint bgCirclePaint, bgBorderPaint, bgProgressPaint;
		Paint progressPaint;
		Path progressPath;
		// RectFs and matrix are cashed for saving GREFs
		RectF cachedRectF;
		Matrix cachedTransformMatrix;
		Color progressColor;

		float textX, textY;
		float radius;
		float bgCx, bgCy;
		float outerLineRadius, progressBackgroundRadius, innerLineRadius;

		/// <summary>
		/// Gets or sets the minimum value
		/// </summary>
		/// <value>
		/// The minimum value.
		/// </value>
		public float MinValue {
			get {
				return minValue;
			}
			set {
				minValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		/// <value>
		/// The max value.
		/// </value>
		public float MaxValue {
			get {
				return maxValue;
			}
			set {
				maxValue = value;
			}
		}

		/// <summary>
		/// Gets or sets progress value.
		/// </summary>
		/// <value>
		/// Progress value.
		/// </value>
		public float Value { 
			get { 
				return currentValue;
			}
			set {
				if (value > MaxValue) {
					value = MaxValue;
				}
				
				if (value < MinValue) {
					value = MinValue;
				}
				
				if (currentValue != value) {
					currentValue = value;
					
					OnCurrentValueChanged();
				}
			}
		}

        /// <summary>
        /// Gets or sets the label text delegate. If null, default function will be used (in %%)
        /// </summary>
        /// <value>The label text delegate.</value>
        public Func<float, string> LabelTextDelegate { get; set; }

		/// <summary>
		/// Gets or sets the color of the progress indicator
		/// </summary>
		/// <value>
		/// The color of the progress indicator.
		/// </value>
		public Color ProgressColor {
			get {
				return progressColor;
			}
			set {
				progressColor = value;
				progressPaint.Color = ProgressColor;
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether label is hidden.
		/// </summary>
		/// <value>
		/// <c>true</c> if label hidden; otherwise, <c>false</c>.
		/// </value>
		public bool LabelHidden { get; set; }

		public bool IsDone {
			get { return Value == MaxValue; }
		}

        public RadialProgressView (Context context, IAttributeSet attributes) : base(context, attributes)
		{
			var parser = new AttributesParser(attributes);
			Init(parser.MinValue, parser.MaxValue, parser.Value, parser.ProgressType, parser.ProgressColor);
			LabelHidden = parser.LabelHidden;
		}

        public RadialProgressView (Context context, float minValue = 0f, float maxValue = 1f, RadialProgressViewStyle progressType = RadialProgressViewStyle.Big, Func<float, string> labelTextFunc = null) : base(context)
		{
            this.LabelTextDelegate = labelTextFunc;
			Init(minValue, maxValue, 0, progressType, DefaultColor);
		}

        public RadialProgressView (Context context, float minValue, float maxValue, RadialProgressViewStyle progressType, Color progressColor, Func<float, string> labelTextFunc = null) : base(context)
		{
            this.LabelTextDelegate = labelTextFunc;
			Init(minValue, maxValue, 0, progressType, progressColor);
		}


		public void Reset ()
		{
			currentValue = MinValue;
			OnCurrentValueChanged ();
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);

			radius = GetRadius (w, h);
			CalculateBackgroundParameters (radius);
		}

		protected override void OnDraw (Canvas canvas)
		{
			// Draw background circle
			if (progressType != RadialProgressViewStyle.Tiny)
				canvas.DrawCircle (bgCx, bgCy, radius, bgCirclePaint);

			// Draw progress borders
			if (progressType != RadialProgressViewStyle.Small) {
				canvas.DrawCircle (bgCx, bgCy, outerLineRadius, bgBorderPaint);
				canvas.DrawCircle (bgCx, bgCy, innerLineRadius, bgBorderPaint);
				canvas.DrawCircle (bgCx, bgCy, progressBackgroundRadius, bgProgressPaint);
			}

			// Draw progress
			CalculateProgressPath (radius);
			canvas.DrawPath (progressPath, progressPaint);
			// Draw percentage text
			if (progressType != RadialProgressViewStyle.Tiny && !LabelHidden) {
				CalculateTextParameters (radius);
				canvas.DrawText (valueText, textX, textY, textPaint);
			}
		}

		void Init(float minValue, float maxValue, float value, RadialProgressViewStyle progressType, Color progressColor)
		{
			this.progressType = progressType;
			this.minValue = minValue;
			this.maxValue = maxValue;
			currentValue = value;
            valueText = GetTextByValue(currentValue);
			this.progressColor = progressColor;

			cachedRectF = new RectF();
			cachedTransformMatrix = new Matrix();
			progressPath = new Path ();

			InitPaints ();
		}

		void OnCurrentValueChanged ()
		{
            valueText = GetTextByValue(currentValue);

			PostInvalidate();
		}

        string GetTextByValue(float value)
        {
            if (LabelTextDelegate != null) {
                return LabelTextDelegate(value);
            } else {
                return Math.Round (CalculatePercentage (currentValue)).ToString ();
            }
        }
		              
		void InitPaints ()
		{
			textPaint = new Paint ();
			textPaint.Color = Color.White;
			textPaint.SetTypeface (Typeface.DefaultBold);
			textPaint.AntiAlias = true;

			bgCirclePaint = new Paint ();
			bgCirclePaint.Color = CircleBackgroundColor;
			bgCirclePaint.AntiAlias = true;

			bgBorderPaint = new Paint ();
			bgBorderPaint.SetStyle (Paint.Style.Stroke);
			bgBorderPaint.Color = progressType == RadialProgressViewStyle.Big ? BorderColor : CircleBackgroundColor;
			bgBorderPaint.AntiAlias = true;

			bgProgressPaint = new Paint ();
			bgProgressPaint.SetStyle (Paint.Style.Stroke);
			bgProgressPaint.Color = progressType == RadialProgressViewStyle.Big ? ProgressBackgroundColor : CircleBackgroundColor;
			bgProgressPaint.AntiAlias = true;

			progressPaint = new Paint ();
			progressPaint.SetStyle (Paint.Style.Fill);
			progressPaint.Color = ProgressColor;
			progressPaint.AntiAlias = true;
		}

		void CalculateTextParameters (float radius)
		{
			textPaint.TextSize = radius * textFontSizePercentage;

			textX = radius - textPaint.MeasureText (valueText) / 2f;
			textY = radius + textPaint.TextSize * textHeightPercentage / 2f;
		}

		void CalculateBackgroundParameters (float radius)
		{
			bgCx = radius;
			bgCy = radius;

			bgBorderPaint.StrokeWidth = radius * lineWidthPercentage;
			bgProgressPaint.StrokeWidth = radius * progressWidthPercentage;

			outerLineRadius = radius * outerLineRadiusPercentage;
			innerLineRadius = radius * innerLineRadiusPercentage;
			progressBackgroundRadius = radius * progressBackgroundRadiusPercentage;
		}

		void CalculateProgressPath (float radius)
		{
			float progressWidth = radius * progressWidthPercentage;
			float angle = 3.6f * CalculatePercentage(currentValue);
			if (angle >= 360)
				angle = 359.9999f;
			float realAngle = angle - 90;

			var progressRadius = radius * progressRadiusPercentage;
			var centerShift = radius - progressRadius;

			float middleProgressRadius = progressRadius - progressWidth / 2;
			float endPointY = (float)(progressRadius + middleProgressRadius * Math.Sin (realAngle * 2 * Math.PI / 360));
			float endPointX = (float)(progressRadius + middleProgressRadius * Math.Cos (realAngle * 2 * Math.PI / 360));

			progressPath.Reset ();

			// outer bound
			cachedRectF.Set(0, 0, progressRadius * 2, progressRadius * 2);
			progressPath.ArcTo (cachedRectF, -90, angle);
			// end rounding
			cachedRectF.Set(endPointX - progressWidth / 2, endPointY - progressWidth / 2, endPointX + progressWidth / 2, endPointY + progressWidth / 2);
			progressPath.ArcTo (cachedRectF, realAngle, 180);
			// inner bound
			cachedRectF.Set(progressWidth, progressWidth, 2 * progressRadius - progressWidth, 2 * progressRadius - progressWidth);
			progressPath.ArcTo (cachedRectF, realAngle, -angle);
			// start rounding
			cachedRectF.Set(progressRadius - progressWidth / 2, 0, progressRadius + progressWidth / 2, progressWidth);
			progressPath.ArcTo (cachedRectF, 90, -180);

			// Move progress to the center of view
			cachedTransformMatrix.SetTranslate (centerShift, centerShift);
			progressPath.Transform (cachedTransformMatrix);
		}

		float GetRadius (int width, int height)
		{
			return Math.Min (width / 2f, height / 2f);
		}

		float CalculatePercentage (float currentValue)
		{
			var fullDistance = maxValue - minValue;
			var currentDistance = maxValue - currentValue;
			
			if (fullDistance == 0f) return 0f;
			
			return (1 - currentDistance / fullDistance) * 100f;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			textPaint.Dispose();
			bgCirclePaint.Dispose();
			bgBorderPaint.Dispose();
			bgProgressPaint.Dispose();
			progressPaint.Dispose();
			progressPath.Dispose();

			cachedRectF.Dispose();
			cachedTransformMatrix.Dispose();

			textPaint = bgCirclePaint = bgBorderPaint = bgProgressPaint = progressPaint = null;
			progressPath = null;
			cachedRectF = null;
			cachedTransformMatrix = null;
		}
	}
}

