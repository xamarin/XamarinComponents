using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "method")]
    public class ApiMethod
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "returns")]
        public string ReturnType { get; set; }

        [XmlElement(ElementName = "parameter")]
        public List<ApiParameter> Parameters { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

        [XmlAttribute(AttributeName = "obsolete")]
        public bool IsObsolete { get; set; }

        [XmlAttribute(AttributeName = "export")]
        public string ExportName { get; set; }

        [XmlAttribute(AttributeName = "eventargs")]
        public string EventArgs { get; set; }

        [XmlAttribute(AttributeName = "eventname")]
        public string EventName { get; set; }

        [XmlAttribute(AttributeName = "nullallowed")]
        public bool IsNullAllowed { get; set; }

        [XmlAttribute(AttributeName = "abstract")]
        public bool IsAbstract { get; set; }

        [XmlAttribute(AttributeName = "RequiresSuper")]
        public bool RequiresSuper { get; set; }

        [XmlAttribute(AttributeName = "designatedinitializer")]
        public bool DesignatedInitializer { get; set; }

        [XmlAttribute(AttributeName = "wrap")]
        public string WrapName { get; set; }

        [XmlAttribute(AttributeName = "default_value")]
        public string DefaultValue { get; set; }

        [XmlAttribute(AttributeName = "delegate_name")]
        public string DelegateName { get; set; }

        [XmlAttribute(AttributeName = "new")]
        public bool IsNew { get; set; }

        [XmlAttribute(AttributeName = "advice")]
        public string Advice { get; set; }

        public ApiMethod()
        {
            Parameters = new List<ApiParameter>();
        }
    }
}
