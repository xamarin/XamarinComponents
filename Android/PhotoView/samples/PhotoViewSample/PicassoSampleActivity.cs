using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Square.Picasso;

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

            Picasso
                .With(this)
                .Load("http://pbs.twimg.com/media/Bist9mvIYAAeAyQ.jpg")
                .Into(photoView);
        }
    }
}
