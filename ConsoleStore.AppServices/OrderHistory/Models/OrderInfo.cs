using ConsoleStore.Products.Models;

namespace ConsoleStore.OrderHistory.Models;

public class OrderInfo
{
    public Dictionary<Product, int> Products { get; }
    public DateTime DateTime { get; }
    public int Sum { get; }

    public OrderInfo(Dictionary<Product, int> products)
    {
        Products = products;
        DateTime = DateTime.Now;
        Sum = Products.Select(p => p.Key.Price * p.Value).Sum();
    }

    public void ProductsInfo()
    {
        foreach (var item in Products)
        {
            Console.WriteLine(
                $" - {item.Key.Name} | {item.Key.Price} р | {item.Value} шт."
            );
        }
    }
}
