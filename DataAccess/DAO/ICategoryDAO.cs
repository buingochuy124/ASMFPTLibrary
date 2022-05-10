using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface ICategoryDAO
    {
        List<CategoryDTO> Categories_GetList();

        CategoryDTO Category_GetDetailByID(int CategoryID);

        CategoryDTO Category_GetDetailByName(string CategoryName);

        int Category_Create(string CategoryName);
    }
}
