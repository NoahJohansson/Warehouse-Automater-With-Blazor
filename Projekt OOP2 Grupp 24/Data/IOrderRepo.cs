using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    interface IOrderRepo
    {
        public event ChangeHandler OrderChanged;
        Task<List<Order>> GetOrdersAsync();
        void AddOrdersAsync(Order order);
        void UpdateOrdersAsync(Order order);
        Task<List<Order>> GetDispatchedOrders();
        Task<List<Order>> GetPendingOrders();
        Task<int> GetStockAmount(int productId);
        void ProcessOrders();
    }
}
