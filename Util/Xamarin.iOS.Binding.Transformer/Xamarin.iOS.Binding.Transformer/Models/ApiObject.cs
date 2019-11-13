using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public abstract class ApiObject
    {
        [XmlIgnore]
        internal protected ApiObject Parent { get; private set; }

        [XmlIgnore]
        internal protected abstract string NodeName { get; }

        [XmlIgnore]
        internal string Path
        {
            get
            {
                if (Parent != null)
                {
                    return $"{Parent.Path}/{NodeName}";
                }
                else
                {
                    return $"{NodeName}";
                }
            }
        }

        internal protected void SetParentInternal(ApiObject parent)
        {
            Parent = parent;
        }

        internal protected abstract void SetParent(ApiObject parent);

        internal protected abstract void UpdatePathList(ref Dictionary<string, ApiObject> dict);
    }
}
