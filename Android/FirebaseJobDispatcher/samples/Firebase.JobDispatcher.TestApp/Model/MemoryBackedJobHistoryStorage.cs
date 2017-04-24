using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.JobDispatcher;

namespace FJDTestApp.Model
{
    /// <summary>
    ///     Memory backed job history store.
    /// </summary>
    public class MemoryBackedJobHistoryStorage : IJobHistoryStorage
    {
        readonly List<JobHistory> jobHistories;

        public MemoryBackedJobHistoryStorage()
        {
            jobHistories = new List<JobHistory>();
        }

        public int Count => jobHistories.Count();

        public event EventHandler<JobHistoryStoreChangedEventArgs> OnChangeEvent;

        public void Add(JobHistory jobHistory)
        {
            jobHistories.Add(jobHistory);
            NotifyChanged();
        }

        public void Remove(int position)
        {
            jobHistories.RemoveAt(position);
            NotifyChanged();
        }

        public IEnumerator<JobHistory> GetEnumerator()
        {
            return jobHistories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return jobHistories.GetEnumerator();
        }

        public IEnumerable<JobHistory> GetAll()
        {
            return jobHistories;
        }

        public JobHistory Get(int index)
        {
            return jobHistories[index];
        }

        public void RecordResult(IJobParameters job, int result)
        {
            if (job == null)
            {
                return;
            }
            JobHistory jobHistory;
            if (jobHistories.Any(arg => arg.IsFor(job)))
            {
                jobHistory = jobHistories.First(arg => arg.IsFor(job));
            }
            else
            {
                jobHistory = new JobHistory(job);
                jobHistories.Add(jobHistory);
            }

            jobHistory.RecordResult(result);
            NotifyChanged();
        }

        void NotifyChanged()
        {
            OnChangeEvent?.Invoke(this, new JobHistoryStoreChangedEventArgs());
        }
    }
}