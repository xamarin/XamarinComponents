using System;
using Android.App;
using Android.Runtime;
using UniversalImageLoader.Core;

namespace PollexorSample
{
    [Application]
    public class PollexorApp : Application
    {
        protected PollexorApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Use default options
            var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);

            // Initialize ImageLoader with configuration.
            ImageLoader.Instance.Init(config);
        }
    }
}
