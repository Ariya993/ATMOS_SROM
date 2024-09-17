using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATMOS_SROM.Domain;

namespace ATMOS_SROM.Model
{
    public class TEMP_POS_TO_EDC_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public string insertTEMP_POS_TO_EDC(TEMP_POS_TO_EDC tempEDC)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_POS_TO_EDC (CardPay, Bank, EDC, KODE_CUST, KODE_CT, CRT_DT, CRT_BY, STAT_TRANS) values " +
                    " (@CardPay, @Bank, @EDC, @KODE_CUST, @KODE_CT, GETDATE(),@user, 'NEW') ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@CardPay", SqlDbType.Decimal).Value = tempEDC.CardPay;
                    command.Parameters.Add("@Bank", SqlDbType.VarChar).Value = tempEDC.Bank;
                    command.Parameters.Add("@EDC", SqlDbType.VarChar).Value = tempEDC.EDC;
                    command.Parameters.Add("@KODE_CUST", SqlDbType.VarChar).Value = tempEDC.KODE_CUST;
                    command.Parameters.Add("@KODE_CT", SqlDbType.VarChar).Value = tempEDC.KODE_CT;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = tempEDC.CRT_BY;

                    command.ExecuteNonQuery();
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