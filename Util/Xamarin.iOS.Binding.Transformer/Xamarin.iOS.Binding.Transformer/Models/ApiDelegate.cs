using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiDelegate
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "returns")]
        public string ReturnType { get; set; }

        [XmlElement(ElementName = "parameter")]
        public List<ApiParameter> Parameters { get; set; }

        public ApiDelegate()
        {
            Parameters = new List<ApiParameter>();
        }
    }
}
