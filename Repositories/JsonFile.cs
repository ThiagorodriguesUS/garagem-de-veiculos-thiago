using System.Text.Json;
namespace GaragemVeiculos.Repositories;
internal static class JsonFile
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    public static List<T> Read<T>(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        if (!File.Exists(path)) File.WriteAllText(path, "[]");
        return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path), Options) ?? new();
    }
    public static void Write<T>(string path, List<T> items)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var temp = path + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(items, Options));
        File.Move(temp, path, true);
    }
    public static string PathFor(IWebHostEnvironment env, string file) => Path.Combine(env.ContentRootPath, "Data", file);
}
