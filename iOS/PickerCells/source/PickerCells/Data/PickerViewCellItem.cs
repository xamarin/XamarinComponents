using System;

namespace PickerCells.Data
{
	public class PickerViewCellItem
	{

		#region Properties

		/// <summary>
		/// Gets or sets the selected value.
		/// </summary>
		/// <value>The selected value.</value>
		public object SelectedValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the display value.
		/// </summary>
		/// <value>The display value.</value>
		public String DisplayValue
		{
			get;
			set;
		}
			
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.Data.PickerViewCellItem"/> class.
		/// </summary>
		public PickerViewCellItem()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.Data.PickerViewCellItem"/> class.
		/// </summary>
		/// <param name="selectedValue">Selected value.</param>
		/// <param name="displayValue">Display value.</param>
		/// <param name="width">Width.</param>
		public PickerViewCellItem(object selectedValue, string displayValue)
		{
			SelectedValue = selectedValue;
			DisplayValue = displayValue;

		}

		#endregion

		#region Methods

		#endregion



	}
}

