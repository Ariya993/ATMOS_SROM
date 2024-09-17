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
    public class LAPORAN_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<SH_BAYAR> getLaporanSHBayar(DateTime start, DateTime end, string and)
        {
            List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, KODE_CUST, KODE, TGL_TRANS, NO_BON, QTY, NET_BAYAR, NET_CASH, NET_CARD, JM_UANG, KEMBALI, JM_VOUCHER " +
                    " from vw_salesHeader where TGL_TRANS between @startDate and @endDate {0}", and), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@startDate", SqlDbType.DateTime2).Value = start;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime2).Value = end;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_BAYAR item = new SH_BAYAR();
                        item.ID = reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.KODE = reader.GetString(2);
                        item.TGL_TRANS = reader.GetDateTime(3);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(4);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(5);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(6);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(7);
                        item.NET_CARD = reader.IsDBNull(reader.GetOrdinal("NET_CARD")) ? 0 : reader.GetDecimal(8);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(9);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(10);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(11);                        

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

        public List<LAPORAN_HARIAN> getLaporanHarian(string kode, DateTime tglTrans, string where, DateTime tglEndTrans)
        {
            List<LAPORAN_HARIAN> listLapHarian = new List<LAPORAN_HARIAN>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select KODE_CUST, KODE, NILAI_BYR, DPP, PPN, " +
                    "NET_BAYAR, NET_CASH, JM_CARD, JM_VOUCHER, KD_CARD_DEBIT, NET_CARD_DEBIT, KD_CARD_KREDIT, " +
                    "NET_CARD_KREDIT, QTY, DISC_R, TAG_PRICE, MARGIN, OTHER " +
                    "from tf_lapHarian(@kode, @tglTrans, @tglEndTrans) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = kode;
                    command.Parameters.Add("@tglTrans", SqlDbType.Date).Value = tglTrans;
                    command.Parameters.Add("@tglEndTrans", SqlDbType.Date).Value = tglEndTrans;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        LAPORAN_HARIAN item = new LAPORAN_HARIAN();
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(0);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(1);
                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(2);
                        item.DPP = reader.IsDBNull(reader.GetOrdinal("DPP")) ? 0 : reader.GetDecimal(3);
                        item.PPN = reader.IsDBNull(reader.GetOrdinal("PPN")) ? 0 : reader.GetDecimal(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(6);
                        item.JM_CARD = reader.IsDBNull(reader.GetOrdinal("JM_CARD")) ? 0 : reader.GetDecimal(7);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(8);
                        item.KD_CARD_DEBIT = reader.IsDBNull(reader.GetOrdinal("KD_CARD_DEBIT")) ? "Debit" : reader.GetString(9);
                        item.NET_CARD_DEBIT = reader.IsDBNull(reader.GetOrdinal("NET_CARD_DEBIT")) ? 0 : reader.GetDecimal(10);
                        item.KD_CARD_KREDIT = reader.IsDBNull(reader.GetOrdinal("KD_CARD_KREDIT")) ? "Kredit" : reader.GetString(11);
                        item.NET_CARD_KREDIT = reader.IsDBNull(reader.GetOrdinal("NET_CARD_KREDIT")) ? 0 : reader.GetDecimal(12);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(13);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(14);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(15);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetDecimal(16);
                        item.OTHERS = reader.IsDBNull(reader.GetOrdinal("OTHER")) ? 0 : reader.GetDecimal(17);

                        listLapHarian.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLapHarian;
        }

        public List<VW_LAPORAN_HARIAN> getPenjualanHarian(string where)
        {
            List<VW_LAPORAN_HARIAN> listLapHarian = new List<VW_LAPORAN_HARIAN>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select TGL_TRANS, QTY, TAG_PRICE, BON_PRICE, DISC_ACARA, CUST_BAYAR, UANG_TERIMA, " +
                    " POTONGAN, MARGIN, SHOWROOM, KODE " +
                    " from vw_penjualanHarian {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        VW_LAPORAN_HARIAN item = new VW_LAPORAN_HARIAN();
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? DateTime.Now : reader.GetDateTime(0);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(1);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(2);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(3);
                        item.DISC_ACARA = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(4);
                        item.CUST_BAYAR = reader.IsDBNull(reader.GetOrdinal("CUST_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.UANG_TERIMA = reader.IsDBNull(reader.GetOrdinal("UANG_TERIMA")) ? 0 : reader.GetDecimal(6);
                        item.POTONGAN = reader.IsDBNull(reader.GetOrdinal("POTONGAN")) ? 0 : reader.GetDecimal(7);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetDecimal(8);

                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(9);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(10);

                        listLapHarian.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLapHarian;
        }

        public List<TF_STATISTIC> getTfStatistic(string where, string kode, DateTime tglCutOff, DateTime tglCutOffEnd)
        {
            List<TF_STATISTIC> listLapHarian = new List<TF_STATISTIC>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select STOCK_AWAL, KODE, PENJUALAN, PUTUS, PUTUS_RETUR, PO, " +	
                    " TERIMA_BRG, TERIMA_PINJAM, KELUAR_BRG, KELUAR_PINJAM, ADJ_MANUAL, ADJUSTMENT_SO, STOCK_AKHIR, DIFF " +
                    " from tf_statistic(@kode, @tglCutOff, @tglCutOffEnd) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", kode);
                    command.Parameters.Add("@tglCutOff", tglCutOff);
                    command.Parameters.Add("@tglCutOffEnd", tglCutOffEnd);
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TF_STATISTIC item = new TF_STATISTIC();
                        item.STOCK_AWAL = reader.IsDBNull(reader.GetOrdinal("STOCK_AWAL")) ? 0 : reader.GetInt32(0);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(1);
                        item.PENJUALAN = reader.IsDBNull(reader.GetOrdinal("PENJUALAN")) ? 0 : reader.GetInt32(2);
                        item.PUTUS = reader.IsDBNull(reader.GetOrdinal("PUTUS")) ? 0 : reader.GetInt32(3);
                        item.PUTUS_RETUR = reader.IsDBNull(reader.GetOrdinal("PUTUS_RETUR")) ? 0 : reader.GetInt32(4);
                        item.PO = reader.IsDBNull(reader.GetOrdinal("PO")) ? 0 : reader.GetInt32(5);
                        item.TERIMA_BRG = reader.IsDBNull(reader.GetOrdinal("TERIMA_BRG")) ? 0 : reader.GetInt32(6);
                        item.TERIMA_PINJAM = reader.IsDBNull(reader.GetOrdinal("TERIMA_PINJAM")) ? 0 : reader.GetInt32(7);
                        item.KELUAR_BRG = reader.IsDBNull(reader.GetOrdinal("KELUAR_BRG")) ? 0 : reader.GetInt32(8);

                        item.KELUAR_PINJAM = reader.IsDBNull(reader.GetOrdinal("KELUAR_PINJAM")) ? 0 : reader.GetInt32(9);
                        item.ADJ_MANUAL = reader.IsDBNull(reader.GetOrdinal("ADJ_MANUAL")) ? 0 : reader.GetInt32(10);
                        item.ADJUSTMENT_SO = reader.IsDBNull(reader.GetOrdinal("ADJUSTMENT_SO")) ? 0 : reader.GetInt32(11);
                        item.STOCK_AKHIR = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR")) ? 0 : reader.GetInt32(12);
                        item.DIFF = reader.IsDBNull(reader.GetOrdinal("DIFF")) ? 0 : reader.GetInt32(13);
                        listLapHarian.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listLapHarian;
        }
        public virtual DataSet GetDatavw_PrintPeminjaman(String ID_HEADER)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Row_Nmbr, ID, ID_HEADER, ID_KDBRG, NO_BUKTI, BARCODE, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, " +
                "STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA, ALASAN, REFF, FLAG, BRAND, FART_DESC, COLOR, SIZE, DARI, KE, KODE_DARI, KODE_KE, NAMA, " +
                "PHONE, EMAIL, WAKTU_KIRIM, WAKTU_KEMBALI, WAKTU_SELESAI, STATUS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS_PINJAM, TOTAL_KIRIM, " +
                "TOTAL_KEMBALI FROM vw_PrintPeminjaman Where ID_HEADER = {0}", ID_HEADER), Connection))
            {
                command.CommandType = CommandType.Text;
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }

    }
}