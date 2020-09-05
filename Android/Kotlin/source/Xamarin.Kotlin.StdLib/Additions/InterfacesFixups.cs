namespace Kotlin.Collections
{
	// TODO: Remove these fixes when this bug is fixed:
	//       https://github.com/xamarin/java.interop/issues/470

	partial class AbstractSetInvoker : Java.Util.ISet
	{
	}

	partial class AbstractListInvoker : Java.Util.IList
	{
	}

    public abstract partial class ULongIterator
    {
        unsafe global::Java.Lang.Object global::Java.Util.IIterator.Next()
        {
            return (Java.Lang.Long)(long)Next();
        }

    }

    public abstract partial class UByteIterator 
    {
        unsafe global::Java.Lang.Object global::Java.Util.IIterator.Next()
        {
            return (Java.Lang.Byte)(sbyte)Next();
        }

    }

    public abstract partial class UIntIterator
    {
        unsafe global::Java.Lang.Object global::Java.Util.IIterator.Next()
        {
            return (Java.Lang.Integer)(int)Next();
        }
    }

    public abstract partial class UShortIterator
    {
        unsafe global::Java.Lang.Object global::Java.Util.IIterator.Next()
        {
            return (Java.Lang.Short)(short)Next();
        }
    }
}
