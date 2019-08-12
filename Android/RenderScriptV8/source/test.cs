using System.Runtime.Serialization.Formatters.Binary;

namespace testing
{
	public class Test
	{
		public static object DeserializeItemString(string str)
		{
			BinaryFormatter f = new BinaryFormatter();
			return f.UnsafeDeserialize(null, null);
		}
	}
}
