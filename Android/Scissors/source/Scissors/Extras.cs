using System.Threading.Tasks;
using Java.Util.Concurrent;

namespace Lyft.Scissors
{
	partial class CropViewExtensions
	{
		partial class CropRequest
		{
			public Task IntoAsync (System.IO.Stream outputStream, bool closeWhenDone)
			{
				return Into (outputStream, closeWhenDone).GetAsync ();
			}

			public Task IntoAsync (Java.IO.File file)
			{
				return Into (file).GetAsync ();
			}

			public IFuture Into (string filename)
			{
				return Into (new Java.IO.File (filename));
			}

			public Task IntoAsync (string filename)
			{
				return Into (filename).GetAsync ();
			}
		}
	}
}
