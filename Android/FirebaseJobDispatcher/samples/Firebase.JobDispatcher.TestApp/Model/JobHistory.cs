using System;
using System.Collections.Generic;
using System.Linq;
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
            Job = job ?? throw new NullReferenceException ("Must provide a valid IJobParameters instance!");
        }

        public int ResultCount => results.Count;

        public IJobParameters Job { get; }

        public void RecordResult (string result)
        {
            results.Add (new Result (result, DateTime.UtcNow.Ticks));
        }

        public string GetLastResult()
        {
            if (results.Any())
            {
                Result newest = results.Last();
                return newest.ToString();
            }
            return "";
        }

        public override string ToString ()
        {
            return $"{Job.Service} / {Job.Tag}";
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
            public Result (string message, long elapsedTime)
            {
                Message = message;
                ElapsedTime = elapsedTime;
            }

            public long ElapsedTime { get; }
            public string Message { get; }

            public override string ToString()
            {
                return $"{Message} ({ElapsedTime})";
            }

            protected bool Equals(Result other)
            {
                return ElapsedTime == other.ElapsedTime && string.Equals(Message, other.Message, StringComparison.OrdinalIgnoreCase);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Result) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (ElapsedTime.GetHashCode() * 397) ^ Message.GetHashCode();
                }
            }

            public static bool operator ==(Result left, Result right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Result left, Result right)
            {
                return !Equals(left, right);
            }
        }
    }
}