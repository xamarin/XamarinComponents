using System;
using Java.Lang;
using Stripe.Android.Model;

namespace Sample.Stripe.XamarinAndroid
{
    public partial class TokenCallbackImplementation : Java.Lang.Object, global::Stripe.Android.ITokenCallback
    {
        Android.Content.Context context = null;

        public TokenCallbackImplementation(Android.Content.Context c)
        {
            this.context = c;

            return;
        }

        public void Dispose()
        {
            return;
        }

        public void OnError(Java.Lang.Exception error)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Error {error.LocalizedMessage}");

            Android.Widget.Toast.MakeText
                                    (
                                        this.context, 
                                        sb.ToString(), 
                                        Android.Widget.ToastLength.Long
                                    ).Show();

            return;
        }

        public void OnSuccess(Token token)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Success send the token to your server");

            Android.Widget.Toast.MakeText
                                    (
                                        this.context,
                                        sb.ToString(),
                                        Android.Widget.ToastLength.Long
                                    ).Show();

            return;
        }
    }
}
