using System;
using UIKit;
using Foundation;

namespace REFrostedViewController {
    
    
    public interface IREFrostedViewControllerDelegate {
        
		void WillAnimateRotationToInterfaceOrientation(REFrostedViewController frostedViewController, UIInterfaceOrientation toInterfaceOrientation, double duration);
        
		void DidRecognizePanGesture(REFrostedViewController frostedViewController, UIPanGestureRecognizer recognizer);

		void WillShowMenuViewController(REFrostedViewController frostedViewController, UIViewController menuViewController);
        
		void DidShowMenuViewController(REFrostedViewController frostedViewController, UIViewController menuViewController);
        
		void WillHideMenuViewController(REFrostedViewController frostedViewController, UIViewController menuViewController);
        
		void DidHideMenuViewController(REFrostedViewController frostedViewController, UIViewController menuViewController);
    }
}
