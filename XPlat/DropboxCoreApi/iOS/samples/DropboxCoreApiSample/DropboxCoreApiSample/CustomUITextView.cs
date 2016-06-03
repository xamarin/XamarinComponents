using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace DropboxCoreApiSample
{
	public class CustomUITextView : UITextView
	{
		UILabel placeholder;

		// Set the Placeholder Text
		public string Placeholder {
			get { return placeholder.Text; }
			set { placeholder.Text = value; }
		}

		// Sync the Font between the TextView and the Placeholder Label
		public override UIFont Font {
			get { return base.Font; }
			set {
				base.Font = value;
				placeholder.Font = value;
			}
		}

		// If you change the TextView text, verify if you
		// need to show or hide the placeholder text
		public override string Text {
			get { return base.Text; }
			set {
				base.Text = value;
				placeholder.Hidden = value.Length != 0;
			}
		}

		public CustomUITextView () : this ("")
		{
		}

		public CustomUITextView (string placeholder) : base ()
		{
			InitializeComponents (placeholder);
		}

		public CustomUITextView (CGRect frame) : this (frame, "")
		{
		}

		public CustomUITextView (CGRect frame, string placeholder) : base (frame)
		{
			InitializeComponents (placeholder);
		}

		public CustomUITextView (CGRect frame, NSTextContainer textContainer) : this (frame, textContainer, "")
		{
		}

		public CustomUITextView (CGRect frame, NSTextContainer textContainer, string placeholder) : base (frame, textContainer)
		{
			InitializeComponents (placeholder);
		}

		public CustomUITextView (NSCoder coder) : base (coder)
		{
		}

		void InitializeComponents (string ph)
		{
			// Everytime you type, check if you need to show or hide the Placeholder
			Changed += (sender, e) => placeholder.Hidden = Text.Length != 0;

			// Create the Placeholder Label
			placeholder = new UILabel (CGRect.Empty);
			placeholder.TranslatesAutoresizingMaskIntoConstraints = false;
			placeholder.Text = ph;
			placeholder.TextColor = UIColor.FromWhiteAlpha ((nfloat).7, (nfloat)1);
			placeholder.BackgroundColor = UIColor.Clear;

			// Sync fonts between the TextView and Label
			Font = placeholder.Font;

			Add (placeholder);

			// Assign constraints to Label
			var views = new NSDictionary ("placeholder", placeholder);

			AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-6-[placeholder]-6-|", 0, null, views));
			AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-[placeholder]", 0, null, views));
		}
	}
}

