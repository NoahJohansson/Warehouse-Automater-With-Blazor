using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public delegate void ChangeHandler();

    public class ProductRepo : IProductRepo
    {
        private readonly Context _context;

        public event ChangeHandler ProductChanged;


        public ProductRepo(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches all products in database
        /// </summary>
        /// <returns> A list of all products </returns>
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Adds a product to database and saves changes made to database
        /// </summary>
        /// <param name="product"> The product that is to be added </param>
        public async void AddProductsAsync(Product product)
        {
            try
            {
                product.RestockingDate = DateTime.Now.AddDays(10);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                ProductChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove a product from database and saves changes made to database
        /// </summary>
        /// <param name="product"> The product that is to be removed </param>
        public async void RemoveProductsAsync(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                ProductChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a product from database and saves changes made to database
        /// </summary>
        /// <param name="product"> The product that is to be updated </param>
        public async void UpdateProductsAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            ProductChanged?.Invoke();
        }

        /// <summary>
        /// Fetches all products in database that have no stock
        /// </summary>
        /// <returns> A list of products </returns>
        public async Task<List<Product>> GetProductsWithNoStock()
        {
            var products = await _context.Products.Where(p =>
            p.Stock == 0).ToListAsync();

            return products;
        }

        /// <summary>
        /// Iterates over orderLines in order and checks if restockingDate is greater than closestTime
        /// If so, sets closestTime to that restockingDate
        /// </summary>
        /// <param name="order"> The order where closest restocking date is selected </param>
        /// <returns> The closest restocking date of an order </returns>
        public DateTime GetClosestRestockingDate(Order order)
        {
            DateTime closestTime = DateTime.Today;
            IEnumerable<Order> resulty = _context.Orders.Where(o =>
            o.Id == order.Id);

            foreach (OrderLine orderLine in _context.OrderLines.Where (ol => ol.OrderId == order.Id))
            {
                IEnumerable<Product> products = _context.Products.Where(p =>
                p.Id == orderLine.ProductId);
                if(products.FirstOrDefault().RestockingDate > closestTime)
                {
                    closestTime = products.FirstOrDefault().RestockingDate;
                }
            }

            return closestTime;
        }

        /// <summary>
        /// Adds ten days from now to restocking day of a product and saves changes made to database
        /// </summary>
        /// <param name="product"> The product that is to be updated </param>
        public async void SetRestockingDateToTenDays(Product product)
        {
            product.RestockingDate = DateTime.Now.AddDays(10);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            ProductChanged?.Invoke();
        }
    }
}
