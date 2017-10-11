using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Dropbox.Api;
using Xamarin.Auth;
using Xamarin.Dropbox.Api.Core.Data;

namespace Xamarin.Dropbox.Api.Helpers
{
    public abstract class DropBoxHelperBase : IDisposable
    {
        #region Fields
        private Action postAuthAction;
        #endregion

        #region Properties


        public bool HasAuthToken
        {
            get
            {
                return !String.IsNullOrWhiteSpace(AccessToken);
            }
        }

        public string AccessToken
        {
            get
            {
                var accounts = AccountStore.Create().FindAccountsForService(DropBoxConfig.Instance.TokenAppName);
                var dbAcct = accounts.FirstOrDefault();

                if (dbAcct != null && !String.IsNullOrWhiteSpace(dbAcct.Properties["access_token"]))
                {
                    return dbAcct.Properties["access_token"];
                }

                return null;
            }
        }

        protected Action PostAuthAction
        {
            get
            {
                return postAuthAction;
            }
        }

        #endregion

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Xamarin.Dropbox.Api.Helpers.DropBoxHelperBase"/> class.
        /// </summary>
        /// <param name="postAuthAction">Post auth action.</param>
        public DropBoxHelperBase(Action postAuthAction)
        {
            this.postAuthAction = postAuthAction;
        }

        #endregion

        #region Methods


        public DropboxClient GenerateClient()
        {
            var httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(20),
            };

            var conf = new DropboxClientConfig(DropBoxConfig.Instance.AppName)
            {
                HttpClient = httpClient
            };

            return new DropboxClient(AccessToken, conf);
        }

        public void SaveAccount(Account act)
        {
            AccountStore.Create().Save(act, DropBoxConfig.Instance.TokenAppName);

        }

        public OAuth2Authenticator BuildAuthenticator()
        {
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

            return auth;

        }

        public void Dispose()
        {


        }

        #endregion

        #region Events

        /// <summary>
        /// Auths the error.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="ee">Ee.</param>
        public virtual void Auth_Error(object sender, AuthenticatorErrorEventArgs ee)
        {
			string msg = "";

			var sb = new StringBuilder();
			sb.Append("Message  = ").Append(ee.Message)
									.Append(Environment.NewLine);
			msg = sb.ToString();

            ShowMessage("", msg, true);
        }

        public void Auth_BrowsingCompleted(object sender, EventArgs ee)
        {
			string msg = "";

			StringBuilder sb = new StringBuilder();
			msg = sb.ToString();

            ShowMessage("", msg, true);
        }

        public void Auth_Completed(object sender, AuthenticatorCompletedEventArgs ee)
        {
            if (!ee.IsAuthenticated)
            {
                ShowMessage("Authentication Failed", "Not Authenticated");
            }
            else
            {
                DismissAuthController();

                try
                {
                    SaveAccount(ee.Account);

                    PostAuthAction?.Invoke();

                }
                catch (Exception ex)
                {
                    if (!HandleNativeException(ex))
                    {
                        ShowMessage("Authentication Error", ex.ToString());
                    }
                }
            }

			return;
        }

        protected abstract void DismissAuthController();

        /// <summary>
        /// Handles the native exception.
        /// </summary>
        /// <returns><c>true</c>, if native exception was handled, <c>false</c> otherwise.</returns>
        /// <param name="ex">Ex.</param>
        protected abstract bool HandleNativeException(Exception ex);

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="toast">If set to <c>true</c> toast.</param>
        protected abstract void ShowMessage(string title, string message, bool toast = false);

        #endregion
    }


    public abstract class DropBoxHelperBase<T> : DropBoxHelperBase
    {
        private T hasContext;

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected T Context
        {
            get
            {
                return hasContext;
            }
            set
            {
                hasContext = value;
            }
        }

        /// <summary>
        /// Presents the authentication controller.
        /// </summary>
        /// <param name="context">Context.</param>
		public void PresentAuthController(T context)
		{
			Context = context;

			ShowAuthUI(context);

		}

        protected abstract void ShowAuthUI(T ctx);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Xamarin.Dropbox.Api.Helpers.DropBoxHelperBase`1"/> class.
        /// </summary>
        /// <param name="postAuthAction">Post auth action.</param>
        public DropBoxHelperBase(Action postAuthAction) 
            : base(postAuthAction)
        {
            
        }


    }
}
