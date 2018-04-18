using System;
using Android.Support.Wearable.Watchface;
using Android.Views;
using Android.OS;
using Android.Graphics;
using Android.App;
using Android.Text.Format;
using Android.Graphics.Drawables;
using Android.Util;
using Java.Util.Concurrent;
using System.Threading;
using Android.Content;
using Android.Service.Wallpaper;

[assembly: UsesFeature (Android.Wearable.Constants.WearableFeature, Required=false)]

namespace WatchFaceSample
{
    public class AnalogWatchFaceService : CanvasWatchFaceService
    {
        const String Tag = "AnalogWatchFaceService";

        public AnalogWatchFaceService ()
        {
        }

        public override WallpaperService.Engine OnCreateEngine () 
        {
            return new AnalogWatchFaceEngine (this);
        }

        public class AnalogWatchFaceEngine : CanvasWatchFaceService.Engine 
        {
            const String Tag = "AnalogWatchFaceEngine";
            static long InterActiveUpdateRateMs = TimeUnit.Seconds.ToMillis (1);

            CanvasWatchFaceService owner;
            const int MsgUpdateTime = 0;

            Paint hourPaint;
            Paint minutePaint;
            Paint secondPaint;
            Paint tickPaint;
            bool mute;
            public Time time;

            Timer timerSeconds;
            TimeZoneReceiver timeZoneReceiver;

            // Whether the display supports fewer bits for each color in ambient mode. When true, we
            // disable anti-aliasing in ambient mode.
            bool lowBitAmbient;

            Bitmap backgroundBitmap;
            Bitmap backgroundScaledBitmap;

            public AnalogWatchFaceEngine (CanvasWatchFaceService owner) : base(owner)
            {
                this.owner = owner;
            }

            public override void OnCreate (ISurfaceHolder holder) 
            {
                SetWatchFaceStyle (new WatchFaceStyle.Builder (owner)
                    .SetCardPeekMode (WatchFaceStyle.PeekModeShort)
                    .SetBackgroundVisibility (WatchFaceStyle.BackgroundVisibilityInterruptive)
                    .SetShowSystemUiTime (false)
                    .Build ());

                var backgroundDrawable = Application.Context.Resources.GetDrawable (Resource.Drawable.XamarinWatchFaceBackground);
                backgroundBitmap = (backgroundDrawable as BitmapDrawable).Bitmap;

                hourPaint = new Paint();
                hourPaint.SetARGB(255, 200, 200, 200);
                hourPaint.StrokeWidth = 5.0f;
                hourPaint.AntiAlias = true;
                hourPaint.StrokeCap = Paint.Cap.Round;

                minutePaint = new Paint();
                minutePaint.SetARGB(255, 200, 200, 200);
                minutePaint.StrokeWidth = 3.0f;
                minutePaint.AntiAlias = true;
                minutePaint.StrokeCap = Paint.Cap.Round;

                secondPaint = new Paint();
                secondPaint.SetARGB(255, 255, 0, 0);
                secondPaint.StrokeWidth = 2.0f;
                secondPaint.AntiAlias = true;
                secondPaint.StrokeCap = Paint.Cap.Round;

                tickPaint = new Paint();
                tickPaint.SetARGB(100, 255, 255, 255);
                tickPaint.StrokeWidth = 2.0f;
                tickPaint.AntiAlias = true;

                time = new Time ();

                // How to stop the timer? It shouldn't run in ambient mode...
                timerSeconds = new Timer (new TimerCallback (state => {
                    Invalidate ();
                }), null, 
                    TimeSpan.FromMilliseconds (InterActiveUpdateRateMs), 
                    TimeSpan.FromMilliseconds (InterActiveUpdateRateMs));
            }

            public override void OnPropertiesChanged(Bundle properties) 
            {
                base.OnPropertiesChanged (properties);

                lowBitAmbient = properties.GetBoolean (WatchFaceService.PropertyLowBitAmbient);

                if (Log.IsLoggable (Tag, LogPriority.Debug))
                    Log.Debug (Tag, "OnPropertiesChanged: low-bit ambient = " + lowBitAmbient);
            }

            public override void OnTimeTick ()
            {
                base.OnTimeTick ();

                if (Log.IsLoggable (Tag, LogPriority.Debug))
                    Log.Debug (Tag, "onTimeTick: ambient = " + IsInAmbientMode);
                
                Invalidate ();
            }

