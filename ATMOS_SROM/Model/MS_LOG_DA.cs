using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;

namespace ATMOS_SROM.Model
{
    public class MS_LOG_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void addMsLog(MS_LOG log)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = "insert into MS_LOG (description, userName,ipAddress,logDate) values (@description, @username, @ipAddress, @logDate)";
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@description", SqlDbType.VarChar).Value = log.description;
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = log.userName;
                    command.Parameters.Add("@ipAddress", SqlDbType.VarChar).Value = log.ipAddress;
                    command.Parameters.Add("@logDate", SqlDbType.DateTime).Value = log.logDate;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}