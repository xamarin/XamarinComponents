using Android.App;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using CodeCrafters.TableViews;
using CodeCrafters.TableViews.Toolkit;

using SortableTableViewSample.Data;

namespace SortableTableViewSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private readonly Database database = new Database();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
            }

            var carTableView = FindViewById<SortableTableView>(Resource.Id.tableView);

            var simpleTableHeaderAdapter = new SimpleTableHeaderAdapter(this, "Manufacturer", "Name", "Performance", "Price");
            simpleTableHeaderAdapter.SetTextColor(ContextCompat.GetColor(this, Resource.Color.table_header_text));
            carTableView.HeaderAdapter = simpleTableHeaderAdapter;

            var rowColorEven = ContextCompat.GetColor(this, Resource.Color.table_data_row_even);
            var rowColorOdd = ContextCompat.GetColor(this, Resource.Color.table_data_row_odd);
            carTableView.SetDataRowBackgroundProvider(TableDataRowBackgroundProviders.AlternatingRowColors(rowColorEven, rowColorOdd));
            carTableView.HeaderSortStateViewProvider = SortStateViewProviders.BrightArrows();

            carTableView.SetColumnWeight(0, 2);
            carTableView.SetColumnWeight(1, 3);
            carTableView.SetColumnWeight(2, 3);
            carTableView.SetColumnWeight(3, 2);

            carTableView.SetColumnComparator(0, database.GetCarProducerComparator());
            carTableView.SetColumnComparator(1, database.GetCarNameComparator());
            carTableView.SetColumnComparator(2, database.GetCarPowerComparator());
            carTableView.SetColumnComparator(3, database.GetCarPriceComparator());
            carTableView.DataAdapter = new CarTableDataAdapter(this, database.Cars);
            carTableView.DataClick += (sender, e) =>
            {
                var car = (Car)e.ClickedData;
                var carString = car.Producer.Name + " " + car.Name;
                Toast.MakeText(this, carString, ToastLength.Short).Show();
            };
        }
    }
}
