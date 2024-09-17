using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using ATMOS_SROM.Domain.CustomObj;
//using MySql.Data.MySqlClient;

namespace ATMOS_SROM.Model
{
    public class MS_KDBRG_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string mySqlConn = "";
        //MySqlConnection Cn2 = new MySqlConnection("server=202.43.166.203;user=root;" + 
        //    "database=theexecutive_shop;port=8085;password=123456;");

        //public void testing()
        //{
        //    DataTable Mt_h = new DataTable();
        //    MySqlDataAdapter Ma_h = new MySqlDataAdapter("select * from columns_priv", Cn2);
        //    Cn2.Open();
        //    DataSet Ms_h = new DataSet();
        //    Ma_h.Fill(Mt_h);
        //    Cn2.Close();
        //}

        public List<TEMP_STRUCK> getTempStruck(string where)
        {
            List<TEMP_STRUCK> listTemp = new List<TEMP_STRUCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select ID, ITEM_CODE, ART_DESC, SIZE, BRAND, PRICE, QTY, SA_NAME, CREATED_BY, CREATED_DATE, " +
                    "BARCODE, ART_PRICE, SPCL_PRICE, DISCOUNT, NET_PRICE, JENIS_DISCOUNT, BON_PRICE, ID_KDBRG, ID_ACARA, RETUR, NET_DISCOUNT, WARNA from vw_tempStruck {0}", where), Connection))
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
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetDecimal(13);
                        //item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(13);
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

        public List<TEMP_WHOLESALE> getTempWholesale(string where)
        {
            List<TEMP_WHOLESALE> listTemp = new List<TEMP_WHOLESALE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select ID, TGL_PENJUALAN, TGL_PENGIRIMAN, NAMA_PEMBELI, KODE_PEMBELI, MARGIN, CREATED_BY, CREATED_DATE " +
                    " from TEMP_WHOLESALE {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_WHOLESALE item = new TEMP_WHOLESALE();
                        item.ID = reader.GetInt64(0);
                        item.TGL_PENJUALAN = reader.GetDateTime(1);
                        item.TGL_PENGIRIMAN = reader.GetDateTime(2);
                        item.NAMA_PEMBELI = reader.GetString(3);
                        item.KODE_PEMBELI = reader.GetString(4);
                        item.MARGIN = reader.GetDecimal(5);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(6);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(7);

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

        public List<MS_KDBRG> getMsKdbrg(string where, string kode)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, SPCL_PRICE, DISCOUNT, START_DATE, END_DATE, FLAG, CATATAN, ID_PROMO, CREATED_BY, CREATED_DATE, STOCK, STATUS_BRG " +
                    //" from vw_articlePromo {0}", where), Connection))
                    " from tf_stockArticlePromo(@kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", kode);
                    command.CommandTimeout = 300;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(17);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(18);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(19);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.FLAG = reader.IsDBNull(reader.GetOrdinal("FLAG")) ? "" : reader.GetString(21);
                        item.CATATAN = reader.IsDBNull(reader.GetOrdinal("CATATAN")) ? "" : reader.GetString(22);
                        item.ID_PROMO = reader.IsDBNull(reader.GetOrdinal("ID_PROMO")) ? 0 : reader.GetInt64(23);
                        item.CREATED_BY = reader.GetString(24);
                        item.CREATED_DATE = reader.GetDateTime(25);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(26);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(27);
                        //item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(28);

                        //item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3) + " " + reader.GetString(4);
                        //item.ID_PROMO_SHR = reader.IsDBNull(reader.GetOrdinal("ID_PROMO_SHR")) ? 0 : reader.GetInt64(27);
                        //item.DISC_PROMO_SHR = reader.IsDBNull(reader.GetOrdinal("DISC_PROMO_SHR")) ? 0 : reader.GetInt32(28);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgSearch100(string where, string kode)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 100 ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    " PRICE, BARCODE, STOCK " +
                    //" from vw_articlePromo {0}", where), Connection))
                    " from tf_stockArticlePromoStockAvailable(@kode) brg {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", kode);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);

                        item.PRICE = reader.GetDecimal(5);

                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);

                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(7);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgTable(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 100 ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    " PRICE, BARCODE from MS_KDBRG brg {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.PRICE = reader.GetDecimal(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }
        public List<MS_KDBRG> getMsKdbrgTablebyBrand(string where, string superBrand)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 100 brg.ID, brg.ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    " PRICE, brg.BARCODE from MS_KDBRG brg join MS_STOCK stock on brg.BARCODE = stock.BARCODE {0} AND stock.STOCK > 0 AND brg.FBRAND IN (SELECT FBRAND FROM BRAND WHERE SUPER_BRAND IN ({1})) ", where, superBrand), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.PRICE = reader.GetDecimal(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }
        public List<MS_KDBRG> getMsKdbrgTablebyBrandNew(string where, string superBrand, string Kode)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 100 ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    " PRICE, BARCODE from tf_stockArticlePromoJenahara('{0}') {1} AND ( FBRAND IN (SELECT FBRAND FROM BRAND WHERE SUPER_BRAND IN ({2}) OR FBRAND IN ({2}))) ", Kode, where, superBrand), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.PRICE = reader.GetDecimal(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getAllMsKdbrgTable(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    " PRICE, BARCODE from MS_KDBRG {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.PRICE = reader.GetDecimal(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgChangeDate(string where, string kode, DateTime tglTrans)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, SPCL_PRICE, DISCOUNT, START_DATE, END_DATE, FLAG, CATATAN, ID_PROMO, CREATED_BY, CREATED_DATE, STOCK, STATUS_BRG " +
                    //" from vw_articlePromo {0}", where), Connection))
                    " from tf_stockArticlePromoChangeDate(@kode, @tglTrans) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", kode);
                    command.Parameters.Add("@tglTrans", tglTrans);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(17);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(18);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(19);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.FLAG = reader.IsDBNull(reader.GetOrdinal("FLAG")) ? "" : reader.GetString(21);
                        item.CATATAN = reader.IsDBNull(reader.GetOrdinal("CATATAN")) ? "" : reader.GetString(22);
                        item.ID_PROMO = reader.IsDBNull(reader.GetOrdinal("ID_PROMO")) ? 0 : reader.GetInt64(23);
                        item.CREATED_BY = reader.GetString(24);
                        item.CREATED_DATE = reader.GetDateTime(25);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(26);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(27);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3) + " " + reader.GetString(4);
                        //item.ID_PROMO_SHR = reader.IsDBNull(reader.GetOrdinal("ID_PROMO_SHR")) ? 0 : reader.GetInt64(27);
                        //item.DISC_PROMO_SHR = reader.IsDBNull(reader.GetOrdinal("DISC_PROMO_SHR")) ? 0 : reader.GetInt32(28);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgSIS(string kode, DateTime date, string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    " TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, SPCL_PRICE, DISCOUNT, START_DATE, END_DATE, FLAG, CATATAN, ID_PROMO " +
                    " from tf_stockArticleChangeDate (@kode, @tglTrans) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", kode);
                    command.Parameters.Add("@tglTrans", date);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(17);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(18);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(19);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.FLAG = reader.IsDBNull(reader.GetOrdinal("FLAG")) ? "" : reader.GetString(21);
                        item.CATATAN = reader.IsDBNull(reader.GetOrdinal("CATATAN")) ? "" : reader.GetString(22);
                        item.ID_PROMO = reader.IsDBNull(reader.GetOrdinal("ID_PROMO")) ? 0 : reader.GetInt64(23);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticle(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, CREATED_BY, CREATED_DATE from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);

                        item.CREATED_BY = reader.GetString(17);
                        item.CREATED_DATE = reader.GetDateTime(18);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticle_1000(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 1000 ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, CREATED_BY, CREATED_DATE from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);

                        item.CREATED_BY = reader.GetString(17);
                        item.CREATED_DATE = reader.GetDateTime(18);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticleBRAND(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select FBRAND from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(0);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticlePRODUK(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select FPRODUK from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(0);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticleARTICLE(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select FART_DESC from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(0);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticleCOLOR(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select FCOL_DESC from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(0);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public List<MS_KDBRG> getMsKdbrgArticleSIZE(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select FSIZE_DESC from vw_article {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(0);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public string insertMsKdbrg(MS_KDBRG kdbrg)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_KDBRG (ITEM_CODE, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, TGL_BS, PRICE, COGS, CREATED_BY, CREATED_DATE, FSEASON, DATE_START ) values " +
                    " (@itemCode, @barcode, @artDesc, @color, @size, @produk, @brand, @group, getdate(), @price, @cogs, @createdBy, getdate(), @season, @dateStart); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = kdbrg.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = kdbrg.BARCODE;
                    command.Parameters.Add("@artDesc", SqlDbType.VarChar).Value = kdbrg.FART_DESC;
                    command.Parameters.Add("@color", SqlDbType.VarChar).Value = kdbrg.FCOL_DESC;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = kdbrg.FSIZE_DESC;
                    command.Parameters.Add("@produk", SqlDbType.VarChar).Value = kdbrg.FPRODUK;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = kdbrg.FBRAND;
                    command.Parameters.Add("@group", SqlDbType.VarChar).Value = kdbrg.FGROUP;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = kdbrg.PRICE;
                    command.Parameters.Add("@cogs", SqlDbType.Decimal).Value = kdbrg.COGS;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = kdbrg.CREATED_BY;
                    command.Parameters.Add("@season", SqlDbType.VarChar).Value = kdbrg.FSEASON;
                    command.Parameters.Add("@dateStart", SqlDbType.Date).Value = kdbrg.DATE_START;

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

        public string updateMsKdbrg(MS_KDBRG kdbrg)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_KDBRG set TGL_BS = getdate(), PRICE = @price, COGS = @cogs, UPDATED_BY = @updateBy, UPDATED_DATE = getdate(), " +
                    " FBRAND = @brand, FPRODUK = @produk, FGROUP = @group, FART_DESC = @art, FCOL_DESC = @warna, FSIZE_DESC = @size, DATE_START = @dateStart where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = kdbrg.PRICE;
                    command.Parameters.Add("@cogs", SqlDbType.Decimal).Value = kdbrg.COGS;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = kdbrg.CREATED_BY;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = kdbrg.FBRAND;
                    command.Parameters.Add("@produk", SqlDbType.VarChar).Value = kdbrg.FPRODUK;
                    command.Parameters.Add("@group", SqlDbType.VarChar).Value = kdbrg.FGROUP;
                    command.Parameters.Add("@art", SqlDbType.VarChar).Value = kdbrg.FART_DESC;
                    command.Parameters.Add("@warna", SqlDbType.VarChar).Value = kdbrg.FCOL_DESC;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = kdbrg.FSIZE_DESC;
                    command.Parameters.Add("@dateStart", SqlDbType.Date).Value = kdbrg.DATE_START;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = kdbrg.ID;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = "Error : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public void deleteStruck(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_STRUCK where CREATED_BY = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
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

        public void deleteStagingWholesale(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE MS_STAGING_WHOLESALE where CREATED_BY = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
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

        public void deleteHeaderWholesale(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_WHOLESALE where CREATED_BY = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
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

        public string insertTempStruck(MS_KDBRG kdbrg, MS_SHOWROOM show, string kasir)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_STRUCK (ID_KDBRG, ID_SHOWROOM, BARCODE, ITEM_CODE, ART_DESC, SIZE, BRAND, PRICE, QTY, SA_NAME, CREATED_BY, CREATED_DATE, SPCL_PRICE, DISCOUNT, NET_PRICE, ART_PRICE, JENIS_DISCOUNT, BON_PRICE, RETUR, WARNA, MEMBER, KODE, SHOWROOM ) values " +
                    " (@idKdbrg, @idShow, @barcode, @ItemCode, @ArtDesc, @Size, @Brand, @Price, @qty, @saName, @createdBy, Getdate(), @spclPrice, @discount, @netPrice, @artPrice, @jenisDisc, @bonPrice, @retur, @warna, @member, @kode, @show ) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = kdbrg.ID;
                    command.Parameters.Add("@idShow", SqlDbType.BigInt).Value = show.ID;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = kdbrg.BARCODE;
                    command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = kdbrg.ITEM_CODE;
                    command.Parameters.Add("@ArtDesc", SqlDbType.VarChar).Value = kdbrg.FART_DESC;// +" " + kdbrg.FCOL_DESC;
                    command.Parameters.Add("@Size", SqlDbType.VarChar).Value = kdbrg.FSIZE_DESC;
                    command.Parameters.Add("@Brand", SqlDbType.VarChar).Value = kdbrg.FBRAND;
                    command.Parameters.Add("@Price", SqlDbType.Decimal).Value = kdbrg.PRICE;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = kdbrg.RETUR == "No" ? 1 : -1;
                    command.Parameters.Add("@saName", SqlDbType.VarChar).Value = "";
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = kasir;
                    command.Parameters.Add("@spclPrice", SqlDbType.Decimal).Value = kdbrg.SPCL_PRICE;
                    command.Parameters.Add("@discount", SqlDbType.Int).Value = kdbrg.DISCOUNT;
                    command.Parameters.Add("@netPrice", SqlDbType.Decimal).Value = kdbrg.NET_PRICE;
                    command.Parameters.Add("@artPrice", SqlDbType.Decimal).Value = kdbrg.ART_PRICE;
                    command.Parameters.Add("@jenisDisc", SqlDbType.VarChar).Value = kdbrg.FLAG;
                    command.Parameters.Add("@bonPrice", SqlDbType.Decimal).Value = kdbrg.BON_PRICE;
                    command.Parameters.Add("@retur", SqlDbType.VarChar).Value = kdbrg.RETUR;
                    command.Parameters.Add("@warna", SqlDbType.VarChar).Value = kdbrg.FCOL_DESC;
                    command.Parameters.Add("@member", SqlDbType.VarChar).Value = kdbrg.MEMBER;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = show.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = show.SHOWROOM;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string deleteTempStruck(Int64 id)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("DELETE TEMP_STRUCK where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string updateQty(int qty, Int64 id)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_STRUCK set QTY = @qty where id = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = qty;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string updateTempStruckAcara(TEMP_STRUCK tempStruck)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_STRUCK set ID_ACARA = @idAcara, NET_ACARA = @acara where id = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = tempStruck.ID_ACARA;
                    command.Parameters.Add("@acara", SqlDbType.Decimal).Value = tempStruck.NET_ACARA;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = tempStruck.ID;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string insertHarga(string where)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert into MS_PRICE (ID_KDBRG, ITEM_CODE, PRICE, START_DATE, CREATED_BY, CREATED_DATE) " +
                    "select ID, ITEM_CODE, PRICE, DATE_START, 'system', GETDATE() from MS_KDBRG where ID in ({0})", where);
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string updateHarga(MS_KDBRG kdbrg)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PRICE set END_DATE = @dateStart where ID_KDBRG = @idKdbrg");
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = kdbrg.ID;
                command.Parameters.Add("@dateStart", SqlDbType.Date).Value = kdbrg.DATE_START;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string insertTempAcara(TEMP_ACARA temp)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert into TEMP_ACARA (ID_TEMP, ID_ACARA, ID_KDBRG, ITEM_CODE, VALUE_ACARA, NET_PRICE, SPCL_PRICE, DISC, CREATED_BY, CREATED_DATE, DISC_PRICE) " +
                    "values (@idTemp, @idAcara, @idKDBRG, @itemCode, @value, @netPrice, @spclPrice, @disc, @createdBy, getdate(), @discPrice)");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idTemp", SqlDbType.VarChar).Value = temp.ID_TEMP;
                    command.Parameters.Add("@idAcara", SqlDbType.VarChar).Value = temp.ID_ACARA;
                    command.Parameters.Add("@idKDBRG", SqlDbType.VarChar).Value = temp.ID_KDBRG;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = temp.ITEM_CODE;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = temp.VALUE_ACARA;
                    command.Parameters.Add("@netPrice", SqlDbType.Decimal).Value = temp.NET_PRICE;
                    command.Parameters.Add("@spclPrice", SqlDbType.Decimal).Value = temp.SPCL_PRICE;
                    command.Parameters.Add("@disc", SqlDbType.Decimal).Value = temp.DISC;
                    //command.Parameters.Add("@disc", SqlDbType.Int).Value = temp.DISC;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = temp.CREATED_BY;
                    command.Parameters.Add("@discPrice", SqlDbType.BigInt).Value = temp.DISC_PRICE == null ? (object)Convert.DBNull : temp.DISC_PRICE;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public void deleteTempAcara(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_ACARA where CREATED_BY = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
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

        #region wholeSale
        public List<MS_KDBRG> getMsKdbrgWholesale(DateTime tglTrans, string kode, string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    " TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, CREATED_BY, CREATED_DATE, STOCK " +
                    //" from vw_article {0}", where), Connection))
                    " from tf_stockArtikelCutOff(@date, @kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@date", tglTrans);
                    command.Parameters.Add("@kode", kode);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);

                        item.CREATED_BY = reader.GetString(17);
                        item.CREATED_DATE = reader.GetDateTime(18);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(19);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3) + " " + reader.GetString(4);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        public string insertTempStruckWholeSale(MS_KDBRG kdbrg, MS_SHOWROOM show, string kasir)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_STRUCK (ID_KDBRG, ID_SHOWROOM, BARCODE, ITEM_CODE, ART_DESC, SIZE, BRAND, PRICE, QTY, SA_NAME, CREATED_BY, CREATED_DATE, SPCL_PRICE, DISCOUNT, NET_PRICE, ART_PRICE, JENIS_DISCOUNT, BON_PRICE, RETUR, WARNA, KODE, SHOWROOM ) values " +
                    " (@idKdbrg, @idShow, @barcode, @ItemCode, @ArtDesc, @Size, @Brand, @Price, @qty, @saName, @createdBy, Getdate(), @spclPrice, @discount, @netPrice, @artPrice, @jenisDisc, @bonPrice, @retur, @warna, @kode, @show) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = kdbrg.ID;
                    command.Parameters.Add("@idShow", SqlDbType.BigInt).Value = show.ID;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = kdbrg.BARCODE;
                    command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = kdbrg.ITEM_CODE;
                    command.Parameters.Add("@ArtDesc", SqlDbType.VarChar).Value = kdbrg.FART_DESC + " " + kdbrg.FCOL_DESC;
                    command.Parameters.Add("@Size", SqlDbType.VarChar).Value = kdbrg.FSIZE_DESC;
                    command.Parameters.Add("@Brand", SqlDbType.VarChar).Value = kdbrg.FBRAND;
                    command.Parameters.Add("@Price", SqlDbType.Decimal).Value = kdbrg.PRICE;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = kdbrg.RETUR == "No" ? 1 : -1;
                    command.Parameters.Add("@saName", SqlDbType.VarChar).Value = "";
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = kasir;
                    command.Parameters.Add("@spclPrice", SqlDbType.Decimal).Value = kdbrg.SPCL_PRICE;
                    command.Parameters.Add("@discount", SqlDbType.Int).Value = kdbrg.DISCOUNT;
                    command.Parameters.Add("@netPrice", SqlDbType.Decimal).Value = kdbrg.NET_PRICE;
                    command.Parameters.Add("@artPrice", SqlDbType.Decimal).Value = kdbrg.ART_PRICE;
                    command.Parameters.Add("@jenisDisc", SqlDbType.VarChar).Value = kdbrg.FLAG == null ? "" : kdbrg.FLAG;
                    command.Parameters.Add("@bonPrice", SqlDbType.Decimal).Value = kdbrg.BON_PRICE;
                    command.Parameters.Add("@retur", SqlDbType.VarChar).Value = kdbrg.RETUR;
                    command.Parameters.Add("@warna", SqlDbType.VarChar).Value = kdbrg.FCOL_DESC;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = show.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = show.SHOWROOM;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string insertTempHeaderWholesale(TEMP_WHOLESALE temp)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_WHOLESALE (TGL_PENJUALAN, TGL_PENGIRIMAN, NAMA_PEMBELI, KODE_PEMBELI, MARGIN, CREATED_BY, CREATED_DATE) values " +
                    " (@tglSale, @tglKirim, @nama, @kode, @margin, @createdBy, GETDATE()) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglSale", SqlDbType.Date).Value = temp.TGL_PENJUALAN;
                    command.Parameters.Add("@tglKirim", SqlDbType.Date).Value = temp.TGL_PENGIRIMAN;
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = temp.NAMA_PEMBELI;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = temp.KODE_PEMBELI;
                    command.Parameters.Add("@margin", SqlDbType.Int).Value = temp.MARGIN;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = temp.CREATED_BY;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string updateTempHeaderWholesale(TEMP_WHOLESALE temp)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_WHOLESALE set TGL_PENJUALAN = @tglSale, TGL_PENGIRIMAN = @tglKirim, NAMA_PEMBELI = @nama, " +
                    " KODE_PEMBELI = @kode, MARGIN = @margin, CREATED_DATE = GETDATE() where CREATED_BY = @createdBy ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglSale", SqlDbType.Date).Value = temp.TGL_PENJUALAN;
                    command.Parameters.Add("@tglKirim", SqlDbType.Date).Value = temp.TGL_PENGIRIMAN;
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = temp.NAMA_PEMBELI;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = temp.KODE_PEMBELI;
                    command.Parameters.Add("@margin", SqlDbType.Int).Value = temp.MARGIN;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = temp.CREATED_BY;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string updateQtyAndPrice(int qty, int price, Int64 id)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_STRUCK set QTY = @qty, PRICE = @price, BON_PRICE = @price, NET_PRICE = @price where id = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = qty;
                    command.Parameters.Add("@price", SqlDbType.Int).Value = price;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string insertMSStagingWholesale(MS_STAGING_WHOLESALE wholesale)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_STAGING_WHOLESALE ( BARCODE, QTY, CREATED_BY, CREATED_DATE, STATUS, ID_KDBRG , NET_PRICE) values " +
                    " (@barcode, @qty, @createdBy, getdate(), 1, @idKdbrg, @netprice) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = wholesale.BARCODE;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = wholesale.QTY;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = wholesale.CREATED_BY;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = wholesale.ID_KDBRG;
                    command.Parameters.Add("@netprice", SqlDbType.Decimal).Value = wholesale.NET_PRICE; //Tambah Save Net_Price --> kalau tidak di save jadi nilai null dan menyebabkan Error
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

        public string insertFromStagingWholesale(string uName)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertFromStagingWholesale", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@nama", uName);
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
        public string insertMsKdbrgWithSP(MS_KDBRG MsKdbrg)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("USP_Insert_MS_KDBRG", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ITEM_CODE", MsKdbrg.ITEM_CODE);
                    command.Parameters.Add("@BARCODE", MsKdbrg.BARCODE);
                    command.Parameters.Add("@FBRAND", MsKdbrg.FBRAND);
                    command.Parameters.Add("@FART_DESC", MsKdbrg.FART_DESC);
                    command.Parameters.Add("@FCOL_DESC", MsKdbrg.FCOL_DESC);
                    command.Parameters.Add("@FSIZE_DESC", MsKdbrg.FSIZE_DESC);
                    command.Parameters.Add("@FPRODUK", MsKdbrg.FPRODUK);
                    command.Parameters.Add("@COGS", MsKdbrg.COGS);
                    command.Parameters.Add("@PRICE", MsKdbrg.PRICE);
                    command.Parameters.Add("@FGROUP", MsKdbrg.FGROUP);
                    command.Parameters.Add("@FSEASON", MsKdbrg.FSEASON);
                    command.Parameters.Add("@DATE_START", MsKdbrg.DATE_START);
                    command.Parameters.Add("@CREATED_BY", MsKdbrg.CREATED_BY);
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
        public string UpdateMsKdbrgWithSP(MS_KDBRG MsKdbrg)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("Usp_Update_MSKdbrg", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PRICE", MsKdbrg.PRICE);
                    command.Parameters.Add("@COGS", MsKdbrg.COGS);
                    command.Parameters.Add("@Update_BY", MsKdbrg.UPDATED_BY);
                    command.Parameters.Add("@FBRAND", MsKdbrg.FBRAND);
                    command.Parameters.Add("@FPRODUK", MsKdbrg.FPRODUK);
                    command.Parameters.Add("@FGROUP", MsKdbrg.FGROUP);
                    command.Parameters.Add("@FART_DESC", MsKdbrg.FART_DESC);
                    command.Parameters.Add("@FCOL_DESC", MsKdbrg.FCOL_DESC);
                    command.Parameters.Add("@FSIZE_DESC", MsKdbrg.FSIZE_DESC);
                    command.Parameters.Add("@DATE_START", MsKdbrg.DATE_START);
                    command.Parameters.Add("@IdKdbrg", MsKdbrg.ID);
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

        #endregion

        #region "PriceChange"
        public MS_KDBRG GetInfoKdbrg(string where)
        {
            MS_KDBRG item = new MS_KDBRG();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, BARCODE from MS_KDBRG {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {

                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.BARCODE = reader.GetString(6);
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
        public List<MS_PRICE> getPriceInfo(string where)
        {
            List<MS_PRICE> listprice = new List<MS_PRICE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ITEM_CODE, PRICE, START_DATE, END_DATE, " +
                    " CASE WHEN END_DATE IS NULL THEN 'Harga Terakhir' else '' END AS Info from MS_PRICE {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PRICE item = new MS_PRICE();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.GetInt64(1);
                        item.ITEM_CODE = reader.GetString(2);
                        item.PRICE = reader.GetDecimal(3);
                        item.START_DATE = reader.GetDateTime(4);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? Convert.ToDateTime("9999-12-31") : reader.GetDateTime(5);
                        item.Info = reader.GetString(6);
                        listprice.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listprice;
        }
        public string insertHargaChangePrice(MS_PRICE item, string dtprice)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Insert into MS_PRICE (ID_KDBRG, ITEM_CODE, PRICE, START_DATE, CREATED_BY, CREATED_DATE) " +
                //    " VALUES (@idKdbrg, @itemCode, @PRICE, @dateStart, @Createdby, GETDATE() )");
                string query = String.Format("Insert into MS_PRICE (ID_KDBRG, ITEM_CODE, PRICE, START_DATE, CREATED_BY, CREATED_DATE) " +
       " VALUES (@idKdbrg, @itemCode, @PRICE, '{0}', @Createdby, GETDATE() )", dtprice);
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = item.ID_KDBRG;
                command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = item.ITEM_CODE;
                command.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = item.PRICE;
                command.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = item.CREATED_BY;
                command.Parameters.Add("@dateStart", SqlDbType.Date).Value = item.START_DATE;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }
        public string updateHargaChangePrice(MS_PRICE kdbrg, string dtprice)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Update MS_PRICE set END_DATE = @dateStart where ID_KDBRG = @idKdbrg AND ID = (SELECT MAX(ID) FROM MS_PRICE where ID_KDBRG = @idKdbrg )");
                string query = String.Format("Update MS_PRICE set END_DATE = '{0}' where ID_KDBRG = @idKdbrg " +
                 "AND ID = (SELECT MAX(ID) FROM MS_PRICE where ID_KDBRG = @idKdbrg )", dtprice); 
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = kdbrg.ID_KDBRG;
                command.Parameters.Add("@dateStart", SqlDbType.Date).Value = kdbrg.START_DATE;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }

        public string EditHargaChangePrice(MS_PRICE prc)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PRICE set PRICE = @price where ID = @id");
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.Parameters.Add("@id", SqlDbType.BigInt).Value = prc.ID;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = prc.PRICE;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }
        #endregion

        #region GWP
        public string updateTempStruckAcaraGWP(TEMP_STRUCK_GWP_PWP tempStruck)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_STRUCK set ID_ACARA = @idAcara, NET_ACARA = @acara where id = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = tempStruck.ID_ACARA;
                    command.Parameters.Add("@acara", SqlDbType.Decimal).Value = tempStruck.NET_ACARA;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = tempStruck.ID;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }
        #endregion

        #region "MS_KDBRG JENAHARA"
        public string updateBlockStatMS_KDBRG(Int64 IdKdbrg)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_KDBRG set BLOCK_STAT = 1 , BLOCK_DT = GETDATE() where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = IdKdbrg;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return ret;
        }
        public List<MS_KDBRG> getMsKdbrgArticleJenahara(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, PRICE, KLMPK, GENDER, BARCODE, COGS, CREATED_BY, CREATED_DATE, STAT_KDBRG from vw_articleJENAHARA {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = reader.GetDecimal(12);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);

                        item.CREATED_BY = reader.GetString(17);
                        item.CREATED_DATE = reader.GetDateTime(18);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3);
                        item.STAT_KDBRG = reader.GetString(19);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }
        public string insertMsKdbrgJenahara(MS_KDBRG kdbrg)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_KDBRG (ITEM_CODE, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, TGL_BS, PRICE, COGS, CREATED_BY, CREATED_DATE, FSEASON, DATE_START ) values " +
                    " (@itemCode, @barcode, @artDesc, @color, @size, @produk, @brand, @group, @tglBs, @price, @cogs, @createdBy, getdate(), @season, @dateStart); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = kdbrg.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = kdbrg.BARCODE;
                    command.Parameters.Add("@artDesc", SqlDbType.VarChar).Value = kdbrg.FART_DESC;
                    command.Parameters.Add("@color", SqlDbType.VarChar).Value = kdbrg.FCOL_DESC;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = kdbrg.FSIZE_DESC;
                    command.Parameters.Add("@produk", SqlDbType.VarChar).Value = kdbrg.FPRODUK;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = kdbrg.FBRAND;
                    command.Parameters.Add("@group", SqlDbType.VarChar).Value = kdbrg.FGROUP;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = kdbrg.PRICE;
                    command.Parameters.Add("@cogs", SqlDbType.Decimal).Value = kdbrg.COGS;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = kdbrg.CREATED_BY;
                    command.Parameters.Add("@season", SqlDbType.VarChar).Value = kdbrg.FSEASON;
                    command.Parameters.Add("@dateStart", SqlDbType.Date).Value = kdbrg.DATE_START;
                    command.Parameters.Add("@tglBs", SqlDbType.Date).Value = Convert.ToDateTime("1900-01-01");
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
        public List<MS_KDBRG> getMsKdbrgJENAHARA(string where)
        {
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, " +
                    "TGL_BS, MILIK, 0 AS PRICE, KLMPK, GENDER, BARCODE, COGS, CREATED_BY, CREATED_DATE from MS_KDBRG {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KDBRG item = new MS_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.FART_DESC = reader.GetString(2);
                        item.FCOL_DESC = reader.GetString(3);
                        item.FSIZE_DESC = reader.GetString(4);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(5);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(6);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(7);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(8);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(9);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(11);
                        item.PRICE = 0;
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(13);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(14);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);

                        item.CREATED_BY = reader.GetString(17);
                        item.CREATED_DATE = reader.GetDateTime(18);
                        item.ART_DESC = reader.GetString(2) + " " + reader.GetString(3);

                        listKdBrg.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listKdBrg;
        }

        #endregion
    }
}