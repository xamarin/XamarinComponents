using System;
using System.Collections.Generic;

namespace PickerCells.Data
{
	public class PickerViewCellComponent
	{
		public float Width
		{
			get;
			set;
		}

		public List<PickerViewCellItem> Items
		{
			get;
			set;
		}

		public PickerViewCellComponent()
		{
		}
	}
}

