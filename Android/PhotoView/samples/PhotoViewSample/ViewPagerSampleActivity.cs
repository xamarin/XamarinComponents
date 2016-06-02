using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;

using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "ViewPager Sample", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class ViewPagerSampleActivity : AppCompatActivity
    {
        private static string IsLockedArgument = "isLocked";

        private HackyViewPager viewPager;
        private IMenuItem menuLockItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            viewPager = new HackyViewPager(this);
            SetContentView(viewPager);

            viewPager.Adapter = new SamplePagerAdapter();

            if (savedInstanceState != null)
            {
                viewPager.IsLocked = savedInstanceState.GetBoolean(IsLockedArgument, false);
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (IsViewPagerActive())
            {
                outState.PutBoolean(IsLockedArgument, viewPager.IsLocked);
            }
            base.OnSaveInstanceState(outState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.viewpager_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menuLockItem = menu.FindItem(Resource.Id.menu_lock);
            ToggleLockBtnTitle();

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_lock)
            {
                ToggleViewPagerScrolling();
                ToggleLockBtnTitle();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void ToggleViewPagerScrolling()
        {
            if (IsViewPagerActive())
            {
                viewPager.ToggleLock();
            }
        }

        private void ToggleLockBtnTitle()
        {
            bool isLocked = false;
            if (IsViewPagerActive())
            {
                isLocked = viewPager.IsLocked;
            }
            var title = (isLocked) ? GetString(Resource.String.menu_unlock) : GetString(Resource.String.menu_lock);
            if (menuLockItem != null)
            {
                menuLockItem.SetTitle(title);
            }
        }

        private bool IsViewPagerActive()
        {
            return (viewPager != null && viewPager is HackyViewPager);
        }

        private class SamplePagerAdapter : PagerAdapter
        {
            public override int Count
            {
                get { return 6; }
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                PhotoView photoView = new PhotoView(container.Context);
                photoView.SetImageResource(Resource.Drawable.wallpaper);

                // Now just add PhotoView to ViewPager and return it
                container.AddView(photoView, ViewPager.LayoutParams.MatchParent, ViewPager.LayoutParams.MatchParent);

                return photoView;
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
            {
                container.RemoveView((View)objectValue);
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
            {
                return view == objectValue;
            }
        }
    }
}
