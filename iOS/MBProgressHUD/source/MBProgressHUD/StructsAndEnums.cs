using System;

namespace MBProgressHUD
{
	public enum MBProgressHUDMode
	{
		/** Progress is shown using an UIActivityIndicatorView. This is the default. */
		Indeterminate,
		/** Progress is shown using a round, pie-chart like, progress view. */
		Determinate,
		/** Progress is shown using a horizontal progress bar */
		DeterminateHorizontalBar,
		/** Progress is shown using a ring-shaped progress view. */
		AnnularDeterminate,
		/** Shows a custom view */
		CustomView,
		/** Shows only labels */
		Text
	}

	public enum MBProgressHUDAnimation
	{
		/** Opacity animation */
		Fade,
		/** Opacity + scale animation */
		Zoom,
		ZoomOut = Zoom,
		ZoomIn
	}
}

