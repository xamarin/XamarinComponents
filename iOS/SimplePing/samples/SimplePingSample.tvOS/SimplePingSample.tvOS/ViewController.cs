using System;
using System.Net;
using Foundation;
using UIKit;

using Xamarin.SimplePing;

namespace SimplePingSample.tvOS
{
	public partial class ViewController : UIViewController
	{
		public const string HostName = "www.microsoft.com";

		private SimplePing pinger;

		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			pinger = new SimplePing(HostName);

			// By default we use the first IP address we get back from host resolution (.Any) 
			// but these flags let the user override that.

			pinger.Started += OnStarted;
			pinger.Failed += OnFailed;
			pinger.Sent += OnSent;
			pinger.SendFailed += OnSendFailed;
			pinger.ResponseRecieved += OnResponseRecieved;
			pinger.UnexpectedResponse += OnUnexpectedResponse;

			pinger?.Start();
		}

		// pinger delegate callbacks

		private void OnStarted(object sender, SimplePingStartedEventArgs e)
		{
			statusLabel.Text = "pinging " + GetDisplayAddress(e.EndPoint);

			// Send the first ping straight away.

			pinger?.SendPing(null);
		}

		private void OnFailed(object sender, SimplePingFailedEventArgs e)
		{
			statusLabel.Text = "failed: " + GetShortError(e.Error);

			pinger.Stop();
		}

		private void OnSent(object sender, SimplePingSentEventArgs e)
		{
			statusLabel.Text = e.SequenceNumber + " sent";
		}

		private void OnSendFailed(object sender, SimplePingSendFailedEventArgs e)
		{
			statusLabel.Text = e.SequenceNumber + " send failed: " + GetShortError(e.Error);
		}

		private void OnResponseRecieved(object sender, SimplePingResponseRecievedEventArgs e)
		{
			statusLabel.Text = e.SequenceNumber + " received, size = " + e.Packet.Length;
		}

		private void OnUnexpectedResponse(object sender, SimplePingUnexpectedResponseEventArgs e)
		{
			statusLabel.Text = "unexpected packet received, size = " + e.Packet.Length;
		}

		private string GetShortError(NSError error)
		{
			var result = error.LocalizedFailureReason;
			if (!string.IsNullOrEmpty(result))
			{
				return result;
			}
			return error.LocalizedDescription;
		}

		private string GetDisplayAddress(IPEndPoint endpoint)
		{
			var entry = Dns.GetHostEntry(endpoint.Address);
			return entry?.HostName ?? "?";
		}
	}
}

