using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Util;
using Firebase.JobDispatcher;

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
            // Beware: This runs on the main thread. 

            int position = jobParameters.Extras.GetInt(FibonacciPositionKey, DEFAULT_VALUE);

            LogMessage($"Starting FibonacciCalculatorJob::OnStartJob. Job tag {jobParameters.Tag}.");
//            long result = Task.Run(() =>GetFibonacciNumberAt(position)).GetAwaiter().GetResult();

            long result = GetFibonacciNumberAt(position);

            LogMessage($"Finished FibonacciCalculatorJob::OnStartJob. The fibonacci number at {position} is {result}. Job tag {jobParameters.Tag}.");

            // For some reason call this on the main thread will throw an exception

            try
            {
                JobFinished(jobParameters, false);
            }
            catch (Exception e)
            {
                Log.Error(TAG, $"What?! {e.Message}" );
            }

            LogMessage("Leaving FibonacciCalculatorJob::OnStartJob.");
            // No more work to do.
            return false;
        }

        long  GetFibonacciNumberAt(int position)
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