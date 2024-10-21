using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace DataLayer;
public class Category : Item
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public override int GetId()
    {
        return Id;
    }

    public override void SetId(int id)
    {
        Id = id;
    }
}