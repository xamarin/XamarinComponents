using System;
using System.Drawing;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using nuint = global::System.UInt32;
#endif

namespace MTiRate
{
	[Static]
	partial interface Constants
	{
#if __UNIFIED__
		// extern const NSUInteger iRateAppStoreGameGenreID;
		[Field ("iRateAppStoreGameGenreID", "__Internal")]
		nuint iRateAppStoreGameGenreID { get; }
#endif

		// extern NSString *const iRateErrorDomain;
		[Field ("iRateErrorDomain", "__Internal")]
		NSString iRateErrorDomain { get; }

		// extern NSString *const iRateMessageTitleKey;
		[Field ("iRateMessageTitleKey", "__Internal")]
		NSString iRateMessageTitleKey { get; }

		// extern NSString *const iRateAppMessageKey;
		[Field ("iRateAppMessageKey", "__Internal")]
		NSString iRateAppMessageKey { get; }

		// extern NSString *const iRateGameMessageKey;
		[Field ("iRateGameMessageKey", "__Internal")]
		NSString iRateGameMessageKey { get; }

		// extern NSString *const iRateUpdateMessageKey;
		[Field ("iRateUpdateMessageKey", "__Internal")]
		NSString iRateUpdateMessageKey { get; }

		// extern NSString *const iRateCancelButtonKey;
		[Field ("iRateCancelButtonKey", "__Internal")]
		NSString iRateCancelButtonKey { get; }

		// extern NSString *const iRateRemindButtonKey;
		[Field ("iRateRemindButtonKey", "__Internal")]
		NSString iRateRemindButtonKey { get; }

		// extern NSString *const iRateRateButtonKey;
		[Field ("iRateRateButtonKey", "__Internal")]
		NSString iRateRateButtonKey { get; }

		// extern NSString *const iRateCouldNotConnectToAppStore;
		[Field ("iRateCouldNotConnectToAppStore", "__Internal")]
		NSString iRateCouldNotConnectToAppStore { get; }

		// extern NSString *const iRateDidDetectAppUpdate;
		[Field ("iRateDidDetectAppUpdate", "__Internal")]
		NSString iRateDidDetectAppUpdate { get; }

		// extern NSString *const iRateDidPromptForRating;
		[Field ("iRateDidPromptForRating", "__Internal")]
		NSString iRateDidPromptForRating { get; }

		// extern NSString *const iRateUserDidAttemptToRateApp;
		[Field ("iRateUserDidAttemptToRateApp", "__Internal")]
		NSString iRateUserDidAttemptToRateApp { get; }

		// extern NSString *const iRateUserDidDeclineToRateApp;
		[Field ("iRateUserDidDeclineToRateApp", "__Internal")]
		NSString iRateUserDidDeclineToRateApp { get; }

		// extern NSString *const iRateUserDidRequestReminderToRateApp;
		[Field ("iRateUserDidRequestReminderToRateApp", "__Internal")]
		NSString iRateUserDidRequestReminderToRateApp { get; }

		// extern NSString *const iRateDidOpenAppStore;
		[Field ("iRateDidOpenAppStore", "__Internal")]
		NSString iRateDidOpenAppStore { get; }
	}

	interface IiRateDelegate {}

	// @protocol iRateDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface iRateDelegate
	{
		// @optional -(void)iRateCouldNotConnectToAppStore:(NSError *)error;
		[Export ("iRateCouldNotConnectToAppStore:"), EventArgs("iRateDelegateError")]
		void CouldNotConnectToAppStore (NSError error);

		// @optional -(void)iRateDidDetectAppUpdate;
		[Export ("iRateDidDetectAppUpdate"), EventArgs("iRateDelegateArgs")]
		void DidDetectAppUpdate ();

		// @optional -(BOOL)iRateShouldPromptForRating;
		[Export ("iRateShouldPromptForRating"), DelegateName("iRateDelegateShouldPromptForRating"), DefaultValue(true)]
		bool ShouldPromptForRating { get; }

		// @optional -(void)iRateDidPromptForRating;
		[Export ("iRateDidPromptForRating"), EventArgs("iRateDelegateArgs")]
		void DidPromptForRating ();

		// @optional -(void)iRateUserDidAttemptToRateApp;
		[Export ("iRateUserDidAttemptToRateApp"), EventArgs("iRateDelegateArgs")]
		void UserDidAttemptToRateApp ();

		// @optional -(void)iRateUserDidDeclineToRateApp;
		[Export ("iRateUserDidDeclineToRateApp"), EventArgs("iRateDelegateArgs")]
		void UserDidDeclineToRateApp ();

		// @optional -(void)iRateUserDidRequestReminderToRateApp;
		[Export ("iRateUserDidRequestReminderToRateApp"), EventArgs("iRateDelegateArgs")]
		void UserDidRequestReminderToRateApp ();

		// @optional -(BOOL)iRateShouldOpenAppStore;
		[Export ("iRateShouldOpenAppStore"), DelegateName("iRateDelegateShouldOpenAppStore"), DefaultValue(true)]
		bool ShouldOpenAppStore { get; }

		// @optional -(void)iRateDidOpenAppStore;
		[Export ("iRateDidOpenAppStore"), EventArgs("iRateDelegateArgs")]
		void DidOpenAppStore ();
	}

