using System;
using Android.Content;
using Android.Util;
using Firebase.JobDispatcher;
using Java.Lang;
using Exception = System.Exception;

namespace FJDTestApp
{
    /// <summary>
    ///     This is a helper class that will schedule a job the Firebase Job Dispatcher according to the the
    ///     values provided by a <code>IJobParametersView</code>.
    /// </summary>
    class FirebaseJob
    {
        static readonly string TAG = typeof(FirebaseJob).FullName;
        bool constrainDeviceCharging;

        bool constrainOnAnyNetwork;
        bool constrainOnUnmeteredNewtwork;
        int initialBackoffSeconds;
        bool initialized;
        int lifeTime;
        int maximumBackoffSeconds;
        bool persistent;
        bool recurring;
        bool replaceCurrent;
        string tag;
        bool useLinearBackoffStrategy;

        int winEndSeconds;
        int winStartSeconds;


        internal static FirebaseJob WithParametersFrom(IJobParametersView view)
        {
            var jf = new FirebaseJob
            {
                winEndSeconds = view.WindowStartSeconds,
                winStartSeconds = view.WindowStartSeconds,
                initialBackoffSeconds = view.InitialBackoffSeconds,
                maximumBackoffSeconds = view.MaximumBackoffSeconds,
                tag = view.JobTag,
                recurring = view.Recurring,
                replaceCurrent = view.ReplaceCurrent,
                constrainOnAnyNetwork = view.ConstrainOnAnyNetwork,
                constrainDeviceCharging = view.ConstrainDeviceCharging,
                constrainOnUnmeteredNewtwork = view.ConstrainOnUnmeteredNetwork,
                persistent = view.Persistent,
                useLinearBackoffStrategy = view.UseLinearBackoffStrategy,
                initialized = true
            };

            return jf;
        }

        /// <summary>
        ///     Schedules a job to run on the JobService subclass.
        /// </summary>
        /// <param name="context">The context that is request the job.</param>
        /// <typeparam name="T">A <code>JobService</code> subclass.</typeparam>
        internal void Schedule<T>(Context context) where T : JobService
        {
            if (!initialized)
            {
                throw new InvalidOperationException(
                    "Must initialize the parameters with FirebaseJob.WithParametersFrom before trying to schedule the job!");
            }

            Type managedJobServiceType = typeof(T);
            Class javaServiceClass = managedJobServiceType.Translate();

            IDriver driver = new GooglePlayDriver(context);
            var dispatcher = new FirebaseJobDispatcher(driver);

            RetryStrategy retryStrategy = BuildRetryStrategy(dispatcher);
            if (retryStrategy == null)
            {
                throw new InvalidCastException("Cannot create a RetryStrategy!");
            }

            JobTrigger trigger = Trigger.ExecutionWindow(winStartSeconds, winEndSeconds);

            Job.Builder builder = dispatcher.NewJobBuilder()
                .SetTag(tag)
                .SetRecurring(recurring)
                .SetLifetime(lifeTime)
                .SetService(javaServiceClass)
                .SetRetryStrategy(retryStrategy)
                .SetTrigger(trigger)
                .SetReplaceCurrent(replaceCurrent);

            if (constrainDeviceCharging)
            {
                builder.SetConstraints(Constraint.DeviceCharging);
            }

            if (constrainOnAnyNetwork)
            {
                builder.SetConstraints(Constraint.OnAnyNetwork);
            }

            if (constrainOnUnmeteredNewtwork)
            {
                builder.SetConstraints(Constraint.OnUnmeteredNetwork);
            }

            Log.Info(TAG, $"Scheduling new job `{tag}` for the service '{managedJobServiceType.Name}'.");

            dispatcher.MustSchedule(builder.Build());
        }

        RetryStrategy BuildRetryStrategy(FirebaseJobDispatcher dispatcher)
        {
            int policy = useLinearBackoffStrategy
                ? RetryStrategy.RetryPolicyLinear
                : RetryStrategy.RetryPolicyExponential;

            RetryStrategy retryStrategy;
            try
            {
                retryStrategy = dispatcher.NewRetryStrategy(policy, initialBackoffSeconds, maximumBackoffSeconds);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.ToString());
                retryStrategy = null;
            }
            return retryStrategy;
        }
    }
}
// ex.ToString()	"Firebase.JobDispatcher.ValidationEnforcer+ValidationException: JobParameters is invalid: Maximum backoff must be greater than 300s (5 minutes)\n  - Initial backoff must be at least 30s\n  at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x0000c] in /Users/builder/data/lanes/4009/f3074d2c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/exceptionservices/exceptionservicescommon.cs:143 \n  at Java.Interop.JniEnvironment+InstanceMethods.CallObjectMethod (Java.Interop.JniObjectReference instance, Java.Interop.JniMethodInfo method, Java.Interop.JniArgumentValue* args) [0x00085] in /Users/builder/data/lanes/4009/9578cdcd/source/Java.Interop/src/Java.Interop/Java.Interop/JniEnvironment.g.cs:11283 \n  at Android.Runtime.JNIEnv.CallObjectMethod (System.IntPtr jobject, System.IntPtr jmethod, Android.Runtime.JValue* parms) [0x00000] in /Users/builder/data/lanes/4009/9578cdcd/source/monodroid/src/Mono.Android/JNIEnv.g.cs:102 \n  at Firebase.JobDispatcher.FirebaseJobDispatcher.NewRetryStrategy (System.Int32 p0, System.Int32 p1, System.Int32 p2) [0x00074] in <95ddd0f578c04bcba430743f19053055>:0 \n  at FJDTestApp.FirebaseJob.BuildRetryStrategy (Firebase.JobDispatcher.FirebaseJobDispatcher dispatcher) [0x00023] in /Users/tom/work/xamarin/code/XamarinComponents/Android/FirebaseJobDispatcher/samples/Firebase.JobDispatcher.TestApp/JobForm/FirebaseJob.cs:119 \n  --- End of managed Firebase.JobDispatcher.ValidationEnforcer+ValidationException stack trace ---\ncom.firebase.jobdispatcher.ValidationEnforcer$ValidationException: JobParameters is invalid: Maximum backoff must be greater than 300s (5 minutes)\n  - Initial backoff must be at least 30s\n\tat com.firebase.jobdispatcher.ValidationEnforcer.ensureNoErrors(ValidationEnforcer.java:113)\n\tat com.firebase.jobdispatcher.ValidationEnforcer.ensureValid(ValidationEnforcer.java:108)\n\tat com.firebase.jobdispatcher.RetryStrategy$Builder.build(RetryStrategy.java:102)\n\tat com.firebase.jobdispatcher.FirebaseJobDispatcher.newRetryStrategy(FirebaseJobDispatcher.java:175)\n\tat mono.android.view.View_OnClickListenerImplementor.n_onClick(Native Method)\n\tat mono.android.view.View_OnClickListenerImplementor.onClick(View_OnClickListenerImplementor.java:30)\n\tat android.view.View.performClick(View.java:5204)\n\tat android.view.View$PerformClick.run(View.java:21153)\n\tat android.os.Handler.handleCallback(Handler.java:739)\n\tat android.os.Handler.dispatchMessage(Handler.java:95)\n\tat android.os.Looper.loop(Looper.java:148)\n\tat android.app.ActivityThread.main(ActivityThread.java:5417)\n\tat java.lang.reflect.Method.invoke(Native Method)\n\tat com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:726)\n\tat com.android.internal.os.ZygoteInit.main(ZygoteInit.java:616)\n"	string