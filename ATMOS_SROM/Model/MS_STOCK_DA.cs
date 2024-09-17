using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using ATMOS_SROM.Domain.Laporan;

namespace ATMOS_SROM.Model
{
    public class MS_STOCK_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_STOCK> getStock(string where)
        {
            List<MS_STOCK> listStock = new List<MS_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ITEM_CODE, BARCODE, WAREHOUSE, KODE, STOCK, RAK, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, " +
                    " ART_DESC, PRODUK, FGROUP, WARNA, SIZE, BRAND" +
                    " from vw_stock {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 3600;
                    Connection.Open();
                    //Int64 i = 0;
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_STOCK item = new MS_STOCK();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(1);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(2);
                        item.BARCODE = reader.GetString(3);
                        item.WAREHOUSE = reader.GetString(4);
                        item.KODE = reader.GetString(5);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(6);
                        item.RAK = reader.IsDBNull(reader.GetOrdinal("RAK")) ? "" : reader.GetString(7);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(8);
                        item.CREATED_DATE = reader.GetDateTime(9);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(10);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(12);
                        item.PRODUK = reader.IsDBNull(reader.GetOrdinal("PRODUK")) ? "" : reader.GetString(13);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(14);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(15);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(16);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(17);
                        
                        listStock.Add(item);
                        //i = reader.GetInt64(0);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<MS_STOCK> getStock_1000(string where)
        {
            List<MS_STOCK> listStock = new List<MS_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select top 1000 ID, ID_KDBRG, ITEM_CODE, BARCODE, WAREHOUSE, KODE, STOCK, RAK, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, " +
                    " ART_DESC, PRODUK, FGROUP, WARNA, SIZE, BRAND" +
                    " from vw_stock {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 3600;
                    Connection.Open();
                    //Int64 i = 0;
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_STOCK item = new MS_STOCK();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(1);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(2);
                        item.BARCODE = reader.GetString(3);
                        item.WAREHOUSE = reader.GetString(4);
                        item.KODE = reader.GetString(5);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(6);
                        item.RAK = reader.IsDBNull(reader.GetOrdinal("RAK")) ? "" : reader.GetString(7);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(8);
                        item.CREATED_DATE = reader.GetDateTime(9);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(10);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(12);
                        item.PRODUK = reader.IsDBNull(reader.GetOrdinal("PRODUK")) ? "" : reader.GetString(13);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(14);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(15);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(16);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(17);

                        listStock.Add(item);
                        //i = reader.GetInt64(0);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<MS_STOCK> getStockCutOff(string where, DateTime dtCut, string kode)
        {
            List<MS_STOCK> listStock = new List<MS_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ITEM_CODE, WAREHOUSE, STOCK, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, BARCODE, KODE, " +
                    " BRAND, ART_DESC, WARNA, SIZE" +
                    " from tf_stockCutOff(@dateCutOff, @kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dateCutOff", dtCut));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_STOCK item = new MS_STOCK();
                        item.ID = reader.GetInt64(0);
                        item.ITEM_CODE = reader.GetString(1);
                        item.WAREHOUSE = reader.GetString(2);
                        item.STOCK = reader.GetInt32(3);
                        item.CREATED_BY = reader.GetString(4);
                        item.CREATED_DATE = reader.GetDateTime(5);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(6);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(8);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(9);

                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(10);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(11);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(12);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(13);
                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<TRF_STOCK_DETAIL> getTrfStock(string where)
        {
            List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, NO_BUKTI, ITEM_CODE, DARI, KE, WAKTU_KIRIM, WAKTU_TERIMA, " +
                    "QTY_KIRIM, QTY_TERIMA, REFF, USER_KIRIM, USER_TERIMA, STATUS, CREATED_BY, CREATED_DATE, ID_KDBRG, ALASAN, BARCODE, FBRAND, FART_DESC, FCOL_DESC, FSIZE_DESC " +
                    " from vw_transferStock {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TRF_STOCK_DETAIL item = new TRF_STOCK_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.GetInt64(1);
                        item.NO_BUKTI = reader.GetString(2);
                        item.ITEM_CODE = reader.GetString(3);
                        item.DARI = reader.GetString(4);
                        item.KE = reader.GetString(5);
                        item.WAKTU_KIRIM = reader.IsDBNull(reader.GetOrdinal("WAKTU_KIRIM")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.WAKTU_TERIMA = reader.IsDBNull(reader.GetOrdinal("WAKTU_TERIMA")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.QTY_KIRIM = reader.IsDBNull(reader.GetOrdinal("QTY_KIRIM")) ? 0 : reader.GetInt32(8);
                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(9);
                        item.REFF = reader.GetString(10);
                        item.USER_KIRIM = reader.IsDBNull(reader.GetOrdinal("USER_KIRIM")) ? "" : reader.GetString(11);
                        item.USER_TERIMA = reader.IsDBNull(reader.GetOrdinal("USER_TERIMA")) ? "" : reader.GetString(12);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(13);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(14);
                        item.CREATED_DATE = reader.GetDateTime(15);
                        item.ID_KDBRG = reader.GetInt64(16);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(17);
                        item.BARCODE = reader.GetString(18);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(19);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(20);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(21);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(22);

                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<TRF_STOCK_DETAIL> getDetailTrfStock(string where)
        {
            List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select  ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, " +
                    " STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA, ALASAN, " +
                    " REFF, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, GENDER " +
                    " from vw_detailTrfStock {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TRF_STOCK_DETAIL item = new TRF_STOCK_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.NO_BUKTI = reader.GetString(3);
                        item.ITEM_CODE = reader.GetString(4);
                        item.QTY_KIRIM = reader.GetInt32(5);
                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(6);
                        item.STOCK_AKHIR_KIRIM = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_KIRIM")) ? 0 : reader.GetInt32(7);
                        item.STOCK_AKHIR_TERIMA = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_TERIMA")) ? 0 : reader.GetInt32(8);
                        item.USER_KIRIM = reader.IsDBNull(reader.GetOrdinal("USER_KIRIM")) ? "" : reader.GetString(9);
                        item.USER_TERIMA = reader.IsDBNull(reader.GetOrdinal("USER_TERIMA")) ? "" : reader.GetString(10);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(11);
                        item.REFF = reader.IsDBNull(reader.GetOrdinal("REFF")) ? "" : reader.GetString(12);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(13);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(14);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(15);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(16);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(17);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(14);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(15);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(16);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(17);

                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<TRF_STOCK_HEADER> getTrfStockHeader(string where)
        {
            List<TRF_STOCK_HEADER> listStock = new List<TRF_STOCK_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_BUKTI, DARI, KE, WAKTU_KIRIM, WAKTU_TERIMA, STATUS, CREATED_BY, CREATED_DATE, " +
                    " UPDATED_BY, UPDATED_DATE, STATUS_TRF, DONE_TIME, KODE_DARI, KODE_KE " +
                    " from TRF_STOCK_HEADER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TRF_STOCK_HEADER item = new TRF_STOCK_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.NO_BUKTI = reader.GetString(1);
                        item.DARI = reader.GetString(2);
                        item.KE = reader.GetString(3);
                        item.WAKTU_KIRIM = reader.IsDBNull(reader.GetOrdinal("WAKTU_KIRIM")) ? (DateTime?)null : reader.GetDateTime(4);
                        item.WAKTU_TERIMA = reader.IsDBNull(reader.GetOrdinal("WAKTU_TERIMA")) ? (DateTime?)null : reader.GetDateTime(5);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(6);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(7);
                        item.CREATED_DATE = reader.GetDateTime(8);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(9);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.STATUS_TRF = reader.GetBoolean(11);
                        item.DONE_TIME = reader.IsDBNull(reader.GetOrdinal("DONE_TIME")) ? (DateTime?)null : reader.GetDateTime(12);
                        item.KODE_DARI = reader.IsDBNull(reader.GetOrdinal("KODE_DARI")) ? "" : reader.GetString(13);
                        item.KODE_KE = reader.IsDBNull(reader.GetOrdinal("KODE_KE")) ? "" : reader.GetString(14);

                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<MS_KARTU_STOCK_HEADER> getKartuStockHeader(DateTime startDate, DateTime cutOff, DateTime endDate, string where)
        {
            List<MS_KARTU_STOCK_HEADER> listStock = new List<MS_KARTU_STOCK_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select KODE, SHOWROOM, SALDO_AWAL, SALE, BELI, TERIMA, KIRIM, ADJUSTMENT, SALDO_AKHIR " +
                    " from tf_movementStockHeader(@dateStart, @dateCutOff, @dateEnd) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dateStart", startDate));
                    command.Parameters.Add(new SqlParameter("@dateCutOff", cutOff));
                    command.Parameters.Add(new SqlParameter("@dateEnd", endDate));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KARTU_STOCK_HEADER item = new MS_KARTU_STOCK_HEADER();
                        item.KODE = reader.GetString(0);
                        item.SHOWROOM = reader.GetString(1);
                        item.SALDO_AWAL = reader.IsDBNull(reader.GetOrdinal("SALDO_AWAL")) ? 0 : reader.GetInt32(2);
                        item.SALE = reader.IsDBNull(reader.GetOrdinal("SALE")) ? 0 : reader.GetInt32(3);
                        item.BELI = reader.IsDBNull(reader.GetOrdinal("BELI")) ? 0 : reader.GetInt32(4);
                        item.TERIMA = reader.IsDBNull(reader.GetOrdinal("TERIMA")) ? 0 : reader.GetInt32(5);
                        item.KIRIM = reader.IsDBNull(reader.GetOrdinal("KIRIM")) ? 0 : reader.GetInt32(6);
                        item.ADJUSTMENT = reader.IsDBNull(reader.GetOrdinal("ADJUSTMENT")) ? 0 : reader.GetInt32(7);
                        item.SALDO_AKHIR = reader.IsDBNull(reader.GetOrdinal("SALDO_AKHIR")) ? 0 : reader.GetInt32(8);
                        
                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<MS_KARTU_STOCK_HEADER> getKartuStockHeaderSldAwal(string where)
        {
            List<MS_KARTU_STOCK_HEADER> listStock = new List<MS_KARTU_STOCK_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select KODE, SHOWROOM, SUM(SALDO_AWAL) [SALDO_AWAL], SUM(PENJUALAN) [SALE], SUM(PEMBELIAN) [BELI], " +
                    " SUM(TERIMA_BARANG) [TERIMA], SUM(KELUAR_BARANG) [KIRIM], SUM(ADJUSTMENT) [ADJUSTMENT], SUM(SALDO_AKHIR) [SALDO_AKHIR]" +
                    " from SLD_AWAL {0} group by KODE, SHOWROOM", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_KARTU_STOCK_HEADER item = new MS_KARTU_STOCK_HEADER();
                        item.KODE = reader.GetString(0);
                        item.SHOWROOM = reader.GetString(1);
                        item.SALDO_AWAL = reader.IsDBNull(reader.GetOrdinal("SALDO_AWAL")) ? 0 : reader.GetInt32(2);
                        item.SALE = reader.IsDBNull(reader.GetOrdinal("SALE")) ? 0 : reader.GetInt32(3);
                        item.BELI = reader.IsDBNull(reader.GetOrdinal("BELI")) ? 0 : reader.GetInt32(4);
                        item.TERIMA = reader.IsDBNull(reader.GetOrdinal("TERIMA")) ? 0 : reader.GetInt32(5);
                        item.KIRIM = reader.IsDBNull(reader.GetOrdinal("KIRIM")) ? 0 : reader.GetInt32(6);
                        item.ADJUSTMENT = reader.IsDBNull(reader.GetOrdinal("ADJUSTMENT")) ? 0 : reader.GetInt32(7);
                        item.SALDO_AKHIR = reader.IsDBNull(reader.GetOrdinal("SALDO_AKHIR")) ? 0 : reader.GetInt32(8);

                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<LAP_SLD_STOCK> getKartuStockDetail(string where)
        {
            List<LAP_SLD_STOCK> listStock = new List<LAP_SLD_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select ID, ID_KDBRG, ID_SHOWROOM, SHOWROOM, KODE, STATUS_SHOWROOM, BARCODE, SALDO_AWAL, " +
                    " PENJUALAN, PEMBELIAN, TERIMA_BARANG, KELUAR_BARANG, ADJUSTMENT, SALDO_AKHIR, TGL_CUT_OFF, CREATED_BY, CREATED_DATE, FBULAN, STATUS, ITEM_CODE, " +
                    " FBRAND, FART_DESC, FCOL_DESC, FSIZE_DESC, FPRODUK, FSEASON " +
                    " from vw_sldStock {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        LAP_SLD_STOCK item = new LAP_SLD_STOCK();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.GetInt64(1);
                        item.ID_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("ID_SHOWROOM")) ? 0 : reader.GetInt64(2);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(3);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(4);
                        item.STATUS_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("STATUS_SHOWROOM")) ? "" : reader.GetString(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);
                        item.SALDO_AWAL = reader.IsDBNull(reader.GetOrdinal("SALDO_AWAL")) ? 0 : reader.GetInt32(7);
                        item.PENJUALAN = reader.IsDBNull(reader.GetOrdinal("PENJUALAN")) ? 0 : reader.GetInt32(8);

                        item.PEMBELIAN = reader.IsDBNull(reader.GetOrdinal("PEMBELIAN")) ? 0 : reader.GetInt32(9);
                        item.TERIMA_BARANG = reader.IsDBNull(reader.GetOrdinal("TERIMA_BARANG")) ? 0 : reader.GetInt32(10);
                        item.KELUAR_BARANG = reader.IsDBNull(reader.GetOrdinal("KELUAR_BARANG")) ? 0 : reader.GetInt32(11);
                        item.ADJUSTMENT = reader.IsDBNull(reader.GetOrdinal("ADJUSTMENT")) ? 0 : reader.GetInt32(12);
                        item.SALDO_AKHIR = reader.IsDBNull(reader.GetOrdinal("SALDO_AKHIR")) ? 0 : reader.GetInt32(13);
                        item.TGL_CUT_OFF = reader.IsDBNull(reader.GetOrdinal("TGL_CUT_OFF")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(15);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(16);
                        item.FBULAN = reader.IsDBNull(reader.GetOrdinal("FBULAN")) ? "" : reader.GetString(17);

                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(18);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(19);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(20);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(21);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(22);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(23);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(24);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(25);
                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<string> getTotalStock(string Kode)
        {
            string newId = "";
            List<string> stockList = new List<string>();
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand(string.Format("Select SUM(STOCK) [Total_Qty] from MS_STOCK where KODE = @kode group by KODE"), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@kode", Kode));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        newId = reader.GetInt32(0).ToString();

                        stockList.Add(newId);
                    }
                    reader.Close();
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
            return stockList;
        }

        public string insertTrfStock(TRF_STOCK_DETAIL stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_STOCK_DETAIL (ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, STOCK_AKHIR_KIRIM, USER_KIRIM, REFF, BARCODE) values " +
                    " (@idHeader, @idKdbrg, @noBukti, @itemCode, @qty, @stock, @user, @reff, @barcode) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idHeader", SqlDbType.BigInt).Value = stock.ID_HEADER;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = stock.ID_KDBRG;
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = stock.NO_BUKTI;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = stock.ITEM_CODE;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = stock.USER_KIRIM;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = stock.QTY_KIRIM;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = stock.STOCK_AKHIR_KIRIM;
                    command.Parameters.Add("@reff", SqlDbType.VarChar).Value = stock.REFF;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = stock.BARCODE;

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

        public string insertDataStock(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_STOCK (ITEM_CODE, BARCODE, WAREHOUSE, KODE, STOCK, RAK, CREATED_BY, CREATED_DATE) values " +
                    " (@itemCode, @barcode, @wareHouse, @kode, @stock, @rak, @createdBy, GETDATE())");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = stock.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = stock.BARCODE;
                    command.Parameters.Add("@wareHouse", SqlDbType.VarChar).Value = stock.WAREHOUSE;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = stock.KODE;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = stock.STOCK;
                    command.Parameters.Add("@rak", SqlDbType.VarChar).Value = stock.RAK;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = stock.CREATED_BY;

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

        public string updateFirstTrfHeader(TRF_STOCK_HEADER header)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_HEADER set WAKTU_TERIMA = @waktuTerima, STATUS = @status, UPDATED_BY = @updateBy, UPDATED_DATE = getdate() " +
                    " where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = header.STATUS;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = header.UPDATED_BY;
                    command.Parameters.Add("@waktuTerima", SqlDbType.DateTime2).Value = header.WAKTU_TERIMA;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = header.ID;

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

        public string updateDoneTrfHeader(TRF_STOCK_HEADER header)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_HEADER set DONE_TIME = getdate(), STATUS = @status, UPDATED_BY = @updateBy, UPDATED_DATE = getdate() " +
                    " where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = header.STATUS;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = header.UPDATED_BY;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = header.ID;

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

        public string updateKirimTrfHeader(TRF_STOCK_HEADER header)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_HEADER set WAKTU_KIRIM = @tglKirim, STATUS = @status, STATUS_TRF = 1 " +
                    " where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = header.STATUS;
                    command.Parameters.Add("@tglKirim", SqlDbType.DateTime2).Value = header.WAKTU_KIRIM;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = header.ID;

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

        public string updateDataStockWithID(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ITEM_CODE = @itemCode and KODE = @kode", stock.STOCK);
                string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id", stock.STOCK);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = stock.UPDATED_BY;

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

        public string updateDataStockWithOutID(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where BARCODE = @barcode and KODE = @kode", stock.STOCK);
                //string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id", stock.STOCK);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = stock.BARCODE;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = stock.KODE;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = stock.UPDATED_BY;

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

        public string updateDataStocAkhirkWithID(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ITEM_CODE = @itemCode and KODE = @kode", stock.STOCK);
                string query = String.Format("Update MS_STOCK set STOCK = @stock, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = stock.STOCK;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = stock.UPDATED_BY;

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

        public string updateDataDiffkWithID(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ITEM_CODE = @itemCode and KODE = @kode", stock.STOCK);
                string query = String.Format("Update MS_STOCK set STOCK = STOCK + @stock, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = stock.STOCK;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = stock.UPDATED_BY;

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

        public string updateTerimaTRFStock(TRF_STOCK_DETAIL stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_DETAIL set QTY_TERIMA = @qty, USER_TERIMA = @user, REFF = @reff, ALASAN = @alasan where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = stock.QTY_TERIMA;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = stock.USER_TERIMA;
                    command.Parameters.Add("@reff", SqlDbType.VarChar).Value = stock.REFF;
                    command.Parameters.Add("@alasan", SqlDbType.VarChar).Value = stock.ALASAN;

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

        public string updateKirimTRFStock(TRF_STOCK_DETAIL stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_DETAIL set QTY_KIRIM = @qty where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = stock.QTY_KIRIM;

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

        public string deleteTRFStock(Int64 id)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE FROM TRF_STOCK_DETAIL where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;

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

        public string cancelDelivery(string noBukti, string kode, string createdBy )
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {//@tglMulai date, @tglCutOff date, @fBulan varchar(5), @dateStart date, @dateEnd date, @createdBy varchar(50)
                using (SqlCommand command = new SqlCommand("usp_cancelDelivery", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noBukti", noBukti));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
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

        public string receiveAllTransferStock(string noBukti, DateTime waktuTerima, string createdBy)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_receiveAllTransferStock", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noBukti", noBukti));
                    command.Parameters.Add(new SqlParameter("@waktuTerima", waktuTerima));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
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

        public string insertVoidWholesale(string noBon)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertVoidWholesale", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noBon", noBon));
                    command.CommandTimeout = 300;
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

        public string updateStockVoidWholesale(string noBon)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_updateStockVoidWholesale", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noBon", noBon));
                    command.CommandTimeout = 300;
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

        #region stockOpname
        public string insertHeaderOpnameRetID(TRF_STOCK_HEADER stockHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_STOCK_HEADER ( DARI, KE, KODE_DARI, KODE_KE, WAKTU_KIRIM, WAKTU_TERIMA, STATUS_TRF, CREATED_BY, CREATED_DATE ) values " +
                    " ( @dari, @ke, @kodeDari, @kodeKe, @waktuKirim, @waktuTerima, 1, @created_by, getdate() ); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@dari", SqlDbType.VarChar).Value = stockHeader.DARI;
                    command.Parameters.Add("@ke", SqlDbType.VarChar).Value = stockHeader.KE;
                    command.Parameters.Add("@kodeDari", SqlDbType.VarChar).Value = stockHeader.KODE_DARI;
                    command.Parameters.Add("@kodeKe", SqlDbType.VarChar).Value = stockHeader.KODE_KE;
                    command.Parameters.Add("@waktuKirim", SqlDbType.DateTime2).Value = stockHeader.WAKTU_KIRIM;
                    command.Parameters.Add("@waktuTerima", SqlDbType.DateTime2).Value = stockHeader.WAKTU_TERIMA;
                    command.Parameters.Add("@created_by", SqlDbType.VarChar).Value = stockHeader.CREATED_BY;

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

        public string updateDataStockOpname(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id", stock.STOCK);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = stock.ID;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = stock.UPDATED_BY;

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

        public string insertTrfStockOpname(TRF_STOCK_DETAIL stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_STOCK_DETAIL (ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, " +
                    " STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA, ALASAN, REFF) values " +
                    " (@idHeader, @idKdbrg, @noBukti, @itemCode, @qtyKirim, @qtyTerima, @stockKirim, @stockTerima, @userKirim, @userTerima, @alasan, @reff) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idHeader", SqlDbType.BigInt).Value = stock.ID_HEADER;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = stock.ID_KDBRG;
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = stock.NO_BUKTI;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = stock.ITEM_CODE;
                    command.Parameters.Add("@qtyKirim", SqlDbType.Int).Value = stock.QTY_KIRIM;
                    command.Parameters.Add("@qtyTerima", SqlDbType.Int).Value = stock.QTY_TERIMA;
                    command.Parameters.Add("@stockKirim", SqlDbType.Int).Value = stock.STOCK_AKHIR_KIRIM;
                    command.Parameters.Add("@stockTerima", SqlDbType.Int).Value = stock.STOCK_AKHIR_TERIMA;
                    command.Parameters.Add("@userKirim", SqlDbType.VarChar).Value = stock.USER_KIRIM;
                    command.Parameters.Add("@userTerima", SqlDbType.VarChar).Value = stock.USER_TERIMA;
                    command.Parameters.Add("@alasan", SqlDbType.VarChar).Value = stock.ALASAN;
                    command.Parameters.Add("@reff", SqlDbType.VarChar).Value = stock.REFF;

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
        #endregion

        #region Download
        public List<EXCEL_DOWNLOAD_STOCK> getDownloadStockCutOff(string where, DateTime dtCut, string kode)
        {
            List<EXCEL_DOWNLOAD_STOCK> listStock = new List<EXCEL_DOWNLOAD_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select KODE, WAREHOUSE, BARCODE, ITEM_CODE, BRAND, ART_DESC, WARNA, SIZE, STOCK " +
                    " from tf_stockCutOff(@dateCutOff, @kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dateCutOff", dtCut));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        EXCEL_DOWNLOAD_STOCK item = new EXCEL_DOWNLOAD_STOCK();
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(0);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("WAREHOUSE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(3);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(4);
                        item.ARTICLE = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(5);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(6);
                        item.UKURAN = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(7);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(8);
                        
                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        public List<EXCEL_DOWNLOAD_STOCK> getDownloadStockCutOffWithPrice(string where, DateTime dtCut, string kode, DateTime DateGetData)
        {
            List<EXCEL_DOWNLOAD_STOCK> listStock = new List<EXCEL_DOWNLOAD_STOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select KODE, WAREHOUSE, BARCODE, ITEM_CODE, BRAND, ART_DESC, WARNA, SIZE, STOCK, PRICE, TANGGAL_GENERATE " +
                    " from tf_stockCutOff_WithPrice(@dateCutOff, @kode, @DateGetdata) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dateCutOff", dtCut));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    command.Parameters.Add(new SqlParameter("@DateGetdata", DateGetData));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        EXCEL_DOWNLOAD_STOCK item = new EXCEL_DOWNLOAD_STOCK();
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(0);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("WAREHOUSE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(3);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(4);
                        item.ARTICLE = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(5);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(6);
                        item.UKURAN = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(7);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(8);
                        item.Price = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(9);
                        item.TANGGAL_GENERATE = reader.GetDateTime(10);
                        listStock.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listStock;
        }

        #endregion
    }
}