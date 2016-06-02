
**SWTableViewCell** is an easy-to-use UITableViewCell subclass that implements a 
swipeable content view which exposes utility buttons (similar to iOS 7 Mail 
Application)

## Functionality

### Right Utility Buttons
Utility buttons that become visible on the right side of the Table View Cell 
when the user swipes left. This behavior is similar to that seen in the iOS 
apps Mail and Reminders.

### Left Utility Buttons
Utility buttons that become visible on the left side of the Table View Cell 
when the user swipes right. 

### Features
* Dynamic utility button scaling: 
  As you add more buttons to a cell, the other buttons on that side get smaller 
  to make room
* Smart selection: 
  The cell will pick up touch events and either scroll the cell back to center 
  or fire the delegate method `RowSelected()`.  
  The cell will not be considered selected when the user touches the cell while 
  utility buttons are visible, instead the cell will slide back into place (same 
  as iOS 7 Mail App functionality)
* Create utility buttons with either a title or an icon along with a RGB color

## Usage

### Standard Table View Cells

In your table view delegate, the `GetCell()` method you set up the `SWTableViewCell` 
and add an arbitrary amount of utility buttons to it, using the 
`LeftUtilityButtons` and `RightUtilityButtons` properties of the 
`SWTableViewCell` instance.

To make use of `SWTableViewCell` from the designer, without having to create a 
new type, just specify "SWTableViewCell" as the class. 

### Custom Table View Cells

The first step is to design your cell either in a standalone nib or inside of a 
table view using prototype cells. Make sure to set the custom class on the cell 
in interface builder to the subclass you made for it. Then your new type should 
be a subclass of `SWTableViewCell`

If you are using a separate nib and not a prototype cell, you'll need to be sure 
to register the nib in your table view.

Then, in the `GetCell` method of your `UITableViewDelegate`, initialize your 
custom cell.

### Delegate

The delegate `ISWTableViewCellDelegate` is used by the developer to find out 
which button was pressed, along with a few other useful events:

    // informs that a new state (left, right, center) is happening
    void ScrollingToState(SWTableViewCell cell, SWCellState state)
    
    // informs that a utility button has been tapped on the right
    void DidTriggerLeftUtilityButton(SWTableViewCell cell, int index)
    
    // informs that a utility button has been tapped on the left
    void DidTriggerRightUtilityButton(SWTableViewCell cell, int index)
    
    // when a new cell is opening, ask whether should it close this one 
    bool ShouldHideUtilityButtonsOnSwipe(SWTableViewCell cell)
  
    // ask whether a certain cell can be opened
    bool CanSwipeToState(SWTableViewCell cell, SWCellState state)

The index signifies which utility button the user pressed, for each side the 
button indices are ordered from right to left 0...n
