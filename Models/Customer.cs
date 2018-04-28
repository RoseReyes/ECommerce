using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace e_commerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Display(Name="Name")]
        public string CustomerName { get; set; }
        public List<Order> Products { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
       

        public Customer () {
            Products = new List<Order>();
            this.Created_at = DateTime.Now;
            this.Updated_at = DateTime.Now;
        }
    }
}