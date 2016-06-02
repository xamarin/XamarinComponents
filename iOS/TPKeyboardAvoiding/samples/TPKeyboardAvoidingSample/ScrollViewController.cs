using System;
using Foundation;
using UIKit;

namespace TPKeyboardAvoidingSample
{
	partial class ScrollViewController : UIViewController
	{
		private const int RowCount = 40;
		private const int GroupCount = 5;

		public ScrollViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Add some text fields in rows
			UIView priorView = null;
			for (int i = 0; i < RowCount; i++) {
				var textField = new UITextField ();
				textField.TranslatesAutoresizingMaskIntoConstraints = false;
				textField.Placeholder = "Field " + i;
				textField.BorderStyle = UITextBorderStyle.RoundedRect;
				scrollView.AddSubview (textField);
		        
				scrollView.AddConstraint (NSLayoutConstraint.Create (textField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 30));
		        
				if (i % GroupCount < 3) {
					var label = new UILabel ();
					label.TranslatesAutoresizingMaskIntoConstraints = false;
					label.Text = "Label";
					scrollView.AddSubview (label);
					View.AddConstraints (new [] { 
						NSLayoutConstraint.Create (label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, textField, NSLayoutAttribute.CenterY, 1, 0),
						NSLayoutConstraint.Create (label, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeadingMargin, 1, 0), 
						NSLayoutConstraint.Create (label, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 80),
						NSLayoutConstraint.Create (textField, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, label, NSLayoutAttribute.Trailing, 1, 10),
						NSLayoutConstraint.Create (textField, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.TrailingMargin, 1, 0)
					});
				} else {
					View.AddConstraints (new [] { 
						NSLayoutConstraint.Create (textField, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeadingMargin, 1, 0), 
						NSLayoutConstraint.Create (textField, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.TrailingMargin, 1, 0)
					});
				}
		        
				if (priorView != null) {
					scrollView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[priorView]-10-[textField]", 0, null, NSDictionary.FromObjectsAndKeys (new [] { priorView, textField }, new [] { (NSString)"priorView", (NSString)"textField" })));
				} else {
					scrollView.AddConstraint (NSLayoutConstraint.Create (textField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.TopMargin, 1, 0));
				}
		        
				priorView = textField;
		        
				if ((i + 1) % GroupCount == 0 && i != RowCount - 1) {
					// Add a horizontal line
					var divider = new UIView ();
					divider.TranslatesAutoresizingMaskIntoConstraints = false;
					divider.BackgroundColor = UIColor.LightGray;
					scrollView.AddSubview (divider);
		            
					scrollView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[textField]-10-[divider(==1)]", 0, null, NSDictionary.FromObjectsAndKeys (new [] {
						textField,
						divider
					}, new [] {
						(NSString)"textField",
						(NSString)"divider"
					})));
					View.AddConstraints (new [] { 
						NSLayoutConstraint.Create (divider, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeadingMargin, 1, 0), 
						NSLayoutConstraint.Create (divider, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.TrailingMargin, 1, 0)
					});
		            
					priorView = divider;
				}
			}
		    
			// Add a button at the bottom, just for funzies
			var button = new UIButton (UIButtonType.RoundedRect);
			button.TranslatesAutoresizingMaskIntoConstraints = false;
			button.SetTitle ("Bing", UIControlState.Normal);
			scrollView.AddSubview (button);
			View.AddConstraints (new [] {
				NSLayoutConstraint.Create (button, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeadingMargin, 1, 0),
				NSLayoutConstraint.Create (button, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.TrailingMargin, 1, 0)
			});
			scrollView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[priorView]-20-[button]-10-|", 0, null, NSDictionary.FromObjectsAndKeys (new [] { priorView, button }, new [] { (NSString)"priorView", (NSString)"button" })));
		}
	}
}
