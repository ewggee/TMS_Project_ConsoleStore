using ConsoleStore.Products.Models;

namespace ConsoleStore.DataAccess;

public class CartDB
{
    public static Dictionary<Product, int> Cart = null!;
}
