using Google.Common.Util.Concurrent;
using Java.Lang;
using Java.Util.Concurrent;

namespace GuavaSample
{
    // Just ensure this compiles
    public class Future : Java.Lang.Object, IListenableFuture
    {
        public bool IsCancelled { get; set; }

        public bool IsDone { get; set; }

        public void AddListener(IRunnable p0, IExecutor p1)
        {
            
        }

        public bool Cancel(bool mayInterruptIfRunning) => true;

        public Object Get() => null;

        public Object Get(long timeout, TimeUnit unit) => null;
    }
}