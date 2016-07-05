using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Estimote;

namespace NearableExample
{
	partial class TriggerOrientationViewController : UIViewController, IUIPickerViewDataSource, IUIPickerViewDelegate
	{
		public TriggerOrientationViewController (IntPtr handle) : base (handle)
		{
		}

		private string TriggerId = "TriggerId";
		private TriggerManager triggerManager;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Trigger Orientation Demo";

			SwitchEnable.ValueChanged += (sender, e) => {
				if (SwitchEnable.On)
					StartTrigger ();
				else
					StopTrigger ();
			};

			NearablePicker.WeakDelegate = this;
			NearablePicker.DataSource = this;
		
		}

		private void StartTrigger ()
		{
			var row = (int)NearablePicker.SelectedRowInComponent (0);

			var rule1 = OrientationRule.OrientationEquals (NearableOrientation.Horizontal, GetTypeForRow (row));

			var forgotBagTrigger = new Trigger (new Rule[]{ rule1 }, TriggerId);

			if (triggerManager == null) {
				triggerManager = new TriggerManager ();
			}

			triggerManager.StartMonitoringForTrigger (forgotBagTrigger);
            triggerManager.ChangedState += HandleTriggerChangedState;

		}

		private NearableType GetTypeForRow (int row)
		{
			switch ((int)row) {
			case 0:
				return NearableType.Bag;
			case 1:
				return NearableType.Bed;
			case 2:
				return NearableType.Bike;
			case 3:
				return NearableType.Car;
			case 4:
				return NearableType.Chair;
			case 5:
				return NearableType.Dog;
			case 6:
				return NearableType.Door;
			case 7:
				return NearableType.Fridge;
			case 8:
				return NearableType.Generic;
			case 9:
				return NearableType.Shoe;
			default:
				return NearableType.Unknown;
			}
		}





        void HandleTriggerChangedState (object sender, TriggerChangedStateEventArgs e)
        {
            LabelTime.Text = DateTime.Now.ToString ("G");
        }

		private void StopTrigger ()
		{
			if (triggerManager == null)
				return;

			triggerManager.StopMonitoringForTriggerWithIdentifier (TriggerId);

		}

		public nint GetComponentCount (UIPickerView pickerView)
		{
			return 1;
		}

		public nint GetRowsInComponent (UIPickerView pickerView, nint component)
		{
			return 11;
		}

		[Export ("pickerView:titleForRow:forComponent:")]
		public string GetTitle (UIKit.UIPickerView pickerView, System.nint row, System.nint component)
		{
			switch ((int)row) {
			case 0:
				return NearableType.Bag.ToString ();
			case 1:
				return NearableType.Bed.ToString ();
			case 2:
				return NearableType.Bike.ToString ();
			case 3:
				return NearableType.Car.ToString ();
			case 4:
				return NearableType.Chair.ToString ();
			case 5:
				return NearableType.Dog.ToString ();
			case 6:
				return NearableType.Door.ToString ();
			case 7:
				return NearableType.Fridge.ToString ();
			case 8:
				return NearableType.Generic.ToString ();
			case 9:
				return NearableType.Shoe.ToString ();
			default:
				return NearableType.Unknown.ToString ();
			}
		}
	}
}
