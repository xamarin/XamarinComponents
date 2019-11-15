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


        [XmlElement(ElementName = "attr", Order = 0)]
        public List<Attr> Changes { get; set; }

        [XmlElement(ElementName = "blank", Order = 1)]
        public BlankLine BlankLine1 { get; set; }

        [XmlElement(ElementName = "add-node",Order = 2)]
        public List<Add_Node> AddNodes { get; set; }

        [XmlElement(ElementName = "blank", Order = 3)]
        public BlankLine BlankLine2 { get; set; }

        [XmlElement(ElementName = "remove-node", Order = 4)]
        public List<Remove_Node> RemoveNodes { get; set; }

        public Metadata()
        {
            BlankLine1 = new BlankLine();
            BlankLine2 = new BlankLine();

            _xmlnamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, "urn:xamarin") });

            RemoveNodes = new List<Remove_Node>();
            AddNodes = new List<Add_Node>();
            Changes = new List<Attr>();
        }





    }
}
