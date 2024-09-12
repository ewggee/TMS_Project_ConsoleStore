using ConsoleStore.Common;
using ConsoleStore.Products.Models;
using ConsoleStore.OrderHistory.Models;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.AppServices.OrderHistory.Repositories;

namespace ConsoleStore.OrderHistory.Services;

public class OrderHistoryService : Representable
{
    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("ИСТОРИЯ ЗАКАЗОВ");
        PrintService.PrintSeparator();
    }

    public void AppendOrder(Dictionary<Product, int> products)
    {
        var order = new Dictionary<Product, int>(products);
        OrderHistoryRepository.Purchases.Push(new OrderInfo(order));
    }

    public override void Show()
    {
        PrintHeader();

        var i = 1;
        foreach (var item in OrderHistoryRepository.Purchases)
        {
            PrintService.PrintColorMessage($" {i}) ", ConsoleColor.DarkGray, false);

            Console.WriteLine(
                $"{item.DateTime:HH:mm, dd.MM.yyyy}"
            );

            item.ProductsInfo();

            //Console.WriteLine(
            //    $"  Сумма: {item.Sum}"
            //);

            //PrintColorMessage(" Сумма: ", ConsoleColor.DarkGray, false);
            Console.Write(" Сумма: ");

            PrintService.PrintColorMessage($"{item.Sum}", ConsoleColor.DarkGreen, false);

            Console.WriteLine("\n");

            i++;
        }

        PrintService.PrintColorMessage(" Нажмите любую клавишу, чтобы вернуться назад...", ConsoleColor.DarkGray, false);
        Console.ReadKey();
    }
}
