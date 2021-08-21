using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppUlacitBnB.Models
{
    public class Room
    {
        public int Roo_ID { get; set; }
        public decimal Roo_Price { get; set; }
        public decimal Roo_Quantity { get; set; }
        public string Roo_Type { get; set; }
        public string Roo_Evaluation { get; set; }
        public decimal Roo_BedQuantity { get; set; }
        public int Ser_ID { get; set; }
        public int Acc_ID { get; set; }
    }
}