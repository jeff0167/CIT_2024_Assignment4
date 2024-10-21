using System.ComponentModel.DataAnnotations;

namespace DataLayer;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
    public string CategoryName => Category.Name;
    public string ProductName => Name;

    [Required]
    public ICollection<OrderDetails> OrderDetails { get; set; }
}

