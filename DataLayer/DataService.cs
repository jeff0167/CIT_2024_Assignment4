using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace DataLayer;

public class DataService
{
    public IList<Category> GetCategories()
    {
        NorthwindContext db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        NorthwindContext db = new NorthwindContext();
        Category category = db.Categories.ToList().Find(x => x.Id == id);
        return category;
    }

    public Category CreateCategory(string name, string description)
    {
        NorthwindContext db = new NorthwindContext();
        Category category = new Category
        {
            Id = GetCategories().Max(x => x.Id) +1,
            Name = name,
            Description = description
        }; 
        db.Categories.Add(category);
        db.SaveChanges();
        return category; 
    }

    public bool DeleteCategory(int id) { 
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            db.Categories.Remove(GetCategory(id));
            db.SaveChanges();
            result = true;
            return result;
        }
        catch (Exception ex)
        {
            result = false;

            return result;
        }
    }
}
