So, you’ve heard about contextual computing, microlocation, beacons, and all that fancy stuff, and now you’ve arrived here to get to the bottom of it all. Cool, because this is the place where you’ll learn how to build a context-aware app with Estimote Beacons and Estimote SDK.

## What is a beacon?

Estimote Beacon is a small computer. Its 32-bit ARM® Cortex M0 CPU is accompanied by accelerometer, temperature sensor, and what is most important—2.4 GHz radio using Bluetooth 4.0 Smart, also known as BLE or Bluetooth low energy.

The greatest advantage of Bluetooth Smart over the previous iterations of BT technology is how energy efficient it is. Thanks to that, and to a lot of work our engineers put into power management, Estimote Beacons can last more than 3 years on default settings on a single CR2477 battery.

Don’t confuse Bluetooth Smart with the first version of Bluetooth: the one that required pairing and never actually worked. It’s a new standard developed by Nokia™, now implemented in all modern smartphones like Apple iPhone™ or Samsung™ Galaxy S. Other devices, ranging from Fitbit fitness trackers to the Apple Watch, use Bluetooth Smart too.

Bluetooth SIG maintains a list of Bluetooth Smart devices.

## Signal range and measuring proximity

You can think about the beacon as a small lighthouse. But instead of light, it uses radio waves, and instead of ships, it alerts smartphones of its presence. Estimote Beacons have a range of up to 70 meters (230 feet). The signal, however, can be diffracted, interfered with, or absorbed by water (including the human body). That’s why in real world conditions you should expect range of about 40–50 meters.



Phones or other smart devices can pick up the beacon’s signal and estimate the distance by measuring received signal strength (RSSI). The closer you are to the beacon, the stronger the signal. Remember that the beacon is not broadcasting continuously—it’s blinking instead. The more frequent the blinks, the more reliable the signal detection.

And because Bluetooth Smart doesn’t require pairing, a phone can listen to many beacons at the same time. This unlocks more opportunities: for example indoor location.

To understand how all this impacts your beacon-enabled app, read our primer on physics behind beacons.

## What is iBeacon, nearables, Indoor Location SDK?

Beacon is only a piece of hardware broadcasting radio signal. On top of that, there are different APIs, SDKs, and protocols that you’ll be using to bring microlocation to your apps. No worries though, we’ll explain all of that!

 - [What is iBeacon?](http://developer.estimote.com/documentation/ibeacon/overview.html)
 - [What is an Estimote Sticker? What are nearables?](http://developer.estimote.com/documentation/nearables/overview.html)
 - [What is Estimote Indoor Location SDK?](http://developer.estimote.com/documentation/indoor/overview.html)



# Estimote SDK for Android

The Estimote SDK for Android is a library that allows interaction with Estimote beacons & stickers. The SDK system works on Android 4.3 or above and requires device with Bluetooth Low Energy (SDK's min Android SDK version is 9).

It allows for:

 - Beacon Ranging (scans beacons and optionally filters them by their properties)
 - Beacon Monitoring (monitors regions for those devices that have entered/exited a region)
 - Nearables (aka stickers) discovery
 - Eddystone scanning
 - Beacon characteristic reading and writing (proximity UUID, major & minor values, broadcasting power, advertising interval)ing beacons.
