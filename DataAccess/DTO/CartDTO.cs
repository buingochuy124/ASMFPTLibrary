using System.Collections.Generic;

namespace DataAccess.DTO
{
    public class CartDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public long BookISBN { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }

    }
}
