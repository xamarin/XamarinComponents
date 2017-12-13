using System;
using CoreBluetooth;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace Estimote
{
	// @interface ESTDevice : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTDevice")]
	interface Device
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull macAddress __attribute__((deprecated("Starting from SDK 3.7.0 use identifier instead of macAddress.")));
		[Obsolete ("Starting from SDK 3.7.0 use Identifier property instead.")]
		[Export ("macAddress", ArgumentSemantic.Strong)]
		string MacAddress { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, nonatomic, strong) NSUUID * _Nonnull peripheralIdentifier;
		[Export ("peripheralIdentifier", ArgumentSemantic.Strong)]
		NSUuid PeripheralIdentifier { get; }

		// @property (readonly, assign, nonatomic) NSInteger rssi;
		[Export ("rssi")]
		nint Rssi { get; }

		// @property (readonly, nonatomic, strong) NSDate * _Nonnull discoveryDate;
		[Export ("discoveryDate", ArgumentSemantic.Strong)]
		NSDate DiscoveryDate { get; }

		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)identifier peripheralIdentifier:(NSUUID * _Nonnull)peripheralIdentifier rssi:(NSInteger)rssi discoveryDate:(NSDate * _Nonnull)discoveryDate;
		[Export ("initWithDeviceIdentifier:peripheralIdentifier:rssi:discoveryDate:")]
		IntPtr Constructor (string identifier, NSUuid peripheralIdentifier, nint rssi, NSDate discoveryDate);
	}

	// @interface ESTBluetoothBeacon : ESTDevice
	[BaseType (typeof (Device), Name = "ESTBluetoothBeacon")]
	interface BluetoothBeacon
	{
		// @property (nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; set; }

		// @property (nonatomic, strong) CBPeripheral * _Nonnull peripheral;
		[Export ("peripheral", ArgumentSemantic.Strong)]
		CBPeripheral Peripheral { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull measuredPower;
		[Export ("measuredPower", ArgumentSemantic.Strong)]
		NSNumber MeasuredPower { get; set; }

		// @property (assign, nonatomic) NSInteger firmwareState;
		[Export ("firmwareState")]
		nint FirmwareState { get; set; }
	}

	// typedef void (^ESTCompletionBlock)(NSError * _Nullable);
	delegate void CompletionBlock ([NullAllowed] NSError error);

	// typedef void (^ESTObjectCompletionBlock)(id _Nullable, NSError * _Nullable);
	delegate void ObjectCompletionBlock ([NullAllowed] NSObject result, [NullAllowed] NSError error);
	delegate void FirmwareInfoVOCompletionBlock ([NullAllowed] FirmwareInfoVO value, [NullAllowed] NSError error);
	delegate void BeaconConnectionCompletionBlock ([NullAllowed] BeaconConnection value, [NullAllowed] NSError error);
	delegate void BeaconVOCompletionBlock ([NullAllowed] BeaconVO value, [NullAllowed] NSError error);
	delegate void NearableVOCompletionBlock ([NullAllowed] NearableVO value, [NullAllowed] NSError error);
	delegate void NSNumberCompletionBlock ([NullAllowed] NSNumber value, [NullAllowed] NSError error);
	delegate void CLLocationCompletionBlock ([NullAllowed] CLLocation value, [NullAllowed] NSError error);

	// typedef void (^ESTDataCompletionBlock)(NSData * _Nullable, NSError * _Nullable);
	delegate void DataCompletionBlock ([NullAllowed] NSData result, [NullAllowed] NSError error);

	// typedef void (^ESTNumberCompletionBlock)(NSNumber * _Nullable, NSError * _Nullable);
	delegate void NumberCompletionBlock ([NullAllowed] NSNumber value, [NullAllowed] NSError error);

	// typedef void (^ESTUnsignedShortCompletionBlock)(unsigned short, NSError * _Nullable);
	delegate void UnsignedShortCompletionBlock (ushort value, [NullAllowed] NSError error);

	// typedef void (^ESTBoolCompletionBlock)(BOOL, NSError * _Nullable);
	delegate void BoolCompletionBlock (bool value, [NullAllowed] NSError error);

	// typedef void (^ESTStringCompletionBlock)(NSString * _Nullable, NSError * _Nullable);
	delegate void StringCompletionBlock ([NullAllowed] string value, [NullAllowed] NSError error);

	// typedef void (^ESTProgressBlock)(NSInteger, NSString * _Nullable, NSError * _Nullable);
	delegate void ProgressBlock (nint value, [NullAllowed] string description, [NullAllowed] NSError error);

	// typedef void (^ESTArrayCompletionBlock)(NSArray * _Nullable, NSError * _Nullable);
	//delegate void ArrayCompletionBlock ([NullAllowed] NSObject[] arg0, [NullAllowed] NSError error);
	delegate void NearableArrayCompletionBlock ([NullAllowed] Nearable[] value, [NullAllowed] NSError error);
	delegate void BeaconVOArrayCompletionBlock ([NullAllowed] BeaconVO[] value, [NullAllowed] NSError error);
	delegate void BeaconUpdateInfoArrayCompletionBlock ([NullAllowed] BeaconUpdateInfo[] value, [NullAllowed] NSError error);

	// typedef void (^ESTDictionaryCompletionBlock)(NSDictionary * _Nullable, NSError * _Nullable);
	delegate void DictionaryCompletionBlock ([NullAllowed] NSDictionary value, [NullAllowed] NSError error);

	// typedef void (^ESTCsRegisterCompletonBlock)(NSError * _Nullable);
	delegate void CsRegisterCompletonBlock ([NullAllowed] NSError error);

	// @interface ESTDefinitions : NSObject
	[BaseType (typeof (NSObject), Name = "ESTDefinitions")]
	interface Definitions
	{
		// +(NSString * _Nonnull)nameForEstimoteColor:(ESTColor)color;
		[Static]
		[Export ("nameForEstimoteColor:")]
		string NameForEstimoteColor (Color color);
	}

	interface ISettingProtocol { }

	// @protocol ESTSettingProtocol <NSObject, NSCopying>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTSettingProtocol")]
	interface SettingProtocol : INSCopying
	{
		// @required -(void)fireSuccessBlockWithData:(NSData * _Nonnull)result;
		[Abstract]
		[Export ("fireSuccessBlockWithData:")]
		void FireSuccessBlock (NSData result);

		// @required -(void)fireFailureBlockWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("fireFailureBlockWithError:")]
		void FireFailureBlock (NSError error);

		// @required -(id _Nonnull)getValue;
		[Abstract]
		[Export ("getValue")]
		NSObject Value { get; }
	}

	interface IDeviceSettingProtocol { }

	// @protocol ESTDeviceSettingProtocol <ESTSettingProtocol>
	[Protocol]
	interface DeviceSettingProtocol : SettingProtocol
	{
		// @required -(uint16_t)registerID;
		[Abstract]
		[Export ("registerID")]
		ushort RegisterId { get; }

		// @required -(NSData * _Nullable)getValueData;
		[Abstract]
		[NullAllowed, Export ("getValueData")]
		NSData ValueData { get; }

		// @required -(void)updateValueWithData:(NSData * _Nonnull)data;
		[Abstract]
		[Export ("updateValueWithData:")]
		void UpdateValue (NSData data);

		// @required -(BOOL)isAvailableForFirmwareVersion:(NSString * _Nonnull)firmwareVersion;
		[Abstract]
		[Export ("isAvailableForFirmwareVersion:")]
		bool IsAvailable (string firmwareVersion);

		// @optional -(NSError * _Nonnull)validateValue;
		[Export ("validateValue")]
		NSError ValidateValue { get; }

		// @optional -(void)updateValueInSettings:(id _Nonnull)settings;
		[Export ("updateValueInSettings:")]
		void UpdateValue (NSObject settings);
	}

	interface ICloudSettingProtocol { }

	// @protocol ESTCloudSettingProtocol <ESTSettingProtocol>
	[Protocol]
	interface CloudSettingProtocol : SettingProtocol
	{
	}

	interface IDeviceNotificationProtocol { }

	// @protocol ESTDeviceNotificationProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTDeviceNotificationProtocol")]
	interface DeviceNotificationProtocol
	{
		// @required -(uint16_t)registerID;
		[Abstract]
		[Export ("registerID")]
		ushort RegisterId { get; }

		// @required -(void)fireHandlerWithData:(NSData * _Nonnull)data;
		[Abstract]
		[Export ("fireHandlerWithData:")]
		void FireHandler (NSData data);

		// @required -(NSString * _Nonnull)supportedFirmwareVersion;
		[Abstract]
		[Export ("supportedFirmwareVersion")]
		string SupportedFirmwareVersion { get; }
	}

	interface IBeaconOperationProtocol { }

	// @protocol ESTBeaconOperationProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTBeaconOperationProtocol")]
	interface BeaconOperationProtocol
	{
		// @required -(ESTSettingOperationType)type;
		[Abstract]
		[Export ("type")]
		SettingOperationType Type { get; }

		// @required -(ESTSettingStorageType)storageType;
		[Abstract]
		[Export ("storageType")]
		SettingStorageType StorageType { get; }

		// @required -(uint16_t)registerID;
		[Abstract]
		[Export ("registerID")]
		ushort RegisterId { get; }

		// @required -(NSData * _Nonnull)valueData;
		[Abstract]
		[Export ("valueData")]
		NSData ValueData { get; }

		// @required -(id _Nonnull)valueCloud;
		[Abstract]
		[Export ("valueCloud")]
		NSObject ValueCloud { get; }

		// @required -(NSString * _Nonnull)supportedFirmwareVersion;
		[Abstract]
		[Export ("supportedFirmwareVersion")]
		string SupportedFirmwareVersion { get; }

		// @required -(BOOL)shouldSynchronize;
		[Abstract]
		[Export ("shouldSynchronize")]
		bool ShouldSynchronize { get; }

		// @required -(ESTSettingBase * _Nonnull)getSetting;
		[Abstract]
		[Export ("getSetting")]
		SettingBase Setting { get; }

		// @required -(void)updateSettingWithData:(NSData * _Nonnull)data;
		[Abstract]
		[Export ("updateSettingWithData:")]
		void UpdateSetting (NSData data);

		// @required -(void)fireSuccessBlockWithData:(NSData * _Nonnull)result;
		[Abstract]
		[Export ("fireSuccessBlockWithData:")]
		void FireSuccessBlock (NSData result);

		// @required -(void)fireFailureBlockWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("fireFailureBlockWithError:")]
		void FireFailureBlock (NSError error);

		// @optional -(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)associatedOperations;
		[Export ("associatedOperations")]
		IBeaconOperationProtocol[] AssociatedOperations { get; }
	}

	// typedef void (^ESTDeviceFirmwareUpdateProgressBlock)(NSInteger);
	delegate void DeviceFirmwareUpdateProgressBlock (nint value);

	interface IDeviceConnectableDelegate { }

	// @protocol ESTDeviceConnectableDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTDeviceConnectableDelegate")]
	interface DeviceConnectableDelegate
	{
		// @optional -(void)estDeviceConnectionDidSucceed:(ESTDeviceConnectable * _Nonnull)device;
		[Export ("estDeviceConnectionDidSucceed:"), EventArgs ("DeviceConnectableConnectionSucceeded")]
		void ConnectionSucceeded (DeviceConnectable device);

		// @optional -(void)estDevice:(ESTDeviceConnectable * _Nonnull)device didDisconnectWithError:(NSError * _Nullable)error;
		[Export ("estDevice:didDisconnectWithError:"), EventArgs ("DeviceConnectableDisconnected")]
		void Disconnected (DeviceConnectable device, [NullAllowed] NSError error);

		// @optional -(void)estDevice:(ESTDeviceConnectable * _Nonnull)device didFailConnectionWithError:(NSError * _Nonnull)error;
		[Export ("estDevice:didFailConnectionWithError:"), EventArgs ("DeviceConnectableConnectionFailed")]
		void ConnectionFailed (DeviceConnectable device, NSError error);
	}

	// @interface ESTDeviceConnectable : ESTDevice
	[BaseType (typeof (Device), Name = "ESTDeviceConnectable", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (DeviceConnectableDelegate) })]
	interface DeviceConnectable
	{
		// @property (nonatomic, weak) id<ESTDeviceConnectableDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IDeviceConnectableDelegate Delegate { get; set; }

		// @property (readonly, assign, nonatomic) ESTConnectionStatus connectionStatus;
		[Export ("connectionStatus", ArgumentSemantic.Assign)]
		ConnectionStatus ConnectionStatus { get; }

		// -(void)connect;
		[Export ("connect")]
		void Connect ();

		// -(void)connectAndUpdate;
		[Export ("connectAndUpdate")]
		void ConnectAndUpdate ();

		// -(void)disconnect;
		[Export ("disconnect")]
		void Disconnect ();

		// -(void)checkFirmwareUpdateWithCompletion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("checkFirmwareUpdateWithCompletion:"), Async]
		void CheckFirmwareUpdate (FirmwareInfoVOCompletionBlock completion);

		// -(void)updateFirmwareWithProgress:(ESTDeviceFirmwareUpdateProgressBlock _Nonnull)progress completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("updateFirmwareWithProgress:completion:"), Async]
		void UpdateFirmware (DeviceFirmwareUpdateProgressBlock progress, CompletionBlock completion);
	}

	// @interface ESTDeviceNearable : ESTDeviceConnectable
	[BaseType (typeof (DeviceConnectable), Name = "ESTDeviceNearable")]
	interface DeviceNearable
	{
		// @property (readonly, nonatomic, strong) ESTNearableSettingsManager * _Nonnull settings;
		[Export ("settings", ArgumentSemantic.Strong)]
		NearableSettingsManager Settings { get; }
	}

	interface IUtilityManagerDelegate { }

	// @protocol ESTUtilityManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTUtilityManagerDelegate")]
	interface UtilityManagerDelegate
	{
		// @optional -(void)utilityManager:(ESTUtilityManager * _Nonnull)manager didDiscoverBeacons:(NSArray<ESTBluetoothBeacon *> * _Nonnull)beacons;
		[Export ("utilityManager:didDiscoverBeacons:"), EventArgs ("UtilityManagerDiscoveredBeacons")]
		void DiscoveredBeacons (UtilityManager manager, BluetoothBeacon[] beacons);

		// @optional -(void)utilityManager:(ESTUtilityManager * _Nonnull)manager didDiscoverNearables:(NSArray<ESTDeviceNearable *> * _Nonnull)nearables;
		[Export ("utilityManager:didDiscoverNearables:"), EventArgs ("UtilityManagerDiscoveredNearables")]
		void DiscoveredNearables (UtilityManager manager, DeviceNearable[] nearables);

		// @optional -(void)utilityManagerDidFailDiscovery:(ESTUtilityManager * _Nonnull)manager;
		[Export ("utilityManagerDidFailDiscovery:"), EventArgs ("UtilityManagerDiscoveryFailed")]
		void DiscoveryFailed (UtilityManager manager);
	}

	// @interface ESTUtilityManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTUtilityManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (UtilityManagerDelegate) })]
	interface UtilityManager
	{
		// @property (readonly, assign, nonatomic) ESTUtilityManagerState state;
		[Export ("state", ArgumentSemantic.Assign)]
		UtilityManagerState State { get; }

		// @property (nonatomic, weak) id<ESTUtilityManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IUtilityManagerDelegate Delegate { get; set; }

		// -(void)startEstimoteBeaconDiscovery;
		[Export ("startEstimoteBeaconDiscovery")]
		void StartEstimoteBeaconDiscovery ();

		// -(void)startEstimoteBeaconDiscoveryWithUpdateInterval:(NSTimeInterval)interval;
		[Export ("startEstimoteBeaconDiscoveryWithUpdateInterval:")]
		void StartEstimoteBeaconDiscovery (double interval);

		// -(void)stopEstimoteBeaconDiscovery;
		[Export ("stopEstimoteBeaconDiscovery")]
		void StopEstimoteBeaconDiscovery ();

		// -(void)startEstimoteNearableDiscovery;
		[Export ("startEstimoteNearableDiscovery")]
		void StartEstimoteNearableDiscovery ();

		// -(void)startEstimoteNearableDiscoveryWithUpdateInterval:(NSTimeInterval)interval;
		[Export ("startEstimoteNearableDiscoveryWithUpdateInterval:")]
		void StartEstimoteNearableDiscovery (double interval);

		// -(void)stopEstimoteNearableDiscovery;
		[Export ("stopEstimoteNearableDiscovery")]
		void StopEstimoteNearableDiscovery ();
	}

	// typedef void (^ESTPeripheralDiscoveryCompletionBlock)(NSError *);
	delegate void PeripheralDiscoveryCompletionBlock (NSError error);

	interface IPeripheral { }

	// @protocol ESTPeripheral <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTPeripheral")]
	interface Peripheral
	{
		// Constructor not allowed in Protocol
		// @required -(id)initWithPeripheral:(CBPeripheral *)peripheral;
		//[Abstract]
		//[Export ("initWithPeripheral:")]
		//IntPtr Constructor (CBPeripheral peripheral);

		// @required -(void)discoverServicesAndCharacteristicsWithCompletion:(ESTPeripheralDiscoveryCompletionBlock)completion;
		[Abstract]
		[Export ("discoverServicesAndCharacteristicsWithCompletion:"), Async]
		void DiscoverServicesAndCharacteristics (PeripheralDiscoveryCompletionBlock completion);
	}

	interface IPeripheralNotificationDelegate { }

	// @protocol ESTPeripheralNotificationDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTPeripheralNotificationDelegate")]
	interface PeripheralNotificationDelegate
	{
		// @required -(void)peripheral:(id<ESTPeripheral>)peripheral didReceiveNotification:(id<ESTDeviceNotificationProtocol>)notification withData:(NSData *)data;
		[Abstract]
		[Export ("peripheral:didReceiveNotification:withData:")]
		void ReceivedNotification (IPeripheral peripheral, IDeviceNotificationProtocol notification, NSData data);
	}

	interface IPeripheralTypeUtilityDelegate { }

	// @protocol ESTPeripheralTypeUtilityDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTPeripheralTypeUtilityDelegate")]
	interface PeripheralTypeUtilityDelegate
	{
		// @required -(void)peripheral:(id<ESTPeripheral>)peripheral didPerformOperation:(id<ESTBeaconOperationProtocol>)operation andReceivedData:(NSData *)data;
		[Abstract]
		[Export ("peripheral:didPerformOperation:andReceivedData:"), EventArgs ("PeripheralTypeUtilityPerformedOperation")]
		void PerformedOperation (IPeripheral peripheral, IBeaconOperationProtocol operation, NSData data);

		// @required -(void)peripheral:(id<ESTPeripheral>)peripheral didFailOperation:(id<ESTBeaconOperationProtocol>)operation withError:(NSError *)error;
		[Abstract]
		[Export ("peripheral:didFailOperation:withError:"), EventArgs ("PeripheralTypeUtilityOperationFailed")]
		void OperationFailed (IPeripheral peripheral, IBeaconOperationProtocol operation, NSError error);
	}

	// @interface ESTPeripheralTypeUtility : NSObject <ESTPeripheral>
	[BaseType (typeof (NSObject), Name = "ESTPeripheralTypeUtility", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (PeripheralTypeUtilityDelegate) })]
	interface PeripheralTypeUtility : Peripheral
	{
		// @property (readonly, nonatomic) ESTPeripheralFirmwareState firmwareState;
		[Export ("firmwareState")]
		PeripheralFirmwareState FirmwareState { get; }

		// @property (nonatomic, strong) id<ESTPeripheralTypeUtilityDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Strong)]
		IPeripheralTypeUtilityDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ESTPeripheralNotificationDelegate> notificationDelegate;
		[NullAllowed, Export ("notificationDelegate", ArgumentSemantic.Weak)]
		IPeripheralNotificationDelegate NotificationDelegate { get; set; }

		// -(void)resetPeripheralToBootWithCompletion:(ESTCompletionBlock)completion;
		[Export ("resetPeripheralToBootWithCompletion:"), Async]
		void ResetPeripheralToBoot (CompletionBlock completion);

		// -(void)performSettingOperation:(id<ESTBeaconOperationProtocol>)operation;
		[Export ("performSettingOperation:")]
		void PerformSettingOperation (IBeaconOperationProtocol operation);

		// -(void)registerNotification:(id<ESTDeviceNotificationProtocol>)notification;
		[Export ("registerNotification:")]
		void RegisterNotification (IDeviceNotificationProtocol notification);

		// -(void)unregisterAllNotifications;
		[Export ("unregisterAllNotifications")]
		void UnregisterAllNotifications ();
	}

	// typedef void (^ESTSettingCompletionBlock)(ESTSettingBase * _Nullable, NSError * _Nullable);
	delegate void SettingCompletionBlock ([NullAllowed] SettingBase setting, [NullAllowed] NSError error);

	// @interface ESTSettingBase : NSObject
	[BaseType (typeof (NSObject), Name = "ESTSettingBase")]
	interface SettingBase
	{
		// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data;
		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		// -(BOOL)isDuplicateOfSetting:(ESTSettingBase * _Nonnull)setting;
		[Export ("isDuplicateOfSetting:")]
		bool Duplicates (SettingBase setting);

		// ESTSettingBase Internal Category
		// @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
		[NullAllowed]
		[Export ("device", ArgumentSemantic.Weak)]
		DeviceConnectable Device { get; set; }
	}

	// @interface Internal (ESTSettingBase)
	//[Category]
	//[BaseType (typeof (SettingBase))]
	//interface ESTSettingBase_Internal
	//{
	//	// @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
	//	[NullAllowed, Export ("device")]
	//	DeviceConnectable GetDevice ();

	//	[Export ("setDevice:")]
	//	void SetDevice ([NullAllowed] DeviceConnectable device);
	//}

	// @interface ESTDeviceSettingsCollection : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "ESTDeviceSettingsCollection")]
	interface DeviceSettingsCollection : INSCopying
	{
		// -(instancetype _Nonnull)initWithSettingsArray:(NSArray<ESTSettingBase *> * _Nonnull)settingsArray;
		[Export ("initWithSettingsArray:")]
		IntPtr Constructor (SettingBase[] settingsArray);

		// -(void)addOrReplaceSetting:(ESTSettingBase * _Nonnull)setting;
		[Export ("addOrReplaceSetting:")]
		void AddOrReplaceSetting (SettingBase setting);

		// -(void)addOrReplaceSettings:(NSArray<ESTSettingBase *> * _Nonnull)settings;
		[Export ("addOrReplaceSettings:")]
		void AddOrReplaceSettings (SettingBase[] settings);

		// -(id _Nullable)getSettingForClass:(Class _Nonnull)targetedClass;
		[Export ("getSettingForClass:")]
		[return: NullAllowed]
		NSObject GetSetting (Class targetedClass);

		// -(NSArray<ESTSettingBase *> * _Nullable)getAllSettingsForClass:(Class _Nonnull)targetedClass;
		[Export ("getAllSettingsForClass:")]
		[return: NullAllowed]
		SettingBase[] GetAllSettings (Class targetedClass);

		// -(NSArray * _Nonnull)getSettings;
		[Export ("getSettings")]
		DeviceSettings[] Settings { get; }
	}

	// @interface ESTSettingsIBeacon : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsIBeacon")]
	interface SettingsIBeacon
	{
		// @property (readonly, nonatomic, strong) ESTSettingIBeaconEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingIBeaconEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingIBeaconPower Power { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingIBeaconInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconProximityUUID * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		SettingIBeaconProximityUuid ProximityUuid { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconMotionUUIDEnable * _Nonnull motionUUIDEnable;
		[Export ("motionUUIDEnable", ArgumentSemantic.Strong)]
		SettingIBeaconMotionUuidEnable MotionUuidEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconMotionUUID * _Nonnull motionUUID;
		[Export ("motionUUID", ArgumentSemantic.Strong)]
		SettingIBeaconMotionUuid MotionUuid { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconMajor * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		SettingIBeaconMajor Major { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconMinor * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		SettingIBeaconMinor Minor { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconSecureUUIDPeriodScaler * _Nonnull secureUUIDPeriodScaler;
		[Export ("secureUUIDPeriodScaler", ArgumentSemantic.Strong)]
		SettingIBeaconSecureUuidPeriodScaler SecureUuidPeriodScaler { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconSecureUUIDEnable * _Nonnull secureUUIDEnable;
		[Export ("secureUUIDEnable", ArgumentSemantic.Strong)]
		SettingIBeaconSecureUuidEnable SecureUuidEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingIBeaconNonStrictMode * _Nonnull nonStrictModeEnable;
		[Export ("nonStrictModeEnable", ArgumentSemantic.Strong)]
		SettingIBeaconNonStrictMode NonStrictModeEnable { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEstimoteLocation : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEstimoteLocation")]
	interface SettingsEstimoteLocation
	{
		// @property (readonly, nonatomic, strong) ESTSettingEstimoteLocationEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEstimoteLocationEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEstimoteLocationInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEstimoteLocationInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEstimoteLocationPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEstimoteLocationPower Power { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingReadOnly : ESTSettingBase
	[BaseType (typeof (SettingBase), Name = "ESTSettingReadOnly")]
	interface SettingReadOnly
	{
		// -(void)readValueWithCompletion:(ESTSettingCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingCompletionBlock completion);
	}

	// @interface ESTSettingReadWrite : ESTSettingReadOnly
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingReadWrite")]
	interface SettingReadWrite
	{
		// -(void)writeValue:(id _Nonnull)value completion:(ESTSettingCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NSObject value, SettingCompletionBlock completion);
	}

	// typedef void (^ESTSettingEstimoteTLMEnableCompletionBlock)(ESTSettingEstimoteTLMEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteTlmEnableCompletionBlock ([NullAllowed] SettingEstimoteTlmEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteTLMEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteTLMEnable")]
	interface SettingEstimoteTlmEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteTlmEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingEstimoteTlmEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEstimoteTLMIntervalCompletionBlock)(ESTSettingEstimoteTLMInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteTlmIntervalCompletionBlock ([NullAllowed] SettingEstimoteTlmInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteTLMInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteTLMInterval")]
	interface SettingEstimoteTlmInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteTlmIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingEstimoteTlmIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingEstimoteTLMPowerCompletionBlock)(ESTSettingEstimoteTLMPower * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteTlmPowerCompletionBlock ([NullAllowed] SettingEstimoteTlmPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteTLMPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteTLMPower")]
	interface SettingEstimoteTlmPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTEstimoteTLMPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EstimoteTlmPower power);

		// -(ESTEstimoteTLMPower)getValue;
		[Export ("getValue")]
		EstimoteTlmPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteTlmPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEstimoteTLMPower)power completion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EstimoteTlmPower power, SettingEstimoteTlmPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEstimoteTLMPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EstimoteTlmPower power);
	}

	// @interface ESTSettingsEstimoteTLM : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEstimoteTLM")]
	interface SettingsEstimoteTlm
	{
		// @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEstimoteTlmEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEstimoteTlmInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEstimoteTlmPower Power { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEddystoneUID : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEddystoneUID")]
	interface SettingsEddystoneUid
	{
		// @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEddystoneUidEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDNamespace * _Nonnull namespaceID;
		[Export ("namespaceID", ArgumentSemantic.Strong)]
		SettingEddystoneUidNamespace NamespaceId { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDInstance * _Nonnull instanceID;
		[Export ("instanceID", ArgumentSemantic.Strong)]
		SettingEddystoneUidInstance InstanceId { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEddystoneUidInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEddystoneUidPower Power { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEddystoneURL : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEddystoneURL")]
	interface SettingsEddystoneUrl
	{
		// @property (readonly, nonatomic, strong) ESTSettingEddystoneURLEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEddystoneUrlEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneURLInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEddystoneUrlInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneURLPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEddystoneUrlPower Power { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneURLData * _Nonnull URLData;
		[Export ("URLData", ArgumentSemantic.Strong)]
		SettingEddystoneUrlData UrlData { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEddystoneTLM : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEddystoneTLM")]
	interface SettingsEddystoneTlm
	{
		// @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEddystoneTlmEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEddystoneTlmInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEddystoneTlmPower Power { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEddystoneEID : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEddystoneEID")]
	interface SettingsEddystoneEid
	{
		// @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDEnable * _Nonnull enable;
		[Export ("enable", ArgumentSemantic.Strong)]
		SettingEddystoneEidEnable Enable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingEddystoneEidInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingEddystoneEidPower Power { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsDeviceInfo : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsDeviceInfo")]
	interface SettingsDeviceInfo
	{
		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoColor * _Nonnull color;
		[Export ("color", ArgumentSemantic.Strong)]
		SettingDeviceInfoColor Color { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoFirmwareVersion * _Nonnull firmwareVersion;
		[Export ("firmwareVersion", ArgumentSemantic.Strong)]
		SettingDeviceInfoFirmwareVersion FirmwareVersion { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoHardwareVersion * _Nonnull hardwareVersion;
		[Export ("hardwareVersion", ArgumentSemantic.Strong)]
		SettingDeviceInfoHardwareVersion HardwareVersion { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoUTCTime * _Nonnull utcTime;
		[Export ("utcTime", ArgumentSemantic.Strong)]
		SettingDeviceInfoUtcTime UtcTime { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoUptime * _Nonnull uptime;
		[Export ("uptime", ArgumentSemantic.Strong)]
		SettingDeviceInfoUptime Uptime { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoTags * _Nonnull tags;
		[Export ("tags", ArgumentSemantic.Strong)]
		SettingDeviceInfoTags Tags { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoGeoLocation * _Nonnull geoLocation;
		[Export ("geoLocation", ArgumentSemantic.Strong)]
		SettingDeviceInfoGeoLocation GeoLocation { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoName * _Nonnull name;
		[Export ("name", ArgumentSemantic.Strong)]
		SettingDeviceInfoName Name { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoDevelopmentMode * _Nonnull developmentMode;
		[Export ("developmentMode", ArgumentSemantic.Strong)]
		SettingDeviceInfoDevelopmentMode DevelopmentMode { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoIndoorLocationIdentifier * _Nonnull indoorLocationIdentifier;
		[Export ("indoorLocationIdentifier", ArgumentSemantic.Strong)]
		SettingDeviceInfoIndoorLocationIdentifier IndoorLocationIdentifier { get; }

		// @property (readonly, nonatomic, strong) ESTSettingDeviceInfoIndoorLocationName * _Nonnull indoorLocationName;
		[Export ("indoorLocationName", ArgumentSemantic.Strong)]
		SettingDeviceInfoIndoorLocationName IndoorLocationName { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsPower : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsPower")]
	interface SettingsPower
	{
		// @property (readonly, nonatomic, strong) ESTSettingPowerBatteryPercentage * _Nonnull batteryPercentage;
		[Export ("batteryPercentage", ArgumentSemantic.Strong)]
		SettingPowerBatteryPercentage BatteryPercentage { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerBatteryVoltage * _Nonnull batteryVoltage;
		[Export ("batteryVoltage", ArgumentSemantic.Strong)]
		SettingPowerBatteryVoltage BatteryVoltage { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerBatteryLifetime * _Nonnull batteryLifetime;
		[Export ("batteryLifetime", ArgumentSemantic.Strong)]
		SettingPowerBatteryLifetime BatteryLifetime { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerFlipToSleepEnable * _Nonnull flipToSleepEnable;
		[Export ("flipToSleepEnable", ArgumentSemantic.Strong)]
		SettingPowerFlipToSleepEnable FlipToSleepEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerSmartPowerModeEnable * _Nonnull smartPowerModeEnable;
		[Export ("smartPowerModeEnable", ArgumentSemantic.Strong)]
		SettingPowerSmartPowerModeEnable SmartPowerModeEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerDarkToSleepEnable * _Nonnull darkToSleepEnable;
		[Export ("darkToSleepEnable", ArgumentSemantic.Strong)]
		SettingPowerDarkToSleepEnable DarkToSleepEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerDarkToSleepThreshold * _Nonnull darkToSleepThreshold;
		[Export ("darkToSleepThreshold", ArgumentSemantic.Strong)]
		SettingPowerDarkToSleepThreshold DarkToSleepThreshold { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerMotionOnlyBroadcastingEnable * _Nonnull motionOnlyBroadcastingEnable;
		[Export ("motionOnlyBroadcastingEnable", ArgumentSemantic.Strong)]
		SettingPowerMotionOnlyBroadcastingEnable MotionOnlyBroadcastingEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerMotionOnlyBroadcastingDelay * _Nonnull motionOnlyBroadcastingDelay;
		[Export ("motionOnlyBroadcastingDelay", ArgumentSemantic.Strong)]
		SettingPowerMotionOnlyBroadcastingDelay MotionOnlyBroadcastingDelay { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerScheduledAdvertisingEnable * _Nonnull scheduledAdvertisingEnable;
		[Export ("scheduledAdvertisingEnable", ArgumentSemantic.Strong)]
		SettingPowerScheduledAdvertisingEnable ScheduledAdvertisingEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingPowerScheduledAdvertisingPeriod * _Nonnull scheduledAdvertisingPeriod;
		[Export ("scheduledAdvertisingPeriod", ArgumentSemantic.Strong)]
		SettingPowerScheduledAdvertisingPeriod ScheduledAdvertisingPeriod { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsGPIO : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsGPIO")]
	interface SettingsGpio
	{
		// @property (readonly, nonatomic, strong) ESTSettingGPIONotificationEnable * _Nonnull notificationEnable;
		[Export ("notificationEnable", ArgumentSemantic.Strong)]
		SettingGpioNotificationEnable NotificationEnable { get; }

		// @property (readonly, nonatomic, strong) ESTSettingGPIOConfigPort0 * _Nonnull configPort0;
		[Export ("configPort0", ArgumentSemantic.Strong)]
		SettingGpioConfigPort0 ConfigPort0 { get; }

		// @property (readonly, nonatomic, strong) ESTSettingGPIOConfigPort1 * _Nonnull configPort1;
		[Export ("configPort1", ArgumentSemantic.Strong)]
		SettingGpioConfigPort1 ConfigPort1 { get; }

		// @property (readonly, nonatomic, strong) ESTSettingGPIOPortsData * _Nonnull portsData;
		[Export ("portsData", ArgumentSemantic.Strong)]
		SettingGpioPortsData PortsData { get; }

		// @property (readonly, nonatomic, strong) ESTSettingGPIO0StateReflectingOnLEDEnable * _Nonnull gpio0StateReflectingOnLEDEnable;
		[Export ("gpio0StateReflectingOnLEDEnable", ArgumentSemantic.Strong)]
		SettingGpio0StateReflectingOnLedEnable Gpio0StateReflectingOnLedEnable { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// typedef void (^ESTSettingConnectivityIntervalCompletionBlock)(ESTSettingConnectivityInterval * _Nullable, NSError * _Nullable);
	delegate void SettingConnectivityIntervalCompletionBlock ([NullAllowed] SettingConnectivityInterval intervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingConnectivityInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingConnectivityInterval")]
	interface SettingConnectivityInterval : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(unsigned short)interval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort interval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingConnectivityIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingConnectivityIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)interval completion:(ESTSettingConnectivityIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort interval, SettingConnectivityIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)interval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort interval);
	}

	// @interface ESTSettingsConnectivity : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsConnectivity")]
	interface SettingsConnectivity
	{
		// @property (readonly, nonatomic, strong) ESTSettingConnectivityInterval * _Nonnull interval;
		[Export ("interval", ArgumentSemantic.Strong)]
		SettingConnectivityInterval Interval { get; }

		// @property (readonly, nonatomic, strong) ESTSettingConnectivityPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingConnectivityPower Power { get; }

		// @property (readonly, nonatomic, strong) ESTSettingShakeToConnectEnable * _Nonnull shakeToConnectEnable;
		[Export ("shakeToConnectEnable", ArgumentSemantic.Strong)]
		SettingShakeToConnectEnable ShakeToConnectEnable { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsSensors : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsSensors")]
	interface SettingsSensors
	{
		// @property (readonly, nonatomic, strong) ESTSettingSensorsAmbientLight * _Nonnull ambientLight;
		[Export ("ambientLight", ArgumentSemantic.Strong)]
		SettingSensorsAmbientLight AmbientLight { get; }

		// @property (readonly, nonatomic, strong) ESTSettingSensorsTemperature * _Nonnull temperature;
		[Export ("temperature", ArgumentSemantic.Strong)]
		SettingSensorsTemperature Temperature { get; }

		// @property (readonly, nonatomic, strong) ESTSettingSensorsPressure * _Nonnull pressure;
		[Export ("pressure", ArgumentSemantic.Strong)]
		SettingSensorsPressure Pressure { get; }

		// @property (readonly, nonatomic, strong) ESTSettingSensorsMotionNotificationEnable * _Nonnull motionNotificationEnable;
		[Export ("motionNotificationEnable", ArgumentSemantic.Strong)]
		SettingSensorsMotionNotificationEnable MotionNotificationEnable { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// @interface ESTSettingsEddystoneConfigurationService : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingsEddystoneConfigurationService")]
	interface SettingsEddystoneConfigurationService
	{
		// @property (readonly, nonatomic) ESTSettingEddystoneConfigurationServiceEnable * _Nonnull enabled;
		[Export ("enabled")]
		SettingEddystoneConfigurationServiceEnable Enabled { get; }

		// -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
		[Export ("initWithSettingsCollection:")]
		IntPtr Constructor (DeviceSettingsCollection settingsCollection);
	}

	// typedef void (^ESTDeviceSettingsManagerSyncCompletionBlock)(NSError * _Nullable);
	delegate void DeviceSettingsManagerSyncCompletionBlock ([NullAllowed] NSError error);

	// typedef void (^ESTDeviceSettingsManagerOperationsCompletionBlock)(NSError * _Nullable);
	delegate void DeviceSettingsManagerOperationsCompletionBlock ([NullAllowed] NSError error);

	// @interface ESTBeaconSettingsManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBeaconSettingsManager")]
	interface BeaconSettingsManager
	{
		// @property (readonly, nonatomic, strong) ESTDeviceSettingsCollection * _Nonnull settingsCollection;
		[Export ("settingsCollection", ArgumentSemantic.Strong)]
		DeviceSettingsCollection SettingsCollection { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsDeviceInfo * _Nonnull deviceInfo;
		[Export ("deviceInfo", ArgumentSemantic.Strong)]
		SettingsDeviceInfo DeviceInfo { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsPower * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		SettingsPower Power { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsConnectivity * _Nonnull connectivity;
		[Export ("connectivity", ArgumentSemantic.Strong)]
		SettingsConnectivity Connectivity { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsIBeacon * _Nonnull iBeacon;
		[Export ("iBeacon", ArgumentSemantic.Strong)]
		SettingsIBeacon IBeacon { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEstimoteLocation * _Nonnull estimoteLocation;
		[Export ("estimoteLocation", ArgumentSemantic.Strong)]
		SettingsEstimoteLocation EstimoteLocation { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEstimoteTLM * _Nonnull estimoteTLM;
		[Export ("estimoteTLM", ArgumentSemantic.Strong)]
		SettingsEstimoteTlm EstimoteTlm { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEddystoneUID * _Nonnull eddystoneUID;
		[Export ("eddystoneUID", ArgumentSemantic.Strong)]
		SettingsEddystoneUid EddystoneUid { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEddystoneURL * _Nonnull eddystoneURL;
		[Export ("eddystoneURL", ArgumentSemantic.Strong)]
		SettingsEddystoneUrl EddystoneUrl { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEddystoneTLM * _Nonnull eddystoneTLM;
		[Export ("eddystoneTLM", ArgumentSemantic.Strong)]
		SettingsEddystoneTlm EddystoneTlm { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEddystoneEID * _Nonnull eddystoneEID;
		[Export ("eddystoneEID", ArgumentSemantic.Strong)]
		SettingsEddystoneEid EddystoneEid { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsGPIO * _Nonnull GPIO;
		[Export ("GPIO", ArgumentSemantic.Strong)]
		SettingsGpio Gpio { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsSensors * _Nonnull sensors;
		[Export ("sensors", ArgumentSemantic.Strong)]
		SettingsSensors Sensors { get; }

		// @property (readonly, nonatomic, strong) ESTSettingsEddystoneConfigurationService * _Nonnull eddystoneConfigurationService;
		[Export ("eddystoneConfigurationService", ArgumentSemantic.Strong)]
		SettingsEddystoneConfigurationService EddystoneConfigurationService { get; }

		// -(void)performOperation:(id<ESTBeaconOperationProtocol> _Nonnull)operation;
		[Export ("performOperation:")]
		void PerformOperation (IBeaconOperationProtocol operation);

		// -(void)performOperations:(id<ESTBeaconOperationProtocol> _Nonnull)firstOperation, ...;
		[Internal]
		[Export ("performOperations:", IsVariadic = true)]
		void PerformOperations (IBeaconOperationProtocol firstOperation, IntPtr varArgs);

		// -(void)performOperationsFromArray:(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)operationsArray;
		[Export ("performOperationsFromArray:")]
		void PerformOperations (IBeaconOperationProtocol[] operationsArray);

		// -(void)performOperationsFromArray:(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)operationsArray completion:(ESTDeviceSettingsManagerOperationsCompletionBlock _Nullable)completion;
		[Export ("performOperationsFromArray:completion:"), Async]
		void PerformOperations (IBeaconOperationProtocol[] operationsArray, [NullAllowed] DeviceSettingsManagerOperationsCompletionBlock completion);

		// -(void)registerNotification:(id<ESTDeviceNotificationProtocol> _Nonnull)notification;
		[Export ("registerNotification:")]
		void RegisterNotification (IDeviceNotificationProtocol notification);

		// -(void)unregisterAllNotifications;
		[Export ("unregisterAllNotifications")]
		void UnregisterAllNotifications ();

		// Moved from ESTBeaconSettingsManager_Internal
		// -(instancetype _Nonnull)initWithDevice:(ESTDeviceLocationBeacon * _Nonnull)device peripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
		[Export ("initWithDevice:peripheral:")]
		IntPtr Constructor (DeviceLocationBeacon device, PeripheralTypeUtility peripheral);
	}

	// @interface Internal (ESTBeaconSettingsManager)
	[Category]
	[BaseType (typeof (BeaconSettingsManager))]
	interface ESTBeaconSettingsManager_Internal
	{
		// Moved to BeaconSettingsManager
		//// -(instancetype _Nonnull)initWithDevice:(ESTDeviceLocationBeacon * _Nonnull)device peripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
		//[Export ("initWithDevice:peripheral:")]
		//IntPtr Constructor (DeviceLocationBeacon device, PeripheralTypeUtility peripheral);

		// -(void)initializedOfflineSettingsWithCompletion:(ESTDeviceSettingsManagerSyncCompletionBlock _Nonnull)completion;
		[Export ("initializedOfflineSettingsWithCompletion:")]
		void InitializedOfflineSettings (DeviceSettingsManagerSyncCompletionBlock completion);

		// -(void)synchronizeSettingsWithCompletion:(ESTDeviceSettingsManagerSyncCompletionBlock _Nonnull)completion;
		[Export ("synchronizeSettingsWithCompletion:")]
		void SynchronizeSettings (DeviceSettingsManagerSyncCompletionBlock completion);

		// -(void)updatePeripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
		[Export ("updatePeripheral:")]
		void UpdatePeripheral (PeripheralTypeUtility peripheral);

		// -(void)updateSetting:(ESTSettingBase * _Nonnull)setting;
		[Export ("updateSetting:")]
		void UpdateSetting (SettingBase setting);
	}

	// @interface ESTStorageManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTStorageManager")]
	interface StorageManager
	{
		// @property (readonly, nonatomic, strong) NSDictionary * _Nonnull storageDictionary;
		[Export ("storageDictionary", ArgumentSemantic.Strong)]
		NSDictionary StorageDictionary { get; }

		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier peripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
		[Export ("initWithDeviceIdentifier:peripheral:")]
		IntPtr Constructor (string deviceIdentifier, PeripheralTypeUtility peripheral);

		// -(void)readStorageDictionaryWithCompletion:(ESTDictionaryCompletionBlock _Nonnull)completion;
		[Export ("readStorageDictionaryWithCompletion:"), Async]
		void ReadStorageDictionary (DictionaryCompletionBlock completion);

		// -(void)saveStorageDictionary:(NSDictionary * _Nonnull)dictionary withCompletion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("saveStorageDictionary:withCompletion:"), Async]
		void SaveStorageDictionary (NSDictionary dictionary, CompletionBlock completion);
	}

	// typedef void (^ESTReportScanVOCompletionBlock)(ESTMeshNearablesScanReportVO * _Nullable, NSError * _Nullable);
	delegate void ReportScanVOCompletionBlock ([NullAllowed] MeshNearablesScanReportVO scanReportVO, [NullAllowed] NSError error);

	// @interface ESTMeshScanReportsManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTMeshScanReportsManager")]
	[DisableDefaultCtor]
	interface MeshScanReportsManager
	{
		// -(void)readScanReportWithCompletion:(ESTReportScanVOCompletionBlock _Nonnull)completion;
		[Export ("readScanReportWithCompletion:"), Async]
		void ReadScanReport (ReportScanVOCompletionBlock completion);

		// Copied from ESTMeshScanReportsManager_Internal
		// -(instancetype _Nonnull)initWithPeripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
		[Export ("initWithPeripheral:")]
		IntPtr Constructor (PeripheralTypeUtility peripheral);
	}

	//// @interface Internal (ESTMeshScanReportsManager)
	//[Category]
	//[BaseType (typeof (MeshScanReportsManager))]
	//interface ESTMeshScanReportsManager_Internal
	//{
	//	Moved to MeshScanReportsManager
	//	// -(instancetype _Nonnull)initWithPeripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
	//	[Export ("initWithPeripheral:")]
	//	IntPtr Constructor (PeripheralTypeUtility peripheral);
	//}

	// @interface ESTDeviceLocationBeacon : ESTDeviceConnectable
	[DisableDefaultCtor]
	[BaseType (typeof (DeviceConnectable), Name = "ESTDeviceLocationBeacon")]
	interface DeviceLocationBeacon
	{
		// @property (readonly, nonatomic, strong) ESTBeaconSettingsManager * _Nullable settings;
		[NullAllowed, Export ("settings", ArgumentSemantic.Strong)]
		BeaconSettingsManager Settings { get; }

		// @property (nonatomic, strong) ESTStorageManager * _Nullable storage;
		[NullAllowed, Export ("storage", ArgumentSemantic.Strong)]
		StorageManager Storage { get; set; }

		// @property (nonatomic, strong) ESTMeshScanReportsManager * _Nullable scanReports;
		[NullAllowed, Export ("scanReports", ArgumentSemantic.Strong)]
		MeshScanReportsManager ScanReports { get; set; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull isShaken;
		[Export ("isShaken", ArgumentSemantic.Strong)]
		NSNumber IsShaken { get; }

		// -(void)connectForStorageRead;
		[Export ("connectForStorageRead")]
		void ConnectForStorageRead ();

		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)identifier peripheralIdentifier:(NSUUID * _Nonnull)peripheralIdentifier rssi:(NSInteger)rssi discoveryDate:(NSDate * _Nonnull)discoveryDate isShaken:(NSNumber * _Nonnull)isShaken;
		[Export ("initWithDeviceIdentifier:peripheralIdentifier:rssi:discoveryDate:isShaken:")]
		IntPtr Constructor (string identifier, NSUuid peripheralIdentifier, nint rssi, NSDate discoveryDate, NSNumber isShaken);
	}

	// @interface ESTTelemetryInfo : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryInfo")]
	interface TelemetryInfo
	{
		// @property (readonly, nonatomic, strong) NSString * shortIdentifier;
		[Export ("shortIdentifier", ArgumentSemantic.Strong)]
		string ShortIdentifier { get; }

		// -(instancetype)initWithShortIdentifier:(NSString *)shortIdentifier;
		[Export ("initWithShortIdentifier:")]
		IntPtr Constructor (string shortIdentifier);
	}

	interface ITelemetryNotificationProtocol { }

	// @protocol ESTTelemetryNotificationProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationProtocol")]
	interface TelemetryNotificationProtocol
	{
		// @required -(void)fireNotificationBlockWithTelemetryInfo:(ESTTelemetryInfo * _Nonnull)info;
		[Abstract]
		[Export ("fireNotificationBlockWithTelemetryInfo:")]
		void FireNotificationBlock (TelemetryInfo info);

		// @required -(Class _Nonnull)getInfoClass;
		[Abstract]
		[Export ("getInfoClass")]
		Class InfoClass { get; }
	}

	interface IDeviceManagerDelegate{ }

	// @protocol ESTDeviceManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTDeviceManagerDelegate")]
	interface DeviceManagerDelegate
	{
		// @optional -(void)deviceManager:(ESTDeviceManager * _Nonnull)manager didDiscoverDevices:(NSArray<ESTDevice *> * _Nonnull)devices;
		[Export ("deviceManager:didDiscoverDevices:"), EventArgs ("DeviceManagerDiscoveredDevices")]
		void DiscoveredDevices (DeviceManager manager, Device[] devices);

		// @optional -(void)deviceManagerDidFailDiscovery:(ESTDeviceManager * _Nonnull)manager;
		[Export ("deviceManagerDidFailDiscovery:"), EventArgs ("DeviceManagerDiscoveryFailed")]
		void DiscoveryFailed (DeviceManager manager);
	}

	// @interface ESTDeviceManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTDeviceManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (DeviceManagerDelegate) })]
	interface DeviceManager
	{
		// @property (readonly, assign, nonatomic) BOOL isScanning;
		[Export ("isScanning")]
		bool IsScanning { get; }

		// @property (nonatomic, weak) id<ESTDeviceManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IDeviceManagerDelegate Delegate { get; set; }

		// -(void)startDeviceDiscoveryWithFilter:(id<ESTDeviceFilter> _Nonnull)filter;
		[Export ("startDeviceDiscoveryWithFilter:")]
		void StartDeviceDiscovery (IDeviceFilter filter);

		// -(void)stopDeviceDiscovery;
		[Export ("stopDeviceDiscovery")]
		void StopDeviceDiscovery ();

		// -(void)registerForTelemetryNotifications:(NSArray<ESTTelemetryNotificationProtocol> * _Nonnull)infos;
		[Export ("registerForTelemetryNotifications:")]
		void RegisterForTelemetryNotifications (ITelemetryNotificationProtocol[] infos);

		// -(void)registerForTelemetryNotification:(id<ESTTelemetryNotificationProtocol> _Nonnull)info;
		[Export ("registerForTelemetryNotification:")]
		void Register (ITelemetryNotificationProtocol info);

		// -(void)unregisterForTelemetryNotification:(id<ESTTelemetryNotificationProtocol> _Nonnull)info;
		[Export ("unregisterForTelemetryNotification:")]
		void Unregister (ITelemetryNotificationProtocol info);
	}

	interface IDeviceFilter { }

	// @protocol ESTDeviceFilter <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTDeviceFilter")]
	interface DeviceFilter
	{
		// @required @property (readonly, nonatomic, strong) NSPredicate * _Nonnull devicesPredicate;
		[Abstract]
		[Export ("devicesPredicate", ArgumentSemantic.Strong)]
		NSPredicate DevicesPredicate { get; }

		// @required -(NSArray<Class> * _Nonnull)getScanInfoClasses;
		[Abstract]
		[Export ("getScanInfoClasses")]
		Class[] ScanInfoClasses { get; }
	}

	// @interface ESTDeviceFilterBeaconV1 : NSObject <ESTDeviceFilter>
	[BaseType (typeof (NSObject), Name = "ESTDeviceFilterBeaconV1")]
	interface DeviceFilterBeaconV1 : DeviceFilter
	{
		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("initWithIdentifier:")]
		IntPtr Constructor (string identifier);
	}

	// @interface ESTDeviceFilterLocationBeacon : NSObject <ESTDeviceFilter>
	[BaseType (typeof (NSObject), Name = "ESTDeviceFilterLocationBeacon")]
	interface DeviceFilterLocationBeacon : DeviceFilter
	{
		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("initWithIdentifier:")]
		IntPtr Constructor (string identifier);

		// -(instancetype _Nonnull)initWithIdentifiers:(NSArray<NSString *> * _Nonnull)identifiers;
		[Export ("initWithIdentifiers:")]
		IntPtr Constructor (string[] identifiers);
	}

	// @interface ESTDeviceFilterNearable : NSObject <ESTDeviceFilter>
	[BaseType (typeof (NSObject), Name = "ESTDeviceFilterNearable")]
	interface DeviceFilterNearable : DeviceFilter
	{
		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("initWithIdentifier:")]
		IntPtr Constructor (string identifier);
	}

	// @interface ESTLogger : NSObject
	[BaseType (typeof (NSObject), Name = "ESTLogger")]
	interface Logger
	{
		// +(void)setConsoleLogLevel:(ESTLogLevel)level;
		[Static]
		[Export ("setConsoleLogLevel:")]
		void SetConsoleLogLevel (LogLevel level);

		// +(void)setCacheLogLevel:(ESTLogLevel)level;
		[Static]
		[Export ("setCacheLogLevel:")]
		void SetCacheLogLevel (LogLevel level);

		// +(NSString *)getLogCache;
		[Static]
		[Export ("getLogCache")]
		string LogCache { get; }

		// +(void)clearLogCache;
		[Static]
		[Export ("clearLogCache")]
		void ClearLogCache ();

		// +(void)log:(NSString *)message withLevel:(ESTLogLevel)level;
		[Static]
		[Export ("log:withLevel:")]
		void Log (string message, LogLevel level);

		// +(void)dumpLogCacheToFile;
		[Static]
		[Export ("dumpLogCacheToFile")]
		void DumpLogCacheToFile ();
	}

	// @interface ESTBeacon : NSObject <NSCopying, NSSecureCoding>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTBeacon")]
	interface Beacon : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic, strong) NSUUID * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		NSUuid ProximityUuid { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) CLProximity proximity;
		[Export ("proximity")]
		CLProximity Proximity { get; }

		// @property (readonly, nonatomic) CLLocationAccuracy accuracy;
		[Export ("accuracy")]
		double Accuracy { get; }

		// @property (readonly, nonatomic) NSInteger rssi;
		[Export ("rssi")]
		nint Rssi { get; }

		// -(instancetype _Nonnull)initWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor proximity:(CLProximity)proximity accuracy:(CLLocationAccuracy)accuracy rssi:(NSInteger)rssi;
		[Export ("initWithProximityUUID:major:minor:proximity:accuracy:rssi:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor, CLProximity proximity, double accuracy, nint rssi);
	}

	interface IBeaconManagerDelegate { }

	// @protocol ESTBeaconManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTBeaconManagerDelegate")]
	interface BeaconManagerDelegate
	{
		// @optional -(void)beaconManager:(id _Nonnull)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
		[Export ("beaconManager:didChangeAuthorizationStatus:"), EventArgs ("BeaconManagerAuthorizationStatusChanged")]
		void AuthorizationStatusChanged (NSObject manager, CLAuthorizationStatus status);

		// @optional -(void)beaconManager:(id _Nonnull)manager didStartMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didStartMonitoringForRegion:"), EventArgs ("BeaconManagerMonitoringStarted")]
		void MonitoringStarted (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager monitoringDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
		[Export ("beaconManager:monitoringDidFailForRegion:withError:"), EventArgs ("BeaconManagerMonitoringFailed")]
		void MonitoringFailed (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

		// @optional -(void)beaconManager:(id _Nonnull)manager didEnterRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didEnterRegion:"), EventArgs ("BeaconManagerEnteredRegion")]
		void EnteredRegion (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didExitRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didExitRegion:"), EventArgs ("BeaconManagerExitedRegion")]
		void ExitedRegion (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didDetermineState:(CLRegionState)state forRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didDetermineState:forRegion:"), EventArgs ("BeaconManagerDeterminedState")]
		void DeterminedState (NSObject manager, CLRegionState state, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didRangeBeacons:(NSArray<CLBeacon *> * _Nonnull)beacons inRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didRangeBeacons:inRegion:"), EventArgs ("BeaconManagerRangedBeacons")]
		void RangedBeacons (NSObject manager, CLBeacon[] beacons, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager rangingBeaconsDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
		[Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:"), EventArgs ("BeaconManagerRangingBeaconsFailed")]
		void RangingBeaconsFailed (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

		// @optional -(void)beaconManagerDidStartAdvertising:(id _Nonnull)manager error:(NSError * _Nullable)error;
		[Export ("beaconManagerDidStartAdvertising:error:"), EventArgs ("BeaconManagerStartedAdvertising")]
		void StartedAdvertising (NSObject manager, [NullAllowed] NSError error);

		// @optional -(void)beaconManager:(id _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
		[Export ("beaconManager:didFailWithError:"), EventArgs ("BeaconManagerFailed")]
		void Failed (NSObject manager, NSError error);
	}

	// @interface ESTBeaconManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBeaconManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconManagerDelegate) })]
	interface BeaconManager
	{
		// @property (nonatomic, weak) id<ESTBeaconManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBeaconManagerDelegate Delegate { get; set; }

		// @property (nonatomic) NSInteger preventUnknownUpdateCount;
		[Export ("preventUnknownUpdateCount")]
		nint PreventUnknownUpdateCount { get; set; }

		// @property (nonatomic) BOOL avoidUnknownStateBeacons;
		[Export ("avoidUnknownStateBeacons")]
		bool AvoidUnknownStateBeacons { get; set; }

		// @property (nonatomic) BOOL returnAllRangedBeaconsAtOnce;
		[Export ("returnAllRangedBeaconsAtOnce")]
		bool ReturnAllRangedBeaconsAtOnce { get; set; }

		// -(void)updateRangeLimit:(NSInteger)limit;
		[Export ("updateRangeLimit:")]
		void UpdateRangeLimit (nint limit);

		// -(void)startAdvertisingWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor identifier:(NSString * _Nonnull)identifier;
		[Export ("startAdvertisingWithProximityUUID:major:minor:identifier:")]
		void StartAdvertising (NSUuid proximityUUID, ushort major, ushort minor, string identifier);

		// -(void)stopAdvertising;
		[Export ("stopAdvertising")]
		void StopAdvertising ();

		// +(CLAuthorizationStatus)authorizationStatus;
		[Static]
		[Export ("authorizationStatus")]
		CLAuthorizationStatus AuthorizationStatus { get; }

		// -(void)requestWhenInUseAuthorization;
		[Export ("requestWhenInUseAuthorization")]
		void RequestWhenInUseAuthorization ();

		// -(void)requestAlwaysAuthorization;
		[Export ("requestAlwaysAuthorization")]
		void RequestAlwaysAuthorization ();

		// -(BOOL)isAuthorizedForRanging;
		[Export ("isAuthorizedForRanging")]
		bool IsAuthorizedForRanging { get; }

		// -(BOOL)isAuthorizedForMonitoring;
		[Export ("isAuthorizedForMonitoring")]
		bool IsAuthorizedForMonitoring { get; }

		// -(void)startMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("startMonitoringForRegion:")]
		void StartMonitoring (CLBeaconRegion region);

		// -(void)stopMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("stopMonitoringForRegion:")]
		void StopMonitoring (CLBeaconRegion region);

		// -(void)stopMonitoringForAllRegions;
		[Export ("stopMonitoringForAllRegions")]
		void StopMonitoringForAllRegions ();

		// -(void)startRangingBeaconsInRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("startRangingBeaconsInRegion:")]
		void StartRangingBeacons (CLBeaconRegion region);

		// -(void)stopRangingBeaconsInRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("stopRangingBeaconsInRegion:")]
		void StopRangingBeacons (CLBeaconRegion region);

		// -(void)stopRangingBeaconsInAllRegions;
		[Export ("stopRangingBeaconsInAllRegions")]
		void StopRangingBeaconsInAllRegions ();

		// -(void)requestStateForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("requestStateForRegion:")]
		void RequestState (CLBeaconRegion region);

		// @property (readonly, copy, nonatomic) NSSet * _Nonnull monitoredRegions;
		[Export ("monitoredRegions", ArgumentSemantic.Copy)]
		NSSet MonitoredRegions { get; }

		// @property (readonly, copy, nonatomic) NSSet * _Nonnull rangedRegions;
		[Export ("rangedRegions", ArgumentSemantic.Copy)]
		NSSet RangedRegions { get; }

		// +(NSUUID * _Nonnull)motionProximityUUIDForProximityUUID:(NSUUID * _Nonnull)proximityUUID;
		[Static]
		[Export ("motionProximityUUIDForProximityUUID:")]
		NSUuid GetMotionProximityUuid (NSUuid proximityUUID);
	}

	interface ISecureBeaconManagerDelegate { }

	// @protocol ESTSecureBeaconManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTSecureBeaconManagerDelegate")]
	interface SecureBeaconManagerDelegate
	{
		// @optional -(void)beaconManager:(id _Nonnull)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
		[Export ("beaconManager:didChangeAuthorizationStatus:"), EventArgs ("SecureBeaconManagerAuthorizationStatusChanged")]
		void AuthorizationStatusChanged (NSObject manager, CLAuthorizationStatus status);

		// @optional -(void)beaconManager:(id _Nonnull)manager didStartMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didStartMonitoringForRegion:"), EventArgs ("SecureBeaconManagerMonitoringStarted")]
		void MonitoringStarted (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager monitoringDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
		[Export ("beaconManager:monitoringDidFailForRegion:withError:"), EventArgs ("SecureBeaconManagerMonitoringFailed")]
		void MonitoringFailed (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

		// @optional -(void)beaconManager:(id _Nonnull)manager didEnterRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didEnterRegion:"), EventArgs ("SecureBeaconManagerEnteredRegion")]
		void EnteredRegion (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didExitRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didExitRegion:"), EventArgs ("SecureBeaconManagerExitedRegion")]
		void ExitedRegion (NSObject manager, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didDetermineState:(CLRegionState)state forRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didDetermineState:forRegion:"), EventArgs ("SecureBeaconManagerDeterminedState")]
		void DeterminedState (NSObject manager, CLRegionState state, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager didRangeBeacons:(NSArray<ESTBeacon *> * _Nonnull)beacons inRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("beaconManager:didRangeBeacons:inRegion:"), EventArgs ("SecureBeaconManagerRangedBeacons")]
		void RangedBeacons (NSObject manager, Beacon[] beacons, CLBeaconRegion region);

		// @optional -(void)beaconManager:(id _Nonnull)manager rangingBeaconsDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
		[Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:"), EventArgs ("SecureBeaconManagerRangingBeaconsFailed")]
		void RangingBeaconsFailed (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

		// @optional -(void)beaconManager:(id _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
		[Export ("beaconManager:didFailWithError:"), EventArgs ("SecureBeaconManagerFailed")]
		void Failed (NSObject manager, NSError error);
	}

	// @interface ESTSecureBeaconManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTSecureBeaconManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (SecureBeaconManagerDelegate) })]
	interface SecureBeaconManager
	{
		// @property (nonatomic, weak) id<ESTSecureBeaconManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ISecureBeaconManagerDelegate Delegate { get; set; }

		// +(CLAuthorizationStatus)authorizationStatus;
		[Static]
		[Export ("authorizationStatus")]
		CLAuthorizationStatus AuthorizationStatus { get; }

		// -(void)requestWhenInUseAuthorization;
		[Export ("requestWhenInUseAuthorization")]
		void RequestWhenInUseAuthorization ();

		// -(void)requestAlwaysAuthorization;
		[Export ("requestAlwaysAuthorization")]
		void RequestAlwaysAuthorization ();

		// -(void)startMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("startMonitoringForRegion:")]
		void StartMonitoring (CLBeaconRegion region);

		// -(void)stopMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("stopMonitoringForRegion:")]
		void StopMonitoring (CLBeaconRegion region);

		// -(void)startRangingBeaconsInRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("startRangingBeaconsInRegion:")]
		void StartRangingBeacons (CLBeaconRegion region);

		// -(void)stopRangingBeaconsInRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("stopRangingBeaconsInRegion:")]
		void StopRangingBeacons (CLBeaconRegion region);

		// -(void)requestStateForRegion:(CLBeaconRegion * _Nonnull)region;
		[Export ("requestStateForRegion:")]
		void RequestState (CLBeaconRegion region);

		// @property (readonly, copy, nonatomic) NSSet * _Nonnull monitoredRegions;
		[Export ("monitoredRegions", ArgumentSemantic.Copy)]
		NSSet MonitoredRegions { get; }

		// @property (readonly, copy, nonatomic) NSSet * _Nonnull rangedRegions;
		[Export ("rangedRegions", ArgumentSemantic.Copy)]
		NSSet RangedRegions { get; }
	}

	// typedef void (^ESTPowerCompletionBlock)(ESTBeaconPower, NSError * _Nullable);
	delegate void PowerCompletionBlock (BeaconPower value, [NullAllowed] NSError error);

	// @interface ESTBeaconDefinitions : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBeaconDefinitions")]
	interface BeaconDefinitions
	{
	}

	// @interface ESTFirmwareInfoVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTFirmwareInfoVO")]
	interface FirmwareInfoVO
	{
		// @property (nonatomic, strong) NSString * _Nullable hardwareVersion;
		[NullAllowed, Export ("hardwareVersion", ArgumentSemantic.Strong)]
		string HardwareVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable firmwareVersion;
		[NullAllowed, Export ("firmwareVersion", ArgumentSemantic.Strong)]
		string FirmwareVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable changelog;
		[NullAllowed, Export ("changelog", ArgumentSemantic.Strong)]
		string Changelog { get; set; }

		// @property (assign, nonatomic) BOOL isUpdateAvailable;
		[Export ("isUpdateAvailable")]
		bool IsUpdateAvailable { get; set; }
	}

	// @interface ESTBeaconVO : NSObject <NSCoding>
	[BaseType (typeof (NSObject), Name = "ESTBeaconVO")]
	interface BeaconVO : INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		string ProximityUuid { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull macAddress;
		[Export ("macAddress", ArgumentSemantic.Strong)]
		string MacAddress { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull publicIdentifier;
		[Export ("publicIdentifier", ArgumentSemantic.Strong)]
		string PublicIdentifier { get; set; }

		// @property (assign, nonatomic) ESTBroadcastingScheme broadcastingScheme;
		[Export ("broadcastingScheme", ArgumentSemantic.Assign)]
		BroadcastingScheme BroadcastingScheme { get; set; }

		// @property (assign, nonatomic) ESTBeaconMotionUUID motionUUIDState;
		[Export ("motionUUIDState", ArgumentSemantic.Assign)]
		BeaconMotionUuid MotionUuidState { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable name;
		[NullAllowed, Export ("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable batteryLifeExpectancy;
		[NullAllowed, Export ("batteryLifeExpectancy", ArgumentSemantic.Strong)]
		NSNumber BatteryLifeExpectancy { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable hardware;
		[NullAllowed, Export ("hardware", ArgumentSemantic.Strong)]
		string Hardware { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable firmware;
		[NullAllowed, Export ("firmware", ArgumentSemantic.Strong)]
		string Firmware { get; set; }

		// @property (assign, nonatomic) ESTBeaconPower power;
		[Export ("power", ArgumentSemantic.Assign)]
		BeaconPower Power { get; set; }

		// @property (assign, nonatomic) NSInteger advInterval;
		[Export ("advInterval")]
		nint AdvInterval { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable basicPowerMode;
		[NullAllowed, Export ("basicPowerMode", ArgumentSemantic.Strong)]
		NSNumber BasicPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable smartPowerMode;
		[NullAllowed, Export ("smartPowerMode", ArgumentSemantic.Strong)]
		NSNumber SmartPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable batteryLevel;
		[NullAllowed, Export ("batteryLevel", ArgumentSemantic.Strong)]
		NSNumber BatteryLevel { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable latitude;
		[NullAllowed, Export ("latitude", ArgumentSemantic.Strong)]
		NSNumber Latitude { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable longitude;
		[NullAllowed, Export ("longitude", ArgumentSemantic.Strong)]
		NSNumber Longitude { get; set; }

		// @property (nonatomic, strong) NSDictionary * _Nullable location;
		[NullAllowed, Export ("location", ArgumentSemantic.Strong)]
		NSDictionary Location { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable city;
		[NullAllowed, Export ("city", ArgumentSemantic.Strong)]
		string City { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable country;
		[NullAllowed, Export ("country", ArgumentSemantic.Strong)]
		string Country { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable formattedAddress;
		[NullAllowed, Export ("formattedAddress", ArgumentSemantic.Strong)]
		string FormattedAddress { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable stateName;
		[NullAllowed, Export ("stateName", ArgumentSemantic.Strong)]
		string StateName { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable stateCode;
		[NullAllowed, Export ("stateCode", ArgumentSemantic.Strong)]
		string StateCode { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable streetName;
		[NullAllowed, Export ("streetName", ArgumentSemantic.Strong)]
		string StreetName { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable streetNumber;
		[NullAllowed, Export ("streetNumber", ArgumentSemantic.Strong)]
		string StreetNumber { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable zipCode;
		[NullAllowed, Export ("zipCode", ArgumentSemantic.Strong)]
		string ZipCode { get; set; }

		// @property (nonatomic, strong) NSSet<NSString *> * _Nullable tags;
		[NullAllowed, Export ("tags", ArgumentSemantic.Strong)]
		NSSet<NSString> Tags { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable indoorLocationIdentifier;
		[NullAllowed, Export ("indoorLocationIdentifier", ArgumentSemantic.Strong)]
		string IndoorLocationIdentifier { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable indoorLocationName;
		[NullAllowed, Export ("indoorLocationName", ArgumentSemantic.Strong)]
		string IndoorLocationName { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable eddystoneNamespaceID;
		[NullAllowed, Export ("eddystoneNamespaceID", ArgumentSemantic.Strong)]
		string EddystoneNamespaceId { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable eddystoneInstanceID;
		[NullAllowed, Export ("eddystoneInstanceID", ArgumentSemantic.Strong)]
		string EddystoneInstanceId { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable eddystoneURL;
		[NullAllowed, Export ("eddystoneURL", ArgumentSemantic.Strong)]
		string EddystoneUrl { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable motionDetection;
		[NullAllowed, Export ("motionDetection", ArgumentSemantic.Strong)]
		NSNumber MotionDetection { get; set; }

		// @property (assign, nonatomic) ESTBeaconConditionalBroadcasting conditionalBroadcasting;
		[Export ("conditionalBroadcasting", ArgumentSemantic.Assign)]
		BeaconConditionalBroadcasting ConditionalBroadcasting { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable security;
		[NullAllowed, Export ("security", ArgumentSemantic.Strong)]
		NSNumber Security { get; set; }

		// @property (assign, nonatomic) BOOL isSecured __attribute__((deprecated("Starting from SDK 3.7.0 use security property instead")));
		[Obsolete ("Starting from SDK 3.7.0 use Security property instead.")]
		[Export ("isSecured")]
		bool IsSecured { get; set; }

		// @property (assign, nonatomic) ESTColor color;
		[Export ("color", ArgumentSemantic.Assign)]
		Color Color { get; set; }

		// -(instancetype _Nonnull)initWithCloudData:(NSDictionary * _Nonnull)data;
		[Export ("initWithCloudData:")]
		IntPtr Constructor (NSDictionary data);
	}

	interface IBeaconConnectionDelegate { }

	// @protocol ESTBeaconConnectionDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTBeaconConnectionDelegate")]
	interface BeaconConnectionDelegate
	{
		// @optional -(void)beaconConnection:(ESTBeaconConnection * _Nonnull)connection didVerifyWithData:(ESTBeaconVO * _Nullable)data error:(NSError * _Nullable)error;
		[Export ("beaconConnection:didVerifyWithData:error:"), EventArgs ("BeaconConnectionVerified")]
		void Verified (BeaconConnection connection, [NullAllowed] BeaconVO data, [NullAllowed] NSError error);

		// @optional -(void)beaconConnectionDidSucceed:(ESTBeaconConnection * _Nonnull)connection;
		[Export ("beaconConnectionDidSucceed:"), EventArgs ("BeaconConnectionSucceeded")]
		void Succeeded (BeaconConnection connection);

		// @optional -(void)beaconConnection:(ESTBeaconConnection * _Nonnull)connection didFailWithError:(NSError * _Nonnull)error;
		[Export ("beaconConnection:didFailWithError:"), EventArgs ("BeaconConnectionFailed")]
		void Failed (BeaconConnection connection, NSError error);

		// @optional -(void)beaconConnection:(ESTBeaconConnection * _Nonnull)connection didDisconnectWithError:(NSError * _Nullable)error;
		[Export ("beaconConnection:didDisconnectWithError:"), EventArgs ("BeaconConnectionDisconnected")]
		void Disconnected (BeaconConnection connection, [NullAllowed] NSError error);

		// @optional -(void)beaconConnection:(ESTBeaconConnection * _Nonnull)connection motionStateChanged:(ESTBeaconMotionState)state;
		[Export ("beaconConnection:motionStateChanged:"), EventArgs ("BeaconConnectionMotionStateChanged")]
		void MotionStateChanged (BeaconConnection connection, BeaconMotionState state);

		// @optional -(void)beaconConnection:(ESTBeaconConnection * _Nonnull)connection didUpdateRSSI:(NSNumber * _Nonnull)rssi;
		[Export ("beaconConnection:didUpdateRSSI:"), EventArgs ("BeaconConnectionUpdatedRssi")]
		void UpdatedRssi (BeaconConnection connection, NSNumber rssi);
	}

	// @interface ESTBeaconConnection : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBeaconConnection", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconConnectionDelegate) })]
	interface BeaconConnection
	{
		// @property (nonatomic, weak) id<ESTBeaconConnectionDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBeaconConnectionDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, nonatomic) ESTConnectionStatus connectionStatus;
		[Export ("connectionStatus")]
		ConnectionStatus ConnectionStatus { get; }

		// +(instancetype _Nonnull)connectionWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate;
		[Static]
		[Export ("connectionWithProximityUUID:major:minor:delegate:")]
		BeaconConnection CreateConnection (NSUuid proximityUUID, ushort major, ushort minor, [NullAllowed] IBeaconConnectionDelegate @delegate);

		// +(instancetype _Nonnull)connectionWithBeacon:(CLBeacon * _Nonnull)beacon delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate;
		[Static]
		[Export ("connectionWithBeacon:delegate:")]
		BeaconConnection CreateConnection (CLBeacon beacon, [NullAllowed] IBeaconConnectionDelegate @delegate);

		// +(instancetype _Nonnull)connectionWithMacAddress:(NSString * _Nonnull)macAddress delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate __attribute__((deprecated("Starting from SDK 4.0.0-beta1 macAddress is deprecated. Use initWithIdentifier constructor")));
		[Obsolete ("Starting from SDK 4.0.0-beta1 macAddress is deprecated. Use CreateConnectionWithIdentifier static method instead.")]
		[Static]
		[Export ("connectionWithMacAddress:delegate:")]
		BeaconConnection CreateConnectionWithMacAddress (string macAddress, [NullAllowed] IBeaconConnectionDelegate @delegate);

		// +(instancetype _Nonnull)connectionWithIdentifier:(NSString * _Nonnull)identifier delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate;
		[Static]
		[Export ("connectionWithIdentifier:delegate:")]
		BeaconConnection CreateConnectionWithIdentifier (string identifier, [NullAllowed] IBeaconConnectionDelegate @delegate);

		// -(instancetype _Nonnull)initWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate startImmediately:(BOOL)startImmediately;
		[Export ("initWithProximityUUID:major:minor:delegate:startImmediately:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor, [NullAllowed] IBeaconConnectionDelegate @delegate, bool startImmediately);

		// -(instancetype _Nonnull)initWithBeacon:(CLBeacon * _Nonnull)beacon delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate startImmediately:(BOOL)startImmediately;
		[Export ("initWithBeacon:delegate:startImmediately:")]
		IntPtr Constructor (CLBeacon beacon, [NullAllowed] IBeaconConnectionDelegate @delegate, bool startImmediately);

		// There is a conflict in Constructors, but this is already deprecated.
		//// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate startImmediately:(BOOL)startImmediately __attribute__((deprecated("Starting from SDK 3.7.0 macAddress is deprecated. Use initWithIdentifier constructor")));
		//[Export ("initWithMacAddress:delegate:startImmediately:")]
		//IntPtr Constructor (string macAddress, [NullAllowed] IBeaconConnectionDelegate @delegate, bool startImmediately);

		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier delegate:(id<ESTBeaconConnectionDelegate> _Nullable)delegate startImmediately:(BOOL)startImmediately;
		[Export ("initWithIdentifier:delegate:startImmediately:")]
		IntPtr Constructor (string identifier, [NullAllowed] IBeaconConnectionDelegate @delegate, bool startImmediately);

		// -(void)startConnection;
		[Export ("startConnection")]
		void StartConnection ();

		// -(void)startConnectionWithAttempts:(NSInteger)attempts connectionTimeout:(NSInteger)timeout;
		[Export ("startConnectionWithAttempts:connectionTimeout:")]
		void StartConnection (nint attempts, nint timeout);

		// -(void)cancelConnection;
		[Export ("cancelConnection")]
		void CancelConnection ();

		// -(void)disconnect;
		[Export ("disconnect")]
		void Disconnect ();

		// @property (readonly, nonatomic) NSString * _Nullable macAddress;
		[NullAllowed, Export ("macAddress")]
		string MacAddress { get; }

		// @property (readonly, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable location;
		[NullAllowed, Export ("location")]
		NSDictionary Location { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable latitude;
		[NullAllowed, Export ("latitude")]
		NSNumber Latitude { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable longitude;
		[NullAllowed, Export ("longitude")]
		NSNumber Longitude { get; }

		// @property (readonly, nonatomic) NSString * _Nullable indoorLocationIdentifier;
		[NullAllowed, Export ("indoorLocationIdentifier")]
		string IndoorLocationIdentifier { get; }

		// @property (readonly, nonatomic) NSString * _Nullable indoorLocationName;
		[NullAllowed, Export ("indoorLocationName")]
		string IndoorLocationName { get; }

		// @property (readonly, nonatomic) ESTColor color;
		[Export ("color")]
		Color Color { get; }

		// @property (readonly, nonatomic) CBPeripheral * _Nullable peripheral __attribute__((deprecated("CBPeripheral peripheral property is deprecated since 3.7.0 version")));
		[Obsolete ("This property is deprecated since 3.7.0 version")]
		[NullAllowed, Export ("peripheral")]
		CBPeripheral Peripheral { get; }

		// @property (readonly, nonatomic) ESTBroadcastingScheme broadcastingScheme;
		[Export ("broadcastingScheme")]
		BroadcastingScheme BroadcastingScheme { get; }

		// @property (readonly, nonatomic) NSUUID * _Nullable proximityUUID;
		[NullAllowed, Export ("proximityUUID")]
		NSUuid ProximityUuid { get; }

		// @property (readonly, nonatomic) NSUUID * _Nullable motionProximityUUID;
		[NullAllowed, Export ("motionProximityUUID")]
		NSUuid MotionProximityUuid { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable major;
		[NullAllowed, Export ("major")]
		NSNumber Major { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable minor;
		[NullAllowed, Export ("minor")]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable power;
		[NullAllowed, Export ("power")]
		NSNumber Power { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable advInterval;
		[NullAllowed, Export ("advInterval")]
		NSNumber AdvInterval { get; }

		// @property (readonly, nonatomic) NSString * _Nullable eddystoneNamespace;
		[NullAllowed, Export ("eddystoneNamespace")]
		string EddystoneNamespace { get; }

		// @property (readonly, nonatomic) NSString * _Nullable eddystoneInstance;
		[NullAllowed, Export ("eddystoneInstance")]
		string EddystoneInstance { get; }

		// @property (readonly, nonatomic) NSString * _Nullable eddystoneURL;
		[NullAllowed, Export ("eddystoneURL")]
		string EddystoneUrl { get; }

		// @property (readonly, nonatomic) NSString * _Nullable hardwareVersion;
		[NullAllowed, Export ("hardwareVersion")]
		string HardwareVersion { get; }

		// @property (readonly, nonatomic) NSString * _Nullable firmwareVersion;
		[NullAllowed, Export ("firmwareVersion")]
		string FirmwareVersion { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable rssi;
		[NullAllowed, Export ("rssi")]
		NSNumber Rssi { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable batteryLevel;
		[NullAllowed, Export ("batteryLevel")]
		NSNumber BatteryLevel { get; }

		// @property (readonly, nonatomic) ESTBeaconBatteryType batteryType;
		[Export ("batteryType")]
		BeaconBatteryType BatteryType { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable remainingLifetime;
		[NullAllowed, Export ("remainingLifetime")]
		NSNumber RemainingLifetime { get; }

		// @property (readonly, nonatomic) ESTBeaconPowerSavingMode basicPowerMode;
		[Export ("basicPowerMode")]
		BeaconPowerSavingMode BasicPowerMode { get; }

		// @property (readonly, nonatomic) ESTBeaconPowerSavingMode smartPowerMode;
		[Export ("smartPowerMode")]
		BeaconPowerSavingMode SmartPowerMode { get; }

		// @property (readonly, nonatomic) ESTBeaconEstimoteSecureUUID estimoteSecureUUIDState;
		[Export ("estimoteSecureUUIDState")]
		BeaconEstimoteSecureUuid EstimoteSecureUuidState { get; }

		// @property (readonly, nonatomic) ESTBeaconMotionUUID motionUUIDState;
		[Export ("motionUUIDState")]
		BeaconMotionUuid MotionUuidState { get; }

		// @property (readonly, nonatomic) ESTBeaconMotionState motionState;
		[Export ("motionState")]
		BeaconMotionState MotionState { get; }

		// @property (readonly, nonatomic) ESTBeaconTemperatureState temperatureState;
		[Export ("temperatureState")]
		BeaconTemperatureState TemperatureState { get; }

		// @property (readonly, nonatomic) ESTBeaconConditionalBroadcasting conditionalBroadcastingState;
		[Export ("conditionalBroadcastingState")]
		BeaconConditionalBroadcasting ConditionalBroadcastingState { get; }

		// @property (readonly, nonatomic) ESTBeaconMotionDetection motionDetectionState;
		[Export ("motionDetectionState")]
		BeaconMotionDetection MotionDetectionState { get; }

		// -(void)readTemperatureWithCompletion:(ESTNumberCompletionBlock _Nonnull)completion;
		[Export ("readTemperatureWithCompletion:"), Async]
		void ReadTemperature (NumberCompletionBlock completion);

		// -(void)readAccelerometerCountWithCompletion:(ESTNumberCompletionBlock _Nonnull)completion;
		[Export ("readAccelerometerCountWithCompletion:"), Async]
		void ReadAccelerometerCount (NumberCompletionBlock completion);

		// -(void)resetAccelerometerCountWithCompletion:(ESTUnsignedShortCompletionBlock _Nonnull)completion;
		[Export ("resetAccelerometerCountWithCompletion:"), Async]
		void ResetAccelerometerCount (UnsignedShortCompletionBlock completion);

		// -(void)writeBroadcastingScheme:(ESTBroadcastingScheme)broadcastingScheme completion:(ESTUnsignedShortCompletionBlock _Nonnull)completion;
		[Export ("writeBroadcastingScheme:completion:"), Async]
		void WriteBroadcastingScheme (BroadcastingScheme broadcastingScheme, UnsignedShortCompletionBlock completion);

		// -(void)writeConditionalBroadcastingType:(ESTBeaconConditionalBroadcasting)conditionalBroadcasting completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeConditionalBroadcastingType:completion:"), Async]
		void WriteConditionalBroadcastingType (BeaconConditionalBroadcasting conditionalBroadcasting, BoolCompletionBlock completion);

		// -(void)writeName:(NSString * _Nonnull)name completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeName:completion:"), Async]
		void WriteName (string name, StringCompletionBlock completion);

		// -(void)writeProximityUUID:(NSString * _Nonnull)pUUID completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeProximityUUID:completion:"), Async]
		void WriteProximityUuid (string pUUID, StringCompletionBlock completion);

		// -(void)writeMajor:(unsigned short)major completion:(ESTUnsignedShortCompletionBlock _Nonnull)completion;
		[Export ("writeMajor:completion:"), Async]
		void WriteMajor (ushort major, UnsignedShortCompletionBlock completion);

		// -(void)writeMinor:(unsigned short)minor completion:(ESTUnsignedShortCompletionBlock _Nonnull)completion;
		[Export ("writeMinor:completion:"), Async]
		void WriteMinor (ushort minor, UnsignedShortCompletionBlock completion);

		// -(void)writeAdvInterval:(unsigned short)interval completion:(ESTUnsignedShortCompletionBlock _Nonnull)completion;
		[Export ("writeAdvInterval:completion:"), Async]
		void WriteAdvInterval (ushort interval, UnsignedShortCompletionBlock completion);

		// -(void)writePower:(ESTBeaconPower)power completion:(ESTPowerCompletionBlock _Nonnull)completion;
		[Export ("writePower:completion:"), Async]
		void WritePower (BeaconPower power, PowerCompletionBlock completion);

		// -(void)writeEddystoneDomainNamespace:(NSString * _Nonnull)eddystoneNamespace completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeEddystoneDomainNamespace:completion:"), Async]
		void WriteEddystoneDomainNamespace (string eddystoneNamespace, StringCompletionBlock completion);

		// -(void)writeEddystoneHexNamespace:(NSString * _Nonnull)eddystoneNamespace completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeEddystoneHexNamespace:completion:"), Async]
		void WriteEddystoneHexNamespace (string eddystoneNamespace, StringCompletionBlock completion);

		// -(void)writeEddystoneInstance:(NSString * _Nonnull)eddystoneInstance completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeEddystoneInstance:completion:"), Async]
		void WriteEddystoneInstance (string eddystoneInstance, StringCompletionBlock completion);

		// -(void)writeEddystoneURL:(NSString * _Nonnull)eddystoneURL completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("writeEddystoneURL:completion:"), Async]
		void WriteEddystoneUrl (string eddystoneURL, StringCompletionBlock completion);

		// -(void)writeBasicPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeBasicPowerModeEnabled:completion:"), Async]
		void WriteBasicPowerModeEnabled (bool enable, BoolCompletionBlock completion);

		// -(void)writeSmartPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeSmartPowerModeEnabled:completion:"), Async]
		void WriteSmartPowerModeEnabled (bool enable, BoolCompletionBlock completion);

		// -(void)writeEstimoteSecureUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeEstimoteSecureUUIDEnabled:completion:"), Async]
		void WriteEstimoteSecureUuidEnabled (bool enable, BoolCompletionBlock completion);

		// -(void)writeMotionDetectionEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeMotionDetectionEnabled:completion:"), Async]
		void WriteMotionDetectionEnabled (bool enable, BoolCompletionBlock completion);

		// -(void)writeMotionUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock _Nonnull)completion;
		[Export ("writeMotionUUIDEnabled:completion:"), Async]
		void WriteMotionUuidEnabled (bool enable, BoolCompletionBlock completion);

		// -(void)writeCalibratedTemperature:(NSNumber * _Nonnull)temperature completion:(ESTNumberCompletionBlock _Nonnull)completion;
		[Export ("writeCalibratedTemperature:completion:"), Async]
		void WriteCalibratedTemperature (NSNumber temperature, NumberCompletionBlock completion);

		// -(void)writeLatitude:(NSNumber * _Nonnull)latitude longitude:(NSNumber * _Nonnull)longitude completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("writeLatitude:longitude:completion:"), Async]
		void WriteLatitude (NSNumber latitude, NSNumber longitude, CompletionBlock completion);

		// -(void)writeTags:(NSSet<NSString *> * _Nonnull)tags completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("writeTags:completion:"), Async]
		void WriteTags (NSSet<NSString> tags, CompletionBlock completion);

		// -(void)resetToFactorySettingsWithCompletion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("resetToFactorySettingsWithCompletion:"), Async]
		void ResetToFactorySettings (CompletionBlock completion);

		// -(void)getMacAddressWithCompletion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("getMacAddressWithCompletion:"), Async]
		void GetMacAddress (StringCompletionBlock completion);

		// -(void)findPeripheralForBeaconWithTimeout:(NSUInteger)timeout completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("findPeripheralForBeaconWithTimeout:completion:"), Async]
		void FindPeripheralForBeacon (nuint timeout, BeaconConnectionCompletionBlock completion);

		// -(void)checkFirmwareUpdateWithCompletion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("checkFirmwareUpdateWithCompletion:"), Async]
		void CheckFirmwareUpdate (FirmwareInfoVOCompletionBlock completion);

		// -(void)updateFirmwareWithProgress:(ESTProgressBlock _Nonnull)progress completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("updateFirmwareWithProgress:completion:"), Async]
		void UpdateFirmware (ProgressBlock progress, CompletionBlock completion);

		// -(ESTBeaconVO * _Nonnull)valueObject;
		[Export ("valueObject")]
		BeaconVO ValueObject { get; }
	}

	// typedef void (^ESTSettingConnectivityPowerCompletionBlock)(ESTSettingConnectivityPower * _Nullable, NSError * _Nullable);
	delegate void SettingConnectivityPowerCompletionBlock ([NullAllowed] SettingConnectivityPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingConnectivityPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingConnectivityPower")]
	interface SettingConnectivityPower : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTConnectablePowerLevel)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (ConnectablePowerLevel power);

		// -(ESTConnectablePowerLevel)getValue;
		[Export ("getValue")]
		ConnectablePowerLevel Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingConnectivityPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingConnectivityPowerCompletionBlock completion);

		// -(void)writeValue:(ESTConnectablePowerLevel)power completion:(ESTSettingConnectivityPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ConnectablePowerLevel power, SettingConnectivityPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTConnectablePowerLevel)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ConnectablePowerLevel power);
	}

	// typedef void (^ESTSettingShakeToConnectEnableCompletionBlock)(ESTSettingShakeToConnectEnable * _Nullable, NSError * _Nullable);
	delegate void SettingShakeToConnectEnableCompletionBlock ([NullAllowed] SettingShakeToConnectEnable shakeToConnectEnableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingShakeToConnectEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingShakeToConnectEnable")]
	interface SettingShakeToConnectEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)shakeToConnectEnable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool shakeToConnectEnable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingShakeToConnectEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingShakeToConnectEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)shakeToConnectEnable completion:(ESTSettingShakeToConnectEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool shakeToConnectEnable, SettingShakeToConnectEnableCompletionBlock completion);
	}

	// @interface ESTSettingOperation : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTSettingOperation")]
	interface SettingOperation
	{
		// @property (assign, nonatomic) ESTSettingOperationStatus status;
		[Export ("status", ArgumentSemantic.Assign)]
		SettingOperationStatus Status { get; set; }

		// -(instancetype _Nonnull)initWithType:(ESTSettingOperationType)type;
		[Export ("initWithType:")]
		IntPtr Constructor (SettingOperationType type);

		// -(ESTSettingOperationType)type;
		[Export ("type")]
		SettingOperationType Type { get; }

		// -(ESTSettingStorageType)storageType;
		[Export ("storageType")]
		SettingStorageType StorageType { get; }

		//ESTSettingOperation Internal Category
		// @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
		[Export ("device", ArgumentSemantic.Weak)]
		DeviceConnectable Device { get; set; }
	}

	// @interface Internal (ESTSettingOperation)
	//[Category]
	//[BaseType (typeof (SettingOperation))]
	//interface ESTSettingOperation_Internal
	//{
	//	// @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
	//	[NullAllowed, Export ("device")]
	//	DeviceConnectable Device ();

	//	[Export ("setDevice:")]
	//	void SetDevice ([NullAllowed] DeviceConnectable device);
	//}

	// @interface ESTBeaconOperationConnectivityInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationConnectivityInterval")]
	interface BeaconOperationConnectivityInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingConnectivityIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationConnectivityInterval ReadOperation (SettingConnectivityIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingConnectivityInterval * _Nonnull)setting completion:(ESTSettingConnectivityIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationConnectivityInterval WriteOperation (SettingConnectivityInterval setting, SettingConnectivityIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationConnectivityPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationConnectivityPower")]
	interface BeaconOperationConnectivityPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingConnectivityPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationConnectivityPower ReadOperation (SettingConnectivityPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingConnectivityPower * _Nonnull)setting completion:(ESTSettingConnectivityPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationConnectivityPower WriteOperation (SettingConnectivityPower setting, SettingConnectivityPowerCompletionBlock completion);
	}

	// @interface ESTBeaconOperationShakeToConnectEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationShakeToConnectEnable")]
	interface BeaconOperationShakeToConnectEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingShakeToConnectEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationShakeToConnectEnable ReadOperation (SettingShakeToConnectEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingShakeToConnectEnable * _Nonnull)setting completion:(ESTSettingShakeToConnectEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationShakeToConnectEnable WriteOperation (SettingShakeToConnectEnable setting, SettingShakeToConnectEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingPowerSmartPowerModeEnableCompletionBlock)(ESTSettingPowerSmartPowerModeEnable * _Nullable, NSError * _Nullable);
	delegate void SettingPowerSmartPowerModeEnableCompletionBlock ([NullAllowed] SettingPowerSmartPowerModeEnable smartPowerModeEnableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerSmartPowerModeEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerSmartPowerModeEnable")]
	interface SettingPowerSmartPowerModeEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)smartPowerModeEnable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool smartPowerModeEnable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerSmartPowerModeEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)smartPowerModeEnable completion:(ESTSettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool smartPowerModeEnable, SettingPowerSmartPowerModeEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)smartPowerModeEnable;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool smartPowerModeEnable);
	}

	// typedef void (^ESTSettingPowerFlipToSleepEnableCompletionBlock)(ESTSettingPowerFlipToSleepEnable * _Nullable, NSError * _Nullable);
	delegate void SettingPowerFlipToSleepEnableCompletionBlock ([NullAllowed] SettingPowerFlipToSleepEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerFlipToSleepEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerFlipToSleepEnable")]
	interface SettingPowerFlipToSleepEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerFlipToSleepEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingPowerFlipToSleepEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled);
	}

	// typedef void (^ESTSettingPowerDarkToSleepEnableCompletionBlock)(ESTSettingPowerDarkToSleepEnable * _Nullable, NSError * _Nullable);
	delegate void SettingPowerDarkToSleepEnableCompletionBlock ([NullAllowed] SettingPowerDarkToSleepEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerDarkToSleepEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerDarkToSleepEnable")]
	interface SettingPowerDarkToSleepEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerDarkToSleepEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingPowerDarkToSleepEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled);
	}

	// typedef void (^ESTSettingPowerBatteryLifetimeCompletionBlock)(ESTSettingPowerBatteryLifetime * _Nullable, NSError * _Nullable);
	delegate void SettingPowerBatteryLifetimeCompletionBlock ([NullAllowed] SettingPowerBatteryLifetime batteryLifetimeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerBatteryLifetime : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingPowerBatteryLifetime")]
	interface SettingPowerBatteryLifetime : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSUInteger)batteryLifetime;
		[Export ("initWithValue:")]
		IntPtr Constructor (nuint batteryLifetime);

		// -(NSUInteger)getValue;
		[Export ("getValue")]
		nuint Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerBatteryLifetimeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerBatteryLifetimeCompletionBlock completion);
	}

	// typedef void (^ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock)(ESTSettingPowerMotionOnlyBroadcastingEnable * _Nullable, NSError * _Nullable);
	delegate void SettingPowerMotionOnlyBroadcastingEnableCompletionBlock ([NullAllowed] SettingPowerMotionOnlyBroadcastingEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerMotionOnlyBroadcastingEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerMotionOnlyBroadcastingEnable")]
	interface SettingPowerMotionOnlyBroadcastingEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled);
	}

	// typedef void (^ESTSettingPowerMotionOnlyBroadcastingDelayCompletionBlock)(ESTSettingPowerMotionOnlyBroadcastingDelay * _Nullable, NSError * _Nullable);
	delegate void SettingPowerMotionOnlyBroadcastingDelayCompletionBlock ([NullAllowed] SettingPowerMotionOnlyBroadcastingDelay motionOnlyBroadcastingDelaySetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerMotionOnlyBroadcastingDelay : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerMotionOnlyBroadcastingDelay")]
	interface SettingPowerMotionOnlyBroadcastingDelay : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(unsigned int)motionOnlyBroadcastingDelay;
		[Export ("initWithValue:")]
		IntPtr Constructor (uint motionOnlyBroadcastingDelay);

		// -(unsigned int)getValue;
		[Export ("getValue")]
		uint Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerMotionOnlyBroadcastingDelayCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerMotionOnlyBroadcastingDelayCompletionBlock completion);

		// -(void)writeValue:(unsigned int)motionOnlyBroadcastingDelay completion:(ESTSettingPowerMotionOnlyBroadcastingDelayCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (uint motionOnlyBroadcastingDelay, SettingPowerMotionOnlyBroadcastingDelayCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned int)motionOnlyBroadcastingDelay;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (uint motionOnlyBroadcastingDelay);
	}

	// @interface ESTBeaconOperationPowerSmartPowerModeEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerSmartPowerModeEnable")]
	interface BeaconOperationPowerSmartPowerModeEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerSmartPowerModeEnable ReadOperation (SettingPowerSmartPowerModeEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerSmartPowerModeEnable * _Nonnull)setting completion:(ESTSettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerSmartPowerModeEnable WriteOperation (SettingPowerSmartPowerModeEnable setting, SettingPowerSmartPowerModeEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerFlipToSleepEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerFlipToSleepEnable")]
	interface BeaconOperationPowerFlipToSleepEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerFlipToSleepEnable ReadOperation (SettingPowerFlipToSleepEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerFlipToSleepEnable * _Nonnull)setting completion:(ESTSettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerFlipToSleepEnable WriteOperation (SettingPowerFlipToSleepEnable setting, SettingPowerFlipToSleepEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerDarkToSleepEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerDarkToSleepEnable")]
	interface BeaconOperationPowerDarkToSleepEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerDarkToSleepEnable ReadOperation (SettingPowerDarkToSleepEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerDarkToSleepEnable * _Nonnull)setting completion:(ESTSettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerDarkToSleepEnable WriteOperation (SettingPowerDarkToSleepEnable setting, SettingPowerDarkToSleepEnableCompletionBlock completion);
	}

	interface ICloudOperationProtocol { }

	// @protocol ESTCloudOperationProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTCloudOperationProtocol")]
	interface CloudOperationProtocol
	{
		// @required -(Class _Nonnull)settingClass;
		[Abstract]
		[Export ("settingClass")]
		Class SettingClass { get; }

		// @required -(void)updateSettingWithSetting:(ESTSettingBase * _Nonnull)setting;
		[Abstract]
		[Export ("updateSettingWithSetting:")]
		void UpdateSettingWithSetting (SettingBase setting);
	}

	// @interface ESTCloudOperationPowerBatteryLifetime : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationPowerBatteryLifetime")]
	interface CloudOperationPowerBatteryLifetime : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryLifetimeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationPowerBatteryLifetime ReadOperation (SettingPowerBatteryLifetimeCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerMotionOnlyBroadcastingEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerMotionOnlyBroadcastingEnable")]
	interface BeaconOperationPowerMotionOnlyBroadcastingEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerMotionOnlyBroadcastingEnable ReadOperation (SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerMotionOnlyBroadcastingEnable * _Nonnull)setting completion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerMotionOnlyBroadcastingEnable WriteOperation (SettingPowerMotionOnlyBroadcastingEnable setting, SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerMotionOnlyBroadcastingDelay : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerMotionOnlyBroadcastingDelay")]
	interface BeaconOperationPowerMotionOnlyBroadcastingDelay : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerMotionOnlyBroadcastingDelayCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerMotionOnlyBroadcastingDelay ReadOperation (SettingPowerMotionOnlyBroadcastingDelayCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerMotionOnlyBroadcastingDelay * _Nonnull)setting completion:(ESTSettingPowerMotionOnlyBroadcastingDelayCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerMotionOnlyBroadcastingDelay WriteOperation (SettingPowerMotionOnlyBroadcastingDelay setting, SettingPowerMotionOnlyBroadcastingDelayCompletionBlock completion);
	}

	// typedef void (^ESTSettingPowerScheduledAdvertisingEnableCompletionBlock)(ESTSettingPowerScheduledAdvertisingEnable * _Nullable, NSError * _Nullable);
	delegate void SettingPowerScheduledAdvertisingEnableCompletionBlock ([NullAllowed] SettingPowerScheduledAdvertisingEnable enableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerScheduledAdvertisingEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerScheduledAdvertisingEnable")]
	interface SettingPowerScheduledAdvertisingEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerScheduledAdvertisingEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enable completion:(ESTSettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enable, SettingPowerScheduledAdvertisingEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enable;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enable);
	}

	// @interface ESTTime : NSObject <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTime")]
	interface Time : INSCopying
	{
		// @property (readonly, assign, nonatomic) NSUInteger secondsSinceMidnight;
		[Export ("secondsSinceMidnight")]
		nuint SecondsSinceMidnight { get; }

		// -(instancetype _Nonnull)initWithSecondsSinceMidnight:(NSUInteger)seconds;
		[Export ("initWithSecondsSinceMidnight:")]
		IntPtr Constructor (nuint seconds);

		// -(instancetype _Nonnull)initWithHours:(NSUInteger)hours minutes:(NSUInteger)minutes seconds:(NSUInteger)seconds;
		[Export ("initWithHours:minutes:seconds:")]
		IntPtr Constructor (nuint hours, nuint minutes, nuint seconds);

		// -(instancetype _Nonnull)initWithHours:(NSUInteger)hours minutes:(NSUInteger)minutes;
		[Export ("initWithHours:minutes:")]
		IntPtr Constructor (nuint hours, nuint minutes);
	}

	// @interface ESTTimePeriod : NSObject <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTimePeriod")]
	interface TimePeriod : INSCopying
	{
		// @property (readonly, nonatomic, strong) ESTTime * _Nonnull startTime;
		[Export ("startTime", ArgumentSemantic.Strong)]
		Time StartTime { get; }

		// @property (readonly, nonatomic, strong) ESTTime * _Nonnull endTime;
		[Export ("endTime", ArgumentSemantic.Strong)]
		Time EndTime { get; }

		// -(instancetype _Nonnull)initWithStartTime:(ESTTime * _Nonnull)startTime endTime:(ESTTime * _Nonnull)endTime;
		[Export ("initWithStartTime:endTime:")]
		IntPtr Constructor (Time startTime, Time endTime);

		// -(instancetype _Nonnull)initWithStartTimeSeconds:(NSUInteger)startTimeSeconds endTimeSeconds:(NSUInteger)endTimeSeconds;
		[Export ("initWithStartTimeSeconds:endTimeSeconds:")]
		IntPtr Constructor (nuint startTimeSeconds, nuint endTimeSeconds);
	}

	// typedef void (^ESTSettingPowerScheduledAdvertisingPeriodCompletionBlock)(ESTSettingPowerScheduledAdvertisingPeriod * _Nullable, NSError * _Nullable);
	delegate void SettingPowerScheduledAdvertisingPeriodCompletionBlock ([NullAllowed] SettingPowerScheduledAdvertisingPeriod periodSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerScheduledAdvertisingPeriod : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerScheduledAdvertisingPeriod")]
	interface SettingPowerScheduledAdvertisingPeriod : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTTimePeriod * _Nonnull)period;
		[Export ("initWithValue:")]
		IntPtr Constructor (TimePeriod period);

		// -(ESTTimePeriod * _Nonnull)getValue;
		[Export ("getValue")]
		TimePeriod Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);

		// -(void)writeValue:(ESTTimePeriod * _Nonnull)period completion:(ESTSettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (TimePeriod period, SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTTimePeriod * _Nonnull)period;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (TimePeriod period);
	}

	// typedef void (^ESTSettingPowerBatteryPercentageCompletionBlock)(ESTSettingPowerBatteryPercentage * _Nullable, NSError * _Nullable);
	delegate void SettingPowerBatteryPercentageCompletionBlock ([NullAllowed] SettingPowerBatteryPercentage batteryPercentageSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerBatteryPercentage : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingPowerBatteryPercentage")]
	interface SettingPowerBatteryPercentage : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(uint8_t)batteryPercentage;
		[Export ("initWithValue:")]
		IntPtr Constructor (byte batteryPercentage);

		// -(uint8_t)getValue;
		[Export ("getValue")]
		byte Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerBatteryPercentageCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerBatteryPercentageCompletionBlock completion);
	}

	// typedef void (^ESTSettingPowerBatteryVoltageCompletionBlock)(ESTSettingPowerBatteryVoltage * _Nullable, NSError * _Nullable);
	delegate void SettingPowerBatteryVoltageCompletionBlock ([NullAllowed] SettingPowerBatteryVoltage voltageSetting, [NullAllowed] NSError error);

	// @interface ESTSettingPowerBatteryVoltage : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingPowerBatteryVoltage")]
	interface SettingPowerBatteryVoltage : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(unsigned short)voltage;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort voltage);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingPowerBatteryVoltageCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingPowerBatteryVoltageCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerScheduledAdvertisingEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerScheduledAdvertisingEnable")]
	interface BeaconOperationPowerScheduledAdvertisingEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerScheduledAdvertisingEnable ReadOperation (SettingPowerScheduledAdvertisingEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerScheduledAdvertisingEnable * _Nonnull)setting completion:(ESTSettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerScheduledAdvertisingEnable WriteOperation (SettingPowerScheduledAdvertisingEnable setting, SettingPowerScheduledAdvertisingEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerScheduledAdvertisingPeriod : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerScheduledAdvertisingPeriod")]
	interface BeaconOperationPowerScheduledAdvertisingPeriod : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerScheduledAdvertisingPeriod ReadOperation (SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerScheduledAdvertisingPeriod * _Nonnull)setting completion:(ESTSettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationPowerScheduledAdvertisingPeriod WriteOperation (SettingPowerScheduledAdvertisingPeriod setting, SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerBatteryPercentage : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerBatteryPercentage")]
	interface BeaconOperationPowerBatteryPercentage : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryPercentageCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerBatteryPercentage ReadOperation (SettingPowerBatteryPercentageCompletionBlock completion);
	}

	// @interface ESTBeaconOperationPowerBatteryVoltage : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerBatteryVoltage")]
	interface BeaconOperationPowerBatteryVoltage : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryVoltageCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationPowerBatteryVoltage ReadOperation (SettingPowerBatteryVoltageCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoFirmwareVersionCompletionBlock)(ESTSettingDeviceInfoFirmwareVersion * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoFirmwareVersionCompletionBlock ([NullAllowed] SettingDeviceInfoFirmwareVersion versionSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoFirmwareVersion : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoFirmwareVersion")]
	interface SettingDeviceInfoFirmwareVersion : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)version;
		[Export ("initWithValue:")]
		IntPtr Constructor (string version);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoFirmwareVersionCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoFirmwareVersionCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoApplicationVersionCompletionBlock)(ESTSettingDeviceInfoApplicationVersion * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoApplicationVersionCompletionBlock ([NullAllowed] SettingDeviceInfoApplicationVersion applicationVersionSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoApplicationVersion : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoApplicationVersion")]
	interface SettingDeviceInfoApplicationVersion : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)applicationVersion;
		[Export ("initWithValue:")]
		IntPtr Constructor (string applicationVersion);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoApplicationVersionCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoApplicationVersionCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoBootloaderVersionCompletionBlock)(ESTSettingDeviceInfoBootloaderVersion * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoBootloaderVersionCompletionBlock ([NullAllowed] SettingDeviceInfoBootloaderVersion bootloaderVersionSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoBootloaderVersion : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoBootloaderVersion")]
	interface SettingDeviceInfoBootloaderVersion : INSCopying
	{
		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoBootloaderVersionCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoBootloaderVersionCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoHardwareVersionCompletionBlock)(ESTSettingDeviceInfoHardwareVersion * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoHardwareVersionCompletionBlock ([NullAllowed] SettingDeviceInfoHardwareVersion versionSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoHardwareVersion : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoHardwareVersion")]
	interface SettingDeviceInfoHardwareVersion : INSCopying
	{
		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoHardwareVersionCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoHardwareVersionCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoUTCTimeCompletionBlock)(ESTSettingDeviceInfoUTCTime * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoUtcTimeCompletionBlock ([NullAllowed] SettingDeviceInfoUtcTime utcTimeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoUTCTime : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingDeviceInfoUTCTime")]
	interface SettingDeviceInfoUtcTime : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSTimeInterval)UTCTime;
		[Export ("initWithValue:")]
		IntPtr Constructor (double UTCTime);

		// -(NSTimeInterval)getValue;
		[Export ("getValue")]
		double Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoUtcTimeCompletionBlock completion);

		// -(void)writeValue:(NSTimeInterval)UTCTime completion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (double UTCTime, SettingDeviceInfoUtcTimeCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSTimeInterval)UTCTime;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (double UTCTime);
	}

	// typedef void (^ESTSettingDeviceInfoTagsCompletionBlock)(ESTSettingDeviceInfoTags * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoTagsCompletionBlock ([NullAllowed] SettingDeviceInfoTags tagsSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoTags : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingDeviceInfoTags")]
	interface SettingDeviceInfoTags : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSSet<NSString *> * _Nonnull)tags;
		[Export ("initWithValue:")]
		IntPtr Constructor (NSSet<NSString> tags);

		// -(NSSet<NSString *> * _Nonnull)getValue;
		[Export ("getValue")]
		NSSet<NSString> Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoTagsCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoTagsCompletionBlock completion);

		// -(void)writeValue:(NSSet<NSString *> * _Nonnull)tags completion:(ESTSettingDeviceInfoTagsCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NSSet<NSString> tags, SettingDeviceInfoTagsCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSSet<NSString *> * _Nonnull)tags;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (NSSet<NSString> tags);
	}

	// typedef void (^ESTSettingDeviceInfoGeoLocationCompletionBlock)(ESTSettingDeviceInfoGeoLocation * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoGeoLocationCompletionBlock ([NullAllowed] SettingDeviceInfoGeoLocation geoLocationSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoGeoLocation : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingDeviceInfoGeoLocation")]
	interface SettingDeviceInfoGeoLocation : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTDeviceGeoLocation * _Nonnull)geoLocation;
		[Export ("initWithValue:")]
		IntPtr Constructor (DeviceGeoLocation geoLocation);

		// -(ESTDeviceGeoLocation * _Nonnull)getValue;
		[Export ("getValue")]
		DeviceGeoLocation Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoGeoLocationCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoGeoLocationCompletionBlock completion);

		// -(void)writeValue:(ESTDeviceGeoLocation * _Nonnull)geoLocation completion:(ESTSettingDeviceInfoGeoLocationCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (DeviceGeoLocation geoLocation, SettingDeviceInfoGeoLocationCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTDeviceGeoLocation * _Nonnull)geoLocation;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (DeviceGeoLocation geoLocation);
	}

	// typedef void (^ESTSettingDeviceInfoNameCompletionBlock)(ESTSettingDeviceInfoName * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoNameCompletionBlock ([NullAllowed] SettingDeviceInfoName nameSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoName : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingDeviceInfoName")]
	interface SettingDeviceInfoName : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)name;
		[Export ("initWithValue:")]
		IntPtr Constructor (string name);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoNameCompletionBlock completion);

		// -(void)writeValue:(NSString * _Nonnull)name completion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (string name, SettingDeviceInfoNameCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)name;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (string name);
	}

	// typedef void (^ESTSettingDeviceInfoColorCompletionBlock)(ESTSettingDeviceInfoColor * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoColorCompletionBlock ([NullAllowed] SettingDeviceInfoColor colorSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoColor : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoColor")]
	interface SettingDeviceInfoColor : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTColor)color;
		[Export ("initWithValue:")]
		IntPtr Constructor (Color color);

		// -(ESTColor)getValue;
		[Export ("getValue")]
		Color Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoColorCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoColorCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoIndoorLocationIdentifierCompletionBlock)(ESTSettingDeviceInfoIndoorLocationIdentifier * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoIndoorLocationIdentifierCompletionBlock ([NullAllowed] SettingDeviceInfoIndoorLocationIdentifier indoorLocationIdentifierSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoIndoorLocationIdentifier : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoIndoorLocationIdentifier")]
	interface SettingDeviceInfoIndoorLocationIdentifier : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)indoorLocationIdentifier;
		[Export ("initWithValue:")]
		IntPtr Constructor (string indoorLocationIdentifier);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoIndoorLocationIdentifierCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoIndoorLocationIdentifierCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoIndoorLocationNameCompletionBlock)(ESTSettingDeviceInfoIndoorLocationName * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoIndoorLocationNameCompletionBlock ([NullAllowed] SettingDeviceInfoIndoorLocationName nameSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoIndoorLocationName : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoIndoorLocationName")]
	interface SettingDeviceInfoIndoorLocationName : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)name;
		[Export ("initWithValue:")]
		IntPtr Constructor (string name);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoIndoorLocationNameCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoIndoorLocationNameCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoUptimeCompletionBlock)(ESTSettingDeviceInfoUptime * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoUptimeCompletionBlock ([NullAllowed] SettingDeviceInfoUptime uptimeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoUptime : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingDeviceInfoUptime")]
	interface SettingDeviceInfoUptime : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(int)uptime;
		[Export ("initWithValue:")]
		IntPtr Constructor (int uptime);

		// -(int)getValue;
		[Export ("getValue")]
		int Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoUptimeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoUptimeCompletionBlock completion);
	}

	// typedef void (^ESTSettingDeviceInfoDevelopmentModeCompletionBlock)(ESTSettingDeviceInfoDevelopmentMode * _Nullable, NSError * _Nullable);
	delegate void SettingDeviceInfoDevelopmentModeCompletionBlock ([NullAllowed] SettingDeviceInfoDevelopmentMode developmentModeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingDeviceInfoDevelopmentMode : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingDeviceInfoDevelopmentMode")]
	interface SettingDeviceInfoDevelopmentMode : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)developmentMode;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool developmentMode);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingDeviceInfoDevelopmentModeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingDeviceInfoDevelopmentModeCompletionBlock completion);

		// -(void)writeValue:(BOOL)developmentMode completion:(ESTSettingDeviceInfoDevelopmentModeCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool developmentMode, SettingDeviceInfoDevelopmentModeCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)developmentMode;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool developmentMode);
	}

	// @interface ESTCloudOperationDeviceInfoFirmwareVersion : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoFirmwareVersion")]
	interface CloudOperationDeviceInfoFirmwareVersion : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoFirmwareVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoFirmwareVersion ReadOperation (SettingDeviceInfoFirmwareVersionCompletionBlock completion);
	}

	// @interface ESTBeaconOperationDeviceInfoApplicationVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoApplicationVersion")]
	interface BeaconOperationDeviceInfoApplicationVersion : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoApplicationVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationDeviceInfoApplicationVersion ReadOperation (SettingDeviceInfoApplicationVersionCompletionBlock completion);
	}

	// @interface ESTBeaconOperationDeviceInfoBootloaderVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoBootloaderVersion")]
	interface BeaconOperationDeviceInfoBootloaderVersion : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoBootloaderVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationDeviceInfoBootloaderVersion ReadOperation (SettingDeviceInfoBootloaderVersionCompletionBlock completion);
	}

	// @interface ESTBeaconOperationDeviceInfoHardwareVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoHardwareVersion")]
	interface BeaconOperationDeviceInfoHardwareVersion : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoHardwareVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationDeviceInfoHardwareVersion ReadOperation (SettingDeviceInfoHardwareVersionCompletionBlock completion);
	}

	// @interface ESTBeaconOperationDeviceInfoUTCTime : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoUTCTime")]
	interface BeaconOperationDeviceInfoUtcTime : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationDeviceInfoUtcTime ReadOperation (SettingDeviceInfoUtcTimeCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoUTCTime * _Nonnull)setting completion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationDeviceInfoUtcTime WriteOperation (SettingDeviceInfoUtcTime setting, SettingDeviceInfoUtcTimeCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoTags : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoTags")]
	interface CloudOperationDeviceInfoTags : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoTagsCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoTags ReadOperation (SettingDeviceInfoTagsCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoTags * _Nonnull)setting completion:(ESTSettingDeviceInfoTagsCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationDeviceInfoTags WriteOperation (SettingDeviceInfoTags setting, SettingDeviceInfoTagsCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoGeoLocation : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoGeoLocation")]
	interface CloudOperationDeviceInfoGeoLocation : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoGeoLocationCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoGeoLocation ReadOperation (SettingDeviceInfoGeoLocationCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoGeoLocation * _Nonnull)setting completion:(ESTSettingDeviceInfoGeoLocationCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationDeviceInfoGeoLocation WriteOperation (SettingDeviceInfoGeoLocation setting, SettingDeviceInfoGeoLocationCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoName : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoName")]
	interface CloudOperationDeviceInfoName : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoName ReadOperation (SettingDeviceInfoNameCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoName * _Nonnull)setting completion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationDeviceInfoName WriteOperation (SettingDeviceInfoName setting, SettingDeviceInfoNameCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoColor : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoColor")]
	interface CloudOperationDeviceInfoColor : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoColorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoColor ReadOperation (SettingDeviceInfoColorCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoColor * _Nonnull)setting completion:(ESTSettingDeviceInfoColorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationDeviceInfoColor WriteOperation (SettingDeviceInfoColor setting, SettingDeviceInfoColorCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoIndoorLocationIdentifier : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoIndoorLocationIdentifier")]
	interface CloudOperationDeviceInfoIndoorLocationIdentifier : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoIndoorLocationIdentifierCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoIndoorLocationIdentifier ReadOperation (SettingDeviceInfoIndoorLocationIdentifierCompletionBlock completion);
	}

	// @interface ESTBeaconOperationDeviceInfoUptime : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoUptime")]
	interface BeaconOperationDeviceInfoUptime : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoUptimeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationDeviceInfoUptime ReadOperation (SettingDeviceInfoUptimeCompletionBlock completion);
	}

	// @interface ESTCloudOperationDeviceInfoDevelopmentMode : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationDeviceInfoDevelopmentMode")]
	interface CloudOperationDeviceInfoDevelopmentMode : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoDevelopmentModeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationDeviceInfoDevelopmentMode ReadOperation (SettingDeviceInfoDevelopmentModeCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoDevelopmentMode * _Nonnull)setting completion:(ESTSettingDeviceInfoDevelopmentModeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationDeviceInfoDevelopmentMode WriteOperation (SettingDeviceInfoDevelopmentMode setting, SettingDeviceInfoDevelopmentModeCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconEnableCompletionBlock)(ESTSettingIBeaconEnable * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconEnableCompletionBlock ([NullAllowed] SettingIBeaconEnable iBeaconEnableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconEnable")]
	interface SettingIBeaconEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingIBeaconEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingIBeaconEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconIntervalCompletionBlock)(ESTSettingIBeaconInterval * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconIntervalCompletionBlock ([NullAllowed] SettingIBeaconInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconInterval")]
	interface SettingIBeaconInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingIBeaconIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingIBeaconIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingIBeaconMajorCompletionBlock)(ESTSettingIBeaconMajor * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconMajorCompletionBlock ([NullAllowed] SettingIBeaconMajor major, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconMajor : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMajor")]
	interface SettingIBeaconMajor : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)major;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort major);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconMajorCompletionBlock completion);

		// -(void)writeValue:(unsigned short)value completion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort value, SettingIBeaconMajorCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)major;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort major);
	}

	// typedef void (^ESTSettingIBeaconMinorCompletionBlock)(ESTSettingIBeaconMinor * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconMinorCompletionBlock ([NullAllowed] SettingIBeaconMinor minor, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconMinor : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMinor")]
	interface SettingIBeaconMinor : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)minor;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort minor);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconMinorCompletionBlock completion);

		// -(void)writeValue:(unsigned short)minor completion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort minor, SettingIBeaconMinorCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)minor;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort minor);
	}

	// typedef void (^ESTSettingIBeaconPowerCompletionBlock)(ESTSettingIBeaconPower * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconPowerCompletionBlock ([NullAllowed] SettingIBeaconPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconPower")]
	interface SettingIBeaconPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTIBeaconPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (IBeaconPower power);

		// -(ESTIBeaconPower)getValue;
		[Export ("getValue")]
		IBeaconPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconPowerCompletionBlock completion);

		// -(void)writeValue:(ESTIBeaconPower)power completion:(ESTSettingIBeaconPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (IBeaconPower power, SettingIBeaconPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTIBeaconPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (IBeaconPower power);
	}

	// typedef void (^ESTSettingIBeaconProximityUUIDCompletionBlock)(ESTSettingIBeaconProximityUUID * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconProximityUuidCompletionBlock ([NullAllowed] SettingIBeaconProximityUuid proximityUuidSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconProximityUUID : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconProximityUUID")]
	interface SettingIBeaconProximityUuid : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSUUID * _Nonnull)proximityUUID;
		[Export ("initWithValue:")]
		IntPtr Constructor (NSUuid proximityUUID);

		// -(NSUUID * _Nonnull)getValue;
		[Export ("getValue")]
		NSUuid Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconProximityUuidCompletionBlock completion);

		// -(void)writeValue:(NSUUID * _Nonnull)proximityUUID completion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NSUuid proximityUUID, SettingIBeaconProximityUuidCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconSecureUUIDEnableCompletionBlock)(ESTSettingIBeaconSecureUUIDEnable * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconSecureUuidEnableCompletionBlock ([NullAllowed] SettingIBeaconSecureUuidEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconSecureUUIDEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconSecureUUIDEnable")]
	interface SettingIBeaconSecureUuidEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconSecureUuidEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingIBeaconSecureUuidEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock)(ESTSettingIBeaconSecureUUIDPeriodScaler * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconSecureUuidPeriodScalerCompletionBlock ([NullAllowed] SettingIBeaconSecureUuidPeriodScaler scalerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconSecureUUIDPeriodScaler : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconSecureUUIDPeriodScaler")]
	interface SettingIBeaconSecureUuidPeriodScaler : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(uint8_t)scaler;
		[Export ("initWithValue:")]
		IntPtr Constructor (byte scaler);

		// -(uint8_t)getValue;
		[Export ("getValue")]
		byte Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconSecureUuidPeriodScalerCompletionBlock completion);

		// -(void)writeValue:(uint8_t)scaler completion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (byte scaler, SettingIBeaconSecureUuidPeriodScalerCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconNonStrictModeCompletionBlock)(ESTSettingIBeaconNonStrictMode * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconNonStrictModeCompletionBlock ([NullAllowed] SettingIBeaconNonStrictMode nonStrictModeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconNonStrictMode : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconNonStrictMode")]
	interface SettingIBeaconNonStrictMode : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)nonStrictMode;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool nonStrictMode);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconNonStrictModeCompletionBlock completion);

		// -(void)writeValue:(BOOL)nonStrictMode completion:(ESTSettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool nonStrictMode, SettingIBeaconNonStrictModeCompletionBlock completion);
	}

	// typedef void (^ESTSettingIBeaconMotionUUIDCompletionBlock)(ESTSettingIBeaconMotionUUID * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconMotionUuidCompletionBlock ([NullAllowed] SettingIBeaconMotionUuid motionUuidSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconMotionUUID : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingIBeaconMotionUUID")]
	interface SettingIBeaconMotionUuid : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSUUID * _Nonnull)motionUUID;
		[Export ("initWithValue:")]
		IntPtr Constructor (NSUuid motionUUID);

		// -(NSUUID * _Nonnull)getValue;
		[Export ("getValue")]
		NSUuid Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconMotionUUIDCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconMotionUuidCompletionBlock completion);

		// +(NSUUID * _Nonnull)motionProximityUUIDForProximityUUID:(NSUUID * _Nonnull)proximityUUID;
		[Static]
		[Export ("motionProximityUUIDForProximityUUID:")]
		NSUuid GetMotionProximityUuid (NSUuid proximityUUID);
	}

	// typedef void (^ESTSettingIBeaconMotionUUIDEnableCompletionBlock)(ESTSettingIBeaconMotionUUIDEnable * _Nullable, NSError * _Nullable);
	delegate void SettingIBeaconMotionUuidEnableCompletionBlock ([NullAllowed] SettingIBeaconMotionUuidEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingIBeaconMotionUUIDEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMotionUUIDEnable")]
	interface SettingIBeaconMotionUuidEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingIBeaconMotionUuidEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingIBeaconMotionUuidEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconEnable")]
	interface BeaconOperationIBeaconEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconEnable ReadOperation (SettingIBeaconEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconEnable * _Nonnull)setting completion:(ESTSettingIBeaconEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconEnable WriteOperation (SettingIBeaconEnable setting, SettingIBeaconEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconInterval")]
	interface BeaconOperationIBeaconInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconInterval ReadOperation (SettingIBeaconIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconInterval * _Nonnull)setting completion:(ESTSettingIBeaconIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconInterval WriteOperation (SettingIBeaconInterval setting, SettingIBeaconIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconMajor : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMajor")]
	interface BeaconOperationIBeaconMajor : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconMajor ReadOperation (SettingIBeaconMajorCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMajor * _Nonnull)value completion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconMajor WriteOperation (SettingIBeaconMajor value, SettingIBeaconMajorCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconMinor : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMinor")]
	interface BeaconOperationIBeaconMinor : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconMinor ReadOperation (SettingIBeaconMinorCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMinor * _Nonnull)setting completion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconMinor WriteOperation (SettingIBeaconMinor setting, SettingIBeaconMinorCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconPower")]
	interface BeaconOperationIBeaconPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconPower ReadOperation (SettingIBeaconPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconPower * _Nonnull)setting completion:(ESTSettingIBeaconPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconPower WriteOperation (SettingIBeaconPower setting, SettingIBeaconPowerCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconProximityUUID : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconProximityUUID")]
	interface BeaconOperationIBeaconProximityUuid : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconProximityUuid ReadOperation (SettingIBeaconProximityUuidCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconProximityUUID * _Nonnull)setting completion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconProximityUuid WriteOperation (SettingIBeaconProximityUuid setting, SettingIBeaconProximityUuidCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconSecureUUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconSecureUUIDEnable")]
	interface BeaconOperationIBeaconSecureUuidEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconSecureUuidEnable ReadOperation (SettingIBeaconSecureUuidEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconSecureUUIDEnable * _Nonnull)setting completion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconSecureUuidEnable WriteOperation (SettingIBeaconSecureUuidEnable setting, SettingIBeaconSecureUuidEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconSecureUUIDPeriodScaler : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconSecureUUIDPeriodScaler")]
	interface BeaconOperationIBeaconSecureUuidPeriodScaler : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconSecureUuidPeriodScaler ReadOperation (SettingIBeaconSecureUuidPeriodScalerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconSecureUUIDPeriodScaler * _Nonnull)setting completion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconSecureUuidPeriodScaler WriteOperation (SettingIBeaconSecureUuidPeriodScaler setting, SettingIBeaconSecureUuidPeriodScalerCompletionBlock completion);
	}

	// @interface ESTCloudOperationIBeaconNonStrictMode : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTCloudOperationIBeaconNonStrictMode")]
	interface CloudOperationIBeaconNonStrictMode : BeaconOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		CloudOperationIBeaconNonStrictMode ReadOperation (SettingIBeaconNonStrictModeCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconNonStrictMode * _Nonnull)setting completion:(ESTSettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		CloudOperationIBeaconNonStrictMode WriteOperation (SettingIBeaconNonStrictMode setting, SettingIBeaconNonStrictModeCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconMotionUUID : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMotionUUID")]
	interface BeaconOperationIBeaconMotionUuid : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMotionUUIDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconMotionUuid ReadOperation (SettingIBeaconMotionUuidCompletionBlock completion);
	}

	// @interface ESTBeaconOperationIBeaconMotionUUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMotionUUIDEnable")]
	interface BeaconOperationIBeaconMotionUuidEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationIBeaconMotionUuidEnable ReadOperation (SettingIBeaconMotionUuidEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMotionUUIDEnable * _Nonnull)setting completion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationIBeaconMotionUuidEnable WriteOperation (SettingIBeaconMotionUuidEnable setting, SettingIBeaconMotionUuidEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEstimoteLocationEnableCompletionBlock)(ESTSettingEstimoteLocationEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteLocationEnableCompletionBlock ([NullAllowed] SettingEstimoteLocationEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteLocationEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteLocationEnable")]
	interface SettingEstimoteLocationEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteLocationEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteLocationEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingEstimoteLocationEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingEstimoteLocationEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEstimoteLocationIntervalCompletionBlock)(ESTSettingEstimoteLocationInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteLocationIntervalCompletionBlock ([NullAllowed] SettingEstimoteLocationInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteLocationInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteLocationInterval")]
	interface SettingEstimoteLocationInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteLocationIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteLocationIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEstimoteLocationIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingEstimoteLocationIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingEstimoteLocationPowerCompletionBlock)(ESTSettingEstimoteLocationPower * _Nullable, NSError * _Nullable);
	delegate void SettingEstimoteLocationPowerCompletionBlock ([NullAllowed] SettingEstimoteLocationPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEstimoteLocationPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEstimoteLocationPower")]
	interface SettingEstimoteLocationPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTEstimoteLocationPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EstimoteLocationPower power);

		// -(ESTEstimoteLocationPower)getValue;
		[Export ("getValue")]
		EstimoteLocationPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEstimoteLocationPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEstimoteLocationPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEstimoteLocationPower)power completion:(ESTSettingEstimoteLocationPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EstimoteLocationPower power, SettingEstimoteLocationPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEstimoteLocationPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EstimoteLocationPower power);
	}

	// @interface ESTBeaconOperationEstimoteLocationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteLocationEnable")]
	interface BeaconOperationEstimoteLocationEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteLocationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteLocationEnable ReadOperation (SettingEstimoteLocationEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteLocationEnable * _Nonnull)setting completion:(ESTSettingEstimoteLocationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteLocationEnable WriteOperation (SettingEstimoteLocationEnable setting, SettingEstimoteLocationEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEstimoteLocationInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteLocationInterval")]
	interface BeaconOperationEstimoteLocationInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteLocationIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteLocationInterval ReadOperation (SettingEstimoteLocationIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteLocationInterval * _Nonnull)setting completion:(ESTSettingEstimoteLocationIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteLocationInterval WriteOperation (SettingEstimoteLocationInterval setting, SettingEstimoteLocationIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEstimoteLocationPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteLocationPower")]
	interface BeaconOperationEstimoteLocationPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteLocationPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteLocationPower ReadOperation (SettingEstimoteLocationPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteLocationPower * _Nonnull)setting completion:(ESTSettingEstimoteLocationPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteLocationPower WriteOperation (SettingEstimoteLocationPower setting, SettingEstimoteLocationPowerCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEstimoteTLMEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMEnable")]
	interface BeaconOperationEstimoteTlmEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteTlmEnable ReadOperation (SettingEstimoteTlmEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMEnable * _Nonnull)setting completion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteTlmEnable WriteOperation (SettingEstimoteTlmEnable setting, SettingEstimoteTlmEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEstimoteTLMInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMInterval")]
	interface BeaconOperationEstimoteTlmInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteTlmInterval ReadOperation (SettingEstimoteTlmIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMInterval * _Nonnull)setting completion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteTlmInterval WriteOperation (SettingEstimoteTlmInterval setting, SettingEstimoteTlmIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEstimoteTLMPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMPower")]
	interface BeaconOperationEstimoteTlmPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEstimoteTlmPower ReadOperation (SettingEstimoteTlmPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMPower * _Nonnull)setting completion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEstimoteTlmPower WriteOperation (SettingEstimoteTlmPower setting, SettingEstimoteTlmPowerCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneUIDEnableCompletionBlock)(ESTSettingEddystoneUIDEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUidEnableCompletionBlock ([NullAllowed] SettingEddystoneUidEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneUIDEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneUIDEnable")]
	interface SettingEddystoneUidEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUidEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingEddystoneUidEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneUIDInstanceCompletionBlock)(ESTSettingEddystoneUIDInstance * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUidInstanceCompletionBlock ([NullAllowed] SettingEddystoneUidInstance instanceIdSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneUIDInstance : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneUIDInstance")]
	interface SettingEddystoneUidInstance : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)instanceID;
		[Export ("initWithValue:")]
		IntPtr Constructor (string instanceID);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUidInstanceCompletionBlock completion);

		// -(void)writeValue:(NSString * _Nonnull)instanceID completion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (string instanceID, SettingEddystoneUidInstanceCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)instanceID;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (string instanceID);
	}

	// typedef void (^ESTSettingEddystoneUIDNamespaceCompletionBlock)(ESTSettingEddystoneUIDNamespace * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUidNamespaceCompletionBlock ([NullAllowed] SettingEddystoneUidNamespace namespaceSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneUIDNamespace : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneUIDNamespace")]
	interface SettingEddystoneUidNamespace : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)namespaceID;
		[Export ("initWithValue:")]
		IntPtr Constructor (string namespaceID);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUidNamespaceCompletionBlock completion);

		// -(void)writeValue:(NSString * _Nonnull)namespaceID completion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (string namespaceID, SettingEddystoneUidNamespaceCompletionBlock completion);

		// +(NSString * _Nonnull)namespaceHexStringForEddystoneDomain:(NSString * _Nonnull)domain;
		[Static]
		[Export ("namespaceHexStringForEddystoneDomain:")]
		string GetNamespaceHexString (string domain);

		// +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)namespaceID;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (string namespaceID);
	}

	// typedef void (^ESTSettingEddystoneUIDIntervalCompletionBlock)(ESTSettingEddystoneUIDInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUidIntervalCompletionBlock ([NullAllowed] SettingEddystoneUidInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneUIDInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneUIDInterval")]
	interface SettingEddystoneUidInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUidIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingEddystoneUidIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingEddystoneUIDPowerCompletionBlock)(ESTSettingEddystoneUIDPower * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUidPowerCompletionBlock ([NullAllowed] SettingEddystoneUidPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneUIDPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneUIDPower")]
	interface SettingEddystoneUidPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTEddystoneUIDPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EddystoneUidPower power);

		// -(ESTEddystoneUIDPower)getValue;
		[Export ("getValue")]
		EddystoneUidPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUidPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEddystoneUIDPower)power completion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EddystoneUidPower power, SettingEddystoneUidPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneUIDPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EddystoneUidPower power);
	}

	// @interface ESTBeaconOperationEddystoneUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDEnable")]
	interface BeaconOperationEddystoneUidEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUidEnable ReadOperation (SettingEddystoneUidEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDEnable * _Nonnull)setting completion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUidEnable WriteOperation (SettingEddystoneUidEnable setting, SettingEddystoneUidEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneUIDInstance : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDInstance")]
	interface BeaconOperationEddystoneUidInstance : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUidInstance ReadOperation (SettingEddystoneUidInstanceCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDInstance * _Nonnull)setting completion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUidInstance WriteOperation (SettingEddystoneUidInstance setting, SettingEddystoneUidInstanceCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneUIDNamespace : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDNamespace")]
	interface BeaconOperationEddystoneUidNamespace : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUidNamespace ReadOperation (SettingEddystoneUidNamespaceCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDNamespace * _Nonnull)setting completion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUidNamespace WriteOperation (SettingEddystoneUidNamespace setting, SettingEddystoneUidNamespaceCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneUIDInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDInterval")]
	interface BeaconOperationEddystoneUidInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUidInterval ReadOperation (SettingEddystoneUidIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDInterval * _Nonnull)setting completion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUidInterval WriteOperation (SettingEddystoneUidInterval setting, SettingEddystoneUidIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneUIDPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDPower")]
	interface BeaconOperationEddystoneUidPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUidPower ReadOperation (SettingEddystoneUidPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDPower * _Nonnull)setting completion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUidPower WriteOperation (SettingEddystoneUidPower setting, SettingEddystoneUidPowerCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneURLEnableCompletionBlock)(ESTSettingEddystoneURLEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUrlEnableCompletionBlock ([NullAllowed] SettingEddystoneUrlEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneURLEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneURLEnable")]
	interface SettingEddystoneUrlEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUrlEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingEddystoneUrlEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneURLDataCompletionBlock)(ESTSettingEddystoneURLData * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUrlDataCompletionBlock ([NullAllowed] SettingEddystoneUrlData eddystoneUrlSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneURLData : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneURLData")]
	interface SettingEddystoneUrlData : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)eddystoneURL;
		[Export ("initWithValue:")]
		IntPtr Constructor (string eddystoneURL);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUrlDataCompletionBlock completion);

		// -(void)writeValue:(NSString * _Nonnull)eddystoneURL completion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (string eddystoneURL, SettingEddystoneUrlDataCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)eddystoneURL;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (string eddystoneURL);
	}

	// typedef void (^ESTSettingEddystoneURLIntervalCompletionBlock)(ESTSettingEddystoneURLInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUrlIntervalCompletionBlock ([NullAllowed] SettingEddystoneUrlInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneURLInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneURLInterval")]
	interface SettingEddystoneUrlInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUrlIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingEddystoneUrlIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingEddystoneURLPowerCompletionBlock)(ESTSettingEddystoneURLPower * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneUrlPowerCompletionBlock ([NullAllowed] SettingEddystoneUrlPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneURLPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneURLPower")]
	interface SettingEddystoneUrlPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTEddystoneURLPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EddystoneUrlPower power);

		// -(ESTEddystoneURLPower)getValue;
		[Export ("getValue")]
		EddystoneUrlPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneUrlPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEddystoneURLPower)power completion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EddystoneUrlPower power, SettingEddystoneUrlPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneURLPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EddystoneUrlPower power);
	}

	// @interface ESTBeaconOperationEddystoneURLEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLEnable")]
	interface BeaconOperationEddystoneUrlEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUrlEnable ReadOperation (SettingEddystoneUrlEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLEnable * _Nonnull)setting completion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUrlEnable WriteOperation (SettingEddystoneUrlEnable setting, SettingEddystoneUrlEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneURLData : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLData")]
	interface BeaconOperationEddystoneUrlData : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUrlData ReadOperation (SettingEddystoneUrlDataCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLData * _Nonnull)setting completion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUrlData WriteOperation (SettingEddystoneUrlData setting, SettingEddystoneUrlDataCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneURLInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLInterval")]
	interface BeaconOperationEddystoneUrlInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUrlInterval ReadOperation (SettingEddystoneUrlIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLInterval * _Nonnull)setting completion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUrlInterval WriteOperation (SettingEddystoneUrlInterval setting, SettingEddystoneUrlIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneURLPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLPower")]
	interface BeaconOperationEddystoneUrlPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneUrlPower ReadOperation (SettingEddystoneUrlPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLPower * _Nonnull)setting completion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneUrlPower WriteOperation (SettingEddystoneUrlPower setting, SettingEddystoneUrlPowerCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneTLMEnableCompletionBlock)(ESTSettingEddystoneTLMEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneTlmEnableCompletionBlock ([NullAllowed] SettingEddystoneTlmEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneTLMEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneTLMEnable")]
	interface SettingEddystoneTlmEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneTlmEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingEddystoneTlmEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneTLMIntervalCompletionBlock)(ESTSettingEddystoneTLMInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneTlmIntervalCompletionBlock ([NullAllowed] SettingEddystoneTlmInterval advertisingIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneTLMInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneTLMInterval")]
	interface SettingEddystoneTlmInterval : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort advertisingInterval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneTlmIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort advertisingInterval, SettingEddystoneTlmIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort advertisingInterval);
	}

	// typedef void (^ESTSettingEddystoneTLMPowerCompletionBlock)(ESTSettingEddystoneTLMPower * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneTlmPowerCompletionBlock ([NullAllowed] SettingEddystoneTlmPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneTLMPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneTLMPower")]
	interface SettingEddystoneTlmPower : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTEddystoneTLMPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EddystoneTlmPower power);

		// -(ESTEddystoneTLMPower)getValue;
		[Export ("getValue")]
		EddystoneTlmPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneTlmPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEddystoneTLMPower)power completion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EddystoneTlmPower power, SettingEddystoneTlmPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneTLMPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EddystoneTlmPower power);
	}

	// @interface ESTBeaconOperationEddystoneTLMEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMEnable")]
	interface BeaconOperationEddystoneTlmEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneTlmEnable ReadOperation (SettingEddystoneTlmEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMEnable * _Nonnull)setting completion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneTlmEnable WriteOperation (SettingEddystoneTlmEnable setting, SettingEddystoneTlmEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneTLMInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMInterval")]
	interface BeaconOperationEddystoneTlmInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneTlmInterval ReadOperation (SettingEddystoneTlmIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMInterval * _Nonnull)setting completion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneTlmInterval WriteOperation (SettingEddystoneTlmInterval setting, SettingEddystoneTlmIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneTLMPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMPower")]
	interface BeaconOperationEddystoneTlmPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneTlmPower ReadOperation (SettingEddystoneTlmPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMPower * _Nonnull)setting completion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneTlmPower WriteOperation (SettingEddystoneTlmPower setting, SettingEddystoneTlmPowerCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneEIDIntervalCompletionBlock)(ESTSettingEddystoneEIDInterval * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneEidIntervalCompletionBlock ([NullAllowed] SettingEddystoneEidInterval intervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneEIDInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneEIDInterval")]
	interface SettingEddystoneEidInterval : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(unsigned short)interval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort interval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneEidIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)interval completion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort interval, SettingEddystoneEidIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)interval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort interval);
	}

	// typedef void (^ESTSettingEddystoneEIDEnableCompletionBlock)(ESTSettingEddystoneEIDEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneEidEnableCompletionBlock ([NullAllowed] SettingEddystoneEidEnable enableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneEIDEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneEIDEnable")]
	interface SettingEddystoneEidEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneEidEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enable completion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enable, SettingEddystoneEidEnableCompletionBlock completion);
	}

	// typedef void (^ESTSettingEddystoneEIDPowerCompletionBlock)(ESTSettingEddystoneEIDPower * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneEidPowerCompletionBlock ([NullAllowed] SettingEddystoneEidPower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneEIDPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneEIDPower")]
	interface SettingEddystoneEidPower : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTEddystoneEIDPower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (EddystoneEidPower power);

		// -(ESTEddystoneEIDPower)getValue;
		[Export ("getValue")]
		EddystoneEidPower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneEidPowerCompletionBlock completion);

		// -(void)writeValue:(ESTEddystoneEIDPower)power completion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (EddystoneEidPower power, SettingEddystoneEidPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneEIDPower)power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (EddystoneEidPower power);
	}

	// @interface ESTBeaconOperationEddystoneEIDInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDInterval")]
	interface BeaconOperationEddystoneEidInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneEidInterval ReadOperation (SettingEddystoneEidIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDInterval * _Nonnull)setting completion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneEidInterval WriteOperation (SettingEddystoneEidInterval setting, SettingEddystoneEidIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneEIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDEnable")]
	interface BeaconOperationEddystoneEidEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneEidEnable ReadOperation (SettingEddystoneEidEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDEnable * _Nonnull)setting completion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneEidEnable WriteOperation (SettingEddystoneEidEnable setting, SettingEddystoneEidEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationEddystoneEIDPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDPower")]
	interface BeaconOperationEddystoneEidPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationEddystoneEidPower ReadOperation (SettingEddystoneEidPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDPower * _Nonnull)setting completion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationEddystoneEidPower WriteOperation (SettingEddystoneEidPower setting, SettingEddystoneEidPowerCompletionBlock completion);
	}

	// typedef void (^ESTSettingGenericAdvertiserEnableCompletionBlock)(ESTSettingGenericAdvertiserEnable * _Nullable, NSError * _Nullable);
	delegate void SettingGenericAdvertiserEnableCompletionBlock ([NullAllowed] SettingGenericAdvertiserEnable genericAdvertiserEnabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGenericAdvertiserEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGenericAdvertiserEnable")]
	interface SettingGenericAdvertiserEnable : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull advertiserID;
		[Export ("advertiserID", ArgumentSemantic.Strong)]
		NSNumber AdvertiserId { get; }

		// -(instancetype _Nonnull)initWithValue:(BOOL)genericAdvertiserEnabled advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithValue:advertiserID:")]
		IntPtr Constructor (bool genericAdvertiserEnabled, GenericAdvertiserId advertiserID);

		// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithData:advertiserID:")]
		IntPtr Constructor (NSData data, GenericAdvertiserId advertiserID);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGenericAdvertiserEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGenericAdvertiserEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)genericAdvertiserEnabled completion:(ESTSettingGenericAdvertiserEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool genericAdvertiserEnabled, SettingGenericAdvertiserEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Static]
		[Export ("validationErrorForValue:advertiserID:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled, GenericAdvertiserId advertiserID);
	}

	// typedef void (^ESTSettingGenericAdvertiserPowerCompletionBlock)(ESTSettingGenericAdvertiserPower * _Nullable, NSError * _Nullable);
	delegate void SettingGenericAdvertiserPowerCompletionBlock ([NullAllowed] SettingGenericAdvertiserPower genericAdvertiserPowerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGenericAdvertiserPower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGenericAdvertiserPower")]
	interface SettingGenericAdvertiserPower : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull advertiserID;
		[Export ("advertiserID", ArgumentSemantic.Strong)]
		NSNumber AdvertiserId { get; }

		// -(instancetype _Nonnull)initWithValue:(ESTGenericAdvertiserPowerLevel)genericAdvertiserPower advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithValue:advertiserID:")]
		IntPtr Constructor (GenericAdvertiserPowerLevel genericAdvertiserPower, GenericAdvertiserId advertiserID);

		// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithData:advertiserID:")]
		IntPtr Constructor (NSData data, GenericAdvertiserId advertiserID);

		// -(ESTGenericAdvertiserPowerLevel)getValue;
		[Export ("getValue")]
		GenericAdvertiserPowerLevel Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGenericAdvertiserPowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGenericAdvertiserPowerCompletionBlock completion);

		// -(void)writeValue:(ESTGenericAdvertiserPowerLevel)genericAdvertiserPower completion:(ESTSettingGenericAdvertiserPowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (GenericAdvertiserPowerLevel genericAdvertiserPower, SettingGenericAdvertiserPowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTGenericAdvertiserPowerLevel)power advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Static]
		[Export ("validationErrorForValue:advertiserID:")]
		[return: NullAllowed]
		NSError GetValidationError (GenericAdvertiserPowerLevel power, GenericAdvertiserId advertiserID);
	}

	// typedef void (^ESTSettingGenericAdvertiserIntervalCompletionBlock)(ESTSettingGenericAdvertiserInterval * _Nullable, NSError * _Nullable);
	delegate void SettingGenericAdvertiserIntervalCompletionBlock ([NullAllowed] SettingGenericAdvertiserInterval genericAdvertiserIntervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGenericAdvertiserInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGenericAdvertiserInterval")]
	interface SettingGenericAdvertiserInterval : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull advertiserID;
		[Export ("advertiserID", ArgumentSemantic.Strong)]
		NSNumber AdvertiserId { get; }

		// -(instancetype _Nonnull)initWithValue:(unsigned short)genericAdvertiserInterval advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithValue:advertiserID:")]
		IntPtr Constructor (ushort genericAdvertiserInterval, GenericAdvertiserId advertiserID);

		// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithData:advertiserID:")]
		IntPtr Constructor (NSData data, GenericAdvertiserId advertiserID);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGenericAdvertiserIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGenericAdvertiserIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)genericAdvertiserInterval completion:(ESTSettingGenericAdvertiserIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort genericAdvertiserInterval, SettingGenericAdvertiserIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)interval advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Static]
		[Export ("validationErrorForValue:advertiserID:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort interval, GenericAdvertiserId advertiserID);
	}

	// typedef void (^ESTSettingGenericAdvertiserDataCompletionBlock)(ESTSettingGenericAdvertiserData * _Nullable, NSError * _Nullable);
	delegate void SettingGenericAdvertiserDataCompletionBlock ([NullAllowed] SettingGenericAdvertiserData genericAdvertiserDataSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGenericAdvertiserData : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGenericAdvertiserData")]
	interface SettingGenericAdvertiserData : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull advertiserID;
		[Export ("advertiserID", ArgumentSemantic.Strong)]
		NSNumber AdvertiserId { get; }

		// -(instancetype _Nonnull)initWithValue:(NSData * _Nonnull)genericAdvertiserData advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Export ("initWithValue:advertiserID:")]
		IntPtr Constructor (NSData genericAdvertiserData, GenericAdvertiserId advertiserID);

		//// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data advertiserID:(ESTGenericAdvertiserID)advertiserID;
		//[Export ("initWithData:advertiserID:")]
		//IntPtr Constructor (NSData data, GenericAdvertiserId advertiserID);

		// -(NSData * _Nonnull)getValue;
		[Export ("getValue")]
		NSData Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGenericAdvertiserDataCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGenericAdvertiserDataCompletionBlock completion);

		// -(void)writeValue:(NSData * _Nonnull)genericAdvertiserData completion:(ESTSettingGenericAdvertiserDataCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NSData genericAdvertiserData, SettingGenericAdvertiserDataCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSData * _Nonnull)data advertiserID:(ESTGenericAdvertiserID)advertiserID;
		[Static]
		[Export ("validationErrorForValue:advertiserID:")]
		[return: NullAllowed]
		NSError GetValidationError (NSData data, GenericAdvertiserId advertiserID);
	}

	// @interface ESTBeaconOperationGenericAdvertiserEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGenericAdvertiserEnable")]
	interface BeaconOperationGenericAdvertiserEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID completion:(ESTSettingGenericAdvertiserEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationForAdvertiser:completion:"), Async]
		BeaconOperationGenericAdvertiserEnable ReadOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID setting:(ESTSettingGenericAdvertiserEnable * _Nonnull)setting completion:(ESTSettingGenericAdvertiserEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationForAdvertiser:setting:completion:"), Async]
		BeaconOperationGenericAdvertiserEnable WriteOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserEnable setting, SettingGenericAdvertiserEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationGenericAdvertiserPower : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGenericAdvertiserPower")]
	interface BeaconOperationGenericAdvertiserPower : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID completion:(ESTSettingGenericAdvertiserPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationForAdvertiser:completion:"), Async]
		BeaconOperationGenericAdvertiserPower ReadOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserPowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID setting:(ESTSettingGenericAdvertiserPower * _Nonnull)setting completion:(ESTSettingGenericAdvertiserPowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationForAdvertiser:setting:completion:"), Async]
		BeaconOperationGenericAdvertiserPower WriteOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserPower setting, SettingGenericAdvertiserPowerCompletionBlock completion);
	}

	// @interface ESTBeaconOperationGenericAdvertiserInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGenericAdvertiserInterval")]
	interface BeaconOperationGenericAdvertiserInterval : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID completion:(ESTSettingGenericAdvertiserIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationForAdvertiser:completion:"), Async]
		BeaconOperationGenericAdvertiserInterval ReadOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID setting:(ESTSettingGenericAdvertiserInterval * _Nonnull)setting completion:(ESTSettingGenericAdvertiserIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationForAdvertiser:setting:completion:"), Async]
		BeaconOperationGenericAdvertiserInterval WriteOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserInterval setting, SettingGenericAdvertiserIntervalCompletionBlock completion);
	}

	// @interface ESTBeaconOperationGenericAdvertiserData : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGenericAdvertiserData")]
	interface BeaconOperationGenericAdvertiserData : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID completion:(ESTSettingGenericAdvertiserDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationForAdvertiser:completion:"), Async]
		BeaconOperationGenericAdvertiserData ReadOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserDataCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationForAdvertiser:(ESTGenericAdvertiserID)advertiserID setting:(ESTSettingGenericAdvertiserData * _Nonnull)setting completion:(ESTSettingGenericAdvertiserDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationForAdvertiser:setting:completion:"), Async]
		BeaconOperationGenericAdvertiserData WriteOperation (GenericAdvertiserId advertiserID, SettingGenericAdvertiserData setting, SettingGenericAdvertiserDataCompletionBlock completion);
	}

	// typedef void (^ESTSettingGPIONotificationEnableCompletionBlock)(ESTSettingGPIONotificationEnable * _Nullable, NSError * _Nullable);
	delegate void SettingGpioNotificationEnableCompletionBlock ([NullAllowed] SettingGpioNotificationEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGPIONotificationEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIONotificationEnable")]
	interface SettingGpioNotificationEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithcompletion:"), Async]
		void ReadValue (SettingGpioNotificationEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingGpioNotificationEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled);
	}

	// @interface ESTGPIOPortsData : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "ESTGPIOPortsData")]
	interface GpioPortsData : INSCopying
	{
		// -(instancetype _Nonnull)initWithPort0Value:(ESTGPIOPortValue)port0Value port1Value:(ESTGPIOPortValue)port1Value;
		[Export ("initWithPort0Value:port1Value:")]
		IntPtr Constructor (GpioPortValue port0Value, GpioPortValue port1Value);

		// -(NSError * _Nonnull)setPort:(ESTGPIOPort)port value:(ESTGPIOPortValue)value;
		[Export ("setPort:value:")]
		NSError SetPort (GpioPort port, GpioPortValue value);

		// -(ESTGPIOPortValue)getValueForPort:(ESTGPIOPort)port;
		[Export ("getValueForPort:")]
		GpioPortValue GetValue (GpioPort port);
	}

	// typedef void (^ESTSettingGPIOPortsDataCompletionBlock)(ESTSettingGPIOPortsData * _Nullable, NSError * _Nullable);
	delegate void SettingGpioPortsDataCompletionBlock ([NullAllowed] SettingGpioPortsData portsDataSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGPIOPortsData : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOPortsData")]
	interface SettingGpioPortsData : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTGPIOPortsData * _Nonnull)portsData;
		[Export ("initWithValue:")]
		IntPtr Constructor (GpioPortsData portsData);

		// -(ESTGPIOPortsData * _Nonnull)getValue;
		[Export ("getValue")]
		GpioPortsData Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGPIOPortsDataCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGpioPortsDataCompletionBlock completion);

		// -(void)writeValue:(ESTGPIOPortsData * _Nonnull)portsData completion:(ESTSettingGPIOPortsDataCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (GpioPortsData portsData, SettingGpioPortsDataCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTGPIOPortsData * _Nonnull)portsData;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (GpioPortsData portsData);
	}

	// typedef void (^ESTSettingGPIOConfigPort0CompletionBlock)(ESTSettingGPIOConfigPort0 * _Nullable, NSError * _Nullable);
	delegate void SettingGpioConfigPort0CompletionBlock ([NullAllowed] SettingGpioConfigPort0 configSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGPIOConfigPort0 : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOConfigPort0")]
	interface SettingGpioConfigPort0 : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTGPIOConfig)config;
		[Export ("initWithValue:")]
		IntPtr Constructor (GpioConfig config);

		// -(ESTGPIOConfig)getValue;
		[Export ("getValue")]
		GpioConfig Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGpioConfigPort0CompletionBlock completion);

		// -(void)writeValue:(ESTGPIOConfig)config completion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (GpioConfig config, SettingGpioConfigPort0CompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTGPIOConfig)config;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (GpioConfig config);
	}

	// typedef void (^ESTSettingGPIOConfigPort1CompletionBlock)(ESTSettingGPIOConfigPort1 * _Nullable, NSError * _Nullable);
	delegate void SettingGpioConfigPort1CompletionBlock ([NullAllowed] SettingGpioConfigPort1 configSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGPIOConfigPort1 : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOConfigPort1")]
	interface SettingGpioConfigPort1 : INSCopying
	{
		// -(instancetype _Nullable)initWithValue:(ESTGPIOConfig)config;
		[Export ("initWithValue:")]
		IntPtr Constructor (GpioConfig config);

		// -(ESTGPIOConfig)getValue;
		[Export ("getValue")]
		GpioConfig Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGpioConfigPort1CompletionBlock completion);

		// -(void)writeValue:(ESTGPIOConfig)config completion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (GpioConfig config, SettingGpioConfigPort1CompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTGPIOConfig)config;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (GpioConfig config);
	}

	// typedef void (^ESTSettingGPIO0StateReflectingOnLEDCompletionBlock)(ESTSettingGPIO0StateReflectingOnLEDEnable * _Nullable, NSError * _Nullable);
	delegate void SettingGpio0StateReflectingOnLedCompletionBlock ([NullAllowed] SettingGpio0StateReflectingOnLedEnable gpio0StateReflectingOnLedEnableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingGPIO0StateReflectingOnLEDEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIO0StateReflectingOnLEDEnable")]
	interface SettingGpio0StateReflectingOnLedEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)gpio0StateReflectingOnLEDEnable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool gpio0StateReflectingOnLEDEnable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingGPIO0StateReflectingOnLEDCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingGpio0StateReflectingOnLedCompletionBlock completion);

		// -(void)writeValue:(BOOL)gpio0StateReflectingOnLEDEnable completion:(ESTSettingGPIO0StateReflectingOnLEDCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool gpio0StateReflectingOnLEDEnable, SettingGpio0StateReflectingOnLedCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)gpio0StateReflectingOnLEDEnable;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool gpio0StateReflectingOnLEDEnable);
	}

	// @interface ESTBeaconOperationGPIONotificationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIONotificationEnable")]
	interface BeaconOperationGpioNotificationEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationGpioNotificationEnable ReadOperation (SettingGpioNotificationEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIONotificationEnable * _Nonnull)setting completion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationGpioNotificationEnable WriteOperation (SettingGpioNotificationEnable setting, SettingGpioNotificationEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationGPIOPortsData : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOPortsData")]
	interface BeaconOperationGpioPortsData : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIOPortsDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationGpioPortsData ReadOperation (SettingGpioPortsDataCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIOPortsData * _Nonnull)setting completion:(ESTSettingGPIOPortsDataCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationGpioPortsData WriteOperation (SettingGpioPortsData setting, SettingGpioPortsDataCompletionBlock completion);
	}

	// @interface ESTBeaconOperationGPIOConfigPort0 : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOConfigPort0")]
	interface BeaconOperationGpioConfigPort0 : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationGpioConfigPort0 ReadOperation (SettingGpioConfigPort0CompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIOConfigPort0 * _Nonnull)setting completion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationGpioConfigPort0 WriteOperation (SettingGpioConfigPort0 setting, SettingGpioConfigPort0CompletionBlock completion);
	}

	// @interface ESTBeaconOperationGPIOConfigPort1 : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOConfigPort1")]
	interface BeaconOperationGpioConfigPort1 : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationGpioConfigPort1 ReadOperation (SettingGpioConfigPort1CompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIOConfigPort1 * _Nonnull)setting completion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationGpioConfigPort1 WriteOperation (SettingGpioConfigPort1 setting, SettingGpioConfigPort1CompletionBlock completion);
	}

	// @interface ESTBeaconOperationGPIO0StateReflectingOnLEDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIO0StateReflectingOnLEDEnable")]
	interface BeaconOperationGpio0StateReflectingOnLedEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIO0StateReflectingOnLEDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationGpio0StateReflectingOnLedEnable ReadOperation (SettingGpio0StateReflectingOnLedCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIO0StateReflectingOnLEDEnable * _Nonnull)setting completion:(ESTSettingGPIO0StateReflectingOnLEDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationGpio0StateReflectingOnLedEnable WriteOperation (SettingGpio0StateReflectingOnLedEnable setting, SettingGpio0StateReflectingOnLedCompletionBlock completion);
	}

	// typedef void (^ESTSettingSensorsAmbientLightCompletionBlock)(ESTSettingSensorsAmbientLight * _Nullable, NSError * _Nullable);
	delegate void SettingSensorsAmbientLightCompletionBlock ([NullAllowed] SettingSensorsAmbientLight ambientLightSetting, [NullAllowed] NSError error);

	// @interface ESTSettingSensorsAmbientLight : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingSensorsAmbientLight")]
	interface SettingSensorsAmbientLight : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSUInteger)ambientLight;
		[Export ("initWithValue:")]
		IntPtr Constructor (nuint ambientLight);

		// -(NSUInteger)getValue;
		[Export ("getValue")]
		nuint Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingSensorsAmbientLightCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingSensorsAmbientLightCompletionBlock completion);
	}

	// typedef void (^ESTSettingSensorsMotionNotificationEnableCompletionBlock)(ESTSettingSensorsMotionNotificationEnable * _Nullable, NSError * _Nullable);
	delegate void SettingSensorsMotionNotificationEnableCompletionBlock ([NullAllowed] SettingSensorsMotionNotificationEnable enabledSetting, [NullAllowed] NSError error);

	// @interface ESTSettingSensorsMotionNotificationEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingSensorsMotionNotificationEnable")]
	interface SettingSensorsMotionNotificationEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool enabled);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingSensorsMotionNotificationEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingSensorsMotionNotificationEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)enabled completion:(ESTSettingSensorsMotionNotificationEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool enabled, SettingSensorsMotionNotificationEnableCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (bool enabled);
	}

	// typedef void (^ESTSettingSensorsTemperatureCompletionBlock)(ESTSettingSensorsTemperature * _Nullable, NSError * _Nullable);
	delegate void SettingSensorsTemperatureCompletionBlock ([NullAllowed] SettingSensorsTemperature temperatureSetting, [NullAllowed] NSError error);

	// @interface ESTSettingSensorsTemperature : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingSensorsTemperature")]
	interface SettingSensorsTemperature : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(float)temperature;
		[Export ("initWithValue:")]
		IntPtr Constructor (float temperature);

		// -(float)getValue;
		[Export ("getValue")]
		float Value { get; }

		// -(float)getValueInFahrenheit;
		[Export ("getValueInFahrenheit")]
		float ValueInFahrenheit { get; }

		// -(void)readValueWithCompletion:(ESTSettingSensorsTemperatureCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingSensorsTemperatureCompletionBlock completion);
	}

	// typedef void (^ESTSettingSensorsPressureCompletionBlock)(ESTSettingSensorsPressure * _Nullable, NSError * _Nullable);
	delegate void SettingSensorsPressureCompletionBlock ([NullAllowed] SettingSensorsPressure pressureSetting, [NullAllowed] NSError error);

	// @interface ESTSettingSensorsPressure : ESTSettingReadOnly <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingReadOnly), Name = "ESTSettingSensorsPressure")]
	interface SettingSensorsPressure : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSUInteger)pressure;
		[Export ("initWithValue:")]
		IntPtr Constructor (nuint pressure);

		// -(NSUInteger)getValue;
		[Export ("getValue")]
		nuint Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingSensorsPressureCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingSensorsPressureCompletionBlock completion);
	}

	// @interface ESTBeaconOperationSensorsAmbientLight : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsAmbientLight")]
	interface BeaconOperationSensorsAmbientLight : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsAmbientLightCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationSensorsAmbientLight ReadOperation (SettingSensorsAmbientLightCompletionBlock completion);
	}

	// @interface ESTBeaconOperationSensorsMotionNotificationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsMotionNotificationEnable")]
	interface BeaconOperationSensorsMotionNotificationEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsMotionNotificationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationSensorsMotionNotificationEnable ReadOperation (SettingSensorsMotionNotificationEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingSensorsMotionNotificationEnable * _Nonnull)setting completion:(ESTSettingSensorsMotionNotificationEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		BeaconOperationSensorsMotionNotificationEnable WriteOperation (SettingSensorsMotionNotificationEnable setting, SettingSensorsMotionNotificationEnableCompletionBlock completion);
	}

	// @interface ESTBeaconOperationSensorsTemperature : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsTemperature")]
	interface BeaconOperationSensorsTemperature : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsTemperatureCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationSensorsTemperature ReadOperation (SettingSensorsTemperatureCompletionBlock completion);
	}

	// @interface ESTBeaconOperationSensorsPressure : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsPressure")]
	interface BeaconOperationSensorsPressure : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsPressureCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		BeaconOperationSensorsPressure ReadOperation (SettingSensorsPressureCompletionBlock completion);
	}

	// typedef void (^ESTNotificationMotionBlock)(BOOL);
	delegate void NotificationMotionBlock (bool inMotion);

	// @interface ESTNotificationMotion : NSObject <ESTDeviceNotificationProtocol, NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTNotificationMotion")]
	interface NotificationMotion : DeviceNotificationProtocol, INSCopying
	{
		// -(instancetype _Nonnull)initWithHandlerBlock:(ESTNotificationMotionBlock _Nonnull)handler;
		[Export ("initWithHandlerBlock:")]
		IntPtr Constructor (NotificationMotionBlock handler);
	}

	// typedef void (^ESTNotificationGPIODataBlock)(ESTGPIOPortsData * _Nonnull);
	delegate void NotificationGpioDataBlock (GpioPortsData portsData);

	// @interface ESTNotificationGPIOData : NSObject <ESTDeviceNotificationProtocol, NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTNotificationGPIOData")]
	interface NotificationGpioData : DeviceNotificationProtocol, INSCopying
	{
		// -(instancetype _Nonnull)initWithHandlerBlock:(ESTNotificationGPIODataBlock _Nonnull)handler;
		[Export ("initWithHandlerBlock:")]
		IntPtr Constructor (NotificationGpioDataBlock handler);
	}

	// typedef void (^ESTSettingEddystoneConfigurationServiceEnableCompletionBlock)(ESTSettingEddystoneConfigurationServiceEnable * _Nullable, NSError * _Nullable);
	delegate void SettingEddystoneConfigurationServiceEnableCompletionBlock ([NullAllowed] SettingEddystoneConfigurationServiceEnable eddystoneConfigurationServiceEnableSetting, [NullAllowed] NSError error);

	// @interface ESTSettingEddystoneConfigurationServiceEnable : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingEddystoneConfigurationServiceEnable")]
	interface SettingEddystoneConfigurationServiceEnable : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(BOOL)eddystoneConfigurationServiceEnable;
		[Export ("initWithValue:")]
		IntPtr Constructor (bool eddystoneConfigurationServiceEnable);

		// -(BOOL)getValue;
		[Export ("getValue")]
		bool Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingEddystoneConfigurationServiceEnableCompletionBlock completion);

		// -(void)writeValue:(BOOL)eddystoneConfigurationServiceEnable completion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (bool eddystoneConfigurationServiceEnable, SettingEddystoneConfigurationServiceEnableCompletionBlock completion);
	}

	// @interface ESTNearableDefinitions : ESTDefinitions
	[BaseType (typeof (Definitions), Name = "ESTNearableDefinitions")]
	interface NearableDefinitions
	{
		// +(NSString * _Nonnull)nameForType:(ESTNearableType)type;
		[Static]
		[Export ("nameForType:")]
		string NameForType (NearableType type);

		// +(NSString * _Nonnull)nameForNearableBroadcastingScheme:(ESTNearableBroadcastingScheme)scheme;
		[Static]
		[Export ("nameForNearableBroadcastingScheme:")]
		string NameForNearableBroadcastingScheme (NearableBroadcastingScheme scheme);
	}

	// @interface ESTNearable : NSObject <NSCopying, NSCoding>
	[BaseType (typeof (NSObject), Name = "ESTNearable")]
	interface Nearable : INSCopying, INSCoding
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, assign, nonatomic) ESTNearableZone zone;
		[Export ("zone", ArgumentSemantic.Assign)]
		NearableZone Zone { get; }

		// @property (readonly, assign, nonatomic) ESTNearableType type;
		[Export ("type", ArgumentSemantic.Assign)]
		NearableType Type { get; }

		// @property (readonly, assign, nonatomic) ESTColor color;
		[Export ("color", ArgumentSemantic.Assign)]
		Color Color { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull hardwareVersion;
		[Export ("hardwareVersion", ArgumentSemantic.Strong)]
		string HardwareVersion { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull firmwareVersion;
		[Export ("firmwareVersion", ArgumentSemantic.Strong)]
		string FirmwareVersion { get; }

		// @property (readonly, assign, nonatomic) NSInteger rssi;
		[Export ("rssi")]
		nint Rssi { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable idleBatteryVoltage;
		[NullAllowed, Export ("idleBatteryVoltage", ArgumentSemantic.Strong)]
		NSNumber IdleBatteryVoltage { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable stressBatteryVoltage;
		[NullAllowed, Export ("stressBatteryVoltage", ArgumentSemantic.Strong)]
		NSNumber StressBatteryVoltage { get; }

		// @property (readonly, assign, nonatomic) unsigned long long currentMotionStateDuration;
		[Export ("currentMotionStateDuration")]
		ulong CurrentMotionStateDuration { get; }

		// @property (readonly, assign, nonatomic) unsigned long long previousMotionStateDuration;
		[Export ("previousMotionStateDuration")]
		ulong PreviousMotionStateDuration { get; }

		// @property (readonly, assign, nonatomic) BOOL isMoving;
		[Export ("isMoving")]
		bool IsMoving { get; }

		// @property (readonly, assign, nonatomic) ESTNearableOrientation orientation;
		[Export ("orientation", ArgumentSemantic.Assign)]
		NearableOrientation Orientation { get; }

		// @property (readonly, assign, nonatomic) NSInteger xAcceleration;
		[Export ("xAcceleration")]
		nint XAcceleration { get; }

		// @property (readonly, assign, nonatomic) NSInteger yAcceleration;
		[Export ("yAcceleration")]
		nint YAcceleration { get; }

		// @property (readonly, assign, nonatomic) NSInteger zAcceleration;
		[Export ("zAcceleration")]
		nint ZAcceleration { get; }

		// @property (readonly, assign, nonatomic) double temperature;
		[Export ("temperature")]
		double Temperature { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		NSNumber Power { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull advInterval;
		[Export ("advInterval", ArgumentSemantic.Strong)]
		NSNumber AdvInterval { get; }

		// @property (readonly, assign, nonatomic) ESTNearableFirmwareState firmwareState;
		[Export ("firmwareState", ArgumentSemantic.Assign)]
		NearableFirmwareState FirmwareState { get; }

		// @property (readonly, assign, nonatomic) ESTNearableBroadcastingScheme broadcastingScheme;
		[Export ("broadcastingScheme", ArgumentSemantic.Assign)]
		NearableBroadcastingScheme BroadcastingScheme { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull eddystoneURL;
		[Export ("eddystoneURL", ArgumentSemantic.Strong)]
		string EddystoneUrl { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		string ProximityUid { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// -(CLBeaconRegion * _Nonnull)beaconRegion;
		[Export ("beaconRegion")]
		CLBeaconRegion BeaconRegion { get; }

		// -(BOOL)isMotionBroken;
		[Export ("isMotionBroken")]
		bool IsMotionBroken { get; }

		// -(BOOL)isTemperatureBroken;
		[Export ("isTemperatureBroken")]
		bool IsTemperatureBroken { get; }
	}

	interface INearableManagerDelegate { }

	// @protocol ESTNearableManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTNearableManagerDelegate")]
	interface NearableManagerDelegate
	{
		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didRangeNearables:(NSArray<ESTNearable *> * _Nonnull)nearables withType:(ESTNearableType)type;
		[Export ("nearableManager:didRangeNearables:withType:"), EventArgs ("NearableManagerRangedNearables")]
		void RangedNearables (NearableManager manager, Nearable[] nearables, NearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didRangeNearable:(ESTNearable * _Nonnull)nearable;
		[Export ("nearableManager:didRangeNearable:"), EventArgs ("NearableManagerRangedNearable")]
		void RangedNearable (NearableManager manager, Nearable nearable);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager rangingFailedWithError:(NSError * _Nonnull)error;
		[Export ("nearableManager:rangingFailedWithError:"), EventArgs ("NearableManagerRangingFailed")]
		void RangingFailed (NearableManager manager, NSError error);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didEnterTypeRegion:(ESTNearableType)type;
		[Export ("nearableManager:didEnterTypeRegion:"), EventArgs ("NearableManagerEnteredTypeRegion")]
		void EnteredTypeRegion (NearableManager manager, NearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didExitTypeRegion:(ESTNearableType)type;
		[Export ("nearableManager:didExitTypeRegion:"), EventArgs ("NearableManagerExitedTypeRegion")]
		void ExitedTypeRegion (NearableManager manager, NearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didEnterIdentifierRegion:(NSString * _Nonnull)identifier;
		[Export ("nearableManager:didEnterIdentifierRegion:"), EventArgs ("NearableManagerEnteredIdentifierRegion")]
		void EnteredIdentifierRegion (NearableManager manager, string identifier);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager didExitIdentifierRegion:(NSString * _Nonnull)identifier;
		[Export ("nearableManager:didExitIdentifierRegion:"), EventArgs ("NearableManagerExitedIdentifierRegion")]
		void ExitedIdentifierRegion (NearableManager manager, string identifier);

		// @optional -(void)nearableManager:(ESTNearableManager * _Nonnull)manager monitoringFailedWithError:(NSError * _Nonnull)error;
		[Export ("nearableManager:monitoringFailedWithError:"), EventArgs ("NearableManagerMonitoringFailed")]
		void MonitoringFailed (NearableManager manager, NSError error);
	}

	// @interface ESTNearableManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTNearableManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (NearableManagerDelegate) })]
	interface NearableManager
	{
		// @property (nonatomic, weak) id<ESTNearableManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		INearableManagerDelegate Delegate { get; set; }

		// -(void)startMonitoringForIdentifier:(NSString * _Nonnull)identifier;
		[Export ("startMonitoringForIdentifier:")]
		void StartMonitoring (string identifier);

		// -(void)stopMonitoringForIdentifier:(NSString * _Nonnull)identifier;
		[Export ("stopMonitoringForIdentifier:")]
		void StopMonitoring (string identifier);

		// -(void)startMonitoringForType:(ESTNearableType)type;
		[Export ("startMonitoringForType:")]
		void StartMonitoring (NearableType type);

		// -(void)stopMonitoringForType:(ESTNearableType)type;
		[Export ("stopMonitoringForType:")]
		void StopMonitoring (NearableType type);

		// -(void)stopMonitoring;
		[Export ("stopMonitoring")]
		void StopMonitoring ();

		// -(void)startRangingForIdentifier:(NSString * _Nonnull)identifier;
		[Export ("startRangingForIdentifier:")]
		void StartRanging (string identifier);

		// -(void)stopRangingForIdentifier:(NSString * _Nonnull)identifier;
		[Export ("stopRangingForIdentifier:")]
		void StopRanging (string identifier);

		// -(void)startRangingForType:(ESTNearableType)type;
		[Export ("startRangingForType:")]
		void StartRanging (NearableType type);

		// -(void)stopRangingForType:(ESTNearableType)type;
		[Export ("stopRangingForType:")]
		void StopRanging (NearableType type);

		// -(void)stopRanging;
		[Export ("stopRanging")]
		void StopRanging ();
	}

	// @interface ESTSimulatedNearableManager : ESTNearableManager <ESTNearableManagerDelegate>
	[DisableDefaultCtor]
	[BaseType (typeof (NearableManager), Name = "ESTSimulatedNearableManager")]
	interface SimulatedNearableManager : NearableManagerDelegate
	{
		// @property (readonly, nonatomic, strong) NSMutableArray<ESTNearable *> * _Nonnull nearables;
		[Export ("nearables", ArgumentSemantic.Strong)]
		NSMutableArray<Nearable> Nearables { get; }

		// -(instancetype _Nonnull)initWithDelegate:(id<ESTNearableManagerDelegate> _Nullable)delegate;
		[Export ("initWithDelegate:")]
		IntPtr Constructor ([NullAllowed] INearableManagerDelegate @delegate);

		// -(instancetype _Nonnull)initWithDelegate:(id<ESTNearableManagerDelegate> _Nullable)delegate pathForJSON:(NSString * _Nonnull)path;
		[Export ("initWithDelegate:pathForJSON:")]
		IntPtr Constructor ([NullAllowed] INearableManagerDelegate @delegate, string path);

		// -(ESTNearable * _Nonnull)generateRandomNearableAndAddToSimulator:(BOOL)add;
		[Export ("generateRandomNearableAndAddToSimulator:")]
		Nearable GenerateRandomNearableAndAddToSimulator (bool add);

		// -(void)addNearableToSimulation:(NSString * _Nonnull)identifier withType:(ESTNearableType)type zone:(ESTNearableZone)zone rssi:(NSInteger)rssi;
		[Export ("addNearableToSimulation:withType:zone:rssi:")]
		void AddNearable (string identifier, NearableType type, NearableZone zone, nint rssi);

		// -(void)addNearablesToSimulationWithPath:(NSString * _Nonnull)path;
		[Export ("addNearablesToSimulationWithPath:")]
		void AddNearablesToSimulation (string path);

		// -(void)simulateZone:(ESTNearableZone)zone forNearable:(NSString * _Nonnull)identifier;
		[Export ("simulateZone:forNearable:")]
		void SimulateZone (NearableZone zone, string identifier);

		// -(void)simulateDidEnterRegionForNearable:(ESTNearable * _Nonnull)nearable;
		[Export ("simulateDidEnterRegionForNearable:")]
		void SimulateEnteredRegion (Nearable nearable);

		// -(void)simulateDidExitRegionForNearable:(ESTNearable * _Nonnull)nearable;
		[Export ("simulateDidExitRegionForNearable:")]
		void SimulateExitedRegion (Nearable nearable);
	}

	interface INearableOperationProtocol { }

	// @protocol ESTNearableOperationProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTNearableOperationProtocol")]
	interface NearableOperationProtocol
	{
		// @required -(ESTSettingOperationType)type;
		[Abstract]
		[Export ("type")]
		SettingOperationType Type { get; }

		// @required -(ESTSettingStorageType)storageType;
		[Abstract]
		[Export ("storageType")]
		SettingStorageType StorageType { get; }

		// @required -(uint16_t)registerID;
		[Abstract]
		[Export ("registerID")]
		ushort RegisterId { get; }

		// @required -(NSData * _Nonnull)valueData;
		[Abstract]
		[Export ("valueData")]
		NSData ValueData { get; }

		// @required -(NSInteger)valueDataSize;
		[Abstract]
		[Export ("valueDataSize")]
		nint ValueDataSize { get; }

		// @required -(NSString * _Nonnull)supportedFirmwareVersion;
		[Abstract]
		[Export ("supportedFirmwareVersion")]
		string SupportedFirmwareVersion { get; }

		// @required -(ESTSettingBase * _Nonnull)getSetting;
		[Abstract]
		[Export ("getSetting")]
		SettingBase Setting { get; }

		// @required -(void)updateSetting:(ESTSettingBase * _Nonnull)setting;
		[Abstract]
		[Export ("updateSetting:")]
		void UpdateSetting (SettingBase setting);

		// @required -(Class _Nonnull)settingClass;
		[Abstract]
		[Export ("settingClass")]
		Class SettingClass { get; }

		// @required -(BOOL)shouldSynchronize;
		[Abstract]
		[Export ("shouldSynchronize")]
		bool ShouldSynchronize { get; }

		// @required -(void)fireSuccessBlock;
		[Abstract]
		[Export ("fireSuccessBlock")]
		void FireSuccessBlock ();

		// @required -(void)fireFailureBlockWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("fireFailureBlockWithError:")]
		void FireFailureBlock (NSError error);
	}

	interface IPeripheralNearableDelegate { }

	// @protocol ESTPeripheralNearableDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTPeripheralNearableDelegate")]
	interface PeripheralNearableDelegate
	{
		// @required -(void)peripheral:(id<ESTPeripheral>)peripheral didPerformOperation:(id<ESTNearableOperationProtocol>)operation andReceivedData:(NSData *)data;
		[Abstract]
		[Export ("peripheral:didPerformOperation:andReceivedData:"), EventArgs ("PeripheralNearablePerformedOperation")]
		void PerformedOperation (IPeripheral peripheral, INearableOperationProtocol operation, NSData data);

		// @required -(void)peripheral:(id<ESTPeripheral>)peripheral didFailOperation:(id<ESTNearableOperationProtocol>)operation withError:(NSError *)error;
		[Abstract]
		[Export ("peripheral:didFailOperation:withError:"), EventArgs ("PeripheralNearableOperationFailed")]
		void OperationFailed (IPeripheral peripheral, INearableOperationProtocol operation, NSError error);
	}

	// @interface ESTPeripheralNearable : NSObject <ESTPeripheral>
	[BaseType (typeof (NSObject), Name = "ESTPeripheralNearable", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (PeripheralNearableDelegate) })]
	interface PeripheralNearable : Peripheral
	{
		// @property (readonly, nonatomic) CBPeripheral * peripheral;
		[Export ("peripheral")]
		CBPeripheral Peripheral { get; }

		// @property (nonatomic, weak) id<ESTPeripheralNearableDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPeripheralNearableDelegate Delegate { get; set; }

		// -(void)readAuthorizationSeedWithCompletion:(ESTObjectCompletionBlock)completion;
		[Export ("readAuthorizationSeedWithCompletion:"), Async]
		void ReadAuthorizationSeed (ObjectCompletionBlock completion);

		// -(void)writeAuthorizationSeed:(NSData *)seed completion:(ESTObjectCompletionBlock)completion;
		[Export ("writeAuthorizationSeed:completion:"), Async]
		void WriteAuthorizationSeed (NSData seed, ObjectCompletionBlock completion);

		// -(void)writeAuthorizationSource:(NSData *)seed completion:(ESTObjectCompletionBlock)completion;
		[Export ("writeAuthorizationSource:completion:"), Async]
		void WriteAuthorizationSource (NSData seed, ObjectCompletionBlock completion);

		// -(void)performNearableOperation:(id<ESTNearableOperationProtocol>)nearableOperation;
		[Export ("performNearableOperation:")]
		void PerformNearableOperation (INearableOperationProtocol nearableOperation);

		// -(void)otaEraseWithCompletion:(ESTCompletionBlock)completion;
		[Export ("otaEraseWithCompletion:"), Async]
		void OtaErase (CompletionBlock completion);

		// -(void)otaVerifyWithCompletion:(ESTCompletionBlock)completion;
		[Export ("otaVerifyWithCompletion:"), Async]
		void OtaVerify (CompletionBlock completion);

		// -(void)otaWriteFirmwareChunk:(NSData *)chunk completion:(ESTObjectCompletionBlock)completion;
		[Export ("otaWriteFirmwareChunk:completion:"), Async]
		void OtaWriteFirmwareChunk (NSData chunk, ObjectCompletionBlock completion);

		// -(void)otaCommandReboot;
		[Export ("otaCommandReboot")]
		void OtaCommandReboot ();
	}

	// @interface ESTNearableSettingsManager : NSObject <ESTPeripheralNearableDelegate>
	[BaseType (typeof (NSObject), Name = "ESTNearableSettingsManager")]
	interface NearableSettingsManager : PeripheralNearableDelegate
	{
		// @property (readonly, nonatomic, strong) ESTDeviceSettingsCollection * _Nonnull settingsCollection;
		[Export ("settingsCollection", ArgumentSemantic.Strong)]
		DeviceSettingsCollection SettingsCollection { get; }

		// -(void)performOperation:(id<ESTNearableOperationProtocol> _Nonnull)operation;
		[Export ("performOperation:")]
		void PerformOperation (INearableOperationProtocol operation);

		// -(void)performOperationsFromArray:(NSArray<id<ESTNearableOperationProtocol>> * _Nonnull)operations;
		[Export ("performOperationsFromArray:")]
		void PerformOperations (INearableOperationProtocol[] operations);

		// Copied From ESTNearableSettingsManager_Internal
		// -(instancetype _Nonnull)initWithDevice:(ESTDeviceNearable * _Nonnull)device peripheral:(ESTPeripheralNearable * _Nonnull)peripheral;
		[Export ("initWithDevice:peripheral:")]
		IntPtr Constructor (DeviceNearable device, PeripheralNearable peripheral);
	}

	// @interface Internal (ESTNearableSettingsManager)
	[Category]
	[BaseType (typeof (NearableSettingsManager))]
	interface ESTNearableSettingsManager_Internal
	{
		// Moved to NearableSettingsManager
		//// -(instancetype _Nonnull)initWithDevice:(ESTDeviceNearable * _Nonnull)device peripheral:(ESTPeripheralNearable * _Nonnull)peripheral;
		//[Export ("initWithDevice:peripheral:")]
		//IntPtr Constructor (DeviceNearable device, PeripheralNearable peripheral);

		// -(void)synchronizeUsingNearableVO:(ESTNearableVO * _Nonnull)nearableVO forFirmwareVersion:(NSString * _Nonnull)firmwareVersion completion:(void (^ _Nonnull)(void))completion;
		[Export ("synchronizeUsingNearableVO:forFirmwareVersion:completion:")]
		void SynchronizeUsingNearableVO (NearableVO nearableVO, string firmwareVersion, Action completion);
	}

	// typedef void (^ESTSettingNearableIntervalCompletionBlock)(ESTSettingNearableInterval * _Nullable, NSError * _Nullable);
	delegate void SettingNearableIntervalCompletionBlock ([NullAllowed] SettingNearableInterval intervalSetting, [NullAllowed] NSError error);

	// @interface ESTSettingNearableInterval : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingNearableInterval")]
	interface SettingNearableInterval : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(unsigned short)interval;
		[Export ("initWithValue:")]
		IntPtr Constructor (ushort interval);

		// -(unsigned short)getValue;
		[Export ("getValue")]
		ushort Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingNearableIntervalCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingNearableIntervalCompletionBlock completion);

		// -(void)writeValue:(unsigned short)interval completion:(ESTSettingNearableIntervalCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (ushort interval, SettingNearableIntervalCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(unsigned short)interval;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (ushort interval);
	}

	// typedef void (^ESTSettingNearablePowerCompletionBlock)(ESTSettingNearablePower * _Nullable, NSError * _Nullable);
	delegate void SettingNearablePowerCompletionBlock ([NullAllowed] SettingNearablePower powerSetting, [NullAllowed] NSError error);

	// @interface ESTSettingNearablePower : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingNearablePower")]
	interface SettingNearablePower : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTNearablePower)power;
		[Export ("initWithValue:")]
		IntPtr Constructor (NearablePower power);

		// -(ESTNearablePower)getValue;
		[Export ("getValue")]
		NearablePower Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingNearablePowerCompletionBlock completion);

		// -(void)writeValue:(ESTNearablePower)power completion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NearablePower power, SettingNearablePowerCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTNearablePower)Power;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (NearablePower Power);
	}

	// @interface ESTNearableOperationNearableInterval : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationNearableInterval")]
	interface NearableOperationNearableInterval : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingNearableIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationNearableInterval ReadOperation (SettingNearableIntervalCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingNearableInterval * _Nonnull)setting completion:(ESTSettingNearableIntervalCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationNearableInterval WriteOperation (SettingNearableInterval setting, SettingNearableIntervalCompletionBlock completion);
	}

	// @interface ESTNearableOperationNearablePower : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationNearablePower")]
	interface NearableOperationNearablePower : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationNearablePower ReadOperation (SettingNearablePowerCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingNearablePower * _Nonnull)setting completion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationNearablePower WriteOperation (SettingNearablePower setting, SettingNearablePowerCompletionBlock completion);
	}

	// @interface ESTNearableOperationName : ESTSettingOperation <ESTNearableOperationProtocol, ESTCloudOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationName")]
	interface NearableOperationName : NearableOperationProtocol, CloudOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationName ReadOperation (SettingDeviceInfoNameCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoName * _Nonnull)setting completion:(ESTSettingDeviceInfoNameCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationName WriteOperation (SettingDeviceInfoName setting, SettingDeviceInfoNameCompletionBlock completion);
	}

	// @interface ESTNearableOperationApplicationVersion : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationApplicationVersion")]
	interface NearableOperationApplicationVersion : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoApplicationVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationApplicationVersion ReadOperation (SettingDeviceInfoApplicationVersionCompletionBlock completion);
	}

	// @interface ESTNearableOperationHardware : ESTSettingOperation <ESTBeaconOperationProtocol, ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationHardware")]
	interface NearableOperationHardware : BeaconOperationProtocol, NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoHardwareVersionCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationHardware ReadOperation (SettingDeviceInfoHardwareVersionCompletionBlock completion);
	}

	// @interface ESTNearableOperationMotionOnly : ESTSettingOperation <ESTBeaconOperationProtocol, ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationMotionOnly")]
	interface NearableOperationMotionOnly : BeaconOperationProtocol, NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationMotionOnly ReadOperation (SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingPowerMotionOnlyBroadcastingEnable * _Nonnull)setting completion:(ESTSettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationMotionOnly WriteOperation (SettingPowerMotionOnlyBroadcastingEnable setting, SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);
	}

	// @interface ESTNearableOperationIBeaconProximityUUID : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationIBeaconProximityUUID")]
	interface NearableOperationIBeaconProximityUuid : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationIBeaconProximityUuid ReadOperation (SettingIBeaconProximityUuidCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconProximityUUID * _Nonnull)setting completion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationIBeaconProximityUuid WriteOperation (SettingIBeaconProximityUuid setting, SettingIBeaconProximityUuidCompletionBlock completion);
	}

	// @interface ESTNearableOperationIBeaconMajor : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationIBeaconMajor")]
	interface NearableOperationIBeaconMajor : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationIBeaconMajor ReadOperation (SettingIBeaconMajorCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMajor * _Nonnull)setting completion:(ESTSettingIBeaconMajorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationIBeaconMajor WriteOperation (SettingIBeaconMajor setting, SettingIBeaconMajorCompletionBlock completion);
	}

	// @interface ESTNearableOperationIBeaconMinor : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationIBeaconMinor")]
	interface NearableOperationIBeaconMinor : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationIBeaconMinor ReadOperation (SettingIBeaconMinorCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMinor * _Nonnull)setting completion:(ESTSettingIBeaconMinorCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationIBeaconMinor WriteOperation (SettingIBeaconMinor setting, SettingIBeaconMinorCompletionBlock completion);
	}

	// typedef void (^ESTSettingNearableEddystoneURLCompletionBlock)(ESTSettingNearableEddystoneURL * _Nullable, NSError * _Nullable);
	delegate void SettingNearableEddystoneUrlCompletionBlock ([NullAllowed] SettingNearableEddystoneUrl eddystoneUrlSetting, [NullAllowed] NSError error);

	// @interface ESTSettingNearableEddystoneURL : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingNearableEddystoneURL")]
	interface SettingNearableEddystoneUrl : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(NSString * _Nonnull)eddystoneURL;
		[Export ("initWithValue:")]
		IntPtr Constructor (string eddystoneURL);

		// -(NSString * _Nonnull)getValue;
		[Export ("getValue")]
		string Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingNearableEddystoneURLCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingNearableEddystoneUrlCompletionBlock completion);

		// -(void)writeValue:(NSString * _Nonnull)eddystoneURL completion:(ESTSettingNearableEddystoneURLCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (string eddystoneURL, SettingNearableEddystoneUrlCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)eddystoneURL;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (string eddystoneURL);
	}

	// @interface ESTNearableOperationEddystoneURL : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationEddystoneURL")]
	interface NearableOperationEddystoneUrl : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingNearableEddystoneURLCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationEddystoneUrl ReadOperation (SettingNearableEddystoneUrlCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingNearableEddystoneURL * _Nonnull)setting completion:(ESTSettingNearableEddystoneURLCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationEddystoneUrl WriteOperation (SettingNearableEddystoneUrl setting, SettingNearableEddystoneUrlCompletionBlock completion);
	}

	// typedef void (^ESTSettingNearableBroadcastingSchemeCompletionBlock)(ESTSettingNearableBroadcastingScheme * _Nullable, NSError * _Nullable);
	delegate void SettingNearableBroadcastingSchemeCompletionBlock ([NullAllowed] SettingNearableBroadcastingScheme broadcastingSchemeSetting, [NullAllowed] NSError error);

	// @interface ESTSettingNearableBroadcastingScheme : ESTSettingReadWrite <NSCopying>
	[BaseType (typeof (SettingReadWrite), Name = "ESTSettingNearableBroadcastingScheme")]
	interface SettingNearableBroadcastingScheme : INSCopying
	{
		// -(instancetype _Nonnull)initWithValue:(ESTNearableBroadcastingScheme)broadcastingScheme;
		[Export ("initWithValue:")]
		IntPtr Constructor (NearableBroadcastingScheme broadcastingScheme);

		// -(ESTNearableBroadcastingScheme)getValue;
		[Export ("getValue")]
		NearableBroadcastingScheme Value { get; }

		// -(void)readValueWithCompletion:(ESTSettingNearableBroadcastingSchemeCompletionBlock _Nonnull)completion;
		[Export ("readValueWithCompletion:"), Async]
		void ReadValue (SettingNearableBroadcastingSchemeCompletionBlock completion);

		// -(void)writeValue:(ESTNearableBroadcastingScheme)broadcastingScheme completion:(ESTSettingNearableBroadcastingSchemeCompletionBlock _Nonnull)completion;
		[Export ("writeValue:completion:"), Async]
		void WriteValue (NearableBroadcastingScheme broadcastingScheme, SettingNearableBroadcastingSchemeCompletionBlock completion);

		// +(NSError * _Nullable)validationErrorForValue:(ESTNearableBroadcastingScheme)broadcastingScheme;
		[Static]
		[Export ("validationErrorForValue:")]
		[return: NullAllowed]
		NSError GetValidationError (NearableBroadcastingScheme broadcastingScheme);
	}

	// @interface ESTNearableOperationBroadcastingScheme : ESTSettingOperation <ESTNearableOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTNearableOperationBroadcastingScheme")]
	interface NearableOperationBroadcastingScheme : NearableOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingNearableBroadcastingSchemeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:"), Async]
		NearableOperationBroadcastingScheme ReadOperation (SettingNearableBroadcastingSchemeCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingNearableBroadcastingScheme * _Nonnull)setting completion:(ESTSettingNearableBroadcastingSchemeCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:"), Async]
		NearableOperationBroadcastingScheme WriteOperation (SettingNearableBroadcastingScheme setting, SettingNearableBroadcastingSchemeCompletionBlock completion);
	}

	// @interface ESTBeaconUpdateConfig : NSObject <NSCoding, NSCopying>
	[BaseType (typeof (NSObject), Name = "ESTBeaconUpdateConfig")]
	interface BeaconUpdateConfig : INSCoding, INSCopying
	{
		// @property (nonatomic, strong) NSString * _Nullable proximityUUID;
		[NullAllowed, Export ("proximityUUID", ArgumentSemantic.Strong)]
		string ProximityUuid { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable major;
		[NullAllowed, Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable minor;
		[NullAllowed, Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable advInterval;
		[NullAllowed, Export ("advInterval", ArgumentSemantic.Strong)]
		NSNumber AdvInterval { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable power;
		[NullAllowed, Export ("power", ArgumentSemantic.Strong)]
		NSNumber Power { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable basicPowerMode;
		[NullAllowed, Export ("basicPowerMode", ArgumentSemantic.Strong)]
		NSNumber BasicPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable smartPowerMode;
		[NullAllowed, Export ("smartPowerMode", ArgumentSemantic.Strong)]
		NSNumber SmartPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable estimoteSecureUUIDState;
		[NullAllowed, Export ("estimoteSecureUUIDState", ArgumentSemantic.Strong)]
		NSNumber EstimoteSecureUuidState { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable conditionalBroadcasting;
		[NullAllowed, Export ("conditionalBroadcasting", ArgumentSemantic.Strong)]
		NSNumber ConditionalBroadcasting { get; set; }
	}

	interface IBeaconUpdateInfoDelegate { }

	// @protocol ESBeaconUpdateInfoDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESBeaconUpdateInfoDelegate")]
	interface BeaconUpdateInfoDelegate
	{
		// @required -(void)beaconUpdateInfoInitialized:(ESTBeaconUpdateInfo * _Nonnull)beaconUpdateInfo;
		[Abstract]
		[Export ("beaconUpdateInfoInitialized:"), EventArgs ("BeaconUpdateInfoBeaconUpdateInfoInitialized")]
		void BeaconUpdateInfoInitialized (BeaconUpdateInfo beaconUpdateInfo);
	}

	// @interface ESTBeaconUpdateInfo : NSObject <NSCoding>
	[BaseType (typeof (NSObject), Name = "ESTBeaconUpdateInfo", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconUpdateInfoDelegate) })]
	interface BeaconUpdateInfo : INSCoding
	{
		// @property (nonatomic, weak) id<ESBeaconUpdateInfoDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBeaconUpdateInfoDelegate Delegate { get; set; }

		// @property (nonatomic, strong) ESTBeaconConnection * _Nonnull beaconConnection;
		[Export ("beaconConnection", ArgumentSemantic.Strong)]
		BeaconConnection BeaconConnection { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull macAddress;
		[Export ("macAddress", ArgumentSemantic.Strong)]
		string MacAddress { get; set; }

		// @property (nonatomic, strong) ESTBeaconUpdateConfig * _Nonnull config;
		[Export ("config", ArgumentSemantic.Strong)]
		BeaconUpdateConfig Config { get; set; }

		// @property (assign, nonatomic) ESBeaconUpdateInfoStatus status;
		[Export ("status", ArgumentSemantic.Assign)]
		EsBeaconUpdateInfoStatus Status { get; set; }

		// @property (nonatomic, strong) NSDate * _Nullable createdAt;
		[NullAllowed, Export ("createdAt", ArgumentSemantic.Strong)]
		NSDate CreatedAt { get; set; }

		// @property (nonatomic, strong) NSDate * _Nullable syncedAt;
		[NullAllowed, Export ("syncedAt", ArgumentSemantic.Strong)]
		NSDate SyncedAt { get; set; }

		// @property (nonatomic, strong) NSError * _Nullable error;
		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; set; }

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress config:(ESTBeaconUpdateConfig * _Nonnull)config;
		[Export ("initWithMacAddress:config:")]
		IntPtr Constructor (string macAddress, BeaconUpdateConfig config);

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress config:(ESTBeaconUpdateConfig * _Nonnull)config delegate:(id<ESBeaconUpdateInfoDelegate> _Nullable)delegate __attribute__((objc_designated_initializer));
		[Export ("initWithMacAddress:config:delegate:")]
		[DesignatedInitializer]
		IntPtr Constructor (string macAddress, BeaconUpdateConfig config, [NullAllowed] IBeaconUpdateInfoDelegate @delegate);

		// -(void)findPeripheralWithTimeout:(NSTimeInterval)timeout;
		[Export ("findPeripheralWithTimeout:")]
		void FindPeripheral (double timeout);

		// -(void)updateWithConfig:(ESTBeaconUpdateConfig * _Nonnull)config;
		[Export ("updateWithConfig:")]
		void Update (BeaconUpdateConfig config);

		// -(NSString * _Nonnull)description;
		[Export ("description")]
		string Description { get; }
	}

	[Static]
	partial interface BulkUpdaterNotification
	{
		// extern NSString *const _Nonnull ESTBulkUpdaterBeginNotification;
		[Field ("ESTBulkUpdaterBeginNotification", "__Internal")]
		NSString Begin { get; }

		// extern NSString *const _Nonnull ESTBulkUpdaterProgressNotification;
		[Field ("ESTBulkUpdaterProgressNotification", "__Internal")]
		NSString Progress { get; }

		// extern NSString *const _Nonnull ESTBulkUpdaterCompleteNotification;
		[Field ("ESTBulkUpdaterCompleteNotification", "__Internal")]
		NSString Complete { get; }

		// extern NSString *const _Nonnull ESTBulkUpdaterFailNotification;
		[Field ("ESTBulkUpdaterFailNotification", "__Internal")]
		NSString Fail { get; }

		// extern NSString *const _Nonnull ESTBulkUpdaterTimeoutNotification;
		[Field ("ESTBulkUpdaterTimeoutNotification", "__Internal")]
		NSString Timeout { get; }
	}

	// @interface ESTBulkUpdater : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTBulkUpdater")]
	interface BulkUpdater
	{
		// @property (nonatomic, strong) NSArray<ESTBeaconUpdateInfo *> * _Nullable beaconInfos;
		[NullAllowed, Export ("beaconInfos", ArgumentSemantic.Strong)]
		BeaconUpdateInfo[] BeaconInfos { get; set; }

		// @property (readonly, nonatomic) ESTBulkUpdaterMode mode;
		[Export ("mode")]
		BulkUpdaterMode Mode { get; }

		// @property (readonly, nonatomic) ESBulkUpdaterStatus status;
		[Export ("status")]
		BulkUpdaterStatus Status { get; }

		// +(ESTBulkUpdater * _Nonnull)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		BulkUpdater SharedInstance { get; }

		// +(BOOL)verifyPushNotificationPayload:(NSDictionary * _Nonnull)payload;
		[Static]
		[Export ("verifyPushNotificationPayload:")]
		bool VerifyPushNotificationPayload (NSDictionary payload);

		// -(void)startWithCloudSettingsAndTimeout:(NSTimeInterval)timeout;
		[Export ("startWithCloudSettingsAndTimeout:")]
		void Start (double timeout);

		// -(void)startWithBeaconInfos:(NSArray * _Nonnull)beaconInfos timeout:(NSTimeInterval)timeout;
		[Export ("startWithBeaconInfos:timeout:")]
		void Start (BeaconUpdateInfo[] beaconInfos, double timeout);

		// -(BOOL)isUpdateInProgressForBeaconWithMacAddress:(NSString * _Nonnull)macAddress;
		[Export ("isUpdateInProgressForBeaconWithMacAddress:")]
		bool IsUpdateInProgressForBeacon (string macAddress);

		// -(BOOL)isBeaconWaitingForUpdate:(NSString * _Nonnull)macAddress;
		[Export ("isBeaconWaitingForUpdate:")]
		bool IsBeaconWaitingForUpdate (string macAddress);

		// -(NSArray * _Nonnull)getBeaconUpdateInfosForBeaconWithMacAddress:(NSString * _Nonnull)macAddress;
		[Export ("getBeaconUpdateInfosForBeaconWithMacAddress:")]
		BeaconUpdateInfo[] GetBeaconUpdateInfosForBeacon (string macAddress);

		// -(NSTimeInterval)getTimeLeftToTimeout;
		[Export ("getTimeLeftToTimeout")]
		double TimeLeftToTimeout { get; }

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();
	}

	// @interface ESTLocationBeaconBulkUpdateConfiguration : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTLocationBeaconBulkUpdateConfiguration")]
	interface LocationBeaconBulkUpdateConfiguration
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull deviceIdentifier;
		[Export ("deviceIdentifier", ArgumentSemantic.Strong)]
		string DeviceIdentifier { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTSettingOperation *> * _Nonnull settingsOperations;
		[Export ("settingsOperations", ArgumentSemantic.Strong)]
		SettingOperation[] SettingsOperations { get; }

		// @property (readonly, assign, nonatomic) BOOL firmwareUpdateAvailable;
		[Export ("firmwareUpdateAvailable")]
		bool FirmwareUpdateAvailable { get; }

		// @property (nonatomic, strong) NSDate * _Nonnull createdAt;
		[Export ("createdAt", ArgumentSemantic.Strong)]
		NSDate CreatedAt { get; set; }

		// @property (nonatomic, strong) NSDate * _Nonnull lastDetectedAt;
		[Export ("lastDetectedAt", ArgumentSemantic.Strong)]
		NSDate LastDetectedAt { get; set; }

		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier settingsOperations:(NSArray<ESTSettingOperation *> * _Nonnull)settingsOperations firmwareUpdateAvailable:(BOOL)firmwareUpdateAvailable;
		[Export ("initWithDeviceIdentifier:settingsOperations:firmwareUpdateAvailable:")]
		IntPtr Constructor (string deviceIdentifier, SettingOperation[] settingsOperations, bool firmwareUpdateAvailable);
	}

	interface ILocationBeaconBulkUpdaterDelegate { }

	// @protocol ESTLocationBeaconBulkUpdaterDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTLocationBeaconBulkUpdaterDelegate")]
	interface LocationBeaconBulkUpdaterDelegate
	{
		// @optional -(void)bulkUpdaterDidFetchDevices:(ESTLocationBeaconBulkUpdater *)bulkUpdater;
		[Export ("bulkUpdaterDidFetchDevices:"), EventArgs ("LocationBeaconBulkUpdaterFetchedDevices")]
		void FetchedDevices (LocationBeaconBulkUpdater bulkUpdater);

		// @optional -(void)bulkUpdater:(ESTLocationBeaconBulkUpdater *)bulkUpdater didUpdateStatus:(ESTBulkUpdaterDeviceUpdateStatus)updateStatus forDeviceWithIdentifier:(NSString *)deviceIdentifier;
		[Export ("bulkUpdater:didUpdateStatus:forDeviceWithIdentifier:"), EventArgs ("LocationBeaconBulkUpdaterUpdatedStatus")]
		void UpdatedStatus (LocationBeaconBulkUpdater bulkUpdater, BulkUpdaterDeviceUpdateStatus updateStatus, string deviceIdentifier);

		// @optional -(void)bulkUpdaterDidFinish:(ESTLocationBeaconBulkUpdater *)bulkUpdater;
		[Export ("bulkUpdaterDidFinish:"), EventArgs ("LocationBeaconBulkUpdaterFinished")]
		void Finished (LocationBeaconBulkUpdater bulkUpdater);

		// @optional -(void)bulkUpdater:(ESTLocationBeaconBulkUpdater *)bulkUpdater didFailWithError:(NSError *)error;
		[Export ("bulkUpdater:didFailWithError:"), EventArgs ("LocationBeaconBulkUpdaterFailed")]
		void Failed (LocationBeaconBulkUpdater bulkUpdater, NSError error);

		// @optional -(void)bulkUpdaterDidCancel:(ESTLocationBeaconBulkUpdater *)bulkUpdater;
		[Export ("bulkUpdaterDidCancel:"), EventArgs ("LocationBeaconBulkUpdaterCanceled")]
		void Canceled (LocationBeaconBulkUpdater bulkUpdater);
	}

	// @interface ESTLocationBeaconBulkUpdater : NSObject
	[BaseType (typeof (NSObject), Name = "ESTLocationBeaconBulkUpdater", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (LocationBeaconBulkUpdaterDelegate) })]
	interface LocationBeaconBulkUpdater
	{
		// @property (nonatomic, weak) id<ESTLocationBeaconBulkUpdaterDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ILocationBeaconBulkUpdaterDelegate Delegate { get; set; }

		// @property (readonly, assign, nonatomic) NSTimeInterval timeout;
		[Export ("timeout")]
		double Timeout { get; }

		// @property (readonly, assign, nonatomic) NSTimeInterval fetchInterval;
		[Export ("fetchInterval")]
		double FetchInterval { get; }

		// @property (readonly, assign, nonatomic) ESTBulkUpdaterStatus status;
		[Export ("status", ArgumentSemantic.Assign)]
		BulkUpdaterStatus Status { get; }

		// @property (readonly, assign, nonatomic) NSArray<ESTLocationBeaconBulkUpdateConfiguration *> * updateConfigurations;
		[Export ("updateConfigurations", ArgumentSemantic.Assign)]
		LocationBeaconBulkUpdateConfiguration[] UpdateConfigurations { get; }

		// @property (readwrite, nonatomic) BOOL skipsFirmwareUpdateStep;
		[Export ("skipsFirmwareUpdateStep")]
		bool SkipsFirmwareUpdateStep { get; set; }

		// -(void)start;
		[Export ("start")]
		void Start ();

		// -(void)startWithTimeout:(NSTimeInterval)timeout;
		[Export ("startWithTimeout:")]
		void Start (double timeout);

		// -(void)startWithTimeout:(NSTimeInterval)timeout fetchInterval:(NSTimeInterval)fetchInterval;
		[Export ("startWithTimeout:fetchInterval:")]
		void Start (double timeout, double fetchInterval);

		// -(void)startWithUpdateConfigurations:(NSArray<ESTLocationBeaconBulkUpdateConfiguration *> *)updateConfigurations;
		[Export ("startWithUpdateConfigurations:")]
		void Start (LocationBeaconBulkUpdateConfiguration[] updateConfigurations);

		// -(void)startWithUpdateConfigurations:(NSArray<ESTLocationBeaconBulkUpdateConfiguration *> *)updateConfigurations timeout:(NSTimeInterval)timeout;
		[Export ("startWithUpdateConfigurations:timeout:")]
		void Start (LocationBeaconBulkUpdateConfiguration[] updateConfigurations, double timeout);

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();

		// -(ESTBulkUpdaterDeviceUpdateStatus)statusForDeviceWithIdentifier:(NSString *)deviceIdentifier;
		[Export ("statusForDeviceWithIdentifier:")]
		BulkUpdaterDeviceUpdateStatus StatusForDevice (string deviceIdentifier);

		// -(NSInteger)numberOfSuccessfulUpdates;
		[Export ("numberOfSuccessfulUpdates")]
		nint NumberOfSuccessfulUpdates { get; }

		// -(NSInteger)numberOfFailedUpdates;
		[Export ("numberOfFailedUpdates")]
		nint NumberOfFailedUpdates { get; }
	}

	// @interface ESTRule : NSObject
	[BaseType (typeof (NSObject), Name = "ESTRule")]
	interface Rule
	{
		// @property (assign, nonatomic) BOOL state;
		[Export ("state")]
		bool State { get; set; }

		// -(void)update;
		[Export ("update")]
		void Update ();
	}

	interface ITriggerDelegate { }

	// @protocol ESTTriggerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTTriggerDelegate")]
	interface TriggerDelegate
	{
		// @optional -(void)triggerDidChangeState:(ESTTrigger * _Nonnull)trigger;
		[Export ("triggerDidChangeState:"), EventArgs ("TriggerChangedState")]
		void ChangedState (Trigger trigger);
	}

	// @interface ESTTrigger : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTrigger", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (TriggerDelegate) })]
	interface Trigger
	{
		// @property (nonatomic, weak) id<ESTTriggerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ITriggerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSArray<ESTRule *> * _Nonnull rules;
		[Export ("rules", ArgumentSemantic.Strong)]
		Rule[] Rules { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, assign, nonatomic) BOOL state;
		[Export ("state")]
		bool State { get; }

		// -(instancetype _Nonnull)initWithRules:(NSArray<ESTRule *> * _Nonnull)rules identifier:(NSString * _Nonnull)identifier;
		[Export ("initWithRules:identifier:")]
		IntPtr Constructor (Rule[] rules, string identifier);
	}

	// @interface ESTDateRule : ESTRule
	[BaseType (typeof (Rule), Name = "ESTDateRule")]
	interface DateRule
	{
		// @property (nonatomic, strong) NSNumber * _Nullable afterHour;
		[NullAllowed, Export ("afterHour", ArgumentSemantic.Strong)]
		NSNumber AfterHour { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable beforeHour;
		[NullAllowed, Export ("beforeHour", ArgumentSemantic.Strong)]
		NSNumber BeforeHour { get; set; }

		// +(instancetype _Nonnull)hourLaterThan:(int)hour;
		[Static]
		[Export ("hourLaterThan:")]
		DateRule HourLaterThan (int hour);

		// +(instancetype _Nonnull)hourEarlierThan:(int)hour;
		[Static]
		[Export ("hourEarlierThan:")]
		DateRule HourEarlierThan (int hour);

		// +(instancetype _Nonnull)hourBetween:(int)firstHour and:(int)secondHour;
		[Static]
		[Export ("hourBetween:and:")]
		DateRule HourBetween (int firstHour, int secondHour);
	}

	// @interface ESTNearableRule : ESTRule
	[DisableDefaultCtor]
	[BaseType (typeof (Rule), Name = "ESTNearableRule")]
	interface NearableRule
	{
		// @property (readonly, nonatomic, strong) NSString * _Nullable nearableIdentifier;
		[NullAllowed, Export ("nearableIdentifier", ArgumentSemantic.Strong)]
		string NearableIdentifier { get; }

		// @property (readonly, assign, nonatomic) ESTNearableType nearableType;
		[Export ("nearableType", ArgumentSemantic.Assign)]
		NearableType NearableType { get; }

		// -(instancetype _Nonnull)initWithNearableIdentifier:(NSString * _Nonnull)identifier;
		[Export ("initWithNearableIdentifier:")]
		IntPtr Constructor (string identifier);

		// -(instancetype _Nonnull)initWithNearableType:(ESTNearableType)type;
		[Export ("initWithNearableType:")]
		IntPtr Constructor (NearableType type);

		// -(void)updateWithNearable:(ESTNearable * _Nonnull)nearable;
		[Export ("updateWithNearable:")]
		void Update (Nearable nearable);
	}

	// @interface ESTProximityRule : ESTNearableRule
	[DisableDefaultCtor]
	[BaseType (typeof (NearableRule), Name = "ESTProximityRule")]
	interface ProximityRule
	{
		// @property (assign, nonatomic) BOOL inRange;
		[Export ("inRange")]
		bool InRange { get; set; }

		// +(instancetype _Nonnull)inRangeOfNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("inRangeOfNearableIdentifier:")]
		ProximityRule InRangeOf (string identifier);

		// +(instancetype _Nonnull)inRangeOfNearableType:(ESTNearableType)type;
		[Static]
		[Export ("inRangeOfNearableType:")]
		ProximityRule InRangeOf (NearableType type);

		// +(instancetype _Nonnull)outsideRangeOfNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("outsideRangeOfNearableIdentifier:")]
		ProximityRule OutsideRangeOf (string identifier);

		// +(instancetype _Nonnull)outsideRangeOfNearableType:(ESTNearableType)type;
		[Static]
		[Export ("outsideRangeOfNearableType:")]
		ProximityRule OutsideRangeOf (NearableType type);
	}

	// @interface ESTTemperatureRule : ESTNearableRule
	[DisableDefaultCtor]
	[BaseType (typeof (NearableRule), Name = "ESTTemperatureRule")]
	interface TemperatureRule
	{
		// @property (nonatomic, strong) NSNumber * _Nullable maxValue;
		[NullAllowed, Export ("maxValue", ArgumentSemantic.Strong)]
		NSNumber MaxValue { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nullable minValue;
		[NullAllowed, Export ("minValue", ArgumentSemantic.Strong)]
		NSNumber MinValue { get; set; }

		// +(instancetype _Nonnull)temperatureGraterThan:(double)value forNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("temperatureGraterThan:forNearableIdentifier:")]
		TemperatureRule TemperatureGraterThan (double value, string identifier);

		// +(instancetype _Nonnull)temperatureLowerThan:(double)value forNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("temperatureLowerThan:forNearableIdentifier:")]
		TemperatureRule TemperatureLowerThan (double value, string identifier);

		// +(instancetype _Nonnull)temperatureBetween:(double)minValue and:(double)maxValue forNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("temperatureBetween:and:forNearableIdentifier:")]
		TemperatureRule TemperatureBetween (double minValue, double maxValue, string identifier);

		// +(instancetype _Nonnull)temperatureGraterThan:(double)value forNearableType:(ESTNearableType)type;
		[Static]
		[Export ("temperatureGraterThan:forNearableType:")]
		TemperatureRule TemperatureGraterThan (double value, NearableType type);

		// +(instancetype _Nonnull)temperatureLowerThan:(double)value forNearableType:(ESTNearableType)type;
		[Static]
		[Export ("temperatureLowerThan:forNearableType:")]
		TemperatureRule TemperatureLowerThan (double value, NearableType type);

		// +(instancetype _Nonnull)temperatureBetween:(double)minValue and:(double)maxValue forNearableType:(ESTNearableType)type;
		[Static]
		[Export ("temperatureBetween:and:forNearableType:")]
		TemperatureRule TemperatureBetween (double minValue, double maxValue, NearableType type);
	}

	// @interface ESTMotionRule : ESTNearableRule
	[DisableDefaultCtor]
	[BaseType (typeof (NearableRule), Name = "ESTMotionRule")]
	interface MotionRule
	{
		// @property (assign, nonatomic) BOOL motionState;
		[Export ("motionState")]
		bool MotionState { get; set; }

		// +(instancetype _Nonnull)motionStateEquals:(BOOL)motionState forNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("motionStateEquals:forNearableIdentifier:")]
		MotionRule MotionStateEquals (bool motionState, string identifier);

		// +(instancetype _Nonnull)motionStateEquals:(BOOL)motionState forNearableType:(ESTNearableType)type;
		[Static]
		[Export ("motionStateEquals:forNearableType:")]
		MotionRule MotionStateEquals (bool motionState, NearableType type);
	}

	// @interface ESTOrientationRule : ESTNearableRule
	[DisableDefaultCtor]
	[BaseType (typeof (NearableRule), Name = "ESTOrientationRule")]
	interface OrientationRule
	{
		// @property (assign, nonatomic) ESTNearableOrientation orientation;
		[Export ("orientation", ArgumentSemantic.Assign)]
		NearableOrientation Orientation { get; set; }

		// +(instancetype _Nonnull)orientationEquals:(ESTNearableOrientation)orientation forNearableIdentifier:(NSString * _Nonnull)identifier;
		[Static]
		[Export ("orientationEquals:forNearableIdentifier:")]
		OrientationRule OrientationEquals (NearableOrientation orientation, string identifier);

		// +(instancetype _Nonnull)orientationEquals:(ESTNearableOrientation)orientation forNearableType:(ESTNearableType)type;
		[Static]
		[Export ("orientationEquals:forNearableType:")]
		OrientationRule OrientationEquals (NearableOrientation orientation, NearableType type);
	}

	interface ITriggerManagerDelegate { }

	// @protocol ESTTriggerManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTTriggerManagerDelegate")]
	interface TriggerManagerDelegate
	{
		// @optional -(void)triggerManager:(ESTTriggerManager * _Nonnull)manager triggerChangedState:(ESTTrigger * _Nonnull)trigger;
		[Export ("triggerManager:triggerChangedState:"), EventArgs ("TriggerManagerTriggerChangedState")]
		void TriggerChangedState (TriggerManager manager, Trigger trigger);
	}

	// @interface ESTTriggerManager : NSObject <ESTTriggerDelegate>
	[BaseType (typeof (NSObject), Name = "ESTTriggerManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (TriggerManagerDelegate) })]
	interface TriggerManager : TriggerDelegate
	{
		// @property (nonatomic, weak) id<ESTTriggerManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ITriggerManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSArray<ESTTrigger *> * _Nonnull triggers;
		[Export ("triggers", ArgumentSemantic.Strong)]
		Trigger[] Triggers { get; }

		// -(void)startMonitoringForTrigger:(ESTTrigger * _Nonnull)trigger;
		[Export ("startMonitoringForTrigger:")]
		void StartMonitoring (Trigger trigger);

		// -(void)stopMonitoringForTriggerWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("stopMonitoringForTriggerWithIdentifier:")]
		void StopMonitoringForTrigger (string identifier);

		// -(BOOL)stateForTriggerWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("stateForTriggerWithIdentifier:")]
		bool StateForTrigger (string identifier);
	}

	// @interface ESTNotificationTransporter : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTNotificationTransporter")]
	interface NotificationTransporter
	{
		// @property (readonly, nonatomic) NSString * currentAppGroupIdentifier;
		[Export ("currentAppGroupIdentifier")]
		string CurrentAppGroupIdentifier { get; }

		// +(instancetype)sharedTransporter;
		[Static]
		[Export ("sharedTransporter")]
		NotificationTransporter SharedTransporter { get; }

		// -(void)setAppGroupIdentifier:(NSString *)identifier;
		[Export ("setAppGroupIdentifier:")]
		void SetAppGroupIdentifier (string identifier);

		// -(BOOL)saveNearableZoneDescription:(NSString *)zone;
		[Export ("saveNearableZoneDescription:")]
		bool SaveNearableZoneDescription (string zone);

		// -(NSString *)readNearableZoneDescription;
		[Export ("readNearableZoneDescription")]
		string ReadNearableZoneDescription { get; }

		// -(BOOL)saveNearable:(ESTNearable *)nearable;
		[Export ("saveNearable:")]
		bool SaveNearable (Nearable nearable);

		// -(ESTNearable *)readNearable;
		[Export ("readNearable")]
		Nearable ReadNearable { get; }

		// -(BOOL)didRangeNearables:(NSArray *)nearables;
		[Export ("didRangeNearables:")]
		bool RangedNearables (Nearable[] nearables);

		// -(NSArray *)readRangedNearables;
		[Export ("readRangedNearables")]
		Nearable[] ReadRangedNearables { get; }

		// -(void)notifyDidEnterRegion:(CLBeaconRegion *)region;
		[Export ("notifyDidEnterRegion:")]
		void NotifyEnteredRegion (CLBeaconRegion region);

		// -(void)notifyDidExitRegion:(CLBeaconRegion *)region;
		[Export ("notifyDidExitRegion:")]
		void NotifyExitedRegion (CLBeaconRegion region);

		// -(void)notifyDidEnterIdentifierRegion:(NSString *)identifier;
		[Export ("notifyDidEnterIdentifierRegion:")]
		void NotifyEnteredIdentifierRegion (string identifier);

		// -(void)notifyDidExitIdentifierRegion:(NSString *)identifier;
		[Export ("notifyDidExitIdentifierRegion:")]
		void NotifyExitedIdentifierRegion (string identifier);

		// -(NSString *)readIdentifierForMonitoringEvents;
		[Export ("readIdentifierForMonitoringEvents")]
		string ReadIdentifierForMonitoringEvents { get; }

		// -(void)addObserver:(id)observer selector:(SEL)selector forNotification:(ESTNotification)notification;
		[Export ("addObserver:selector:forNotification:")]
		void AddObserver (NSObject observer, Selector selector, Notification notification);

		// -(void)removeObserver:(id)observer;
		[Export ("removeObserver:")]
		void RemoveObserver (NSObject observer);
	}

	// @interface ESTEddystone : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "ESTEddystone")]
	interface Eddystone : INSCopying
	{
		// @property (nonatomic, strong) NSString * _Nonnull macAddress;
		[Export ("macAddress", ArgumentSemantic.Strong)]
		string MacAddress { get; set; }

		// @property (nonatomic, strong) NSUUID * _Nonnull peripheralIdentifier;
		[Export ("peripheralIdentifier", ArgumentSemantic.Strong)]
		NSUuid PeripheralIdentifier { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull rssi;
		[Export ("rssi", ArgumentSemantic.Strong)]
		NSNumber Rssi { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull accuracy;
		[Export ("accuracy", ArgumentSemantic.Strong)]
		NSNumber Accuracy { get; set; }

		// @property (nonatomic) ESTEddystoneProximity proximity;
		[Export ("proximity", ArgumentSemantic.Assign)]
		EddystoneProximity Proximity { get; set; }

		// @property (nonatomic, strong) NSDate * _Nonnull discoveryDate;
		[Export ("discoveryDate", ArgumentSemantic.Strong)]
		NSDate DiscoveryDate { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull measuredPower;
		[Export ("measuredPower", ArgumentSemantic.Strong)]
		NSNumber MeasuredPower { get; set; }

		// -(void)updateWithEddystone:(ESTEddystone * _Nonnull)eddystone;
		[Export ("updateWithEddystone:")]
		void Update (Eddystone eddystone);

		// -(BOOL)isEqualToEddystone:(ESTEddystone * _Nonnull)eddystone;
		[Export ("isEqualToEddystone:")]
		bool IsEqualTo (Eddystone eddystone);
	}

	// @interface ESTEddystoneUID : ESTEddystone
	[DisableDefaultCtor]
	[BaseType (typeof (Eddystone), Name = "ESTEddystoneUID")]
	interface EddystoneUid
	{
		// @property (readonly, nonatomic, strong) NSString * _Nullable namespaceID;
		[NullAllowed, Export ("namespaceID", ArgumentSemantic.Strong)]
		string NamespaceId { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable instanceID;
		[NullAllowed, Export ("instanceID", ArgumentSemantic.Strong)]
		string InstanceId { get; }

		// -(instancetype _Nonnull)initWithNamespaceID:(NSString * _Nonnull)namespaceID;
		[Export ("initWithNamespaceID:")]
		IntPtr Constructor (string namespaceID);

		// -(instancetype _Nonnull)initWithNamespaceID:(NSString * _Nonnull)namespaceID instanceID:(NSString * _Nullable)instanceID;
		[Export ("initWithNamespaceID:instanceID:")]
		IntPtr Constructor (string namespaceID, [NullAllowed] string instanceID);
	}

	// @interface ESTEddystoneURL : ESTEddystone
	[DisableDefaultCtor]
	[BaseType (typeof (Eddystone), Name = "ESTEddystoneURL")]
	interface EddystoneUrl
	{
		// @property (readonly, nonatomic, strong) NSString * _Nullable url;
		[NullAllowed, Export ("url", ArgumentSemantic.Strong)]
		string Url { get; }

		// -(instancetype _Nonnull)initWithURL:(NSString * _Nonnull)url;
		[Export ("initWithURL:")]
		IntPtr Constructor (string url);
	}

	// @interface ESTEddystoneTLM : ESTEddystone
	[BaseType (typeof (Eddystone), Name = "ESTEddystoneTLM")]
	interface EddystoneTlm
	{
		// @property (nonatomic, strong) NSNumber * _Nonnull batteryVoltage;
		[Export ("batteryVoltage", ArgumentSemantic.Strong)]
		NSNumber BatteryVoltage { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull temperature;
		[Export ("temperature", ArgumentSemantic.Strong)]
		NSNumber Temperature { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull packetCount;
		[Export ("packetCount", ArgumentSemantic.Strong)]
		NSNumber PacketCount { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull uptimeMillis;
		[Export ("uptimeMillis", ArgumentSemantic.Strong)]
		NSNumber UptimeMillis { get; set; }

		// -(instancetype _Nonnull)initWithBatteryVoltage:(NSNumber * _Nonnull)batteryVoltage temperature:(NSNumber * _Nonnull)temperature packetCount:(NSNumber * _Nonnull)packetCont uptimeMillis:(NSNumber * _Nonnull)uptimeMillis;
		[Export ("initWithBatteryVoltage:temperature:packetCount:uptimeMillis:")]
		IntPtr Constructor (NSNumber batteryVoltage, NSNumber temperature, NSNumber packetCont, NSNumber uptimeMillis);
	}

	// @interface ESTEddystoneAttachment : NSObject <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTEddystoneAttachment")]
	interface EddystoneAttachment : INSCopying
	{
		// @property (readonly, nonatomic) NSString * _Nullable namespacedType;
		[NullAllowed, Export ("namespacedType")]
		string NamespacedType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable data;
		[NullAllowed, Export ("data")]
		string Data { get; }

		// -(instancetype _Nonnull)initWithNamespacedType:(NSString * _Nullable)namespacedType data:(NSString * _Nullable)data;
		[Export ("initWithNamespacedType:data:")]
		IntPtr Constructor ([NullAllowed] string namespacedType, [NullAllowed] string data);

		// -(instancetype _Nonnull)initWithCloudData:(NSDictionary * _Nonnull)cloudData;
		[Export ("initWithCloudData:")]
		IntPtr Constructor (NSDictionary cloudData);
	}

	// @interface ESTEddystoneEID : ESTEddystone
	[DisableDefaultCtor]
	[BaseType (typeof (Eddystone), Name = "ESTEddystoneEID")]
	interface EddystoneEid
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull ephemeralID;
		[Export ("ephemeralID", ArgumentSemantic.Strong)]
		string EphemeralId { get; }

		// @property (readonly, nonatomic) BOOL isResolved;
		[Export ("isResolved")]
		bool IsResolved { get; }

		// @property (nonatomic, strong) NSArray<ESTEddystoneAttachment *> * _Nullable attachments;
		[NullAllowed, Export ("attachments", ArgumentSemantic.Strong)]
		EddystoneAttachment[] Attachments { get; set; }

		// -(instancetype _Nonnull)initWithEphemeralID:(NSString * _Nullable)ephemeralID resolved:(BOOL)resolved;
		[Export ("initWithEphemeralID:resolved:")]
		IntPtr Constructor ([NullAllowed] string ephemeralID, bool resolved);
	}

	// @interface ESTEddystoneFilter : NSObject
	[BaseType (typeof (NSObject), Name = "ESTEddystoneFilter")]
	interface EddystoneFilter
	{
		// -(NSArray<ESTEddystone *> * _Nonnull)filterEddystones:(NSArray<ESTEddystone *> * _Nonnull)eddystones;
		[Export ("filterEddystones:")]
		Eddystone[] FilterEddystones (Eddystone[] eddystones);
	}

	interface IEddystoneManagerDelegate { }

	// @protocol ESTEddystoneManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTEddystoneManagerDelegate")]
	interface EddystoneManagerDelegate
	{
		// @optional -(void)eddystoneManager:(ESTEddystoneManager * _Nonnull)manager didDiscoverEddystones:(NSArray<ESTEddystone *> * _Nonnull)eddystones withFilter:(ESTEddystoneFilter * _Nullable)eddystoneFilter;
		[Export ("eddystoneManager:didDiscoverEddystones:withFilter:"), EventArgs ("EddystoneManagerDiscoveredEddystones")]
		void DiscoveredEddystones (EddystoneManager manager, Eddystone[] eddystones, [NullAllowed] EddystoneFilter eddystoneFilter);

		// @optional -(void)eddystoneManagerDidFailDiscovery:(ESTEddystoneManager * _Nonnull)manager withError:(NSError * _Nullable)error;
		[Export ("eddystoneManagerDidFailDiscovery:withError:"), EventArgs ("EddystoneManagerFailedDiscovery")]
		void FailedDiscovery (EddystoneManager manager, [NullAllowed] NSError error);
	}

	// @interface ESTEddystoneManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTEddystoneManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (EddystoneManagerDelegate) })]
	interface EddystoneManager
	{
		// @property (nonatomic, weak) id<ESTEddystoneManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IEddystoneManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSArray<ESTEddystoneFilter *> * _Nonnull filtersInDiscovery;
		[Export ("filtersInDiscovery", ArgumentSemantic.Strong)]
		EddystoneFilter[] FiltersInDiscovery { get; }

		// -(void)startEddystoneDiscoveryWithFilter:(ESTEddystoneFilter * _Nullable)eddystoneFilter;
		[Export ("startEddystoneDiscoveryWithFilter:")]
		void StartEddystoneDiscovery ([NullAllowed] EddystoneFilter eddystoneFilter);

		// -(void)stopEddystoneDiscoveryWithFilter:(ESTEddystoneFilter * _Nullable)eddystoneFilter;
		[Export ("stopEddystoneDiscoveryWithFilter:")]
		void StopEddystoneDiscovery ([NullAllowed] EddystoneFilter eddystoneFilter);
	}

	// @interface ESTEddystoneFilterUID : ESTEddystoneFilter
	[DisableDefaultCtor]
	[BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterUID")]
	interface EddystoneFilterUid
	{
		// @property (readonly, nonatomic, strong) NSString * _Nullable namespaceID;
		[NullAllowed, Export ("namespaceID", ArgumentSemantic.Strong)]
		string NamespaceId { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable instanceID;
		[NullAllowed, Export ("instanceID", ArgumentSemantic.Strong)]
		string InstanceId { get; }

		// -(instancetype _Nonnull)initWithNamespaceID:(NSString * _Nonnull)namespaceID;
		[Export ("initWithNamespaceID:")]
		IntPtr Constructor (string namespaceID);

		// -(instancetype _Nonnull)initWithNamespaceID:(NSString * _Nonnull)namespaceID instanceID:(NSString * _Nonnull)instanceID;
		[Export ("initWithNamespaceID:instanceID:")]
		IntPtr Constructor (string namespaceID, string instanceID);
	}

	// @interface ESTEddystoneFilterEID : ESTEddystoneFilter
	[BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterEID")]
	interface EddystoneFilterEid
	{
	}

	// @interface ESTEddystoneFilterURL : ESTEddystoneFilter
	[BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterURL")]
	interface EddystoneFilterUrl
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull eddystoneURL;
		[Export ("eddystoneURL", ArgumentSemantic.Strong)]
		string EddystoneUrl { get; }

		// -(instancetype _Nonnull)initWithURL:(NSString * _Nonnull)eddystoneURL;
		[Export ("initWithURL:")]
		IntPtr Constructor (string eddystoneURL);
	}

	// @interface ESTEddystoneFilterURLDomain : ESTEddystoneFilter
	[DisableDefaultCtor]
	[BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterURLDomain")]
	interface EddystoneFilterUrlDomain
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull eddystoneURLDomain;
		[Export ("eddystoneURLDomain", ArgumentSemantic.Strong)]
		string EddystoneUrlDomain { get; }

		// -(instancetype _Nonnull)initWithURLDomain:(NSString * _Nonnull)eddystoneURLDomain;
		[Export ("initWithURLDomain:")]
		IntPtr Constructor (string eddystoneURLDomain);
	}

	// @interface ESTConfig : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTConfig")]
	interface Config
	{
		// +(void)setupAppID:(NSString * _Nonnull)appID andAppToken:(NSString * _Nonnull)appToken;
		[Static]
		[Export ("setupAppID:andAppToken:")]
		void SetupAppId (string appID, string appToken);

		// +(NSString * _Nullable)appID;
		[Static]
		[NullAllowed, Export ("appID")]
		string AppId { get; }

		// +(NSString * _Nullable)appToken;
		[Static]
		[NullAllowed, Export ("appToken")]
		string AppToken { get; }

		// +(BOOL)isAuthorized;
		[Static]
		[Export ("isAuthorized")]
		bool IsAuthorized { get; }

		// +(void)setupGoogleAPIKey:(NSString * _Nonnull)googleAPIKey;
		[Static]
		[Export ("setupGoogleAPIKey:")]
		void SetupGoogleApiKey (string googleAPIKey);

		// +(NSString * _Nullable)googleAPIKey;
		[Static]
		[NullAllowed, Export ("googleAPIKey")]
		string GoogleApiKey { get; }

		// +(void)enableMonitoringAnalytics:(BOOL)enable __attribute__((deprecated("Starting from SDK 4.1.1 this method is deprecated. Please use [ESTAnalyticsManager enableMonitoringAnalytics:]")));
		[Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableMonitoringAnalytics static method instead.")]
		[Static]
		[Export ("enableMonitoringAnalytics:")]
		void EnableMonitoringAnalytics (bool enable);

		// +(void)enableRangingAnalytics:(BOOL)enable __attribute__((deprecated("Starting from SDK 4.1.1 this method is deprecated. Please use [ESTAnalyticsManager enableRangingAnalytics:]")));
		[Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableRangingAnalytics static method instead.")]
		[Static]
		[Export ("enableRangingAnalytics:")]
		void EnableRangingAnalytics (bool enable);

		// +(void)enableGPSPositioningForAnalytics:(BOOL)enable __attribute__((deprecated("Starting from SDK 4.1.1 this method is deprecated. Please use [ESTAnalyticsManager enableGPSPositioningForAnalytics:]")));
		[Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableGpsPositioningForAnalytics static method instead.")]
		[Static]
		[Export ("enableGPSPositioningForAnalytics:")]
		void EnableGpsPositioningForAnalytics (bool enable);

		// +(BOOL)isMonitoringAnalyticsEnabled __attribute__((deprecated("Starting from SDK 4.1.1 this method is deprecated. Please use [ESTAnalyticsManager isMonitoringAnalyticsEnabled:]")));
		[Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.IsMonitoringAnalyticsEnabled static property instead.")]
		[Static]
		[Export ("isMonitoringAnalyticsEnabled")]
		bool IsMonitoringAnalyticsEnabled { get; }

		// +(BOOL)isRangingAnalyticsEnabled __attribute__((deprecated("Starting from SDK 4.1.1 this method is deprecated. Please use [ESTAnalyticsManager isRangingAnalyticsEnabled:]")));
		[Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.IsRangingAnalyticsEnabled static property instead.")]
		[Static]
		[Export ("isRangingAnalyticsEnabled")]
		bool IsRangingAnalyticsEnabled { get; }
	}

	// @interface ESTCloudManager : NSObject
	[Obsolete ("Starting from SDK 3.5.0 use Config class and particular requests (eg. RequestGetBeacons) to interact with Estimote Cloud API.")]
	[BaseType (typeof (NSObject), Name = "ESTCloudManager")]
	interface CloudManager
	{
		// +(void)setupAppID:(NSString * _Nonnull)appID andAppToken:(NSString * _Nonnull)appToken;
		[Static]
		[Export ("setupAppID:andAppToken:")]
		void SetupAppId (string appID, string appToken);

		// +(NSString * _Nullable)appID;
		[Static]
		[NullAllowed, Export ("appID")]
		string AppId { get; }

		// +(NSString * _Nullable)appToken;
		[Static]
		[NullAllowed, Export ("appToken")]
		string AppToken { get; }

		// +(BOOL)isAuthorized;
		[Static]
		[Export ("isAuthorized")]
		bool IsAuthorized { get; }

		// +(void)enableAnalytics:(BOOL)enable __attribute__((deprecated("Starting from SDK 3.2.0 use enableMonitoringAnalytics: or enableRangingAnalytics: instead.")));
		[Static]
		[Export ("enableAnalytics:")]
		void EnableAnalytics (bool enable);

		// +(void)enableMonitoringAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableMonitoringAnalytics:")]
		void EnableMonitoringAnalytics (bool enable);

		// +(void)enableRangingAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableRangingAnalytics:")]
		void EnableRangingAnalytics (bool enable);

		// +(void)enableGPSPositioningForAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableGPSPositioningForAnalytics:")]
		void EnableGpsPositioningForAnalytics (bool enable);

		// +(BOOL)isAnalyticsEnabled __attribute__((deprecated("Starting from SDK 3.2.0 use enableMonitoringAnalytics: or enableRangingAnalytics: instead.")));
		[Static]
		[Export ("isAnalyticsEnabled")]
		bool IsAnalyticsEnabled { get; }

		// +(BOOL)isMonitoringAnalyticsEnabled;
		[Static]
		[Export ("isMonitoringAnalyticsEnabled")]
		bool IsMonitoringAnalyticsEnabled { get; }

		// +(BOOL)isRangingAnalyticsEnabled;
		[Static]
		[Export ("isRangingAnalyticsEnabled")]
		bool IsRangingAnalyticsEnabled { get; }

		// -(void)fetchEstimoteNearablesWithCompletion:(ESTArrayCompletionBlock _Nonnull)completion;
		[Export ("fetchEstimoteNearablesWithCompletion:"), Async]
		void FetchEstimoteNearables (NearableArrayCompletionBlock completion);

		// -(void)fetchEstimoteBeaconsWithCompletion:(ESTArrayCompletionBlock _Nonnull)completion;
		[Export ("fetchEstimoteBeaconsWithCompletion:"), Async]
		void FetchEstimoteBeacons (BeaconVOArrayCompletionBlock completion);

		// -(void)fetchBeaconDetails:(NSString * _Nonnull)beaconUID completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("fetchBeaconDetails:completion:"), Async]
		void FetchBeaconDetails (string beaconUID, BeaconVOCompletionBlock completion);

		// -(void)fetchNearableDetails:(NSString * _Nonnull)nearableUID completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("fetchNearableDetails:completion:"), Async]
		void FetchNearableDetails (string nearableUID, NearableVOCompletionBlock completion);

		// -(void)fetchColorForBeacon:(CLBeacon * _Nonnull)beacon completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("fetchColorForBeacon:completion:"), Async]
		void FetchColor (CLBeacon beacon, NSNumberCompletionBlock completion);

		// -(void)fetchColorForBeaconWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("fetchColorForBeaconWithProximityUUID:major:minor:completion:"), Async]
		void FetchColorForBeacon (NSUuid proximityUUID, ushort major, ushort minor, NSNumberCompletionBlock completion);

		// -(void)fetchColorForBeaconWithMacAddress:(NSString * _Nonnull)macAddress completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("fetchColorForBeaconWithMacAddress:completion:"), Async]
		void FetchColorForBeacon (string macAddress, NSNumberCompletionBlock completion);

		// -(void)fetchMacAddressForBeacon:(CLBeacon * _Nonnull)beacon completion:(ESTStringCompletionBlock _Nonnull)completion;
		[Export ("fetchMacAddressForBeacon:completion:"), Async]
		void FetchMacAddress (CLBeacon beacon, StringCompletionBlock completion);

		// -(void)assignGPSLocation:(CLLocation * _Nonnull)location toBeacon:(CLBeacon * _Nonnull)beacon completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("assignGPSLocation:toBeacon:completion:"), Async]
		void AssignGpsLocation (CLLocation location, CLBeacon beacon, CLLocationCompletionBlock completion);

		// -(void)assignGPSLocation:(CLLocation * _Nonnull)location toBeaconWithMac:(NSString * _Nonnull)macAddress completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("assignGPSLocation:toBeaconWithMac:completion:"), Async]
		void AssignGpsLocation (CLLocation location, string macAddress, CLLocationCompletionBlock completion);

		// -(void)assignCurrentGPSLocationToBeacon:(CLBeacon * _Nonnull)beacon completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("assignCurrentGPSLocationToBeacon:completion:"), Async]
		void AssignCurrentGpsLocation (CLBeacon beacon, CLLocationCompletionBlock completion);

		// -(void)assignCurrentGPSLocationToBeaconWithMac:(NSString * _Nonnull)macAddress completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("assignCurrentGPSLocationToBeaconWithMac:completion:"), Async]
		void AssignCurrentGpsLocationToBeacon (string macAddress, CLLocationCompletionBlock completion);

		// -(void)assignCurrentGPSLocationToBeaconWithProximityUUID:(NSUUID * _Nonnull)uuid major:(NSNumber * _Nonnull)major minor:(NSNumber * _Nonnull)minor completion:(ESTObjectCompletionBlock _Nonnull)completion;
		[Export ("assignCurrentGPSLocationToBeaconWithProximityUUID:major:minor:completion:"), Async]
		void AssignCurrentGpsLocationToBeaconD (NSUuid uuid, NSNumber major, NSNumber minor, CLLocationCompletionBlock completion);

		// -(void)registerDeviceForRemoteManagement:(NSData * _Nonnull)deviceToken completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("registerDeviceForRemoteManagement:completion:"), Async]
		void RegisterDeviceForRemoteManagement (NSData deviceToken, CompletionBlock completion);

		// -(void)fetchPendingBeaconsSettingsWithCompletion:(ESTArrayCompletionBlock _Nonnull)completion;
		[Export ("fetchPendingBeaconsSettingsWithCompletion:"), Async]
		void FetchPendingBeaconsSettings (BeaconUpdateInfoArrayCompletionBlock completion);
	}

	// typedef void (^ESTRequestBlock)(id _Nullable, NSError * _Nullable);
	delegate void RequestBlock ([NullAllowed] NSObject result, [NullAllowed] NSError error);

	interface IRequestBaseDelegate { }

	// @protocol ESTRequestBaseDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTRequestBaseDelegate")]
	interface RequestBaseDelegate
	{
		// @required -(void)request:(ESTRequestBase * _Nonnull)request didFinishLoadingWithResposne:(id _Nullable)response;
		[Abstract]
		[Export ("request:didFinishLoadingWithResposne:"), EventArgs ("RequestBaseFinishedLoading")]
		void FinishedLoading (RequestBase request, [NullAllowed] NSObject response);

		// @required -(void)request:(ESTRequestBase * _Nonnull)request didFailLoadingWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("request:didFailLoadingWithError:"), EventArgs ("RequestBaseFailedLoading")]
		void FailedLoading (RequestBase request, NSError error);
	}

	// @interface ESTRequestBase : NSObject <NSURLConnectionDelegate, NSURLConnectionDataDelegate>
	[BaseType (typeof (NSObject), Name = "ESTRequestBase", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (RequestBaseDelegate) })]
	interface RequestBase : INSUrlConnectionDelegate, INSUrlConnectionDataDelegate
	{
		// @property (nonatomic, weak) id<ESTRequestBaseDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IRequestBaseDelegate Delegate { get; set; }

		// @property (copy, nonatomic) ESTRequestBlock _Nullable completionBlock;
		[NullAllowed, Export ("completionBlock", ArgumentSemantic.Copy)]
		RequestBlock CompletionBlock { get; set; }

		// @property (assign, nonatomic) NSInteger code;
		[Export ("code")]
		nint Code { get; set; }

		// @property (nonatomic, strong) NSURLConnection * _Nullable connection;
		[NullAllowed, Export ("connection", ArgumentSemantic.Strong)]
		NSUrlConnection Connection { get; set; }

		// @property (nonatomic, strong) NSMutableData * _Nullable receivedData;
		[NullAllowed, Export ("receivedData", ArgumentSemantic.Strong)]
		NSMutableData ReceivedData { get; set; }

		// -(NSMutableURLRequest * _Nonnull)createRequestWithUrl:(NSString * _Nonnull)url;
		[Export ("createRequestWithUrl:")]
		NSMutableUrlRequest CreateRequest (string url);

		// -(void)fireRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("fireRequest:")]
		void FireRequest (NSMutableUrlRequest request);

		// -(void)parseRespondedData:(id _Nullable)data;
		[Export ("parseRespondedData:")]
		void ParseRespondedData ([NullAllowed] NSObject data);

		// -(void)parseError:(NSError * _Nonnull)error;
		[Export ("parseError:")]
		void ParseError (NSError error);

		// -(void)parseErrorWithCode:(ESTRequestBaseError)errorCode reason:(NSString * _Nullable)reason;
		[Export ("parseErrorWithCode:reason:")]
		void ParseError (RequestBaseError errorCode, [NullAllowed] string reason);

		// -(void)sendRequest;
		[Export ("sendRequest")]
		void SendRequest ();

		// -(void)sendRequestWithCompletion:(ESTRequestBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestBlock completion);

		// -(void)cancelRequest;
		[Export ("cancelRequest")]
		void CancelRequest ();

		// -(id _Nullable)objectForKey:(NSString * _Nonnull)aKey inDictionary:(NSDictionary * _Nullable)dict;
		[Export ("objectForKey:inDictionary:")]
		[return: NullAllowed]
		NSObject ObjectForKey (string aKey, [NullAllowed] NSDictionary dict);

		// -(BOOL)isEqualToRequest:(ESTRequestBase * _Nonnull)request;
		[Export ("isEqualToRequest:")]
		bool IsEqualTo (RequestBase request);
	}

	// @interface ESTRequestGetJSON : ESTRequestBase
	[BaseType (typeof (RequestBase), Name = "ESTRequestGetJSON")]
	interface RequestGetJson
	{
	}

	// typedef void (^ESTRequestGetBeaconsBlock)(NSArray<ESTBeaconVO *> * _Nullable, NSError * _Nullable);
	delegate void RequestGetBeaconsBlock ([NullAllowed] BeaconVO[] beaconVOs, [NullAllowed] NSError error);

	// @interface ESTRequestGetBeacons : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetBeacons")]
	interface RequestGetBeacons
	{
		// -(void)sendRequestWithCompletion:(ESTRequestGetBeaconsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetBeaconsBlock completion);
	}

	// typedef void (^ESTRequestGetBeaconsDetailsBlock)(NSArray * _Nullable, NSError * _Nullable);
	delegate void RequestGetBeaconsDetailsBlock ([NullAllowed] NSObject [] beaconVOArray, [NullAllowed] NSError error);
	delegate void RequestGetBeaconsDetailsBeaconArrayBlock ([NullAllowed] BeaconVO [] beaconVOArray, [NullAllowed] NSError error);

	// @interface ESTRequestGetBeaconsDetails : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetBeaconsDetails")]
	interface RequestGetBeaconsDetails
	{
		// @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull beaconIdentifiers;
		[Export ("beaconIdentifiers", ArgumentSemantic.Strong)]
		string[] BeaconIdentifiers { get; }

		// @property (readonly, assign, nonatomic) ESTBeaconDetailsFields fields;
		[Export ("fields", ArgumentSemantic.Assign)]
		BeaconDetailsFields Fields { get; }

		// -(instancetype _Nonnull)initWithBeacons:(NSArray<CLBeacon *> * _Nonnull)beacons andFields:(ESTBeaconDetailsFields)fields;
		[Export ("initWithBeacons:andFields:")]
		IntPtr Constructor (CLBeacon[] beacons, BeaconDetailsFields fields);

		// -(instancetype _Nonnull)initWithMacAddresses:(NSArray<NSString *> * _Nonnull)macAddresses andFields:(ESTBeaconDetailsFields)fields;
		[Export ("initWithMacAddresses:andFields:")]
		IntPtr Constructor (string[] macAddresses, BeaconDetailsFields fields);

		// -(void)sendRequestWithCompletion:(ESTRequestGetBeaconsDetailsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetBeaconsDetailsBeaconArrayBlock completion);
	}

	// typedef void (^ESTRequestGetBeaconsPublicDetailsBlock)(NSArray * _Nullable, NSError * _Nullable);
	delegate void RequestGetBeaconsPublicDetailsBlock ([NullAllowed] NSObject[] beaconVOArray, [NullAllowed] NSError error);
	delegate void RequestGetBeaconsPublicDetailsBeaconArrayBlock ([NullAllowed] BeaconVO [] beaconVOArray, [NullAllowed] NSError error);

	// @interface ESTRequestGetBeaconsPublicDetails : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetBeaconsPublicDetails")]
	interface RequestGetBeaconsPublicDetails
	{
		// @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull beaconIdentifiers;
		[Export ("beaconIdentifiers", ArgumentSemantic.Strong)]
		string[] BeaconIdentifiers { get; }

		// @property (readonly, assign, nonatomic) ESTBeaconPublicDetailsFields fields;
		[Export ("fields", ArgumentSemantic.Assign)]
		BeaconPublicDetailsFields Fields { get; }

		// -(instancetype _Nonnull)initWithBeacons:(NSArray<CLBeacon *> * _Nonnull)beacons andFields:(ESTBeaconPublicDetailsFields)fields;
		[Export ("initWithBeacons:andFields:")]
		IntPtr Constructor (CLBeacon[] beacons, BeaconPublicDetailsFields fields);

		// -(instancetype _Nonnull)initWithMacAddresses:(NSArray<NSString *> * _Nonnull)macAddresses andFields:(ESTBeaconPublicDetailsFields)fields;
		[Export ("initWithMacAddresses:andFields:")]
		IntPtr Constructor (string[] macAddresses, BeaconPublicDetailsFields fields);

		// -(void)sendRequestWithCompletion:(ESTRequestGetBeaconsPublicDetailsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetBeaconsPublicDetailsBeaconArrayBlock completion);
	}

	// typedef void (^ESTRequestBeaconColorBlock)(ESTColor, NSError * _Nullable);
	delegate void RequestBeaconColorBlock (Color beaconColor, [NullAllowed] NSError error);

	// @interface ESTRequestBeaconColor : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestBeaconColor")]
	interface RequestBeaconColor
	{
		// @property (readonly, nonatomic, strong) NSString * _Nullable macAddress;
		[NullAllowed, Export ("macAddress", ArgumentSemantic.Strong)]
		string MacAddress { get; }

		// @property (readonly, nonatomic, strong) NSUUID * _Nullable proximityUUID;
		[NullAllowed, Export ("proximityUUID", ArgumentSemantic.Strong)]
		NSUuid ProximityUuid { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable major;
		[NullAllowed, Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable minor;
		[NullAllowed, Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress;
		[Export ("initWithMacAddress:")]
		IntPtr Constructor (string macAddress);

		// -(instancetype _Nonnull)initWithBeacon:(CLBeacon * _Nonnull)beacon;
		[Export ("initWithBeacon:")]
		IntPtr Constructor (CLBeacon beacon);

		// -(instancetype _Nonnull)initWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor;
		[Export ("initWithProximityUUID:major:minor:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor);

		// -(void)sendRequestWithCompletion:(ESTRequestBeaconColorBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestBeaconColorBlock completion);
	}

	// typedef void (^ESTRequestBeaconMacBlock)(NSString * _Nullable, NSError * _Nullable);
	delegate void RequestBeaconMacBlock ([NullAllowed] string macAddress, [NullAllowed] NSError error);

	// @interface ESTRequestBeaconMac : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestBeaconMac")]
	interface RequestBeaconMac
	{
		// @property (readonly, nonatomic, strong) NSUUID * _Nonnull beaconProximityUUID;
		[Export ("beaconProximityUUID", ArgumentSemantic.Strong)]
		NSUuid BeaconProximityUuid { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull beaconMajor;
		[Export ("beaconMajor", ArgumentSemantic.Strong)]
		NSNumber BeaconMajor { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull beaconMinor;
		[Export ("beaconMinor", ArgumentSemantic.Strong)]
		NSNumber BeaconMinor { get; }

		// -(instancetype _Nonnull)initWithBeacon:(CLBeacon * _Nonnull)beacon;
		[Export ("initWithBeacon:")]
		IntPtr Constructor (CLBeacon beacon);

		// -(instancetype _Nonnull)initWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(short)major minor:(short)minor;
		[Export ("initWithProximityUUID:major:minor:")]
		IntPtr Constructor (NSUuid proximityUUID, short major, short minor);

		// -(void)sendRequestWithCompletion:(ESTRequestBeaconMacBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestBeaconMacBlock completion);
	}

	// @interface ESTRequestPostJSON : ESTRequestBase
	[BaseType (typeof (RequestBase), Name = "ESTRequestPostJSON")]
	interface RequestPostJson
	{
		// -(void)setParams:(id _Nonnull)params forRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("setParams:forRequest:")]
		void SetParams (NSObject @params, NSMutableUrlRequest request);
	}

	// @interface ESTRequestPutJSON : ESTRequestPostJSON
	[BaseType (typeof (RequestPostJson), Name = "ESTRequestPutJSON")]
	interface RequestPutJson
	{
		// -(void)setParams:(id _Nonnull)params forRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("setParams:forRequest:")]
		void SetParams (NSObject @params, NSMutableUrlRequest request);
	}

	// typedef void (^ESTRequestAssignGPSLocationBlock)(CLLocation * _Nullable, NSError * _Nullable);
	delegate void RequestAssignGpsLocationBlock ([NullAllowed] CLLocation result, [NullAllowed] NSError error);

	// @interface ESTRequestAssignGPSLocation : ESTRequestPutJSON
	[BaseType (typeof (RequestPutJson), Name = "ESTRequestAssignGPSLocation")]
	interface RequestAssignGpsLocation
	{
		// -(instancetype _Nonnull)initWithBeacon:(CLBeacon * _Nonnull)beacon location:(CLLocation * _Nonnull)location;
		[Export ("initWithBeacon:location:")]
		IntPtr Constructor (CLBeacon beacon, CLLocation location);

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress location:(CLLocation * _Nonnull)location;
		[Export ("initWithMacAddress:location:")]
		IntPtr Constructor (string macAddress, CLLocation location);

		// -(instancetype _Nonnull)initWithProximityUUID:(NSUUID * _Nonnull)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor;
		[Export ("initWithProximityUUID:major:minor:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor);

		// -(void)sendRequestWithCompletion:(ESTRequestAssignGPSLocationBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestAssignGpsLocationBlock completion);
	}

	// typedef void (^ESTRequestRegisterDeviceBlock)(NSError * _Nullable);
	delegate void RequestRegisterDeviceBlock ([NullAllowed] NSError error);

	// @interface ESTRequestRegisterDevice : ESTRequestPostJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestPostJson), Name = "ESTRequestRegisterDevice")]
	interface RequestRegisterDevice
	{
		// @property (readonly, nonatomic, strong) NSData * _Nonnull deviceToken;
		[Export ("deviceToken", ArgumentSemantic.Strong)]
		NSData DeviceToken { get; }

		// -(instancetype _Nonnull)initWithDeviceToken:(NSData * _Nonnull)deviceToken;
		[Export ("initWithDeviceToken:")]
		IntPtr Constructor (NSData deviceToken);

		// -(void)sendRequestWithCompletion:(ESTRequestRegisterDeviceBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestRegisterDeviceBlock completion);
	}

	// typedef void (^ESTRequestGetPendingSettingsBlock)(NSArray<ESTBeaconUpdateInfo *> * _Nullable, NSError * _Nullable);
	delegate void RequestGetPendingSettingsBlock ([NullAllowed] BeaconUpdateInfo[] beaconUpdateInfos, [NullAllowed] NSError error);

	// @interface ESTRequestGetPendingSettings : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetPendingSettings")]
	interface RequestGetPendingSettings
	{
		// -(void)sendRequestWithCompletion:(ESTRequestGetPendingSettingsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetPendingSettingsBlock completion);
	}

	// typedef void (^ESTRequestV2DeletePendingSettingsBlock)(id _Nullable, NSError * _Nullable);
	delegate void RequestV2DeletePendingSettingsBlock ([NullAllowed] NSObject result, [NullAllowed] NSError error);

	// @interface ESTRequestV2DeletePendingSettings : ESTRequestPostJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestPostJson), Name = "ESTRequestV2DeletePendingSettings")]
	interface RequestV2DeletePendingSettings
	{
		// @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull deviceIdentifiers;
		[Export ("deviceIdentifiers", ArgumentSemantic.Strong)]
		string[] DeviceIdentifiers { get; }

		// -(instancetype _Nonnull)initWithDeviceIdentifiers:(NSArray<NSString *> * _Nonnull)deviceIdentifiers;
		[Export ("initWithDeviceIdentifiers:")]
		IntPtr Constructor (string[] deviceIdentifiers);

		// -(void)sendRequestWithCompletion:(ESTRequestV2DeletePendingSettingsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestV2DeletePendingSettingsBlock completion);
	}

	// @interface ESTRequestDelete : ESTRequestBase
	[BaseType (typeof (RequestBase), Name = "ESTRequestDelete")]
	interface RequestDelete
	{
		// -(void)setParams:(id _Nonnull)params forRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("setParams:forRequest:")]
		void SetParams (NSObject @params, NSMutableUrlRequest request);
	}

	// typedef void (^ESTRequestCancelPendingSettingsBlock)(NSError * _Nullable);
	delegate void RequestCancelPendingSettingsBlock ([NullAllowed] NSError error);

	// @interface ESTRequestCancelPendingSettings : ESTRequestDelete
	[DisableDefaultCtor]
	[BaseType (typeof (RequestDelete), Name = "ESTRequestCancelPendingSettings")]
	interface RequestCancelPendingSettings
	{
		// @property (readonly, nonatomic) NSString * _Nonnull macAddress;
		[Export ("macAddress")]
		string MacAddress { get; }

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress;
		[Export ("initWithMacAddress:")]
		IntPtr Constructor (string macAddress);

		// -(void)sendRequestWithCompletion:(ESTRequestCancelPendingSettingsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestCancelPendingSettingsBlock completion);
	}

	// typedef void (^ESTRequestGetSettingsHistoryBlock)(NSArray<ESTBeaconUpdateInfo *> * _Nullable, NSError * _Nullable);
	delegate void RequestGetSettingsHistoryBlock ([NullAllowed] BeaconUpdateInfo[] beaconUpdateInfos, [NullAllowed] NSError error);

	// @interface ESTRequestGetSettingsHistory : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetSettingsHistory")]
	interface RequestGetSettingsHistory
	{
		// @property (readonly, nonatomic) NSString * _Nonnull macAddress;
		[Export ("macAddress")]
		string MacAddress { get; }

		// -(instancetype _Nonnull)initWithMacAddress:(NSString * _Nonnull)macAddress;
		[Export ("initWithMacAddress:")]
		IntPtr Constructor (string macAddress);

		// -(void)sendRequestWithCompletion:(ESTRequestGetSettingsHistoryBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetSettingsHistoryBlock completion);
	}

	// typedef void (^ESTRequestGetNearablesBlock)(NSArray<ESTNearable *> * _Nullable, NSError * _Nullable);
	delegate void RequestGetNearablesBlock ([NullAllowed] Nearable[] nearables, [NullAllowed] NSError error);

	// @interface ESTRequestGetNearables : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetNearables")]
	interface RequestGetNearables
	{
		// -(void)sendRequestWithCompletion:(ESTRequestGetNearablesBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetNearablesBlock completion);
	}

	// @interface ESTNearableVO : NSObject <NSCoding>
	[BaseType (typeof (NSObject), Name = "ESTNearableVO")]
	interface NearableVO : INSCoding
	{
		// @property (nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; set; }

		// @property (assign, nonatomic) ESTNearableType type;
		[Export ("type", ArgumentSemantic.Assign)]
		NearableType Type { get; set; }

		// @property (assign, nonatomic) ESTColor color;
		[Export ("color", ArgumentSemantic.Assign)]
		Color Color { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable indoorLocationName;
		[NullAllowed, Export ("indoorLocationName", ArgumentSemantic.Strong)]
		string IndoorLocationName { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable indoorLocationIdentifier;
		[NullAllowed, Export ("indoorLocationIdentifier", ArgumentSemantic.Strong)]
		string IndoorLocationIdentifier { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull advInterval;
		[Export ("advInterval", ArgumentSemantic.Strong)]
		NSNumber AdvInterval { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull power;
		[Export ("power", ArgumentSemantic.Strong)]
		NSNumber Power { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull hardware;
		[Export ("hardware", ArgumentSemantic.Strong)]
		string Hardware { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull firmware;
		[Export ("firmware", ArgumentSemantic.Strong)]
		string Firmware { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull motionOnly;
		[Export ("motionOnly", ArgumentSemantic.Strong)]
		NSNumber MotionOnly { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull broadcastingScheme;
		[Export ("broadcastingScheme", ArgumentSemantic.Strong)]
		NSNumber BroadcastingScheme { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		string ProximityUuid { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; set; }

		// @property (nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull eddystoneURL;
		[Export ("eddystoneURL", ArgumentSemantic.Strong)]
		string EddystoneUrl { get; set; }

		// -(instancetype _Nonnull)initWithData:(NSDictionary * _Nonnull)data;
		[Export ("initWithData:")]
		IntPtr Constructor (NSDictionary data);
	}

	// @interface ESTNearableFirmwareVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTNearableFirmwareVO")]
	interface NearableFirmwareVO
	{
		// @property (nonatomic, strong) NSNumber * firmwareId;
		[Export ("firmwareId", ArgumentSemantic.Strong)]
		NSNumber FirmwareId { get; set; }

		// @property (nonatomic, strong) NSString * firmwareName;
		[Export ("firmwareName", ArgumentSemantic.Strong)]
		string FirmwareName { get; set; }
	}

	// @interface ESTNearableFirmwareUpdateVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTNearableFirmwareUpdateVO")]
	interface NearableFirmwareUpdateVO
	{
	}

	// @interface ESTAnalyticsEventVO : NSObject <NSCoding, NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTAnalyticsEventVO")]
	interface AnalyticsEventVO : INSCoding, INSCopying
	{
		// @property (readonly, nonatomic, strong) CLBeaconRegion * region;
		[Export ("region", ArgumentSemantic.Strong)]
		CLBeaconRegion Region { get; }

		// @property (readonly, nonatomic, strong) CLLocation * location;
		[Export ("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; }

		// @property (readonly, nonatomic, strong) NSNumber * eventType;
		[Export ("eventType", ArgumentSemantic.Strong)]
		NSNumber EventType { get; }

		// @property (readonly, nonatomic, strong) NSNumber * timestamp;
		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }

		// @property (readonly, nonatomic, strong) NSNumber * inForeground;
		[Export ("inForeground", ArgumentSemantic.Strong)]
		NSNumber InForeground { get; }

		// -(instancetype)initWithBeaconRegion:(CLBeaconRegion *)region location:(CLLocation *)location eventType:(ESTAnalyticsEventType)eventType;
		[Export ("initWithBeaconRegion:location:eventType:")]
		IntPtr Constructor (CLBeaconRegion region, CLLocation location, AnalyticsEventType eventType);

		// +(NSNumber *)getCurrentTimestamp;
		[Static]
		[Export ("getCurrentTimestamp")]
		NSNumber CurrentTimestamp { get; }
	}

	// typedef void (^ESTRequestAnalyticsGroupTrackBlock)(NSError *);
	delegate void RequestAnalyticsGroupTrackBlock (NSError error);

	// @interface ESTRequestAnalyticsTrack : ESTRequestPostJSON
	[BaseType (typeof (RequestPostJson), Name = "ESTRequestAnalyticsTrack")]
	interface RequestAnalyticsTrack
	{
		// -(instancetype)initWithEvents:(NSArray *)events;
		[Export ("initWithEvents:")]
		//[Verify (StronglyTypedNSArray)]
		IntPtr Constructor (NSObject[] events);

		// -(void)sendRequestWithCompletion:(ESTRequestAnalyticsGroupTrackBlock)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestAnalyticsGroupTrackBlock completion);
	}

	// @interface ESTAnalyticsManager : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTAnalyticsManager")]
	interface AnalyticsManager
	{
		// +(instancetype)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		AnalyticsManager SharedInstance { get; }

		// -(void)registerAnalyticsEventWithRegion:(CLBeaconRegion *)region type:(ESTAnalyticsEventType)eventType;
		[Export ("registerAnalyticsEventWithRegion:type:")]
		void RegisterAnalyticsEvent (CLBeaconRegion region, AnalyticsEventType eventType);

		// -(void)sendRegisteredEvents;
		[Export ("sendRegisteredEvents")]
		void SendRegisteredEvents ();

		// +(ESTAnalyticsEventType)getEventTypeForProximity:(CLProximity)proximity;
		[Static]
		[Export ("getEventTypeForProximity:")]
		AnalyticsEventType GetEventType (CLProximity proximity);

		// +(void)enableMonitoringAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableMonitoringAnalytics:")]
		void EnableMonitoringAnalytics (bool enable);

		// +(void)enableRangingAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableRangingAnalytics:")]
		void EnableRangingAnalytics (bool enable);

		// +(void)enableGPSPositioningForAnalytics:(BOOL)enable;
		[Static]
		[Export ("enableGPSPositioningForAnalytics:")]
		void EnableGpsPositioningForAnalytics (bool enable);

		// +(BOOL)isMonitoringAnalyticsEnabled;
		[Static]
		[Export ("isMonitoringAnalyticsEnabled")]
		bool IsMonitoringAnalyticsEnabled { get; }

		// +(BOOL)isRangingAnalyticsEnabled;
		[Static]
		[Export ("isRangingAnalyticsEnabled")]
		bool IsRangingAnalyticsEnabled { get; }
	}

	// typedef void (^ESTRequestV2GetDeviceDetailsBlock)(ESTDeviceDetails * _Nullable, NSError * _Nullable);
	delegate void RequestV2GetDeviceDetailsBlock ([NullAllowed] DeviceDetails deviceDetails, [NullAllowed] NSError error);

	// @interface ESTRequestV2GetDeviceDetails : ESTRequestGetJSON
	[Obsolete ("Please use `RequestGetDeviceDetails` class for fetching device's details from Estimote Cloud.")]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV2GetDeviceDetails")]
	interface RequestV2GetDeviceDetails
	{
		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier;
		[Export ("initWithDeviceIdentifier:")]
		IntPtr Constructor (string deviceIdentifier);

		// -(void)sendRequestWithCompletion:(ESTRequestV2GetDeviceDetailsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestV2GetDeviceDetailsBlock completion);
	}

	// typedef void (^ESTRequestV2GetDevicesBlock)(NSArray<ESTDeviceDetails *> * _Nullable, NSError * _Nullable);
	delegate void RequestV2GetDevicesBlock ([NullAllowed] DeviceDetails[] devicesDetails, [NullAllowed] NSError error);

	// @interface ESTRequestV2GetDevices : ESTRequestGetJSON
	[Obsolete ("Please use `RequestGetDevices` class for fetching user's from Estimote Cloud.")]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV2GetDevices")]
	interface RequestV2GetDevices
	{
		// -(void)sendRequestWithCompletion:(ESTRequestV2GetDevicesBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestV2GetDevicesBlock completion);
	}

	// @interface ESTBaseVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBaseVO")]
	interface BaseVO
	{
		// -(id)objectForKey:(NSString *)aKey inDictionary:(NSDictionary *)dict;
		[Export ("objectForKey:inDictionary:")]
		NSObject ObjectForKey (string aKey, NSDictionary dict);
	}

	// @interface ESTDeviceUpdateInfo : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceUpdateInfo")]
	interface DeviceUpdateInfo
	{
		// @property (readonly, nonatomic, strong) NSString * identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, assign, nonatomic) BOOL pendingSettingsAvailable;
		[Export ("pendingSettingsAvailable")]
		bool PendingSettingsAvailable { get; }

		// @property (readonly, assign, nonatomic) BOOL firmwareUpdateAvailable;
		[Export ("firmwareUpdateAvailable")]
		bool FirmwareUpdateAvailable { get; }

		// -(instancetype)initWithDeviceIdentifier:(NSString *)identifier pendingSettingsAvailable:(BOOL)pendingSettingsAvailable firmwareUpdateAvailable:(BOOL)firmwareUpdateAvailable;
		[Export ("initWithDeviceIdentifier:pendingSettingsAvailable:firmwareUpdateAvailable:")]
		IntPtr Constructor (string identifier, bool pendingSettingsAvailable, bool firmwareUpdateAvailable);

		// -(instancetype)initWithCloudDictionary:(NSDictionary *)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	// typedef void (^ESTRequestV2DevicesUpdateBlock)(NSArray<ESTDeviceUpdateInfo *> * _Nullable, NSError * _Nullable);
	delegate void RequestV2DevicesUpdateBlock ([NullAllowed] DeviceUpdateInfo[] result, [NullAllowed] NSError error);

	// @interface ESTRequestV2DevicesUpdate : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV2DevicesUpdate")]
	interface RequestV2DevicesUpdate
	{
		// -(void)sendRequestWithCompletion:(ESTRequestV2DevicesUpdateBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestV2DevicesUpdateBlock completion);
	}

	// @interface ESTDeviceDetails : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceDetails")]
	interface DeviceDetails
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull hardwareType;
		[Export ("hardwareType", ArgumentSemantic.Strong)]
		string HardwareType { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull hardwareRevision;
		[Export ("hardwareRevision", ArgumentSemantic.Strong)]
		string HardwareRevision { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull hardwareFootprint;
		[Export ("hardwareFootprint", ArgumentSemantic.Strong)]
		string HardwareFootprint { get; }

		// @property (readonly, nonatomic) ESTColor color;
		[Export ("color")]
		Color Color { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull formFactor;
		[Export ("formFactor", ArgumentSemantic.Strong)]
		string FormFactor { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceShadow * _Nonnull shadow;
		[Export ("shadow", ArgumentSemantic.Strong)]
		DeviceShadow Shadow { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceSettings * _Nonnull settings;
		[Export ("settings", ArgumentSemantic.Strong)]
		DeviceSettings Settings { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceSettings * _Nonnull pendingSettings;
		[Export ("pendingSettings", ArgumentSemantic.Strong)]
		DeviceSettings PendingSettings { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceStatusReport * _Nonnull statusReport;
		[Export ("statusReport", ArgumentSemantic.Strong)]
		DeviceStatusReport StatusReport { get; }

		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	// @interface ESTDeviceGeoLocation : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceGeoLocation")]
	interface DeviceGeoLocation
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nullable latitude;
		[NullAllowed, Export ("latitude", ArgumentSemantic.Strong)]
		NSNumber Latitude { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable longitude;
		[NullAllowed, Export ("longitude", ArgumentSemantic.Strong)]
		NSNumber Longitude { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable country;
		[NullAllowed, Export ("country", ArgumentSemantic.Strong)]
		string Country { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable zipCode;
		[NullAllowed, Export ("zipCode", ArgumentSemantic.Strong)]
		string ZipCode { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable state;
		[NullAllowed, Export ("state", ArgumentSemantic.Strong)]
		string State { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable stateCode;
		[NullAllowed, Export ("stateCode", ArgumentSemantic.Strong)]
		string StateCode { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable city;
		[NullAllowed, Export ("city", ArgumentSemantic.Strong)]
		string City { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable streetName;
		[NullAllowed, Export ("streetName", ArgumentSemantic.Strong)]
		string StreetName { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable streetNumber;
		[NullAllowed, Export ("streetNumber", ArgumentSemantic.Strong)]
		string StreetNumber { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable formattedAddress;
		[NullAllowed, Export ("formattedAddress", ArgumentSemantic.Strong)]
		string FormattedAddress { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable accuracy;
		[NullAllowed, Export ("accuracy", ArgumentSemantic.Strong)]
		NSNumber Accuracy { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable timezone;
		[NullAllowed, Export ("timezone", ArgumentSemantic.Strong)]
		string Timezone { get; }

		// -(instancetype _Nonnull)initWithLatitude:(NSNumber * _Nonnull)latitude longitude:(NSNumber * _Nonnull)longitude;
		[Export ("initWithLatitude:longitude:")]
		IntPtr Constructor (NSNumber latitude, NSNumber longitude);

		// Moved from ESTDeviceGeoLocation_Internal
		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	//// @interface Internal (ESTDeviceGeoLocation)
	//[Category]
	//[BaseType (typeof (DeviceGeoLocation))]
	//interface ESTDeviceGeoLocation_Internal
	//{
	//	 Moved to DeviceGeoLocation
	//	// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
	//	[Export ("initWithCloudDictionary:")]
	//	IntPtr Constructor (NSDictionary dictionary);
	//}

	// @interface ESTDeviceIndoorLocation : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceIndoorLocation")]
	interface DeviceIndoorLocation
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; }

		// Moved from ESTDeviceIndoorLocation_Internal
		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	//// @interface Internal (ESTDeviceIndoorLocation)
	//[Category]
	//[BaseType (typeof (DeviceIndoorLocation))]
	//interface ESTDeviceIndoorLocation_Internal
	//{
	//	Moved to DeviceIndoorLocation
	//	// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
	//	[Export ("initWithCloudDictionary:")]
	//	IntPtr Constructor (NSDictionary dictionary);
	//}

	// @interface ESTDeviceShadow : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceShadow")]
	interface DeviceShadow
	{
		// @property (readonly, nonatomic, strong) NSString * identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; }

		// @property (readonly, nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; }

		// @property (readonly, nonatomic, strong) NSArray<NSString *> * tags;
		[Export ("tags", ArgumentSemantic.Strong)]
		string[] Tags { get; }

		// @property (readonly, nonatomic, strong) NSNumber * developmentMode;
		[Export ("developmentMode", ArgumentSemantic.Strong)]
		NSNumber DevelopmentMode { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceGeoLocation * geoLocation;
		[Export ("geoLocation", ArgumentSemantic.Strong)]
		DeviceGeoLocation GeoLocation { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceIndoorLocation * indoorLocation;
		[Export ("indoorLocation", ArgumentSemantic.Strong)]
		DeviceIndoorLocation IndoorLocation { get; }

		// -(instancetype)initWithCloudDictionary:(NSDictionary *)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	// @interface ESTDeviceSettings : ESTBaseVO <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceSettings")]
	interface DeviceSettings : INSCopying
	{
		// @property (readonly, nonatomic, strong) ESTDeviceSettingsGeneral * _Nonnull general;
		[Export ("general", ArgumentSemantic.Strong)]
		DeviceSettingsGeneral General { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserConnectivity *> * _Nonnull connectivity;
		[Export ("connectivity", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserConnectivity[] Connectivity { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserIBeacon *> * _Nonnull iBeacon;
		[Export ("iBeacon", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserIBeacon[] IBeacon { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneUID *> * _Nonnull eddystoneUID;
		[Export ("eddystoneUID", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEddystoneUid[] EddystoneUid { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneURL *> * _Nonnull eddystoneURL;
		[Export ("eddystoneURL", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEddystoneUrl[] EddystoneUrl { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneTLM *> * _Nonnull eddystoneTLM;
		[Export ("eddystoneTLM", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEddystoneTlm[] EddystoneTlm { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneEID *> * _Nonnull eddystoneEID;
		[Export ("eddystoneEID", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEddystoneEid[] EddystoneEid { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserGeneric *> * _Nonnull generic;
		[Export ("generic", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserGeneric[] Generic { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEstimoteLocation *> * _Nonnull estimoteLocation;
		[Export ("estimoteLocation", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEstimoteLocation[] EstimoteLocation { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEstimoteTLM *> * _Nonnull estimoteTLM;
		[Export ("estimoteTLM", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserEstimoteTlm[] EstimoteTlm { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserUWB *> * _Nonnull uwb;
		[Export ("uwb", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserUwb[] Uwb { get; }

		// @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserMesh *> * _Nonnull mesh;
		[Export ("mesh", ArgumentSemantic.Strong)]
		DeviceSettingsAdvertiserMesh[] Mesh { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceSettingsGPIO * _Nonnull gpio;
		[Export ("gpio", ArgumentSemantic.Strong)]
		DeviceSettingsGpio Gpio { get; }

		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);

		// -(ESTDeviceSettings * _Nonnull)settingsUpdatedWithDeviceSettings:(ESTDeviceSettings * _Nonnull)deviceSettings;
		[Export ("settingsUpdatedWithDeviceSettings:")]
		DeviceSettings SettingsUpdated (DeviceSettings deviceSettings);

		// -(NSDictionary * _Nonnull)cloudDictionary;
		[Export ("cloudDictionary")]
		NSDictionary CloudDictionary { get; }
	}

	// @interface ESTDeviceSettingsGeneral : ESTBaseVO <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceSettingsGeneral")]
	interface DeviceSettingsGeneral : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull motionDetectionEnabled;
		[Export ("motionDetectionEnabled", ArgumentSemantic.Strong)]
		NSNumber MotionDetectionEnabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull darkToSleepEnabled;
		[Export ("darkToSleepEnabled", ArgumentSemantic.Strong)]
		NSNumber DarkToSleepEnabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull darkToSleepThresholdInLux;
		[Export ("darkToSleepThresholdInLux", ArgumentSemantic.Strong)]
		NSNumber DarkToSleepThresholdInLux { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull flipToSleepEnabled;
		[Export ("flipToSleepEnabled", ArgumentSemantic.Strong)]
		NSNumber FlipToSleepEnabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull temperatureOffsetInCelsius;
		[Export ("temperatureOffsetInCelsius", ArgumentSemantic.Strong)]
		NSNumber TemperatureOffsetInCelsius { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull smartPowerModeEnabled;
		[Export ("smartPowerModeEnabled", ArgumentSemantic.Strong)]
		NSNumber SmartPowerModeEnabled { get; }

		// @property (readonly, nonatomic, strong) ESTDeviceSchedule * _Nonnull schedule;
		[Export ("schedule", ArgumentSemantic.Strong)]
		DeviceSchedule Schedule { get; }

		// @property (readonly, nonatomic) NSNumber * _Nonnull eddystoneConfigurationServiceEnabled;
		[Export ("eddystoneConfigurationServiceEnabled")]
		NSNumber EddystoneConfigurationServiceEnabled { get; }

		// @property (readonly, nonatomic) NSNumber * _Nonnull motionOnlyEnabled;
		[Export ("motionOnlyEnabled")]
		NSNumber MotionOnlyEnabled { get; }

		// @property (readonly, nonatomic) NSNumber * _Nonnull motionOnlyDelay;
		[Export ("motionOnlyDelay")]
		NSNumber MotionOnlyDelay { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull automaticFirmwareUpdateEnabled;
		[Export ("automaticFirmwareUpdateEnabled", ArgumentSemantic.Strong)]
		NSNumber AutomaticFirmwareUpdateEnabled { get; }

		// @property (readonly, nonatomic, strong) NSArray<NSNumber *> * _Nonnull magnetometerCalibrationData;
		[Export ("magnetometerCalibrationData", ArgumentSemantic.Strong)]
		NSNumber[] MagnetometerCalibrationData { get; }

		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);

		// -(void)updateWithGeneralSettings:(ESTDeviceSettingsGeneral * _Nonnull)generalSettings;
		[Export ("updateWithGeneralSettings:")]
		void Update (DeviceSettingsGeneral generalSettings);

		// -(NSDictionary * _Nonnull)cloudDictionary;
		[Export ("cloudDictionary")]
		NSDictionary CloudDictionary { get; }
	}

	// @interface ESTDeviceSettingsAdvertiser : ESTBaseVO <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceSettingsAdvertiser")]
	interface DeviceSettingsAdvertiser : INSCopying
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull index;
		[Export ("index", ArgumentSemantic.Strong)]
		NSNumber Index { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull enabled;
		[Export ("enabled", ArgumentSemantic.Strong)]
		NSNumber Enabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull powerInDBm;
		[Export ("powerInDBm", ArgumentSemantic.Strong)]
		NSNumber PowerInDbm { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull intervalInSeconds;
		[Export ("intervalInSeconds", ArgumentSemantic.Strong)]
		NSNumber IntervalInSeconds { get; }

		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);

		// -(void)updateWithAdvertiserSettings:(ESTDeviceSettingsAdvertiser * _Nonnull)advertiserSettings;
		[Export ("updateWithAdvertiserSettings:")]
		void Update (DeviceSettingsAdvertiser advertiserSettings);

		// -(NSDictionary * _Nonnull)cloudDictionary;
		[Export ("cloudDictionary")]
		NSDictionary CloudDictionary { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserConnectivity : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserConnectivity")]
	interface DeviceSettingsAdvertiserConnectivity
	{
		// @property (readonly, nonatomic) NSNumber * _Nonnull shakeToConnectEnabled;
		[Export ("shakeToConnectEnabled")]
		NSNumber ShakeToConnectEnabled { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserIBeacon : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserIBeacon")]
	interface DeviceSettingsAdvertiserIBeacon
	{
		// @property (readonly, nonatomic, strong) NSUUID * _Nonnull proximityUUID;
		[Export ("proximityUUID", ArgumentSemantic.Strong)]
		NSUuid ProximityUuid { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull major;
		[Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull minor;
		[Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull nonStrictModeEnabled;
		[Export ("nonStrictModeEnabled", ArgumentSemantic.Strong)]
		NSNumber NonStrictModeEnabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull securityEnabled;
		[Export ("securityEnabled", ArgumentSemantic.Strong)]
		NSNumber SecurityEnabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull securityIntervalScaler;
		[Export ("securityIntervalScaler", ArgumentSemantic.Strong)]
		NSNumber SecurityIntervalScaler { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull motionUUIDEnabled;
		[Export ("motionUUIDEnabled", ArgumentSemantic.Strong)]
		NSNumber MotionUuidEnabled { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserEddystoneUID : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEddystoneUID")]
	interface DeviceSettingsAdvertiserEddystoneUid
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull namespaceID;
		[Export ("namespaceID", ArgumentSemantic.Strong)]
		string NamespaceId { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull instanceID;
		[Export ("instanceID", ArgumentSemantic.Strong)]
		string InstanceId { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserEddystoneURL : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEddystoneURL")]
	interface DeviceSettingsAdvertiserEddystoneUrl
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull url;
		[Export ("url", ArgumentSemantic.Strong)]
		string Url { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserEddystoneTLM : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEddystoneTLM")]
	interface DeviceSettingsAdvertiserEddystoneTlm
	{
	}

	// @interface ESTDeviceSettingsAdvertiserEddystoneEID : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEddystoneEID")]
	interface DeviceSettingsAdvertiserEddystoneEid
	{
		// @property (readonly, nonatomic, strong) NSString * identityKey;
		[Export ("identityKey", ArgumentSemantic.Strong)]
		string IdentityKey { get; }

		// @property (readonly, nonatomic, strong) NSNumber * rotationScaler;
		[Export ("rotationScaler", ArgumentSemantic.Strong)]
		NSNumber RotationScaler { get; }

		// @property (readonly, nonatomic, strong) NSString * registeredNamespaceID;
		[Export ("registeredNamespaceID", ArgumentSemantic.Strong)]
		string RegisteredNamespaceId { get; }

		// @property (readonly, nonatomic, strong) NSString * registeredInstanceID;
		[Export ("registeredInstanceID", ArgumentSemantic.Strong)]
		string RegisteredInstanceId { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserEstimoteLocation : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEstimoteLocation")]
	interface DeviceSettingsAdvertiserEstimoteLocation
	{
	}

	// @interface ESTDeviceSettingsAdvertiserEstimoteTLM : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEstimoteTLM")]
	interface DeviceSettingsAdvertiserEstimoteTlm
	{
	}

	// @interface ESTDeviceSettingsAdvertiserGeneric : ESTDeviceSettingsAdvertiser
	[BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserGeneric")]
	interface DeviceSettingsAdvertiserGeneric
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull payload;
		[Export ("payload", ArgumentSemantic.Strong)]
		string Payload { get; }
	}

	// @interface ESTDeviceStatusReport : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceStatusReport")]
	interface DeviceStatusReport
	{
		// @property (readonly, nonatomic, strong) NSNumber * batteryPercentage;
		[Export ("batteryPercentage", ArgumentSemantic.Strong)]
		NSNumber BatteryPercentage { get; }

		// @property (readonly, nonatomic, strong) NSNumber * batteryLifetime;
		[Export ("batteryLifetime", ArgumentSemantic.Strong)]
		NSNumber BatteryLifetime { get; }

		// @property (readonly, nonatomic, strong) NSNumber * batteryVoltageInVolts;
		[Export ("batteryVoltageInVolts", ArgumentSemantic.Strong)]
		NSNumber BatteryVoltageInVolts { get; }

		// @property (readonly, nonatomic, strong) NSNumber * clockOffsetInSeconds;
		[Export ("clockOffsetInSeconds", ArgumentSemantic.Strong)]
		NSNumber ClockOffsetInSeconds { get; }

		// @property (readonly, nonatomic, strong) NSString * firmwareVersion;
		[Export ("firmwareVersion", ArgumentSemantic.Strong)]
		string FirmwareVersion { get; }

		// @property (readonly, nonatomic, strong) NSDate * lastSync;
		[Export ("lastSync", ArgumentSemantic.Strong)]
		NSDate LastSync { get; }

		// -(instancetype)initWithCloudDictionary:(NSDictionary *)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);
	}

	// @interface ESTDeviceSchedule : ESTBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTDeviceSchedule")]
	interface DeviceSchedule
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull enabled;
		[Export ("enabled", ArgumentSemantic.Strong)]
		NSNumber Enabled { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable startTime;
		[NullAllowed, Export ("startTime", ArgumentSemantic.Strong)]
		NSNumber StartTime { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable stopTime;
		[NullAllowed, Export ("stopTime", ArgumentSemantic.Strong)]
		NSNumber StopTime { get; }

		// -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
		[Export ("initWithCloudDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);

		// -(NSDictionary * _Nonnull)cloudDictionary;
		[Export ("cloudDictionary")]
		NSDictionary CloudDictionary { get; }
	}

	// @interface ESTFirmwareInfoV4VO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTFirmwareInfoV4VO")]
	interface FirmwareInfoV4VO
	{
		// @property (assign, nonatomic) BOOL updateAvailable;
		[Export ("updateAvailable")]
		bool UpdateAvailable { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable version;
		[NullAllowed, Export ("version", ArgumentSemantic.Strong)]
		string Version { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable hardwareVersion;
		[NullAllowed, Export ("hardwareVersion", ArgumentSemantic.Strong)]
		string HardwareVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable applicationVersion;
		[NullAllowed, Export ("applicationVersion", ArgumentSemantic.Strong)]
		string ApplicationVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable bootloaderVersion;
		[NullAllowed, Export ("bootloaderVersion", ArgumentSemantic.Strong)]
		string BootloaderVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable softdeviceVersion;
		[NullAllowed, Export ("softdeviceVersion", ArgumentSemantic.Strong)]
		string SoftdeviceVersion { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable changelog;
		[NullAllowed, Export ("changelog", ArgumentSemantic.Strong)]
		string Changelog { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable tarURL;
		[NullAllowed, Export ("tarURL", ArgumentSemantic.Strong)]
		string TarUrl { get; set; }

		// +(BOOL)firmwareVersion:(NSString * _Nonnull)firmwareVersion isBiggerThan:(NSString * _Nonnull)referenceFirmwareVersion;
		[Static]
		[Export ("firmwareVersion:isBiggerThan:")]
		bool IsBiggerThan (string firmwareVersion, string referenceFirmwareVersion);
	}

	// @interface ESTMesh : ESTBaseVO <NSCopying, NSCoding>
	[DisableDefaultCtor]
	[BaseType (typeof (BaseVO), Name = "ESTMesh")]
	interface Mesh : INSCopying, INSCoding
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull networkIdentifier;
		[Export ("networkIdentifier", ArgumentSemantic.Strong)]
		NSNumber NetworkIdentifier { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull networkName;
		[Export ("networkName", ArgumentSemantic.Strong)]
		string NetworkName { get; }

		// @property (readonly, assign, nonatomic) ESTMeshNetworkType networkType;
		[Export ("networkType", ArgumentSemantic.Assign)]
		MeshNetworkType NetworkType { get; }

		// @property (readwrite, nonatomic, strong) NSArray<NSString *> * _Nonnull devices;
		[Export ("devices", ArgumentSemantic.Strong)]
		string[] Devices { get; set; }

		// @property (readonly, nonatomic, strong) ESTDeviceSettings * _Nonnull settings;
		[Export ("settings", ArgumentSemantic.Strong)]
		DeviceSettings Settings { get; }

		// -(instancetype _Nonnull)initWithCloudData:(NSDictionary * _Nonnull)cloudData;
		[Export ("initWithCloudData:")]
		IntPtr Constructor (NSDictionary cloudData);

		// -(void)addDevice:(ESTDeviceLocationBeacon * _Nonnull)device completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("addDevice:completion:"), Async]
		void AddDevice (DeviceLocationBeacon device, CompletionBlock completion);

		// -(void)removeDevice:(ESTDeviceLocationBeacon * _Nonnull)device completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("removeDevice:completion:"), Async]
		void RemoveDevice (DeviceLocationBeacon device, CompletionBlock completion);
	}

	// @interface ESTRequestV3GetFirmwares : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV3GetFirmwares")]
	interface RequestV3GetFirmwares
	{
	}

	// typedef void (^ESTRequestV3GetDeviceOwnerBlock)(NSString * _Nullable, NSError * _Nullable);
	delegate void RequestV3GetDeviceOwnerBlock ([NullAllowed] string emailAddress, [NullAllowed] NSError error);

	// @interface ESTRequestV3GetDeviceOwner : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV3GetDeviceOwner")]
	interface RequestV3GetDeviceOwner
	{
		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier;
		[Export ("initWithDeviceIdentifier:")]
		IntPtr Constructor (string deviceIdentifier);

		// -(void)sendRequestWithCompletion:(ESTRequestV3GetDeviceOwnerBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestV3GetDeviceOwnerBlock completion);
	}

	// typedef void (^ESTRequestGetDeviceDetailsBlock)(ESTDeviceDetails * _Nullable, NSError * _Nullable);
	delegate void RequestGetDeviceDetailsBlock ([NullAllowed] DeviceDetails deviceDetails, [NullAllowed] NSError error);

	// @interface ESTRequestGetDeviceDetails : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetDeviceDetails")]
	[DisableDefaultCtor]
	interface RequestGetDeviceDetails
	{
		// -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier __attribute__((objc_designated_initializer));
		[Export ("initWithDeviceIdentifier:")]
		[DesignatedInitializer]
		IntPtr Constructor (string deviceIdentifier);

		// -(void)sendRequestWithCompletion:(ESTRequestGetDeviceDetailsBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:"), Async]
		void SendRequest (RequestGetDeviceDetailsBlock completion);
	}

	// typedef void (^ESTRequestGetDevicesBlock)(NSArray<ESTDeviceDetails *> * _Nullable, NSNumber * _Nullable, NSNumber * _Nullable, NSError * _Nullable);
	delegate void RequestGetDevicesBlock ([NullAllowed] DeviceDetails[] devicesDetails, [NullAllowed] NSNumber totalCount, [NullAllowed] NSNumber nextPage, [NullAllowed] NSError error);

	// @interface ESTRequestGetDevices : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestGetDevices")]
	interface RequestGetDevices
	{
		// -(instancetype _Nonnull)initWithIdentifiers:(NSArray<NSString *> * _Nullable)identifiers type:(ESTRequestGetDevicesTypeMask)deviceType page:(NSNumber * _Nonnull)page __attribute__((objc_designated_initializer));
		[Export ("initWithIdentifiers:type:page:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string[] identifiers, RequestGetDevicesTypeMask deviceType, NSNumber page);

		//// -(instancetype _Nonnull)initWithIdentifiers:(NSArray<NSString *> * _Nullable)identifiers type:(ESTRequestGetDevicesTypeMask)deviceType;
		[Export ("initWithIdentifiers:type:")]
		IntPtr Constructor ([NullAllowed] string[] identifiers, RequestGetDevicesTypeMask deviceType);

		// -(void)sendRequestWithCompletion:(ESTRequestGetDevicesBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:")]
		void SendRequest (RequestGetDevicesBlock completion);
	}

	// @interface ESTTelemetryInfoMotion : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoMotion")]
	interface TelemetryInfoMotion
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull accelerationX;
		[Export ("accelerationX", ArgumentSemantic.Strong)]
		NSNumber AccelerationX { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull accelerationY;
		[Export ("accelerationY", ArgumentSemantic.Strong)]
		NSNumber AccelerationY { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull accelerationZ;
		[Export ("accelerationZ", ArgumentSemantic.Strong)]
		NSNumber AccelerationZ { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull previousMotionStateDurationInSeconds;
		[Export ("previousMotionStateDurationInSeconds", ArgumentSemantic.Strong)]
		NSNumber PreviousMotionStateDurationInSeconds { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull currentMotionStateDurationInSeconds;
		[Export ("currentMotionStateDurationInSeconds", ArgumentSemantic.Strong)]
		NSNumber CurrentMotionStateDurationInSeconds { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull motionState;
		[Export ("motionState", ArgumentSemantic.Strong)]
		NSNumber MotionState { get; }

		// -(instancetype _Nonnull)initWithAccelerationX:(NSNumber * _Nonnull)accelerationX accelerationY:(NSNumber * _Nonnull)accelerationY accelerationZ:(NSNumber * _Nonnull)accelerationZ previousMotionStateDurationInSeconds:(NSNumber * _Nonnull)previousMotionStateDurationInSeconds currentMotionStateDurationInSeconds:(NSNumber * _Nonnull)currentMotionStateDurationInSeconds motionState:(NSNumber * _Nonnull)motionState shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithAccelerationX:accelerationY:accelerationZ:previousMotionStateDurationInSeconds:currentMotionStateDurationInSeconds:motionState:shortIdentifier:")]
		IntPtr Constructor (NSNumber accelerationX, NSNumber accelerationY, NSNumber accelerationZ, NSNumber previousMotionStateDurationInSeconds, NSNumber currentMotionStateDurationInSeconds, NSNumber motionState, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationMotionCompletionBlock)(ESTTelemetryInfoMotion * _Nonnull);
	delegate void TelemetryNotificationMotionCompletionBlock (TelemetryInfoMotion motion);

	// @interface ESTTelemetryNotificationMotion : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationMotion")]
	interface TelemetryNotificationMotion : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationMotionCompletionBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationMotionCompletionBlock block);
	}

	// @interface ESTTelemetryInfoAmbientLight : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoAmbientLight")]
	interface TelemetryInfoAmbientLight
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull ambientLightLevelInLux;
		[Export ("ambientLightLevelInLux", ArgumentSemantic.Strong)]
		NSNumber AmbientLightLevelInLux { get; }

		// -(instancetype _Nonnull)initWithAmbientLightLevelInLux:(NSNumber * _Nonnull)ambientLightLevelInLux shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithAmbientLightLevelInLux:shortIdentifier:")]
		IntPtr Constructor (NSNumber ambientLightLevelInLux, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationAmbientLightNotificationBlock)(ESTTelemetryInfoAmbientLight * _Nonnull);
	delegate void TelemetryNotificationAmbientLightNotificationBlock (TelemetryInfoAmbientLight ambientLight);

	// @interface ESTTelemetryNotificationAmbientLight : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationAmbientLight")]
	interface TelemetryNotificationAmbientLight : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationAmbientLightNotificationBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationAmbientLightNotificationBlock block);
	}

	// @interface ESTTelemetryInfoTemperature : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoTemperature")]
	interface TelemetryInfoTemperature
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull temperatureInCelsius;
		[Export ("temperatureInCelsius", ArgumentSemantic.Strong)]
		NSNumber TemperatureInCelsius { get; }

		// -(instancetype _Nonnull)initWithTemperature:(NSNumber * _Nonnull)temperature shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithTemperature:shortIdentifier:")]
		IntPtr Constructor (NSNumber temperature, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationTemperatureNotificationBlock)(ESTTelemetryInfoTemperature * _Nonnull);
	delegate void TelemetryNotificationTemperatureNotificationBlock (TelemetryInfoTemperature temperature);

	// @interface ESTTelemetryNotificationTemperature : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationTemperature")]
	interface TelemetryNotificationTemperature : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationTemperatureNotificationBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationTemperatureNotificationBlock block);
	}

	// @interface ESTTelemetryInfoSystemStatus : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoSystemStatus")]
	interface TelemetryInfoSystemStatus
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull batteryVoltageInMillivolts;
		[Export ("batteryVoltageInMillivolts", ArgumentSemantic.Strong)]
		NSNumber BatteryVoltageInMillivolts { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull uptimeInSeconds;
		[Export ("uptimeInSeconds", ArgumentSemantic.Strong)]
		NSNumber UptimeInSeconds { get; }

		// -(instancetype _Nonnull)initWithBatteryVoltageInMillivolts:(NSNumber * _Nonnull)batteryVoltageInMillivolts uptimeInSeconds:(NSNumber * _Nonnull)uptimeInSeconds shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithBatteryVoltageInMillivolts:uptimeInSeconds:shortIdentifier:")]
		IntPtr Constructor (NSNumber batteryVoltageInMillivolts, NSNumber uptimeInSeconds, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationSystemStatusNotificationBlock)(ESTTelemetryInfoSystemStatus * _Nonnull);
	delegate void TelemetryNotificationSystemStatusNotificationBlock (TelemetryInfoSystemStatus systemStatus);

	// @interface ESTTelemetryNotificationSystemStatus : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationSystemStatus")]
	interface TelemetryNotificationSystemStatus : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationSystemStatusNotificationBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationSystemStatusNotificationBlock block);
	}

	// @interface ESTTelemetryInfoMagnetometer : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoMagnetometer")]
	interface TelemetryInfoMagnetometer
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull normalizedMagneticFieldX;
		[Export ("normalizedMagneticFieldX", ArgumentSemantic.Strong)]
		NSNumber NormalizedMagneticFieldX { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull normalizedMagneticFieldY;
		[Export ("normalizedMagneticFieldY", ArgumentSemantic.Strong)]
		NSNumber NormalizedMagneticFieldY { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull normalizedMagneticFieldZ;
		[Export ("normalizedMagneticFieldZ", ArgumentSemantic.Strong)]
		NSNumber NormalizedMagneticFieldZ { get; }

		// -(instancetype _Nonnull)initWithNormalizedMagneticFieldX:(NSNumber * _Nonnull)fieldX normalizedMagneticFieldY:(NSNumber * _Nonnull)fieldY normalizedMagneticFieldZ:(NSNumber * _Nonnull)fieldZ shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithNormalizedMagneticFieldX:normalizedMagneticFieldY:normalizedMagneticFieldZ:shortIdentifier:")]
		IntPtr Constructor (NSNumber fieldX, NSNumber fieldY, NSNumber fieldZ, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationMagnetometerNotificationBlock)(ESTTelemetryInfoMagnetometer * _Nonnull);
	delegate void TelemetryNotificationMagnetometerNotificationBlock (TelemetryInfoMagnetometer magnetometer);

	// @interface ESTTelemetryNotificationMagnetometer : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationMagnetometer")]
	interface TelemetryNotificationMagnetometer : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationMagnetometerNotificationBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationMagnetometerNotificationBlock block);
	}

	// @interface ESTTelemetryInfoGPIO : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoGPIO")]
	interface TelemetryInfoGpio
	{
		// @property (readonly, nonatomic, strong) ESTGPIOPortsData * _Nonnull portsData;
		[Export ("portsData", ArgumentSemantic.Strong)]
		GpioPortsData PortsData { get; }

		// -(instancetype _Nonnull)initWithGPIOPortsData:(ESTGPIOPortsData * _Nonnull)portsData shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithGPIOPortsData:shortIdentifier:")]
		IntPtr Constructor (GpioPortsData portsData, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationGPIONotificationBlock)(ESTTelemetryInfoGPIO * _Nonnull);
	delegate void TelemetryNotificationGpioNotificationBlock (TelemetryInfoGpio gpio);

	// @interface ESTTelemetryNotificationGPIO : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationGPIO")]
	interface TelemetryNotificationGpio : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationGPIONotificationBlock _Nonnull)notificationBlock;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationGpioNotificationBlock notificationBlock);
	}

	// @interface ESTTelemetryInfoPressure : ESTTelemetryInfo
	[DisableDefaultCtor]
	[BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoPressure")]
	interface TelemetryInfoPressure
	{
		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull pressureInPascals;
		[Export ("pressureInPascals", ArgumentSemantic.Strong)]
		NSNumber PressureInPascals { get; }

		// -(instancetype _Nonnull)initWithPressureInPascals:(NSNumber * _Nonnull)pressureInPascals shortIdentifier:(NSString * _Nonnull)shortIdentifier;
		[Export ("initWithPressureInPascals:shortIdentifier:")]
		IntPtr Constructor (NSNumber pressureInPascals, string shortIdentifier);
	}

	// typedef void (^ESTTelemetryNotificationPressureNotificationBlock)(ESTTelemetryInfoPressure * _Nonnull);
	delegate void TelemetryNotificationPressureNotificationBlock (TelemetryInfoPressure pressure);

	// @interface ESTTelemetryNotificationPressure : NSObject <ESTTelemetryNotificationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTTelemetryNotificationPressure")]
	interface TelemetryNotificationPressure : TelemetryNotificationProtocol
	{
		// -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationPressureNotificationBlock _Nonnull)block;
		[Export ("initWithNotificationBlock:")]
		IntPtr Constructor (TelemetryNotificationPressureNotificationBlock block);
	}

	interface IMonitoringManagerDelegate { }

	// @protocol ESTMonitoringManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTMonitoringManagerDelegate")]
	interface MonitoringManagerDelegate
	{
		// @optional -(void)monitoringManager:(ESTMonitoringManager * _Nonnull)manager didEnterRangeOfIdentifier:(NSString * _Nonnull)identifier;
		[Export ("monitoringManager:didEnterRangeOfIdentifier:"), EventArgs ("MonitoringManagerEnteredRange")]
		void EnteredRange (MonitoringManager manager, string identifier);

		// @optional -(void)monitoringManager:(ESTMonitoringManager * _Nonnull)manager didExitRangeOfIdentifier:(NSString * _Nonnull)identifier;
		[Export ("monitoringManager:didExitRangeOfIdentifier:"), EventArgs ("MonitoringManagerExitedRange")]
		void ExitedRange (MonitoringManager manager, string identifier);

		// @optional -(void)monitoringManagerDidStart:(ESTMonitoringManager * _Nonnull)manager;
		[Export ("monitoringManagerDidStart:"), EventArgs ("MonitoringManagerStarted")]
		void Started (MonitoringManager manager);

		// @optional -(void)monitoringManager:(ESTMonitoringManager * _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
		[Export ("monitoringManager:didFailWithError:"), EventArgs ("MonitoringManagerFailed")]
		void Failed (MonitoringManager manager, NSError error);
	}

	// @interface ESTMonitoringManager : NSObject
	[Obsolete ("Use MonitoringV2Manager class instead.")]
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTMonitoringManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (MonitoringManagerDelegate) })]
	interface MonitoringManager
	{
		// @property (nonatomic, weak) id<ESTMonitoringManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMonitoringManagerDelegate Delegate { get; set; }

		// -(void)startMonitoringForIdentifier:(NSString * _Nonnull)identifier inProximity:(ESTMonitoringProximity)proximity;
		[Export ("startMonitoringForIdentifier:inProximity:")]
		void StartMonitoring (string identifier, MonitoringProximity proximity);

		// -(void)startDefaultMonitoringForIdentifier:(NSString * _Nonnull)identifier;
		[Export ("startDefaultMonitoringForIdentifier:")]
		void StartDefaultMonitoring (string identifier);

		// -(void)stopMonitoring;
		[Export ("stopMonitoring")]
		void StopMonitoring ();

		// -(void)startTurboMode;
		[Export ("startTurboMode")]
		void StartTurboMode ();

		// -(void)stopTurboMode;
		[Export ("stopTurboMode")]
		void StopTurboMode ();
	}

	interface IMonitoringV2ManagerDelegate { }

	// @protocol ESTMonitoringV2ManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTMonitoringV2ManagerDelegate")]
	interface MonitoringV2ManagerDelegate
	{
		// @required -(void)monitoringManager:(ESTMonitoringV2Manager * _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("monitoringManager:didFailWithError:"), EventArgs ("MonitoringV2ManagerFailed")]
		void Failed (MonitoringV2Manager manager, NSError error);

		// @optional -(void)monitoringManagerDidStart:(ESTMonitoringV2Manager * _Nonnull)manager;
		[Export ("monitoringManagerDidStart:"), EventArgs ("MonitoringV2ManagerStarted")]
		void Started (MonitoringV2Manager manager);

		// @optional -(void)monitoringManager:(ESTMonitoringV2Manager * _Nonnull)manager didDetermineInitialState:(ESTMonitoringState)state forBeaconWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("monitoringManager:didDetermineInitialState:forBeaconWithIdentifier:"), EventArgs ("MonitoringV2ManagerDeterminedInitialState")]
		void DeterminedInitialState (MonitoringV2Manager manager, MonitoringState state, string identifier);

		// @optional -(void)monitoringManager:(ESTMonitoringV2Manager * _Nonnull)manager didEnterDesiredRangeOfBeaconWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("monitoringManager:didEnterDesiredRangeOfBeaconWithIdentifier:"), EventArgs ("MonitoringV2ManagerEnterdDesiredRange")]
		void EnterdDesiredRange (MonitoringV2Manager manager, string identifier);

		// @optional -(void)monitoringManager:(ESTMonitoringV2Manager * _Nonnull)manager didExitDesiredRangeOfBeaconWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("monitoringManager:didExitDesiredRangeOfBeaconWithIdentifier:"), EventArgs ("MonitoringV2ManagerExitedDesiredRange")]
		void ExitedDesiredRange (MonitoringV2Manager manager, string identifier);

		// @optional -(void)monitoringManager:(ESTMonitoringV2Manager * _Nonnull)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)authorizationStatus;
		[Export ("monitoringManager:didChangeAuthorizationStatus:"), EventArgs ("MonitoringV2ManagerChangedAuthorizationStatus")]
		void ChangedAuthorizationStatus (MonitoringV2Manager manager, CLAuthorizationStatus authorizationStatus);
	}

	// @interface ESTMonitoringV2Manager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTMonitoringV2Manager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (MonitoringV2ManagerDelegate) })]
	[DisableDefaultCtor]
	interface MonitoringV2Manager
	{
		// @property (readonly, assign, nonatomic) double desiredMeanTriggerDistance;
		[Export ("desiredMeanTriggerDistance")]
		double DesiredMeanTriggerDistance { get; }

		// @property (readwrite, nonatomic, weak) id<ESTMonitoringV2ManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMonitoringV2ManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) CLBeaconRegion * _Nullable backgroundSupportRegion;
		[NullAllowed, Export ("backgroundSupportRegion", ArgumentSemantic.Strong)]
		CLBeaconRegion BackgroundSupportRegion { get; }

		// @property (readonly, nonatomic, strong) NSSet<NSString *> * _Nonnull monitoredIdentifiers;
		[Export ("monitoredIdentifiers", ArgumentSemantic.Strong)]
		NSSet<NSString> MonitoredIdentifiers { get; }

		// @property (readonly, assign, nonatomic) CLAuthorizationStatus authorizationStatus;
		[Export ("authorizationStatus", ArgumentSemantic.Assign)]
		CLAuthorizationStatus AuthorizationStatus { get; }

		// -(instancetype _Nonnull)initWithDesiredMeanTriggerDistance:(double)meanTriggerDistance delegate:(id<ESTMonitoringV2ManagerDelegate> _Nonnull)delegate __attribute__((objc_designated_initializer));
		[Export ("initWithDesiredMeanTriggerDistance:delegate:")]
		[DesignatedInitializer]
		IntPtr Constructor (double meanTriggerDistance, IMonitoringV2ManagerDelegate @delegate);

		// -(void)startMonitoringForIdentifiers:(NSArray<NSString *> * _Nonnull)identifiers;
		[Export ("startMonitoringForIdentifiers:")]
		void StartMonitoring (string[] identifiers);

		// -(void)stopMonitoring;
		[Export ("stopMonitoring")]
		void StopMonitoring ();

		// -(ESTMonitoringState)stateForBeaconWithIdentifier:(NSString * _Nonnull)identifier;
		[Export ("stateForBeaconWithIdentifier:")]
		MonitoringState StateForBeacon (string identifier);

		// -(void)requestWhenInUseAuthorization;
		[Export ("requestWhenInUseAuthorization")]
		void RequestWhenInUseAuthorization ();

		// -(void)requestAlwaysAuthorization;
		[Export ("requestAlwaysAuthorization")]
		void RequestAlwaysAuthorization ();
	}

	// @interface ESTFeaturesetEstimoteMonitoring : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTFeaturesetEstimoteMonitoring")]
	interface FeaturesetEstimoteMonitoring
	{
		// -(instancetype _Nonnull)initWithDevice:(ESTDeviceLocationBeacon * _Nonnull)device;
		[Export ("initWithDevice:")]
		IntPtr Constructor (DeviceLocationBeacon device);

		// -(void)readSettingsWithCompletion:(void (^ _Nonnull)(BOOL, NSArray<NSError *> * _Nullable))completion;
		[Export ("readSettingsWithCompletion:")]
		void ReadSettings (Action<bool, NSArray<NSError>> completion);

		// -(void)writeEnableSettings:(BOOL)enabled withCompletion:(void (^ _Nonnull)(NSArray<NSError *> * _Nullable))completion;
		[Export ("writeEnableSettings:withCompletion:"), Async]
		void WriteEnableSettings (bool enabled, Action<NSArray<NSError>> completion);

		// +(NSDictionary<NSString *,ESTSettingBase *> * _Nonnull)classNamesToSettings __attribute__((deprecated("Use +classNamesToSettingsForDeviceIdentifier: instead")));
		[Obsolete ("Use ClassNamesToSettings (string) static method instead.")]
		[Static]
		[Export ("classNamesToSettings")]
		NSDictionary ClassNamesToSettings ();

		// +(NSDictionary<NSString *,ESTSettingBase *> * _Nonnull)classNamesToSettingsForDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("classNamesToSettingsForDeviceIdentifier:")]
		NSDictionary ClassNamesToSettings ([NullAllowed] string deviceIdentifier);

		// +(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)getWriteOperations __attribute__((deprecated("Use +getWriteOperationsForDeviceIdentifier: instead")));
		[Obsolete ("Use GetWriteOperations (string) static method instead.")]
		[Static]
		[Export ("getWriteOperations")]
		IBeaconOperationProtocol [] GetWriteOperations ();

		// +(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)getWriteOperationsForDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("getWriteOperationsForDeviceIdentifier:")]
		IBeaconOperationProtocol[] GetWriteOperations ([NullAllowed] string deviceIdentifier);

		// +(BOOL)featuresetEnabledForSettings:(NSArray<ESTSettingBase *> * _Nonnull)settingsToTest forDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("featuresetEnabledForSettings:forDeviceIdentifier:")]
		bool FeaturesetEnabled (SettingBase[] settingsToTest, [NullAllowed] string deviceIdentifier);
	}

	// @interface ESTFeaturesetBackgroundMode : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTFeaturesetBackgroundMode")]
	interface FeaturesetBackgroundMode
	{
		// -(instancetype _Nonnull)initWithDevice:(ESTDeviceLocationBeacon * _Nonnull)device;
		[Export ("initWithDevice:")]
		IntPtr Constructor (DeviceLocationBeacon device);

		// -(void)readSettingsWithCompletion:(void (^ _Nonnull)(BOOL, NSArray<NSError *> * _Nullable))completion;
		[Export ("readSettingsWithCompletion:")]
		void ReadSettings (Action<bool, NSError []> completion);

		// -(void)writeEnableSettings:(BOOL)enabled withCompletion:(void (^ _Nonnull)(NSArray<NSError *> * _Nullable))completion;
		[Export ("writeEnableSettings:withCompletion:"), Async]
		void WriteEnableSettings (bool enabled, Action<NSError []> completion);

		// +(NSDictionary<NSString *,ESTSettingBase *> * _Nonnull)classNamesToSettings __attribute__((deprecated("Use +classNamesToSettingsForDeviceIdentifier: instead")));
		[Obsolete ("Use ClassNamesToSettings (string) static method instead.")]
		[Static]
		[Export ("classNamesToSettings")]
		NSDictionary ClassNamesToSettings ();

		// +(NSDictionary<NSString *,ESTSettingBase *> * _Nonnull)classNamesToSettingsForDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("classNamesToSettingsForDeviceIdentifier:")]
		NSDictionary ClassNamesToSettings ([NullAllowed] string deviceIdentifier);

		// +(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)getWriteOperations __attribute__((deprecated("Use +getWriteOperationsForDeviceIdentifier: instead")));
		[Obsolete ("Use GetWriteOperations (string) static method instead.")]
		[Static]
		[Export ("getWriteOperations")]
		IBeaconOperationProtocol [] GetWriteOperations ();

		// +(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)getWriteOperationsForDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("getWriteOperationsForDeviceIdentifier:")]
		IBeaconOperationProtocol[] GetWriteOperations ([NullAllowed] string deviceIdentifier);

		// +(BOOL)featuresetEnabledForSettings:(NSArray<ESTSettingBase *> * _Nonnull)settingsToTest forDeviceIdentifier:(NSString * _Nullable)deviceIdentifier;
		[Static]
		[Export ("featuresetEnabledForSettings:forDeviceIdentifier:")]
		bool FeaturesetEnabled (SettingBase[] settingsToTest, [NullAllowed] string deviceIdentifier);
	}

	// typedef void (^ESTMeshCompletionBlock)(ESTMesh * _Nullable, NSError * _Nullable);
	delegate void MeshCompletionBlock ([NullAllowed] Mesh networkDetails, [NullAllowed] NSError error);

	// typedef void (^ESTMeshArrayCompletionBlock)(NSArray<ESTMesh *> * _Nullable, NSError * _Nullable);
	delegate void MeshArrayCompletionBlock ([NullAllowed] Mesh[] meshList, [NullAllowed] NSError error);

	interface IMeshManagerDelegate { }

	// @protocol ESTMeshManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTMeshManagerDelegate")]
	interface MeshManagerDelegate
	{
		// @optional -(void)meshManager:(ESTMeshManager * _Nonnull)manager didConfirmMeshSettingsForDeviceIdentifiers:(NSArray<NSString *> * _Nonnull)deviceIdentifiers;
		[Export ("meshManager:didConfirmMeshSettingsForDeviceIdentifiers:"), EventArgs ("MeshManagerConfirmedMeshSettings")]
		void ConfirmedMeshSettings (MeshManager manager, string[] deviceIdentifiers);

		// @optional -(void)meshManager:(ESTMeshManager * _Nonnull)manager didFailMeshSettingsConfirmationWithError:(NSError * _Nonnull)error;
		[Export ("meshManager:didFailMeshSettingsConfirmationWithError:"), EventArgs ("MeshManagerMeshSettingsConfirmationFailed")]
		void MeshSettingsConfirmationFailed (MeshManager manager, NSError error);
	}

	// @interface ESTMeshManager : NSObject
	[BaseType (typeof (NSObject), Name = "ESTMeshManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (MeshManagerDelegate) })]
	interface MeshManager
	{
		// @property (nonatomic, weak) id<ESTMeshManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMeshManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) BOOL isConfirmingMeshSettings;
		[Export ("isConfirmingMeshSettings")]
		bool IsConfirmingMeshSettings { get; }

		// -(instancetype _Nonnull)initWithDelegate:(id<ESTMeshManagerDelegate> _Nullable)delegate;
		[Export ("initWithDelegate:")]
		IntPtr Constructor ([NullAllowed] IMeshManagerDelegate @delegate);

		// -(void)createMeshFromDevices:(NSArray<NSString *> * _Nonnull)devices networkName:(NSString * _Nonnull)name networkType:(ESTMeshNetworkType)type referenceSettings:(ESTDeviceSettings * _Nonnull)settings completion:(ESTMeshCompletionBlock _Nonnull)completion;
		[Export ("createMeshFromDevices:networkName:networkType:referenceSettings:completion:"), Async]
		void CreateMesh (string[] devices, string name, MeshNetworkType type, DeviceSettings settings, MeshCompletionBlock completion);

		// -(void)fetchMeshListWithCompletion:(ESTMeshArrayCompletionBlock _Nonnull)completion;
		[Export ("fetchMeshListWithCompletion:"), Async]
		void FetchMeshList (MeshArrayCompletionBlock completion);

		// -(void)fetchMeshDetailsWithIdentifier:(NSNumber * _Nonnull)identifier completion:(ESTMeshCompletionBlock _Nonnull)completion;
		[Export ("fetchMeshDetailsWithIdentifier:completion:"), Async]
		void FetchMeshDetails (NSNumber identifier, MeshCompletionBlock completion);

		// -(void)startConfirmingMeshSettings;
		[Export ("startConfirmingMeshSettings")]
		void StartConfirmingMeshSettings ();

		// -(void)stopConfirmingMeshSettings;
		[Export ("stopConfirmingMeshSettings")]
		void StopConfirmingMeshSettings ();

		// -(void)queueAutomappingCommandInCloudForNetworkIdentifier:(uint32_t)networkIdentifier completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("queueAutomappingCommandInCloudForNetworkIdentifier:completion:"), Async]
		void QueueAutomappingCommandInCloud (uint networkIdentifier, CompletionBlock completion);

		// -(void)configureNetwork:(uint32_t)networkIdentifier settings:(ESTDeviceSettingsCollection * _Nonnull)deviceSettings completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("configureNetwork:settings:completion:"), Async]
		void ConfigureNetwork (uint networkIdentifier, DeviceSettingsCollection deviceSettings, CompletionBlock completion);

		// -(void)enableAssetTrackingForNetworkIdentifier:(uint32_t)networkIdentifier completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("enableAssetTrackingForNetworkIdentifier:completion:"), Async]
		void EnableAssetTracking (uint networkIdentifier, CompletionBlock completion);

		// -(void)prepareNearablesScanReportCommandForNetworkIdentifier:(uint32_t)networkIdentifier completion:(ESTCompletionBlock _Nonnull)completion;
		[Export ("prepareNearablesScanReportCommandForNetworkIdentifier:completion:"), Async]
		void PrepareNearablesScanReportCommand (uint networkIdentifier, CompletionBlock completion);
	}

	interface IMeshGatewayDelegate { }

	// @protocol ESTMeshGatewayDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "ESTMeshGatewayDelegate")]
	interface MeshGatewayDelegate
	{
		// @optional -(void)gateway:(ESTMeshGateway * _Nonnull)gateway didReadScanReport:(ESTMeshNearablesScanReportVO * _Nonnull)scanReport withSettingsVersion:(NSNumber * _Nonnull)settingsVersion forNetwork:(NSNumber * _Nonnull)networkIdentifier;
		[Export ("gateway:didReadScanReport:withSettingsVersion:forNetwork:"), EventArgs ("MeshGatewayReadScanReport")]
		void ReadScanReport (MeshGateway gateway, MeshNearablesScanReportVO scanReport, NSNumber settingsVersion, NSNumber networkIdentifier);
	}

	// @interface ESTMeshGateway : NSObject
	[BaseType (typeof (NSObject), Name = "ESTMeshGateway", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (MeshGatewayDelegate) })]
	interface MeshGateway
	{
		// @property (nonatomic, weak) id<ESTMeshGatewayDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMeshGatewayDelegate Delegate { get; set; }

		// -(void)start;
		[Export ("start")]
		void Start ();

		// -(void)stop;
		[Export ("stop")]
		void Stop ();
	}

	// @interface ESTMeshNearablesScanResultVO : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTMeshNearablesScanResultVO")]
	interface MeshNearablesScanResultVO
	{
		// @property (readonly, nonatomic, strong) NSString * _Nonnull nearableIdentifier;
		[Export ("nearableIdentifier", ArgumentSemantic.Strong)]
		string NearableIdentifier { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull rssi;
		[Export ("rssi", ArgumentSemantic.Strong)]
		NSNumber Rssi { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nonnull measuredPower;
		[Export ("measuredPower", ArgumentSemantic.Strong)]
		NSNumber MeasuredPower { get; }

		// @property (readonly, getter = getDistance, nonatomic, strong) NSNumber * _Nonnull distance;
		[Export ("getDistance", ArgumentSemantic.Strong)]
		NSNumber Distance { get; }

		// -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data;
		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)deviceIdentifier rssi:(NSNumber * _Nonnull)rssi;
		[Export ("initWithIdentifier:rssi:")]
		IntPtr Constructor (string deviceIdentifier, NSNumber rssi);

		// -(NSData * _Nonnull)data;
		[Export ("data")]
		NSData Data { get; }
	}

	// @interface ESTMeshNearablesScanReportVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTMeshNearablesScanReportVO")]
	[DisableDefaultCtor]
	interface MeshNearablesScanReportVO
	{
		// @property (nonatomic) NSDate * collectedAt;
		[Export ("collectedAt", ArgumentSemantic.Assign)]
		NSDate CollectedAt { get; set; }

		// -(instancetype)initWithData:(NSArray<NSData *> *)data collectedAt:(NSDate *)collectedAt __attribute__((objc_designated_initializer));
		[Export ("initWithData:collectedAt:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSData[] data, NSDate collectedAt);

		// -(NSArray<ESTMeshNearablesScanResultVO *> *)scanResultsForShortDeviceIdentifier:(NSString *)deviceIdentifier;
		[Export ("scanResultsForShortDeviceIdentifier:")]
		MeshNearablesScanResultVO[] ScanResults (string deviceIdentifier);

		// -(NSDictionary *)cloudDictionary;
		[Export ("cloudDictionary")]
		NSDictionary CloudDictionary { get; }
	}

	// @interface ESTDeviceSettingsAdvertiserUWB : NSObject
	[BaseType (typeof (NSObject), Name = "ESTDeviceSettingsAdvertiserUWB")]
	interface DeviceSettingsAdvertiserUwb
	{

	}

	// @interface ESTDeviceSettingsAdvertiserMesh : NSObject
	[BaseType (typeof (NSObject), Name = "ESTDeviceSettingsAdvertiserMesh")]
	interface DeviceSettingsAdvertiserMesh
	{

	}

	// @interface ESTDeviceSettingsGPIO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTDeviceSettingsGPIO")]
	interface DeviceSettingsGpio
	{

	}

	// @interface ESTSettingPowerDarkToSleepThreshold : NSObject
	[BaseType (typeof (NSObject), Name = "ESTSettingPowerDarkToSleepThreshold")]
	interface SettingPowerDarkToSleepThreshold
	{

	}

	// @interface ESTBeaconBaseVO : NSObject
	[BaseType (typeof (NSObject), Name = "ESTBeaconBaseVO")]
	interface BeaconBaseVO
	{
		// -(id)objectForKey:(NSString *)aKey inDictionary:(NSDictionary *)dict;
		[Export ("objectForKey:inDictionary:")]
		NSObject ObjectForKey (string aKey, NSDictionary dict);
	}

	// @interface ESTBeaconBatteryLifetimesVO : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "ESTBeaconBatteryLifetimesVO")]
	interface BeaconBatteryLifetimesVO
	{
		// -(instancetype _Nonnull)initWithLifetimes:(NSDictionary * _Nonnull)lifetimes;
		[Export ("initWithLifetimes:")]
		IntPtr Constructor (NSDictionary lifetimes);

		// -(NSString * _Nonnull)lifetimeForAdvertisingInterval:(int)interval;
		[Export ("lifetimeForAdvertisingInterval:")]
		string GetLifetimeForAdvertisingInterval (int interval);

		// -(NSString * _Nonnull)lifetimeForBroadcastingPower:(int)power;
		[Export ("lifetimeForBroadcastingPower:")]
		string GetLifetimeForBroadcastingPower (int power);

		// -(NSString * _Nonnull)lifetimeForBasicPowerMode:(ESTBeaconPowerSavingMode)basic andSmart:(ESTBeaconPowerSavingMode)smart;
		[Export ("lifetimeForBasicPowerMode:andSmart:")]
		string GetLifetimeForBasicPowerMode (BeaconPowerSavingMode basic, BeaconPowerSavingMode smart);

		// -(NSString * _Nonnull)lifetimeForBroadcastingScheme:(ESTBroadcastingScheme)scheme;
		[Export ("lifetimeForBroadcastingScheme:")]
		string GetLifetimeForBroadcastingScheme (BroadcastingScheme scheme);

		// -(BOOL)shouldDisplayAlertForAdvertisingInterval:(int)interval;
		[Export ("shouldDisplayAlertForAdvertisingInterval:")]
		bool ShouldDisplayAlertForAdvertisingInterval (int interval);

		// -(BOOL)shouldDisplayAlertForBroadcastingPower:(int)power;
		[Export ("shouldDisplayAlertForBroadcastingPower:")]
		bool ShouldDisplayAlertForBroadcastingPower (int power);

		// -(BOOL)shouldDisplayAlertForBasicPowerMode:(ESTBeaconPowerSavingMode)basic andSmart:(ESTBeaconPowerSavingMode)smart;
		[Export ("shouldDisplayAlertForBasicPowerMode:andSmart:")]
		bool ShouldDisplayAlertForBasicPowerMode (BeaconPowerSavingMode basic, BeaconPowerSavingMode smart);
	}

	// @interface ESTBeaconFirmwareVO : ESTFirmwareInfoVO
	[BaseType (typeof (FirmwareInfoVO), Name = "ESTBeaconFirmwareVO")]
	interface BeaconFirmwareVO
	{
		// @property (nonatomic, strong) NSString * firmwareUrl;
		[Export ("firmwareUrl", ArgumentSemantic.Strong)]
		string FirmwareUrl { get; set; }

		// @property (nonatomic, strong) NSString * firmwareCleanerUrl;
		[Export ("firmwareCleanerUrl", ArgumentSemantic.Strong)]
		string FirmwareCleanerUrl { get; set; }
	}

	// @interface ESTBeaconOperationEddystoneConfigurationServiceEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
	[DisableDefaultCtor]
	[BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneConfigurationServiceEnable")]
	interface BeaconOperationEddystoneConfigurationServiceEnable : BeaconOperationProtocol
	{
		// +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("readOperationWithCompletion:")]
		BeaconOperationEddystoneConfigurationServiceEnable ReadOperation (SettingEddystoneConfigurationServiceEnableCompletionBlock completion);

		// +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneConfigurationServiceEnable * _Nonnull)setting completion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
		[Static]
		[Export ("writeOperationWithSetting:completion:")]
		BeaconOperationEddystoneConfigurationServiceEnable WriteOperation (SettingEddystoneConfigurationServiceEnable setting, SettingEddystoneConfigurationServiceEnableCompletionBlock completion);
	}

	// @interface ESTBeaconRecentConfig : ESTBeaconBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BeaconBaseVO), Name = "ESTBeaconRecentConfig")]
	interface BeaconRecentConfig
	{
		// @property (readonly, nonatomic) NSString * proximityUUID;
		[Export ("proximityUUID")]
		string ProximityUuid { get; }

		// @property (readonly, nonatomic) NSNumber * major;
		[Export ("major")]
		NSNumber Major { get; }

		// @property (readonly, nonatomic) NSNumber * minor;
		[Export ("minor")]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) NSNumber * security;
		[Export ("security")]
		NSNumber Security { get; }

		// @property (readonly, nonatomic) NSNumber * advInterval;
		[Export ("advInterval")]
		NSNumber AdvInterval { get; }

		// @property (readonly, nonatomic) NSNumber * power;
		[Export ("power")]
		NSNumber Power { get; }

		// @property (readonly, nonatomic) NSNumber * basicPowerMode;
		[Export ("basicPowerMode")]
		NSNumber BasicPowerMode { get; }

		// @property (readonly, nonatomic) NSNumber * smartPowerMode;
		[Export ("smartPowerMode")]
		NSNumber SmartPowerMode { get; }

		// @property (readonly, nonatomic) NSString * firmware;
		[Export ("firmware")]
		string Firmware { get; }

		// @property (readonly, nonatomic) NSNumber * broadcastingScheme;
		[Export ("broadcastingScheme")]
		NSNumber BroadcastingScheme { get; }

		// @property (readonly, nonatomic) NSString * formattedAddress;
		[Export ("formattedAddress")]
		string FormattedAddress { get; }

		// @property (readonly, nonatomic) BOOL geoLocationDeleted;
		[Export ("geoLocationDeleted")]
		bool GeoLocationDeleted { get; }

		// @property (readonly, nonatomic) NSNumber * conditionalBroadcasting;
		[Export ("conditionalBroadcasting")]
		NSNumber ConditionalBroadcasting { get; }

		// @property (readonly, nonatomic) NSString * zone;
		[Export ("zone")]
		string Zone { get; }

		// @property (readonly, nonatomic) NSNumber * motionDetection;
		[Export ("motionDetection")]
		NSNumber MotionDetection { get; }

		// -(instancetype)initWithCloudData:(NSDictionary *)data;
		[Export ("initWithCloudData:")]
		IntPtr Constructor (NSDictionary data);
	}

	// @interface ESTBeaconRecentUpdateInfo : ESTBeaconBaseVO
	[DisableDefaultCtor]
	[BaseType (typeof (BeaconBaseVO), Name = "ESTBeaconRecentUpdateInfo")]
	interface BeaconRecentUpdateInfo
	{
		// @property (readonly, nonatomic) NSString * macAddress;
		[Export ("macAddress")]
		string MacAddress { get; }

		// @property (readonly, nonatomic) ESTBeaconRecentConfig * config;
		[Export ("config")]
		BeaconRecentConfig Config { get; }

		// @property (readonly, nonatomic) NSDate * createdAt;
		[Export ("createdAt")]
		NSDate CreatedAt { get; }

		// @property (readonly, nonatomic) NSDate * syncedAt;
		[Export ("syncedAt")]
		NSDate SyncedAt { get; }

		// -(instancetype)initWithCloudData:(NSDictionary *)data andMacAddress:(NSString *)mac;
		[Export ("initWithCloudData:andMacAddress:")]
		IntPtr Constructor (NSDictionary data, string macAddress);
	}



	// @interface ESTRequestFirmwareV4 : ESTRequestGetJSON
	[DisableDefaultCtor]
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestFirmwareV4")]
	interface RequestFirmwareV4
	{
		// -(instancetype)initWithPublicID:(NSString *)publicID;
		[Export ("initWithPublicID:")]
		IntPtr Constructor (string publicId);
	}

	// @interface ESTRequestPatchJSON : ESTRequestBase
	[BaseType (typeof (RequestBase), Name = "ESTRequestPatchJSON")]
	interface RequestPatchJson
	{
		// -(void)setParams:(id _Nonnull)params forRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("setParams:forRequest:")]
		void SetParams (NSObject parameters, NSMutableUrlRequest request);
	}

	// @interface ESTRequestPostFormData : ESTRequestBase
	[BaseType (typeof (RequestBase), Name = "ESTRequestPostFormData")]
	interface RequestPostFormData
	{
		// -(void)setFilePath:(NSString * _Nonnull)filePath forRequest:(NSMutableURLRequest * _Nonnull)request;
		[Export ("setFilePath:forRequest:")]
		void SetFilePath (string filePath, NSMutableUrlRequest request);
	}

	// typedef void (^ESTRequestV2GetDevicesPendingBlock)(NSArray<NSString *> * _Nullable, NSError * _Nullable);
	delegate void RequestV2GetDevicesPendingBlock ([NullAllowed] string [] result, [NullAllowed] NSError error);

	// @interface ESTRequestV2GetDevicesPending : ESTRequestGetJSON
	[BaseType (typeof (RequestGetJson), Name = "ESTRequestV2GetDevicesPending")]
	interface RequestV2GetDevicesPending
	{
		// -(void)sendRequestWithCompletion:(ESTRequestV2GetDevicesPendingBlock _Nonnull)completion;
		[Export ("sendRequestWithCompletion:")]
		void SendRequest (RequestV2GetDevicesPendingBlock completion);
	}

	interface IDeviceNearableSettingProtocol { }

	// @protocol ESTDeviceNearableSettingProtocol <ESTDeviceSettingProtocol>
	[Protocol (Name = "ESTDeviceNearableSettingProtocol")]
	interface DeviceNearableSettingProtocol : DeviceSettingProtocol
	{
		// @required -(NSInteger)size;
		[Abstract]
		[Export ("size")]
		nint Size { get; }
	}
}
