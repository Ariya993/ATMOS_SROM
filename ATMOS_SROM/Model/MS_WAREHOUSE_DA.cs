using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using ATMOS_SROM.Domain;
using System.Data;

namespace ATMOS_SROM.Model
{
    public class MS_WAREHOUSE_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<TEMP_KDBRG> getTempTrf(string where)
        {
            List<TEMP_KDBRG> listTemp = new List<TEMP_KDBRG>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ID_HEADER, ID_SHOWROOM, KODE, SHOWROOM, BARCODE, ITEM_CODE, " +
                    " QTY, CREATED_BY, CREATED_DATE, FLAG, STAT, BRAND, ART_DESC, FCOLOR, FSIZE " +
                    " from TEMP_KDBRG {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_KDBRG item = new TEMP_KDBRG();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(1);
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(2);
                        item.ID_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("ID_SHOWROOM")) ? 0 : reader.GetInt64(3);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(4);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(5);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(6);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(7);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(8);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(9);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(10);
                        item.FLAG = reader.IsDBNull(reader.GetOrdinal("FLAG")) ? "" : reader.GetString(11);
                        item.STAT = reader.IsDBNull(reader.GetOrdinal("STAT")) ? "" : reader.GetString(12);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(13);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(14);
                        item.FCOLOR = reader.IsDBNull(reader.GetOrdinal("FCOLOR")) ? "" : reader.GetString(15);
                        item.FSIZE = reader.IsDBNull(reader.GetOrdinal("FSIZE")) ? "" : reader.GetString(16);

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

