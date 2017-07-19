**ShimmerLayout** can be used to add shimmer effect (like the one used at Facebook or at LinkedIn)
to your Android application. Beside memory efficiency even animating a big layout, you can modify
the shimmer color and the speed of the animation as well.

## Usage

Create the layout on which you want to apply the effect and add as a child of a `ShimmerLayout`:

```xml
<io.supercharge.shimmerlayout.ShimmerLayout
    android:id="@+id/shimmer_text"
    android:layout_width="wrap_content"
    android:layout_height="40dp"
    android:layout_gravity="center_horizontal"
    android:layout_marginTop="50dp"
    android:paddingLeft="30dp"
    android:paddingRight="30dp">

    <TextView
        android:layout_width="wrap_content"
        android:layout_height="match_parent"
        android:gravity="center"
        android:text="ShimmerLayout"
        android:textColor="@color/shimmer_background_color"
        android:textSize="26sp"/>

</io.supercharge.shimmerlayout.ShimmerLayout>
```

Last, but not least you have to start it from your code:

```csharp
var shimmerText = FindViewById<ShimmerLayout>(Resource.Id.shimmer_text);
shimmerText.StartShimmerAnimation();
```

## Customization

In addition to the default shimmer, there are a couple of ways to customize
or enhance the shimmer effect:

 - `shimmer_color` - specify a color to use as the shimmer
 - `shimmer_animation_duration` - the duration of the shimmer in milliseconds

```xml
<io.supercharge.shimmerlayout.ShimmerLayout
        ...
        app:shimmer_color="@color/shimmer_color"
        app:shimmer_animation_duration="1200">

        ...

</io.supercharge.shimmerlayout.ShimmerLayout>
```
