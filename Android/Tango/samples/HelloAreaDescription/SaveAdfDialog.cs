using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace HelloAreaDescription
{
    /// <summary>
    /// Displays progress bar and text information while saving an adf.
    /// </summary>
    public class SaveAdfDialog : AlertDialog
    {
        private ProgressBar mProgressBar;

        public SaveAdfDialog(Context context)
            : base(context)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.save_adf_dialog);
            SetCancelable(false);

            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);

            if (mProgressBar == null)
            {
                Console.WriteLine("Unable to find view progress_bar.");
            }
        }

        public int Progress
        {
            get { return mProgressBar?.Progress ?? 0; }
            set
            {
                if (mProgressBar != null)
                {
                    mProgressBar.Visibility = ViewStates.Visible;
                    mProgressBar.Progress = value;
                }
            }
        }
    }
}
