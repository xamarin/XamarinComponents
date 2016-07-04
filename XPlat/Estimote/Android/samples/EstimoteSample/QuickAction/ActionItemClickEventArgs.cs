using System;

namespace Estimotes.Droid
{
    public class ActionItemClickEventArgs : EventArgs
    {
        public ActionItemClickEventArgs(QuickAction source, int position)
        {
            Source = source;
            Position = position;
            ActionItem = source.GetActionItem(position);
        }

        public int Position { get; private set; }
        public QuickAction Source { get; private set; }
        public ActionItem ActionItem { get; private set; }
    }
}
