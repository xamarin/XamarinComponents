using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using AlertDialog = Android.Support.V7.App.AlertDialog;

using Square.OkIO;

namespace OkIOSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            var doThings = FindViewById<Button>(Resource.Id.doThings);

            doThings.Click += delegate
            {
                var buffer = OkIO.Buffer(new RandomSource(5));
                var bytes = buffer.ReadByteArray(3);
                buffer.Close();

                new AlertDialog.Builder(this)
                    .SetTitle("OkIO")
                    .SetMessage("BYTES: " + string.Join(", ", bytes))
                    .SetNeutralButton("OK", (sender, e) => ((AlertDialog)sender).Dismiss())
                    .Create()
                    .Show();
            };
        }
    }

    public class RandomSource : Java.Lang.Object, ISource
    {
        private readonly Random random;
        private long bytesLeft;

        public RandomSource(long bytes)
        {
            random = new Random();
            bytesLeft = bytes;
        }

        public long Read(OkBuffer sink, long byteCount)
        {
            if (bytesLeft == -1L) throw new ArgumentException("closed");
            if (bytesLeft == 0L) return -1L;
            if (byteCount > int.MaxValue) byteCount = int.MaxValue;
            if (byteCount > bytesLeft) byteCount = bytesLeft;

            // Random is most efficient when computing 32 bits of randomness. Start with that.
            int ints = (int)(byteCount / 4);
            for (int i = 0; i < ints; i++)
            {
                sink.WriteInt(random.Next());
            }

            // If we need 1, 2, or 3 bytes more, keep going. We'll discard 24, 16 or 8 random bits!
            int bytes = (int)(byteCount - ints * 4);
            if (bytes > 0)
            {
                int bits = random.Next();
                for (int i = 0; i < bytes; i++)
                {
                    sink.WriteByte(bits & 0xff);
                    bits >>= 8;
                }
            }

            bytesLeft -= byteCount;
            return byteCount;
        }

        public Timeout Timeout() => Square.OkIO.Timeout.None;

        public void Close() => bytesLeft = -1L;
    }
}
