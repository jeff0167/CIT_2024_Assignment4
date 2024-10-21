using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace DataLayer;

public class Product : Item
{

    public int Id { get; set; }
    public string Name { get; set; }
    public int UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string CategoryName => Category.Name;
    public string ProductName => Name;


    public ICollection<OrderDetails> OrderDetails { get; set; }
    public override int GetId()
    {
        return Id;
    }
    public override void SetId(int id)
    {
        Id = id;
    }
}

