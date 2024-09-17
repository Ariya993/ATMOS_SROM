using ATMOS_SROM.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Model.CustomObj
{
    public class MS_SUPPLIER_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public List<MS_SHOWROOM> getListSuppl(string where)
        {
            List<MS_SHOWROOM> ListSuppl = new List<MS_SHOWROOM>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, SHOWROOM, ALAMAT, PHONE, STATUS, STATUS_SHOWROOM, KODE, STORE, BRAND, VENDOR from MS_SHOWROOM {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_SHOWROOM item = new MS_SHOWROOM();
                        item.ID = reader.GetInt64(0);
                        item.SHOWROOM = reader.GetString(1);
                        item.ALAMAT = reader.GetString(2);
                        item.PHONE = reader.GetString(3);
                        item.STATUS = reader.GetString(4);
                        item.STATUS_SHOWROOM = reader.GetString(5);
                        item.KODE = reader.GetString(6);
                        item.STORE = reader.GetString(7);
                        item.BRAND = reader.GetString(8);
                        item.VALUE = reader.GetInt64(0).ToString() + "#" + reader.GetString(6);
                        item.VENDOR = reader.IsDBNull(reader.GetOrdinal("VENDOR")) ? "" : reader.GetString(9);
                        ListSuppl.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListSuppl;
        }
        public string AddSuppl(MS_SHOWROOM showroom)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_SHOWROOM (KODE, BRAND, ALAMAT, PHONE, STATUS, STATUS_SHOWROOM, VENDOR, SHOWROOM, STORE) values " +
                    " (@kode, @brand, @alamat, @phone, '', 'SUP', @vendor, '','') ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = showroom.KODE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = showroom.BRAND;
                    command.Parameters.Add("@alamat", SqlDbType.Text).Value = showroom.ALAMAT;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = showroom.PHONE;
                    command.Parameters.Add("@vendor", SqlDbType.VarChar).Value = showroom.VENDOR;
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
        public string updateSuppl(MS_SHOWROOM shr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_SHOWROOM SET ALAMAT=@alamat, PHONE=@phone, VENDOR=@vendor " +
                    "WHERE KODE = @kode");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = shr.KODE;
                    command.Parameters.Add("@alamat", SqlDbType.VarChar).Value = shr.ALAMAT;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = shr.PHONE;
                    command.Parameters.Add("@vendor", SqlDbType.VarChar).Value = shr.VENDOR;
                   
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