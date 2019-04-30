using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.AR.Sceneform.Animation
{
    public partial class ModelAnimator
    {
        public override unsafe long StartDelay
        {

            [Register("getStartDelay", "()J", "GetGetStartDelayHandler")]
            get
            {
                const string __id = "getStartDelay.()J";
                try
                {
                    var __rm = _members.InstanceMethods.InvokeVirtualInt64Method(__id, this, null);
                    return __rm;
                }
                finally
                {

                }
            }
            set
            {
                SetStartDelay(value);
            }
        }

        public unsafe void SetStartDelay(long p0)
        {
            const string __id = "setStartDelay.(J)V";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[1];
                __args[0] = new JniArgumentValue(p0);
                _members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
            }
            finally
            {
            }
        }
    }
}
