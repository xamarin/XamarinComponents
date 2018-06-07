using System;
using System.IO;
using UIKit;
using Newtonsoft.Json;

using VKontakte;
using VKontakte.Core;

namespace VKontakteSampleiOS
{
	partial class ApiCallViewController : UIViewController
	{
		public ApiCallViewController (IntPtr handle)
			: base (handle)
		{
		}

		public VKRequest CallingRequest { get; set; }

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = CallingRequest.MethodName;
			CallingRequest.DebugTiming = true;
			CallingRequest.RequestTimeout = 10;

			try {
				// make the request
				var response = await CallingRequest.ExecuteAsync ();
				CallingRequest = null;

				// apply JSON formatting
				using (var stringReader = new StringReader (response.ResponseString))
				using (var stringWriter = new StringWriter ())
				using (var jsonReader = new JsonTextReader (stringReader))
				using (var jsonWriter = new JsonTextWriter (stringWriter) { Formatting = Formatting.Indented }) {
					jsonWriter.WriteToken (jsonReader);

					// show result
					callResult.Text = string.Format ("Result ({0:0.##}s):\n{1}", response.Request.RequestTiming.TotalTime, stringWriter);
				}
			} catch (VKException ex) {
				callResult.Text = "Error: " + ex.Error;
			}
		}
	}
}
