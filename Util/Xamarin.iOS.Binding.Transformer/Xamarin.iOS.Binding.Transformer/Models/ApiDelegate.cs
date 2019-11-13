using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiDelegate : ApiObject
    {
        protected internal override string NodeName => $"delegate[@name='{Name}']";

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "returns")]
        public string ReturnType { get; set; }

        [XmlElement(ElementName = "parameter")]
        public List<ApiParameter> Parameters { get; set; }

        public ApiDelegate()
        {
            Parameters = new List<ApiParameter>();
        }

        internal protected override void SetParent(ApiObject parent)
        {
            base.SetParentInternal(parent);

            foreach (var aObject in Parameters)
            {
                aObject.SetParent(this);
            }

        }

        internal protected override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(Path, this);

            foreach (var aNamespace in Parameters)
            {
                aNamespace.UpdatePathList(ref dict);
            }
        }
    }
}
