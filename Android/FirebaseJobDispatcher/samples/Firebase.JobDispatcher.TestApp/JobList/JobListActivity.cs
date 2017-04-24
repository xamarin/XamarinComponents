using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using FJDTestApp.Model;

namespace FJDTestApp.JobList
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class JobListActivity : AppCompatActivity
    {
        JobHistoryAdapter jobHistoryAdapter;
        RecyclerView.LayoutManager layoutManager;
        RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_job_list);

            jobHistoryAdapter = new JobHistoryAdapter();

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += OnAddButtonClicked;

            layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.job_list_recyclerview);
            recyclerView.HasFixedSize = false;
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(jobHistoryAdapter);
        }

        protected override void OnResume()
        {
            base.OnResume();
            TestAppApplication.JobHistoryStorage.OnChangeEvent += JobHistoryStorage_OnChangeEvent;
            jobHistoryAdapter.NotifyDataSetChanged();
        }

        void OnAddButtonClicked(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(JobFormActivity)));
        }

        protected override void OnPause()
        {
            TestAppApplication.JobHistoryStorage.OnChangeEvent -= JobHistoryStorage_OnChangeEvent;
            base.OnPause();
        }

        void JobHistoryStorage_OnChangeEvent(object sender, JobHistoryStoreChangedEventArgs e)
        {
            jobHistoryAdapter.NotifyDataSetChanged();
        }
    }
}