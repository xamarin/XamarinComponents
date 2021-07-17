using Android;
using Android.Runtime;
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
            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, int resource)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, resource);

                return this;
            }

            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, global::Android.Net.Uri sourceUri)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, sourceUri);

                return this;
            }

            public unsafe ModelRenderable.Builder SetSource(global::Android.Content.Context context, global::Java.Util.Concurrent.ICallable inputStreamCreator)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(context, inputStreamCreator);

                return this;
            }

            public unsafe ModelRenderable.Builder SetSource(global::Google.AR.Sceneform.Rendering.RenderableDefinition definition)
            {
                var rbuilder = this.JavaCast<Renderable.Builder>();
                rbuilder.SetSource(definition);

                return this;
            }

            public void Build(Action<ModelRenderable> completionAction)
            {
                this.Build().ThenAccept(new ModelRenderableConsumer(completionAction));
            }
        }

    }
}
