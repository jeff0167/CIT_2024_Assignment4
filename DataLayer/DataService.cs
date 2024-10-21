using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace DataLayer;

public class Service<T> : IServiceCRUD<T> where T : Item //Implements interface IServiceCRUD and determines type through abstract class Item
{
    public T Create (T item)
    {
        NorthwindContext db = new NorthwindContext();
        item.SetId(GetAll().Max(x => x.GetId()) + 1);
        db.Add(item);
        db.SaveChanges();
        return item;
    }

    public IList<T> GetAll()
    {
        NorthwindContext db = new NorthwindContext();
        return db.Set<T>().ToList();
    }

    public T GetById(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.Set<T>().ToList().Find(x => x.GetId() == id);
    }

    public bool Update(int id, T item) //Method to update any object type (Categories, Order, Product, ...)
    {
        bool result = false;
        try
        {
            NorthwindContext db = new NorthwindContext();
            T genericObject = GetById(id); //Creating generic object and inserting the parameter ID
            item.SetId(genericObject.GetId()); //Transfering ID to generic object type parameter 

            genericObject = (T)item.Clone(); //Cloning a new instance of generic object type parameter and transferring updated values into it
            db.Set<T>().Update(genericObject); //Updating the filled out generic object type
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
            db.Set<T>().Remove(GetById(id));
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
    
    Service<Category> categoryService = new Service<Category>();
    Service<Product> productService = new Service<Product>();
    Service<Order> orderService = new Service<Order>();
    //No Services required for OrderDetail as all of OrderDetail methods contains Include()
    //which we were unable to implement into our Service class


    // CATEGORY
    public IList<Category> GetCategories()
    {
        return categoryService.GetAll(); 
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
        return productService.GetById(id); 
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
        return orderService.GetAll();
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