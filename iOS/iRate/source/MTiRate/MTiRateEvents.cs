using System;

#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace MTiRate 
{
	public partial class iRate
	{
		public event EventHandler<iRateDelegateErrorEventArgs> CouldNotConnectToAppStore {
			add { EnsureiRateDelegate ().couldNotConnectToAppStore += value; }
			remove { EnsureiRateDelegate ().couldNotConnectToAppStore -= value; }
		}

		public event EventHandler DidDetectAppUpdate {
			add { EnsureiRateDelegate ().didDetectAppUpdate += value; }
			remove { EnsureiRateDelegate ().didDetectAppUpdate -= value; }
		}

		public event EventHandler DidOpenAppStore {
			add { EnsureiRateDelegate ().didOpenAppStore += value; }
			remove { EnsureiRateDelegate ().didOpenAppStore -= value; }
		}

		public event EventHandler DidPromptForRating {
			add { EnsureiRateDelegate ().didPromptForRating += value; }
			remove { EnsureiRateDelegate ().didPromptForRating -= value; }
		}

		public Func<object, EventArgs, bool> ShouldOpenAppStore {
			get { return EnsureiRateDelegate ().shouldOpenAppStore; }
			set { EnsureiRateDelegate ().shouldOpenAppStore = value; }
		}

		public Func<object, EventArgs, bool> ShouldPromptForRating {
			get { return EnsureiRateDelegate ().shouldPromptForRating; }
			set { EnsureiRateDelegate ().shouldPromptForRating = value; }
		}

		public event EventHandler UserDidAttemptToRateApp {
			add { EnsureiRateDelegate ().userDidAttemptToRateApp += value; }
			remove { EnsureiRateDelegate ().userDidAttemptToRateApp -= value; }
		}

		public event EventHandler UserDidDeclineToRateApp {
			add { EnsureiRateDelegate ().userDidDeclineToRateApp += value; }
			remove { EnsureiRateDelegate ().userDidDeclineToRateApp -= value; }
		}

		public event EventHandler UserDidRequestReminderToRateApp {
			add { EnsureiRateDelegate ().userDidRequestReminderToRateApp += value; }
			remove { EnsureiRateDelegate ().userDidRequestReminderToRateApp -= value; }
		}

		_iRateDelegate EnsureiRateDelegate ()
		{
			var del = Delegate;
			if (del == null || (!(del is _iRateDelegate))){
				del = new _iRateDelegate ();
				Delegate = del;
			}
			return (_iRateDelegate) del;
		}

		#pragma warning disable 672
		[Register]
		sealed class _iRateDelegate : MTiRate.iRateDelegate { 
			public _iRateDelegate () { IsDirectBinding = false; }

			internal EventHandler<iRateDelegateErrorEventArgs> couldNotConnectToAppStore;
			[Preserve (Conditional = true)]
			public override void CouldNotConnectToAppStore (NSError error)
			{
				EventHandler<iRateDelegateErrorEventArgs> handler = couldNotConnectToAppStore;
				if (handler != null){
					var args = new iRateDelegateErrorEventArgs (error);
					handler (MTiRate.iRate.SharedInstance, args);
				}
			}

			internal EventHandler didDetectAppUpdate;
			[Preserve (Conditional = true)]
			public override void DidDetectAppUpdate ()
			{
				EventHandler handler = didDetectAppUpdate;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

			internal EventHandler didOpenAppStore;
			[Preserve (Conditional = true)]
			public override void DidOpenAppStore ()
			{
				EventHandler handler = didOpenAppStore;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

			internal EventHandler didPromptForRating;
			[Preserve (Conditional = true)]
			public override void DidPromptForRating ()
			{
				EventHandler handler = didPromptForRating;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

			internal Func<object, EventArgs, bool> shouldOpenAppStore;
			[Preserve (Conditional = true)]
			public override bool ShouldOpenAppStore {
				get {
					Func<object, EventArgs, bool> handler = shouldOpenAppStore;
					if (handler != null)
						return handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
					return true;
				}
			}

			internal Func<object, EventArgs, bool> shouldPromptForRating;
			[Preserve (Conditional = true)]
			public override bool ShouldPromptForRating {
				get {
					Func<object, EventArgs, bool> handler = shouldPromptForRating;
					if (handler != null)
						return handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
					return true;
				}
			}

			internal EventHandler userDidAttemptToRateApp;
			[Preserve (Conditional = true)]
			public override void UserDidAttemptToRateApp ()
			{
				EventHandler handler = userDidAttemptToRateApp;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

			internal EventHandler userDidDeclineToRateApp;
			[Preserve (Conditional = true)]
			public override void UserDidDeclineToRateApp ()
			{
				EventHandler handler = userDidDeclineToRateApp;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

			internal EventHandler userDidRequestReminderToRateApp;
			[Preserve (Conditional = true)]
			public override void UserDidRequestReminderToRateApp ()
			{
				EventHandler handler = userDidRequestReminderToRateApp;
				if (handler != null){
					handler (MTiRate.iRate.SharedInstance, EventArgs.Empty);
				}
			}

		}
		#pragma warning restore 672
	}

	public partial class iRateDelegateErrorEventArgs : EventArgs {
		public iRateDelegateErrorEventArgs (NSError error)
		{
			this.Error = error;
		}
		public NSError Error { get; set; }
	}
}

