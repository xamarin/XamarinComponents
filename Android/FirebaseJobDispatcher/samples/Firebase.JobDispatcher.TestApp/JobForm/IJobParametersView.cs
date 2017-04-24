namespace FJDTestApp
{
    internal interface IJobParametersView
    {
        bool ConstrainOnAnyNetwork { get; }
        bool ConstrainOnUnmeteredNetwork { get; }
        bool ConstrainDeviceCharging { get; }
        bool Recurring { get; }
        bool Persistent { get; }
        bool ReplaceCurrent { get; }
        bool UseLinearBackoffStrategy { get; }
        string JobTag { get; }
        int WindowStartSeconds { get; }
        int WindowEndSeconds { get; }
        int InitialBackoffSeconds { get; }
        int MaximumBackoffSeconds { get; }
    }
}