using System.Collections;
using System.Collections.Generic;
using Java.Util;

namespace MinimalJson
{
	public partial class JsonArray : IEnumerable<JsonValue>, IEnumerable
	{
		public IEnumerator<JsonValue> GetEnumerator ()
		{
			return Values ().GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Values ().GetEnumerator ();
		}
	}
}
