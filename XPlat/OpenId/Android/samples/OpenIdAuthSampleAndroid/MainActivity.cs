using System;
using Android.App;
using Android.Graphics;
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
		private AuthorizationService mAuthService;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			mAuthService = new AuthorizationService(this);
			var idpButtonContainer = FindViewById<ViewGroup>(Resource.Id.idp_button_container);
			var providers = IdentityProvider.GetEnabledProviders(this);

			FindViewById(Resource.Id.sign_in_container).Visibility = providers.Count == 0 ? ViewStates.Gone : ViewStates.Visible;
			FindViewById(Resource.Id.no_idps_configured).Visibility = providers.Count == 0 ? ViewStates.Visible : ViewStates.Gone;

			foreach (var idp in providers)
			{
				var idpButton = new FrameLayout(this);
				idpButton.SetBackgroundResource(idp.buttonImageRes);
				idpButton.ContentDescription = Resources.GetString(idp.buttonContentDescriptionRes);
				idpButton.LayoutParameters = new LinearLayout.LayoutParams(
					LinearLayout.LayoutParams.MatchParent,
					LinearLayout.LayoutParams.WrapContent);
				idpButton.Click += async delegate
				{
					Console.WriteLine("initiating auth for " + idp.name);

					try
					{
						var serviceConfiguration = await idp.retrieveConfigAsync(this);

						Console.WriteLine("$configuration retrieved for {idp.name}, proceeding");
						if (idp.getClientId() == null)
						{
							// Do dynamic client registration if no client_id
							makeRegistrationRequest(serviceConfiguration, idp);
						}
						else {
							makeAuthRequest(serviceConfiguration, idp, new AuthState());
						}
					}
					catch (AuthorizationException ex)
					{
						Console.WriteLine("Failed to retrieve configuration for " + idp.name, ex);
					}
				};

				var label = new TextView(this);
				label.Text = idp.name;
				label.SetTextColor(getColorCompat(idp.buttonTextColorRes));
				label.LayoutParameters = new FrameLayout.LayoutParams(
					FrameLayout.LayoutParams.WrapContent,
					FrameLayout.LayoutParams.WrapContent,
					GravityFlags.Center);
				idpButton.AddView(label);

				idpButtonContainer.AddView(idpButton);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			mAuthService.Dispose();
		}

		private void makeAuthRequest(AuthorizationServiceConfiguration serviceConfig, IdentityProvider idp, AuthState authState)
		{
			var authRequest = new AuthorizationRequest.Builder(serviceConfig, idp.getClientId(), ResponseTypeValues.Code, idp.getRedirectUri())
				.SetScope(idp.getScope())
				.Build();

			Console.WriteLine("Making auth request to " + serviceConfig.AuthorizationEndpoint);
			mAuthService.PerformAuthorizationRequest(
				authRequest,
				TokenActivity.createPostAuthorizationIntent(this, authRequest, serviceConfig.DiscoveryDoc, authState),
				mAuthService.CreateCustomTabsIntentBuilder().SetToolbarColor(getColorCompat(Resource.Color.colorAccent)).Build());
		}

		private async void makeRegistrationRequest(AuthorizationServiceConfiguration serviceConfig, IdentityProvider idp)
		{
			var registrationRequest = new RegistrationRequest.Builder(serviceConfig, new[] { idp.getRedirectUri() })
				.SetTokenEndpointAuthenticationMethod(ClientSecretBasic.Name)
				.Build();

			Console.WriteLine("Making registration request to " + serviceConfig.RegistrationEndpoint);

			try
			{
				var registrationResponse = await mAuthService.PerformRegistrationRequestAsync(registrationRequest);
				Console.WriteLine("Registration request complete");

				if (registrationResponse != null)
				{
					idp.setClientId(registrationResponse.ClientId);
					Console.WriteLine("Registration request complete successfully");
					// Continue with the authentication
					makeAuthRequest(registrationResponse.Request.Configuration, idp, new AuthState((registrationResponse)));
				}
			}
			catch (AuthorizationException ex)
			{
				Console.WriteLine("Registration request had an error: " + ex);
			}
		}

		private Color getColorCompat(int color)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
			{
				return new Color(GetColor(color));
			}
			else
			{
				return Resources.GetColor(color);
			}
		}
	}
}
