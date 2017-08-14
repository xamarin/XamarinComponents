using System;
using System.Net;
using System.Runtime.InteropServices;
using Foundation;

namespace Xamarin.SimplePing
{
	partial class SimplePing
	{
		public IPEndPoint HostEndPoint => EndPointFromAddressPtr(HostAddress);

		internal static IPEndPoint EndPointFromAddressPtr(NSData address)
		{
			var buffer = new byte[address.Length];
			Marshal.Copy(address.Bytes, buffer, 0, buffer.Length);

			if (buffer[1] == 30)
			{
				// AF_INET6

				int port = (buffer[2] << 8) + buffer[3];
				var bytes = new byte[16];
				Buffer.BlockCopy(buffer, 8, bytes, 0, 16);
				return new IPEndPoint(new IPAddress(bytes), port);
			}
			else if (buffer[1] == 2)
			{
				// AF_INET

				int port = (buffer[2] << 8) + buffer[3];
				var bytes = new byte[4];
				Buffer.BlockCopy(buffer, 4, bytes, 0, 4);
				return new IPEndPoint(new IPAddress(bytes), port);
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}
