using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Binding.IOS.Helpers
{
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
