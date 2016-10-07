using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Net;

using OpenId.AppAuth;

namespace OpenIdAuthSampleAndroid
{
	public class IdentityProvider
	{
		// Value used to indicate that a configured property is not specified or required.
		public static int NOT_SPECIFIED = -1;

		public static IdentityProvider GOOGLE = new IdentityProvider(
			"Google",
			Resource.Boolean.google_enabled,
			Resource.String.google_discovery_uri,
			NOT_SPECIFIED, // auth endpoint is discovered
			NOT_SPECIFIED, // token endpoint is discovered
			NOT_SPECIFIED, // dynamic registration not supported
			Resource.String.google_client_id,
			Resource.String.google_auth_redirect_uri,
			Resource.String.google_scope_string,
			Resource.Drawable.btn_google,
			Resource.String.google_name,
			Android.Resource.Color.White);

		public static List<IdentityProvider> PROVIDERS = new List<IdentityProvider> { GOOGLE };

		public static List<IdentityProvider> GetEnabledProviders(Context context)
		{
			var providers = new List<IdentityProvider>();
			foreach (IdentityProvider provider in PROVIDERS)
			{
				provider.readConfiguration(context);
				if (provider.isEnabled())
				{
					providers.Add(provider);
				}
			}
			return providers;
		}

		public string name;
		public int buttonImageRes;
		public int buttonContentDescriptionRes;
		public int buttonTextColorRes;

		private int mEnabledRes;
		private int mDiscoveryEndpointRes;
		private int mAuthEndpointRes;
		private int mTokenEndpointRes;
		private int mRegistrationEndpointRes;
		private int mClientIdRes;
		private int mRedirectUriRes;
		private int mScopeRes;

		private bool mConfigurationRead = false;
		private bool mEnabled;
		private Uri mDiscoveryEndpoint;
		private Uri mAuthEndpoint;
		private Uri mTokenEndpoint;
		private Uri mRegistrationEndpoint;
		private string mClientId;
		private Uri mRedirectUri;
		private string mScope;

		private IdentityProvider(
			string name,
			int enabledRes,
			int discoveryEndpointRes,
			int authEndpointRes,
			int tokenEndpointRes,
			int registrationEndpointRes,
			int clientIdRes,
			int redirectUriRes,
			int scopeRes,
			int buttonImageRes,
			int buttonContentDescriptionRes,
			int buttonTextColorRes)
		{
			if (!isSpecified(discoveryEndpointRes) && !isSpecified(authEndpointRes) && !isSpecified(tokenEndpointRes))
			{
				throw new System.ArgumentException("the discovery endpoint or the auth and token endpoints must be specified");
			}

			this.name = name;
			this.mEnabledRes = checkSpecified(enabledRes, "enabledRes");
			this.mDiscoveryEndpointRes = discoveryEndpointRes;
			this.mAuthEndpointRes = authEndpointRes;
			this.mTokenEndpointRes = tokenEndpointRes;
			this.mRegistrationEndpointRes = registrationEndpointRes;
			this.mClientIdRes = clientIdRes;
			this.mRedirectUriRes = checkSpecified(redirectUriRes, "redirectUriRes");
			this.mScopeRes = checkSpecified(scopeRes, "scopeRes");
			this.buttonImageRes = checkSpecified(buttonImageRes, "buttonImageRes");
			this.buttonContentDescriptionRes = checkSpecified(buttonContentDescriptionRes, "buttonContentDescriptionRes");
			this.buttonTextColorRes = checkSpecified(buttonTextColorRes, "buttonTextColorRes");
		}

		// This must be called before any of the getters will function.
		public void readConfiguration(Context context)
		{
			if (mConfigurationRead)
			{
				return;
			}

			var res = context.Resources;
			mEnabled = res.GetBoolean(mEnabledRes);

			mDiscoveryEndpoint = isSpecified(mDiscoveryEndpointRes) ? getUriResource(res, mDiscoveryEndpointRes, "discoveryEndpointRes") : null;
			mAuthEndpoint = isSpecified(mAuthEndpointRes) ? getUriResource(res, mAuthEndpointRes, "authEndpointRes") : null;
			mTokenEndpoint = isSpecified(mTokenEndpointRes) ? getUriResource(res, mTokenEndpointRes, "tokenEndpointRes") : null;
			mRegistrationEndpoint = isSpecified(mRegistrationEndpointRes) ? getUriResource(res, mRegistrationEndpointRes, "registrationEndpointRes") : null;
			mClientId = isSpecified(mClientIdRes) ? res.GetString(mClientIdRes) : null;
			mRedirectUri = getUriResource(res, mRedirectUriRes, "mRedirectUriRes");
			mScope = res.GetString(mScopeRes);

			mConfigurationRead = true;
		}

		private void checkConfigurationRead()
		{
			if (!mConfigurationRead)
			{
				throw new System.InvalidOperationException("Configuration not read");
			}
		}

		public bool isEnabled()
		{
			checkConfigurationRead();
			return mEnabled;
		}

		public Uri getDiscoveryEndpoint()
		{
			checkConfigurationRead();
			return mDiscoveryEndpoint;
		}

		public Uri getAuthEndpoint()
		{
			checkConfigurationRead();
			return mAuthEndpoint;
		}

		public Uri getTokenEndpoint()
		{
			checkConfigurationRead();
			return mTokenEndpoint;
		}

		public string getClientId()
		{
			checkConfigurationRead();
			return mClientId;
		}

		public void setClientId(string clientId)
		{
			mClientId = clientId;
		}

		public Uri getRedirectUri()
		{
			checkConfigurationRead();
			return mRedirectUri;
		}

		public string getScope()
		{
			checkConfigurationRead();
			return mScope;
		}

		public Task<AuthorizationServiceConfiguration> retrieveConfigAsync(Context context)
		{
			readConfiguration(context);
			if (getDiscoveryEndpoint() != null)
			{
				return AuthorizationServiceConfiguration.FetchFromUrlAsync(mDiscoveryEndpoint);
			}
			else
			{
				return Task.FromResult(new AuthorizationServiceConfiguration(mAuthEndpoint, mTokenEndpoint, mRegistrationEndpoint));
			}
		}

		private static bool isSpecified(int value)
		{
			return value != NOT_SPECIFIED;
		}

		private static int checkSpecified(int value, string valueName)
		{
			if (value == NOT_SPECIFIED)
			{
				throw new System.ArgumentException(valueName + " must be specified");
			}
			return value;
		}

		private static Uri getUriResource(Resources res, int resId, string resName)
		{
			return Uri.Parse(res.GetString(resId));
		}
	}
}
