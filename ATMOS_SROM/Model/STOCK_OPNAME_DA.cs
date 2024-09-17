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
    public class STOCK_OPNAME_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<TEMP_SO_DETAIL> getCompare(string where, DateTime dtCut, string kode, string noBukti)
        {
            List<TEMP_SO_DETAIL> listTempSO = new List<TEMP_SO_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                //using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, ID_KDBRG, ID_STOCK, NO_BUKTI, RAK, ITEM_CODE, BARCODE, DIFF_STOCK, " +
                //    " STOCK, IDSTOCK, ITEM_CODE_STOCK, BARCODE_STOCK, WAREHOUSE, KODE, QTY_STOCK, STOCK_RAK, ART_DESC, WARNA, SIZE " +
                    //" from tf_newStockCutOffTotal(@dateCutOff, @kode, @noBukti) {0}", where), Connection))
                    //" from vw_tempStock {0}", where), Connection))
                using (SqlCommand command = new SqlCommand(string.Format("Select ID_HEADER, ID_KDBRG, NO_BUKTI, BARCODE, DIFF_STOCK, " +
                    " STOCK, IDSTOCK, BARCODE_STOCK, WAREHOUSE, KODE, QTY_STOCK, ART_DESC, WARNA, SIZE, BRAND " +
                    " from tf_newStockCutOffTotal(@dateCutOff, @kode, @noBukti) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@dateCutOff", dtCut));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    command.Parameters.Add(new SqlParameter("@noBukti", noBukti));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_SO_DETAIL item = new TEMP_SO_DETAIL();
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(0);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(1);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(2);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(3);
                        item.DIFF_STOCK = reader.IsDBNull(reader.GetOrdinal("DIFF_STOCK")) ? 0 : reader.GetInt32(4);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(5);
                        item.IDSTOCK = reader.IsDBNull(reader.GetOrdinal("IDSTOCK")) ? 0 : reader.GetInt64(6);
                        item.BARCODE_STOCK = reader.IsDBNull(reader.GetOrdinal("BARCODE_STOCK")) ? "" : reader.GetString(7);
                        item.WAREHOUSE = reader.IsDBNull(reader.GetOrdinal("WAREHOUSE")) ? "" : reader.GetString(8);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(9);
                        item.QTY_STOCK = reader.IsDBNull(reader.GetOrdinal("QTY_STOCK")) ? 0 : reader.GetInt32(10);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(11);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(12);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(13);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(14);

                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        public List<TR_OP_HEADER> getOPHeader(string where)
        {
            List<TR_OP_HEADER> listTempSO = new List<TR_OP_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_BUKTI, KODE_MOVE, DARI, KE, KODE_DARI, KODE_KE, WAKTU_KIRIM, WAKTU_TERIMA," +
                    " DONE_TIME, STATUS, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS_TRF " +
                    " from TR_OP_HEADER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_OP_HEADER item = new TR_OP_HEADER();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(1);
                        item.KODE_MOVE = reader.IsDBNull(reader.GetOrdinal("KODE_MOVE")) ? "" : reader.GetString(2);
                        item.DARI = reader.IsDBNull(reader.GetOrdinal("DARI")) ? "" : reader.GetString(3);
                        item.KE = reader.IsDBNull(reader.GetOrdinal("KE")) ? "" : reader.GetString(4);
                        item.KODE_DARI = reader.IsDBNull(reader.GetOrdinal("KODE_DARI")) ? "" : reader.GetString(5);
                        item.KODE_KE = reader.IsDBNull(reader.GetOrdinal("KODE_KE")) ? "" : reader.GetString(6);
                        item.WAKTU_KIRIM = reader.IsDBNull(reader.GetOrdinal("WAKTU_KIRIM")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.WAKTU_TERIMA = reader.IsDBNull(reader.GetOrdinal("WAKTU_TERIMA")) ? (DateTime?)null : reader.GetDateTime(8);
                        item.DONE_TIME = reader.IsDBNull(reader.GetOrdinal("DONE_TIME")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(10);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(11);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(12);

                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(13);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.STATUS_TRF = reader.IsDBNull(reader.GetOrdinal("STATUS_TRF")) ? false : reader.GetBoolean(15);
                        
                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        public List<TR_OP_DETAIL> getOPDetail(string where)
        {
            List<TR_OP_DETAIL> listTempSO = new List<TR_OP_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, ID_KDBRG, NO_BUKTI, ITEM_CODE, QTY_KIRIM, QTY_TERIMA," +
                    " STOCK_AKHIR_KIRIM, STOCK_AKHIR_TERIMA, USER_KIRIM, USER_TERIMA, ALASAN, REFF " +
                    " from TR_OP_DETAIL {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_OP_DETAIL item = new TR_OP_DETAIL();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(1);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(2);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(3);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(4);
                        item.QTY_KIRIM = reader.IsDBNull(reader.GetOrdinal("QTY_KIRIM")) ? 0 : reader.GetInt32(5);
                        item.QTY_TERIMA = reader.IsDBNull(reader.GetOrdinal("QTY_TERIMA")) ? 0 : reader.GetInt32(6);
                        item.STOCK_AKHIR_KIRIM = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_KIRIM")) ? 0 : reader.GetInt32(7);
                        item.STOCK_AKHIR_TERIMA = reader.IsDBNull(reader.GetOrdinal("STOCK_AKHIR_TERIMA")) ? 0 : reader.GetInt32(8);
                        item.USER_KIRIM = reader.IsDBNull(reader.GetOrdinal("USER_KIRIM")) ? "" : reader.GetString(9);
                        item.USER_TERIMA = reader.IsDBNull(reader.GetOrdinal("USER_TERIMA")) ? "" : reader.GetString(10);
                        item.ALASAN = reader.IsDBNull(reader.GetOrdinal("ALASAN")) ? "" : reader.GetString(11);
                        item.REFF = reader.IsDBNull(reader.GetOrdinal("REFF")) ? "" : reader.GetString(12);

                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        public List<TEMP_SO_DETAIL> getTempOPDetail(string where)
        {
            List<TEMP_SO_DETAIL> listTempSO = new List<TEMP_SO_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, ID_KDBRG, ID_STOCK, NO_BUKTI, RAK, ITEM_CODE, BARCODE, BRAND, PRODUK, FGROUP, " +
                    " ART_DESC, WARNA, SIZE, DIFF_STOCK, STOCK, CREATED_BY, CREATED_DATE " +
                    " from TEMP_SO_DETAIL {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_SO_DETAIL item = new TEMP_SO_DETAIL();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(1);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(2);
                        item.ID_STOCK = reader.IsDBNull(reader.GetOrdinal("ID_STOCK")) ? 0 : reader.GetInt64(3);
                        item.NO_BUKTI = reader.IsDBNull(reader.GetOrdinal("NO_BUKTI")) ? "" : reader.GetString(4);
                        item.RAK = reader.IsDBNull(reader.GetOrdinal("RAK")) ? "" : reader.GetString(5);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(6);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(7);
                        item.BRAND = reader.IsDBNull(reader.GetOrdinal("BRAND")) ? "" : reader.GetString(8);
                        item.PRODUK = reader.IsDBNull(reader.GetOrdinal("PRODUK")) ? "" : reader.GetString(9);
                        item.FGROUP = reader.IsDBNull(reader.GetOrdinal("FGROUP")) ? "" : reader.GetString(10);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(11);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(12);

                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(13);
                        item.DIFF_STOCK = reader.IsDBNull(reader.GetOrdinal("DIFF_STOCK")) ? 0 : reader.GetInt32(14);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(15);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(16);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(17);

                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        public string insertTempSORetID(TEMP_SO tempSO)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_SO ( KE, KODE_KE, UPLOAD_TIME, STATUS, CREATED_BY, CREATED_DATE) values " +
                    " ( @ke, @kodeKe, @uploadTime, @status, @created_by, getdate() ); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@ke", SqlDbType.VarChar).Value = tempSO.KE;
                    command.Parameters.Add("@kodeKe", SqlDbType.VarChar).Value = tempSO.KODE_KE;
                    command.Parameters.Add("@uploadTime", SqlDbType.DateTime2).Value = tempSO.UPLOAD_TIME;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = tempSO.STATUS;
                    command.Parameters.Add("@created_by", SqlDbType.VarChar).Value = tempSO.CREATED_BY;

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

        public string updateNoBuktiTempSO(TEMP_SO tempSO)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE TEMP_SO set NO_BUKTI = @noBukti where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = tempSO.NO_BUKTI;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = tempSO.ID;

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

        public string updateTempSODetail(TEMP_SO_DETAIL tempSODetail)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TEMP_SO_DETAIL set ID_KDBRG = @idKdbrg, RAK = @rak, ITEM_CODE = @itemCode, " +
                    " BARCODE = @barcode, STOCK = @stock, ART_DESC = @artDesc, WARNA = @warna, SIZE = @size, FGROUP = @group, BRAND = @brand where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = tempSODetail.ID_KDBRG;
                    command.Parameters.Add("@rak", SqlDbType.VarChar).Value = tempSODetail.RAK;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = tempSODetail.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = tempSODetail.BARCODE;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = tempSODetail.STOCK;

                    command.Parameters.Add("@artDesc", SqlDbType.VarChar).Value = tempSODetail.ART_DESC;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = tempSODetail.BRAND;
                    command.Parameters.Add("@group", SqlDbType.VarChar).Value = tempSODetail.FGROUP;
                    command.Parameters.Add("@warna", SqlDbType.VarChar).Value = tempSODetail.WARNA;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = tempSODetail.SIZE;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = tempSODetail.ID;

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

        public string insertTempSoDetail(TEMP_SO_DETAIL tempSODetail)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_SO_DETAIL (ID_HEADER, ID_KDBRG, NO_BUKTI, RAK, ITEM_CODE, BARCODE, BRAND, PRODUK, ART_DESC, WARNA, SIZE, " +
                    " STOCK, CREATED_BY, CREATED_DATE) values " +
                    " (@idHeader, @idKdbrg, @noBukti, @rak, @itemCode, @barcode, @brand, @produk, @artDesc, @warna, @size, @stock, @createdBy, GETDATE()) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idHeader", SqlDbType.BigInt).Value = tempSODetail.ID_HEADER;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = tempSODetail.ID_KDBRG;
                    //command.Parameters.Add("@idStock", SqlDbType.BigInt).Value = tempSODetail.ID_STOCK;
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = tempSODetail.NO_BUKTI;
                    command.Parameters.Add("@rak", SqlDbType.VarChar).Value = tempSODetail.RAK;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = tempSODetail.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = tempSODetail.BARCODE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = tempSODetail.BRAND;
                    command.Parameters.Add("@produk", SqlDbType.VarChar).Value = tempSODetail.PRODUK;
                    command.Parameters.Add("@artDesc", SqlDbType.VarChar).Value = tempSODetail.ART_DESC;
                    command.Parameters.Add("@warna", SqlDbType.VarChar).Value = tempSODetail.WARNA;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = tempSODetail.SIZE;
                    //command.Parameters.Add("@diffStock", SqlDbType.Int).Value = tempSODetail.DIFF_STOCK;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = tempSODetail.STOCK;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = tempSODetail.CREATED_BY;

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

        public void deleteTempSO(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_SO where CREATED_BY = @name ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    command.ExecuteNonQuery();
                }

                query = String.Format("DELETE TEMP_SO_DETAIL where CREATED_BY = @name ");
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

        public void deleteTempSODetail(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_SO_DETAIL where CREATED_BY = @name ");
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

        public void deleteOneTempSODetail(Int64 id)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_SO_DETAIL where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
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

        public string insertOPHeaderRetID(Int64 idTempHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertOPHeader", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idTempHeader", idTempHeader));
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

        public string insertOPDetail(Int64 newIdHeader, Int64 idHeaderTemp)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertOPDetail", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idHeaderOP", idHeaderTemp));
                    command.Parameters.Add(new SqlParameter("@newIDHeader", newIdHeader));
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

        public string insertSOHeaderRetID(Int64 idTempHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertSOHeader", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idTempHeader", idTempHeader));
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

        public string insertSODetail(Int64 idHeader, Int64 idHeaderTemp, string createdBy, string kode, string warehouse, string noBukti, DateTime dateCutOff)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertDetailSO", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idHeader", idHeader));
                    command.Parameters.Add(new SqlParameter("@idHeaderTemp", idHeaderTemp));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    command.Parameters.Add(new SqlParameter("@warehouse", warehouse));
                    command.Parameters.Add(new SqlParameter("@noBukti", noBukti));
                    command.Parameters.Add(new SqlParameter("@dateCutOff", dateCutOff));
                    command.CommandTimeout = 3600;
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

        public List<EXCELL_ALL_RAK_TEMP_SO> getTempSO(string where)
        {
            List<EXCELL_ALL_RAK_TEMP_SO> listTempSO = new List<EXCELL_ALL_RAK_TEMP_SO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select RAK, SUM(STOCK) [STOCK] " +
                    " from TEMP_SO_DETAIL {0} group BY RAK", where), Connection))
                    //" from tf_stockCutOffTotal(@dateCutOff, @kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        EXCELL_ALL_RAK_TEMP_SO item = new EXCELL_ALL_RAK_TEMP_SO();
                        item.RAK = reader.IsDBNull(reader.GetOrdinal("RAK")) ? "" : reader.GetString(0);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(1);
                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        public List<EXCELL_RAK_TEMP_SO> getTempSOByRak(string where)
        {
            List<EXCELL_RAK_TEMP_SO> listTempSO = new List<EXCELL_RAK_TEMP_SO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select RAK, ITEM_CODE, BARCODE, ART_DESC, WARNA, SIZE, STOCK " +
                    " from TEMP_SO_DETAIL {0}", where), Connection))
                //" from tf_stockCutOffTotal(@dateCutOff, @kode) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        EXCELL_RAK_TEMP_SO item = new EXCELL_RAK_TEMP_SO();
                        item.RAK = reader.IsDBNull(reader.GetOrdinal("RAK")) ? "" : reader.GetString(0);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(1);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(2);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(3);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(4);
                        item.SIZE = reader.IsDBNull(reader.GetOrdinal("SIZE")) ? "" : reader.GetString(5);
                        item.STOCK = reader.IsDBNull(reader.GetOrdinal("STOCK")) ? 0 : reader.GetInt32(6);
                        listTempSO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listTempSO;
        }

        //Added By VAV
        public  int ExecUIDPortal(string pQuery, string[] pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Length == pValue.Length && pParam.Length > 0)
            {
                List<SqlParameter> l = new List<SqlParameter>();

                for (int i = 0; i <= pParam.Length - 1; i++)
                    l.Add(new SqlParameter(pParam[i].ToString(), pValue[i].ToString()));

                return ExecUIDPortal(pQuery, l, IsProc, pConnString);
            }
            else
                throw new Exception("Jumlah parameter harus sama dengan jumlah value");
        }

        public  int ExecUIDPortal(string pQuery, List<SqlParameter> pParam = null, bool IsProc = false, string pConnString = null)
        {
            int _affected_row = 0;
          //  string _conn_string = "";
            //if (pConnString == null || pConnString.Length <= 0)
            //    _conn_string = ConfigurationManager.ConnectionStrings("pricing_constr").ConnectionString.ToString();
            //else
            //    _conn_string = pConnString;

            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = IsProc ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = pQuery;

                    if (pParam != null && pParam.Count > 0)
                    {
                        foreach (SqlParameter p in pParam)
                            cmd.Parameters.Add(p);
                    }

                    conn.Open();
                    cmd.CommandTimeout = 20000;
                    _affected_row = cmd.ExecuteNonQuery();
                }
            }

            return _affected_row;
        }
    }
}
