using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace e_commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name="Product Name")]
        public string ProdName { get; set; }

        [Display(Name="Image (url)")]
        public string ProductImg { get; set; }

        [Display(Name="Description")]
        public string ProductDesc { get; set; }
        
        [Display(Name="Initial Quantity")]
        public int ProductQty { get; set; }
        public List<Order> Customers { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public Product () {
            Customers = new List<Order>();
            this.Created_at = DateTime.Now;
        }
    }
}