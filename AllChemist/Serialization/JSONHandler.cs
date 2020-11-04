using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace AllChemist.Serialization
{
    public static class JSONHandler
    {
        public static T Load<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
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
