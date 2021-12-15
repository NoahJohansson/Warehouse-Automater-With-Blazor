using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class Order
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _customerId;
        [Required(ErrorMessage = "Choosing a customer is required.")]
        [Range(1, 99999999, ErrorMessage = "Choosing a customer is required.")]
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        private DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        private string _deliveryAdress;
        [Required(ErrorMessage = "The Delivery Adress field is required.")]
        [StringLength(60, ErrorMessage = " Delivery Adress must be between {2} and {1} characters. ", MinimumLength = 4)]
        public string DeliveryAdress
        {
            get { return _deliveryAdress; }
            set { _deliveryAdress = value; }
        }

        private bool _paymentCompleted;
        public bool PaymentCompleted
        {
            get { return _paymentCompleted; }
            set { _paymentCompleted = value; }
        }

        private bool _dispatched;
        public bool Dispatched
        {
            get { return _dispatched; }
            set { _dispatched = value; }
        }

        private List<OrderLine> _items;
        public List<OrderLine> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
