using System;
using UIKit;

using AMScrollingNavbar;

namespace AMScrollingNavbarSample
{
	partial class WebViewController : ScrollingNavigationViewController
	{
		private const string Html = "<html><body style='background-color:#34495e; color:white; font-family:Heiti SC, sans-serif'><h2>There's an old joke - um... two elderly women are at a Catskill mountain resort, and one of 'em says:<br/><br/> 'Boy, the food at this place is really terrible.'<br/><br/> The other one says: <br/><br/>'Yeah, I know; and such small portions.'<br/><br/> Well, that's essentially how I feel about life - full of loneliness, and misery, and suffering, and unhappiness, and it's all over much too quickly.<br/><br/> The... the other important joke, for me, is one that's usually attributed to Groucho Marx, but I think it appears originally in Freud's 'Wit and Its Relation to the Unconscious,' and it goes like this - I'm paraphrasing <br/><br/> 'I would never want to belong to any club that would have someone like me for a member.' <br/><br/> That's the key joke of my adult life, in terms of my relationships with women.</h2><i>Woody Allen</i></body></html>";

		public WebViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Web View";

			View.BackgroundColor = new UIColor (0.17f, 0.24f, 0.32f, 1.0f);
			webView.BackgroundColor = new UIColor (0.17f, 0.24f, 0.32f, 1.0f);
			NavigationController.NavigationBar.BarTintColor = new UIColor (0.2f, 0.28f, 0.37f, 1.0f);

			webView.LoadHtmlString (Html, null);
			webView.ScrollView.Delegate = this;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// Enable the navbar scrolling
			ScrollingNavigationController?.FollowScrollView (webView, 50.0);
		}
	}
}
