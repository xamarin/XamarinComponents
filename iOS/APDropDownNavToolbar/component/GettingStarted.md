APDropDownNavToolbar
====================

iOS7 Messages App style toolbar that drops down from navigation bar when tapping on the UIBarButton item
  
Usage  
----
  
Use `APNavigationController` instead of the standard `UINavigationController` to make the sub-toolbar available to your `UIViewController` instances.  

	var navController = new APNavigationController(new DemoViewController()
	{
		Title = "Sample",
	});
		

	// If you have defined a root view controller, set it here:
	Window.RootViewController = navController;
	
	// make the window visible
	Window.MakeKeyAndVisible();
  
Within your `UIViewController` you can now cast the `NavigationController` property to `APNavigationController` so you can start settting properties.  

	public APNavigationController NavController 
	{
		get;
		set;
	}
	
	public override void ViewDidLoad()
	{
		base.ViewDidLoad();
		this.View.BackgroundColor = UIColor.White;
		if (this.NavigationController != null 
			&& this.NavigationController is APNavigationController)
		{
			NavController = (APNavigationController)this.NavigationController;
			NavController.ActiveBarButtonTitle = "Hide";
			NavController.ActiveNavigationBarTitle = "Tool bar visible";
			//set the right button to show the menu
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem("Show", UIBarButtonItemStyle.Plain, DidSelectShow);
		}
	}  
		
Lastly on the button click event set the items of the toolbar. 

	public void DidSelectShow(object sender, EventArgs evt)
	{
		this.NavController.DropDownToolbar.Items = new UIBarButtonItem[]
		{
			new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
			new UIBarButtonItem(UIBarButtonSystemItem.Edit),
			new UIBarButtonItem(UIBarButtonSystemItem.Action),
		};
	    if(this.NavController.IsDropDownVisible)
		{
			this.NavController.HideDropDown(sender);
		}
	        else
		{
			this.NavController.ShowDropDown(sender);
		}
	}
	
	
Attribution  
----
  
This component is a port to C# from the original Objective-C repo created by [Ankur Patel](https://github.com/ankurp/APDropDownNavToolbar)
	
	
 

