using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using Crashlytics.Devtools;

namespace CrashAppSample
{
    [Activity(Label = "CrashAppSample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            CrashReporting.StartWithMonoHook(this, true);
            Crashlytics.Devtools.Crashlytics.SetString("BUILDNUMBER", "1.0.0.0");

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            var button1 = FindViewById<Button>(Resource.Id.Crash1);
            var button2 = FindViewById<Button>(Resource.Id.Crash2);
            var button3 = FindViewById<Button>(Resource.Id.Crash3);
            var button4 = FindViewById<Button>(Resource.Id.Crash4);

            button1.Click += (sender, args) => { throw new InvalidOperationException("This is a .NET exception!"); };

            button2.Click += (sender, args) => SetContentView(888888);

            button3.Click += (sender, args) =>
            {
                try
                {
                    try
                    {
                        throw new ApplicationException("This is a nexted exception");
                    }
                    catch (Exception e1)
                    {
                        throw new InvalidCastException("Level 1", e1);
                    }
                }
                catch (Exception e2)
                {
                    throw new InvalidCastException("Level 2", e2);
                }
            };

            button4.Click += (sender, args) => CrashAsync().Wait();
        }

        public async Task CrashAsync()
        {
            await Task.Delay(10).ConfigureAwait(false);
            throw new InvalidOperationException("Exception in task");
        }
    }
}

