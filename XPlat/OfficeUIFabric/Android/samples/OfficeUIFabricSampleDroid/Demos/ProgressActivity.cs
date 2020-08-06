using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class ProgressActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_progress;

        ProgressBar progress_bar_large_primary;
        ProgressBar progress_bar_medium_primary;
        ProgressBar progress_bar_small_primary;
        ProgressBar progress_bar_xsmall_primary;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress_bar_large_primary = FindViewById<ProgressBar>(Resource.Id.progress_bar_large_primary);
            progress_bar_medium_primary = FindViewById<ProgressBar>(Resource.Id.progress_bar_medium_primary);
            progress_bar_small_primary = FindViewById<ProgressBar>(Resource.Id.progress_bar_small_primary);
            progress_bar_xsmall_primary = FindViewById<ProgressBar>(Resource.Id.progress_bar_xsmall_primary);

            SetDrawableColorPreLollipop(progress_bar_large_primary);
            SetDrawableColorPreLollipop(progress_bar_medium_primary);
            SetDrawableColorPreLollipop(progress_bar_small_primary);
            SetDrawableColorPreLollipop(progress_bar_xsmall_primary);
        }

        void SetDrawableColorPreLollipop(ProgressBar progressBar)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                return;

            var mutatedProgressBarDrawable = progressBar.IndeterminateDrawable.Mutate();
            mutatedProgressBarDrawable.SetColorFilter(new Color(ContextCompat.GetColor(this, Resource.Color.uifabric_demo_primary)), PorterDuff.Mode.SrcIn);
            progressBar.IndeterminateDrawable = mutatedProgressBarDrawable;
        }
    }
}