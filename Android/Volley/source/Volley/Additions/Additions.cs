using System;

namespace Volley.Toolbox {
	public partial class StringRequest {
		protected override void DeliverResponse (Java.Lang.Object response)
		{
			DeliverResponse ((string)response);
		}
	}
}
