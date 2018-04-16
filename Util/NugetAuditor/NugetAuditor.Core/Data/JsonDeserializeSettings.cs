using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
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
