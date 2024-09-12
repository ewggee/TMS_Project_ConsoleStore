using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.MainMenu;
using ConsoleStore.Products.Models;
using ConsoleStore.AppServices.Cart.Repositories;

namespace ConsoleStore.Products.Services;

public class ConcreteProduct
{
    Product Product { get; }

    bool cartContainsProduct;

    public ConcreteProduct(Product product)
    {
        Product = product;
    }

    private void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine(Product.Name);
        PrintService.PrintSeparator();

        if (Product.Count > 0)
        {
            Console.WriteLine(" · Для добавления в корзину введите нужное количество товара");
        }

        Console.WriteLine(" · Назад — 0");

        PrintService.PrintSeparator();
    }
    public void Show()
    {
        cartContainsProduct = CartRepository.Cart.ContainsKey(Product);

        PrintHeader();

        Console.WriteLine(
            $"Описание: {Product.Description}\n" +
            $"Цена: {Product.Price} р\n" +
            $"В наличии: {Product.Count} шт."
        );

        Console.WriteLine();

        if (cartContainsProduct)
        {
            Console.WriteLine($"В корзине: {CartRepository.Cart[Product]} шт.");
            Console.WriteLine();
        }

        string? choice;
        while (true)
        {
            Console.Write("Действие: ");

            choice = Console.ReadLine()?.Trim();

            if (choice == "0")
                return;

            bool successParse = int.TryParse(choice, out var number);

            if (string.IsNullOrWhiteSpace(choice) || !successParse || Product.Count == 0)
            {
                PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                continue;
            }

            if (number > Product.Count)
            {
                PrintService.PrintColorMessage(" Данного количества нет на складе", ConsoleColor.Yellow);
                continue;
            }

            if (cartContainsProduct && number + CartRepository.Cart[Product] > Product.Count)
            {
                PrintService.PrintColorMessage(" Данного количества нет на складе", ConsoleColor.Yellow);
                continue;
            }

            UserMenu.Cart.AddProduct(Product, number);

            PrintService.PrintColorMessage(" Добавлено!", ConsoleColor.Green);

            break;
        }

        PrintService.PrintSeparator();
        Console.WriteLine(
            " · Для перехода к корзине нажмите любую клавишу\n" +
            " · Назад - 0"
        );

        Console.WriteLine();
        Console.Write("Действие: ");

        if (Console.ReadKey().KeyChar == '0')
            return;

        UserMenu.Cart.Show();
    }
}
