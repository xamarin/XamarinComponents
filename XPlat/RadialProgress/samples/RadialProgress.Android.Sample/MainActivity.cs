using System;
using System.Timers;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RadialProgress.Android.Sample
{
	[Activity (Label = "RadialProgress.Android.Sample", MainLauncher = true)]
	public class MainActivity : Activity
	{
		RadialProgressView bigRadialProgress, smallRadialProgress, tinyRadialProgress;
		Timer timer;
		Button btn;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			bigRadialProgress = FindViewById<RadialProgressView> (Resource.Id.bigProgress);
			smallRadialProgress = FindViewById<RadialProgressView> (Resource.Id.smallProgress);
			tinyRadialProgress = FindViewById<RadialProgressView> (Resource.Id.tinyProgress);

			btn = FindViewById<Button> (Resource.Id.myButton);
			btn.Click += HandleClick;

            bigRadialProgress.LabelTextDelegate = (val) => Math.Floor(100-val).ToString().PadLeft(2, '0');

			// Also you can add radial progress programmatically, if you want
			// var radialProgress = new RadialProgress(this, 0, 100, RadialProgressViewStyle.Big);
			// FindViewById<LinearLayout>(Resource.Id.rootLayout).AddView(radialProgress);
		}

		void HandleClick (object sender, EventArgs e)
		{
			if (timer != null) {
				timer.Stop();
				timer.Close();
				timer = null;
				btn.Text = "Start";
				return;
			}

			bigRadialProgress.Value = bigRadialProgress.MinValue;
			smallRadialProgress.Value = bigRadialProgress.MinValue;
			tinyRadialProgress.Value = bigRadialProgress.MinValue;


			timer = new Timer(20);
			timer.Elapsed += HandleElapsed;
			timer.Start();

			btn.Text = "Stop";
		}

		void HandleElapsed (object sender, ElapsedEventArgs e)
		{
			bigRadialProgress.Value ++;
			smallRadialProgress.Value ++;
			tinyRadialProgress.Value ++;

			if (bigRadialProgress.Value >= bigRadialProgress.MaxValue) {
				bigRadialProgress.Value = 0;
				smallRadialProgress.Value = 0;
				tinyRadialProgress.Value = 0;
			}
		}
	}
}


