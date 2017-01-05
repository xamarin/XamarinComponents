using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LoginScreen.Activities
{
	class BaseActivity : Activity
	{
		internal const string CredentialsProviderKey = "CredentialsProviderKey";
		internal const string MessagesKey = "MessagesKey";

		protected ICredentialsProvider credentialsProvider;
		protected ILoginScreenMessages messages;

		protected AlertDialog progressDialog;

		protected RelativeLayout rootLayout;

		private Dictionary<EditText, View> validationAlerts = new Dictionary<EditText, View>();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			string credentialsProviderTypeName = Intent.GetStringExtra(CredentialsProviderKey);
			Type credentialsProviderType = Type.GetType(credentialsProviderTypeName);
			credentialsProvider = Activator.CreateInstance(credentialsProviderType) as ICredentialsProvider;

			string messagesTypeName = Intent.GetStringExtra(MessagesKey);
			Type messagesType = Type.GetType(messagesTypeName);
			messages = Activator.CreateInstance(messagesType) as ILoginScreenMessages;
		}

		protected override void OnResume()
		{
			base.OnResume();
			foreach (var pair in validationAlerts)
			{
				RemoveAlert(pair.Key);
			}
			validationAlerts.Clear();
		}

		protected void ShowAlert(EditText editText, string alertText)
		{
			if (validationAlerts.ContainsKey(editText))
			{
				return;
			}

			var metrics = GetDisplayMetrics();
			int layoutOffsetY = metrics.HeightPixels - rootLayout.Height; 
			
			int[] editTextCoordinates = new int[2];
			editText.GetLocationOnScreen(editTextCoordinates);
			
			int xOffset = editTextCoordinates[0];
			int yOffset = editTextCoordinates[1] + editText.Height - GetPixelsFromDpi(17) - layoutOffsetY;
			
			var inflater = (LayoutInflater)this.GetSystemService(Context.LayoutInflaterService);
			var view = inflater.Inflate(Resource.Layout.a_validationerror, null);
			
			view.FindViewById<TextView>(Resource.Id.errorMessage).Text = alertText;
			
			var lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
			lp.LeftMargin = xOffset;
			lp.TopMargin = yOffset;
			rootLayout.AddView(view, lp);

			validationAlerts.Add(editText, view);

			editText.FocusChange += HandleFocusChange;
		}

		private void HandleFocusChange (object sender, View.FocusChangeEventArgs e)
		{
			if (e.HasFocus)
			{
				var editText = sender as EditText;
				if (editText != null && validationAlerts.ContainsKey(editText))
				{
					RemoveAlert(editText);
					validationAlerts.Remove(editText);
				}
			}
		}

		private void RemoveAlert(EditText editText)
		{
			rootLayout.RemoveView(validationAlerts[editText]);
			editText.FocusChange -= HandleFocusChange;
		}

		private int GetPixelsFromDpi(int dpi)
		{
			var metrics = GetDisplayMetrics();
			return (int)(dpi * metrics.ScaledDensity);
		}
		
		private DisplayMetrics GetDisplayMetrics()
		{
			var metrics = new DisplayMetrics();
			WindowManager.DefaultDisplay.GetMetrics(metrics);
			return metrics;
		}

		protected bool IsValidEmail(string emailAddress)
		{
			try
			{
				new MailAddress(emailAddress);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}

