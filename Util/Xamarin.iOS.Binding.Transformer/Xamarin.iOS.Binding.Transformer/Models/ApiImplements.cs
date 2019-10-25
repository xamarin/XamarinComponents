using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiImplements
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        public ApiImplements()
        {
        }
    }
}
