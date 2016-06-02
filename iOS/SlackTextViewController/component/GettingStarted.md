
**SlackTextViewController** is a drop-in UIViewController subclass with a growing 
text input view and other useful messaging features. Meant to be a replacement for 
`UITableViewController` & `UICollectionViewController`.

This library is used in Slack's iOS app. It was built to fit our needs, but is 
flexible enough to be reused by others wanting to build great messaging apps 
for iOS.

## Features

### Core
- Works out of the box with `UITableView`, `UICollectionView` or `UIScrollView`
- Growing Text View, with line count limit support
- Flexible UI built with Auto Layout
- Customizable: provides left and right button, and toolbar outlets
- Tap Gesture for dismissing the keyboard
- External keyboard commands support
- Undo/Redo (with keyboard commands and UIMenuController)
- Text Appending APIs

### Additional
- Autocomplete Mode by registering any prefix key (`@`, `#`, `/`)
- Edit Mode
- Typing Indicator display
- Shake Gesture for clearing text view
- Multimedia Pasting (png, gif, mov, etc.)
- Inverted Mode for displaying cells upside-down (using `CATransform`) 
  -- a necessary hack for some messaging apps. `true` by default, so beware, 
  your entire cells might be flipped!
- Tap Gesture for dismissing the keyboard
- Panning Gesture for sliding down/up the keyboard
- Hiddable TextInputbar
- Dynamic Type for adjusting automatically the text input bar height based on the 
  font size.
- Bouncy Animations

### Compatibility
- iOS 7, 8 & 9
- iPhone & iPad
- Storyboard
- `UIPopOverController` & `UITabBarController`
- Container View Controller
- Auto-Rotation
- iPad Multitasking (iOS 9 only)
- Localization

## How to use

### Subclassing

`SlackTextViewController` is meant to be subclassed, like you would normally do with
`UITableViewController`, `UICollectionViewController` or `UIScrollView`. This 
pattern is a convenient way of extending `UIViewController`. 
`SlackTextViewController` manages a lot behind the scenes while still providing 
the ability to add custom behaviours. You may override methods, and decide to 
call super and  perform additional logic, or not to call super and override 
default logic.

Start by creating a new subclass of `SlackTextViewController`.

For the constructor, if you wish to use the `UITableView` version:

    public ViewController()
        : base(UITableViewStyle.Plain)
	{
	}

or the `UICollectionView` version:

    public ViewController()
        : base(new UICollectionViewFlowLayout())
	{
	}

or the `UIScrollView` version:

    public ViewController()
        : base(new UIScrollView())
	{
	}

Protocols like `UITableViewDelegate` and `UITableViewDataSource` are already setup 
for you. You will be able to call whatever delegate and data source methods you 
need for customising your control.

Using the deafult base constructor will use the `UITableView`.


### Storyboard

When using `SlackTextViewController` with storyboards, instead of overriding the 
traditional constructor you will need to override any of the two custom methods 
below. 

This approach helps preserving the exact same features from the programatic 
approach, but also limits the edition of the nib of your `SlackTextViewController` 
subclass since it doesn't layout subviews from the nib (subviews are still 
initialized and layed out programatically).

If you wish to use the `UITableView` version, add:

    [Export("tableViewStyleForCoder:")]
    private static UITableViewStyle GetTableViewStyleForCoder(NSCoder decoder)
    {
        return UITableViewStyle.Plain;
    }

or the `UICollectionView` version:

    [Export("collectionViewLayoutForCoder:")]
    private static UICollectionViewLayout GetCollectionViewLayoutForCoder(NSCoder decoder)
    {
        return new UICollectionViewFlowLayout();
    }


### Growing Text View

The text view expands automatically when a new line is required, until it reaches 
its `MaxNumberOfLines` value. You may change this property's value in the 
`TextView`.

By default, the number of lines is set to best fit each device dimensions:
- iPhone 4      (<=480pts): 4 lines
- iPhone 5/6    (>=568pts): 6 lines
- iPad          (>=768pts): 8 lines

On iPhone devices, in landscape orientation, the maximum number of lines is 
changed to fit the available space.


### Inverted Mode

Some layouts may require to show from bottom to top and new subviews are inserted 
from the bottom. To enable this, you must use the `Inverted` flag property 
(default is `true`). This will actually invert the entire scroll view object. 

Make sure to apply the same transformation to every subview. In the case of 
`UITableView`, the best place for adjusting the transformation is in its data source 
methods like:

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	{
		var cell = TableView.DequeueReusableCell(MessengerCellIdentifier);
		cell.Transform = TableView.Transform;
	}


### Autocompletion

We use autocompletion for many things: names, channels, emoji, and more.

To set up autocompletion in your app, follow these simple steps:

#### 1. Registration
You must first register all the prefixes you'd like to support for autocompletion 
detection:

    RegisterPrefixesForAutoCompletion(new [] { "#" });

#### 2. Processing
Every time a new character is inserted in the text view, the nearest word to the 
caret will be processed and verified if it contains any of the registered prefixes.

