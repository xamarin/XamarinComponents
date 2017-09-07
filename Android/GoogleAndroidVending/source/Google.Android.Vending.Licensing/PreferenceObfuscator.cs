using System;

namespace Google.Android.Vending.Licensing
{
	partial class PreferenceObfuscator
	{
		public T GetValue<T>(string key, T defValue)
		{
			var type = typeof(T);
			var value = GetString(key, defValue.ToString());

			if (type.IsEnum)
			{
				return (T)Enum.Parse(type, value, true);
			}

			type = Nullable.GetUnderlyingType(type) ?? type;

			return (T)Convert.ChangeType(value, type);
		}

		public T GetValue<T>(string key)
		{
			return GetValue<T>(key, default(T));
		}

		public void PutValue<T>(string key, T value)
		{
			PutString(key, value.ToString());
		}
	}
}
