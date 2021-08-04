using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.YouTube.Converters
{
    /// <summary>
    /// Converts the Youtube ID to a string. 
    /// When querying the Search API, it comes back as a complex object, however when querying the Video API, it comes back as a simple string
    /// </summary>
    class IdJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(string) || objectType == typeof(Models.Video.YT_ID));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Object)
            {
                return token.ToObject<Models.Video.YT_ID>().GetId();
            }
            return token.ToString(); ;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
