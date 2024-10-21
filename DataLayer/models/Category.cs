using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace DataLayer;
public class Category : Item
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Implementing the GetId and SetId methods from the abstract class Item
    public override int GetId()
    {
        return Id;
    }
    public override void SetId(int id)
    {
        Id = id;
    }
}