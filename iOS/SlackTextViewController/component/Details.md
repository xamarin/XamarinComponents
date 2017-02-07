
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
