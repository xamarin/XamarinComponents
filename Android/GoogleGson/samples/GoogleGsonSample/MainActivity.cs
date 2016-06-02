using System;
using Android.Accounts;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using GoogleGson;

namespace GoogleGsonSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.main);
			var textView = FindViewById<TextView> (Resource.Id.textView);

			// create an object
			var account = new Account ("Xamarin", "Corporate");

			// create the serializer
			var gson = new GsonBuilder ()
				.SetPrettyPrinting ()
				.Create ();

			// serialize the object into json
			var json = gson.ToJson (account);

			// deserialize the json into the object
			var clazz = Java.Lang.Class.FromType (typeof(Account));
			var newAccount = (Account)gson.FromJson (json, clazz);

			textView.Text = string.Join (System.Environment.NewLine, new [] { 
				json, 
				"Name == 'Xamarin': " + (newAccount.Name == "Xamarin"), 
				"Type == 'Corporate': " + (newAccount.Type == "Corporate") 
			});
		}
	}
}
