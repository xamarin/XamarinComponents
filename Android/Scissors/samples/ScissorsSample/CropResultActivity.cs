using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Java.IO;
using Genetics;
using Genetics.Attributes;
using Square.Picasso;

namespace ScissorsSample
{
	[Activity (Label = "Crop Result", NoHistory = true)]
	public class CropResultActivity : AppCompatActivity
	{
		private static String ExtraFilePath = "EXTRA_FILE_PATH";

		[Splice (Resource.Id.result_image)]
		private ImageView resultView;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_crop_result);
			Geneticist.Splice (this);

			var filePath = Intent.GetStringExtra (ExtraFilePath);

			Picasso.With (this)
				.Load (new File (filePath))
				.MemoryPolicy (MemoryPolicy.NoCache, MemoryPolicy.NoStore)
				.Into (resultView);
		}

		public static void StartUsing (string croppedPath, Activity activity)
		{
			var intent = new Intent (activity, typeof(CropResultActivity));
			intent.PutExtra (ExtraFilePath, croppedPath);
			activity.StartActivity (intent);
		}
	}
}
