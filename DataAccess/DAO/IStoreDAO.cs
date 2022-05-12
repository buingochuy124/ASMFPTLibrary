using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IStoreDAO
    {
        StoreDTO Store_GetStoreByUser(int UserID);

        List<BookDTO> Store_GetBookOfStoreByPage(int? PageNumber, int? NumberPerPage, int StoreID);

        int Store_Create(int UserID,string StoreName);

        List<StoreDTO> Stores_GetList();
    }
}
