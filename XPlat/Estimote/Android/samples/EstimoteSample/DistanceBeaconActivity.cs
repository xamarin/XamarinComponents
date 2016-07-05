using System;

using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;

using EstimoteSdk;

namespace Estimotes.Droid
{
    [Activity(Label = "DistanceBeaconActivity")]
    public class DistanceBeaconActivity : Activity, ViewTreeObserver.IOnGlobalLayoutListener
    {
        static readonly string Tag = typeof(DistanceBeaconActivity).FullName;
        static readonly double RELATIVE_START_POS = 320.0 / 1110.0;
        static readonly double RELATIVE_STOP_POS = 885.0 / 1110.0;
        Beacon _beacon;
        View _dotView;
        FindSpecificBeacon _findBeacon;
        Region _region;
        int _segmentLength = -1;
        View _sonar;
        int _startY = -1;

        public void OnGlobalLayout()
        {
            _sonar.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
            _startY = (int)(RELATIVE_START_POS * _sonar.MeasuredHeight);
            int stopY = (int)(RELATIVE_STOP_POS * _sonar.MeasuredHeight);
            _segmentLength = stopY - _startY;
            _dotView.Visibility = ViewStates.Visible;
            _dotView.TranslationY = ComputeDotPosY(_beacon);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStart()
        {
            base.OnStart();
            _findBeacon.LookForBeacon(_region, _beacon);
        }

        protected override void OnStop()
        {
            _findBeacon.Stop();
            base.OnStop();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Tuple<Beacon, Region> stuff = this.GetBeaconAndRegion();
            _beacon = stuff.Item1;
            _region = stuff.Item2;

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.distance_view);
            _dotView = FindViewById(Resource.Id.dot);

            _sonar = FindViewById(Resource.Id.sonar);
            _sonar.ViewTreeObserver.AddOnGlobalLayoutListener(this);

            _findBeacon = new FindSpecificBeacon(this);
            _findBeacon.BeaconFound += (sender, e) => RunOnUiThread(() =>{
                                                                        Log.Debug(Tag, "Found the beacon!");
                                                                        if (_segmentLength == -1)
                                                                        {
                                                                            return;
                                                                        }
                                                                        _dotView.Animate().TranslationY(ComputeDotPosY(e.FoundBeacon)).Start();
                                                                    });
        }

        float ComputeDotPosY(Beacon foundBeacon)
        {
            // Put the dot at the end of the scale when it's further than 6m.
            double x = Utils.ComputeAccuracy(foundBeacon);
            Log.Debug(Tag, "Beacon is approx. {0:N1} metres away", x);
            double distance = Math.Min(x, 6.0);
            return _startY + (int)(_segmentLength * (distance / 6.0));
        }
    }
}
