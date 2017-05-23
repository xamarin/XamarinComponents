using System.Text;
using Java.Security;
using Android.Util;
using Java.Security.Spec;
using Java.Lang;

namespace Xamarin.InAppBilling.Utilities
{
	/// <summary>
	/// Utility class to support secure transactions for Google Play In-App Billing
	/// </summary>
	public sealed class Security
	{
		#region Constants
		const string KeyFactoryAlgorithm = "RSA";
		const string SignatureAlgorithm = "SHA1withRSA";
		#endregion 

		#region Public Methods
		/// <summary>
		/// Recombines the given elements and segments to reconstruct an obfuscated string.
		/// </summary>
		/// <param name="element">List of elements used to reconstruct the string.</param>
		/// <param name="segment">List of segments speciying the order of the elements.</param>
		/// <remarks>Given a list of elements {"A","B","C","D"} and segments {1,0,3,2} this function returns "BADC". This function is used to
		/// hide a string inside of a Xamarin.Android app.</remarks>
		public static string Unify(string[] element, int[] segment) {
			string output = "";

			// Process each segment
			foreach (int i in segment) {
				output += element [i];
			}

			// Return result
			return output;
		}

		/// <summary>
		/// Recombines the given elements, segments and hashes to reconstruct an obfuscated string.
		/// </summary>
		/// <param name="element">List of elements used to reconstruct the string.</param>
		/// <param name="segment">List of segments speciying the order of the elements.</param>
		/// <param name="hash">Given a list of elements {"A","B","C123","D"}, segments {1,0,3,2} and hashes {"123","007"} this function returns "BADC007". This function is used to
		/// hide a string inside of a Xamarin.Android app.</param>
		public static string Unify(string[] element, int[] segment, string[] hash) {
			string output = "";

			// Unify elements and segments first
			output = Unify (element, segment);

			// Replace any key/value pairs from the hash list
			for (int i=0; i < hash.Length; i+=2) {
				output = output.Replace (hash [i], hash [i + 1]);
			}

			// Return result
			return output;
		}

		/// <summary>
		/// Verifies the purchase.
		/// </summary>
		/// <returns><c>true</c>, if purchase was verified, <c>false</c> otherwise.</returns>
		/// <param name="publicKey">Public key.</param>
		/// <param name="signedData">Signed data.</param>
		/// <param name="signature">Signature.</param>
		public static bool VerifyPurchase (string publicKey, string signedData, string signature)
		{
			if (signedData == null) {
				Logger.Error ("Security. data is null");
				return false;
			}

			try {
				if (!string.IsNullOrEmpty (signature)) {
					var key = Security.GeneratePublicKey (publicKey);
					bool verified = Security.Verify (key, signedData, signature);

					if (verified) {
						return true;
					} else {
						Logger.Error ("Security. Signature does not match data.");
						return false;
					}
				}
			}
			catch {
				// Return false on error
				return false;
			}

			return false;
		}

		/// <summary>
		/// Generates the public key.
		/// </summary>
		/// <returns>The public key.</returns>
		/// <param name="encodedPublicKey">Encoded public key.</param>
		public static IPublicKey GeneratePublicKey (string encodedPublicKey)
		{
			try {
				var keyFactory = KeyFactory.GetInstance (KeyFactoryAlgorithm);
				return keyFactory.GeneratePublic (new X509EncodedKeySpec (Base64.Decode (encodedPublicKey, 0)));
			} catch (NoSuchAlgorithmException e) {
				Logger.Error (e.Message);
				throw new RuntimeException (e);
			} catch (Exception e) {
				Logger.Error (e.Message);
				throw new IllegalArgumentException ();
			}
		}

		/// <summary>
		/// Verify the specified publicKey, signedData and signature.
		/// </summary>
		/// <param name="publicKey">Public key.</param>
		/// <param name="signedData">Signed data.</param>
		/// <param name="signature">Signature.</param>
		public static bool Verify (IPublicKey publicKey, string signedData, string signature)
		{
			Logger.Debug ("Signature: {0}", signature);
			try {
				var sign = Signature.GetInstance (SignatureAlgorithm);
				sign.InitVerify (publicKey);
				sign.Update (Encoding.UTF8.GetBytes (signedData));

				if (!sign.Verify (Base64.Decode (signature, 0))) {
					Logger.Error ("Security. Signature verification failed.");
					return false;
				}

				return true;
			} catch (Exception e) {
				Logger.Error (e.Message);
			}

			return false;
		}
		#endregion 
	}
}
