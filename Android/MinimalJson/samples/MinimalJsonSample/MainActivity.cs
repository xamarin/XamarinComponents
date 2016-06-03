using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using MinimalJson;

namespace MinimalJsonSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		private const string JsonString = @"
{
  ""order"": 4711,
  ""items"": [
    {
      ""name"": ""NE555 Timer IC"",
      ""cat-id"": ""645723"",
      ""quantity"": 10
    },
    {
      ""name"": ""LM358N OpAmp IC"",
      ""cat-id"": ""764525"",
      ""quantity"": 2
    }
  ]
}";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.main);

			var textViewIn = FindViewById<TextView> (Resource.Id.textViewIn);
			var textViewOut = FindViewById<TextView> (Resource.Id.textViewOut);
			var textViewNew = FindViewById<TextView> (Resource.Id.textViewNew);

			textViewIn.Text = JsonString;

			// parse the JSON
			var parsed = Json.Parse (JsonString).AsObject ();

			// read the parsed JSON
			textViewOut.Text += "Order ID: " + parsed.GetInt ("order", 0);
			textViewOut.Text += "\nItems: ";
			foreach (var item in parsed.Get("items").AsArray()) {
				var itemObject = item.AsObject ();
				textViewOut.Text += "\n - Name: " + itemObject.GetString ("name", "N/A");
				textViewOut.Text += "\n   Quantity: " + itemObject.GetInt ("quantity", 0);
			}

			// create new JSON
			var newJson = Json.Object ()
				.Add ("name", "Xamarin")
				.Add ("products", Json.Array ()
					.Add (Json.Object ()
						.Add ("name", "Xamarin.Android")
						.Add ("description", "Stuff for Android"))
					.Add (Json.Object ()
						.Add ("name", "Xamarin.iOS")
						.Add ("description", "Stuff for iOS"))
					.Add (Json.Object ()
						.Add ("name", "Xamarin.Mac")
						.Add ("description", "Stuff for Mac")));

			// get JSON string
			textViewNew.Text = newJson.ToString (WriterConfig.PrettyPrint);
		}
	}
}
