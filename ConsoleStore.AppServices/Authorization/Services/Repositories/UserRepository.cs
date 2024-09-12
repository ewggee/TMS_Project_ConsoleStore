using ConsoleStore.AppServices.Authorization.Models;
using ConsoleStore.Common.Services.PrintService;
using System.Text.Json;

namespace ConsoleStore.AppServices.Authorization.Services.Repositories;

public class UserRepository : IUserRepository
{
    const string UsersDB = "C:\\Users\\Evgeniy\\source\\repos\\TMS_Project_ConsoleStore\\ConsoleStore.Data\\Users\\";

    public UserInfo GetUser(string login)
    {
        try
        {
            using FileStream fs = new FileStream(UsersDB + $"{login}\\" + "UserInfo.json", FileMode.Open);

            var obj = JsonSerializer.Deserialize<UserInfo>(fs);

            return obj!;
        }
        catch (FileNotFoundException)
        {
            PrintService.PrintColorMessage(" ! Файл не найден", ConsoleColor.Red);
            throw new FileNotFoundException("Файл не найден");
        }
    }

    public void SaveUser(string login, string password)
    {
        Directory.CreateDirectory(UsersDB + $"{login}\\");
        using FileStream fs = File.Create(UsersDB + $"{login}\\" + "UserInfo.json");
        //using FileStream fs = new FileStream(UsersDB + $"{login}\\" + "UserInfo.json", FileMode.Create);

        JsonSerializer.Serialize(fs, new UserInfo(login, password));
    }
}
