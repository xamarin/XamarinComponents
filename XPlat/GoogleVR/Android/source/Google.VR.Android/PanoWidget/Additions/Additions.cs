using System;
using Android.Runtime;

namespace Google.VR.SDK.Widgets.Pano
{
    public partial class VrPanoramaView
    {
        static IntPtr id_createRenderer_Manual;

         //Metadata.xml XPath method reference: path="/api/package[@name='com.google.vr.sdk.widgets.common']/class[@name='VrWidgetView']/method[@name='createRenderer' and count(parameter)=5 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.google.vr.sdk.widgets.common.VrWidgetRenderer.GLThreadScheduler'] and parameter[3][@type='float'] and parameter[4][@type='float'] and parameter[5][@type='int']]"
        [Register ("createRenderer", "(Landroid/content/Context;Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer$GLThreadScheduler;FFI)Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer;", "GetCreateRenderer_Landroid_content_Context_Lcom_google_vr_sdk_widgets_common_VrWidgetRenderer_GLThreadScheduler_FFIHandler")]
        protected override unsafe global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer CreateRenderer (global::Android.Content.Context context, global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer.IGLThreadScheduler glThreadScheduler, float xMetersPerPixel, float yMetersPerPixel, int screenRotation)
        {
            if (id_createRenderer_Manual == IntPtr.Zero)
                id_createRenderer_Manual = JNIEnv.GetMethodID (class_ref, "createRenderer", "(Landroid/content/Context;Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer$GLThreadScheduler;FFI)Lcom/google/vr/sdk/widgets/common/VrWidgetRenderer;");
            try {
                JValue* __args = stackalloc JValue [5];
                __args [0] = new JValue (context);
                __args [1] = new JValue (glThreadScheduler);
                __args [2] = new JValue (xMetersPerPixel);
                __args [3] = new JValue (yMetersPerPixel);
                __args [4] = new JValue (screenRotation);
                global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer __ret = global::Java.Lang.Object.GetObject<global::Google.VR.SDK.Widgets.Common.VrWidgetRenderer> (JNIEnv.CallObjectMethod (Handle, id_createRenderer_Manual, __args), JniHandleOwnership.TransferLocalRef);
                return __ret;
            } finally {
            }
        }
    }
}

