using System;

namespace EntityFrameworkCore.Generator.Reflection
{
    /// <summary>
    /// An <see langword="interface"/> for late binding member accessors.
    /// </summary>
    public interface IMemberAccessor : IMemberInfo
    {
        /// <summary>
        /// Returns the value of the member.
        /// </summary>
        /// <param name="instance">The instance whose member value will be returned.</param>
        /// <returns>The member value for the instance parameter.</returns>
        object GetValue(object instance);

        /// <summary>
        /// Sets the <paramref name="value"/> of the member.
        /// </summary>
        /// <param name="instance">The instance whose member value will be set.</param>
        /// <param name="value">The new value for this member.</param>
        void SetValue(object instance, object value);
    }
}
