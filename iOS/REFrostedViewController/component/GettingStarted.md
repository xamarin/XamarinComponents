# REFrostedViewController  

iOS 7/8 style blurred view controller that appears on top of your view controller.
  
Usage  
----

**Building the controllers**

In order to setup the refrosted view controller you need to setup a navigation controller and a menu controller.

You can create any type of menu controller that you want, but we have provided and default implementation to make it easier for you.  

`REFrostedMenuViewController` is designed to make it easy to provide a menu structure, Title and Avatar image without the need to sub-class anything.

Below is an example of using `REFrostedMenuViewController` and `RENavigationController` to produce the side menu.  See the 'Defining a menu' section for details on building the menu structure.  

			// ViewControllers
			var aRootVC = new DEMOHomeViewController();
			var secondVC = new DEMOSecondViewController();
			
			//build the default navigation controller and menu controller
			var navigationController = new RENavigationController(aRootVC);
			var menuController = new REFrostedMenuViewController()
			{
				Avatar = UIImage.FromBundle(@"monkey.png"),
				AvatarName = @"Xamarin Monkey",
				Sections = sections,
			};

			//  Setup the frosted view controller
			var frostedViewController = new REFrostedViewController.REFrostedViewController(navigationController, menuController)
			{
				Direction = REFrostedViewControllerDirection.Left,
				LiveBlurBackgroundStyle = REFrostedViewControllerLiveBackgroundStyle.Light,
				LiveBlur = true,
				Delegate = this,
			};
			
			this.Window.RootViewController = frostedViewController;
			this.Window.BackgroundColor = UIColor.White;
			this.Window.MakeKeyAndVisible();


There are a number of properties on the `REFrostedMenuViewController` class that can affect the appearance of the menu.

 - `TintColor`
   - Change the tint color of the avatar label, the menu items and the icons
 - `Avatar`
   - The image to show in the menu
 - `AvatarName`
   - The name to appear below the avatar in the menu
   

**Defining a menu**  
  
The API for the `REFrostedMenuViewController` for Xamarin.iOS is different to the original API, in that it is now designed to be used as a library rather than compiled in to you application as source.  
  
As such this means that it works differently.  
  
The menu is made up of a list of `REMenuItem` items, or rather sub-classes of `REMenuItem`.  We provide two such classes(and a Generic version of one) to enable you to define both View Controllers and Actions in your menu.  
  
 - `REMenuActionItem`  
  - Call an `Action` on the UI thread when the item is selected  
 - `REMenuViewControllerItem` and `REMenuViewControllerItem<T>`  
  - Attach and show a view controller when the item is shown.  
  - `REMenuViewControllerItem<T>` can generate a new instance of the view controller each time the item is selected or keep a single instance of it

To enable you to group menu items together we provide the `REMenuItemSection` class.  This allows you define a section title and add the items to each section as you wish.
 
	
			//define the menu structure
			var sections = new List<REMenuItemSection>()
			{
				
				new REMenuItemSection()
				{
					Items = new List<REMenuItem>()
					{
						new REMenuViewControllerItem()
						{
							//View exisiting view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"home-48"),
							Title = @"Home",
							ViewController = aRootVC,
						},
						new REMenuViewControllerItem()
						{
							//New view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"about-48"),
							Title = @"Profile",
							ViewController = secondVC,
						},
					},
				},
				new REMenuItemSection()
				{
					Title = "Friends Online",
					Items = new List<REMenuItem>()
					{
						new REMenuViewControllerItem()
						{
							//View exisiting view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"business_contact-48"),
							Title = @"John Appleseed",
							ViewController = secondVC,
						},
						new REMenuActionItem()
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
					},
				},
			};
			
			var menuController = new REFrostedMenuViewController()
			{
				Sections = sections,
			};

  
You can then set this to the `Sections` property of the `REFrostedMenuViewController` instance.  

  
  
Attribution  
----
  
This component is a port to C# from the original Objective-C repo created by [Roman Efimov](https://github.com/romaonthego/REFrostedViewController)
	
	
 

