using ObjCRuntime;
using System;

namespace Estimote
{
	using ObjCRuntime;

	public enum  Color
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
		Transparent
	}

	public enum  FirmwareUpdate
	{
		None,
		Available,
		Unsupported
	}

	public enum  ConnectionStatus
	{
		Disconnected,
		Connecting,
		Connected,
		Updating
	}

	public enum  BroadcastingScheme : sbyte
	{
		Unknown,
		Estimote,
		IBeacon,
		EddystoneURL,
		EddystoneUID
	}

	[Native]
	public enum  NearableType : long
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
	public enum  NearableOrientation : long
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
	public enum  NearableZone : long
	{
		Unknown = 0,
		Immediate,
		Near,
		Far
	}

	[Native]
	public enum  NearableFirmwareState : long
	{
		Boot = 0,
		App
	}

	public enum  BeaconPower : sbyte
	{
		BeaconPowerLevel1 = -30,
		BeaconPowerLevel2 = -20,
		BeaconPowerLevel3 = -16,
		BeaconPowerLevel4 = -12,
		BeaconPowerLevel5 = -8,
		BeaconPowerLevel6 = -4,
		BeaconPowerLevel7 = 0,
		BeaconPowerLevel8 = 4
	}

	public enum  BeaconBatteryType
	{
		Unknown = 0,
		Cr2450,
		Cr2477
	}

	[Native]
	public enum  BeaconFirmwareState : long
	{
		Boot,
		App
	}

	[Native]
	public enum  BeaconPowerSavingMode : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum  BeaconEstimoteSecureUUID : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum  BeaconMotionUUID : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum  BeaconMotionState : long
	{
		Unknown,
		Unsupported,
		Moving,
		NotMoving
	}

	[Native]
	public enum  BeaconTemperatureState : long
	{
		Unknown,
		Unsupported,
		Supported
	}

	[Native]
	public enum  BeaconMotionDetection : long
	{
		Unknown,
		Unsupported,
		On,
		Off
	}

	[Native]
	public enum  BeaconConditionalBroadcasting : long
	{
		Unknown,
		Unsupported,
		Off,
		MotionOnly,
		FlipToStop
	}

	[Native]
	public enum  BeaconCharInfoType : long
	{
		Read,
		Only
	}

	public enum  Connection : uint
	{
		InternetConnectionError,
		IdentifierMissingError,
		NotAuthorizedError,
		NotConnectedToReadWrite
	}

	[Native]
	public enum  UtilitManagerState : long
	{
		Idle,
		Scanning
	}

	[Native]
	public enum  Notification : long
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
	public enum BeaconUpdateInfoStatus : long
	{
		Idle,
		ReadyToUpdate,
		Updating,
		UpdateSuccess,
		UpdateFailed
	}

	[Native]
	public enum BulkUpdaterStatus : long
	{
		Idle,
		Updating,
		Completed
	}

	[Native]
	public enum  BulkUpdaterMode : long
	{
		Foreground,
		Background
	}

	[Native]
	public enum  EddystoneProximity : long
	{
		Unknown,
		Immediate,
		Near,
		Far
	}

	[Native]
	public enum  EddystoneManagerState : long
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
    public enum NearableDeviceError : long
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
        Level2 = -20,
        Level3 = -16,
        Level4 = -12,
        Level5 = -8,
        Level6 = -4,
        Level7 = 0,
        Level8 = 4
    }

    [Native]
    public enum BulkUpdaterDeviceUpdateStatus : long
    {
        Unknown,
        Scanning,
        PendingUpdate,
        Updating,
        Succeeded,
        Failed
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
        Level2 = -20,
        Level3 = -16,
        Level4 = -12,
        Level5 = -8,
        Level6 = -4,
        Level7 = 0,
        Level8 = 4
    }

    public enum EddystoneUIDPower : sbyte
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

    public enum EddystoneURLPower : sbyte
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

    public enum EddystoneEIDPower : sbyte
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

    public enum EstimoteLocationPower : sbyte
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

    public enum EstimoteTLMPower : sbyte
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

    public enum IBeaconPower : sbyte
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
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }

}

