namespace ulacit_bnb.Models
{
    public partial class Review
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Rate { get; set; }
        public bool Recommendation { get; set; }
        public string Comment { get; set; }
        public int Usefull { get; set; }
        public string Title { get; set; }
        public int CustomerID { get; set; }
        public int AccomodationID { get; set; }
    }
}
