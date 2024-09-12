using ConsoleStore.AppServices.Cart.Repositories;
using ConsoleStore.AppServices.Common;
using ConsoleStore.AppServices.OrderHistory.Repositories;
using ConsoleStore.Common.Services.PrintService;
using System.Text.Json;

namespace ConsoleStore.AppServices.OrderHistory.Services;

public class OrderHistorySerialization : IJsonSerialization<OrderHistoryRepository>
{
    private const string CATALOG = "C:\\Users\\Evgeniy\\source\\repos\\TMS_Project_ConsoleStore\\ConsoleStore.Data\\Users\\";

    /// <inheritdoc/>
    public void Serialize(OrderHistoryRepository obj, string login)
    {
        using FileStream fs = new FileStream(CATALOG + $"{login}\\" + "OrderHistory.json", FileMode.Create);

        JsonSerializer.Serialize(fs, obj);
    }

    /// <inheritdoc/>
    public OrderHistoryRepository Deserialize(string login)
    {
        try
        {
            using FileStream fs = new FileStream(CATALOG + $"{login}\\" + "OrderHistory.json", FileMode.Open);

            var obj = JsonSerializer.Deserialize<OrderHistoryRepository>(fs);

            return obj!;
        }
        catch (FileNotFoundException)
        {
            PrintService.PrintColorMessage(" ! Файл не найден", ConsoleColor.Red);
            throw new FileNotFoundException("Файл не найден");
        }
    }
}
