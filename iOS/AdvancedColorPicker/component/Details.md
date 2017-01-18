# AdvancedColorPicker

An open source color picker component for Xamarin.iOS that is very easy to use.

## Usage

AdvancedColorPicker is very simple and easy to use. There are two helper methods 
that allow for quickly presenting a color picker. 
The first is `PresentAsync` that returns the selected color:

    var color = await ColorPickerViewController.PresentAsync(
        NavigationController, 
        "Pick a color!",
        View.BackgroundColor);
    
    // use selected color
        
In the case when async method aren't preferrable, there is the synchronous
`Present` method that takes a callback:

    ColorPickerViewController.Present(
        NavigationController, 
        "Pick a color!",
        View.BackgroundColor,
        color => {
            // use selected color
        });

If there is need to embed the picker into another view, this can be done
using `ColorPickerView`:

    var colorPicker = new ColorPickerView();
    colorPicker.ColorPicked += (sender, e) => {
        var color = e.SelectedColor;
        
        // use selected color
    };
