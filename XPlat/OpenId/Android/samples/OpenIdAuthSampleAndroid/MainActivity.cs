using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using OpenId.AppAuth;

namespace OpenIdAuthSampleAndroid
{
	[Activity(
		Label = "@string/app_name_short",
		MainLauncher = true,
		Theme = "@style/AppTheme.NoActionBar",
		WindowSoftInputMode = SoftInput.StateHidden)]
	public class MainActivity : AppCompatActivity
	{
		private static string DiscoveryEndpoint = "https://accounts.google.com/.well-known/openid-configuration";
		private static string ClientId = "YOUR_APP_ID.apps.googleusercontent.com";
		private static string RedirectUri = "com.googleusercontent.apps.YOUR_APP_ID:/oauth2redirect";

		private static string AuthEndpoint = null; // auth endpoint is discovered
		private static string TokenEndpoint = null; // token endpoint is discovered
		private static string RegistrationEndpoint = null; // dynamic registration not supported
		
		private AuthorizationService authService;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			authService = new AuthorizationService(this);

			var idpButton = FindViewById<Button>(Resource.Id.idpButton);
			idpButton.Click += async delegate
			{
				Console.WriteLine("initiating auth...");

				try
				{
					AuthorizationServiceConfiguration serviceConfiguration;
					if (DiscoveryEndpoint != null)
					{
						serviceConfiguration = await AuthorizationServiceConfiguration.FetchFromUrlAsync(
							Android.Net.Uri.Parse(DiscoveryEndpoint));
					}
					else
					{
						serviceConfiguration = new AuthorizationServiceConfiguration(
							Android.Net.Uri.Parse(AuthEndpoint), 
							Android.Net.Uri.Parse(TokenEndpoint),
							Android.Net.Uri.Parse(RegistrationEndpoint));
					}

					Console.WriteLine("configuration retrieved, proceeding");
					if (ClientId == null)
					{
						// Do dynamic client registration if no client_id
						MakeRegistrationRequest(serviceConfiguration);
					}
					else
					{
						MakeAuthRequest(serviceConfiguration, new AuthState());
					}
				}
				catch (AuthorizationException ex)
				{
					Console.WriteLine("Failed to retrieve configuration:" + ex);
				}
			};
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			authService.Dispose();
		}

		private void MakeAuthRequest(AuthorizationServiceConfiguration serviceConfig, AuthState authState)
		{
			var authRequest = new AuthorizationRequest.Builder(serviceConfig, ClientId, ResponseTypeValues.Code, Android.Net.Uri.Parse(RedirectUri))
				.SetScope("openid profile email")
				.Build();

			Console.WriteLine("Making auth request to " + serviceConfig.AuthorizationEndpoint);
			authService.PerformAuthorizationRequest(
				authRequest,
				TokenActivity.CreatePostAuthorizationIntent(this, authRequest, serviceConfig.DiscoveryDoc, authState),
				authService.CreateCustomTabsIntentBuilder().SetToolbarColor(Resources.GetColor(Resource.Color.colorAccent)).Build());
		}

		private async void MakeRegistrationRequest(AuthorizationServiceConfiguration serviceConfig)
		{
			var registrationRequest = new RegistrationRequest.Builder(serviceConfig, new[] { Android.Net.Uri.Parse(RedirectUri) })
				.SetTokenEndpointAuthenticationMethod(ClientSecretBasic.Name)
				.Build();

			Console.WriteLine("Making registration request to " + serviceConfig.RegistrationEndpoint);

			try
			{
				var registrationResponse = await authService.PerformRegistrationRequestAsync(registrationRequest);
				Console.WriteLine("Registration request complete");

				if (registrationResponse != null)
				{
					ClientId = registrationResponse.ClientId;
					Console.WriteLine("Registration request complete successfully");
					// Continue with the authentication
					MakeAuthRequest(registrationResponse.Request.Configuration, new AuthState((registrationResponse)));
				}
			}
			catch (AuthorizationException ex)
			{
				Console.WriteLine("Registration request had an error: " + ex);
			}
		}
	}
}