	// @interface iRate : NSObject
	[BaseType (typeof (NSObject)/*,
		Delegates=new string [] {"Delegate"},
		Events=new Type [] { typeof (iRateDelegate) }*/)]
	interface iRate
	{
		// +(instancetype)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		iRate SharedInstance { get; }

		// @property (assign, nonatomic) NSUInteger appStoreID;
		[Export ("appStoreID", ArgumentSemantic.Assign)]
		nuint AppStoreID { get; set; }

		// @property (assign, nonatomic) NSUInteger appStoreGenreID;
		[Export ("appStoreGenreID", ArgumentSemantic.Assign)]
		nuint AppStoreGenreID { get; set; }

		// @property (copy, nonatomic) NSString * appStoreCountry;
		[Export ("appStoreCountry")]
		string AppStoreCountry { get; set; }

		// @property (copy, nonatomic) NSString * applicationName;
		[Export ("applicationName")]
		string ApplicationName { get; set; }

		// @property (copy, nonatomic) NSString * applicationVersion;
		[Export ("applicationVersion")]
		string ApplicationVersion { get; set; }

		// @property (copy, nonatomic) NSString * applicationBundleID;
		[Export ("applicationBundleID")]
		string ApplicationBundleID { get; set; }

		// @property (assign, nonatomic) NSUInteger usesUntilPrompt;
		[Export ("usesUntilPrompt", ArgumentSemantic.Assign)]
		nuint UsesUntilPrompt { get; set; }

		// @property (assign, nonatomic) NSUInteger eventsUntilPrompt;
		[Export ("eventsUntilPrompt", ArgumentSemantic.Assign)]
		nuint EventsUntilPrompt { get; set; }

		// @property (assign, nonatomic) float daysUntilPrompt;
		[Export ("daysUntilPrompt")]
		float DaysUntilPrompt { get; set; }

		// @property (assign, nonatomic) float usesPerWeekForPrompt;
		[Export ("usesPerWeekForPrompt")]
		float UsesPerWeekForPrompt { get; set; }

		// @property (assign, nonatomic) float remindPeriod;
		[Export ("remindPeriod")]
		float RemindPeriod { get; set; }

		// @property (copy, nonatomic) NSString * messageTitle;
		[Export ("messageTitle")]
		string MessageTitle { get; set; }

		// @property (copy, nonatomic) NSString * message;
		[Export ("message")]
		string Message { get; set; }

		// @property (copy, nonatomic) NSString * updateMessage;
		[Export ("updateMessage")]
		string UpdateMessage { get; set; }

		// @property (copy, nonatomic) NSString * cancelButtonLabel;
		[Export ("cancelButtonLabel")]
		string CancelButtonLabel { get; set; }

		// @property (copy, nonatomic) NSString * remindButtonLabel;
		[Export ("remindButtonLabel")]
		string RemindButtonLabel { get; set; }

		// @property (copy, nonatomic) NSString * rateButtonLabel;
		[Export ("rateButtonLabel")]
		string RateButtonLabel { get; set; }

		// @property (assign, nonatomic) BOOL useSKStoreReviewControllerIfAvailable;
		[Export("useSKStoreReviewControllerIfAvailable")]
		bool UseSKStoreReviewControllerIfAvailable { get; set; }

