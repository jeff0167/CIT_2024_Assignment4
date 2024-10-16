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
}
