using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class CustomerRepo : ICustomerRepo
    {

        private readonly Context _context;

        public event ChangeHandler CustomerChanged;

        public CustomerRepo(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches all customers from database
        /// </summary>
        /// <returns> List of customers </returns>
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Adds a customer to database and saves all changes made to database
        /// </summary>
        /// <param name="customer"> The customer to be inserted </param>
        public async void AddCustomersAsync(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                CustomerChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes a customer from database and saves all changes made to database
        /// </summary>
        /// <param name="customer"> The customer that is to be removed </param>
        public async void RemoveCustomersAsync(Customer customer)
        {
            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                CustomerChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a customer in database and saves changes made to database
        /// </summary>
        /// <param name="customer"> The customer that is to be updated </param>
        public async void UpdateCustomersAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            CustomerChanged?.Invoke();
        }

        /// <summary>
        /// Fetches a specific customer in database by its unique ID
        /// </summary>
        /// <param name="id"> The unique ID to identify customer </param>
        /// <returns> The customer with input ID </returns>
        public async Task<Customer> GetCustomerById(int id)
        {
            IEnumerable<Customer> FoundCustomer =  _context.Customers.Where(c =>
            c.Id == id);
            CustomerChanged?.Invoke();
            return FoundCustomer.First();
        }


        /// <summary>
        /// Gets all orders from database where order is dispatched and CustomerId matches 
        /// input customerId
        /// </summary>
        /// <param name="customerId"> The unique ID to identify customer </param>
        /// <returns>
        /// Returns result as a list</returns>
        public async Task<List<Order>> GetArchivedOrders(int customerId)
        {
            return await _context.Orders.Where(o =>
            o.Dispatched == true && o.CustomerId == customerId).ToListAsync();
        }

        /// <summary>
        /// Gets all orders from database where order is not dispatched and CustomerId matches 
        /// input customerId
        /// </summary>
        /// <param name="customerId"> The unique ID to identify customer </param>
        /// <returns>
        /// Returns found orders as a list
        /// </returns>
        public async Task<List<Order>> GetActiveOrders(int customerId)
        {
            return await _context.Orders.Where(o =>
            o.Dispatched == false && o.CustomerId == customerId).ToListAsync();
        }
    }
}
