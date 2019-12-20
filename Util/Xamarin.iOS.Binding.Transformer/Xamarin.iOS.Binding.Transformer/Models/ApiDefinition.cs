using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;
using Xamarin.iOS.Binding.Transformer.Models;
using Xamarin.iOS.Binding.Transformer.Models.Collections;
using Xamarin.iOS.Binding.Transformer.Models.Metadata;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = @"api",Namespace = null)]
    public class ApiDefinition : ApiObject
    {
        [ChangeIgnore]
        protected internal override string NodeName => "/api";

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

        /// <summary>
        /// Compare against a provided ApiDefintion
        /// </summary>
        /// <param name="target">ApiDefinition to compare against</param>
        /// <returns></returns>
        public Metadata Compare(ApiDefinition target)
        {
            return ChangeManager.CompareNew(this, target);
        }

        public void UpdateHierachy()
        {
            foreach (var aUsing in Usings.Items)
            {
                aUsing.SetParent(this);
            }

            foreach (var aNamespace in Namespaces)
            {
                aNamespace.SetParent(this);
            }
        }

        public Dictionary<string, ApiObject> BuildTreePath()
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

            foreach (var aUsing in Usings.Items)
            {
                aUsing.UpdatePathList(ref dict);
            }

            foreach (var aNamespace in Namespaces)
            {
                aNamespace.UpdatePathList(ref dict);
            }
        }

        internal override void Add(ApiObject item)
        {
            if (item is ApiUsing)
            {
                Usings.Items.Add((ApiUsing)item);
            }
        }

        internal override void Remove(ApiObject item)
        {
            if (item is ApiUsing)
            {
                Usings.Items.Remove((ApiUsing)item);
            }
 
        }

        public override void RemovePrefix(string prefix)
        {
           foreach (var ans in Namespaces)
            {
                ans.RemovePrefix(prefix);
            }
        }

        public void FlattenCategories(params string[] suffixes)
        {
            foreach (var aNamespace in Namespaces)
            {
                aNamespace.FlattenCategories(suffixes);
            }
        }

        internal List<ApiClass> GetTypes()
        {
            var results = new List<ApiClass>();

            foreach (var aNamespace in Namespaces)
            {
                results.AddRange(aNamespace.Types);
            }

            return results;
        }
    }
}
