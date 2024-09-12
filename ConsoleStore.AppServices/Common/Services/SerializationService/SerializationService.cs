using System.Text.Json;

/// <summary>
/// Сервис сериализации для хранения данных пользователя.
/// </summary>
public class SerializationService //: ISerializationService
{
    /// <inheritdoc/>
    public void SerializeToJson<T>(T? obj, string login, string catalogPath)
    {
        using FileStream fs = new FileStream(@$"{catalogPath}{login}.json", FileMode.Create);

        JsonSerializer.Serialize(fs, obj);
    }

    /// <inheritdoc/>
    public T? DeserializeJson<T>(string catalogPath, string login)
    {
        using FileStream fs = new FileStream($"{catalogPath}{login}.json", FileMode.Open);

        var obj = JsonSerializer.Deserialize<T>(fs);

        return obj;
    }
}