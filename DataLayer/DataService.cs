using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace DataLayer;

public class DataService
{

    // CATEGORY
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
            Id = GetCategories().Max(x => x.Id) + 1,
            Name = name,
            Description = description
        };
        db.Categories.Add(category);
        db.SaveChanges();
        return category;
    }

    public bool DeleteCategory(int id)
    {
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            db.Categories.Remove(GetCategory(id));
            db.SaveChanges();

            return result = true;
        }
        catch
        {
            return result;
        }
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            Category category = GetCategory(id);
            category.Name = name;
            category.Description = description;

            db.Categories.Update(category);
            db.SaveChanges();

            return result = true;

        }
        catch
        {
            return result;
        }
    }

    //  PRODUCT

    public Product GetProduct(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList().Find(x => x.Id == id);
    }

    public IList<Product> GetProductByCategory(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList().FindAll(x => x.CategoryId == id); // some of the category ref are null, hence why it can't get the category name some times
    }

    public IList<Product> GetProductByName(string name)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList().FindAll(x => x.Name.Contains(name));
    }

    // ORDER

    public IList<Order> GetOrders()
    {
        NorthwindContext db = new NorthwindContext();
        return db.Orders.ToList();
    }

    public Order GetOrder(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Orders.ToList().Find(x => x.Id == id);
    }
}