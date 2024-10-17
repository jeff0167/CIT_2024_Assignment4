using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;
internal class NorthwindContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categories
        MapCategories(modelBuilder);

        // Products
        MapProducts(modelBuilder);

        // Orders
        MapOrders(modelBuilder);

        // OrderDetails
        MapOrdersDetails(modelBuilder);
    }

    private static void MapProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().HasKey(x => x.Id);
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");

        //modelBuilder.Entity<Product>().HasOne(x => x.Category).WithOne().HasForeignKey<Category>(a => a.Id); // this one doesn't make if fail, but makes the ref sometimes be null, no idea why
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        //modelBuilder.Entity<Product>().Property(x => x.CategoryName).HasColumnName("categoryname"); // if this is uncommented it will fail
    }

    private static void MapCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().HasKey(x => x.Id);
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");
    }
    private static void MapOrders(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().HasKey(x => x.Id);
        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");


        //modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithMany(x => x.OrderId ==)
        // modelBuilder.Entity<Order>().is(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x);
        //modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithMany().HasForeignKey(x => x.OrderId); 
        //modelBuilder .Entity<Order>().HasMany(x => x.OrderDetails).WithMany().HasForeignKey<Order>(a => a.OrderId);
        //modelBuilder.Entity<Order>().HasMany(p => p.OrderDetails).WithOne(c => c.Order).HasForeignKey(c => c.Order);
        // modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithMany(x => x.OrderId).HasForeignKey<Category>(a => a.Id);

        //modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne().HasForeignKey(x => x.OrderId);

       // modelBuilder .Entity<Order>() .HasOne(x => x.OrderDetails);

       // modelBuilder .Entity<Order>() .HasMany(x => x.OrderDetails) .WithOne(x => x.Order) .IsRequired();

        //modelBuilder .Entity<Order>() .HasOne(x => x.OrderDetails) .WithOne().IsRequired();

        //modelBuilder .Entity<Order>() .HasOne(x => x.OrderDetails) .WithMany() .HasForeignKey(x => x.Id); // none of them work               Comment them out to get rid of errors

    }
    private static void MapOrdersDetails(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.OrderId, x.ProductId });
        // modelBuilder.Entity<OrderDetails>().HasMany(p => p.).WithOne(c => c.Order).HasForeignKey(c => c.Order);
    }
}