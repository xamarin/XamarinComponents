using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;

namespace PhotoViewSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_launcher);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.app_name);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(new ItemAdapter());
        }

        private class ItemAdapter : RecyclerView.Adapter
        {
            private static List<KeyValuePair<string, Type>> options = new Dictionary<string, Type>
            {
                {"Simple Sample", typeof(SimpleSampleActivity)},
                {"ViewPager Sample", typeof(ViewPagerSampleActivity)},
                {"Rotation Sample", typeof(RotationSampleActivity)},
                {"Picasso Sample", typeof(PicassoSampleActivity)},
                {"Activity Transition Sample", typeof(ActivityTransitionSampleActivity)},
                {"Immersive Sample", typeof(ImmersiveSampleActivity)},
            }.ToList();

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                return ItemViewHolder.Create(parent);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                ((ItemViewHolder) holder).Bind(options[position]);
            }

            public override int ItemCount => options.Count;
        }

        private class ItemViewHolder : RecyclerView.ViewHolder
        {
            private readonly TextView textTitle;
            private Type activityType;

            public static ItemViewHolder Create(ViewGroup parent)
            {
                var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_sample, parent, false);
                return new ItemViewHolder(view);
            }

            public ItemViewHolder(View view)
                : base(view)
            {
                textTitle = view.FindViewById<TextView>(Resource.Id.title);

                ItemView.Click += (sender, e) => { ItemView.Context.StartActivity(new Intent(ItemView.Context, activityType)); };
            }

            public void Bind(KeyValuePair<string, Type> option)
            {
                textTitle.Text = option.Key;
                activityType = option.Value;
            }
        }
    }
}