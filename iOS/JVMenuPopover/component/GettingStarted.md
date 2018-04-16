# JVMenuPopover  

This is a simple menu controller where I tried to simulate the native iOS animation of switching between apps. It can be used in many different ways and you can also customize it to use your own animations.  
  
Usage  
----
  
**Navigation Controller Mode**  
  
If you wish for `JVMenuPopover` to operate as a navigation controller then you will need to use sub-classes of `JVMenuViewController`   
  
This will provide a menu button on each view controller from the menu and handle displaying and switching between views.  
  
Typically this will use a shared menu from `JVMenuPopoverConfig.SharedInstance.MenuItems`.  
  
This method can be seen demonstrated in the `JVMenuPopoveNavigationSample`  
  
**Navigation Controller Mode**  
  
If you would like `JVMenuPopover` to operate as simply a menu then you just need to add a `JVMenuPopoverViewController` object to your view controller and call the `ShowMenuFromController` method to display it.  
  
If you want to have a different menu on each instance of `JVMenuPopoverViewController` you can pass a `List<JVMenuItem>` through as part of the constructor, or it can use a shared menu from `JVMenuPopoverConfig.SharedInstance.MenuItems` if you use the default constructor.   
  
This method can be seen demonstrated in the `JVMenuPopoverMenuSample`  
  
**Defining a menu**  
  
The API for the `JVMenuPopover` for Xamarin.iOS is different to the original API, in that it is now designed to be used as a library rather than compiled in to you application as source.  
  
As such this means that it works differently.  
  
The menu is made up of a list of `JVMenuItem` items, or rather sub-classes of `JVMenuItem`.  We provide two such classes(and a Generic version of one) to enable you to define both View Controllers and Actions in you menu.  
  
 - `JVMenuActionItem`  
  - Call an `Action` on the UI thread when the item is selected  
 - `JVMenuViewControllerItem` and `JVMenuViewControllerItem<T>`  
  - Attach and show a view controller when the item is shown.  
  - `JVMenuViewControllerItem<T>` can generate a new instance of the view controller each time the item is selected or keep a single instance of it

Below is an example of a shared menu definition using `JVMenuPopoverConfig.SharedInstance.MenuItems`  
	
	//build the shared menu
	JVMenuPopoverConfig.SharedInstance.MenuItems = new List<JVMenuItem>()
	{
		new JVMenuViewControllerItem()
		{
			//New view controller, will be reused everytime the item is selected
			Icon = UIImage.FromBundle(@"about-48"),
			Title = @"About Us",
			ViewController = new JVMenuSecondController(),
		},
		new JVMenuViewControllerItem<JVMenuFifthController>()
		{
			//New view controller, will be recreated afresh everytime the item is selected
			Icon = UIImage.FromBundle(@"ask_question-48"),
			Title = @"Help?",
			AlwaysNew = true,
		},
		new JVMenuActionItem()
		{
			//Action is called, on the UI thread, everytime the item is selected
			Icon = UIImage.FromBundle(@"ask_question-48"),
			Title = @"Logout",
			Command = ()=>
			{
				var uiAlert = new UIAlertView("Logout","Are you sure you want to log out?",null,"No","Yes");
				uiAlert.Show();
				},
		},
	};

  
You can also pass the `List<JVMenuItem>` to the constructor of `JVMenuPopoverViewController` and `JVMenuViewController`  

**Configuring the menu appearance** 

`JVMenuPopoverConfig` now has several new properties to configure the appearance of the menu.  This are.  

- FontName   
 - Select the font type to use in the menu  
- FontSize  
 - Select the size of the font  
- RowHeight  
 - Provide the Height of the rows in the menu's tableview  
- TintColor  
 - The Selected tint color for the icons(when using UIImageRenderingMode.AlwaysTemplate) and text  
- MenuImage
 - The image to use for the menu toggle button
- CancelImage
 - The image to use for the cancel button
- DisableMenuImageTinting  
  - Disable application of the tint color to the Menu Image
- DisableCancelImageTinting  
  - Disable application of the tint color to the Cancel Image  

These properties are all access from the singleton instance of the `JVMenuPopoverConfig` class, `JVMenuPopoverConfig.SharedInstance`  
  
  
Attribution  
----
  
This component is a port to C# from the original Objective-C repo created by [Jorge Valbuena](https://github.com/JV17/JVMenuPopover)  
	
	
 