		// @property (assign, nonatomic) BOOL useUIAlertControllerIfAvailable;
		[Export ("useUIAlertControllerIfAvailable")]
		bool UseUIAlertControllerIfAvailable { get; set; }

		// @property (assign, nonatomic) BOOL useAllAvailableLanguages;
		[Export ("useAllAvailableLanguages")]
		bool UseAllAvailableLanguages { get; set; }

		// @property (assign, nonatomic) BOOL promptForNewVersionIfUserRated;
		[Export ("promptForNewVersionIfUserRated")]
		bool PromptForNewVersionIfUserRated { get; set; }

		// @property (assign, nonatomic) BOOL onlyPromptIfLatestVersion;
		[Export ("onlyPromptIfLatestVersion")]
		bool OnlyPromptIfLatestVersion { get; set; }

		// @property (assign, nonatomic) BOOL onlyPromptIfMainWindowIsAvailable;
		[Export ("onlyPromptIfMainWindowIsAvailable")]
		bool OnlyPromptIfMainWindowIsAvailable { get; set; }

		// @property (assign, nonatomic) BOOL promptAtLaunch;
		[Export ("promptAtLaunch")]
		bool PromptAtLaunch { get; set; }

		// @property (assign, nonatomic) BOOL verboseLogging;
		[Export ("verboseLogging")]
		bool VerboseLogging { get; set; }

		// @property (assign, nonatomic) BOOL previewMode;
		[Export ("previewMode")]
		bool PreviewMode { get; set; }

		// @property (nonatomic, strong) NSURL * ratingsURL;
		[Export ("ratingsURL", ArgumentSemantic.Strong)]
		NSUrl RatingsURL { get; set; }

		// @property (nonatomic, strong) NSDate * firstUsed;
		[Export ("firstUsed", ArgumentSemantic.Strong)]
		NSDate FirstUsed { get; set; }

		// @property (nonatomic, strong) NSDate * lastReminded;
		[Export ("lastReminded", ArgumentSemantic.Strong)]
		NSDate LastReminded { get; set; }

		// @property (assign, nonatomic) NSUInteger usesCount;
		[Export ("usesCount", ArgumentSemantic.Assign)]
		nuint UsesCount { get; set; }

		// @property (assign, nonatomic) NSUInteger eventCount;
		[Export ("eventCount", ArgumentSemantic.Assign)]
		nuint EventCount { get; set; }

		// @property (readonly, nonatomic) float usesPerWeek;
		[Export ("usesPerWeek")]
		float UsesPerWeek { get; }

		// @property (assign, nonatomic) BOOL declinedThisVersion;
		[Export ("declinedThisVersion")]
		bool DeclinedThisVersion { get; set; }

		// @property (readonly, nonatomic) BOOL declinedAnyVersion;
		[Export ("declinedAnyVersion")]
		bool DeclinedAnyVersion { get; }

		// @property (assign, nonatomic) BOOL ratedThisVersion;
		[Export ("ratedThisVersion")]
		bool RatedThisVersion { get; set; }

		// @property (readonly, nonatomic) BOOL ratedAnyVersion;
		[Export ("ratedAnyVersion")]
		bool RatedAnyVersion { get; }

		// @property (nonatomic, unsafe_unretained) id<iRateDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		IiRateDelegate Delegate { get; set; }

		// -(BOOL)shouldPromptForRating;
		[Export ("shouldPromptForRating")]
		bool ShouldPromptForRatingM { get; }

		// -(void)promptForRating;
		[Export ("promptForRating")]
		void PromptForRating ();

		// -(void)promptIfNetworkAvailable;
		[Export ("promptIfNetworkAvailable")]
		void PromptIfNetworkAvailable ();

		// -(void)promptIfAllCriteriaMet;
		[Export ("promptIfAllCriteriaMet")]
		void PromptIfAllCriteriaMet ();

		// -(void)openRatingsPageInAppStore;
		[Export ("openRatingsPageInAppStore")]
		void OpenRatingsPageInAppStore ();

		// -(void)logEvent:(BOOL)deferPrompt;
		[Export ("logEvent:")]
		void LogEvent (bool deferPrompt);

		// -(void)remindLater;
		[Export ("remindLater")]
		void RemindLater ();
	}

}

