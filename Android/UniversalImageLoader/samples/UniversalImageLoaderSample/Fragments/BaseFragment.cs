using Android.OS;
using Android.Views;
using Fragment = Android.Support.V4.App.Fragment;

using UniversalImageLoader.Core;

namespace UniversalImageLoaderSample.Fragments
{
    public abstract class BaseFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            HasOptionsMenu = true;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.main_menu, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.item_clear_memory_cache:
                    ImageLoader.Instance.ClearMemoryCache();
                    return true;
                case Resource.Id.item_clear_disc_cache:
                    ImageLoader.Instance.ClearDiskCache();
                    return true;
                default:
                    return false;
            }
        }
    }
}
