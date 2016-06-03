namespace SidePanels
{
    public class PanCompletedEventArgs
    {
        public PanCompletedEventArgs(bool canceled)
        {
            Canceled = canceled;
        }

        public bool Canceled { get; private set; }
    }
}
