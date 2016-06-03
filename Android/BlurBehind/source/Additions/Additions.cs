using System;
using System.Threading.Tasks;
using Android.App;

namespace BlurBehindSdk
{
    public partial class BlurBehind
    {
        public Task ExecuteAsync (Activity activity)
        {
            var tcsBlur = new TaskCompletionSource<object> ();

            var listener = new BlurCompleteListener {
                BlurCompleteHandler = () => tcsBlur.SetResult (null)
            };

            Execute (activity, listener);

            return tcsBlur.Task;
        }

        public void Execute (Activity activity, Action blurCompleteCallback)
        {
            var listener = new BlurCompleteListener {
                BlurCompleteHandler = () => blurCompleteCallback?.Invoke ()
            };

            Execute (activity, listener);
        }
    }

    public class BlurCompleteListener : Java.Lang.Object, IOnBlurCompleteListener
    {
        public Action BlurCompleteHandler { get; set; }
        public void OnBlurComplete ()
        {
            BlurCompleteHandler?.Invoke ();
        }
    }
}

