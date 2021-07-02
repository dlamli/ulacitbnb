namespace ulacit_bnb.Models
{
    public partial class Review
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Rate { get; set; }
        public string Recommendation { get; set; }
        public string Comment { get; set; }
        public string Usefull { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public int AccomodationID { get; set; }
    
        public virtual Accomodation Accomodation { get; set; }
        public virtual User User { get; set; }
    }
}
