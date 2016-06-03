
<iframe src="https://appetize.io/embed/zereg0cbdyw2yy0jdtpudevrjw?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

Add `AnimatedCircleLoadingView` to your layout and define `mainColor` and `secondaryColor` as custom 
attributes:

    <com.github.jlmd.animatedcircleloadingview.AnimatedCircleLoadingView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/circle_loading_view"
        android:layout_width="250dp"
        android:layout_height="250dp"
        android:background="@color/background"
        android:layout_centerInParent="true"
        app:mainColor="@color/main_color"
        app:secondaryColor="@color/secondary_color"
        />

## Determinate Usage

Start determinate:

    animatedCircleLoadingView.StartDeterminate();

Modify percent:

    animatedCircleLoadingView.SetPercent(10);

If percent is 100, the animation ends with success animation.  
On error you must invoke the `StopFailure` method, then the application ends with failure animation.

## Indeterminate Usage

Start indeterminate:

    animatedCircleLoadingView.StartIndeterminate();

Stop with success:

    animatedCircleLoadingView.StopOk();

Stop with failure:

    animatedCircleLoadingView.StopFailure();

Reset loading:

    animatedCircleLoadingView.ResetLoading();
