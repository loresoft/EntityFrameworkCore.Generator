namespace System.Text;

/// <summary>
/// A thin wrapper over <see cref="StringBuilder" /> that adds indentation to each line built.
/// </summary>
public sealed class IndentedStringBuilder
{
    private const byte IndentSize = 4;
    private byte _indent;
    private bool _indentPending = true;

    private readonly StringBuilder _stringBuilder = new();

    /// <summary>
    /// The current length of the built string.
    /// </summary>
    public int Length
        => _stringBuilder.Length;

    /// <summary>
    /// Appends the current indent and then the given string to the string being built.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder Append(string value)
    {
        DoIndent();

        _stringBuilder.Append(value);

        return this;
    }

    /// <summary>
    /// Appends the current indent and then the given char to the string being built.
    /// </summary>
    /// <param name="value">The char to append.</param>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder Append(char value)
    {
        DoIndent();

        _stringBuilder.Append(value);

        return this;
    }

    /// <summary>
    /// Appends the current indent and then the given strings to the string being built.
    /// </summary>
    /// <param name="value">The strings to append.</param>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder Append(IEnumerable<string> value)
    {
        DoIndent();

        foreach (var str in value)
            _stringBuilder.Append(str);

        return this;
    }

    /// <summary>
    ///     Appends the current indent and then the given chars to the string being built.
    /// </summary>
    /// <param name="value">The chars to append.</param>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder Append(IEnumerable<char> value)
    {
        DoIndent();

        foreach (var chr in value)
            _stringBuilder.Append(chr);

        return this;
    }

    /// <summary>
    /// Appends the current indent and then the string representation of the given value to the string being built.
    /// </summary>
    public IndentedStringBuilder Append<T>(T value, string? format = null, IFormatProvider? formatProvider = null) where T : IFormattable
    {
        string text = value.ToString(format, formatProvider ?? Globalization.CultureInfo.InvariantCulture);
        Append(text);

        return this;
    }


    /// <summary>
    ///     Appends a new line to the string being built.
    /// </summary>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder AppendLine()
    {
        AppendLine(string.Empty);

        return this;
    }

    /// <summary>
    ///     <para>
    ///         Appends the current indent, the given string, and a new line to the string being built.
    ///     </para>
    ///     <para>
    ///         If the given string itself contains a new line, the part of the string after that new line will not be indented.
    ///     </para>
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder AppendLine(string value)
    {
        if (value.Length != 0)
            DoIndent();

        _stringBuilder.AppendLine(value);

        _indentPending = true;

        return this;
    }

    /// <summary>
    /// Appends the current indent and then the string representation of the given value to the string being built.
    /// </summary>
    public IndentedStringBuilder AppendLine<T>(T value, string? format = null, IFormatProvider? formatProvider = null) where T : IFormattable
    {
        string text = value.ToString(format, formatProvider ?? Globalization.CultureInfo.InvariantCulture);
        AppendLine(text);

        return this;
    }


    /// <summary>
    /// Appends a copy of the specified string if <paramref name="condition"/> is met.
    /// </summary>
    /// <param name="text">The string to append.</param>
    /// <param name="condition">The condition delegate to evaluate. If condition is null, String.IsNullOrWhiteSpace method will be used.</param>
    public IndentedStringBuilder AppendIf(string text, Func<string, bool>? condition = null)
    {
        var c = condition ?? (s => !string.IsNullOrEmpty(s));

        if (c(text))
            Append(text);

        return this;
    }

    /// <summary>
    /// Appends a copy of the specified string if <paramref name="condition"/> is met.
    /// </summary>
    /// <param name="text">The string to append.</param>
    /// <param name="condition">The condition delegate to evaluate. If condition is null, String.IsNullOrWhiteSpace method will be used.</param>
    public IndentedStringBuilder AppendIf(string text, bool condition)
    {
        if (condition)
            Append(text);

        return this;
    }

    /// <summary>
    /// Appends a copy of the specified string followed by the default line terminator if <paramref name="condition"/> is met.
    /// </summary>
    /// <param name="text">The string to append.</param>
    /// <param name="condition">The condition delegate to evaluate. If condition is null, String.IsNullOrWhiteSpace method will be used.</param>
    public IndentedStringBuilder AppendLineIf(string text, Func<string, bool>? condition = null)
    {
        var c = condition ?? (s => !string.IsNullOrEmpty(s));

        if (c(text))
            AppendLine(text);

        return this;
    }

    /// <summary>
    ///     Resets this builder ready to build a new string.
    /// </summary>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder Clear()
    {
        _stringBuilder.Clear();
        _indent = 0;

        return this;
    }

    /// <summary>
    ///     Increments the indent.
    /// </summary>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder IncrementIndent()
    {
        _indent++;

        return this;
    }

    /// <summary>
    ///     Decrements the indent.
    /// </summary>
    /// <returns>This builder so that additional calls can be chained.</returns>
    public IndentedStringBuilder DecrementIndent()
    {
        if (_indent > 0)
            _indent--;

        return this;
    }

    /// <summary>
    ///     Creates a scoped indenter that will increment the indent, then decrement it when disposed.
    /// </summary>
    /// <returns>An indenter.</returns>
    public IDisposable Indent()
        => new Indenter(this);

    /// <summary>
    ///     Temporarily disables all indentation. Restores the original indentation when the returned object is disposed.
    /// </summary>
    /// <returns>An object that restores the original indentation when disposed.</returns>
    public IDisposable SuspendIndent()
        => new IndentSuspender(this);

    /// <summary>
    ///     Returns the built string.
    /// </summary>
    /// <returns>The built string.</returns>
    public override string ToString()
        => _stringBuilder.ToString();

    private void DoIndent()
    {
        if (_indentPending && _indent > 0)
            _stringBuilder.Append(' ', _indent * IndentSize);

        _indentPending = false;
    }

    private readonly struct Indenter : IDisposable
    {
        private readonly IndentedStringBuilder _stringBuilder;

        public Indenter(IndentedStringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;

            _stringBuilder.IncrementIndent();
        }

        public void Dispose()
            => _stringBuilder.DecrementIndent();
    }

    private readonly struct IndentSuspender : IDisposable
    {
        private readonly IndentedStringBuilder _stringBuilder;
        private readonly byte _indent;

        public IndentSuspender(IndentedStringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
            _indent = _stringBuilder._indent;
            _stringBuilder._indent = 0;
        }

        public void Dispose()
            => _stringBuilder._indent = _indent;
    }
}
