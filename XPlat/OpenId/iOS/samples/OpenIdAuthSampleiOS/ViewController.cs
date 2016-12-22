using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Foundation;
using UIKit;

using OpenId.AppAuth;

namespace OpenIdAuthSampleiOS
{
	public partial class ViewController : UIViewController, IAuthStateChangeDelegate, IAuthStateErrorDelegate
	{
		// The authorization state. This is the AppAuth object that you should keep around and
		// serialize to disk.
		private AuthState _authState;
		public AuthState AuthState
		{
			get { return _authState; }
			set
			{
				if (_authState != value)
				{
					_authState = value;
					if (_authState != null)
					{
						_authState.StateChangeDelegate = this;
					}
					StateChanged();
				}
			}
		}

		// The OIDC issuer from which the configuration will be discovered.
		public const string kIssuer = @"https://accounts.google.com";

		// The OAuth client ID.
		public const string kClientID = "YOUR_APP_ID.apps.googleusercontent.com";

		// The OAuth redirect URI for the client kClientID.
		public const string kRedirectURI = "com.googleusercontent.apps.YOUR_APP_ID:/oauthredirect";

		// NSCoding key for the authState property.
		public static NSString kAppAuthExampleAuthStateKey = (NSString)"authState";

		protected ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			LoadState();
			UpdateUi();
		}

		// Authorization code flow using OIDAuthState automatic code exchanges.
		async partial void AuthWithAutoCodeExchange(UIButton sender)
		{
			var issuer = new NSUrl(kIssuer);
			var redirectURI = new NSUrl(kRedirectURI);

			Console.WriteLine($"Fetching configuration for issuer: {issuer}");

			try
			{
				// discovers endpoints
				var configuration = await AuthorizationService.DiscoverServiceConfigurationForIssuerAsync(issuer);

				Console.WriteLine($"Got configuration: {configuration}");

				// builds authentication request
				var request = new AuthorizationRequest(configuration, kClientID, new string[] { Scope.OpenId, Scope.Profile }, redirectURI, ResponseType.Code, null);
				// performs authentication request
				var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				Console.WriteLine($"Initiating authorization request with scope: {request.Scope}");

				appDelegate.CurrentAuthorizationFlow = AuthState.PresentAuthorizationRequest(request, this, (authState, error) =>
				{
					if (authState != null)
					{
						AuthState = authState;
						Console.WriteLine($"Got authorization tokens. Access token: {authState.LastTokenResponse.AccessToken}");
					}
					else {
						Console.WriteLine($"Authorization error: {error.LocalizedDescription}");
						AuthState = null;
					}
				});
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error retrieving discovery document: {ex}");
				AuthState = null;
			}
		}

