using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class OrderLine
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _productId;
        [Required(ErrorMessage = "Choosing a product is required.")]
        [Range(1, 99999999, ErrorMessage = "Choosing a product is required.")]
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private Order _order;
        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }

        private int _quantity;
        [Required]
        [Range(1, 999999)]
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

    }
}
