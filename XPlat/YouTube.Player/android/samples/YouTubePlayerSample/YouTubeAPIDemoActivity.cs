using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

using YouTubePlayerSample.Adapters;

namespace YouTubePlayerSample
{
	// Main activity from which the user can select one of the other demo activities.
	[Activity(Label = "@string/youtube_api_demo", MainLauncher = true)]
	public class YouTubeAPIDemoActivity : Activity
	{
		private readonly List<DemoListViewItem> activities = new List<DemoListViewItem>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.demo_home);

			PackageInfo packageInfo = null;
			var pm = PackageManager;
			try
			{
				packageInfo = pm.GetPackageInfo(PackageName, PackageInfoFlags.Activities | PackageInfoFlags.MetaData);
			}
			catch (PackageManager.NameNotFoundException)
			{
				Console.WriteLine("Could not find package with name " + PackageName);
			}
			if (packageInfo == null)
				Finish();

			var appMinVersion = (int)packageInfo.ApplicationInfo.TargetSdkVersion;
			foreach (var activityInfo in packageInfo.Activities)
			{
				var name = activityInfo.Name;
				var metaData = activityInfo.MetaData;

				if (metaData != null && metaData.GetBoolean(GetString(Resource.String.isLaunchableActivity), false))
				{
					var label = GetString(activityInfo.LabelRes);
					var minVersion = (BuildVersionCodes)metaData.GetInt(GetString(Resource.String.minVersion), appMinVersion);
					activities.Add(new Demo(label, name, minVersion));
				}
			}

			activities.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.Title, y.Title));

			var listView = FindViewById<ListView>(Resource.Id.demo_list);
			var adapter = new DemoArrayAdapter(this, Resource.Layout.list_item, activities);
			listView.Adapter = adapter;
			listView.ItemClick += (sender, e) =>
			{
				var clickedDemo = (Demo)activities[e.Position];

				var intent = new Intent();
				intent.SetComponent(new ComponentName(PackageName, clickedDemo.ActivityClass));
				StartActivity(intent);
			};

			var disabledText = FindViewById<TextView>(Resource.Id.some_demos_disabled_text);
			disabledText.Text = GetString(Resource.String.some_demos_disabled, (int)Build.VERSION.SdkInt);
			disabledText.Visibility = adapter.AnyDisabled ? ViewStates.Visible : ViewStates.Gone;
		}

		private class Demo : DemoListViewItem
		{
			private readonly string title;
			private readonly BuildVersionCodes minVersion;
			private readonly string className;

			public Demo(string title, string className, BuildVersionCodes minVersion)
			{
				this.className = className;
				this.title = title;
				this.minVersion = minVersion;
			}

			public override bool IsEnabled => Build.VERSION.SdkInt >= minVersion;

			public override string DisabledText => string.Format(Application.Context.GetString(Resource.String.list_item_disabled), minVersion);

			public override string Title => title;

			public string ActivityClass => className;
		}
	}
}
