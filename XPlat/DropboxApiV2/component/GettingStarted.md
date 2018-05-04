Dropbox API v2 SDK is an .NET SDK for the DropBox API v2, which helps you easily integrate Dropbox into your app. We also have also provided simple integration with Xamarin.Auth, Xamarin.iOS and Xamarin.Android. 

## Creating an application

In order to use the Dropbox v2 API you will need to create a new Dropbox application that will allow you to make API requests.

You can go to [https://www.dropbox.com/developers/apps](https://www.dropbox.com/developers/apps) to setup the app and get the API key

## Obtaining an access token

All requests need to be made with an OAuth 2 access token.  This can be retrieved using Xamarin.Auth, or any other OAuth authenticator.  

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

Once authorisation is completed you can store the the token for use later, such as using the AccountStore from Xamarin.Auth

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
        
The authorisation token can then be used to create the `DropboxClient`

	//Load the account details from Xamarin.Auth Accounts store
    var accounts = AccountStore.Create().FindAccountsForService(tokenAppName);
    var dbAcct = accounts.FirstOrDefault();

    if (dbAcct != null && !String.IsNullOrWhiteSpace(dbAcct.Properties["access_token"]))
    {
        //get the access token
        var accessToken = dbAcct.Properties["access_token"];

		//create the http client for the DropboxClient to use
        var httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromMinutes(20),
        };

	    //Create the config for the DropboxClient
        var config = new DropboxClientConfig(AppName)
        {
             HttpClient = httpClient
        };

		//Create the client
        var client = new DropboxClient(AccessToken, config);
    }
                
## Loading items from dropbox
Once you have instantiated an instance of `DropboxClient` you can use it to access the list of items at a give path with the dropbox file structure.

The root location can be accessed by passing an empty string to the `ListFolderArg` constructor.

	var someFiles = await client.Files.ListFolderAsync(new Dropbox.Api.Files.ListFolderArg(currentPath)
	{
	
	});
	
	var sList = someFiles.Entries.OrderBy(x => x.Name);
	foreach (var item in sList)
	{
		fileNames.Add(new DropBoxItem()
		{
			Name = item.Name,
			ItemType = (item.IsFolder) ? DropBoxItemType.Folder : DropBoxItemType.File,
			Path = item.PathLower,
		});
	}
					
					
#Dropbox + Xamarin.Auth#

To make it even easier to use Dropbox we have library that combines Dropbox with Xamrin.Auth. This wraps the Authorization, configuration and access into a simple class called `DropboxHelper`

##Configuration##

To configure DropboxHelper there is a fluent Api available on the `DropboxConfig` that allows you to set the relevant properties need to set up your Dropbox access.

	// Config the dropbox helper settings
	DropBoxConfig.Configure()
        .SetAppName("SimpleTestApp")
        .SetAuthorizeUrl("https://www.dropbox.com/1/oauth2/authorize")
        .SetApiKey("")
        .SetRedirectUri("https://xamarin.com")
        .SetTokenAppName("DropBox");

##Authenticate##

Once configured you can then check to see if the authorisation has been set already or request authentication to be done

	if (DropBoxHelper.IsAuthenticated)
	{
		await ReloadDataAsync();
	}
	else
	{
		var authHelp = new DropBoxHelper(async () =>
		{
			await ReloadDataAsync();

		});

		//pass the view controller on iOS or Activity on Android
		authHelp.PresentAuthController(this);
		
	}
	
##Connect##

Once authenticated you can get `DropboxHelper` to generate a new instance of `DropboxClient` so that you can access all of the Dropbox functionality.

	using (var client = DropBoxHelper.CreateDropboxClient())
	{
		var someFiles = await client.Files.ListFolderAsync(new Dropbox.Api.Files.ListFolderArg(currentPath)
		{
		
		});
		
		var sList = someFiles.Entries.OrderBy(x => x.Name);
		foreach (var item in sList)
		{
			var ffold = item.AsFolder;
			fileNames.Add(new DropBoxItem()
			{
				Name = item.Name,
				ItemType = (item.IsFolder) ? DropBoxItemType.Folder : DropBoxItemType.File,
				Path = item.PathLower,
			});
		}
	}