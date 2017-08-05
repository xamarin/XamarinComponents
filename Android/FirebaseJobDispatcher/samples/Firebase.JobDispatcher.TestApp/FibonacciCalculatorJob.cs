using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Util;
using Firebase.JobDispatcher;
using FJDTestApp.Model;

namespace FJDTestApp
{
    [Service(Name = "com.xamarin.fjdtestapp.FibonacciCalculatorJob")]
    [IntentFilter(new[] {FirebaseJobServiceIntent.Action})]
    public class FibonacciCalculatorJob : JobService
    {
        const int DEFAULT_VALUE = 50;
        static readonly string TAG = "X:FibonacciCalculatorJob";
        public static string FibonacciPositionKey = "fibonnaci_position";

        public override bool OnStartJob(IJobParameters jobParameters)
        {
            int position = jobParameters.Extras.GetInt(FibonacciPositionKey, DEFAULT_VALUE);

            LogMessage($"Starting FibonacciCalculatorJob::OnStartJob. Job tag {jobParameters.Tag}.");

            // Look for the Fibonacci number on a thread - don't bog down the main thread.
            Task.Run(() => GetFibonacciNumberAt(position)).
                ContinueWith(task =>
                {
                    // Record the result, so that it will show up in the Main activity.
                    IJobHistoryStorage storage = FJDTestApplication.JobHistoryStorage;
                    string msg = $"The fibonnaci number at {position} is {task.Result}";
                    storage.RecordResult(jobParameters, msg);

                    // Have to call JobFinished when the thread is done. This lets the FJD know that
                    // the work is done.
                    JobFinished(jobParameters, false);                
                    LogMessage(
                        $"Finished FibonacciCalculatorJob::OnStartJob/Job tag {jobParameters.Tag}. {msg}");
                }, TaskScheduler.FromCurrentSynchronizationContext());

            LogMessage("Leaving FibonacciCalculatorJob::OnStartJob.");
            

            return false; // No more work to do, so return false.
        }

        long GetFibonacciNumberAt(int position)
        {
            long firstNumber = 0;
            long secondNumber = 1;
            long result = 0;

            LogMessage($"Calculating the fibonacci number at position {position}.");

            switch (position)
            {
                case 0:
                    result = 0;
                    break;
                case 1:
                    result = 1;
                    break;
                default:
                    for (int i = 2; i < position; i++)
                    {
                        result = firstNumber + secondNumber;
                        firstNumber = secondNumber;
                        secondNumber = result;
                    }
                    break;
            }

            return result;
        }

        static void LogMessage(string message)
        {
            string currentTime = DateTime.Now.ToString("yy-MM-dd hh:mm:ss.FFF");
            Log.Debug(TAG, $"{currentTime} | TID #{Thread.CurrentThread.ManagedThreadId} | {message}");
        }

        public override bool OnStopJob(IJobParameters jobParameters)
        {
            Log.Debug(TAG, "FibonacciCalculatorJob::OnStopJob");
            // nothing to do.
            return false;
        }
    }
}