namespace Firebase.JobDispatcher
{
    /// <summary>
    ///     These values mimic those from  <code>com.firebase.jobdispatcher.JobService.JobResult</code>.s
    /// </summary>
    public enum JobResultEnum
    {
        Success = 0,
        FailRetry = 1,
        FailNoRetry = 2
    }
}