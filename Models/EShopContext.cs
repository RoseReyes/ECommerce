using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
 
namespace e_commerce.Models
{
    public class EShopContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public EShopContext(DbContextOptions<EShopContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
