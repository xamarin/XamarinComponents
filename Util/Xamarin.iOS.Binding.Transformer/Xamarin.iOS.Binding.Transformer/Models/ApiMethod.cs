using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "method")]
    public class ApiMethod
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "returns")]
        public string ReturnType { get; set; }

        [XmlElement(ElementName = "parameter")]
        public List<ApiParameter> Parameters { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

        [XmlAttribute(AttributeName = "export")]
        public string ExportName { get; set; }

        public ApiMethod()
        {
            Parameters = new List<ApiParameter>();
        }
    }
}
