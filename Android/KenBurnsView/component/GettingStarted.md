
Android library that provides an extension to ImageView that creates an immersive 
experience by animating its drawable using the [Ken Burns Effect][1].

## Usage

A `KenBurnsView` provides many advantages over a traditional `ImageView`, and can easily 
be added to your layout instead of `ImageView`:

    <com.flaviofaria.kenburnsview.KenBurnsView
        android:id="@+id/imageView"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        android:src="@drawable/image" />

## Control  
   
The duration and interpolator of transitions can be changed:

    var generator = new RandomTransitionGenerator(duration, interpolator);
    kenBurnsView.SetTransitionGenerator(generator);
    
The transition can be started and stopped:

    kenBurnsView.Pause();
    kenBurnsView.Resume();

The transitions have events when starting or stopping:

    kenBurnsView.TransitionStart += (sender, e) => {
    };

    kenBurnsView.TransitionEnd += (sender, e) => {
    };

 ## Extensibility
 
The initial and final view rectangles can be controled when using the `ITransition`and 
the `ITransitionGenerator` types. 

## Compatibility

Since `KenBurnsView` is a direct extension of ImageView, it seamlessly works 
out-the-box with your favorite image loader library.

[1]: http://en.wikipedia.org/wiki/Ken_Burns_effect