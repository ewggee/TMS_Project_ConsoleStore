using ConsoleStore.AppServices.Authorization.Services.Repositories;
using ConsoleStore.Common;
using ConsoleStore.Common.Services.PrintService;

namespace ConsoleStore.AppServices.Authorization.Services;

public sealed class RegistrationService : Representable
{
    string? Login = string.Empty;
    string? Password = string.Empty;

    protected override void PrintHeader()
    {
        Console.Clear();

        Console.WriteLine("РЕГИСТРАЦИЯ");
        PrintService.PrintSeparator();

        Console.WriteLine(
            " · Отмена (назад) - 0"
        );
        PrintService.PrintSeparator();
    }

    public override void Show()
    {
        PrintHeader();

        // Добавить проверку на уникальность логина
        Login = EnterLogin();
        if (Login is null)
            return;

        Password = EnterPassword();
        if (Password is null)
            return;

        AuthorizationMenu.isBacked = false;

        UserRepository UserRepository = new();
        UserRepository.SaveUser(Login, Password);
    }

    private string? EnterLogin()
    {
        string? login;
        while (true)
        {
            PrintHeader();

            Console.Write("Логин: ");

            login = Console.ReadLine()?
                .Trim();



            if (login == "0")
                return null;

            if (string.IsNullOrWhiteSpace(login) || 
                login.Length < 8 || login.Length > 20)
            {
                PrintService.PrintColorMessage(" ! Недопустимый логин", ConsoleColor.Red);
                PrintService.PrintColorMessage(" Нажмите любую клавишу для повторного заполнения", ConsoleColor.DarkGray, false);

                Console.ReadKey();
                continue;
            }

            break;
        }

        return login;
    }

    private string? EnterPassword()
    {
        string? password;
        string? confirmPassword;

        while (true)
        {
            PrintHeader();

            #region Ввод пароля
            Console.Write(
                $"Логин: {Login}\n" +
                "Пароль: "
            );

            password = Console.ReadLine()?
                .Trim();

            if (password == "0")
                return null;

            if (string.IsNullOrWhiteSpace(password) ||
                password.Length < 8 || password.Length > 20)
            {
                PrintService.PrintColorMessage(" ! Недопустимый пароль", ConsoleColor.Red);
                PrintService.PrintColorMessage(" Нажмите любую клавишу для повторного заполнения...", ConsoleColor.DarkGray, false);

                Console.ReadKey();
                continue;
            }
            #endregion

            #region Подтверждение пароля
            Console.Write($"Подтвердите пароль: ");

            confirmPassword = Console.ReadLine()?
                .Trim();

            if (confirmPassword == "0")
                return null;

            if (string.IsNullOrWhiteSpace(confirmPassword) ||
                confirmPassword.Length < 8 || confirmPassword.Length > 20 ||
                !password.Equals(confirmPassword))
            {
                PrintService.PrintColorMessage(" ! Пароли не совпадают", ConsoleColor.Red);
                PrintService.PrintColorMessage(" Нажмите любую клавишу для повторного заполнения...", ConsoleColor.DarkGray, false);

                Console.ReadKey();
                continue;
            }
            #endregion

            return password;
        }
    }
}
