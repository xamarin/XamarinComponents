
<iframe src="https://appetize.io/embed/b9knv620997np08wtr2cgetmu0?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

A fast ImageView (and Drawable) that supports rounded corners with many more advanced
features like support for ovals, rounded rectangles, ScaleTypes and TileModes.

## Usage

A `RoundedImageView` provides many advantages over a traditional `ImageView`, and can easily 
be added to your layout instead of `ImageView`:

    <com.makeramen.roundedimageview.RoundedImageView
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/imageView1"
        android:src="@drawable/photo1"
        android:scaleType="fitCenter"
        app:riv_corner_radius="30dip"
        app:riv_border_width="2dip"
        app:riv_border_color="#333333"
        app:riv_mutate_background="true"
        app:riv_tile_mode="repeat"
        app:riv_oval="true" />
        
This view can also be constructed in code:

    RoundedImageView riv = new RoundedImageView(context);
    riv.SetScaleType(ImageView.ScaleType.CenterCrop);
    riv.CornerRadius = 10;
    riv.BorderWidth = 2;
    riv.BorderColor = Color.DarkGray;
    riv.MutatesBackground = true;
    riv.SetImageDrawable(drawable);
    riv.SetBackground(backgroundDrawable);
    riv.IsOval = true;
    riv.TileModeX = Shader.TileMode.Repeat;
    riv.TileModeY = Shader.TileMode.Repeat;

## Compatibility

Since `RoundedImageView` is a direct extension of `ImageView`, it seamlessly works 
out-the-box with your favorite image loader library, such as Picasso:

    var transformation = new RoundedTransformationBuilder()
        .BorderColor(Color.Black)
        .BorderWidthDp(3)
        .CornerRadiusDp(30)
        .Oval(false)
        .Build();

    Picasso.With(context)
        .Load(url)
        .Fit()
        .Transform(transformation)
        .Into(imageView);

