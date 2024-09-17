using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace ATMOS_SROM.Model
{
    public class MS_SHOWROOM_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_SHOWROOM> getShowRoom(string where)
        {
            List<MS_SHOWROOM> listShowRoom = new List<MS_SHOWROOM>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, SHOWROOM, ALAMAT, PHONE, STATUS, STATUS_SHOWROOM, KODE, STORE, BRAND, BRAND_JUAL, LOGO_IMG from MS_SHOWROOM {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_SHOWROOM item = new MS_SHOWROOM();
                        item.ID = reader.GetInt64(0);
                        item.SHOWROOM = reader.GetString(1);
                        item.ALAMAT = reader.GetString(2);
                        item.PHONE = reader.GetString(3);
                        item.STATUS = reader.GetString(4);
                        item.STATUS_SHOWROOM = reader.GetString(5);
                        item.KODE = reader.GetString(6);
                        item.STORE = reader.GetString(7);
                        item.BRAND = reader.GetString(8);
                        item.VALUE = reader.GetInt64(0).ToString() + "#" + reader.GetString(6);
                        item.BRAND_JUAL = reader.IsDBNull(reader.GetOrdinal("BRAND_JUAL")) ? "" : reader.GetString(9);
                        item.LOGO_IMG = reader.IsDBNull(reader.GetOrdinal("LOGO_IMG")) ? "" : reader.GetString(10);
                        listShowRoom.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listShowRoom;
        }

        public string insertShowRoom(MS_SHOWROOM showroom)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_SHOWROOM (KODE, SHOWROOM, STORE, BRAND, ALAMAT, PHONE, STATUS, STATUS_SHOWROOM, VENDOR) values " +
                    " (@kode, @showroom, @store, @brand, @alamat, @phone, @status, @statusShowroom, @vendor) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = showroom.KODE;
                    command.Parameters.Add("@showroom", SqlDbType.VarChar).Value = showroom.SHOWROOM;
                    command.Parameters.Add("@store", SqlDbType.VarChar).Value = showroom.STORE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = showroom.BRAND;
                    command.Parameters.Add("@alamat", SqlDbType.Text).Value = showroom.ALAMAT;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = showroom.PHONE;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = showroom.STATUS;
                    command.Parameters.Add("@statusShowroom", SqlDbType.VarChar).Value = showroom.STATUS_SHOWROOM;
                    command.Parameters.Add("@vendor", SqlDbType.VarChar).Value = "";
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

        public List<TR_ADJUSTMENT> getTrAdjustment(string where)
        {
            List<TR_ADJUSTMENT> listAdjustment = new List<TR_ADJUSTMENT>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ID_WAREHOUSE, KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ART_DESC, COLOR, SIZE, " +
                    " STOCK_AWAL, ADJUSTMENT, STOCK_AKHIR, ALASAN, CREATED_BY, CREATED_DATE  from TR_ADJUSTMENT {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        TR_ADJUSTMENT item = new TR_ADJUSTMENT();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.GetInt64(1);
                        item.ID_WAREHOUSE = reader.GetInt64(2);
                        item.KODE = reader.GetString(3);
                        item.SHOWROOM = reader.GetString(4);
                        item.BARCODE = reader.GetString(5);
                        item.ITEM_CODE = reader.GetString(6);
                        item.BRAND = reader.GetString(7);
                        item.ART_DESC = reader.GetString(8);

                        item.COLOR = reader.GetString(9);
                        item.SIZE = reader.GetString(10);
                        item.STOCK_AWAL = reader.GetInt32(11);
                        item.ADJUSTMENT = reader.GetInt32(12);
                        item.STOCK_AKHIR = reader.GetInt32(13);
                        item.ALASAN = reader.GetString(14);
                        item.CREATED_BY = reader.GetString(15);
                        item.CREATED_DATE = reader.GetDateTime(16);

                        listAdjustment.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAdjustment;
        }

        public string insertTrfStock(TRF_STOCK_DETAIL stock)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_STOCK_DETAIL (ID_HEADER, NO_BUKTI, ITEM_CODE, QTY_KIRIM, USER_KIRIM, REFF) values " +
                    " (@idHeader, @noBukti, @itemCode, @qty, @user, @reff) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idHeader", SqlDbType.BigInt).Value = stock.ID_HEADER;
                    command.Parameters.Add("@noBukti", SqlDbType.VarChar).Value = stock.NO_BUKTI;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = stock.ITEM_CODE;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = stock.USER_KIRIM;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = stock.QTY_KIRIM;
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

        public string insertHeaderRetID(TRF_STOCK_HEADER stockHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TRF_STOCK_HEADER ( DARI, KE, KODE_DARI, KODE_KE, STATUS_TRF, CREATED_BY, CREATED_DATE ) values " +
                    " ( @dari, @ke, @kodeDari, @kodeKe, 0, @created_by, getdate() ); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@dari", SqlDbType.VarChar).Value = stockHeader.DARI;
                    command.Parameters.Add("@ke", SqlDbType.VarChar).Value = stockHeader.KE;
                    command.Parameters.Add("@kodeDari", SqlDbType.VarChar).Value = stockHeader.KODE_DARI;
                    command.Parameters.Add("@kodeKe", SqlDbType.VarChar).Value = stockHeader.KODE_KE;
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

        public string updateNoBukti(string noBukti, string id)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_DETAIL set NO_BUKTI = '{0}' where ID = {1} ", noBukti, id);
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

        public string updateNoBuktiHeader(string noBukti, string id, string kodeMove)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update TRF_STOCK_HEADER set NO_BUKTI = '{0}', KODE_MOVE = '{2}'  where ID = {1} ", noBukti, id, kodeMove);
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
        
        public string updateStock(string stock, string id)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_STOCK set STOCK = '{0}' where ID = {1} ", stock, id);
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

        public string insertTrAdjusment(TR_ADJUSTMENT adj)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TR_ADJUSTMENT (ID_KDBRG, ID_WAREHOUSE, KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ART_DESC, COLOR, SIZE, STOCK_AWAL, ADJUSTMENT, STOCK_AKHIR, ALASAN, CREATED_BY, CREATED_DATE ) values " +
                    " (@idKdbrg, @idWare, @kode, @show, @barcode, @itemCode, @brand, @artDesc, @color, @size, @stockAwal, @adj, @stockAkhir, @alasan, @createdBy, @createdDate) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = adj.ID_KDBRG;
                    command.Parameters.Add("@idWare", SqlDbType.BigInt).Value = adj.ID_WAREHOUSE;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = adj.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = adj.SHOWROOM;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = adj.BARCODE;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = adj.ITEM_CODE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = adj.BRAND;
                    command.Parameters.Add("@artDesc", SqlDbType.VarChar).Value = adj.ART_DESC;
                    command.Parameters.Add("@color", SqlDbType.VarChar).Value = adj.COLOR;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = adj.SIZE;
                    command.Parameters.Add("@stockAwal", SqlDbType.Int).Value = adj.STOCK_AWAL;
                    command.Parameters.Add("@adj", SqlDbType.Int).Value = adj.ADJUSTMENT;
                    command.Parameters.Add("@stockAkhir", SqlDbType.Int).Value = adj.STOCK_AKHIR;
                    command.Parameters.Add("@alasan", SqlDbType.VarChar).Value = adj.ALASAN;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = adj.CREATED_BY;
                    command.Parameters.Add("@createdDate", SqlDbType.DateTime2).Value = adj.CREATED_DATE;

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

        #region "Upload Adjustment"
        public int cekAdjustmentUp(String flPath, String source, string fileType, string usr)
        {
            int jml = 0;
            OleDbConnection cnn = new OleDbConnection();
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=NO;FMT=Delimited;IMEX=1;'";
            cnn = new OleDbConnection(connetionString);

            DataSet dsOle = new DataSet();
            cnn.Open();

            DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dbSchema == null || dbSchema.Rows.Count < 1)
            {
                throw new Exception("Error: Could not determine the name of the first worksheet.");
            }

            int rowTable = fileType == ".xls" ? 0 : dbSchema.Rows.Count - 1;
            string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
            cnn.Close();
            OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);
            oAdapter.Fill(dsOle, "Sheet1");

            for (int i = 1; i < dsOle.Tables[0].Rows.Count; i++)
            {
                if (dsOle.Tables[0].Rows[i][0].ToString() == null || dsOle.Tables[0].Rows[i][1].ToString() == null || dsOle.Tables[0].Rows[i][2].ToString() == null
                    || dsOle.Tables[0].Rows[i][0].ToString() == "" || dsOle.Tables[0].Rows[i][1].ToString() == "" || dsOle.Tables[0].Rows[i][2].ToString() == ""
                    || dsOle.Tables[0].Rows[i][1].ToString() == "0")
                {
                    jml = jml + 1;
                }
            }

            return jml;
        }

        public String upTrAdjustment(String flPath, String source, string fileType, string Kode_SHR, string SHR, string usr, DateTime adjustDate)
        {
            string res = "Upload Berhasil";
            String test;

            try
            {
                OleDbConnection cnn = new OleDbConnection();
                string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                cnn.Open();

                DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }

                int rowTable = fileType == ".xls" ? 0 : dbSchema.Rows.Count - 1;
                string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);
                oAdapter.Fill(dsOle, "Sheet1");
                test = InsertTrAdjustmentwithSP(dsOle.Tables[0], Kode_SHR, SHR, usr, adjustDate);
            }
            catch (Exception ex)
            {
                res = "ERROR: " + ex.Message;
                throw ex;
            }

            return res;
        }

        public string InsertTrAdjustmentwithSP(DataTable dt, string Kode_SHR, string SHR, string createdBy, DateTime adjustDate)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);

            try
            {
                Connection.Open();
                SqlCommand cmdProc = new SqlCommand("CreateTRAdjustment", Connection);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@KODE_SHR", Kode_SHR);
                cmdProc.Parameters.AddWithValue("@SHOWROOM", SHR);
                cmdProc.Parameters.AddWithValue("@dt", dt);
                cmdProc.Parameters.AddWithValue("@CREATED_BY", createdBy);
                cmdProc.Parameters.AddWithValue("@ADJUSTDATE", adjustDate);
                cmdProc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
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