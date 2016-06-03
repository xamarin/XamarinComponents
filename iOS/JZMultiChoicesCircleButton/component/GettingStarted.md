JZMultiChoicesCircleButton is a Multi-choices button, Just tap it and hold to make your choice! yeah ,so cool, such easy.

## Usage  

JZMultiChoicesCicleButton is wrapped up in a single view called `JZMultiChoicesCircleButton`.

This view has a constructor that takes all of the variables that you need to set up the `JZMultiChoicesCircleButton` instance.

###Build the menu

To define the items in the menu the class `ChoiceItem` is provided and can be passed through to the constructor in a `ChoiceItemCollection` collection.

			var menuItems = new ChoiceItemCollection () {

				new ChoiceItem()
				{
					Title = "Send",
					Icon = UIImage.FromBundle (@"SendRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 1 Selected"); 
						ShowSimple();
					},
					DisableActionAnimation = true,
				},
				new ChoiceItem()
				{
					Title = "Complete",
					Icon = UIImage.FromBundle (@"CompleteRound"),
					Action = ()=>
					{
						Console.WriteLine("Button 2 Selected"); 
						ShowSuccess(true, "YES!!");
					},
				},
			}


Each `ChoiceItem` object has the following properties

 - Title
 - Icon
 - Action
  - Called when the item is selected in the menu
 - DisableActionAnimation
  - Disable showing the completion screen after the action has been called.  Usefull if you don't want to show the completion screen
  
 
###Constructor

The contructor `JZMultiChoicesCircleButton` for takes the following parameters in order
 
  - Center Point
  	- Center point of the view to position with the view controller
  - Icon
   - The image to appear at the center of the button when small
  - Small Radius
  - Big Radius
  - Enable Parallax
   - Enable the parallax effect
  - Parallex depth
   - Depth of the parallex effect
  - View Controller
   - The view controller that is hosting the button
   

###Completion Screen

Once an item has been selected the Completions 'screen' is shown, unless `DisableActionAnimation` is set to yes on the `ChoiceItem` object, and you can show the corresponding complete message using the `ShowCompleteScreen` method.

		public void ShowSuccess(bool success, string message)
		{
			mCircleButton.ShowCompleteScreen (message, success);

		}
 
This will show a message and corresponding image on screen for two seconds and the dismiss the completion screen and return to the normal button. 


