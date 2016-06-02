using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

using UniversalImageLoaderSample.Fragments;

namespace UniversalImageLoaderSample
{
    [Activity(Label = "@string/ac_name_complex", Theme = "@style/Theme.AppCompat")]
    public class ComplexImageActivity : AppCompatActivity
    {
        private const string StatePosition = "STATE_POSITION";

        private ViewPager pager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ac_complex);
            
            int pagerPosition = savedInstanceState == null ? 0 : savedInstanceState.GetInt(StatePosition);

            pager = FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = new ImagePagerAdapter(SupportFragmentManager);
            pager.CurrentItem = pagerPosition;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(StatePosition, pager.CurrentItem);
        }

        private class ImagePagerAdapter : FragmentPagerAdapter
        {
            private Fragment listFragment;
            private Fragment gridFragment;

            public ImagePagerAdapter(FragmentManager fm)
                : base(fm)
            {
                listFragment = new ImageListFragment();
                gridFragment = new ImageGridFragment();
            }

            public override int Count
            {
                get { return 2; }
            }

            public override Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:
                        return listFragment;
                    case 1:
                        return gridFragment;
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return Application.Context.GetTextFormatted(Resource.String.title_list);
                    case 1:
                        return Application.Context.GetTextFormatted(Resource.String.title_grid);
                    default:
                        return null;
                }
            }
        }
    }
}
