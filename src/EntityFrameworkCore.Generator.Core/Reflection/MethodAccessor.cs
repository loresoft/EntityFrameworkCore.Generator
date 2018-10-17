using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EntityFrameworkCore.Generator.Reflection
{
    /// <summary>
    /// An accessor class for <see cref="MethodInfo"/>.
    /// </summary>
    [DebuggerDisplay("Name: {Name}")]
    public class MethodAccessor : IMethodAccessor
    {
        private readonly Lazy<Func<object, object[], object>> _invoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodAccessor"/> class.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        public MethodAccessor(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));

            MethodInfo = methodInfo;
            Name = methodInfo.Name;
            _invoker = new Lazy<Func<object, object[], object>>(() => DelegateFactory.CreateMethod(MethodInfo));
        }

        /// <summary>
        /// Gets the method info.
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Invokes the method on the specified <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">The object on which to invoke the method. If a method is static, this argument is ignored.</param>
        /// <param name="arguments">An argument list for the invoked method.</param>
        /// <returns>
        /// An object containing the return value of the invoked method.
        /// </returns>
        public object Invoke(object instance, params object[] arguments)
        {
            return _invoker.Value.Invoke(instance, arguments);
        }

        /// <summary>
        /// Gets the method key using a hash code from the name and paremeter types.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="parameterTypes">The method parameter types.</param>
        /// <returns>The method key</returns>
        internal static int GetKey(string name, IEnumerable<Type> parameterTypes)
        {
            unchecked
            {
                int result = name?.GetHashCode() ?? 0;
                result = parameterTypes.Aggregate(result,
                  (r, p) => (r * 397) ^ (p?.GetHashCode() ?? 0));

                return result;
            }
        }
    }
}
