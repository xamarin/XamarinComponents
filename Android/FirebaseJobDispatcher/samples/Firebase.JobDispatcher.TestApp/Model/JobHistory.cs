using System;
using System.Collections.Generic;
using Firebase.JobDispatcher;

namespace FJDTestApp.Model
{
    /// <summary>
    ///     A simple class for storing the results (in memory) when a job is run for a given set of IJobParameters.
    /// </summary>
    public class JobHistory
    {
        readonly List<Result> results = new List<Result> ();

        public JobHistory (IJobParameters job)
        {
            if (job == null) 
            {
                throw new NullReferenceException ("Must provide a valid IJobParameters instance!");
            }
            Job = job;
        }

        public int ResultCount => results.Count;

        public IJobParameters Job { get; }

        public void RecordResult (int result)
        {
            results.Add (new Result (result, DateTime.UtcNow.Ticks));
        }

        public override string ToString ()
        {
            return $"[JobHistory: Service({Job.Service})/Tag({Job.Tag})]";
        }

        public override int GetHashCode ()
        {
            return Job.Service.GetHashCode () + 11 * Job.Tag.GetHashCode ();
        }

        public bool IsFor (IJobParameters job)
        {
            if (job == null) {
                return false;
            }

            int jobHashCode = job.Service.GetHashCode () + 11 * job.Tag.GetHashCode ();

            return GetHashCode () == jobHashCode;
        }

        public override bool Equals (object obj)
        {
            var jh = obj as JobHistory;
            if (jh == null) {
                return false;
            }

            return GetHashCode () == jh.GetHashCode ();
        }

        public class Result
        {
            readonly long elapsedTime;
            readonly int result;

            public Result (int result, long elapsedTime)
            {
                this.result = result;
                this.elapsedTime = elapsedTime;
            }
        }
    }
}