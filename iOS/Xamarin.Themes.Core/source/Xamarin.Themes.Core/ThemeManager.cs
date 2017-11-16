using System;
using Xamarin.Themes.Core.Interfaces;
using UIKit;

namespace Xamarin.Themes.Core
{
	/// <summary>
	/// Theme manager.
	/// </summary>
	public static class ThemeManager
	{
		#region Fields
		private static ITheme mTheme;
		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating if the theme has been set.
		/// </summary>
		/// <value><c>true</c> if is theme set; otherwise, <c>false</c>.</value>
		public static bool IsThemeSet
		{
			get
			{
				return (mTheme != null);
			}
		}

		/// <summary>
		/// Gets or sets the current.
		/// </summary>
		/// <value>The current.</value>
		public static ITheme Current
		{
			get
			{
				if (mTheme == null)
					throw new Exception("No theme has been registered with the theme manage");

				return mTheme;
			}
			set 
			{
				mTheme = value;
			}
		}


		#endregion
		#region Methods


		/// <summary>
		/// Determines if is landscape the specified orientation.
		/// </summary>
		/// <returns><c>true</c> if is landscape the specified orientation; otherwise, <c>false</c>.</returns>
		/// <param name="orientation">Orientation.</param>
		public static bool IsLandscape(UIInterfaceOrientation orientation)
		{
			if (orientation == UIInterfaceOrientation.LandscapeLeft 
				|| orientation == UIInterfaceOrientation.LandscapeRight)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines if is portrait the specified orientation.
		/// </summary>
		/// <returns><c>true</c> if is portrait the specified orientation; otherwise, <c>false</c>.</returns>
		/// <param name="orientation">Orientation.</param>
		public static bool IsPortrait(UIInterfaceOrientation orientation)
		{
			if (orientation == UIInterfaceOrientation.Portrait 
				|| orientation == UIInterfaceOrientation.PortraitUpsideDown)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Register this instance.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static ITheme Register<T>() where T : ITheme, new()
		{
			mTheme = new T();

			return mTheme;
		}

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <returns>The current.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetCurrent<T>() where T : ITheme, new()
		{
			return (T)Current;
		}

//		/// <summary>
//		/// Currents the or new.
//		/// </summary>
//		/// <returns>The or new.</returns>
//		/// <typeparam name="T">The 1st type parameter.</typeparam>
//		public static T CurrentOrNew<T>() where T : ITheme, new()
//		{
//			if (mTheme == null)
//				mTheme = new T();
//			return (T)Current;
//		}

		#endregion

	}
}

