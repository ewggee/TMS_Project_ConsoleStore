using ConsoleStore.Categories.Models;
using ConsoleStore.Common;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.Products.Models;
using ConsoleStore.Products.Services;

namespace ConsoleStore.Categories.Services;

class ConcreteCategory : Representable
{
    private Category Category { get; }

    public ConcreteCategory(Category category)
    {
        Category = category;
    }

    public Dictionary<string, Product> dictForChoosing = null!;

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine(Category.Name.ToUpper());
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Для перехода к товару введите его номер\n" +
            " · Назад — 0"
        );
        PrintService.PrintSeparator();
    }

    public override void Show()
    {
        while (true)
        {
            dictForChoosing = new Dictionary<string, Product>();

            PrintHeader();

            if (Category.Products.Count == 0)
                return;

            var key = 1;
            foreach (var product in Category.Products)
            {
                dictForChoosing.Add(key.ToString(), product);
                Console.WriteLine($" {key}. {product.Name}");
                key++;
            }

            Console.WriteLine();

            string? choice;
            while (true)
            {
                Console.Write("Товар: ");

                choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                    return;

                if (string.IsNullOrWhiteSpace(choice) || !dictForChoosing.ContainsKey(choice))
                {
                    PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                    continue;
                }

                new ConcreteProduct(dictForChoosing[choice]).Show();
                break;
            }
        }
    }
}
