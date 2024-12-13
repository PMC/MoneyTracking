using System.Reflection;
using System.Text.Json;

namespace MoneyTracking.Transactions;
public static class JsonSerializerHelper
{
    public static string GetFullPathToJsonFile(string fileName)
    {
        // Get the path to the executing assembly (your program)
        var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var dataPath = Path.Combine(appPath, "Data");

        // Create data directory if needed
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        // Construct the full path to the data file
        return Path.Combine(dataPath, fileName);
    }

    private static readonly JsonSerializerOptions s_writeOptions = new()
    {
        WriteIndented = true,
    };

    private static readonly JsonSerializerOptions s_readOptions = new()
    {
        AllowTrailingCommas = true
    };

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, s_writeOptions);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, s_readOptions);
    }
    public static void SerializeToFile<T>(string filePath, T value)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            string json = JsonSerializer.Serialize(value, s_writeOptions);
            writer.Write(json);
        }
    }
}
