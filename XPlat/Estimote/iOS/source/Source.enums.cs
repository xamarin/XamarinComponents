using ObjCRuntime;
using System;

namespace Estimote
{
	using ObjCRuntime;

	public enum Color
	{
		Unknown,
		MintCocktail,
		IcyMarshmallow,
		BlueberryPie,
		SweetBeetroot,
		CandyFloss,
		LemonTart,
		VanillaJello,
		LiquoriceSwirl,
		White,
		Black,
		CoconutPuff,
		Transparent
	}

	public enum ConnectionStatus
	{
		Disconnected,
		Connecting,
		Connected,
		Updating
	}

	public enum BroadcastingScheme : sbyte
	{
		Unknown,
		Estimote,
		IBeacon,
		EddystoneURL,
		EddystoneUID
	}

	[Native]
	public enum NearableType : long
	{
		Unknown = 0,
		Dog,
		Car,
		Fridge,
		Bag,
		Bike,
		Chair,
		Bed,
		Door,
		Shoe,
		Generic,
		All
	}

	[Native]
	public enum NearableOrientation : long
	{
		Unknown = 0,
		Horizontal,
		HorizontalUpsideDown,
		Vertical,
		VerticalUpsideDown,
		LeftSide,
		RightSide
	}

	[Native]
	public enum NearableZone : long
	{
		Unknown = 0,
		Immediate,
		Near,
		Far
	}

	[Native]
	public enum NearableFirmwareState : long
	{
		Boot = 0,
		App
	}

	public enum BeaconPower : sbyte
	{
		Level1 = -30,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4
	}

	public enum BeaconBatteryType
	{
		Unknown = 0,
		Cr2450,
		Cr2477
	}

	[Native]
	public enum BeaconFirmwareState : long
	{
		Boot,
		App
	}

	[Native]
	public enum BeaconPowerSavingMode : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum BeaconEstimoteSecureUUID : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum BeaconMotionUUID : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum BeaconMotionState : long
	{
		Unknown,
		Unsupported,
		Moving,
		NotMoving
	}

	[Native]
	public enum BeaconTemperatureState : long
	{
		Unknown,
		Unsupported,
		Supported
	}

	[Native]
	public enum BeaconMotionDetection : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum BeaconConditionalBroadcasting : long
	{
		Unknown,
		Unsupported,
		Off,
		MotionOnly,
		FlipToStop
	}

	[Native]
	public enum BeaconCharInfoType : long
	{
		Read,
		Only
	}

	public enum Connection : uint
	{
		InternetConnectionError,
		IdentifierMissingError,
		NotAuthorizedError,
		NotConnectedToReadWrite
	}

	[Native]
	public enum UtilityManagerState : long
	{
		Idle,
		Scanning
	}

	[Native]
	public enum Notification : long
	{
		SaveNearableZoneDescription,
		SaveNearable,
		BeaconEnterRegion,
		BeaconExitRegion,
		NearableEnterRegion,
		NearableExitRegion,
		RangeNearables
	}

	[Native]
	public enum ESBeaconUpdateInfoStatus : long
	{
		Idle,
		ReadyToUpdate,
		Updating,
		UpdateSuccess,
		UpdateFailed
	}

	[Native]
	public enum ESBulkUpdaterStatus : long
	{
		Idle,
		Updating,
		Completed
	}

	[Native]
	public enum BulkUpdaterMode : long
	{
		Foreground,
		Background
	}

	[Native]
	public enum EddystoneProximity : long
	{
		Unknown,
		Immediate,
		Near,
		Far
	}

	[Native]
	public enum EddystoneManagerState : long
	{
		Idle,
		Scanning
	}

	[Native]
	public enum BeaconManagerError : long
	{
		InvalidRegion = 1,
		LocationServicesUnauthorized
	}

	[Native]
	public enum DeviceNearableError : long
	{
		DeviceNotConnected,
		ConnectionOwnershipVerificationFail,
		DisconnectDuringConnection,
		ConnectionVersionReadFailed,
		SettingNotSupported,
		SettingWriteValueMissing,
		ConnectionCloudConfirmationFailed,
		UpdateNotAvailable,
		FailedToDownloadFirmware,
		FailedToConfirmUpdate
	}

	[Native]
	public enum GPIOConfigError : long
	{
		ValueNotAllowed = 1
	}

