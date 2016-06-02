
<iframe src="https://appetize.io/embed/r4wzk7xab64ghtn7m5frr6n38w?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

StickyListHeaders is an Android library that makes it easy to integrate section 
headers in your `ListView`. These section headers stick to the top like in the 
new People app of Android 4.0 Ice Cream Sandwich. This behavior is also found 
in lists with sections on iOS devices. This library can also be used without the 
sticky functionality if you just want section headers.

StickyListHeaders actively supports Android versions 2.3 (gingerbread) and above.
That said, it works all the way down to 2.1 but is not actively tested or working
perfectly.

## Goal

The goal of this project is to deliver a high performance replacement to 
`ListView`. You should with minimal effort and time be able to add section 
headers to a list. This should be done via a simple to use API without any 
special features. 

This library will always priorities general use cases over
special ones. This means that the library will add very few public methods to
the standard  ListView  and will not try to work for every use case. While I 
will want to support even narrow use cases I will not do so if it compromises 
the API or any other feature.

## Using

The `StickyListHeadersListView` view can be added to layout files:

    <se.emilsjolander.stickylistheaders.StickyListHeadersListView
        android:id="@+id/list"
        android:layout_width="match_parent"
        android:layout_height="match_parent"/>

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
   
