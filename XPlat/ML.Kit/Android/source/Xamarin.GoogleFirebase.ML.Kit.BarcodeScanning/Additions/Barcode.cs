using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.GoogleFirebase.ML.Kit.Barhopper
{
	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.libraries.barhopper']/class[@name='Barcode']"
	//[global::Android.Runtime.Register("com/google/android/libraries/barhopper/Barcode", DoNotGenerateAcw = true)]
	public partial class Barcode : global::Java.Lang.Object
	{
		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.libraries.barhopper']/class[@name='Barcode']/field[@name='cornerPoints']"
		[Register("cornerPoints")]
		public IList<global::Android.Graphics.Point> CornerPoints
		{
			get
			{
				const string __id = "cornerPoints.[Landroid/graphics/Point;";

				var __v = _members.InstanceFields.GetObjectValue(__id, this);
				return global::Android.Runtime.JavaArray<global::Android.Graphics.Point>.FromJniHandle(__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set
			{
				const string __id = "cornerPoints.[Landroid/graphics/Point;";

				IntPtr native_value = global::Android.Runtime.JavaArray<global::Android.Graphics.Point>.ToLocalJniHandle(value);
				try
				{
					_members.InstanceFields.SetValue(__id, this, new JniObjectReference(native_value));
				}
				finally
				{
					global::Android.Runtime.JNIEnv.DeleteLocalRef(native_value);
				}
			}
		}
	}
}
