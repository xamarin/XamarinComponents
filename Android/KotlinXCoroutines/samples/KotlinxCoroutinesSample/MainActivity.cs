using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace KotlinxCoroutinesSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView textView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            textView = FindViewById<TextView>(Resource.Id.textView);
            textView.Text = "";

            KotlinxCoroutinesTestGlobalScope();

            Thread.Sleep(3000);

            KotlinxCoroutinesTestScopeBuilder();

            textView.Append("\n\nEnd\n\n");
        }

        private void KotlinxCoroutinesTestScopeBuilder()
        {
            var k = new Com.Example.Kotlinxcoroutinessample.KotlinxCoroutineTestClass(textView);

            k.TestScopeBuilder();
        }

        private void KotlinxCoroutinesTestGlobalScope()
        {
            var k = new Com.Example.Kotlinxcoroutinessample.KotlinxCoroutineTestClass(textView);

            k.TestGlobalScope();
        }
    }
}