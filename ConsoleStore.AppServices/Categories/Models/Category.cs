using ConsoleStore.Products.Models;

namespace ConsoleStore.Categories.Models;

class Category
{
    public List<Product> Products { get; } = new();
    public string Name { get; set; }
    public Category(string name) => Name = name;
}
