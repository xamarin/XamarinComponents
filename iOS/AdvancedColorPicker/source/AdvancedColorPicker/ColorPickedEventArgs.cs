using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace AdvancedColorPicker
{
	/// <summary>
	/// The event arguments for a when a color is picked from a <see cref="ColorPickerView"/>.
	/// </summary>
	public class ColorPickedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickedEventArgs"/> class.
		/// </summary>
		/// <param name="selectedColor">The initial selected color.</param>
		public ColorPickedEventArgs(UIColor selectedColor)
		{
			SelectedColor = selectedColor;
		}

		/// <summary>
		/// Gets the color selected be the <see cref="ColorPickerView"/>.
		/// </summary>
		/// <value>The color.</value>
		public UIColor SelectedColor { get; private set; }
	}
}
