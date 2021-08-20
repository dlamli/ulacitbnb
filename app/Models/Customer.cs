using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppUlacitBnB.Models

{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
    }
}