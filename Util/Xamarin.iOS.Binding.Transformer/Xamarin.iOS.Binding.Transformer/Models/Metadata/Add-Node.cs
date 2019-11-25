using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer.Models.Metadata
{
    public class Add_Node
    {
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [XmlElement(ElementName = "using")]
        public ApiUsing Using { get; set; }

        [XmlElement(ElementName = "class")]
        public ApiClass Class { get; set; }

        [XmlElement(ElementName = "delegate")]
        public ApiDelegate Delegate { get; set; }

        [XmlElement(ElementName = "method")]
        public ApiMethod Method { get; set; }

        [XmlElement(ElementName = "property")]
        public ApiProperty Property { get; set; }

        [XmlElement(ElementName = "parameter")]
        public ApiParameter Parameter { get; set; }

        internal Add_Node Clone()
        {
            //var newNode = new Add_Node()
            //{
            //    Path = this.Path,
            //};

            //newNode.Using = this.Using.Clone();
            //newNode.Class = this.Class.Clone();
            //newNode.Delegate = this.Delegate.Clone();
            //newNode.Method = this.Method.Clone();
            //newNode.Property = this.Property.Clone();
            //newNode.Parameter = this.Parameter.Clone();
            var newNode = (Add_Node)this.MemberwiseClone();

            return newNode;
        }
    }
}
