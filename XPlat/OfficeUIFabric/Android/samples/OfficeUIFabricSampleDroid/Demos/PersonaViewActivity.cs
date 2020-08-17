
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class PersonaViewActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_persona_view;

        ViewGroup persona_layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            persona_layout = FindViewById<ViewGroup>(Resource.Id.persona_layout);

            CreateNewPersonaFromCode();
        }

        void CreateNewPersonaFromCode()
        {
            var personaView = new PersonaView(this);
            personaView.AvatarSize = AvatarSize.Small;
            personaView.Name = Resources.GetString(Resource.String.persona_name_mauricio_august);
            personaView.Email = Resources.GetString(Resource.String.persona_email_mauricio_august);
            personaView.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            persona_layout.AddView(personaView);
        }
    }
}