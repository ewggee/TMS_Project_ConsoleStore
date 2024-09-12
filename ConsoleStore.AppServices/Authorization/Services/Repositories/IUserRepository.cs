using ConsoleStore.AppServices.Authorization.Models;

namespace ConsoleStore.AppServices.Authorization.Services.Repositories;

public interface IUserRepository
{
    UserInfo GetUser(string login);
    void SaveUser(string login, string password);
}
