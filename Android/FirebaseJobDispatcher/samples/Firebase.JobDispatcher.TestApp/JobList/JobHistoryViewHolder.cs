using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FJDTestApp.Model;

namespace FJDTestApp.JobList
{
    public class JobHistoryViewHolder : RecyclerView.ViewHolder
    {
        readonly TextView jobHistoryNameTextView;
        readonly TextView resultTextView;

        public JobHistoryViewHolder(View itemView) : base(itemView)
        {
            jobHistoryNameTextView = itemView.FindViewById<TextView>(Resource.Id.job_history_textview);
            resultTextView = itemView.FindViewById<TextView>(Resource.Id.job_history_result_count_textview);
        }

        public void Display(JobHistory jobHistory)
        {
            jobHistoryNameTextView.Text = $"{jobHistory}";
            resultTextView.Text = $"{jobHistory.GetLastResult()}.";
        }
    }
}