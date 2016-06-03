

Some pickers are implemented as a dialog and can be used like the Radial Time Picker:

```csharp
// Create our Picker Dialog
var p = new BetterPickers.RadialTimePickers.RadialTimePickerDialog ();

// Set some options
p.SetStartTime (DateTime.Now.Hour, DateTime.Now.Minute);
p.SetDoneText ("Finish!");

// Wire up the changed handler
p.TimeSet += (sender, e) => {
	ShowToast ("RadialTimePicker Set: Hour={0}, Minute={1}", e.P1, e.P2);
};

// Show the Dialog
p.Show (FragmentManager, null);
```

While others use the builder pattern like the Date Picker:

```csharp
// Create our picker builder
var p = new BetterPickers.DatePickers.DatePickerBuilder ()
                .SetFragmentManager (FragmentManager)
                .SetStyleResId (Resource.Style.BetterPickersDialogFragment_Light);
          
// Add a delegate to handle the picker change      
p.AddDatePickerDialogHandler ((reference, year, month, day) => {
	ShowToast ("DatePicker Set: Ref={0}, Year={1}, Month={2}, Day={3}", reference, year, month, day);
});

// Show the Dialog
p.Show ();
```

## Learn More
To find out more about Better Pickers, you can visit the [original project's GitHub page](https://github.com/derekbrameyer/android-betterpickers).