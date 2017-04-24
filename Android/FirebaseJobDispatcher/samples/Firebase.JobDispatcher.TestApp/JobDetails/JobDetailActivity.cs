using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.JobDispatcher;

namespace FJDTestApp.JobDetails
{
    [Activity(Label = "Schedule a Job")]
    public class JobDetailActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IJobParameters job = GetCurrentJob();

            SupportActionBar.Title = job.Tag;
            SetContentView(CreateViewForJob(job));
        }

        IJobParameters GetCurrentJob()
        {
            int pos = CurrentJobParameter();
            IJobParameters job = TestAppApplication.JobHistoryStorage.Get(pos).Job;
            if (job == null)
            {
                throw new ArgumentException($"No IJobParmeters at {pos}!");
            }
            return job;
        }


        View CreateViewForJob(IJobParameters job)
        {
            var tagRow = new TableRow(this);
            var tagTV = new TextView(this)
            {
                Text = $"TAG = ${job.Tag}"
            };
            tagRow.AddView(tagTV);

            var serviceRow = new TableRow(this);
            var serviceTV = new TextView(this) {Text = $"SERVICE = ${job.Service}"};
            serviceRow.AddView(serviceTV);

            var layout = new TableLayout(this);
            layout.AddView(tagRow);
            layout.AddView(serviceRow);
            return layout;
        }

        int CurrentJobParameter()
        {
            int? pos = Intent?.Extras?.GetInt("pos", -1);
            if (pos.HasValue)
            {
                if (pos.Value == -1)
                {
                    throw new ArgumentException("Expected the value `pos` to be in the Bundle.Extras!");
                }
            }
            else
            {
                throw new NullReferenceException(
                    "Expected to have a Intent with a Bundle and Extras, one or both are missing.!");
            }
            return pos.Value;
        }
    }
}