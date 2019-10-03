using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using Android.Support.V8.Renderscript;
using Java.Interop;

namespace RenderScriptSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ImageView _imageView;
		SeekBar _seekbar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			_imageView = FindViewById<ImageView> (Resource.Id.originalImageView);

			_seekbar = FindViewById<SeekBar> (Resource.Id.seekBar1);
			_seekbar.ProgressChanged += BlurImageHandler;			
		}


		private void BlurImageHandler (object sender, SeekBar.ProgressChangedEventArgs e)
		{
			int radius = e.SeekBar.Progress;
			if (radius == 0) {
				// We don't want to blur, so just load the un-altered image.
				_imageView.SetImageResource (Resource.Drawable.dog_and_monkeys);
			} else {
				BlurImage(radius);
			}

		}

		void BlurImage(int radius)
		{
			// Disable the event handler and the Seekbar to prevent this from 
			// happening multiple times.
			_seekbar.ProgressChanged -= BlurImageHandler;
			_seekbar.Enabled = false;

			_imageView.SetImageDrawable(null);

			Task.Run(() =>
			{
				// Load a clean bitmap and work from that.
				var sentBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.dog_and_monkeys);

				var bitmap = sentBitmap.Copy(sentBitmap.GetConfig(), true);
				var rs = RenderScript.Create(this);
				var input = Allocation.CreateFromBitmap(rs, sentBitmap, Allocation.MipmapControl.MipmapNone, Allocation.UsageScript);
				var output = Allocation.CreateTyped(rs, input.Type);
				var script = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));
				script.SetRadius(radius);
				script.SetInput(input);
				script.ForEach(output);
				output.CopyTo(bitmap);
				// clean up renderscript resources
				rs.Destroy();
				input.Destroy();
				output.Destroy();
				script.Destroy();

				RunOnUiThread(() =>
				{
				    _imageView.SetImageBitmap(bitmap);
				    _seekbar.ProgressChanged += BlurImageHandler;
				    _seekbar.Enabled = true;
				});
			});
		}
	}
}
