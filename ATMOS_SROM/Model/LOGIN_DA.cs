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
    public class LOGIN_DA
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_USER> getMsUserLogin(string userName, string password)
        {
            List<MS_USER> listMsUser = new List<MS_USER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("Select idUser, userName, realName, store, userLevel, email, appraisal, online, lastLogin, " +
                    "lastIP, createdBy, createdDate, updatedBy, updatedDate, status,DATEDIFF(MINUTE, lastLogin, dateadd(hour,7,GETUTCDATE())) LASTLOG, kodeCust, " +
                    "idStore, STORE_SHOWROOM, BRAND, STATUS_SHOWROOM, DATEDIFF(MONTH, ISNULL(lastChangePass,'2010-10-12'), GETDATE()) TIMELASTCHANGEPASS " +
                    "from vw_user where userName = @userName and password = @password and status = 1", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@userName", userName));
                    command.Parameters.Add(new SqlParameter("@password", password));
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
                        item.lastIP = reader.IsDBNull(reader.GetOrdinal("lastIP")) ? "" : reader.GetString(9);
                        item.createdBy = reader.GetString(10);
                        item.createdDate = reader.GetDateTime(11);
                        item.updatedBy = reader.IsDBNull(reader.GetOrdinal("updatedBy")) ? "" : reader.GetString(12);
                        item.updatedDate = reader.IsDBNull(reader.GetOrdinal("updatedDate")) ? (DateTime?)null : reader.GetDateTime(13);
                        item.status = reader.GetBoolean(14);
                        item.lastLongOff = reader.IsDBNull(reader.GetOrdinal("LASTLOG")) ? 0 : reader.GetInt32(15);
                        item.kodeCust = reader.GetString(16);
                        item.idStore = reader.GetInt64(17);
                        item.STORE_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("STORE_SHOWROOM")) ? "" : reader.GetString(18);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(19);
                        item.STATUS_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("STATUS_SHOWROOM")) ? "" : reader.GetString(20);
                        item.timeLastChangePass = reader.IsDBNull(reader.GetOrdinal("TIMELASTCHANGEPASS")) ? 0 : reader.GetInt32(21);

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

        public List<MS_MENU> getMenu(string where)
        {
            List<MS_MENU> listMenu = new List<MS_MENU>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_USER_LEVEL, ID_MENU, ID_PARENT, MENU, MENU_NAME, MENU_PATH, USER_LEVEL " + 
                    " from vw_menu {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MENU item = new MS_MENU();
                        item.ID = reader.GetInt64(0);
                        item.ID_USER_LEVEL = reader.IsDBNull(reader.GetOrdinal("ID_USER_LEVEL")) ? 0 : reader.GetInt64(1);
                        item.ID_MENU = reader.IsDBNull(reader.GetOrdinal("ID_MENU")) ? 0 : reader.GetInt64(2);
                        item.ID_PARENT = reader.IsDBNull(reader.GetOrdinal("ID_PARENT")) ? 0 : reader.GetInt64(3);
                        item.MENU = reader.IsDBNull(reader.GetOrdinal("MENU")) ? "" : reader.GetString(4);
                        item.MENU_NAME = reader.IsDBNull(reader.GetOrdinal("MENU_NAME")) ? "" : reader.GetString(5);
                        item.MENU_PATH = reader.IsDBNull(reader.GetOrdinal("MENU_PATH")) ? "" : reader.GetString(6);
                        item.USER_LEVEL = reader.IsDBNull(reader.GetOrdinal("USER_LEVEL")) ? "" : reader.GetString(7);
                        
                        listMenu.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMenu;
        }

        public List<MS_MENU> getMenuAdmin(string where)
        {
            List<MS_MENU> listMenu = new List<MS_MENU>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_PARENT, MENU, MENU_NAME, MENU_PATH " +
                    " from MS_MENU {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MENU item = new MS_MENU();
                        item.ID = reader.GetInt64(0);
                        item.ID_PARENT = reader.IsDBNull(reader.GetOrdinal("ID_PARENT")) ? 0 : reader.GetInt64(1);
                        item.MENU = reader.IsDBNull(reader.GetOrdinal("MENU")) ? "" : reader.GetString(2);
                        item.MENU_NAME = reader.IsDBNull(reader.GetOrdinal("MENU_NAME")) ? "" : reader.GetString(3);
                        item.MENU_PATH = reader.IsDBNull(reader.GetOrdinal("MENU_PATH")) ? "" : reader.GetString(4);

                        listMenu.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMenu;
        }

        public void updateLoginStatus(MS_USER user)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_USER set online = @online, lastLogin = @lastLogin, lastIP = @IP where idUser = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {                    
                    command.Parameters.Add("@online", SqlDbType.VarChar).Value = user.online;
                    command.Parameters.Add("@lastLogin", SqlDbType.DateTime2).Value = user.lastLogin;
                    command.Parameters.Add("@IP", SqlDbType.VarChar).Value = user.lastIP;
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

        public void changePass(MS_USER user)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_USER set password = @pass, lastChangePass = @lastChange where idUser = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@pass", SqlDbType.VarChar).Value = user.password;
                    command.Parameters.Add("@lastChange", SqlDbType.DateTime).Value = user.lastChangePass;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = user.idUser;
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

        public MS_PARAMETER getParam(string where)
        {
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NAME, VALUE, CREATED_DATE, UPDATED_DATE from MS_PARAMETER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PARAMETER item = new MS_PARAMETER();
                        item.ID = reader.GetInt64(0);
                        item.NAME = reader.GetString(1);
                        item.VALUE = reader.GetString(2);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? Convert.ToDateTime("9999-01-01") : reader.GetDateTime(3);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(4);

                        listParam.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listParam.First();
        }

        public List<MS_PARAMETER> getListParam(string where)
        {
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NAME, VALUE, CREATED_DATE, UPDATED_DATE from MS_PARAMETER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PARAMETER item = new MS_PARAMETER();
                        item.ID = reader.GetInt64(0);
                        item.NAME = reader.GetString(1);
                        item.VALUE = reader.GetString(2);
                        item.CREATED_DATE = reader.GetDateTime(3);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(4);

                        listParam.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listParam;
        }

        public void updateValueParam(MS_PARAMETER param)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_PARAMETER set VALUE = @value where NAME = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = param.VALUE;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = param.NAME;
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
        public string usp_checkLoginUser(String userName, String Pass, String HostLogIn)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_checkLoginUser", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@userName", userName));
                    command.Parameters.Add(new SqlParameter("@Pass", Pass));
                    command.Parameters.Add(new SqlParameter("@HostLogIn", HostLogIn));
                    Connection.Open();

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

    }
}