using System;
using Java.Util;

namespace Square.TimesSquare
{
    public abstract class CalendarCellDecorator : Java.Lang.Object, ICalendarCellDecorator
    {
        void ICalendarCellDecorator.Decorate(CalendarCellView cellView, Date date)
        {
            Decorate(cellView, CalendarPickerView.GetDate(date));
        }

        public abstract void Decorate(CalendarCellView cellView, DateTime date);
    }
}
