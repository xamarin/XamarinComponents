using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

using Explosions;

namespace ExplosionsSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        private ExplosionView explosions;

        private List<View> views = new List<View>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            explosions = ExplosionView.Attach(this);

            SetContentView(Resource.Layout.Main);

            views.Add(FindViewById(Resource.Id.image1));
            views.Add(FindViewById(Resource.Id.image2));
            views.Add(FindViewById(Resource.Id.image3));
            views.Add(FindViewById(Resource.Id.image4));
            views.Add(FindViewById(Resource.Id.image5));
            views.Add(FindViewById(Resource.Id.image6));
            views.Add(FindViewById(Resource.Id.clickToDestroy));

            foreach (var view in views)
            {
                view.Click += OnViewClick;
            }
        }

        private void OnViewClick(object sender, EventArgs e)
        {
            var view = sender as View;
            view.Click -= OnViewClick;
            explosions.Explode(view);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.reset)
            {
                foreach (var view in views)
                {
                    view.Click += OnViewClick;
                    explosions.Reset(view);
                }
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

