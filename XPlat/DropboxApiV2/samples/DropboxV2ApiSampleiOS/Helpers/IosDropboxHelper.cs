using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Dropbox.Api;
using UIKit;
using Xamarin.Auth;


namespace DropboxV2ApiSampleiOS.Helpers
{
    //Local instance of a helper class for dropbbox
    public class IosDropBoxHelper : IDisposable
    {
        private const string tokenAppName = "DropBox2";
        private const string oauth2State = "";
        private const string RedirectUri = "https://xamarin.com";
        private const string ApiKey = "";
        private const string AuthorizeUrl = "https://www.dropbox.com/1/oauth2/authorize";
        private const string Scope = "";
        private const string AppName = "SimpleTestApp";
        private UIViewController currentAuth = null;
        private UIViewController hostVC;
        private Action postAuthAction;

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

        public IosDropBoxHelper(Action postAuthAction)
        {
            this.postAuthAction = postAuthAction;
        }


        public void PresentAuthViewController(UIViewController hostViewController)
        {
            currentAuth = GetAuthUI();
            hostVC = hostViewController;

            hostVC.PresentViewController(currentAuth, false, null);
        }

        /// <summary>
        /// Gets the auth user interface.
        /// </summary>
        /// <returns>The auth user interface.</returns>
        public UIViewController GetAuthUI()
        {
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, ApiKey, new Uri(RedirectUri), state: oauth2State);

            var auth = new OAuth2Authenticator(
                    clientId: ApiKey, // your OAuth2 client id
                    scope: Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                    authorizeUrl: authorizeUri, // the auth URL for the service
                    redirectUrl: new Uri(RedirectUri),
                    isUsingNativeUI: false); // the redirect URL for the service



            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.Completed += Auth_Completed;
            auth.Error += Auth_Error;
            auth.BrowsingCompleted += Auth_BrowsingCompleted;

            currentAuth = (UIViewController)auth.GetUI();

            return currentAuth;
        }

        public void Auth_Completed(object sender, AuthenticatorCompletedEventArgs ee)
        {
            string title = "OAuth Results";
            string msg = "";

            if (!ee.IsAuthenticated)
            {
                msg = "Not Authenticated";
            }
            else
            {
                if (currentAuth != null)
                    currentAuth.DismissViewController(false, null);

                try
                {
                    SaveAccount(ee.Account);

                    if (postAuthAction != null)
                        postAuthAction();
                }
                catch (Xamarin.Auth.AuthException exc)
                {
                    msg = exc.Message;

                    var alert = new UIAlertView
                                    (
                                        "Error - AccountStore Saving",
                                        "AuthException = " + Environment.NewLine + msg,
                                        null,
                                        "OK",
                                        null
                                    );
                    alert.Show();

                    throw new Exception("AuthException", exc);
                }

            }

            hostVC.InvokeOnMainThread
            (
                () =>
                {
                    // manipulate UI controls
                    var _error = new UIAlertView(title, msg, null, "Ok", null);
                    _error.Show();
                }
            );

            return;
        }

        private void Auth_Error(object sender, AuthenticatorErrorEventArgs ee)
        {
            string title = "OAuth Error";
            string msg = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("Message  = ").Append(ee.Message)
                .Append(System.Environment.NewLine);
            msg = sb.ToString();

            hostVC.InvokeOnMainThread
            (
                () =>
                {
                    // manipulate UI controls
                    var _error = new UIAlertView(title, msg, null, "Ok", null);
                    _error.Show();
                }
            );

            return;

        }

        private void Auth_BrowsingCompleted(object sender, EventArgs ee)
        {
            string msg = "";

            StringBuilder sb = new StringBuilder();
            msg = sb.ToString();

            return;
        }

        public void SaveAccount(Account act)
        {
            AccountStore.Create().Save(act, tokenAppName);

        }

        public void Dispose()
        {

        }
    }
}
