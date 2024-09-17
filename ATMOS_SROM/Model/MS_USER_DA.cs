using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ATMOS_SROM.Model
{
    public class MS_USER_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_USER> getMsUserByQuery(string where)
        {
            List<MS_USER> listMsUser = new List<MS_USER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select idUser, userName, realName, store, userLevel, email, appraisal, online, lastLogin, createdBy, createdDate, updatedBy, updatedDate, status, kodeCust from MS_USER {0}",where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_USER item = new MS_USER();
                        item.idUser = reader.GetInt64(0);
                        item.userName = reader.GetString(1);
                        item.realName = reader.GetString(2);
                        item.store = reader.GetString(3);
                        item.userLevel = reader.GetString(4);
                        item.email = reader.GetString(5);
                        item.appraisal = reader.IsDBNull(reader.GetOrdinal("appraisal")) ? "" : reader.GetString(6);
                        item.online = reader.IsDBNull(reader.GetOrdinal("online")) ? "" : reader.GetString(7);
                        item.lastLogin = reader.IsDBNull(reader.GetOrdinal("lastLogin")) ? (DateTime?)null : reader.GetDateTime(8);
                        
                        item.createdBy = reader.GetString(9);
                        item.createdDate = reader.GetDateTime(10);
                        item.updatedBy = reader.IsDBNull(reader.GetOrdinal("updatedBy")) ? "" : reader.GetString(11);
                        item.updatedDate = reader.IsDBNull(reader.GetOrdinal("updatedDate")) ? (DateTime?)null : reader.GetDateTime(12);
                        item.status = reader.GetBoolean(13);
                        item.kodeCust = reader.GetString(14);
                        
                        listMsUser.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMsUser;
        }

        public void updateMsUser(MS_USER user)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_USER set userName = @username, realName = @realName, store = @store, userLevel = @userLevel, " +
                    " email = @email, appraisal = @appraisal, online = @online, lastLogin = @lastLogin, status=@status, updatedBy = @lastModifiedBy, updatedDate = getdate() where idUser = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = user.userName;
                    command.Parameters.Add("@realName", SqlDbType.VarChar).Value = user.realName;
                    command.Parameters.Add("@store", SqlDbType.VarChar).Value = user.store;
                    command.Parameters.Add("@userLevel", SqlDbType.VarChar).Value = user.userLevel;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = user.email;
                    command.Parameters.Add("@appraisal", SqlDbType.VarChar).Value = user.appraisal;
                    command.Parameters.Add("@online", SqlDbType.VarChar).Value = user.online;
                    command.Parameters.Add("@lastLogin", SqlDbType.DateTime2).Value = user.lastLogin;
                    
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = user.status;
                    command.Parameters.Add("@lastModifiedBy", SqlDbType.VarChar).Value = user.updatedBy;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.idUser;
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

        public string insertMsUser(MS_USER user)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_USER (userName, idStore) values " +
                    " (@userName, @idStore) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@userName", SqlDbType.VarChar).Value = user.userName;
                    command.Parameters.Add("@idStore", SqlDbType.BigInt).Value = user.idStore == null ? (object) Convert.DBNull : user.idStore;

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