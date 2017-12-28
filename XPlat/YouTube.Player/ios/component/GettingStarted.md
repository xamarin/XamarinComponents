# Getting Started with YouTube Player for iOS

## Add a YouTube PlayerView via Interface Builder or the Storyboard

To add a YouTube `PlayerView` via Interface Builder or the Storyboard:

1. Drag an `UIView` instance onto your View.
2. In **Properties** pad, do the following steps in **Identity** section:
	* Set **PlayerView** in **Name** property.
	* Set `YTPlayerView` in **Class** in property
![Image1](https://raw.githubusercontent.com/xamarin/XamarinComponents/master/XPlat/YouTube.Player/images/ios_image1.png)

3. Go to **ViewController.cs** and add the following code to the end of your `ViewDidLoad` method:

```csharp
PlayerView.LoadVideoById ("M7lc1UVf-VE");
```

Build and run your application. When the video thumbnail loads, tap the video thumbnail to launch the fullscreen video player.

## Control video playback

The `LoadVideoById` method has an overload method, `LoadVideoById (string, NSDictionary)`, that allows developers to pass additional player variables to the view. These player variables correspond to the [player parameters in the JavaScript Player API][1]. The `playsinline` parameter enables the video to play directly in the view rather than playing fullscreen. When a video is playing inline, the containing iOS application can programmatically control playback.

Replace the `LoadVideoById` call with this code:

```csharp
object [] keys = { "playsinline" };
object [] values = { 1 };
var playerVars = NSDictionary.FromObjectsAndKeys (values, keys, keys.Length);
PlayerView.LoadVideoById ("M7lc1UVf-VE", playerVars);
```

Open up the storyboard or Interface Builder. Drag two buttons onto your View, labeling them Play and Stop. Go to **events** tab and type `PlayVideo` in **Touch Up Inside** event for **Play button** and `StopVideo` for **Stop button**.

Now open **ViewController.cs** and define these two functions:

```csharp
partial void PlayVideo (UIButton sender)
{
	PlayerView.PlayVideo();
}

partial void StopVideo (UIButton sender)
{
	PlayerView.StopVideo ();
}
```

Most of the JavaScript Player API functions have C# equivalents, though some of the naming may differ slightly to more closely match C# coding guidelines. Notable exceptions are methods controlling the volume of the video, since these are controlled by the phone hardware or with built in `UIView` instances designed for this purpose, such as [`MPVolumeView`][2].

Now build and run the application. Once the video thumbnail loads, you should be able to play and stop the video using native controls in addition to the player controls.

## Handle player callbacks

It can be useful to programmatically handle playback events, such as playback state changes and playback errors. In the JavaScript API, this is done with [event listeners][3]. In C#, this is done with a [delegate][4] or with events.

### Handle player callbacks with a delegate

The following code shows how to update the class declaration in ViewController.cs so the class conforms to the delegate interface. Change ViewController.cs’ class declaration as follows:

```csharp
public partial class ViewController : UIViewController, IPlayerViewDelegate
```

`IPlayerViewDelegate` is an interface for handling playback events in the player. To update ViewController.cs to handle some of the events, you first need to set the ViewController instance as the `Delegate` of the `PlayerView` instance. To make this change, add the following line to the ViewDidLoad method in ViewController.cs:

```csharp
PlayerView.Delegate = this;
```

Now add the following method to ViewController.cs:

```csharp
[Export ("playerView:didChangeToState:")]
public void DidChangeToState (PlayerView playerView, PlayerState state)
{
	switch (state) {
	case PlayerState.Playing:
		Console.WriteLine ("Started playback");
		break;
	case PlayerState.Paused:
		Console.WriteLine ("Paused playback");
		break;
	case PlayerState.Unstarted:
	case PlayerState.Ended:
	case PlayerState.Buffering:
	case PlayerState.Queued:
	case PlayerState.Unknown:
		break;
	}
}
```

Build and run the application. Watch the log output in Xamarin Studio/Visual Studio as the player state changes. You should see updates when the video is played or stopped.

The library provides the enums that have State, Quality and Error constant values for a better handling. Other useful interface methods include:

```csharp
public void DidBecomeReady (PlayerView playerView);
public void DidChangeToQuality (PlayerView playerView, PlaybackQuality quality);
public void ReceivedError (PlayerView playerView, PlayerError error);
public void DidPlayTime (PlayerView playerView, float playTime);
```

### Handle player callbacks with events

`PlayerView` class has events for handling playback events in the player. To handle the state change, for example, add the following code in `ViewDidLoad` method:

```csharp
PlayerView.StateChanged += (sender, e) => {
	switch (e.State) {
	case PlayerState.Playing:
		Console.WriteLine ("Started playback");
		break;
	case PlayerState.Paused:
		Console.WriteLine ("Paused playback");
		break;
	case PlayerState.Unstarted:
	case PlayerState.Ended:
	case PlayerState.Buffering:
	case PlayerState.Queued:
	case PlayerState.Unknown:
		break;
	}
};
```

Build and run the application. Watch the log output in Xamarin Studio/Visual Studio as the player state changes. You should see updates when the video is played or stopped.

The library provides the enums that have State, Quality and Error constant values for a better handling. Other useful events include:

```csharp
PlayerView.BecameReady
PlayerView.QualityChanged
PlayerView.ErrorReceived
PlayerView.TimePlayed
```

## Best practices and limitations

The library builds on top of the iframe player API by creating a `UIWebView` and rendering the HTML and JavaScript required for a basic player. The library's goal is to be as easy-to-use as possible, bundling methods that developers frequently have to write into a package. There are a few limitations that should be noted:

* The library does not support concurrent video playback in multiple `PlayerView` instances. If your application has multiple `PlayerView` instances, a recommended best practice is to pause or stop playback in any existing instances before starting playback in a different instance.
* Reuse your existing, loaded `PlayerView` instances when possible. When a video needs to be changed in a View, don't create a new `UIView` instance or a new `PlayerView` instance, and don't call either `LoadVideoById` or `LoadPlaylistById`. Instead, use the the `CueVideoById` family of functions, which do not reload the `UIWebView`. There is a noticeable delay when loading the entire iframe player.
* This player cannot play private videos, but it can play unlisted videos. Since this library wraps the existing iframe player, the player's behavior should be nearly identical to that of a player embedded on a webpage in a mobile browser.

<sub>_Portions of this page are modifications based on work created and [shared by Google](https://developers.google.com/readme/policies/) and used according to terms described in the [Creative Commons 3.0 Attribution License](http://creativecommons.org/licenses/by/3.0/). Click [here](https://developers.google.com/youtube/v3/guides/ios_youtube_helper) to see original YouTube documentation._</sub>

[1]: https://developers.google.com/youtube/player_parameters
[2]: https://developer.apple.com/library/ios/documentation/mediaplayer/reference/MPVolumeView_Class/Reference/Reference.html
[3]: https://developers.google.com/youtube/js_api_reference#Adding_event_listener
[4]: https://developer.apple.com/library/ios/documentation/general/conceptual/CocoaEncyclopedia/DelegatesandDataSources/DelegatesandDataSources.html