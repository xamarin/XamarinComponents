
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Google.AR.Sceneform.UX;
using Google.AR.Sceneform.Rendering;
using Google.AR.Schemas.Sceneform;
using Google.AR.Core;
using Google.AR.Sceneform;

namespace HelloSceneForm
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked, Exported = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class HelloSceneformActivity : AppCompatActivity
    {
        private static double MIN_OPENGL_VERSION = 3.0;

        private ArFragment arFragment;
        private ModelRenderable andyRenderable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (!CheckIsSupportedDeviceOrFinish(this))
                return;
        
            SetContentView(Resource.Layout.activity_ux);

            arFragment = (ArFragment)SupportFragmentManager.FindFragmentById(Resource.Id.ux_fragment);
            // Create your application here


            var builder = ModelRenderable.InvokeBuilder().SetSource(this, Resource.Raw.andy).Build();

            while (!builder.IsDone)
            {
                //wait until the model is built
            }

            andyRenderable = (ModelRenderable)builder.Get();

            arFragment.TapArPlane += OnTapArPlane;

        }

        private void OnTapArPlane(object sender, BaseArFragment.TapArPlaneEventArgs e)
        {
            if (andyRenderable == null)
                return;

            // Create the Anchor.
            Anchor anchor = e.HitResult.CreateAnchor();
            AnchorNode anchorNode = new AnchorNode(anchor);
            anchorNode.SetParent(arFragment.ArSceneView.Scene);

            // Create the transformable andy and add it to the anchor.
            TransformableNode andy = new TransformableNode(arFragment.TransformationSystem);
            andy.SetParent(anchorNode);
            andy.Renderable = andyRenderable;
            andy.Select();

        }

        public static bool CheckIsSupportedDeviceOrFinish(Activity activity)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                Toast.MakeText(activity, "Sceneform requires Android N or later", ToastLength.Long).Show();

                activity.Finish();

                return false;
            }

            var openglString = ((ActivityManager)activity.GetSystemService(Context.ActivityService)).DeviceConfigurationInfo.GlEsVersion;

            if (Double.Parse(openglString) < MIN_OPENGL_VERSION)
            {
                Toast.MakeText(activity, "Sceneform requires OpenGL ES 3.0 or later", ToastLength.Long).Show();

                return false;
            }

            return true;

        }
    }
}


