using ConsoleStore.Common;
using ConsoleStore.Common.Services.PrintService;

namespace ConsoleStore.AppServices.Authorization.Services;

public sealed class AuthorizationMenu : Representable
{
    public static bool isBacked = true;

    string[] actions = { "0", "1" };

    static RegistrationService RegistrationService = new();
    static LoginService LoginService = new();

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine(
            "\n · РЕГИСТРАЦИЯ - 0\n" +
            " · ВОЙТИ - 1"
        );
        PrintService.PrintSeparator();
    }

    public override void Show()
    {
        while (isBacked)
        {
            PrintHeader();

            string? choice;
            while (true)
            {
                Console.Write("Действие: ");

                choice = Console.ReadLine()?
                    .Trim();

                if (string.IsNullOrWhiteSpace(choice) || !actions.Contains(choice))
                {
                    PrintService.PrintColorMessage(" ! Неверный выбор", ConsoleColor.Red);
                    continue;
                }

                switch (choice)
                {
                    case "0":

                        RegistrationService.Show();
                        break;

                    case "1":

                        LoginService.Show();
                        break;
                }

                break;
            }
        }

        return;
    }
}
