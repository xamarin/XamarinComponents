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

		[Flags]
		public enum Days : uint
		{
			None = 0,
			Monday = 1,
			Tuesday = 2,
			Wednesday = 4,
			Thursday = 8,
			Friday = 16,
			All = Monday | Tuesday | Wednesday | Thursday | Friday
		}

		public enum Color : sbyte
		{
			None = 0,
			Red = 1,
			Orange = 3,
			Yellow = 4
		}

		public static object TestMethod()
		{
			RijndaelManaged rjMan = new RijndaelManaged();
			return rjMan;
		}
	}
}
