using System;

using CoreBluetooth;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace Estimote
{
    // typedef void (^ESTCompletionBlock)(NSError *);
    delegate void CompletionBlock ([NullAllowed]NSError error);

    // typedef void (^ESTObjectCompletionBlock)(idNSError *);
    delegate void ObjectCompletionBlock ([NullAllowed]NSObject result, [NullAllowed]NSError error);

    // typedef void (^ESTDataCompletionBlock)(NSData *NSError *);
    delegate void DataCompletionBlock ([NullAllowed]NSData data, [NullAllowed]NSError error);

    // typedef void (^ESTNumberCompletionBlock)(NSNumber *NSError *);
    delegate void NumberCompletionBlock ([NullAllowed]NSNumber num, [NullAllowed]NSError error);

    // typedef void (^ESTUnsignedShortCompletionBlock)(unsigned shortNSError *);
    delegate void UnsignedShortCompletionBlock (ushort val, [NullAllowed]NSError error);

    // typedef void (^ESTBoolCompletionBlock)(BOOLNSError *);
    delegate void BoolCompletionBlock (bool val, [NullAllowed]NSError error);

    // typedef void (^ESTStringCompletionBlock)(NSString *NSError *);
    delegate void StringCompletionBlock ([NullAllowed]string str, [NullAllowed]NSError error);

    // typedef void (^ESTProgressBlock)(NSIntegerNSString *NSError *);
    delegate void ProgressBlock (nint progress, [NullAllowed]string description, [NullAllowed]NSError error);

    // typedef void (^ESTArrayCompletionBlock)(NSArray *NSError *);
    delegate void ArrayCompletionBlock ([NullAllowed]NSObject [] val, [NullAllowed]NSError error);

    // typedef void (^ESTDictionaryCompletionBlock)(NSDictionary *NSError *);
    delegate void DictionaryCompletionBlock ([NullAllowed]NSDictionary values, [NullAllowed]NSError error);

    // typedef void (^ESTCsRegisterCompletonBlock)(NSError *);
    delegate void CsRegisterCompletonBlock ([NullAllowed]NSError error);

    // typedef void(^ESTSettingAdvertisingIntervalCompletionBlock)(NSNumber * _Nullable advertisingInterval, NSError * _Nullable error);
    delegate void SettingAdvertisingIntervalCompletionBlock ([NullAllowed]NSNumber advertisingInterval, [NullAllowed]NSError error);

    // typedef void (^ESTSettingConnectivityIntervalCompletionBlock)(ESTSettingConnectivityInterval * _Nullable, NSError * _Nullable);
    delegate void SettingConnectivityIntervalCompletionBlock ([NullAllowed] SettingConnectivityInterval connectivityInterval, [NullAllowed] NSError error);

    // typedef void (^ESTSettingConnectivityPowerCompletionBlock)(ESTSettingConnectivityPower * _Nullable, NSError * _Nullable);
    delegate void SettingConnectivityPowerCompletionBlock ([NullAllowed] SettingConnectivityPower connectivityPower, [NullAllowed] NSError error);

    // typedef void(^ESTSettingBroadcastingPowerCompletionBlock)(NSNumber * _Nullable broadcastingPower, NSError * _Nullable error);
    delegate void SettingBroadcastingPowerCompletionBlock ([NullAllowed] NSNumber broadcastingPower, [NullAllowed]NSError error);

    // typedef void(^ESTRequestAssignGPSLocationBlock)(CLLocation * _Nullable result, NSError * _Nullable error);
    delegate void RequestAssignGPSLocationBlock ([NullAllowed]CLLocation result, [NullAllowed]NSError error);


    [BaseType (typeof (NSObject), Name = "ESTConfig")]
    interface Config
    {
        [Static]
        [Export ("setupAppID:andAppToken:")]
        void Setup (string appId, string appToken);

        [Static]
        [Export ("appID")]
        [NullAllowed]
        string AppId { get; }

        [Static]
        [Export ("appToken")]
        [NullAllowed]
        string AppToken { get; }

        [Static]
        [Export ("isAuthorized")]
        bool IsAuthorized { get; }

        // + (void)setupGoogleAPIKey:(NSString *)googleAPIKey;
        [Static]
        [Export ("setupGoogleAPIKey:")]
        void SetupGoogleAPIKey (string googleApiKey);

        [Static]
        [Export ("googleAPIKey")]
        [NullAllowed]
        string GoogleApiKey { get; }

        [Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableMonitoringAnalytics")]
        [Static]
        [Export ("enableMonitoringAnalytics:")]
        void EnableMonitoringAnalytics (bool enable);

        [Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableRangingAnalytics")]
        [Static]
        [Export ("enableRangingAnalytics:")]
        void EnableRangingAnalytics (bool enable);

        [Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.EnableGPSPositioningForAnalytics")]
        [Static]
        [Export ("enableGPSPositioningForAnalytics:")]
        void EnableGPSPositioningForAnalytics (bool enable);

        [Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.IsMonitoringAnalyticsEnabled")]
        [Static]
        [Export ("isMonitoringAnalyticsEnabled")]
        bool IsMonitoringAnalyticsEnabled { get; }

        [Obsolete ("Starting from SDK 4.1.1 this method is deprecated. Please use AnalyticsManager.IsRangingAnalyticsEnabled")]
        [Static]
        [Export ("isRangingAnalyticsEnabled")]
        bool IsRangingAnalyticsEnabled { get; }

    }

    // @interface ESTDefinitions : NSObject
    /// <summary>
    /// Definitions.
    /// </summary>
    [BaseType (typeof (NSObject), Name = "ESTDefinitions")]
    interface Definitions
    {
        // +(NSString *)nameForEstimoteColor:(ESTColor)color;
        [Static]
        [Export ("nameForEstimoteColor:")]
        string NameForEstimoteColor (Color color);
    }

    // @interface ESTNearableDefinitions : ESTDefinitions
    [BaseType (typeof (Definitions), Name = "ESTNearableDefinitions")]
    interface NearableDefinitions
    {
        // +(NSString *)nameForType:(ESTNearableType)type;
        [Static]
        [Export ("nameForType:")]
        string Name (NearableType type);

        // + (NSString*)nameForNearableBroadcastingScheme:(ESTNearableBroadcastingScheme)scheme;
        [Static]
        [Export ("nameForNearableBroadcastingScheme:")]
        string Name (NearableBroadcastingScheme scheme);
    }

    // @interface ESTNearable : NSObject <NSCopying, NSCoding>
    [BaseType (typeof (NSObject), Name = "ESTNearable")]
    interface Nearable : INSCopying, INSCoding
    {
        // @property (readonly, nonatomic, strong) NSString * identifier;
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

        // @property (readonly, nonatomic, strong) NSString * hardwareVersion;
        [Export ("hardwareVersion", ArgumentSemantic.Strong)]
        string HardwareVersion { get; }

        // @property (readonly, nonatomic, strong) NSString * firmwareVersion;
        [Export ("firmwareVersion", ArgumentSemantic.Strong)]
        string FirmwareVersion { get; }

        // @property (readonly, assign, nonatomic) NSInteger rssi;
        [Export ("rssi", ArgumentSemantic.Assign)]
        nint Rssi { get; }

        // @property (readonly, nonatomic, strong) NSNumber * idleBatteryVoltage;
        [Export ("idleBatteryVoltage", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber IdleBatteryVoltage { get; }

        // @property (readonly, nonatomic, strong) NSNumber * stressBatteryVoltage;
        [Export ("stressBatteryVoltage", ArgumentSemantic.Strong)]
        [NullAllowed]
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
        [Export ("xAcceleration", ArgumentSemantic.Assign)]
        nint XAcceleration { get; }

        // @property (readonly, assign, nonatomic) NSInteger yAcceleration;
        [Export ("yAcceleration", ArgumentSemantic.Assign)]
        nint YAcceleration { get; }

        // @property (readonly, assign, nonatomic) NSInteger zAcceleration;
        [Export ("zAcceleration", ArgumentSemantic.Assign)]
        nint ZAcceleration { get; }

        // @property (readonly, assign, nonatomic) double temperature;
        [Export ("temperature")]
        double Temperature { get; }

        // @property (readonly, nonatomic, strong) NSNumber * power;
        [Export ("power", ArgumentSemantic.Strong)]
        NSNumber Power { get; }

        // @property (nonatomic, strong, readonly) NSNumber *advInterval;
        [Export ("advInterval", ArgumentSemantic.Strong)]
        NSNumber AdvInterval { get; }

        // @property (readonly, assign, nonatomic) ESTNearableFirmwareState firmwareState;
        [Export ("firmwareState", ArgumentSemantic.Assign)]
        NearableFirmwareState FirmwareState { get; }

        // -(CLBeaconRegion *)beaconRegion;
        [Export ("beaconRegion")]
        CLBeaconRegion BeaconRegion { get; }
    }

    interface ITriggerDelegate { }

    // @protocol ESTTriggerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTTriggerDelegate")]
    interface TriggerDelegate
    {
        // @optional -(void)triggerDidChangeState:(ESTTrigger *)trigger;
        [Export ("triggerDidChangeState:")]
        void TriggerDidChangeState (Trigger trigger);
    }

    // @interface ESTTrigger : NSObject
    [BaseType (typeof (NSObject), Name = "ESTTrigger", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (TriggerDelegate) })]
    interface Trigger
    {
        // @property (assign, nonatomic) id<ESTTriggerDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
        [NullAllowed]
        ITriggerDelegate Delegate { get; set; }

        // @property (readonly, nonatomic, strong) NSArray * rules;
        [Export ("rules", ArgumentSemantic.Strong)]
        Rule [] Rules { get; }

        // @property (readonly, nonatomic, strong) NSString * identifier;
        [Export ("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        // @property (readonly, assign, nonatomic) BOOL state;
        [Export ("state")]
        bool State { get; }

        // -(instancetype)initWithRules:(NSArray *)rules identifier:(NSString *)identifier;
        [Export ("initWithRules:identifier:")]
        IntPtr Constructor (Rule [] rules, string identifier);
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

    // @interface ESTDateRule : ESTRule
    [BaseType (typeof (Rule), Name = "ESTDateRule")]
    interface DateRule
    {
        // @property (nonatomic, strong) NSNumber * afterHour;
        [Export ("afterHour", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber AfterHour { get; set; }

        // @property (nonatomic, strong) NSNumber * beforeHour;
        [Export ("beforeHour", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber BeforeHour { get; set; }

        // +(instancetype)hourLaterThan:(int)hour;
        [Static]
        [Export ("hourLaterThan:")]
        DateRule HourLaterThan (int hour);

        // +(instancetype)hourEarlierThan:(int)hour;
        [Static]
        [Export ("hourEarlierThan:")]
        DateRule HourEarlierThan (int hour);

        // +(instancetype)hourBetween:(int)firstHour and:(int)secondHour;
        [Static]
        [Export ("hourBetween:and:")]
        DateRule HourBetween (int firstHour, int secondHour);
    }

    // @interface ESTNearableRule : ESTRule
    [BaseType (typeof (Rule), Name = "ESTNearableRule")]
    interface NearableRule
    {
        // @property (readonly, nonatomic, strong) NSString * nearableIdentifier;
        [Export ("nearableIdentifier", ArgumentSemantic.Strong)]
        [NullAllowed]
        string NearableIdentifier { get; }

        // @property (readonly, assign, nonatomic) ESTNearableType nearableType;
        [Export ("nearableType", ArgumentSemantic.Assign)]
        NearableType NearableType { get; }

        // -(instancetype)initWithNearableIdentifier:(NSString *)identifier;
        [Export ("initWithNearableIdentifier:")]
        IntPtr Constructor (string identifier);

        // -(instancetype)initWithNearableType:(ESTNearableType)type;
        [Export ("initWithNearableType:")]
        IntPtr Constructor (NearableType type);

        // -(void)updateWithNearable:(ESTNearable *)nearable;
        [Export ("updateWithNearable:")]
        void UpdateWithNearable (Nearable nearable);
    }

    // @interface ESTProximityRule : ESTNearableRule
    [BaseType (typeof (NearableRule), Name = "ESTProximityRule")]
    interface ProximityRule
    {
        // @property (assign, nonatomic) BOOL inRange;
        [Export ("inRange")]
        bool InRange { get; set; }

        // +(instancetype)inRangeOfNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("inRangeOfNearableIdentifier:")]
        ProximityRule InRangeOfNearableIdentifier (string identifier);

        // +(instancetype)inRangeOfNearableType:(ESTNearableType)type;
        [Static]
        [Export ("inRangeOfNearableType:")]
        ProximityRule InRangeOfNearableType (NearableType type);

        // +(instancetype)outsideRangeOfNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("outsideRangeOfNearableIdentifier:")]
        ProximityRule OutsideRangeOfNearableIdentifier (string identifier);

        // +(instancetype)outsideRangeOfNearableType:(ESTNearableType)type;
        [Static]
        [Export ("outsideRangeOfNearableType:")]
        ProximityRule OutsideRangeOfNearableType (NearableType type);
    }

    // @interface ESTTemperatureRule : ESTNearableRule
    [BaseType (typeof (NearableRule), Name = "ESTTemperatureRule")]
    interface TemperatureRule
    {
        // @property (nonatomic, strong) NSNumber * maxValue;
        [Export ("maxValue", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber MaxValue { get; set; }

        // @property (nonatomic, strong) NSNumber * minValue;
        [Export ("minValue", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber MinValue { get; set; }

        // +(instancetype)temperatureGraterThan:(double)value forNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("temperatureGraterThan:forNearableIdentifier:")]
        TemperatureRule TemperatureGraterThan (double value, string identifier);

        // +(instancetype)temperatureLowerThan:(double)value forNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("temperatureLowerThan:forNearableIdentifier:")]
        TemperatureRule TemperatureLowerThan (double value, string identifier);

        // +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("temperatureBetween:and:forNearableIdentifier:")]
        TemperatureRule TemperatureBetween (double minValue, double maxValue, string identifier);

        // +(instancetype)temperatureGraterThan:(double)value forNearableType:(ESTNearableType)type;
        [Static]
        [Export ("temperatureGraterThan:forNearableType:")]
        TemperatureRule TemperatureGraterThan (double value, NearableType type);

        // +(instancetype)temperatureLowerThan:(double)value forNearableType:(ESTNearableType)type;
        [Static]
        [Export ("temperatureLowerThan:forNearableType:")]
        TemperatureRule TemperatureLowerThan (double value, NearableType type);

        // +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableType:(ESTNearableType)type;
        [Static]
        [Export ("temperatureBetween:and:forNearableType:")]
        TemperatureRule TemperatureBetween (double minValue, double maxValue, NearableType type);
    }

    // @interface ESTMotionRule : ESTNearableRule
    [BaseType (typeof (NearableRule), Name = "ESTMotionRule")]
    interface MotionRule
    {
        // @property (assign, nonatomic) BOOL motionState;
        [Export ("motionState")]
        bool MotionState { get; set; }

        // +(instancetype)motionStateEquals:(BOOL)motionState forNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("motionStateEquals:forNearableIdentifier:")]
        MotionRule MotionStateEquals (bool motionState, string identifier);

        // +(instancetype)motionStateEquals:(BOOL)motionState forNearableType:(ESTNearableType)type;
        [Static]
        [Export ("motionStateEquals:forNearableType:")]
        MotionRule MotionStateEquals (bool motionState, NearableType type);
    }

    // @interface ESTOrientationRule : ESTNearableRule
    [BaseType (typeof (NearableRule), Name = "ESTOrientationRule")]
    interface OrientationRule
    {
        // @property (assign, nonatomic) ESTNearableOrientation orientation;
        [Export ("orientation", ArgumentSemantic.Assign)]
        NearableOrientation Orientation { get; set; }

        // +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableIdentifier:(NSString *)identifier;
        [Static]
        [Export ("orientationEquals:forNearableIdentifier:")]
        OrientationRule OrientationEquals (NearableOrientation orientation, string identifier);

        // +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableType:(ESTNearableType)type;
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
        // @optional -(void)triggerManager:(ESTTriggerManager *)manager triggerChangedState:(ESTTrigger *)trigger;
        [Export ("triggerManager:triggerChangedState:"), EventArgs ("TriggerChangedState")]
        void ChangedState (TriggerManager manager, Trigger trigger);
    }

    // @interface ESTTriggerManager : NSObject <ESTTriggerDelegate>
    [BaseType (typeof (NSObject), Name = "ESTTriggerManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (TriggerManagerDelegate) })]
    interface TriggerManager : TriggerDelegate
    {
        // @property (assign, nonatomic) id<ESTTriggerManagerDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
        [NullAllowed]
        ITriggerManagerDelegate Delegate { get; set; }

        // @property (readonly, nonatomic, strong) NSArray * triggers;
        [Export ("triggers", ArgumentSemantic.Strong)]
        Trigger [] Triggers { get; }

        // -(void)startMonitoringForTrigger:(ESTTrigger *)trigger;
        [Export ("startMonitoringForTrigger:")]
        void StartMonitoringForTrigger (Trigger trigger);

        // -(void)stopMonitoringForTriggerWithIdentifier:(NSString *)identifier;
        [Export ("stopMonitoringForTriggerWithIdentifier:")]
        void StopMonitoringForTriggerWithIdentifier (string identifier);

        // -(BOOL)stateForTriggerWithIdentifier:(NSString *)identifier;
        [Export ("stateForTriggerWithIdentifier:")]
        bool StateForTriggerWithIdentifier (string identifier);
    }

    interface IBeaconManagerDelegate { }

    // @protocol ESTBeaconManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTBeaconManagerDelegate")]
    interface BeaconManagerDelegate
    {
        // @optional -(void)beaconManager:(id)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
        [Export ("beaconManager:didChangeAuthorizationStatus:"), EventArgs ("AuthorizationStatusChanged")]
        void AuthorizationStatusChanged (NSObject manager, CLAuthorizationStatus status);

        // @optional -(void)beaconManager:(id)manager didStartMonitoringForRegion:(CLBeaconRegion *)region;
        [Export ("beaconManager:didStartMonitoringForRegion:"), EventArgs ("MonitoringStarted")]
        void MonitoringStarted (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id)manager monitoringDidFailForRegion:(CLBeaconRegion *)region withError:(NSError *)error;
        [Export ("beaconManager:monitoringDidFailForRegion:withError:"), EventArgs ("BeaconManagerMonitoringFailed")]
        void MonitoringFailed (NSObject manager, [NullAllowed]CLBeaconRegion region, NSError error);

        // @optional -(void)beaconManager:(id)manager didEnterRegion:(CLBeaconRegion *)region;
        [Export ("beaconManager:didEnterRegion:"), EventArgs ("EnteredRegion")]
        void EnteredRegion (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id)manager didExitRegion:(CLBeaconRegion *)region;
        [Export ("beaconManager:didExitRegion:"), EventArgs ("ExitedRegion")]
        void ExitedRegion (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id)manager didDetermineState:(CLRegionState)state forRegion:(CLBeaconRegion *)region;
        [Export ("beaconManager:didDetermineState:forRegion:"), EventArgs ("DeterminedState")]
        void DeterminedState (NSObject manager, CLRegionState state, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id)manager didRangeBeacons:(NSArray *)beacons inRegion:(CLBeaconRegion *)region;
        [Export ("beaconManager:didRangeBeacons:inRegion:"), EventArgs ("RangedBeacons")]
        void RangedBeacons (NSObject manager, CLBeacon [] beacons, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id)manager rangingBeaconsDidFailForRegion:(CLBeaconRegion *)region withError:(NSError *)error;
        [Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:"), EventArgs ("RangingBeaconsFailed")]
        void RangingBeaconsFailed (NSObject manager, [NullAllowed]CLBeaconRegion region, NSError error);

        // @optional -(void)beaconManagerDidStartAdvertising:(id)manager error:(NSError *)error;
        [Export ("beaconManagerDidStartAdvertising:error:"), EventArgs ("StartedAdvertising")]
        void StartedAdvertising (NSObject manager, [NullAllowed]NSError error);

        // @optional -(void)beaconManager:(id)manager didFailWithError:(NSError *)error;
        [Export ("beaconManager:didFailWithError:"), EventArgs ("FailedWithError")]
        void FailedWithError (NSObject manager, NSError error);
    }

    // @interface ESTBeaconManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBeaconManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconManagerDelegate) })]
    interface BeaconManager
    {
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        IBeaconManagerDelegate Delegate { get; set; }

        // @property (nonatomic) NSInteger preventUnknownUpdateCount;
        [Export ("preventUnknownUpdateCount", ArgumentSemantic.Assign)]
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

        // -(void)startAdvertisingWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor identifier:(NSString *)identifier;
        [Export ("startAdvertisingWithProximityUUID:major:minor:identifier:")]
        void StartAdvertisingWithProximityUUID (NSUuid proximityUUID, ushort major, ushort minor, string identifier);

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

        /**
         * Checks if the current Location Services authorization is enough to do ranging (i.e., either 'when in use' or 'always').
         */
        //- (BOOL)isAuthorizedForRanging;
        [Export ("isAuthorizedForRanging")]
        bool IsAuthorizedForRanging { get; }

        /**
         * Checks if the current Location Services authorization is enough to do monitoring (i.e., 'always').
         */
        //- (BOOL)isAuthorizedForMonitoring;
        [Export ("isAuthorizedForMonitoring")]
        bool IsAuthorizedForMonitoring { get; }

        // -(void)startMonitoringForRegion:(CLBeaconRegion *)region;
        [Export ("startMonitoringForRegion:")]
        void StartMonitoringForRegion (CLBeaconRegion region);

        // -(void)stopMonitoringForRegion:(CLBeaconRegion *)region;
        [Export ("stopMonitoringForRegion:")]
        void StopMonitoringForRegion (CLBeaconRegion region);

        // - (void)stopMonitoringForAllRegions;
        [Export ("stopMonitoringForAllRegions")]
        void StopMonitoringForAllRegions ();

        // -(void)startRangingBeaconsInRegion:(CLBeaconRegion *)region;
        [Export ("startRangingBeaconsInRegion:")]
        void StartRangingBeaconsInRegion (CLBeaconRegion region);

        // -(void)stopRangingBeaconsInRegion:(CLBeaconRegion *)region;
        [Export ("stopRangingBeaconsInRegion:")]
        void StopRangingBeaconsInRegion (CLBeaconRegion region);

        // - (void)stopRangingBeaconsInAllRegions;
        [Export ("stopRangingBecaonsInAllRegions")]
        void StopRangingBeaconsInAllRegions ();

        // -(void)requestStateForRegion:(CLBeaconRegion *)region;
        [Export ("requestStateForRegion:")]
        void RequestStateForRegion (CLBeaconRegion region);

        // @property (readonly, copy, nonatomic) NSSet * monitoredRegions;
        [Export ("monitoredRegions", ArgumentSemantic.Copy)]
        NSSet MonitoredRegions { get; }

        // @property (readonly, copy, nonatomic) NSSet * rangedRegions;
        [Export ("rangedRegions", ArgumentSemantic.Copy)]
        NSSet RangedRegions { get; }

        // +(NSUUID *)motionProximityUUIDForProximityUUID:(NSUUID *)proximityUUID;
        [Static]
        [Export ("motionProximityUUIDForProximityUUID:")]
        NSUuid MotionProximityUUIDForProximityUUID (NSUuid proximityUUID);
    }

    // typedef void (^ESTPowerCompletionBlock)(ESTBeaconPowerNSError *);
    delegate void PowerCompletionBlock (BeaconPower power, [NullAllowed]NSError error);

    // @interface ESTBeaconDefinitions : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBeaconDefinitions")]
    interface BeaconDefinitions
    {
    }

    // @interface ESTBeaconVO : NSObject <NSCoding>
    [BaseType (typeof (NSObject), Name = "ESTBeaconVO")]
    interface BeaconVO : INSCoding
    {
        // @property (nonatomic, strong) NSString * proximityUUID;
        [Export ("proximityUUID", ArgumentSemantic.Strong)]
        string ProximityUUID { get; set; }

        // @property (nonatomic, strong) NSNumber * major;
        [Export ("major", ArgumentSemantic.Strong)]
        NSNumber Major { get; set; }

        // @property (nonatomic, strong) NSNumber * minor;
        [Export ("minor", ArgumentSemantic.Strong)]
        NSNumber Minor { get; set; }

        // @property (nonatomic, strong) NSString * macAddress;
        [Export ("macAddress", ArgumentSemantic.Strong)]
        string MacAddress { get; set; }

        [Export ("publicIdentifier", ArgumentSemantic.Strong)]
        string PublicIdentifier { get; set; }

        // @property (assign, nonatomic) ESTBroadcastingScheme broadcastingScheme;
        [Export ("broadcastingScheme", ArgumentSemantic.Assign)]
        BroadcastingScheme BroadcastingScheme { get; set; }

        // @property (nonatomic, strong) NSString * name;
        [Export ("name", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Name { get; set; }

        // @property (nonatomic, strong) NSNumber * batteryLifeExpectancy;
        [Export ("batteryLifeExpectancy", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber BatteryLifeExpectancy { get; set; }

        // @property (nonatomic, strong) NSString * hardware;
        [Export ("hardware", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Hardware { get; set; }

        // @property (nonatomic, strong) NSString * firmware;
        [Export ("firmware", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Firmware { get; set; }

        // @property (assign, nonatomic) ESTBeaconPower power;
        [Export ("power", ArgumentSemantic.Assign)]
        BeaconPower Power { get; set; }

        // @property (assign, nonatomic) NSInteger advInterval;
        [Export ("advInterval", ArgumentSemantic.Assign)]
        nint AdvInterval { get; set; }

        // @property (nonatomic, strong) NSNumber * basicPowerMode;
        [Export ("basicPowerMode", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber BasicPowerMode { get; set; }

        // @property (nonatomic, strong) NSNumber * smartPowerMode;
        [Export ("smartPowerMode", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber SmartPowerMode { get; set; }

        // @property (nonatomic, strong) NSNumber * batteryLevel;
        [Export ("batteryLevel", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber BatteryLevel { get; set; }

        // @property (nonatomic, strong) NSNumber * latitude;
        [Export ("latitude", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Latitude { get; set; }

        // @property (nonatomic, strong) NSNumber * longitude;
        [Export ("longitude", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Longitude { get; set; }

        // @property (nonatomic, strong) NSString * location;
        [Export ("location", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Location { get; set; }

        // @property (nonatomic) NSString * _Nullable city;
        [Export ("city", ArgumentSemantic.Strong)]
        [NullAllowed]
        string City { get; set; }

        // @property (nonatomic) NSString * _Nullable country;
        [Export ("country", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Country { get; set; }

        // @property (nonatomic) NSString * _Nullable formattedAddress;
        [Export ("formattedAddress", ArgumentSemantic.Strong)]
        [NullAllowed]
        string FormattedAddress { get; set; }

        // @property (nonatomic) NSString * _Nullable stateName;
        [Export ("stateName", ArgumentSemantic.Strong)]
        [NullAllowed]
        string StateName { get; set; }

        // @property (nonatomic) NSString * _Nullable stateCode;
        [Export ("stateCode", ArgumentSemantic.Strong)]
        [NullAllowed]
        string StateCode { get; set; }

        // @property (nonatomic) NSString * _Nullable streetName;
        [Export ("streetName", ArgumentSemantic.Strong)]
        [NullAllowed]
        string StreetName { get; set; }

        // @property (nonatomic) NSString * _Nullable streetNumber;
        [Export ("streetNumber", ArgumentSemantic.Strong)]
        [NullAllowed]
        string StreetNumber { get; set; }

        // @property (nonatomic) NSString * _Nullable zipCode;
        [Export ("zipCode", ArgumentSemantic.Strong)]
        [NullAllowed]
        string ZipCode { get; set; }

        // @property (nonatomic) NSString * _Nullable indoorLocationIdentifier;
        [Export ("indoorLocationIdentifier", ArgumentSemantic.Strong)]
        [NullAllowed]
        string IndoorLocationIdentifier { get; set; }

        // @property (nonatomic) NSString * _Nullable indoorLocationName;
        [Export ("indoorLocationName", ArgumentSemantic.Strong)]
        [NullAllowed]
        string IndoorLocationName { get; set; }

        // @property (nonatomic, strong) NSString * eddystoneNamespaceID;
        [Export ("eddystoneNamespaceID", ArgumentSemantic.Strong)]
        [NullAllowed]
        string EddystoneNamespaceID { get; set; }

        // @property (nonatomic, strong) NSString * eddystoneInstanceID;
        [Export ("eddystoneInstanceID", ArgumentSemantic.Strong)]
        [NullAllowed]
        string EddystoneInstanceID { get; set; }

        // @property (nonatomic, strong) NSString * eddystoneURL;
        [Export ("eddystoneURL", ArgumentSemantic.Strong)]
        [NullAllowed]
        string EddystoneUrl { get; set; }

        // @property (nonatomic, strong) NSNumber * motionDetection;
        [Export ("motionDetection", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber MotionDetection { get; set; }

        // @property (assign, nonatomic) ESTBeaconConditionalBroadcasting conditionalBroadcasting;
        [Export ("conditionalBroadcasting", ArgumentSemantic.Assign)]
        BeaconConditionalBroadcasting ConditionalBroadcasting { get; set; }

        // @property (nonatomic, strong) NSNumber * _Nullable security;
        [Export ("security", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Security { get; set; }

        // @property (assign, nonatomic) BOOL isSecured;
        [Obsolete ("Use Security instead")] // Obsolete in 3.7.0
        [Export ("isSecured")]
        bool IsSecured { get; set; }

        // @property (nonatomic) ESTColor color;
        [Export ("color", ArgumentSemantic.Assign)]
        Color Color { get; set; }

        // -(instancetype)initWithCloudData:(NSDictionary *)data;
        [Export ("initWithCloudData:")]
        IntPtr Constructor (NSDictionary data);
    }

    [BaseType (typeof (NSObject), Name = "ESTFirmwareInfoVO")]
    interface FirmwareInfoVO : INSCoding
    {
        // @property (nonatomic, strong) NSString * _Nullable hardwareVersion;
        [Export ("hardwareVersion", ArgumentSemantic.Strong)]
        [NullAllowed]
        string HardwareVersion { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable firmwareVersion;
        [Export ("firmwareVersion", ArgumentSemantic.Strong)]
        [NullAllowed]
        string FirmwareVersion { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable changelog;
        [Export ("changelog", ArgumentSemantic.Strong)]
        [NullAllowed]
        string Changelog { get; set; }

        // @property (nonatomic, assign) BOOL isUpdateAvailable;
        [Export ("isUpdateAvailable", ArgumentSemantic.Assign)]
        bool IsUpdateAvailable { get; set; }
    }

    [BaseType (typeof (NSObject), Name = "ESTNearableVO")]
    interface NearableVO
    {
        // @property (nonatomic, strong) NSString *identifier;
        [Export ("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; set; }

        // @property (nonatomic, assign) ESTNearableType type;
        [Export ("type", ArgumentSemantic.Assign)]
        NearableType Type { get; set; }

        // @property (nonatomic, assign) ESTColor color;
        [Export ("color", ArgumentSemantic.Assign)]
        Color Color { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable indoorLocationName;
        [Export ("indoorLocationName", ArgumentSemantic.Strong)]
        [NullAllowed]
        string IndoorLocationName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable indoorLocationIdentifier;
        [Export ("indoorLocationIdentifier", ArgumentSemantic.Strong)]
        [NullAllowed]
        string IndoorLocationIdentifier { get; set; }

        // @property (nonatomic, strong) NSNumber *advInterval;
        [Export ("advInterval", ArgumentSemantic.Strong)]
        NSNumber AdvInterval { get; set; }

        // @property (nonatomic, strong) NSNumber *power;
        [Export ("power", ArgumentSemantic.Strong)]
        NSNumber Power { get; set; }

        // @property (nonatomic, strong) NSString* hardware;
        [Export ("hardware", ArgumentSemantic.Strong)]
        string Hardware { get; set; }

        // @property (nonatomic, strong) NSString* firmware;
        [Export ("firmware", ArgumentSemantic.Strong)]
        string Firmware { get; set; }

        // @property (nonatomic, strong) NSString* name;
        [Export ("name", ArgumentSemantic.Strong)]
        string Name { get; set; }

        // @property (nonatomic, strong) NSNumber* motionOnly;
        [Export ("motionOnly", ArgumentSemantic.Strong)]
        NSNumber MotionOnly { get; set; }

        // @property (nonatomic, strong) NSNumber* broadcastingScheme;
        [Export ("broadcastingScheme", ArgumentSemantic.Strong)]
        NSNumber BroadcastingScheme { get; set; }

        // @property (nonatomic, strong) NSString* proximityUUID;
        [Export ("proximityUUID", ArgumentSemantic.Strong)]
        string ProximityUUID { get; set; }

        // @property (nonatomic, strong) NSNumber* major;
        [Export ("major", ArgumentSemantic.Strong)]
        NSNumber Major { get; set; }

        // @property (nonatomic, strong) NSNumber* minor;
        [Export ("minor", ArgumentSemantic.Strong)]
        NSNumber Minor { get; set; }

        // @property (nonatomic, strong) NSString* eddystoneURL;
        [Export ("eddystoneURL", ArgumentSemantic.Strong)]
        string EddystoneUrl { get; set; }

        // - (instancetype)initWithData:(NSDictionary *)data;
        [Export ("initWithData:")]
        IntPtr Constructor (NSDictionary data);
    }

    interface IBeaconConnectionDelegate { }

    // @protocol ESTBeaconConnectionDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTBeaconConnectionDelegate")]
    interface BeaconConnectionDelegate
    {
        // @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didVerifyWithData:(ESTBeaconVO *)data error:(NSError *)error;
        [Export ("beaconConnection:didVerifyWithData:error:"), EventArgs ("VerifiedWithDataArgs")]
        void Verified (BeaconConnection connection, BeaconVO data, NSError error);

        // @optional -(void)beaconConnectionDidSucceed:(ESTBeaconConnection *)connection;
        [Export ("beaconConnectionDidSucceed:")]
        void Succeeded (BeaconConnection connection);

        // @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didFailWithError:(NSError *)error;
        [Export ("beaconConnection:didFailWithError:"), EventArgs ("FailedWithErrorArgs")]
        void Failed (BeaconConnection connection, NSError error);

        // @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didDisconnectWithError:(NSError *)error;
        [Export ("beaconConnection:didDisconnectWithError:"), EventArgs ("DisconnectedWithErrorArgs")]
        void Disconnected (BeaconConnection connection, NSError error);

        // @optional -(void)beaconConnection:(ESTBeaconConnection *)connection motionStateChanged:(ESTBeaconMotionState)state;
        [Export ("beaconConnection:motionStateChanged:"), EventArgs ("MotionStateChangedArgs")]
        void MotionStateChanged (BeaconConnection connection, BeaconMotionState state);

        // @optional -(void)beaconConnection:( BeaconConnection *)connection didUpdateRSSI:(NSNumber *)rssi;
        [Export ("beaconConnection:didUpdateRSSI:"), EventArgs ("UpdatedRssi")]
        void UpdatedRssi (BeaconConnection connection, NSNumber rssi);
    }

    // @interface ESTBeaconConnection : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBeaconConnection", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconConnectionDelegate) })]
    interface BeaconConnection
    {
        // @property (nonatomic, weak) id<ESTBeaconConnectionDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        IBeaconConnectionDelegate Delegate { get; set; }


        // @property (readonly, nonatomic, strong) NSString * identifier;
        [Export ("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        // @property (readonly, nonatomic) ESTConnectionStatus connectionStatus;
        [Export ("connectionStatus")]
        ConnectionStatus ConnectionStatus { get; }

        // +(instancetype)connectionWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate>)delegate;
        [Static]
        [Export ("connectionWithProximityUUID:major:minor:delegate:")]
        BeaconConnection ConnectionWithProximityUUID (NSUuid proximityUUID, ushort major, ushort minor, BeaconConnectionDelegate _delegate);

        // +(instancetype)connectionWithBeacon:(CLBeacon *)beacon delegate:(id<ESTBeaconConnectionDelegate>)delegate;
        [Static]
        [Export ("connectionWithBeacon:delegate:")]
        BeaconConnection ConnectionWithBeacon (CLBeacon beacon, BeaconConnectionDelegate _delegate);

        // +(instancetype)connectionWithMacAddress:(NSString *)macAddress delegate:(id<ESTBeaconConnectionDelegate>)delegate;
        [Static]
        [Export ("connectionWithMacAddress:delegate:")]
        BeaconConnection ConnectionWithMacAddress (string macAddress, BeaconConnectionDelegate _delegate);

        // -(instancetype)initWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
        [Export ("initWithProximityUUID:major:minor:delegate:startImmediately:")]
        IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor, BeaconConnectionDelegate _delegate, bool startImmediately);

        // -(instancetype)initWithBeacon:(CLBeacon *)beacon delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
        [Export ("initWithBeacon:delegate:startImmediately:")]
        IntPtr Constructor (CLBeacon beacon, BeaconConnectionDelegate _delegate, bool startImmediately);

        // -(instancetype)initWithMacAddress:(NSString *)macAddress delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
        [Export ("initWithMacAddress:delegate:startImmediately:")]
        IntPtr Constructor (string macAddress, BeaconConnectionDelegate _delegate, bool startImmediately);

        // -(void)startConnection;
        [Export ("startConnection")]
        void StartConnection ();

        // -(void)startConnectionWithAttempts:(NSInteger)attempts connectionTimeout:(NSInteger)timeout;
        [Export ("startConnectionWithAttempts:connectionTimeout:")]
        void StartConnectionWithAttempts (nint attempts, nint timeout);

        // -(void)cancelConnection;
        [Export ("cancelConnection")]
        void CancelConnection ();

        // -(void)disconnect;
        [Export ("disconnect")]
        void Disconnect ();

        // @property (readonly, nonatomic) NSString * macAddress;
        [Export ("macAddress")]
        string MacAddress { get; }

        // @property (readonly, nonatomic) NSString * name;
        [Export ("name")]
        string Name { get; }

        // @property (readonly, nonatomic) ESTColor color;
        [Export ("color")]
        Color Color { get; }

        // @property (readonly, nonatomic) CBPeripheral * peripheral;
        [Export ("peripheral")]
        CBPeripheral Peripheral { get; }

        // @property (readonly, nonatomic) ESTBroadcastingScheme broadcastingScheme;
        [Export ("broadcastingScheme")]
        BroadcastingScheme BroadcastingScheme { get; }

        // @property (readonly, nonatomic) NSUUID * proximityUUID;
        [Export ("proximityUUID")]
        NSUuid ProximityUUID { get; }

        // @property (readonly, nonatomic) NSUUID * motionProximityUUID;
        [Export ("motionProximityUUID")]
        NSUuid MotionProximityUUID { get; }

        // @property (readonly, nonatomic) NSNumber * major;
        [Export ("major")]
        NSNumber Major { get; }

        // @property (readonly, nonatomic) NSNumber * minor;
        [Export ("minor")]
        NSNumber Minor { get; }

        // @property (readonly, nonatomic) NSNumber * power;
        [Export ("power")]
        NSNumber Power { get; }

        // @property (readonly, nonatomic) NSNumber * advInterval;
        [Export ("advInterval")]
        NSNumber AdvInterval { get; }

        // @property (readonly, nonatomic) NSString * eddystoneNamespace;
        [Export ("eddystoneNamespace")]
        string EddystoneNamespace { get; }

        // @property (readonly, nonatomic) NSString * eddystoneInstance;
        [Export ("eddystoneInstance")]
        string EddystoneInstance { get; }

        // @property (readonly, nonatomic) NSString * eddystoneURL;
        [Export ("eddystoneURL")]
        string EddystoneURL { get; }

        // @property (readonly, nonatomic) NSString * hardwareVersion;
        [Export ("hardwareVersion")]
        string HardwareVersion { get; }

        // @property (readonly, nonatomic) NSString * firmwareVersion;
        [Export ("firmwareVersion")]
        string FirmwareVersion { get; }

        // @property (readonly, nonatomic) NSNumber * rssi;
        [Export ("rssi")]
        NSNumber Rssi { get; }

        // @property (readonly, nonatomic) NSNumber * batteryLevel;
        [Export ("batteryLevel")]
        NSNumber BatteryLevel { get; }

        // @property (readonly, nonatomic) ESTBeaconBatteryType batteryType;
        [Export ("batteryType")]
        BeaconBatteryType BatteryType { get; }

        // @property (readonly, nonatomic) NSNumber * remainingLifetime;
        [Export ("remainingLifetime")]
        NSNumber RemainingLifetime { get; }

        // @property (readonly, nonatomic) ESTBeaconPowerSavingMode basicPowerMode;
        [Export ("basicPowerMode")]
        BeaconPowerSavingMode BasicPowerMode { get; }

        // @property (readonly, nonatomic) ESTBeaconPowerSavingMode smartPowerMode;
        [Export ("smartPowerMode")]
        BeaconPowerSavingMode SmartPowerMode { get; }

        // @property (readonly, nonatomic) ESTBeaconEstimoteSecureUUID estimoteSecureUUIDState;
        [Export ("estimoteSecureUUIDState")]
        BeaconEstimoteSecureUUID EstimoteSecureUUIDState { get; }

        // @property (readonly, nonatomic) ESTBeaconMotionUUID motionUUIDState;
        [Export ("motionUUIDState")]
        BeaconMotionUUID MotionUUIDState { get; }

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

        // -(void)readTemperatureWithCompletion:(ESTNumberCompletionBlock)completion;
        [Export ("readTemperatureWithCompletion:"), Async]
        void ReadTemperature (NumberCompletionBlock completion);

        // -(void)readAccelerometerCountWithCompletion:(ESTNumberCompletionBlock)completion;
        [Export ("readAccelerometerCountWithCompletion:"), Async]
        void ReadAccelerometerCount (NumberCompletionBlock completion);

        // -(void)resetAccelerometerCountWithCompletion:(ESTUnsignedShortCompletionBlock)completion;
        [Export ("resetAccelerometerCountWithCompletion:"), Async]
        void ResetAccelerometerCount (UnsignedShortCompletionBlock completion);

        // -(void)writeBroadcastingScheme:(ESTBroadcastingScheme)broadcastingScheme completion:(ESTUnsignedShortCompletionBlock)completion;
        [Export ("writeBroadcastingScheme:completion:"), Async]
        void WriteBroadcastingScheme (BroadcastingScheme broadcastingScheme, UnsignedShortCompletionBlock completion);

        // -(void)writeConditionalBroadcastingType:(ESTBeaconConditionalBroadcasting)conditionalBroadcasting completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeConditionalBroadcastingType:completion:"), Async]
        void WriteConditionalBroadcastingType (BeaconConditionalBroadcasting conditionalBroadcasting, BoolCompletionBlock completion);

        // -(void)writeName:(NSString *)name completion:(ESTStringCompletionBlock)completion;
        [Export ("writeName:completion:"), Async]
        void WriteName (string name, StringCompletionBlock completion);

        // -(void)writeProximityUUID:(NSString *)pUUID completion:(ESTStringCompletionBlock)completion;
        [Export ("writeProximityUUID:completion:"), Async]
        void WriteProximityUUID (string pUUID, StringCompletionBlock completion);

        // -(void)writeMajor:(unsigned short)major completion:(ESTUnsignedShortCompletionBlock)completion;
        [Export ("writeMajor:completion:"), Async]
        void WriteMajor (ushort major, UnsignedShortCompletionBlock completion);

        // -(void)writeMinor:(unsigned short)minor completion:(ESTUnsignedShortCompletionBlock)completion;
        [Export ("writeMinor:completion:"), Async]
        void WriteMinor (ushort minor, UnsignedShortCompletionBlock completion);

        // -(void)writeAdvInterval:(unsigned short)interval completion:(ESTUnsignedShortCompletionBlock)completion;
        [Export ("writeAdvInterval:completion:"), Async]
        void WriteAdvInterval (ushort interval, UnsignedShortCompletionBlock completion);

        // -(void)writePower:(ESTBeaconPower)power completion:(ESTPowerCompletionBlock)completion;
        [Export ("writePower:completion:"), Async]
        void WritePower (BeaconPower beaconPower, PowerCompletionBlock completion);

        // -(void)writeEddystoneDomainNamespace:(NSString *)eddystoneNamespace completion:(ESTStringCompletionBlock)completion;
        [Export ("writeEddystoneDomainNamespace:completion:"), Async]
        void WriteEddystoneDomainNamespace (string eddystoneNamespace, StringCompletionBlock completion);

        // -(void)writeEddystoneHexNamespace:(NSString *)eddystoneNamespace completion:(ESTStringCompletionBlock)completion;
        [Export ("writeEddystoneHexNamespace:completion:"), Async]
        void WriteEddystoneHexNamespace (string eddystoneNamespace, StringCompletionBlock completion);

        // -(void)writeEddystoneInstance:(NSString *)eddystoneInstance completion:(ESTStringCompletionBlock)completion;
        [Export ("writeEddystoneInstance:completion:"), Async]
        void WriteEddystoneInstance (string eddystoneInstance, StringCompletionBlock completion);

        // -(void)writeEddystoneURL:(NSString *)eddystoneURL completion:(ESTStringCompletionBlock)completion;
        [Export ("writeEddystoneURL:completion:"), Async]
        void WriteEddystoneUrl (string eddystoneUrl, StringCompletionBlock completion);

        // -(void)writeBasicPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeBasicPowerModeEnabled:completion:"), Async]
        void WriteBasicPowerModeEnabled (bool enable, BoolCompletionBlock completion);

        // -(void)writeSmartPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeSmartPowerModeEnabled:completion:"), Async]
        void WriteSmartPowerModeEnabled (bool enable, BoolCompletionBlock completion);

        // -(void)writeEstimoteSecureUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeEstimoteSecureUUIDEnabled:completion:"), Async]
        void WriteEstimoteSecureUUIDEnabled (bool enable, BoolCompletionBlock completion);

        // -(void)writeMotionDetectionEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeMotionDetectionEnabled:completion:"), Async]
        void WriteMotionDetectionEnabled (bool enable, BoolCompletionBlock completion);

        // -(void)writeMotionUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
        [Export ("writeMotionUUIDEnabled:completion:"), Async]
        void WriteMotionUUIDEnabled (bool enable, BoolCompletionBlock completion);

        // -(void)writeCalibratedTemperature:(NSNumber *)temperature completion:(ESTNumberCompletionBlock)completion;
        [Export ("writeCalibratedTemperature:completion:"), Async]
        void WriteCalibratedTemperature (NSNumber temperature, NumberCompletionBlock completion);

        // -  (void)writeTags:(NSSet <NSString *> *)tags completion:(ESTCompletionBlock)completion;
        [Export ("writeTags:completion:"), Async]
        void WriteTags (string [] set, CompletionBlock completion);

        // -(void)resetToFactorySettingsWithCompletion:(ESTCompletionBlock)completion;
        [Export ("resetToFactorySettingsWithCompletion:"), Async]
        void ResetToFactorySettings (CompletionBlock completion);

        // -(void)getMacAddressWithCompletion:(ESTStringCompletionBlock)completion;
        [Export ("getMacAddressWithCompletion:"), Async]
        void GetMacAddress (StringCompletionBlock completion);

        // -(void)findPeripheralForBeaconWithTimeout:(NSUInteger)timeout completion:(ESTObjectCompletionBlock)completion;
        [Export ("findPeripheralForBeaconWithTimeout:completion:"), Async]
        void FindPeripheralForBeaconWithTimeout (nuint timeout, ObjectCompletionBlock completion);

        // -(void)checkFirmwareUpdateWithCompletion:(ESTObjectCompletionBlock)completion;
        [Export ("checkFirmwareUpdateWithCompletion:"), Async]
        void CheckFirmwareUpdate (ObjectCompletionBlock completion);

        // -(void)updateFirmwareWithProgress:(ESTProgressBlock)progress completion:(ESTCompletionBlock)completion;
        [Export ("updateFirmwareWithProgress:completion:"), Async]
        void UpdateFirmware (ProgressBlock progress, CompletionBlock completion);

        // - (ESTBeaconVO *)valueObject;
        [Export ("valueObject")]
        BeaconVO ValueObject { get; }

    }

    // @interface ESTBluetoothBeacon : NSObject
    [BaseType (typeof (Device), Name = "ESTBluetoothBeacon")]
    interface BluetoothBeacon
    {
        // Removed in 3.7.0
        // @property (nonatomic, strong) NSString * macAddress;
        //[Export ("macAddress", ArgumentSemantic.Strong)]
        //string MacAddress { get; set; }

        // @property (nonatomic, strong) NSNumber * major;
        [Export ("major", ArgumentSemantic.Strong)]
        NSNumber Major { get; set; }

        // @property (nonatomic, strong) NSNumber * minor;
        [Export ("minor", ArgumentSemantic.Strong)]
        NSNumber Minor { get; set; }

        // @property (nonatomic, strong) CBPeripheral * peripheral;
        [Export ("peripheral", ArgumentSemantic.Strong)]
        CBPeripheral Peripheral { get; set; }

        // @property (nonatomic, strong) NSNumber * measuredPower;
        [Export ("measuredPower", ArgumentSemantic.Strong)]
        NSNumber MeasuredPower { get; set; }

        // Removed in 3.7.0
        // @property (nonatomic, strong) NSDate * discoveryDate;
        //[Export ("discoveryDate", ArgumentSemantic.Strong)]
        //NSDate DiscoveryDate { get; set; }

        // Removed in 3.7.0
        // @property (nonatomic, strong) NSData * advertisementData;
        //[Export ("advertisementData", ArgumentSemantic.Strong)]
        //NSData AdvertisementData { get; set; }

        // Removed in 3.7.0
        // @property (assign, nonatomic) NSInteger rssi;
        //[Export ("rssi", ArgumentSemantic.Assign)]
        // nint Rssi { get; set; }

        // @property (assign, nonatomic) NSInteger firmwareState;
        [Export ("firmwareState", ArgumentSemantic.Assign)]
        nint FirmwareState { get; set; }
    }

    interface IUtilityManagerDelegate { }

    // @protocol ESTUtilityManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTUtilityManagerDelegate")]
    interface UtilityManagerDelegate
    {
        // @optional -(void)utilityManager:(ESTUtilityManager *)manager didDiscoverBeacons:(NSArray *)beacons;
        [Export ("utilityManager:didDiscoverBeacons:"), EventArgs ("DiscoveredBeacons")]
        void DiscoveredBeacons (UtilityManager manager, BluetoothBeacon [] beacons);

        // @optional - (void)utilityManager:(ESTUtilityManager *)manager didDiscoverNearables:(NSArray<ESTDeviceNearable *> *)nearables;
        [Export ("utilityManager:didDiscoverNearables:"), EventArgs ("DiscoveredNearables")]
        void DiscoveredNearables (UtilityManager manager, DeviceNearable [] nearables);

        // @optional -(void)utilityManagerDidFailDiscovery:(ESTUtilityManager *)manager;
        [Export ("utilityManagerDidFailDiscovery:"), EventArgs ("DiscoveryFailed")]
        void DiscoveryFailed (UtilityManager manager);
    }

    // @interface ESTUtilityManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTUtilityManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (UtilityManagerDelegate) })]
    interface UtilityManager
    {
        // @property (readonly, assign, nonatomic) ESTUtilityManagerState state;
        [Export ("state", ArgumentSemantic.Assign)]
        UtilityManagerState State { get; }

        // @property (nonatomic, weak) id<ESTUtilityManagerDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        IUtilityManagerDelegate Delegate { get; set; }

        // -(void)startEstimoteBeaconDiscovery;
        [Export ("startEstimoteBeaconDiscovery")]
        void StartEstimoteBeaconDiscovery ();

        // -(void)startEstimoteBeaconDiscoveryWithUpdateInterval:(NSTimeInterval)interval;
        [Export ("startEstimoteBeaconDiscoveryWithUpdateInterval:")]
        void StartEstimoteBeaconDiscoveryWithUpdateInterval (double interval);

        // -(void)stopEstimoteBeaconDiscovery;
        [Export ("stopEstimoteBeaconDiscovery")]
        void StopEstimoteBeaconDiscovery ();

        // - (void)startEstimoteNearableDiscovery;
        [Export ("startEstimoteNearableDiscovery")]
        void StartEstimoteNearableDiscovery ();

        // - (void)startEstimoteNearableDiscoveryWithUpdateInterval:(NSTimeInterval)interval;
        [Export ("startEstimoteNearableDiscoveryWithUpdateInterval:")]
        void StartEstimoteNearableDiscovery (double interval);

        // - (void)stopEstimoteNearableDiscovery;
        [Export ("stopEstimoteNearableDiscovery")]
        void StopEstimoteNearableDiscovery ();
    }

    interface INearableManagerDelegate { }

    // @protocol ESTNearableManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTNearableManagerDelegate")]
    interface NearableManagerDelegate
    {
        // @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearables:(NSArray *)nearables withType:(ESTNearableType)type;
        [Export ("nearableManager:didRangeNearables:withType:"), EventArgs ("DidRangeNearables")]
        void RangedNearables (NearableManager manager, Nearable [] nearables, NearableType type);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearable:(ESTNearable *)nearable;
        [Export ("nearableManager:didRangeNearable:"), EventArgs ("DidRangeNearable")]
        void RangedNearable (NearableManager manager, Nearable nearable);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager rangingFailedWithError:(NSError *)error;
        [Export ("nearableManager:rangingFailedWithError:"), EventArgs ("RangingFailed")]
        void RangingFailed (NearableManager manager, NSError error);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterTypeRegion:(ESTNearableType)type;
        [Export ("nearableManager:didEnterTypeRegion:"), EventArgs ("EnteredTypeRegion")]
        void EnteredTypeRegion (NearableManager manager, NearableType type);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager didExitTypeRegion:(ESTNearableType)type;
        [Export ("nearableManager:didExitTypeRegion:"), EventArgs ("ExitedTypeRegion")]
        void ExitedTypeRegion (NearableManager manager, NearableType type);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterIdentifierRegion:(NSString *)identifier;
        [Export ("nearableManager:didEnterIdentifierRegion:"), EventArgs ("EnteredIdentifierRegion")]
        void EnteredIdentifierRegion (NearableManager manager, string identifier);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager didExitIdentifierRegion:(NSString *)identifier;
        [Export ("nearableManager:didExitIdentifierRegion:"), EventArgs ("ExitedIdentifierRegion")]
        void ExitedIdentifierRegion (NearableManager manager, string identifier);

        // @optional -(void)nearableManager:(ESTNearableManager *)manager monitoringFailedWithError:(NSError *)error;
        [Export ("nearableManager:monitoringFailedWithError:"), EventArgs ("MonitoringFailed")]
        void MonitoringFailed (NearableManager manager, NSError error);
    }

    // @interface ESTNearableManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTNearableManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (NearableManagerDelegate) })]
    interface NearableManager
    {
        // @property (nonatomic, weak) id<ESTNearableManagerDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        INearableManagerDelegate Delegate { get; set; }

        // -(void)startMonitoringForIdentifier:(NSString *)identifier;
        [Export ("startMonitoringForIdentifier:")]
        void StartMonitoringForIdentifier (string identifier);

        // -(void)stopMonitoringForIdentifier:(NSString *)identifier;
        [Export ("stopMonitoringForIdentifier:")]
        void StopMonitoringForIdentifier (string identifier);

        // -(void)startMonitoringForType:(ESTNearableType)type;
        [Export ("startMonitoringForType:")]
        void StartMonitoringForType (NearableType type);

        // -(void)stopMonitoringForType:(ESTNearableType)type;
        [Export ("stopMonitoringForType:")]
        void StopMonitoringForType (NearableType type);

        // -(void)stopMonitoring;
        [Export ("stopMonitoring")]
        void StopMonitoring ();

        // -(void)startRangingForIdentifier:(NSString *)identifier;
        [Export ("startRangingForIdentifier:")]
        void StartRangingForIdentifier (string identifier);

        // -(void)stopRangingForIdentifier:(NSString *)identifier;
        [Export ("stopRangingForIdentifier:")]
        void StopRangingForIdentifier (string identifier);

        // -(void)startRangingForType:(ESTNearableType)type;
        [Export ("startRangingForType:")]
        void StartRangingForType (NearableType type);

        // -(void)stopRangingForType:(ESTNearableType)type;
        [Export ("stopRangingForType:")]
        void StopRangingForType (NearableType type);

        // -(void)stopRanging;
        [Export ("stopRanging")]
        void StopRanging ();
    }

    // @interface ESTSimulatedNearableManager : ESTNearableManager <ESTNearableManagerDelegate>
    [BaseType (typeof (NearableManager), Name = "ESTSimulatedNearableManager")]
    interface SimulatedNearableManager : NearableManagerDelegate
    {
        // @property (readonly, nonatomic, strong) NSMutableArray * nearables;
        [Export ("nearables", ArgumentSemantic.Strong)]
        NSMutableArray<Nearable> Nearables { get; }

        // -(instancetype)initWithDelegate:(id<ESTNearableManagerDelegate>)delegate;
        [Export ("initWithDelegate:")]
        IntPtr Constructor ([NullAllowed]NearableManagerDelegate _delegate);

        // -(instancetype)initWithDelegate:(id<ESTNearableManagerDelegate>)delegate pathForJSON:(NSString *)path;
        [Export ("initWithDelegate:pathForJSON:")]
        IntPtr Constructor ([NullAllowed]NearableManagerDelegate _delegate, string path);

        // -(ESTNearable *)generateRandomNearableAndAddToSimulator:(BOOL)add;
        [Export ("generateRandomNearableAndAddToSimulator:")]
        Nearable GenerateRandomNearableAndAddToSimulator (bool add);

        // -(void)addNearableToSimulation:(NSString *)identifier withType:(ESTNearableType)type zone:(ESTNearableZone)zone rssi:(NSInteger)rssi;
        [Export ("addNearableToSimulation:withType:zone:rssi:")]
        void AddNearableToSimulation (string identifier, NearableType type, NearableZone zone, nint rssi);

        // -(void)addNearablesToSimulationWithPath:(NSString *)path;
        [Export ("addNearablesToSimulationWithPath:")]
        void AddNearablesToSimulationWithPath (string path);

        // -(void)simulateZone:(ESTNearableZone)zone forNearable:(NSString *)identifier;
        [Export ("simulateZone:forNearable:")]
        void SimulateZone (NearableZone zone, string identifier);

        // -(void)simulateDidEnterRegionForNearable:(ESTNearable *)nearable;
        [Export ("simulateDidEnterRegionForNearable:")]
        void SimulateDidEnterRegionForNearable (Nearable nearable);

        // -(void)simulateDidExitRegionForNearable:(ESTNearable *)nearable;
        [Export ("simulateDidExitRegionForNearable:")]
        void SimulateDidExitRegionForNearable (Nearable nearable);
    }

    delegate void FetchNearablesCompletionBlock (Nearable [] nearables, NSError error);
    delegate void FetchBeaconsCompletionBlock (BeaconVO [] beacons, NSError error);

    // @interface ESTCloudManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTCloudManager")]
    interface CloudManager
    {
        // +(void)setupAppID:(NSString *)appID andAppToken:(NSString *)appToken;
        [Static]
        [Export ("setupAppID:andAppToken:")]
        void SetupAppID (string appID, string appToken);

        [Static]
        [Export ("appId")]
        [NullAllowed]
        string AppId { get; }

        [Static]
        [Export ("appToken")]
        [NullAllowed]
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
        void EnableGPSPositioningForAnalytics (bool enable);

        // +(BOOL)isAnalyticsEnabled __attribute__((deprecated("Starting from SDK 3.2.0 use enableMonitoringAnalytics: or enableRangingAnalytics: instead.")));
        [Obsolete ("Use EnableMonitoringAnalytics or EnableRangingAnalytics instead")]
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

        // -(void)fetchEstimoteNearablesWithCompletion:(ESTArrayCompletionBlock)completion;
        [Export ("fetchEstimoteNearablesWithCompletion:"), Async]
        void FetchEstimoteNearables (FetchNearablesCompletionBlock completion);

        // -(void)fetchEstimoteBeaconsWithCompletion:(ESTArrayCompletionBlock)completion;
        [Export ("fetchEstimoteBeaconsWithCompletion:"), Async]
        void FetchEstimoteBeacons (FetchBeaconsCompletionBlock completion);

        // -(void)fetchBeaconDetails:(NSString *)beaconUID completion:(ESTObjectCompletionBlock)completion;
        [Export ("fetchBeaconDetails:completion:"), Async]
        void FetchBeaconDetails (string beaconUID, ObjectCompletionBlock completion);

        // -(void)fetchColorForBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
        [Export ("fetchColorForBeacon:completion:"), Async]
        void FetchColor (CLBeacon beacon, ObjectCompletionBlock completion);

        // -(void)fetchColorForBeaconWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor completion:(ESTObjectCompletionBlock)completion;
        [Export ("fetchColorForBeaconWithProximityUUID:major:minor:completion:"), Async]
        void FetchColor (NSUuid proximityUUID, ushort major, ushort minor, ObjectCompletionBlock completion);

        // -(void)fetchColorForBeaconWithMacAddress:(NSString *)macAddress completion:(ESTObjectCompletionBlock)completion;
        [Export ("fetchColorForBeaconWithMacAddress:completion:"), Async]
        void FetchColor (string macAddress, ObjectCompletionBlock completion);

        // -(void)fetchMacAddressForBeacon:(CLBeacon *)beacon completion:(ESTStringCompletionBlock)completion;
        [Export ("fetchMacAddressForBeacon:completion:"), Async]
        void FetchMacAddress (CLBeacon beacon, StringCompletionBlock completion);

        // -(void)assignGPSLocation:(CLLocation *)location toBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
        [Export ("assignGPSLocation:toBeacon:completion:"), Async]
        void AssignGPSLocation (CLLocation location, CLBeacon beacon, ObjectCompletionBlock completion);

        // -(void)assignCurrentGPSLocationToBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
        [Export ("assignCurrentGPSLocationToBeacon:completion:"), Async]
        void AssignCurrentGPSLocation (CLBeacon beacon, ObjectCompletionBlock completion);

        // - (void)assignCurrentGPSLocationToBeaconWithProximityUUID:(NSUUID *)uuid major:(NSNumber *)major minor:(NSNumber *)minor completion:(ESTObjectCompletionBlock)completion;
        [Export ("assignCurrentGPSLocationToBeaconWithProximityUUID:major:minor:completion:"), Async]
        void AssignCurrentGPSLocation (NSUuid uuid, ushort major, ushort minor, ObjectCompletionBlock completion);

        // -(void)registerDeviceForRemoteManagement:(NSData *)deviceToken completion:(ESTCompletionBlock)completion;
        [Export ("registerDeviceForRemoteManagement:completion:"), Async]
        void RegisterDeviceForRemoteManagement (NSData deviceToken, CompletionBlock completion);

        // -(void)fetchPendingBeaconsSettingsWithCompletion:(ESTArrayCompletionBlock)completion;
        [Export ("fetchPendingBeaconsSettingsWithCompletion:"), Async]
        void FetchPendingBeaconsSettings (ArrayCompletionBlock completion);
    }

    interface ICloudSettingProtocol { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTCloudSettingProtocol")]
    interface CloudSettingProtocol : SettingProtocol
    {
        // Removed in 4.x
        // [Export ("updateValueInSettings:")]
        // void UpdateValue (NSObject settings);
    }

    // @interface ESTNotificationTransporter : NSObject
    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTNotificationTransporter")]
    interface NotificationTransporter
    {
        // @property (readonly, nonatomic) NSString * currentAppGroupIdentifier;
        [Export ("currentAppGroupIdentifier")]
        string CurrentAppGroupIdentifier { get; }

        // +(instancetype)sharedTransporter;
        [Static]
        [Export ("sharedTransporter")]
        NotificationTransporter SharedTransporter ();

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
        bool RangedNearables (Nearable [] nearables);

        // -(NSArray *)readRangedNearables;
        [Export ("readRangedNearables")]
        Nearable [] ReadRangedNearables { get; }

        // -(void)notifyDidEnterRegion:(CLBeaconRegion *)region;
        [Export ("notifyDidEnterRegion:")]
        void NotifyDidEnterRegion (CLBeaconRegion region);

        // -(void)notifyDidExitRegion:(CLBeaconRegion *)region;
        [Export ("notifyDidExitRegion:")]
        void NotifyDidExitRegion (CLBeaconRegion region);

        // -(void)notifyDidEnterIdentifierRegion:(NSString *)identifier;
        [Export ("notifyDidEnterIdentifierRegion:")]
        void NotifyDidEnterIdentifierRegion (string identifier);

        // -(void)notifyDidExitIdentifierRegion:(NSString *)identifier;
        [Export ("notifyDidExitIdentifierRegion:")]
        void NotifyDidExitIdentifierRegion (string identifier);

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

    // @interface ESTBeaconUpdateConfig : NSObject <NSCoding, NSCopying>
    [BaseType (typeof (NSObject), Name = "ESTBeaconUpdateConfig")]
    interface BeaconUpdateConfig : INSCoding, INSCopying
    {
        // @property (nonatomic, strong) NSString * proximityUUID;
        [Export ("proximityUUID", ArgumentSemantic.Strong)]
        [NullAllowed]
        string ProximityUUID { get; set; }

        // @property (nonatomic, strong) NSNumber * major;
        [Export ("major", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Major { get; set; }

        // @property (nonatomic, strong) NSNumber * minor;
        [Export ("minor", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Minor { get; set; }

        // @property (nonatomic, strong) NSNumber * advInterval;
        [Export ("advInterval", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber AdvInterval { get; set; }

        // @property (nonatomic, strong) NSNumber * power;
        [Export ("power", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber Power { get; set; }

        // @property (nonatomic, strong) NSNumber * basicPowerMode;
        [Export ("basicPowerMode", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber BasicPowerMode { get; set; }

        // @property (nonatomic, strong) NSNumber * smartPowerMode;
        [Export ("smartPowerMode", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber SmartPowerMode { get; set; }

        // @property (nonatomic, strong) NSNumber * estimoteSecureUUIDState;
        [Export ("estimoteSecureUUIDState", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber EstimoteSecureUUIDState { get; set; }

        // @property (nonatomic, strong) NSNumber* _Nullable conditionalBroadcasting;
        [Export ("conditionalBroadcasting", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSNumber ConditionalBroadcasting { get; set; }
    }

    /// <summary>
    /// IBeacon update info delegate.
    /// </summary>
    interface IBeaconUpdateInfoDelegate { }

    // @protocol ESBeaconUpdateInfoDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESBeaconUpdateInfoDelegate")]
    interface BeaconUpdateInfoDelegate
    {
        // @required -(void)beaconUpdateInfoInitialized:(id)beaconUpdateInfo;
        [Abstract]
        [Export ("beaconUpdateInfoInitialized:")]
        void BeaconUpdateInfoInitialized (BeaconUpdateInfo beaconUpdateInfo);
    }

    // @interface ESTBeaconUpdateInfo : NSObject <NSCoding>
    [BaseType (typeof (NSObject), Name = "ESTBeaconUpdateInfo", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (BeaconUpdateInfoDelegate) })]
    interface BeaconUpdateInfo : INSCoding
    {
        // @property (assign, nonatomic) id<ESBeaconUpdateInfoDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
        [NullAllowed]
        IBeaconUpdateInfoDelegate Delegate { get; set; }

        // @property (nonatomic, strong) ESTBeaconConnection * beaconConnection;
        [Export ("beaconConnection", ArgumentSemantic.Strong)]
        BeaconConnection BeaconConnection { get; set; }

        // @property (nonatomic, strong) NSString * macAddress;
        [Export ("macAddress", ArgumentSemantic.Strong)]
        string MacAddress { get; set; }

        // @property (nonatomic, strong) ESTBeaconUpdateConfig * config;
        [Export ("config", ArgumentSemantic.Strong)]
        BeaconUpdateConfig Config { get; set; }

        // @property (assign, nonatomic) ESBeaconUpdateInfoStatus status;
        [Export ("status", ArgumentSemantic.Assign)]
        ESBeaconUpdateInfoStatus Status { get; set; }

        /**
         *  Settings creation timestamp.
         */
        //@property (nonatomic, strong) NSDate * _Nullable createdAt;
        [ExportAttribute ("createdAt", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSDate CreatedAt { get; set; }

        /**
         *  Time when settings were applied to the device.
         */
        //@property (nonatomic, strong) NSDate * _Nullable syncedAt;
        [ExportAttribute ("syncedAt", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSDate SyncedAt { get; set; }

        // @property (nonatomic, strong) NSError * error;
        [Export ("error", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSError Error { get; set; }

        // -(instancetype)initWithMacAddress:(NSString *)macAddress config:(ESTBeaconUpdateConfig *)config;
        [Export ("initWithMacAddress:config:")]
        IntPtr Constructor (string macAddress, BeaconUpdateConfig config);

        // -(instancetype)initWithMacAddress:(NSString *)macAddress config:(ESTBeaconUpdateConfig *)config delegate:(id<ESBeaconUpdateInfoDelegate>)delegate __attribute__((objc_designated_initializer));
        [Export ("initWithMacAddress:config:delegate:")]
        IntPtr Constructor (string macAddress, BeaconUpdateConfig config, [NullAllowed]BeaconUpdateInfoDelegate _delegate);

        // -(void)findPeripheralWithTimeout:(NSTimeInterval)timeout;
        [Export ("findPeripheralWithTimeout:")]
        void FindPeripheralWithTimeout (double timeout);

        // -(void)updateWithConfig:(ESTBeaconUpdateConfig *)config;
        [Export ("updateWithConfig:")]
        void UpdateWithConfig (BeaconUpdateConfig config);

        // -(NSString *)description;
        [Export ("description")]
        string Description { get; }
    }

    partial interface Constants
    {
        // extern NSString *const ESTBulkUpdaterBeginNotification;
        [Field ("ESTBulkUpdaterBeginNotification")]
        NSString BulkUpdaterBeginNotification { get; }

        // extern NSString *const ESTBulkUpdaterProgressNotification;
        [Field ("ESTBulkUpdaterProgressNotification")]
        NSString BulkUpdaterProgressNotification { get; }

        // extern NSString *const ESTBulkUpdaterCompleteNotification;
        [Field ("ESTBulkUpdaterCompleteNotification")]
        NSString BulkUpdaterCompleteNotification { get; }

        // extern NSString *const ESTBulkUpdaterFailNotification;
        [Field ("ESTBulkUpdaterFailNotification")]
        NSString BulkUpdaterFailNotification { get; }

        // extern NSString *const ESTBulkUpdaterTimeoutNotification;
        [Field ("ESTBulkUpdaterTimeoutNotification")]
        NSString BulkUpdaterTimeoutNotification { get; }
    }

    // @interface ESTBulkUpdater : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBulkUpdater")]
    interface BulkUpdater
    {
        // @property (nonatomic, strong) NSArray * beaconInfos;
        [Export ("beaconInfos", ArgumentSemantic.Strong)]
        [NullAllowed]
        BeaconUpdateInfo [] BeaconInfos { get; set; }

        // @property (readonly, nonatomic) ESTBulkUpdaterMode mode;
        [Export ("mode")]
        BulkUpdaterMode Mode { get; }

        // @property (readonly, nonatomic) ESBulkUpdaterStatus status;
        [Export ("status")]
        BulkUpdaterStatus Status { get; }

        // +(ESTBulkUpdater *)sharedInstance;
        [Static]
        [Export ("sharedInstance")]
        BulkUpdater SharedInstance { get; }

        // +(BOOL)verifyPushNotificationPayload:(NSDictionary *)payload;
        [Static]
        [Export ("verifyPushNotificationPayload:")]
        bool VerifyPushNotificationPayload (NSDictionary payload);

        // -(void)startWithCloudSettingsAndTimeout:(NSTimeInterval)timeout;
        [Export ("startWithCloudSettingsAndTimeout:")]
        void StartWithCloudSettingsAndTimeout (double timeout);

        // -(void)startWithBeaconInfos:(NSArray *)beaconInfos timeout:(NSTimeInterval)timeout;
        [Export ("startWithBeaconInfos:timeout:")]
        void StartWithBeaconInfos (BeaconUpdateInfo [] beaconInfos, double timeout);

        // -(BOOL)isUpdateInProgressForBeaconWithMacAddress:(NSString *)macAddress;
        [Export ("isUpdateInProgressForBeaconWithMacAddress:")]
        bool IsUpdateInProgressForBeaconWithMacAddress (string macAddress);

        // -(BOOL)isBeaconWaitingForUpdate:(NSString *)macAddress;
        [Export ("isBeaconWaitingForUpdate:")]
        bool IsBeaconWaitingForUpdate (string macAddress);

        // -(NSArray *)getBeaconUpdateInfosForBeaconWithMacAddress:(NSString *)macAddress;
        [Export ("getBeaconUpdateInfosForBeaconWithMacAddress:")]
        BeaconUpdateInfo [] GetBeaconUpdateInfosForBeacon (string macAddress);

        // -(NSTimeInterval)getTimeLeftToTimeout;
        [Export ("getTimeLeftToTimeout")]
        double TimeLeftToTimeout { get; }

        // -(void)cancel;
        [Export ("cancel")]
        void Cancel ();
    }


    // @interface ESTEddystone : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name = "ESTEddystone")]
    interface Eddystone : INSCopying
    {
        // @property (nonatomic, strong) NSString * macAddress;
        [Export ("macAddress", ArgumentSemantic.Strong)]
        string MacAddress { get; set; }

        // @property (nonatomic, strong) NSUUID *peripheralIdentifier;
        [Export ("peripheralIdentifier", ArgumentSemantic.Strong)]
        NSUuid PeripheralIdentifier { get; set; }

        // @property (nonatomic, strong) NSNumber * rssi;
        [Export ("rssi", ArgumentSemantic.Strong)]
        NSNumber Rssi { get; set; }

        // @property (nonatomic, strong) NSNumber * accuracy;
        [Export ("accuracy", ArgumentSemantic.Strong)]
        NSNumber Accuracy { get; set; }

        // @property (nonatomic) ESTEddystoneProximity proximity;
        [Export ("proximity", ArgumentSemantic.Assign)]
        EddystoneProximity Proximity { get; set; }

        // @property (nonatomic, strong) NSDate * discoveryDate;
        [Export ("discoveryDate", ArgumentSemantic.Strong)]
        NSDate DiscoveryDate { get; set; }

        // @property (nonatomic, strong) NSNumber * measuredPower;
        [Export ("measuredPower", ArgumentSemantic.Strong)]
        NSNumber MeasuredPower { get; set; }

        // @property (nonatomic, strong) NSString * namespaceID;
        [Export ("namespaceID", ArgumentSemantic.Strong)]
        [NullAllowed]
        string NamespaceID { get; set; }

        // Removed in 4.x
        // // @property (nonatomic, strong) NSString * instanceID;
        //       [Export ("instanceID", ArgumentSemantic.Strong)][NullAllowed]
        // string InstanceID { get; set; }

        // // @property (nonatomic, strong) NSString * url;
        //       [Export ("url", ArgumentSemantic.Strong)][NullAllowed]
        // string Url { get; set; }

        // Removed in 4.x
        // @property (nonatomic, strong) ESTEddystoneTelemetry * telemetry;
        //[Export ("telemetry", ArgumentSemantic.Strong)]
        //[NullAllowed]
        //EddystoneTelemetry Telemetry { get; set; }

        // -(void)updateWithEddystone:(ESTEddystone *)eddystone;
        [Export ("updateWithEddystone:")]
        void Update (Eddystone eddystone);
    }

    // @interface ESTEddystoneUID : NSObject
    [BaseType (typeof (NSObject), Name = "ESTEddystoneUID")]
    interface EddystoneUID
    {
        // @property (readonly, nonatomic, strong) NSString * namespaceID;
        [Export ("namespaceID", ArgumentSemantic.Strong), NullAllowed]
        string NamespaceID { get; }

        // @property (readonly, nonatomic, strong) NSString * instanceID;
        [Export ("instanceID", ArgumentSemantic.Strong), NullAllowed]
        string InstanceID { get; }

        // -(instancetype)initWithNamespaceID:(NSString *)namespaceID;
        [Export ("initWithNamespaceID:")]
        IntPtr Constructor (string namespaceID);

        // -(instancetype)initWithNamespaceID:(NSString *)namespaceID instanceID:(NSString *)instanceID;
        [Export ("initWithNamespaceID:instanceID:")]
        IntPtr Constructor (string namespaceID, string instanceID);
    }

    // @interface ESTEddystoneFilter : NSObject
    [BaseType (typeof (NSObject), Name = "ESTEddystoneFilter")]
    interface EddystoneFilter
    {
        // -(NSArray *)filterEddystones:(NSArray *)eddystones;
        [Export ("filterEddystones:")]
        Eddystone [] FilterEddystones (Eddystone [] eddystones);
    }

    interface IEddystoneManagerDelegate { }

    // @protocol ESTEddystoneManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "ESTEddystoneManagerDelegate")]
    interface EddystoneManagerDelegate
    {
        // @optional -(void)eddystoneManager:(ESTEddystoneManager *)manager didDiscoverEddystones:(NSArray *)eddystones withFilter:(ESTEddystoneFilter *)eddystoneFilter;
        [Export ("eddystoneManager:didDiscoverEddystones:withFilter:"), EventArgs ("DiscoveredEddystones")]
        void DiscoveredEddystones (EddystoneManager manager, Eddystone [] eddystones, [NullAllowed]EddystoneFilter eddystoneFilter);

        // @optional -(void)eddystoneManagerDidFailDiscovery:( EddystoneManager *)manager withError:(NSError *)error;
        [Export ("eddystoneManagerDidFailDiscovery:withError:"), EventArgs ("DiscoveryFailed")]
        void DiscoveryFailed (EddystoneManager manager, [NullAllowed]NSError error);
    }

    // @interface ESTEddystoneManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTEddystoneManager", Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof (EddystoneManagerDelegate) })]
    interface EddystoneManager
    {
        // @property (nonatomic, weak) id<ESTEddystoneManagerDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        IEddystoneManagerDelegate Delegate { get; set; }

        // @property (readonly, nonatomic, strong) NSArray * filtersInDiscovery;
        [Export ("filtersInDiscovery", ArgumentSemantic.Strong)]
        [NullAllowed]
        EddystoneFilter [] FiltersInDiscovery { get; }

        // -(void)startEddystoneDiscoveryWithFilter:(ESTEddystoneFilter *)eddystoneFilter;
        [Export ("startEddystoneDiscoveryWithFilter:")]
        void StartEddystoneDiscovery ([NullAllowed]EddystoneFilter eddystoneFilter);

        // -(void)stopEddystoneDiscoveryWithFilter:(ESTEddystoneFilter *)eddystoneFilter;
        [Export ("stopEddystoneDiscoveryWithFilter:")]
        void StopEddystoneDiscovery ([NullAllowed]EddystoneFilter eddystoneFilter);
    }

    // @interface ESTEddystoneFilterUID : ESTEddystoneFilter
    [BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterUID")]
    interface EddystoneFilterUID
    {
        // @property (nonatomic, strong, readonly) NSString * _Nullable namespaceID;
        [Export ("namespaceID", ArgumentSemantic.Strong), NullAllowed]
        string NamespaceID { get; }

        // @property (nonatomic, strong, readonly) NSString * _Nullable instanceID;
        [Export ("instanceID", ArgumentSemantic.Strong), NullAllowed]
        string InstanceID { get; }

        // - (instancetype)initWithNamespaceID:(NSString *)namespaceID;
        [Export ("initWithNamespaceID:")]
        IntPtr Constructor (string namespaceID);

        // - (instancetype)initWithNamespaceID:(NSString *)namespaceID instanceID:(NSString *)instanceID;
        [Export ("initWithNamespaceID:instanceID:")]
        IntPtr Constructor (string namespaceID, string instanceID);
    }

    // @interface ESTEddystoneFilterURL : ESTEddystoneFilter
    [BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterURL")]
    interface EddystoneFilterUrl
    {
        // @property (readonly, nonatomic, strong) NSString * eddystoneURL;
        [Export ("eddystoneURL", ArgumentSemantic.Strong)]
        string EddystoneUrl { get; }

        // -(instancetype)initWithURL:(NSString *)eddystoneURL;
        [Export ("initWithURL:")]
        IntPtr Constructor (string eddystoneURL);
    }

    // @interface ESTEddystoneFilterURLDomain : ESTEddystoneFilter
    [BaseType (typeof (EddystoneFilter), Name = "ESTEddystoneFilterURLDomain")]
    interface EddystoneFilterUrlDomain
    {
        // @property (readonly, nonatomic, strong) NSString * eddystoneURLDomain;
        [Export ("eddystoneURLDomain", ArgumentSemantic.Strong)]
        string EddystoneUrlDomain { get; }

        // -(instancetype)initWithURLDomain:(NSString *)eddystoneURLDomain;
        [Export ("initWithURLDomain:")]
        IntPtr Constructor (string eddystoneURLDomain);
    }

    [BaseType (typeof (NSObject), Name = "ESTDevice")]
    interface Device
    {
        [Obsolete ("Use Identifier instead")]
        [Export ("macAddress", ArgumentSemantic.Strong)]
        string MacAddress { get; }

        [Export ("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        [Export ("peripheralIdentifier", ArgumentSemantic.Strong)]
        NSUuid PeripheralIdentifier { get; }

        [Export ("rssi", ArgumentSemantic.Assign)]
        nint Rssi { get; }

        [Export ("discoverDate", ArgumentSemantic.Strong)]
        NSDate DiscoveryDate { get; }

        [Export ("initWithDeviceIdentifier:peripheralIdentifier:rssi:discoveryDate:")]
        IntPtr Constructor (string identifier, NSUuid peripheralIdentifier, nint rssi, NSDate discoveryDate);
    }

    interface IDeviceConnectableDelegate { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTDeviceConnectableDelegate")]
    interface DeviceConnectableDelegate
    {
        // @optional - (void)estDeviceConnectionDidSucceed:(ESTDeviceConnectable *)device;
        [Export ("estDeviceConnectionDidSucceed:")]
        void ConnectionSucceeded (DeviceConnectable device);

        // @optional - (void)estDevice:(ESTDeviceConnectable *)device didDisconnectWithError:(NSError *)error;
        [Export ("estDevice:didDisconnectWithError:")]
        void Disconnected (DeviceConnectable device, [NullAllowed]NSError error);

        // @optional - (void)estDevice:(ESTDeviceConnectable *)device didFailConnectionWithError:(NSError *)error;
        [Export ("estDevice:didFailConnectionWithError:")]
        void ConnectionFailed (DeviceConnectable device, NSError error);
    }

    // typedef void (^ESTDeviceFirmwareUpdateProgressBlock)(NSInteger);
    delegate void DeviceFirmwareUpdateProgressBlock (nint progress);

    [BaseType (typeof (Device), Name = "ESTDeviceConnectable")]
    interface DeviceConnectable
    {
        // @property (nonatomic, weak) id <ESTDeviceConnectableDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        IDeviceConnectableDelegate Delegate { get; set; }

        // @property (nonatomic, assign, readonly) ESTConnectionStatus connectionStatus;
        [Export ("connectionStatus", ArgumentSemantic.Assign)]
        ConnectionStatus ConnectionStatus { get; }

        // - (void)connect;
        [Export ("connect")]
        void Connect ();

        // - (void)disconnect;
        [Export ("disconnect")]
        void Disconnect ();

        // Removed in 4.x
        // // - (void)readSetting:(id <ESTSettingProtocol>)setting;
        // [Export ("readSetting:")]
        // void ReadSetting (SettingProtocol setting);

        // // - (void)readSettings:(NSArray *)settings;
        // [Export ("readSettings:")]
        // void ReadSettings (SettingProtocol[] settings);

        // // - (void)writeSetting:(id <ESTSettingProtocol>)setting;
        // [Export ("writeSetting:")]
        // void WriteSetting (SettingProtocol setting);

        // // - (void)writeSettings:(NSArray *)settings;
        // [Export ("writeSettings:")]
        // void WriteSettings (SettingProtocol[] settings);

        // // - (void)registerForNotificationSetting:(id <ESTNotificationSettingProtocol>)setting;
        // [Export ("registerForNotificationSetting:")]
        // void RegisterForNotificationSetting (NotificationSettingProtocol setting);

        // // - (void)registerForNotificationSettings:(NSArray *)settings;
        // [Export ("registerForNotificationSettings:")]
        // void RegisterForNotificationSettings (NotificationSettingProtocol[] settings);

        // - (void)checkFirmwareUpdateWithCompletion:(ESTObjectCompletionBlock)completion;
        [Export ("checkFirmwareUpdateWithCompletion:"), Async]
        void CheckFirmwareUpdate (ObjectCompletionBlock completion);

        // - (void)updateFirmwareWithProgress:(ESTDeviceFirmwareUpdateProgressBlock)progress completion:(ESTCompletionBlock)completion;
        [Export ("updateFirmwareWithProgress:completion:"), Async]
        void UpdateFirmware (DeviceFirmwareUpdateProgressBlock progress, CompletionBlock completion);
    }

    [BaseType (typeof (DeviceConnectable), Name = "ESTDeviceNearable")]
    interface DeviceNearable
    {
        // Removed in 4.x
        // [Export ("checkFirmwareUpdateWithCompletion:"), Async]
        // void CheckFirmwareUpdate (ObjectCompletionBlock completion);

        // [Export ("updateFirmwareWithData:progress:completion:"), Async]
        // void UpdateFirmware (NSData data, ProgressBlock progress, CompletionBlock completion);

        // [Export ("updateFirmwareWithProgress:completion:"), Async]
        // void UpdateFirmware (ProgressBlock progress, CompletionBlock completion);


        // @property (nonatomic, strong, readonly) ESTNearableSettingsManager *settings;
        [Export ("settings", ArgumentSemantic.Strong)]
        NearableSettingsManager Settings { get; }
    }

    interface IDeviceNearableSettingProtocol { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTDeviceNearableSettingProtocol")]
    interface DeviceNearableSettingProtocol : DeviceSettingProtocol
    {
        [Export ("size"), Abstract]
        nint Size { get; }
    }

    interface IDeviceSettingProtocol { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTDeviceSettingProtocol")]
    interface DeviceSettingProtocol : SettingProtocol
    {
        // - (uint16_t)registerID;
        [Export ("registerID"), Abstract]
        nuint RegisterID { get; }

        // - (NSData *)getValueData;
        [Export ("getValueData"), Abstract, NullAllowed]
        NSData ValueData { get; }

        // - (void)updateValueWithData:(NSData *)data;
        [Export ("updateValueWithData:"), Abstract]
        void UpdateValue (NSData data);

        // - (BOOL)isAvailableForFirmwareVersion:(NSString *)firmwareVersion;
        [Export ("isAvailableForFirmwareVersion:"), Abstract]
        bool IsAvailableForFirmwareVersion (string firmwareVersion);

        // - (NSError *)validateValue;
        [Export ("validateValue")]
        NSError ValidateValue { get; }

        // - (void)updateValueInSettings:(id)settings;
        [Export ("updateValueInSettings:")]
        void UpdateValue (IntPtr settings);
    }

    interface ISettingProtocol { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTSettingProtocol")]
    interface SettingProtocol
    {
        // - (void)fireSuccessBlockWithData:(NSData *)result;
        [Export ("fireSuccessBlockWithData:"), Abstract]
        void FireSuccessBlock (NSData result);

        // - (void)fireFailureBlockWithError:(NSError *)error;
        [Export ("fireFailureBlockWithError:"), Abstract]
        void FireFailureBlock (NSError error);

        // - (id)getValue;
        [Export ("getValue"), Abstract]
        NSObject Value { get; }
    }

    interface INotificationSettingProtocol { }

    [Model, Protocol]
    [BaseType (typeof (NSObject), Name = "ESTNotificationSettingProtocol")]
    interface NotificationSettingProtocol
    {
        // - (NSInteger)notificationRegisterID;
        [Export ("notificationRegisterID"), Abstract]
        nint NotificationRegisterID { get; }

        // - (void)fireNotificationBlockWithData:(NSData *)result;
        [Export ("fireNotificationBlockWithData:"), Abstract]
        void FireNotificationBlock (NSData result);

        // - (void)updateNotificationValueWithData:(NSData *)data;
        [Export ("updateNotificationValueWithData:"), Abstract]
        void UpdateNotificationValue (NSData data);

        // - (void)updateValueInSettings:(id)settings;
        [Export ("updateValueInSettings:"), Abstract]
        void UpdateValue (NSObject settings);

        // - (BOOL)isAvailableForFirmwareVersion:(NSString *)firmware;
        [Export ("isAvailableForFirmwareVersion:"), Abstract]
        bool IsAvailableForFirmwareVersion (string firmware);
    }

    // @interface ESTAnalyticsEventVO : NSObject <NSCoding, NSCopying>
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


    // @interface ESTAnalyticsManager : NSObject
    [BaseType (typeof (NSObject), Name = "ESTAnalyticsManager")]
    interface AnalyticsManager
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export ("sharedInstance")]
        AnalyticsManager SharedInstance ();

        // -(void)registerAnalyticsEventWithRegion:(CLBeaconRegion *)region type:(ESTAnalyticsEventType)eventType;
        [Export ("registerAnalyticsEventWithRegion:type:")]
        void RegisterAnalyticsEventWithRegion (CLBeaconRegion region, AnalyticsEventType eventType);

        // -(void)sendRegisteredEvents;
        [Export ("sendRegisteredEvents")]
        void SendRegisteredEvents ();

        // +(ESTAnalyticsEventType)getEventTypeForProximity:(CLProximity)proximity;
        [Static]
        [Export ("getEventTypeForProximity:")]
        AnalyticsEventType GetEventTypeForProximity (CLProximity proximity);

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
        void EnableGPSPositioningForAnalytics (bool enable);

        // +(BOOL)isMonitoringAnalyticsEnabled;
        [Static]
        [Export ("isMonitoringAnalyticsEnabled")]
        bool IsMonitoringAnalyticsEnabled { get; }

        // +(BOOL)isRangingAnalyticsEnabled;
        [Static]
        [Export ("isRangingAnalyticsEnabled")]
        bool IsRangingAnalyticsEnabled { get; }
    }

    // @interface ESTBaseVO : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBaseVO")]
    interface BaseVO
    {
        // -(id)objectForKey:(NSString *)aKey inDictionary:(NSDictionary *)dict;
        [Export ("objectForKey:inDictionary:")]
        NSObject ObjectForKey (string aKey, NSDictionary dict);
    }

    // @interface ESTBeacon : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof (NSObject), Name = "ESTBeacon")]
    interface Beacon : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic, strong) NSUUID * _Nonnull proximityUUID;
        [Export ("proximityUUID", ArgumentSemantic.Strong)]
        NSUuid ProximityUUID { get; }

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


    // @interface ESTBeaconBaseVO : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBeaconBaseVO")]
    interface BeaconBaseVO
    {
        // - (id)objectForKey:(NSString *)aKey inDictionary:(NSDictionary *)dict;
        [Export ("objectForKey:inDictionary:")]
        NSObject ObjectForKey (string aKey, NSDictionary dict);
    }


    // @interface ESTBeaconBatteryLifetimesVO : NSObject
    [BaseType (typeof (NSObject), Name = "ESTBeaconBatteryLifetimesVO")]
    interface BeaconBatteryLifetimesVO
    {
        // - (instancetype)initWithLifetimes:(NSDictionary *)lifetimes;
        [Export ("initWithLifetimes:")]
        IntPtr Constructor (NSDictionary lifetimes);

        // - (NSString *)lifetimeForAdvertisingInterval:(int)interval;
        [Export ("lifetimeForAdvertisingInterval:")]
        string GetLifetimeForAdvertisingInverval (int interval);

        // - (NSString *)lifetimeForBroadcastingPower:(int)power;
        [Export ("lifetimeForBroadcastingPower:")]
        string GetLifetimeForBroadcastingPower (int power);

        // - (NSString *)lifetimeForBasicPowerMode:(ESTBeaconPowerSavingMode)basic
        //                                andSmart:(ESTBeaconPowerSavingMode)smart;
        [Export ("lifetimeForBasicPowerMode:andSmart:")]
        string GetLifetimeForPowerMode (BeaconPowerSavingMode basic, BeaconPowerSavingMode smart);

        // - (NSString *)lifetimeForBroadcastingScheme:(ESTBroadcastingScheme)scheme;
        [Export ("lifetimeForBroadcastingScheme:")]
        string GetLifetimeForBroadcastingScheme (BroadcastingScheme scheme);

        // - (BOOL)shouldDisplayAlertForAdvertisingInterval:(int)interval;
        [Export ("shouldDisplayAlertForAdvertisingInterval:")]
        bool ShouldDisplayAlertForAdvertisingInterval (int interval);

        // - (BOOL)shouldDisplayAlertForBroadcastingPower:(int)power;
        [Export ("shouldDisplayAlertForBroadcastingPower:")]
        bool ShouldDisplayAlertForBroadcastingPower (int power);

        // - (BOOL)shouldDisplayAlertForBasicPowerMode:(ESTBeaconPowerSavingMode)basic
        //                                    andSmart:(ESTBeaconPowerSavingMode)smart;
        [Export ("shouldDisplayAlertForBasicPowerMode:andSmart:")]
        bool ShouldDisplayAlertForPowerMode (BeaconPowerSavingMode basic, BeaconPowerSavingMode smart);
    }


    // @interface ESTBeaconOperationConnectivityInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
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

    // @interface ESTBeaconOperationSensorsAmbientLight : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsAmbientLight")]
    interface BeaconOperationSensorsAmbientLight : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsAmbientLightCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationSensorsAmbientLight ReadOperation (SettingSensorsAmbientLightCompletionBlock completion);
    }

    // @interface ESTBeaconOperationSensorsMotionNotificationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationSensorsTemperature")]
    interface BeaconOperationSensorsTemperature : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingSensorsTemperatureCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationSensorsTemperature ReadOperation (SettingSensorsTemperatureCompletionBlock completion);
    }

    // @interface ESTBeaconOperationDeviceInfoApplicationVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoApplicationVersion")]
    interface BeaconOperationDeviceInfoApplicationVersion : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoApplicationVersionCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationDeviceInfoApplicationVersion ReadOperation (SettingDeviceInfoApplicationVersionCompletionBlock completion);
    }

    // @interface ESTBeaconOperationDeviceInfoBootloaderVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoBootloaderVersion")]
    interface BeaconOperationDeviceInfoBootloaderVersion : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoBootloaderVersionCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationDeviceInfoBootloaderVersion ReadOperation (SettingDeviceInfoBootloaderVersionCompletionBlock completion);
    }

    // @interface ESTBeaconOperationDeviceInfoHardwareVersion : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoHardwareVersion")]
    interface BeaconOperationDeviceInfoHardwareVersion : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoHardwareVersionCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationDeviceInfoHardwareVersion ReadOperation (SettingDeviceInfoHardwareVersionCompletionBlock completion);
    }

    // @interface ESTBeaconOperationDeviceInfoUTCTime : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationDeviceInfoUTCTime")]
    interface BeaconOperationDeviceInfoUTCTime : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationDeviceInfoUTCTime ReadOperation (SettingDeviceInfoUTCTimeCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingDeviceInfoUTCTime * _Nonnull)setting completion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationDeviceInfoUTCTime WriteOperation (SettingDeviceInfoUTCTime setting, SettingDeviceInfoUTCTimeCompletionBlock completion);
    }


    // @interface ESTBeaconOperationEddystoneUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDEnable")]
    interface BeaconOperationEddystoneUIDEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneUIDEnable ReadOperation (SettingEddystoneUIDEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDEnable * _Nonnull)setting completion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneUIDEnable WriteOperation (SettingEddystoneUIDEnable setting, SettingEddystoneUIDEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneUIDInstance : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDInstance")]
    interface BeaconOperationEddystoneUIDInstance : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneUIDInstance ReadOperation (SettingEddystoneUIDInstanceCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDInstance * _Nonnull)setting completion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneUIDInstance WriteOperation (SettingEddystoneUIDInstance setting, SettingEddystoneUIDInstanceCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneUIDNamespace : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDNamespace")]
    interface BeaconOperationEddystoneUIDNamespace : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneUIDNamespace ReadOperation (SettingEddystoneUIDNamespaceCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDNamespace * _Nonnull)setting completion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneUIDNamespace WriteOperation (SettingEddystoneUIDNamespace setting, SettingEddystoneUIDNamespaceCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneUIDInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDInterval")]
    interface BeaconOperationEddystoneUIDInterval : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneUIDInterval ReadOperation (SettingEddystoneUIDIntervalCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDInterval * _Nonnull)setting completion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneUIDInterval WriteOperation (SettingEddystoneUIDInterval setting, SettingEddystoneUIDIntervalCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneUIDPower : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneUIDPower")]
    interface BeaconOperationEddystoneUIDPower : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneUIDPower ReadOperation (SettingEddystoneUIDPowerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneUIDPower * _Nonnull)setting completion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneUIDPower WriteOperation (SettingEddystoneUIDPower setting, SettingEddystoneUIDPowerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneURLEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLEnable")]
    interface BeaconOperationEddystoneURLEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneURLEnable ReadOperation (SettingEddystoneURLEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLEnable * _Nonnull)setting completion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneURLEnable WriteOperation (SettingEddystoneURLEnable setting, SettingEddystoneURLEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneURLData : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLData")]
    interface BeaconOperationEddystoneURLData : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneURLData ReadOperation (SettingEddystoneURLDataCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLData * _Nonnull)setting completion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneURLData WriteOperation (SettingEddystoneURLData setting, SettingEddystoneURLDataCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneURLInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLInterval")]
    interface BeaconOperationEddystoneURLInterval : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneURLInterval ReadOperation (SettingEddystoneURLIntervalCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLInterval * _Nonnull)setting completion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneURLInterval WriteOperation (SettingEddystoneURLInterval setting, SettingEddystoneURLIntervalCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneURLPower : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneURLPower")]
    interface BeaconOperationEddystoneURLPower : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneURLPower ReadOperation (SettingEddystoneURLPowerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneURLPower * _Nonnull)setting completion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneURLPower WriteOperation (SettingEddystoneURLPower setting, SettingEddystoneURLPowerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneTLMEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMEnable")]
    interface BeaconOperationEddystoneTLMEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneTLMEnable ReadOperation (SettingEddystoneTLMEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMEnable * _Nonnull)setting completion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneTLMEnable WriteOperation (SettingEddystoneTLMEnable setting, SettingEddystoneTLMEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneTLMInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMInterval")]
    interface BeaconOperationEddystoneTLMInterval : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneTLMInterval ReadOperation (SettingEddystoneTLMIntervalCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMInterval * _Nonnull)setting completion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneTLMInterval WriteOperation (SettingEddystoneTLMInterval setting, SettingEddystoneTLMIntervalCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneTLMPower : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneTLMPower")]
    interface BeaconOperationEddystoneTLMPower : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneTLMPower ReadOperation (SettingEddystoneTLMPowerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneTLMPower * _Nonnull)setting completion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneTLMPower WriteOperation (SettingEddystoneTLMPower setting, SettingEddystoneTLMPowerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneEIDInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDInterval")]
    interface BeaconOperationEddystoneEIDInterval : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneEIDInterval ReadOperation (SettingEddystoneEIDIntervalCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDInterval * _Nonnull)setting completion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneEIDInterval WriteOperation (SettingEddystoneEIDInterval setting, SettingEddystoneEIDIntervalCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneEIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDEnable")]
    interface BeaconOperationEddystoneEIDEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneEIDEnable ReadOperation (SettingEddystoneEIDEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDEnable * _Nonnull)setting completion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneEIDEnable WriteOperation (SettingEddystoneEIDEnable setting, SettingEddystoneEIDEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEddystoneEIDPower : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEddystoneEIDPower")]
    interface BeaconOperationEddystoneEIDPower : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEddystoneEIDPower ReadOperation (SettingEddystoneEIDPowerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEddystoneEIDPower * _Nonnull)setting completion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEddystoneEIDPower WriteOperation (SettingEddystoneEIDPower setting, SettingEddystoneEIDPowerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEstimoteLocationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMEnable")]
    interface BeaconOperationEstimoteTLMEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEstimoteTLMEnable ReadOperation (SettingEstimoteTLMEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMEnable * _Nonnull)setting completion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEstimoteTLMEnable WriteOperation (SettingEstimoteTLMEnable setting, SettingEstimoteTLMEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEstimoteTLMInterval : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMInterval")]
    interface BeaconOperationEstimoteTLMInterval : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEstimoteTLMInterval ReadOperation (SettingEstimoteTLMIntervalCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMInterval * _Nonnull)setting completion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEstimoteTLMInterval WriteOperation (SettingEstimoteTLMInterval setting, SettingEstimoteTLMIntervalCompletionBlock completion);
    }

    // @interface ESTBeaconOperationEstimoteTLMPower : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationEstimoteTLMPower")]
    interface BeaconOperationEstimoteTLMPower : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationEstimoteTLMPower ReadOperation (SettingEstimoteTLMPowerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingEstimoteTLMPower * _Nonnull)setting completion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationEstimoteTLMPower WriteOperation (SettingEstimoteTLMPower setting, SettingEstimoteTLMPowerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationGPIONotificationEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIONotificationEnable")]
    interface BeaconOperationGPIONotificationEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationGPIONotificationEnable ReadOperation (SettingGPIONotificationEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIONotificationEnable * _Nonnull)setting completion:(ESTSettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationGPIONotificationEnable WriteOperation (SettingGPIONotificationEnable setting, SettingGPIONotificationEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationGPIOConfigPort0 : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOConfigPort0")]
    interface BeaconOperationGPIOConfigPort0 : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationGPIOConfigPort0 ReadOperation (SettingGPIOConfigPort0CompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIOConfigPort0 * _Nonnull)setting completion:(ESTSettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationGPIOConfigPort0 WriteOperation (SettingGPIOConfigPort0 setting, SettingGPIOConfigPort0CompletionBlock completion);
    }

    // @interface ESTBeaconOperationGPIOConfigPort1 : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOConfigPort1")]
    interface BeaconOperationGPIOConfigPort1 : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationGPIOConfigPort1 ReadOperation (SettingGPIOConfigPort1CompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingGPIOConfigPort1 * _Nonnull)setting completion:(ESTSettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationGPIOConfigPort1 WriteOperation (SettingGPIOConfigPort1 setting, SettingGPIOConfigPort1CompletionBlock completion);
    }


    // @interface ESTBeaconOperationGPIOPortsData : ESTSettingOperation<ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationGPIOPortsData")]
    interface BeaconOperationGPIOPortsData : BeaconOperationProtocol
    {
        // + (instancetype)readOperationWithCompletion:(ESTSettingGPIOPortsDataCompletionBlock)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationGPIOPortsData ReadOperation (SettingGPIOPortsDataCompletionBlock completion);

        // + (instancetype)writeOperationWithSetting:(ESTSettingGPIOPortsData*)setting completion:(ESTSettingGPIOPortsDataCompletionBlock)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationGPIOConfigPort1 WriteOperation (SettingGPIOConfigPort1 setting, SettingGPIOConfigPort1CompletionBlock completion);
    }


    // @interface ESTBeaconOperationIBeaconEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconProximityUUID")]
    interface BeaconOperationIBeaconProximityUUID : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationIBeaconProximityUUID ReadOperation (SettingIBeaconProximityUUIDCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconProximityUUID * _Nonnull)setting completion:(ESTSettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationIBeaconProximityUUID WriteOperation (SettingIBeaconProximityUUID setting, SettingIBeaconProximityUUIDCompletionBlock completion);
    }

    // @interface ESTBeaconOperationIBeaconSecureUUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconSecureUUIDEnable")]
    interface BeaconOperationIBeaconSecureUUIDEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationIBeaconSecureUUIDEnable ReadOperation (SettingIBeaconSecureUUIDEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconSecureUUIDEnable * _Nonnull)setting completion:(ESTSettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationIBeaconSecureUUIDEnable WriteOperation (SettingIBeaconSecureUUIDEnable setting, SettingIBeaconSecureUUIDEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationIBeaconSecureUUIDPeriodScaler : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconSecureUUIDPeriodScaler")]
    interface BeaconOperationIBeaconSecureUUIDPeriodScaler : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationIBeaconSecureUUIDPeriodScaler ReadOperation (SettingIBeaconSecureUUIDPeriodScalerCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconSecureUUIDPeriodScaler * _Nonnull)setting completion:(ESTSettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationIBeaconSecureUUIDPeriodScaler WriteOperation (SettingIBeaconSecureUUIDPeriodScaler setting, SettingIBeaconSecureUUIDPeriodScalerCompletionBlock completion);
    }

    // @interface ESTBeaconOperationIBeaconMotionUUID : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMotionUUID")]
    interface BeaconOperationIBeaconMotionUUID : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMotionUUIDCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationIBeaconMotionUUID ReadOperation (SettingIBeaconMotionUUIDCompletionBlock completion);
    }

    // @interface ESTBeaconOperationIBeaconMotionUUIDEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationIBeaconMotionUUIDEnable")]
    interface BeaconOperationIBeaconMotionUUIDEnable : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationIBeaconMotionUUIDEnable ReadOperation (SettingIBeaconMotionUUIDEnableCompletionBlock completion);

        // +(instancetype _Nonnull)writeOperationWithSetting:(ESTSettingIBeaconMotionUUIDEnable * _Nonnull)setting completion:(ESTSettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        BeaconOperationIBeaconMotionUUIDEnable WriteOperation (SettingIBeaconMotionUUIDEnable setting, SettingIBeaconMotionUUIDEnableCompletionBlock completion);
    }

    // @interface ESTBeaconOperationPowerSmartPowerModeEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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

    // @interface ESTBeaconOperationPowerMotionOnlyBroadcastingEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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

    // @interface ESTBeaconOperationPowerScheduledAdvertisingEnable : ESTSettingOperation <ESTBeaconOperationProtocol>
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
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerBatteryPercentage")]
    interface BeaconOperationPowerBatteryPercentage : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryPercentageCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationPowerBatteryPercentage ReadOperation (SettingPowerBatteryPercentageCompletionBlock completion);
    }

    // @interface ESTBeaconOperationPowerBatteryVoltage : ESTSettingOperation <ESTBeaconOperationProtocol>
    [BaseType (typeof (SettingOperation), Name = "ESTBeaconOperationPowerBatteryVoltage")]
    interface BeaconOperationPowerBatteryVoltage : BeaconOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryVoltageCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        BeaconOperationPowerBatteryVoltage ReadOperation (SettingPowerBatteryVoltageCompletionBlock completion);
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
        ushort RegisterID { get; }

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
        BeaconOperationProtocol [] AssociatedOperations { get; }
    }

    // typedef void (^ESTDeviceSettingsManagerSyncCompletionBlock)(NSError * _Nullable);
    delegate void DeviceSettingsManagerSyncCompletionBlock ([NullAllowed] NSError error);

    // typedef void (^ESTDeviceSettingsManagerOperationsCompletionBlock)(NSError * _Nullable);
    delegate void DeviceSettingsManagerOperationsCompletionBlock ([NullAllowed] NSError error);

    // @interface ESTBeaconSettingsManager : NSObject
    [BaseType (typeof (NSObject), Name="ESTBeaconSettingsManager")]
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
        SettingsEstimoteTLM EstimoteTLM { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsEddystoneUID * _Nonnull eddystoneUID;
        [Export ("eddystoneUID", ArgumentSemantic.Strong)]
        SettingsEddystoneUID EddystoneUID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsEddystoneURL * _Nonnull eddystoneURL;
        [Export ("eddystoneURL", ArgumentSemantic.Strong)]
        SettingsEddystoneURL EddystoneURL { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsEddystoneTLM * _Nonnull eddystoneTLM;
        [Export ("eddystoneTLM", ArgumentSemantic.Strong)]
        SettingsEddystoneTLM EddystoneTLM { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsEddystoneEID * _Nonnull eddystoneEID;
        [Export ("eddystoneEID", ArgumentSemantic.Strong)]
        SettingsEddystoneEID EddystoneEID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsGPIO * _Nonnull GPIO;
        [Export ("GPIO", ArgumentSemantic.Strong)]
        SettingsGPIO GPIO { get; }

        // @property (readonly, nonatomic, strong) ESTSettingsSensors * _Nonnull sensors;
        [Export ("sensors", ArgumentSemantic.Strong)]
        SettingsSensors Sensors { get; }

        // @property (readonly, nonatomic) ESTSettingsEddystoneConfigurationService * _Nonnull eddystoneConfigurationService;
        [Export ("eddystoneConfigurationService")]
        SettingsEddystoneConfigurationService EddystoneConfigurationService { get; }

        // -(void)performOperation:(id<ESTBeaconOperationProtocol> _Nonnull)operation;
        [Export ("performOperation:")]
        void PerformOperation (BeaconOperationProtocol operation);

        // -(void)performOperations:(id<ESTBeaconOperationProtocol> _Nonnull)firstOperation, ...;
        [Internal]
        [Export ("performOperations:", IsVariadic = true)]
        void PerformOperations (BeaconOperationProtocol firstOperation, IntPtr varArgs);

        // -(void)performOperationsFromArray:(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)operationsArray;
        [Export ("performOperationsFromArray:")]
        void PerformOperations (BeaconOperationProtocol [] operationsArray);

        // -(void)performOperationsFromArray:(NSArray<id<ESTBeaconOperationProtocol>> * _Nonnull)operationsArray completion:(ESTDeviceSettingsManagerOperationsCompletionBlock _Nullable)completion;
        [Export ("performOperationsFromArray:completion:"), Async]
        void PerformOperations (BeaconOperationProtocol [] operationsArray, [NullAllowed] DeviceSettingsManagerOperationsCompletionBlock completion);

        // -(void)registerNotification:(id<ESTDeviceNotificationProtocol> _Nonnull)notification;
        [Export ("registerNotification:")]
        void RegisterNotification (DeviceNotificationProtocol notification);

        // -(void)unregisterAllNotifications;
        [Export ("unregisterAllNotifications")]
        void UnregisterAllNotifications ();
    }

    // @interface Internal (ESTBeaconSettingsManager)
    [Category]
    //[BaseType (typeof (BeaconSettingsManager), Name="ESTBeaconSettingsManager_Internal")]
    [BaseType (typeof (BeaconSettingsManager))]
    interface BeaconSettingsManagerExtensions
    {
        //// -(instancetype _Nonnull)initWithDevice:(ESTDeviceLocationBeacon * _Nonnull)device peripheral:(ESTPeripheralTypeUtility * _Nonnull)peripheral;
        //[Export ("initWithDevice:peripheral:")]
        //IntPtr Constructor (DeviceLocationBeacon device, PeripheralTypeUtility peripheral);

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


    // @interface ESTBeaconRecentConfig : ESTBeaconBaseVO
    [BaseType (typeof (BeaconBaseVO), Name="ESTBeaconRecentConfig")]
    interface BeaconRecentConfig
    {
        // @property (nonatomic, readonly) NSString* proximityUUID;
        [Export ("proximityUUID")]
        string ProximityUUID { get; }

        // @property (nonatomic, readonly) NSNumber* major;
        [Export ("major")]
        NSNumber Major { get; }

        // @property (nonatomic, readonly) NSNumber* minor;
        [Export ("minor")]
        NSNumber Minor { get; }

        // @property (nonatomic, readonly) NSNumber* security;
        [Export ("security")]
        NSNumber Security { get; }

        // @property (nonatomic, readonly) NSNumber* advInterval;
        [Export ("advInterval")]
        NSNumber AdvInterval { get; }

        // @property (nonatomic, readonly) NSNumber* power;
        [Export ("power")]
        NSNumber Power { get; }

        // @property (nonatomic, readonly) NSNumber* basicPowerMode;
        [Export ("basicPowerMode")]
        NSNumber BasicPowerMode { get; }

        // @property (nonatomic, readonly) NSNumber* smartPowerMode;
        [Export ("smartPowerMode")]
        NSNumber SmartPowerMode { get; }

        // @property (nonatomic, readonly) NSString* firmware;
        [Export ("firmware")]
        string Firmware { get; }

        // @property (nonatomic, readonly) NSNumber* broadcastingScheme;
        [Export ("broadcastingScheme")]
        NSNumber BroadcastingScheme { get; }

        // @property (nonatomic, readonly) NSString* formattedAddress;
        [Export ("FormattedAddress")]
        string FormattedAddress { get; }

        // @property (nonatomic, readonly) BOOL geoLocationDeleted;
        [Export ("geoLocationDeleted")]
        bool GeoLocationDeleted { get; }

        // @property (nonatomic, readonly) NSNumber* conditionalBroadcasting;
        [Export ("conditionalBroadcasting")]
        NSNumber ConditionalBroadcasting { get; }

        // @property (nonatomic, readonly) NSString* zone;
        [Export ("zone")]
        string Zone { get; }

        // @property (nonatomic, readonly) NSNumber* motionDetection;
        [Export ("motionDetection")]
        NSNumber MotionDetection { get; }

        // - (instancetype)initWithCloudData:(NSDictionary*)data;
        [Export("initWithCloudData:")]
        IntPtr Constructor (NSDictionary data);
    }


    // @interface ESTBeaconRecentUpdateInfo : ESTBeaconBaseVO
    [BaseType (typeof (BeaconBaseVO), Name = "ESTBeaconRecentUpdateInfo")]
    interface BeaconRecentUpdateInfo
    {
        // @property (nonatomic, readonly) NSString* macAddress;
        [Export ("macAddress")]
        string MacAddress { get; }

        // @property (nonatomic, readonly) ESTBeaconRecentConfig* config;
        [Export ("config")]
        BeaconRecentConfig Config { get; }

        // @property (nonatomic, readonly) NSDate* createdAt;
        [Export ("createdAt")]
        NSDate CreatedAt { get; }

        // @property (nonatomic, readonly) NSDate* syncedAt;
        [Export ("syncedAt")]
        NSDate SyncedAt { get; }

        // - (instancetype)initWithCloudData:(NSDictionary*)data andMacAddress:(NSString*)mac;
        [Export ("initWithCloudData:andMacAddress:")]
        IntPtr Constructor (NSDictionary data, string mac);
    }

    interface ICloudOperationProtocol { }

    // @protocol ESTCloudOperationProtocol <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface CloudOperationProtocol
    {
        // @required -(Class _Nonnull)settingClass;
        [Abstract]
        [Export ("settingClass")]
        Class SettingClass { get; }

        // @required -(void)updateSettingWithSetting:(ESTSettingBase * _Nonnull)setting;
        [Abstract]
        [Export ("updateSettingWithSetting:")]
        void UpdateSetting (SettingBase setting);
    }

    // @interface ESTCloudOperationPowerBatteryLifetime : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationPowerBatteryLifetime")]
    interface CloudOperationPowerBatteryLifetime : BeaconOperationProtocol, CloudOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingPowerBatteryLifetimeCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        CloudOperationPowerBatteryLifetime ReadOperation (SettingPowerBatteryLifetimeCompletionBlock completion);
    }

    // @interface ESTCloudOperationDeviceInfoFirmwareVersion : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoFirmwareVersion")]
    interface CloudOperationDeviceInfoFirmwareVersion : BeaconOperationProtocol, CloudOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoFirmwareVersionCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        CloudOperationDeviceInfoFirmwareVersion ReadOperation (SettingDeviceInfoFirmwareVersionCompletionBlock completion);
    }

    // @interface ESTCloudOperationDeviceInfoTags : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoTags")]
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
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoGeoLocation")]
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
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoName")]
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
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoColor")]
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
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationDeviceInfoIndoorLocationIdentifier")]
    interface CloudOperationDeviceInfoIndoorLocationIdentifier : BeaconOperationProtocol, CloudOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoIndoorLocationIdentifierCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        CloudOperationDeviceInfoIndoorLocationIdentifier ReadOperation (SettingDeviceInfoIndoorLocationIdentifierCompletionBlock completion);
    }

    // @interface ESTCloudOperationIBeaconNonStrictMode : ESTSettingOperation <ESTBeaconOperationProtocol, ESTCloudOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTCloudOperationIBeaconNonStrictMode")]
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

    // @interface ESTDeviceDetails : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceDetails")]
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

    interface IDeviceFilter { }
    // @protocol ESTDeviceFilter <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTDeviceFilter")]
    interface DeviceFilter
    {
        // @required @property (readonly, nonatomic, strong) NSPredicate * _Nonnull devicesPredicate;
        [Abstract]
        [Export ("devicesPredicate", ArgumentSemantic.Strong)]
        NSPredicate DevicesPredicate { get; }

        // @required -(NSArray<Class> * _Nonnull)getScanInfoClasses;
        [Abstract]
        [Export ("getScanInfoClasses")]
        Class [] ScanInfoClasses { get; }
    }

    // @interface ESTDeviceFilterBeaconV1 : NSObject <ESTDeviceFilter>
    [BaseType (typeof (NSObject), Name="ESTDeviceFilterBeaconV1")]
    interface DeviceFilterBeaconV1 : DeviceFilter
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("initWithIdentifier:")]
        IntPtr Constructor (string identifier);
    }

    // @interface ESTDeviceFilterLocationBeacon : NSObject <ESTDeviceFilter>
    [BaseType (typeof (NSObject), Name="ESTDeviceFilterLocationBeacon")]
    interface DeviceFilterLocationBeacon : DeviceFilter
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("initWithIdentifier:")]
        IntPtr Constructor (string identifier);
    }

    // @interface ESTDeviceFilterNearable : NSObject <ESTDeviceFilter>
    [BaseType (typeof (NSObject), Name="ESTDeviceFilterNearable")]
    interface DeviceFilterNearable : DeviceFilter
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("initWithIdentifier:")]
        IntPtr Constructor (string identifier);
    }

    // @interface ESTDeviceGeoLocation : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceGeoLocation")]
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
    }

    //// @interface Internal (ESTDeviceGeoLocation)
    //[Category]
    //[BaseType (typeof (DeviceGeoLocation))]
    //interface DeviceGeoLocationExtensions
    //{
    //    // -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
    //    [Export ("initWithCloudDictionary:")]
    //    IntPtr Constructor (NSDictionary dictionary);
    //}

    // @interface ESTDeviceIndoorLocation : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceIndoorLocation")]
    interface DeviceIndoorLocation
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull identifier;
        [Export ("identifier", ArgumentSemantic.Strong)]
        string Identifier { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull name;
        [Export ("name", ArgumentSemantic.Strong)]
        string Name { get; }
    }

    //// @interface Internal (ESTDeviceIndoorLocation)
    //[Category]
    //[BaseType (typeof (DeviceIndoorLocation))]
    //interface DeviceIndoorLocationExtensions
    //{
    //    // -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
    //    [Export ("initWithCloudDictionary:")]
    //    IntPtr Constructor (NSDictionary dictionary);
    //}

    // @interface ESTDeviceLocationBeacon : ESTDeviceConnectable
    [BaseType (typeof (DeviceConnectable), Name="ESTDeviceLocationBeacon")]
    interface DeviceLocationBeacon
    {
        // @property (readonly, nonatomic, strong) ESTBeaconSettingsManager * _Nullable settings;
        [NullAllowed, Export ("settings", ArgumentSemantic.Strong)]
        BeaconSettingsManager Settings { get; }
    }

    interface IDeviceManagerDelegate { }
    // @protocol ESTDeviceManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTDeviceManagerDelegate")]
    interface DeviceManagerDelegate
    {
        // @optional -(void)deviceManager:(ESTDeviceManager * _Nonnull)manager didDiscoverDevices:(NSArray<ESTDevice *> * _Nonnull)devices;
        [Export ("deviceManager:didDiscoverDevices:")]
        void DidDiscoverDevices (DeviceManager manager, Device [] devices);

        // @optional -(void)deviceManagerDidFailDiscovery:(ESTDeviceManager * _Nonnull)manager;
        [Export ("deviceManagerDidFailDiscovery:")]
        void DidFailDiscovery (DeviceManager manager);
    }

    // @interface ESTDeviceManager : NSObject
    [BaseType (typeof (NSObject), Name="ESTDeviceManager")]
    interface DeviceManager
    {
        // @property (readonly, assign, nonatomic) BOOL isScanning;
        [Export ("isScanning")]
        bool IsScanning { get; }

        [Wrap ("WeakDelegate")]
        [NullAllowed]
        DeviceManagerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ESTDeviceManagerDelegate> _Nullable delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // -(void)startDeviceDiscoveryWithFilter:(id<ESTDeviceFilter> _Nonnull)filter;
        [Export ("startDeviceDiscoveryWithFilter:")]
        void StartDeviceDiscovery (DeviceFilter filter);

        // -(void)stopDeviceDiscovery;
        [Export ("stopDeviceDiscovery")]
        void StopDeviceDiscovery ();

        // -(void)registerForTelemetryNotifications:(NSArray<ESTTelemetryNotificationProtocol> * _Nonnull)infos;
        [Export ("registerForTelemetryNotifications:")]
        void RegisterForTelemetryNotifications (TelemetryNotificationProtocol [] infos);

        // -(void)registerForTelemetryNotification:(id<ESTTelemetryNotificationProtocol> _Nonnull)info;
        [Export ("registerForTelemetryNotification:")]
        void RegisterForTelemetryNotification (TelemetryNotificationProtocol info);

        // -(void)unregisterForTelemetryNotification:(id<ESTTelemetryNotificationProtocol> _Nonnull)info;
        [Export ("unregisterForTelemetryNotification:")]
        void UnregisterForTelemetryNotification (TelemetryNotificationProtocol info);
    }

    interface IDeviceNotificationProtocol { }

    // @protocol ESTDeviceNotificationProtocol <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTDeviceNotificationProtocol")]
    interface DeviceNotificationProtocol
    {
        // @required -(uint16_t)registerID;
        [Abstract]
        [Export ("registerID")]
        ushort RegisterID { get; }

        // @required -(void)fireHandlerWithData:(NSData * _Nonnull)data;
        [Abstract]
        [Export ("fireHandlerWithData:")]
        void FireHandler (NSData data);

        // @required -(NSString * _Nonnull)supportedFirmwareVersion;
        [Abstract]
        [Export ("supportedFirmwareVersion")]
        string SupportedFirmwareVersion { get; }
    }

    // @interface ESTDeviceSchedule : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceSchedule")]
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
    }

    // @interface ESTDeviceSettings : ESTBaseVO <NSCopying>
    [BaseType (typeof (BaseVO), Name="ESTDeviceSettings")]
    interface DeviceSettings : INSCopying
    {
        // @property (readonly, nonatomic, strong) ESTDeviceSettingsGeneral * _Nonnull general;
        [Export ("general", ArgumentSemantic.Strong)]
        DeviceSettingsGeneral General { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiser *> * _Nonnull connectivity;
        [Export ("connectivity", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiser [] Connectivity { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserIBeacon *> * _Nonnull iBeacon;
        [Export ("iBeacon", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserIBeacon [] IBeacon { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneUID *> * _Nonnull eddystoneUID;
        [Export ("eddystoneUID", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEddystoneUID [] EddystoneUID { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneURL *> * _Nonnull eddystoneURL;
        [Export ("eddystoneURL", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEddystoneURL [] EddystoneURL { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneTLM *> * _Nonnull eddystoneTLM;
        [Export ("eddystoneTLM", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEddystoneTLM [] EddystoneTLM { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEddystoneEID *> * _Nonnull eddystoneEID;
        [Export ("eddystoneEID", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEddystoneEID [] EddystoneEID { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEstimoteLocation *> * _Nonnull estimoteLocation;
        [Export ("estimoteLocation", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEstimoteLocation [] EstimoteLocation { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTDeviceSettingsAdvertiserEstimoteTLM *> * _Nonnull estimoteTLM;
        [Export ("estimoteTLM", ArgumentSemantic.Strong)]
        DeviceSettingsAdvertiserEstimoteTLM [] EstimoteTLM { get; }

        // -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
        [Export ("initWithCloudDictionary:")]
        IntPtr Constructor (NSDictionary dictionary);

        // -(ESTDeviceSettings * _Nonnull)settingsUpdatedWithDeviceSettings:(ESTDeviceSettings * _Nonnull)deviceSettings;
        [Export ("settingsUpdatedWithDeviceSettings:")]
        DeviceSettings SettingsUpdated (DeviceSettings deviceSettings);
    }

    // @interface ESTDeviceSettingsAdvertiser : ESTBaseVO <NSCopying>
    [BaseType (typeof (BaseVO), Name="ESTDeviceSettingsAdvertiser")]
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
        NSNumber PowerInDBm { get; }

        // @property (readonly, nonatomic, strong) NSNumber * _Nonnull intervalInSeconds;
        [Export ("intervalInSeconds", ArgumentSemantic.Strong)]
        NSNumber IntervalInSeconds { get; }

        // -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
        [Export ("initWithCloudDictionary:")]
        IntPtr Constructor (NSDictionary dictionary);

        // -(void)updateWithAdvertiserSettings:(ESTDeviceSettingsAdvertiser * _Nonnull)advertiserSettings;
        [Export ("updateWithAdvertiserSettings:")]
        void UpdateWithAdvertiserSettings (DeviceSettingsAdvertiser advertiserSettings);
    }

    // @interface ESTDeviceSettingsAdvertiserIBeacon : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserIBeacon")]
    interface DeviceSettingsAdvertiserIBeacon
    {
        // @property (readonly, nonatomic, strong) NSUUID * _Nonnull proximityUUID;
        [Export ("proximityUUID", ArgumentSemantic.Strong)]
        NSUuid ProximityUUID { get; }

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
        NSNumber MotionUUIDEnabled { get; }
    }

    // @interface ESTDeviceSettingsAdvertiserEddystoneUID : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserEddystoneUID")]
    interface DeviceSettingsAdvertiserEddystoneUID
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull namespaceID;
        [Export ("namespaceID", ArgumentSemantic.Strong)]
        string NamespaceID { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull instanceID;
        [Export ("instanceID", ArgumentSemantic.Strong)]
        string InstanceID { get; }
    }

    // @interface ESTDeviceSettingsAdvertiserEddystoneURL : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserEddystoneURL")]
    interface DeviceSettingsAdvertiserEddystoneURL
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull url;
        [Export ("url", ArgumentSemantic.Strong)]
        string Url { get; }
    }

    // @interface ESTDeviceSettingsAdvertiserEddystoneTLM : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserEddystoneTLM")]
    interface DeviceSettingsAdvertiserEddystoneTLM
    {
    }

    // @interface ESTDeviceSettingsAdvertiserEddystoneEID : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserEddystoneEID")]
    interface DeviceSettingsAdvertiserEddystoneEID
    {
        // @property (readonly, nonatomic, strong) NSString * identityKey;
        [Export ("identityKey", ArgumentSemantic.Strong)]
        string IdentityKey { get; }

        // @property (readonly, nonatomic, strong) NSNumber * rotationScaler;
        [Export ("rotationScaler", ArgumentSemantic.Strong)]
        NSNumber RotationScaler { get; }

        // @property (readonly, nonatomic, strong) NSString * registeredNamespaceID;
        [Export ("registeredNamespaceID", ArgumentSemantic.Strong)]
        string RegisteredNamespaceID { get; }

        // @property (readonly, nonatomic, strong) NSString * registeredInstanceID;
        [Export ("registeredInstanceID", ArgumentSemantic.Strong)]
        string RegisteredInstanceID { get; }
    }

    // @interface ESTDeviceSettingsAdvertiserEstimoteTLM : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name="ESTDeviceSettingsAdvertiserEstimoteTLM")]
    interface DeviceSettingsAdvertiserEstimoteTLM
    {
    }

    // @interface ESTDeviceSettingsAdvertiserEstimoteLocation : ESTDeviceSettingsAdvertiser
    [BaseType (typeof (DeviceSettingsAdvertiser), Name = "ESTDeviceSettingsAdvertiserEstimoteLocation")]
    interface DeviceSettingsAdvertiserEstimoteLocation
    {
    }

    // @interface ESTDeviceSettingsCollection : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name="ESTDeviceSettingsCollection")]
    interface DeviceSettingsCollection : INSCopying
    {
        // -(instancetype _Nonnull)initWithSettingsArray:(NSArray<ESTSettingBase *> * _Nonnull)settingsArray;
        [Export ("initWithSettingsArray:")]
        IntPtr Constructor (SettingBase [] settingsArray);

        // -(void)addOrReplaceSetting:(ESTSettingBase * _Nonnull)setting;
        [Export ("addOrReplaceSetting:")]
        void AddOrReplaceSetting (SettingBase setting);

        // -(void)addOrReplaceSettings:(NSArray<ESTSettingBase *> * _Nonnull)settings;
        [Export ("addOrReplaceSettings:")]
        void AddOrReplaceSettings (SettingBase [] settings);

        // -(id _Nonnull)getSettingForClass:(Class _Nonnull)targetedClass;
        [Export ("getSettingForClass:")]
        NSObject GetSettingForClass (Class targetedClass);

        // -(NSArray * _Nonnull)getSettings;
        [Export ("getSettings")]
        SettingBase [] Settings { get; }
    }

    // @interface ESTDeviceSettingsGeneral : ESTBaseVO <NSCopying>
    [BaseType (typeof (BaseVO), Name="ESTDeviceSettingsGeneral")]
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

        // @property (readonly, nonatomic, strong) NSNumber * _Nonnull conditionalBroadcasting;
        [Export ("conditionalBroadcasting", ArgumentSemantic.Strong)]
        NSNumber ConditionalBroadcasting { get; }

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

        // @property (nonatomic, readonly) NSNumber* motionOnlyEnabled;
        [Export ("motionOnlyEnabled")]
        NSNumber MotionOnlyEnabled { get; }

        // -(instancetype _Nonnull)initWithCloudDictionary:(NSDictionary * _Nonnull)dictionary;
        [Export ("initWithCloudDictionary:")]
        IntPtr Constructor (NSDictionary dictionary);

        // -(void)updateWithGeneralSettings:(ESTDeviceSettingsGeneral * _Nonnull)generalSettings;
        [Export ("updateWithGeneralSettings:")]
        void Update (DeviceSettingsGeneral generalSettings);
    }

    // @interface ESTDeviceShadow : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceShadow")]
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
        string [] Tags { get; }

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

    // @interface ESTDeviceStatusReport : ESTBaseVO
    [BaseType (typeof (BaseVO), Name="ESTDeviceStatusReport")]
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

    // @interface ESTEddystoneAttachment : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name="ESTEddystoneAttachment")]
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
    [BaseType (typeof (Eddystone), Name="ESTEddystoneEID")]
    interface EddystoneEID
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull ephemeralID;
        [Export ("ephemeralID", ArgumentSemantic.Strong)]
        string EphemeralID { get; }

        // @property (readonly, nonatomic) BOOL isResolved;
        [Export ("isResolved")]
        bool IsResolved { get; }

        // @property (nonatomic, strong) NSArray<ESTEddystoneAttachment *> * _Nullable attachments;
        [NullAllowed, Export ("attachments", ArgumentSemantic.Strong)]
        EddystoneAttachment [] Attachments { get; set; }

        // -(instancetype _Nonnull)initWithEphemeralID:(NSString * _Nullable)ephemeralID resolved:(BOOL)resolved;
        [Export ("initWithEphemeralID:resolved:")]
        IntPtr Constructor ([NullAllowed] string ephemeralID, bool resolved);
    }

    // @interface ESTEddystoneFilterEID : ESTEddystoneFilter
    [BaseType (typeof (EddystoneFilter), Name="ESTEddystoneFilterEID")]
    interface EddystoneFilterEID
    {
    }

    // @interface ESTEddystoneTLM : ESTEddystone
    [BaseType (typeof (Eddystone), Name="ESTEddystoneTLM")]
    interface EddystoneTLM
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

    // @interface ESTEddystoneURL : ESTEddystone
    [BaseType (typeof (Eddystone), Name="ESTEddystoneURL")]
    interface EddystoneURL
    {
        // @property (readonly, nonatomic, strong) NSString * _Nullable url;
        [NullAllowed, Export ("url", ArgumentSemantic.Strong)]
        string Url { get; }

        // -(instancetype _Nonnull)initWithURL:(NSString * _Nonnull)url;
        [Export ("initWithURL:")]
        IntPtr Constructor (string url);
    }

    // @interface ESTFirmwareInfoV4VO : NSObject
    [BaseType (typeof (NSObject), Name="ESTFirmwareInfoV4VO")]
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
        string TarURL { get; set; }

        // +(BOOL)firmwareVersion:(NSString * _Nonnull)firmwareVersion isBiggerThan:(NSString * _Nonnull)referenceFirmwareVersion;
        [Static]
        [Export ("firmwareVersion:isBiggerThan:")]
        bool FirmwareVersion (string firmwareVersion, string referenceFirmwareVersion);
    }

    // @interface ESTGPIOPortsData : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name="ESTGPIOPortsData")]
    interface GPIOPortsData : INSCopying
    {
        // -(instancetype _Nonnull)initWithPort0Value:(ESTGPIOPortValue)port0Value port1Value:(ESTGPIOPortValue)port1Value;
        [Export ("initWithPort0Value:port1Value:")]
        IntPtr Constructor (GPIOPortValue port0Value, GPIOPortValue port1Value);

        // -(NSError * _Nonnull)setPort:(ESTGPIOPort)port value:(ESTGPIOPortValue)value;
        [Export ("setPort:value:")]
        NSError SetPort (GPIOPort port, GPIOPortValue value);

        // -(ESTGPIOPortValue)getValueForPort:(ESTGPIOPort)port;
        [Export ("getValueForPort:")]
        GPIOPortValue GetValueForPort (GPIOPort port);
    }

    // @interface ESTLocationBeaconBulkUpdateConfiguration : NSObject
    [BaseType (typeof (NSObject), Name="ESTLocationBeaconBulkUpdateConfiguration")]
    interface LocationBeaconBulkUpdateConfiguration
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull deviceIdentifier;
        [Export ("deviceIdentifier", ArgumentSemantic.Strong)]
        string DeviceIdentifier { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTSettingOperation *> * _Nonnull settingsOperations;
        [Export ("settingsOperations", ArgumentSemantic.Strong)]
        SettingOperation [] SettingsOperations { get; }

        // -(id _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier settingsOperations:(NSArray<ESTSettingOperation *> * _Nonnull)settingsOperations;
        [Export ("initWithDeviceIdentifier:settingsOperations:")]
        IntPtr Constructor (string deviceIdentifier, SettingOperation [] settingsOperations);
    }

    interface ILocationBeaconBulkUpdaterDelegate { }

    // @protocol ESTLocationBeaconBulkUpdaterDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTLocationBeaconBulkUpdaterDelegate")]
    interface LocationBeaconBulkUpdaterDelegate
    {
        // @optional -(void)bulkUpdater:(ESTLocationBeaconBulkUpdater *)bulkUpdater didUpdateStatus:(ESTBulkUpdaterDeviceUpdateStatus)updateStatus forDeviceWithIdentifier:(NSString *)deviceIdentifier;
        [Export ("bulkUpdater:didUpdateStatus:forDeviceWithIdentifier:")]
        void DidUpdateStatus (LocationBeaconBulkUpdater bulkUpdater, BulkUpdaterDeviceUpdateStatus updateStatus, string deviceIdentifier);

        // @optional -(void)bulkUpdaterDidFinish:(ESTLocationBeaconBulkUpdater *)bulkUpdater;
        [Export ("bulkUpdaterDidFinish:")]
        void DidFinish (LocationBeaconBulkUpdater bulkUpdater);

        // @optional -(void)bulkUpdater:(ESTLocationBeaconBulkUpdater *)bulkUpdater didFailWithError:(NSError *)error;
        [Export ("bulkUpdater:didFailWithError:")]
        void DidFail (LocationBeaconBulkUpdater bulkUpdater, NSError error);
    }

    // @interface ESTLocationBeaconBulkUpdater : NSObject
    [BaseType (typeof (NSObject), Name="ESTLocationBeaconBulkUpdater")]
    interface LocationBeaconBulkUpdater
    {
        [Wrap ("WeakDelegate")]
        LocationBeaconBulkUpdaterDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ESTLocationBeaconBulkUpdaterDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (assign, nonatomic) NSTimeInterval timeout;
        [Export ("timeout")]
        double Timeout { get; set; }

        // @property (readonly, assign, nonatomic) ESTBulkUpdaterStatus status;
        [Export ("status", ArgumentSemantic.Assign)]
        BulkUpdaterStatus Status { get; }

        // @property (readonly, nonatomic, strong) NSArray<ESTLocationBeaconBulkUpdateConfiguration *> * updateConfigurations;
        [Export ("updateConfigurations", ArgumentSemantic.Strong)]
        LocationBeaconBulkUpdateConfiguration [] UpdateConfigurations { get; }

        // - (void)startCloudUpdate;
        [Export ("startCloudUpdate")]
        void StartCloudUpdate ();

        // -(void)startWithUpdateConfigurations:(NSArray<ESTLocationBeaconBulkUpdateConfiguration *> *)updateConfigurations;
        [Export ("startWithUpdateConfigurations:")]
        void Start (LocationBeaconBulkUpdateConfiguration [] updateConfigurations);

        // -(void)cancel;
        [Export ("cancel")]
        void Cancel ();

        // -(ESTBulkUpdaterDeviceUpdateStatus)statusForDeviceWithIdentifier:(NSString *)deviceIdentifier;
        [Export ("statusForDeviceWithIdentifier:")]
        BulkUpdaterDeviceUpdateStatus GetDeviceStatus (string deviceIdentifier);
    }

    // @interface ESTLogger : NSObject
    [BaseType (typeof (NSObject), Name="ESTLogger")]
    interface Logger
    {
        // +(void)enableLogger:(BOOL)enabled withLevel:(ESTLogLevel)level;
        [Static]
        [Export ("enableLogger:withLevel:")]
        void EnableLogger (bool enabled, LogLevel level);

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
    }

    // @interface ESTNearableFirmwareVO : NSObject
    [BaseType (typeof (NSObject), Name="ESTNearableFirmwareVO")]
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
    [BaseType (typeof (NSObject), Name="ESTNearableFirmwareUpdateVO")]
    interface NearableFirmwareUpdateVO
    {
    }

    // @interface ESTNearableOperationApplicationVersion : ESTSettingOperation <ESTNearableOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTNearableOperationApplicationVersion")]
    interface NearableOperationApplicationVersion : NearableOperationProtocol
    {
        // +(instancetype _Nonnull)readOperationWithCompletion:(ESTSettingDeviceInfoApplicationVersionCompletionBlock _Nonnull)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        NearableOperationApplicationVersion ReadOperation (SettingDeviceInfoApplicationVersionCompletionBlock completion);
    }

    // @interface ESTNearableOperationNearableInterval : ESTSettingOperation <ESTNearableOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTNearableOperationNearableInterval")]
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

    // typedef void(^ESTSettingNearablePowerCompletionBlock)(ESTSettingNearablePower* _Nullable powerSetting, NSError* _Nullable error);
    delegate void SettingNearablePowerCompletionBlock (SettingNearablePower powerSetting, NSError error);

    // @interface ESTNearableOperationNearablePower : ESTSettingOperation <ESTNearableOperationProtocol>
    [BaseType (typeof (SettingOperation), Name="ESTNearableOperationNearablePower")]
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

    interface INearableOperationProtocol { }
    // @protocol ESTNearableOperationProtocol <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTNearableOperationProtocol")]
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
        ushort RegisterID { get; }

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

    // @interface ESTNearableSettingsManager : NSObject <ESTPeripheralNearableDelegate>
    [BaseType (typeof (NSObject), Name="ESTNearableSettingsManager")]
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
        void PerformOperations (INearableOperationProtocol [] operations);
    }

    // @interface Internal (ESTNearableSettingsManager)
    [Category]
    [BaseType (typeof (NearableSettingsManager))]
    interface NearableSettingsManagerExtensions
    {
        //// -(instancetype _Nonnull)initWithDevice:(ESTDeviceNearable * _Nonnull)device peripheral:(ESTPeripheralNearable * _Nonnull)peripheral;
        //[Export ("initWithDevice:peripheral:")]
        //IntPtr Constructor (DeviceNearable device, PeripheralNearable peripheral);

        // -(void)synchronizeUsingNearableVO:(ESTNearableVO * _Nonnull)nearableVO forFirmwareVersion:(NSString * _Nonnull)firmwareVersion completion:(void (^ _Nonnull)())completion;
        [Export ("synchronizeUsingNearableVO:forFirmwareVersion:completion:")]
        void Synchronize (NearableVO nearableVO, string firmwareVersion, Action completion);
    }

    // typedef void (^ESTNotificationMotionBlock)(BOOL);
    delegate void NotificationMotionBlock (bool motion);

    // @interface ESTNotificationMotion : NSObject <ESTDeviceNotificationProtocol, NSCopying>
    [BaseType (typeof (NSObject), Name="ESTNotificationMotion")]
    interface NotificationMotion : DeviceNotificationProtocol, INSCopying
    {
        // -(instancetype _Nonnull)initWithHandlerBlock:(ESTNotificationMotionBlock _Nonnull)handler;
        [Export ("initWithHandlerBlock:")]
        IntPtr Constructor (NotificationMotionBlock handler);
    }

    // typedef void (^ESTNotificationGPIODataBlock)(ESTGPIOPortsData * _Nonnull);
    delegate void NotificationGPIODataBlock (GPIOPortsData portsData);

    // @interface ESTNotificationGPIOData : NSObject <ESTDeviceNotificationProtocol, NSCopying>
    [BaseType (typeof (NSObject), Name="ESTNotificationGPIOData")]
    interface NotificationGPIOData : DeviceNotificationProtocol, INSCopying
    {
        // -(instancetype _Nonnull)initWithHandlerBlock:(ESTNotificationGPIODataBlock _Nonnull)handler;
        [Export ("initWithHandlerBlock:")]
        IntPtr Constructor (NotificationGPIODataBlock handler);
    }

    // typedef void (^ESTPeripheralDiscoveryCompletionBlock)(NSError *);
    delegate void PeripheralDiscoveryCompletionBlock (NSError error);

    // @protocol ESTPeripheral <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTPeripheral")]
    interface Peripheral
    {
        // @required -(id)initWithPeripheral:(CBPeripheral *)peripheral;
        //[Abstract]
        [Export ("initWithPeripheral:")]
        IntPtr Constructor (CBPeripheral peripheral);

        // @required -(void)discoverServicesAndCharacteristicsWithCompletion:(ESTPeripheralDiscoveryCompletionBlock)completion;
        [Abstract]
        [Export ("discoverServicesAndCharacteristicsWithCompletion:")]
        void DiscoverServicesAndCharacteristics (PeripheralDiscoveryCompletionBlock completion);
    }

    interface IPeripheralNotificationDelegate { }

    // @protocol ESTPeripheralNotificationDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTPeripheralNotificationDelegate")]
    interface PeripheralNotificationDelegate
    {
        // @required -(void)peripheral:(id<ESTPeripheral>)peripheral didReceiveNotification:(id<ESTDeviceNotificationProtocol>)notification withData:(NSData *)data;
        [Abstract]
        [Export ("peripheral:didReceiveNotification:withData:")]
        void DidReceiveNotification (Peripheral peripheral, DeviceNotificationProtocol notification, NSData data);
    }

    interface IPeripheralNearableDelegate { }
    // @protocol ESTPeripheralNearableDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTPeripheralNearableDelegate")]
    interface PeripheralNearableDelegate
    {
        // @required -(void)peripheral:(id<ESTPeripheral>)peripheral didPerformOperation:(id<ESTNearableOperationProtocol>)operation andReceivedData:(NSData *)data;
        [Abstract]
        [Export ("peripheral:didPerformOperation:andReceivedData:")]
        void DidPerformOperation (Peripheral peripheral, NearableOperationProtocol operation, NSData data);

        // @required -(void)peripheral:(id<ESTPeripheral>)peripheral didFailOperation:(id<ESTNearableOperationProtocol>)operation withError:(NSError *)error;
        [Abstract]
        [Export ("peripheral:didFailOperation:withError:")]
        void DidFailOperation (Peripheral peripheral, NearableOperationProtocol operation, NSError error);
    }

    // @interface ESTPeripheralNearable : NSObject <ESTPeripheral>
    [BaseType (typeof (NSObject), Name="ESTPeripheralNearable")]
    interface PeripheralNearable : Peripheral
    {
        // @property (readonly, nonatomic) CBPeripheral * peripheral;
        [Export ("peripheral")]
        CBPeripheral Peripheral { get; }

        [Wrap ("WeakDelegate")]
        PeripheralNearableDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ESTPeripheralNearableDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

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
        void PerformNearableOperation (NearableOperationProtocol nearableOperation);

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

    interface IPeripheralTypeUtilityDelegate { }
    // @protocol ESTPeripheralTypeUtilityDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTPeripheralTypeUtilityDelegate")]
    interface PeripheralTypeUtilityDelegate
    {
        // @required -(void)peripheral:(id<ESTPeripheral>)peripheral didPerformOperation:(id<ESTBeaconOperationProtocol>)operation andReceivedData:(NSData *)data;
        [Abstract]
        [Export ("peripheral:didPerformOperation:andReceivedData:")]
        void DidPerformOperation (Peripheral peripheral, BeaconOperationProtocol operation, NSData data);

        // @required -(void)peripheral:(id<ESTPeripheral>)peripheral didFailOperation:(id<ESTBeaconOperationProtocol>)operation withError:(NSError *)error;
        [Abstract]
        [Export ("peripheral:didFailOperation:withError:")]
        void DidFailOperation (Peripheral peripheral, BeaconOperationProtocol operation, NSError error);
    }

    // @interface ESTPeripheralTypeUtility : NSObject <ESTPeripheral>
    [BaseType (typeof (NSObject), Name="ESTPeripheralTypeUtility")]
    interface PeripheralTypeUtility : Peripheral
    {
        // @property (readonly, nonatomic) ESTPeripheralFirmwareState firmwareState;
        [Export ("firmwareState")]
        PeripheralFirmwareState FirmwareState { get; }

        [Wrap ("WeakDelegate")]
        PeripheralTypeUtilityDelegate Delegate { get; set; }

        // @property (nonatomic, strong) id<ESTPeripheralTypeUtilityDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Strong)]
        NSObject WeakDelegate { get; set; }

        [Wrap ("WeakNotificationDelegate")]
        PeripheralNotificationDelegate NotificationDelegate { get; set; }

        // @property (nonatomic, weak) id<ESTPeripheralNotificationDelegate> notificationDelegate;
        [NullAllowed, Export ("notificationDelegate", ArgumentSemantic.Weak)]
        NSObject WeakNotificationDelegate { get; set; }

        // -(void)resetPeripheralToBootWithCompletion:(ESTCompletionBlock)completion;
        [Export ("resetPeripheralToBootWithCompletion:"), Async]
        void ResetPeripheralToBoot (CompletionBlock completion);

        // -(void)performSettingOperation:(id<ESTBeaconOperationProtocol>)operation;
        [Export ("performSettingOperation:")]
        void PerformSettingOperation (BeaconOperationProtocol operation);

        // -(void)registerNotification:(id<ESTDeviceNotificationProtocol>)notification;
        [Export ("registerNotification:")]
        void RegisterNotification (DeviceNotificationProtocol notification);

        // -(void)unregisterAllNotifications;
        [Export ("unregisterAllNotifications")]
        void UnregisterAllNotifications ();
    }

    // typedef void (^ESTRequestAnalyticsGroupTrackBlock)(NSError *);
    delegate void RequestAnalyticsGroupTrackBlock (NSError error);

    // @interface ESTRequestAnalyticsTrack : ESTRequestPostJSON
    [BaseType (typeof (RequestPostJSON), Name="ESTRequestAnalyticsTrack")]
    interface RequestAnalyticsTrack
    {
        // -(instancetype)initWithEvents:(NSArray *)events;
        [Export ("initWithEvents:")]
        IntPtr Constructor (NSObject [] events);

        // -(void)sendRequestWithCompletion:(ESTRequestAnalyticsGroupTrackBlock)completion;
        [Export ("sendRequestWithCompletion:"), Async]
        void SendRequest (RequestAnalyticsGroupTrackBlock completion);
    }


    // @interface ESTRequestFirmwareV4 : ESTRequestGetJSON
    [BaseType (typeof (RequestGetJSON), Name="ESTRequestFirmwareV4")]
    interface RequestFirmwareV4
    {
        // - (instancetype)initWithPublicID:(NSString*)publicID;
        [Export ("initWithPublicID:")]
        IntPtr Constructor (string publicId);
    }

    // typedef void (^ESTRequestGetBeaconsPublicDetailsBlock)(NSArray * _Nullable, NSError * _Nullable);
    delegate void RequestGetBeaconsPublicDetailsBlock ([NullAllowed] NSObject [] items, [NullAllowed] NSError error);

    // @interface ESTRequestGetBeaconsPublicDetails : ESTRequestGetJSON
    [BaseType (typeof (RequestGetJSON), Name="ESTRequestGetBeaconsPublicDetails")]
    interface RequestGetBeaconsPublicDetails
    {
        // @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull beaconIdentifiers;
        [Export ("beaconIdentifiers", ArgumentSemantic.Strong)]
        string [] BeaconIdentifiers { get; }

        // @property (readonly, assign, nonatomic) ESTBeaconPublicDetailsFields fields;
        [Export ("fields", ArgumentSemantic.Assign)]
        BeaconPublicDetailsFields Fields { get; }

        // -(instancetype _Nonnull)initWithBeacons:(NSArray<CLBeacon *> * _Nonnull)beacons andFields:(ESTBeaconPublicDetailsFields)fields;
        [Export ("initWithBeacons:andFields:")]
        IntPtr Constructor (CLBeacon [] beacons, BeaconPublicDetailsFields fields);

        // -(instancetype _Nonnull)initWithMacAddresses:(NSArray<NSString *> * _Nonnull)macAddresses andFields:(ESTBeaconPublicDetailsFields)fields;
        [Export ("initWithMacAddresses:andFields:")]
        IntPtr Constructor (string [] macAddresses, BeaconPublicDetailsFields fields);

        // -(void)sendRequestWithCompletion:(ESTRequestGetBeaconsPublicDetailsBlock _Nonnull)completion;
        [Export ("sendRequestWithCompletion:"), Async]
        void SendRequest (RequestGetBeaconsPublicDetailsBlock completion);
    }

    // @interface ESTRequestPostFormData : ESTRequestBase
    [BaseType (typeof (RequestBase), Name = "ESTRequestPostFormData")]
    interface RequestPostFormData
    {
        // - (void)setFilePath:(NSString*)filePath forRequest:(NSMutableURLRequest*)request;
        [Export ("setFilePath:forRequest:")]
        void SetFilePath (string filePath, NSMutableUrlRequest request);
    }

    // typedef void (^ESTRequestV2GetDeviceDetailsBlock)(ESTDeviceDetails * _Nullable, NSError * _Nullable);
    delegate void RequestV2GetDeviceDetailsBlock ([NullAllowed] DeviceDetails deviceDetails, [NullAllowed] NSError error);

    // @interface ESTRequestV2GetDeviceDetails : ESTRequestGetJSON
    [BaseType (typeof (RequestGetJSON), Name="ESTRequestV2GetDeviceDetails")]
    interface ESTRequestV2GetDeviceDetails
    {
        // -(instancetype _Nonnull)initWithDeviceIdentifier:(NSString * _Nonnull)deviceIdentifier;
        [Export ("initWithDeviceIdentifier:")]
        IntPtr Constructor (string deviceIdentifier);

        // -(void)sendRequestWithCompletion:(ESTRequestV2GetDeviceDetailsBlock _Nonnull)completion;
        [Export ("sendRequestWithCompletion:"), Async]
        void SendRequest (RequestV2GetDeviceDetailsBlock completion);
    }

    // typedef void (^ESTRequestV2GetDevicesBlock)(NSArray<ESTDeviceDetails *> * _Nullable, NSError * _Nullable);
    delegate void RequestV2GetDevicesBlock ([NullAllowed] DeviceDetails [] deviceDetails, [NullAllowed] NSError error);

    // @interface ESTRequestV2GetDevices : ESTRequestGetJSON
    [BaseType (typeof (RequestGetJSON), Name="ESTRequestV2GetDevices")]
    interface RequestV2GetDevices
    {
        // -(void)sendRequestWithCompletion:(ESTRequestV2GetDevicesBlock _Nonnull)completion;
        [Export ("sendRequestWithCompletion:"), Async]
        void SendRequest (RequestV2GetDevicesBlock completion);
    }

    interface ISecureBeaconManagerDelegate { }

    // @protocol ESTSecureBeaconManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTSecureBeaconManagerDelegate")]
    interface SecureBeaconManagerDelegate
    {
        // @optional -(void)beaconManager:(id _Nonnull)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
        [Export ("beaconManager:didChangeAuthorizationStatus:")]
        void DidChangeAuthorizationStatus (NSObject manager, CLAuthorizationStatus status);

        // @optional -(void)beaconManager:(id _Nonnull)manager didStartMonitoringForRegion:(CLBeaconRegion * _Nonnull)region;
        [Export ("beaconManager:didStartMonitoringForRegion:")]
        void DidStartMonitoringForRegion (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id _Nonnull)manager monitoringDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
        [Export ("beaconManager:monitoringDidFailForRegion:withError:")]
        void MonitoringDidFailForRegion (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

        // @optional -(void)beaconManager:(id _Nonnull)manager didEnterRegion:(CLBeaconRegion * _Nonnull)region;
        [Export ("beaconManager:didEnterRegion:")]
        void DidEnterRegion (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id _Nonnull)manager didExitRegion:(CLBeaconRegion * _Nonnull)region;
        [Export ("beaconManager:didExitRegion:")]
        void DidExitRegion (NSObject manager, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id _Nonnull)manager didDetermineState:(CLRegionState)state forRegion:(CLBeaconRegion * _Nonnull)region;
        [Export ("beaconManager:didDetermineState:forRegion:")]
        void DidDetermineState (NSObject manager, CLRegionState state, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id _Nonnull)manager didRangeBeacons:(NSArray<ESTBeacon *> * _Nonnull)beacons inRegion:(CLBeaconRegion * _Nonnull)region;
        [Export ("beaconManager:didRangeBeacons:inRegion:")]
        void DidRangeBeacons (NSObject manager, Beacon [] beacons, CLBeaconRegion region);

        // @optional -(void)beaconManager:(id _Nonnull)manager rangingBeaconsDidFailForRegion:(CLBeaconRegion * _Nullable)region withError:(NSError * _Nonnull)error;
        [Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:")]
        void RangingBeaconsDidFailForRegion (NSObject manager, [NullAllowed] CLBeaconRegion region, NSError error);

        // @optional -(void)beaconManager:(id _Nonnull)manager didFailWithError:(NSError * _Nonnull)error;
        [Export ("beaconManager:didFailWithError:")]
        void DidFailWithError (NSObject manager, NSError error);
    }


    // @interface ESTSecureBeaconManager : NSObject
    [BaseType (typeof (NSObject), Name="ESTSecureBeaconManager")]
    interface SecureBeaconManager
    {
        [Wrap ("WeakDelegate")]
        [NullAllowed]
        SecureBeaconManagerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ESTSecureBeaconManagerDelegate> _Nullable delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

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

    // typedef void (^ESTSettingCompletionBlock)(ESTSettingBase * _Nullable, NSError * _Nullable);
    delegate void SettingCompletionBlock ([NullAllowed] SettingBase setting, [NullAllowed] NSError error);

    // @interface ESTSettingBase : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingBase")]
    interface SettingBase
    {
        // -(instancetype _Nonnull)initWithData:(NSData * _Nonnull)data;
        [Export ("initWithData:")]
        IntPtr Constructor (NSData data);
    }

    //// @interface Internal (ESTSettingBase)
    //[Category]
    //[BaseType (typeof (SettingBase))]
    //interface SettingBaseExtensions
    //{
    //    // @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
    //    [NullAllowed, Export ("device", ArgumentSemantic.Weak)]
    //    DeviceConnectable Device { get; set; }
    //}

    // @interface ESTSettingConnectivityInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingConnectivityInterval")]
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

    // @interface ESTSettingConnectivityPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingConnectivityPower")]
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

    // typedef void (^ESTSettingDeviceInfoFirmwareVersionCompletionBlock)(ESTSettingDeviceInfoFirmwareVersion * _Nullable, NSError * _Nullable);
    delegate void SettingDeviceInfoFirmwareVersionCompletionBlock ([NullAllowed] SettingDeviceInfoFirmwareVersion deviceInfoFirmwareVersion, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoFirmwareVersion : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoFirmwareVersion")]
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
    delegate void SettingDeviceInfoApplicationVersionCompletionBlock ([NullAllowed] SettingDeviceInfoApplicationVersion deviceInfoApplicationVersion, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoApplicationVersion : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoApplicationVersion")]
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
    delegate void SettingDeviceInfoBootloaderVersionCompletionBlock ([NullAllowed] SettingDeviceInfoBootloaderVersion deviceInfoBootloaderVersion, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoBootloaderVersion : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoBootloaderVersion")]
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
    delegate void SettingDeviceInfoHardwareVersionCompletionBlock ([NullAllowed] SettingDeviceInfoHardwareVersion deviceInfoHardwareVersion, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoHardwareVersion : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoHardwareVersion")]
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
    delegate void SettingDeviceInfoUTCTimeCompletionBlock ([NullAllowed] SettingDeviceInfoUTCTime deviceInfoUTCTime, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoUTCTime : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingDeviceInfoUTCTime")]
    interface SettingDeviceInfoUTCTime : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(NSTimeInterval)UTCTime;
        [Export ("initWithValue:")]
        IntPtr Constructor (double UTCTime);

        // -(NSTimeInterval)getValue;
        [Export ("getValue")]
        double Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingDeviceInfoUTCTimeCompletionBlock completion);

        // -(void)writeValue:(NSTimeInterval)UTCTime completion:(ESTSettingDeviceInfoUTCTimeCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (double UTCTime, SettingDeviceInfoUTCTimeCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(NSTimeInterval)UTCTime;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (double UTCTime);
    }

    // typedef void (^ESTSettingDeviceInfoTagsCompletionBlock)(ESTSettingDeviceInfoTags * _Nullable, NSError * _Nullable);
    delegate void SettingDeviceInfoTagsCompletionBlock ([NullAllowed] SettingDeviceInfoTags deviceInfoTags, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoTags : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingDeviceInfoTags")]
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
    delegate void SettingDeviceInfoGeoLocationCompletionBlock ([NullAllowed] SettingDeviceInfoGeoLocation deviceInfoGeoLocation, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoGeoLocation : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingDeviceInfoGeoLocation")]
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
    delegate void SettingDeviceInfoNameCompletionBlock ([NullAllowed] SettingDeviceInfoName deviceInfoName, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoName : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingDeviceInfoName")]
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
    delegate void SettingDeviceInfoColorCompletionBlock ([NullAllowed] SettingDeviceInfoColor deviceInfoColor, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoColor : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoColor")]
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
    delegate void SettingDeviceInfoIndoorLocationIdentifierCompletionBlock ([NullAllowed] SettingDeviceInfoIndoorLocationIdentifier deviceInfoIndoorLocationIdentifier, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoIndoorLocationIdentifier : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoIndoorLocationIdentifier")]
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
    delegate void SettingDeviceInfoIndoorLocationNameCompletionBlock ([NullAllowed] SettingDeviceInfoIndoorLocationName deviceInfoIndoorLocationName, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoIndoorLocationName : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoIndoorLocationName")]
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
    delegate void SettingDeviceInfoUptimeCompletionBlock ([NullAllowed] SettingDeviceInfoUptime deviceInfoUpdate, [NullAllowed] NSError error);

    // @interface ESTSettingDeviceInfoUptime : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingDeviceInfoUptime")]
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


    // typedef void (^ESTSettingEddystoneUIDEnableCompletionBlock)(ESTSettingEddystoneUIDEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneUIDEnableCompletionBlock ([NullAllowed] SettingEddystoneUIDEnable eddystoneUIDEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneUIDEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneUIDEnable")]
    interface SettingEddystoneUIDEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneUIDEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingEddystoneUIDEnableCompletionBlock completion);
    }

    // typedef void (^ESTSettingEddystoneUIDInstanceCompletionBlock)(ESTSettingEddystoneUIDInstance * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneUIDInstanceCompletionBlock ([NullAllowed] SettingEddystoneUIDInstance eddystoneUIDInstance, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneUIDInstance : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneUIDInstance")]
    interface SettingEddystoneUIDInstance : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)instanceID;
        [Export ("initWithValue:")]
        IntPtr Constructor (string instanceID);

        // -(NSString * _Nonnull)getValue;
        [Export ("getValue")]
        string Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneUIDInstanceCompletionBlock completion);

        // -(void)writeValue:(NSString * _Nonnull)instanceID completion:(ESTSettingEddystoneUIDInstanceCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (string instanceID, SettingEddystoneUIDInstanceCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)instanceID;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (string instanceID);
    }

    // typedef void (^ESTSettingEddystoneUIDNamespaceCompletionBlock)(ESTSettingEddystoneUIDNamespace * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneUIDNamespaceCompletionBlock ([NullAllowed] SettingEddystoneUIDNamespace eddystoneUIDNamespace, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneUIDNamespace : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneUIDNamespace")]
    interface SettingEddystoneUIDNamespace : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)namespaceID;
        [Export ("initWithValue:")]
        IntPtr Constructor (string namespaceID);

        // -(NSString * _Nonnull)getValue;
        [Export ("getValue")]
        string Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneUIDNamespaceCompletionBlock completion);

        // -(void)writeValue:(NSString * _Nonnull)namespaceID completion:(ESTSettingEddystoneUIDNamespaceCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (string namespaceID, SettingEddystoneUIDNamespaceCompletionBlock completion);

        // +(NSString * _Nonnull)namespaceHexStringForEddystoneDomain:(NSString * _Nonnull)domain;
        [Static]
        [Export ("namespaceHexStringForEddystoneDomain:")]
        string GetNamespaceHexString (string eddystoneDomain);

        // +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)namespaceID;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (string namespaceID);
    }

    // typedef void (^ESTSettingEddystoneUIDIntervalCompletionBlock)(ESTSettingEddystoneUIDInterval * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneUIDIntervalCompletionBlock ([NullAllowed] SettingEddystoneUIDInterval eddystoneUIDInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneUIDInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneUIDInterval")]
    interface SettingEddystoneUIDInterval : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort advertisingInterval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneUIDIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneUIDIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort advertisingInterval, SettingEddystoneUIDIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort advertisingInterval);
    }

    // typedef void (^ESTSettingEddystoneUIDPowerCompletionBlock)(ESTSettingEddystoneUIDPower * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneUIDPowerCompletionBlock ([NullAllowed] SettingEddystoneUIDPower eddystoneUIDPower, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneUIDPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneUIDPower")]
    interface SettingEddystoneUIDPower : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(ESTEddystoneUIDPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (EddystoneUIDPower power);

        // -(ESTEddystoneUIDPower)getValue;
        [Export ("getValue")]
        EddystoneUIDPower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneUIDPowerCompletionBlock completion);

        // -(void)writeValue:(ESTEddystoneUIDPower)power completion:(ESTSettingEddystoneUIDPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (EddystoneUIDPower power, SettingEddystoneUIDPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneUIDPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (EddystoneUIDPower power);
    }


    // typedef void (^ESTSettingEddystoneURLEnableCompletionBlock)(ESTSettingEddystoneURLEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneURLEnableCompletionBlock ([NullAllowed] SettingEddystoneURLEnable eddystoneURLEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneURLEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneURLEnable")]
    interface SettingEddystoneURLEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneURLEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneURLEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingEddystoneURLEnableCompletionBlock completion);
    }

    // typedef void (^ESTSettingEddystoneURLDataCompletionBlock)(ESTSettingEddystoneURLData * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneURLDataCompletionBlock ([NullAllowed] SettingEddystoneURLData eddystoneURLData, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneURLData : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneURLData")]
    interface SettingEddystoneURLData : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(NSString * _Nonnull)eddystoneURL;
        [Export ("initWithValue:")]
        IntPtr Constructor (string eddystoneURL);

        // -(NSString * _Nonnull)getValue;
        [Export ("getValue")]
        string Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneURLDataCompletionBlock completion);

        // -(void)writeValue:(NSString * _Nonnull)eddystoneURL completion:(ESTSettingEddystoneURLDataCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (string eddystoneURL, SettingEddystoneURLDataCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(NSString * _Nonnull)eddystoneURL;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (string eddystoneURL);
    }

    // typedef void (^ESTSettingEddystoneURLIntervalCompletionBlock)(ESTSettingEddystoneURLInterval * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneURLIntervalCompletionBlock ([NullAllowed] SettingEddystoneURLInterval eddystoneURLInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneURLInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneURLInterval")]
    interface SettingEddystoneURLInterval : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort advertisingInterval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneURLIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneURLIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort advertisingInterval, SettingEddystoneURLIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort advertisingInterval);
    }

    // typedef void (^ESTSettingEddystoneURLPowerCompletionBlock)(ESTSettingEddystoneURLPower * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneURLPowerCompletionBlock ([NullAllowed] SettingEddystoneURLPower eddystoneURLPower, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneURLPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneURLPower")]
    interface SettingEddystoneURLPower : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(ESTEddystoneURLPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (EddystoneURLPower power);

        // -(ESTEddystoneURLPower)getValue;
        [Export ("getValue")]
        EddystoneURLPower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneURLPowerCompletionBlock completion);

        // -(void)writeValue:(ESTEddystoneURLPower)power completion:(ESTSettingEddystoneURLPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (EddystoneURLPower power, SettingEddystoneURLPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneURLPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (EddystoneURLPower power);
    }

    // typedef void (^ESTSettingEddystoneTLMEnableCompletionBlock)(ESTSettingEddystoneTLMEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneTLMEnableCompletionBlock ([NullAllowed] SettingEddystoneTLMEnable eddystoneTLMEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneTLMEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneTLMEnable")]
    interface SettingEddystoneTLMEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneTLMEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(ESTSettingEddystoneTLMEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingEddystoneTLMEnableCompletionBlock completion);
    }

    // typedef void (^ESTSettingEddystoneTLMIntervalCompletionBlock)(ESTSettingEddystoneTLMInterval * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneTLMIntervalCompletionBlock ([NullAllowed] SettingEddystoneTLMInterval eddystoneTLMInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneTLMInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneTLMInterval")]
    interface SettingEddystoneTLMInterval : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort advertisingInterval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneTLMIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEddystoneTLMIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort advertisingInterval, SettingEddystoneTLMIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort advertisingInterval);
    }

    // typedef void (^ESTSettingEddystoneTLMPowerCompletionBlock)(ESTSettingEddystoneTLMPower * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneTLMPowerCompletionBlock ([NullAllowed] SettingEddystoneTLMPower eddystoneTLMPower, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneTLMPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneTLMPower")]
    interface SettingEddystoneTLMPower : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(ESTEddystoneTLMPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (EddystoneTLMPower power);

        // -(ESTEddystoneTLMPower)getValue;
        [Export ("getValue")]
        EddystoneTLMPower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneTLMPowerCompletionBlock completion);

        // -(void)writeValue:(ESTEddystoneTLMPower)power completion:(ESTSettingEddystoneTLMPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (EddystoneTLMPower power, SettingEddystoneTLMPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneTLMPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (EddystoneTLMPower power);
    }



    // typedef void (^ESTSettingEddystoneEIDIntervalCompletionBlock)(ESTSettingEddystoneEIDInterval * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneEIDIntervalCompletionBlock ([NullAllowed] SettingEddystoneEIDInterval eddystoneEIDInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneEIDInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneEIDInterval")]
    interface SettingEddystoneEIDInterval : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(unsigned short)interval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort interval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneEIDIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)interval completion:(ESTSettingEddystoneEIDIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort interval, SettingEddystoneEIDIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)interval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort interval);
    }

    // typedef void (^ESTSettingEddystoneEIDEnableCompletionBlock)(ESTSettingEddystoneEIDEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneEIDEnableCompletionBlock ([NullAllowed] SettingEddystoneEIDEnable eddystoneEIDEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneEIDEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneEIDEnable")]
    interface SettingEddystoneEIDEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enable;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enable);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneEIDEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enable completion:(ESTSettingEddystoneEIDEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enable, SettingEddystoneEIDEnableCompletionBlock completion);
    }

    // typedef void (^ESTSettingEddystoneEIDPowerCompletionBlock)(ESTSettingEddystoneEIDPower * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneEIDPowerCompletionBlock ([NullAllowed] SettingEddystoneEIDPower eddystoneEIDPower, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneEIDPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneEIDPower")]
    interface SettingEddystoneEIDPower : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(ESTEddystoneEIDPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (EddystoneEIDPower power);

        // -(ESTEddystoneEIDPower)getValue;
        [Export ("getValue")]
        EddystoneEIDPower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneEIDPowerCompletionBlock completion);

        // -(void)writeValue:(ESTEddystoneEIDPower)power completion:(ESTSettingEddystoneEIDPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (EddystoneEIDPower power, SettingEddystoneEIDPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTEddystoneEIDPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (EddystoneEIDPower power);
    }


    // typedef void (^ESTSettingEstimoteLocationEnableCompletionBlock)(ESTSettingEstimoteLocationEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEstimoteLocationEnableCompletionBlock ([NullAllowed] SettingEstimoteLocationEnable estimoteLocationEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteLocationEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteLocationEnable")]
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
    delegate void SettingEstimoteLocationIntervalCompletionBlock ([NullAllowed] SettingEstimoteLocationInterval estimoteLocationInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteLocationInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteLocationInterval")]
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
    delegate void SettingEstimoteLocationPowerCompletionBlock ([NullAllowed] SettingEstimoteLocationPower estimoteLocationPower, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteLocationPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteLocationPower")]
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

    // typedef void (^ESTSettingEstimoteTLMEnableCompletionBlock)(ESTSettingEstimoteTLMEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEstimoteTLMEnableCompletionBlock ([NullAllowed] SettingEstimoteTLMEnable estimoteTLMEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteTLMEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteTLMEnable")]
    interface SettingEstimoteTLMEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEstimoteTLMEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(ESTSettingEstimoteTLMEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingEstimoteTLMEnableCompletionBlock completion);
    }

    // typedef void (^ESTSettingEstimoteTLMIntervalCompletionBlock)(ESTSettingEstimoteTLMInterval * _Nullable, NSError * _Nullable);
    delegate void SettingEstimoteTLMIntervalCompletionBlock ([NullAllowed] SettingEstimoteTLMInterval estimoteTLMInterval, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteTLMInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteTLMInterval")]
    interface SettingEstimoteTLMInterval : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort advertisingInterval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEstimoteTLMIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)advertisingInterval completion:(ESTSettingEstimoteTLMIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort advertisingInterval, SettingEstimoteTLMIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort advertisingInterval);
    }

    // typedef void (^ESTSettingEstimoteTLMPowerCompletionBlock)(ESTSettingEstimoteTLMPower * _Nullable, NSError * _Nullable);
    delegate void SettingEstimoteTLMPowerCompletionBlock ([NullAllowed] SettingEstimoteTLMPower estimoteTLMPower, [NullAllowed] NSError error);

    // @interface ESTSettingEstimoteTLMPower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEstimoteTLMPower")]
    interface SettingEstimoteTLMPower : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(ESTEstimoteTLMPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (EstimoteTLMPower power);

        // -(ESTEstimoteTLMPower)getValue;
        [Export ("getValue")]
        EstimoteTLMPower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEstimoteTLMPowerCompletionBlock completion);

        // -(void)writeValue:(ESTEstimoteTLMPower)power completion:(ESTSettingEstimoteTLMPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (EstimoteTLMPower power, SettingEstimoteTLMPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTEstimoteTLMPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (EstimoteTLMPower power);
    }


    // typedef void (^SettingGPIONotificationEnableCompletionBlock)(SettingGPIONotificationEnable * _Nullable, NSError * _Nullable);
    delegate void SettingGPIONotificationEnableCompletionBlock ([NullAllowed] SettingGPIONotificationEnable gpioNotificationEnable, [NullAllowed] NSError error);

    // @interface SettingGPIONotificationEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIONotificationEnable")]
    interface SettingGPIONotificationEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingGPIONotificationEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingGPIONotificationEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingGPIONotificationEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool enabled);
    }



    // typedef void (^SettingGPIOPortsDataCompletionBlock)(SettingGPIOPortsData * _Nullable, NSError * _Nullable);
    delegate void SettingGPIOPortsDataCompletionBlock ([NullAllowed] SettingGPIOPortsData gpioPortsData, [NullAllowed] NSError error);

    // @interface SettingGPIOPortsData : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOPortsData")]
    interface SettingGPIOPortsData : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(GPIOPortsData * _Nonnull)portsData;
        [Export ("initWithValue:")]
        IntPtr Constructor (GPIOPortsData portsData);

        // -(GPIOPortsData * _Nonnull)getValue;
        [Export ("getValue")]
        GPIOPortsData Value { get; }

        // -(void)readValueWithCompletion:(SettingGPIOPortsDataCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingGPIOPortsDataCompletionBlock completion);

        // -(void)writeValue:(GPIOPortsData * _Nonnull)portsData completion:(SettingGPIOPortsDataCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (GPIOPortsData portsData, SettingGPIOPortsDataCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(GPIOPortsData * _Nonnull)portsData;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (GPIOPortsData portsData);
    }

    // typedef void (^SettingGPIOConfigPort0CompletionBlock)(SettingGPIOConfigPort0 * _Nullable, NSError * _Nullable);
    delegate void SettingGPIOConfigPort0CompletionBlock ([NullAllowed] SettingGPIOConfigPort0 gpioConfigPort0, [NullAllowed] NSError error);

    // @interface SettingGPIOConfigPort0 : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOConfigPort0")]
    interface SettingGPIOConfigPort0 : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(GPIOConfig)config;
        [Export ("initWithValue:")]
        IntPtr Constructor (GPIOConfig config);

        // -(GPIOConfig)getValue;
        [Export ("getValue")]
        GPIOConfig Value { get; }

        // -(void)readValueWithCompletion:(SettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingGPIOConfigPort0CompletionBlock completion);

        // -(void)writeValue:(GPIOConfig)config completion:(SettingGPIOConfigPort0CompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (GPIOConfig config, SettingGPIOConfigPort0CompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(GPIOConfig)config;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (GPIOConfig config);
    }

    // typedef void (^SettingGPIOConfigPort1CompletionBlock)(SettingGPIOConfigPort1 * _Nullable, NSError * _Nullable);
    delegate void SettingGPIOConfigPort1CompletionBlock ([NullAllowed] SettingGPIOConfigPort1 gpioConfigPort1, [NullAllowed] NSError error);

    // @interface SettingGPIOConfigPort1 : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingGPIOConfigPort1")]
    interface SettingGPIOConfigPort1 : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(GPIOConfig)config;
        [Export ("initWithValue:")]
        IntPtr Constructor (GPIOConfig config);

        // -(GPIOConfig)getValue;
        [Export ("getValue")]
        GPIOConfig Value { get; }

        // -(void)readValueWithCompletion:(SettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingGPIOConfigPort1CompletionBlock completion);

        // -(void)writeValue:(GPIOConfig)config completion:(SettingGPIOConfigPort1CompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (GPIOConfig config, SettingGPIOConfigPort1CompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(GPIOConfig)config;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (GPIOConfig config);
    }


    // typedef void (^SettingIBeaconEnableCompletionBlock)(SettingIBeaconEnable * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconEnableCompletionBlock ([NullAllowed] SettingIBeaconEnable ibeaconEnable, [NullAllowed] NSError error);

    // @interface SettingIBeaconEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconEnable")]
    interface SettingIBeaconEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ÏSettingIBeaconEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingIBeaconEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingIBeaconEnableCompletionBlock completion);
    }

    // typedef void (^SettingIBeaconIntervalCompletionBlock)(SettingIBeaconInterval * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconIntervalCompletionBlock ([NullAllowed] SettingIBeaconInterval ibeaconInterval, [NullAllowed] NSError error);

    // @interface SettingIBeaconInterval : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconInterval")]
    interface SettingIBeaconInterval : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)advertisingInterval;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort advertisingInterval);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconIntervalCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconIntervalCompletionBlock completion);

        // -(void)writeValue:(unsigned short)advertisingInterval completion:(SettingIBeaconIntervalCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort advertisingInterval, SettingIBeaconIntervalCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)advertisingInterval;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort advertisingInterval);
    }

    // typedef void (^SettingIBeaconMajorCompletionBlock)(SettingIBeaconMajor * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconMajorCompletionBlock ([NullAllowed] SettingIBeaconMajor ibeaconMajor, [NullAllowed] NSError error);

    // @interface SettingIBeaconMajor : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMajor")]
    interface SettingIBeaconMajor : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)major;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort major);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconMajorCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconMajorCompletionBlock completion);

        // -(void)writeValue:(unsigned short)value completion:(SettingIBeaconMajorCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort value, SettingIBeaconMajorCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)major;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort major);
    }

    // typedef void (^SettingIBeaconMinorCompletionBlock)(SettingIBeaconMinor * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconMinorCompletionBlock ([NullAllowed] SettingIBeaconMinor ibeaconMinor, [NullAllowed] NSError error);

    // @interface SettingIBeaconMinor : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMinor")]
    interface SettingIBeaconMinor : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(unsigned short)minor;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort minor);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconMinorCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconMinorCompletionBlock completion);

        // -(void)writeValue:(unsigned short)minor completion:(SettingIBeaconMinorCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (ushort minor, SettingIBeaconMinorCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(unsigned short)minor;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (ushort minor);
    }

    // typedef void (^SettingIBeaconPowerCompletionBlock)(SettingIBeaconPower * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconPowerCompletionBlock ([NullAllowed] SettingIBeaconPower ibeaconPower, [NullAllowed] NSError error);

    // @interface SettingIBeaconPower : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconPower")]
    interface SettingIBeaconPower : INSCopying
    {
        // -(instancetype _Nullable)initWithValue:(IBeaconPower)power;
        [Export ("initWithValue:")]
        IntPtr Constructor (IBeaconPower power);

        // -(IBeaconPower)getValue;
        [Export ("getValue")]
        IBeaconPower Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconPowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconPowerCompletionBlock completion);

        // -(void)writeValue:(IBeaconPower)power completion:(SettingIBeaconPowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (IBeaconPower power, SettingIBeaconPowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(IBeaconPower)power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (IBeaconPower power);
    }

    // typedef void (^SettingIBeaconProximityUUIDCompletionBlock)(SettingIBeaconProximityUUID * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconProximityUUIDCompletionBlock ([NullAllowed] SettingIBeaconProximityUUID ibeaconProximityUUID, [NullAllowed] NSError error);

    // @interface SettingIBeaconProximityUUID : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconProximityUUID")]
    interface SettingIBeaconProximityUUID : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(NSUUID * _Nonnull)proximityUUID;
        [Export ("initWithValue:")]
        IntPtr Constructor (NSUuid proximityUUID);

        // -(NSUUID * _Nonnull)getValue;
        [Export ("getValue")]
        NSUuid Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconProximityUUIDCompletionBlock completion);

        // -(void)writeValue:(NSUUID * _Nonnull)proximityUUID completion:(SettingIBeaconProximityUUIDCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (NSUuid proximityUUID, SettingIBeaconProximityUUIDCompletionBlock completion);
    }

    // typedef void (^SettingIBeaconSecureUUIDEnableCompletionBlock)(SettingIBeaconSecureUUIDEnable * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconSecureUUIDEnableCompletionBlock ([NullAllowed] SettingIBeaconSecureUUIDEnable ibeaconSecureUUIDEnable, [NullAllowed] NSError error);

    // @interface SettingIBeaconSecureUUIDEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconSecureUUIDEnable")]
    interface SettingIBeaconSecureUUIDEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconSecureUUIDEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingIBeaconSecureUUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingIBeaconSecureUUIDEnableCompletionBlock completion);
    }

    // typedef void (^SettingIBeaconSecureUUIDPeriodScalerCompletionBlock)(SettingIBeaconSecureUUIDPeriodScaler * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconSecureUUIDPeriodScalerCompletionBlock ([NullAllowed] SettingIBeaconSecureUUIDPeriodScaler ibeaconSecureUUIDPeriodScaler, [NullAllowed] NSError error);

    // @interface SettingIBeaconSecureUUIDPeriodScaler : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconSecureUUIDPeriodScaler")]
    interface SettingIBeaconSecureUUIDPeriodScaler : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(uint8_t)scaler;
        [Export ("initWithValue:")]
        IntPtr Constructor (byte scaler);

        // -(uint8_t)getValue;
        [Export ("getValue")]
        byte Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconSecureUUIDPeriodScalerCompletionBlock completion);

        // -(void)writeValue:(uint8_t)scaler completion:(SettingIBeaconSecureUUIDPeriodScalerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (byte scaler, SettingIBeaconSecureUUIDPeriodScalerCompletionBlock completion);
    }

    // typedef void (^SettingIBeaconNonStrictModeCompletionBlock)(SettingIBeaconNonStrictMode * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconNonStrictModeCompletionBlock ([NullAllowed] SettingIBeaconNonStrictMode ibeaconNonStrictMode, [NullAllowed] NSError error);

    // @interface SettingIBeaconNonStrictMode : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconNonStrictMode")]
    interface SettingIBeaconNonStrictMode : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)nonStrictMode;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool nonStrictMode);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconNonStrictModeCompletionBlock completion);

        // -(void)writeValue:(BOOL)nonStrictMode completion:(SettingIBeaconNonStrictModeCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool nonStrictMode, SettingIBeaconNonStrictModeCompletionBlock completion);
    }

    // typedef void (^SettingIBeaconMotionUUIDCompletionBlock)(SettingIBeaconMotionUUID * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconMotionUUIDCompletionBlock ([NullAllowed] SettingIBeaconMotionUUID ibeaconMotionUUID, [NullAllowed] NSError error);

    // @interface SettingIBeaconMotionUUID : SettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name = "ESTSettingIBeaconMotionUUID")]
    interface SettingIBeaconMotionUUID : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(NSUUID * _Nonnull)motionUUID;
        [Export ("initWithValue:")]
        IntPtr Constructor (NSUuid motionUUID);

        // -(NSUUID * _Nonnull)getValue;
        [Export ("getValue")]
        NSUuid Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconMotionUUIDCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconMotionUUIDCompletionBlock completion);

        // +(NSUUID * _Nonnull)motionProximityUUIDForProximityUUID:(NSUUID * _Nonnull)proximityUUID;
        [Static]
        [Export ("motionProximityUUIDForProximityUUID:")]
        NSUuid GetMotionProximityUUID (NSUuid proximityUUID);
    }

    // typedef void (^SettingIBeaconMotionUUIDEnableCompletionBlock)(SettingIBeaconMotionUUIDEnable * _Nullable, NSError * _Nullable);
    delegate void SettingIBeaconMotionUUIDEnableCompletionBlock ([NullAllowed] SettingIBeaconMotionUUIDEnable ibeaconMotionUUIDEnable, [NullAllowed] NSError error);

    // @interface SettingIBeaconMotionUUIDEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingIBeaconMotionUUIDEnable")]
    interface SettingIBeaconMotionUUIDEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingIBeaconMotionUUIDEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingIBeaconMotionUUIDEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingIBeaconMotionUUIDEnableCompletionBlock completion);
    }


    // typedef void (^ESTSettingNearableIntervalCompletionBlock)(ESTSettingNearableInterval * _Nullable, NSError * _Nullable);
    delegate void SettingNearableIntervalCompletionBlock ([NullAllowed] SettingNearableInterval nearableInterval, [NullAllowed] NSError error);

    // @interface ESTSettingNearableInterval : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingNearableInterval")]
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
    delegate void ESTSettingNearablePowerCompletionBlock ([NullAllowed] SettingNearablePower settingNearablePower, [NullAllowed] NSError error);

    // @interface ESTSettingNearablePower : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingNearablePower")]
    interface SettingNearablePower : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(ESTNearablePower)ower;
        [Export ("initWithValue:")]
        IntPtr Constructor (NearablePower ower);

        // -(ESTNearablePower)getValue;
        [Export ("getValue")]
        NearablePower Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (ESTSettingNearablePowerCompletionBlock completion);

        // -(void)writeValue:(ESTNearablePower)power completion:(ESTSettingNearablePowerCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (NearablePower power, ESTSettingNearablePowerCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(ESTNearablePower)Power;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (NearablePower Power);
    }

    // @interface ESTSettingOperation : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingOperation")]
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
    }

    //// @interface Internal (ESTSettingOperation)
    //[Category]
    //[BaseType (typeof (SettingOperation))]
    //interface SettingOperationExtensions
    //{
    //    // @property (nonatomic, weak) ESTDeviceConnectable * _Nullable device;
    //    [NullAllowed, Export ("device", ArgumentSemantic.Weak)]
    //    DeviceConnectable Device { get; set; }
    //}


    // typedef void (^SettingPowerSmartPowerModeEnableCompletionBlock)(SettingPowerSmartPowerModeEnable * _Nullable, NSError * _Nullable);
    delegate void SettingPowerSmartPowerModeEnableCompletionBlock ([NullAllowed] SettingPowerSmartPowerModeEnable powerSmartPowerModeEnable, [NullAllowed] NSError error);

    // @interface SettingPowerSmartPowerModeEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerSmartPowerModeEnable")]
    interface SettingPowerSmartPowerModeEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)smartPowerModeEnable;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool smartPowerModeEnable);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerSmartPowerModeEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)smartPowerModeEnable completion:(SettingPowerSmartPowerModeEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool smartPowerModeEnable, SettingPowerSmartPowerModeEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)smartPowerModeEnable;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool smartPowerModeEnable);
    }

    // typedef void (^SettingPowerFlipToSleepEnableCompletionBlock)(SettingPowerFlipToSleepEnable * _Nullable, NSError * _Nullable);
    delegate void SettingPowerFlipToSleepEnableCompletionBlock ([NullAllowed] SettingPowerFlipToSleepEnable powerFlipToSleepEnable, [NullAllowed] NSError error);

    // @interface SettingPowerFlipToSleepEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerFlipToSleepEnable")]
    interface SettingPowerFlipToSleepEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerFlipToSleepEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingPowerFlipToSleepEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingPowerFlipToSleepEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool enabled);
    }

    // typedef void (^SettingPowerDarkToSleepEnableCompletionBlock)(SettingPowerDarkToSleepEnable * _Nullable, NSError * _Nullable);
    delegate void SettingPowerDarkToSleepEnableCompletionBlock ([NullAllowed] SettingPowerDarkToSleepEnable powerDarkToSleepEnable, [NullAllowed] NSError error);

    // @interface SettingPowerDarkToSleepEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerDarkToSleepEnable")]
    interface SettingPowerDarkToSleepEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerDarkToSleepEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingPowerDarkToSleepEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingPowerDarkToSleepEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool enabled);
    }

    // typedef void (^SettingPowerBatteryLifetimeCompletionBlock)(SettingPowerBatteryLifetime * _Nullable, NSError * _Nullable);
    delegate void SettingPowerBatteryLifetimeCompletionBlock ([NullAllowed] SettingPowerBatteryLifetime powerBatteryLifetime, [NullAllowed] NSError error);

    // @interface SettingPowerBatteryLifetime : SettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingPowerBatteryLifetime")]
    interface SettingPowerBatteryLifetime : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(NSUInteger)batteryLifetime;
        [Export ("initWithValue:")]
        IntPtr Constructor (nuint batteryLifetime);

        // -(NSUInteger)getValue;
        [Export ("getValue")]
        nuint Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerBatteryLifetimeCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerBatteryLifetimeCompletionBlock completion);
    }

    // typedef void (^SettingPowerMotionOnlyBroadcastingEnableCompletionBlock)(SettingPowerMotionOnlyBroadcastingEnable * _Nullable, NSError * _Nullable);
    delegate void SettingPowerMotionOnlyBroadcastingEnableCompletionBlock ([NullAllowed] SettingPowerMotionOnlyBroadcastingEnable powerMotionOnlyBroadcastingEnable, [NullAllowed] NSError error);

    // @interface SettingPowerMotionOnlyBroadcastingEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerMotionOnlyBroadcastingEnable")]
    interface SettingPowerMotionOnlyBroadcastingEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enabled;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enabled);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enabled completion:(SettingPowerMotionOnlyBroadcastingEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enabled, SettingPowerMotionOnlyBroadcastingEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)enabled;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool enabled);
    }






    // typedef void (^SettingPowerScheduledAdvertisingEnableCompletionBlock)(SettingPowerScheduledAdvertisingEnable * _Nullable, NSError * _Nullable);
    delegate void SettingPowerScheduledAdvertisingEnableCompletionBlock ([NullAllowed] SettingPowerScheduledAdvertisingEnable powerScheduledAdvertisingEnable, [NullAllowed] NSError error);

    // @interface SettingPowerScheduledAdvertisingEnable : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerScheduledAdvertisingEnable")]
    interface SettingPowerScheduledAdvertisingEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)enable;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool enable);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerScheduledAdvertisingEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)enable completion:(SettingPowerScheduledAdvertisingEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool enable, SettingPowerScheduledAdvertisingEnableCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(BOOL)enable;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (bool enable);
    }



    // typedef void (^SettingPowerScheduledAdvertisingPeriodCompletionBlock)(SettingPowerScheduledAdvertisingPeriod * _Nullable, NSError * _Nullable);
    delegate void SettingPowerScheduledAdvertisingPeriodCompletionBlock ([NullAllowed] SettingPowerScheduledAdvertisingPeriod powerScheduledAdvertisingPeriod, [NullAllowed] NSError error);

    // @interface SettingPowerScheduledAdvertisingPeriod : SettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name = "ESTSettingPowerScheduledAdvertisingPeriod")]
    interface SettingPowerScheduledAdvertisingPeriod : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(TimePeriod * _Nonnull)period;
        [Export ("initWithValue:")]
        IntPtr Constructor (TimePeriod period);

        // -(TimePeriod * _Nonnull)getValue;
        [Export ("getValue")]
        TimePeriod Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);

        // -(void)writeValue:(TimePeriod * _Nonnull)period completion:(SettingPowerScheduledAdvertisingPeriodCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (TimePeriod period, SettingPowerScheduledAdvertisingPeriodCompletionBlock completion);

        // +(NSError * _Nullable)validationErrorForValue:(TimePeriod * _Nonnull)period;
        [Static]
        [Export ("validationErrorForValue:")]
        [return: NullAllowed]
        NSError GetValidationError (TimePeriod period);
    }



    // typedef void (^SettingPowerBatteryPercentageCompletionBlock)(SettingPowerBatteryPercentage * _Nullable, NSError * _Nullable);
    delegate void SettingPowerBatteryPercentageCompletionBlock ([NullAllowed] SettingPowerBatteryPercentage powerBatteryPercentage, [NullAllowed] NSError error);

    // @interface SettingPowerBatteryPercentage : SettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name = "ESTSettingPowerBatteryPercentage")]
    interface SettingPowerBatteryPercentage : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(NSUInteger)batteryPercentage;
        [Export ("initWithValue:")]
        IntPtr Constructor (nuint batteryPercentage);

        // -(NSUInteger)getValue;
        [Export ("getValue")]
        nuint Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerBatteryPercentageCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerBatteryPercentageCompletionBlock completion);
    }

    // typedef void (^SettingPowerBatteryVoltageCompletionBlock)(SettingPowerBatteryVoltage * _Nullable, NSError * _Nullable);
    delegate void SettingPowerBatteryVoltageCompletionBlock ([NullAllowed] SettingPowerBatteryVoltage powerBatteryVoltage, [NullAllowed] NSError error);

    // @interface SettingPowerBatteryVoltage : SettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name = "ESTSettingPowerBatteryVoltage")]
    interface SettingPowerBatteryVoltage : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(unsigned short)voltage;
        [Export ("initWithValue:")]
        IntPtr Constructor (ushort voltage);

        // -(unsigned short)getValue;
        [Export ("getValue")]
        ushort Value { get; }

        // -(void)readValueWithCompletion:(SettingPowerBatteryVoltageCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingPowerBatteryVoltageCompletionBlock completion);
    }

    // @interface ESTSettingReadOnly : ESTSettingBase
    [BaseType (typeof (SettingBase), Name="ESTSettingReadOnly")]
    interface SettingReadOnly
    {
        // -(void)readValueWithCompletion:(ESTSettingCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingCompletionBlock completion);
    }

    // @interface ESTSettingReadWrite : ESTSettingReadOnly
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingReadWrite")]
    interface SettingReadWrite
    {
        // -(void)writeValue:(id _Nonnull)value completion:(ESTSettingCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (NSObject value, SettingCompletionBlock completion);
    }

    // @interface ESTSettingsConnectivity : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsConnectivity")]
    interface SettingsConnectivity
    {
        // @property (readonly, nonatomic, strong) ESTSettingConnectivityInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingConnectivityInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingConnectivityPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingConnectivityPower Power { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsDeviceInfo : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsDeviceInfo")]
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
        SettingDeviceInfoUTCTime UtcTime { get; }

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

    // typedef void (^ESTSettingEddystoneConfigurationServiceEnableCompletionBlock)(ESTSettingEddystoneConfigurationServiceEnable * _Nullable, NSError * _Nullable);
    delegate void SettingEddystoneConfigurationServiceEnableCompletionBlock ([NullAllowed] SettingEddystoneConfigurationServiceEnable eddystoneConfigurationServiceEnable, [NullAllowed] NSError error);

    // @interface ESTSettingEddystoneConfigurationServiceEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingEddystoneConfigurationServiceEnable")]
    interface SettingEddystoneConfigurationServiceEnable : INSCopying
    {
        // -(instancetype _Nonnull)initWithValue:(BOOL)eddystoneConfigurationServiceEnable;
        [Export ("initWithValue:")]
        IntPtr Constructor (bool eddystoneConfigServiceEnable);

        // -(BOOL)getValue;
        [Export ("getValue")]
        bool Value { get; }

        // -(void)readValueWithCompletion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingEddystoneConfigurationServiceEnableCompletionBlock completion);

        // -(void)writeValue:(BOOL)eddystoneConfigurationServiceEnable completion:(ESTSettingEddystoneConfigurationServiceEnableCompletionBlock _Nonnull)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (bool eddystoneConfigServiceEnable, SettingEddystoneConfigurationServiceEnableCompletionBlock completion);
    }

    // @interface ESTSettingsEddystoneUID : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEddystoneUID")]
    interface SettingsEddystoneUID
    {
        // @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDEnable * _Nonnull enable;
        [Export ("enable", ArgumentSemantic.Strong)]
        SettingEddystoneUIDEnable Enable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDNamespace * _Nonnull namespaceID;
        [Export ("namespaceID", ArgumentSemantic.Strong)]
        SettingEddystoneUIDNamespace NamespaceID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDInstance * _Nonnull instanceID;
        [Export ("instanceID", ArgumentSemantic.Strong)]
        SettingEddystoneUIDInstance InstanceID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingEddystoneUIDInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneUIDPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingEddystoneUIDPower Power { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsEddystoneURL : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEddystoneURL")]
    interface SettingsEddystoneURL
    {
        // @property (readonly, nonatomic, strong) ESTSettingEddystoneURLEnable * _Nonnull enable;
        [Export ("enable", ArgumentSemantic.Strong)]
        SettingEddystoneURLEnable Enable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneURLInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingEddystoneURLInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneURLPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingEddystoneURLPower Power { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneURLData * _Nonnull URLData;
        [Export ("URLData", ArgumentSemantic.Strong)]
        SettingEddystoneURLData URLData { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsEddystoneTLM : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEddystoneTLM")]
    interface SettingsEddystoneTLM
    {
        // @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMEnable * _Nonnull enable;
        [Export ("enable", ArgumentSemantic.Strong)]
        SettingEddystoneTLMEnable Enable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingEddystoneTLMInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneTLMPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingEddystoneTLMPower Power { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsEddystoneEID : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEddystoneEID")]
    interface SettingsEddystoneEID
    {
        // @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDEnable * _Nonnull enable;
        [Export ("enable", ArgumentSemantic.Strong)]
        SettingEddystoneEIDEnable Enable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingEddystoneEIDInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEddystoneEIDPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingEddystoneEIDPower Power { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTTelemetryInfo : NSObject
    [BaseType (typeof (NSObject), Name="ESTTelemetryInfo")]
    interface TelemetryInfo
    {
        // @property (readonly, nonatomic, strong) NSString * shortIdentifier;
        [Export ("shortIdentifier", ArgumentSemantic.Strong)]
        string ShortIdentifier { get; }

        // -(instancetype)initWithShortIdentifier:(NSString *)shortIdentifier;
        [Export ("initWithShortIdentifier:")]
        IntPtr Constructor (string shortIdentifier);
    }

    // typedef void (^ESTSettingSensorsAmbientLightCompletionBlock)(ESTSettingSensorsAmbientLight * _Nullable, NSError * _Nullable);
    delegate void SettingSensorsAmbientLightCompletionBlock ([NullAllowed] SettingSensorsAmbientLight sensorsAmbientLight, [NullAllowed] NSError error);

    // @interface ESTSettingSensorsAmbientLight : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingSensorsAmbientLight")]
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
    delegate void SettingSensorsMotionNotificationEnableCompletionBlock ([NullAllowed] SettingSensorsMotionNotificationEnable sensorsMotionNotificationEnable, [NullAllowed] NSError error);

    // @interface ESTSettingSensorsMotionNotificationEnable : ESTSettingReadWrite <NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingSensorsMotionNotificationEnable")]
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
        NSError GetValidationErrorForValue (bool enabled);
    }

    // typedef void (^ESTSettingSensorsTemperatureCompletionBlock)(ESTSettingSensorsTemperature * _Nullable, NSError * _Nullable);
    delegate void SettingSensorsTemperatureCompletionBlock ([NullAllowed] SettingSensorsTemperature sensorsTemperature, [NullAllowed] NSError error);

    // @interface ESTSettingSensorsTemperature : ESTSettingReadOnly <NSCopying>
    [BaseType (typeof (SettingReadOnly), Name="ESTSettingSensorsTemperature")]
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





    // @interface ESTTelemetryInfoMotion : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name="ESTTelemetryInfoMotion")]
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

    // @interface ESTTelemetryInfoAmbientLight : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name="ESTTelemetryInfoAmbientLight")]
    interface TelemetryInfoAmbientLight
    {
        // @property (readonly, nonatomic, strong) NSNumber * _Nonnull ambientLightLevelInLux;
        [Export ("ambientLightLevelInLux", ArgumentSemantic.Strong)]
        NSNumber AmbientLightLevelInLux { get; }

        // -(instancetype _Nonnull)initWithAmbientLightLevelInLux:(NSNumber * _Nonnull)ambientLightLevelInLux shortIdentifier:(NSString * _Nonnull)shortIdentifier;
        [Export ("initWithAmbientLightLevelInLux:shortIdentifier:")]
        IntPtr Constructor (NSNumber ambientLightLevelInLux, string shortIdentifier);
    }

    // @interface ESTTelemetryInfoTemperature : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name="ESTTelemetryInfoTemperature")]
    interface TelemetryInfoTemperature
    {
        // @property (readonly, nonatomic, strong) NSNumber * _Nonnull temperatureInCelsius;
        [Export ("temperatureInCelsius", ArgumentSemantic.Strong)]
        NSNumber TemperatureInCelsius { get; }

        // -(instancetype _Nonnull)initWithTemperature:(NSNumber * _Nonnull)temperature shortIdentifier:(NSString * _Nonnull)shortIdentifier;
        [Export ("initWithTemperature:shortIdentifier:")]
        IntPtr Constructor (NSNumber temperature, string shortIdentifier);
    }


    // @interface ESTTelemetryInfoSystemStatus : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name="ESTTelemetryInfoSystemStatus")]
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


    // @interface ESTTelemetryInfoGPIO : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name="ESTTelemetryInfoGPIO")]
    interface TelemetryInfoGPIO
    {
        // @property (readonly, nonatomic, strong) ESTGPIOPortsData * _Nonnull portsData;
        [Export ("portsData", ArgumentSemantic.Strong)]
        GPIOPortsData PortsData { get; }

        // -(instancetype _Nonnull)initWithGPIOPortsData:(ESTGPIOPortsData * _Nonnull)portsData shortIdentifier:(NSString * _Nonnull)shortIdentifier;
        [Export ("initWithGPIOPortsData:shortIdentifier:")]
        IntPtr Constructor (GPIOPortsData portsData, string shortIdentifier);
    }


    // @interface ESTTelemetryInfoMagnetometer : ESTTelemetryInfo
    [BaseType (typeof (TelemetryInfo), Name = "ESTTelemetryInfoMagnetometer")]
    interface TelemetryInfoMagnetometer
    {
        //@property (nonatomic, strong, readonly) NSNumber* normalizedMagneticFieldX;
        [Export ("normalizedMagneticFieldX", ArgumentSemantic.Strong)]
        NSNumber NormalizedMagneticFieldX { get; }

        //@property (nonatomic, strong, readonly) NSNumber* normalizedMagneticFieldY;
        [Export ("normalizedMagneticFieldY", ArgumentSemantic.Strong)]
        NSNumber NormalizedMagneticFieldY { get; }

        //@property (nonatomic, strong, readonly) NSNumber* normalizedMagneticFieldZ;
        [Export ("normalizedMagneticFieldZ", ArgumentSemantic.Strong)]
        NSNumber NormalizedMagneticFieldZ { get; }

        //- (instancetype)initWithNormalizedMagneticFieldX:(NSNumber*)fieldX
        //                       normalizedMagneticFieldY:(NSNumber*)fieldY
        //                      normalizedMagneticFieldZ:(NSNumber*)fieldZ
        //                              shortIdentifier:(NSString*)shortIdentifier;
        [Export ("initWithNormalizedMagneticFieldX:normalizedMagneticFieldY:normalizedMagneticFieldZ:shortIdentifier:")]
        IntPtr Constructor (NSNumber normalizedFieldX, NSNumber normalizedFieldY, NSNumber normalizedFieldZ, string shortIdentifier);
    }


    interface ITelemetryNotificationProtocol { }
    // @protocol ESTTelemetryNotificationProtocol <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationProtocol")]
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


    // typedef void (^ESTTelemetryNotificationMotionCompletionBlock)(ESTTelemetryInfoMotion * _Nonnull);
    delegate void TelemetryNotificationMotionCompletionBlock (TelemetryInfoMotion telemetryInfoMotion);

    // @interface ESTTelemetryNotificationMotion : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationMotion")]
    interface TelemetryNotificationMotion : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationMotionCompletionBlock _Nonnull)block;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationMotionCompletionBlock block);
    }


    // typedef void (^ESTTelemetryNotificationAmbientLightNotificationBlock)(ESTTelemetryInfoAmbientLight * _Nonnull);
    delegate void TelemetryNotificationAmbientLightNotificationBlock (TelemetryInfoAmbientLight telemetryInfoAmbientLight);

    // @interface ESTTelemetryNotificationAmbientLight : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationAmbientLight")]
    interface TelemetryNotificationAmbientLight : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationAmbientLightNotificationBlock _Nonnull)block;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationAmbientLightNotificationBlock block);
    }


    // typedef void (^ESTTelemetryNotificationTemperatureNotificationBlock)(ESTTelemetryInfoTemperature * _Nonnull);
    delegate void TelemetryNotificationTemperatureNotificationBlock (TelemetryInfoTemperature telemetryInfoTemperature);

    // @interface ESTTelemetryNotificationTemperature : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationTemperature")]
    interface TelemetryNotificationTemperature : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationTemperatureNotificationBlock _Nonnull)block;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationTemperatureNotificationBlock block);
    }


    // typedef void (^ESTTelemetryNotificationSystemStatusNotificationBlock)(ESTTelemetryInfoSystemStatus * _Nonnull);
    delegate void TelemetryNotificationSystemStatusNotificationBlock (TelemetryInfoSystemStatus telemetryInfoSystemStatus);

    // @interface ESTTelemetryNotificationSystemStatus : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationSystemStatus")]
    interface ESTTelemetryNotificationSystemStatus : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationSystemStatusNotificationBlock _Nonnull)block;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationSystemStatusNotificationBlock block);
    }

    // typedef void (^ESTTelemetryNotificationMagnetometerNotificationBlock)(ESTTelemetryInfoMagnetometer * _Nonnull);
    delegate void TelemetryNotificationMagnetometerNotificationBlock (TelemetryInfoMagnetometer telemetryInfoMagnetometer);

    // @interface ESTTelemetryNotificationMagnetometer : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationMagnetometer")]
    interface TelemetryNotificationMagnetometer : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationMagnetometerNotificationBlock _Nonnull)block;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationMagnetometerNotificationBlock block);
    }


    // typedef void (^ESTTelemetryNotificationGPIONotificationBlock)(ESTTelemetryInfoGPIO * _Nonnull);
    delegate void TelemetryNotificationGPIONotificationBlock (TelemetryInfoGPIO telemetryInfoGPIO);

    // @interface ESTTelemetryNotificationGPIO : NSObject <ESTTelemetryNotificationProtocol>
    [BaseType (typeof (NSObject), Name="ESTTelemetryNotificationGPIO")]
    interface TelemetryNotificationGPIO : TelemetryNotificationProtocol
    {
        // -(instancetype _Nonnull)initWithNotificationBlock:(ESTTelemetryNotificationGPIONotificationBlock _Nonnull)notificationBlock;
        [Export ("initWithNotificationBlock:")]
        IntPtr Constructor (TelemetryNotificationGPIONotificationBlock notificationBlock);
    }

    // @interface ESTTime : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name="ESTTime")]
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
    [BaseType (typeof (NSObject), Name="ESTTimePeriod")]
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

    // @interface ESTSettingsPower : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsPower")]
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

        //// @property (readonly, nonatomic, strong) ESTSettingPowerDarkToSleepThreshold * _Nonnull darkToSleepThreshold;
        //[Export ("darkToSleepThreshold", ArgumentSemantic.Strong)]
        //SettingPowerDarkToSleepThreshold DarkToSleepThreshold { get; }

        // @property (readonly, nonatomic, strong) ESTSettingPowerMotionOnlyBroadcastingEnable * _Nonnull motionOnlyBroadcastingEnable;
        [Export ("motionOnlyBroadcastingEnable", ArgumentSemantic.Strong)]
        SettingPowerMotionOnlyBroadcastingEnable MotionOnlyBroadcastingEnable { get; }

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

    // @interface ESTSettingsIBeacon : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsIBeacon")]
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
        SettingIBeaconProximityUUID ProximityUUID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconMotionUUIDEnable * _Nonnull motionUUIDEnable;
        [Export ("motionUUIDEnable", ArgumentSemantic.Strong)]
        SettingIBeaconMotionUUIDEnable MotionUUIDEnable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconMotionUUID * _Nonnull motionUUID;
        [Export ("motionUUID", ArgumentSemantic.Strong)]
        SettingIBeaconMotionUUID MotionUUID { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconMajor * _Nonnull major;
        [Export ("major", ArgumentSemantic.Strong)]
        SettingIBeaconMajor Major { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconMinor * _Nonnull minor;
        [Export ("minor", ArgumentSemantic.Strong)]
        SettingIBeaconMinor Minor { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconSecureUUIDPeriodScaler * _Nonnull secureUUIDPeriodScaler;
        [Export ("secureUUIDPeriodScaler", ArgumentSemantic.Strong)]
        SettingIBeaconSecureUUIDPeriodScaler SecureUUIDPeriodScaler { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconSecureUUIDEnable * _Nonnull secureUUIDEnable;
        [Export ("secureUUIDEnable", ArgumentSemantic.Strong)]
        SettingIBeaconSecureUUIDEnable SecureUUIDEnable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingIBeaconNonStrictMode * _Nonnull nonStrictModeEnable;
        [Export ("nonStrictModeEnable", ArgumentSemantic.Strong)]
        SettingIBeaconNonStrictMode NonStrictModeEnable { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsEstimoteLocation : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEstimoteLocation")]
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

    // @interface ESTSettingsEstimoteTLM : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEstimoteTLM")]
    interface SettingsEstimoteTLM
    {
        // @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMEnable * _Nonnull enable;
        [Export ("enable", ArgumentSemantic.Strong)]
        SettingEstimoteTLMEnable Enable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMInterval * _Nonnull interval;
        [Export ("interval", ArgumentSemantic.Strong)]
        SettingEstimoteTLMInterval Interval { get; }

        // @property (readonly, nonatomic, strong) ESTSettingEstimoteTLMPower * _Nonnull power;
        [Export ("power", ArgumentSemantic.Strong)]
        SettingEstimoteTLMPower Power { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }


    // @interface ESTSettingsGPIO : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsGPIO")]
    interface SettingsGPIO
    {
        // @property (readonly, nonatomic, strong) ESTSettingGPIONotificationEnable * _Nonnull notificationEnable;
        [Export ("notificationEnable", ArgumentSemantic.Strong)]
        SettingGPIONotificationEnable NotificationEnable { get; }

        // @property (readonly, nonatomic, strong) ESTSettingGPIOConfigPort0 * _Nonnull configPort0;
        [Export ("configPort0", ArgumentSemantic.Strong)]
        SettingGPIOConfigPort0 ConfigPort0 { get; }

        // @property (readonly, nonatomic, strong) ESTSettingGPIOConfigPort1 * _Nonnull configPort1;
        [Export ("configPort1", ArgumentSemantic.Strong)]
        SettingGPIOConfigPort1 ConfigPort1 { get; }

        // @property (readonly, nonatomic, strong) ESTSettingGPIOPortsData * _Nonnull portsData;
        [Export ("portsData", ArgumentSemantic.Strong)]
        SettingGPIOPortsData PortsData { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsSensors : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsSensors")]
    interface SettingsSensors
    {
        // @property (readonly, nonatomic, strong) ESTSettingSensorsAmbientLight * _Nonnull ambientLight;
        [Export ("ambientLight", ArgumentSemantic.Strong)]
        SettingSensorsAmbientLight AmbientLight { get; }

        // @property (readonly, nonatomic, strong) ESTSettingSensorsTemperature * _Nonnull temperature;
        [Export ("temperature", ArgumentSemantic.Strong)]
        SettingSensorsTemperature Temperature { get; }

        // @property (readonly, nonatomic, strong) ESTSettingSensorsMotionNotificationEnable * _Nonnull motionNotificationEnable;
        [Export ("motionNotificationEnable", ArgumentSemantic.Strong)]
        SettingSensorsMotionNotificationEnable MotionNotificationEnable { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // @interface ESTSettingsEddystoneConfigurationService : NSObject
    [BaseType (typeof (NSObject), Name="ESTSettingsEddystoneConfigurationService")]
    interface SettingsEddystoneConfigurationService
    {
        // @property (readonly, nonatomic) ESTSettingEddystoneConfigurationServiceEnable * _Nonnull enabled;
        [Export ("enabled")]
        SettingEddystoneConfigurationServiceEnable Enabled { get; }

        // -(instancetype _Nonnull)initWithSettingsCollection:(ESTDeviceSettingsCollection * _Nonnull)settingsCollection;
        [Export ("initWithSettingsCollection:")]
        IntPtr Constructor (DeviceSettingsCollection settingsCollection);
    }

    // typedef void (^ESTRequestBlock)(id _Nullable, NSError * _Nullable);
    delegate void RequestBlock ([NullAllowed] NSObject value, [NullAllowed] NSError error);

    // @protocol ESTRequestBaseDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="ESTRequestBaseDelegate")]
    interface RequestBaseDelegate
    {
        // @required -(void)request:(ESTRequestBase * _Nonnull)request didFinishLoadingWithResposne:(id _Nullable)response;
        [Abstract]
        [Export ("request:didFinishLoadingWithResposne:")]
        void DidFinishLoading (RequestBase request, [NullAllowed] NSObject response);

        // @required -(void)request:(ESTRequestBase * _Nonnull)request didFailLoadingWithError:(NSError * _Nonnull)error;
        [Abstract]
        [Export ("request:didFailLoadingWithError:")]
        void DidFailLoading (RequestBase request, NSError error);
    }

    // @interface ESTRequestBase : NSObject <NSURLConnectionDelegate, NSURLConnectionDataDelegate>
    [BaseType (typeof (NSObject), Name="ESTRequestBase")]
    interface RequestBase : INSUrlConnectionDelegate, INSUrlConnectionDataDelegate
    {
        [Wrap ("WeakDelegate")]
        [NullAllowed]
        RequestBaseDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<ESTRequestBaseDelegate> _Nullable delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

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
        NSMutableUrlRequest CreateRequestWithUrl (string url);

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
        void ParseErrorWithCode (RequestBaseError errorCode, [NullAllowed] string reason);

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
        bool IsEqualToRequest (RequestBase request);
    }

    // @interface ESTRequestGetJSON : ESTRequestBase
    [BaseType (typeof (RequestBase), Name="ESTRequestGetJSON")]
    interface RequestGetJSON
    {
    }


    // @interface ESTRequestPostJSON : ESTRequestBase
    [BaseType (typeof (RequestBase), Name="ESTRequestPostJSON")]
    interface RequestPostJSON
    {
        // - (void)setParams:(id)params forRequest:(NSMutableURLRequest*)request;
        [Export ("setParams:forRequest:")]
        void SetParams (IntPtr parameters, NSMutableUrlRequest request); 
    }

    // @interface ESTNearableOperationBroadcastingScheme : ESTSettingOperation<ESTNearableOperationProtocol>

    [BaseType (typeof (SettingOperation), Name="ESTNearableOperationBroadcastingScheme")]
    interface NearableOperationBroadcastingScheme : NearableOperationProtocol
    {
        // + (instancetype)readOperationWithCompletion:(ESTSettingNearableBroadcastingSchemeCompletionBlock)completion;
        [Static]
        [Export ("readOperationWithCompletion:"), Async]
        NearableOperationBroadcastingScheme ReadOperation (SettingNearableBroadcastingSchemeCompletionBlock completion);

        // + (instancetype)writeOperationWithSetting:(ESTSettingNearableBroadcastingScheme*)setting completion:(ESTSettingNearableBroadcastingSchemeCompletionBlock)completion;
        [Static]
        [Export ("writeOperationWithSetting:completion:"), Async]
        NearableOperationBroadcastingScheme WriteOperation (SettingNearableBroadcastingScheme setting, SettingNearableBroadcastingSchemeCompletionBlock completion);
    }


    // typedef void(^ESTRequestV2GetDevicesPendingBlock)(NSArray<NSString*>* _Nullable result, NSError* _Nullable error);
    delegate void RequestV2GetDevicesPendingBlock ([NullAllowed]IntPtr result, [NullAllowed]NSError error);

    // @interface ESTRequestV2GetDevicesPending : ESTRequestGetJSON
    [BaseType (typeof (RequestGetJSON), Name="ESTRequestV2GetDevicesPending")]
    interface RequestV2GetDevicesPending
    {
        // - (void)sendRequestWithCompletion:(ESTRequestV2GetDevicesPendingBlock)completion;
        [Export ("sendRequestWithCompletion:"), Async]
        void SendRequest (RequestV2GetDevicesPendingBlock completion);
    }

    // typedef void(^ESTSettingNearableBroadcastingSchemeCompletionBlock)(ESTSettingNearableBroadcastingScheme* _Nullable broadcastingSchemeSetting, NSError* _Nullable error);
    delegate void SettingNearableBroadcastingSchemeCompletionBlock ([NullAllowed]SettingNearableBroadcastingScheme broadcastingSchemeSetting, [NullAllowed]NSError error);

    // @interface ESTSettingNearableBroadcastingScheme : ESTSettingReadWrite<NSCopying>
    [BaseType (typeof (SettingReadWrite), Name="ESTSettingNearableBroadcastingScheme")]
    interface SettingNearableBroadcastingScheme : INSCopying
    {
        // - (instancetype)initWithValue:(ESTNearableBroadcastingScheme)broadcastingScheme;
        [Export ("initWithValue:")]
        IntPtr Constructor (NearableBroadcastingScheme broadcastingScheme);

        // - (ESTNearableBroadcastingScheme)getValue;
        [Export ("getValue")]
        NearableBroadcastingScheme Value { get; }

        // - (void)readValueWithCompletion:(ESTSettingNearableBroadcastingSchemeCompletionBlock)completion;
        [Export ("readValueWithCompletion:"), Async]
        void ReadValue (SettingNearableBroadcastingSchemeCompletionBlock completion);

        // - (void)writeValue:(ESTNearableBroadcastingScheme)broadcastingScheme completion:(ESTSettingNearableBroadcastingSchemeCompletionBlock)completion;
        [Export ("writeValue:completion:"), Async]
        void WriteValue (NearableBroadcastingScheme broadcastingScheme, SettingNearableBroadcastingSchemeCompletionBlock completion);

        // + (NSError* _Nullable)validationErrorForValue:(ESTNearableBroadcastingScheme)broadcastingScheme;
        [Static]
        [return: NullAllowed]
        [Export ("validationErrorForValue:")]
        NSError ValidationError (NearableBroadcastingScheme broadcastingScheme);

    }

}