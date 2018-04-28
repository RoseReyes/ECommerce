using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using e_commerce.Models;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private EShopContext _context;
 
        public HomeController(EShopContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Order> ordersummary = _context.Orders.Include(c => c.Customer).ThenInclude( p => p.Products).ToList();
            ViewBag.all_orders = ordersummary;
            
            List<Order> transaction = _context.Orders.ToList();
            List<Product> all_products = _context.Products.ToList();
            
            ViewBag.all_products = all_products;
            ViewBag.all_transactions = transaction;
            return View();
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult Products()
        {
            List<Order> transaction = _context.Orders.ToList();
            List<Product> all_products = _context.Products.ToList();

            ViewBag.all_products = all_products;
            ViewBag.all_transactions = transaction;
            return View("Products");
        }

        [HttpGet]
        [Route("Customer")]
        public IActionResult Customer()
        {
            List<Customer> ReturnValues = _context.Customers.ToList();
            ViewBag.all_users = ReturnValues;
            return View("Customer");
        }

        [HttpGet]
        [Route("Order")]
        public IActionResult Order()
        {
            //query to populate the dropdown for Customers and Products
            List<Customer> ReturnValues = _context.Customers.ToList();
            ViewBag.all_users = ReturnValues;
            List<Product> all_products = _context.Products.ToList();
            ViewBag.all_products = all_products;

            //query to populate the table
            List<Order> transaction = _context.Orders.Include(c => c.Customer).ThenInclude( p => p.Products).ToList();
            ViewBag.all_orders = transaction;
            return View("Order");
        }

        [HttpPost]
        [Route("createcust")]
        public IActionResult createcust(Customer newcustomer)
        {          
            List<Customer> ReturnValues = _context.Customers.Where(user => user.CustomerName.Equals(newcustomer.CustomerName)).ToList();
        
            foreach (var item in ReturnValues)
            {
               if(item.CustomerName == newcustomer.CustomerName)
                 {
                    TempData["error"] ="Customer already exists";
                    return RedirectToAction("Customer");  
                 } 
            }
             _context.Customers.Add(newcustomer);
            _context.SaveChanges();
            return RedirectToAction("Customer");
        }

        [HttpPost]
        [Route("deletecust")]
        public IActionResult deletecust(int custid)
        {
            Customer RetrievedUser = _context.Customers.SingleOrDefault(user => user.CustomerId == custid);
            _context.Customers.Remove(RetrievedUser);
            _context.SaveChanges(); 
            return RedirectToAction("Customer");
        }

        [HttpPost]
        [Route("createprod")]
        public IActionResult createprod(Product newproduct)
        {
            List<Product> ReturnValues = _context.Products.Where(p => p.ProdName.Equals(newproduct.ProdName)).ToList();
        
            foreach (var products in ReturnValues)
            {
               if(products.ProdName == newproduct.ProdName)
                 {
                    TempData["updatemsg"] ="Product already exists - no update feature yet";
                    return RedirectToAction("Products");  
                 } 
            }
            _context.Add(newproduct);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }

        [HttpPost]
        [Route("createorder")]
        public IActionResult createorder(string CustName, int OrderQty, string ProdName)
        {
            //get the customer id, product id from the post request
            Customer get_customer = _context.Customers.SingleOrDefault(c => c.CustomerName == CustName);
            Product get_product = _context.Products.SingleOrDefault(p => p.ProdName == ProdName);
            int OrderCustID = get_customer.CustomerId;
            int OrderProdID = get_product.ProductId;

            Order neworder = new Order {
                ProductId = OrderProdID,
                CustomerId = OrderCustID,
                OrderQty = OrderQty
            };
            _context.Add(neworder);
            _context.SaveChanges();
            return RedirectToAction("Order");
        }

        [HttpPost]
        [Route("SearchDashboard")]
        public IActionResult SearchDashboard() {

            return View("Index");
        }
        
        [HttpPost]
        [Route("SearchProduct")]
        public ViewResult SearchProduct(string SearchStr) {
            var products = from p in _context.Products
                   select p;

            if (!String.IsNullOrEmpty(SearchStr))
            {
                products = products.Where(s => s.ProdName.Contains(SearchStr)
                                    || s.ProductDesc.Contains(SearchStr));
            }
            ViewBag.SearchStr = products.ToList();
            return View("Products");
        }
    }
}