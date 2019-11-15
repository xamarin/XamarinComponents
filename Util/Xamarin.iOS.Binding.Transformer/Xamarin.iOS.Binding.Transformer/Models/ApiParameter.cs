using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiParameter : ApiObject
    {
        [ChangeIgnore]
        protected internal override string NodeName => $"parameter[@name='{Name}']";

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "nullallowed")]
        public bool IsNullAllowed { get; set; }

        public ApiParameter()
        {
        }

        internal protected override void SetParent(ApiObject parent)
        {
            base.SetParentInternal(parent);

        }

        internal protected override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(Path, this);
        }
    }
}
