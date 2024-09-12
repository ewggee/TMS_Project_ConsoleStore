namespace ConsoleStore.Common;

/// <summary>
/// Абстрактный класс для сущностей, которые выводят информацию на консоль.
/// </summary>
public abstract class Representable
{
    /// <summary>
    /// Вывод заголовка.
    /// </summary>
    protected abstract void PrintHeader();
    /// <summary>
    /// Вывод основной информации.
    /// </summary>
    public abstract void Show();
}
