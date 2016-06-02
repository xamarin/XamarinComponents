using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace NineOldAndroidsSample
{
    [Activity(Label = "Nine Old Androids", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            listView = FindViewById<ListView>(Resource.Id.listView);

            var path = Intent.GetStringExtra("com.example.android.apis.Path") ?? string.Empty;
            var data = GetData(path).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var labels = data.Keys.ToArray();
            var intents = data.Values.ToArray();

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, labels);
            listView.ItemClick += (sender, e) =>
            {
                StartActivity(intents[e.Position]);
            };
        }

        private IDictionary<string, Intent> GetData(string prefix)
        {
            var myData = new Dictionary<string, Intent>();

            var mainIntent = new Intent(Intent.ActionMain, null);
            mainIntent.AddCategory("com.yourcompany.nineoldandroids.sample.SAMPLE");
            var list = PackageManager.QueryIntentActivities(mainIntent, 0);
            if (list != null)
            {
                string[] prefixPath = null;
                var prefixWithSlash = prefix;
                if (!string.IsNullOrEmpty(prefix))
                {
                    prefixPath = prefix.Split('/');
                    prefixWithSlash = prefix + "/";
                }

                var entries = new List<string>();
                foreach (var info in list)
                {
                    var labelSeq = info.LoadLabel(PackageManager);
                    var label = labelSeq ?? info.ActivityInfo.Name;

                    if (prefixWithSlash.Length == 0 || label.StartsWith(prefixWithSlash, System.StringComparison.Ordinal))
                    {
                        var labelPath = label.Split('/');
                        var nextLabel = prefixPath == null ? labelPath[0] : labelPath[prefixPath.Length];

                        if ((prefixPath != null ? prefixPath.Length : 0) == labelPath.Length - 1)
                        {
                            var result = new Intent();
                            result.SetClassName(info.ActivityInfo.ApplicationInfo.PackageName, info.ActivityInfo.Name);
                            myData.Add(nextLabel, result);
                        }
                        else if (!entries.Contains(nextLabel))
                        {
                            var result = new Intent();
                            result.SetClass(this, typeof(MainActivity));
                            result.PutExtra("com.example.android.apis.Path", string.IsNullOrEmpty(prefix) ? nextLabel : prefix + "/" + nextLabel);
                            myData.Add(nextLabel, result);
                            entries.Add(nextLabel);
                        }
                    }
                }
            }

            return myData;
        }
    }
}
