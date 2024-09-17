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
    public class MS_MEMBER_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_MEMBER> getMember(string where)
        {
            List<MS_MEMBER> listMember = new List<MS_MEMBER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, FIRST_NAME, LAST_NAME, PHONE, EMAIL, ALAMAT, BRAND, STATUS_MEMBER, " +
                    " CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS from MS_MEMBER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MEMBER item = new MS_MEMBER();
                        item.ID = reader.GetInt64(0);
                        item.FIRST_NAME = reader.IsDBNull(reader.GetOrdinal("FIRST_NAME")) ? "" : reader.GetString(1);
                        item.LAST_NAME = reader.IsDBNull(reader.GetOrdinal("LAST_NAME")) ? "" : reader.GetString(2);
                        item.PHONE = reader.IsDBNull(reader.GetOrdinal("PHONE")) ? "" : reader.GetString(3);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(4);
                        item.ALAMAT = reader.IsDBNull(reader.GetOrdinal("ALAMAT")) ? "" : reader.GetString(5);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(6);
                        item.STATUS_MEMBER = reader.IsDBNull(reader.GetOrdinal("STATUS_MEMBER")) ? "" : reader.GetString(7);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(8);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(9);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(10);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(12);
                        
                        listMember.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMember;
        }

        public string insertMember(MS_MEMBER member)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_MEMBER (FIRST_NAME, LAST_NAME, PHONE, EMAIL, ALAMAT, BRAND, STATUS_MEMBER, CREATED_BY, CREATED_DATE, STATUS) values " +
                    " (@first, @last, @phone, @email, @alamat, @brand, @statusMember, @createdBy, getdate(), 1); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@first", SqlDbType.VarChar).Value = member.FIRST_NAME;
                    command.Parameters.Add("@last", SqlDbType.VarChar).Value = member.LAST_NAME;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = member.PHONE;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = member.EMAIL;
                    command.Parameters.Add("@alamat", SqlDbType.VarChar).Value = member.ALAMAT;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = member.BRAND;
                    command.Parameters.Add("@statusMember", SqlDbType.VarChar).Value = member.STATUS_MEMBER;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = member.CREATED_BY;
                    
                    newId = command.ExecuteScalar().ToString();
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

        public void updateStatus(MS_MEMBER member)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_MEMBER set STATUS_MEMBER = @status, UPDATED_BY = @updateBy, UPDATED_DATE = getdate() where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = member.STATUS_MEMBER;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = member.UPDATED_BY;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = member.ID;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}