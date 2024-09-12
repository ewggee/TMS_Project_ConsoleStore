namespace ConsoleStore.AppServices.Common;

/// <summary>
/// Интерфейс сериализации.
/// </summary>
public interface IJsonSerialization<T> where T : class
{
    /// <summary>
    /// Сериализовать объект.
    /// </summary>
    /// <param name="obj">Объект сериализации.</param>
    /// <param name="login">Логин пользователя.</param>
    void Serialize(T obj, string login);
    /// <summary>
    /// Десериализовать объект.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <returns>Десериализованный объект.</returns>
    T Deserialize(string login);
}
