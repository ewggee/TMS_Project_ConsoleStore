using ConsoleStore.Categories.Services;
using ConsoleStore.OrderHistory.Services;
using ConsoleStore.Search;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.AppServices.Cart.Services;

namespace ConsoleStore.MainMenu;

public class UserMenu
{
    /// <summary>
    /// Поле для отображения текста для скидок на главном экране
    /// </summary>
    static string discountDesc = string.Empty;

    public static SearchService Search = new();
    public static AllCategories AllCategories = new();
    public static CartService Cart { get; } = new();
    public static OrderHistoryService OrderHistory = new();

    private static void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("ГЛАВНОЕ МЕНЮ");
        PrintService.PrintSeparator();

        if (!string.IsNullOrEmpty(discountDesc))
        {
            Console.WriteLine(discountDesc);
            PrintService.PrintSeparator();
        }

        Console.WriteLine("Для выбора действия введите его номер");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " 1. Поиск\n" +
            " 2. Каталог\n" +
            " 3. Корзина\n" +
            " 4. История заказов\n"
        );
    }

    private static string SelectAction()
    {
        var actions = new[] { "1", "2", "3", "4" };

        string? choice;
        while (true)
        {
            Console.Write("Действие: ");

            choice = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(choice) || !actions.Contains(choice))
            {
                PrintService.PrintColorMessage(" ! Неверное действие", ConsoleColor.Red);
                continue;
            }

            return choice;
        }
    }

    public static void Show()
    {
        while (true)
        {
            PrintHeader();

            var choice = SelectAction();

            switch (choice)
            {
                case "1":

                    Search.Show();
                    break;

                case "2":

                    AllCategories.Show();
                    break;

                case "3":

                    Cart.Show();
                    break;

                case "4":

                    OrderHistory.Show();
                    break;
            }
        }
    }
}
