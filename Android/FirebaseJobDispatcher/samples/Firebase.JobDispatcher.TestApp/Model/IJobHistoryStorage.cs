using System;
using System.Collections.Generic;
using Firebase.JobDispatcher;

namespace FJDTestApp.Model
{
    /// <summary>
    ///     This interface is implemented by classes that will save and retrive the JobHistory instances.
    /// </summary>
    public interface IJobHistoryStorage : IEnumerable<JobHistory>
    {
        event EventHandler<JobHistoryStoreChangedEventArgs> OnChangeEvent;

        void Add(JobHistory jobHistory);
        IEnumerable<JobHistory> GetAll();
        void Remove(int position);
        JobHistory Get(int index);
        void RecordResult(IJobParameters job, int result);
    }
}