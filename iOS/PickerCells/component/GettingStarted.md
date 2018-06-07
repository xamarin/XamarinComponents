TableViewCell classes for adding inline UIDatePicker and UIPickerView elements into a UITableView.

## Usage ##

The classes in PickerCells can easily be integrated in the an existing `UITableView` or `UITableViewController` as that are simply sub-classes of `UITableViewCell`

There is, however, one caveat that allows the collapsed/expanded state to be preserved.  You must keep a reference to the specific instance or `DataPickerCell` or `PickerViewCell` to return from the `GetCell` call.  If you try to Reuse a cell the that state of the cell will be lost and it will always show as collapsed.

In the sample a simple `List<BasePickerCell>` object stores the cells to be displayed with the `UITableViewSource` sub-class, called `DateTableViewSource`.  

Within the constructor for `DateTableViewSource` we initialise the list object and add the cells to it.  

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
			
**GetCell**

Within `GetCell` you return the cell from the list object that matches the index of the cell item.

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			return mCells[indexPath.Row];
		}
		
**RowsInSection**

You can return the number of rows by returning the `Count` from the list object

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return mCells.Count;
		}

**GetHeightForRow**

You can call `GetCell` to return the current cell and then return the approriate height for the state if `DHPickerCell` based on it state by returning the value from `PickerHeight`

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

#Events#

You can handle changes to the selected value from any of the classes that sub-class `BasePickerCell` by implenenting a handler on the `ItemChanged`  

			aCell.OnItemChanged += (object sender, PickerCellArgs e) => {

				var result = e.Items;
			};
			
`PickerCellArgs` class contains the sender of the event and an `object[]` array with the selected items.

In the case of `DatePickerCell` it sill contain a single `DateTime` object.

In the case of `PickerViewCell` it will contain the selected item from each `Components` of the `UIPickerView`.  

#BasePickerCell#

There are some shared properties that can be used for both `DHPickerCell` and `PickerViewCell`

 - SelectObject
  - The currrent selected object. Note: this maybe an `object[]` for `PickerViewCell`
 - Key
  - Optional object value to allow for additional identification of the cell
 - RightLabelTextColor
  - Color of the right label that contains the selected value
 - RightLabelTextAlignment
  - Text of the right label that contains the selected value
 - LabelFixedWidth
  - Make all the left labels a fixed width

#DatePickerCell#

The `DatePickerCell` class provides a simple `UITableViewCell` sub-class to add an inline `UIDatePicker` to your `UITableView`

To create a new instance of the class simpy provide the `UIDatePickerMode` and an optional `DateTime` object to select the default value.  

			var aCell = new DatePickerCell(UIDatePickerMode.Date,DateTime.Now)
			{
				Key = "1234",
			};
			aCell.TextLabel.Text = "Date";
			aCell.RightLabelTextAlignment = UITextAlignment.Right;
			aCell.OnItemChanged += (object sender, PickerCellArgs e) => {

				var result = e.Items;
			};
			
There are a number properties that can be used to access data and change the way the cell looks

 - Date
   - NSDate representation of the date
 - SelectedDate
   - DateTime representation of the date
 - DateMode
   -  Mode of operation for the `UIDatePicker`
 - TimeStyle
   - `NSDateFormatterStyle` for the time element of the string value
 - DateStyle
    - `NSDateFormatterStyle` for the date element of the string value
 - ValueString
    -  String representation of the selected date, based on the `TimeStyle` and `DateStyle` properties
 
#PickerViewCell#

The `PickerViewCell` provides a `UITableViewCell` sub-classes for selecting multiple items from a `UIPickerView` embedded into your `UITableView`

In order to support multiple-selection we provide the `PickerViewCellComponent` class.  This allow you to define the list of `Items` for each `Component` within the `UIPickerview` as well as specify the width of the `Component`

This is neatly wrapped in a `Dictionary<int,PickerViewCellComponent>` object which is passed to the `PickerViewCell` class when call the constructor

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
				
			var pickerCell = new PickerViewCell(someDict);
			pickerCell.TextLabel.Text = "Artist";
			pickerCell.RightLabelTextAlignment = UITextAlignment.Right;
			pickerCell.SelectedObject = new object[]{1,1};


			pickerCell.OnItemChanged += (object sender, PickerCellArgs e) => {

				var result = e.Items;
			};

			mCells.Add(pickerCell);

The `int` value indicates the index of the `Component` within the `UIPickerView` that the `PickerViewCellComponent` object relates too.

If you only require a single `Component` then you can pass a simple `List<String` object to the constructor instead

			var aList = new List<String> (){ "Ringo", "John", "Paul", "George" };
			var pickerCell = new PickerViewCell(aList);
			

To select a specific object within each `Component` set the `SelectedObject` property with either an `object[]` object with the value, from the list of objects in the relevant `PickerViewCellComponent` instance, for each `Component` or an `object[]` cotaining an `int` value representing the index of the object to select within each `Component`

			pickerCell.SelectedObject = new object[]{1,1};
			
			or
			
			pickerCell.SelectedObject = new object[]{mList[1],mList2[2]};
			
To get the selected items use `SelectedObject` to return the values selected in each `Component` of the `UIPickerView`


	
	
