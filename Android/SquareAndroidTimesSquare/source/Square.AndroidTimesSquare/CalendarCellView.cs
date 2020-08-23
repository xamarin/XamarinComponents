using System;
using Java.Util;

namespace Square.TimesSquare
{
    partial class CalendarCellView
    {
        [Obsolete("Use Highlighted instead")]
        public void SetHighlighted(bool isHighlighted)
        {
            Highlighted = isHighlighted;
        }

        [Obsolete("Use RangeState instead")]
        public void SetRangeState(RangeState rangeState)
        {
            RangeState = rangeState;
        }
    }
}
