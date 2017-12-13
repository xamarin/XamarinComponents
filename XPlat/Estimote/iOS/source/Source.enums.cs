using ObjCRuntime;
using System;

namespace Estimote
{
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
		EddystoneUrl,
		EddystoneUid
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
	public enum UtilityManagerState : long
	{
		Idle,
		Scanning
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
		InvalidCrc,
		RegisterIdChanged,
		InvalidChunkIndex,
		InvalidLength,
		InvalidValueSize,
		InvalidValue,
		InvalidRegisterId,
		InvalidOperation,
		TooLowAuthLevel,
		OperationBlocked,
		NoDataReturned,
		WaitingForMore
	}

	[Native]
	public enum PeripheralFirmwareState : long
	{
		Unknown,
		Boot,
		App
	}

	[Native]
	public enum SettingBaseError : long
	{
		DeviceReferenceNotAvailable
	}

	[Native]
	public enum SettingEstimoteTlmIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEstimoteTlmPowerError : ulong
	{
		ValueNotAllowed = 1
	}

	public enum EstimoteTlmPower : sbyte
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
	public enum SettingConnectableIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum DeviceSettingsManagerError : long
	{
		SynchronizationInProgress,
		SettingNotSupported,
		SettingNotProvidedForWrite,
		SettingValidationFailed,
		SettingCloudReadFailed,
		CloudSaveFailed
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

	public enum LogLevel
	{
		None,
		Error,
		Warning,
		Debug,
		Info,
		Verbose
	}

	[Native]
	public enum BeaconManagerError : long
	{
		InvalidRegion = 1,
		LocationServicesUnauthorized
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
	public enum BeaconEstimoteSecureUuid : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum BeaconMotionUuid : long
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
	public enum SettingConnectablePowerError : ulong
	{
		ValueNotAllowed = 1
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

	[Native]
	public enum SettingOperationStatus : long
	{
		InProgress,
		Complete,
		Failed
	}

	[Native]
	public enum SettingPowerMotionOnlyBroadcastingDelayError : ulong
	{
		TooSmall = 1,
		TooBig = 2
	}

	[Native]
	public enum SettingPowerScheduledAdvertisingPeriodError : ulong
	{
		StartTimeTooBig = 1,
		EndTimeTooBig
	}

	[Native]
	public enum SettingDeviceInfoApplicationVersionError : ulong
	{
		NullValue,
		EmptyString
	}

	[Native]
	public enum SettingDeviceInfoBootloaderVersionError : ulong
	{
		NullValue,
		EmptyString
	}

	[Native]
	public enum SettingDeviceInfoHardwareVersionError : ulong
	{
		NullValue,
		EmptyString
	}

	[Native]
	public enum SettingIBeaconIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingIBeaconMajorError : ulong
	{
		EqualsZero = 1
	}

	[Native]
	public enum SettingIBeaconMinorError : ulong
	{
		EqualsZero = 1
	}

	[Native]
	public enum SettingIBeaconPowerError : ulong
	{
		ValueNotAllowed = 1
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

	[Native]
	public enum SettingIBeaconProximityUuidError : ulong
	{
		InvalidValue = 1
	}

	[Native]
	public enum SettingEstimoteLocationIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEstimoteLocationPowerError : ulong
	{
		ValueNotAllowed = 1
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

	[Native]
	public enum SettingEddystoneUidInstanceError : ulong
	{
		InvalidFormat = 1,
		TooShort,
		TooLong
	}

	[Native]
	public enum SettingEddystoneUidNamespaceError : ulong
	{
		InvalidFormat = 1,
		TooShort,
		TooLong
	}

	[Native]
	public enum SettingEddystoneUidIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneUidPowerError : ulong
	{
		ValueNotAllowed = 1
	}

	public enum EddystoneUidPower : sbyte
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
	public enum SettingEddystoneUrlNamespaceError : ulong
	{
		InvalidUrl = 1
	}

	[Native]
	public enum SettingEddystoneUrlIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneUrlPowerError : ulong
	{
		ErrorValueNotAllowed = 1
	}

	public enum EddystoneUrlPower : sbyte
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
	public enum SettingEddystoneTlmIntervalError : ulong
	{
		Small = 1,
		Big
	}

	[Native]
	public enum SettingEddystoneTlmPowerError : ulong
	{
		ValueNotAllowed = 1
	}

	public enum EddystoneTlmPower : sbyte
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
	public enum SettingEddystoneEidIntervalError : ulong
	{
		Small = 1,
		Big = 2
	}

	[Native]
	public enum SettingEddystoneEidPowerError : ulong
	{
		ValueNotAllowed = 1
	}

	public enum EddystoneEidPower : sbyte
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
	public enum GenericAdvertiserId : long
	{
		Id1 = 1,
		Id2 = 2
	}

