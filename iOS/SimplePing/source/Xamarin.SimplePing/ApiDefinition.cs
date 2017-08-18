using System;
using Foundation;
using ObjCRuntime;

namespace Xamarin.SimplePing
{
	// @interface SimplePing : NSObject
	[BaseType(typeof(NSObject), Name = "SimplePing", Delegates = new string[] { "Delegate" }, Events = new Type[] { typeof(SimplePingDelegate) })]
	[DisableDefaultCtor]
	interface SimplePing
	{
		// -(instancetype _Nonnull)initWithHostName:(NSString * _Nonnull)hostName __attribute__((objc_designated_initializer));
		[Export("initWithHostName:")]
		[DesignatedInitializer]
		IntPtr Constructor(string hostName);

		// @property (readonly, copy, nonatomic) NSString * _Nonnull hostName;
		[Export("hostName")]
		string HostName { get; }

		// @property (readwrite, nonatomic, weak) id<SimplePingDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		ISimplePingDelegate Delegate { get; set; }

		// @property (assign, readwrite, nonatomic) SimplePingAddressStyle addressStyle;
		[Export("addressStyle", ArgumentSemantic.Assign)]
		SimplePingAddressStyle AddressStyle { get; set; }

		// @property (readonly, copy, nonatomic) NSData * _Nullable hostAddress;
		[NullAllowed, Export("hostAddress", ArgumentSemantic.Copy)]
		NSData HostAddress { get; }

		// @property (readonly, assign, nonatomic) sa_family_t hostAddressFamily;
		[Export("hostAddressFamily")]
		byte HostAddressFamily { get; }

		// @property (readonly, assign, nonatomic) uint16_t identifier;
		[Export("identifier")]
		ushort Identifier { get; }

		// @property (readonly, assign, nonatomic) uint16_t nextSequenceNumber;
		[Export("nextSequenceNumber")]
		ushort NextSequenceNumber { get; }

		// -(void)start;
		[Export("start")]
		void Start();

		// -(void)sendPingWithData:(NSData * _Nullable)data;
		[Export("sendPingWithData:")]
		void SendPing([NullAllowed] NSData data);

		// -(void)stop;
		[Export("stop")]
		void Stop();
	}

	interface ISimplePingDelegate { }

	// @protocol SimplePingDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface SimplePingDelegate
	{
		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didStartWithAddress:(NSData * _Nonnull)address;
		[Export("simplePing:didStartWithAddress:")]
		[EventArgs("SimplePingStarted")]
		[EventName("Started")]
		void DidStart(SimplePing pinger, NSData address);

		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didFailWithError:(NSError * _Nonnull)error;
		[Export("simplePing:didFailWithError:")]
		[EventArgs("SimplePingFailed")]
		[EventName("Failed")]
		void DidFail(SimplePing pinger, NSError error);

		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didSendPacket:(NSData * _Nonnull)packet sequenceNumber:(uint16_t)sequenceNumber;
		[Export("simplePing:didSendPacket:sequenceNumber:")]
		[EventArgs("SimplePingSent")]
		[EventName("Sent")]
		void DidSendPacket(SimplePing pinger, NSData packet, ushort sequenceNumber);

		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didFailToSendPacket:(NSData * _Nonnull)packet sequenceNumber:(uint16_t)sequenceNumber error:(NSError * _Nonnull)error;
		[Export("simplePing:didFailToSendPacket:sequenceNumber:error:")]
		[EventArgs("SimplePingSendFailed")]
		[EventName("SendFailed")]
		void DidFailToSendPacket(SimplePing pinger, NSData packet, ushort sequenceNumber, NSError error);

		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didReceivePingResponsePacket:(NSData * _Nonnull)packet sequenceNumber:(uint16_t)sequenceNumber;
		[Export("simplePing:didReceivePingResponsePacket:sequenceNumber:")]
		[EventArgs("SimplePingResponseRecieved")]
		[EventName("ResponseRecieved")]
		void DidReceivePingResponsePacket(SimplePing pinger, NSData packet, ushort sequenceNumber);

		// @optional -(void)simplePing:(SimplePing * _Nonnull)pinger didReceiveUnexpectedPacket:(NSData * _Nonnull)packet;
		[Export("simplePing:didReceiveUnexpectedPacket:")]
		[EventArgs("SimplePingUnexpectedResponse")]
		[EventName("UnexpectedResponse")]
		void DidReceiveUnexpectedPacket(SimplePing pinger, NSData packet);
	}
}
