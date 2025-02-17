using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

namespace Code.Infrastructure.Serialization
{
    public static class JsonSerialization
    {
        static JsonSerialization()
        {
            AotHelper.Ensure(() => new ReferenceConverter(typeof(Dummy)));
        }

        public static string ToJson(this object self)
        {
            return JsonConvert.SerializeObject(self, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public class Dummy
        {
        }
    }
}