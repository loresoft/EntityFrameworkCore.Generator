using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntityFrameworkCore.Generator
{
    public class VariableDictionary
    {
        private readonly Dictionary<string, string> _variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Sets a variable value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void Set(string name, string value)
        {
            if (name == null)
                return;

            _variables[name] = value;
        }

        /// <summary>
        /// Gets or sets a variable by name.
        /// </summary>
        /// <param name="name">The name of the variable to set.</param>
        /// <returns>The current (evaluated) value of the variable.</returns>
        public string this[string name]
        {
            get { return Get(name); }
            set { Set(name, value); }
        }

        /// <summary>
        /// Performs the raw (not evaluated) value of a variable.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns>The value of the variable, or null if one is not defined.</returns>
        public string GetRaw(string variableName)
        {
            string variable;
            if (_variables.TryGetValue(variableName, out variable) && variable != null)
                return variable;

            return null;
        }

        /// <summary>
        /// Gets the value of a variable, or returns a default value if the variable is not defined. If the variable contains an expression, it will be evaluated first.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The value of the variable, or the default value if the variable is not defined.</returns>
        public string Get(string variableName)
        {
            if (!_variables.TryGetValue(variableName, out var variable))
                return null;

            return Evaluate(variable);
        }

        /// <summary>
        /// Evaluates the specified variable or text.
        /// </summary>
        /// <param name="variableOrText">The variable or text.</param>
        /// <returns>The result of the variable.</returns>
        /// <exception cref="System.FormatException">Invalid variable format</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Variable not found</exception>
        /// <exception cref="System.InvalidOperationException">Invalid parse state.</exception>
        public string Evaluate(string variableOrText)
        {
            if (variableOrText == null)
                return null;

            var result = new StringBuilder(variableOrText.Length * 2);
            var variable = new StringBuilder();
            var state = State.OutsideExpression;

            using (var reader = new StringReader(variableOrText))
            {
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
                                    variable.Append((char)c);
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
                                    if (!_variables.TryGetValue(variable.ToString(), out string value))
                                        throw new KeyNotFoundException($"Variable '{variable}' not found");

                                    value = Evaluate(value);
                                    result.Append(value);
                                    variable.Length = 0;
                                    state = State.OutsideExpression;
                                    break;
                                default:
                                    variable.Append((char)c);
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
                            throw new InvalidOperationException("Invalid parse state.");
                    }
                } while (state != State.End);
            }

            return result.ToString();
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
