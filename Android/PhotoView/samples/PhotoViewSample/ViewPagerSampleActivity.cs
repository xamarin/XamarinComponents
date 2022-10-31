using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.ViewPager.Widget;
using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "ViewPager Sample")]
    public class ViewPagerSampleActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var viewPager = new HackyViewPager(this);
            SetContentView(viewPager);

            viewPager.Adapter = new SamplePagerAdapter();
        }

        private class SamplePagerAdapter : PagerAdapter
        {
            public override int Count => 6;

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                var photoView = new PhotoView(container.Context);
                photoView.SetImageResource(Resource.Drawable.wallpaper);

                // Now just add PhotoView to ViewPager and return it
                container.AddView(photoView, ViewPager.LayoutParams.MatchParent, ViewPager.LayoutParams.MatchParent);

                return photoView;
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
            {
                container.RemoveView((View) objectValue);
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
            {
                return view == objectValue;
            }
        }
    }
}
