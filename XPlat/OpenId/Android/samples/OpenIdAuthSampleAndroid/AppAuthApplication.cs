using System;
using Android.App;
using Android.Runtime;
using Android.Support.V7.App;

namespace OpenIdAuthSampleAndroid
{
	[Application]
	public class AppAuthApplication : Application
	{
		protected AppAuthApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
		}
	}
}
