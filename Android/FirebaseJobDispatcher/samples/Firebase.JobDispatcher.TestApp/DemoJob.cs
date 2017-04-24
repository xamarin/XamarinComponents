using System;
using Android.App;
using Android.Util;
using Firebase.JobDispatcher;

namespace FJDTestApp
{
    [Service(Name = "com.xamarin.fjdtestapp.DemoJob")]
    [IntentFilter(new[] {FirebaseJobServiceIntent.Action})]
    public class DemoJob : JobService
    {
        static readonly string TAG = "X:DemoService";

        public override bool OnStartJob(IJobParameters p0)
        {
            Log.Debug(TAG, "DemoJob::OnStartJob");
            // Beware: This runs on the main thread. 
            if (p0.Extras == null)
            {
                throw new NullReferenceException("Expected there to be Extras in the job");
            }

            int result = p0.Extras.GetInt("return");

            TestAppApplication.JobHistoryStorage.RecordResult(p0, result);

            // No more work to do.
            return false;
        }

        public override bool OnStopJob(IJobParameters p0)
        {
            Log.Debug(TAG, "DemoJob::OnStartJob");
            // nothing to do.
            return false;
        }
    }
}