using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

using Square.OkHttp3;

namespace OkHttp3Sample
{
    [Activity(Label = "OkHttpSample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private static string Endpoint = "https://api.github.com/repos/square/okhttp/contributors";
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button download = FindViewById<Button>(Resource.Id.download);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            download.Click += async delegate
            {
                OkHttpClient client = new OkHttpClient();

                // Create request for remote resource.
                Request request = new Request.Builder()
                    .Url(Endpoint)
                    .Build();

                // Execute the request and retrieve the response.
                Response response = await client.NewCall(request).ExecuteAsync();

                // Deserialize HTTP response to concrete type.
				string body = await response.Body().StringAsync();
                List<Contributor> contributors = JsonConvert.DeserializeObject<List<Contributor>>(body);

                // Sort list by the most contributions.
                List<string> data = contributors
                    .OrderByDescending(c => c.contributions)
                    .Select(c => string.Format("{0} ({1})", c.login, c.contributions))
                    .ToList();

                // Output list of contributors.
                IListAdapter adapter = new ArrayAdapter<string>(
                    this, 
                    Android.Resource.Layout.SimpleListItem1,
                    Android.Resource.Id.Text1, 
                    data);
                listView.Adapter = adapter;
            };
        }

        public class Contributor
        {
            public string login { get; set; }

            public int contributions { get; set; }
        }
    }
}
