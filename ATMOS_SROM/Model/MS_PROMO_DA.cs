using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATMOS_SROM.Domain;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ATMOS_SROM.Model
{
    public class MS_PROMO_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        
        public List<MS_PROMO> getPromo(string where)
        {
            List<MS_PROMO> listPromo = new List<MS_PROMO>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ITEM_CODE, SPCL_PRICE, DISCOUNT, START_DATE, END_DATE, FLAG, CATATAN, CREATED_BY, CREATED_DATE, STATUS, BARCODE, " +
                    " BARCODE_BRG, ITEM_CODE_BRG, FBRAND, FART_DESC, FCOL_DESC, FSIZE_DESC " +
                    " from vw_promoKdbrg {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PROMO item = new MS_PROMO();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.GetInt64(1);
                        item.ITEM_CODE = reader.GetString(2);
                        item.SPCL_PRICE = reader.IsDBNull(reader.GetOrdinal("SPCL_PRICE")) ? 0 : reader.GetDecimal(3);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(4);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?)null : reader.GetDateTime(5);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.FLAG = reader.GetString(7);
                        item.CATATAN = reader.IsDBNull(reader.GetOrdinal("CATATAN")) ? "" : reader.GetString(8);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(9);
                        item.CREATED_DATE = reader.GetDateTime(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(11);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(12);

                        item.BARCODE_BRG = reader.IsDBNull(reader.GetOrdinal("BARCODE_BRG")) ? "" : reader.GetString(13);
                        item.ITEM_CODE_BRG = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE_BRG")) ? "" : reader.GetString(14);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(15);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(16);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(17);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(18);
                        listPromo.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPromo;
        }

        public List<MS_PROMO_SHR> getPromoSHR(string where)
        {
            List<MS_PROMO_SHR> listPromo = new List<MS_PROMO_SHR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_KDBRG, ID_STORE, BARCODE, SHOWROOM, KODE, DISCOUNT, START_DATE, END_DATE, " +
                    " CREATED_BY, CREATED_DATE, KET, PRICE, STAT, BARCODE_BRG, ITEM_CODE, FBRAND, FART_DESC, FCOL_DESC, FSIZE_DESC, PRICE_BRG, ART_DESC " +
                    //" from MS_PROMO_SHR {0}", where), Connection))
                    " from vw_promoShr {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_PROMO_SHR item = new MS_PROMO_SHR();
                        item.ID = reader.GetInt64(0);
                        item.ID_KDBRG = reader.GetInt64(1);
                        item.ID_STORE = reader.GetInt64(2);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(3);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(4);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(5);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(6);
                        item.START_DATE = reader.IsDBNull(reader.GetOrdinal("START_DATE")) ? (DateTime?) null : reader.GetDateTime(7);
                        item.END_DATE = reader.IsDBNull(reader.GetOrdinal("END_DATE")) ? (DateTime?)null : reader.GetDateTime(8);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(9);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.KET = reader.IsDBNull(reader.GetOrdinal("KET")) ? "" : reader.GetString(11);

                        item.PRICE = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(12);
                        item.STAT = reader.IsDBNull(reader.GetOrdinal("STAT")) ? "" : reader.GetString(13);
                        item.BARCODE_BRG = reader.IsDBNull(reader.GetOrdinal("BARCODE_BRG")) ? "" : reader.GetString(14);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(15);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(16);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(17);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(18);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(19);
                        item.PRICE_BRG = reader.IsDBNull(reader.GetOrdinal("PRICE_BRG")) ? 0 : reader.GetDecimal(20);
                        item.ART_DESC = reader.IsDBNull(reader.GetOrdinal("ART_DESC")) ? "" : reader.GetString(21);

                        listPromo.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPromo;
        }
        
        public string insertMsPromo(MS_PROMO promo)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_PROMO ( ID_KDBRG, ITEM_CODE, BARCODE, SPCL_PRICE, DISCOUNT, START_DATE, END_DATE, FLAG, CATATAN, CREATED_BY, CREATED_DATE, STATUS ) values " +
                    " (@idKdbrg, @itemCode, @barcode, @price, @discount, @stardDate, @endDate, @flag, @catatan, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = promo.ID_KDBRG;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = promo.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = promo.BARCODE;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = promo.SPCL_PRICE;
                    command.Parameters.Add("@discount", SqlDbType.Int).Value = promo.DISCOUNT == null ? (object)Convert.DBNull : promo.DISCOUNT;
                    command.Parameters.Add("@stardDate", SqlDbType.DateTime2).Value = promo.START_DATE;
                    command.Parameters.Add("@endDate", SqlDbType.DateTime2).Value = promo.END_DATE;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = promo.FLAG;
                    command.Parameters.Add("@catatan", SqlDbType.VarChar).Value = promo.CATATAN;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = promo.CREATED_BY;

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

        public string updateEndDateMsPromo(MS_PROMO promo)
        {
            string ret = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_PROMO set END_DATE = @endDate, UPDATED_BY = @updateBy, UPDATED_DATE = getdate() where ID = @id");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@endDate", SqlDbType.DateTime2).Value = promo.END_DATE;
                    command.Parameters.Add("@updateBy", SqlDbType.VarChar).Value = promo.UPDATED_BY;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = promo.ID;
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
        
        public string softDeleteMsPromo(MS_PROMO promo)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update STATUS = 0 where ID = {0} ", promo.ID);
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.ExecuteNonQuery();
                
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

        public string insertTempPromoShr(TEMP_KDBRG tempKdbrg)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_KDBRG (ID_KDBRG, ID_SHOWROOM, KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ART_DESC, FCOLOR, FSIZE, FLAG, STAT, " +
                    " CREATED_BY, CREATED_DATE, PRICE, DISCOUNT, PRICE_AKHIR, START_DATE, END_DATE) values " +
                    " (@idKdbrg, @idShow, @kode, @show, @barcode, @itemCode, @brand, @desc, @color, @size, @flag, @stat, " +
                    " @createdBy, GETDATE(), @price, @disc, @priceAkhir, @sDate, @eDate); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = tempKdbrg.ID_KDBRG;
                    command.Parameters.Add("@idShow", SqlDbType.BigInt).Value = tempKdbrg.ID_SHOWROOM;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = tempKdbrg.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = tempKdbrg.SHOWROOM;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = tempKdbrg.BARCODE;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = tempKdbrg.ITEM_CODE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = tempKdbrg.BRAND;
                    command.Parameters.Add("@desc", SqlDbType.VarChar).Value = tempKdbrg.ART_DESC;
                    command.Parameters.Add("@color", SqlDbType.VarChar).Value = tempKdbrg.FCOLOR;
                    command.Parameters.Add("@size", SqlDbType.VarChar).Value = tempKdbrg.FSIZE;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = tempKdbrg.FLAG;
                    command.Parameters.Add("@stat", SqlDbType.VarChar).Value = tempKdbrg.STAT;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = tempKdbrg.CREATED_BY;

                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = tempKdbrg.PRICE;
                    command.Parameters.Add("@disc", SqlDbType.Decimal).Value = tempKdbrg.DISCOUNT;
                    command.Parameters.Add("@priceAkhir", SqlDbType.Decimal).Value = tempKdbrg.PRICE_AKHIR;
                    command.Parameters.Add("@sDate", SqlDbType.Date).Value = tempKdbrg.START_DATE;
                    command.Parameters.Add("@eDate", SqlDbType.Date).Value = tempKdbrg.END_DATE;

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

        public void insertPromoSHR(string kode, string createdBy)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertPromoShr", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 900;
                    command.Parameters.Add(new SqlParameter("@kode", kode));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
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

        public void insertPromo(string createdBy)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertPromo", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 900;
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
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
    }
}