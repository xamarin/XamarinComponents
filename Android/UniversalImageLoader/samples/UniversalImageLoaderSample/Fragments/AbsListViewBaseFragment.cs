using Android.Content;
using Android.Widget;
using Android.Views;

using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Listener;

namespace UniversalImageLoaderSample.Fragments
{
    public abstract class AbsListViewBaseFragment : BaseFragment
    {
        protected internal AbsListView listView;

        protected internal bool pauseOnScroll = false;
        protected internal bool pauseOnFling = true;

        public override void OnResume()
        {
            base.OnResume();
            ApplyScrollListener();
        }

        public override void OnPrepareOptionsMenu(IMenu menu)
        {
            var pauseOnScrollItem = menu.FindItem(Resource.Id.item_pause_on_scroll);
            pauseOnScrollItem.SetVisible(true);
            pauseOnScrollItem.SetChecked(pauseOnScroll);

            var pauseOnFlingItem = menu.FindItem(Resource.Id.item_pause_on_fling);
            pauseOnFlingItem.SetVisible(true);
            pauseOnFlingItem.SetChecked(pauseOnFling);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.item_pause_on_scroll:
                    pauseOnScroll = !pauseOnScroll;
                    item.SetChecked(pauseOnScroll);
                    ApplyScrollListener();
                    return true;
                case Resource.Id.item_pause_on_fling:
                    pauseOnFling = !pauseOnFling;
                    item.SetChecked(pauseOnFling);
                    ApplyScrollListener();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected internal virtual void StartImagePagerActivity(int position)
        {
            var intent = new Intent(Activity, typeof(SimpleImageActivity));
            intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.Pager);
            intent.PutExtra(Constants.Extra.ImagePosition, position);
            StartActivity(intent);
        }

        private void ApplyScrollListener()
        {
            listView.SetOnScrollListener(new PauseOnScrollListener(ImageLoader.Instance, pauseOnScroll, pauseOnFling));
        }
    }
}
