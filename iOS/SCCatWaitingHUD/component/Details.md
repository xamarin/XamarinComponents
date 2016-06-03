
<iframe src="https://appetize.io/embed/zd059vnjvge85zmamubrkqu2jc?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

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

