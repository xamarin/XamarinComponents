using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

using KotlinSampleLibrary;

namespace KotlinSample
{
	[Activity(Label = "KotlinSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			var textView = FindViewById<TextView>(Resource.Id.textView);

			var instance = new TestClass();
			var list = instance.Test(new[] { "first", "second", "third" });

			textView.Text = $"There are {list.Count} items.";
		}
	}
}
