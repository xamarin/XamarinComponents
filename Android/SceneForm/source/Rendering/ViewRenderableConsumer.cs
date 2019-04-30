using Android.Runtime;
using Google.AR.Sceneform.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.AR.Sceneform.Rendering
{
    internal class ViewRenderableConsumer : Java.Lang.Object, Java.Util.Functions.IConsumer
    {
        Action<ViewRenderable> _completed;

        public ViewRenderableConsumer(Action<ViewRenderable> action)
        {
            _completed = action;
        }

        public void Accept(Java.Lang.Object t)
        {
            _completed(t.JavaCast<ViewRenderable>());
        }
    }
}
