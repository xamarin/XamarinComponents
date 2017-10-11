using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Dropbox.Api;
using UIKit;
using Xamarin.Auth;
using Xamarin.Dropbox.Api.Helpers;

namespace Xamarin.Dropbox.Api.iOS
{
    public class DropBoxHelper : DropBoxHelperBase<UIViewController>
    {

        private UIViewController currentAuth = null;


        /// <summary>
        /// Gets a value indicating whether this <see cref="T:DropboxV2ApiSampleDroid.Helpers.DropboxHelper"/> is authenticated.
        /// </summary>
        /// <value><c>true</c> if is authenticated; otherwise, <c>false</c>.</value>
        public static bool IsAuthenticated
        {
            get
            {
                return new DropBoxHelper().HasAuthToken;
            }
        }

        /// <summary>
        /// Create a new new client
        /// </summary>
        /// <value>The create client.</value>
        public static DropboxClient CreateDropboxClient()
        {
            return new DropBoxHelper().GenerateClient();
        }

        public DropBoxHelper() : base(null)
        {

        }

        public DropBoxHelper(Action postAuthAction) : base(postAuthAction)
        {

        }

        protected override void ShowAuthUI(UIViewController ctx)
        {
            Context = ctx;

            var auth = BuildAuthenticator();

            currentAuth = (UIViewController)auth.GetUI();


            Context.PresentViewController(currentAuth, false, null);

        }


        protected override void DismissAuthController()
        {
            if (currentAuth != null)
            {
                currentAuth.DismissViewController(false, null);

                currentAuth = null;
            }

        }



        protected override bool HandleNativeException(Exception ex)
        {
            if (ex is Xamarin.Auth.AuthException)
            {
                ShowMessage("Error - AccountStore Saving",
                                        "AuthException = " + Environment.NewLine + ex.Message);

                return true;
            }

            return false;
        }

        protected override void ShowMessage(string title, string message, bool toast = false)
        {
            Context.InvokeOnMainThread
            (
                () =>
                {
                    // manipulate UI controls
                    var _error = new UIAlertView(title, message, null, "Ok", null);
                    _error.Show();
                }
            );


        }


    }
}
