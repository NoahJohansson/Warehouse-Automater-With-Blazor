using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    interface ICustomerRepo
    {
        public event ChangeHandler CustomerChanged;
        Task<List<Customer>> GetCustomersAsync();
        void AddCustomersAsync(Customer customer);
        void RemoveCustomersAsync(Customer customer);
        void UpdateCustomersAsync(Customer customer);
        Task<List<Order>> GetArchivedOrders(int customerId);
        Task<List<Order>> GetActiveOrders(int customerId);
        Task<Customer> GetCustomerById(int id);
    }
}
