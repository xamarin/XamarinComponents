using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;
using Xamarin.iOS.Binding.Transformer.Models.Collections;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "using")]
    public class ApiUsings
    {
        [XmlElement(ElementName = "using", Order = 0)]
        public List<ApiUsing> Items { get; set; }

        public ApiUsings()
        {
            Items = new List<ApiUsing>();
        }
    }
    [XmlRoot(ElementName = "using")]
    public class ApiUsing : ApiObject
    {
        [XmlAttribute(attributeName:"name")]
        public string Name { get; set; }

        [ChangeIgnore]
        protected internal override string NodeName => $"using[@namespace='{Name}']";

        protected internal override void SetParent(ApiObject parent)
        {
            SetParentInternal(parent);
        }

        protected internal override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(this.Path, this);
        }

        internal override void Add(ApiObject item)
        {
           
        }

        internal override void Remove(ApiObject item)
        {
            
        }
    }
}
