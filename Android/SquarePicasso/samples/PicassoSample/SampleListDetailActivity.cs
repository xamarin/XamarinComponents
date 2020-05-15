using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;

using Square.Picasso;

namespace PicassoSample
{
    [Activity(Theme = "@style/Theme.AppCompat")]
    public class SampleListDetailActivity : PicassoSampleActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState == null)
            {
                SupportFragmentManager.BeginTransaction()
                                      .Add(Resource.Id.sample_content, ListFragment.NewInstance())
                                      .Commit();
            }
        }

        protected virtual void ShowDetails(string url)
        {
            SupportFragmentManager.BeginTransaction()
                                  .Replace(Resource.Id.sample_content, DetailFragment.NewInstance(url))
                                  .AddToBackStack(null)
                                  .Commit();
        }

        public class ListFragment : Fragment
        {
            public static ListFragment NewInstance()
            {
                return new ListFragment();
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                SampleListDetailActivity activity = (SampleListDetailActivity)Activity;
                SampleListDetailAdapter adapter = new SampleListDetailAdapter(activity);

                ListView listView = (ListView)LayoutInflater.From(activity).Inflate(Resource.Layout.sample_list_detail_list, container, false);
                listView.Adapter = adapter;
                listView.ScrollStateChanged += (sender, e) =>
                {
                    Picasso picasso = Picasso.Get();
                    if (e.ScrollState == ScrollState.Idle || e.ScrollState == ScrollState.TouchScroll)
                    {
                        picasso.ResumeTag(activity);
                    }
                    else
                    {
                        picasso.PauseTag(activity);
                    }
                };
                listView.ItemClick += (sender, e) =>
                {
                    string url = adapter[e.Position];
                    activity.ShowDetails(url);
                };
                return listView;
            }
        }

        public class DetailFragment : Fragment
        {
            private const string UrlKey = "picasso:url";

            public static DetailFragment NewInstance(string url)
            {
                Bundle arguments = new Bundle();
                arguments.PutString(UrlKey, url);

                DetailFragment fragment = new DetailFragment();
                fragment.Arguments = arguments;
                return fragment;
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                Activity activity = Activity;

                View view = LayoutInflater.From(activity).Inflate(Resource.Layout.sample_list_detail_detail, container, false);

                TextView urlView = view.FindViewById<TextView>(Resource.Id.url);
                ImageView imageView = view.FindViewById<ImageView>(Resource.Id.photo);

                string url = Arguments.GetString(UrlKey);

                urlView.Text = url;
                Picasso.Get()
                       .Load(url)
                       .Fit()
                       .Tag(activity)
                       .Into(imageView);

                return view;
            }
        }
    }
}
