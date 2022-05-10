using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectDB
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlconn = null;
            try
            {
                var connectName = "Server=.;Database=FPTLibraryManagement;Trusted_Connection=True;Max Pool Size=400";

                sqlconn = new SqlConnection(connectName);

                if (sqlconn.State == System.Data.ConnectionState.Open)
                {
                    sqlconn.Close();
                }

                sqlconn.Open();
            }
            catch (Exception)
            {
                throw;
            }

            return sqlconn;

        }
    }
}