		// Authorization code flow without a the code exchange (need to call codeExchange manually)
		async partial void AuthNoCodeExchange(UIButton sender)
		{
			var issuer = new NSUrl(kIssuer);
			var redirectURI = new NSUrl(kRedirectURI);

			Console.WriteLine($"Fetching configuration for issuer: {issuer}");

			try
			{
				// discovers endpoints
				var configuration = await AuthorizationService.DiscoverServiceConfigurationForIssuerAsync(issuer);

				Console.WriteLine($"Got configuration: {configuration}");

				// builds authentication request
				AuthorizationRequest request = new AuthorizationRequest(configuration, kClientID, new string[] { Scope.OpenId, Scope.Profile }, redirectURI, ResponseType.Code, null);
				// performs authentication request
				var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				Console.WriteLine($"Initiating authorization request: {request}");
				appDelegate.CurrentAuthorizationFlow = AuthorizationService.PresentAuthorizationRequest(request, this, (authorizationResponse, error) =>
				{
					if (authorizationResponse != null)
					{
						AuthState authState = new AuthState(authorizationResponse);
						AuthState = authState;

						Console.WriteLine($"Authorization response with code: {authorizationResponse.AuthorizationCode}");
						// could just call [self tokenExchange:nil] directly, but will let the user initiate it.
					}
					else
					{
						Console.WriteLine($"Authorization error: {error.LocalizedDescription }");
					}
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving discovery document: {ex}");
				AuthState = null;
			}
		}

		// Performs the authorization code exchange at the token endpoint.
		async partial void CodeExchange(UIButton sender)
		{
			// performs code exchange request
			TokenRequest tokenExchangeRequest = AuthState.LastAuthorizationResponse.CreateTokenExchangeRequest();

			Console.WriteLine($"Performing authorization code exchange with request: {tokenExchangeRequest}");

			try
			{
				var tokenResponse = await AuthorizationService.PerformTokenRequestAsync(tokenExchangeRequest);
				Console.WriteLine($"Received token response with accessToken: {tokenResponse.AccessToken}");

				AuthState.Update(tokenResponse, null);
			}
			catch (NSErrorException ex)
			{
				AuthState.Update(ex.Error);

				Console.WriteLine($"Token exchange error: {ex}");
				AuthState = null;
			}
		}

		// Performs a Userinfo API call using OIDAuthState.withFreshTokensPerformAction.
		partial void Userinfo(UIButton sender)
		{
			var userinfoEndpoint = AuthState.LastAuthorizationResponse.Request.Configuration.DiscoveryDocument.UserinfoEndpoint;
			if (userinfoEndpoint == null)
			{
				Console.WriteLine($"Userinfo endpoint not declared in discovery document");
				return;
			}

			var currentAccessToken = AuthState.LastTokenResponse.AccessToken;

			Console.WriteLine($"Performing userinfo request");

			AuthState.PerformWithFreshTokens(async (accessToken, idToken, error) =>
			{
				if (error != null)
				{
					Console.WriteLine($"Error fetching fresh tokens: {error.LocalizedDescription}");
					return;
				}

				// log whether a token refresh occurred
				if (currentAccessToken != accessToken)
				{
					Console.WriteLine($"Access token was refreshed automatically ({currentAccessToken} to {accessToken})");
				}
				else {
					Console.WriteLine($"Access token was fresh and not updated {accessToken}");
				}

				// creates request to the userinfo endpoint, with access token in the Authorization header
				var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

				// performs HTTP request
				var response = await httpClient.GetAsync(userinfoEndpoint);
				var content = await response.Content.ReadAsStringAsync();
				NSError deserializeError;
				var data = (NSDictionary)NSJsonSerialization.Deserialize(NSData.FromString(content), 0, out deserializeError);

				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine($"Success: {content}");

					new UIAlertView("OpenID AppAuth", $"Hello, {data["name"]}!", null, "Hi").Show();
				}
				else
				{
					// server replied with an error

					if (response.StatusCode == HttpStatusCode.Unauthorized)
					{
						// "401 Unauthorized" generally indicates there is an issue with the authorization
						// grant. Puts OIDAuthState into an error state.
						var authError = ErrorUtilities.CreateResourceServerAuthorizationError(0, data, error);
						AuthState.Update(authError);
						// log error
						Console.WriteLine($"Authorization Error ({authError}). Response: {content}");
					}
					else
					{
						// log error
						Console.WriteLine($"HTTP Error ({response.StatusCode}). Response: {content}");
					}
				}
			});
		}

		// Nils the OIDAuthState object.
		partial void ClearAuthState(UIButton sender)
		{
			AuthState = null;
		}

		// Saves the OIDAuthState to NSUSerDefaults.
		private void SaveState()
		{
			// for production usage consider using the OS Keychain instead
			if (AuthState != null)
			{
				var archivedAuthState = NSKeyedArchiver.ArchivedDataWithRootObject(AuthState);
				NSUserDefaults.StandardUserDefaults[kAppAuthExampleAuthStateKey] = archivedAuthState;
			}
			else
			{
				NSUserDefaults.StandardUserDefaults.RemoveObject(kAppAuthExampleAuthStateKey);
			}
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		// Loads the OIDAuthState from NSUSerDefaults.
		private void LoadState()
		{
			// loads OIDAuthState from NSUSerDefaults
			var archivedAuthState = (NSData)NSUserDefaults.StandardUserDefaults[kAppAuthExampleAuthStateKey];
			if (archivedAuthState != null)
			{
				AuthState = (AuthState)NSKeyedUnarchiver.UnarchiveObject(archivedAuthState);
			}
		}

		// Refreshes UI, typically called after the auth state changed
		private void UpdateUi()
		{
			userinfoButton.Enabled = AuthState?.IsAuthorized == true;
			clearAuthStateButton.Enabled = AuthState != null;
			codeExchangeButton.Enabled = AuthState?.LastAuthorizationResponse?.AuthorizationCode != null && AuthState?.LastTokenResponse == null;

			// dynamically changes authorize button text depending on authorized state
			if (AuthState == null)
			{
				authAutoButton.SetTitle("Authorize", UIControlState.Normal);
				authAutoButton.SetTitle("Authorize", UIControlState.Highlighted);
				authManual.SetTitle("Authorize (Manual)", UIControlState.Normal);
				authManual.SetTitle("Authorize (Manual)", UIControlState.Highlighted);
			}
			else
			{
				authAutoButton.SetTitle("Re-authorize", UIControlState.Normal);
				authAutoButton.SetTitle("Re-authorize", UIControlState.Highlighted);
				authManual.SetTitle("Re-authorize (Manual)", UIControlState.Normal);
				authManual.SetTitle("Re-authorize (Manual)", UIControlState.Highlighted);
			}
		}

		private void StateChanged()
		{
			SaveState();
			UpdateUi();
		}

		void IAuthStateChangeDelegate.DidChangeState(AuthState state)
		{
			StateChanged();
		}

		void IAuthStateErrorDelegate.DidEncounterAuthorizationError(AuthState state, NSError error)
		{
			Console.WriteLine($"Received authorization error: {error}.");
		}
	}
}