	[Native]
	public enum SettingGenericAdvertiserEnableError : ulong
	{
		InvalidAdvertiserId = 1
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
	public enum SettingGenericAdvertiserPowerError : ulong
	{
		ValueNotAllowed = 1,
		InvalidAdvertiserId
	}

	[Native]
	public enum SettingGenericAdvertiserIntervalError : ulong
	{
		ValueTooSmall = 1,
		ValueTooBig,
		InvalidAdvertiserId
	}

	[Native]
	public enum SettingGenericAdvertiserDataError : ulong
	{
		CanNotBeNull = 1,
		InvalidAdvertiserId
	}

	[Native]
	public enum GpioConfigError : ulong
	{
		ValueNotAllowed = 1
	}

	public enum GpioConfig : byte
	{
		InputNoPull = 0,
		InputPullDown = 1,
		InputPullUp = 2,
		Output = 3,
		Uart = 4
	}

	[Native]
	public enum GpioPort : long
	{
		Port0,
		Port1
	}

	[Native]
	public enum GpioPortsDataError : long
	{
		Port,
		Value
	}

	[Native]
	public enum GpioPortValue : long
	{
		Unknown = -1,
		Low = 0,
		High = 1
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

	public enum NearableBroadcastingScheme : sbyte
	{
		Unknown = -1,
		Nearable,
		IBeacon,
		EddystoneUrl
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
	public enum NearableSettingsManagerError : ulong
	{
		SynchronizationInProgress,
		SettingNotSupported,
		SettingNotProvidedForWrite,
		SettingValidationFailed,
		SettingCloudReadFailed,
		CloudSaveFailed
	}

	[Native]
	public enum SettingNearableIntervalError : ulong
	{
		ValueTooSmall = 1,
		ValueTooBig,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum SettingNearablePowerError : ulong
	{
		ValueNotAllowed = 1,
		ConvenienceAPIUnsupported
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
	public enum SettingNearableEddystoneUrlError : ulong
	{
		InvalidUrl = 1,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum SettingNearableBroadcastingSchemeError : ulong
	{
		NotAllowed = 1,
		ConvenienceAPIUnsupported
	}

	[Native]
	public enum EsBeaconUpdateInfoStatus : long
	{
		Idle,
		ReadyToUpdate,
		Updating,
		UpdateSuccess,
		UpdateFailed
	}

	[Native]
	public enum EsBulkUpdaterStatus : long
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
	public enum RequestGetBeaconsError : long
	{
		EsTRequestGetBeaconsErrorUnknown
	}

	[Native]
	public enum BeaconDetailsFields : ulong
	{
		AllFields = 1 << 0,
		Mac = 1 << 1,
		Color = 1 << 2,
		Name = 1 << 3,
		GpsLocation = 1 << 4,
		IndoorLocation = 1 << 5,
		PublicIdentifier = 1 << 6,
		RemainingBatteryLifetime = 1 << 7,
		AllSettings = 1 << 8,
		Battery = 1 << 9,
		Power = 1 << 10,
		Interval = 1 << 11,
		Hardware = 1 << 12,
		Firmware = 1 << 13,
		BasicPowerMode = 1 << 14,
		SmartPowerMode = 1 << 15,
		TimeZone = 1 << 16,
		Security = 1 << 17,
		MotionDetection = 1 << 18,
		ConditionalBroadcasting = 1 << 19,
		BroadcastingScheme = 1 << 20,
		UUidMajorMinor = 1 << 21,
		EddystoneNamespaceId = 1 << 22,
		EddystoneInstanceId = 1 << 23
	}

	[Native]
	public enum BeaconPublicDetailsFields : ulong
	{
		AllFields = 1 << 0,
		Mac = 1 << 1,
		Color = 1 << 2,
		PublicIdentifier = 1 << 3,
		AllSettings = 1 << 4,
		Power = 1 << 5,
		Security = 1 << 6,
		BroadcastingScheme = 1 << 7,
		UUidMajorMinor = 1 << 8,
		EddystoneNamespaceId = 1 << 9,
		EddystoneInstanceId = 1 << 10
	}

	[Native]
	public enum RequestBeaconColorError : long
	{
		ColorNotAvailable
	}

	[Native]
	public enum RequestBeaconMacError : long
	{
		Unknown
	}

	[Native]
	public enum RequestAssignGpsLocationError : long
	{
		GpsLocationNotAvailable
	}

	[Native]
	public enum RequestGetNearablesError : long
	{
		Unknown
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
	public enum RequestV3GetFirmwresError : long
	{
		InvalidValue = 1
	}

	[Native]
	public enum RequestGetDevicesTypeMask : ulong
	{
		Beacon = 1 << 0,
		Mirror = 1 << 1,
		Sticker = 1 << 2,
		All = 7
	}

	[Native]
	public enum MonitoringProximity : ulong
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
	public enum MonitoringState : ulong
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
}

