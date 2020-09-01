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
using Java.Util;
using Java.Interop;

namespace Square.TimesSquare
{
    partial class CalendarPickerView
    {
        private WeakReference cellClickInterceptor;
        private WeakReference dateSelectableFilter;

        internal static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime SelectedDate
        {
            get { return GetDate(SelectedJavaDate); }
        }

        public DateTime[] SelectedDates
        {
            get { return SelectedJavaDates.Select(d => GetDate(d)).ToArray(); }
        }

        public void HighlightDates(params DateTime[] dates)
        {
            HighlightDates(dates.Select(d => GetDate(d)).ToArray());
        }

        public void HighlightDates(IEnumerable<DateTime> dates)
        {
            HighlightDates(dates.Select(d => GetDate(d)).ToArray());
        }

        public FluentInitializer Init(DateTime minDate, DateTime maxDate)
        {
            return Init(GetDate(minDate), GetDate(maxDate));
        }

        public FluentInitializer Init(DateTime minDate, DateTime maxDate, Locale locale)
        {
            return Init(GetDate(minDate), GetDate(maxDate), locale);
        }

        public FluentInitializer Init(DateTime minDate, DateTime maxDate, string language)
        {
            return Init(minDate, maxDate, new Locale(language));
        }

        public FluentInitializer Init(DateTime minDate, DateTime maxDate, string language, string country)
        {
            return Init(minDate, maxDate, new Locale(language, country));
        }

        public FluentInitializer Init(DateTime minDate, DateTime maxDate, string language, string country, string variant)
        {
            return Init(minDate, maxDate, new Locale(language, country, variant));
        }

        public void ScrollToDate(DateTime date)
        {
            ScrollToDate(GetDate(date));
        }

        public void SelectDate(DateTime date)
        {
            SelectDate(GetDate(date));
        }

        public void SelectDate(DateTime date, bool smoothScroll)
        {
            SelectDate(GetDate(date), smoothScroll);
        }

        public static Date GetDate(DateTime netDate)
        {
            var javaDate = new Date();
            javaDate.Time = (long)(netDate.ToUniversalTime() - epoch).TotalMilliseconds;
            return javaDate;
        }

        public static DateTime GetDate(Date javaDate)
        {
            return (epoch + TimeSpan.FromMilliseconds(javaDate.Time)).ToLocalTime();
        }

