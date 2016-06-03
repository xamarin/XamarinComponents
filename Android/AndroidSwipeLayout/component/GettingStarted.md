
**Android Swipe Layout** provides a layout which enables view to be swiped 
away to reveal other views underneath.

This library can be used anywhere, such as in a `ListView`, `GridView`, 
`RecyclerView` or just another `View`. There are several events that can
be subscribed to, such as `Closed`, `Opened` or `Update`.

The layouts can be nested within each other, even allowing draggable 
controls to till function.

## Usage

First, create a `SwipeLayout`, with the last child being the `SurfaceView`,
or the main view that is swiped out of the way. The other children are
the `BottomViews`.

    <com.daimajia.swipe.SwipeLayout 
        xmlns:android="http://schemas.android.com/apk/res/android"
        android:layout_width="match_parent" 
        android:layout_height="80dp">
        
        <!-- Bottom View Start-->
        <LinearLayout
            android:background="#66ddff00"
            android:id="@+id/bottom_wrapper"
            android:layout_width="160dp"
            android:weightSum="1"
            android:layout_height="match_parent">
            <!--What you want to show-->
        </LinearLayout>
        <!-- Bottom View End-->
        
        <!-- Surface View Start -->
        <LinearLayout
            android:padding="10dp"
            android:background="#ffffff"
            android:layout_width="match_parent"
            android:layout_height="match_parent">
            <!--What you want to show in SurfaceView-->
        </LinearLayout>
        <!-- Surface View End -->
        
    </com.daimajia.swipe.SwipeLayout>

Then, in the code, you can set up the layouts for the swipe.

    // get SwipeLayout instance
    swipeLayout = FindViewById<SwipeLayout>(Resource.Id.sample1);

    // set show mode
    swipeLayout.SetShowMode(SwipeLayout.ShowMode.LayDown);
    
    // add drag edge
    // NOTE: if the BottomView has 'layout_gravity' attribute, 
    //       this line is unnecessary
    var bottomView = FindViewById(Resource.Id.bottom_wrapper);
    swipeLayout.AddDrag(SwipeLayout.DragEdge.Left, bottomView);
    
    swipeLayout.Opening += (sender, e) => {
        // the SurfaceView has started moving to open
    };
    swipeLayout.Opened += (sender, e) => {
        // the BottomView is completely visible
    };
    swipeLayout.Update += (sender, e) => {
        // you are swiping
        var left = e.LeftOffset;
        var top = e.TopOffset;
    };
    swipeLayout.Closing += (sender, e) => {
        // the SurfaceView has started moving to close
    };
    swipeLayout.Closed += (sender, e) => {
        // when the SurfaceView totally covers the BottomView
    };
    swipeLayout.HandRelease += (sender, e) => {
        // when user's hand releases the SurfaceView
    };

## Using SwipeAdapter

`SwipeAdapter` is a set of helper members to save time when using
scrollable views. It can help save and restore `SwipeLayout` status 
(`Open` | `Middle` | `Close`) when using `ListView` or `GridView`.

**Provided Adapters**
- `BaseSwipeAdapter`
- `ArraySwipeAdapter`
- `CursorSwipeAdapter`
- `SimpleCursorSwipeAdapter`

### BaseSwipeAdapter 

#### Usage

`BaseSwipeAdapter` is extended from `BaseAdapter`, so it functions 
the same. However, it's a little bit different from `BaseAdapter`. 
You don't need to override `GetView()` method, but there are 3 new 
methods you do need to override.
The `SwipeAdapter` will automatically maintain the `convertView`.

    public override int GetSwipeLayoutResourceId(int position)
    {
        // return the `SwipeLayout` resource id in your listview | gridview item layout.
    }    

    public override View GenerateView(int position, ViewGroup parent)
    {
        // render a new item layout.
    }
    
    public override void FillValues(int position, View convertView)
    {
        // fill values to your item layout returned from "GenerateView".
        // The position param here is passed from the BaseAdapter's "GetView"
    }

#### Example

    public class GridViewAdapter : BaseSwipeAdapter 
    {
        private Context context;
        
        public GridViewAdapter(Context context)
        {
            this.context = context;
        }

        public override int GetSwipeLayoutResourceId(int position)
        {
            return Resource.Id.swipe;
        }
    
        public override View GenerateView (int position, ViewGroup parent)
        {
            var view = LayoutInflater.From (context).Inflate (Resource.Layout.grid_item, null);
            view.FindViewById (Resource.Id.trash).Click += (sender, e) => {
                var pos = (int)view.Tag;
                Toast.MakeText (context, "click delete " + pos, ToastLength.Short).Show ();
            };
            return view;
        }

        public override void FillValues (int position, View convertView)
        {
            convertView.Tag = position;

            var t = convertView.FindViewById<TextView> (Resource.Id.position);
            t.Text = (position + 1) + ".";
        }

		public override int Count {
			get { return 50; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return position;
		}
    }

### ArraySwipeAdapter, CursorSwipeAdapter, SimpleCursorAdapter Usage

These three adapters are just like:
 - [`ArrayAdapter<T>`](http://developer.android.com/reference/android/widget/ArrayAdapter.html)
 - [`CursorAdapter`](http://developer.android.com/reference/android/widget/CursorAdapter.html)
 - [`SimpleCursorAdapter`](http://developer.android.com/reference/android/widget/SimpleCursorAdapter.html)

All of these 3 classes require an implementation that overrides
`GetSwipeLayoutResourceId`:

    public int GetSwipeLayoutResourceId(int position)
    {
        // return the SwipeLayout resource id in the layout.
    }
