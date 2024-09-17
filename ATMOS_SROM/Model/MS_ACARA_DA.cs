using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using ATMOS_SROM.Domain.CustomObj;
using System.Data.OleDb;

namespace ATMOS_SROM.Model
{
    public class MS_ACARA_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_ACARA> getAcara(string where)
        {
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_ACARA_STATUS, NO_URUT, ACARA_VALUE, ACARA_STATUS, DISC, NAMA_ACARA, " +
                    " ACARA_DESC, START_DATE, END_DATE, STATUS_ACARA, ARTICLE, KODE, SHOWROOM, DESC_DISC, SPCL_PRICE, MIN_PURCHASE " +
                    " from vw_acara {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA item = new MS_ACARA();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_ACARA_STATUS = reader.GetInt64(2);
                        item.NO_URUT = reader.GetInt32(3);
                        item.ACARA_VALUE = reader.IsDBNull(reader.GetOrdinal("ACARA_VALUE")) ? "" : reader.GetString(4);
                        item.ACARA_STATUS = reader.IsDBNull(reader.GetOrdinal("ACARA_STATUS")) ? "" : reader.GetString(5);
                        item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetDecimal(6);
                        //item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetInt32(6);
                        item.NAMA_ACARA = reader.IsDBNull(reader.GetOrdinal("NAMA_ACARA")) ? "" : reader.GetString(7);
                        item.ACARA_DESC = reader.IsDBNull(reader.GetOrdinal("ACARA_DESC")) ? "" : reader.GetString(8);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(10);

                        item.STATUS_ACARA = reader.IsDBNull(reader.GetOrdinal("STATUS_ACARA")) ? false : reader.GetBoolean(11);
                        item.ARTICLE = reader.IsDBNull(reader.GetOrdinal("ARTICLE")) ? "" : reader.GetString(12);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(13);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(14);
                        item.DESC_DISC = reader.IsDBNull(reader.GetOrdinal("DESC_DISC")) ? "" : reader.GetString(15);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(16);
                        //Tambahan
                        item.MIN_PURCHASE = reader.IsDBNull(reader.GetOrdinal("MIN_PURCHASE")) ? 0 : reader.GetDecimal(17);
                        listAcara.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAcara;
        }

        public List<MS_ACARA> getvw_acaraGrid(string where)
        {
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_ACARA_STATUS, NO_URUT, ACARA_VALUE, ACARA_STATUS, DISC, NAMA_ACARA, " +
                    " ACARA_DESC, START_DATE, END_DATE, STATUS_ACARA, ARTICLE, KODE, SHOWROOM, DESC_DISC, SPCL_PRICE, MIN_PURCHASE " +
                    " from vw_acaraGrid {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA item = new MS_ACARA();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_ACARA_STATUS = reader.GetInt64(2);
                        item.NO_URUT = reader.GetInt32(3);
                        item.ACARA_VALUE = reader.IsDBNull(reader.GetOrdinal("ACARA_VALUE")) ? "" : reader.GetString(4);
                        item.ACARA_STATUS = reader.IsDBNull(reader.GetOrdinal("ACARA_STATUS")) ? "" : reader.GetString(5);
                        //item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetInt32(6);
                        item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetDecimal(6);

                        item.NAMA_ACARA = reader.IsDBNull(reader.GetOrdinal("NAMA_ACARA")) ? "" : reader.GetString(7);
                        item.ACARA_DESC = reader.IsDBNull(reader.GetOrdinal("ACARA_DESC")) ? "" : reader.GetString(8);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(10);

                        item.STATUS_ACARA = reader.IsDBNull(reader.GetOrdinal("STATUS_ACARA")) ? false : reader.GetBoolean(11);
                        item.ARTICLE = reader.IsDBNull(reader.GetOrdinal("ARTICLE")) ? "" : reader.GetString(12);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(13);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(14);
                        item.DESC_DISC = reader.IsDBNull(reader.GetOrdinal("DESC_DISC")) ? "" : reader.GetString(15);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(16);
                        //Tambahan
                        item.MIN_PURCHASE = reader.IsDBNull(reader.GetOrdinal("MIN_PURCHASE")) ? 0 : reader.GetDecimal(17);
                        listAcara.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAcara;
        }
        
