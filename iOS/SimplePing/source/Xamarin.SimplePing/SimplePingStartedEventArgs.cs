using System.Net;

namespace Xamarin.SimplePing
{
	partial class SimplePingStartedEventArgs
	{
		public IPEndPoint EndPoint => SimplePing.EndPointFromAddressPtr(Address);
	}
}
