
**SCCatWaitingHUD** is a cute and simple loading HUD.

## Usage

We can make use of the shared instance of the HUD:

    CatWaitingHUD.SharedInstance.Start();

Or, we can create and customize a specific instance

    var loader = new CatWaitingHUD();
    loader.Start();

We can check to see if the HUD is visible and/or hide it:
    
    if (loader.IsAnimating) {
        loader.Stop();
    }

