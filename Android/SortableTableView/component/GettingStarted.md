
An Android library containing a simple `TableView` and an advanced `SortableTableView` providing a 
lot of customisation possibilities to fit all needs.

## Features

### Layouting

#### Column Count

The provided TableView is very easy to adapt to your needs. To set the column count simple set the 
parameter inside your XML layout:

    <de.codecrafters.tableview.TableView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/tableView"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        app:columnCount="4" />

A second possibility to define the column count of your TableView is to set it directly in the code:

    var tableView = FindViewById<TableView>(Resource.Id.tableView);
    tableView.ColumnCount = 4;

#### Column Width

To define the relative width of your columns you can define a specific *weight* for each of them 
(as you may know from [LinearLayout](http://developer.android.com/guide/topics/ui/layout/linear.html)). 
By default the weight of each column is set to 1. So every column has the same width. 
To make the first column (index of first column is 0) twice as wide as the other columns simple do the 
following call:

    tableView.SetColumnWeight(0, 2);

Because the width of an column is not given absolute but relative, the `TableView` will adapt to all 
screen sizes.

### Showing Data

#### Simple Data

For displaying simple data like a 2D `string` array you can use the `SimpleTableDataAdapter`. 
The `SimpleTableDataAdapter` will turn the given `string` to 
[TextViews](http://developer.android.com/reference/android/widget/TextView.html) and display them 
inside the TableView at the same position as previous in the 2D `string` array:

    var dataToShow = new [] {
        new [] { "first", "row", "data", "here" },
        new [] { "the", "next", "row's", "data" },
        new [] { "the", "last", "row's", "data" }
    };
    tableView.DataAdapter = new SimpleTableDataAdapter(this, dataToShow);

#### Custom Data

For displaying more complex custom data you need to implement your own `TableDataAdapter`. Therefore 
you need to implement the `GetCellView(int rowIndex, int columnIndex, ViewGroup parentView)` method. 

This method is called for every table cell and needs to returned the 
[View](http://developer.android.com/reference/android/view/View.html) that shall be displayed in the 
cell with the given *rowIndex* and *columnIndex*:

    public class CarTableDataAdapter : TableDataAdapter
    {
        public override View getCellView(int rowIndex, int columnIndex, ViewGroup parentView) 
        {
            // get the row data
            var rowData = GetRowData(rowIndex);
            
            // create the cell for each column and cell
            View renderedView = null;
            switch (columnIndex) {
                case 0:
                    renderedView = RenderFirstColumnCell(rowData);
                    break;
                case 1:
                    renderedView = RenderSecondColumnCell(rowData);
                    break;
                case 2:
                    renderedView = RenderThirdColumnCell(rowData);
                    break;
                case 3:
                    renderedView = RenderFourthColumnCell(rowData);
                    break;
            }
            return renderedView;
        }
    }

#### Sortable Data

If you need to make your data sortable, you should use the `SortableTableView` instead of the 
ordinary `TableView`. To make a table sortable by a column, all you need to do is to implement 
a [Comparator](http://docs.oracle.com/javase/7/docs/api/java/util/Comparator.html) and set it to 
the specific column:

    var sortableTableView = FindViewById<SortableTableView>(Resource.Id.sortableTableView);
    sortableTableView.SetColumnComparator(0, new CarProducerComparator());

    private class CarProducerComparator : Java.Lang.Object, IComparator
    {
        public int Compare(Java.Lang.Object lhs, Java.Lang.Object rhs)
        {
            var car1 = (Car)lhs;
            var car2 = (Car)rhs;
            
            return car1.Producer.Name.CompareTo(car2.Producer.Name);
        }
    }

By doing so the `SortableTableView` will automatically display a sortable indicator next to the table 
header of the column with the index 0. By clicking this table header, the table is sorted ascending 
with the given `IComparator`. If the table header is clicked again, it will be sorted in descending 
order.

#### Header Data

Setting data to the header views is identical to setting data to the table cells. All you need to do 
is extending the `TableHeaderAdapter` which is also providing the easy access methods that are 
described for the `TableDataAdapter`.  

If all you want to display in the header is the column title as String (like in most cases) the 
`SimpleTableHeaderAdapter` will fulfil your needs.

### Events

#### Data Click Event

To listen for clicks on data items you can register an event handler. The`TableView` provides 
an event called `DataClick` to do so:

    tableView.DataClick += (sender, e) => {
        int rowIndex = e.RowIndex;
        object rowData = e.ClickedData;
    };

#### Header Click Event

To listen for clicks on headers you can register an event handler. The `TableView` provides an event 
called `HeaderClick` to do so:

    tableView.HeaderClick += (sender, e) => {
        int columnIndex = e.ColumnIndex;
    };

### Styling

#### Header Styling

The table view provides several possibilities to style its header. One possibility is to set a 
**colour** for the header. Therefore you can adapt the XML file:

    <de.codecrafters.tableview.TableView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/tableView"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        app:headerColor="@color/primary" />
        
Or, add it to your code:
 
    tableView.SetHeaderBackgroundColor(Resources.Color(Resource.Color.primary));

For more complex header styles you can also set a **drawable** as header background using the 
following method:

    tableView.SetHeaderBackground(Resource.Drawable.linear_gradient);

Additionally, you can set an **elevation** of the table header. To achieve this you have the 
possibility to set the elevation in XML:

    <de.codecrafters.tableview.TableView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/tableView"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        app:headerElevation="10" />

Or, alternatively set it in your code:
 
    tableView.SetHeaderElevation(10);

For a `SortableTableView` it is also possible to replace the default **sortable indicator icons** by 
your custom ones. To do so you need to implement the `ISortStateViewProvider` and set it to your 
`SortableTableView`:

    sortableTableView.SetHeaderSortStateViewProvider(new MySortStateViewProvider());

    private class MySortStateViewProvider : Java.Lang.Object, ISortStateViewProvider
    {
        public override int GetSortStateViewResource(SortState state)
        {
            if (state == SortState.Sortable) {
                return Resource.Drawable.Sortable;
            } else if (state == SortState.SortedAsc) {
                return Resource.Drawable.SortedAsc;
            } else if (state == SortState.SortedDesc) {
                return Resource.Drawable.SortedDesc;
            } else {
                return -1;
            }
        }
    }

There is also a factory class existing called `SortStateViewProviders` where you can get some 
predefined implementations of the `ISortStateViewProvider`.

#### Data Row Styling

In general you can do all your styling of data content in your custom `TableDataAdapter`. But if you 
want to add colouring of whole table rows you can use the `ITableDataRowColoriser`. There are already 
some implementations of the `ITableDataRowColoriser` existing in the library. You can get the by using 
the Factory class `TableDataRowColorisers`.  

This Factory contains for example an alternating-table-data-row coloriser that will colour rows with 
even index different from rows with odd index:

    var even = Resources.GetColor(Resource.Color.white);
    var odd = Resources.GetColor(Resource.Color.gray);
    tableView.setDataRowColoriser(TableDataRowColorisers.AlternatingRows(even, odd));

If the implementations of `ITableDataRowColoriser` contained in the `TableDataRowColorisers` factory 
don't fulfil you needs you can create your own implementation of `ITableDataRowColoriser`. Here is a 
small example of how to do so:

    tableView.SetDataRowColoriser(new CarPriceRowColoriser());
    
    private class CarPriceRowColoriser : Java.Lang.Object, ITableDataRowColoriser
    {
        public int GetRowColor(int rowIndex, Java.Lang.Object obj)
        {
            var car = (Car)obj;
            var rowColor = Resources(Resource.Color.white);
            if (car.Price < 50000) {
                rowColor = getResources(Resource.Color.light_green);
            } else if (car.Price > 100000) {
                rowColor = getResources(Resource.Color.light_red);
            }
            return rowColor;
        }
    }

This coloriser will set the background colour of each row corresponding to the price of the car that 
is displayed at in this row. Cheap cars (less then 50,000) get a green background, expensive cars 
(more then 100,000) get a red background and all other cars get a white background.
  