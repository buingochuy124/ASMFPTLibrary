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
    public class OrderDetaiDAOlImpl : IOrderDetailDAO
    {
        public int OrderDetail_Create(long BookISBN, int Quantity, int OrderID)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_OrderDetailCreate", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);
                cmd.Parameters.AddWithValue("@_Quantity", Quantity);
                cmd.Parameters.AddWithValue("@_OrderID", OrderID);


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


        public List<OrderDetailDTO> OrderDetail_GetList()
        {
            var result = new List<DataAccess.DTO.OrderDetailDTO>();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListOrderDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;




                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new DataAccess.DTO.OrderDetailDTO
                    {

                        BookISBN = long.Parse(read["BookISBN"].ToString()),
                        OrderID = int.Parse(read["OrderID"].ToString()),
                        Quantity = int.Parse(read["Quantity"].ToString()),

                    });
                }


                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<DataAccess.DTO.OrderDetailDTO> OrderDetail_GetOrderDetail(int OrderID)
        {
            var result = new  List<DataAccess.DTO.OrderDetailDTO>();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetOrderDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_OrderID", OrderID);



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add( new DataAccess.DTO.OrderDetailDTO
                    {

                        BookISBN = long.Parse(read["BookISBN"].ToString()),
                        OrderID = int.Parse(read["OrderID"].ToString()),
                        Quantity = int.Parse(read["Quantity"].ToString()),

                    });
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
