//
// ExposureNotifications C# bindings
//
// Authors:
//	Alex Soto <alexsoto@microsoft.com>
//
// Copyright (c) Microsoft Corporation.
//

using System;

using ObjCRuntime;
using Foundation;
using CoreFoundation;

namespace ExposureNotifications {

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENErrorCode : long {
		Ok = 0,
		Unknown = 1,
		BadParameter = 2,
		NotEntitled = 3,
		NotAuthorized = 4,
		Unsupported = 5,
		Invalidated = 6,
		BluetoothOff = 7,
		InsufficientStorage = 8,
		NotEnabled = 9,
		ApiMisuse = 10,
		Internal = 11,
		InsufficientMemory = 12,
		RateLimited = 13,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENAuthorizationStatus : long {
		Unknown = 0,
		Restricted = 1,
		NotAuthorized = 2,
		Authorized = 3,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	public enum ENRiskLevel : byte {
		Invalid = 0,
		Lowest = 1,
		Low = 10,
		LowMedium = 25,
		Medium = 50,
		MediumHigh = 65,
		High = 80,
		VeryHigh = 90,
		Highest = 100,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[Native]
	public enum ENStatus : long {
		Unknown = 0,
		Active = 1,
		Disabled = 2,
		BluetoothOff = 3,
		Restricted = 4,
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	interface ENTemporaryExposureKey {

		[Export ("keyData", ArgumentSemantic.Copy)]
		NSData KeyData { get; set; }

		[Export ("rollingStartNumber")]
		uint RollingStartNumber { get; set; }

		[Export ("transmissionRiskLevel", ArgumentSemantic.Assign)]
		ENRiskLevel TransmissionRiskLevel { get; set; }
	}

	delegate void ENErrorHandler ([NullAllowed] NSError error);
	delegate void ENExposureDetectionFinishCompletionHandler ([NullAllowed] ENExposureDetectionSummary summary, [NullAllowed] NSError error);
	delegate void ENGetExposureInfoCompletionHandler ([NullAllowed] ENExposureInfo [] exposures, bool done, [NullAllowed] NSError error);

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	interface ENExposureDetectionSession {

		[Static]
		[Export ("authorizationStatus", ArgumentSemantic.Assign)]
		ENAuthorizationStatus AuthorizationStatus { get; }

		[Export ("configuration", ArgumentSemantic.Strong)]
		ENExposureConfiguration Configuration { get; set; }

		[Export ("dispatchQueue", ArgumentSemantic.Strong)]
		DispatchQueue DispatchQueue { get; set; }

		[NullAllowed, Export("invalidationHandler", ArgumentSemantic.Copy)]
		Action InvalidationHandler { get; set; }

		[Export ("maximumKeyCount")]
		nuint MaximumKeyCount { get; }

		[Async]
		[Export ("activateWithCompletionHandler:")]
		void Activate (ENErrorHandler completionHandler);

		[Export ("invalidate")]
		void Invalidate ();

		[Async]
		[Export ("addDiagnosisKeys:completionHandler:")]
		void AddDiagnosisKeys(ENTemporaryExposureKey [] keys, ENErrorHandler completionHandler);

		[Async]
		[Export ("finishedDiagnosisKeysWithCompletionHandler:")]
		void FinishedDiagnosisKeys (ENExposureDetectionFinishCompletionHandler completionHandler);

		[Async (ResultTypeName = "ENExposureDetectionSessionGetExposureInfoResult")]
		[Export ("getExposureInfoWithMaximumCount:completionHandler:")]
		void GetExposureInfo (nuint maximumCount, ENGetExposureInfoCompletionHandler completionHandler);
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface ENExposureDetectionSummary {

		[Export ("daysSinceLastExposure")]
		nint DaysSinceLastExposure { get; }

		[Export ("matchedKeyCount")]
		ulong MatchedKeyCount { get; }

		[Export ("maximumRiskScore")]
		byte MaximumRiskScore { get; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	interface ENExposureConfiguration {

		[Export ("minimumRiskScore")]
		byte MinimumRiskScore { get; set; }

		[BindAs (typeof (int[]))]
		[Export ("attenuationScores", ArgumentSemantic.Copy)]
		NSNumber [] AttenuationScores { get; set; }

		[Export ("attenuationWeight")]
		double AttenuationWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("daysSinceLastExposureScores", ArgumentSemantic.Copy)]
		NSNumber [] DaysSinceLastExposureScores { get; set; }

		[Export ("daysSinceLastExposureWeight")]
		double DaysSinceLastExposureWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("durationScores", ArgumentSemantic.Copy)]
		NSNumber [] DurationScores { get; set; }

		[Export ("durationWeight")]
		double DurationWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("transmissionRiskScores", ArgumentSemantic.Copy)]
		NSNumber [] TransmissionRiskScores { get; set; }

		[Export ("transmissionRiskWeight")]
		double TransmissionRiskWeight { get; set; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface ENExposureInfo {

		[Export ("attenuationValue")]
		byte AttenuationValue { get; }

		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		[Export ("duration")]
		double Duration { get; }

		[Export ("totalRiskScore")]
		byte TotalRiskScore { get; }

		[Export ("transmissionRiskLevel", ArgumentSemantic.Assign)]
		ENRiskLevel TransmissionRiskLevel { get; }
	}

	delegate void ENGetDiagnosisKeysHandler ([NullAllowed] ENTemporaryExposureKey [] keys, [NullAllowed] NSError error);

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	interface ENManager {

		[Export ("dispatchQueue", ArgumentSemantic.Strong)]
		DispatchQueue DispatchQueue { get; set; }

		[Export ("exposureNotificationStatus", ArgumentSemantic.Assign)]
		ENStatus ExposureNotificationStatus { get; }

		[NullAllowed, Export("invalidationHandler", ArgumentSemantic.Copy)]
		Action InvalidationHandler { get; set; }

		[Async]
		[Export ("activateWithCompletionHandler:")]
		void Activate (ENErrorHandler completionHandler);

		[Export ("invalidate")]
		void Invalidate ();

		[Static]
		[Export ("authorizationStatus", ArgumentSemantic.Assign)]
		ENAuthorizationStatus AuthorizationStatus { get; }

		[Export ("exposureNotificationEnabled")]
		bool ExposureNotificationEnabled { get; }

		[Async]
		[Export ("setExposureNotificationEnabled:completionHandler:")]
		void SetExposureNotificationEnabled (bool enabled, ENErrorHandler completionHandler);

		[Async]
		[Export ("getDiagnosisKeysWithCompletionHandler:")]
		void GetDiagnosisKeys (ENGetDiagnosisKeysHandler completionHandler);

		[Async]
		[Export ("resetAllDataWithCompletionHandler:")]
		void ResetAllData (ENErrorHandler completionHandler);
	}
}
