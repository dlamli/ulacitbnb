//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ulacitbnb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int Res_ID { get; set; }
        public System.DateTime Res_StartDate { get; set; }
        public System.DateTime Res_ReservationDate { get; set; }
        public System.DateTime Res_EndDate { get; set; }
        public string Res_Status { get; set; }
        public decimal Res_Quantity { get; set; }
        public string Res_ResolutionDate { get; set; }
        public int Res_PaymentID { get; set; }
        public int Cus_ID { get; set; }
        public int Roo_ID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Room Room { get; set; }
    }
}
