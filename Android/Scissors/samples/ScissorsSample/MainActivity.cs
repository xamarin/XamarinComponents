using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Genetics;
using Genetics.Attributes;
using CompressFormat = Android.Graphics.Bitmap.CompressFormat;

using Lyft.Scissors;

namespace ScissorsSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		[Splice (Resource.Id.crop_view)]
		private CropView cropView;

		[Splice (Resource.Id.crop_fab)]
		private FloatingActionButton cropButton;

		[Splice (Resource.Id.pick_mini_fab)]
		private FloatingActionButton pickMiniButton;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_main);
			Geneticist.Splice (this);

			// load the image using the traditional method
			cropView.SetImageResource (Resource.Drawable.AfricanLion);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Android.Content.Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == SampleApp.PickImageFromGallery && resultCode == Result.Ok) {
				Android.Net.Uri galleryPictureUri = data.Data;

				// load the image using an available loader (in this sample, Picasso)
				cropView.WithExtensions.Load (galleryPictureUri);
			}
		}

		[SpliceClick (Resource.Id.crop_fab)]
		private async void OnCropClicked (object sender, EventArgs e)
		{
			var croppedFile = Path.Combine (CacheDir.AbsolutePath, "cropped.jpg");

			await cropView.WithExtensions
				.Crop ()
				.Quality (100)
				.Format (CompressFormat.Jpeg)
				.IntoAsync (croppedFile);
			
			CropResultActivity.StartUsing (croppedFile, this);
		}

		[SpliceClick (Resource.Id.pick_mini_fab)]
		private void OnPickClicked (object sender, EventArgs e)
		{
			cropView.WithExtensions.PickUsing (this, SampleApp.PickImageFromGallery);
		}

		[SpliceTouch (Resource.Id.crop_view)]
		private void OnTouchCropView (object sender, View.TouchEventArgs e)
		{ 
			e.Handled = true;

			if (e.Event.PointerCount <= 1 && cropView.ImageBitmap != null) {
				switch (e.Event.ActionMasked) {
				case MotionEventActions.Down:
				case MotionEventActions.Move:
					cropButton.Visibility = ViewStates.Invisible;
					pickMiniButton.Visibility = ViewStates.Invisible;
					break;
				default:
					cropButton.Visibility = ViewStates.Visible;
					pickMiniButton.Visibility = ViewStates.Visible;
					break;
				}
			}
		}
	}
}
