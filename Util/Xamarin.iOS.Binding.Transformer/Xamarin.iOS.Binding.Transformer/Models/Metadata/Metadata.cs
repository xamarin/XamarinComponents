using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer.Models.Metadata
{
    [XmlRoot(ElementName = "metadata", Namespace = null)]
    public class Metadata
    {
        #region Generator Members
        private XmlSerializerNamespaces _xmlnamespaces;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces XmlNamespaces
        {
            get { return this._xmlnamespaces; }
        }

        #endregion

        [XmlElement(ElementName = "remove-node")]
        public List<Remove_Node> RemoveNodes { get; set; }

        public Metadata()
        {
            _xmlnamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, "urn:xamarin") });

            RemoveNodes = new List<Remove_Node>();
        }





    }
}
