The Liquid Floating Action Button is a floating action button in liquid state.

##  Using LiquidFloatingActionButton

`LiquidFloatingActionButton` can be used in both storyboards and code-behind. Once instantiated, it can be used to
provide an expanding menu:

    var frame = new CGRect(16, 16, 56, 56);
    var button = new LiquidFloatingActionButton(frame);
    
    // add cells
    button.Cells = new List<LiquidFloatingCell> {
        new LiquidFloatingCell(UIImage.FromBundle("first")),
        new LiquidFloatingCell(UIImage.FromBundle("second")),
        new LiquidFloatingCell(UIImage.FromBundle("third"))
    };

    // wait for events
    button.CellSelected += (sender, e) => {
        // read the event values
        var selectedCell = e.Cell;
        var selectedCellIndex = e.Index;
        
        // close the button
        button.Close();
    };

## Using Properties

`LiquidFloatingActionButton` is fully supported in storyboards and in the storyboard designer. 
There are several properties that are available in the designer or code:

  * `AnimateStyle`  
    The direction that the cells open in (Up/Down/Left/Right)
    
  * `CellRadiusRatio`  
    The size ratio of the cells to the button
    
  * `Cells`  
    The collection of cells that will appear when the button is tapped
    
  * `Color`  
    The color of the button and the cells
    
  * `EnableShadow`  
    The value representing whether the button and the cells have shadows
    
  * `IsClosed`  
    The value representing whether the button is expanded
    
  * `Responsible`  
    The value representing whether the cells automatically lighten when tapped
    
## Using Events

Along with the various properties, there is support for listening to events:
    
  * `CellSelected`  
    The event that is raised when a cell is selected

## Using LiquidFloatingCell Properties

Each `LiquidFloatingCell` added to the `LiquidFloatingActionButton` also has a set of properties:
    
  * `ActionButton`  
    The button that contains this cell
    
  * `Responsible`  
    The value representing whether the cells automatically lighten when tapped
    
  * `View`  
    The view that is used to provide the cell icon (usually a `UIImageView`) 
