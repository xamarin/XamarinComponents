using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

using GigyaSDK.Socialize;
using GigyaSDK.Socialize.Android;
using GigyaSDK.Socialize.Android.Events;

[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]

[assembly: UsesPermission(Android.Manifest.Permission.GetAccounts)]
[assembly: UsesPermission(Android.Manifest.Permission.UseCredentials)]

[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/facebook_app_id")]

namespace GigyaSDKSampleAndroid
{
	[Activity(Label = "GigyaSDKSampleAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : FragmentActivity, IGSLoginUIListener
	{
		private const string ApiKey = "3_maetISj1vIK2f6uWrM1gHaYHOxT-kKw-Y0g6D531hKF_8t74nBgbAHsJI4YUairG";

		private TextView userStatus;
		private Button loginButton;
		private Button logoutButton;
		private Button getFriendsButton;
		private Spinner friendsSpinner;
		private GSObject user;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			loginButton = FindViewById<Button>(Resource.Id.loginButton);
			logoutButton = FindViewById<Button>(Resource.Id.logoutButton);
			userStatus = FindViewById<TextView>(Resource.Id.userStatus);
			getFriendsButton = FindViewById<Button>(Resource.Id.getFriendsButton);
			friendsSpinner = FindViewById<Spinner>(Resource.Id.friendsSpinner);

			userStatus.Text = string.Empty;

			loginButton.Click += delegate { Login(); };
			logoutButton.Click += delegate { Logout(); };
			getFriendsButton.Click += delegate { GetFriends(); };

			InitGigya();
			LoadComments();

		}

		public GSObject User
		{
			get { return user; }
			set
			{
				user = value;

				if (user == null)
				{
					userStatus.Text = "User is logged out";
				}
				else
				{
					userStatus.Text = "User is logged in as " + user.GetString("nickname", "");
				}
			}
		}

		public async void InitGigya()
		{
			GSAPI.Instance.Initialize(this, ApiKey);

			GSAPI.Instance.SocializeLogin += (sender, e) =>
			{
				Console.WriteLine("Gigya logged in with " + e.Provider);
				User = e.User;
			};

			GSAPI.Instance.SocializeLogout += (sender, e) =>
			{
				Console.WriteLine("Gigya logged out");
				User = null;
			};

			GSAPI.Instance.SocializeConnectionAdded += (sender, e) =>
			{
				Console.WriteLine(e.Provider + " connection was added");
			};

			GSAPI.Instance.SocializeConnectionRemoved += (sender, e) =>
			{
				Console.WriteLine(e.Provider + " connection was removed");
			};

			var response = await GSAPI.Instance.SendRequestAsync("socialize.getUserInfo", null);
			if (response.ErrorCode == 0)
			{
				User = response.Data;
			}
			else
			{
				User = null;
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			if (GSAPI.Instance.HandleAndroidPermissionsResult(requestCode, permissions, grantResults))
				return;

			// handle other permissions result here
		}

		public void LoadComments()
		{
			var parameters = new GSObject();
			parameters["categoryID"] = "comments_demo";
			parameters["streamID"] = "android";

			var pluginFragment = GSPluginFragment.NewInstance("comments.commentsUI", parameters);
			pluginFragment.Load += (sender, e) => Console.WriteLine("Comments UI has finished loading");
			pluginFragment.Event += (sender, e) => Console.WriteLine("Received plugin event from Comments UI - " + e.Event.GetString("eventName", ""));
			pluginFragment.Error += (sender, e) => Console.WriteLine("Error in Comments UI - " + e.Error.GetInt("errorCode", -1));

			SupportFragmentManager
				.BeginTransaction()
				.Add(Resource.Id.comments_container, pluginFragment)
				.Commit();
		}

		public void Login()
		{
			GSAPI.Instance.ShowLoginUI(null, this, null);
		}

		void IGSLoginUIListener.OnLogin(string provider, GSObject user, Java.Lang.Object context)
		{
			Console.WriteLine("Gigya loginUI has logged in");
		}

		void IGSUIListener.OnClose(bool canceled, Java.Lang.Object context)
		{
			Console.WriteLine("Gigya loginUI was closed");
		}

		void IGSUIListener.OnError(GSResponse response, Java.Lang.Object context)
		{
			Console.WriteLine("Gigya loginUI had an error - " + response.ErrorMessage);
		}

		void IGSUIListener.OnLoad(Java.Lang.Object context)
		{
			Console.WriteLine("Gigya loginUI was loaded");
		}

		public void Logout()
		{
			GSAPI.Instance.Logout();

			var spinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
			friendsSpinner.Adapter = spinnerAdapter;
			spinnerAdapter.NotifyDataSetChanged();
		}

		public async void GetFriends()
		{
			var response = await GSAPI.Instance.SendRequestAsync("socialize.getFriendsInfo", null);

			var friends = response.GetArray("friends", new GSArray());

			var spinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Android.Resource.Id.Text1);
			spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			friendsSpinner.Adapter = spinnerAdapter;

			spinnerAdapter.Add("Choose a friend");
			for (int i = 0; i < friends.Length; i++)
			{
				var friend = friends[i];
				var username = friend.GetString("nickname", "");
				spinnerAdapter.Add(username);
			}

			spinnerAdapter.NotifyDataSetChanged();
		}
	}
}

