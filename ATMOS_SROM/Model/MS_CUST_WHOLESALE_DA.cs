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
    public class MS_CUST_WHOLESALE_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection Connection = new SqlConnection(conString);

        public DataSet GetMS_CUST_WHOLESALE(string whereCon)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KD_PEMBELI, [NM_PEMBELI], [CREATED_BY], [CREATED_DATE], [BLOCK], [UPDATED_BY], [UPDATED_DATE] " +
                "from MS_CUST_WHOLESALE {0}", whereCon), Connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 3600;
                //command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                Connection.Close();
            }
            return dataSet;
        }
        public DataSet GetTf_View_Wholesale_Customer(string whereCon)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KD_PEMBELI, NM_PEMBELI, JML_SO, " +
                "HAS_SO_ORDER, NO_SO, [BLOCK] " +
                "from Tf_View_Wholesale_Customer() {0} ORDER BY ID DESC", whereCon), Connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 3600;
                //command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                Connection.Close();
            }
            return dataSet;
        }
        public string InsMS_CUST_WHOLESALE(MS_CUST_WHOLESALE item)
        {
            string newid = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("INSERT INTO MS_CUST_WHOLESALE (KD_PEMBELI, NM_PEMBELI, CREATED_BY, CREATED_DATE, BLOCK) " +
                    " VALUES (@KD_PEMBELI, @NM_PEMBELI, @CREATED_BY, GETDATE(), @BLOCK)");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@KD_PEMBELI", SqlDbType.VarChar).Value = item.KD_PEMBELI;
                    command.Parameters.Add("@NM_PEMBELI", SqlDbType.VarChar).Value = item.NM_PEMBELI;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = item.CREATED_BY;
                    command.Parameters.Add("@BLOCK", SqlDbType.Bit).Value = item.BLOCK;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newid = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newid;
        }
        public string UpdMS_CUST_WHOLESALE(MS_CUST_WHOLESALE item)
        {
            string newid = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_CUST_WHOLESALE Set BLOCK = @BLOCK, UPDATED_BY = @UPDATED_BY, " +
                    "UPDATED_DATE = GETDATE(), KD_PEMBELI = @KD_PEMBELI, NM_PEMBELI = @NM_PEMBELI WHERE ID = @ID");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.BigInt).Value = item.ID;
                    command.Parameters.Add("@UPDATED_BY", SqlDbType.VarChar).Value = item.UPDATED_BY;
                    command.Parameters.Add("@BLOCK", SqlDbType.Bit).Value = item.BLOCK;
                    command.Parameters.Add("@KD_PEMBELI", SqlDbType.VarChar).Value = item.KD_PEMBELI;
                    command.Parameters.Add("@NM_PEMBELI", SqlDbType.VarChar).Value = item.NM_PEMBELI;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newid = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newid;
        }

    }

}