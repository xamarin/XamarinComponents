using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "Rotation Sample", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class RotationSampleActivity : AppCompatActivity
    {
        private PhotoView photo;
        private Handler handler = new Handler();
        private bool rotating = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            photo = new PhotoView(this);
            photo.SetImageResource(Resource.Drawable.wallpaper);
            SetContentView(photo);
        }

        protected override void OnPause()
        {
            base.OnPause();
            handler.RemoveCallbacksAndMessages(null);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(Menu.None, 0, Menu.None, "Rotate 10° Right");
            menu.Add(Menu.None, 1, Menu.None, "Rotate 10° Left");
            menu.Add(Menu.None, 2, Menu.None, "Toggle automatic rotation");
            menu.Add(Menu.None, 3, Menu.None, "Reset to 0");
            menu.Add(Menu.None, 4, Menu.None, "Reset to 90");
            menu.Add(Menu.None, 5, Menu.None, "Reset to 180");
            menu.Add(Menu.None, 6, Menu.None, "Reset to 270");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    photo.SetRotationBy(10);
                    return true;
                case 1:
                    photo.SetRotationBy(-10);
                    return true;
                case 2:
                    ToggleRotation();
                    return true;
                case 3:
                    photo.SetRotationTo(0);
                    return true;
                case 4:
                    photo.SetRotationTo(90);
                    return true;
                case 5:
                    photo.SetRotationTo(180);
                    return true;
                case 6:
                    photo.SetRotationTo(270);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ToggleRotation()
        {
            if (rotating)
            {
                handler.RemoveCallbacksAndMessages(null);
            }
            else
            {
                RotateLoop();
            }
            rotating = !rotating;
        }

        private void RotateLoop()
        {
            handler.PostDelayed(() =>
            {
                photo.SetRotationBy(1);
                RotateLoop();
            }, 15);
        }
    }
}
