using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Fragment = Android.Support.V4.App.Fragment;

using UniversalImageLoaderSample.Fragments;

namespace UniversalImageLoaderSample
{
    [Activity(Label = "@string/ac_name_image_list", Theme = "@style/Theme.AppCompat")]
    public class SimpleImageActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var frIndex = (ImageFragments)Intent.GetIntExtra(Constants.Extra.FragmentIndex, 0);
            Fragment fr;
            string tag;
            int titleRes;
            switch (frIndex)
            {
                default:
                case ImageFragments.List:
                    tag = typeof(ImageListFragment).Name;
                    fr = SupportFragmentManager.FindFragmentByTag(tag) ?? new ImageListFragment();
                    titleRes = Resource.String.ac_name_image_list;
                    break;
                case ImageFragments.Grid:
                    tag = typeof(ImageGridFragment).Name;
                    fr = SupportFragmentManager.FindFragmentByTag(tag) ?? new ImageGridFragment();
                    titleRes = Resource.String.ac_name_image_grid;
                    break;
                case ImageFragments.Pager:
                    tag = typeof(ImagePagerFragment).Name;
                    fr = SupportFragmentManager.FindFragmentByTag(tag);
                    if (fr == null)
                    {
                        fr = new ImagePagerFragment();
                        fr.Arguments = Intent.Extras;
                    }
                    titleRes = Resource.String.ac_name_image_pager;
                    break;
                case ImageFragments.Gallery:
                    tag = typeof(ImageGalleryFragment).Name;
                    fr = SupportFragmentManager.FindFragmentByTag(tag) ?? new ImageGalleryFragment();
                    titleRes = Resource.String.ac_name_image_gallery;
                    break;
            }

            SetTitle(titleRes);
            SupportFragmentManager.BeginTransaction()
                .Replace(Android.Resource.Id.Content, fr, tag)
                .Commit();
        }
    }
}
