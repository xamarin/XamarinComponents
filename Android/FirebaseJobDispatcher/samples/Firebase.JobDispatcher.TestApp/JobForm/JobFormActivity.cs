using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace FJDTestApp.JobForm
{
    [Activity(Label = "JobFormActivity")]
    public class JobFormActivity : Activity, IJobParametersView
    {
        static readonly string UseLinearBackoffStrategyKey = "use_linear_backoff_strategy";
        RadioButton exponentialBackoffRadioButton;
        RadioButton linearBackoffRadioButton;
        Button scheduleButton;

        public bool ConstrainOnAnyNetwork => FindViewById<CheckBox>(Resource.Id.constrain_to_network_checkbox).Checked;

        public bool ConstrainOnUnmeteredNetwork => FindViewById<CheckBox>(Resource.Id
                .constrain_to_unmetered_network_checkbox)
            .Checked;

        public bool ConstrainDeviceCharging => FindViewById<CheckBox>(Resource.Id.only_when_charging_checkbox).Checked;
        public bool Recurring => FindViewById<CheckBox>(Resource.Id.recurring_checkbox).Checked;
        public bool Persistent => FindViewById<CheckBox>(Resource.Id.persistent_checkbox).Checked;
        public bool ReplaceCurrent => FindViewById<CheckBox>(Resource.Id.replace_current_checkbox).Checked;
        public bool UseLinearBackoffStrategy { get; set; }

        public string JobTag => FindViewById<EditText>(Resource.Id.tag_edittext).Text;

        public int WindowStartSeconds => Convert.ToInt32(
            FindViewById<EditText>(Resource.Id.window_start_seconds_edittext).Text);

        public int WindowEndSeconds => Convert.ToInt32(FindViewById<EditText>(Resource.Id.window_end_seconds_edittext)
            .Text);

        public int InitialBackoffSeconds => Convert.ToInt32(
            FindViewById<EditText>(Resource.Id.initial_backoff_seconds_edittext).Text);

        public int MaximumBackoffSeconds => Convert.ToInt32(
            FindViewById<EditText>(Resource.Id.maximum_backoff_seconds_edittext).Text);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                UseLinearBackoffStrategy = savedInstanceState.GetBoolean(UseLinearBackoffStrategyKey, true);
            }
            else
            {
                UseLinearBackoffStrategy = true;
            }
            SetContentView(Resource.Layout.activity_job_form);

            scheduleButton = FindViewById<Button>(Resource.Id.schedule_button);
            exponentialBackoffRadioButton = FindViewById<RadioButton>(Resource.Id.exponential_backoff_radiobutton);
            linearBackoffRadioButton = FindViewById<RadioButton>(Resource.Id.linear_backoff_radiobutton);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (UseLinearBackoffStrategy)
            {
                linearBackoffRadioButton.Checked = true;
            }
            else
            {
                exponentialBackoffRadioButton.Checked = true;
            }
            scheduleButton.Click += ScheduleButton_Click;
            exponentialBackoffRadioButton.Click += BackoffStrategyRadioButton_OnClick;
            linearBackoffRadioButton.Click += BackoffStrategyRadioButton_OnClick;
        }


        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean(UseLinearBackoffStrategyKey, UseLinearBackoffStrategy);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnPause()
        {
            exponentialBackoffRadioButton.Click -= BackoffStrategyRadioButton_OnClick;
            linearBackoffRadioButton.Click -= BackoffStrategyRadioButton_OnClick;
            scheduleButton.Click -= ScheduleButton_Click;

            base.OnPause();
        }

        void ScheduleButton_Click(object sender, EventArgs e)
        {
            JobFormScheduler.WithParametersFrom(this)
                .SubmitJob<FibonacciCalculatorJob>(this);
            Finish();
        }

        void BackoffStrategyRadioButton_OnClick(object sender, EventArgs e)
        {
            UseLinearBackoffStrategy = linearBackoffRadioButton.Checked;
        }
    }
}