using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace IdentityServer4.OpenAdmin.API.Shared.Converters
{
    public class StringToArrayJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var arrayValue = (IEnumerable<string>) value;
            if (arrayValue != null && arrayValue.Count() > 1)
            {
                writer.WriteValue(value);
            }
            else
            {
                writer.WriteValue(arrayValue.FirstOrDefault());
            }
            //if (writer.WriteValue())
            //throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value is string s)
            {
                return new[] {s};
            }

            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}