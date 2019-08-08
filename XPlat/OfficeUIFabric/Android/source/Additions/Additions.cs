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
                [Register("copy", "(II)Lcom/microsoft/officeuifabric/persona/PersonaView$Companion$Spacing;", "")]
                public unsafe global::Microsoft.OfficeUIFabric.PersonaView.PersonaViewCompanion.Spacing Copy(int cellPadding, int insetLeft)
                {
                    const string __id = "copy.(II)Lcom/microsoft/officeuifabric/persona/PersonaView$Companion$Spacing;";
                    try
                    {
                        JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                        __args[0] = new JniArgumentValue(cellPadding);
                        __args[1] = new JniArgumentValue(insetLeft);
                        var __rm = _members.InstanceMethods.InvokeNonvirtualObjectMethod(__id, this, __args);
                        return global::Java.Lang.Object.GetObject<global::Microsoft.OfficeUIFabric.PersonaView.PersonaViewCompanion.Spacing>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
                    }
                    finally
                    {
                    }
                }
            }
        }
    }
    public partial class Drawer
    {
        public static Drawer NewInstance(int contentLayoutId)
            => Companion.NewInstance(contentLayoutId);
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

    public partial class CalendarAdapter
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.microsoft.officeuifabric.calendar']/class[@name='CalendarAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='com.microsoft.officeuifabric.calendar.CalendarAdapter.CalendarDayViewHolder'] and parameter[2][@type='int']]"
        [Register("onBindViewHolder", "(Lcom/microsoft/officeuifabric/calendar/CalendarAdapter$CalendarDayViewHolder;I)V", "")]
        public override unsafe void OnBindViewHolder(global::Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            const string __id = "onBindViewHolder.(Lcom/microsoft/officeuifabric/calendar/CalendarAdapter$CalendarDayViewHolder;I)V";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                __args[0] = new JniArgumentValue((holder == null) ? IntPtr.Zero : ((global::Java.Lang.Object)holder).Handle);
                __args[1] = new JniArgumentValue(position);
                _members.InstanceMethods.InvokeAbstractVoidMethod(__id, this, __args);
            }
            finally
            {
            }
        }
    }

    public partial class PersonaListAdapter
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.microsoft.officeuifabric.calendar']/class[@name='PersonaListAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='com.microsoft.officeuifabric.persona.PersonaListAdapter.ViewHolder'] and parameter[2][@type='int']]"
        [Register("onBindViewHolder", "(Lcom/microsoft/officeuifabric/persona/PersonaListAdapter$ViewHolder;I)V", "")]
        public override unsafe void OnBindViewHolder(global::Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            const string __id = "onBindViewHolder.(Lcom/microsoft/officeuifabric/persona/PersonaListAdapter$ViewHolder;I)V";
            try
            {
                JniArgumentValue* __args = stackalloc JniArgumentValue[2];
                __args[0] = new JniArgumentValue((holder == null) ? IntPtr.Zero : ((global::Java.Lang.Object)holder).Handle);
                __args[1] = new JniArgumentValue(position);
                _members.InstanceMethods.InvokeAbstractVoidMethod(__id, this, __args);
            }
            finally
            {
            }
        }
    }
}