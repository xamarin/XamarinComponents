using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "class")]
    public class ApiClass
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public ApiTypeType Type { get; set; }

        [XmlAttribute(AttributeName = "disabledefaultctor")]
        public bool DisableDefaultCtor { get; set; }

        [XmlAttribute(AttributeName = "category")]
        public bool IsCategory { get; set; }

        [XmlAttribute(AttributeName = "protocol")]
        public bool IsProtocol { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

        [XmlAttribute(AttributeName = "partial")]
        public bool IsPartial { get; set; }

        [XmlElement(ElementName ="model", Order = 0)]
        public ApiTypeModel Model { get; set; }

        [XmlElement(ElementName = "basetype", Order = 1)]
        public ApiBaseType BaseType { get; set; }

        [XmlElement(ElementName = "implements", Order = 2)]
        public List<ApiImplements> Implements { get; set; }

        [XmlElement(ElementName = "property", Order = 3)]
        public List<ApiProperty> Properties { get; set; }

        [XmlElement(ElementName = "method", Order = 4)]
        public List<ApiMethod> Methods { get; set; }

        [XmlElement(ElementName = "verify", Order = 5)]
        public ApiVerify Verify { get; set; }

        #region Ignoreable Properties

        [XmlIgnore]
        public string NativeName
        {
            get
            {
                if (BaseType != null && !string.IsNullOrWhiteSpace(BaseType.Name))
                    return BaseType.Name;

                return Name;
            }
        }

        #endregion
        public ApiClass()
        {
            Implements = new List<ApiImplements>();
            Methods = new List<ApiMethod>();
            Properties = new List<ApiProperty>();
        }
    }
}
