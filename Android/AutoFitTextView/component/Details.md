
<iframe src="https://appetize.io/embed/4mtvvt42mhzcrhr9xjdu71drm0?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

A TextView that automatically resizes text to fit perfectly within its bounds.

## Usage

Enable any View extending TextView in code:

    AutofitHelper.Create(textView);

Enable any View extending TextView in XML:

    <me.grantland.widget.AutofitLayout
        android:layout_width="match_parent"
        android:layout_height="wrap_content">
        <Button
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:singleLine="true" />
    </me.grantland.widget.AutofitLayout>

Use the built in Widget in code or XML:

    <me.grantland.widget.AutofitTextView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:singleLine="true"
        android:maxLines="2"
        android:textSize="40sp"
        app:minTextSize="16sp" />
