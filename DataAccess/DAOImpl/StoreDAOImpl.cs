using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class StoreDAOImpl : IStoreDAO
    {
        public List<StoreDTO> Stores_GetList()
        {
            var result = new List<StoreDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListStore", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new StoreDTO
                    {
                        StoreName = read["StoreName"].ToString(),
                        UserID = int.Parse(read["UserID"].ToString()),
                        StoreID = int.Parse(read["StoreID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Store_Create(int UserID, string StoreName)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateStore", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_StoreName", StoreName);
                cmd.Parameters.AddWithValue("@_UserID", UserID);



                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<BookDTO> Store_GetBookOfStoreByPage(int? PageNumber, int? NumberPerPage, int StoreID)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetBookPaginationForStore", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);
                cmd.Parameters.AddWithValue("@_StoreID", StoreID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new BookDTO
                    {
                        BookISBN = long.Parse(read["BookISBN"].ToString()),
                        BookName = read["BookName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        Pages = int.Parse(read["Pages"].ToString()),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        BookImageURL = read["BookURL"].ToString(),
                        Author = read["Author"].ToString(),
                        BookDescription = read["BookDescription"].ToString(),
                        StoreID = int.Parse(read["StoreID"].ToString()),
                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public StoreDTO Store_GetStoreByUser(int UserID)
        {
            var result = new StoreDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetStoreByUser", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserID", UserID);

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = new StoreDTO
                    {
                        StoreID = int.Parse(read["StoreID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        StoreName = read["StoreName"].ToString(),
                    };
                }
                return result;

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
