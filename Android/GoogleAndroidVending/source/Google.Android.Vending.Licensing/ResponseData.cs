namespace Google.Android.Vending.Licensing
{
	partial class ResponseData
	{
		public ServerResponseCode ResponseCode
		{
			get { return (ServerResponseCode)ResponseCodeInternal; }
			set { ResponseCodeInternal = (int)value; }
		}
	}
}
