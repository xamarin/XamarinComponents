using System;

namespace AnimatedButtons
{
    public class CellSelectedEventArgs : EventArgs
    {
        public CellSelectedEventArgs(LiquidFloatingCell cell, int index)
        {
            Cell = cell;
            Index = index;
        }

        public LiquidFloatingCell Cell { get; set; }

        public int Index { get; set; }
    }
}
