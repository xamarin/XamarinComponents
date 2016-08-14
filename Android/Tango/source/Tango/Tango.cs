using System;
using Android.Content;
using Java.Lang;

namespace ProjectTango.Service
{
    partial class Tango
    {
        public Tango(Context context, Action runOnTangoReady)
            : this(context, new Runnable(runOnTangoReady))
        {
        }
    }
}