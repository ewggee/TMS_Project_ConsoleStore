using ConsoleStore.AppServices.Authorization.Services;
using ConsoleStore.AppServices.Cart.Repositories;
using ConsoleStore.AppServices.Cart.Services;
using ConsoleStore.AppServices.OrderHistory.Repositories;
using ConsoleStore.AppServices.OrderHistory.Services;
using ConsoleStore.Common.Services.PrintService;
using ConsoleStore.MainMenu;

namespace ConsoleStore;

class Program
{
    readonly static CartSerialization CartSerialization = new();
    readonly static OrderHistorySerialization OrderHistorySerialization = new();

    readonly static AuthorizationMenu Authorization = new();
    
    public static bool isAdmin;

    static void Main()
    {
        // Доделать
        Authorization.Show();

        CartRepository.Cart = new();
        OrderHistoryRepository.Purchases = new();
        // Десериализация

        //AppDomain.CurrentDomain.ProcessExit += new(ClosingApp);
        isAdmin = false;
        UserMenu.Show();
    }

    static void ClosingApp(object? sender, EventArgs e)
    {
        PrintService.PrintColorMessage("\nВыход...", ConsoleColor.DarkGray, false);
        //CartSerialization.Serialize(CartRepository, );
        //OrderHistorySerialization.Serialize(OrderHistoryRepository, );
    }
}