using System;
using UIKit;
using Foundation;

namespace PickerCells
{
	/// <summary>
	/// Base picker cell.
	/// </summary>
	public abstract class BasePickerCell : UITableViewCell
	{
		private class DVColorLockView : UIView
		{
			/// <summary>
			/// Gets or sets the color of the locked background.
			/// </summary>
			/// <value>The color of the locked background.</value>
			public UIColor LockedBackgroundColor
			{
				get
				{
					return base.BackgroundColor;
				}
				set
				{
					base.BackgroundColor = value;
				}
			}

			/// <summary>
			/// Gets or sets the color of the background.
			/// </summary>
			/// <value>The color of the background.</value>
			public override UIColor BackgroundColor
			{
				get
				{
					return base.BackgroundColor;
				}
				set
				{

				}
			}
		}

		/// <summary>
		/// Occurs when on item changed.
		/// </summary>
		public event EventHandler<PickerCellArgs> OnItemChanged = delegate {};

		#region Fields

		private DVColorLockView mSeperator = new DVColorLockView();

		/// Label on the left side of the cell.
		private UILabel mLeftLabel;
		private UILabel mRightLabel;

		private UIColor mRightLabelTextColor = UIColor.FromHSBA(0.639f, 0.041f, 0.576f,1.0f);

		private UIView pickerContainer = new UIView();

		private bool expanded;

		private nfloat unexpandedHeight = 44.0f;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the selected object.
		/// </summary>
		/// <value>The selected object.</value>
		public abstract object SelectedObject {get;set;}

		/// <summary>
		/// Gets or sets the key for the cell
		/// </summary>
		/// <value>The key.</value>
		public object Key {get; set;}

		/// <summary>
		/// Gets the picker view.
		/// </summary>
		/// <value>The picker view.</value>
		protected abstract UIView PickerView {get;}

		/// <summary>
		/// Gets the height of the date picker.
		/// </summary>
		/// <value>The height of the date picker.</value>
		public nfloat PickerHeight
		{
			get
			{
				var expandedHeight = unexpandedHeight + PickerView.Frame.Size.Height;

				return expanded ? expandedHeight : unexpandedHeight;
			}
		}
			
		/// <summary>
		/// Gets the right label.
		/// </summary>
		/// <value>The right label.</value>
		public UILabel RightLabel {
			get 
			{
				if (mRightLabel == null)
					mRightLabel = new UILabel();

				return mRightLabel; 
			}
		}

		/// <summary>
		/// Gets or sets the color of the right label text.
		/// </summary>
		/// <value>The color of the right label text.</value>
		public UIColor RightLabelTextColor
		{
			get
			{
				if (mRightLabelTextColor == null)
					mRightLabelTextColor = UIColor.FromHSBA(0.639f, 0.041f, 0.576f,1.0f);

				return mRightLabelTextColor;
					
			}
			set {mRightLabelTextColor = value;}
		}
			
