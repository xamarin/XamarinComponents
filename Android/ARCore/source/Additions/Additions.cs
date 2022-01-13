using System;
using Android.Runtime;

namespace Google.AR.Core
{
    public partial class Point
    {
        static Delegate cb_getAnchors;
#pragma warning disable 0169
        static Delegate GetGetAnchorsHandler()
        {
            if (cb_getAnchors == null)
                cb_getAnchors = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetAnchors);
            return cb_getAnchors;
        }

        static IntPtr n_GetAnchors(IntPtr jnienv, IntPtr native__this)
        {
            global::Google.AR.Core.Point __this = global::Java.Lang.Object.GetObject<global::Google.AR.Core.Point>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.ToLocalJniHandle(__this.Anchors);
        }
#pragma warning restore 0169

        static IntPtr id_getAnchors;
        public virtual unsafe global::System.Collections.Generic.ICollection<global::Google.AR.Core.Anchor> Anchors {
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.core']/class[@name='Point']/method[@name='getAnchors' and count(parameter)=0]"
            [Register("getAnchors", "()Ljava/util/Collection;", "GetGetAnchorsHandler")]
            get {
                if (id_getAnchors == IntPtr.Zero)
                    id_getAnchors = JNIEnv.GetMethodID(class_ref, "getAnchors", "()Ljava/util/Collection;");
                try {

                    if (((object)this).GetType() == ThresholdType)
                        return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getAnchors), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.FromJniHandle(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "getAnchors", "()Ljava/util/Collection;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }


    public partial class Plane
    {
        static Delegate cb_getAnchors;
#pragma warning disable 0169
        static Delegate GetGetAnchorsHandler()
        {
            if (cb_getAnchors == null)
                cb_getAnchors = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetAnchors);
            return cb_getAnchors;
        }

        static IntPtr n_GetAnchors(IntPtr jnienv, IntPtr native__this)
        {
            global::Google.AR.Core.Plane __this = global::Java.Lang.Object.GetObject<global::Google.AR.Core.Plane>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.ToLocalJniHandle(__this.Anchors);
        }
#pragma warning restore 0169

        static IntPtr id_getAnchors;
        public virtual unsafe global::System.Collections.Generic.ICollection<global::Google.AR.Core.Anchor> Anchors {
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.core']/class[@name='Plane']/method[@name='getAnchors' and count(parameter)=0]"
            [Register("getAnchors", "()Ljava/util/Collection;", "GetGetAnchorsHandler")]
            get {
                if (id_getAnchors == IntPtr.Zero)
                    id_getAnchors = JNIEnv.GetMethodID(class_ref, "getAnchors", "()Ljava/util/Collection;");
                try {

                    if (((object)this).GetType() == ThresholdType)
                        return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getAnchors), JniHandleOwnership.TransferLocalRef);
                    else
                        return global::Android.Runtime.JavaCollection<global::Google.AR.Core.Anchor>.FromJniHandle(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "getAnchors", "()Ljava/util/Collection;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }
}
