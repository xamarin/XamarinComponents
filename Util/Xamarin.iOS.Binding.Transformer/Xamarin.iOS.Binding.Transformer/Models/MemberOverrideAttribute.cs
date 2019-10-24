using System;
using System.Collections.Generic;

namespace Xamarin.iOS.Binding.Transformer
{
    public enum AttributeDataType
    {
        TypeName,
        String,
        Boolean,
        TypeOf,
        TypeOfArray,
        StringArray,
        MemberAccess,
        Null,
        NullReturn,
        Number,
    }

    public class MemberOverrideAttribute
    {
        public string Name { get; set; }

        public List<MemberOverrideArguments> Arguments { get; set; }

        public MemberOverrideAttribute()
        {
            Arguments = new List<MemberOverrideArguments>();
        }
    }

    public class MemberOverrideArguments
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public AttributeDataType DataType { get; set; }

    }
}
