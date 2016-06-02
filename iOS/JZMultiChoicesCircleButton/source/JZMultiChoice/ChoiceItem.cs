using System;
using UIKit;

namespace JZMultiChoice
{
	/// <summary>
	/// Choice item for describing a item in the button
	/// </summary>
	public class ChoiceItem
	{
		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public UIImage Icon {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public String Title {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the action to be executed when the item is selected
		/// </summary>
		/// <value>The action.</value>
		public Action Action {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="JZMultiChoice.ChoiceItem"/> disable action animation
		/// </summary>
		/// <value><c>true</c> if disable action animation; otherwise, <c>false</c>.</value>
		public bool DisableActionAnimation {
			get;
			set;
		}

		public ChoiceItem ()
		{
		}
	}
}