	public enum GPIOConfig : byte
	{
		InputNoPull = 0,
		InputPullDown = 1,
		InputPullUp = 2,
		Output = 3,
		Uart = 4
	}

	[Native]
	public enum GPIOPort : long
	{
		Port0,
		Port1
	}

	[Native]
	public enum GPIOPortsDataError : long
	{
		Port,
		Value
	}

	[Native]
	public enum GPIOPortValue : long
	{
		Unknown = -1,
		Low = 0,
		High = 1
	}

	[Native]
	public enum AnalyticsEventType : long
	{
		EnterRegion,
		ExitRegion,
		InFar,
		InNear,
		InImmediate,
		InUnknown
	}

	[Native]
	public enum SettingOperationType : long
	{
		Read,
		Write
	}

	[Native]
	public enum SettingStorageType : ulong
	{
		DeviceCloud,
		CloudOnly,
		DeviceOnly
	}

	public enum LogLevel
	{
		None,
		Error,
		Warning,
		Debug,
		Info,
		Verbose
	}

	public enum EddystoneTLMPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	[Native]
	public enum BulkUpdaterDeviceUpdateStatus : long
	{
		Unknown,
		Scanning,
		PendingUpdate,
		Updating,
		Succeeded,
		Failed,
		OutOfRange
	}

	[Native]
	public enum PeripheralFirmwareState : long
	{
		Unknown,
		Boot,
		App
	}

	[Native]
	public enum BeaconPublicDetailsFields : ulong
	{
		AllFields = 1 << 0,
		FieldMac = 1 << 1,
		FieldColor = 1 << 2,
		FieldPublicIdentifier = 1 << 3,
		AllSettings = 1 << 4,
		FieldPower = 1 << 5,
		FieldSecurity = 1 << 6,
		FieldBroadcastingScheme = 1 << 7,
		FieldUUIDMajorMinor = 1 << 8,
		FieldEddystoneNamespaceID = 1 << 9,
		FieldEddystoneInstanceID = 1 << 10
	}

	public enum ConnectablePowerLevel : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum EddystoneUIDPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum EddystoneURLPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum EddystoneEIDPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum EstimoteLocationPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum EstimoteTLMPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum IBeaconPower : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	public enum NearablePower : sbyte
	{
		Level1 = -30,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4
	}

	[Native]
	public enum SettingOperationStatus : long
	{
		InProgress,
		Complete,
		Failed
	}

	[Native]
	public enum RequestBaseError : long
	{
		ConnectionFail = -1,
		NoData = -2,
		BadRequest = 400,
		Unauthorized = 401,
		PaymentRequired = 402,
		Forbidden = 403,
		NotFound = 404,
		InternalServerError = 500
	}

	[Native]
	public enum BeaconDetailsFields : ulong
	{
		AllFields = 1 << 0,
		FieldMac = 1 << 1,
		FieldColor = 1 << 2,
		FieldName = 1 << 3,
		FieldGPSLocation = 1 << 4,
		FieldIndoorLocation = 1 << 5,
		FieldPublicIdentifier = 1 << 6,
		FieldRemainingBatteryLifetime = 1 << 7,
		AllSettings = 1 << 8,
		FieldBattery = 1 << 9,
		FieldPower = 1 << 10,
		FieldInterval = 1 << 11,
		FieldHardware = 1 << 12,
		FieldFirmware = 1 << 13,
		FieldBasicPowerMode = 1 << 14,
		FieldSmartPowerMode = 1 << 15,
		FieldTimeZone = 1 << 16,
		FieldSecurity = 1 << 17,
		FieldMotionDetection = 1 << 18,
		FieldConditionalBroadcasting = 1 << 19,
		FieldBroadcastingScheme = 1 << 20,
		FieldUUIDMajorMinor = 1 << 21,
		FieldEddystoneNamespaceID = 1 << 22,
		FieldEddystoneInstanceID = 1 << 23
	}

	[Native]
	public enum SettingIBeaconProximityUUIDError : ulong
	{
		InvalidValue = 1
	}

    // typedef NS_ENUM(char, ESTNearableBroadcastingScheme)
	public enum NearableBroadcastingScheme : sbyte
	{
		Unknown = -1,
		Nearable,
		IBeacon,
		EddystoneURL
	}

	[Native]
	public enum SettingPowerMotionOnlyBroadcastingDelayError : long
	{
		Small = 1,
		Big = 2
	}

