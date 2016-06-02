
<iframe src="https://appetize.io/embed/g0nb5k4b395u331xy0q970vn10?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

An Android library that provides explosions for any and all your Android views.

## Usage

Before exploding views, the `ExplosionView` must be attached to the `Activity`:

    var explosions = ExplosionView.Attach(this);
    
Then, the views can be exploded, rendering it `Invisible` but not `Gone`:

    explosions.Explode(myView);
    
To be notified when a view is no longer visible, there is an overload:

    explosions.Explode(myView, () => {
        // myView is invisible
        myView.Visibility = ViewStates.Gone;
    });
    
To restore the view to its original form, `Visible`:

    explosions.Reset(myView);

The size of the explosions can also be controlled:

    explosions.ExpansionLimit = 250; // + px