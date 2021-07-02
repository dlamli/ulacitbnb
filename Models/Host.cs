namespace ulacit_bnb.Models
{
    using System.Collections.Generic;
    
    public partial class Host
    {
        public Host()
        {
            this.Accomodation = new HashSet<Accomodation>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Accomodation> Accomodation { get; set; }
    }
}
