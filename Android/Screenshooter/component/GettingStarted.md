The simplest way to take a screenshot of any view in your app.

## Usage

One way to take a screen shot of a view is to use the `Snap` method:

    var bitmap = Shooter.Snap(myView)
    
If we want to use the resource ID, then we pass that to `Snap`:

    var bitmap = Shooter.Snap(activity, Resource.Id.myView);
    
In the cases where we want the entire `Activity` or `Dialog`, we pass
that to `Snap`:

    var bitmap = Shooter.Snap(this);