using System;
using System.Reflection;

namespace EntityFrameworkCore.Generator.Reflection
{
    internal static class DelegateFactory
    {
        public static Func<object, object[], object> CreateMethod(MethodInfo methodInfo)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateMethod(methodInfo);
#else
            return DynamicMethodFactory.CreateMethod(methodInfo);
#endif
        }

        public static Func<object> CreateConstructor(Type type)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateConstructor(type);
#else
            return DynamicMethodFactory.CreateConstructor(type);
#endif
        }

        public static Func<object, object> CreateGet(PropertyInfo propertyInfo)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateGet(propertyInfo);
#else
            return DynamicMethodFactory.CreateGet(propertyInfo);
#endif
        }

        public static Func<object, object> CreateGet(FieldInfo fieldInfo)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateGet(fieldInfo);
#else
            return DynamicMethodFactory.CreateGet(fieldInfo);
#endif
        }

        public static Action<object, object> CreateSet(PropertyInfo propertyInfo)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateSet(propertyInfo);
#else
            return DynamicMethodFactory.CreateSet(propertyInfo);
#endif
        }

        public static Action<object, object> CreateSet(FieldInfo fieldInfo)
        {
#if SILVERLIGHT
            return ExpressionFactory.CreateSet(fieldInfo);
#else
            return DynamicMethodFactory.CreateSet(fieldInfo);
#endif
        }
    }
}