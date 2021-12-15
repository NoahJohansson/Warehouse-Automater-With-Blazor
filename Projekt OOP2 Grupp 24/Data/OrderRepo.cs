using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class OrderRepo : IOrderRepo
    {

        private readonly Context _context;

        public event ChangeHandler OrderChanged;

        public OrderRepo(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches all orders from database
        /// </summary>
        /// <returns> List of all orders </returns>
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        /// <summary>
        /// Adds an order to database and saves all changes made to database
        /// </summary>
        /// <param name="order"> The order that is to be added</param>
        public async void AddOrdersAsync(Order order)
        {
            try
            {
                order.OrderDate = DateTime.Now;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                OrderChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an order and saves all changes made to database
        /// </summary>
        /// <param name="order"> The order that is to be added </param>
        public async void UpdateOrdersAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            OrderChanged?.Invoke();
        }

        /// <summary>
        /// Fetches all orders that are dispatched from database
        /// </summary>
        /// <returns> List of all dispatched orders </returns>
        public async Task<List<Order>> GetDispatchedOrders()
        {
            IEnumerable<Order> result = _context.Orders.Where(o =>
            o.Dispatched == true);

            return result.ToList();
        }

        /// <summary>
        /// Fetches all orders that are not dispatched from database
        /// </summary>
        /// <returns> List of all pending orders </returns>
        public async Task<List<Order>> GetPendingOrders()
        {
            IEnumerable<Order> result = _context.Orders.Where(o =>
            o.Dispatched == false);

            return result.ToList();
        }
        
        //Gets stock amount of a specific product in database
        //Not sure this will even be needed

        /// <summary>
        /// Fetches stock amount of a product in database
        /// </summary>
        /// <param name="productId"> Unique identifier to retrieve stock from correct product </param>
        /// <returns> The stock amount of a product </returns>
        public async Task<int> GetStockAmount(int productId)
        {
            IEnumerable<int> stockAmount = _context.Products.Where(p =>
            p.Stock > 0 && p.Id == productId).Select(p =>
            p.Stock);

            return Convert.ToInt32(stockAmount);
        }

        //Not really working first attempt at process order
        public async void ProcessOrderss()
        {
            IEnumerable<Order> sortedOrders = _context.Orders.OrderBy(o =>
            o.OrderDate).Where(o => o.Dispatched == false);
            foreach (Order order in sortedOrders.ToList())
            {
                IEnumerable<OrderLine> result = _context.OrderLines.Where(o =>
                o.OrderId == order.Id);
                foreach (OrderLine orderline in result.ToList())
                {
                    IEnumerable<Product> products = _context.Products.Where(p =>
                    p.Id == orderline.ProductId).ToList();
                    if (products.First().Stock >= orderline.Quantity)
                    {
                        products.First().Stock -= orderline.Quantity;
                    }
                }
                List<Product> productss = _context.Products.ToList();
                if (productss.All(p => p.Stock >= 0))
                {
                    order.Dispatched = true;
                    await _context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Processes orders that are not marked as dispatched already in database.
        /// Iterates through all orders and checks if they have completed payment
        /// If so, iterate through all orderlines of that order and lowers stock of product in database by orderLine quantity
        /// If stock falls below zero, sets enoughStock to false.
        /// If enoughStock is true, sets order to dispatched and save changes in database. Else revert changes made to stock of products. 
        /// </summary>
        public async void ProcessOrders()
        {
            IEnumerable<Order> sortedOrders = _context.Orders.OrderBy(o =>
            o.OrderDate).Where(o => o.Dispatched == false);
            foreach (Order order in sortedOrders.ToList())
            {
                if (order.PaymentCompleted == true)
                {
                    bool enoughStock = true;
                    foreach (OrderLine orderLine in _context.OrderLines.Where(o => o.OrderId == order.Id))
                    {
                        IEnumerable<Product> products = _context.Products.Where(p =>
                        p.Id == orderLine.ProductId);

                        products.FirstOrDefault().Stock -= orderLine.Quantity;

                        if (products.FirstOrDefault().Stock < 0)
                        {
                            enoughStock = false;
                            order.Dispatched = false;
                            products.FirstOrDefault().RestockingDate = DateTime.Today.AddDays(10);
                        }
                    }

                    if (enoughStock)
                    {
                        foreach (OrderLine orderLine in _context.OrderLines.Where(o => o.OrderId == order.Id))
                        {
                            IEnumerable<Product> products = _context.Products.Where(p =>
                            p.Id == orderLine.ProductId);

                            _context.Update(products.FirstOrDefault());
                            order.Dispatched = true;
                            await _context.SaveChangesAsync();
                            OrderChanged?.Invoke();
                        }
                    }

                    else
                    {
                        foreach (OrderLine orderLine in _context.OrderLines.Where(o => o.OrderId == order.Id))
                        {
                            IEnumerable<Product> products = _context.Products.Where(p =>
                            p.Id == orderLine.ProductId);
                            products.FirstOrDefault().Stock += orderLine.Quantity;
                        }
                    }
                }
                
            }
        }
    }
}
