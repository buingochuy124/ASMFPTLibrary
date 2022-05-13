using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface ICartDAO
    {
        int Cart_AddBookToCart(long BookISBN, int UserID);

        List<CartDTO> Carts_GetCartByUser(int UserID);

        int Cart_CheckOut(int UserID);

        int Cart_Update(long BookISBN, int Quantity);

        int Cart_Delete(long BookISBN);
    }
}
