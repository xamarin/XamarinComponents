using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Square.Picasso;

namespace PicassoSample
{
    [Activity(Theme = "@style/Theme.AppCompat")]
    public class SampleGalleryActivity : PicassoSampleActivity
    {
        private const int GalleryRequest = 9391;
        private const string ImageKey = "PicassoSample:image";

        private ImageView imageView;
        private ViewAnimator animator;
        private string image;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sample_gallery_activity);

            animator = FindViewById<ViewAnimator>(Resource.Id.animator);
            imageView = FindViewById<ImageView>(Resource.Id.image);

            FindViewById(Resource.Id.go).Click += delegate
            {
                Intent gallery = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
                StartActivityForResult(gallery, GalleryRequest);
            };

            if (savedInstanceState != null)
            {
                image = savedInstanceState.GetString(ImageKey);
                if (image != null)
                {
                    LoadImage();
                }
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (IsFinishing)
            {
                // Always cancel the request here, this is safe to call even if the image has been loaded.
                // This ensures that the anonymous callback we have does not prevent the activity from
                // being garbage collected. It also prevents our callback from getting invoked even after the
                // activity has finished.
                Picasso.Get().CancelRequest(imageView);
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(ImageKey, image);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == GalleryRequest && resultCode == Result.Ok && data != null)
            {
                image = data.Data.ToString();
                LoadImage();
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        private void LoadImage()
        {
            // Index 1 is the progress bar. Show it while we're loading the image.
            animator.DisplayedChild = 1;

            Picasso.Get()
                   .Load(image)
                   .Into(imageView, delegate { animator.DisplayedChild = 0; }, null);
        }
    }
}
