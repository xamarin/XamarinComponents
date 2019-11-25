using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Loads the specified input file.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid input file</exception>
        public static Metadata Load(string inputFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputFile) || !File.Exists(inputFile))
                    throw new Exception("Invalid input file");

                var serializer = new XmlSerializer(typeof(Metadata));

                var output = new Metadata();

                using (StreamReader str = new StreamReader(inputFile))
                {

                    output = (Metadata)serializer.Deserialize(str);
                }

                return output;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Metadata Clone()
        {
            var meta = new Metadata();

            foreach (var aAttr in Changes)
                meta.Changes.Add(aAttr.Clone());

            foreach (var aNode in AddNodes)
                meta.AddNodes.Add(aNode.Clone());

            foreach (var aNode in RemoveNodes)
                meta.RemoveNodes.Add(aNode.Clone());

            return meta;
        }
    }
}
