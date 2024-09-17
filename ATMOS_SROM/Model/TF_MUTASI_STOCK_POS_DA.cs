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
    public class TF_MUTASI_STOCK_POS_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public List<TF_MUTASI_STOCK_POS> getTfMutasiStockPOS(DateTime tglAwal, DateTime tglAkhir, string Brcode, string kode)//(string where)
        {
            List<TF_MUTASI_STOCK_POS> listTemp = new List<TF_MUTASI_STOCK_POS>();
            try
            {
                SqlConnection Connection = new SqlConnection(conn);

                using (SqlCommand command = new SqlCommand(string.Format("select KODE, BARCODE, SLD_AWAL, QTY_BELI, QTY_TERIMA, QTY_RTR_PTS, QTY_IN_PINJAM, QTY_KIRIM, QTY_JUAL, " +
                    "QTY_JUAL_PTS, QTY_OUT_PINJAM, QTY_ADJ, QTY_OPNM, SLD_AKHIR, ADJ_GIT " +
                "from tf_MutasiStockPOS(@tglawal, @tglakhir, @barcode, @kode)"), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@tglawal", tglAwal);
                    command.Parameters.Add("@tglakhir", tglAkhir);
                    command.Parameters.Add("@barcode", Brcode);
                    command.Parameters.Add("@kode", kode);
                    command.CommandTimeout = 300;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TF_MUTASI_STOCK_POS item = new TF_MUTASI_STOCK_POS();
                        item.KODE = reader.GetString(0);
                        item.BARCODE = reader.GetString(1);
                        item.SLD_AWAl = reader.GetInt32(2);
                        item.QTY_BELI = reader.GetInt32(3);
                        item.QTY_TERIMA = reader.GetInt32(4);
                        item.QTY_RTR_PTS = reader.GetInt32(5);
                        item.QTY_IN_PINJAM = reader.GetInt32(6);
                        item.QTY_KIRIM = reader.GetInt32(7);
                        item.QTY_JUAL = reader.GetInt32(8);
                        item.QTY_JUAL_PTS = reader.GetInt32(9);
                        item.QTY_OUT_PINJAM = reader.GetInt32(10);
                        item.QTY_ADJ = reader.GetInt32(11);
                        item.QTY_OPNM = reader.GetInt32(12);
                        item.SLD_AKHIR = reader.GetInt32(13);
                        item.ADJ_GIT = reader.GetInt32(14);
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

        public string InsSldPeriode(DateTime tglAwal, DateTime tglAkhir, string Brcode, string kode)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conn);
            try
            {//@tglMulai date, @tglCutOff date, @fBulan varchar(5), @dateStart date, @dateEnd date, @createdBy varchar(50)
                using (SqlCommand command = new SqlCommand("Usp_InsSldPeriode", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@tglAwal", tglAwal));
                    command.Parameters.Add(new SqlParameter("@tglAkhir", tglAkhir));
                    command.Parameters.Add(new SqlParameter("@barcode", Brcode));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                   
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

        public string InsLoopSldPeriode(DateTime tglAwal, DateTime tglAkhir)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conn);
            try
            {//@tglMulai date, @tglCutOff date, @fBulan varchar(5), @dateStart date, @dateEnd date, @createdBy varchar(50)
                using (SqlCommand command = new SqlCommand("Usp_LoopInsSldPeriode", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@tglAwal", tglAwal));
                    command.Parameters.Add(new SqlParameter("@tglAkhir", tglAkhir));
                    command.CommandTimeout = 3600;

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


        public string getFblnSldPeriode(string where)
        {
            string res = "";
            SqlConnection Connection = new SqlConnection(conn);
            try
            {
                string query = String.Format("SELECT FBLN FROM SLD_PERIODE {0} ",where);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    res = command.ExecuteScalar().ToString();
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
        public string getMaxFblnSldPeriode(string where)
        {
            string res = "";
            SqlConnection Connection = new SqlConnection(conn);
            try
            {
                string query = String.Format("SELECT MAX(FBLN) FROM SLD_PERIODE {0} ", where);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    res = command.ExecuteScalar().ToString();
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

        public List<SLD_PERIODE> getListSldPeriode(string where)
        {
            List<SLD_PERIODE> listTemp = new List<SLD_PERIODE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conn);

                using (SqlCommand command = new SqlCommand(string.Format("select KODE,BARCODE,SLD_AWAL,QTY_BELI,QTY_TERIMA,QTY_RTR_PTS,QTY_IN_PINJAM,QTY_KIRIM,QTY_JUAL,QTY_JUAL_PTS,QTY_OUT_PINJAM,QTY_ADJ,QTY_OPNM,SLD_AKHIR,ADJ_GIT,FBLN " +
                "from SLD_PERIODE {0}",where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SLD_PERIODE item = new SLD_PERIODE();
                        item.KODE = reader.GetString(0);
                        item.BARCODE = reader.GetString(1);
                        item.SLD_AWAl = reader.GetInt32(2);
                        item.QTY_BELI = reader.GetInt32(3);
                        item.QTY_TERIMA = reader.GetInt32(4);
                        item.QTY_RTR_PTS = reader.GetInt32(5);
                        item.QTY_IN_PINJAM = reader.GetInt32(6);
                        item.QTY_KIRIM = reader.GetInt32(7);
                        item.QTY_JUAL = reader.GetInt32(8);
                        item.QTY_JUAL_PTS = reader.GetInt32(9);
                        item.QTY_OUT_PINJAM = reader.GetInt32(10);
                        item.QTY_ADJ = reader.GetInt32(11);
                        item.QTY_OPNM = reader.GetInt32(12);
                        item.SLD_AKHIR = reader.GetInt32(13);
                        item.ADJ_GIT = reader.GetInt32(14);
                        item.FBLN = reader.GetString(15);
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

    }
}