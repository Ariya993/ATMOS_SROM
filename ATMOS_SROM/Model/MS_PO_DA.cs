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
    public class MS_PO_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public string insertPORetID(MS_PO po)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_PO (NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, PHONE, KODE_SUPPLIER, SUPPLIER, QTY, STATUS, CREATED_BY, CREATED_DATE, STATUS_PO) values " +
                    " (@noPO, @poREFF, @date, @brand, @contact, @position, @email, @address, @phone, @kdSupplier, @supplier, @qty, @status, @createdBy, getdate(), 1); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noPO", SqlDbType.VarChar).Value = po.NO_PO;
                    command.Parameters.Add("@poREFF", SqlDbType.VarChar).Value = po.PO_REFF;
                    command.Parameters.Add("@date", SqlDbType.Date).Value = po.DATE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = po.BRAND;
                    command.Parameters.Add("@contact", SqlDbType.VarChar).Value = po.CONTACT;
                    command.Parameters.Add("@position", SqlDbType.VarChar).Value = po.POSITION;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = po.EMAIL;
                    command.Parameters.Add("@address", SqlDbType.VarChar).Value = po.ADDRESS;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = po.PHONE;
                    command.Parameters.Add("@supplier", SqlDbType.VarChar).Value = po.SUPPLIER;
                    command.Parameters.Add("@kdSupplier", SqlDbType.VarChar).Value = po.KODE_SUPPLIER;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = po.QTY;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = po.STATUS;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = po.CREATED_BY;
                    //command.Parameters.Add("@FPPN_PCT", SqlDbType.Decimal).Value = po.FPPN_PCT;
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
        public string insertDetailPO(MS_PO_DETAIL detailPO)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_PO_DETAIL (ID_PO, ID_KDBRG, BARCODE, ITEM_CODE, COGS, PRICE, QTY, STATUS, STATUS_PO) values " +
                    " (@idPO, @idKdbrg, @barcode, @itemCode, @cogs, @price, @qty, @status, 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idPO", SqlDbType.BigInt).Value = detailPO.ID_PO;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = detailPO.ID_KDBRG;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = detailPO.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = detailPO.BARCODE;
                    command.Parameters.Add("@cogs", SqlDbType.Decimal).Value = detailPO.COGS;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = detailPO.PRICE;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = detailPO.STATUS;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = detailPO.QTY;

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

        public string insertDetailPONew(MS_PO_DETAIL detailPO)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_PO_DETAIL (ID_PO, ID_KDBRG, BARCODE, ITEM_CODE, COGS, PRICE, QTY, STATUS, STATUS_PO, FPPN_PCT, FPPN_RP, FJUMLAH) values " +
                    " (@idPO, @idKdbrg, @barcode, @itemCode, @cogs, @price, @qty, @status, 1, @FPPN_PCT, @FPPN_RP, @FJUMLAH) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idPO", SqlDbType.BigInt).Value = detailPO.ID_PO;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = detailPO.ID_KDBRG;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = detailPO.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = detailPO.BARCODE;
                    command.Parameters.Add("@cogs", SqlDbType.Decimal).Value = detailPO.COGS;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = detailPO.PRICE;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = detailPO.STATUS;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = detailPO.QTY;
                    command.Parameters.Add("@FPPN_PCT", SqlDbType.Decimal).Value = detailPO.FPPN_PCT;
                    command.Parameters.Add("@FPPN_RP", SqlDbType.Decimal).Value = detailPO.FPPN_RP;
                    command.Parameters.Add("@FJUMLAH", SqlDbType.Decimal).Value = detailPO.FJUMLAH;

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

        public List<MS_PO> getPO(string where)
        {
            List<MS_PO> listPO = new List<MS_PO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, " +
                    " PHONE, QTY, STATUS, CREATED_BY, CREATED_DATE, KODE_SUPPLIER, SUPPLIER from MS_PO {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PO item = new MS_PO();
                        item.ID = reader.GetInt64(0);
                        item.NO_PO = reader.GetString(1);
                        item.PO_REFF = reader.GetString(2);
                        item.DATE = reader.GetDateTime(3);
                        item.BRAND = reader.GetString(4);
                        item.CONTACT = reader.IsDBNull(reader.GetOrdinal("CONTACT")) ? "" : reader.GetString(5);
                        item.POSITION = reader.IsDBNull(reader.GetOrdinal("POSITION")) ? "" : reader.GetString(6);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(7);
                        item.ADDRESS = reader.IsDBNull(reader.GetOrdinal("ADDRESS")) ? "" : reader.GetString(8);
                        item.PHONE = reader.IsDBNull(reader.GetOrdinal("PHONE")) ? "" : reader.GetString(9);
                        item.QTY = reader.GetInt32(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(11);
                        item.CREATED_BY = reader.GetString(12);
                        item.CREATED_DATE = reader.GetDateTime(13);
                        item.KODE_SUPPLIER = reader.IsDBNull(reader.GetOrdinal("KODE_SUPPLIER")) ? "" : reader.GetString(14);
                        item.SUPPLIER = reader.IsDBNull(reader.GetOrdinal("SUPPLIER")) ? "" : reader.GetString(15);
                        
                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }

        public List<MS_PO_DETAIL> getDetailPO(string where)
        {
            List<MS_PO_DETAIL> listPO = new List<MS_PO_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_PO, ITEM_CODE, COGS, PRICE, QTY, QTY_TIBA, BARCODE from vw_detailPO {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PO_DETAIL item = new MS_PO_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_PO = reader.GetInt64(1);
                        item.ITEM_CODE = reader.GetString(2);
                        item.COGS = reader.GetDecimal(3);
                        item.PRICE = reader.GetDecimal(4);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(5);
                        item.QTY_TIBA = reader.IsDBNull(reader.GetOrdinal("QTY_TIBA")) ? 0 : reader.GetInt32(6);
                        item.BARCODE = reader.GetString(7);
                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }

        public string updateDetailPO(MS_STOCK stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_STOCK set STOCK = STOCK + {0}, UPDATED_BY = @updateBy, UPDATED_DATE = GETDATE() where ITEM_CODE = @itemCode and WAREHOUSE = @warehouse", stock.STOCK);
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = stock.ITEM_CODE;
                    command.Parameters.Add("@warehouse", SqlDbType.VarChar).Value = stock.WAREHOUSE;
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

        public string updateHeaderPO(MS_PO po)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PO set NO_PO = @noPO, QTY = @qty where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = po.ID;
                    command.Parameters.Add("@noPO", SqlDbType.VarChar).Value = po.NO_PO;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = po.QTY;

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

        public string updateQtyDetailPO(MS_PO_DETAIL po)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PO_DETAIL set QTY = @qty where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = po.ID;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = po.QTY;

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

        public string softDelete(string id, string namaTable)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update {0} set STATUS_PO = 0 where ID = {1}", namaTable, id);
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

        //Good Receive//
        public void deleteTempGR(string name)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("DELETE TEMP_GR where CREATED_BY = @name ");
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

        public List<MS_PO> getPOGR(string where)
        {
            List<MS_PO> listPO = new List<MS_PO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, " +
                    " PHONE, QTY, STATUS, CREATED_BY, CREATED_DATE from vw_tempPO {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PO item = new MS_PO();
                        item.ID = reader.GetInt64(0);
                        item.NO_PO = reader.GetString(1);
                        item.PO_REFF = reader.GetString(2);
                        item.DATE = reader.GetDateTime(3);
                        item.BRAND = reader.GetString(4);
                        item.CONTACT = reader.IsDBNull(reader.GetOrdinal("CONTACT")) ? "" : reader.GetString(5);
                        item.POSITION = reader.IsDBNull(reader.GetOrdinal("POSITION")) ? "" : reader.GetString(6);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(7);
                        item.ADDRESS = reader.IsDBNull(reader.GetOrdinal("ADDRESS")) ? "" : reader.GetString(8);
                        item.PHONE = reader.IsDBNull(reader.GetOrdinal("PHONE")) ? "" : reader.GetString(9);
                        item.QTY = reader.GetInt32(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(11);
                        item.CREATED_BY = reader.GetString(12);
                        item.CREATED_DATE = reader.GetDateTime(13);

                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }

        public List<MS_PO_DETAIL> getDetailPOGR(string where)
        {
            List<MS_PO_DETAIL> listPO = new List<MS_PO_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_PO, ITEM_CODE, COGS, PRICE, QTY, STATUS, QTY_TIBA, " +
                    "QTY_WAIT, NO_PO, BARCODE, FART_DESC, FCOL_DESC, FSIZE_DESC from vw_detailPO {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PO_DETAIL item = new MS_PO_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_PO = reader.GetInt64(1);
                        item.ITEM_CODE = reader.GetString(2);
                        item.COGS = reader.GetDecimal(3);
                        item.PRICE = reader.GetDecimal(4);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(5);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(6);
                        item.QTY_TIBA = reader.IsDBNull(reader.GetOrdinal("QTY_TIBA")) ? 0 : reader.GetInt32(7);
                        item.QTY_WAIT = reader.IsDBNull(reader.GetOrdinal("QTY_WAIT")) ? 0 : reader.GetInt32(8);
                        item.NO_PO = reader.IsDBNull(reader.GetOrdinal("NO_PO")) ? "" : reader.GetString(9);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(10);

                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(11);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(12);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(13);
                        
                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }

        public List<TR_BELI_HEADER> getGRHeader(string where)
        {
            List<TR_BELI_HEADER> listGRHeader = new List<TR_BELI_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_PO, NO_GR, KODE_SUPPLIER, SUPPLIER, TGL_TRANS, CREATED_BY, CREATED_DATE " +
                    " from TR_BELI_HEADER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_BELI_HEADER item = new TR_BELI_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.ID_PO = reader.IsDBNull(reader.GetOrdinal("ID_PO")) ? "" : reader.GetString(1);
                        item.NO_GR = reader.IsDBNull(reader.GetOrdinal("NO_GR")) ? "" : reader.GetString(2);
                        item.KODE_SUPPLIER = reader.IsDBNull(reader.GetOrdinal("KODE_SUPPLIER")) ? "" : reader.GetString(3);
                        item.SUPPLIER = reader.IsDBNull(reader.GetOrdinal("SUPPLIER")) ? "" : reader.GetString(4);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(5);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(6);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(7);
                        
                        listGRHeader.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listGRHeader;
        }

        public List<TR_BELI_HEADER> getGRHeaderGroupBy(string where)
        {
            List<TR_BELI_HEADER> listGRHeader = new List<TR_BELI_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select NO_GR, KODE_SUPPLIER, TGL_TRANS, CREATED_BY " +
                    " from TR_BELI_HEADER {0} GROUP BY NO_GR, KODE_SUPPLIER, TGL_TRANS, CREATED_BY ORDER BY TGL_TRANS DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_BELI_HEADER item = new TR_BELI_HEADER();
                        item.NO_GR = reader.IsDBNull(reader.GetOrdinal("NO_GR")) ? "" : reader.GetString(0);
                        item.KODE_SUPPLIER = reader.IsDBNull(reader.GetOrdinal("KODE_SUPPLIER")) ? "" : reader.GetString(1);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(2);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(3);

                        listGRHeader.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listGRHeader;
        }

        public List<TR_BELI_DETAIL> getGRDetail(string where)
        {
            List<TR_BELI_DETAIL> listGRDetail = new List<TR_BELI_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_HEADER, ID_DETAIL_PO, ID_KDBRG, NO_GR, QTY_TIBA, KODE, " +
                    " SHOWROOM, RECEIVED_BY, RECEIVE_DATE, COGS, PRICE, BARCODE, ITEM_CODE, FART_DESC, FCOL_DESC, FSIZE_DESC, FBRAND, TGL_TRANS " +
                    " from vw_beliDetail {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_BELI_DETAIL item = new TR_BELI_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_HEADER = reader.IsDBNull(reader.GetOrdinal("ID_HEADER")) ? 0 : reader.GetInt64(1);
                        item.ID_DETAIL_PO = reader.IsDBNull(reader.GetOrdinal("ID_DETAIL_PO")) ? 0 : reader.GetInt64(2);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(3);
                        item.NO_GR = reader.IsDBNull(reader.GetOrdinal("NO_GR")) ? "" : reader.GetString(4);
                        item.QTY_TIBA = reader.IsDBNull(reader.GetOrdinal("QTY_TIBA")) ? 0 : reader.GetInt32(5);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(6);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(7);

                        item.RECEIVED_BY = reader.IsDBNull(reader.GetOrdinal("RECEIVED_BY")) ? "" : reader.GetString(8);
                        item.RECEIVE_DATE = reader.IsDBNull(reader.GetOrdinal("RECEIVE_DATE")) ? (DateTime?)null : reader.GetDateTime(9);
                        item.COGS = reader.IsDBNull(reader.GetOrdinal("COGS")) ? 0 : reader.GetDecimal(10);
                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(11);

                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(12);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(13);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(14);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(15);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(16);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(17);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(18);
                        
                        listGRDetail.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listGRDetail;
        }

        public string insertTempGrRetID(TEMP_GR gr)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_GR (ID_PO, KODE_SUPPLIER, SUPPLIER, CREATED_BY) values " +
                    " (@id, @kode, @supplier, @createdBy); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = gr.ID_PO;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = gr.KODE_SUPPLIER;
                    command.Parameters.Add("@supplier", SqlDbType.VarChar).Value = gr.SUPPLIER;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = gr.CREATED_BY;

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

        public string insertGr(string GR, string userName, DateTime tglTrans)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("INSERT into TR_BELI_HEADER (ID_PO, NO_GR, KODE_SUPPLIER, SUPPLIER, TGL_TRANS, CREATED_BY, CREATED_DATE) " +
                    " select ID_PO, @noGR, KODE_SUPPLIER, SUPPLIER, @tglTrans, CREATED_BY, GETDATE() from TEMP_GR where CREATED_BY = @userName");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noGR", SqlDbType.VarChar).Value = GR;
                    command.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = tglTrans;
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

        public string deleteOneTempGR(TEMP_GR gr)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("DELETE TEMP_GR where ID_PO = @id and CREATED_BY = @createdBy ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = gr.ID_PO;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = gr.CREATED_BY;
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

        public string insertReceivePO(TR_BELI_DETAIL receivePI)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TR_BELI_DETAIL (ID_DETAIL_PO, NO_GR, QTY_TIBA, KODE, SHOWROOM, RECEIVED_BY, RECEIVE_DATE) values " +
                    " (@idDetailPO, @noGR, @qtyTiba, @kode, @warehouse, @recivedBy, getdate()) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idDetailPO", SqlDbType.BigInt).Value = receivePI.ID_DETAIL_PO;
                    command.Parameters.Add("@noGR", SqlDbType.VarChar).Value = receivePI.NO_GR;
                    command.Parameters.Add("@qtyTiba", SqlDbType.Int).Value = receivePI.QTY_TIBA;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = receivePI.KODE;
                    command.Parameters.Add("@warehouse", SqlDbType.VarChar).Value = receivePI.SHOWROOM;
                    command.Parameters.Add("@recivedBy", SqlDbType.VarChar).Value = receivePI.RECEIVED_BY;

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

        public string updateStatusDetailPO(string id, string status, int stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PO_DETAIL set STATUS = '{0}', QTY_REAL = {2} where ID = {1} ", status, id, stock);
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

        public string updateStatusPO(string where, string status)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_PO set STATUS = '{0}' {1} ", status, where);
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

        public void cancelGR(string GR)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_cancelGR", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 900;
                    command.Parameters.Add(new SqlParameter("@noGR", GR));
                    Connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        //New
        public List<MS_PO> getPONew(string where)
        {
            List<MS_PO> listPO = new List<MS_PO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_PO, PO_REFF, DATE, BRAND, CONTACT, POSITION, EMAIL, ADDRESS, " +
                    " PHONE, QTY, STATUS, CREATED_BY, CREATED_DATE, KODE_SUPPLIER, SUPPLIER, QTY_NOW from vw_PurchaseOrder {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PO item = new MS_PO();
                        item.ID = reader.GetInt64(0);
                        item.NO_PO = reader.GetString(1);
                        item.PO_REFF = reader.GetString(2);
                        item.DATE = reader.GetDateTime(3);
                        item.BRAND = reader.GetString(4);
                        item.CONTACT = reader.IsDBNull(reader.GetOrdinal("CONTACT")) ? "" : reader.GetString(5);
                        item.POSITION = reader.IsDBNull(reader.GetOrdinal("POSITION")) ? "" : reader.GetString(6);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(7);
                        item.ADDRESS = reader.IsDBNull(reader.GetOrdinal("ADDRESS")) ? "" : reader.GetString(8);
                        item.PHONE = reader.IsDBNull(reader.GetOrdinal("PHONE")) ? "" : reader.GetString(9);
                        item.QTY = reader.GetInt32(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? "" : reader.GetString(11);
                        item.CREATED_BY = reader.GetString(12);
                        item.CREATED_DATE = reader.GetDateTime(13);
                        item.KODE_SUPPLIER = reader.IsDBNull(reader.GetOrdinal("KODE_SUPPLIER")) ? "" : reader.GetString(14);
                        item.SUPPLIER = reader.IsDBNull(reader.GetOrdinal("SUPPLIER")) ? "" : reader.GetString(15);
                        item.QTY_NOW = reader.GetInt32(16);
                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }

    }
}