            public override void OnAmbientModeChanged (bool inAmbientMode) 
            {
                base.OnAmbientModeChanged (inAmbientMode);

                if (Log.IsLoggable (Tag, LogPriority.Debug))
                    Log.Debug (Tag, "OnAmbientMode");
                
                if (lowBitAmbient) {
                    bool antiAlias = !inAmbientMode;
                    hourPaint.AntiAlias = antiAlias;
                    minutePaint.AntiAlias = antiAlias;
                    secondPaint.AntiAlias = antiAlias;
                    tickPaint.AntiAlias = antiAlias;
                }
                Invalidate ();
            }

            public override void OnDraw (Canvas canvas, Rect bounds)
            {
                time.SetToNow ();
                int width = bounds.Width ();
                int height = bounds.Height ();

                // Draw the background, scaled to fit.
                if (backgroundScaledBitmap == null
                    || backgroundScaledBitmap.Width != width
                    || backgroundScaledBitmap.Height != height) {
                    backgroundScaledBitmap = Bitmap.CreateScaledBitmap(backgroundBitmap,
                        width, height, true /* filter */);
                }
                canvas.DrawColor (Color.Black);
                canvas.DrawBitmap(backgroundScaledBitmap, 0, 0, null);

                float centerX = width / 2.0f;
                float centerY = height / 2.0f;

                // Draw the ticks.
                float innerTickRadius = centerX - 10;
                float outerTickRadius = centerX;
                for (int tickIndex = 0; tickIndex < 12; tickIndex++) {
                    float tickRot = (float) (tickIndex * Math.PI * 2 / 12);
                    float innerX = (float) Math.Sin(tickRot) * innerTickRadius;
                    float innerY = (float) -Math.Cos(tickRot) * innerTickRadius;
                    float outerX = (float) Math.Sin(tickRot) * outerTickRadius;
                    float outerY = (float) -Math.Cos(tickRot) * outerTickRadius;
                    canvas.DrawLine(centerX + innerX, centerY + innerY,
                        centerX + outerX, centerY + outerY, tickPaint);
                }

                float secRot = time.Second / 30f * (float) Math.PI;
                int minutes = time.Minute;
                float minRot = minutes / 30f * (float) Math.PI;
                float hrRot = ((time.Hour + (minutes / 60f)) / 6f ) * (float) Math.PI;

                float secLength = centerX - 20;
                float minLength = centerX - 40;
                float hrLength = centerX - 80;

                if (!IsInAmbientMode) {
                    float secX = (float) Math.Sin(secRot) * secLength;
                    float secY = (float) -Math.Cos(secRot) * secLength;
                    canvas.DrawLine(centerX, centerY, centerX + secX, centerY + secY, secondPaint);
                }

                float minX = (float) Math.Sin(minRot) * minLength;
                float minY = (float) -Math.Cos(minRot) * minLength;
                canvas.DrawLine(centerX, centerY, centerX + minX, centerY + minY, minutePaint);

                float hrX = (float) Math.Sin(hrRot) * hrLength;
                float hrY = (float) -Math.Cos(hrRot) * hrLength;
                canvas.DrawLine(centerX, centerY, centerX + hrX, centerY + hrY, hourPaint);
            }

            public override void OnVisibilityChanged (bool visible)
            {
                base.OnVisibilityChanged (visible);

                if (Log.IsLoggable (Tag, LogPriority.Debug))
                    Log.Debug (Tag, "OnVisibilityChanged: " + visible);
                
                if (visible) {
                    RegisterTimezoneReceiver ();
                    time.Clear (Java.Util.TimeZone.Default.ID);
                    time.SetToNow ();
                } else {
                    UnregisterTimezoneReceiver ();
                }
            }

            bool ShouldTimerBeRunning() 
            {
                return IsVisible && !IsInAmbientMode;
            }

            bool registeredTimezoneReceiver = false;

            void RegisterTimezoneReceiver()
            {
                if (registeredTimezoneReceiver) {
                    return;
                } else  {
                    if (timeZoneReceiver == null) {
                        timeZoneReceiver = new TimeZoneReceiver ();
                        timeZoneReceiver.Receive = (intent) => {
                            time.Clear (intent.GetStringExtra ("time-zone"));
                            time.SetToNow ();
                        };
                    }
                    registeredTimezoneReceiver = true;
                    IntentFilter filter = new IntentFilter(Intent.ActionTimezoneChanged);
                    Application.Context.RegisterReceiver (timeZoneReceiver, filter);
                }
            }

            void UnregisterTimezoneReceiver() 
            {
                if (!registeredTimezoneReceiver)
                    return;
                
                registeredTimezoneReceiver = false;
                Application.Context.UnregisterReceiver (timeZoneReceiver);
            }

        }

    }

    public class TimeZoneReceiver: BroadcastReceiver 
    {
        public Action<Intent> Receive { get; set; }

        public override void OnReceive (Context context, Intent intent)
        {
            if (Receive != null)
                Receive (intent);
        }
    }
}

