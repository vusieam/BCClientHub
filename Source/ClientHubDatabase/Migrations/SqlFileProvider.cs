using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientHubDatabase.Migrations;

public class SqlFileProvider
{
    private readonly IReadOnlyDictionary<string, string> _sqlFiles;


    public SqlFileProvider()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var sqlFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var resourceNames = assembly
            .GetManifestResourceNames()
            .Where(name =>
                name.Contains(".Sql.", StringComparison.OrdinalIgnoreCase) &&
                name.EndsWith(".sql", StringComparison.OrdinalIgnoreCase));

        foreach (var resourceName in resourceNames)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Could not load resource '{resourceName}'.");

            using var reader = new StreamReader(stream);
            var sqlContent = reader.ReadToEnd();

            // Extract just the folder/file name for convenience
            var parts = resourceName.Split('.');

            var folderName = parts[^4]; // 'tables' or 'triggers'
            var fileName = $"{parts[^3]}.{parts[^2]}.{parts[^1]}"; // 'clients.sql'

            sqlFiles[$"{folderName}/{fileName}"] = sqlContent;
        }

        _sqlFiles = sqlFiles;

        if (_sqlFiles.Count == 0)
            throw new InvalidOperationException("No embedded SQL files were found.");
    }


    public IReadOnlyDictionary<string, string> GetAll(string? folder = null)
    {
        if (string.IsNullOrWhiteSpace(folder))
            return _sqlFiles;

        return _sqlFiles
            .Where(kv => kv.Key.StartsWith($"{folder}/", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
    }


    public IReadOnlyDictionary<string, string> GetAll() => _sqlFiles;


}
