using System;
using Android.Runtime;

namespace Xamarin.Protobuf.Lite
{
    
    public partial class LazyStringArrayList
    {
        static Delegate cb_get_I;
#pragma warning disable 0169
        static Delegate GetGet_IHandler()
        {
            if (cb_get_I == null)
                cb_get_I = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, int, IntPtr>)n_Get_I);
            return cb_get_I;
        }

        static IntPtr n_Get_I(IntPtr jnienv, IntPtr native__this, int index)
        {
            global::Xamarin.Protobuf.Lite.LazyStringArrayList __this = global::Java.Lang.Object.GetObject<global::Xamarin.Protobuf.Lite.LazyStringArrayList>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return JNIEnv.NewString((string)__this.Get(index));
        }
#pragma warning restore 0169

        static IntPtr id_get_I;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.protobuf']/class[@name='LazyStringArrayList']/method[@name='get' and count(parameter)=1 and parameter[1][@type='int']]"
        [Register("get", "(I)Ljava/lang/String;", "GetGet_IHandler")]
        public override unsafe global::Java.Lang.Object Get(int index)
        {
            if (id_get_I == IntPtr.Zero)
                id_get_I = JNIEnv.GetMethodID(class_ref, "get", "(I)Ljava/lang/String;");
            try
            {
                JValue* __args = stackalloc JValue[1];
                __args[0] = new JValue(index);

                if (((object)this).GetType() == ThresholdType)
                    return JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_get_I, __args), JniHandleOwnership.TransferLocalRef);
                else
                    return JNIEnv.GetString(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "get", "(I)Ljava/lang/String;"), __args), JniHandleOwnership.TransferLocalRef);
            }
            finally
            {
            }
        }
    }
}
