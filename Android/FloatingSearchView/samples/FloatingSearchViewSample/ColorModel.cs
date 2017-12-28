using Android.Graphics;
using Newtonsoft.Json.Linq;

namespace FloatingSearchViewSample
{
	public class ColorModel
	{
		public ColorModel(JToken name, JToken rgb, JToken hex)
		{
			Name = name.Value<string>();
			Hex = hex.Value<string>();
			RGB = rgb.Value<string>();
			Color = Color.ParseColor(Hex);
		}

		public string Name { get; set; }

		public string Hex { get; set; }

		public string RGB { get; set; }

		public Color Color { get; set; }
	}
}
