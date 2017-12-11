using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Threading.Tasks;

namespace XamDialogs
{
	public class XamDatePickerDialog : XamDialogView
	{

		#region Fields

		private UIDatePicker mDatePicker;
		private DatetimeFormat mDateTimeFormat = DatetimeFormat.FULL;

		private DateTime mMinDatetime;
		private DateTime mMaxDatetime;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected override UIView ContentView {
			get 
			{
				return mDatePicker;
			}
		}

		/// <summary>
		/// Gets or sets the DatetimeFormat.
		/// </summary>
		/// <value>The content view.</value>
		public DatetimeFormat DatetimeFormat
		{
			get
			{
				return mDateTimeFormat;
			}
			set
			{
				mDateTimeFormat = value;
				if (mDateTimeFormat == DatetimeFormat.FULL)
				{
					mDatePicker.Locale = NSLocale.FromLocaleIdentifier("en_GB");
				}
				else
				{
					mDatePicker.Locale = NSLocale.FromLocaleIdentifier("en_US");
				}
			}
		}

		/// <summary>
		/// Gets or sets the Min Datetime.
		/// </summary>
		/// <value>The min date.</value>
		public DateTime MinDatetime
		{
			get
			{
				return mMinDatetime;
			}
			set
			{
				mMinDatetime = value;
				mDatePicker.MinimumDate = (NSDate)DateTime.SpecifyKind(mMinDatetime, DateTimeKind.Local);
			}
		}

		/// <summary>
		/// Gets or sets the Max Datetime.
		/// </summary>
		/// <value>The max date.</value>
		public DateTime MaxDatetime
		{
			get
			{
				return mMaxDatetime;
			}
			set
			{
				mMaxDatetime = value;
				mDatePicker.MaximumDate = (NSDate)DateTime.SpecifyKind(mMaxDatetime, DateTimeKind.Local);
			}
		}
			
		/// <summary>
		/// Gets or sets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime SelectedDate {
			get 
			{
				return DateTime.SpecifyKind ((DateTime)mDatePicker.Date, DateTimeKind.Utc).ToLocalTime();
			}
			set 
			{
				mDatePicker.Date = (NSDate)DateTime.SpecifyKind(value, DateTimeKind.Local);
			}
		}

		public DateTime MaximumDate
		{
			get
			{
				return DateTime.SpecifyKind((DateTime)mDatePicker.MaximumDate, DateTimeKind.Utc).ToLocalTime();
			}

			set
			{
				mDatePicker.MaximumDate = (NSDate)DateTime.SpecifyKind(value, DateTimeKind.Local);
			}
		}

		public DateTime MinimumDate
		{
			get
			{
				return DateTime.SpecifyKind((DateTime)mDatePicker.MinimumDate, DateTimeKind.Utc).ToLocalTime();
			}

			set
			{
				mDatePicker.MinimumDate = (NSDate)DateTime.SpecifyKind(value, DateTimeKind.Local);
			}
		}
			
		/// <summary>
		/// Called when the selected data has changed
		/// </summary>
		public event EventHandler<DateTime> OnSelectedDateChanged = delegate {		};

		/// <summary>
		/// Gets or sets the validation function to call when submitting
		/// </summary>
		/// <value>The validate submit.</value>
		public Func<DateTime,bool> ValidateSubmit { get; set; }

		#endregion

		#region Constructors

		public XamDatePickerDialog(UIDatePickerMode mode)
			: base(XamDialogType.DatePicker)
		{
			mDatePicker = new UIDatePicker(CGRect.Empty);
			mDatePicker.Mode = mode;
			mDatePicker.TimeZone = NSTimeZone.LocalTimeZone;
			mDatePicker.Calendar = NSCalendar.CurrentCalendar;
			mDatePicker.ValueChanged += OnValueChanged;
		}

		/// <summary>
		/// Raises the value changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnValueChanged (object sender, EventArgs e)
		{
			if (ConstantUpdates == true)
				OnSelectedDateChanged (this, SelectedDate);
		}


		#endregion

		#region implemented abstract members of DHDialogView

		protected override void HandleCancel ()
		{
			
		}

		protected override bool CanSubmit ()
		{
			if (ValidateSubmit != null)
				return ValidateSubmit (SelectedDate);
			
			return true;
		}

		protected override void HandleSubmit ()
		{
			OnSelectedDateChanged (this, SelectedDate);
		}

		#endregion

		#region static Methods


		/// <summary>
		/// Shows the dialog on a specific View Controller
		/// </summary>
		/// <returns>The dialog async.</returns>
		/// <param name="vc">The UIViewController</param>
		/// <param name="mode">Mode.</param>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="selectedDate">Selected date.</param>
		/// <param name="effectStyle">Effect style.</param>
		public static Task<DateTime?> ShowDialogAsync(UIViewController vc, UIDatePickerMode mode, String title, String message, DateTime? selectedDate = null, UIBlurEffectStyle effectStyle = UIBlurEffectStyle.ExtraLight)
		{
			var tcs = new TaskCompletionSource<DateTime?> ();


			new NSObject ().BeginInvokeOnMainThread (() => {

				var dialog = new XamDatePickerDialog(mode)
				{
					Title = title,
					Message = message,
					BlurEffectStyle = effectStyle,
					ConstantUpdates = false,
				};

				if (selectedDate.HasValue)
					dialog.SelectedDate = selectedDate.Value;

				dialog.OnCancel += (object sender, EventArgs e) => 
				{
					tcs.SetResult(null);
				};

				dialog.OnSelectedDateChanged += (object s, DateTime e) => 
				{
					tcs.SetResult(dialog.SelectedDate);
				};

				dialog.Show(vc);

			});

			return tcs.Task;
		}


		#endregion
	}
}

