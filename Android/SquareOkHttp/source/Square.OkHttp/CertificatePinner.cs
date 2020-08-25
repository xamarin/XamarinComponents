using System.Collections.Generic;

namespace Square.OkHttp
{
	partial class CertificatePinner
	{
		public void Check (string p0, Java.Security.Cert.Certificate[] p1) =>
			Check (p0, (IList<Java.Security.Cert.Certificate>)p1);
	}
}
