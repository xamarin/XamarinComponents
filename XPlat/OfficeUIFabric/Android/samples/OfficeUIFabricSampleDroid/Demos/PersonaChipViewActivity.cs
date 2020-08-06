using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class PersonaChipViewActivity : DemoActivity
    {

        protected override int ContentLayoutId => Resource.Layout.activity_persona_chip_view;

        View root_view;
        PersonaChipView persona_chip_example_basic;
        PersonaChipView persona_chip_example_no_icon;
        PersonaChipView persona_chip_example_error;
        ViewGroup persona_chip_layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            root_view = FindViewById<View>(Resource.Id.root_view);

            persona_chip_example_basic = FindViewById<PersonaChipView>(Resource.Id.persona_chip_example_basic);
            persona_chip_example_no_icon = FindViewById<PersonaChipView>(Resource.Id.persona_chip_example_no_icon);
            persona_chip_example_error = FindViewById<PersonaChipView>(Resource.Id.persona_chip_example_error);

            persona_chip_layout = FindViewById<ViewGroup>(Resource.Id.persona_chip_layout);

            var listener = new MyListener
            {
                ClickHandler = () =>
                {
                    Snackbar.Make(root_view, GetString(Resource.String.persona_chip_example_click), Snackbar.LengthShort, Snackbar.Style.Regular).Show();
                }
            };

            CreateDisabledPersonaChip();
            persona_chip_example_basic.Listener = listener;
            persona_chip_example_no_icon.Listener = listener;
            persona_chip_example_error.Listener = listener;
            persona_chip_example_error.HasError = true;
        }

        void CreateDisabledPersonaChip()
        {
            var personaChipView = new PersonaChipView(this);
            personaChipView.Enabled = false;
            personaChipView.Name = Resources.GetString(Resource.String.persona_name_kat_larsson);
            personaChipView.Email = Resources.GetString(Resource.String.persona_email_kat_larsson);
            personaChipView.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            persona_chip_layout.AddView(personaChipView);
        }

        class MyListener : Java.Lang.Object, PersonaChipView.IListener
        {
            public Action ClickHandler { get; set; }

            public void OnClicked()
                => ClickHandler?.Invoke();

            public void OnSelected(bool p0)
            {
            }
        }
    }
}