using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class Customer
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        [Required]
        [StringLength(30, ErrorMessage = " {0} length must be between {2} and {1} characters. ", MinimumLength = 3)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _phone;
        [Required]
        [StringLength(10, ErrorMessage = " {0} number length must be {2} digits. ", MinimumLength = 10)]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _email;
        [Required]
        [StringLength(40, ErrorMessage = " {0} length must be between {2} and {1} characters. ", MinimumLength = 5)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private List<Order> _orders;
        public List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

    }
}
