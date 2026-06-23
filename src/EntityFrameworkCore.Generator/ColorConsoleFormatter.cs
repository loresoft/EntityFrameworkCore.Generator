using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace EntityFrameworkCore.Generator;

public class ColorConsoleFormatter(IOptionsMonitor<SimpleConsoleFormatterOptions> options)
    : ConsoleFormatter(FormatterName)
{
    public const string FormatterName = "colorFormatter";

    protected SimpleConsoleFormatterOptions FormatterOptions { get; } = options.CurrentValue;

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        if (string.IsNullOrEmpty(message))
            return;

        if (!string.IsNullOrEmpty(FormatterOptions.TimestampFormat))
        {
            var timestamp = DateTimeOffset.Now.ToString(FormatterOptions.TimestampFormat);
            textWriter.Write(timestamp);
        }

        WriteWithColor(
            writer: textWriter,
            value: GetLevelText(logEntry.LogLevel),
            color: GetLevelColor(logEntry.LogLevel));

        textWriter.Write(": ");
        WriteMessage(textWriter, message, logEntry.State);
        textWriter.WriteLine();

        if (logEntry.Exception is not null)
        {
            WriteWithColor(textWriter, logEntry.Exception.ToString(), ConsoleColor.Red);
            textWriter.WriteLine();
        }
    }


    private void WriteWithColor(TextWriter writer, string value, ConsoleColor color)
    {
        if (FormatterOptions.ColorBehavior == LoggerColorBehavior.Disabled)
            writer.Write(value);
        else
            writer.WriteWithColor(value, background: null, foreground: color);
    }

    private void WriteMessage<TState>(TextWriter writer, string message, TState state)
    {
        if (FormatterOptions.ColorBehavior == LoggerColorBehavior.Disabled
            || !TryGetTemplateValues(state, out var messageTemplate, out var values)
            || values.Count == 0)
        {
            writer.Write(message);
            return;
        }

        var template = messageTemplate.AsSpan();
        var valueIndex = 0;
        var literalStart = 0;

        for (var index = 0; index < template.Length; index++)
        {
            var current = template[index];
            if (current == '{')
            {
                if (index + 1 < template.Length && template[index + 1] == '{')
                {
                    writer.Write(template[literalStart..index]);
                    writer.Write('{');
                    index++;
                    literalStart = index + 1;

                    continue;
                }

                var closeIndex = template[(index + 1)..].IndexOf('}');
                if (closeIndex < 0 || valueIndex >= values.Count)
                    continue;

                closeIndex += index + 1;

                writer.Write(template[literalStart..index]);

                var placeholder = template[(index + 1)..closeIndex];
                var value = FormatValue(values[valueIndex].Value, placeholder);

                WriteWithColor(writer, value, ConsoleColor.Cyan);

                valueIndex++;
                index = closeIndex;
                literalStart = index + 1;
            }
            else if (current == '}' && index + 1 < template.Length && template[index + 1] == '}')
            {
                writer.Write(template[literalStart..index]);
                writer.Write('}');

                index++;
                literalStart = index + 1;
            }
        }

        if (literalStart < template.Length)
            writer.Write(template[literalStart..]);
    }

    private static bool TryGetTemplateValues<TState>(
        TState state,
        out string messageTemplate,
        out List<KeyValuePair<string, object?>> values)
    {
        messageTemplate = string.Empty;
        values = [];

        if (state is not IEnumerable<KeyValuePair<string, object?>> properties)
            return false;

        foreach (var property in properties)
        {
            if (property.Key == "{OriginalFormat}" && property.Value is string originalFormat)
            {
                messageTemplate = originalFormat;
                continue;
            }

            values.Add(property);
        }

        return !string.IsNullOrEmpty(messageTemplate);
    }

    private static string FormatValue(object? value, ReadOnlySpan<char> placeholder)
    {
        var formatStart = placeholder.IndexOfAny(',', ':');
        var argument = FormatArgument(value);

        if (formatStart < 0)
            return Convert.ToString(argument, CultureInfo.InvariantCulture) ?? string.Empty;

        try
        {
            return string.Format(CultureInfo.InvariantCulture, $"{{0{placeholder[formatStart..]}}}", argument);
        }
        catch (FormatException)
        {
            return Convert.ToString(argument, CultureInfo.InvariantCulture) ?? string.Empty;
        }
    }

    private static object FormatArgument(object? value)
    {
        if (value is null)
            return "(null)";

        if (value is string)
            return value;

        if (value is not System.Collections.IEnumerable enumerable)
            return value;

        var builder = new StringBuilder();
        foreach (var item in enumerable)
        {
            if (builder.Length > 0)
                builder.Append(", ");

            builder.Append(item ?? "(null)");
        }

        return builder.ToString();
    }

    private static string GetLevelText(LogLevel level)
        => level switch
        {
            LogLevel.Trace => "T",
            LogLevel.Debug => "D",
            LogLevel.Information => "I",
            LogLevel.Warning => "W",
            LogLevel.Error => "E",
            LogLevel.Critical => "F",
            _ => "T"
        };

    private static ConsoleColor GetLevelColor(LogLevel level)
        => level switch
        {
            LogLevel.Trace => ConsoleColor.DarkGray,
            LogLevel.Debug => ConsoleColor.DarkCyan,
            LogLevel.Information => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.Magenta,
            _ => ConsoleColor.Gray
        };
}
