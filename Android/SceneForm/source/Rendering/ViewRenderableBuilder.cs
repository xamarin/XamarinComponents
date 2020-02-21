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

            public void Build(Action<ViewRenderable> completionAction)
            {
                this.Build().ThenAccept(new ViewRenderableConsumer(completionAction));
            }
        }
    }
}
