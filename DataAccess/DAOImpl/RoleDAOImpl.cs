using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOImpl
{
    public class RoleDAOImpl
    {
        public List<RoleDTO> Roles_GetList()
        {
            var result = new List<RoleDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();
                SqlCommand cmd = new SqlCommand("SP_GetListRole", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new RoleDTO
                    {
                        RoleID = int.Parse(read["RoleID"].ToString()),
                        RoleName = read["RoleName"].ToString()
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
