
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

namespace HelloSceneForm
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked, Exported = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class HelloSceneformActivity : AppCompatActivity
    {
        private ArFragment arFragment;
        private ModelRenderable andyRenderable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_ux);

            arFragment = (ArFragment)SupportFragmentManager.FindFragmentById(Resource.Id.ux_fragment);
            // Create your application here


            ModelRenderable.InvokeBuilder().SetSource(this, Resource.Raw.andy).Build();
            
        }
    }
}