        public List<TRF_PINJAM_HEADER> getPinjamHeader(string where)
        {
            List<TRF_PINJAM_HEADER> listPinjamHeader = new List<TRF_PINJAM_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_BUKTI, DARI, KE, KODE_DARI, KODE_KE, NAMA, PHONE, EMAIL, WAKTU_KIRIM, WAKTU_KEMBALI, " +
                    " WAKTU_SELESAI, STATUS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS_PINJAM, TOTAL_KIRIM, TOTAL_KEMBALI " +
                    " from TRF_PINJAM_HEADER {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TRF_PINJAM_HEADER item = new TRF_PINJAM_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(1);
                        item.DARI = reader.IsDBNull(reader.GetOrdinal("DARI")) ? "" : reader.GetString(2);
                        item.KE = reader.IsDBNull(reader.GetOrdinal("KE")) ? "" : reader.GetString(3);
                        item.KODE_DARI = reader.IsDBNull(reader.GetOrdinal("KODE_DARI")) ? "" : reader.GetString(4);
                        item.KODE_KE = reader.IsDBNull(reader.GetOrdinal("KODE_KE")) ? "" : reader.GetString(5);
                        item.NAMA = reader.IsDBNull(reader.GetOrdinal("NAMA")) ? "" : reader.GetString(6);
                        item.PHONE = reader.IsDBNull(reader.GetOrdinal("PHONE")) ? "" : reader.GetString(7);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(8);
                        item.WAKTU_KIRIM = reader.IsDBNull(reader.GetOrdinal("WAKTU_KIRIM")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.WAKTU_KEMBALI = reader.IsDBNull(reader.GetOrdinal("WAKTU_KEMBALI")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.WAKTU_SELESAI = reader.IsDBNull(reader.GetOrdinal("WAKTU_SELESAI")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(12);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(13);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(15);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(16);

                        item.STATUS_PINJAM = reader.IsDBNull(reader.GetOrdinal("STATUS_PINJAM")) ? false : reader.GetBoolean(17);
                        item.TOTAL_KIRIM = reader.IsDBNull(reader.GetOrdinal("TOTAL_KIRIM")) ? 0 : reader.GetInt32(18);
                        item.TOTAL_KEMBALI = reader.IsDBNull(reader.GetOrdinal("TOTAL_KEMBALI")) ? 0 : reader.GetInt32(19);

                        listPinjamHeader.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPinjamHeader;
        }

        public List<TRF_PINJAM_DETAIL> getPinjamDetail(string where)
        {
            List<TRF_PINJAM_DETAIL> listPinjamDetail = new List<TRF_PINJAM_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, ID_KDBRG, NO_BUKTI, BARCODE, ITEM_CODE, QTY_KIRIM, QTY_TERIMA, " +
                    " STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA, ALASAN, REFF, FLAG, BRAND, FART_DESC, COLOR, SIZE" +
                    " from vw_pinjamDetail {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TRF_PINJAM_DETAIL item = new TRF_PINJAM_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(1);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(2);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(3);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(4);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(5);
                        item.QTY_KIRIM = reader.IsDBNull(reader.GetOrdinal("QTY_KIRIM")) ? 0 : reader.GetInt32(6);
                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(7);
                        item.STOCK_AKHIR_KIRIM = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_KIRIM")) ? 0 : reader.GetInt32(8);
                        item.STOCK_AKHIR_TERIMA = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_TERIMA")) ? 0 : reader.GetInt32(9);
                        item.USER_KIRIM = reader.IsDBNull(reader.GetOrdinal("USER_KIRIM")) ? "" : reader.GetString(10);
                        item.USER_TERIMA = reader.IsDBNull(reader.GetOrdinal("USER_TERIMA")) ? "" : reader.GetString(11);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(12);
                        item.REFF = reader.IsDBNull(reader.GetOrdinal("REFF")) ? "" : reader.GetString(13);
                        item.FLAG = reader.IsDBNull(reader.GetOrdinal("FLAG")) ? "" : reader.GetString(14);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(15);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(16);

                        item.COLOR = reader.IsDBNull(reader.GetOrdinal("COLOR")) ? "" : reader.GetString(17);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(18);

                        listPinjamDetail.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPinjamDetail;
        }

        public string deleteTempKdbrg(string user)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE from TEMP_KDBRG where CREATED_BY = '{0}' ", user);
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.ExecuteScalar();
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

        public string insertTempTrfStock(TEMP_KDBRG tempKdbrg)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_KDBRG (ID_KDBRG, ID_HEADER, ID_SHOWROOM, KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ART_DESC, FCOLOR, FSIZE, QTY, FLAG, STAT, CREATED_BY, CREATED_DATE) values " +
                    " (@idKdbrg, @idHeader, @idShow, @kode, @show, @barcode, @itemCode, @brand, @desc, @color, @size, @qty, @flag, @stat, @createdBy, GETDATE()); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = tempKdbrg.ID_KDBRG;
                    command.Parameters.Add("@idHeader", SqlDbType.BigInt).Value = tempKdbrg.ID_HEADER;
                    command.Parameters.Add("@idShow", SqlDbType.BigInt).Value = tempKdbrg.ID_SHOWROOM;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = tempKdbrg.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = tempKdbrg.SHOWROOM;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = tempKdbrg.BARCODE;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = tempKdbrg.ITEM_CODE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = tempKdbrg.BRAND;
                    command.Parameters.Add("@desc", SqlDbType.VarChar).Value = tempKdbrg.ART_DESC;
                    command.Parameters.Add("@color", SqlDbType.VarChar).Value = tempKdbrg.FCOLOR;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = tempKdbrg.FSIZE;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = tempKdbrg.QTY;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = tempKdbrg.FLAG;
                    command.Parameters.Add("@stat", SqlDbType.VarChar).Value = tempKdbrg.STAT;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = tempKdbrg.CREATED_BY;

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

        public string updateTempTrfStock(TEMP_KDBRG tempKdbrg)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_KDBRG set QTY = QTY + @qty where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = tempKdbrg.ID;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = tempKdbrg.QTY;

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

        public string insertTRFStock(string user, string kodeMove)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertTrfStock", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@createdBy", user));
                    command.Parameters.Add(new SqlParameter("@kodeMove", kodeMove));
                    Connection.Open();

                    //newId = command.ExecuteScalar().ToString();
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
        public string insertTRFStockNEW(string user, string kodeMove, String NoBukti, Int64 idHeader, DateTime tglKirim, string status)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertTrfStockNEW", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@createdBy", user));
                    command.Parameters.Add(new SqlParameter("@kodeMove", kodeMove));
                    command.Parameters.Add(new SqlParameter("@NoBukti", NoBukti));
                    command.Parameters.Add(new SqlParameter("@idHeader", idHeader));
                    command.Parameters.Add(new SqlParameter("@tglKirim", tglKirim));
                    command.Parameters.Add(new SqlParameter("@status", status));

                    Connection.Open();

                    //newId = command.ExecuteScalar().ToString();
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

        public string insertPinjamHeaderRetID(TRF_PINJAM_HEADER pinjamHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_PINJAM_HEADER " +
                    " ( NO_BUKTI, DARI, KE, KODE_DARI, KODE_KE, NAMA, PHONE, EMAIL, WAKTU_KIRIM, WAKTU_KEMBALI, STATUS, CREATED_BY, CREATED_DATE, STATUS_PINJAM ) values " +
                    " ( @noBukti, @dari, @ke, @kodeDari, @kodeKe, @nama, @phone, @email, @waktuKirim, @waktuKembali, @status, @createdBy, GETDATE(), 1 ); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = pinjamHeader.NO_BUKTI;
                    command.Parameters.Add("@dari", SqlDbType.VarChar).Value = pinjamHeader.DARI;
                    command.Parameters.Add("@ke", SqlDbType.VarChar).Value = pinjamHeader.KE;
                    command.Parameters.Add("@kodeDari", SqlDbType.VarChar).Value = pinjamHeader.KODE_DARI;
                    command.Parameters.Add("@kodeKe", SqlDbType.VarChar).Value = pinjamHeader.KODE_KE;
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = pinjamHeader.NAMA;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = pinjamHeader.PHONE;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = pinjamHeader.EMAIL;
                    command.Parameters.Add("@waktuKirim", SqlDbType.DateTime2).Value = pinjamHeader.WAKTU_KIRIM;
                    command.Parameters.Add("@waktuKembali", SqlDbType.DateTime2).Value = pinjamHeader.WAKTU_KEMBALI;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = pinjamHeader.STATUS;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = pinjamHeader.CREATED_BY;

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

        public string insertPinjamDetail(TRF_PINJAM_HEADER pinjamHeader, int selectedIndex)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertPinjamDetail", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idHeader", pinjamHeader.ID));
                    command.Parameters.Add(new SqlParameter("@noBukti", pinjamHeader.NO_BUKTI));
                    command.Parameters.Add(new SqlParameter("@createdBy", pinjamHeader.CREATED_BY));
                    command.Parameters.Add(new SqlParameter("@index", selectedIndex));
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

        public string updatePinjamDetail(TRF_PINJAM_DETAIL pinjamDetail)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_PINJAM_DETAIL set QTY_TERIMA = @qty, STOCK_AKHIR_TERIMA = @stock, USER_TERIMA = @user, " +
                    "ALASAN = @alasan, FLAG = @flag where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = pinjamDetail.QTY_TERIMA;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = pinjamDetail.STOCK_AKHIR_TERIMA;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = pinjamDetail.USER_TERIMA;
                    command.Parameters.Add("@alasan", SqlDbType.VarChar).Value = pinjamDetail.ALASAN;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = pinjamDetail.FLAG;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = pinjamDetail.ID;

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

        public string updateWaktuSelesaiPinjamHeader(TRF_PINJAM_HEADER pinjamHeader)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_PINJAM_HEADER set WAKTU_SELESAI = @selesai, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@selesai", SqlDbType.DateTime2).Value = pinjamHeader.WAKTU_SELESAI;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = pinjamHeader.UPDATED_BY;
                   
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = pinjamHeader.ID;

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