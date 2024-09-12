using ConsoleStore.Common.Services.PrintService;

namespace ConsoleStore.Authorization.Services;

public class AuthorizationService_demo
{
    static Dictionary<string, string> Users { get; } = new();
    static Dictionary<string, string> Admins { get; } = new();

    static (int Left, int Top) LoginPosition;
    static (int Left, int Top) PasswordPosition;

    readonly char[] validKeys = new[] {
        // Английский алфавит (строчные)
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        // Английский алфавит (заглавные)
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        // Цифры
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    private static void RegisterHeader()
    {
        Console.WriteLine("РЕГИСТРАЦИЯ");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Отмена (назад) - 0"
        );
        PrintService.PrintSeparator();

        Console.Write(" Логин: ");
        LoginPosition = Console.GetCursorPosition();

        Console.Write(" Пароль: ");
        PasswordPosition = Console.GetCursorPosition();
    }

    private static void RegisterUser()
    {
        RegisterHeader();


    }

    private static string GetLogin()
    {

        var login = new List<char>();

        char key;
        while (true)
        {
            key = Console.ReadKey(true).KeyChar;

            if (key == (char)ConsoleKey.Enter)
                break;

            if (char.IsLetterOrDigit(key))
            {
                Console.Write(key);
                login.Add(key);
                continue;
            }
        }

        if (login.Count < 8)
        {

        }

        return new string(login.ToArray());
    }

    private static void Login()
    {

    }

    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                " 1) Регистрация\n" +
                " 2) Вход\n"
            );

            string? choice;
            while (true)
            {
                Console.Write("Действие: ");

                choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;

                    case "2":
                        Login();
                        break;

                    default:
                        PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                        continue;
                }

                break;
            }
        }
    }
}
