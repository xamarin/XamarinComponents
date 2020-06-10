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
		Restricted = 14,
		BadFormat = 15,
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

		[Export ("rollingPeriod")]
		uint RollingPeriod { get; set; }

		[Export ("rollingStartNumber")]
		uint RollingStartNumber { get; set; }

		[Export ("transmissionRiskLevel")]
		byte TransmissionRiskLevel { get; set; }
	}

	delegate void ENErrorHandler ([NullAllowed] NSError error);

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface ENExposureDetectionSummary {

		[BindAs (typeof (int []))]
		[Export ("attenuationDurations", ArgumentSemantic.Copy)]
		NSNumber [] AttenuationDurations { get; }

		[Export ("daysSinceLastExposure")]
		nint DaysSinceLastExposure { get; }

		[Export ("matchedKeyCount")]
		ulong MatchedKeyCount { get; }

		[Export ("maximumRiskScore")]
		byte MaximumRiskScore { get; }

		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	interface ENExposureConfiguration {

		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; set; }

		[Export ("minimumRiskScore")]
		byte MinimumRiskScore { get; set; }

		[BindAs (typeof (int []))]
		[Export ("attenuationLevelValues", ArgumentSemantic.Copy)]
		NSNumber [] AttenuationLevelValues { get; set; }

		[Export ("attenuationWeight")]
		double AttenuationWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("daysSinceLastExposureLevelValues", ArgumentSemantic.Copy)]
		NSNumber [] DaysSinceLastExposureLevelValues { get; set; }

		[Export ("daysSinceLastExposureWeight")]
		double DaysSinceLastExposureWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("durationLevelValues", ArgumentSemantic.Copy)]
		NSNumber [] DurationLevelValues { get; set; }

		[Export ("durationWeight")]
		double DurationWeight { get; set; }

		[BindAs (typeof (int []))]
		[Export ("transmissionRiskLevelValues", ArgumentSemantic.Copy)]
		NSNumber [] TransmissionRiskLevelValues { get; set; }

		[Export ("transmissionRiskWeight")]
		double TransmissionRiskWeight { get; set; }
	}

	[Introduced (PlatformName.iOS, 13, 5)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface ENExposureInfo {

		[BindAs (typeof (int []))]
		[Export ("attenuationDurations", ArgumentSemantic.Copy)]
		NSNumber [] AttenuationDurations { get; }

		[Export ("attenuationValue")]
		byte AttenuationValue { get; }

		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		[Export ("duration")]
		double Duration { get; }

		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		[Export ("totalRiskScore")]
		byte TotalRiskScore { get; }

		[Export ("transmissionRiskLevel")]
		byte TransmissionRiskLevel { get; }
	}

	delegate void ENGetDiagnosisKeysHandler ([NullAllowed] ENTemporaryExposureKey [] keys, [NullAllowed] NSError error);
	delegate void ENDetectExposuresHandler ([NullAllowed] ENExposureDetectionSummary summary, [NullAllowed] NSError error);
	delegate void ENGetExposureInfoHandler ([NullAllowed] ENExposureInfo [] exposures, [NullAllowed] NSError error);

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
		[Export ("detectExposuresWithConfiguration:diagnosisKeyURLs:completionHandler:")]
		NSProgress DetectExposures (ENExposureConfiguration configuration, NSUrl [] diagnosisKeyUrls, ENDetectExposuresHandler completionHandler);

		[Async]
		[Export ("getExposureInfoFromSummary:userExplanation:completionHandler:")]
		NSProgress GetExposureInfo (ENExposureDetectionSummary summary, string userExplanation, ENGetExposureInfoHandler completionHandler);

		[Async]
		[Export ("getDiagnosisKeysWithCompletionHandler:")]
		void GetDiagnosisKeys (ENGetDiagnosisKeysHandler completionHandler);

		[Async]
		[Export ("getTestDiagnosisKeysWithCompletionHandler:")]
		void GetTestDiagnosisKeys (ENGetDiagnosisKeysHandler completionHandler);
	}
}
