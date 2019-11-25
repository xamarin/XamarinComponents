using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer.Models.Metadata
{
    public class AttrProperty
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        internal AttrProperty Clone()
        {
            var newProp = (AttrProperty)this.MemberwiseClone();

            return newProp;
        }
    }
}
