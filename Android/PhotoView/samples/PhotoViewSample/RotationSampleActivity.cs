using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "Rotation Sample")]
    public class RotationSampleActivity : AppCompatActivity
    {
        private PhotoView photo;
        private Handler handler = new Handler();
        private bool rotating = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_rotation_sample);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Rotation Sample";
            toolbar.InflateMenu(Resource.Menu.rotation);
            toolbar.MenuItemClick += (sender, e) =>
            {
                switch (e.Item.ItemId)
                {
                    case Resource.Id.action_rotate_10_right:
                        photo.SetRotationBy(10);
                        e.Handled = true;
                        break;

                    case Resource.Id.action_rotate_10_left:
                        photo.SetRotationBy(-10);
                        e.Handled = true;
                        break;

                    case Resource.Id.action_toggle_automatic_rotation:
                        ToggleRotation();
                        e.Handled = true;
                        break;

                    case Resource.Id.action_reset_to_0:
                        photo.SetRotationTo(0);
                        e.Handled = true;
                        break;

                    case Resource.Id.action_reset_to_90:
                        photo.SetRotationTo(90);
                        e.Handled = true;
                        break;

                    case Resource.Id.action_reset_to_180:
                        photo.SetRotationTo(180);
                        e.Handled = true;
                        break;

                    case Resource.Id.action_reset_to_270:
                        photo.SetRotationTo(270);
                        e.Handled = true;
                        break;
                }
            };

            photo = FindViewById<PhotoView>(Resource.Id.iv_photo);
        }

        protected override void OnPause()
        {
            base.OnPause();
            handler.RemoveCallbacksAndMessages(null);
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
