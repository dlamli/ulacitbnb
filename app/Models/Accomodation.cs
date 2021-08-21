using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppUlacitBnB.Models
{
    public class Accomodation
    {

        public int Acc_ID { get; set; }
        public string Acc_Name { get; set; }
        public string Acc_Country { get; set; }
        public string Acc_Zipcode { get; set; }
        public string Acc_State { get; set; }
        public string Acc_Address { get; set; }
        public string Acc_Description { get; set; }
        public string Acc_Evaluation { get; set; }
        public int Hos_ID { get; set; }

    }
}