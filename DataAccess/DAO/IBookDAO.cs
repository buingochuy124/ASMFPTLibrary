using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IBookDAO
    {
        List<BookDTO> Books_GetList();

        int Book_SellBook(long BookISBN, string BookName, string Author, double Cost, int Pages, int CategoryID, string Description, string BookImageURL, int StoreID);

        BookDTO Book_GetDetail(long BookISBN);

        List<BookDTO> Books_GetListByPage(int? PageNumber, int? NumberPerPage);

        int Book_Delete(long BookISBN);

        List<BookDTO> Books_Search(string SearchKeyWord);

        int Book_Update(long BookISBN, string BookName, double Cost, string BookURL, int Pages, string Author, string BookDescription);

    }
}
