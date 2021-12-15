using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Projekt_OOP2_Grupp_24.Data
{
    interface IOrderLineRepo
    {
        Task<List<OrderLine>> GetAllOrderLines();

        Task<List<OrderLine>> GetOrderLinesByOrderId(int orderid);
        void AddOrderLinesAsync(OrderLine orderLine);
        public event ChangeHandler OrderLineChanged;

    }
}
