A library that provides explosions for any and all of your Xamarin.Android views.

## Usage

Before exploding views, the `ExplosionView` must be attached to the `Activity`:

```csharp
var explosions = ExplosionView.Attach(this);
```

Then, the views can be exploded, rendering it `Invisible` but not `Gone`:

```csharp
explosions.Explode(myView);
```

To be notified when a view is no longer visible, there is an overload:

```csharp
explosions.Explode(myView, () => {
    // myView is invisible
    myView.Visibility = ViewStates.Gone;
});
```

To restore the view to its original form, `Visible`:

```csharp
explosions.Reset(myView);
```

The size of the explosions can also be controlled:

```csharp
explosions.ExpansionLimit = 250; // + px
```
