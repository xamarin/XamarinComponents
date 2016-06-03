
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;

namespace BetterPickersSample
{ 
    public class ChoiceListFragment : Android.Support.V4.App.ListFragment
    {
        ChoiceAdapter adapter;

        public override void OnActivityCreated (Bundle savedInstanceState)
        {
            base.OnActivityCreated (savedInstanceState);

            adapter = new ChoiceAdapter (this.Activity, new List<ChoiceItem> {
                new ChoiceItem ("Calendar Date Picker", CalendarDatePicker),
                new ChoiceItem ("Date Picker Light", () => DatePicker (true)),
                new ChoiceItem ("Date Picker Dark", () => DatePicker (false)),
                new ChoiceItem ("Number Picker Light", () => NumberPicker (true)),
                new ChoiceItem ("Number Picker Dark", () => NumberPicker (false)),
                new ChoiceItem ("Expiration Picker Light", () => ExpirationPicker (true)),
                new ChoiceItem ("Expiration Picker Dark", () => ExpirationPicker (false)),
                new ChoiceItem ("Radial Time Picker", RadialTimePicker),
                new ChoiceItem ("Time Picker Light", () => TimePicker (true)),
                new ChoiceItem ("Time Picker Dark", () => TimePicker (false)),
                new ChoiceItem ("Time Zone Picker", TimeZonePicker),
                new ChoiceItem ("Recurrence Picker", RecurrencePicker),
                new ChoiceItem ("HMS Picker Light", () => HmsPicker (true)),
                new ChoiceItem ("HMS Picker Dark", () => HmsPicker (false)),
            });

            ListAdapter = adapter;
            ListView.ItemClick += (sender, e) => {

                var item = adapter[e.Position];

                item.Action ();
            };
        }

        void CalendarDatePicker ()
        {
            var p = BetterPickers.CalendarDatePickers.CalendarDatePickerDialog.NewInstance (null, 2014, 04, 24);
            p.DateSet += (sender, e) => {
                ShowToast ("CalendarDatePicker Set: Year={0}, Month={1}, Day={2}", + e.P1, e.P2, e.P3);
            };
            p.Show(this.FragmentManager, "OK");
        }

        void DatePicker (bool light)
        {
            var p = new BetterPickers.DatePickers.DatePickerBuilder ()
                .SetFragmentManager (FragmentManager)
//                .SetYear (DateTime.Now.Year)
//                .SetMonthOfYear (DateTime.Now.Month)
//                .SetDayOfMonth (DateTime.Now.Day)
                .SetStyleResId (light ? Resource.Style.BetterPickersDialogFragment_Light : Resource.Style.BetterPickersDialogFragment);
            p.AddDatePickerDialogHandler ((reference, year, month, day) => {
                ShowToast ("DatePicker Set: Ref={0}, Year={1}, Month={2}, Day={3}", reference, year, month, day);
            });
            p.Show ();
        }

        void NumberPicker (bool light)
        {
            var p = new BetterPickers.NumberPickers.NumberPickerBuilder ()
                .SetFragmentManager (this.FragmentManager)
                .SetStyleResId (light ? Resource.Style.BetterPickersDialogFragment_Light : Resource.Style.BetterPickersDialogFragment)
                .SetMaxNumber (999999)
                .SetMinNumber (0)
                .AddNumberPickerDialogHandler ((reference, number, decimalNumber, isNegative, fullNumber) => {
                    ShowToast ("NumberPicker Set: Ref={0}, Num={1}, Decimal={2}, IsNegative={3}, FullNum={4}",
                        reference, number, decimalNumber, isNegative, fullNumber);
                });
            p.Show();
        }

        void ExpirationPicker (bool light)
        {
            var p = new BetterPickers.ExpirationPickers.ExpirationPickerBuilder ()
                .SetFragmentManager (FragmentManager)
                .SetStyleResId (light ? Resource.Style.BetterPickersDialogFragment_Light : Resource.Style.BetterPickersDialogFragment)
//                .SetYear (2015)
//                .SetMonthOfYear (12)
                .AddExpirationPickerDialogHandler ((reference, year, monthOfYear) => {
                    ShowToast ("ExpirationPicker Set: Ref={0}, Year={1}, Month={2}", reference, year, monthOfYear);
                });
            p.Show ();
        }

        void RadialTimePicker ()
        {
            var p = BetterPickers.RadialTimePickers.RadialTimePickerDialog.NewInstance (null, 0, 0, false);
            p.TimeSet += (sender, e) => {
                ShowToast ("RadialTimePicker Set: Hour={0}, Minute={1}", e.P1, e.P2);
            };
            p.SetStartTime (DateTime.Now.Hour, DateTime.Now.Minute);
            p.SetDoneText ("Finish!");
            p.Show (FragmentManager, null);
        }

        void TimePicker (bool light)
        {
            var p = new BetterPickers.TimePickers.TimePickerBuilder ()
                .SetFragmentManager (FragmentManager)
                .SetStyleResId (light ? Resource.Style.BetterPickersDialogFragment_Light : Resource.Style.BetterPickersDialogFragment)
                .AddTimePickerDialogHandler ((reference, hourOfDay, minute) => {
                    ShowToast ("TimePicker Set: Ref={0}, Hour={1}, Minute={2}", reference, hourOfDay, minute);
                });
            p.Show ();
        }

        void TimeZonePicker ()
        {
            var p = new BetterPickers.TimeZonePickers.TimeZonePickerDialog ();
            p.TimeZoneSet += (sender, e) => {
                ShowToast ("TimeZonePicker Set: {0}", e.Tzi.MDisplayName);
            };
            p.Show (FragmentManager, null);
        }

        void RecurrencePicker ()
        {
            var p = new BetterPickers.RecurrencePickers.RecurrencePickerDialog ();
            p.RecurrenceSet += (sender, e) => {
                ShowToast ("RecurrencePicker Set: {0}", e.Rrule);
            };
            p.Show (FragmentManager, null);
        }

        void HmsPicker (bool light)
        {
            var p = new BetterPickers.HmsPickers.HmsPickerBuilder ()
                .SetFragmentManager (FragmentManager)
                .SetStyleResId (light ? Resource.Style.BetterPickersDialogFragment_Light : Resource.Style.BetterPickersDialogFragment)
                .AddHmsPickerDialogHandler ((reference, hours, minutes, seconds) => {
                    ShowToast ("HmsPicker Set: Ref={0}, Hours={1}, Minutes={2}, Seconds={3}", reference, hours, minutes, seconds);
                });
            p.Show ();
        }

        void ShowToast (string msg, params object[] args)
        {
            Toast.MakeText (this.Activity, string.Format (msg, args), ToastLength.Short).Show ();
        }
    }
}
