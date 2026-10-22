using System.ComponentModel.DataAnnotations;

namespace Prodcuts.Domain.Entities;

public class Product
{
    public int Id;
    public string Name = null!;
    public decimal Price;
}