        public List<MS_ACARA_STATUS> getAcaraStatus(string where)
        {
            List<MS_ACARA_STATUS> listGroup = new List<MS_ACARA_STATUS>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ACARA_STATUS, DESC_DISC, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS " +
                    " from MS_ACARA_STATUS {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA_STATUS item = new MS_ACARA_STATUS();
                        item.ID = reader.GetInt64(0);
                        item.ACARA_STATUS = reader.GetString(1);
                        item.DESC_DISC = reader.GetString(2);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(3);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(4);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(5);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(7);

                        listGroup.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listGroup;
        }

        public List<MS_ACARA> getAcaraShowroom(string where)
        {
            List<MS_ACARA> listGroup = new List<MS_ACARA>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_SHOWROOM, ACARA_VALUE, KODE, SHOWROOM, CREATED_BY, CREATED_DATE, UPDATED_BY, " +
                    " UPDATED_DATE, STATUS, NAMA_ACARA, ACARA_DESC, START_DATE, END_DATE, STATUS_ACARA " +
                    " from vw_acaraShowroom {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA item = new MS_ACARA();
                        item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt64(0);
                        item.ID_ACARA = reader.IsDBNull(reader.GetOrdinal("ID_ACARA")) ? 0 : reader.GetInt64(1);
                        item.ID_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("ID_SHOWROOM")) ? 0 : reader.GetInt64(2);
                        item.ACARA_VALUE = reader.IsDBNull(reader.GetOrdinal("ACARA_VALUE")) ? "" : reader.GetString(3);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(4);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(5);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(6);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(7);

                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(8);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(10);
                        item.NAMA_ACARA = reader.IsDBNull(reader.GetOrdinal("NAMA_ACARA")) ? "" : reader.GetString(11);
                        item.ACARA_DESC = reader.IsDBNull(reader.GetOrdinal("ACARA_DESC")) ? "" : reader.GetString(12);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? DateTime.Now : reader.GetDateTime(13);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.STATUS_ACARA = reader.IsDBNull(reader.GetOrdinal("STATUS_ACARA")) ? false : reader.GetBoolean(15);

