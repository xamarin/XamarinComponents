using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Estimote;

namespace NearableExample
{
	partial class TriggersViewController : UIViewController, IUIPickerViewDataSource, IUIPickerViewDelegate
	{
		public TriggersViewController (IntPtr handle) : base (handle)
		{
		}

		private string TriggerId = "ForgotBagTriggerId";
		private TriggerManager triggerManager;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Trigger Demo";

			RemindSwitch.ValueChanged += (sender, e) => {
				if (RemindSwitch.On)
					StartReminderTrigger ();
				else
					StopReminderTrigger ();
			};

			FirstHourSwitch.WeakDelegate = this;
			FirstHourSwitch.DataSource = this;
			SecondHourSwitch.WeakDelegate = this;
			SecondHourSwitch.DataSource = this;


		}

		private void StartReminderTrigger ()
		{
			var firstHour = (int)FirstHourSwitch.SelectedRowInComponent (0);
			var secondHour = (int)SecondHourSwitch.SelectedRowInComponent (0);

			var goingToWork = DateRule.HourBetween (firstHour, secondHour);
			var insideCarRule = ProximityRule.InRangeOfNearableType (NearableType.Car);
			var noBagRule = ProximityRule.OutsideRangeOfNearableType (NearableType.Bag);

			var forgotBagTrigger = new Trigger (new Rule[]{ goingToWork, insideCarRule, noBagRule }, TriggerId); 

			if (triggerManager == null) {
				triggerManager = new TriggerManager ();
			}

			triggerManager.StartMonitoringForTrigger (forgotBagTrigger);
            triggerManager.ChangedState += HandleTriggerChangedState;
		}



        void HandleTriggerChangedState (object sender, TriggerChangedStateEventArgs e)
        {
            if(e.Trigger.Identifier == TriggerId && e.Trigger.State)
            {
                Console.WriteLine("You forgot your bag!");
                var notification = new UILocalNotification();
                notification.AlertBody = "You forgot your bag!";
                UIApplication.SharedApplication.PresentLocalNotificationNow(notification);

            }
        }

		private void StopReminderTrigger ()
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
			return 24;
		}

		[Export ("pickerView:titleForRow:forComponent:")]
		public string GetTitle (UIKit.UIPickerView pickerView, System.nint row, System.nint component)
		{
			return string.Format ("{0}:00", row);
		}
	}
}
