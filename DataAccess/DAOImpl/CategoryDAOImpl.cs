using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOImpl
{
    public class CategoryDAOImpl : ICategoryDAO
    {
        public List<CategoryDTO> Categories_GetList()
        {
            throw new NotImplementedException();
        }

        public int Category_Create(string CategoryName)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_CategoryName", CategoryName);


                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;


                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoryDTO Category_GetDetailByID(int CategoryID)
        {
            var result = new CategoryDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                SqlCommand cmd = new SqlCommand("SP_CategoryGetDetailByID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = (new CategoryDTO
                    {
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        CategoryName = read["CategoryName"].ToString()
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public CategoryDTO Category_GetDetailByName(string CategoryName)
        {
            var result = new CategoryDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                SqlCommand cmd = new SqlCommand("SP_CategoryGetDetailByName", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_CategoryName", CategoryName);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = (new CategoryDTO
                    {
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        CategoryName = read["CategoryName"].ToString()
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
