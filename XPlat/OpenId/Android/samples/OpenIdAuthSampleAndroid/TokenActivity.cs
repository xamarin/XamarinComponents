using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
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

		private static int BUFFER_SIZE = 1024;

		private AuthState mAuthState;
		private AuthorizationService mAuthService;
		private JSONObject mUserInfoJson;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_token);

			mAuthService = new AuthorizationService(this);

			if (savedInstanceState != null)
			{
				if (savedInstanceState.ContainsKey(KEY_AUTH_STATE))
				{
					try
					{
						mAuthState = AuthState.JsonDeserialize(savedInstanceState.GetString(KEY_AUTH_STATE));
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
						mUserInfoJson = new JSONObject(savedInstanceState.GetString(KEY_USER_INFO));
					}
					catch (JSONException ex)
					{
						Console.WriteLine("Failed to parse saved user info JSON: " + ex);
					}
				}
			}

			if (mAuthState == null)
			{
				mAuthState = getAuthStateFromIntent(Intent);
				AuthorizationResponse response = AuthorizationResponse.FromIntent(Intent);
				AuthorizationException ex = AuthorizationException.FromIntent(Intent);
				mAuthState.Update(response, ex);

				if (response != null)
				{
					Console.WriteLine("Received AuthorizationResponse.");
					showSnackbar(Resource.String.exchange_notification);
					exchangeAuthorizationCode(response);
				}
				else {
					Console.WriteLine("Authorization failed: " + ex);
					showSnackbar(Resource.String.authorization_failed);
				}
			}

			refreshUi();

			var refreshTokenButton = FindViewById<Button>(Resource.Id.refresh_token);
			refreshTokenButton.Click += delegate
			{
				refreshAccessToken();
			};

			var viewProfileButton = FindViewById<Button>(Resource.Id.view_profile);
			viewProfileButton.Click += delegate
			{
				Task.Run(() => fetchUserInfo());
			};
		}

		protected override void OnSaveInstanceState(Bundle state)
		{
			if (mAuthState != null)
			{
				state.PutString(KEY_AUTH_STATE, mAuthState.JsonSerializeString());
			}

			if (mUserInfoJson != null)
			{
				state.PutString(KEY_USER_INFO, mUserInfoJson.ToString());
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			mAuthService.Dispose();
		}

		private void receivedTokenResponse(TokenResponse tokenResponse, AuthorizationException authException)
		{
			Console.WriteLine("Token request complete");
			mAuthState.Update(tokenResponse, authException);
			showSnackbar(tokenResponse != null ? Resource.String.exchange_complete : Resource.String.refresh_failed);
			refreshUi();
		}

		private void refreshUi()
		{
			var refreshTokenInfoView = FindViewById<TextView>(Resource.Id.refresh_token_info);
			var accessTokenInfoView = FindViewById<TextView>(Resource.Id.access_token_info);
			var idTokenInfoView = FindViewById<TextView>(Resource.Id.id_token_info);
			var refreshTokenButton = FindViewById<Button>(Resource.Id.refresh_token);

			if (mAuthState.IsAuthorized)
			{
				refreshTokenInfoView.SetText(mAuthState.RefreshToken == null ? Resource.String.no_refresh_token_returned : Resource.String.refresh_token_returned);
				idTokenInfoView.SetText(mAuthState.IdToken == null ? Resource.String.no_id_token_returned : Resource.String.id_token_returned);

				if (mAuthState.AccessToken == null)
				{
					accessTokenInfoView.SetText(Resource.String.no_access_token_returned);
				}
				else
				{
					var expiresAt = mAuthState.AccessTokenExpirationTime;
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

			refreshTokenButton.Visibility = mAuthState.RefreshToken != null ? ViewStates.Visible : ViewStates.Gone;

			var viewProfileButton = FindViewById<Button>(Resource.Id.view_profile);

			AuthorizationServiceDiscovery discoveryDoc = getDiscoveryDocFromIntent(Intent);
			if (!mAuthState.IsAuthorized || discoveryDoc == null || discoveryDoc.UserinfoEndpoint == null)
			{
				viewProfileButton.Visibility = ViewStates.Gone;
			}
			else
			{
				viewProfileButton.Visibility = ViewStates.Visible;
			}

			var userInfoCard = FindViewById(Resource.Id.userinfo_card);
			if (mUserInfoJson == null)
			{
				userInfoCard.Visibility = ViewStates.Invisible;
			}
			else
			{
				try
				{
					string name = "???";
					if (mUserInfoJson.Has("name"))
					{
						name = mUserInfoJson.GetString("name");
					}
					FindViewById<TextView>(Resource.Id.userinfo_name).Text = name;

					if (mUserInfoJson.Has("picture"))
					{
						var uri = Android.Net.Uri.Parse(mUserInfoJson.GetString("picture"));
						FindViewById<ImageView>(Resource.Id.userinfo_profile).SetImageURI(uri);
					}

					FindViewById<TextView>(Resource.Id.userinfo_json).Text = mUserInfoJson.ToString(2);

					userInfoCard.Visibility = ViewStates.Visible;
				}
				catch (JSONException ex)
				{
					Console.WriteLine("Failed to read userinfo JSON: " + ex);
				}
			}
		}

		private void refreshAccessToken()
		{
			performTokenRequest(mAuthState.CreateTokenRefreshRequest());
		}

		private void exchangeAuthorizationCode(AuthorizationResponse authorizationResponse)
		{
			performTokenRequest(authorizationResponse.CreateTokenExchangeRequest());
		}

		private async void performTokenRequest(TokenRequest request)
		{
			IClientAuthentication clientAuthentication;
			try
			{
				clientAuthentication = mAuthState.ClientAuthentication;
			}
			catch (ClientAuthenticationUnsupportedAuthenticationMethod ex)
			{
				Console.WriteLine("Token request cannot be made, client authentication for the token endpoint could not be constructed: " + ex);
				return;
			}

			mAuthService.PerformTokenRequest(request, receivedTokenResponse);
		}

		private void fetchUserInfo()
		{
			if (mAuthState.AuthorizationServiceConfiguration == null)
			{
				Console.WriteLine("Cannot make userInfo request without service configuration");
			}

			mAuthState.PerformActionWithFreshTokens(mAuthService, async (accessToken, idToken, ex) =>
			{
				if (ex != null)
				{
					Console.WriteLine("Token refresh failed when fetching user info");
					return;
				}

				AuthorizationServiceDiscovery discoveryDoc = getDiscoveryDocFromIntent(Intent);
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
							mUserInfoJson = new JSONObject(response);
							refreshUi();
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

		private void showSnackbar(int messageId)
		{
			Snackbar.Make(FindViewById(Resource.Id.coordinator), Resources.GetString(messageId), Snackbar.LengthShort).Show();
		}

		public static PendingIntent createPostAuthorizationIntent(Context context, AuthorizationRequest request, AuthorizationServiceDiscovery discoveryDoc, AuthState authState)
		{
			var intent = new Intent(context, typeof(TokenActivity));
			intent.PutExtra(EXTRA_AUTH_STATE, authState.JsonSerializeString());
			if (discoveryDoc != null)
			{
				intent.PutExtra(EXTRA_AUTH_SERVICE_DISCOVERY, discoveryDoc.DocJson.ToString());
			}

			return PendingIntent.GetActivity(context, request.GetHashCode(), intent, 0);
		}

		private static AuthorizationServiceDiscovery getDiscoveryDocFromIntent(Intent intent)
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

		private static AuthState getAuthStateFromIntent(Intent intent)
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
				Console.WriteLine("Malformed AuthState JSON saved", ex);
				throw new InvalidOperationException("The AuthState instance is missing in the intent.", ex);
			}
		}
	}
}
