using Android.App;
using Android.Widget;
using Android.OS;
using Android.Things.Pio;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using System;
using Android.Animation;
using Android.Graphics;
using Android.InputMethodServices;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Google.Android.Things.Contrib.Driver.Apa102;
using Google.Android.Things.Contrib.Driver.Bmx280;
using Google.Android.Things.Contrib.Driver.Button;
using Google.Android.Things.Contrib.Driver.Ht16k33;
using Google.Android.Things.Contrib.Driver.Pwmspeaker;
using Java.Util.Concurrent;
using Button = Android.Widget.Button;
using Keycode = Android.Views.Keycode;

namespace WeatherStation
{
    [Activity(Label = "WeatherStation")]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryLauncher })]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "android.intent.category.IOT_LAUNCHER" })]
    public class MainActivity : Activity
    {
        public static string TAG = typeof(MainActivity).FullName;

        public SensorManager sensorManager;
        public ButtonInputDriver buttonInputDriver;
        public Bmx280SensorDriver environmentalSensorDriver;
        public AlphanumericDisplay display;
        public DisplayMode displayMode = DisplayMode.TEMPERATURE;

        private Apa102 ledStrip;
        private int[] rainbow = new int[7];
        private static int LEDSTRIP_BRIGHTNESS = 1;
        private static float BAROMETER_RANGE_LOW = 965f;
        private static float BAROMETER_RANGE_HIGH = 1035f;
        private static float BAROMETER_RANGE_SUNNY = 1010f;
        private static float BAROMETER_RANGE_RAINY = 990f;

        private IGpio led;
        private int SPEAKER_READY_DELAY_MS = 300;
        public Speaker speaker;
        public float lastTemperature;
        public float lastPressure;

        private ImageView imageView;

        private SensorManager.DynamicSensorCallback dynamicSensorCallback;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Log.Debug(TAG, "Started Weather Station");

            dynamicSensorCallback = new WeatherDynamicSensorCallback(this);

            SetContentView (Resource.Layout.Main);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            sensorManager = (SensorManager) GetSystemService(SensorService);

            try
            {
                buttonInputDriver = new ButtonInputDriver(BoardDefaults.GetButtonGpioPin(),
                    Google.Android.Things.Contrib.Driver.Button.Button.LogicState.PressedWhenLow,
                    (int) KeyEvent.KeyCodeFromString("KEYCODE_A"));
                buttonInputDriver.Register();
                Log.Debug(TAG, "Initialized GPIO Button that generates a keypress with KEYCODE_A");
            }
            catch (Exception e)
            {
                throw new Exception("Error initializing GPIO button", e);
            }

            try
            {
                environmentalSensorDriver = new Bmx280SensorDriver(BoardDefaults.GetI2cBus());
                sensorManager.RegisterDynamicSensorCallback(dynamicSensorCallback);
                environmentalSensorDriver.RegisterTemperatureSensor();
                environmentalSensorDriver.RegisterPressureSensor();
                Log.Debug(TAG, "Initialized I2C BMP280");
            }
            catch (Exception e)
            {
                throw new Exception("Error initializing BMP280", e);
            }

            try
            {
                display = new AlphanumericDisplay(BoardDefaults.GetI2cBus());
                display.SetEnabled(true);
                display.Clear();
                Log.Debug(TAG, "Initialized I2C Display");
            }
            catch (Exception e)
            {
                Log.Error(TAG, "Error initializing display", e);
                Log.Debug(TAG, "Display disabled");
                display = null;
            }

            try
            {
                ledStrip = new Apa102(BoardDefaults.GetSpiBus(), Apa102.Mode.Bgr);
                ledStrip.Brightness = LEDSTRIP_BRIGHTNESS;
                for (int i = 0; i < rainbow.Length; i++)
                {
                    float[] hsv = { i * 360f / rainbow.Length, 1.0f, 1.0f };
                    rainbow[i] = Color.HSVToColor(255, hsv);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ledStrip = null;
            }

            try
            {

				var pioService = PeripheralManager.Instance;
                led = pioService.OpenGpio(BoardDefaults.GetLedGpioPin());
				led.SetEdgeTriggerType(Gpio.EdgeNone);
				led.SetDirection(Gpio.DirectionOutInitiallyLow);
				led.SetActiveType(Gpio.ActiveHigh);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                speaker = new Speaker(BoardDefaults.GetSpeakerPwmPin());
                ValueAnimator slide = ValueAnimator.OfFloat(440, 440* 4);
                slide.SetDuration(50);
                slide.RepeatCount = 5;
                slide.SetInterpolator(new LinearInterpolator());
                slide.AddUpdateListener(new SlideUpdateListener(this));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateDisplay(float value)
        {
            if (display != null)
            {
                try
                {
                    display.Display(value);
                }
                catch (Exception e)
                {
                    Log.Error(TAG, "Error setting display", e);
                }
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.A)
            {
                displayMode = DisplayMode.PRESSURE;
                UpdateDisplay(lastPressure);
                try
                {
                    led.Value = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                return true;
            }
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.A)
            {
                displayMode = DisplayMode.TEMPERATURE;
                UpdateDisplay(lastTemperature);
                try
                {
                    led.Value = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                return true;
            }

            return base.OnKeyUp(keyCode, e);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //TODO https://github.com/androidthings/weatherstation/blob/master/app/src/main/java/com/example/androidthings/weatherstation/WeatherStationActivity.java#L304
        }

        public void UpdateBarometer(float pressure)
        {
            if (pressure > BAROMETER_RANGE_SUNNY)
            {
                imageView.SetImageResource(Resource.Drawable.ic_sunny);
            }
            else if (pressure < BAROMETER_RANGE_RAINY)
            {
                imageView.SetImageResource(Resource.Drawable.ic_rainy);
            }
            else
            {
                imageView.SetImageResource(Resource.Drawable.ic_cloudy);
            }

            if (ledStrip == null)
            {
                return;
            }

            float t = (pressure - BAROMETER_RANGE_LOW) / (BAROMETER_RANGE_HIGH - BAROMETER_RANGE_LOW);
            int n = (int)Math.Ceiling(rainbow.Length * t);
            n = Math.Max(0, Math.Min(n, rainbow.Length));
            int[] colors = new int[rainbow.Length];
            for (int i = 0; i < n; i++)
            {
                int ri = rainbow.Length - 1 - i;
                colors[ri] = rainbow[ri];
            }

            try
            {
                ledStrip.Write(colors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public class WeatherDynamicSensorCallback : SensorManager.DynamicSensorCallback
    {
        private MainActivity _context;
        public WeatherDynamicSensorCallback(MainActivity context)
        {
            _context = context;
        }
        public override void OnDynamicSensorConnected(Sensor sensor)
        {
            if (sensor.Type == SensorType.AmbientTemperature)
            {
                _context.sensorManager.RegisterListener(new TemperatureListener(_context), sensor, SensorDelay.Normal);
                //if (mPubsubPublisher != null)
                //{
                //    mSensorManager.registerListener(mPubsubPublisher.getTemperatureListener(), sensor,
                //            SensorManager.SENSOR_DELAY_NORMAL);
                //}
            }
            else if (sensor.Type == SensorType.Pressure)
            {
                _context.sensorManager.RegisterListener(new PressureListener(_context), sensor, SensorDelay.Normal);
                //if (mPubsubPublisher != null)
                //{
                //    mSensorManager.registerListener(mPubsubPublisher.getTemperatureListener(), sensor,
                //            SensorManager.SENSOR_DELAY_NORMAL);
                //}
            }
        }

        public override void OnDynamicSensorDisconnected(Sensor sensor)
        {
            base.OnDynamicSensorDisconnected(sensor);
        }

    }

    public class TemperatureListener : Java.Lang.Object, ISensorEventListener
    {
        private MainActivity _context;
        public TemperatureListener(MainActivity context)
        {
            _context = context;
        }
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            Log.Debug(MainActivity.TAG, "accuracy changed: " + accuracy);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            _context.lastTemperature = e.Values[0];
            Log.Debug(MainActivity.TAG, "sensor changed: " + _context.lastTemperature);

            if (_context.displayMode == DisplayMode.TEMPERATURE)
            {
                _context.UpdateDisplay(_context.lastTemperature);
            }
        }
    }

    public class PressureListener : Java.Lang.Object, ISensorEventListener
    {
        private MainActivity _context;
        public PressureListener(MainActivity context)
        {
            _context = context;
        }
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            Log.Debug(MainActivity.TAG, "accuracy changed: " + accuracy);
        }

        public void OnSensorChanged(SensorEvent e)
        {
            _context.lastPressure = e.Values[0];
            Log.Debug(MainActivity.TAG, "sensor changed: " + _context.lastPressure);

            if (_context.displayMode == DisplayMode.PRESSURE)
            {
                _context.UpdateDisplay(_context.lastPressure);
            }
            _context.UpdateBarometer(_context.lastPressure);
        }
    }

    public class SlideUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
    {
        private MainActivity _context;
        public SlideUpdateListener(MainActivity context)
        {
            _context = context;
        }
        public void OnAnimationUpdate(ValueAnimator animation)
        {
            try
            {
                float v = (float) animation.AnimatedValue;
                _context.speaker.Play(v);
            }
            catch (Exception e)
            {
                throw new Exception("Error sliding speaker", e);
            }
        }
    }


    public enum DisplayMode
    {
        TEMPERATURE,
        PRESSURE
    }

}

