
**NumberProgressBar** is a slim progress indicator which displays a numeric percentage 
alongside the progress bar.  

## Usage

Use it in your own code:

    <com.daimajia.numberprogressbar.NumberProgressBar
        android:id="@+id/number_progress_bar"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content" />

There are some pre-deifined styles that can be used:

 - Default (`NumberProgressBar_Default`)
 - Passing Green (`NumberProgressBar_Passing_Green`)
 - Relax Blue (`NumberProgressBar_Relax_Blue`)
 - Grace Yellow (`NumberProgressBar_Grace_Yellow`)
 - Warning Red (`NumberProgressBar_Warning_Red`)
 - Funny Orange (`NumberProgressBar_Funny_Orange`)
 - Beauty Red (`NumberProgressBar_Beauty_Red`)
 - Twinkle Night (`NumberProgressBar_Twinkle_Night`)

These styles can be applied using the `style` attribute:

    <com.daimajia.numberprogressbar.NumberProgressBar
        android:id="@+id/number_progress_bar"
        style="@style/NumberProgressBar_Default" />

### Attributes

There are several attributes you can set:

![](http://ww2.sinaimg.cn/mw690/610dc034jw1efyttukr1zj20eg04bmx9.jpg)

The **reached area** and **unreached area**:

* color
* height 

The **text area**:

* color
* text size
* visibility
* distance between **reached area** and **unreached area**

The **bar**:

* max progress
* current progress

These attributes can be set in the layout file as well:

    <com.daimajia.numberprogressbar.NumberProgressBar
        app:max="100"
        app:progress="90"
        
        app:progress_unreached_color="#77d065"
        app:progress_reached_color="#3498db"
        
        app:progress_text_size="10sp"
        app:progress_text_color="#2c3e50"
        app:progress_text_offset="1dp"
        app:progress_text_visibility="visible"
        
        app:progress_reached_bar_height="1.5dp"
        app:progress_unreached_bar_height="0.75dp" />
