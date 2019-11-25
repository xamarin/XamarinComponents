using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;
using Xamarin.iOS.Binding.Transformer.Models.Collections;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiParameter : ApiObject
    {
        [ChangeIgnore]
        protected internal override string NodeName => $"parameter[@name='{Name}']";

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "nullallowed")]
        public bool IsNullAllowed { get; set; }

        [XmlAttribute(AttributeName = "isreference")]
        public bool IsReference { get; set; }

        public ApiParameter()
        {
        }

        internal protected override void SetParent(ApiObject parent)
        {
            base.SetParentInternal(parent);

        }

        internal protected override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(Path, this);
        }

        internal override void Add(ApiObject item)
        {

        }

        internal override void Remove(ApiObject item)
        {

        }

        internal static ApiParameter Clone()
        {
            throw new NotImplementedException();
        }

        public override void RemovePrefix(string prefix)
        {
           if (Type.StartsWith(prefix))
           {
                Type = Type.Replace(prefix, "");
           }
        }
    }
}