		/// <summary>
		/// Gets or sets the right label text alignment.
		/// </summary>
		/// <value>The right label text alignment.</value>
		public UITextAlignment RightLabelTextAlignment
		{
			get {return RightLabel.TextAlignment;}
			set
			{
				RightLabel.TextAlignment = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum width of the label.
		/// </summary>
		/// <value>The minimum width of the label.</value>
		public int LabelFixedWidth {get; set;}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.BasePickerCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		public BasePickerCell(UITableViewCellStyle style) 
			: base(style, String.Empty)
		{
			Setup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.BasePickerCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public BasePickerCell(UITableViewCellStyle style, string resuseidentifier) 
			: base(style,resuseidentifier)
		{
			Setup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.BasePickerCell"/> class.
		/// </summary>
		public BasePickerCell() 
			: base(UITableViewCellStyle.Value1, String.Empty)
		{
			Setup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public BasePickerCell(string resuseidentifier) 
			: base(UITableViewCellStyle.Value1,resuseidentifier)
		{
			Setup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="aCoder">A coder.</param>
		public BasePickerCell(NSCoder aCoder) 
			: base(aCoder)
		{
			Setup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.BasePickerCell"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public BasePickerCell(IntPtr handle) 
			: base(handle)
		{
			Setup();
		}
		#endregion

		#region Methods

		/// <summary>
		/// Setup this instance.
		/// </summary>
		protected void Setup()
		{
			// The datePicker overhangs the view slightly to avoid invalid constraints.
			this.ClipsToBounds = true;

			var aFrame = TextLabel.Frame;
			aFrame.Height = 44.0f;
			TextLabel.Frame = aFrame;
			TextLabel.TranslatesAutoresizingMaskIntoConstraints = false;


			//RightLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			this.Add(RightLabel);

			var views = new UIView[]{mSeperator, pickerContainer, PickerView};

			foreach (var view in views) 
			{
				this.ContentView.AddSubview(view);

				view.TranslatesAutoresizingMaskIntoConstraints = false;
			}

			pickerContainer.ClipsToBounds = true;
			pickerContainer.AddSubview(PickerView);


			// Add a seperator between the date text display, and the datePicker. Lighter grey than a normal seperator.
			mSeperator.LockedBackgroundColor = UIColor.FromWhiteAlpha(0, 0.1f);
			pickerContainer.AddSubview(mSeperator);


			pickerContainer.AddConstraints(new NSLayoutConstraint[]
			{
				NSLayoutConstraint.Create(mSeperator,NSLayoutAttribute.Left, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.Left,1.0f,0.0f),
				NSLayoutConstraint.Create(mSeperator,NSLayoutAttribute.Right, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.Right,1.0f,0.0f),
				NSLayoutConstraint.Create(mSeperator,NSLayoutAttribute.Height, NSLayoutRelation.Equal,null, NSLayoutAttribute.NoAttribute,1.0f,0.5f),
				NSLayoutConstraint.Create(mSeperator,NSLayoutAttribute.Top, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.Top,1.0f,0.0f),

			});

			RightLabel.TextColor = RightLabelTextColor;

			this.ContentView.AddConstraints(new NSLayoutConstraint[]
			{
				NSLayoutConstraint.Create(TextLabel,NSLayoutAttribute.Height, NSLayoutRelation.Equal,null, NSLayoutAttribute.NoAttribute,1.0f,44f),
				NSLayoutConstraint.Create(TextLabel,NSLayoutAttribute.Top, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Top,1.0f,0.0f),
				NSLayoutConstraint.Create(TextLabel,NSLayoutAttribute.Left, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Left,1.0f,this.SeparatorInset.Left),
			});
//
//			this.ContentView.AddConstraints(new NSLayoutConstraint[]
//			{
//				NSLayoutConstraint.Create(RightLabel,NSLayoutAttribute.Height, NSLayoutRelation.Equal,null, NSLayoutAttribute.NoAttribute,1.0f,44f),
//				NSLayoutConstraint.Create(RightLabel,NSLayoutAttribute.Top, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Top,1.0f,0.0f),
//				NSLayoutConstraint.Create(RightLabel,NSLayoutAttribute.Right, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Right,1.0f,-this.SeparatorInset.Left),
//			});

			this.ContentView.AddConstraints(new NSLayoutConstraint[]
			{
				NSLayoutConstraint.Create(pickerContainer,NSLayoutAttribute.Left, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Left,1.0f,0.0f),
				NSLayoutConstraint.Create(pickerContainer,NSLayoutAttribute.Right, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Right,1.0f,0.0f),
				NSLayoutConstraint.Create(pickerContainer,NSLayoutAttribute.Top, NSLayoutRelation.Equal,TextLabel, NSLayoutAttribute.Bottom,1.0f,0.0f),
				NSLayoutConstraint.Create(pickerContainer,NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,this.ContentView, NSLayoutAttribute.Bottom,1.0f,1.0f),

			});


			pickerContainer.AddConstraints(new NSLayoutConstraint[]
			{
				NSLayoutConstraint.Create(PickerView,NSLayoutAttribute.Left, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.Left,1.0f,0.0f),
				NSLayoutConstraint.Create(PickerView,NSLayoutAttribute.Right, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.Right,1.0f,0.0f),
				NSLayoutConstraint.Create(PickerView,NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,pickerContainer, NSLayoutAttribute.CenterY,1.0f,0.0f),

			});

			CellSetup();
				
		}

		/// <summary>
		/// Sets up the cell
		/// </summary>
		protected abstract void CellSetup();

		/// <summary>
		/// Setup to be called after the base class has initialised
		/// </summary>
		protected abstract void SecondarySetup();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableView">Table view.</param>
		public void SelectedInTableView(UITableView tableView)
		{
			expanded = !expanded;

			UIView.Transition(RightLabel, 0.25f, UIViewAnimationOptions.TransitionCrossDissolve,() => 
			{ 
				this.RightLabel.TextColor = this.expanded ? this.TintColor : this.RightLabelTextColor;

			}, completion: null);

			tableView.BeginUpdates();
			tableView.EndUpdates();
		}

		/// <summary>
		/// Did select the items
		/// </summary>
		/// <param name="items">Items.</param>
		/// <param name="key">Key.</param>
		protected void DidSelectItem(object[] items)
		{
			OnItemChanged(this, new PickerCellArgs(items,Key));
		}

		/// <summary>
		/// Did select the item
		/// </summary>
		/// <param name="items">Items.</param>
		/// <param name="key">Key.</param>
		protected void DidSelectItem(object item)
		{
			DidSelectItem(new object[]{item});
		}

		public override void LayoutSubviews()
		{
			var rightPadding = 10;
			var leftPadding = 5;

			TextLabel.SizeToFit();
		

			TextLabel.SizeToFit();
			var aFrame = TextLabel.Frame;
			aFrame.Height = unexpandedHeight;

			if (LabelFixedWidth > 0)
			{
				aFrame.Width = LabelFixedWidth;
			}

			TextLabel.Frame = aFrame;


			base.LayoutSubviews();

			if (RightLabel != null)
			{
				this.TextLabel.SizeToFit();
				var aTFrame = this.TextLabel.Frame;

				if (LabelFixedWidth > 0)
				{
					aTFrame.Width = LabelFixedWidth;
				}

				var eFrame = RightLabel.Frame;


				var edge = aTFrame.X + aTFrame.Width;
				eFrame.Width = (this.ContentView.Frame.Width -  edge) - (leftPadding + rightPadding);

				eFrame.Height = unexpandedHeight;

				eFrame.X = edge + leftPadding;
				eFrame.Y = 0;



				RightLabel.Frame = eFrame;

			}
				
		}
		#endregion



	}

	public class PickerCellArgs : EventArgs
	{
		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>
		public object[] Items {get; private set;}

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public object Key
		{
			get;
			set;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.PickerCellArgs"/> class.
		/// </summary>
		/// <param name="items">Items.</param>
		/// <param name="key">Key.</param>
		public PickerCellArgs(object[] items, object key)
		{
			Items = items;
			Key = key;

		}
	}

}

