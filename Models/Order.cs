using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace e_commerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set;}
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int OrderQty { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime Updated_at { get; set; }
        public Order () {
            this.Updated_at = DateTime.Now;
            this.OrderDate = DateTime.Now;
        }
    }
}