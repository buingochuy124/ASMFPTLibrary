namespace DataAccess.DTO
{
    public class BookDTO
    {
        public long BookISBN { get; set; }
        public string BookName { get; set; }
        public double Cost { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string BookImageURL { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public string BookDescription { get; set; }
        public int StoreID { get; set; }
        public int Quantity { get; set; }
    }
}
