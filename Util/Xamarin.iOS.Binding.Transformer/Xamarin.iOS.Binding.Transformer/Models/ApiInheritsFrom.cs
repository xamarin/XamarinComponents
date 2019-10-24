using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiInheritsFrom
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        public ApiInheritsFrom()
        {
        }
    }
}
