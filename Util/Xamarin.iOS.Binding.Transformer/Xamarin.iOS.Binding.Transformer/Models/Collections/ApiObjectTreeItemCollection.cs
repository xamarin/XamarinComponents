using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Xamarin.iOS.Binding.Transformer.Models.Collections
{
    public class ApiObjectTreeItemCollection : Collection<ApiObjectTreeItem>
    {
        public IEnumerable<string> Keys
        {
            get
            {
                if (!Items.Any())
                    return new List<string>();

                var paths = Items.Select(x => x.Path);

                return paths.ToList();
            }
        }

        public ApiObjectTreeItem this[string path]
        {
            get
            {
                return Items.FirstOrDefault(x => x.Path.Equals(path, StringComparison.OrdinalIgnoreCase)); ;
            }
        }

        public bool ContainsKey(string key)
        {
            var item = Items.FirstOrDefault(x => x.Path.Equals(key, StringComparison.OrdinalIgnoreCase));

            return (item != null);
        }

        public void Add(string path, ApiObject apiObject)
        {

        }
    }
}
