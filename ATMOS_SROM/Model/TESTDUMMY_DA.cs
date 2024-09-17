using ATMOS_SROM.Domain;
using ATMOS_SROM.Domain.CustomObj;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Model
{
    public class TESTDUMMY_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public string insertFromStagingWholesaleToSOWholeSales(string uName, SO_WHOLESALES_HEADER SoHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertFromStagingWholesaleToSOWholeSales", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@nama", uName);
                    command.Parameters.Add("@nourut", SoHeader.NO_SO);
                    command.Parameters.Add("@nopo", SoHeader.NO_PO);
                    command.Parameters.Add("@kodecust", SoHeader.KODE_CUST);
                    command.Parameters.Add("@kode", SoHeader.KODE);
                    command.Parameters.Add("@tgltrans", SoHeader.TGL_TRANS);
                    command.Parameters.Add("@tglsend", SoHeader.SEND_DATE);
                    command.Parameters.Add("@qty", SoHeader.QTY);
                    command.Parameters.Add("@margin", SoHeader.MARGIN);
                    command.Parameters.Add("@frtur", SoHeader.FRETUR);
                    command.CommandTimeout = 300;
                    Connection.Open();

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

        public List<TEMP_STRUCK> getSO_WholeSales(string where)
        {
            List<TEMP_STRUCK> listTemp = new List<TEMP_STRUCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select ID, ITEM_CODE, ART_DESC, SIZE, BRAND, PRICE, QTY, SA_NAME, CREATED_BY, CREATED_DATE, " +
                    "BARCODE, ART_PRICE, SPCL_PRICE, DISCOUNT, NET_PRICE, JENIS_DISCOUNT, BON_PRICE, ID_KDBRG, ID_ACARA, RETUR, NET_DISCOUNT, WARNA from vw_TempSOWholeSales {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_STRUCK item = new TEMP_STRUCK();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.ART_DESC = reader.GetString(2);
                        item.SIZE = reader.GetString(3);
                        item.BRAND = reader.GetString(4);
                        item.PRICE = reader.GetDecimal(5);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(6);
                        item.SA_NAME = reader.IsDBNull(reader.GetOrdinal("SA_NAME")) ? "" : reader.GetString(7);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(8);
                        item.CREATED_DATE = reader.GetDateTime(9);
                        item.ALL_DETAIL = reader.GetString(2) + reader.GetString(3);
                        item.TOTAL_PRICE = reader.GetInt32(6) * reader.GetDecimal(14);

                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(10);
                        item.ART_PRICE = reader.IsDBNull(reader.GetOrdinal("ART_PRICE")) ? 0 : reader.GetDecimal(11);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(12);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(13);
                        item.NET_PRICE = reader.IsDBNull(reader.GetOrdinal("NET_PRICE")) ? 0 : reader.GetDecimal(14);
                        item.JENIS_DISCOUNT = reader.IsDBNull(reader.GetOrdinal("JENIS_DISCOUNT")) ? "" : reader.GetString(15);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(16);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(17);
                        item.ID_ACARA = reader.IsDBNull(reader.GetOrdinal("ID_ACARA")) ? 0 : reader.GetInt64(18);
                        item.RETUR = reader.IsDBNull(reader.GetOrdinal("RETUR")) ? "" : reader.GetString(19);
                        item.NET_DISCOUNT = reader.IsDBNull(reader.GetOrdinal("NET_DISCOUNT")) ? 0 : reader.GetDecimal(20);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(21);

                        listTemp.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTemp;
        }
        public List<SO_WHOLESALES_HEADER> getSoHeader(string where)
        {
            List<SO_WHOLESALES_HEADER> listBayar = new List<SO_WHOLESALES_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_PO, NO_SO, KODE_CUST, KODE, TGL_TRANS, STATUS_HEADER, SEND_DATE, QTY, MARGIN, FRETUR " +
                    " from SO_WHOLESALES_HEADER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SO_WHOLESALES_HEADER item = new SO_WHOLESALES_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.NO_PO = reader.GetString(1);
                        item.NO_SO = reader.GetString(2);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(3);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(4);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(5);
                        item.STATUS_HEADER = reader.IsDBNull(reader.GetOrdinal("STATUS_HEADER")) ? "" : reader.GetString(6);
                        item.SEND_DATE = reader.IsDBNull(reader.GetOrdinal("SEND_DATE")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(8);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetInt32(9);
                        item.FRETUR = reader.IsDBNull(reader.GetOrdinal("FRETUR")) ? "No" : reader.GetString(10);

                        listBayar.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBayar;
        }

        public string CekDoubleBarcode(string where)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand(string.Format("select BARCODE from TEMP_STRUCK {0} GROUP BY BARCODE HAVING COUNT(BARCODE)>1 ", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        if (newId == "")
                        {
                            newId = reader.GetString(0);
                        }
                        else
                        {
                            newId = newId + ", " + reader.GetString(0);
                        }
                    }
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

        public string CekNoPOInSOWholeSales(string where)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand(string.Format("select COUNT(*) from SO_WHOLESALES_HEADER {0}  ", where), Connection))
                {
                    command.CommandType = CommandType.Text;
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

        public List<SO_WHOLESALES> GetSOToShPutusList(string where)
        {
            List<SO_WHOLESALES> listTemp = new List<SO_WHOLESALES>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select  SCAN_NO , NO_SO, QTY, QTY_REAL, TGL_REAL, TGL_KIRIM, ID from vw_SoScan {0} ORDER BY NO_SO", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SO_WHOLESALES item = new SO_WHOLESALES();
                        item.NO_SCAN = reader.GetString(0);
                        item.NO_SO = reader.GetString(1);
                        item.QTY = reader.GetInt32(2);
                        item.QTY_REAL_1 = reader.GetInt32(3);
                        item.TGL_REAL_1 = reader.IsDBNull(reader.GetOrdinal("TGL_REAL")) ? Convert.ToDateTime(null) : reader.GetDateTime(4);
                        item.TGL_KIRIM_1 = reader.IsDBNull(reader.GetOrdinal("TGL_KIRIM")) ? Convert.ToDateTime(null) : reader.GetDateTime(5);
                        item.ID = reader.GetInt64(6);
                        listTemp.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTemp;
        }

        public string CekSoInSHPutus(string where)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand(string.Format("select NO_BON from SH_PUTUS_HEADER {0} ", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {

                        newId = reader.GetString(0);
                    }
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

        public List<SO_WHOLESALES> GetSODetail(string where)
        {
            List<SO_WHOLESALES> listTemp = new List<SO_WHOLESALES>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select ID,	NO_SO,	BARCODE,	ITEM_CODE,	ART_DESC,	WARNA,	SIZE,	BRAND,	PRICE,	QTY,	QTY_REAL_1,	TGL_REAL_1,	TGL_KIRIM_1,	QTY_REAL_2,	TGL_REAL_2,	TGL_KIRIM_2 "+
                " from SO_WHOLESALES {0} ORDER BY NO_SO", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SO_WHOLESALES item = new SO_WHOLESALES();
                        item.ID = reader.GetInt64(0);
                        item.NO_SO = reader.GetString(1);
                        item.BARCODE = reader.GetString(2);
                        item.ITEM_CODE = reader.GetString(3);
                        item.ART_DESC = reader.GetString(4);
                        item.WARNA = reader.GetString(5);
                        item.SIZE = reader.GetString(6);
                        item.BRAND = reader.GetString(7);
                        item.PRICE = reader.GetDecimal(8);
                        item.QTY = reader.GetInt32(9);
                        item.QTY_REAL_1 = reader.GetInt32(10);
                        item.TGL_REAL_1 = reader.IsDBNull(reader.GetOrdinal("TGL_REAL_1")) ? Convert.ToDateTime(null) : reader.GetDateTime(11);
                        item.TGL_KIRIM_1 = reader.IsDBNull(reader.GetOrdinal("TGL_KIRIM_1")) ? Convert.ToDateTime(null) : reader.GetDateTime(12);
                        item.QTY_REAL_2 = reader.GetInt32(13);
                        item.TGL_REAL_2 = reader.IsDBNull(reader.GetOrdinal("TGL_REAL_2")) ? Convert.ToDateTime(null) : reader.GetDateTime(14);
                        item.TGL_KIRIM_2 = reader.IsDBNull(reader.GetOrdinal("TGL_KIRIM_2")) ? Convert.ToDateTime(null) : reader.GetDateTime(15);
                        listTemp.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTemp;
        }

        public List<vw_CloseSO> GetCloseSOList(string where)
        {
            List<vw_CloseSO> listTemp = new List<vw_CloseSO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select NO_PO, NO_SO, KODE_CUST, TGL_TRANS, SEND_DATE, QTY, QTY_REAL_1, TGL_KIRIM_1, QTY_REAL_2, TGL_KIRIM_2, FRETUR, NO_BON, STATUS_HEADER from VW_CLOSESO {0} ORDER BY NO_SO", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        vw_CloseSO item = new vw_CloseSO();
                        item.NO_PO = reader.GetString(0);
                        item.NO_SO = reader.GetString(1);
                        item.KODE_CUST = reader.GetString(2);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? Convert.ToDateTime(null) : reader.GetDateTime(3);
                        item.SEND_DATE = reader.IsDBNull(reader.GetOrdinal("SEND_DATE")) ? Convert.ToDateTime(null) : reader.GetDateTime(4);
                        item.QTY = reader.GetInt32(5);
                        item.QTY_REAL_1 = reader.GetInt32(6);
                        item.TGL_KIRIM_1 = reader.IsDBNull(reader.GetOrdinal("TGL_KIRIM_1")) ? Convert.ToDateTime(null) : reader.GetDateTime(7);
                        item.QTY_REAL_2 = reader.GetInt32(8);
                        item.TGL_KIRIM_2 = reader.IsDBNull(reader.GetOrdinal("TGL_KIRIM_2")) ? Convert.ToDateTime(null) : reader.GetDateTime(9);
                        item.FRETUR = reader.GetString(10);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(11);
                        item.STATUS_HEADER = reader.GetString(12);
                        listTemp.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTemp;
        }
        public virtual void UpdateSoWholesalesHeaderStatus(string noSO)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE SO_WHOLESALES_HEADER set STATUS_HEADER = 'DONE' where NO_SO = @noSO");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noSO", SqlDbType.VarChar).Value = noSO;

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
        }

        public Int64 getIDSHPutusDetail(String where)
        {
            Int64 iddetail = 0 ;
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                
                using (SqlCommand command = new SqlCommand(string.Format("Select ID from SH_PUTUS_DETAIL {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        iddetail = reader.GetInt64(0);
                       
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iddetail;
        }
    }
}