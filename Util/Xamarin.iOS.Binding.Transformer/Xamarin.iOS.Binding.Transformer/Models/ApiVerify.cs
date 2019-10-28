using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiVerify
    {
        [XmlAttribute(AttributeName = "type")]
        public string VerifyType { get; set; }

        public ApiVerify()
        {
        }
    }
}
