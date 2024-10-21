using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace DataLayer;
public class Order : Item
{

    public int Id { get; set; }
    public DateTime Date { get; set; } = new DateTime();
    public DateTime Required { get; set; } = new DateTime();
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public string ShipName { get; set; }
    public string ShipCity { get; set; }

    public override int GetId()
    {
        return Id;
    }

    public override void SetId(int id)
    {
        Id = id;
    }
}
