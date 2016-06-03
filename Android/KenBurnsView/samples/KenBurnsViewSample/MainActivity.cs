using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using ImageViews.KenBurns;

namespace KenBurnsViewSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private bool isPaused;

        private ViewSwitcher viewSwitcher;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            viewSwitcher = FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            var img1 = FindViewById<KenBurnsView>(Resource.Id.img1);
            img1.TransitionEnd += OnTransitionEnded;
            var img2 = FindViewById<KenBurnsView>(Resource.Id.img2);
            img2.TransitionEnd += OnTransitionEnded;
        }

        private void OnTransitionEnded(object sender, KenBurnsView.TransitionEndEventArgs e)
        {
            viewSwitcher.ShowNext();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            var item = menu.FindItem(Resource.Id.playPause);
            if (isPaused)
            {
                item.SetIcon(Resource.Drawable.ic_media_play);
                item.SetTitle(Resource.String.play);
            }
            else
            {
                item.SetIcon(Resource.Drawable.ic_media_pause);
                item.SetTitle(Resource.String.pause);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.playPause)
            {
                var currentImage = (KenBurnsView)viewSwitcher.CurrentView;
                if (isPaused)
                {
                    currentImage.Resume();
                }
                else
                {
                    currentImage.Pause();
                }
                isPaused = !isPaused;
                SupportInvalidateOptionsMenu();
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
