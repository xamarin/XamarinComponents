using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer.Models.Metadata
{
    public class Attr
    {
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlElement(ElementName = "property")]
        public List<AttrProperty> Properties { get; set; }

        public Attr()
        {
            Properties = new List<AttrProperty>();
        }

        public Attr Clone()
        {
            var newClone = (Attr)this.MemberwiseClone();
            newClone.Properties = new List<AttrProperty>();

            foreach (var aProp in Properties)
            {
                var newProp = aProp.Clone();

                newClone.Properties.Add(newProp);
            }

            return newClone;
        }
    }
}
