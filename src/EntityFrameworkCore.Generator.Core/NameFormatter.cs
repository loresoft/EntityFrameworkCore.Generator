using System;
using System.IO;
using System.Text;
using EntityFrameworkCore.Generator.Reflection;

namespace EntityFrameworkCore.Generator
{
    /// <summary>
    /// Named string formatter.
    /// </summary>
    public static class NameFormatter
    {
        /// <summary>
        /// Replaces each named format item in a specified string with the text equivalent of a corresponding object's property value.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="source">The object to format.</param>
        /// <returns>A copy of format in which any named format items are replaced by the string representation.</returns>
        /// <example>
        /// <code>
        /// var o = new { First = "John", Last = "Doe" };
        /// string result = NameFormatter.Format("Full Name: {First} {Last}", o);
        /// </code>
        /// </example>
        public static string Format(string format, object source)
        {
            if (format == null)
                return null;

            var result = new StringBuilder(format.Length * 2);

            using (var reader = new StringReader(format))
            {
                var expression = new StringBuilder();

                State state = State.OutsideExpression;
                do
                {
                    int c = -1;
                    switch (state)
                    {
                        case State.OutsideExpression:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    state = State.End;
                                    break;
                                case '{':
                                    state = State.OnOpenBracket;
                                    break;
                                case '}':
                                    state = State.OnCloseBracket;
                                    break;
                                default:
                                    result.Append((char)c);
                                    break;
                            }
                            break;
                        case State.OnOpenBracket:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    throw new FormatException();
                                case '{':
                                    result.Append('{');
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    expression.Append((char)c);
                                    state = State.InsideExpression;
                                    break;
                            }
                            break;
                        case State.InsideExpression:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    throw new FormatException();
                                case '}':
                                    string value = Evaluate(source, expression.ToString());
                                    result.Append(value);
                                    expression.Length = 0;
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    expression.Append((char)c);
                                    break;
                            }
                            break;
                        case State.OnCloseBracket:
                            c = reader.Read();
                            switch (c)
                            {
                                case '}':
                                    result.Append('}');
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    throw new FormatException();
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Invalid state.");
                    }
                } while (state != State.End);
            }

            return result.ToString();
        }

        private static string Evaluate(object source, string expression)
        {
            string format = "";

            int colonIndex = expression.IndexOf(':');
            if (colonIndex > 0)
            {
                format = expression.Substring(colonIndex + 1);
                expression = expression.Substring(0, colonIndex);
            }

            try
            {
                object value = LateBinder.GetProperty(source, expression) ?? string.Empty;
                return string.IsNullOrEmpty(format)
                  ? value.ToString()
                  : string.Format("{0:" + format + "}", value);
            }
            catch (InvalidOperationException ex)
            {
                throw new FormatException("One of the identified items was in an invalid format.", ex);
            }
        }

        private enum State
        {
            OutsideExpression,
            OnOpenBracket,
            InsideExpression,
            OnCloseBracket,
            End
        }
    }
}
