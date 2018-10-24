using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Generator.Extensions
{
    public static class StringExtensions
    {

        private static readonly Regex _splitNameRegex = new Regex(@"[\W_]+");

        /// <summary>
        /// Indicates whether the specified String object is null or an empty string
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///     <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///      <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string item)
        {
            if (item == null)
                return true;

            for (int i = 0; i < item.Length; i++)
                if (!char.IsWhiteSpace(item[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether the specified string is not <see cref="IsNullOrEmpty"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> is not <see cref="IsNullOrEmpty"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Does string contain both uppercase and lowercase characters?
        /// </summary>
        /// <param name="s">The value.</param>
        /// <returns>True if contain mixed case.</returns>
        public static bool IsMixedCase(this string s)
        {
            if (s.IsNullOrEmpty())
                return false;

            var containsUpper = s.Any(Char.IsUpper);
            var containsLower = s.Any(Char.IsLower);

            return containsLower && containsUpper;
        }

        /// <summary>
        /// Converts a string to use camelCase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The to camel case. </returns>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string output = ToPascalCase(value);
            if (output.Length > 2)
                return char.ToLower(output[0]) + output.Substring(1);

            return output.ToLower();
        }

        /// <summary>
        /// Converts a string to use PascalCase.
        /// </summary>
        /// <param name="value">Text to convert</param>
        /// <returns>The string</returns>
        public static string ToPascalCase(this string value)
        {
            return value.ToPascalCase(_splitNameRegex);
        }

        /// <summary>
        /// Converts a string to use PascalCase.
        /// </summary>
        /// <param name="value">Text to convert</param>
        /// <param name="splitRegex">Regular Expression to split words on.</param>
        /// <returns>The string</returns>
        public static string ToPascalCase(this string value, Regex splitRegex)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var mixedCase = value.IsMixedCase();
            var names = splitRegex.Split(value);
            var output = new StringBuilder();

            if (names.Length > 1)
            {
                foreach (string name in names)
                {
                    if (name.Length > 1)
                    {
                        output.Append(char.ToUpper(name[0]));
                        output.Append(mixedCase ? name.Substring(1) : name.Substring(1).ToLower());
                    }
                    else
                    {
                        output.Append(name);
                    }
                }
            }
            else if (value.Length > 1)
            {
                output.Append(char.ToUpper(value[0]));
                output.Append(mixedCase ? value.Substring(1) : value.Substring(1).ToLower());
            }
            else
            {
                output.Append(value.ToUpper());
            }

            return output.ToString();
        }


    }
}
