using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xamarin.Binding.IOS.Helpers
{
    public class MemberOverride
    {
        public string Name { get; set; }

        public string OriginalName { get; set; }

        public List<MemberOverride> Members { get; set; }

        public List<MemberOverrideAttribute> Attributes { get; set; }

        public MemberType Type { get; set; }


        public MemberOverride()
        {
            Attributes = new List<MemberOverrideAttribute>();
            Members = new List<MemberOverride>();
        }

        [JsonIgnore]
        public bool IsChanged
        {
            get
            {
                switch (Type)
                {
                    case MemberType.Class:
                        {
                            if (OriginalName == null)
                                return false;

                            return Name != OriginalName;
                        }
                    case MemberType.Method:
                        {
                           return (!string.IsNullOrWhiteSpace(OriginalName)) ;
                        }
                    case MemberType.Property:
                        {
                            return (!string.IsNullOrWhiteSpace(OriginalName));
                        }
                }

                return false;
            }
        }
    }
}
