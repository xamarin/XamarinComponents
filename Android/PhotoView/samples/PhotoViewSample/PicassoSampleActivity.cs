using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Bumptech.Glide;
using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "Picasso Sample")]
    public class PicassoSampleActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_simple);

            var photoView = FindViewById<PhotoView>(Resource.Id.iv_photo);

            Glide.With(this)
                .Load("http://pbs.twimg.com/media/Bist9mvIYAAeAyQ.jpg")
                .Into(photoView);
        }
    }
}