	[Native]
	public enum SettingPowerScheduledAdvertisingPeriodError : long
	{
		StartTimeTooBig = 1,
		EndTimeTooBig
	}

	[Native]
	public enum SettingDeviceInfoApplicationVersionError : long
	{
		NilValue,
		EmptyString
	}

	[Native]
	public enum SettingDeviceInfoBootloaderVersionError : long
	{
		NilValue,
		EmptyString
	}

	[Native]
	public enum SettingDeviceInfoHardwareVersionError : long
	{
		NilValue,
		EmptyString
	}

	[Native]
	public enum SettingIBeaconIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingIBeaconMajorError : long
	{
		SettingIBeaconMajorErrorEqualsZero = 1
	}

	[Native]
	public enum SettingIBeaconMinorError : long
	{
		SettingIBeaconMinorErrorEqualsZero = 1
	}

	[Native]
	public enum SettingIBeaconPowerError : long
	{
		SettingIBeaconPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEstimoteLocationIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneUIDInstanceError : long
	{
		InvalidFormat = 1,
		TooShort,
		TooLong
	}

	[Native]
	public enum SettingEddystoneUIDNamespaceError : long
	{
		InvalidFormat = 1,
		TooShort,
		TooLong
	}

	[Native]
	public enum SettingEddystoneUIDIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneURLNamespaceError : long
	{
		SettingEddystoneURLDataErrorInvalidURL = 1
	}

	[Native]
	public enum SettingEddystoneURLIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneTLMIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneTLMPowerError : long
	{
		SettingEddystoneTLMPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEddystoneEIDIntervalError : long
	{
		Small = 1,
		Big = 2
	}

	[Native]
	public enum GenericAdvertiserID : long
	{
		ID1 = 1,
		ID2 = 2
	}

	[Native]
	public enum SettingGenericAdvertiserEnableError : long
	{
		SettingGenericAdvertiserEnableErrorInvalidAdvertiserID = 1
	}

	public enum GenericAdvertiserPowerLevel : sbyte
	{
		Level1 = -30,
		Level1A = -40,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4,
		Level9 = 10,
		Level9A = 20
	}

	[Native]
	public enum SettingGenericAdvertiserPowerError : long
	{
		ValueNotAllowed = 1,
		InvalidAdvertiserID
	}

	[Native]
	public enum SettingGenericAdvertiserIntervalError : long
	{
		ValueTooSmall = 1,
		ValueTooBig,
		InvalidAdvertiserID
	}

	[Native]
	public enum SettingGenericAdvertiserDataError : long
	{
		CanNotBeNil = 1,
		InvalidAdvertiserID
	}

	[Native]
	public enum PeripheralNearableError : long
	{
		Unknown,
		InvalidOperation,
		TimeoutReached,
		PacketError
	}

	[Native]
	public enum NearableSettingsManagerError : long
	{
		SynchronizationInProgress,
		SettingNotSupported,
		SettingNotProvidedForWrite,
		SettingValidationFailed,
		SettingCloudReadFailed,
		CloudSaveFailed
	}

