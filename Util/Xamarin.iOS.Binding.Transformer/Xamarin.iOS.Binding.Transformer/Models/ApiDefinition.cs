using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "api",Namespace = null)]
    public class ApiDefinition
    {
        #region Generator Members
        private XmlSerializerNamespaces _xmlnamespaces;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces XmlNamespaces
        {
            get { return this._xmlnamespaces; }
        }

        #endregion

        [XmlAttribute(AttributeName = "namespace")]
        public string Namespace { get; set; }

        public ApiDefinition()
        {
            _xmlnamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, "urn:xamarin") });
           // Usings = new List<ApiUsing>();
            Types = new List<ApiClass>();
            Delegates = new List<ApiDelegate>();
            Usings = new ApiUsings();
            
        }

        [XmlElement(ElementName = "usings", Order = 0)]
        public ApiUsings Usings { get; set; }

        //[XmlElement(ElementName = "using", Order = 0)]
        //public List<ApiUsing> Usings { get; set; }

        [XmlElement(ElementName = "delegate", Order = 1)]
        public List<ApiDelegate> Delegates { get; set; }

        [XmlElement(ElementName = "class", Order = 2)]
        public List<ApiClass> Types { get; set; }


    }
}
