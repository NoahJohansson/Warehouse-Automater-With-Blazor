using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_OOP2_Grupp_24.Data
{
    public class Product
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
        private string _description;
        [Required]
        [StringLength(60, ErrorMessage = " {0} length must be between {2} and {1} characters. ", MinimumLength = 3)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private double _price;
        [Required]
        [Range(0, 999999)]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private int _stock;
        [Required]
        [Range(0, 999999)]
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        private DateTime _restockingDate;
        public DateTime RestockingDate
        {
            get { return _restockingDate; }
            set { _restockingDate = value; }
        }
    }
}
