# Getting Started with Seismic

> Android device shake detection.

## Usage

To make use of the shake detector, all we need is to start it:

    // get the sensor manager
    var sensorManager = SensorManager.FromContext(this);
	
	// create the detector
    var shakeDetector = new ShakeDetector(() => {
        Toast.MakeText(this, "Don't shake me, bro!", ToastLength.Short).Show();
    });

Once started, the shake detector can also be stopped:

    shakeDetector.Start(sensorManager);

We can control how strong the shake needs to be by adjusting the sensitivity:

    shakeDetector.SetSensitivity(Sensitivity.Light);

There are three sensitivity levels: `Hard`, `Medium` and `Light`.
