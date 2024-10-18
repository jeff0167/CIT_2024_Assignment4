using Microsoft.EntityFrameworkCore;
namespace DataLayer;

interface IServiceGetById<T>
{
    T GetById(int id);
}
interface IServiceGetAll<T>
{
    IList<T> GetAll();
}

interface IServiceGetByName<T>
{
    IList<T> GetByName(string name);
}

interface IServiceCreate<T>
{
    T Create(T item);
}

interface IServiceDelete<T>
{
    bool Delete(int id);
}
interface IServiceUpdate<T>
{
    bool Update(int id, T item);
}

interface IServiceCRUD<T> : IServiceGetById<T>, IServiceGetAll<T>, IServiceCreate<T>, IServiceUpdate<T>, IServiceDelete<T> // put in folder for interfaces or something
{

}

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
        //NorthwindContext db = new NorthwindContext();
        //return db.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        return categoryService.GetById(id);
        //NorthwindContext db = new NorthwindContext();
        //return db.Categories.ToList().Find(x => x.Id == id);
    }

    public Category CreateCategory(string name, string description)
    {
        return categoryService.Create(new Category { Name = name, Description = description });
        //NorthwindContext db = new NorthwindContext();
        //Category category = new Category
        //{
        //    Id = GetCategories().Max(x => x.Id) + 1,
        //    Name = name,
        //    Description = description
        //};
        //db.Categories.Add(category);
        //db.SaveChanges();
        //return category;
    }

    public bool DeleteCategory(int id)
    {
        return categoryService.Delete(id);
        //bool result = false;
        //try
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    db.Categories.Remove(GetCategory(id));
        //    db.SaveChanges();

        //    return result = true;
        //}
        //catch
        //{
        //    return result;
        //}
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        return categoryService.Update(id, new Category { Name = name, Description = description });
        //bool result = false;
        //try
        //{
        //    NorthwindContext db = new NorthwindContext();
        //    Category category = GetCategory(id);
        //    category.Name = name;
        //    category.Description = description;

        //    db.Categories.Update(category);
        //    db.SaveChanges();

        //    return result = true;

        //}
        //catch
        //{
        //    return result;
        //}
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