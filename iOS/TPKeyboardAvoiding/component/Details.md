
<iframe src="https://appetize.io/embed/zafeap5j0bugwk5pm74kauat38?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

**TPKeyboardAvoiding** is a relatively universal, drop-in solution: `UIScrollView`, 
`UITableView` and `UICollectionView` subclasses that handle everything. 

## Features

When the keyboard is about to appear, the subclass will find the subview that's about to be 
edited, and adjust its frame and content offset to make sure that view is visible, 
with an animation to match the keyboard pop-up. 

When the keyboard disappears, it restores its prior size.

It also automatically hooks up "Next" buttons on the keyboard to switch through the text fields.

## Usage

TPKeyboardAvoiding should work with basically any setup, either a `UITableView`-based 
interface, or one consisting of views placed manually.

For non-`UITableViewController`s, use it as-is by popping a `UIScrollView` into your view 
controller’s interface file, setting the class to `TPKeyboardAvoidingScrollView`, and 
putting all your controls within that scroll view.

To use it with `UITableViewController` just make your `UITableView` a 
`TPKeyboardAvoidingTableView` in the interface file — everything should be taken care of.

To disable the automatic "Next" button functionality, change the `UITextField`'s return 
key type to anything but `UIReturnKeyType.Default`.

## Notes

These classes currently adjust the `ContentInset` parameter to avoid content moving beneath 
the keyboard. 

This is done, as opposed to adjusting the frame, in order to work around an 
iOS bug that results in a jerky animation where the view jumps upwards, before settling 
down. In order to facilitate this workaround, the `ContentSize` is maintained to be at 
least same size as the view's frame.

## Why TPKeyboardAvoiding?

There are a hundred and one proposed solutions out there for how to move `UITextField`
and `UITextView` out of the way of the keyboard during editing — usually, it comes down 
to observing `UIKeyboardWillShowNotification` and `UIKeyboardWillHideNotification`, or 
implementing `UITextFieldDelegate` delegate methods, and adjusting the frame of the 
superview, or using `UITableView`‘s `ScrollToRow()` method, but all the proposed 
solutions I’ve found tend to be quite DIY, and have to be implemented for each view 
controller that needs it.
