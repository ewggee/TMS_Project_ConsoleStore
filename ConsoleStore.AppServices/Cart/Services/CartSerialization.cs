using ConsoleStore.AppServices.Cart.Repositories;
using ConsoleStore.AppServices.Common;
using ConsoleStore.Common.Services.PrintService;
using System.Text.Json;

namespace ConsoleStore.AppServices.Cart.Services;

public class CartSerialization : IJsonSerialization<CartRepository>
{
    /// <summary>
    /// Путь к папке с данными.
    /// </summary>
    private const string CATALOG = "C:\\Users\\Evgeniy\\source\\repos\\TMS_Project_ConsoleStore\\ConsoleStore.Data\\Users\\";

    /// <inheritdoc/>
    public void Serialize(CartRepository obj, string login)
    {
        using FileStream fs = new FileStream(CATALOG + $"{login}\\" + "Cart.json", FileMode.Create);

        JsonSerializer.Serialize(fs, obj);
    }

    /// <inheritdoc/>
    public CartRepository Deserialize(string login)
    {
        try
        {
            using FileStream fs = new FileStream(CATALOG + $"{login}\\" + "Cart.json", FileMode.Open);

            var obj = JsonSerializer.Deserialize<CartRepository>(fs);

            return obj!;
        }
        catch (FileNotFoundException)
        {
            PrintService.PrintColorMessage(" ! Файл не найден", ConsoleColor.Red);
            throw new FileNotFoundException("Файл не найден");
        }
    }
}
