using System;
using Android.Runtime;
using Java.Interop;

namespace Xamarin.GoogleFirebase.ML.Kit.Vision.Label.AutoML
{

    // Metadata.xml XPath class reference: path="/api/package[@name='com.google.mlkit.vision.label.automl']/class[@name='AutoMLImageLabelerOptions']"
    //[global::Android.Runtime.Register("com/google/mlkit/vision/label/automl/AutoMLImageLabelerOptions", DoNotGenerateAcw = true)]
    public partial class AutoMLImageLabelerOptions //: global::Xamarin.GoogleFirebase.ML.Kit.Vision.Label.ImageLabelerOptionsBase
    {
		static Delegate cb_build;
#pragma warning disable 0169
		static Delegate GetBuild2Handler()
		{
			if (cb_build == null)
				cb_build = JNINativeWrapper.CreateDelegate((_JniMarshal_PP_L)n_Build);
			return cb_build;
		}

		static IntPtr n_Build(IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Xamarin.GoogleFirebase.ML.Kit.Vision.Label.AutoML.AutoMLImageLabelerOptions.Builder>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle(__this.Build()); // cannot set Build2()
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.mlkit.vision.label.automl']/class[@name='AutoMLImageLabelerOptions.Builder']/method[@name='build' and count(parameter)=0]"
		[Register("build2", "()Lcom/google/mlkit/vision/label/automl/AutoMLImageLabelerOptions;", "GetBuild2Handler")]
		public virtual unsafe global::Xamarin.GoogleFirebase.ML.Kit.Vision.Label.AutoML.AutoMLImageLabelerOptions Build2()
		{
			const string __id = "build.()Lcom/google/mlkit/vision/label/automl/AutoMLImageLabelerOptions;";
			try
			{
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, null);
				return global::Java.Lang.Object.GetObject<global::Xamarin.GoogleFirebase.ML.Kit.Vision.Label.AutoML.AutoMLImageLabelerOptions>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}





		static Delegate cb_setConfidenceThreshold_F;
#pragma warning disable 0169
		static Delegate GetSetConfidenceThreshold_FHandler()
		{
			if (cb_setConfidenceThreshold_F == null)
				cb_setConfidenceThreshold_F = JNINativeWrapper.CreateDelegate((_JniMarshal_PPF_L)n_SetConfidenceThreshold_F);
			return cb_setConfidenceThreshold_F;
		}

		static IntPtr n_SetConfidenceThreshold_F(IntPtr jnienv, IntPtr native__this, float p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Xamarin.GoogleFirebase.ML.Kit.Vision.Label.AutoML.AutoMLImageLabelerOptions.Builder>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle(__this.SetConfidenceThreshold(p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.mlkit.vision.label.automl']/class[@name='AutoMLImageLabelerOptions.Builder']/method[@name='setConfidenceThreshold' and count(parameter)=1 and parameter[1][@type='float']]"
		[Register("setConfidenceThreshold", "(F)Ljava/lang/Object;", "GetSetConfidenceThreshold_FHandler")]
		public virtual unsafe global::Java.Lang.Object SetConfidenceThreshold(float p0)
		{
			const string __id = "setConfidenceThreshold.(F)Ljava/lang/Object;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
				__args[0] = new JniArgumentValue(p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Java.Lang.Object>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}

	}
}
