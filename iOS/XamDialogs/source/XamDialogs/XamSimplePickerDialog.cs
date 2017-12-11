using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;
using Foundation;
using System.Threading.Tasks;

namespace XamDialogs
{
	/// <summary>
	/// XamSimplePickerDialog
	/// </summary>
	public class XamSimplePickerDialog : XamDialogView
	{

		#region Fields

		private UIPickerView mPicker;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected override UIView ContentView {
			get {
				return mPicker;
			}
		}

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public String SelectedItem 
		{
			get 
			{
				return ((SimplePickerModel)mPicker.Model).SelectedItem;
			}
			set 
			{
				((SimplePickerModel)mPicker.Model).UpdateSelectedItem (value);
			}
		}


		/// <summary>
		/// Called when the selected data has changed
		/// </summary>
		public event EventHandler<String> OnSelectedItemChanged = delegate {};

		/// <summary>
		/// Gets or sets the validation function to call when submitting
		/// </summary>
		/// <value>The validate submit.</value>
		public Func<String,bool> ValidateSubmit { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XamDialogs.DHSimplePickerDialog"/> class.
		/// </summary>
		/// <param name="items">Items.</param>
		public XamSimplePickerDialog (List<String> items) 
			: base(XamDialogType.PickerView)
		{
			mPicker = new UIPickerView (CGRect.Empty);
			mPicker.Model = new SimplePickerModel (this, items);

			//mPicker.BackgroundColor = UIColor.Red;
		}

		#endregion

		#region Methods

		protected override bool CanSubmit ()
		{
			if (ValidateSubmit != null)
				return ValidateSubmit (SelectedItem);
			
			return true;
		}

		protected override void HandleCancel ()
		{
			
		}

		protected override void HandleSubmit ()
		{
			SelectionDidChange(SelectedItem);
		}

		/// <summary>
		/// Called when the selection did change
		/// </summary>
		/// <param name="item">Item.</param>
		internal void SelectionDidChange(string item)
		{
			OnSelectedItemChanged (this, item);
		}

		#endregion

		#region static Methods

		/// <summary>
		/// Shows the dialog on the window
		/// </summary>
		/// <returns>The dialog async.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="items">Items.</param>
		/// <param name="selectedItem">Selected item.</param>
		/// <param name="effectStyle">Effect style.</param>
		public static Task<String> ShowDialogAsync(String title, String message, List<String> items, String selectedItem = null, UIBlurEffectStyle effectStyle = UIBlurEffectStyle.ExtraLight)
		{
			var tcs = new TaskCompletionSource<String> ();


			new NSObject ().BeginInvokeOnMainThread (() => {

				var dialog = new XamSimplePickerDialog(items)
				{
					Title = title,
					Message = message,
					BlurEffectStyle = effectStyle,
					ConstantUpdates = false,
				};

				if (!String.IsNullOrWhiteSpace(selectedItem))
					dialog.SelectedItem = selectedItem;

				dialog.OnCancel += (object sender, EventArgs e) => 
				{
					tcs.SetResult(null);
				};

				dialog.OnSelectedItemChanged += (object s, string e) => 
				{
					tcs.SetResult(dialog.SelectedItem);
				};

				dialog.Show();

			});

			return tcs.Task;
		}
			
		/// <summary>
		/// Shows the dialog on the specified View Controller
		/// </summary>
		/// <returns>The dialog async.</returns>
		/// <param name="vc">The view controlller</param>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="items">Items.</param>
		/// <param name="selectedItem">Selected item.</param>
		/// <param name="effectStyle">Effect style.</param>
		public static Task<String> ShowDialogAsync(UIViewController vc, String title, String message, List<String> items, String selectedItem = null, UIBlurEffectStyle effectStyle = UIBlurEffectStyle.ExtraLight)
		{
			var tcs = new TaskCompletionSource<String> ();


			new NSObject ().BeginInvokeOnMainThread (() => {

				var dialog = new XamSimplePickerDialog(items)
				{
					Title = title,
					Message = message,
					BlurEffectStyle = effectStyle,
					ConstantUpdates = false,
				};

				if (!String.IsNullOrWhiteSpace(selectedItem))
					dialog.SelectedItem = selectedItem;

				dialog.OnCancel += (object sender, EventArgs e) => 
				{
					tcs.SetResult(null);
				};

				dialog.OnSelectedItemChanged += (object s, string e) => 
				{
					tcs.SetResult(dialog.SelectedItem);
				};

				dialog.Show(vc);

			});

			return tcs.Task;
		}


		///// <summary>
		///// Shows the dialog on the specified view controller
		///// </summary>
		///// <returns>The dialog async.</returns>
		///// <param name="view">View.</param>
		///// <param name="title">Title.</param>
		///// <param name="message">Message.</param>
		///// <param name="items">Items.</param>
		///// <param name="selectedItem">Selected item.</param>
		///// <param name="effectStyle">Effect style.</param>
		//public static Task<String> ShowDialogAsync(UIView view, String title, String message, List<String> items, String selectedItem = null, UIBlurEffectStyle effectStyle = UIBlurEffectStyle.ExtraLight)
		//{
		//	var tcs = new TaskCompletionSource<String> ();


		//	new NSObject ().BeginInvokeOnMainThread (() => {

		//		var dialog = new XamSimplePickerDialog(items)
		//		{
		//			Title = title,
		//			Message = message,
		//			BlurEffectStyle = effectStyle,
		//			ConstantUpdates = false,
		//		};

		//		if (!String.IsNullOrWhiteSpace(selectedItem))
		//			dialog.SelectedItem = selectedItem;

		//		dialog.OnCancel += (object sender, EventArgs e) => 
		//		{
		//			tcs.SetResult(null);
		//		};

		//		dialog.OnSelectedItemChanged += (object s, string e) => 
		//		{
		//			tcs.SetResult(dialog.SelectedItem);
		//		};

		//		dialog.Show(view);

		//	});

		//	return tcs.Task;
		//}

		#endregion

		private class SimplePickerModel : UIPickerViewModel {

			private XamSimplePickerDialog pvc;
			private List<String> mItems;

			/// <summary>
			/// Gets or sets the selected item.
			/// </summary>
			/// <value>The selected item.</value>
			internal String SelectedItem {
				get ;
				set;
			}


			public SimplePickerModel (XamSimplePickerDialog pvc, List<String> items) {
				this.pvc = pvc;
				mItems = items;
			}

			public override nint GetComponentCount (UIPickerView v)
			{
				return 1;
			}

			public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
			{
				return mItems.Count;
			}

			public override string GetTitle (UIPickerView picker, nint row, nint component)
			{
				return mItems [(int)row];

			}

			public override Foundation.NSAttributedString GetAttributedTitle (UIPickerView pickerView, nint row, nint component)
			{
				var str = mItems [(int)row];

				var firstAttributes = new UIStringAttributes {
					ForegroundColor = pvc.TitleLabelTextColor,
				};

				return new NSAttributedString(str, firstAttributes);
			}

			public override void Selected (UIPickerView picker, nint row, nint component)
			{
				if (mItems == null || mItems.Count == 0)
					return;
				
				var item = mItems [(int)row];

				if (item != SelectedItem) 
				{
					SelectedItem = item; 

					if (pvc.ConstantUpdates == true)
						pvc.SelectionDidChange (item);
				}

			}
				
			public override nfloat GetRowHeight (UIPickerView picker, nint component)
			{
				return 40f;
			}

			public void UpdateSelectedItem(string value)
			{
				SelectedItem = value;

				//fing the index of the selected value
				var indexOf = mItems.IndexOf(value);

				if (indexOf != -1) 
				{

					((UIPickerView)pvc.ContentView).Select (indexOf, 0, true);
				}


			}

		}
	}
}

