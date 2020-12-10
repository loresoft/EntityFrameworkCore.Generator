using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EntityFrameworkCore.Generator.ModelCache
{
    public class AnnotationsContractResolver : DefaultContractResolver
    {
        public static readonly AnnotationsContractResolver Instance =
            new AnnotationsContractResolver();

        private static readonly Dictionary<Type, JsonProperty> _annotationPropertyByDeclaryingType =
            new Dictionary<Type, JsonProperty>();

        public static IEnumerable<Type> DeclaringTypes => _annotationPropertyByDeclaryingType.Keys;

        public static JsonProperty GetAnnotationProperty(Type declaringType)
        {
            if (!_annotationPropertyByDeclaryingType.TryGetValue(declaringType, out var property))
            {
                property = new JsonProperty
                {
                    DeclaringType = declaringType,
                    PropertyType = typeof(IEnumerable<Annotation>),
                    PropertyName = "$annotations",
                    Readable = true,
                    Writable = true,
                    ValueProvider = AnnotationsValueProvider.Instance,
                };
                _annotationPropertyByDeclaryingType.Add(declaringType, property);
            }
            return property;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = base.CreateProperties(type, memberSerialization);

            if (typeof(Annotatable).IsAssignableFrom(type))
            {
                props.Add(GetAnnotationProperty(type));
            }

            return props;
        }
    }
}