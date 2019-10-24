using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "class")]
    public class ApiClass
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public ApiTypeType Type { get; set; }

        [XmlAttribute(AttributeName = "disabledefaultctor")]
        public bool DisableDefaultCtor { get; set; }

        [XmlAttribute(AttributeName = "category")]
        public bool IsCategory { get; set; }

        [XmlAttribute(AttributeName = "protocol")]
        public bool IsProtocol { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

        [XmlElement(ElementName ="model", Order = 0)]
        public ApiTypeModel Model { get; set; }

        [XmlElement(ElementName = "basetype", Order = 1)]
        public ApiBaseType BaseType { get; set; }

        [XmlElement(ElementName = "inheritsfrom", Order = 2)]
        public List<ApiInheritsFrom> InheritsFrom { get; set; }

        public ApiClass()
        {
            InheritsFrom = new List<ApiInheritsFrom>();
        }
    }
}
