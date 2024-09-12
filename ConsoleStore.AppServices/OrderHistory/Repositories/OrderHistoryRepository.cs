using ConsoleStore.OrderHistory.Models;

namespace ConsoleStore.AppServices.OrderHistory.Repositories;

public class OrderHistoryRepository
{
    public static Stack<OrderInfo> Purchases = new();
}