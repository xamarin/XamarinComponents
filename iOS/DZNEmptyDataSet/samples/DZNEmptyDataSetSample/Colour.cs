using System;
using CoreGraphics;
using UIKit;
using Newtonsoft.Json.Linq;

namespace DZNEmptyDataSetSample
{
	public class Colour
	{
		public Colour (JToken name, JToken rgb, JToken hex)
		{
			Name = name.Value<string> ();

			Hex = "#" + hex.Value<string> ();

			var color = rgb.Value<string> ();
			color = color.Substring (1, color.Length - 2);
			var parts = color.Split (',');
			Color = UIColor.FromRGB (
				int.Parse (parts [0].Trim ()), 
				int.Parse (parts [1].Trim ()), 
				int.Parse (parts [2].Trim ()));
		}

		public string Name { get; set; }

		public string Hex { get; set; }

		public UIColor Color { get; set; }

		public UIImage Image {
			get {
				// Constants
				var bounds = new CGRect(0, 0, 32, 32);

				// Create the image context
				UIGraphics.BeginImageContextWithOptions(bounds.Size, false, 0);

				// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(bounds);
				Color.SetFill ();
				ovalPath.Fill ();

				//Create the image using the current context.
				var image = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();

				return image;
			}
		}
	}
}
