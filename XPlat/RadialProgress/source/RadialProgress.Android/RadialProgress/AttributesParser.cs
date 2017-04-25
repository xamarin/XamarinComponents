using System;

using Android.Graphics;
using Android.Util;

namespace RadialProgress
{
	internal class AttributesParser
	{
		public RadialProgressViewStyle ProgressType;
		public float MinValue;
		public float MaxValue;
		public float Value;
		public bool LabelHidden;
		public Color ProgressColor = RadialProgressView.DefaultColor;

		public AttributesParser (IAttributeSet attributes)
		{
			MinValue = ParseFloat(attributes, "min_value", 0);
			MaxValue = ParseFloat(attributes, "max_value", 1);
			Value = ParseFloat(attributes, "value", MinValue);
			LabelHidden = attributes.GetAttributeBooleanValue (null, "hide_label", false);
			var typeAttribute = attributes.GetAttributeValue (null, "progress_type");
			var progressColorAttribute = attributes.GetAttributeValue (null, "progress_color");
			
			if (typeAttribute == null || typeAttribute == "big")
				ProgressType = RadialProgressViewStyle.Big;
			else if (typeAttribute == "small")
				ProgressType = RadialProgressViewStyle.Small;
			else if (typeAttribute == "tiny")
				ProgressType = RadialProgressViewStyle.Tiny;
			else
				throw new ArgumentException ("Unrecognized RadialProgress type '" + typeAttribute + "'. Must be either 'big', 'small' or 'tiny'.", "progress_type");
			
			if (!string.IsNullOrEmpty(progressColorAttribute)) {
				try
				{
					ProgressColor = Color.ParseColor(progressColorAttribute);
				}
				catch (AndroidException)
				{
					throw new ArgumentException ("Wrong color string '" + progressColorAttribute + "'. Must be either '#RRGGBB' or '#AARRGGBB'.", "progress_color");
				}
			}
		}

		float ParseFloat(IAttributeSet attributes, string attributeName, float defaultNumber)
		{
			var numberAttr = attributes.GetAttributeValue (null, attributeName);

			if (string.IsNullOrEmpty(numberAttr))
				return defaultNumber;

			float result;
			if (float.TryParse(numberAttr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result))
				return result;
			else
				throw new ArgumentException("Wrong " + attributeName + " value '" + numberAttr + "'. Must be a number.");
		}
	}
}