                        listGroup.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listGroup;
        }

        public List<MS_ACARA> getAcaraGroupBy(string where, DateTime tglTrans)
        {
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID_ACARA, ACARA_VALUE, NAMA_ACARA " +
                    " from vw_acara where @date between START_DATE and ISNULL(END_DATE,GETDATE()) {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@date", SqlDbType.Date).Value = tglTrans;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA item = new MS_ACARA();
                        item.ID_ACARA = reader.GetInt64(0);
                        item.ACARA_VALUE = reader.GetString(1);
                        item.NAMA_ACARA = reader.GetString(2);
                        
                        listAcara.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAcara;
        }

        public List<MS_ITEM_ACARA> getItemAcara(string where)
        {
            List<MS_ITEM_ACARA> listItemAcara = new List<MS_ITEM_ACARA>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS_ACARA, ITEM_DESC, BARCODE " +
                    " from vw_itemAcara {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ITEM_ACARA item = new MS_ITEM_ACARA();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.VALUE_ACARA = reader.GetString(3);
                        item.ITEM_CODE = reader.GetString(4);
                        item.CREATED_BY = reader.GetString(5);
                        item.CREATED_DATE = reader.GetDateTime(6);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(7);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?) null : reader.GetDateTime(8);
                        item.STATUS_ACARA = reader.IsDBNull(reader.GetOrdinal("STATUS_ACARA")) ? false : reader.GetBoolean(9);
                        item.ITEM_DESC = reader.GetString(10);
                        item.BARCODE = reader.GetString(11);

                        listItemAcara.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listItemAcara;
        }

        public string insertMsAcara(MS_ACARA acara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ACARA ( NAMA_ACARA, ACARA_DESC, ACARA_VALUE, START_DATE, END_DATE, CREATED_BY, CREATED_DATE, STATUS_ACARA, ARTICLE ) values " +
                    " (@namaAcara, @desc, @value, @start, @end, @createdBy, getdate(), 1, @article); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@namaAcara", SqlDbType.VarChar).Value = acara.NAMA_ACARA;
                    command.Parameters.Add("@desc", SqlDbType.VarChar).Value = acara.ACARA_DESC;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = acara.ACARA_VALUE;
                    command.Parameters.Add("@start", SqlDbType.DateTime2).Value = acara.START_DATE;
                    command.Parameters.Add("@end", SqlDbType.DateTime2).Value = acara.END_DATE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = acara.CREATED_BY;
                    command.Parameters.Add("@article", SqlDbType.VarChar).Value = acara.ARTICLE;
                    
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

        public string insertMsAcaraDisc(MS_ACARA_DISC acara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ACARA_DISC ( ID_ACARA, ID_ACARA_STATUS, NO_URUT, ACARA_VALUE, ACARA_STATUS, DISC, SPCL_PRICE, CREATED_BY, CREATED_DATE, STATUS, MIN_PURCHASE ) values " +
                    " (@idAcara, @idAcaraStatus, @noUrut, @value, @status, @disc, @price, @createdBy, getdate(), 1, @MinPrchs) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = acara.ID_ACARA;
                    command.Parameters.Add("@idAcaraStatus", SqlDbType.BigInt).Value = acara.ID_ACARA_STATUS;
                    command.Parameters.Add("@noUrut", SqlDbType.Int).Value = acara.NO_URUT;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = acara.ACARA_VALUE; //temp.DISC_PRICE == null ? (object)Convert.DBNull : temp.DISC_PRICE;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = acara.ACARA_STATUS;
                    command.Parameters.Add("@disc", SqlDbType.Decimal).Value = acara.DISC == null ? (object)Convert.DBNull : acara.DISC;
                    //command.Parameters.Add("@disc", SqlDbType.Int).Value = acara.DISC == null ? (object)Convert.DBNull : acara.DISC;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = acara.SPCL_PRICE == null ? (object)Convert.DBNull : acara.SPCL_PRICE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = acara.CREATED_BY;
                    command.Parameters.Add("@MinPrchs", SqlDbType.Decimal).Value = acara.MIN_PURCHASE;
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

        public string insertMsAcaraShow(MS_ACARA_SHOWROOM acara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ACARA_SHOWROOM ( ID_ACARA, ID_SHOWROOM, ACARA_VALUE, KODE, SHOWROOM, CREATED_BY, CREATED_DATE, STATUS ) values " +
                    " (@idAcara, @idShowroom, @value, @kode, @show, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = acara.ID_ACARA;
                    command.Parameters.Add("@idShowroom", SqlDbType.BigInt).Value = acara.ID_SHOWROOM;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = acara.ACARA_VALUE;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = acara.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = acara.SHOWROOM;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = acara.CREATED_BY;

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

        public string insertMsItemAcara(MS_ITEM_ACARA itemAcara)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ITEM_ACARA ( ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, STATUS_ACARA, BARCODE ) values " +
                    " (@idAcara, @idKdbrg, @value, @itemCode, @createdBy, getdate(), 1, @barcode) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.VarChar).Value = itemAcara.ID_ACARA;
                    command.Parameters.Add("@idKdbrg", SqlDbType.VarChar).Value = itemAcara.ID_KDBRG;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = itemAcara.VALUE_ACARA;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = itemAcara.ITEM_CODE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = itemAcara.CREATED_BY;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = itemAcara.BARCODE;

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
        
        public string deleteMsAcara(MS_ACARA acara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_ACARA set STATUS_ACARA = 0 where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = acara.ID;

                    command.ExecuteScalar().ToString();
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
        public string SoftdeleteMsItemAcara(Int64 ID)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_ITEM_ACARA set STATUS_ACARA = 0 where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = ID;

                    command.ExecuteScalar().ToString();
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
        public string SoftdeleteMsItemGWPPWPAcara(Int64 ID)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_ITEM_GWP_PWP set STATUS_ACARA = 0 where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = ID;

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
        public List<MS_VOUCHER> getListVoucher(string where)
        {
            List<MS_VOUCHER> listVoucher = new List<MS_VOUCHER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, NO_VOUCHER, NILAI, JENIS, VALID_FROM, VALID_UNTIL, STATUS_VOUCHER, " +
                    " CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS, KODE, KODE_CREATED " +
                    " from MS_VOUCHER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_VOUCHER item = new MS_VOUCHER();

                        item.ID = reader.GetInt64(0);
                        item.NO_VOUCHER = reader.IsDBNull(reader.GetOrdinal("NO_VOUCHER")) ? "" : reader.GetString(1);
                        item.NILAI = reader.IsDBNull(reader.GetOrdinal("NILAI")) ? 0 : reader.GetDecimal(2);
                        item.JENIS = reader.IsDBNull(reader.GetOrdinal("JENIS")) ? "" : reader.GetString(3);
                        item.VALID_FROM = reader.IsDBNull(reader.GetOrdinal("VALID_FROM")) ? DateTime.Now : reader.GetDateTime(4);
                        item.VALID_UNTIL = reader.IsDBNull(reader.GetOrdinal("VALID_UNTIL")) ? (DateTime?)null : reader.GetDateTime(5);
                        item.STATUS_VOUCHER = reader.IsDBNull(reader.GetOrdinal("STATUS_VOUCHER")) ? "" : reader.GetString(6);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(7);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(8);

                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(9);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(11);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(12);
                        item.KODE_CREATED = reader.IsDBNull(reader.GetOrdinal("KODE_CREATED")) ? (DateTime?)null : reader.GetDateTime(13);

                        listVoucher.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listVoucher;
        }
        public DataSet GeneratVoucher(String where)
        {
            // List<MS_CBYR> listitem = new List<MS_CBYR>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {

                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, NO_VOUCHER, NILAI, JENIS, VALID_FROM, VALID_UNTIL, STATUS_VOUCHER, " +
                    " CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, KODE, KODE_CREATED " +
                    " from MS_VOUCHER {0}", where), Connection))
                
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }
        public string insertMsVoucher(MS_VOUCHER msVoucher)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_VOUCHER (NO_VOUCHER, NILAI, JENIS, VALID_FROM, VALID_UNTIL, STATUS_VOUCHER, CREATED_BY, CREATED_DATE, STATUS) values " +
                    " (@noVoucher, @nilai, @jenis, @from, @until, @statusVoucher, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noVoucher", SqlDbType.VarChar).Value = msVoucher.NO_VOUCHER;
                    command.Parameters.Add("@nilai", SqlDbType.Decimal).Value = msVoucher.NILAI;
                    command.Parameters.Add("@jenis", SqlDbType.VarChar).Value = msVoucher.JENIS;
                    command.Parameters.Add("@from", SqlDbType.Date).Value = msVoucher.VALID_FROM;
                    command.Parameters.Add("@until", SqlDbType.Date).Value = msVoucher.VALID_UNTIL;
                    command.Parameters.Add("@statusVoucher", SqlDbType.VarChar).Value = msVoucher.STATUS_VOUCHER;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = msVoucher.CREATED_BY;

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

        public string updateMsVoucher(MS_VOUCHER msVoucher)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_VOUCHER SET NO_VOUCHER = @noVoucher, NILAI = @nilai, JENIS = @jenis, " +
                    " VALID_FROM = @from, VALID_UNTIL = @until, STATUS_VOUCHER = @status, UPDATED_BY = @updatedBy, UPDATED_DATE = getdate() where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noVoucher", SqlDbType.VarChar).Value = msVoucher.NO_VOUCHER;
                    command.Parameters.Add("@nilai", SqlDbType.Decimal).Value = msVoucher.NILAI;
                    command.Parameters.Add("@jenis", SqlDbType.VarChar).Value = msVoucher.JENIS;
                    command.Parameters.Add("@from", SqlDbType.Date).Value = msVoucher.VALID_FROM;
                    command.Parameters.Add("@until", SqlDbType.Date).Value = msVoucher.VALID_UNTIL;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = msVoucher.STATUS_VOUCHER;
                    command.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = msVoucher.UPDATED_BY;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = msVoucher.ID;

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

        public string clearTempVoucher(string uName)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Delete from TEMP_VOUCHER where CREATED_BY = @createdBy");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = uName;
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

        public string insertTempVoucher(MS_VOUCHER msVoucher)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_VOUCHER (NO_VOUCHER, NILAI, JENIS, VALID_FROM, VALID_UNTIL, CREATED_BY, CREATED_DATE) values " +
                    " (@noVoucher, @nilai, @jenis, @from, @until, @createdBy, getdate()) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noVoucher", SqlDbType.VarChar).Value = msVoucher.NO_VOUCHER;
                    command.Parameters.Add("@nilai", SqlDbType.Decimal).Value = msVoucher.NILAI;
                    command.Parameters.Add("@jenis", SqlDbType.VarChar).Value = msVoucher.JENIS;
                    command.Parameters.Add("@from", SqlDbType.Date).Value = msVoucher.VALID_FROM;
                    command.Parameters.Add("@until", SqlDbType.Date).Value = msVoucher.VALID_UNTIL;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = msVoucher.CREATED_BY;

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

        public string insertVoucherFromTemp(string uName)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertVoucher", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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

        //Tambahan Untuk Acara Bertingkat 
        public int CountAcaraDisc(String Where)
        {
            int jml = 0;
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                string query = String.Format("SELECT COUNT(*) FROM MS_ACARA_DISC {0} ", Where);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    jml = Convert.ToInt32(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jml;
        }

        public List<MS_ACARA_DISC> GetListMsAcaraDisc(String where)
        {
            List<MS_ACARA_DISC> listitem = new List<MS_ACARA_DISC>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_ACARA_STATUS, NO_URUT, ACARA_VALUE, ACARA_STATUS, DISC, SPCL_PRICE" +
                    " from MS_ACARA_DISC {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA_DISC item = new MS_ACARA_DISC();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_ACARA_STATUS = reader.GetInt64(2);
                        item.NO_URUT = reader.GetInt32(3);
                        item.ACARA_VALUE = reader.IsDBNull(reader.GetOrdinal("ACARA_VALUE")) ? "" : reader.GetString(4);
                        item.ACARA_STATUS = reader.IsDBNull(reader.GetOrdinal("ACARA_STATUS")) ? "" : reader.GetString(5);
                        item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetDecimal(6);
                        //item.DISC = reader.IsDBNull(reader.GetOrdinal("DISC")) ? 0 : reader.GetInt32(6);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(7);
                        listitem.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listitem;
        }

        //Tambahan untuk Add Showroom untuk ACara Berjalan
        public List<MS_ACARA_SHOWROOM> GetListMsAcaraShowroom(String where)
        {
            List<MS_ACARA_SHOWROOM> listitem = new List<MS_ACARA_SHOWROOM>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_SHOWROOM, ACARA_VALUE, KODE, SHOWROOM" +
                    " from MS_ACARA_SHOWROOM {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ACARA_SHOWROOM item = new MS_ACARA_SHOWROOM();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_SHOWROOM = reader.GetInt64(2);
                        item.ACARA_VALUE = reader.GetString(3);
                        item.KODE = reader.GetString(4);
                        item.SHOWROOM = reader.GetString(5);
                       
                        listitem.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listitem;
        }

        //Tambahan Untuk ACara GWP
        public string insertMsItemAcaraGWP(MS_ITEM_GWP_PWP itemAcara)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ITEM_GWP_PWP ( ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, STATUS_ACARA, BARCODE,ITEM_PRICE_ACARA ) values " +
                    " (@idAcara, @idKdbrg, @value, @itemCode, @createdBy, getdate(), 1, @barcode, @itempriceAcara) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = itemAcara.ID_ACARA;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = itemAcara.ID_KDBRG;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = itemAcara.VALUE_ACARA;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = itemAcara.ITEM_CODE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = itemAcara.CREATED_BY;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = itemAcara.BARCODE;
                    command.Parameters.Add("@itempriceAcara", SqlDbType.Decimal).Value = itemAcara.ITEM_PRICE_ACARA;
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
        public List<MS_ITEM_GWP_PWP> getItemAcaraGWP(string where)
        {
            List<MS_ITEM_GWP_PWP> listItemAcara = new List<MS_ITEM_GWP_PWP>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS_ACARA,  BARCODE, ITEM_PRICE_ACARA " +
                    " from MS_ITEM_GWP_PWP {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_ITEM_GWP_PWP item = new MS_ITEM_GWP_PWP();
                        item.ID = reader.GetInt64(0);
                        item.ID_ACARA = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.VALUE_ACARA = reader.GetString(3);
                        item.ITEM_CODE = reader.GetString(4);
                        item.CREATED_BY = reader.GetString(5);
                        item.CREATED_DATE = reader.GetDateTime(6);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(7);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(8);
                        item.STATUS_ACARA = reader.IsDBNull(reader.GetOrdinal("STATUS_ACARA")) ? false : reader.GetBoolean(9);
                        item.BARCODE = reader.GetString(10);
                        item.ITEM_PRICE_ACARA = reader.GetDecimal(11);
                        listItemAcara.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listItemAcara;
        }
        public List<TEMP_STRUCK_GWP_PWP> getTempStruckGWP(string where)
        {
            List<TEMP_STRUCK_GWP_PWP> listTemp = new List<TEMP_STRUCK_GWP_PWP>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("select top 1 a.ID, a.ITEM_CODE, a.ART_DESC, a.SIZE, a.BRAND, a.PRICE, a.QTY, a.SA_NAME, a.CREATED_BY, a.CREATED_DATE, " +
                    "a.BARCODE, a.ART_PRICE, a.SPCL_PRICE, a.DISCOUNT, a.NET_PRICE, a.JENIS_DISCOUNT, a.BON_PRICE, a.ID_KDBRG, a.ID_ACARA, a.RETUR, a.NET_DISCOUNT, a.WARNA, b.ITEM_PRICE_ACARA from vw_tempStruck a join MS_ITEM_GWP_PWP b on a.ID_KDBRG = b.ID_KDBRG {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TEMP_STRUCK_GWP_PWP item = new TEMP_STRUCK_GWP_PWP();
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
                        item.NET_PRICE = reader.IsDBNull(reader.GetOrdinal("NET_PRICE")) ? 0 : reader.GetDecimal(14);
                        item.JENIS_DISCOUNT = reader.IsDBNull(reader.GetOrdinal("JENIS_DISCOUNT")) ? "" : reader.GetString(15);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(16);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(17);
                        item.ID_ACARA = reader.IsDBNull(reader.GetOrdinal("ID_ACARA")) ? 0 : reader.GetInt64(18);
                        item.RETUR = reader.IsDBNull(reader.GetOrdinal("RETUR")) ? "" : reader.GetString(19);
                        item.NET_DISCOUNT = reader.IsDBNull(reader.GetOrdinal("NET_DISCOUNT")) ? 0 : reader.GetDecimal(20);
                        item.WARNA = reader.IsDBNull(reader.GetOrdinal("WARNA")) ? "" : reader.GetString(21);
                        item.ITEM_PRICE_ACARA = reader.GetDecimal(22);
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

        #region "Test Upload With Bulk Insert"
        public string BulkInsItemAcara(String filePath)
        {
            string res = "";
            try
            {
                String strConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;// "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Hemant\\documents\\visual studio 2010\\Projects\\CRMdata\\CRMdata\\App_Data\\Database1.mdf';Integrated Security=True;User Instance=True";

                String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);
                //Create Connection to Excel work book 
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
                {
                    //Create OleDbCommand to fetch data from Excel 
                    using (OleDbCommand cmd = new OleDbCommand("Select ID_ACARA, CODE_ACARA, BARCODE, ITEM_CODE from [Item_Acara$]", excelConnection))
                    {
                        excelConnection.Open();
                        using (OleDbDataReader dReader = cmd.ExecuteReader())
                        {
                            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection))
                            {
                                //Give your Destination table name 
                                sqlBulk.DestinationTableName = "TEMP_ITEM_ACARA";
                                sqlBulk.WriteToServer(dReader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = "ERROR : " + ex;
            }
            return res;
        }
        public string insertMsItemAcaraBulk(string uName, Int64 idacara, string valacara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsertItemAcara", Connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdAcara", idacara));
                    command.Parameters.Add(new SqlParameter("@ValAcara", valacara));
                    command.Parameters.Add(new SqlParameter("@usercrt", uName));
                    command.Parameters.Add("@itemExist", SqlDbType.VarChar, 200);
                    command.Parameters["@itemExist"].Direction = ParameterDirection.Output;
                    Connection.Open();

                    command.ExecuteNonQuery();
                   newId = command.Parameters["@itemExist"].Value.ToString();
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
        public string BulkInsItemAcaraGWP(String filePath)
        {
            string res = "";
            try
            {
                String strConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;// "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Hemant\\documents\\visual studio 2010\\Projects\\CRMdata\\CRMdata\\App_Data\\Database1.mdf';Integrated Security=True;User Instance=True";

                String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);
                //Create Connection to Excel work book 
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
                {
                    //Create OleDbCommand to fetch data from Excel 
                    using (OleDbCommand cmd = new OleDbCommand("Select ID_ACARA, CODE_ACARA, BARCODE, ITEM_CODE from [Item_GWP_PWP$]", excelConnection))
                    {
                        excelConnection.Open();
                        using (OleDbDataReader dReader = cmd.ExecuteReader())
                        {
                            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection))
                            {
                                //Give your Destination table name 
                                sqlBulk.DestinationTableName = "TEMP_ITEM_ACARA_GWP";
                                sqlBulk.WriteToServer(dReader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = "ERROR : " + ex;
            }
            return res;
        }
        public string insertMsItemAcaraBulkGWP(string uName, Int64 idacara, string valacara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsertItemAcaraGWP", Connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IdAcara", idacara));
                    command.Parameters.Add(new SqlParameter("@ValAcara", valacara));
                    command.Parameters.Add(new SqlParameter("@usercrt", uName));
                    command.Parameters.Add("@itemExist", SqlDbType.VarChar, 200);
                    command.Parameters["@itemExist"].Direction = ParameterDirection.Output;
                    Connection.Open();

                    command.ExecuteNonQuery();
                    newId = command.Parameters["@itemExist"].Value.ToString();
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
      
        public string clearTempItemAcara(Int64 idacara, string valacara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Delete from TEMP_ITEM_ACARA where ID_ACARA = @IdAcara AND VALUE_ACARA = @ValAcara");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@IdAcara", SqlDbType.VarChar).Value = Convert.ToString(idacara);
                    command.Parameters.Add("@ValAcara", SqlDbType.VarChar).Value = valacara;

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
        public string clearTempItemAcaraGWP(Int64 idacara, string valacara)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Delete from TEMP_ITEM_ACARA_GWP where ID_ACARA = @IdAcara AND VALUE_ACARA = @ValAcara");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@IdAcara", SqlDbType.VarChar).Value = Convert.ToString(idacara);
                    command.Parameters.Add("@ValAcara", SqlDbType.VarChar).Value = valacara;

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
        #region "Duplicate Item Acra & GWP_PWP"
        public DataSet GetDataAcaraDuplicate(String where)
        {
            // List<MS_CBYR> listitem = new List<MS_CBYR>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {

                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("Select ID, START_DATE, END_DATE, NAMA_ACARA," +
                    " ACARA_DESC, ARTICLE, ID_ACARA_STATUS, ACARA_STATUS, DESC_DISC, ACR_DISC, ACR_SPCL_PRICE," +
                    " MIN_PURCHASE, KODE_SHR_ACARA, ID_SHR_ACARA, SHOWROOM_ACARA " +
                    "from vw_acara_FOR_DUPLICATE {0}", where), Connection))

                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }
        public DataSet GetDataSeMsAcaraShowroom(String where)
        {
            // List<MS_CBYR> listitem = new List<MS_CBYR>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {

                SqlConnection Connection = new SqlConnection(conString);

                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_ACARA, ID_SHOWROOM, ACARA_VALUE, KODE, SHOWROOM" +
                    " from MS_ACARA_SHOWROOM {0}", where), Connection))

                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }

        public string DuplicateMsItemAcara(Int64 IdToDupilcate, Int64 NewAcrID, string KodeAcara, String UserName)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ITEM_ACARA ( ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, STATUS_ACARA, BARCODE ) " +
                   "SELECT @NewAcrID, ID_KDBRG, @KodeAcara, ITEM_CODE, @UserName, GETDATE(), 1, BARCODE FROM MS_ITEM_ACARA WHERE ID_ACARA = @IdToDupilcate");
                //" (@NewAcrID, @idKdbrg, @KodeAcara, @itemCode, @createdBy, getdate(), 1, @barcode) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@NewAcrID", SqlDbType.BigInt).Value = NewAcrID;
                    command.Parameters.Add("@IdToDupilcate", SqlDbType.BigInt).Value = IdToDupilcate;
                    command.Parameters.Add("@KodeAcara", SqlDbType.VarChar).Value = KodeAcara;
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;

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
        public string DuplicateMsItemAcaraGWP_PWP(Int64 IdToDupilcate, Int64 NewAcrID, string KodeAcara, String UserName)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_ITEM_GWP_PWP ( ID_ACARA, ID_KDBRG, VALUE_ACARA, ITEM_CODE, CREATED_BY, CREATED_DATE, STATUS_ACARA, BARCODE, ITEM_PRICE_ACARA ) " +
                   "SELECT @NewAcrID, ID_KDBRG, @KodeAcara, ITEM_CODE, @UserName, GETDATE(), 1, BARCODE, ITEM_PRICE_ACARA FROM MS_ITEM_GWP_PWP WHERE ID_ACARA = @IdToDupilcate");
                //" (@NewAcrID, @idKdbrg, @KodeAcara, @itemCode, @createdBy, getdate(), 1, @barcode) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@NewAcrID", SqlDbType.BigInt).Value = NewAcrID;
                    command.Parameters.Add("@IdToDupilcate", SqlDbType.BigInt).Value = IdToDupilcate;
                    command.Parameters.Add("@KodeAcara", SqlDbType.VarChar).Value = KodeAcara;
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;

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

    }
}