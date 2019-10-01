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
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class CalendarViewActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_calendar_view;

        Microsoft.OfficeUIFabric.CalendarView calendar_view;
        TextView example_date_title;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            calendar_view = FindViewById<Microsoft.OfficeUIFabric.CalendarView>(Resource.Id.calendar_view);
            example_date_title = FindViewById<TextView>(Resource.Id.example_date_title);

            calendar_view.DateSelected += Calendar_view_DateSelected;
        }

        private void Calendar_view_DateSelected(object sender, Microsoft.OfficeUIFabric.DateSelectedEventArgs e)
        {
            example_date_title.Text = DateStringUtils.FormatDateWithWeekDay(this, e.P0);
        }
    }
}