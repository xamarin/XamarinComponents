using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Foundation;
using UIKit;

using OpenId.AppAuth;

namespace OpenIdAuthSampleiOS
{
	public partial class ViewController : UIViewController, IOIDAuthStateChangeDelegate, IOIDAuthStateErrorDelegate
	{
		// The authorization state. This is the AppAuth object that you should keep around and
		// serialize to disk.
		private OIDAuthState _authState;
		public OIDAuthState authState
		{
			get { return _authState; }
			set
			{
				if (_authState != value)
				{
					_authState = value;
					if (_authState != null)
					{
						_authState.WeakStateChangeDelegate = this;
					}
					stateChanged();
				}
			}
		}

		// The OIDC issuer from which the configuration will be discovered.
		public const string kIssuer = @"https://accounts.google.com";

		// The OAuth client ID.
		public const string kClientID = "60322915503-t6s4kgg8jf7bfos910agh9qb9fa5jvju.apps.googleusercontent.com";

		// The OAuth redirect URI for the client kClientID.
		public const string kRedirectURI = "com.googleusercontent.apps.60322915503-t6s4kgg8jf7bfos910agh9qb9fa5jvju:/oauthredirect";

		// NSCoding key for the authState property.
		public static NSString kAppAuthExampleAuthStateKey = (NSString)"authState";

		protected ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			loadState();
			updateUI();
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
				var configuration = await OIDAuthorizationService.DiscoverServiceConfigurationForIssuerAsync(issuer);

				Console.WriteLine($"Got configuration: {configuration}");

				// builds authentication request
				var request = new OIDAuthorizationRequest(configuration, kClientID, new string[] { OIDScope.OpenID, OIDScope.Profile }, redirectURI, OIDResponseType.Code, null);
				// performs authentication request
				var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				Console.WriteLine($"Initiating authorization request with scope: {request.Scope}");

				appDelegate.CurrentAuthorizationFlow = OIDAuthState.PresentAuthorizationRequest(request, this, (authState, error) =>
				{
					if (authState != null)
					{
						this.authState = authState;
						Console.WriteLine($"Got authorization tokens. Access token: {authState.LastTokenResponse.AccessToken}");
					}
					else {
						Console.WriteLine($"Authorization error: {error.LocalizedDescription}");
						this.authState = null;
					}
				});
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error retrieving discovery document: {ex}");
				authState = null;
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
				var configuration = await OIDAuthorizationService.DiscoverServiceConfigurationForIssuerAsync(issuer);

				Console.WriteLine($"Got configuration: {configuration}");

				// builds authentication request
				OIDAuthorizationRequest request = new OIDAuthorizationRequest(configuration, kClientID, new string[] { OIDScope.OpenID, OIDScope.Profile }, redirectURI, OIDResponseType.Code, null);
				// performs authentication request
				var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				Console.WriteLine($"Initiating authorization request: {request}");
				appDelegate.CurrentAuthorizationFlow = OIDAuthorizationService.PresentAuthorizationRequest(request, this, (authorizationResponse, error) =>
				{
					if (authorizationResponse != null)
					{
						OIDAuthState authState = new OIDAuthState(authorizationResponse);
						this.authState = authState;

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
				authState = null;
			}
		}

		// Performs the authorization code exchange at the token endpoint.
		async partial void CodeExchange(UIButton sender)
		{
			// performs code exchange request
			OIDTokenRequest tokenExchangeRequest = authState.LastAuthorizationResponse.CreateTokenExchangeRequest();

			Console.WriteLine($"Performing authorization code exchange with request: {tokenExchangeRequest}");

			try
			{
				var tokenResponse = await OIDAuthorizationService.PerformTokenRequestAsync(tokenExchangeRequest);
				Console.WriteLine($"Received token response with accessToken: {tokenResponse.AccessToken}");

				authState.Update(tokenResponse, null);
			}
			catch (NSErrorException ex)
			{
				authState.Update(ex.Error);

				Console.WriteLine($"Token exchange error: {ex}");
				authState = null;
			}
		}

		// Performs a Userinfo API call using OIDAuthState.withFreshTokensPerformAction.
		partial void Userinfo(UIButton sender)
		{
			var userinfoEndpoint = authState.LastAuthorizationResponse.Request.Configuration.DiscoveryDocument.UserinfoEndpoint;
			if (userinfoEndpoint == null)
			{
				Console.WriteLine($"Userinfo endpoint not declared in discovery document");
				return;
			}

			var currentAccessToken = authState.LastTokenResponse.AccessToken;

			Console.WriteLine($"Performing userinfo request");

			authState.PerformWithFreshTokens(async (accessToken, idToken, error) =>
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
						var authError = OIDErrorUtilities.CreateResourceServerAuthorizationError(0, data, error);
						authState.Update(authError);
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
			authState = null;
		}

		// Saves the OIDAuthState to NSUSerDefaults.
		private void saveState()
		{
			// for production usage consider using the OS Keychain instead
			if (authState != null)
			{
				var archivedAuthState = NSKeyedArchiver.ArchivedDataWithRootObject(authState);
				NSUserDefaults.StandardUserDefaults[kAppAuthExampleAuthStateKey] = archivedAuthState;
			}
			else
			{
				NSUserDefaults.StandardUserDefaults.RemoveObject(kAppAuthExampleAuthStateKey);
			}
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		// Loads the OIDAuthState from NSUSerDefaults.
		private void loadState()
		{
			// loads OIDAuthState from NSUSerDefaults
			var archivedAuthState = (NSData)NSUserDefaults.StandardUserDefaults[kAppAuthExampleAuthStateKey];
			if (archivedAuthState != null)
			{
				authState = (OIDAuthState)NSKeyedUnarchiver.UnarchiveObject(archivedAuthState);
			}
		}

		// Refreshes UI, typically called after the auth state changed
		private void updateUI()
		{
			userinfoButton.Enabled = authState?.IsAuthorized == true;
			clearAuthStateButton.Enabled = authState != null;
			codeExchangeButton.Enabled = authState?.LastAuthorizationResponse?.AuthorizationCode != null && authState?.LastTokenResponse == null;

			// dynamically changes authorize button text depending on authorized state
			if (authState == null)
			{
				authAutoButton.SetTitle("Authorize", UIControlState.Normal);
				authAutoButton.SetTitle("Authorize", UIControlState.Highlighted);
				authManual.SetTitle("Authorize (Manual)", UIControlState.Normal);
				authManual.SetTitle("Authorize (Manual)", UIControlState.Highlighted);
			}
			else {
				authAutoButton.SetTitle("Re-authorize", UIControlState.Normal);
				authAutoButton.SetTitle("Re-authorize", UIControlState.Highlighted);
				authManual.SetTitle("Re-authorize (Manual)", UIControlState.Normal);
				authManual.SetTitle("Re-authorize (Manual)", UIControlState.Highlighted);
			}
		}

		private void stateChanged()
		{
			saveState();
			updateUI();
		}

		void IOIDAuthStateChangeDelegate.DidChangeState(OIDAuthState state)
		{
			stateChanged();
		}

		void IOIDAuthStateErrorDelegate.DidEncounterAuthorizationError(OIDAuthState state, NSError error)
		{
			Console.WriteLine($"Received authorization error: {error}.");
		}
	}
}
