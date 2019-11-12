using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiParameter : ApiObject
    {
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
    }
}
