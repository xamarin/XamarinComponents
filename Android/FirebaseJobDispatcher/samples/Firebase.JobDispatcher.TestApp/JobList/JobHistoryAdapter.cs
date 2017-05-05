using System.Diagnostics;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using FJDTestApp.Model;

namespace FJDTestApp.JobList
{
    class JobHistoryAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => FJDTestApplication.JobHistoryStorage.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            JobHistory jh = FJDTestApplication.JobHistoryStorage.Get(position);
            var viewHolder = holder as JobHistoryViewHolder;
            Debug.Assert(viewHolder != null, "viewHolder != null");
            viewHolder.Display(jh);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.jobhistory_card, parent, false);
            var vh = new JobHistoryViewHolder(v);
            return vh;
        }
    }
}