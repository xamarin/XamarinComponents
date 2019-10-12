using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class DrawerActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_drawer;

        Button show_drawer_button;
        Button show_drawer_dialog_button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            show_drawer_button = FindViewById<Button>(Resource.Id.show_drawer_button);
            show_drawer_dialog_button = FindViewById<Button>(Resource.Id.show_drawer_dialog_button);

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
            var drawerContents = e.P0;
            var personaList = Utils.CreatePersonaList(this);
            drawerContents.FindViewById<PersonaListView>(Resource.Id.drawer_demo_persona_list)
                .Personas = personaList.ToList();
        }
    }
}