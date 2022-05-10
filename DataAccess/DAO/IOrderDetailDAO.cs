using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IOrderDetailDAO
    {
        int OrderDetail_Create(long BookISBN, int Quantity,int OderID);

        List<DataAccess.DTO.OrderDetailDTO> OrderDetail_GetOrderDetail(int OrderID);

        List<DataAccess.DTO.OrderDetailDTO> OrderDetail_GetList();
    }
}
