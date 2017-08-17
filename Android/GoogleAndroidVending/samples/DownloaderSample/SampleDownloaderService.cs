using Android.App;

using Google.Android.Vending.Expansion.Downloader;

namespace DownloaderSample
{
	[Service]
	public class SampleDownloaderService : DownloaderService
	{
		/// <summary>
		/// This public key comes from your Android Market publisher account, and it
		/// used by the LVL to validate responses from Market on your behalf.
		/// Note: MODIFY FOR YOUR APPLICATION!
		/// </summary>
		public override string PublicKey => "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA96TCUr/Rhx/fcIVcCrWTz0FKvI+hZICb/yXaxNPhSWeo7TROB+Op5wKhdmjsaSvbi/v75RgyikS/HrSKvQCqwix6b3IgjIu8iGYYZz2ieoFMVt39WFP20fSfjNoBr0KJOsoIAso6zF845ZtIE+3vJFg4z/tTe/jPgi73AYJS6RnUO2pC2tzeGVe+TQemhPUfFWAczunpAoT8ioBCYzK1FzTc1uyAFMh8riijrKDXbQd42nByJq3SSjJiyx/5pcMMj2kWvuJjD5ugk0X10jEfwptVQytXOAvMPhbyvJ2yNN6Ha9ZUHIawXC+JyCr9bvMAoKIFTqzqLYfpX10feYTDsQIDAQAB";

		/// <summary>
		/// This is used by the preference obfuscater to make sure that your
		/// obfuscated preferences are different than the ones used by other
		/// applications.
		/// </summary>
		public override byte[] GetSalt() => new byte[] { 1, 43, 12, 1, 54, 98, 100, 12, 43, 2, 8, 4, 9, 5, 106, 108, 33, 45, 1, 84 };

		/// <summary>
		/// Fill this in with the class name for your alarm receiver. We do this
		/// because receivers must be unique across all of Android (it's a good idea
		/// to make sure that your receiver is in your unique package)
		/// </summary>
		public override string AlarmReceiverClassName => "DownloaderSample.SampleAlarmReceiver";
	}
}
