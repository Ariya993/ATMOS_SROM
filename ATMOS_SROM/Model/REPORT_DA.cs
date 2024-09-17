using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using ATMOS_SROM.Domain.Report;

namespace ATMOS_SROM.Model
{
    public class REPORT_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<USP_RPTPENJUALAN> getRptPenjualan(DateTime start, DateTime end)
        {
            List<USP_RPTPENJUALAN> listReport = new List<USP_RPTPENJUALAN>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_rptPenjualan", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@dateAwal", start));
                    command.Parameters.Add(new SqlParameter("@dateAkhir", end));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_RPTPENJUALAN item = new USP_RPTPENJUALAN();
                        item.KODE_CUST = reader.GetString(0);
                        item.totalPenjualan = reader.GetInt32(1);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_TOTALMARGIN> getRptTotalMargin(DateTime start, DateTime end)
        {
            List<USP_TOTALMARGIN> listReport = new List<USP_TOTALMARGIN>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_totalMargin", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_TOTALMARGIN item = new USP_TOTALMARGIN();
                        item.SHOWROOM = reader.GetString(0);
                        item.KODE_CUST = reader.GetString(1);
                        item.BRAND = reader.GetString(2);
                        item.STORE = reader.GetString(3);
                        item.STATUS_MARGIN = reader.GetString(4);
                        item.MARGIN = reader.GetDecimal(5);
                        item.TOTAL_SALES = reader.GetInt32(6);
                        item.UANG_STORE = reader.GetDecimal(7);
                        item.UANG_TERIMA = reader.GetDecimal(8);
                        item.DPP_TERIMA = reader.GetDecimal(9);
                        item.PPN_TERIMA = reader.GetDecimal(10);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_RPTTOTALSALES> getRptTotalSales(DateTime start, DateTime end)
        {
            List<USP_RPTTOTALSALES> listReport = new List<USP_RPTTOTALSALES>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_rptTotalSales", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_RPTTOTALSALES item = new USP_RPTTOTALSALES();
                        item.KODE_CUST = reader.GetString(0);
                        item.KODE_CT = reader.GetString(1);
                        item.STATUS_STORE = reader.GetString(2);
                        item.CARD = reader.GetString(3);
                        item.TOTAL_SALES = reader.GetInt32(4);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_RPTTOTALPENJUALAN> getRptTotalPenjualan(DateTime start, DateTime end, string store)
        {
            List<USP_RPTTOTALPENJUALAN> listReport = new List<USP_RPTTOTALPENJUALAN>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_rptTotalPenjualan", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_RPTTOTALPENJUALAN item = new USP_RPTTOTALPENJUALAN();
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(2);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(3);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(4);
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(5);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(6);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(7);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(8);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(9);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(10);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(11);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(12);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(13);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(14);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(15);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(16);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(17);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(18);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(19);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(20);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(21);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(22);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(23);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(24);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(25);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        #region Report Singapore

        public List<USP_SRPTTOTALPENJUALANPERPRODUCT> getSrptTotalPenjualanPerProduct(DateTime start, DateTime end, string store, string where)
        {
            List<USP_SRPTTOTALPENJUALANPERPRODUCT> listReport = new List<USP_SRPTTOTALPENJUALANPERPRODUCT>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptTotalPenjualanPerProduct", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    command.Parameters.Add(new SqlParameter("@where", where));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTTOTALPENJUALANPERPRODUCT item = new USP_SRPTTOTALPENJUALANPERPRODUCT();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(3);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(4);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(5);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(6);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(7);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(8);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(9);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(10);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(12);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(13);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(14);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(17);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(18);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(19);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(21);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(22);


                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(23);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(24);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(25);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(26);
                        item.DPP = reader.IsDBNull(reader.GetOrdinal("DPP")) ? 0 : reader.GetDecimal(27);
                        item.PPN = reader.IsDBNull(reader.GetOrdinal("PPN")) ? 0 : reader.GetDecimal(28);
                        
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_SRPTSTOCK> getSrptStock(DateTime date, string store)
        {
            List<USP_SRPTSTOCK> listStock = new List<USP_SRPTSTOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptStock", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@date", date));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTSTOCK item = new USP_SRPTSTOCK();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(1);
                        item.WAREHOUSE = reader.IsDBNull(reader.GetOrdinal("WAREHOUSE")) ? "" : reader.GetString(2);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("WAREHOUSE")) ? "" : reader.GetString(3);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(4);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(5);
                        item.BARCODE_BRG = reader.IsDBNull(reader.GetOrdinal("BARCODE_BRG")) ? "" : reader.GetString(6);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(7);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(8);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(9);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(10);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(11);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(12);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(13);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(14);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(15);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(16);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(17);


                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(18);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(19);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(20);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(21);
                        item.KODE_PT = reader.IsDBNull(reader.GetOrdinal("KODE_PT")) ? "" : reader.GetString(22);

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

        public List<USP_SRPTADJUSMENTMANUAL> getSrptAdjustmentManual(DateTime start, DateTime end, string store)
        {
            List<USP_SRPTADJUSMENTMANUAL> listReport = new List<USP_SRPTADJUSMENTMANUAL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptAdjusmentManual", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTADJUSMENTMANUAL item = new USP_SRPTADJUSMENTMANUAL();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(3);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(4);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(5);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(6);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(7);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(8);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(9);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(10);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(12);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(13);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(14);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(17);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(18);
                        item.STOCK_AWAL = reader.IsDBNull(reader.GetOrdinal("STOCK_AWAL")) ? 0 : reader.GetInt32(19);
                        item.ADJUSTMENT = reader.IsDBNull(reader.GetOrdinal("ADJUSTMENT")) ? 0 : reader.GetInt32(20);
                        item.STOCK_AKHIR = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR")) ? 0 : reader.GetInt32(21);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(22);

                        item.CREATED_BY_ADJ = reader.IsDBNull(reader.GetOrdinal("CREATED_BY_ADJ")) ? "" : reader.GetString(23);
                        item.CREATED_DATE_ADJ = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE_ADJ")) ? (DateTime?)null : reader.GetDateTime(24);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_SRPTTRANSFERSTOCK> getSrptTransferStock(DateTime start, DateTime end, string store)
        {
            List<USP_SRPTTRANSFERSTOCK> listReport = new List<USP_SRPTTRANSFERSTOCK>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptTransferStock", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTTRANSFERSTOCK item = new USP_SRPTTRANSFERSTOCK();
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(0);
                        item.KODE_MOVE = reader.IsDBNull(reader.GetOrdinal("KODE_MOVE")) ? "" : reader.GetString(1);
                        item.DARI = reader.IsDBNull(reader.GetOrdinal("DARI")) ? "" : reader.GetString(2);
                        item.KE = reader.IsDBNull(reader.GetOrdinal("KE")) ? "" : reader.GetString(3);
                        item.KODE_DARI = reader.IsDBNull(reader.GetOrdinal("KODE_DARI")) ? "" : reader.GetString(4);
                        item.KODE_KE = reader.IsDBNull(reader.GetOrdinal("KODE_KE")) ? "" : reader.GetString(5);
                        item.WAKTU_KIRIM = reader.IsDBNull(reader.GetOrdinal("WAKTU_KIRIM")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.WAKTU_TERIMA = reader.IsDBNull(reader.GetOrdinal("WAKTU_TERIMA")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.DONE_TIME = reader.IsDBNull(reader.GetOrdinal("DONE_TIME")) ? (DateTime?)null : reader.GetDateTime(8);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(9);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(10);
                        item.NO_BUKTI_DETAIL = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI_DETAIL")) ? "" : reader.GetString(11);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(12);
                        item.QTY_KIRIM = reader.IsDBNull(reader.GetOrdinal("QTY_KIRIM")) ? 0 : reader.GetInt32(13);
                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(14);
                        item.STOCK_AKHIR_KIRIM = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_KIRIM")) ? 0 : reader.GetInt32(15);
                        item.STOCK_AKHIR_TERIMA = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_TERIMA")) ? 0 : reader.GetInt32(16);
                        item.USER_KIRIM = reader.IsDBNull(reader.GetOrdinal("USER_KIRIM")) ? "" : reader.GetString(17);
                        item.USER_TERIMA = reader.IsDBNull(reader.GetOrdinal("USER_TERIMA")) ? "" : reader.GetString(18);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(19);
                        item.REFF = reader.IsDBNull(reader.GetOrdinal("REFF")) ? "" : reader.GetString(20);
                        item.BARCODE_BRG = reader.IsDBNull(reader.GetOrdinal("BARCODE_BRG")) ? "" : reader.GetString(21);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(22);

                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(23);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(24);


                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(25);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(26);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(27);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(28);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(29);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(30);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(31);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(32);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(33);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(34);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(35);

                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(36);
                        item.KODE_PT = reader.IsDBNull(reader.GetOrdinal("KODE_PT")) ? "" : reader.GetString(37);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_SRPTSTOCKOPNAME> getSrptStockOpname(DateTime start, DateTime end, string store)
        {
            List<USP_SRPTSTOCKOPNAME> listReport = new List<USP_SRPTSTOCKOPNAME>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptStockOpname", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTSTOCKOPNAME item = new USP_SRPTSTOCKOPNAME();
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(0);
                        item.NO_BUKTI_DETAIL = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI_DETAIL")) ? "" : reader.GetString(1);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(2);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(3);
                        item.KODE_MOVE = reader.IsDBNull(reader.GetOrdinal("KODE_MOVE")) ? "" : reader.GetString(4);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(5);
                        item.KODE_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("KODE_SHOWROOM")) ? "" : reader.GetString(6);
                        item.WAKTU_SO = reader.IsDBNull(reader.GetOrdinal("WAKTU_SO")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(8);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(9);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(11);
                        item.DIFF = reader.IsDBNull(reader.GetOrdinal("DIFF")) ? 0 : reader.GetInt32(12);
                        item.STOCK_AWAL = reader.IsDBNull(reader.GetOrdinal("DIFF")) ? 0 : reader.GetInt32(13);
                        item.STOCK_AKHIR = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR")) ? 0 : reader.GetInt32(14);
                        item.BARCODE_BRG = reader.IsDBNull(reader.GetOrdinal("BARCODE_BRG")) ? "" : reader.GetString(15);
                        item.ITEM_CODE_BRG = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE_BRG")) ? "" : reader.GetString(16);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(17);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(18);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(19);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(20);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(21);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(22);

                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(23);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(24);


                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(25);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(26);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(27);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(28);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(29);
                        
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_SRPTSALES> getSrptSales(DateTime start, DateTime end, string store)
        {
            List<USP_SRPTSALES> listReport = new List<USP_SRPTSALES>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptSales", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTSALES item = new USP_SRPTSALES();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(3);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(4);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(5);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(6);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(7);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(8);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(9);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(10);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(12);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(13);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(14);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(17);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(18);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(19);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(21);
                        item.KODE_PT = reader.IsDBNull(reader.GetOrdinal("KODE_PT")) ? "" : reader.GetString(22);

                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 00 : reader.GetInt64(23);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(24);


                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(25);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(26);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(27);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(28);
                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(29);

                        item.KD_CCARD = reader.IsDBNull(reader.GetOrdinal("KD_CCARD")) ? "" : reader.GetString(30);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(31);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        public List<USP_SRPTSALESTHRU> getSrptSalesThru(DateTime start, DateTime end, string store)
        {
            List<USP_SRPTSALESTHRU> listReport = new List<USP_SRPTSALESTHRU>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_srptSalesThru", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@startDate", start));
                    command.Parameters.Add(new SqlParameter("@endDate", end));
                    command.Parameters.Add(new SqlParameter("@store", store));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        USP_SRPTSALESTHRU item = new USP_SRPTSALESTHRU();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(3);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(4);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(5);
                        item.FPRODUK = reader.IsDBNull(reader.GetOrdinal("FPRODUK")) ? "" : reader.GetString(6);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(7);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(8);
                        item.FSEASON = reader.IsDBNull(reader.GetOrdinal("FSEASON")) ? "" : reader.GetString(9);
                        item.FBS = reader.IsDBNull(reader.GetOrdinal("FBS")) ? "" : reader.GetString(10);
                        item.TGL_BS = reader.IsDBNull(reader.GetOrdinal("TGL_BS")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.MILIK = reader.IsDBNull(reader.GetOrdinal("MILIK")) ? "" : reader.GetString(12);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(13);
                        item.KLMPK = reader.IsDBNull(reader.GetOrdinal("KLMPK")) ? "" : reader.GetString(14);
                        item.GENDER = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? "" : reader.GetString(15);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(16);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(17);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(18);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(19);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(20);
                        item.STATUS_BRG = reader.IsDBNull(reader.GetOrdinal("STATUS_BRG")) ? "" : reader.GetString(21);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(22);


                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(23);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(24);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(25);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(26);
                        item.DPP = reader.IsDBNull(reader.GetOrdinal("DPP")) ? 0 : reader.GetDecimal(27);
                        item.PPN = reader.IsDBNull(reader.GetOrdinal("PPN")) ? 0 : reader.GetDecimal(28);

                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(29);
                        item.SELL_THRU = reader.IsDBNull(reader.GetOrdinal("SELL_THRU")) ? "" : reader.GetString(30);
                        
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        #endregion

        public List<tf_lapHarianPerthn> GetTfLapHarianPerThn(DateTime start, DateTime end, String KDCust)
        {
            List<tf_lapHarianPerthn> listReport = new List<tf_lapHarianPerthn>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT KODE_CUST, KODE, cast(round(NILAI_BYR,2) as numeric(36,2)) NILAI_BYR, DPP, PPN, cast(round(NET_BAYAR,2) as numeric(36,2)) NET_BAYAR," +
                    "cast(round(NET_CASH,2) as numeric(36,2)) NET_CASH, cast(round(JM_CARD,2) as numeric(36,2)) JM_CARD, cast(round(JM_VOUCHER,2) as numeric(36,2)) JM_VOUCHER, "+
                    "cast(round(NET_CARD_DEBIT,2) as numeric(36,2)) NET_CARD_DEBIT , cast(round(NET_CARD_KREDIT,2) as numeric(36,2)) NET_CARD_KREDIT, QTY, "+
                    "cast(round(DISC_R,2) as numeric(36,2)) DISC_R, cast(round(TAG_PRICE,2) as numeric(36,2)) TAG_PRICE, UANG_MARGIN, TGL_TRANS FROM tf_lapHarianPerthn(@kode, @tglTrans, @tglEndTrans)", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@tglTrans", start));
                    command.Parameters.Add(new SqlParameter("@tglEndTrans", end));
                    command.Parameters.Add(new SqlParameter("@kode", KDCust));

                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        tf_lapHarianPerthn item = new tf_lapHarianPerthn();
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(0);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(1);
                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(2);
                        item.DPP = reader.IsDBNull(reader.GetOrdinal("DPP")) ? 0 : reader.GetDecimal(3);
                        item.PPN = reader.IsDBNull(reader.GetOrdinal("PPN")) ? 0 : reader.GetDecimal(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(6);
                        item.JM_CARD = reader.IsDBNull(reader.GetOrdinal("JM_CARD")) ? 0 : reader.GetDecimal(7);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(8);
                        item.NET_CARD_DEBIT = reader.IsDBNull(reader.GetOrdinal("NET_CARD_DEBIT")) ? 0 : reader.GetDecimal(9);
                        item.NET_CARD_KREDIT = reader.IsDBNull(reader.GetOrdinal("NET_CARD_KREDIT")) ? 0 : reader.GetDecimal(10);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(11);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(12);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(13);
                        item.UANG_MARGIN = reader.IsDBNull(reader.GetOrdinal("UANG_MARGIN")) ? 0 : reader.GetDecimal(14);
                        item.TGL_TRANS = reader.GetDateTime(15);
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        #region "JENAHARA"
        public List<Tf_ReportPusrchaseOrder> GetTf_ReportPusrchaseOrder(String IDPO)
        {
            List<Tf_ReportPusrchaseOrder> listReport = new List<Tf_ReportPusrchaseOrder>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT Row_Nmbr, ID, NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, PHONE, KODE_SUPPLIER, SUPPLIER, " +
                    "ID_KDBRG, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC, QTY, COGS, ttl_amt, STATUS_PO FROM Tf_ReportPusrchaseOrder(@IdPO)", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@IdPO", IDPO));

                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Tf_ReportPusrchaseOrder item = new Tf_ReportPusrchaseOrder();
                        item.Row_Nmbr = reader.GetInt64(0);
                        item.ID = reader.GetInt64(1);
                        item.NO_PO = reader.GetString(2);
                        item.PO_REFF = reader.GetString(3);
                        item.DATE = reader.GetDateTime(4);
                        item.BRAND = reader.GetString(5);
                        item.CONTACT = reader.GetString(6);
                        item.POSITION = reader.GetString(7);
                        item.EMAIL = reader.GetString(8);
                        item.ADDRESS = reader.GetString(9);
                        item.PHONE = reader.GetString(10);
                        item.KODE_SUPPLIER = reader.GetString(11);
                        item.SUPPLIER = reader.GetString(12);
                        item.ID_KDBRG = reader.GetInt64(13);
                        item.BARCODE = reader.GetString(14);
                        item.FART_DESC = reader.GetString(15);
                        item.FCOL_DESC = reader.GetString(16);
                        item.FSIZE_DESC = reader.GetString(17);
                        item.QTY = reader.GetInt32(18);
                        item.COGS = reader.GetDecimal(19);
                        item.ttl_amt = reader.GetDecimal(20);
                        item.STATUS_PO = reader.GetBoolean(21);
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }
        public virtual DataSet GetDataTf_ReportPusrchaseOrder(String IDPO)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Row_Nmbr, ID, NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, PHONE, KODE_SUPPLIER, SUPPLIER, " +
                    "ID_KDBRG, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC, QTY, COGS, ttl_amt, STATUS_PO, ISNULL(FPPN_PCT, 0) FPPN_PCT,ISNULL(FPPN_RP, 0) FPPN_RP FROM Tf_ReportPusrchaseOrder(@IdPO)"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@IDPO", IDPO);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DS_Tf_ReportPusrchaseOrder");
                Connection.Close();
            }
            return dataSet;
        }

        public List<TF_RptDeliveryNotesTrfStock> GetTF_RptDeliveryNotesTrfStock(String IDHeader)
        {
            List<TF_RptDeliveryNotesTrfStock> listReport = new List<TF_RptDeliveryNotesTrfStock>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA,ALASAN, REFF, BARCODE,FART_DESC, " +
                    "FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, TGL_BS, MILIK, PRICE, KLMPK, GENDER, COGS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, KODE_KE," +
                    "SHR_TUJUAN, KODE_DARI, SHR_ASAL, Row_Nmbr, No_Refrensi, ALMT_TUJUAN, ALMT_ASAL, TELP_TUJUAN, TELP_ASAL FROM TF_RptDeliveryNotesTrfStock(@IDHeader) ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@IDHeader", IDHeader));

                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TF_RptDeliveryNotesTrfStock item = new TF_RptDeliveryNotesTrfStock();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.NO_BUKTI = reader.GetString(3);
                        item.ITEM_CODE = reader.GetString(4);
                        item.QTY_KIRIM = reader.GetInt32(5);
                        item.QTY_TERIMA = reader.GetInt32(6);
                        item.STOCK_AKHIR_KIRIM = reader.GetInt32(7);
                        item.STOCK_AKHIR_TERIMA = reader.GetInt32(8);
                        item.USER_KIRIM = reader.GetString(9);
                        item.USER_TERIMA = reader.GetString(10);
                        item.ALASAN = reader.GetString(11);
                        item.REFF = reader.GetString(12);
                        item.BARCODE = reader.GetString(13);
                        item.FART_DESC = reader.GetString(14);
                        item.FCOL_DESC = reader.GetString(15);
                        item.FSIZE_DESC = reader.GetString(16);
                        item.FPRODUK = reader.GetString(17);
                        item.FBRAND = reader.GetString(18);
                        item.FGROUP = reader.GetString(19);
                        item.FSEASON = reader.GetString(20);
                        item.FBS = reader.GetString(21);
                        item.TGL_BS = reader.GetDateTime(22);
                        item.MILIK = reader.GetString(23);
                        item.PRICE = reader.GetDecimal(24);
                        item.KLMPK = reader.GetString(25);
                        item.GENDER = reader.GetString(26);
                        item.COGS = reader.GetDecimal(27);
                        item.CREATED_BY = reader.GetString(28);
                        item.CREATED_DATE = reader.GetDateTime(29);
                        item.UPDATED_BY = reader.GetString(30);
                        item.UPDATED_DATE = reader.GetDateTime(31);
                        item.KODE_KE = reader.GetString(32);
                        item.SHR_TUJUAN = reader.GetString(33);
                        item.KODE_DARI = reader.GetString(34);
                        item.SHR_ASAL = reader.GetString(35);
                        item.Row_Nmbr = reader.GetInt64(36);
                        item.No_Refrensi = reader.GetString(37);
                        item.ALMT_TUJUAN = reader.GetString(38);
                        item.ALMT_ASAL = reader.GetString(39);
                        item.TELP_TUJUAN = reader.GetString(40);
                        item.TELP_ASAL = reader.GetString(41);
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }
        public virtual DataSet GetDataTF_RptDeliveryNotesTrfStock(String IDHeader)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA,ALASAN, REFF, BARCODE,FART_DESC, " +
                    "FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, TGL_BS, MILIK, PRICE, KLMPK, GENDER, COGS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, KODE_KE," +
                    "SHR_TUJUAN, KODE_DARI, SHR_ASAL, Row_Nmbr, No_Refrensi, ALMT_TUJUAN, ALMT_ASAL, TELP_TUJUAN, TELP_ASAL  FROM TF_RptDeliveryNotesTrfStock(@IDHeader) ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@IDHeader", IDHeader);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }
        public List<TF_RptPackingListTrfStock> GetTF_RptPackingListTrfStock(String IDHeader)
        {
            List<TF_RptPackingListTrfStock> listReport = new List<TF_RptPackingListTrfStock>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA,ALASAN, REFF, BARCODE,FART_DESC, " +
                    "FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, TGL_BS, MILIK, PRICE, KLMPK, GENDER, COGS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, KODE_KE," +
                    "SHR_TUJUAN, KODE_DARI, SHR_ASAL, Row_Nmbr, No_Refrensi, ALMT_TUJUAN, ALMT_ASAL, TELP_TUJUAN, TELP_ASAL, WAKTU_KIRIM, WAKTU_TERIMA   FROM TF_RptPackingListTrfStock(@IDHeader) ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@IDHeader", IDHeader));

                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TF_RptPackingListTrfStock item = new TF_RptPackingListTrfStock();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.NO_BUKTI = reader.GetString(3);
                        item.ITEM_CODE = reader.GetString(4);
                        item.QTY_KIRIM = reader.GetInt32(5);
                        item.QTY_TERIMA = reader.GetInt32(6);
                        item.STOCK_AKHIR_KIRIM = reader.GetInt32(7);
                        item.STOCK_AKHIR_TERIMA = reader.GetInt32(8);
                        item.USER_KIRIM = reader.GetString(9);
                        item.USER_TERIMA = reader.GetString(10);
                        item.ALASAN = reader.GetString(11);
                        item.REFF = reader.GetString(12);
                        item.BARCODE = reader.GetString(13);
                        item.FART_DESC = reader.GetString(14);
                        item.FCOL_DESC = reader.GetString(15);
                        item.FSIZE_DESC = reader.GetString(16);
                        item.FPRODUK = reader.GetString(17);
                        item.FBRAND = reader.GetString(18);
                        item.FGROUP = reader.GetString(19);
                        item.FSEASON = reader.GetString(20);
                        item.FBS = reader.GetString(21);
                        item.TGL_BS = reader.GetDateTime(22);
                        item.MILIK = reader.GetString(23);
                        item.PRICE = reader.GetDecimal(24);
                        item.KLMPK = reader.GetString(25);
                        item.GENDER = reader.GetString(26);
                        item.COGS = reader.GetDecimal(27);
                        item.CREATED_BY = reader.GetString(28);
                        item.CREATED_DATE = reader.GetDateTime(29);
                        item.UPDATED_BY = reader.GetString(30);
                        item.UPDATED_DATE = reader.GetDateTime(31);
                        item.KODE_KE = reader.GetString(32);
                        item.SHR_TUJUAN = reader.GetString(33);
                        item.KODE_DARI = reader.GetString(34);
                        item.SHR_ASAL = reader.GetString(35);
                        item.Row_Nmbr = reader.GetInt64(36);
                        item.No_Refrensi = reader.GetString(37);
                        item.ALMT_TUJUAN = reader.GetString(38);
                        item.ALMT_ASAL = reader.GetString(39);
                        item.TELP_TUJUAN = reader.GetString(40);
                        item.TELP_ASAL = reader.GetString(41);
                        item.WAKTU_KIRIM = reader.GetDateTime(42);
                        item.WAKTU_TERIMA = reader.GetDateTime(43);
                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }
        public virtual DataSet GetDataTF_RptPackingListTrfStock(String IDHeader)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA,ALASAN, REFF, BARCODE,FART_DESC, " +
                    "FCOL_DESC, FSIZE_DESC, FPRODUK, FBRAND, FGROUP, FSEASON, FBS, TGL_BS, MILIK, PRICE, KLMPK, GENDER, COGS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, KODE_KE," +
                    "SHR_TUJUAN, KODE_DARI, SHR_ASAL, Row_Nmbr, No_Refrensi, ALMT_TUJUAN, ALMT_ASAL, TELP_TUJUAN, TELP_ASAL, WAKTU_KIRIM, WAKTU_TERIMA   FROM TF_RptPackingListTrfStock(@IDHeader) ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@IDHeader", IDHeader);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }
        public virtual DataSet GetDataTF_ReportPackingListGR(String NoGR)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Row_Nmbr, KODE_SUPPLIER	, SUPPLIER, ADDRESS, PHONE, ID_TR_BELI_DTL, ID_TR_BELI_HDR, ID_DETAIL_PO, ID_KDBRG, " +
                    " NO_GR, QTY_TIBA, KODE, RECEIVED_BY, RECEIVE_DATE, COGS, PRICE, BARCODE, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, " +
                    "FBRAND, SHOWROOM, TGL_TRANS, No_Refrensi FROM TF_ReportPackingListGR(@NoGR) order by ID_DETAIL_PO"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }

        #endregion
        #region "DEIS BI"
        public List<TF_DeisBIF> GetTF_DeisBIF(DateTime dtNow)
        {
            List<TF_DeisBIF> listReport = new List<TF_DeisBIF>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT DT_FIRST, DT_NOW, DT_FIRST_LY, DT_NOW_LY, KODE, BRAND, STATUS_SHOWROOM, SHOWROOM, ALAMAT, " +
                    "QTY_TY, cast(NILAI_BYR_TY as numeric(10,2)) as NILAI_BYR_TY, QTY_BON_TY, ATV, UPT, ASP, QTY_LY, cast(NILAI_BYR_LY as numeric(10,2)) as NILAI_BYR_LY, QTY_BON_LY, QTY_TY_DtToMonth, NILAI_BYR_TY_DtToMonth," +
                    " QTY_BON_TY_DtToMonth, ATV_DtToMonth, UPT_DtToMonth, ASP_DtToMonth, QTY_LY_DtToMonth, NILAI_BYR_LY_DtToMonth, QTY_BON_LY_DtToMonth" +
                    " FROM TF_DeisBIF(@dtNow) ORDER BY BRAND, STATUS_SHOWROOM", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dtNow", dtNow));

                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TF_DeisBIF item = new TF_DeisBIF();
                        item.DT_FIRST = reader.GetDateTime(0);
                        item.DT_NOW = reader.GetDateTime(1);
                        item.DT_FIRST_LY = reader.GetDateTime(2);
                        item.DT_NOW_LY = reader.GetDateTime(3);
                        item.KODE = reader.GetString(4);
                        item.BRAND = reader.GetString(5);
                        item.STATUS_SHOWROOM = reader.GetString(6);
                        item.SHOWROOM = reader.GetString(7);
                        item.ALAMAT = reader.GetString(8);
                        item.QTY_TY = reader.GetInt32(9);
                        item.NILAI_BYR_TY = reader.GetDecimal(10);
                        item.QTY_BON_TY = reader.GetInt32(11);
                        item.ATV = reader.GetDecimal(12);
                        item.UPT = reader.GetInt32(13);
                        item.ASP = reader.GetDecimal(14);
                        item.QTY_LY = reader.GetInt32(15);
                        item.NILAI_BYR_LY = reader.GetDecimal(16);
                        item.QTY_BON_LY = reader.GetInt32(17);
                        item.QTY_TY_DtToMonth = reader.GetInt32(18);
                        item.NILAI_BYR_TY_DtToMonth = reader.GetDecimal(19);
                        item.QTY_BON_TY_DtToMonth = reader.GetInt32(20);
                        item.ATV_DtToMonth = reader.GetDecimal(21);
                        item.UPT_DtToMonth = reader.GetInt32(22);
                        item.ASP_DtToMonth = reader.GetDecimal(23);
                        item.QTY_LY_DtToMonth = reader.GetInt32(24);
                        item.NILAI_BYR_LY_DtToMonth = reader.GetDecimal(25);
                        item.QTY_BON_LY_DtToMonth = reader.GetInt32(26);

                        listReport.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listReport;
        }

        #endregion
        public virtual DataSet GetDataTf_GetStatistikDokumen_Report(String StatistikType, DateTime dtStart, DateTime dtEnd, String KdShr)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT id, No_Dok, TGL_Trans, TTL_QTY, TTL_DOK, TimeStamp, ID_USER, KD_SHR " +
                "FROM Tf_GetStatistikDokumen_Report(@StatistikType, @dtStart, @dtEnd, @KdShr)"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@StatistikType", StatistikType);
                command.Parameters.Add("@dtStart", dtStart);
                command.Parameters.Add("@dtEnd", dtEnd);
                command.Parameters.Add("@KdShr", KdShr);

                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                Connection.Close();
            }
            return dataSet;
        }
        public virtual DataSet GetDataTf_Dtl_GetStatistikDokumen_Report(String StatistikType, String IdHdr, DateTime dtStart, DateTime dtEnd, String KdShr)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT barcode, Art_Desc, Warna, Size, Qty, NoDok, TGL_Trans " +
                "FROM Tf_Dtl_GetStatistikDokumen_Report(@StatistikType, @IdHdr, @dtStart, @dtEnd, @KdShr)"), Connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@StatistikType", StatistikType);
                command.Parameters.Add("@IdHdr", IdHdr);
                //command.Parameters.Add("@NoDok", NoDok);
                command.Parameters.Add("@dtStart", dtStart);
                command.Parameters.Add("@dtEnd", dtEnd);
                command.Parameters.Add("@KdShr", KdShr);

                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                Connection.Close();
            }
            return dataSet;
        }

    }
}