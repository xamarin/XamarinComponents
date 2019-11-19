using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.iOS.Binding.Transformer.Models
{
    public class ApiObjectTreeItem
    {
        public string Path { get; set; }

        public List<ApiObject> Items { get; set; }

        public ApiObjectTreeItem()
        {
            Items = new List<ApiObject>();
        }
    }
}
