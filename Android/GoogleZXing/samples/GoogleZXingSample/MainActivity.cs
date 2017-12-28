using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using Google.ZXing;

namespace GoogleZXingSample
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.main);
			var imageView = FindViewById<ImageView>(Resource.Id.imageView);

			var code = CreateCode("https://github.com/xamarin/XamarinComponents");

			imageView.SetImageBitmap(code);
		}

		private static Bitmap CreateCode(string url)
		{
			try
			{
				// create the QR code
				var hints = new Dictionary<EncodeHintType, object>
				{
					{ EncodeHintType.Margin, 6 }
				};
				var writer = new MultiFormatWriter();
				var matrix = writer.Encode(url, BarcodeFormat.QrCode, 256, 256, hints);

				// create a bitmap from the code
				int h = matrix.Height;
				int w = matrix.Width;
				int[] pixels = new int[h * w];
				for (int i = 0; i < h; i++)
				{
					int offset = i * w;
					for (int j = 0; j < w; j++)
					{
						pixels[offset + j] = matrix.Get(j, i) ? Color.Black : Color.White;
					}
				}
				var qrCode = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
				qrCode.SetPixels(pixels, 0, w, 0, 0, w, h);

				return qrCode;
			}
			catch (WriterException)
			{
				// ignored because exception would be thrown from ZXing library.
			}

			return null;
		}
	}
}
