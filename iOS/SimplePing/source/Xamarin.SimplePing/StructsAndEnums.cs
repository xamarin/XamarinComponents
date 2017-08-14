using System;
using Foundation;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Xamarin.SimplePing
{
	[Native]
	public enum SimplePingAddressStyle : long
	{
		Any,
		ICMPv4,
		ICMPv6
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ICMPHeader
	{
		public byte Type;
		public byte Code;
		public ushort Checksum;
		public ushort Identifier;
		public ushort SequenceNumber;
	}

	public enum ICMPv4Type : uint
	{
		EchoRequest = 8,
		EchoReply = 0
	}

	public enum ICMPv6Type : uint
	{
		EchoRequest = 128,
		EchoReply = 129
	}
}
