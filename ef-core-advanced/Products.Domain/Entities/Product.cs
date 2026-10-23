using System.ComponentModel.DataAnnotations;
using Prodcuts.Domain.Interfaces;

namespace Prodcuts.Domain.Entities;

public class Product : ISoftDeletable
{
    public Guid Id {get; private set; }
    public Guid? CategoryId {get; private set;}
    public string Name  {get; private set;} = null!;
    public decimal Price {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public DateTime? DeletedAt {get; private set;}
    public bool IsDeleted {get; private set;} 
    public Category? Category { get; set; }
    public byte[] RowVersion {get; set; } = [];
    public Product CreateNewProduct(string name, decimal price, Category? category = null)
    {
        Name = name;
        Price = price;
        CategoryId = category?.Id;

        // new product initialization
        Id = Guid.NewGuid();

        IsDeleted = false;
        DeletedAt = null;

        CreatedAt = DateTime.Now;


        return this;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }


    public void RevertDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }

    public void SetName(string name) => Name = name;

    public void SetPrice(decimal price) => Price = price;
}
