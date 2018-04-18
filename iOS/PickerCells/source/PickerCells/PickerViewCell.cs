using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using PickerCells.Data;

namespace PickerCells
{
	public class PickerViewCell : BasePickerCell
	{
		#region Fields
		private UIPickerView mPickerView;
		private InternalPickerViewModel mViewModel;
		private Dictionary<int,PickerViewCellComponent> mDataDict;
		#endregion

		#region Properties

		private String mSeperatorCharacter = " ";

		/// <summary>
		/// Gets or sets the seperator character used to seperate multiple values
		/// </summary>
		/// <value>The seperator character.</value>
		public String SeperatorCharacter 
		{
			get { return mSeperatorCharacter; }
			set { mSeperatorCharacter = value; }
		}

		/// <summary>
		/// Gets or sets the selected object.
		/// </summary>
		/// <value>The selected object.</value>
		public override object SelectedObject
		{
			get
			{
				return ViewModel.SelectedItems;}
			set
			{
				ViewModel.UpdateSelectedItems((UIPickerView)PickerView,value);
			}
		}

		private Dictionary<int,PickerViewCellComponent> DataDictionary
		{
			get
			{
				if (mDataDict == null)
					mDataDict = new Dictionary<int,PickerViewCellComponent>();

				return mDataDict;
			}
		}
		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		/// <value>The view model.</value>
		private InternalPickerViewModel ViewModel 
		{
		    get 
			{
				if (mViewModel == null)
					mViewModel = new InternalPickerViewModel(this, DataDictionary);
				
				return mViewModel; 
			}
		    set 
			{ 
				mViewModel = value; 
			}
		}


