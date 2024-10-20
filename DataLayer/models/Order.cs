using System.ComponentModel.DataAnnotations;

namespace DataLayer;
public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; } = new DateTime();
    public DateTime Required { get; set; } = new DateTime();

    [Required]
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public string ShipName { get; set; }
    public string ShipCity { get; set; }
}
