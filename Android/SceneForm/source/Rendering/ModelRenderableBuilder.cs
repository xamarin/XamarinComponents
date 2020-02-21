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
            public void Build(Action<ModelRenderable> completionAction)
            {
                this.Build().ThenAccept(new ModelRenderableConsumer(completionAction));
            }
        }

    }
}
