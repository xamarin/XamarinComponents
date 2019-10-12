using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class DemoListActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        RecyclerView demo_list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.AppTheme);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_demo_list);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            demo_list = FindViewById<RecyclerView>(Resource.Id.demo_list);

            SetSupportActionBar(toolbar);
            toolbar.Title = Title;
            toolbar.Subtitle = Microsoft.OfficeUIFabric.BuildConfig.VersionName;

            demo_list.SetAdapter(new DemoListAdapter());
            demo_list.AddItemDecoration(new ListItemDivider(this, DividerItemDecoration.Vertical));
        }

        class DemoListAdapter : RecyclerView.Adapter
        {
            public override int ItemCount => Demo.Demos.Count;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var demo = Demo.Demos[position];
                var vh = (holder as ViewHolder);
                vh.ListItem.Title = demo.Title;
                vh.ItemView.Tag = demo;
                vh.ItemView.Click += ItemView_Click;
            }

            private void ItemView_Click(object sender, EventArgs e)
            {
                var view = sender as View;
                var demo = view.Tag as Demo;
                var intent = new Intent(view.Context, demo.ActivityType);
                intent.PutExtra("DEMO_ID", demo.Id);
                view.Context.StartActivity(intent);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var listItemView = new ListItemView(parent.Context);
                listItemView.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                return new ViewHolder(listItemView);
            }

            class ViewHolder : RecyclerView.ViewHolder
            {
                public ViewHolder(View view) : base(view)
                {
                    ListItem = view as ListItemView;
                }

                public ListItemView ListItem { get; set; }
            }
        }
    }
}