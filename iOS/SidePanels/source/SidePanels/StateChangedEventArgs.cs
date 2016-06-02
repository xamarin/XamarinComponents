using System;

namespace SidePanels
{
    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(SidePanelState oldState, SidePanelState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public SidePanelState OldState { get; private set; }

        public SidePanelState NewState { get; private set; }
    }
}
