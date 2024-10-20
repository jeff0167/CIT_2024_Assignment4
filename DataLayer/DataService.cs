using Microsoft.EntityFrameworkCore;
namespace DataLayer;

// ideal if the whole class was generic
public class CategoryService : IServiceCRUD<Category> // put each class in it's own file
{
    public Category Create(Category item)
    {
        NorthwindContext db = new NorthwindContext();
        item.Id = GetAll().Max(x => x.Id) + 1;
        db.Categories.Add(item);
        db.SaveChanges();
        return item;
    }

    public IList<Category> GetAll()
    {
        NorthwindContext db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category GetById(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Categories.ToList().Find(x => x.Id == id);
    }

    public bool Update(int id, Category item)
    {
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            Category category = GetById(id);
            category.Name = item.Name;
            category.Description = item.Description; // you have to manualy specify the properties that has to be updated, not great :(

            db.Categories.Update(category);
            db.SaveChanges();

            return result = true;
        }
        catch
        {
            return result;
        }
    }
    public bool Delete(int id)
    {
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            db.Categories.Remove(GetById(id));
            db.SaveChanges();

            return result = true;
        }
        catch
        {
            return result;
        }
    }
}


public class DataService
{
    CategoryService categoryService = new CategoryService();
    // CATEGORY
    public IList<Category> GetCategories()
    {
        return categoryService.GetAll(); // the issue is the name is defined in the test
    }

    public Category GetCategory(int id)
    {
        return categoryService.GetById(id);
    }

    public Category CreateCategory(string name, string description)
    {
        return categoryService.Create(new Category { Name = name, Description = description });
    }

    public bool DeleteCategory(int id)
    {
        return categoryService.Delete(id);
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        return categoryService.Update(id, new Category { Name = name, Description = description });
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
        return db.Products.Include(x => x.Category).ToList().FindAll(x => x.CategoryId == id);
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
        using NorthwindContext db = new NorthwindContext();
        return db.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(x => x.Category).ToList().Find(x => x.Id == id);
    }

    // ORDERDETAILS

    public IList<OrderDetails> GetOrderDetailsByOrderId(int id)
    {
        using NorthwindContext db = new NorthwindContext();
        return db.OrderDetails.Include(x => x.Product).ToList().FindAll(x => x.OrderId == id);
    }

    public IList<OrderDetails> GetOrderDetailsByProductId(int id)
    {
        using NorthwindContext db = new NorthwindContext();
        return db.OrderDetails.Include(x => x.Order).ToList().FindAll(x => x.ProductId == id).OrderByDescending(x => x.OrderId).Reverse().ToList();
    }
}