using System;

using VKontakte.API;

namespace VKontakte
{
	public class VKException : Exception
	{
		public VKException (VKError error)
			: base (error.ErrorMessage)
		{
			Error = error;
		}

		public VKException (string message, VKError error)
			: base (message)
		{
			Error = error;
		}

		public VKException (VKError error, Exception innerException)
			: base (error.ErrorMessage, innerException)
		{
			Error = error;
		}

		public VKException (string message, VKError error, Exception innerException)
			: base (message, innerException)
		{
			Error = error;
		}

		public VKError Error { get; private set; }
	}
}
