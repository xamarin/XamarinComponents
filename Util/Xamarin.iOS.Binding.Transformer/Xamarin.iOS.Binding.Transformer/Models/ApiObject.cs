using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Xamarin.iOS.Binding.Transformer.Attributes;

namespace Xamarin.iOS.Binding.Transformer
{
    public abstract class ApiObject
    {
        [ChangeIgnore]
        [XmlIgnore]
        internal protected ApiObject Parent { get; private set; }

        [ChangeIgnore]
        [XmlIgnore]
        internal protected abstract string NodeName { get; }

        [ChangeIgnore]
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

        internal abstract void Add(ApiObject item);

        internal abstract void Remove(ApiObject item);

        internal protected string GetProposedPath(ApiObject parent)
        {
            return $"{parent.Path}/{NodeName}";
        }

        public abstract void RemovePrefix(string prefix);


    }
}
