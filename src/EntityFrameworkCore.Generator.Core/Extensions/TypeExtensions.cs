namespace EntityFrameworkCore.Generator.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Gets the underlying type dealing with <see cref="T:Nullable`1"/>.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Returns a type dealing with <see cref="T:Nullable`1"/>.</returns>
    public static Type GetUnderlyingType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return Nullable.GetUnderlyingType(type) ?? type;
    }

    /// <summary>
    /// Determines whether the specified <paramref name="type"/> can be null.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    ///   <c>true</c> if the specified <paramref name="type"/> can be null; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullable(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!type.IsGenericType || type.IsGenericTypeDefinition)
            return false;

        // Instantiated generic type only
        Type genericType = type.GetGenericTypeDefinition();
        return ReferenceEquals(genericType, typeof(Nullable<>));
    }

    public static bool IsNumeric(this Type type)
    {
        type = type.GetUnderlyingType();

        return type.IsInteger()
            || type == typeof(decimal)
            || type == typeof(float)
            || type == typeof(double);
    }

    public static bool IsInteger(this Type type)
    {
        type = type.GetUnderlyingType();

        return type == typeof(int)
            || type == typeof(long)
            || type == typeof(short)
            || type == typeof(byte)
            || type == typeof(uint)
            || type == typeof(ulong)
            || type == typeof(ushort)
            || type == typeof(sbyte)
            || type == typeof(char);
    }

    public static bool IsSignedInteger(this Type type)
        => type == typeof(int)
            || type == typeof(long)
            || type == typeof(short)
            || type == typeof(sbyte);
}
