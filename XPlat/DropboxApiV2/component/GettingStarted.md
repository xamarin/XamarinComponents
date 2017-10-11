Dropbox API v2 SDK is an .NET SDK for the DropBox API v2, which helps you easily integrate Dropbox into your app. We also have also provided simple  with Xamarin.Auth and Xamarin.iOS and Xamarin.Android. 

## Creating an application

In order to use the Dropbox v2 API you will need to create an Dropbox application that will allow you to make API requests.

You can go to [https://www.dropbox.com/developers/apps](https://www.dropbox.com/developers/apps) to setup the app and get the API key

## Obtaining an access token

All requests to be made with an OAuth 2 access token.  This can be retrieved using Xamarin.Auth, or any other OAuth authenticator.  

	var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, DropBoxConfig.Instance.ApiKey, new Uri(DropBoxConfig.Instance.RedirectUri), state: DropBoxConfig.Instance.Oauth2State);
    
    var auth = new OAuth2Authenticator(
                    clientId: DropBoxConfig.Instance.ApiKey, // your OAuth2 client id
                    scope: DropBoxConfig.Instance.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                    authorizeUrl: authorizeUri, // the auth URL for the service
                    redirectUrl: new Uri(DropBoxConfig.Instance.RedirectUri),
                    isUsingNativeUI: false);

	// If authorization succeeds or is canceled, .Completed will be fired.
	auth.Completed += Auth_Completed;
	auth.Error += Auth_Error;
	auth.BrowsingCompleted += Auth_BrowsingCompleted;

once authorisation is completed you can store the the token for use later, such as using the AccountStore 

        public void Auth_Completed(object sender, AuthenticatorCompletedEventArgs ee)
        {
            if (!ee.IsAuthenticated)
            {
                //failed auth
            }
            else
            {
                try
                {
                	AccountStore.Create().Save(ee.Account), tokenAppName);                
                }
                catch (Xamarin.Auth.AuthException exc)
                {
                    
                }
            }

			return;
        } 