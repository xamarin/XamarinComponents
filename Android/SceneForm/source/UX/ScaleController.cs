using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.AR.Sceneform.UX
{

    public partial class ScaleController
    {
        protected override unsafe void OnEndTransformation(global::Java.Lang.Object gesture)
        {
            const string __id = "onEndTransformation.(Ljava/lang/Object;)V";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[1];
                __args[0] = new JniArgumentValue((gesture == null) ? IntPtr.Zero : ((global::Java.Lang.Object)gesture).Handle);
                _members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
            }
            finally
            {
            }
        }

        protected override unsafe void OnContinueTransformation(global::Java.Lang.Object gesture)
        {
            const string __id = "onContinueTransformation.(Ljava/lang/Object;)V";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[1];
                __args[0] = new JniArgumentValue((gesture == null) ? IntPtr.Zero : ((global::Java.Lang.Object)gesture).Handle);
                _members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
            }
            finally
            {
            }
        }

        protected override unsafe bool CanStartTransformation(global::Java.Lang.Object gesture)
        {
            const string __id = "canStartTransformation.(Ljava/lang/Object;)Z";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[1];
                __args[0] = new JniArgumentValue((gesture == null) ? IntPtr.Zero : ((global::Java.Lang.Object)gesture).Handle);
                var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod(__id, this, __args);
                return __rm;
            }
            finally
            {
            }
        }
    }
}
