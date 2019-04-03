using System;
using Java.Interop;

namespace Google.AR.Sceneform.UX
{

    public partial class TranslationController
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

    }
}
