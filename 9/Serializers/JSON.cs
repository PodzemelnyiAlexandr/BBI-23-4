using System.Text.Json;

public class JSON : SerializeClass 
{
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };
    public override void Write<T>(T obj, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj, options);
        }
    }
    public override T Read<T>(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
    }
}