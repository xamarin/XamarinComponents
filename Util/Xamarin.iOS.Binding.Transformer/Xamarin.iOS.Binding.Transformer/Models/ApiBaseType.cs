using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName="BaseType")]
    public class ApiBaseType
    {
        [XmlAttribute(AttributeName = "typename")]
        public string TypeName { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        public ApiBaseType()
        {

        }
    }
}
