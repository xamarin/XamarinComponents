using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Text;
using Java.Util;
using Org.Json;

using OpenId.AppAuth;

namespace OpenIdAuthSampleAndroid
{
	[Activity(
		Label = "@string/app_name_short",
		MainLauncher = true,
		Theme = "@style/AppTheme.NoActionBar",
		WindowSoftInputMode = SoftInput.StateHidden)]
	public class TokenActivity : AppCompatActivity
	{
		private static string KEY_AUTH_STATE = "authState";
		private static string KEY_USER_INFO = "userInfo";

		private static string EXTRA_AUTH_SERVICE_DISCOVERY = "authServiceDiscovery";
		private static string EXTRA_AUTH_STATE = "authState";

		private AuthState authState;
		private AuthorizationService authService;
		private JSONObject userInfoJson;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_token);

			authService = new AuthorizationService(this);

			if (savedInstanceState != null)
			{
				if (savedInstanceState.ContainsKey(KEY_AUTH_STATE))
				{
					try
					{
						authState = AuthState.JsonDeserialize(savedInstanceState.GetString(KEY_AUTH_STATE));
					}
					catch (JSONException ex)
					{
						Console.WriteLine("Malformed authorization JSON saved: " + ex);
					}
				}

				if (savedInstanceState.ContainsKey(KEY_USER_INFO))
				{
					try
					{
						userInfoJson = new JSONObject(savedInstanceState.GetString(KEY_USER_INFO));
					}
					catch (JSONException ex)
					{
						Console.WriteLine("Failed to parse saved user info JSON: " + ex);
					}
				}
			}

			if (authState == null)
			{
				authState = GetAuthStateFromIntent(Intent);
				AuthorizationResponse response = AuthorizationResponse.FromIntent(Intent);
				AuthorizationException ex = AuthorizationException.FromIntent(Intent);
				authState.Update(response, ex);

				if (response != null)
				{
					Console.WriteLine("Received AuthorizationResponse.");
					PerformTokenRequest(response.CreateTokenExchangeRequest());
				}
				else
				{
					Console.WriteLine("Authorization failed: " + ex);
				}
			}

			RefreshUi();

			var refreshTokenButton = FindViewById<Button>(Resource.Id.refresh_token);
			refreshTokenButton.Click += delegate
			{
				PerformTokenRequest(authState.CreateTokenRefreshRequest());
			};

			var viewProfileButton = FindViewById<Button>(Resource.Id.view_profile);
			viewProfileButton.Click += delegate
			{
				Task.Run(() => FetchUserInfo());
			};
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			if (authState != null)
			{
				outState.PutString(KEY_AUTH_STATE, authState.JsonSerializeString());
			}

			if (userInfoJson != null)
			{
				outState.PutString(KEY_USER_INFO, userInfoJson.ToString());
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			authService.Dispose();
		}

		private void ReceivedTokenResponse(TokenResponse tokenResponse, AuthorizationException authException)
		{
			Console.WriteLine("Token request complete");
			authState.Update(tokenResponse, authException);
			RefreshUi();
		}

		private void RefreshUi()
		{
			var refreshTokenInfoView = FindViewById<TextView>(Resource.Id.refresh_token_info);
			var accessTokenInfoView = FindViewById<TextView>(Resource.Id.access_token_info);
			var idTokenInfoView = FindViewById<TextView>(Resource.Id.id_token_info);
			var refreshTokenButton = FindViewById<Button>(Resource.Id.refresh_token);

			if (authState.IsAuthorized)
			{
				refreshTokenInfoView.SetText(authState.RefreshToken == null ? Resource.String.no_refresh_token_returned : Resource.String.refresh_token_returned);
				idTokenInfoView.SetText(authState.IdToken == null ? Resource.String.no_id_token_returned : Resource.String.id_token_returned);

				if (authState.AccessToken == null)
				{
					accessTokenInfoView.SetText(Resource.String.no_access_token_returned);
				}
				else
				{
					var expiresAt = authState.AccessTokenExpirationTime;
					string expiryStr;
					if (expiresAt == null)
					{
						expiryStr = Resources.GetString(Resource.String.unknown_expiry);
					}
					else {
						expiryStr = DateFormat.GetDateTimeInstance(DateFormat.Full, DateFormat.Full).Format(new Date(expiresAt.LongValue()));
					}
					var tokenInfo = string.Format(Resources.GetString(Resource.String.access_token_expires_at), expiryStr);
					accessTokenInfoView.Text = tokenInfo;
				}
			}

			refreshTokenButton.Visibility = authState.RefreshToken != null ? ViewStates.Visible : ViewStates.Gone;

			var viewProfileButton = FindViewById<Button>(Resource.Id.view_profile);

			AuthorizationServiceDiscovery discoveryDoc = GetDiscoveryDocFromIntent(Intent);
			if (!authState.IsAuthorized || discoveryDoc == null || discoveryDoc.UserinfoEndpoint == null)
			{
				viewProfileButton.Visibility = ViewStates.Gone;
			}
			else
			{
				viewProfileButton.Visibility = ViewStates.Visible;
			}

			var userInfoCard = FindViewById(Resource.Id.userinfo_card);
			if (userInfoJson == null)
			{
				userInfoCard.Visibility = ViewStates.Invisible;
			}
			else
			{
				try
				{
					string name = "???";
					if (userInfoJson.Has("name"))
					{
						name = userInfoJson.GetString("name");
					}
					FindViewById<TextView>(Resource.Id.userinfo_name).Text = name;

					FindViewById<TextView>(Resource.Id.userinfo_json).Text = userInfoJson.ToString(2);

					userInfoCard.Visibility = ViewStates.Visible;
				}
				catch (JSONException ex)
				{
					Console.WriteLine("Failed to read userinfo JSON: " + ex);
				}
			}
		}

