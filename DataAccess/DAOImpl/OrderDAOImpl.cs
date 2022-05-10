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
    public class OrderDAOImpl : IOrderDAO
    {
        public List<OrderDTO> Orders_GetListByUser(int UserID)
        {
            var result = new List<DataAccess.DTO.OrderDTO>();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetOrderListByUser", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserID", UserID);



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new OrderDTO
                    {

                        OrderID = int.Parse(read["OrderID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        Total = double.Parse(read["Total"].ToString()),
                        Date = DateTime.Parse(read["Date"].ToString()),

                    });
                }

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int Order_Create(int UserID, double Total, DateTime Date)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_OrderCreate", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_Total", Total);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.Parameters.AddWithValue("@_Date", Date);



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

        public OrderDTO Order_GetOrderByID(int OrderID)
        {
            var result = new DataAccess.DTO.OrderDTO();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetOrderByID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_OrderID", OrderID);



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = (new OrderDTO
                    {

                        OrderID = int.Parse(read["OrderID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        Total = double.Parse(read["Total"].ToString()),
                        Date = DateTime.Parse(read["Date"].ToString()),

                    });
                }

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public OrderDTO Order_GetOrderID(int UserID, DateTime Date)
        {
            var result = new DataAccess.DTO.OrderDTO();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetOrderID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserID", UserID);
                cmd.Parameters.AddWithValue("@_Date", Date);



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = (new OrderDTO
                    {

                        OrderID = int.Parse(read["OrderID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        Total = double.Parse(read["Total"].ToString()),
                        Date = DateTime.Parse(read["Date"].ToString()),

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
