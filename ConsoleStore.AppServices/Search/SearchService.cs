using ConsoleStore.Common;
using ConsoleStore.Products.Models;
using System.Text.RegularExpressions;
using ConsoleStore.Products.Services;
using static ConsoleStore.Common.Models.TestProducts;
using ConsoleStore.Common.Services.PrintService;

namespace ConsoleStore.Search;

public class SearchService : Representable
{
    // Посимвольный ввод с консоли в список
    List<char> searchBar = null!;

    Dictionary<string, Product> foundingProducts = new Dictionary<string, Product>();

    readonly char[] validKeys = new[] {
        // Русский алфавит (строчные)
        'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
        // Русский алфавит (заглавные)
        'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
        // Английский алфавит (строчные)
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        // Английский алфавит (заглавные)
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        // Пробел
        ' '
    };
    readonly char[] validChoice = new[] {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    // Позиция курсора для ввода названия товара
    (int Left, int Top) InputPosition;

    // Позиция курсора для вывода результатов поиска
    (int Left, int Top) RecPosition;

    // Позиция для ввода номера из найденных товаров
    (int Left, int Top) ChoicePosition;

    private void SetInputPos() => Console.SetCursorPosition(InputPosition.Left, InputPosition.Top);
    private void SetRecPos() => Console.SetCursorPosition(RecPosition.Left, RecPosition.Top);
    private void SetChoicePos() => Console.SetCursorPosition(ChoicePosition.Left, ChoicePosition.Top);

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("ПОИСК");
        PrintService.PrintSeparator();

        Console.WriteLine(" · Назад — 0");

        PrintService.PrintSeparator();

        Console.Write(" - Введите название товара: ");
    }

    public override void Show()
    {
        searchBar = new();

        PrintHeader();

        // Начальная позиция курсора для ввода названия товара
        InputPosition = Console.GetCursorPosition();

        // Граница поля ввода названия товара
        var LeftBorder = InputPosition.Left;

        // Позиция курсора для вывода рекомендаций
        RecPosition = (0, InputPosition.Top + 2);

        ConsoleKeyInfo keyinfo;
        char key;
        while (true)
        {
            keyinfo = Console.ReadKey(true);

            key = keyinfo.KeyChar;

            if (key == '0' && searchBar.Count == 0)
            {
                Console.Write(key);

                while (true)
                {
                    keyinfo = Console.ReadKey(true);

                    key = keyinfo.KeyChar;

                    if (key == (char)ConsoleKey.Enter)
                        return;

                    if (keyinfo.Key == ConsoleKey.Backspace)
                    {
                        Console.SetCursorPosition(InputPosition.Left, InputPosition.Top);
                        Console.Write(" ");
                        Console.SetCursorPosition(InputPosition.Left, InputPosition.Top);

                        break;
                    }

                    continue;
                }

            }

            if (InputPosition.Left == LeftBorder & keyinfo.Key == ConsoleKey.Spacebar)
                continue;

            if (key == (char)ConsoleKey.Enter && searchBar.Count > 0)
                break;

            if (keyinfo.Key == ConsoleKey.Backspace)
            {
                // Если нечего стереть - вернуть курсор на исходную позицию для ввода
                if (InputPosition.Left <= LeftBorder)
                {
                    Console.SetCursorPosition(LeftBorder, InputPosition.Top);
                    continue;
                }

                Console.SetCursorPosition(--InputPosition.Left, InputPosition.Top);
                Console.Write(" ");
                searchBar.RemoveAt(searchBar.Count - 1); // Удаление последнего символа из списка (поисковой строки)

                PrintRecs();

                continue;
            }

            if (!validKeys.Contains(key))
                continue;

            searchBar.Add(key);

            Console.Write(key);
            InputPosition.Left++;

            PrintRecs();
        }

        if (foundingProducts.Count == 0)
        {
            SetRecPos();

            PrintService.PrintColorMessage($" Товары по запросу \"{new string(searchBar.ToArray())}\" не найдены. ", ConsoleColor.Yellow, false);
            PrintService.PrintColorMessage("Нажмите любую клавишу для выхода...", ConsoleColor.DarkGray, false);

            Console.ReadKey();

            return;
        }

        SetChoicePos();

        string? choice;
        while (true)
        {
            Console.Write("Номер товара: ");

            choice = Console.ReadLine()?.Trim();

            if (choice == "0")
                return;

            if (string.IsNullOrWhiteSpace(choice) || !foundingProducts.ContainsKey(choice))
            {
                PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                continue;
            }

            new ConcreteProduct(foundingProducts[choice]).Show();
            break;
        }
    }

    private void PrintRecs()
    {
        // Очистка и установка курсора на поле рекомендаций
        SetRecPos();
        Console.Write(new string(' ', Console.WindowWidth * 20));
        SetRecPos();

        var recs = FindRecs();
        if (recs == null)
        {
            SetInputPos();
            return;
        }

        var i = 1;
        foreach (var r in recs)
        {
            foundingProducts.Add(i.ToString(), r.Value);
            Console.WriteLine($" {i++}. {r.Value.Name}");
        }

        ChoicePosition.Top = Console.GetCursorPosition().Top + 1;

        // Возврат курсора на исходное положение
        Console.SetCursorPosition(InputPosition.Item1, InputPosition.Item2);
    }

    private Dictionary<string, Product>? FindRecs()
    {
        var request = new string(searchBar.ToArray());

        if (string.IsNullOrEmpty(request))
            return null;

        var regex = new Regex($"^{request}", RegexOptions.IgnoreCase);

        var recs = new Dictionary<string, Product>();
        var i = 1;
        foreach (var category in testCategories)
        {
            foreach (var p in category.Products)
            {
                if (regex.IsMatch(p.Name))
                {
                    recs.Add(i.ToString(), p);
                    i++;
                }
            }
        }

        foundingProducts.Clear();

        return recs;
    }
}
