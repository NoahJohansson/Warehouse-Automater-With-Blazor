using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    interface IProductRepo
    {
        public event ChangeHandler ProductChanged;
        Task<List<Product>> GetProductsAsync();
        void AddProductsAsync(Product product);
        void RemoveProductsAsync(Product product);
        void UpdateProductsAsync(Product product);
        Task<List<Product>> GetProductsWithNoStock();
        DateTime GetClosestRestockingDate(Order order);
        void SetRestockingDateToTenDays(Product product);
    }
}
