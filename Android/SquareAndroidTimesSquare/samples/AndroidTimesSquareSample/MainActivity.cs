using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;
using AlertDialog = Android.Support.V7.App.AlertDialog;

using Square.TimesSquare;

[assembly: Application(Label = "@string/app_name", Icon = "@drawable/icon", SupportsRtl = true)]

namespace AndroidTimesSquareSample
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        Theme = "@style/Theme.AppCompat",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class SampleTimesSquareActivity : AppCompatActivity
    {
        private readonly List<Button> modeButtons = new List<Button>();

        [Splice(Resource.Id.calendar_view)] private CalendarPickerView calendar;
        [Splice(Resource.Id.button_single)] private Button single;
        [Splice(Resource.Id.button_multi)] private Button multi;
        [Splice(Resource.Id.button_range)] private Button range;
        [Splice(Resource.Id.button_display_only)] private Button displayOnly;
        [Splice(Resource.Id.button_weekdays_only)] private Button weekdaysOnly;
        [Splice(Resource.Id.button_decorator)] private Button decorator;
        [Splice(Resource.Id.button_dialog)] private Button dialog;
        [Splice(Resource.Id.button_customized)] private Button customized;
        [Splice(Resource.Id.button_rtl)] private Button rtl;
        [Splice(Resource.Id.done_button)] private Button done;

        private AlertDialog theDialog;
        private CalendarPickerView dialogView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Geneticist.Splice(this);

            var nextYear = DateTime.Now.AddYears(1);
            var lastYear = DateTime.Now.AddYears(-1);

            calendar
                .Init(lastYear, nextYear)
                .InMode(CalendarPickerView.SelectionMode.Single)
                .WithSelectedDate(DateTime.Now);

            modeButtons.AddRange(new[] { single, multi, range, displayOnly, weekdaysOnly, decorator });

            single.Click += delegate
            {
                SetButtonsEnabled(single);

                calendar.Decorators = new ICalendarCellDecorator[0];
                calendar
                    .Init(lastYear, nextYear)
                    .InMode(CalendarPickerView.SelectionMode.Single)
                    .WithSelectedDate(DateTime.Now);
            };

            multi.Click += delegate
            {
                SetButtonsEnabled(multi);

                var today = DateTime.Now;
                var dates = new List<DateTime>();
                for (int i = 0; i < 5; i++)
                {
                    today = today.AddDays(3);
                    dates.Add(today);
                }
                calendar.Decorators = new ICalendarCellDecorator[0];
                calendar
                    .Init(DateTime.Now, nextYear)
                    .InMode(CalendarPickerView.SelectionMode.Multiple)
                    .WithSelectedDates(dates);
            };

            range.Click += delegate
            {
                SetButtonsEnabled(range);

                var today = DateTime.Now;
                var dates = new List<DateTime>();
                today = today.AddDays(3);
                dates.Add(today);
                today = today.AddDays(5);
                dates.Add(today);
                calendar.Decorators = new ICalendarCellDecorator[0];
                calendar
                    .Init(DateTime.Now, nextYear)
                    .InMode(CalendarPickerView.SelectionMode.Range)
                    .WithSelectedDates(dates);
            };

            displayOnly.Click += delegate
            {
                SetButtonsEnabled(displayOnly);

                calendar.Decorators = new ICalendarCellDecorator[0];
                calendar
                    .Init(DateTime.Now, nextYear)
                    .InMode(CalendarPickerView.SelectionMode.Single)
                    .WithSelectedDate(DateTime.Now)
                    .DisplayOnly();
            };

            weekdaysOnly.Click += delegate
            {
                SetButtonsEnabled(weekdaysOnly);

                calendar.Decorators = new ICalendarCellDecorator[0];
                calendar
                    .Init(DateTime.Now, nextYear)
                    .InMode(CalendarPickerView.SelectionMode.Single)
                    .WithSelectedDate(DateTime.Now);
            };

            decorator.Click += delegate
            {
                SetButtonsEnabled(decorator);

                calendar.Decorators = new[] { new SampleDecorator() };
                calendar
                  .Init(lastYear, nextYear)
                  .InMode(CalendarPickerView.SelectionMode.Single)
                  .WithSelectedDate(DateTime.Now);
            };

            dialog.Click += delegate
            {
                ShowCalendarInDialog("I'm a dialog!", Resource.Layout.Dialog);
                dialogView
                    .Init(lastYear, nextYear)
                    .WithSelectedDate(DateTime.Now);
            };

            customized.Click += delegate
            {
                ShowCalendarInDialog("Pimp my calendar!", Resource.Layout.DialogCustomized);
                dialogView
                    .Init(lastYear, nextYear)
                    .WithSelectedDate(DateTime.Now);
            };

            rtl.Click += delegate
            {
                ShowCalendarInDialog("I'm right-to-left!", Resource.Layout.Dialog);
                dialogView
                  .Init(lastYear, nextYear, "iw", "IL")
                  .WithSelectedDate(DateTime.Now);
            };

            done.Click += delegate
            {
                var dates = calendar.SelectedDates.Select(d => d.ToString("d MMM yyyy")).ToArray();
                var toast = "Selected: " + string.Join(", ", dates);
                Toast.MakeText(this, toast, ToastLength.Short).Show();
            };
        }

        private void WeekdaysOnlySelection(object sender, CalendarPickerView.DateSelectableFilterEventArgs e)
        {
            e.IsDateSelectable = e.Date.DayOfWeek != DayOfWeek.Saturday && e.Date.DayOfWeek != DayOfWeek.Sunday;
        }
        
        private void ShowCalendarInDialog(string title, int layoutResId)
        {
            dialogView = (CalendarPickerView)LayoutInflater.Inflate(layoutResId, null, false);
            theDialog = new AlertDialog.Builder(this)
                .SetTitle(title)
                .SetView(dialogView)
                .SetNeutralButton("Dismiss", (sender, e) => theDialog.Dismiss())
                .Create();
            theDialog.ShowEvent += delegate
            {
                dialogView.FixDialogDimens();
            };
            theDialog.Show();
        }

        private void SetButtonsEnabled(Button button)
        {
            // make sure to unregister any events
            calendar.DateSelecting -= WeekdaysOnlySelection;
            if (button == weekdaysOnly)
            {
                calendar.DateSelecting += WeekdaysOnlySelection;
            }

            foreach (var modeButton in modeButtons)
            {
                modeButton.Enabled = modeButton != button;
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            bool applyFixes = theDialog != null && theDialog.IsShowing;
            if (applyFixes)
            {
                dialogView.UnfixDialogDimens();
            }
            base.OnConfigurationChanged(newConfig);
            if (applyFixes)
            {
                dialogView.Post(dialogView.FixDialogDimens);
            }
        }

        public class SampleDecorator : CalendarCellDecorator
        {
            public override void Decorate(CalendarCellView cellView, DateTime date)
            {
                var dateString = date.Day.ToString();
                var span = new SpannableString(dateString + "\ntitle");
                span.SetSpan(new RelativeSizeSpan(0.5f), 0, dateString.Length, SpanTypes.InclusiveExclusive);
				cellView.DayOfMonthTextView.TextFormatted = span;
            }
        }
    }
}
