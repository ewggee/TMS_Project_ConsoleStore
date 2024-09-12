namespace ConsoleStore.AppServices.Authorization.Models;

public class UserInfo
{
    public string Login { get; }
    public string Password { get; }

    public UserInfo(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
