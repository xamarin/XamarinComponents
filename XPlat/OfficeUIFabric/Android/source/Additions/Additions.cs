using Android.Runtime;
using Android.Views;
using Java.Interop;
using System;

namespace Microsoft.OfficeUIFabric
{
    public partial class PersonaView
    {
        public partial class PersonaViewCompanion
        {
            public partial class Spacing
            {
                // When we rename PersonaView.Companion to PersonaView.PersonaViewCompanion
                // the nested Spacing type gets emitted as a type in this method as
                //   PersonaViewCompanion.Spacing instead of PersonaView.PersonaViewCompanion.Spacing
                // So we manually remove the method and implement it manually

                // Metadata.xml XPath method reference: path="/api/package[@name='com.microsoft.officeuifabric.persona']/class[@name='PersonaView.Companion.Spacing']/method[@name='copy' and count(parameter)=2 and parameter[1][@type='int'] and parameter[2][@type='int']]"
                //[Register("copy", "(II)Lcom/microsoft/officeuifabric/persona/PersonaView$Companion$Spacing;", "")]
                //public unsafe global::Java.Interop.IJavaPeerable Copy(int cellPadding, int insetLeft)
                //{
                //    const string __id = "copy.(II)Lcom/microsoft/officeuifabric/persona/PersonaView$Companion$Spacing;";
                //    try
                //    {
                //        JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                //        __args[0] = new JniArgumentValue(cellPadding);
                //        __args[1] = new JniArgumentValue(insetLeft);
                //        var __rm = _members.InstanceMethods.InvokeNonvirtualObjectMethod(__id, this, __args);
                //        return global::Java.Lang.Object.GetObject<global::Java.Interop.IJavaPeerable>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                //    }
                //    finally
                //    {
                //    }
                //}
            }
        }
    }

    public partial class Snackbar
    {
        public static Snackbar Make(Android.Views.View view, Java.Lang.ICharSequence text, int duration, Style style)
            => Companion.Make(view, text, duration, style);

        public static Snackbar Make(Android.Views.View view, string text, int duration, Style style)
            => Companion.Make(view, text, duration, style);

        public static Snackbar Make(Android.Views.View view, string text)
            => Companion.Make(view, text, Snackbar.LengthShort, Snackbar.Style.Regular);

        public static Snackbar Make(Android.Views.View view, string text, int duration)
            => Companion.Make(view, text, duration, Snackbar.Style.Regular);


        public Snackbar SetCustomView(View view)
            => SetCustomView(view, Snackbar.CustomViewSize.Small);

        public Snackbar SetAction(string text, Action action)
        {
            var listener = new SnackbarActionClickListener
            {
                ClickHandler = action
            };

            return SetAction(text, listener);
        }

        class SnackbarActionClickListener : Java.Lang.Object, Android.Views.View.IOnClickListener
        {
            public Action ClickHandler { get; set; }

            public void OnClick(View v)
                => ClickHandler?.Invoke();
        }
    }

}