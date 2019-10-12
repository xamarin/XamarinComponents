using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class BasicInputsActivity : DemoActivity
    {
        protected override int ContentLayoutId
            => Resource.Layout.activity_basic_inputs;
    }
}