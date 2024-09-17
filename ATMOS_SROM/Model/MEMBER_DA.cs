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
    public class MEMBER_DA
    {
        private static string conStringMember = ConfigurationManager.ConnectionStrings["ConnectionStringMember"].ConnectionString;

        public List<MS_MEMBER> getMember(string where)
        {
            List<MS_MEMBER> listMember = new List<MS_MEMBER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_CARD, PIN " +
                    " from CUSTCARD {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MEMBER item = new MS_MEMBER();
                        item.ID = reader.GetInt64(0);
                        item.NO_CARD = reader.GetString(1);
                        item.PIN = reader.GetString(2);

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

        public int checkCard(string noCard, string pin, int poin, string kodeCust, string createdBy)
        {
            int countCust = 0;
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);

                using (SqlCommand command = new SqlCommand(string.Format("usp_checkValidMember"), Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@noMember", noCard);
                    command.Parameters.Add("@pin", pin);
                    command.Parameters.Add("@point", poin);
                    command.Parameters.Add("@kodeCust", kodeCust);
                    command.Parameters.Add("@createdBy", createdBy);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        countCust = reader.GetInt32(0);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return countCust;
        }

        public int getPointMember(string where)
        {
            int point = 0;
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("Select POINT " +
                    " from MS_POINT {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {

                        point = reader.IsDBNull(reader.GetOrdinal("POINT")) ? 0 : reader.GetInt32(0);
                       
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return point;
        }

        public MS_MEMBER getStatusAktifMember(string where)
        {
            MS_MEMBER item = new MS_MEMBER();
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("Select WRONG_PIN,TGL_MASA_TENGGANG,TGL_EXPIRED " +
                    " from CUSTCARD {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {

                        item.WRONG_PIN = reader.IsDBNull(reader.GetOrdinal("WRONG_PIN")) ? 0 : reader.GetInt32(0);
                        item.TGL_MASA_TENGGANG =  reader.GetDateTime(1);
                        item.TGL_EXPIRED = reader.GetDateTime(2);

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public string UpCustCardBlockPIN(string where)
        {
            string res = "Pin Blocked";
            SqlConnection Connection = new SqlConnection(conStringMember);
            try
            {
                string query = String.Format("UPDATE CUSTCARD SET WRONG_PIN = 3 {0}", where);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                res = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return res;
        }

        public List<MS_MEMBER> getMemberForEmail(string where)
        {
            List<MS_MEMBER> listMember = new List<MS_MEMBER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_CARD, PIN, EMAIL, NAMA " +
                    " from CUSTCARD {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MEMBER item = new MS_MEMBER();
                        item.ID = reader.GetInt64(0);
                        item.NO_CARD = reader.GetString(1);
                        item.PIN = reader.GetString(2);
                        item.EMAIL = reader.GetString(3);
                        item.FIRST_NAME = reader.GetString(4);
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


        public String getLastpoinRP(string where)
        {
            String poinRP = "";
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("select top 1 Q_POINT from PONM where JNS = 'RP' {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        poinRP = reader.GetInt32(0).ToString("N");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return poinRP;
        }
        public String getLastpoinRD(string where)
        {
            String poinRD = "";
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("select top 1 Q_POINT from PONM where JNS = 'RD' {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        poinRD = reader.GetInt32(0).ToString("N");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return poinRD;
        }
        public String getLastpoinBalance(string where)
        {
            String poinBalance = "";
            try
            {
                SqlConnection Connection = new SqlConnection(conStringMember);
                using (SqlCommand command = new SqlCommand(string.Format("select POINT from MS_POINT {0} ", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        poinBalance = reader.GetInt32(0).ToString("N");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return poinBalance;
        }
    }
}