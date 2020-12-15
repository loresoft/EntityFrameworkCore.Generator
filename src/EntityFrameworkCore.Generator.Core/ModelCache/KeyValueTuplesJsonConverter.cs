using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;

namespace EntityFrameworkCore.Generator.ModelCache
{
    public class KeyValueTuplesJsonConverter : JsonConverter<IEnumerable<(string, object)>>
    {
        public static readonly KeyValueTuplesJsonConverter Instance =
            new KeyValueTuplesJsonConverter();

        public override void WriteJson(JsonWriter writer, [AllowNull] IEnumerable<(string, object)> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var kv in value.OrderBy(kv => kv.Item1))
            {
                writer.WritePropertyName(kv.Item1);
                serializer.Serialize(writer, kv.Item2);
            }
            writer.WriteEndObject();
        }

        public override IEnumerable<(string, object)> ReadJson(JsonReader reader, Type objectType, [AllowNull] IEnumerable<(string, object)> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
                var list = new List<(string, object)>();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TokenType != JsonToken.PropertyName)
                        throw new JsonSerializationException("Unexpected token type found where property name expected in KeyValueTuples object:" + reader.TokenType);
                    var name = (string)reader.Value;
                    reader.Read();
                    var value = serializer.Deserialize(reader);
                    reader.Read();
                    list.Add((name, value));
                }
                return list;
            }
            else
            {
                throw new JsonSerializationException("Unexpected token type found at start of KeyValueTuples: " + reader.TokenType);
            }
        }
    }
}