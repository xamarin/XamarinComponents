using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName="BaseType")]
    public class ApiBaseType
    {
        [XmlAttribute(AttributeName = "type")]
        public string TypeName { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "delegate")]
        public string DelegateName { get; set; }

        [XmlAttribute(AttributeName = "eventstype")]
        public string EventsType { get; set; }

        public ApiBaseType()
        {

        }
    }
}
