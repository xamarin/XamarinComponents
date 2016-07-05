using Android.Views;
using Android.Widget;

using EstimoteSdk;

using Java.Lang;

namespace Estimotes.Droid
{
    public class DeviceItemViewHolder : Object
    {
        readonly TextView _macTextView;
        readonly TextView _majorTextView;
        readonly TextView _measuredPowerTextView;
        readonly TextView _minorTextView;
        readonly TextView _rssiTextView;

        public DeviceItemViewHolder(View view)
        {
            _macTextView = (TextView)view.FindViewWithTag("mac");
            _majorTextView = (TextView)view.FindViewWithTag("major");
            _minorTextView = (TextView)view.FindViewWithTag("minor");
            _measuredPowerTextView = (TextView)view.FindViewWithTag("mpower");
            _rssiTextView = (TextView)view.FindViewWithTag("rssi");
        }

        public void Display(Beacon beacon)
        {
            _macTextView.Text = string.Format("MAC: {0} ({1:N2})", beacon.MacAddress, Utils.ComputeAccuracy(beacon));
            _majorTextView.Text = string.Format("Major: {0}", beacon.Major);
            _minorTextView.Text = string.Format("Minor: {0}", beacon.Minor);
            _measuredPowerTextView.Text = string.Format("MPower: {0}", beacon.MeasuredPower);
            _rssiTextView.Text = string.Format("RSSI: {0}", beacon.Rssi);
        }
    }
}
