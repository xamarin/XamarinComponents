using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Dropbox.Api;
using Xamarin.Auth;


namespace DropboxV2ApiSampleDroid.Helpers
{
    //local implementation of drob box helper
    public class AndroidDropBoxHelper
    {
        private const string tokenAppName = "DropBox";
        private const string oauth2State = "";
        private const string RedirectUri = "https://xamarin.com";
        private const string ApiKey = "";
        private const string AuthorizeUrl = "https://www.dropbox.com/1/oauth2/authorize";
        private const string Scope = "";
        private const string AppName = "SimpleTestApp";
        private Action postAuthAction;
        private Activity hostContext;

        public static bool HasAuthToken
        {
            get
            {
                return !String.IsNullOrWhiteSpace(AccessToken);
            }
        }

        public static string AccessToken
        {
            get
            {
                var accounts = AccountStore.Create().FindAccountsForService(tokenAppName);
                var dbAcct = accounts.FirstOrDefault();

                if (dbAcct != null && !String.IsNullOrWhiteSpace(dbAcct.Properties["access_token"]))
                {
                    return dbAcct.Properties["access_token"];
                }

                return null;
            }
        }

        public static DropboxClient NewClient
        {
            get
            {
                var httpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromMinutes(20),
                };

                var config = new DropboxClientConfig(AppName)
                {
                    HttpClient = httpClient
                };

                return new DropboxClient(AccessToken, config);


            }
        }

        public AndroidDropBoxHelper(Action postAuthAction)
        {
            this.postAuthAction = postAuthAction;
        }


        public void PresentAuthController(Activity ctx)
        {
            hostContext = ctx;

            ShowAuthUI(ctx);

        }


        /// <summary>
        /// Gets the auth user interface.
        /// </summary>
        /// <returns>The auth user interface.</returns>
        public void ShowAuthUI(Activity ctx)
        {
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, ApiKey, new Uri(RedirectUri), state: oauth2State);

            var auth = new OAuth2Authenticator(
                    clientId: ApiKey, // your OAuth2 client id
                    scope: Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                    authorizeUrl: authorizeUri, // the auth URL for the service
                    redirectUrl: new Uri(RedirectUri),
                    isUsingNativeUI: false); // the redirect URL for the service

            auth.AllowCancel = true;

            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.Completed += Auth_Completed;
            auth.Error += Auth_Error;
            auth.BrowsingCompleted += Auth_BrowsingCompleted;

            global::Android.Content.Intent ui_object = auth.GetUI(ctx);


            if (auth.IsUsingNativeUI == true)
            {
                // Step 2.2 Customizing the UI - Native UI [OPTIONAL]
                // In order to access CustomTabs API 
                Xamarin.Auth.CustomTabsConfiguration.AreAnimationsUsed = true;
                Xamarin.Auth.CustomTabsConfiguration.IsShowTitleUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.IsUrlBarHidingUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.IsCloseButtonIconUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.IsActionButtonUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.IsActionBarToolbarIconUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.IsDefaultShareMenuItemUsed = false;
                Xamarin.Auth.CustomTabsConfiguration.MenuItemTitle = null;
                Xamarin.Auth.CustomTabsConfiguration.ToolbarColor = global::Android.Graphics.Color.Orange;
            }

            // Step 3 Present/Launch the Login UI
            ctx.StartActivity(ui_object);
        }

        private void Auth_Error(object sender, AuthenticatorErrorEventArgs ee)
        {

            string msg = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("Message  = ").Append(ee.Message)
                                    .Append(System.Environment.NewLine);
            msg = sb.ToString();

            Toast.MakeText
                        (
                            hostContext,
                            "Message = " + msg,
                            ToastLength.Long
                        ).Show();

            return;

        }

        private void Auth_BrowsingCompleted(object sender, EventArgs ee)
        {

            string msg = "";

            StringBuilder sb = new StringBuilder();
            msg = sb.ToString();

            Toast.MakeText
                        (
                            hostContext,
                            "Message = " + msg,
                            ToastLength.Long
                        ).Show();

            return;
        }

        public void Auth_Completed(object sender, AuthenticatorCompletedEventArgs ee)
        {
            var builder = new AlertDialog.Builder(hostContext);

            if (!ee.IsAuthenticated)
            {
                builder.SetMessage("Not Authenticated");
            }
            else
            {
                try
                {
                    SaveAccount(ee.Account);

                    if (postAuthAction != null)
                        postAuthAction();
                }
                catch (global::Android.OS.OperationCanceledException)
                {
                    builder.SetTitle("Task Canceled");
                }
                catch (Exception ex)
                {
                    builder.SetTitle("Error");
                    builder.SetMessage(ex.ToString());
                }
            }

            //ee.Account

            builder.SetPositiveButton("Ok", (o, e) => { });
            builder.Create().Show();

            return;
        }


        public void SaveAccount(Account act)
        {
            AccountStore.Create().Save(act, tokenAppName);

        }

    }
}