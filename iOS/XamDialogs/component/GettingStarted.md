Custom dialogs for picking dates and simple lists

## Usage ##

Both the `XamSimplePickerDialog` and `XamDatePickerDialog` dialog classes are based on `XamDialogView` which can easily be sub-classed to create your own custom dialogs.  

Currently there are two standard dialogs, though more are planned in the future.

- Date Picker
- Simple list picker

### XamDialogView ###

`XamDialogView` is the base class for the pre-defined dialogs and for your own sub-clasess.  It contains properties for showing, hiding and configuring the dialogs which are detailed below.  

#### Properties ####

 - Title
  - Gets/Sets the title of the dialog
 - TitleLabelTextColor
  - Gets/Sets the Color of the title label. Also sets the colour of the button text
 - Message
  - Gets/Sets the message of the dialog
 - MessageLabelTextColor
  - Gets/Sets the color of the message label.
 - SubmitButtonText
  - The text to appear on the submit button
 - CancelButtonText
  - The text appear on the cancel button
 - BlurEffectStyle
  - The type of blur effect to use
 - ConstantUpdates
  - Gets/sets whether the value you be updated constantly or just on submit
 - ButtonMode
  - Mode for the display of the Cancel/Submit buttons(Both, Cancel only or Submit Only)
 - DisableBackgroundOverlay
  - Disable the overlay that covers the content underneath the dialog
 - BackgroundOverlayColor
  - Set the color of the overlay that covers the content underneath the dialog
  
#### Methods ####

 - Show
  - Show the dialog
 - Hide
  - Hide the dialog
  
#### Events ####

 - OnCancel
  - Called when the cancel button is pressed
  
The submit event is provided on the sub-class as it will be typed to the data that matches the type of dialog.

### XamSimplePickerDialog ###

`XamSimplePickerDialog` is a dialog with a simple UIPickerView with a single component.  This can be used to present a simple list.  

To create the picker simply provide the constructor with a list of strings to display.

			var dialog = new XamSimplePickerDialog(new List<String>(){"Ringo","John","Paul", "George"})
			{
				Title = "Favorite Beatle",
				Message = "Pick your favorite beatle",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Cancel",
				ConstantUpdates = false,
			};
				
			dialog.OnSelectedItemChanged += (object s, string e) => 
			{
				Console.WriteLine(e);
			};

			dialog.SelectedItem = "John";

			dialog.Show();

You can also use an awaitable static method called `ShowDialogAsync` to show the dialog and await the result.  

			var result = await XamSimplePickerDialog.ShowDialogAsync("Who are you?","Select your name", new List<String>(){"Dave","Rob","Jamie"}, "Rob");
			Console.WriteLine(result);
			
### Show() ###

By default Show() will add the view to the first window in the system, but this may not be the behaviour your are looking for.

We have added some new overloads for `Show`

 - Show()
   - Default behaviour using the first system window
 - Show(UIViewController vc)
   - Show the dialog on the specified view contoller, attaching to its view
 - Show(UIView view)
   - Attach the dialog to the specified view


#### Properties ####

 - SelectedItem
  - Gets/Sets the selected item
 - ValidateSubmit
  - Gets/Set function to handle validation of the selection on submit

#### Events ####

 - OnSelectedItemChanged
  - Called when the selected item has changed, if `ConstantUpdates` is true, or when the submit button has been clicked.  
  

### XamDatePickerDialog ###

`XamDatePickerDialog` is a dialog with a `UIDatePicker` view embedded into the dialog.

To create the picker simply provide the constructor with the `UIDatePickerMode` setting, to set the mode of the `UIDatePicker`  

			var dialog = new XamDatePickerDialog(UIDatePickerMode.DateAndTime)
			{
				Title = "Date Picker",
				Message = "Please Pick a date and time",
				BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
				CancelButtonText = "Cancel",
				ConstantUpdates = false,
			};
				
			dialog.SelectedDate = new DateTime(1969,7,20,20,18,00,00);

			dialog.ButtonMode = ButtonMode.OkAndCancel;

			dialog.ValidateSubmit = (DateTime data)=>
			{
				return true;
			};

			dialog.OnSelectedDateChanged += (object s, DateTime e) => 
			{
				Console.WriteLine(e);
			};

			dialog.Show();

You can also use an awaitable static method called `ShowDialogAsync` to show the dialog and await the result.  

			var result = await XamDatePickerDialog.ShowDialogAsync(UIDatePickerMode.DateAndTime,"Date of Birth","Select your Date of Birth", new DateTime(1969,7,20,20,18,00,00) );

			Console.WriteLine(result);

#### Properties ####

 - SelectedItem
  - Gets/Sets the selected item
 - ValidateSubmit
  - Gets/Set function to handle validation of the selection on submit

#### Events ####

 - OnSelectedItemChanged
  - Called when the selected item has changed, if `ConstantUpdates` is true, or when the submit button has been clicked.  
  
## Sub-classing ##

`XamDialogView` is an abstract class, allowing you to easily sub-class it to produce your own dialogs very easily, as demonstrated by the `CustomDialog` in the sample.  

You will need to implement three abstract methods and a single property, which are detailed below.  Addtionally your constructor will need to call the base `XamDialogView` constructor that takes an `XamDialogType` value.  

#### Methods ####

 - CanSubmit
  - Determins if the submit can be processes
 - HandleCancel
  - Called when the cancel button is pressed
 - HandleSubmit
  - Called whe the submit button is pressed
  
#### Properties ####

 - ContentView
  - Returns the custom `UIView` to be embedded into the Dialog