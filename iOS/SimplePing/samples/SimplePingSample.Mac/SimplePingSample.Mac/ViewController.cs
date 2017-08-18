using System;
using System.Net;
using AppKit;
using Foundation;

using Xamarin.SimplePing;

namespace SimplePingSample.Mac
{
	public partial class ViewController : NSViewController
	{
		public const string HostName = "www.microsoft.com";

		private SimplePing pinger;
		private NSTimer sendTimer;

		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = HostName;
		}

		/// <summary>
		/// Called by the table view selection delegate callback to start the ping.
		/// </summary>
		public void Start(bool forceIPv4, bool forceIPv6)
		{
			PingerWillStart();

			Console.WriteLine("start");

			pinger = new SimplePing(HostName);

			// By default we use the first IP address we get back from host resolution (.Any) 
			// but these flags let the user override that.

			if (forceIPv4 && !forceIPv6)
			{
				pinger.AddressStyle = SimplePingAddressStyle.ICMPv4;
			}
			else if (forceIPv6 && !forceIPv4)
			{
				pinger.AddressStyle = SimplePingAddressStyle.ICMPv6;
			}

			pinger.Started += OnStarted;
			pinger.Failed += OnFailed;
			pinger.Sent += OnSent;
			pinger.SendFailed += OnSendFailed;
			pinger.ResponseRecieved += OnResponseRecieved;
			pinger.UnexpectedResponse += OnUnexpectedResponse;

			pinger.Start();
		}

		/// <summary>
		/// Called by the table view selection delegate callback to stop the ping.
		/// </summary>
		public void Stop()
		{
			Console.WriteLine("stop");

			pinger?.Stop();
			pinger = null;

			sendTimer?.Invalidate();
			sendTimer = null;

			PingerDidStop();
		}

		/// <summary>
		/// Sends a ping.
		/// 
		/// Called to send a ping, both directly (as soon as the SimplePing object starts up) and 
		/// via a timer (to continue sending pings periodically).
		/// </summary>
		public void SendPing()
		{
			pinger?.SendPing(null);
		}

		// pinger delegate callbacks

		private void OnStarted(object sender, SimplePingStartedEventArgs e)
		{
			Console.WriteLine("pinging " + GetDisplayAddress(e.EndPoint));

			// Send the first ping straight away.

			SendPing();

			// And start a timer to send the subsequent pings.

			sendTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(1.0), t => SendPing());
		}

		private void OnFailed(object sender, SimplePingFailedEventArgs e)
		{
			Console.WriteLine("failed: " + GetShortError(e.Error));

			Stop();
		}

		private void OnSent(object sender, SimplePingSentEventArgs e)
		{
			Console.WriteLine(e.SequenceNumber + " sent");
		}

		private void OnSendFailed(object sender, SimplePingSendFailedEventArgs e)
		{
			Console.WriteLine(e.SequenceNumber + " send failed: " + GetShortError(e.Error));
		}

		private void OnResponseRecieved(object sender, SimplePingResponseRecievedEventArgs e)
		{
			Console.WriteLine(e.SequenceNumber + " received, size = " + e.Packet.Length);
		}

		private void OnUnexpectedResponse(object sender, SimplePingUnexpectedResponseEventArgs e)
		{
			Console.WriteLine("unexpected packet received, size = " + e.Packet.Length);
		}

		// utilities

		/// <summary>
		/// Returns the string representation of the supplied address.
		/// </summary>
		/// <returns>A string representation of that address.</returns>
		/// <param name="endpoint">Contains the address to render.</param>
		private string GetDisplayAddress(IPEndPoint endpoint)
		{
			var entry = Dns.GetHostEntry(endpoint.Address);
			return entry?.HostName ?? "?";
		}

		/// <summary>
		/// Returns a short error string for the supplied error.
		/// </summary>
		/// <returns>A short string representing that error.</returns>
		/// <param name="error">The error to render.</param>
		private string GetShortError(NSError error)
		{
			var result = error.LocalizedFailureReason;
			if (!string.IsNullOrEmpty(result))
			{
				return result;
			}
			return error.LocalizedDescription;
		}

		private void PingerWillStart()
		{
			startStopButton.Title = "Stop…";
		}

		private void PingerDidStop()
		{
			startStopButton.Title = "Start…";
		}

		partial void startStopClicked(NSObject sender)
		{
			if (pinger == null)
			{
				var forceIPv4 = forceIPv4Check.State == NSCellStateValue.On;
				var forceIPv6 = forceIPv6Check.State == NSCellStateValue.On;

				Start(forceIPv4, forceIPv6);
			}
			else
			{
				Stop();
			}
		}
	}
}
