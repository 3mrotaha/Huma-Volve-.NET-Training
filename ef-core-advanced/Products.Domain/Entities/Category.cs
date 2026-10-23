namespace Prodcuts.Domain.Entities;

public class Category
{
    public Guid Id {get; private set;}
    public string Name {get; private set;} = null!;
    public ICollection<Product>? Products {get; private set;} = new HashSet<Product>();

    public Category CreateNewCategory(string name, ICollection<Product>? products = null)
    {
        Name = name;
        Products = products;

        // new category initialization
        Id = Guid.NewGuid();

        return this;
    }
}
