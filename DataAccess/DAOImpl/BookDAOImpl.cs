using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class BookDAOImpl : IBookDAO
    {
        public List<BookDTO> Books_GetList()
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


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

        public List<BookDTO> Books_GetListByPage(int? PageNumber, int? NumberPerPage)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetBookPagination", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);


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

        public int Book_SellBook(long BookISBN, string BookName, string Author, double Cost, int Pages, int CategoryID, string Description, string BookImageURL, int StoreID)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);
                cmd.Parameters.AddWithValue("@_BookName", BookName);
                cmd.Parameters.AddWithValue("@_Author", Author);
                cmd.Parameters.AddWithValue("@_Cost", Cost);
                cmd.Parameters.AddWithValue("@_Pages", Pages);
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@_BookDescription", Description);
                cmd.Parameters.AddWithValue("@_BookImageURL", BookImageURL);
                cmd.Parameters.AddWithValue("@_StoreID", StoreID);


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

        public int Book_Delete(long BookISBN)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_BookDelete", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);


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

        public BookDTO Book_GetDetail(long BookISBN)
        {
            var result = new BookDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetBookDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = new BookDTO
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


                    };
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookDTO> Books_Search(string SearchKeyWord)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_SearchListBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_SearchKeyWord", SearchKeyWord);


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
        public List<BookDTO> Books_SearchAndGetListByPage(int? PageNumber, int? NumberPerPage, string Keyword)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_SearchBookAndGetListByPage", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);
                cmd.Parameters.AddWithValue("@_Keyword", Keyword);


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
        public List<BookDTO> Book_Search(string Keyword)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_SearchBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_Keyword", Keyword);


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

        public int Book_Update(long BookISBN, string BookName, double Cost, string BookURL, int Pages, string Author, string BookDescription)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_BookUpdate", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);
                cmd.Parameters.AddWithValue("@_BookName", BookName);
                cmd.Parameters.AddWithValue("@_Cost", Cost);
                cmd.Parameters.AddWithValue("@_BookURL", BookURL);
                cmd.Parameters.AddWithValue("@_Pages", Pages);
                cmd.Parameters.AddWithValue("@_Author", Author);
                cmd.Parameters.AddWithValue("@_BookDescription", BookDescription);


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
    }
}
