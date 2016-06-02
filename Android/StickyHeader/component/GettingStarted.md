

A very simple library that allows you to stick any `View` as a header of a:

 - `ListView`
 - `RecyclerView`
 - `ScrollView`

## Adding a Sticky Header

To stick a header to a list view, we just need a `ListView` and any `View` to use as the header:

    <?xml version="1.0" encoding="utf-8"?>
    <FrameLayout xmlns:android="http://schemas.android.com/apk/res/android"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        android:id="@+id/layout_container">
        <ListView
            android:id="@+id/listview"
            android:layout_width="match_parent"
            android:layout_height="match_parent" />
        <FrameLayout
            android:id="@+id/header"
            android:layout_width="match_parent"
            android:layout_height="@dimen/max_height_header"
            android:background="@android:color/holo_blue_dark">
            <TextView
                android:layout_width="wrap_content"
                android:layout_height="wrap_content"
                android:layout_gravity="center_horizontal|bottom"
                android:layout_marginBottom="10dp"
                android:text="Hello World!"
                android:textSize="25dp" />
        </FrameLayout>
    </FrameLayout>

Once we have the layout inflated, we can use the `StickyHeaderBuilder` to attach the header to the list:

    var listView = container.FindViewById<ListView>(Resource.Id.listview);
    StickyHeaderBuilder
        .StickTo(listView)
        .SetHeader(Resource.Id.header, container)
        .SetMinHeight(250)
        .Apply();

## Using a Sticky Header Animator

There are various animations that can be used when transitioning the header. This is easily done with the `AnimatorBuilder` type. Some of the built in transitions are:

 - Scale
 - Translation
 - Fade
 - Parallax

For simple transitions, we can pass a delegate that returns an `AnimatorBuilder` to the `SetAnimator` method:

    .SetAnimator(() => {
        var image = View.FindViewById(Resource.Id.header_image);
        return AnimatorBuilder
          .Create()
          .ApplyVerticalParallax(image);
    })

If we need more complex transitions, we can inherit from `HeaderStickyAnimator`:

	public class IconActionBarAnimator : HeaderStickyAnimator
	{
		private readonly View homeActionBar;
		private readonly int layoutResource;

		public IconActionBarAnimator(Activity activity, int layoutResource)
		{
			this.layoutResource = layoutResource;
			this.homeActionBar = activity.FindViewById(Android.Resource.Id.Home);
		}

		public override AnimatorBuilder CreateAnimatorBuilder()
		{
			var view = Header.FindViewById(layoutResource);
			var rect = new RectangleF(
				homeActionBar.Left, homeActionBar.Top,
				homeActionBar.Right, homeActionBar.Bottom);
			var point = new PointF(homeActionBar.Left, homeActionBar.Top);
			return AnimatorBuilder
				.Create()
				.ApplyScale(view, rect)
				.ApplyTranslation(view, point);
		}
	}

