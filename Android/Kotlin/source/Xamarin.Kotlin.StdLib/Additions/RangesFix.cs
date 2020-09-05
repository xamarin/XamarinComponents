using System;

namespace Kotlin.Ranges
{
	public sealed partial class ULongRange
    {
        Java.Lang.Long Start => (Java.Lang.Long)(long)GetStart();

        Java.Lang.Long EndInclusive => (Java.Lang.Long)(long)GetEndInclusive();
    }

    public sealed partial class UIntRange
    {
        Java.Lang.Integer Start => (Java.Lang.Integer)(int)GetStart();

        Java.Lang.Integer EndInclusive => (Java.Lang.Integer)(int)GetEndInclusive();
    }
}