	[Native]
	public enum SettingNearableIntervalError : long
	{
		ValueTooSmall = 1,
		ValueTooBig,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum SettingNearablePowerError : long
	{
		ValueNotAllowed = 1,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum SettingNearableEddystoneURLError : long
	{
		InvalidURL = 1,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum SettingNearableBroadcastingSchemeError : long
	{
		NotAllowed = 1,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum LocationBeaconBulkUpdaterError : long
	{
		DeviceDiscoveryFailed,
		NoPendingChanges,
		Timeout
	}

	[Native]
	public enum BulkUpdaterStatus : long
	{
		Idle = 0,
		Running
	}

	[Native]
	public enum RequestGetBeaconsError : long
	{
		RequestGetBeaconsErrorUnknown
	}

	[Native]
	public enum RequestBeaconColorError : long
	{
		RequestBeaconColorErrorColorNotAvailable
	}

	[Native]
	public enum RequestBeaconMacError : long
	{
		RequestBeaconMacErrorUnknown
	}

	[Native]
	public enum RequestAssignGPSLocationError : long
	{
		RequestAssignGPSLocationErrorGPSLocationNotAvailable
	}

	[Native]
	public enum RequestGetNearablesError : long
	{
		RequestGetNearablesUnknown
	}

	[Native]
	public enum DeviceSettingsAdvertiserSettingsPower : long
	{
		Power1 = -30,
		Power2 = -20,
		Power3 = -16,
		Power4 = -12,
		Power5 = -8,
		Power6 = -4,
		Power7 = 0,
		Power8 = 4
	}

	[Native]
	public enum MeshNetworkType : long
	{
		Standard,
		Cluster
	}

	[Native]
	public enum MeshError : long
	{
		InvalidArguments,
		AddingDeviceFailed,
		RemovingDeviceFailed
	}

	[Native]
	public enum EStrequestV3GetFirmwresError : long
	{
		EStrequestV3GetFirmwresErrorIvalidValue = 1
	}

	[Native]
	public enum RequestGetDevicesTypeMask : long
	{
		Beacon = 1 << 0,
		Mirror = 1 << 1,
		Sticker = 1 << 2,
		All = 7
	}

	[Native]
	public enum MonitoringProximity : long
	{
		Unknown,
		Near,
		Medium,
		Far
	}

	[Native]
	public enum MonitoringManagerError : long
	{
		BluetoothNotSupported = 1,
		UnauthorizedToUseBluetooth = 2,
		BluetoothOff = 3,
		ConnectionFail = -1,
		NoData = -2,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		InternalServerError = 500
	}

	[Native]
	public enum MonitoringState : long
	{
		Unknown = 0,
		InsideZone,
		OutsideZone
	}

	[Native]
	public enum MonitoringV2ManagerError : long
	{
		BluetoothNotSupported = 1,
		UnauthorizedToUseBluetooth = 2,
		BluetoothOff = 3,
		DesiredDistanceTooLow = 4,
		UnauthorizedToMonitorBeacons = 5
	}

	[Native]
	public enum MeshManagerError : long
	{
		ErrorInvalidValue,
		BluetoothNotSupported,
		UnauthorizedToUseBluetooth,
		BluetoothOff,
		ErrorAutomappingFailed,
		ErrorAssetTrackingFailed,
		ErrorPrepareNearablesScanReportFailed,
		ErrorConfigurationFailed
	}

	[Native]
	public enum PeripheralDiscoveryError : long
	{
		NoServices = 1000,
		ServicesFailure = 1001,
		CharacteristicsFailure = 1002
	}

	[Native]
	public enum PeripheralTypeUtilityError : long
	{
		ReadWriteOperationFailed,
		PacketGenerationFailed
	}

	[Native]
	public enum PeripheralTypeUtilityErrorCode : long
	{
		Unknown,
		InvalidCRC,
		RegisterIDChanged,
		InvalidChunkIndex,
		InvalidLength,
		InvalidValueSize,
		InvalidValue,
		InvalidRegisterID,
		InvalidOperation,
		TooLowAuthLevel,
		OperationBlocked,
		NoDataReturned,
		WaitingForMore
	}

	[Native]
	public enum SettingBaseError : long
	{
		SettingBaseErrorDeviceReferenceNotAvailable
	}

	[Native]
	public enum SettingEstimoteTLMIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEstimoteTLMPowerError : long
	{
		SettingEstimoteTLMPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingConnectableIntervalError : long
	{
		Small = 1,
		Big
	}

	[Native]
	public enum DeviceSettingsManagerError : long
	{
		ynchronizationInProgress,
		ettingNotSupported,
		ettingNotProvidedForWrite,
		ettingValidationFailed,
		ettingCloudReadFailed,
		ettingCloudSaveFailed
	}

	[Native]
	public enum DeviceLocationBeaconError : long
	{
		CloudVerificationFailed,
		BluetoothConnectionFailed,
		ServicesDiscoveryFailed,
		AuthorizationFailed,
		SettingsSynchronizationFailed,
		FirmwareUpdateDeviceNotConnected,
		FirmwareUpdateCloudResponseFailed,
		FirmwareUpdateNoUpdate,
		FirmwareUpdateInProgress
	}

	[Native]
	public enum SettingConnectablePowerError : long
	{
		SettingConnectablePowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEddystoneURLPowerError : long
	{
		SettingEddystoneURLPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEstimoteLocationPowerError : long
	{
		SettingEstimoteLocationPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEddystoneEIDPowerError : long
	{
		SettingEddystoneEIDPowerErrorValueNotAllowed = 1
	}

	[Native]
	public enum SettingEddystoneUIDPowerError : long
	{
		SettingEddystoneUIDPowerErrorValueNotAllowed = 1
	}
}