		private void PerformTokenRequest(TokenRequest request)
		{
			IClientAuthentication clientAuthentication;
			try
			{
				clientAuthentication = authState.ClientAuthentication;
			}
			catch (ClientAuthenticationUnsupportedAuthenticationMethod ex)
			{
				Console.WriteLine("Token request cannot be made, client authentication for the token endpoint could not be constructed: " + ex);
				return;
			}

			authService.PerformTokenRequest(request, ReceivedTokenResponse);
		}

		private void FetchUserInfo()
		{
			if (authState.AuthorizationServiceConfiguration == null)
			{
				Console.WriteLine("Cannot make userInfo request without service configuration");
			}

			authState.PerformActionWithFreshTokens(authService, async (accessToken, idToken, ex) =>
			{
				if (ex != null)
				{
					Console.WriteLine("Token refresh failed when fetching user info");
					return;
				}

				AuthorizationServiceDiscovery discoveryDoc = GetDiscoveryDocFromIntent(Intent);
				if (discoveryDoc == null)
				{
					throw new InvalidOperationException("no available discovery doc");
				}

				try
				{
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
						var response = await client.GetStringAsync(discoveryDoc.UserinfoEndpoint.ToString());

						new Handler(MainLooper).Post(() =>
						{
							userInfoJson = new JSONObject(response);
							RefreshUi();
						});
					}
				}
				catch (HttpRequestException ioEx)
				{
					Console.WriteLine("Network error when querying userinfo endpoint:" + ioEx);
				}
				catch (JSONException jsonEx)
				{
					Console.WriteLine("Failed to parse userinfo response: " + jsonEx);
				}
			});
		}

		public static PendingIntent CreatePostAuthorizationIntent(Context context, AuthorizationRequest request, AuthorizationServiceDiscovery discoveryDoc, AuthState authState)
		{
			var intent = new Intent(context, typeof(TokenActivity));
			intent.PutExtra(EXTRA_AUTH_STATE, authState.JsonSerializeString());
			if (discoveryDoc != null)
			{
				intent.PutExtra(EXTRA_AUTH_SERVICE_DISCOVERY, discoveryDoc.DocJson.ToString());
			}

			return PendingIntent.GetActivity(context, request.GetHashCode(), intent, 0);
		}

		private static AuthorizationServiceDiscovery GetDiscoveryDocFromIntent(Intent intent)
		{
			if (!intent.HasExtra(EXTRA_AUTH_SERVICE_DISCOVERY))
			{
				return null;
			}

			var discoveryJson = intent.GetStringExtra(EXTRA_AUTH_SERVICE_DISCOVERY);
			try
			{
				return new AuthorizationServiceDiscovery(new JSONObject(discoveryJson));
			}
			catch (JSONException ex)
			{
				throw new InvalidOperationException("Malformed JSON in discovery doc", ex);
			}
			catch (AuthorizationServiceDiscovery.MissingArgumentException ex)
			{
				throw new InvalidOperationException("Malformed JSON in discovery doc", ex);
			}
		}

		private static AuthState GetAuthStateFromIntent(Intent intent)
		{
			if (!intent.HasExtra(EXTRA_AUTH_STATE))
			{
				throw new InvalidOperationException("The AuthState instance is missing in the intent.");
			}

			try
			{
				return AuthState.JsonDeserialize(intent.GetStringExtra(EXTRA_AUTH_STATE));
			}
			catch (JSONException ex)
			{
				Console.WriteLine("Malformed AuthState JSON saved: " + ex);
				throw new InvalidOperationException("The AuthState instance is missing in the intent.", ex);
			}
		}
	}
}
