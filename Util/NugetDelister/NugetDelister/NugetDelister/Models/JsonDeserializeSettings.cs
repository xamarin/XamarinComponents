using System;
using Newtonsoft.Json;

namespace NugetDelister.Models
{
    public class JsonDeserializeSettings
    {
        public static JsonSerializerSettings Default
        {
            get
            {
                return new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                };
            }
        }

    }
}
