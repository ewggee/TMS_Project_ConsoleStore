using ConsoleStore.Categories.Models;
using ConsoleStore.Common;
using ConsoleStore.Common.Services.PrintService;
using static ConsoleStore.Common.Models.TestProducts;

namespace ConsoleStore.Categories.Services;

public class AllCategories : Representable
{
    private Dictionary<string, Category> dictForChoosing = null!;

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("КАТАЛОГ");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Для перехода к категории введите её номер\n" +
            " · Назад — 0"
        );
        PrintService.PrintSeparator();
    }

    public override void Show()
    {
        while (true)
        {
            // Создание нового словаря
            dictForChoosing = new Dictionary<string, Category>();

            PrintHeader();

            var key = 1;
            // Получение категорий и создание новой коллекции категорий с ключом string 
            for (int i = 0; i < testCategories.Count; i++)
            {
                dictForChoosing.Add(key.ToString(), testCategories[i]);
                Console.WriteLine($" {key}. {testCategories[i].Name}");
                key++;
            }

            Console.WriteLine();

            string? choice;
            while (true)
            {
                Console.Write("Категория: ");

                choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                    return;

                if (string.IsNullOrWhiteSpace(choice) || !dictForChoosing.ContainsKey(choice))
                {
                    PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                    continue;
                }

                //if (isAdmin)
                //{
                //    new EditCategory(dictForChoosing[choice]);
                //    return;
                //}

                new ConcreteCategory(dictForChoosing[choice]).Show();
                break;
            }
        }
    }
}
