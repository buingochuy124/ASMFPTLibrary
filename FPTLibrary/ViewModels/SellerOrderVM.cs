using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTLibrary.ViewModels
{
    public class SellerOrderVM
    {
        public DataAccess.DTO.BookDTO Book { get; set; }

        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }

        public double Total { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}