Once the prefix has been detected, `DidChangeAutoCompletionPrefix()` will be called. 
This is the perfect place to populate your data source and show/hide the 
autocompletion view. So you must override it in your subclass, to be able to 
perform additional tasks. Default returns `false`.

	public override void DidChangeAutoCompletionPrefix(string prefix, string word)
	{
		// filter
		searchResult = new string[0];
		if (prefix == "#" && word.Length > 0) {
			searchResult = channels.Where(c => c.StartsWith(word, StringComparison.CurrentCultureIgnoreCase)).ToArray();
		}
		
		// sort
		Array.Sort(searchResult, StringComparer.CurrentCultureIgnoreCase);
		
		// show autocomplete
		var show = (searchResult.Length > 0);
		ShowAutoCompletionView(show);
	}

The autocompletion view is a `UITableView` instance, so you will need to use 
`UITableViewDataSource` to populate its cells. You have complete freedom for 
customizing the cells.

You don't need to call `ReloadData()` yourself, since it will be invoked 
automatically right after calling the `ShowAutoCompletionView()` method.

#### 3. Layout
The maximum height of the autocompletion view is set to 140 pts by default. You 
can update this value anytime, so the view automatically adjusts based on the 
amount of displayed cells.

    public override nfloat HeightForAutoCompletionView {
        get {
            var cellHeight = 34.0f;
            return cellHeight * searchResult.Length;
        }
    }

#### 4. Confirmation
If the user selects any autocompletion view cell on `RowSelected()`, you must call 
`AcceptAutoCompletion` to commit autocompletion. That method expects a string 
matching the selected item, that you would like to be inserted in the text view.

    [Export("tableView:didSelectRowAtIndexPath:")]
    public void RowSelected(UITableView tableView, NSIndexPath indexPath)
	{
		// make sure it is the autocomplete
		if (AutoCompletionView == tableView) {
			var item = searchResult[indexPath.Row];
			AcceptAutoCompletion(item, true);
		}
	}

The autocompletion view will automatically be dismissed and the chosen string will 
be inserted in the text view, replacing the detected prefix and word.

You can always call `CancelAutoCompletion()` to exit the autocompletion mode and 
refresh the UI.


### Edit Mode

To enable edit mode, you simply need to call `EditText()`, and the text input 
will switch to edit mode, removing both left and right buttons, extending the 
input bar a bit higher with "Accept" and "Cancel" buttons. Both of this buttons 
are accessible in the `SlackTextInputbar` instance for customisation.

To capture the "Accept" or "Cancel" events, you must override the following 
methods:

		public override void DidCommitTextEditing (NSObject sender)
		{
			// fetch
			var message = TextView.Text;
			
			// add
			messages.RemoveAt(0);
			messages.Insert(0, message);
			
			// refresh
			TableView.ReloadData();
			
			base.DidCommitTextEditing(sender);
		}
		
		public override void DidCancelTextEditing(NSObject sender)
		{
			base.DidCancelTextEditing(sender);
		}

Notice that you must call `base` at some point, so the text input exits the edit 
mode, re-adjusting the layout and clearing the text view.

Use the `Editing` property to know if the editing mode is on.


### Typing Indicator

Optionally, you can enable a simple typing indicator, which will be displayed 
right above the text input. It shows the name of the people that are typing, and 
if more than 2, it will display "Several are typing" message.

To enable the typing indicator, just call:

    var username = "current user";
    TypingIndicatorView.InsertUsername(username);

and the view will automatically be animated on top of the text input. After a 
default interval of 6 seconds, if the same name hasn't been assigned once more, 
the view will be dismissed with animation.

You can remove names from the list by calling:

    var username = "current user";
    TypingIndicatorView.RemoveUsername(username);

You can also dismiss it by calling:

    TypingIndicatorView.DismissIndicator();


### Panning Gesture

Dismissing the keyboard with a panning gesture is enabled by default with the 
`KeyboardPanningEnabled` property. You can always disable it if you'd like. You 
can extend the `VerticalPanGesture` behaviors with the `UIGestureRecognizerDelegate` 
methods.


### Hiddable TextInputbar

Sometimes you may need to hide the text input bar. Very similar to 
`UINavigationViewController`'s API, simple do:

    SetTextInputbarHidden(true, true);


### Shake Gesture

A shake gesture to clear text is enabled by default with the `ShakeToClearEnabled` 
property.

You can optionally override `WillRequestUndo()`, to implement your UI to ask the 
users if he would like to clean the text view's text. If there is not text entered, 
the method will not be called.

If you don't override `WillRequestUndo` and `ShakeToClearEnabled` is set to `true`, 
a system `UIAlertView` will appear with an undo option. 


### External Keyboard

There a few basic key commands enabled by default:
- cmd + z -> undo
- shift + cmd + z -> redo
- return key -> calls `DidPressRightButton()`, or `DidCommitTextEditing()` if in 
  edit mode
- shift/cmd + return key -> line break
- escape key -> exits edit mode, or auto-completion mode, or dismisses the keyboard
- up & down arrows -> vertical cursor movement

To add additional key commands, simply override `KeyCommands` and append 
`base`'s array.

    public override UIKeyCommand[] KeyCommands {
		get {
			var commands = base.KeyCommands.ToList();
			commands.Add(UIKeyCommand.Create(UIKeyCommand.UpArrow, 0, delegate {
				// edit last message
			}));
			return commands.ToArray();
		}
	}


### Dynamic Type

Dynamic Type is enabled by default with the `KeyboardPanningEnabled` property. You 
can always disable it if you'd like.
