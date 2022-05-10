using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IOrderDAO
    {
        List<OrderDTO> Orders_GetListByUser(int UserID);

        int Order_Create(int UserID, double Total, DateTime Date);

        OrderDTO Order_GetOrderID(int UserID,DateTime Date);

        OrderDTO Order_GetOrderByID(int OrderID);

    }
}
