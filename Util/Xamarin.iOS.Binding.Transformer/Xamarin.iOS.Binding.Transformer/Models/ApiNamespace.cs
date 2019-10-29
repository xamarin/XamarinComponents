using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiNamespace
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "delegate", Order = 1)]
        public List<ApiDelegate> Delegates { get; set; }

        [XmlElement(ElementName = "class", Order = 2)]
        public List<ApiClass> Types { get; set; }

        public ApiNamespace()
        {
            Types = new List<ApiClass>();
            Delegates = new List<ApiDelegate>();
            
        }
    }
}
