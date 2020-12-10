using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace EntityFrameworkCore.Generator.ModelCache
{
    public class AnnotationsValueProvider : IValueProvider
    {
        public static readonly AnnotationsValueProvider Instance = new AnnotationsValueProvider();

        public object GetValue(object target)
        {
            var annotatable = (Annotatable)target;
            var annotations = annotatable.GetAnnotations();
            if (annotations == null || annotations.Count() == 0)
            {
                return null;
            }
            return annotations; //.Select(a => KeyValuePair.Create(a.Name, a.Value)).ToList();
        }

        public void SetValue(object target, object value)
        {
            if (target is Annotatable annotatable
                && value is IEnumerable<Annotation> annotations)
            {
                foreach (var a in annotations)
                {
                    annotatable.AddAnnotation(a.Name, a.Value);
                }
            }
        }
    }
}