using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;
using Xamarin.iOS.Binding.Transformer.Models.Collections;

namespace Xamarin.iOS.Binding.Transformer
{
    [XmlRoot(ElementName = "method")]
    public class ApiMethod : ApiObject
    {
        [ChangeIgnore]
        protected internal override string NodeName => $"method[@name='{NativeName}']";

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "returns")]
        public string ReturnType { get; set; }

        [ChangeIgnore]
        [XmlElement(ElementName = "parameter")]
        public List<ApiParameter> Parameters { get; set; }

        [XmlAttribute(AttributeName = "static")]
        public bool IsStatic { get; set; }

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

        [XmlAttribute(AttributeName = "obsolete")]
        public string Obsolete { get; set; }

        [XmlIgnore]
        public string NativeName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ExportName))
                    return ExportName;

                return Name;
            }
        }

        public ApiMethod()
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

        internal override void Add(ApiObject item)
        {
            if (item is ApiParameter)
            {
                Parameters.Add((ApiParameter)item);
            }
        }

        internal override void Remove(ApiObject item)
        {
            if (item is ApiParameter)
            {
                Parameters.Remove((ApiParameter)item);
            }
        }

    }
}
