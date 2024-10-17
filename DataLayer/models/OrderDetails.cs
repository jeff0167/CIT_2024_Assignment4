using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}



