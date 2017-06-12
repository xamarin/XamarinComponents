using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace DownloaderSample
{
	[Activity(Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
	public class ZipTestActivity : AppCompatActivity
	{
		private VideoView video;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.VideoPlayer);

			FindViewById<Button>(Resource.Id.MainPlay).Click += delegate { ButtonClick("MetalGearSolidV.mp4"); };
			FindViewById<Button>(Resource.Id.PatchPlay).Click += delegate { ButtonClick("Titanfall.mp4"); };

			video = FindViewById<VideoView>(Resource.Id.MyVideo);
		}

		private void ButtonClick(string file)
		{
			var uri = Android.Net.Uri.Parse($"content://{ZipFileContentProvider.ContentProviderAuthority}/{file}");
			video.SetVideoURI(uri);

			// add the start/pause controls
			var controller = new MediaController(this);
			controller.SetMediaPlayer(video);
			video.SetMediaController(controller);
			video.Prepared += delegate
			{
				// play the video
				video.Start();
			};

			video.RequestFocus();
		}
	}
}
