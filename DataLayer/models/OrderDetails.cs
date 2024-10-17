using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer;
public class OrderDetails
{
    [Required][ForeignKey("Order")]
    public int OrderId { get; set; }
    [Required][ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    public Product Product { get; set; }

    public Order Order { get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
}
