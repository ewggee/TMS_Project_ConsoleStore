using ConsoleStore.MainMenu;
using ConsoleStore.Products.Models;
using ConsoleStore.Categories.Services;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.AppServices.Cart.Repositories;
using ConsoleStore.Cart.Services;

namespace ConsoleStore.AppServices.Cart.Services;

public class CartService
{
    private Dictionary<string, Product> dictForChoosing = null!;

    private void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("КОРЗИНА");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Для редактирования корзины введите номер товара\n" +
            " · Для очистки корзины введите Clear\n" +
            " · Для оплаты корзины введите Pay\n" +
            " · Назад - 0"
        );

        PrintService.PrintSeparator();
    }

    private void PrintHeaderEmptyCart()
    {
        Console.Clear();

        Console.WriteLine("КОРЗИНА");
        PrintService.PrintSeparator();

        Console.WriteLine(
        " · Назад - 0"
        );

        PrintService.PrintSeparator();

        PrintService.PrintColorMessage("\n Корзина пуста!", ConsoleColor.Yellow, false);
        PrintService.PrintColorMessage(" Нажмите любую клавишу для перехода к каталогу...", ConsoleColor.DarkGray, false);
    }

    public void AddProduct(Product product, int count)
    {
        if (!CartRepository.Cart.ContainsKey(product))
        {
            CartRepository.Cart.Add(product, count);
            return;
        }

        CartRepository.Cart[product] += count;
    }

    private void Clear() => CartRepository.Cart.Clear();

    private void Pay()
    {
        foreach (var p in CartRepository.Cart)
        {
            p.Key.Count -= p.Value;
        }

        UserMenu.OrderHistory.AppendOrder(CartRepository.Cart);

        Clear();

        PrintService.PrintColorMessage("\n Спасибо за покупку!", ConsoleColor.Green, false);
        PrintService.PrintColorMessage(" Нажмите любую клавишу для выхода...", ConsoleColor.DarkGray, false);

        Console.ReadKey();
    }

    public void Show()
    {
        while (true)
        {
            dictForChoosing = new();

            if (CartRepository.Cart.Count == 0)
            {
                PrintHeaderEmptyCart();

                if (Console.ReadKey().KeyChar != '0')
                    new AllCategories().Show();

                return;
            }

            PrintHeader();

            var key = 1;
            Product product;
            foreach (var item in CartRepository.Cart)
            {
                product = item.Key;

                dictForChoosing.Add(key.ToString(), product);

                Console.WriteLine(
                    $" {key}. {product.Name} | {product.Price} р | {CartRepository.Cart[product]} шт."
                );

                key++;
            }

            PrintService.PrintSeparator();
            Console.WriteLine();

            var sum = CartRepository.Cart.Select(p => p.Key.Price * p.Value).Sum();
            Console.WriteLine($"К оплате: {sum}");

            Console.WriteLine();

            string? choice;
            while (true)
            {
                Console.Write("Действие: ");

                choice = Console.ReadLine()?
                    .Trim()
                    .ToLower();

                if (choice == "0")
                {
                    return;
                }
                else if (choice == "clear")
                {
                    Clear();
                    break;
                }
                else if (choice == "pay")
                {
                    Pay();
                    return;
                }

                if (string.IsNullOrWhiteSpace(choice) || !dictForChoosing.ContainsKey(choice))
                {
                    PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                    continue;
                }

                new CartEditService(dictForChoosing[choice]).Show();
                break;
            }
        }
    }
}
