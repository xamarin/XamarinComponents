using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace HelloAreaDescription
{
    /// <summary>
    /// This is an adapter class which maps the ListView with a Data Source(Array of strings).
    /// </summary>
    public class AdfUuidArrayAdapter : ArrayAdapter<string>
    {
        private IList<AdfData> mAdfDataList;

        public AdfUuidArrayAdapter(Context context, IList<AdfData> adfDataList)
            : base(context, Resource.Layout.adf_list_row)
        {
            SetAdfData(adfDataList);
        }

        public void SetAdfData(IList<AdfData> adfDataList)
        {
            mAdfDataList = adfDataList;
        }

        public override int Count
        {
            get { return mAdfDataList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row;
            if (convertView == null)
            {
                var inflator = LayoutInflater.FromContext(Context);
                row = inflator.Inflate(Resource.Layout.adf_list_row, parent, false);
            }
            else
            {
                row = convertView;
            }
            var uuid = row.FindViewById<TextView>(Resource.Id.adf_uuid);
            var name = row.FindViewById<TextView>(Resource.Id.adf_name);

            if (mAdfDataList == null)
            {
                name.SetText(Resource.String.metadata_not_read);
            }
            else
            {
                name.Text = mAdfDataList[position].Name;
                uuid.Text = mAdfDataList[position].Uuid;
            }
            return row;
        }
    }
}
