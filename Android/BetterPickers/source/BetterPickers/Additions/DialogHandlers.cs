using System;

namespace BetterPickers.DatePickers
{
    public partial class DatePickerBuilder
    {
        public DatePickerBuilder AddDatePickerDialogHandler (DatePickerDialogHandler.DialogCompleteDelegate dialogCompleteHandler)
        {
            return this.AddDatePickerDialogHandler (new DatePickerDialogHandler (dialogCompleteHandler));
        }
    }

    public class DatePickerDialogHandler : Java.Lang.Object, DatePickerDialogFragment.IDatePickerDialogHandler
    {
        public delegate void DialogCompleteDelegate (int reference, int year, int monthOfYear, int dayOfMonth);

        public DatePickerDialogHandler (DialogCompleteDelegate dialogCompleteHandler)
        {
            DialogCompleteHandler = dialogCompleteHandler;
        }

        public DialogCompleteDelegate DialogCompleteHandler { get;set; }

        public void OnDialogDateSet (int reference, int year, int monthOfYear, int dayOfMonth)
        {
            if (DialogCompleteHandler != null)
                DialogCompleteHandler (reference, year, monthOfYear, dayOfMonth);
        }
    }
}

namespace BetterPickers.TimePickers
{
    public partial class TimePickerBuilder
    {
        public TimePickerBuilder AddTimePickerDialogHandler (TimePickerDialogHandler.DialogCompleteDelegate dialogCompleteHandler)
        {
            return this.AddTimePickerDialogHandler (new TimePickerDialogHandler (dialogCompleteHandler));
        }
    }

    public class TimePickerDialogHandler : Java.Lang.Object, TimePickerDialogFragment.ITimePickerDialogHandler
    {
        public delegate void DialogCompleteDelegate (int reference, int hourOfDay, int minute);

        public TimePickerDialogHandler (DialogCompleteDelegate dialogCompleteHandler)
        {
            DialogCompleteHandler = dialogCompleteHandler;
        }

        public DialogCompleteDelegate DialogCompleteHandler { get;set; }

        public void OnDialogTimeSet(int reference, int hourOfDay, int minute)
        {
            if (DialogCompleteHandler != null)
                DialogCompleteHandler (reference, hourOfDay, minute);
        }
    }
}
    
namespace BetterPickers.NumberPickers
{
    public partial class NumberPickerBuilder
    {
        public NumberPickerBuilder AddNumberPickerDialogHandler (NumberPickerDialogHandler.DialogCompleteDelegate dialogCompleteHandler)
        {
            return this.AddNumberPickerDialogHandler (new NumberPickerDialogHandler (dialogCompleteHandler));
        }
    }

    public class NumberPickerDialogHandler : Java.Lang.Object, NumberPickerDialogFragment.INumberPickerDialogHandler
    {
        public delegate void DialogCompleteDelegate (int reference, int number, double decimalNumber, bool isNegative, double fullNumber);


        public NumberPickerDialogHandler (DialogCompleteDelegate dialogCompleteHandler)
        {
            DialogCompleteHandler = dialogCompleteHandler;
        }

        public DialogCompleteDelegate DialogCompleteHandler { get;set; }

        public void OnDialogNumberSet (int reference, int number, double decimalNumber, bool isNegative, double fullNumber)
        {
            if (DialogCompleteHandler != null)
                DialogCompleteHandler (reference, number, decimalNumber, isNegative, fullNumber);
        }
    }
}


namespace BetterPickers.ExpirationPickers
{
    public partial class ExpirationPickerBuilder
    {
        public ExpirationPickerBuilder AddExpirationPickerDialogHandler (ExpirationPickerDialogHandler.DialogCompleteDelegate dialogCompleteHandler)
        {
            return this.AddExpirationPickerDialogHandler (new ExpirationPickerDialogHandler (dialogCompleteHandler));
        }
    }

    public class ExpirationPickerDialogHandler : Java.Lang.Object, ExpirationPickerDialogFragment.IExpirationPickerDialogHandler
    {
        public delegate void DialogCompleteDelegate (int reference, int year, int monthOfYear);


        public ExpirationPickerDialogHandler (DialogCompleteDelegate dialogCompleteHandler)
        {
            DialogCompleteHandler = dialogCompleteHandler;
        }

        public DialogCompleteDelegate DialogCompleteHandler { get;set; }

        public void OnDialogExpirationSet(int reference, int year, int monthOfYear)
        {
            if (DialogCompleteHandler != null)
                DialogCompleteHandler (reference, year, monthOfYear);
        }
    }
}

namespace BetterPickers.HmsPickers
{
    public partial class HmsPickerBuilder
    {
        public HmsPickerBuilder AddHmsPickerDialogHandler (HmsPickerDialogHandler.DialogCompleteDelegate dialogCompleteHandler)
        {
            return this.AddHmsPickerDialogHandler (new HmsPickerDialogHandler (dialogCompleteHandler));
        }
    }

    public class HmsPickerDialogHandler : Java.Lang.Object, HmsPickerDialogFragment.IHmsPickerDialogHandler
    {
        public delegate void DialogCompleteDelegate (int reference, int hours, int minutes, int seconds);


        public HmsPickerDialogHandler (DialogCompleteDelegate dialogCompleteHandler)
        {
            DialogCompleteHandler = dialogCompleteHandler;
        }

        public DialogCompleteDelegate DialogCompleteHandler { get;set; }

        public void OnDialogHmsSet(int reference, int hours, int minutes, int seconds)
        {
            if (DialogCompleteHandler != null)
                DialogCompleteHandler (reference, hours, minutes, seconds);
        }
    }
}