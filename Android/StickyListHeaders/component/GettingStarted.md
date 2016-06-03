
StickyListHeaders is an Android library that makes it easy to integrate section 
headers in your `ListView`. These section headers stick to the top like in the 
new People app of Android 4.0 Ice Cream Sandwich. This behavior is also found 
in lists with sections on iOS devices. This library can also be used without the 
sticky functionality if you just want section headers.

StickyListHeaders actively supports Android versions 2.3 (gingerbread) and above.
That said, it works all the way down to 2.1 but is not actively tested or working
perfectly.

## Using

The `StickyListHeadersListView` view can be added to layout files:

    <se.emilsjolander.stickylistheaders.StickyListHeadersListView
      android:id="@+id/list"
      android:layout_width="match_parent"
      android:layout_height="match_parent" />

The list view can be accessed like any list view:

    var stickyList = FindViewById<StickyListHeadersListView>(Resource.Id.list);
    var adapter = new MyAdapter(this);
    stickyList.Adapter = adapter;

The `IStickyListHeadersAdapter` interface can be added to a `IListAdapter` 
implementation:

    public class MyAdapter : BaseAdapter, IStickyListHeadersAdapter
    {
      public View GetHeaderView(int position, View convertView, ViewGroup parent)
      {
        // return a re-usable view
      }
      public long GetHeaderId(int position)
      {
        // return a static ID for each header
      }
    }

## Styling

We can apply our own theme to a `StickyListHeadersListView` instance. For 
example, we can define a style called `Widget.MyApp.ListView` in the 
`values/styles.xml` resource folder:

    <resources>
      <style name="Widget.MyApp.ListView" parent="@android:style/Widget.ListView">
        <item name="android:paddingLeft">@dimen/vertical_padding</item>
        <item name="android:paddingRight">@dimen/vertical_padding</item>
      </style>
    </resources>

Then, we can apply this style to all `StickyListHeadersListView` instances by 
adding a resource to our app theme definition with the
`stickyListHeadersListViewStyle` element:

    <resources>
      <style name="Theme.MyApp" parent="android:Theme.NoTitleBar">
        <item name="stickyListHeadersListViewStyle">@style/Widget.MyApp.ListView</item>
      </style>
    </resources>

## Expandable Support

We can also use `ExpandableStickyListHeadersListView` to expand/collapse subitems. 
We make use of the `ExpandableStickyListHeadersListView` element in our layout
resources:

    <se.emilsjolander.stickylistheaders.ExpandableStickyListHeadersListView
      android:id="@+id/list"
      android:layout_width="match_parent"
      android:layout_height="match_parent" />

Then we need to setup our expandable list view, using the same adapter:

    var stickyList = FindViewById<ExpandableStickyListHeadersListView>(Resource.Id.list);
    var adapter = new MyAdapter(this);
    stickyList.Adapter = adapter;
    stickyList.Click += (sender, e) => {
      if (stickyList.IsHeaderCollapsed(e.HeaderId)) {
        stickyList.Expand(e.HeaderId);
      } else {
        stickyList.Collapse(e.HeaderId);
      }
    };

The method `IsHeaderCollapsed` is used to check whether the subitems belonging 
to the header have collapsed. We can invoke `Expand` or `Collapse` method to 
hide or show subitems. 

A custom animation executor can be assigened to the list view using the
`SetAnimExecutor` method. This can be used to customize the collapse/expand
animations.

## Properties

The sticky headers can be controlled by various properties.Here are some that 
may be used:

 * **AreHeadersSticky**  
   Specifies whether the headers will stick.
   
 * **DrawingListUnderStickyHeader**  
   Specifies whether the list items are drawn under the stuck headers.
   
 * **EmptyView**    
   Provide a view that will be displayed when there are no items in the adapter.
   
 * **WrappedList**  
   Provided access to the underlying list view.
   
