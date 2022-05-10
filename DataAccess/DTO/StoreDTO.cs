using System.Collections.Generic;

namespace DataAccess.DTO
{
    public class StoreDTO
    {
        public int StoreID { get; set; }
        public int UserID { get; set; }
        public string StoreName { get; set; }

        public List<BookDTO> Books { get; set; }
    }
}
