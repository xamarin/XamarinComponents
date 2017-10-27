using System;
using System.Runtime.InteropServices;

namespace Dropbox.CoreApi.iOS
{
	public static class Base64Transcoder
	{

		//		[DllImport ("__Internal", EntryPoint = "GENDER")]
		//		private static extern IntPtr _Gender (int gender);
		//
		//		public static string Gender (GenderOption gender)
		//		{
		//			var str = Runtime.GetNSObject<NSString> (_Gender ((int)gender));
		//			return (string) str;
		//		}

		// extern size_t DBEstimateBas64EncodedDataSize (size_t inDataSize);
		[DllImport ("__Internal", EntryPoint = "DBEstimateBas64EncodedDataSize")]
		private static extern nuint _EstimateBas64EncodedDataSize (nuint inDataSize);

		public static nuint EstimateBas64EncodedDataSize (nuint inDataSize)
		{
			return _EstimateBas64EncodedDataSize (inDataSize);
		}

		// extern size_t DBEstimateBas64DecodedDataSize (size_t inDataSize);
		[DllImport ("__Internal", EntryPoint = "DBEstimateBas64EncodedDataSize")]
		private static extern nuint _EstimateBas64DecodedDataSize (nuint inDataSize);

		public static nuint EstimateBas64DecodedDataSize (nuint inDataSize)
		{
			return _EstimateBas64DecodedDataSize (inDataSize);
		}

		// extern _Bool DBBase64EncodeData (const void * inInputData, size_t inInputDataSize, char * outOutputData, size_t * ioOutputDataSize);
		[DllImport ("__Internal", EntryPoint = "DBBase64EncodeData")]
		static extern unsafe bool _Base64EncodeData (IntPtr inInputData, nuint inInputDataSize, out IntPtr outOutputData, out nuint ioOutputDataSize);

		public static unsafe bool Base64EncodeData (IntPtr inInputData, nuint inInputDataSize, out IntPtr outOutputData, out nuint ioOutputDataSize)
		{
			return _Base64EncodeData (inInputData, inInputDataSize, out outOutputData, out ioOutputDataSize);
		}

		// extern _Bool DBBase64DecodeData (const void * inInputData, size_t inInputDataSize, void * ioOutputData, size_t * ioOutputDataSize);
		[DllImport ("__Internal", EntryPoint = "DBBase64DecodeData")]
		static extern unsafe bool _Base64DecodeData (IntPtr inInputData, nuint inInputDataSize, IntPtr ioOutputData, out nuint ioOutputDataSize);

		public static unsafe bool Base64DecodeData (IntPtr inInputData, nuint inInputDataSize, IntPtr outOutputData, out nuint ioOutputDataSize)
		{
			return _Base64DecodeData (inInputData, inInputDataSize, outOutputData, out ioOutputDataSize);
		}
	}
}

