namespace AMScrollingNavbar
{
    /// <summary>
	/// Represents the state of the navigation bar
    /// </summary>
	public enum NavigationBarState
    {
        /// <summary>
		/// The navigation bar is fully collapsed
        /// </summary>
		Collapsed,

        /// <summary>
		/// The navigation bar is fully visible
        /// </summary>
		Expanded,

        /// <summary>
		/// The navigation bar is transitioning to either `Collapsed` or `Scrolling`
        /// </summary>
		Scrolling,
    }
}
