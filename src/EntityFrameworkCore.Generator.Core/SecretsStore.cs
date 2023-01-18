using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace EntityFrameworkCore.Generator;

public class SecretsStore
{
    private readonly string _secretsFilePath;
    private readonly IDictionary<string, string> _secrets;

    public SecretsStore(string userSecretsId)
    {
        if (string.IsNullOrEmpty(userSecretsId))
            throw new ArgumentException("Value cannot be null or empty.", nameof(userSecretsId));

        _secretsFilePath = PathHelper.GetSecretsPathFromSecretsId(userSecretsId);

        // workaround bug in configuration
        var secretDir = Path.GetDirectoryName(_secretsFilePath);
        Directory.CreateDirectory(secretDir);

        _secrets = Load(userSecretsId);
    }

    public string this[string key] => _secrets[key];

    public int Count => _secrets.Count;

    public bool ContainsKey(string key) => _secrets.ContainsKey(key);

    public IEnumerable<KeyValuePair<string, string>> AsEnumerable() => _secrets;

    public void Clear() => _secrets.Clear();

    public void Set(string key, string value) => _secrets[key] = value;

    public void Remove(string key)
    {
        if (_secrets.ContainsKey(key))
        {
            _secrets.Remove(key);
        }
    }

    public virtual void Save()
    {
        var secretDir = Path.GetDirectoryName(_secretsFilePath);
        Directory.CreateDirectory(secretDir);

        var options = new JsonWriterOptions
        {
            Indented = true
        };

        using var stream = File.Create(_secretsFilePath);
        using var writer = new Utf8JsonWriter(stream, options);

        writer.WriteStartObject();

        foreach (var secret in _secrets.AsEnumerable())
            writer.WriteString(secret.Key, secret.Value);

        writer.WriteEndObject();
        writer.Flush();
    }

    protected virtual IDictionary<string, string> Load(string userSecretsId)
    {
        return new ConfigurationBuilder()
            .AddJsonFile(_secretsFilePath, optional: true)
            .Build()
            .AsEnumerable()
            .Where(i => i.Value != null)
            .ToDictionary(i => i.Key, i => i.Value, StringComparer.OrdinalIgnoreCase);
    }
}
