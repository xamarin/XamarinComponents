using UIKit;

using SlackHQ;

namespace SlackTextViewControllerSample
{
	public class MessageTextView : SlackTextView
	{
		public MessageTextView ()
		{
			BackgroundColor = UIColor.White;

			Placeholder = "Message";
			PlaceholderColor = UIColor.LightGray;
			PastableMediaTypes = PastableMediaType.All;

			Layer.BorderColor = UIColor.FromRGB (217, 217, 217).CGColor;
		}
	}
}
