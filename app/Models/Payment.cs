using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppUlacitBnB.Models
{
    public class Payment
    {
        public int Pay_ID { get; set; }
        public string Pay_Brand { get; set; }
        public string Pay_Type { get; set; }
        public string Pay_Modality { get; set; }
        public DateTime Pay_Date { get; set; }
        public int Pay_Amount { get; set; }
        public decimal Pay_Taxes { get; set; }
        public decimal Pay_Total { get; set; }
    }
}