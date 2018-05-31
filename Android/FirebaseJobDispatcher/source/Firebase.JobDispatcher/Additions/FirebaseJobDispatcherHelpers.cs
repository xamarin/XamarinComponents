using System;
using Android.Content;

// ReSharper disable once CheckNamespace
namespace Firebase.JobDispatcher
{
    public static class FirebaseJobDispatcherHelpers
    {

        /// <summary>
        /// Creates a <code>FirebaseJobDispatcher</code> object.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static FirebaseJobDispatcher CreateJobDispatcher(this Context context)
        {
            IDriver driver = new GooglePlayDriver(context);
            return new FirebaseJobDispatcher(driver);
        }

        /// <summary>
        ///  Extension method that will provide a Job.Builder with the type of JobService to schedule, along with a "tag" 
        /// that will identify the JobService.
        /// </summary>
        /// <typeparam name="T">The managed C# type of the JobService to schedule.</typeparam>
        /// <param name="builder">A Job.Builder that will be used to help the JobDispatcher schedule a job.</param>
        /// <param name="tag">A unique tag to identify a scheduled job.</param>
        /// <returns>The Job.Builder.</returns>
        public static Job.Builder SetService<T>(this Job.Builder builder, string tag) where T : JobService
        {
            return builder.SetService<T>()
                .SetTag(tag);
        }

        /// <summary>
        ///  Extension method that will provide a Job.Builder with the type of JobService to schedule.
        /// </summary>
        /// <typeparam name="T">The managed C# type of the JobService to schedule.</typeparam>
        /// <param name="builder">A Job.Builder that will be used to help the JobDispatcher schedule a job.</param>
        /// <returns>The Job.Builder.</returns>
        public static Job.Builder SetService<T>(this Job.Builder builder) where T : JobService
        {
            Type csharpJobServiceClass = typeof(T);
            Java.Lang.Class javaServiceClass = Java.Lang.Class.FromType(csharpJobServiceClass);
            return builder.SetService(javaServiceClass);
        }
    }
}