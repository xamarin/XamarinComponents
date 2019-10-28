using System;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "property")]
    public class ApiProperty
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "nullallowed")]
        public bool IsNullAllowed { get; set; }

        [XmlAttribute(AttributeName = "abstract")]
        public bool IsAbstract { get; set; }

        [XmlAttribute(AttributeName = "export")]
        public string ExportName { get; set; }

        [XmlAttribute(AttributeName = "get")]
        public bool CanGet { get; set; }

        [XmlAttribute(AttributeName = "get_bind")]
        public string GetBindName { get; set; }

        [XmlAttribute(AttributeName = "set")]
        public bool CanSet { get; set; }

        [XmlAttribute(AttributeName = "set_bind")]
        public string SetBindName { get; set; }

        [XmlAttribute(AttributeName = "obsolete")]
        public bool IsObsolete { get; set; }

        [XmlAttribute(AttributeName = "semanticstrength")]
        public string SemanticStrength { get; set; }

        [XmlAttribute(AttributeName = "wrap")]
        public string WrapName { get; set; }

        [XmlAttribute(AttributeName = "ios_version")]
        public string IosVersion { get; set; }

        [XmlAttribute(AttributeName = "tv_version")]
        public string TVVersion { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

        [XmlElement(ElementName = "verify")]
        public ApiVerify Verify { get; set; }

        [XmlAttribute(AttributeName = "field_params")]
        public string FieldParams { get; set; }

        public ApiProperty()
        {

        }
    }
}
