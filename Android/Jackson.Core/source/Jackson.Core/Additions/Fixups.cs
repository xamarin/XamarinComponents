using System;
using System.Collections.Generic;
using Android.Runtime;

namespace FasterXml.Jackson.Core
{
    public partial class Version
    {
        int global::Java.Lang.IComparable.CompareTo (global::Java.Lang.Object p0)
        {
            Version p0Cast = p0 == null ? null : p0.JavaCast<Version> ();
            return this.CompareTo (p0Cast);
        }
    }
}

namespace FasterXml.Jackson.Core.Base
{
    public abstract partial class ParserBase
    {
        public override global::FasterXml.Jackson.Core.JsonParser.NumberType GetNumberType ()
        {
            return NumberType;
        }
    }
}