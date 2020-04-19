using System;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Generator.Extensions;
using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// String match options
    /// </summary>
    public class MatchOptions
    {
        /// <summary>
        /// Gets or sets the exact string match option.
        /// </summary>
        /// <value>
        /// The exact string match option.
        /// </value>
        [YamlMember(Alias = "exact")] 
        public string Exact { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern match option.
        /// </summary>
        /// <value>
        /// The regular expression pattern match option.
        /// </value>
        [YamlMember(Alias = "regex")] 
        public string Expression { get; set; }
        
        /// <summary>
        /// Determines whether the specified value is a match.
        /// </summary>
        /// <param name="value">The value to match.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is a match; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(string value)
        {
            if (Exact.HasValue())
                return string.Equals(value, Exact);
            
            if (Expression.HasValue())
                return Regex.IsMatch(value, Expression);

            return false;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="MatchOptions"/>.
        /// </summary>
        /// <param name="value">The value to use for conversion.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator MatchOptions(string value)
        {
            return new MatchOptions
            {
                Expression = value
            };
        }

        /// <summary>
        /// Determines whether the specified <see cref="MatchOptions" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="MatchOptions" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="MatchOptions" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected bool Equals(MatchOptions other)
        {
            return Exact == other.Exact && Expression == other.Expression;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;

            if (ReferenceEquals(this, obj)) 
                return true;

            if (obj.GetType() != this.GetType()) 
                return false;

            return Equals((MatchOptions) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Exact, Expression);
        }
    }
}