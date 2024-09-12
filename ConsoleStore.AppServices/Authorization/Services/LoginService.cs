using ConsoleStore.Common;
using ConsoleStore.Common.Services.PrintService;

namespace ConsoleStore.AppServices.Authorization.Services;

enum Statuses
{
    Back = 0,
    Success = 1
}

public sealed class LoginService : Representable
{
    /// <summary>
    /// Для вывода введённого логина на экран при вводе пароля.
    /// </summary>
    string Login = string.Empty;

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("ВХОД");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Отмена (назад) - 0"
        );
        PrintService.PrintSeparator();
    }

    public override void Show()
    {
        PrintHeader();

        // Добавить проверку на существование логина
        if (CheckLogin() == Statuses.Back)
            return;

        // Добавить проверку на существование пароля
        if (CheckPassword() == Statuses.Back)
            return;

        AuthorizationMenu.isBacked = false;

        PrintService.PrintColorMessage("\n Успешно!", ConsoleColor.Green, false);
        PrintService.PrintColorMessage(" Для продолжения нажмите любую клавишу...", ConsoleColor.DarkGray, false);
        Console.ReadKey();
    }

    private Statuses CheckLogin()
    {
        string? login;
        while (true)
        {
            PrintHeader();

            Console.Write(" Логин: ");
            
            login = Console.ReadLine()?
                .Trim();

            if (login == "0")
                return Statuses.Back;

            if (string.IsNullOrWhiteSpace(login))
            {
                PrintService.PrintColorMessage(" ! Неверный логин", ConsoleColor.Red);
                PrintService.PrintColorMessage(" Нажмите любую клавишу для повторного заполнения", ConsoleColor.DarkGray, false);

                Console.ReadKey();
                continue;
            }

            Login = login;
            return Statuses.Success;
        }
    }

    private Statuses CheckPassword()
    {
        string? password;
        while (true)
        {
            PrintHeader();

            Console.Write(
                $" Логин: {Login}\n" +
                " Пароль: "
            );

            password = Console.ReadLine()?
                .Trim();

            if (password == "0")
                return Statuses.Back;

            if (string.IsNullOrWhiteSpace(password))
            {
                PrintService.PrintColorMessage(" ! Недопустимый пароль", ConsoleColor.Red);
                PrintService.PrintColorMessage(" Нажмите любую клавишу для повторного заполнения...", ConsoleColor.DarkGray, false);

                Console.ReadKey();
                continue;
            }

            return Statuses.Success;
        }
    }
}
