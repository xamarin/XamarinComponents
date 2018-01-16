using System;
using UIKit;

namespace DatePickerCellSample
{

	public class SamplePickerViewModel : UIPickerViewModel 
	{
		static string [] names = new string [] {
			"Brian Kernighan",
			"Dennis Ritchie",
			"Ken Thompson",
			"Kirk McKusick",
			"Rob Pike",
			"Dave Presotto",
			"Steve Johnson"
		};


		public SamplePickerViewModel () {
			
		}

		public override nint GetComponentCount (UIPickerView v)
		{
			return 2;
		}

		public override nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return names.Length;
		}

		public override string GetTitle (UIPickerView picker, nint row, nint component)
		{
			if (component == 0)
				return names [row];
			else
				return row.ToString ();
		}

		public override void Selected (UIPickerView picker, nint row, nint component)
		{

		}

		public override nfloat GetComponentWidth (UIPickerView picker, nint component)
		{
			if (component == 0)
				return 240f;
			else
				return 40f;
		}

		public override nfloat GetRowHeight (UIPickerView picker, nint component)
		{
			return 40f;
		}
	}
}

