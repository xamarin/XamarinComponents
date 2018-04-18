using System;
using UIKit;
using Foundation;

namespace PickerCells
{
	/// <summary>
	/// Date picker cell.
	/// </summary>
	public class DatePickerCell : BasePickerCell
	{

		#region Fields

		private UIDatePicker mDatePicker;
		private UIDatePickerMode mMode = UIDatePickerMode.DateAndTime;

		private NSDateFormatterStyle mTimeStyle = NSDateFormatterStyle.Medium;
		private NSDateFormatterStyle mDateStyle = NSDateFormatterStyle.Medium;

		private NSDateFormatter mDateFormatter;
		private DateTime? mSelectedDate;
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the selected object.
		/// </summary>
		/// <value>The selected object.</value>
		public override object SelectedObject
		{
			get { return SelectedDate;}
			set
			{
				if (value == null)
					SelectedDate = null;

				if (value is DateTime)
				{
					SelectedDate = (DateTime)value;
				}

			}
		}

		/// <summary>
		/// Gets or sets the time style.
		/// </summary>
		/// <value>The time style.</value>
		public NSDateFormatterStyle TimeStyle
		{
			get
			{
				return mTimeStyle;
			}
			set {mTimeStyle = value;}
		}
	
		/// <summary>
		/// Gets or sets the date style.
		/// </summary>
		/// <value>The date style.</value>
		public NSDateFormatterStyle DateStyle
		{
			get
			{
				return mDateStyle;
			}
			set {mDateStyle = value;}
		}

		/// <summary>
		/// Gets the date formatter.
		/// </summary>
		/// <value>The date formatter.</value>
		public NSDateFormatter DateFormatter
		{
			get
			{
				if (mDateFormatter == null)
					mDateFormatter = new NSDateFormatter();

				return mDateFormatter;
			}
		}
			
		/// <summary>
		/// Gets the value string.
		/// </summary>
		/// <value>The value string.</value>
		protected String ValueString
		{
			get
			{
				switch (DateMode)
				{
					case UIDatePickerMode.Date:
						{
							DateFormatter.DateStyle = DateStyle;
							DateFormatter.TimeStyle = NSDateFormatterStyle.None;
						}
						break;
					case UIDatePickerMode.Time:
						{
							DateFormatter.DateStyle = NSDateFormatterStyle.None;
							DateFormatter.TimeStyle = TimeStyle;
						}
						break;
					default:
						{
							DateFormatter.DateStyle = DateStyle;
							DateFormatter.TimeStyle = TimeStyle;
						}
						break;

				}

				var dST = DateFormatter.ToString(Date);
				return dST;
			}
		}

		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime? SelectedDate 
		{
			get {return mSelectedDate;}
			set
			{
				mSelectedDate = value;

				if (value != null)
				{
					Date = (NSDate)value;
				}

			}
		}

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		private NSDate Date
		{
			get
			{
				return ((UIDatePicker)PickerView).Date;
			}
			set
			{
          
				((UIDatePicker)PickerView).Date = value;
				RightLabel.Text = ValueString;
			}
		}

		/// <summary>
		/// Gets or sets the date mode.
		/// </summary>
		/// <value>The date mode.</value>
		public UIDatePickerMode DateMode {
		    get { return mMode; }
		    set 
			{
				if (mMode != value)
				{
					mMode = value; 

					((UIDatePicker)PickerView).Mode = value;
				}

			}
		}

		/// <summary>
		/// Gets the picker view.
		/// </summary>
		/// <value>The picker view.</value>
		protected override UIView PickerView
		{
			get
			{
				if (mDatePicker == null)
				{
					mDatePicker = new UIDatePicker();
					mDatePicker.Mode = DateMode;
				}

				return mDatePicker; 
			}
		}
			
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.DatePickerCell"/> class.
		/// </summary>
		public DatePickerCell(UIDatePickerMode mode, DateTime? defaultDate = null) 
			: base()
		{
			mMode = mode;

			SelectedDate = defaultDate;

			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		public DatePickerCell(UITableViewCellStyle style) 
			: base(style)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public DatePickerCell(UITableViewCellStyle style, string resuseidentifier) 
			: base(style, resuseidentifier)
		{
			SecondarySetup();
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="resuseidentifier">Resuseidentifier.</param>
		public DatePickerCell(string resuseidentifier) 
			: base(resuseidentifier)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DHPickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="aCoder">A coder.</param>
		public DatePickerCell(NSCoder aCoder) 
			: base(aCoder)
		{
			SecondarySetup();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PickerCells.DatePickerCell"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public DatePickerCell(IntPtr handle) 
			: base(handle)
		{
			SecondarySetup();
		}
		#endregion

		#region Methods

		/// <summary>
		/// Setup to be called after the base class has initialised
		/// </summary>
		protected override void SecondarySetup()
		{
			((UIDatePicker)PickerView).Mode = mMode;

			if (SelectedDate.HasValue)
				this.Date = (NSDate)SelectedDate.Value;
		}

		/// <summary>
		/// s
		/// </summary>
		protected override void CellSetup()
		{
			((UIDatePicker)PickerView).ValueChanged += (object sender, EventArgs e) => 
			{
				SelectedDate = (DateTime)((UIDatePicker)PickerView).Date;

				DidSelectItem(SelectedDate);

			};

		}
			
		#endregion

	}
}

