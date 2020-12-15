using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

namespace EntityFrameworkCore.Generator.ModelCache
{
    public class AnnotationsJsonConverter : JsonConverter<IEnumerable<Annotation>>
    {
        public static readonly AnnotationsJsonConverter Instance = new AnnotationsJsonConverter();

        public override void WriteJson(JsonWriter writer, [AllowNull] IEnumerable<Annotation> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var a in value)
            {
                writer.WritePropertyName(a.Name);
                serializer.Serialize(writer, a.Value);
            }
            writer.WriteEndObject();
        }

        public override IEnumerable<Annotation> ReadJson(JsonReader reader, Type objectType, [AllowNull] IEnumerable<Annotation> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
                var list = new List<Annotation>();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TokenType != JsonToken.PropertyName)
                        throw new JsonSerializationException("Unexpected token type found where property name expected in Annotation object:" + reader.TokenType);
                    var name = (string)reader.Value;
                    //Console.WriteLine("Reading ann: " + name);
                    reader.Read();
                    var value = serializer.Deserialize(reader);
                    reader.Read();
                    list.Add(new Annotation(name, value));
                }
                return list;
            }
            else
            {
                throw new JsonSerializationException("Unexpected token type found at start of Annotation: " + reader.TokenType);
            }
        }
    }
}