using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppUlacitBnB.Models
{
    public class Reservation
    {
        public int Res_ID { get; set; }
        public DateTime Res_StartDate { get; set; }
        public DateTime Res_ReservationDate { get; set; }
        public DateTime Res_EndDate { get; set; }
        public string Res_Status { get; set; }
        public decimal Res_Quantity { get; set; }
        public string Res_ResolutionDate { get; set; }
        public int Res_PaymentID { get; set; }
        public int Cus_ID { get; set; }
        public int Roo_ID { get; set; }
    }
}