		/// <summary>
		/// Gets the picker view.
		/// </summary>
		/// <value>The picker view.</value>
		protected override UIKit.UIView PickerView
		{
			get
			{
				if (mPickerView == null)
					mPickerView = new UIPickerView();

				return mPickerView;
			}
		}
			
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		public PickerViewCell(Dictionary<int,PickerViewCellComponent> dictionary) 
			: base()
		{
			mDataDict = dictionary;

			SecondarySetup();

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		/// <param name="dictionary">Dictionary.</param>
		/// <param name="style">Style.</param>
		public PickerViewCell(Dictionary<int,PickerViewCellComponent> dictionary, UITableViewCellStyle style = UITableViewCellStyle.Default) 
			: base(style)
		{
			mDataDict = dictionary;

			SecondarySetup();

		}


		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		public PickerViewCell() 
			: base()
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		public PickerViewCell(UITableViewCellStyle style)
			: base(style)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public PickerViewCell(UITableViewCellStyle style, string resuseidentifier) 
			: base(style, resuseidentifier)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		/// <param name="items">Items.</param>
		public PickerViewCell(List<String> items, UITableViewCellStyle style = UITableViewCellStyle.Default) 
			: base(style)
		{

			if (items != null)
			{
				var simps = new Dictionary<int,PickerViewCellComponent>();
				simps[0] = new PickerViewCellComponent()
				{
					Width = -1,
					Items = new List<PickerViewCellItem>()
					{

					}
				};

				foreach (var aItem in items)
				{
					simps[0].Items.Add(new PickerViewCellItem()
					{
						SelectedValue = aItem,
						DisplayValue = aItem,

					});
				}

				mDataDict = simps;

			}

			SecondarySetup();
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public PickerViewCell(string resuseidentifier) 
			: base(resuseidentifier)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="aCoder">A coder.</param>
		public PickerViewCell(NSCoder aCoder) 
			: base(aCoder)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerViewCell"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public PickerViewCell (IntPtr handle) 
			: base(handle)
		{
			SecondarySetup ();
		}
		#endregion

		#region Methods

		/// <summary>
		/// Setup to be called after the base class has initialised
		/// </summary>
		protected override void SecondarySetup()
		{
			((UIPickerView)PickerView).Model = ViewModel;

		}

		/// <summary>
		/// Sets up the cell
		/// </summary>
		protected override void CellSetup()
		{
			//LeftLabel.Text = "Picker View";
		
		}

		/// <summary>
		/// Sets the items.
		/// </summary>
		/// <param name="items">Items.</param>
		public void SetItems(List<String> items)
		{
			var simps = new Dictionary<int,PickerViewCellComponent>();
			simps[0] = new PickerViewCellComponent()
			{
				Width = -1,
				Items = new List<PickerViewCellItem>()
				{

				}
			};

			foreach (var aItem in items)
			{
				simps[0].Items.Add(new PickerViewCellItem()
				{
					SelectedValue = aItem,
					DisplayValue = aItem,

				});
			}

			mDataDict = simps;

			SecondarySetup();
		}
			

		#endregion

		private class InternalPickerViewModel : UIPickerViewModel 
		{
			private Dictionary<int,PickerViewCellComponent> mDataDict;
			private PickerViewCell mTableViewCell;

			public object SelectedItems {get; set;}

			public InternalPickerViewModel (PickerViewCell cell, Dictionary<int,PickerViewCellComponent> dataDict) {

				mDataDict = dataDict;
				mTableViewCell = cell;
			}

			public override nint GetComponentCount (UIPickerView v)
			{
				return mDataDict.Keys.Count;
			}

			public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
			{
				var aList = mDataDict[(int)component];

				return aList.Items.Count;
			}

			public override string GetTitle (UIPickerView picker, nint row, nint component)
			{
				var aList = mDataDict[(int)component];

				var aRow = aList.Items[(int)row];

				return aRow.DisplayValue;
			}

			public override void Selected (UIPickerView picker, nint row, nint component)
			{
				var selObjctes = new List<object>();

				var aMsg = String.Empty;

				foreach(var aId in mDataDict.Keys)
				{
					var selIndex = picker.SelectedRowInComponent(aId);

					var aList = mDataDict[aId];

					var aRow = aList.Items[(int)selIndex];

					selObjctes.Add(aRow.SelectedValue);

					aMsg += aRow.DisplayValue + mTableViewCell.SeperatorCharacter;

				}

				if (aMsg.EndsWith(mTableViewCell.SeperatorCharacter))
					aMsg = aMsg.Substring(0,aMsg.LastIndexOf(mTableViewCell.SeperatorCharacter));

				mTableViewCell.RightLabel.Text = aMsg;

				SelectedItems = selObjctes.ToArray();

				mTableViewCell.DidSelectItem(selObjctes.ToArray());

			}

			public override nfloat GetComponentWidth (UIPickerView picker, nint component)
			{
				var aList = mDataDict[(int)component];

				if (aList.Width == -1)
					return picker.Frame.Size.Width;
				
				return aList.Width;

			}

			public override nfloat GetRowHeight (UIPickerView picker, nint component)
			{
				return 40f;
			}

			/// <summary>
			/// Updates the selected items.
			/// </summary>
			/// <param name="picker">Picker.</param>
			/// <param name="value">Value.</param>
			public void UpdateSelectedItems(UIPickerView picker,object value)
			{
				if (value == null)
					SelectedItems = null;
				
				if (value is object[])
				{
					var vals = (object[])value;

					var aMsg = String.Empty;

					for (int index = 0; index < vals.Length; index++)
					{
						var theVal = vals[index];

						if (mDataDict.Count == 0)
							return;

						var aList = mDataDict[index];

						if (theVal is int)
						{
							//index of the selected item
							var iVal = (int)theVal;

							if (iVal < aList.Items.Count 
								&& iVal >= 0)
							{
								var aItem = aList.Items[iVal];


								aMsg += aItem.DisplayValue + mTableViewCell.SeperatorCharacter;
							}


							picker.Select(iVal,index,true);
						}
						else if (theVal is string)
						{
							//selected value of the selected item
							var sVal = (string)theVal;

							foreach (var aItem in aList.Items)
							{
								if (aItem.SelectedValue != null 
									&& aItem.SelectedValue is String)
								{
									if (((string)aItem.SelectedValue).ToLower().Equals(sVal.ToLower()))
									{
										picker.Select(aList.Items.IndexOf(aItem),index,true);

										aMsg += aItem.DisplayValue + mTableViewCell.SeperatorCharacter;
									}
								}
								else if (aItem.SelectedValue != null)
								{
									if (aItem.SelectedValue == theVal)
									{
										picker.Select(aList.Items.IndexOf(aItem),index,true);

										aMsg += aItem.DisplayValue + mTableViewCell.SeperatorCharacter;
									}
								}

							}


						}
					}

					if (aMsg.EndsWith(mTableViewCell.SeperatorCharacter))
						aMsg = aMsg.Substring(0,aMsg.LastIndexOf(mTableViewCell.SeperatorCharacter));

					mTableViewCell.RightLabel.Text = aMsg;

					SelectedItems = vals;
				}
			}
		}
	}
}

