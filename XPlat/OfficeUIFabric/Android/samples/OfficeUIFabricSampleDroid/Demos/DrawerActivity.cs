using System.Linq;

using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class DrawerActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_drawer;

        Microsoft.OfficeUIFabric.Button show_drawer_button;
        Microsoft.OfficeUIFabric.Button show_drawer_dialog_button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            show_drawer_button = FindViewById<Microsoft.OfficeUIFabric.Button>(Resource.Id.show_drawer_button);
            show_drawer_dialog_button = FindViewById<Microsoft.OfficeUIFabric.Button>(Resource.Id.show_drawer_dialog_button);

            show_drawer_button.Click += delegate {
                var drawerDemo = Drawer.Companion.NewInstance(Resource.Layout.demo_drawer_content);
                drawerDemo.Show(SupportFragmentManager, null);
            };

            show_drawer_dialog_button.Click += delegate {
                var drawerDialogDemo = new DrawerDialog(this);
                drawerDialogDemo.DrawerContentCreated += DrawerDialogDemo_DrawerContentCreated;
                drawerDialogDemo.SetContentView(Resource.Layout.demo_drawer_content);
                drawerDialogDemo.Show();
            };
        }

        private void DrawerDialogDemo_DrawerContentCreated(object sender, DrawerContentCreatedEventArgs e)
        {
            var drawerContents = e.DrawerContents;
            var personaList = Utils.CreatePersonaList(this);
            drawerContents.FindViewById<PersonaListView>(Resource.Id.drawer_demo_persona_list)
                .Personas = personaList.ToList();
        }
    }
}