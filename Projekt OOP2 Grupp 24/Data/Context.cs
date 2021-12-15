using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class Context : DbContext
    {
        private DbSet<Customer> _customers;

        public DbSet<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        private DbSet<Product> _products;

        public DbSet<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        private DbSet<Order> _orders;

        public DbSet<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        private DbSet<OrderLine> _orderlines;

        public DbSet<OrderLine> OrderLines
        {
            get { return _orderlines; }
            set { _orderlines = value; }
        }

        /// <summary>
        /// Creates a database if it does not exist
        /// </summary>
        /// <param name="options"></param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Creates dummydata in database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer);
            //modelBuilder.Entity<Order>().HasMany(o => o.Items).WithOne(ol => ol.Order);
            //modelBuilder.Entity<OrderLine>().HasOne(ol => ol.Product);

            modelBuilder.Entity<Customer>().HasData(GetCustomerSeededData());
            modelBuilder.Entity<Product>().HasData(GetProductSeededData());
            modelBuilder.Entity<Order>().HasData(GetOrdersSeededData());
            modelBuilder.Entity<OrderLine>().HasData(GetOrderLinesSeededData());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Creates new List of orders with dummy data to be inserted into database
        /// </summary>
        /// <returns>A List with dummy data of orders</returns>
        private List<Order> GetOrdersSeededData()
        {
            List<Order> orders = new List<Order>() { new Order()
            { Id = 1, CustomerId = GetCustomerSeededData()[0].Id, DeliveryAdress = "TestAdress 996",
                Dispatched = true, OrderDate = DateTime.Now, PaymentCompleted = true}, new Order()
            { Id = 2, CustomerId = GetCustomerSeededData()[1].Id, DeliveryAdress = "TestAdress 997",
                Dispatched = true, OrderDate = DateTime.Now, PaymentCompleted = true}, new Order()
            { Id = 3, CustomerId = GetCustomerSeededData()[2].Id, DeliveryAdress = "TestAdress 998",
                Dispatched = false, OrderDate = DateTime.Now, PaymentCompleted = false}, new Order()
            { Id = 4, CustomerId = GetCustomerSeededData()[3].Id, DeliveryAdress = "TestAdress 995",
                Dispatched = false, OrderDate = DateTime.Now, PaymentCompleted = false}, new Order()
            { Id = 5, CustomerId = GetCustomerSeededData()[3].Id, DeliveryAdress = "TestAdress 995",
                Dispatched = false, OrderDate = DateTime.Now, PaymentCompleted = false} };
            return orders;
        }

        /// <summary>
        /// Creates a new List of products with dummy data to be inserted into database
        /// </summary>
        /// <returns>A List with dummy data of products</returns>
        private List<Product> GetProductSeededData()
        {
            List<Product> products = new List<Product>() { new Product()
            { Id = 1, Name = "TestProdukt", Description = "En produktbeskrivning", 
                Price = 100, RestockingDate = DateTime.Now.AddDays(5), Stock = 300 }, new Product()
            { Id = 2, Name = "ProduktTvå", Description = "En produktbeskrivning",
                Price = 300, RestockingDate = DateTime.Now.AddDays(6), Stock = 0 }, new Product()
            { Id = 3, Name = "ProduktTre", Description = "En produktbeskrivning",
                Price = 500, RestockingDate = DateTime.Now.AddDays(7), Stock = 50 },  };
            return products;

        }

        /// <summary>
        /// Creates a new List of customers with dummy data to be inserted into database
        /// </summary>
        /// <returns>A List with dummy data of customers</returns>
        private List<Customer> GetCustomerSeededData()
        {
            List<Customer> customers = new List<Customer>() { new Customer()
            { Id = 1, Name = "Test Testsson", Email = "Test@Test.test", Phone = "9999999999"}, new Customer()
            { Id = 2, Name = "David", Email = "Test@Test.test", Phone = "9999999998"}, new Customer()
            { Id = 3, Name = "Noah", Email = "Test@Test.test", Phone = "9999999997"}, new Customer()
            { Id = 4, Name = "Jonis", Email = "Test@Test.test", Phone = "9999999996"}, new Customer()
            { Id = 5, Name = "Myran", Email = "Test@Test.test", Phone = "9999999995"} };
            return customers;
        }

        /// <summary>
        /// Creates a new List of orderLines with dummy data to be inserted into database
        /// </summary>
        /// <returns>A List with dummy data of orderLines</returns>
        private List<OrderLine> GetOrderLinesSeededData()
        {
            List<OrderLine> orderlines = new List<OrderLine>() { new OrderLine()
            { Id = 1, Quantity = 99, ProductId = GetProductSeededData()[0].Id,
                OrderId = GetOrdersSeededData()[0].Id}, new OrderLine()
            { Id = 7, Quantity = 9, ProductId = GetProductSeededData()[0].Id,
                OrderId = GetOrdersSeededData()[0].Id},  new OrderLine()
            { Id = 2, Quantity = 7, ProductId = GetProductSeededData()[1].Id,
                OrderId = GetOrdersSeededData()[1].Id}, new OrderLine()
            { Id = 3, Quantity = 3, ProductId = GetProductSeededData()[2].Id,
                OrderId = GetOrdersSeededData()[2].Id}, new OrderLine()
            { Id = 4, Quantity = 50, ProductId = GetProductSeededData()[2].Id,
                OrderId = GetOrdersSeededData()[3].Id}, new OrderLine()
            { Id = 5, Quantity = 30, ProductId = GetProductSeededData()[0].Id,
                OrderId = GetOrdersSeededData()[4].Id}, new OrderLine()
            { Id = 6, Quantity = 500, ProductId = GetProductSeededData()[2].Id,
                OrderId = GetOrdersSeededData()[4].Id} };
            return orderlines;
        }
    }
}
