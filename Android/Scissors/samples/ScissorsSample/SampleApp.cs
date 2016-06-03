using System;
using Android.App;
using Android.OS;
using Android.Runtime;

[assembly: UsesPermission (Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission (Android.Manifest.Permission.WriteExternalStorage)]

namespace ScissorsSample
{
	[Application (Theme = "@style/Theme.Scissors")]
	public class SampleApp : Application
	{
		public const int PickImageFromGallery = 10001;

		protected SampleApp (IntPtr javaReference, JniHandleOwnership transfer)
			: base (javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// make sure we aren't loading anything wrong
			StrictMode.SetThreadPolicy (new StrictMode.ThreadPolicy.Builder ()
				.DetectDiskReads ()
				.DetectDiskWrites ()
				.DetectNetwork ()   // or .DetectAll() for all detectable problems
				.PenaltyLog ()
				.Build ());
			StrictMode.SetVmPolicy (new StrictMode.VmPolicy.Builder ()
				.DetectLeakedSqlLiteObjects ()
				.DetectLeakedClosableObjects ()
				.PenaltyLog ()
				.PenaltyDeath ()
				.Build ());
		}
	}
}
