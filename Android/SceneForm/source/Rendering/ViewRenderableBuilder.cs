using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;

namespace Google.AR.Sceneform.Rendering
{
    public partial class ViewRenderable
    {
        public partial class Builder
        {
            public unsafe ViewRenderable.Builder SetSource(global::Android.Content.Context context, int resource)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, resource);

                return this;
            }

            public unsafe ViewRenderable.Builder SetSource(global::Android.Content.Context context, global::Android.Net.Uri sourceUri)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, sourceUri);

                return this;
            }

            public unsafe ViewRenderable.Builder SetSource(global::Android.Content.Context context, global::Java.Util.Concurrent.ICallable inputStreamCreator)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, inputStreamCreator);

                return this;
            }

            public unsafe ViewRenderable.Builder SetSource(global::Google.AR.Sceneform.Rendering.RenderableDefinition definition)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(definition);

                return this;
            }

            public void Build(Action<ViewRenderable> completionAction)
            {
                this.Build().ThenAccept(new ViewRenderableConsumer(completionAction));
            }
        }
    }
}
