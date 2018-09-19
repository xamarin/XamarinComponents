# What are Material Design and Material Components for iOS?

**Material Design** is a system for building bold and beautiful digital products. By uniting style, branding, interaction, and motion under a consistent set of principles and components, product teams can realize their greatest design potential.

**Material Components** for iOS unites design and engineering with a library of components for creating consistency across apps and websites. As the Material Design system evolves, these components are updated to ensure consistent pixel-perfect implementation and adherence to Google's front-end development standards.

Table of contents:

- [What are Material Design and Material Components for iOS?](#what-are-material-design-and-material-components-for-ios)
- [Animation timing](#animation-timing)
	- [Examples](#examples)
		- [Using Animation Timing](#using-animation-timing)
- [App bars: bottom](#app-bars-bottom)
- [App bars: top](#app-bars-top)
	- [Example](#example)
	- [Header stack view](#header-stack-view)
		- [Example](#example)
	- [Navigation bar](#navigation-bar)
- [Bottom navigation](#bottom-navigation)
- [Buttons](#buttons)
	- [Example](#example)
- [Cards](#cards)
	- [Examples](#examples)
- [Feature highlight](#feature-highlight)
- [Flexible header](#flexible-header)
- [Ink](#ink)
- [Library Info](#library-info)
- [Masked transitions](#masked-transitions)
- [Page control](#page-control)
- [Palettes](#palettes)
- [Progress Indicators](#progress-indicators)
	- [Circular progress & activity indicator](#circular-progress--activity-indicator)
	- [Progress view](#progress-view)
- [Shadow elevations](#shadow-elevations)
- [Sheets: bottom](#sheets-bottom)
- [Slider](#slider)
- [Snackbar](#snackbar)
	- [Overlay window](#overlay-window)
- [Tabs](#tabs)
- [Text fields](#text-fields)
- [Theming](#theming)
	- [Color scheme](#color-scheme)
	- [Typography Scheme](#typography-scheme)

---

# Animation timing

Like color and typography, motion can play a role in defining your app’s style and brand. The animation timing component provides implementations of the Material Motion easing curve types for iOS.

<img width="400" src="https://material.io/components/images/content/dcfc2db26195037e1edf0a28186e40f2.gif" />

## Examples

### Using Animation Timing

To use an animation timing curve select an appropriate a predefined `AnimationTimingFunction` enum value. Use this value to look up an animation curve’s timing function. The timing function can then be used in an animation.

```csharp
var materialCurve = AnimationTimingFunction.Deceleration;
var timingFunction = CAMediaTimingFunctionAnimationTiming.GetFunction (materialCurve);

var animation = CABasicAnimation.FromKeyPath ("transform.translation.x");
animation.TimingFunction = timingFunction;
```

---

# App bars: bottom

A bottom app bar displays navigation and key actions at the bottom of the screen. Bottom app bars work like navigation bars, but with the additional option to show a floating action button.

<img width="400" src="https://material.io/components/images/content/7bb2ee50db0ff605e34e2249c55796cd.png" />

`BottomAppBarView` can be added to a view hierarchy like any `UIView`. Material Design guidelines recommend always placing the bottom app bar at the bottom of the screen.

---

# App bars: top

The Material Design top app bar displays information and actions relating to the current view.

<img width="400" src="https://material.io/components/images/content/44f36a1675da121828059c35cb7657d5.gif" />

## Example

The easiest integration path for using the app bar is through the `AppBarNavigationController`. This API is a subclass of `UINavigationController` that automatically adds an `AppBarViewController` instance to each view controller that is pushed onto it, unless an app bar or flexible header already exists.

When using the `AppBarNavigationController` you will, at a minimum, need to configure the added app bar’s background color using the delegate.

```csharp
var navigationController = new AppBarNavigationController ();
navigationController.PushViewController (viewController, true);

#region Material Components AppBarNavigationController Delegate

[Export ("appBarNavigationController:willAddAppBarViewController:asChildOfViewController:")]
public void WillAddAppBarViewController (AppBarNavigationController navigationController, AppBarViewController appBarViewController, UIViewController viewController)
{

	appBarViewController.HeaderView.BackgroundColor = UIColor.LightGray;
}

#endregion
```

## Header stack view

The header stack view component is a view that coordinates the layout of two vertically stacked bar views.

<img width="400" src="https://material.io/components/images/content/24bd3734a01b50c7a3f3e8aed2123c86.png" />

This view’s sole purpose is to facilitate the relative layout of two horizontal bars. The bottom bar will bottom align and be of fixed height. The top bar will stretch to fill the remaining space if there is any.

The top bar is typically a navigation bar. The bottom bar, when provided, is typically a tab bar.

### Example

Header stack view provides `HeaderStackView`, which is a `UIView` subclass.

```csharp
var headerStackView = new HeaderStackView ();
```

You may provide a top bar:

```csharp
headerStackView.TopBar = navigationBar;
```

You may provide a bottom bar:

```csharp
headerStackView.BottomBar = tabBar;
```

## Navigation bar

A navigation bar is a view composed of leading and trailing buttons and either a title label or a custom title view.

<img width="400" src="https://material.io/components/images/content/58573961bb04670b5200930010ac0790.png" />

---

# Bottom navigation

Bottom navigation bars allow movement between primary destinations in an app. Tapping on a bottom navigation icon takes you directly to the associated view or refreshes the currently active view.

<img width="400" src="https://material.io/components/images/content/a25433b519c2d4d02bf0c18fd0fee6d1.gif" />

---

# Buttons

Material design buttons allow users to take actions, and make choices, with a single tap. There are many distinct button styles including text buttons, contained buttons, and floating action buttons.

<p>
	<img height="50" src="https://material.io/components/images/content/087078f924695ba285b40d7b154b1908.gif" />
	<img height="50" src="https://material.io/components/images/content/4a6654772356e2242e1af31018a61ec1.gif" />
	<img height="50" src="https://material.io/components/images/content/6ee7fe0988e6d27a2444598aba596d51.gif" />
	<img height="50" src="https://material.io/components/images/content/cbd76e89520828b8cbdc506d7464cafe.gif" />
</p>

## Example

Create an instance of `Button` and theme it with as one of the Material Design button styles using the `ButtonThemer` extension. Once themed, use the button like you would use a typical `UIButton` instance.

```csharp
var button = new Button ();
TextButtonThemer.ApplyScheme (buttonScheme, button);
```

---

# Cards

Cards contain content and actions about a single subject. They can be used standalone, or as part of a list. Cards are meant to be interactive, and aren’t meant to be be used solely for style purposes.

<img width="300" src="https://material.io/components/images/content/23ec45fc360d81066c95dc1e1c7be71c.png" />

## Examples

`Card` can be used like a regular UIView.

```csharp
var card = new Card ();

// Create, position, and add content views:
var imageView = new UIImageView ();
card.AddSubview (imageView);
```

---

# Feature highlight

The Feature Highlight component is a way to visually highlight a part of the screen in order to introduce users to new features and functionality.

<img width="300" src="https://material.io/components/images/content/58db8e7845c188627d95a0043e5ee3a0.png" />

---

# Flexible header

A flexible header is a container view whose height and vertical offset react to UIScrollViewDelegate events.

<img width="400" src="https://material.io/components/images/content/fab35fda115143f462ea609ef6219900.gif" />

---

# Ink

The Ink component provides a radial action in the form of a visual ripple expanding outward from the user’s touch.

<img width="400" src="https://material.io/components/images/content/7d6b5c689cd1cea96fde3f6c34754aa8.gif" />

---

# Library Info

Library info contains programmatic access to information about the Material Components library.

---

# Masked transitions

A masked transition reveals content from a source view using a view controller transition.

---

# Page control

This control is designed to be a drop-in replacement for `UIPageControl`, with a user experience influenced by Material Design specifications for animation and layout. The API methods are the same as a `UIPageControl`, with the addition of a few key methods required to achieve the desired animation of the control.

<img width="400" src="https://material.io/components/images/content/9f0aa5816c12703dcce15799831f3295.gif" />

---

# Palettes

The Palettes component provides Material colors organized into similar palettes.

---

# Progress Indicators

## Circular progress & activity indicator

Material Design progress indicators display the length of a process or express an unspecified wait time. There are two styles of progress indicators: linear and circular.

<img width="100" src="https://material.io/components/images/content/46d4059f334af7a760c809c06458dcff.gif" />

## Progress view

Progress view is a linear progress indicator that implements Material Design animation and layout.

<img width="300" src="https://material.io/components/images/content/2a9e9cfe8eb9e18eef890373145dd676.png" />

---

# Shadow elevations

A shadow elevation specifies the degree of shadow intensity to be displayed beneath an object. Higher shadow elevations have greater shadow intensities, akin to raising an object above a surface resulting in a more prominent, albeit more diffuse, shadow. This component provides commonly used Material Design elevations for components.

<img width="300" src="https://material.io/components/images/content/42c37dd2bb0d442b352789d959c3472e.png" />

---

# Sheets: bottom

Bottom sheets slide up from the bottom of the screen to reveal more content. Bottom sheets integrate with the app to display supporting content or present deep-linked content from other apps.

<img width="300" src="https://material.io/components/images/content/78b25c314ea96fdc4058920f23777679.png" />

---

# Slider

The Slider object is a Material Design control used to select a value from a continuous range or discrete set of values.

<img width="300" src="https://material.io/components/images/content/92e2f57e2e220e0b4e025659942cb919.png" />

---

# Snackbar

Snackbars provide brief feedback about an operation through a message at the bottom of the screen. Snackbars contain up to two lines of text directly related to the operation performed. They may contain a text action, but no icons.

<img width="300" src="https://material.io/components/images/content/cdca9845aff189c7f2ebb01c1d2d3170.png" />

## Overlay window

Provides a window which can have an arbitrary number of overlay views that will sit above the root view of the window. Overlays will be the full size of the screen, and will be rotated as appropriate based on device orientation. For performance, owners of overlay views should set the |hidden| property to YES when the overlay is not in use.

Overlay Window is used by components such as Snackbar. Snackbar uses Overlay Window to ensure displayed message views are always visible to the user by being at the top of the view hierarchy.

---

# Tabs

Tabs are bars of buttons used to navigate between groups of content.

<img width="300" src="https://material.io/components/images/content/247639e27fe65fd41ac20270f399495f.png" />

---

# Text fields

Text fields allow users to input text into your app. They are a direct connection to your users’ thoughts and intentions via on-screen, or physical, keyboard. The Material Design Text Fields take the familiar element to new levels by adding useful animations, character counts, helper text, error states, and styles.

<img width="300" src="https://material.io/components/images/content/a80b32cb9e8c650aba867fbec7f7e96b.png" />

---

# Theming

Material Theming refers to the customization of your Material Design app to better reflect your product’s brand.

## Color scheme

The Material Design color system can be used to create a color theme that reflects your brand or style. A color scheme represents your theme’s specific color values, such as its primary color and the surface colors of views.

## Typography Scheme

The Material Design typography system can be used to create a type hierarchy that reflects your brand or style. A typography scheme represents your theme’s specific fonts, such as its body font or button font.
