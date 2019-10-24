using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiParameter
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        public ApiParameter()
        {
        }
    }
}
