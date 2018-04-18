
using System;

using Foundation;
using UIKit;
using PickerCells;
using System.Collections.Generic;
using PickerCells.Data;

namespace DatePickerCellSample
{
	public class DateTableViewSource : UITableViewSource
	{
		private List<BasePickerCell> mCells;

		public DateTableViewSource()
		{
			mCells = new List<BasePickerCell>();

			var aCell = new DatePickerCell(UIDatePickerMode.Date,DateTime.Now)
			{
				Key = "1234",
			};
			aCell.TextLabel.Text = "Date";
			aCell.RightLabelTextAlignment = UITextAlignment.Right;
			aCell.OnItemChanged += (object sender, PickerCellArgs e) => {

				var result = e.Items;
			};

			mCells.Add(aCell);


			var someDict = new Dictionary<int,PickerViewCellComponent>();
			someDict[0] = new PickerViewCellComponent()
			{
				Width = 160,
				Items = new List<PickerViewCellItem>()
				{
					new PickerViewCellItem()
					{
						SelectedValue = 1,
						DisplayValue = "Bob",
					},
					new PickerViewCellItem()
					{
						SelectedValue = 2,
						DisplayValue = "John",
					},
				}
			};

			someDict[1] = new PickerViewCellComponent()
			{
				Width = 160,
				Items = new List<PickerViewCellItem>()
				{
					new PickerViewCellItem()
					{
						SelectedValue = 1,
						DisplayValue = "Dylan",
					},
					new PickerViewCellItem()
					{
						SelectedValue = 2,
						DisplayValue = "Lennon",
					},
				}
			};

            // 	You can also use a string list for a single component
            //	var aList = new List<String> (){ "Ringo", "John", "Paul", "George" };
            //	var pickerCell = new PickerViewCell(aList);

			var pickerCell = new PickerViewCell(someDict);
			pickerCell.TextLabel.Text = "Artist";
			pickerCell.RightLabelTextAlignment = UITextAlignment.Right;
			pickerCell.SelectedObject = new object[]{1,1};


			pickerCell.OnItemChanged += (object sender, PickerCellArgs e) => {

				var result = e.Items;
			};

			mCells.Add(pickerCell);




		}

		public override nint NumberOfSections(UITableView tableView)
		{
			// TODO: return the actual number of sections
			return 1;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			// TODO: return the actual number of items in the section
			return mCells.Count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return "Header";
		}

		public override string TitleForFooter(UITableView tableView, nint section)
		{
			return "Footer";
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{

			var aCell = GetCell(tableView,indexPath);

			if (aCell is BasePickerCell)
			{
				var datePickerTableViewCell = aCell as BasePickerCell;

				return datePickerTableViewCell.PickerHeight;

			}

			return 44.0f;

		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			return mCells[indexPath.Row];
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			
			var aCell = GetCell(tableView,indexPath);

			if (aCell is BasePickerCell)
			{
				var datePickerTableViewCell = aCell as BasePickerCell;

				datePickerTableViewCell.SelectedInTableView(tableView);

				tableView.DeselectRow(indexPath, true);
			}
		}
	}
}

