namespace YouTubePlayerSample.Adapters
{
	public abstract class DemoListViewItem
	{
		public abstract string Title { get; }

		public abstract bool IsEnabled { get; }

		public abstract string DisabledText { get; }
	}
}
