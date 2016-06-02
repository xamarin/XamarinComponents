using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;
using NineOldAndroids.View;

using StickyListHeaders;

namespace StickyListHeadersSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        private StickyAdapter adapter;
        private bool fadeHeader = true;
        private ActionBarDrawerToggle drawerToggle;

        [Splice(Resource.Id.drawer_layout)]
        private DrawerLayout drawerLayout;

        [Splice(Resource.Id.list)]
        private StickyListHeadersListView stickyList;

        [Splice(Resource.Id.restore_button)]
        private Button restoreButton;
        [Splice(Resource.Id.update_button)]
        private Button updateButton;
        [Splice(Resource.Id.clear_button)]
        private Button clearButton;
        [Splice(Resource.Id.open_expandable_list_button)]
        private Button openExpandableListButton;

        [Splice(Resource.Id.sticky_checkBox)]
        private CheckBox stickyCheckBox;
        [Splice(Resource.Id.fade_checkBox)]
        private CheckBox fadeCheckBox;
        [Splice(Resource.Id.draw_behind_checkBox)]
        private CheckBox drawBehindCheckBox;
        [Splice(Resource.Id.fast_scroll_checkBox)]
        private CheckBox fastScrollCheckBox;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainLayout);

            // inject views
            Geneticist.Splice(this);

            // set up chrome
            drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            //var styledAttributes = Theme.ObtainStyledAttributes(new[] { Android.Resource.Attribute.ActionBarSize });
            //var actionBarSize = (int)styledAttributes.GetDimension(0, 0);
            //styledAttributes.Recycle();

            // set up list view
            stickyList.EmptyView = FindViewById(Resource.Id.empty);
            stickyList.DrawingListUnderStickyHeader = true;
            stickyList.AreHeadersSticky = true;
            stickyList.StickyHeaderTopOffset = 0;// -20;
            stickyList.AddHeaderView(LayoutInflater.Inflate(Resource.Layout.ListHeader, null));
            stickyList.AddFooterView(LayoutInflater.Inflate(Resource.Layout.ListFooter, null));
            stickyList.ItemClick += (sender, e) =>
                Toast.MakeText(this, "Item " + e.Position + " clicked!", ToastLength.Short).Show();
            stickyList.HeaderClick += (sender, e) =>
                Toast.MakeText(this, "Header " + e.HeaderId + " currentlySticky ? " + e.CurrentlySticky, ToastLength.Short).Show();
            stickyList.StickyHeaderChanged += (sender, e) => ViewHelper.SetAlpha(e.Header, 1);
            stickyList.StickyHeaderOffsetChanged += (sender, e) =>
            {
                if (fadeHeader)
                {
                    ViewHelper.SetAlpha(e.Header, 1 - (e.Offset / (float)e.Header.MeasuredHeight));
                }
            };

            // set up data
            adapter = new StickyAdapter(this);
            stickyList.Adapter = adapter;
            
            // actions
            openExpandableListButton.Click += (sender, e) => StartActivity(new Intent(this, typeof(ExpandableListActivity)));
            restoreButton.Click += (sender, e) => adapter.Restore();
            updateButton.Click += (sender, e) => adapter.NotifyDataSetChanged();
            clearButton.Click += (sender, e) => adapter.Clear();
            stickyCheckBox.CheckedChange += (sender, e) => stickyList.AreHeadersSticky = e.IsChecked;
            fadeCheckBox.CheckedChange += (sender, e) => fadeHeader = e.IsChecked;
            drawBehindCheckBox.CheckedChange += (sender, e) => stickyList.DrawingListUnderStickyHeader = e.IsChecked;
            fastScrollCheckBox.CheckedChange += (sender, e) =>
            {
                stickyList.SetFastScrollEnabled(e.IsChecked);
                stickyList.FastScrollAlwaysVisible = e.IsChecked;
            };
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            // Sync the toggle state after onRestoreInstanceState has occurred.
            drawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            drawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (drawerToggle.OnOptionsItemSelected(item))
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
