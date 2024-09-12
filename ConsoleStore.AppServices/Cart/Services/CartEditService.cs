using ConsoleStore.AppServices.Cart.Repositories;
using ConsoleStore.AppServices.Cart.Services;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.Products.Models;

namespace ConsoleStore.Cart.Services;

class CartEditService : CartService
{
    Product Product { get; }

    public CartEditService(Product product) => Product = product;

    private void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("КОРЗИНА");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Для изменения товара введите нужное число\n" +
            " · Удалить товар из корзины - Del\n" +
            " · Назад - 0"
        );

        PrintService.PrintSeparator();

        Console.WriteLine(
            $"{Product.Name} | {Product.Price} р | {CartRepository.Cart[Product]} шт.\n" +
            $"(Всего на складе: {Product.Count} )"
        );

        PrintService.PrintSeparator();
    }

    private bool EditProductCount(Product product, int newCount)
    {
        if (product.Count < newCount)
        {
            PrintService.PrintColorMessage(" Данного количества нет на складе", ConsoleColor.Yellow);
            return false;
        }

        CartRepository.Cart[product] = newCount;

        return true;
    }

    private void RemoveProduct(Product product)
    {
        CartRepository.Cart.Remove(product);
    }

    public new void Show()
    {
        PrintHeader();

        Console.WriteLine();

        string? choice;
        while (true)
        {
            Console.Write("Дейсвие: ");

            choice = Console.ReadLine()?
                .Trim()
                .ToLower();

            if (choice == "0")
                return;

            if (choice == "del")
            {
                RemoveProduct(Product);
                PrintService.PrintColorMessage("\n Товар удалён!", ConsoleColor.Green, false);

                break;
            }
            else if (int.TryParse(choice, out var newCount))
            {
                if (!EditProductCount(Product, newCount))
                    continue;

                PrintService.PrintColorMessage("\n Изменено!", ConsoleColor.Green, false);
                break;
            }

            PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
            continue;
        }

        PrintService.PrintColorMessage(" Нажмите любую клавишу, чтобы вернуться назад...", ConsoleColor.DarkGray, false);
        Console.ReadKey();

        return;
    }
}
