using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class OrderLineRepo : IOrderLineRepo
    {
        public event ChangeHandler OrderLineChanged;
        private readonly Context _context;
        
        public OrderLineRepo(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches all orderLines from database
        /// </summary>
        /// <returns> List of all orderLines </returns>
        public async Task<List<OrderLine>> GetAllOrderLines()
        {
            IEnumerable<OrderLine> result = _context.OrderLines;
            return result.ToList();
        }

        /// <summary>
        /// Fetches orderLines of a specific order from database
        /// </summary>
        /// <param name="orderid"> Unique order identifier </param>
        /// <returns> A list of orderLines from a specific order </returns>
        public async Task<List<OrderLine>> GetOrderLinesByOrderId(int orderid)
        {
            IEnumerable<OrderLine> result = _context.OrderLines.Where(ol =>
            ol.OrderId == orderid);
            return result.ToList();
        }

        /// <summary>
        /// Adds an orderLine to database and saves all changes made to database
        /// </summary>
        /// <param name="orderLine"> The orderLine that is to be added </param>
        public async void AddOrderLinesAsync(OrderLine orderLine)
        {
            try
            {
                orderLine.OrderId = GetOrderIdFromLastOrder();
                _context.OrderLines.Add(orderLine);
                await _context.SaveChangesAsync();
                OrderLineChanged?.Invoke();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fetches latest orderId from Orders in database to get Foreign Key value
        /// </summary>
        /// <returns> The orderID of latest order inserted into database </returns>
        public int GetOrderIdFromLastOrder()
        {
            IEnumerable<Order> result = _context.Orders.OrderBy(o => o.OrderDate);
            return Convert.ToInt32(result.LastOrDefault().Id);
        }
    }
}
