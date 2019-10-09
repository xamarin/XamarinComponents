using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace OfficeUIFabricSampleDroid
{
    public abstract class DemoActivity : AppCompatActivity
    {
        protected abstract int ContentLayoutId { get; }

        protected virtual bool ContentNeedsScrollableContainer { get; } = true;

        Android.Support.V7.Widget.Toolbar detail_toolbar;
        Android.Support.V4.Widget.NestedScrollView demo_detail_scrollable_container;
        LinearLayout demo_detail_container;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_demo_detail);

            detail_toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.detail_toolbar);
            demo_detail_scrollable_container = FindViewById<Android.Support.V4.Widget.NestedScrollView>(Resource.Id.demo_detail_scrollable_container);
            demo_detail_container = FindViewById<LinearLayout>(Resource.Id.demo_detail_container);

            SetSupportActionBar(detail_toolbar);

            // Show the Up button in the action bar.
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);

            // Set demo title
            var demoID = Intent.GetStringExtra("DEMO_ID");
            var demo = Demo.Demos.FirstOrDefault(d => d.Id == demoID);
            Title = demo?.Title;

            // Load content and place it in the requested container
            var container = ContentNeedsScrollableContainer ? (ViewGroup)demo_detail_scrollable_container : demo_detail_container;
            LayoutInflater.Inflate(ContentLayoutId, container, true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home) {
                NavigateUpTo(new Intent(this, typeof(DemoListActivity)));
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}