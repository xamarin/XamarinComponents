using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.AR.Sceneform.Rendering
{
    public partial class ModelRenderable
    {
        public partial class Builder
        {
            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.sceneform.rendering']/class[@name='Renderable.Builder']/method[@name='setSource' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='int']]"
            [Register("setSource", "(Landroid/content/Context;I)Ljava/lang/Object;", "")]
            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, int resource)
            {
                const string __id = "setSource.(Landroid/content/Context;I)Ljava/lang/Object;";
                try
                {
                    JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                    __args[0] = new JniArgumentValue((context == null) ? IntPtr.Zero : ((global::Java.Lang.Object)context).Handle);
                    __args[1] = new JniArgumentValue(resource);
                    var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                    return global::Java.Lang.Object.GetObject<ModelRenderable.Builder>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                }
                finally
                {
                }
            }

            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.sceneform.rendering']/class[@name='Renderable.Builder']/method[@name='setSource' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.net.Uri']]"
            [Register("setSource", "(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/Object;", "")]
            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, global::Android.Net.Uri sourceUri)
            {
                const string __id = "setSource.(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/Object;";
                try
                {
                    JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                    __args[0] = new JniArgumentValue((context == null) ? IntPtr.Zero : ((global::Java.Lang.Object)context).Handle);
                    __args[1] = new JniArgumentValue((sourceUri == null) ? IntPtr.Zero : ((global::Java.Lang.Object)sourceUri).Handle);
                    var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                    return global::Java.Lang.Object.GetObject<ModelRenderable.Builder>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                }
                finally
                {
                }
            }

            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.sceneform.rendering']/class[@name='Renderable.Builder']/method[@name='setSource' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.util.concurrent.Callable&lt;java.io.InputStream&gt;']]"
            [Register("setSource", "(Landroid/content/Context;Ljava/util/concurrent/Callable;)Ljava/lang/Object;", "")]
            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, global::Java.Util.Concurrent.ICallable inputStreamCreator)
            {
                const string __id = "setSource.(Landroid/content/Context;Ljava/util/concurrent/Callable;)Ljava/lang/Object;";
                try
                {
                    JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                    __args[0] = new JniArgumentValue((context == null) ? IntPtr.Zero : ((global::Java.Lang.Object)context).Handle);
                    __args[1] = new JniArgumentValue((inputStreamCreator == null) ? IntPtr.Zero : ((global::Java.Lang.Object)inputStreamCreator).Handle);
                    var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                    return global::Java.Lang.Object.GetObject<ModelRenderable.Builder>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                }
                finally
                {
                }
            }

            // Metadata.xml XPath method reference: path="/api/package[@name='com.google.ar.sceneform.rendering']/class[@name='Renderable.Builder']/method[@name='setSource' and count(parameter)=1 and parameter[1][@type='com.google.ar.sceneform.rendering.RenderableDefinition']]"
            [Register("setSource", "(Lcom/google/ar/sceneform/rendering/RenderableDefinition;)Ljava/lang/Object;", "")]
            public unsafe ModelRenderable.Builder SetSource(global::Google.AR.Sceneform.Rendering.RenderableDefinition definition)
            {
                const string __id = "setSource.(Lcom/google/ar/sceneform/rendering/RenderableDefinition;)Ljava/lang/Object;";
                try
                {
                    JniArgumentValue* __args = stackalloc JniArgumentValue[1];
                    __args[0] = new JniArgumentValue((definition == null) ? IntPtr.Zero : ((global::Java.Lang.Object)definition).Handle);
                    var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
                    return global::Java.Lang.Object.GetObject<ModelRenderable.Builder>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                }
                finally
                {
                }
            }
        }

    }
}
