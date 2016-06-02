using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using DeviceYearClass;

namespace DeviceYearClassSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var yearClass = FindViewById<TextView>(Resource.Id.yearClass);
            yearClass.Text = await Task.Run(() =>
            {
                var year = YearClass.GetDeviceYearClass(ApplicationContext);
                return year.ToString();
            });
        }
    }
}
