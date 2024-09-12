namespace ConsoleStore.Products.Models;

public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }

    public Product(string name, string description, int price, int count)
    {
        Name = name;
        Description = description;
        Price = price;
        Count = count;
    }
}
