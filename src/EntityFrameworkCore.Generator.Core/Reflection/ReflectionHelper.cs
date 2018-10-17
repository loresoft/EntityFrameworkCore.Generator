using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFrameworkCore.Generator.Reflection
{
    /// <summary>
    /// Reflection helper methods
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="TValue">The of the property value.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        ///     Not a <see cref="MemberExpression"/><br/>
        ///     The <see cref="MemberExpression"/> does not represent a property.<br/>
        ///     Or, the property is static.
        /// </exception>
        public static string ExtractPropertyName<TValue>(Expression<Func<TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyName(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p =&gt; p.PropertyName)</param>
        /// <returns>
        /// The name of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        ///   
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        /// Not a <see cref="MemberExpression"/><br/>
        /// The <see cref="MemberExpression"/> does not represent a property.<br/>
        /// Or, the property is static.
        ///   </exception>
        public static string ExtractPropertyName<TSource, TValue>(Expression<Func<TSource, TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyName(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <param name="memberExpression">The member expression</param>
        /// <returns>
        /// The name of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="memberExpression"/> is null.</exception>
        ///   
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        /// Not a <see cref="MemberExpression"/><br/>
        /// The <see cref="MemberExpression"/> does not represent a property.<br/>
        /// Or, the property is static.
        ///   </exception>
        public static string ExtractPropertyName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
                throw new ArgumentNullException(nameof(memberExpression));

            return memberExpression.Member.Name;
        }


        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="TValue">The of the property value.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        ///     Not a <see cref="MemberExpression"/><br/>
        ///     The <see cref="MemberExpression"/> does not represent a property.<br/>
        ///     Or, the property is static.
        /// </exception>
        public static string ExtractColumnName<TValue>(Expression<Func<TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractColumnName(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p =&gt; p.PropertyName)</param>
        /// <returns>
        /// The name of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        ///   
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        /// Not a <see cref="MemberExpression"/><br/>
        /// The <see cref="MemberExpression"/> does not represent a property.<br/>
        /// Or, the property is static.
        ///   </exception>
        public static string ExtractColumnName<TSource, TValue>(Expression<Func<TSource, TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractColumnName(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <param name="memberExpression">The member expression</param>
        /// <returns>
        /// The name of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="memberExpression"/> is null.</exception>
        ///   
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        /// Not a <see cref="MemberExpression"/><br/>
        /// The <see cref="MemberExpression"/> does not represent a property.<br/>
        /// Or, the property is static.
        ///   </exception>
        public static string ExtractColumnName(MemberExpression memberExpression)
        {
            var property = ExtractPropertyInfo(memberExpression);

#if NET40
            var display = Attribute.GetCustomAttribute(property, typeof(System.ComponentModel.DataAnnotations.DisplayAttribute)) as System.ComponentModel.DataAnnotations.DisplayAttribute;
            if (!string.IsNullOrEmpty(display?.Name))
                return display.Name;
#else
            var column = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.ColumnAttribute>();
            if (!string.IsNullOrEmpty(column?.Name))
                return column.Name;

            var display = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
            if (!string.IsNullOrEmpty(display?.Name))
                return display.Name;
#endif

            return property.Name;
        }


        /// <summary>
        /// Extracts the <see cref="PropertyInfo"/> from the specified property expression.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyInfo ExtractPropertyInfo<TValue>(Expression<Func<TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyInfo(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the <see cref="PropertyInfo"/> from the specified property expression.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyInfo ExtractPropertyInfo<TSource, TValue>(Expression<Func<TSource, TValue>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyInfo(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        /// Extracts the <see cref="PropertyInfo"/> from the specified member expression.
        /// </summary>
        /// <param name="memberExpression">The member expression.</param>
        /// <returns></returns>
        public static PropertyInfo ExtractPropertyInfo(MemberExpression memberExpression)
        {
            if (memberExpression == null)
                throw new ArgumentException("The expression is not a member access expression.", nameof(memberExpression));

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("The member access expression does not access a property.", nameof(memberExpression));

            return property;
        }


        /// <summary>
        /// Gets the underlying type dealing with <see cref="Nullable"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns a type dealing with <see cref="Nullable"/>.</returns>
        public static Type GetUnderlyingType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var t = type;
            var typeInfo = t.GetTypeInfo();

            bool isNullable = typeInfo.IsGenericType && (t.GetGenericTypeDefinition() == typeof(Nullable<>));
            if (isNullable)
                return Nullable.GetUnderlyingType(t);

            return t;
        }


        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is a collection.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="type"/> is a collection; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCollection(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type
                .GetTypeInfo()
                .GetInterfaces()
                .Union(new[] { type })
                .Any(x => x == typeof(ICollection) || (x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)));
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is a collection.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="elementType">The Type of the generic element.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="type"/> is a collection; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCollection(this Type type, out Type elementType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            elementType = type;
            var collectionType = type
                .GetTypeInfo()
                .GetInterfaces()
                .Union(new[] { type })
                .FirstOrDefault(t => t.GetTypeInfo().IsGenericType && (t.GetGenericTypeDefinition() == typeof(ICollection<>)));

            if (collectionType == null)
                return false;

            elementType = collectionType.GetTypeInfo().GetGenericArguments().Single();
            return true;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is a dictionary.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="type"/> is a dictionary; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDictionary(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type
                .GetTypeInfo()
                .GetInterfaces()
                .Union(new[] { type })
                .Any(x => x == typeof(IDictionary) || (x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>)));
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> is a dictionary.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="keyType">Type of the generic key.</param>
        /// <param name="elementType">The Type of the generic element.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="type"/> is a dictionary; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDictionary(this Type type, out Type keyType, out Type elementType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            keyType = type;
            elementType = type;

            var collectionType = type
                .GetTypeInfo()
                .GetInterfaces()
                .Union(new[] { type })
                .FirstOrDefault(t => t.GetTypeInfo().IsGenericType && (t.GetGenericTypeDefinition() == typeof(IDictionary<,>)));

            if (collectionType == null)
                return false;

            var arguments = collectionType.GetTypeInfo().GetGenericArguments();
            keyType = arguments.First();
            elementType = arguments.Skip(1).First();

            return true;
        }


        /// <summary>
        /// Attempts to coerce a value of one type into
        /// a value of a different type.
        /// </summary>
        /// <param name="desiredType">
        /// Type to which the value should be coerced.MO
        /// </param>
        /// <param name="valueType">
        /// Original type of the value.
        /// </param>
        /// <param name="value">
        /// The value to coerce.
        /// </param>
        /// <remarks>
        /// <para>
        /// If the desired type is a primitive type or Decimal, 
        /// empty string and null values will result in a 0 
        /// or equivalent.
        /// </para>
        /// <para>
        /// If the desired type is a <see cref="Nullable"/> type, empty string
        /// and null values will result in a null result.
        /// </para>
        /// <para>
        /// If the desired type is an <c>enum</c> the value's ToString()
        /// result is parsed to convert into the <c>enum</c> value.
        /// </para>
        /// </remarks>
        public static object CoerceValue(Type desiredType, Type valueType, object value)
        {
            if (desiredType == null)
                throw new ArgumentNullException(nameof(desiredType));

            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));

            // types match, just copy value
            if (desiredType == valueType)
                return value;

            bool isNullable = desiredType.GetTypeInfo().IsGenericType && (desiredType.GetGenericTypeDefinition() == typeof(Nullable<>));
            if (isNullable)
            {
                if (value == null)
                    return null;
                if (typeof(string) == valueType && Convert.ToString(value) == string.Empty)
                    return null;
            }

            desiredType = GetUnderlyingType(desiredType);

            if ((desiredType.GetTypeInfo().IsPrimitive || typeof(decimal) == desiredType)
                && typeof(string) == valueType
                && string.IsNullOrEmpty((string)value))
                return 0;

            if (value == null)
                return null;

            // types don't match, try to convert
            if (typeof(Guid) == desiredType)
                return new Guid(value.ToString());

            if (desiredType.GetTypeInfo().IsEnum && typeof(string) == valueType)
                return Enum.Parse(desiredType, value.ToString(), true);

            bool isBinary = desiredType.IsArray && typeof(byte[]) == desiredType;

            if (isBinary && typeof(string) == valueType)
            {
                byte[] bytes = Convert.FromBase64String((string)value);
                return bytes;
            }

            isBinary = valueType.IsArray && typeof(byte[]) == valueType;

            if (isBinary && typeof(string) == desiredType)
            {
                byte[] bytes = (byte[])value;
                return Convert.ToBase64String(bytes);
            }

            try
            {
                if (typeof(string) == desiredType)
                    return value.ToString();

                return Convert.ChangeType(value, desiredType);
            }
            catch
            {
#if !SILVERLIGHT
                var converter = TypeDescriptor.GetConverter(desiredType);
                if (converter.CanConvertFrom(valueType))
                    return converter.ConvertFrom(value);
#endif
                throw;
            }
        }


        /// <summary>
        /// Determines whether the specified <paramref name="method"/> overrides a base method.
        /// </summary>
        /// <param name="method">The method information.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="method"/> overrides a base method; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">methodInfo</exception>
        public static bool IsOverriding(this MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            return method.DeclaringType != method.GetBaseDefinition().DeclaringType;
        }

    }
}
