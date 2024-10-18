﻿using Microsoft.EntityFrameworkCore;
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
        return db.Categories.ToList().Find(x => x.Id == id);
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
        NorthwindContext db = new NorthwindContext();
        return db.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(x => x.Category).ToList().Find(x => x.Id == id);
    }

    public IList<OrderDetails> GetOrderDetailsByOrderId(int id)
    {
        NorthwindContext db = new NorthwindContext();
        return db.OrderDetails.Include(x => x.Product).ToList().FindAll(x => x.OrderId == id);
    }

    //public IList<OrderDetails> GetOrderDetailsByProductId(int id)
    //{
    //    NorthwindContext db = new NorthwindContext();
    //    return db.OrderDetails.Include(x => x.Order).Include(x => x.Product).Where(x => x.ProductId == id).ToList();


    //}
    public IList<OrderDetails> GetOrderDetailsByProductId(int id)
    {
        NorthwindContext db = new NorthwindContext();



        return db.OrderDetails.Include(x => x.Order).ToList().FindAll(x => x.ProductId == id);
    }
}