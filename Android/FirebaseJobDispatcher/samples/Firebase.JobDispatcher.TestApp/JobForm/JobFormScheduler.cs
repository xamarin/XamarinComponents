using System;
using Android.Content;
using Android.OS;
using Android.Util;
using Firebase.JobDispatcher;

namespace FJDTestApp.JobForm
{
    /// <summary>
    ///     This class schedule a job the Firebase Job Dispatcher according to the the
    ///     values provided by a <code>IJobParametersView</code>.
    /// </summary>
    class JobFormScheduler
    {
        static readonly string TAG = typeof(JobFormScheduler).FullName;
        bool constrainDeviceCharging;

        bool constrainOnAnyNetwork;
        bool constrainOnUnmeteredNewtwork;
        int initialBackoffSeconds;
        bool initialized;
        int lifeTime;
        int maximumBackoffSeconds;
        bool recurring;
        bool replaceCurrent;
        string tag;
        bool useLinearBackoffStrategy;

        int winEndSeconds;
        int winStartSeconds;

        /// <summary>
        ///     Creates a JobFormScheduler based on the triggers and constraints specified in
        ///     the view.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        internal static JobFormScheduler WithParametersFrom(IJobParametersView view)
        {
            JobFormScheduler jf = new JobFormScheduler
            {
                winEndSeconds = view.WindowStartSeconds,
                winStartSeconds = view.WindowStartSeconds,
                initialBackoffSeconds = view.InitialBackoffSeconds,
                maximumBackoffSeconds = view.MaximumBackoffSeconds,
                tag = view.JobTag,
                lifeTime = view.Persistent ? Lifetime.Forever : Lifetime.UntilNextBoot,
                recurring = view.Recurring,
                replaceCurrent = view.ReplaceCurrent,
                constrainOnAnyNetwork = view.ConstrainOnAnyNetwork,
                constrainDeviceCharging = view.ConstrainDeviceCharging,
                constrainOnUnmeteredNewtwork = view.ConstrainOnUnmeteredNetwork,
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
        internal void SubmitJob<T>(Context context) where T : JobService
        {
            if (!initialized)
            {
                throw new InvalidOperationException(
                    "Must initialize the parameters with JobFormScheduler.WithParametersFrom before trying to schedule the job!");
            }

            FirebaseJobDispatcher dispatcher = context.CreateJobDispatcher();

            RetryStrategy retryStrategy = BuildRetryStrategy(dispatcher);
            if (retryStrategy == null)
            {
                throw new InvalidCastException("Cannot create a RetryStrategy!");
            }

            JobTrigger trigger = Trigger.ExecutionWindow(winStartSeconds, winEndSeconds);


            var jobParameters = new Bundle();
            jobParameters.PutInt(FibonacciCalculatorJob.FibonacciPositionKey, 25);

            Job.Builder builder = dispatcher.NewJobBuilder()
                .SetService<T>(tag)
                .SetRecurring(recurring)
                .SetLifetime(lifeTime)
                .SetRetryStrategy(retryStrategy)
                .SetTrigger(trigger)
                .SetExtras(jobParameters)
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

            int scheduleResult = dispatcher.Schedule(builder.Build());
            string message =
                $"Scheduled new job `{tag}` for the service `{typeof(T).Name}`. SCHEDULE_RESULT = {scheduleResult}.";

            if (scheduleResult == FirebaseJobDispatcher.ScheduleResultSuccess)
            {
                Log.Info(TAG, message);
            }
            else
            {
                Log.Error(TAG, message);
            }
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