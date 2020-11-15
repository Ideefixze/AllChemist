using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AllChemist.Serialization
{
    /// <summary>
    /// Static class for unified serialization and deserialization.
    /// </summary>
    public static class JSONHandler
    {
        public static T Load<T>(string json)
        {
            var trace = new MemoryTraceWriter();
            
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                TraceWriter = trace,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented

            });

        }

        public static string Save<T>(T target)
        {
            return JsonConvert.SerializeObject(target, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented

            });
        }
    }
}
