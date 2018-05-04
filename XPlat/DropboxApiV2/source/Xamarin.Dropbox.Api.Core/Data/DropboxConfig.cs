using System;
namespace Xamarin.Dropbox.Api.Core.Data
{
    public class DropBoxConfig
    {
        private static Lazy<DropBoxConfig> instance = new Lazy<DropBoxConfig>(() => new DropBoxConfig());

        public static DropBoxConfig Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public static DropBoxConfig Configure()
        {
            return Instance;

        }

        public DropBoxConfig SetAppName(string appName)
        {
            AppName = appName;

            return this;
        }

		public DropBoxConfig SetScope(string data)
		{
			Scope = data;

			return this;
		}

		public DropBoxConfig SetAuthorizeUrl(string data)
		{
			AuthorizeUrl = data;

			return this;
		}

		public DropBoxConfig SetApiKey(string data)
		{
			ApiKey = data;

			return this;
		}

		public DropBoxConfig SetRedirectUri(string data)
		{
			RedirectUri = data;

			return this;
		}

		public DropBoxConfig SetOauth2State(string data)
		{
			Oauth2State = data;

			return this;
		}

		public DropBoxConfig SetTokenAppName(string data)
		{
			TokenAppName = data;

			return this;
		}

        public string AppName { get; set; }

		public string Scope { get; set; }
		public string AuthorizeUrl { get; set; }
		public string ApiKey { get; set; }
		public string RedirectUri { get; set; }
		public string Oauth2State { get; set; }
		public string TokenAppName { get; set; }

        public DropBoxConfig()
        {
            Scope = string.Empty;
            Oauth2State = string.Empty;
        }
    }
}
