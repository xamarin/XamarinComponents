using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Grantland.Widget;

namespace AutoFitTextViewSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		private TextView output;
		private TextView autofitOutput;
		private EditText input;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.main);

			output = FindViewById<TextView> (Resource.Id.output);
			autofitOutput = FindViewById<TextView> (Resource.Id.output_autofit);

			input = FindViewById<EditText> (Resource.Id.input);
			input.TextChanged += (sender, e) => {
				output.Text = input.Text;
				autofitOutput.Text = input.Text;
			};
		}
	}
}
