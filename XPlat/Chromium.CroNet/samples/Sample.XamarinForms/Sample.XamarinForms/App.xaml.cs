using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sample.XamarinForms.Services;
using Sample.XamarinForms.Views;

namespace Sample.XamarinForms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
