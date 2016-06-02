
SidePanels is a UIViewController container designed for presenting a center panel with revealable side panels - one to the left and one to the right. 

##  Using Code

To make use of SidePanels, we can set up the interface with four view controllers, in a similar fashon to a `UINavigationController`. 
Instead of just hosting a single view controller at a time, a `SidePanelController` can host up to three. The center view 
controller is required, and then there are optional left and right view controllers.

    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private MyMainViewController viewController;

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // set up the actual view controller here
            viewController = new SidePanelController();

            // set up the controller panes here
            viewController.LeftPanel = new MyLeftViewController();
            viewController.CenterPanel = new UINavigationController(new MyCenterViewController());
            viewController.RightPanel = new MyRightViewController();

            // off we go
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = viewController;
            Window.MakeKeyAndVisible();

            return true;
        }
    }

## Using Storyboards

When using storyboards, we can provide the same functionality. Using SidePanels with storyboards just takes a few steps:

  1. Create/load a storyboard, and add three view controllers (or four for a right panel)
  2. Set the `Class` of field to a class name, such as `MyMainViewController`
  3. Set the `Storyboard ID` field of the other two (or other three) to a value, such as `leftViewController` and `centerViewController`
  4. Instantiate and set the panels using `Storyboard.InstantiateViewController("storyboard_id")`

In the generated `MyMainViewController.cs` file, override the `AwakeFromNib` method:

    public override void AwakeFromNib()
    {
        base.AwakeFromNib();
     
        CenterPanel = Storyboard.InstantiateViewController("centerViewController");
        LeftPanel = Storyboard.InstantiateViewController("leftViewController");
        RightPanel = Storyboard.InstantiateViewController("rightViewController");
    }

## Extension Method

An extension method is also provided in the project. This adds a single convenience method to `UIViewController`. 
The method provides access to the nearest `SidePanelController` ancestor in your view controller heirarchy. 
It behaves similar to the `NavigationController` property on the `UIViewController` type.

    public class MyLeftViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var controller = this.GetSidePanelController();
            controller.ShowCenterPanelAnimated(true);
        }
    }
