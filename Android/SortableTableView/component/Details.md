
<iframe src="https://appetize.io/embed/jxt9hnf2bzr534b24hq3jv3bwc?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

An Android library containing a simple `TableView` and an advanced `SortableTableView` providing a 
lot of customisation possibilities to fit all needs.

## Features

The provided TableView is very easy to adapt to your needs and can be added to your XML layout:

    <de.codecrafters.tableview.TableView
        android:id="@+id/tableView"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />

Or, in the case of a sortable table view:

    <de.codecrafters.tableview.SortableTableView
        android:id="@+id/tableView"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />

### Layouting

#### Column Count

To set the column count, simply set the column count of your TableView:

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
        public override View GetCellView(int rowIndex, int columnIndex, ViewGroup parentView) 
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