        public event EventHandler<CellClickInterceptorEventArgs> CellClicking
        {
            add
            {
                EventHelper.AddEventHandler<ICellClickInterceptor, CellClickInterceptorImplementor>(
                    ref cellClickInterceptor,
                    () => new CellClickInterceptorImplementor(this),
                    SetCellClickInterceptor,
                    h => h.Handler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<ICellClickInterceptor, CellClickInterceptorImplementor>(
                    ref cellClickInterceptor,
                    CellClickInterceptorImplementor.IsEmpty,
                    v => SetCellClickInterceptor(null),
                    h => h.Handler -= value);
            }
        }
        [Register(TypeName)]
        private sealed class CellClickInterceptorImplementor : Java.Lang.Object, ICellClickInterceptor
        {
            private const string TypeName = "mono/com/squareup/timessquare/CalendarPickerView_CellClickInterceptorImplementor";
            private object sender;

            public CellClickInterceptorImplementor(object sender)
                : base(JNIEnv.StartCreateInstance(TypeName, "()V"), JniHandleOwnership.TransferLocalRef)
            {
                JNIEnv.FinishCreateInstance(Handle, "()V");
                this.sender = sender;
            }

            public EventHandler<CellClickInterceptorEventArgs> Handler;

            public bool OnCellClicked(Date date)
            {
                var handler = Handler;
                if (handler != null)
                {
                    var e = new CellClickInterceptorEventArgs(date);
                    handler(sender, e);
                    return e.Handled;
                }
                return false;
            }

            public static bool IsEmpty(CellClickInterceptorImplementor value)
            {
                return value.Handler == null;
            }
        }
        public class CellClickInterceptorEventArgs : EventArgs
        {
            public CellClickInterceptorEventArgs(DateTime date)
            {
                JavaDate = GetDate(date);
                Date = date;
            }

            public CellClickInterceptorEventArgs(Date date)
            {
                JavaDate = date;
                Date = GetDate(date);
            }

            public Date JavaDate { get; private set; }

            public DateTime Date { get; private set; }

            public bool Handled { get; set; }
        }
        public abstract class CellClickInterceptor : Java.Lang.Object, ICellClickInterceptor
        {
            bool ICellClickInterceptor.OnCellClicked(Date date)
            {
                return OnCellClicked(GetDate(date));
            }

            public abstract bool OnCellClicked(DateTime date);
        }

        public event EventHandler<DateSelectableFilterEventArgs> DateSelecting
        {
            add
            {
                EventHelper.AddEventHandler<IDateSelectableFilter, DateSelectableFilterImplementor>(
                    ref dateSelectableFilter,
                    () => new DateSelectableFilterImplementor(this),
                    SetDateSelectableFilter,
                    h => h.Handler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<IDateSelectableFilter, DateSelectableFilterImplementor>(
                    ref dateSelectableFilter,
                    DateSelectableFilterImplementor.IsEmpty,
                    v => SetDateSelectableFilter(null),
                    h => h.Handler -= value);
            }
        }
        [Register(TypeName)]
        private sealed class DateSelectableFilterImplementor : Java.Lang.Object, IDateSelectableFilter
        {
            private const string TypeName = "mono/com/squareup/timessquare/CalendarPickerView_DateSelectableFilterImplementor";
            private object sender;

            public DateSelectableFilterImplementor(object sender)
                : base(JNIEnv.StartCreateInstance(TypeName, "()V"), JniHandleOwnership.TransferLocalRef)
            {
                JNIEnv.FinishCreateInstance(Handle, "()V");
                this.sender = sender;
            }

            public EventHandler<DateSelectableFilterEventArgs> Handler;

            public bool IsDateSelectable(Date date)
            {
                var handler = Handler;
                if (handler != null)
                {
                    var e = new DateSelectableFilterEventArgs(date);
                    handler(sender, e);
                    return e.IsDateSelectable;
                }
                return false;
            }

            public static bool IsEmpty(DateSelectableFilterImplementor value)
            {
                return value.Handler == null;
            }
        }
        public class DateSelectableFilterEventArgs : EventArgs
        {
            public DateSelectableFilterEventArgs(DateTime date)
            {
                JavaDate = GetDate(date);
                Date = date;
                IsDateSelectable = true;
            }

            public DateSelectableFilterEventArgs(Date date)
            {
                JavaDate = date;
                Date = GetDate(date);
                IsDateSelectable = true;
            }

            public Date JavaDate { get; private set; }

            public DateTime Date { get; private set; }

            public bool IsDateSelectable { get; set; }
        }
        public abstract class DateSelectableFilter : Java.Lang.Object, IDateSelectableFilter
        {
            bool IDateSelectableFilter.IsDateSelectable(Date date)
            {
                return IsDateSelectable(GetDate(date));
            }

            public abstract bool IsDateSelectable(DateTime date);
        }

        partial class InvalidDateSelectedEventArgs
        {
            public InvalidDateSelectedEventArgs(DateTime date)
            {
                javaDate = GetDate(date);
            }

            public DateTime Date
            {
                get { return GetDate(javaDate); }
            }
        }
        public abstract class OnInvalidDateSelectedListener : Java.Lang.Object, IOnInvalidDateSelectedListener
        {
            void IOnInvalidDateSelectedListener.OnInvalidDateSelected(Date date)
            {
                OnInvalidDateSelected(GetDate(date));
            }

            public abstract void OnInvalidDateSelected(DateTime date);
        }

        partial class DateSelectedEventArgs
        {
            public DateSelectedEventArgs(DateTime date)
            {
                javaDate = GetDate(date);
            }

            public DateTime Date
            {
                get { return GetDate(javaDate); }
            }
        }

        partial class DateUnselectedEventArgs
        {
            public DateUnselectedEventArgs(DateTime date)
            {
                javaDate = GetDate(date);
            }

            public DateTime Date
            {
                get { return GetDate(javaDate); }
            }
        }

        partial class FluentInitializer
        {
            public FluentInitializer WithSelectedDate(DateTime date)
            {
                return WithSelectedDate(GetDate(date));
            }

            public FluentInitializer WithSelectedDates(params DateTime[] dates)
            {
                return WithSelectedDates(dates.Select(d => GetDate(d)).ToArray());
            }

            public FluentInitializer WithSelectedDates(IEnumerable<DateTime> dates)
            {
                return WithSelectedDates(dates.Select(d => GetDate(d)).ToArray());
            }

            public FluentInitializer WithHighlightedDate(DateTime date)
            {
                return WithHighlightedDate(GetDate(date));
            }

            public FluentInitializer WithHighlightedDates(params DateTime[] dates)
            {
                return WithHighlightedDates(dates.Select(d => GetDate(d)).ToArray());
            }

            public FluentInitializer WithHighlightedDates(IEnumerable<DateTime> dates)
            {
                return WithHighlightedDates(dates.Select(d => GetDate(d)).ToArray());
            }
        }
    }
}
