using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace thisisatest
{
	public class MyClass
	{
		public static object DeserializeItemString(string str)
		{
			BinaryFormatter f = new BinaryFormatter();
			return f.UnsafeDeserialize(null, null);
		}

		public static object TestMethod()
		{
			RijndaelManaged rjMan = new RijndaelManaged();
			return rjMan;
		}
	}
}
