using System.Collections.Generic;

using Android.Content;
using Android.Views;
using Android.Widget;

using EstimoteSdk;

using Java.Lang;

namespace Estimotes.Droid
{
    public class LeDevicesListAdapter : BaseAdapter<Beacon>
    {
        readonly List<Beacon> _beacons;
        readonly LayoutInflater _inflater;

        public LeDevicesListAdapter(Context context)
        {
            _inflater = LayoutInflater.From(context);
            _beacons = new List<Beacon>();
        }

        public override Beacon this[int index] { get { return _beacons[index]; } }
        public override int Count { get { return _beacons.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Object GetItem(int position)
        {
            return _beacons[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            DeviceItemViewHolder holder;
            if (convertView == null)
            {
                convertView = _inflater.Inflate(Resource.Layout.device_item, null);
                holder = new DeviceItemViewHolder(convertView);
                convertView.Tag = holder;
            }
            else
            {
                holder = (DeviceItemViewHolder)convertView.Tag;
            }

            holder.Display(_beacons[position]);
            return convertView;
        }

        public void Update(IEnumerable<Beacon> estimoteBeacons)
        {
            _beacons.Clear();
            _beacons.AddRange(estimoteBeacons);
            NotifyDataSetChanged();
        }
    }
}
