using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "api",Namespace = null)]
    public class ApiDefinition : ApiObject
    {
        protected internal override string NodeName => "api";

        #region Generator Members
        private XmlSerializerNamespaces _xmlnamespaces;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces XmlNamespaces
        {
            get { return this._xmlnamespaces; }
        }

        #endregion

        public ApiDefinition()
        {
            _xmlnamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, "urn:xamarin") });

            Namespaces = new List<ApiNamespace>();
            Usings = new ApiUsings();
        }

        [XmlElement(ElementName = "usings", Order = 0)]
        public ApiUsings Usings { get; set; }

        [XmlElement(ElementName = "namespace", Order = 1)]
        public List<ApiNamespace> Namespaces { get; set; }

        

        public void UpdateHierachy()
        {
            foreach (var aNamespace in Namespaces)
            {
                aNamespace.SetParent(this);
            }
        }

        public Dictionary<string, ApiObject> GetFlatPathList()
        {
            var aList = new Dictionary<string, ApiObject>();

            UpdatePathList(ref aList);

            return aList;

        }

        internal protected override void SetParent(ApiObject parent)
        {
            
        }

        internal protected override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(Path, this);

            foreach (var aNamespace in Namespaces)
            {
                aNamespace.UpdatePathList(ref dict);
            }
        }
    }
}
