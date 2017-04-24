using System;
namespace Firebase.JobDispatcher
{
	public static class JobDispatchHelpers
	{
		public static Java.Lang.Class Translate(this Type managedType) 
		{
			if (managedType == null) 
			{
				throw new NullReferenceException("Must provide a Managed Type");
			}
			Java.Lang.Class javaServiceClass = Java.Lang.Class.FromType(managedType);
			return javaServiceClass;
		}
	}
	
}
