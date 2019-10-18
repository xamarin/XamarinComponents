using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xamarin.Binding.IOS.Helpers
{
    public class TransformationMap
    {
        public List<MemberOverride> Mappings { get; set; }

        [JsonIgnore]
        public List<MemberOverride> Removed { get; set; }

        public TransformationMap()
        {
            Mappings = new List<MemberOverride>();
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                WriteIndented = true
            }); ;
        }

        public static TransformationMap Deserialize(string content)
        {
            return JsonSerializer.Deserialize<TransformationMap>(content);
        }

        public void RemoveUnchanged()
        {
            Removed = Mappings.Where(x => !x.IsChanged).ToList();

            Mappings.RemoveAll(x => !x.IsChanged);
        }
    }
}
