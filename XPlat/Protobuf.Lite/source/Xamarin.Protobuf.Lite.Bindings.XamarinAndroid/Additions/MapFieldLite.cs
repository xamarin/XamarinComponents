using System;
using Android.Runtime;

namespace Xamarin.Protobuf.Lite
{

    public partial class MapFieldLite
    {
        static IntPtr id_entrySet;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.google.protobuf']/class[@name='MapFieldLite']/method[@name='entrySet' and count(parameter)=0]"
        // [Register("entrySet", "()Ljava/util/Set;", "")]
        public unsafe global::System.Collections.ICollection EntrySet()
        {
            if (id_entrySet == IntPtr.Zero)
                id_entrySet = JNIEnv.GetMethodID(class_ref, "entrySet", "()Ljava/util/Set;");
            try
            {
                return (System.Collections.ICollection)
                        global::Android.Runtime.JavaSet<global::Java.Util.IMapEntry>.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_entrySet), JniHandleOwnership.TransferLocalRef);
            }
            finally
            {
            }
        }
    }
}
