using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Model
{
    public class CONFIG_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public string insertTRFStock(DateTime tglBlnStartCut, DateTime tglBlnEndCut, string fBulan, DateTime tglStart, DateTime tglEnd, string createdBy, string statusShow1, string statusShow2)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {//@tglMulai date, @tglCutOff date, @fBulan varchar(5), @dateStart date, @dateEnd date, @createdBy varchar(50)
                using (SqlCommand command = new SqlCommand("usp_insertKartuStock", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@tglMulai", tglBlnStartCut));
                    command.Parameters.Add(new SqlParameter("@tglCutOff", tglBlnEndCut));
                    command.Parameters.Add(new SqlParameter("@fBulan", fBulan));
                    command.Parameters.Add(new SqlParameter("@dateStart", tglStart));
                    command.Parameters.Add(new SqlParameter("@dateEnd", tglEnd));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
                    command.Parameters.Add(new SqlParameter("@statusShowroom1", statusShow1));
                    command.Parameters.Add(new SqlParameter("@statusShowroom2", statusShow2));
                    command.CommandTimeout = 0;

                    Connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }

        public string unlockData(string bulanUnlock, string bulanLock, string param, string status1, string status2)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_unlockData", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@bulanUnlock", bulanUnlock));
                    command.Parameters.Add(new SqlParameter("@bulanLock", bulanLock));
                    command.Parameters.Add(new SqlParameter("@param", param));
                    command.Parameters.Add(new SqlParameter("@status1", status1));
                    command.Parameters.Add(new SqlParameter("@status2", status2));
                    command.CommandTimeout = 3600;
                    Connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }
    }
}