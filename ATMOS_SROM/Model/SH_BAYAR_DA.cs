using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ATMOS_SROM.Domain;
using System.Configuration;
using System.Data;
using ATMOS_SROM.Domain.CustomObj;

namespace ATMOS_SROM.Model
{
    public class SH_BAYAR_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<SH_BAYAR> getSHBayar(string where)
        {
            List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, KODE_CUST, TGL_TRANS, NO_BON, QTY, NET_BAYAR, CARD, JM_UANG, KEMBALI, " +
                    " NET_CASH, VOUCHER, CREATED_BY, STATUS_STORE, JM_CARD, JM_VOUCHER, KODE, JM_ONGKIR, JM_FREE_ONGKIR, ONGKIR " +
                    " from SH_BAYAR {0} Order BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_BAYAR item = new SH_BAYAR();
                        item.ID = reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.TGL_TRANS = reader.GetDateTime(2);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(3);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.CARD = reader.IsDBNull(reader.GetOrdinal("CARD")) ? "" : reader.GetString(6);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(7);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(8);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(9);
                        item.VOUCHER = reader.IsDBNull(reader.GetOrdinal("VOUCHER")) ? "" : reader.GetString(10);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(11);
                        item.STATUS_STORE = reader.IsDBNull(reader.GetOrdinal("STATUS_STORE")) ? "" : reader.GetString(12);
                        item.JM_CARD = reader.IsDBNull(reader.GetOrdinal("JM_CARD")) ? 0 : reader.GetDecimal(13);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(14);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(15);
                        item.JM_ONGKIR = reader.IsDBNull(reader.GetOrdinal("JM_ONGKIR")) ? 0 : reader.GetDecimal(16);
                        item.JM_FREE_ONGKIR = reader.IsDBNull(reader.GetOrdinal("JM_FREE_ONGKIR")) ? 0 : reader.GetDecimal(17);
                        item.ONGKIR = reader.IsDBNull(reader.GetOrdinal("ONGKIR")) ? "" : reader.GetString(18);
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

        public List<SH_JUAL> getSHJual(string where)
        {
            List<SH_JUAL> listJual = new List<SH_JUAL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_BAYAR, ID_KDBRG, KODE_CUST, TGL_TRANS, NO_BON, ITEM_CODE, " +
                    " TAG_PRICE, BON_PRICE, NILAI_BYR, FBRAND, FART_DESC, FCOL_DESC, FSIZE_DESC, BARCODE, JNS_DISC, DISC_P " +
                    " from vw_shJual {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_JUAL item = new SH_JUAL();
                        item.ID = reader.GetInt64(0);
                        item.ID_BAYAR = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.KODE_CUST = reader.GetString(3);
                        item.TGL_TRANS = reader.GetDateTime(4);
                        item.NO_BON = reader.GetString(5);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(6);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(7);
                        item.BON_PRICE = reader.GetDecimal(8);
                        item.NILAI_BYR = reader.GetDecimal(9);

                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(10);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(11);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(12);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(13);
                        item.BARCODE = reader.IsDBNull(reader.GetOrdinal("BARCODE")) ? "" : reader.GetString(14);
                        item.JNS_DISC = reader.IsDBNull(reader.GetOrdinal("JNS_DISC")) ? "" : reader.GetString(15);
                        item.DISC_P = reader.IsDBNull(reader.GetOrdinal("DISC_P")) ? 0 : reader.GetDecimal(16);
                        listJual.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listJual;
        }

        public List<MS_MEMBER> getMember(string where)
        {
            List<MS_MEMBER> listMember = new List<MS_MEMBER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, FIRST_NAME, LAST_NAME, PHONE, EMAIL, ALAMAT, BRAND, STATUS_MEMBER, CREATED_BY, CREATED_DATE, STATUS" +
                    " from MS_MEMBER {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_MEMBER item = new MS_MEMBER();
                        item.ID = reader.GetInt64(0);
                        item.FIRST_NAME = reader.GetString(1);
                        item.LAST_NAME = reader.GetString(2);
                        item.PHONE = reader.GetString(3);
                        item.EMAIL = reader.GetString(4);
                        item.ALAMAT = reader.GetString(5);
                        item.BRAND = reader.GetString(6);
                        item.STATUS_MEMBER = reader.GetString(7);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(8);
                        item.CREATED_DATE = reader.GetDateTime(9);
                        item.STATUS = reader.GetBoolean(10);

                        listMember.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMember;
        }

        public List<NO_DOC> getNoDoc(string where)
        {
            List<NO_DOC> listNoDoc = new List<NO_DOC>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NO_URUT, KODE, CREATED_BY, CREATED_DATE, DATEDIFF(YEAR, CREATED_DATE, GETDATE()) DIFF_YEAR" +
                    " from NO_DOC {0}", where), Connection))
                    //" from vw_doc {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        NO_DOC item = new NO_DOC();
                        item.ID = reader.GetInt64(0);
                        item.NO_URUT = reader.GetInt32(1);
                        item.KODE = reader.GetString(2);
                        item.CREATED_BY = reader.GetString(3);
                        item.CREATED_DATE = reader.GetDateTime(4);
                        item.DIFF_YEAR = reader.GetInt32(5);

                        listNoDoc.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNoDoc;
        }

        public List<MS_EMPLOYEE> getEmployee(string where)
        {
            List<MS_EMPLOYEE> listNoEmp = new List<MS_EMPLOYEE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, NIK, NAMA, JABATAN, JOIN_DATE, LIMIT, STATUS_EMPLOYEE, " +
                    " CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS, DATEDIFF(DAY,JOIN_DATE,GETDATE()) [DAY_JOIN], STATUS_CARD, TIPE, LIMIT_DELAMI, STATUS_EPC, SISA_LIMIT " +
                    " from vw_epc {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_EMPLOYEE item = new MS_EMPLOYEE();
                        item.ID = reader.GetInt64(0);
                        item.NIK = reader.IsDBNull(reader.GetOrdinal("NIK")) ? "" : reader.GetString(1);
                        item.NAMA = reader.IsDBNull(reader.GetOrdinal("NAMA")) ? "" : reader.GetString(2);
                        item.JABATAN = reader.IsDBNull(reader.GetOrdinal("JABATAN")) ? "" : reader.GetString(3);
                        item.JOIN_DATE = reader.IsDBNull(reader.GetOrdinal("JOIN_DATE")) ? DateTime.Now : reader.GetDateTime(4);

                        item.LIMIT = reader.IsDBNull(reader.GetOrdinal("LIMIT")) ? 0 : reader.GetDecimal(5);
                        item.STATUS_EMPLOYEE = reader.IsDBNull(reader.GetOrdinal("STATUS_EMPLOYEE")) ? "" : reader.GetString(6);

                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(7);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? DateTime.Now : reader.GetDateTime(8);
                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(9);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(10);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(11);
                        item.DAY_JOIN = reader.IsDBNull(reader.GetOrdinal("DAY_JOIN")) ? 0 : reader.GetInt32(12);
                        item.STATUS_CARD = reader.IsDBNull(reader.GetOrdinal("STATUS_CARD")) ? 0 : reader.GetInt32(13);
                        item.TIPE = reader.IsDBNull(reader.GetOrdinal("TIPE")) ? "" : reader.GetString(14);
                        item.LIMIT_DELAMI = reader.IsDBNull(reader.GetOrdinal("LIMIT_DELAMI")) ? 0 : reader.GetDecimal(15);
                        item.STATUS_EPC = reader.IsDBNull(reader.GetOrdinal("STATUS_EPC")) ? "" : reader.GetString(16);
                        item.SISA_LIMIT = reader.IsDBNull(reader.GetOrdinal("SISA_LIMIT")) ? 0 : reader.GetDecimal(17);

                        listNoEmp.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNoEmp;
        }

        public List<MS_DISCOUNT_SHR> getListDiscShr(string where)
        {
            List<MS_DISCOUNT_SHR> listDisc = new List<MS_DISCOUNT_SHR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_SHR, KODE, SHOWROOM, DISCOUNT, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS, TIPE, TIPE_DISCOUNT " +
                    " from MS_DISCOUNT_SHR {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_DISCOUNT_SHR item = new MS_DISCOUNT_SHR();
                        item.ID = reader.GetInt64(0);
                        item.ID_SHR = reader.IsDBNull(reader.GetOrdinal("ID_SHR")) ? 0 : reader.GetInt64(1);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(2);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(3);
                        item.DISCOUNT = reader.IsDBNull(reader.GetOrdinal("DISCOUNT")) ? 0 : reader.GetInt32(4);

                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(5);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(6);

                        item.UPDATED_BY = reader.IsDBNull(reader.GetOrdinal("UPDATED_BY")) ? "" : reader.GetString(7);
                        item.UPDATED_DATE = reader.IsDBNull(reader.GetOrdinal("UPDATED_DATE")) ? (DateTime?)null : reader.GetDateTime(8);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(9);
                        item.TIPE = reader.IsDBNull(reader.GetOrdinal("TIPE")) ? "" : reader.GetString(10);
                        item.TIPE_DISCOUNT = reader.IsDBNull(reader.GetOrdinal("TIPE_DISCOUNT")) ? "" : reader.GetString(11);

                        listDisc.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDisc;
        }

        public string insertBayarRetID(SH_BAYAR bayar)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_BAYAR (KODE_CUST, KODE, STATUS_STORE, QTY, CREATED_BY, JM_CARD, JM_VOUCHER) values " +
                    " (@Kode, @kodeCT, @Store, @Qty, @CreatedBy, @card, @voucher); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@Kode", SqlDbType.VarChar).Value = bayar.KODE_CUST;
                    command.Parameters.Add("@kodeCT", SqlDbType.VarChar).Value = bayar.KODE;
                    command.Parameters.Add("@Store", SqlDbType.VarChar).Value = bayar.STATUS_STORE;
                    command.Parameters.Add("@Qty", SqlDbType.Int).Value = bayar.QTY;
                    command.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = bayar.CREATED_BY;
                    command.Parameters.Add("@card", SqlDbType.Decimal).Value = bayar.JM_CARD;
                    command.Parameters.Add("@voucher", SqlDbType.Decimal).Value = bayar.JM_VOUCHER;

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
        
        public string deleteBayar(Int64 id)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("DELETE SH_BAYAR where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
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
        
        public string updateBayar(SH_BAYAR bayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_BAYAR set TGL_TRANS = @tglTrans, NO_BON = @bon, CARD = @card, NET_CASH = @cash, JM_UANG = @jmUang, " +
                       " KEMBALI = @kembali, NET_BAYAR = @bayar, VOUCHER = @voucher, EPC = @epc, MEMBER = @member, CREATED_DATE = getdate(), FVER = @version, " +
                       " DPP = @dpp, PPN = @ppn, OTHER = @other, NO_MEMBER = @noMember, NO_BONM = @bonM, FREMARK =@fremark, " +
                       " ONGKIR = @ONGKIR, JM_ONGKIR = @JM_ONGKIR, JM_FREE_ONGKIR = @JM_FREE_ONGKIR where ID = @id ");
                //string query = String.Format("update SH_BAYAR set TGL_TRANS = @tglTrans, NO_BON = @bon, CARD = @card, NET_CASH = @cash, JM_UANG = @jmUang, " +
                //    " KEMBALI = @kembali, NET_BAYAR = @bayar, VOUCHER = @voucher, EPC = @epc, MEMBER = @member, CREATED_DATE = getdate(), FVER = @version, " +
                //    " DPP = @dpp, PPN = @ppn, OTHER = @other, NO_MEMBER = @noMember, NO_BONM = @bonM, FREMARK =@fremark where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = bayar.TGL_TRANS;
                    command.Parameters.Add("@bon", SqlDbType.VarChar).Value = bayar.NO_BON;
                    command.Parameters.Add("@card", SqlDbType.VarChar).Value = bayar.CARD;
                    command.Parameters.Add("@cash", SqlDbType.Decimal).Value = bayar.NET_CASH;
                    command.Parameters.Add("@jmUang", SqlDbType.Decimal).Value = bayar.JM_UANG;
                    command.Parameters.Add("@kembali", SqlDbType.Decimal).Value = bayar.KEMBALI;
                    command.Parameters.Add("@bayar", SqlDbType.Decimal).Value = bayar.NET_BAYAR;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = bayar.ID;
                    command.Parameters.Add("@voucher", SqlDbType.VarChar).Value = bayar.VOUCHER;
                    command.Parameters.Add("@epc", SqlDbType.VarChar).Value = bayar.EPC;
                    command.Parameters.Add("@member", SqlDbType.VarChar).Value = bayar.MEMBER;
                    command.Parameters.Add("@version", SqlDbType.VarChar).Value = bayar.FVER;
                    command.Parameters.Add("@dpp", SqlDbType.Decimal).Value = bayar.DPP;
                    command.Parameters.Add("@ppn", SqlDbType.Decimal).Value = bayar.PPN;
                    command.Parameters.Add("@other", SqlDbType.Decimal).Value = bayar.OTHER;
                    command.Parameters.Add("@noMember", SqlDbType.VarChar).Value = bayar.NO_MEMBER;
                    command.Parameters.Add("@bonM", SqlDbType.VarChar).Value = bayar.NO_BONM;
                    command.Parameters.Add("@fremark", SqlDbType.VarChar).Value = bayar.FREMARK;
                    command.Parameters.Add("@ONGKIR", SqlDbType.VarChar).Value = bayar.ONGKIR;
                    command.Parameters.Add("@JM_ONGKIR", SqlDbType.Decimal).Value = bayar.JM_ONGKIR;
                    command.Parameters.Add("@JM_FREE_ONGKIR", SqlDbType.Decimal).Value = bayar.JM_FREE_ONGKIR;
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

        public string updateBayarVoucher(SH_BAYAR bayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_BAYAR set JM_VOUCHER = JM_VOUCHER + @voucher where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = bayar.ID;
                    command.Parameters.Add("@voucher", SqlDbType.Decimal).Value = bayar.JM_VOUCHER;
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

        public string updateBayarCard(SH_BAYAR bayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_BAYAR set JM_CARD = JM_CARD + @card where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = bayar.ID;
                    command.Parameters.Add("@card", SqlDbType.Decimal).Value = bayar.JM_CARD;
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

        public string updateTglTrans(SH_BAYAR bayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_BAYAR set TGL_TRANS = @tglTrans where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = bayar.TGL_TRANS;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = bayar.ID;
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

        public string updateJualTglTrans(SH_JUAL jual)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_JUAL set TGL_TRANS = @tglTrans where ID_BAYAR = @idBayar ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = jual.TGL_TRANS;
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = jual.ID_BAYAR;
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

        public string insertSHCard(SH_CARD card)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_CARD (ID_BAYAR, NET_CARD, KD_CCARD, NO_CCARD, VL_CCARD, BANK, EDC) values " +
                    " (@idBayar, @netCard, @kdCard, @noCard, @vlCard, @bank, @edc) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.VarChar).Value = card.ID_BAYAR;
                    command.Parameters.Add("@netCard", SqlDbType.Decimal).Value = card.NET_CARD;
                    command.Parameters.Add("@kdCard", SqlDbType.VarChar).Value = card.KD_CCARD;
                    command.Parameters.Add("@noCard", SqlDbType.VarChar).Value = card.NO_CCARD;
                    command.Parameters.Add("@vlCard", SqlDbType.VarChar).Value = card.VL_CCARD;
                    command.Parameters.Add("@bank", SqlDbType.VarChar).Value = card.BANK;
                    command.Parameters.Add("@edc", SqlDbType.VarChar).Value = card.EDC;

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

        public string insertSHJual(SH_JUAL jual)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_JUAL (ID_BAYAR, ID_KDBRG, KODE_CUST, KODE, TGL_TRANS, NO_BON, JUAL_RETUR, ITEM_CODE, BARCODE, QTY, TAG_PRICE, BON_PRICE, JNS_DISC, DISC_P, DISC_R, NILAI_BYR, DPP, PPN, ALASAN, FNOACARA, CREATED_DATE, CREATED_BY, NO_REFF) values " +
                    " (@idBayar, @idKdbrg, @kodeCust, @kodeCT, @tglTrans, @noBon, @jualRetur, @itemCode, @barcode, @qty, @tagPrice, @bonPrice, @jnsDisc, @discPrice, @discRate, @bayar, @dpp, @ppn, @alasan, @fnoAcara, GETDATE(), @name, @noreff); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = jual.ID_BAYAR;
                    command.Parameters.Add("@idKdbrg", SqlDbType.BigInt).Value = jual.ID_KDBRG;
                    command.Parameters.Add("@kodeCust", SqlDbType.VarChar).Value = jual.KODE_CUST;
                    command.Parameters.Add("@kodeCT", SqlDbType.VarChar).Value = jual.KODE;
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = jual.TGL_TRANS;
                    command.Parameters.Add("@noBon", SqlDbType.VarChar).Value = jual.NO_BON;
                    command.Parameters.Add("@jualRetur", SqlDbType.VarChar).Value = jual.JUAL_RETUR;
                    command.Parameters.Add("@itemCode", SqlDbType.VarChar).Value = jual.ITEM_CODE;
                    command.Parameters.Add("@barcode", SqlDbType.VarChar).Value = jual.BARCODE;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = jual.QTY;
                    command.Parameters.Add("@tagPrice", SqlDbType.Decimal).Value = jual.TAG_PRICE;
                    command.Parameters.Add("@bonPrice", SqlDbType.Decimal).Value = jual.BON_PRICE;
                    command.Parameters.Add("@jnsDisc", SqlDbType.VarChar).Value = jual.JNS_DISC;
                    command.Parameters.Add("@discPrice", SqlDbType.Decimal).Value = jual.DISC_P;
                    command.Parameters.Add("@discRate", SqlDbType.Decimal).Value = jual.DISC_R;
                    command.Parameters.Add("@bayar", SqlDbType.Decimal).Value = jual.NILAI_BYR;
                    command.Parameters.Add("@dpp", SqlDbType.Decimal).Value = jual.DPP;
                    command.Parameters.Add("@ppn", SqlDbType.Decimal).Value = jual.PPN;
                    command.Parameters.Add("@alasan", SqlDbType.VarChar).Value = jual.ALASAN;
                    command.Parameters.Add("@fnoAcara", SqlDbType.VarChar).Value = jual.FNOACARA;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = jual.CREATED_BY;
                    command.Parameters.Add("@noreff", SqlDbType.VarChar).Value = jual.NO_REFF;
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

        public string insertSHVoucher(SH_VOUCHER voucher)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_VOUCHER (ID_BAYAR, KODE_CUST, NO_VCR, NILAI, NO_CARD, CREATED_BY, CREATED_DATE, STATUS) values " +
                    " (@idBayar, @kodeCust, @noVCR, @nilai, @noCard, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = voucher.ID_BAYAR;
                    command.Parameters.Add("@kodeCust", SqlDbType.VarChar).Value = voucher.KODE_CUST;
                    command.Parameters.Add("@noVCR", SqlDbType.VarChar).Value = voucher.NO_VCR;
                    command.Parameters.Add("@nilai", SqlDbType.Decimal).Value = voucher.NILAI;
                    command.Parameters.Add("@noCard", SqlDbType.VarChar).Value = voucher.NO_CARD;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = voucher.CREATED_BY;
                    
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

        public string insertSHVoucherNew(SH_VOUCHER voucher)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_VOUCHER (ID_BAYAR, KODE_CUST, NO_VCR, NILAI, NO_CARD, CREATED_BY, CREATED_DATE, STATUS, NO_BON) values " +
                    " (@idBayar, @kodeCust, @noVCR, @nilai, @noCard, @createdBy, getdate(), 1, @nobon) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = voucher.ID_BAYAR;
                    command.Parameters.Add("@kodeCust", SqlDbType.VarChar).Value = voucher.KODE_CUST;
                    command.Parameters.Add("@noVCR", SqlDbType.VarChar).Value = voucher.NO_VCR;
                    command.Parameters.Add("@nilai", SqlDbType.Decimal).Value = voucher.NILAI;
                    command.Parameters.Add("@noCard", SqlDbType.VarChar).Value = voucher.NO_CARD;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = voucher.CREATED_BY;
                    command.Parameters.Add("@nobon", SqlDbType.VarChar).Value = voucher.NO_BON;
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


        public string insertSHAcaraValue(SH_ACARA_VALUE acaraValue)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_ACARA_VALUE (ID_BAYAR, ID_ACARA, PARAM, VALUE, CREATED_BY, CREATED_DATE, STATUS) values " +
                    " (@idBayar, @idAcara, @param, @value, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = acaraValue.ID_BAYAR;
                    command.Parameters.Add("@idAcara", SqlDbType.BigInt).Value = acaraValue.ID_ACARA;
                    command.Parameters.Add("@param", SqlDbType.VarChar).Value = acaraValue.PARAM;
                    command.Parameters.Add("@value", SqlDbType.VarChar).Value = acaraValue.VALUE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = acaraValue.CREATED_BY;

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

        public string deleteVoucher(Int64 idBayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("DELETE SH_VOUCHER where ID_BAYAR = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = idBayar;
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

        public string deleteAcaraValue(Int64 idBayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("DELETE SH_ACARA_VALUE where ID_BAYAR = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = idBayar;
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

        public void insertNilaiMargin(Int64 idJual, decimal margin)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertNilaiMargin", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idJual", idJual));
                    command.Parameters.Add(new SqlParameter("@margin", margin));
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

        public string insertNoDoc(NO_DOC noDoc)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert NO_DOC ( NO_URUT, KODE, CREATED_BY, CREATED_DATE, FLAG ) values " +
                    " (@noUrut, @kode, @createdBy, getdate(), @flag) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noUrut", SqlDbType.VarChar).Value = noDoc.NO_URUT;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = noDoc.KODE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = noDoc.CREATED_BY;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = noDoc.FLAG;

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

        public string updateNoDoc(NO_DOC noDoc)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update NO_DOC set NO_URUT = @noUrut, UPDATED_BY = @createdBy, UPDATED_DATE = GETDATE() where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@noUrut", SqlDbType.VarChar).Value = noDoc.NO_URUT;
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = noDoc.ID;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = noDoc.CREATED_BY;
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

        public string updateMember(TEMP_STRUCK struck)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update TEMP_STRUCK set MEMBER = @member, EPC = @epc, DISC_TEMP = @discTemp where CREATED_BY = @createdBy and RETUR = 'No' ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = struck.CREATED_BY;
                    command.Parameters.Add("@member", SqlDbType.VarChar).Value = struck.MEMBER;
                    command.Parameters.Add("@epc", SqlDbType.VarChar).Value = struck.EPC;
                    command.Parameters.Add("@discTemp", SqlDbType.Int).Value = struck.DISC_TEMP;
                    
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

        public string deleteShMember(Int64 idBayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("Delete from SH_MEMBER where ID_BAYAR = @idBayar");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = idBayar;
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

        public string deleteShEmployee(Int64 idBayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("Delete from SH_EMPLOYEE where ID_BAYAR = @idBayar");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = idBayar;
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

        public string deleteTempDoc(string createdBy)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("Delete from TEMP_DOC where CREATED_BY = @createdBy");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = createdBy;
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

        public string insertTempDoc(TEMP_DOC tempDOC)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert TEMP_DOC ( KODE, CREATED_BY, CREATED_DATE, FLAG ) values " +
                    " (@kode, @createdBy, getdate(), @flag) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = tempDOC.KODE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = tempDOC.CREATED_BY;
                    command.Parameters.Add("@flag", SqlDbType.VarChar).Value = tempDOC.FLAG;

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

        public string insertShMember(SH_MEMBER shMember)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_MEMBER ( ID_BAYAR, ID_MEMBER, NAMA, PHONE, NO_BON, DISC_RATE, DISC_PRICE, CREATED_BY, CREATED_DATE, STATUS ) values " +
                    " (@idBayar, @idMember, @nama, @phone, @no_bon, @disc, @discPrice, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.BigInt).Value = shMember.ID_BAYAR;
                    command.Parameters.Add("@idMember", SqlDbType.BigInt).Value = shMember.ID_MEMBER;
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = shMember.NAMA;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = shMember.PHONE;
                    command.Parameters.Add("@no_bon", SqlDbType.VarChar).Value = shMember.NO_BON;
                    command.Parameters.Add("@disc", SqlDbType.Int).Value = shMember.DISC_RATE;
                    command.Parameters.Add("@discPrice", SqlDbType.Decimal).Value = shMember.DISC_PRICE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = shMember.CREATED_BY;

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

        public string insertShEmployee(SH_EMPLOYEE shEmployee)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_EMPLOYEE ( ID_BAYAR, NAME, POSITION, CREATED_BY, CREATED_DATE, NIK ) values " +
                    " (@idBayar, @name, @position, @createdBy, getdate(), @nik) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idBayar", SqlDbType.VarChar).Value = shEmployee.ID_BAYAR;
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = shEmployee.NAME;
                    command.Parameters.Add("@position", SqlDbType.VarChar).Value = shEmployee.POSITION;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = shEmployee.CREATED_BY;
                    command.Parameters.Add("@nik", SqlDbType.VarChar).Value = shEmployee.NIK;

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

        public string insertMsEmployee(MS_EMPLOYEE msEmployee)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert MS_EMPLOYEE ( NIK, NAMA, JABATAN, JOIN_DATE, LIMIT, STATUS_EMPLOYEE, CREATED_BY, CREATED_DATE, STATUS, STATUS_CARD, TIPE, LIMIT_DELAMI, STATUS_EPC, SISA_LIMIT ) values " +
                    " (@NIK, @NAMA, @JABATAN, @JOIN_DATE, @LIMIT, @STATUS_EMPLOYEE, @CREATED_BY, getdate(), 1, 1, @TIPE, @LIMIT_DELAMI, @STATUS_EPC, @sisaLimit) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@NIK", SqlDbType.VarChar).Value = msEmployee.NIK;
                    command.Parameters.Add("@NAMA", SqlDbType.VarChar).Value = msEmployee.NAMA;
                    command.Parameters.Add("@JABATAN", SqlDbType.VarChar).Value = msEmployee.JABATAN;
                    command.Parameters.Add("@JOIN_DATE", SqlDbType.Date).Value = msEmployee.JOIN_DATE;
                    command.Parameters.Add("@LIMIT", SqlDbType.Decimal).Value = msEmployee.LIMIT;

                    command.Parameters.Add("@STATUS_EMPLOYEE", SqlDbType.VarChar).Value = msEmployee.STATUS_EMPLOYEE;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = msEmployee.CREATED_BY;
                    command.Parameters.Add("@TIPE", SqlDbType.VarChar).Value = msEmployee.TIPE;
                    command.Parameters.Add("@LIMIT_DELAMI", SqlDbType.Decimal).Value = msEmployee.LIMIT_DELAMI;
                    command.Parameters.Add("@STATUS_EPC", SqlDbType.VarChar).Value = msEmployee.STATUS_EPC;
                    command.Parameters.Add("@sisaLimit", SqlDbType.VarChar).Value = msEmployee.SISA_LIMIT;
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

        public string updateMsEmployee(MS_EMPLOYEE msEmployee)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update MS_EMPLOYEE set NAMA = @nama, JABATAN = @jabatan, JOIN_DATE = @join, LIMIT = @limit, LIMIT_DELAMI = @limitDel, UPDATED_BY = @updatedBy, UPDATED_DATE = getdate(), SISA_LIMIT = @sisa where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = msEmployee.NAMA;
                    command.Parameters.Add("@jabatan", SqlDbType.VarChar).Value = msEmployee.JABATAN;
                    command.Parameters.Add("@join", SqlDbType.Date).Value = msEmployee.JOIN_DATE;
                    command.Parameters.Add("@limit", SqlDbType.Decimal).Value = msEmployee.LIMIT;
                    command.Parameters.Add("@limitDel", SqlDbType.Decimal).Value = msEmployee.LIMIT_DELAMI;
                    command.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = msEmployee.UPDATED_BY;
                    command.Parameters.Add("@sisa", SqlDbType.Decimal).Value = msEmployee.SISA_LIMIT;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = msEmployee.ID;
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

        public string deleteMsEmployee(MS_EMPLOYEE msEmployee)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update MS_EMPLOYEE set STATUS = 0, UPDATED_BY = @updatedBy, UPDATED_DATE = getdate(), STATUS_EMPLOYEE = @status where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = msEmployee.UPDATED_BY;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = msEmployee.STATUS_EMPLOYEE;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = msEmployee.ID;
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

        #region void
        public string updateVoidHeader(SH_BAYAR bayar)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_voidTransactionHeader", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idBayar", bayar.ID));
                    command.Parameters.Add(new SqlParameter("@noBon", bayar.NO_BON));
                    command.Parameters.Add(new SqlParameter("@voidBy", bayar.VOID_BY));
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

        public string updateVoidDetail(SH_BAYAR bayar, Int64 newIdBayar)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_voidTransactionDetail", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idBayar", bayar.ID));
                    command.Parameters.Add(new SqlParameter("@newIdBayar", newIdBayar));
                    command.Parameters.Add(new SqlParameter("@noBon", bayar.NO_BON));
                    command.Parameters.Add(new SqlParameter("@voidBy", bayar.VOID_BY));
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
        #endregion

        #region WholeSale

        public List<SH_PUTUS_HEADER> getSHPutusHeader(string where)
        {
            List<SH_PUTUS_HEADER> listBayar = new List<SH_PUTUS_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, KODE_CUST, TGL_TRANS, NO_BON, QTY, NET_BAYAR, CARD, JM_UANG, KEMBALI, " +
                    " NET_CASH, VOUCHER, CREATED_BY, STATUS_STORE, STATUS_HEADER, SEND_DATE, MARGIN, KODE, FRETUR " +
                    " from SH_PUTUS_HEADER {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_PUTUS_HEADER item = new SH_PUTUS_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(2);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(3);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.CARD = reader.IsDBNull(reader.GetOrdinal("CARD")) ? "" : reader.GetString(6);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(7);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(8);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(9);
                        item.VOUCHER = reader.IsDBNull(reader.GetOrdinal("VOUCHER")) ? "" : reader.GetString(10);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(11);
                        item.STATUS_STORE = reader.IsDBNull(reader.GetOrdinal("STATUS_STORE")) ? "" : reader.GetString(12);

                        item.STATUS_HEADER = reader.IsDBNull(reader.GetOrdinal("STATUS_HEADER")) ? "" : reader.GetString(13);
                        item.SEND_DATE = reader.IsDBNull(reader.GetOrdinal("SEND_DATE")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetDecimal(15);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(16);
                        item.FRETUR = reader.IsDBNull(reader.GetOrdinal("FRETUR")) ? "No" : reader.GetString(17);

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
        
        public List<SH_PUTUS_DETAIL> getSHPutusDetail(string where)
        {
            List<SH_PUTUS_DETAIL> listJual = new List<SH_PUTUS_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_BAYAR, ID_KDBRG, KODE_CUST, TGL_TRANS, NO_BON, ITEM_CODE, " +
                    " TAG_PRICE, BON_PRICE, NILAI_BYR, MARGIN " +
                    " from SH_PUTUS_DETAIL {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_PUTUS_DETAIL item = new SH_PUTUS_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_BAYAR = reader.GetInt64(1);
                        item.ID_KDBRG = reader.GetInt64(2);
                        item.KODE_CUST = reader.GetString(3);
                        item.TGL_TRANS = reader.GetDateTime(4);
                        item.NO_BON = reader.GetString(5);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(6);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(7);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(8);
                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(9);
                        item.MARGIN = reader.GetDecimal(10);

                        listJual.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listJual;
        }

        public List<SH_PUTUS_DETAIL> getSHPutusDetailWithSP(string tableName, string selectTable, string where)
        {
            List<SH_PUTUS_DETAIL> listJual = new List<SH_PUTUS_DETAIL>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("usp_joinQryWithIDKDBRG", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@tableName", tableName));
                    command.Parameters.Add(new SqlParameter("@selectTable", selectTable));
                    command.Parameters.Add(new SqlParameter("@where", where));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_PUTUS_DETAIL item = new SH_PUTUS_DETAIL();
                        item.ID = reader.GetInt64(0);
                        item.ID_BAYAR = reader.GetInt64(1);
                        item.ID_KDBRG = reader.IsDBNull(reader.GetOrdinal("ID_KDBRG")) ? 0 : reader.GetInt64(2);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(3);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(4);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? DateTime.Now : reader.GetDateTime(5);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(6);
                        item.ITEM_CODE = reader.IsDBNull(reader.GetOrdinal("ITEM_CODE")) ? "" : reader.GetString(7);
                        item.TAG_PRICE = reader.IsDBNull(reader.GetOrdinal("TAG_PRICE")) ? 0 : reader.GetDecimal(8);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(9);
                        item.NILAI_BYR = reader.IsDBNull(reader.GetOrdinal("NILAI_BYR")) ? 0 : reader.GetDecimal(10);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetDecimal(11);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(12);
                        item.JUAL_RETUR = reader.IsDBNull(reader.GetOrdinal("JUAL_RETUR")) ? "" : reader.GetString(13);
                        item.QTY_ACTUAL = reader.IsDBNull(reader.GetOrdinal("QTY_ACTUAL")) ? 0 : reader.GetInt32(14);
                        item.FSTATUS = reader.IsDBNull(reader.GetOrdinal("FSTATUS")) ? " " : reader.GetString(15);
                        item.FRETUR = reader.IsDBNull(reader.GetOrdinal("FRETUR")) ? "No" : reader.GetString(16);

                        item.FBARCODE = reader.IsDBNull(reader.GetOrdinal("FBARCODE")) ? "" : reader.GetString(17);
                        item.FITEM_CODE = reader.IsDBNull(reader.GetOrdinal("FITEM_CODE")) ? "" : reader.GetString(18);
                        item.FBRAND = reader.IsDBNull(reader.GetOrdinal("FBRAND")) ? "" : reader.GetString(19);
                        item.FART_DESC = reader.IsDBNull(reader.GetOrdinal("FART_DESC")) ? "" : reader.GetString(20);
                        item.FCOL_DESC = reader.IsDBNull(reader.GetOrdinal("FCOL_DESC")) ? "" : reader.GetString(21);
                        item.FSIZE_DESC = reader.IsDBNull(reader.GetOrdinal("FSIZE_DESC")) ? "" : reader.GetString(22);

                        listJual.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listJual;
        }

        public string insertPutusHeaderRetID(SH_PUTUS_HEADER putusHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_PUTUS_HEADER (KODE_CUST, KODE, STATUS_STORE, QTY, CREATED_BY) values " +
                    " (@Kode, @kodeCT, @Store, @Qty, @CreatedBy); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@Kode", SqlDbType.VarChar).Value = putusHeader.KODE_CUST;
                    command.Parameters.Add("@kodeCT", SqlDbType.VarChar).Value = putusHeader.KODE;
                    command.Parameters.Add("@Store", SqlDbType.VarChar).Value = putusHeader.STATUS_STORE;
                    command.Parameters.Add("@Qty", SqlDbType.Int).Value = putusHeader.QTY;
                    command.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = putusHeader.CREATED_BY;

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
        public string insertPutusHeaderRetIDNEW(SH_PUTUS_HEADER putusHeader)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert SH_PUTUS_HEADER (KODE_CUST, KODE, STATUS_STORE, QTY, CREATED_BY, NO_SO, NO_SCAN) values " +
                    " (@Kode, @kodeCT, @Store, @Qty, @CreatedBy, @noso, @noscan); SELECT CAST(scope_identity() AS int) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@Kode", SqlDbType.VarChar).Value = putusHeader.KODE_CUST;
                    command.Parameters.Add("@kodeCT", SqlDbType.VarChar).Value = putusHeader.KODE;
                    command.Parameters.Add("@Store", SqlDbType.VarChar).Value = putusHeader.STATUS_STORE;
                    command.Parameters.Add("@Qty", SqlDbType.Int).Value = putusHeader.QTY;
                    command.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = putusHeader.CREATED_BY;
                    command.Parameters.Add("@noso", SqlDbType.VarChar).Value = putusHeader.NO_SO;
                    command.Parameters.Add("@noscan", SqlDbType.VarChar).Value = putusHeader.NO_SCAN;

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

        public string updatePutusHeader(SH_PUTUS_HEADER putusHeader)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_PUTUS_HEADER set TGL_TRANS = @tglTrans, NO_BON = @bon, NET_CASH = @cash, JM_UANG = @jmUang, " +
                    "KEMBALI = @kembali, NET_BAYAR = @bayar, CREATED_DATE = GETDATE(), STATUS_HEADER = @statusHeader, SEND_DATE = @dateKirim, " +
                    " MARGIN = @margin, FRETUR = @retur where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = putusHeader.TGL_TRANS;
                    command.Parameters.Add("@bon", SqlDbType.VarChar).Value = putusHeader.NO_BON;
                    command.Parameters.Add("@cash", SqlDbType.Decimal).Value = putusHeader.NET_CASH;
                    command.Parameters.Add("@jmUang", SqlDbType.Decimal).Value = putusHeader.JM_UANG;
                    command.Parameters.Add("@kembali", SqlDbType.Decimal).Value = putusHeader.KEMBALI;
                    command.Parameters.Add("@bayar", SqlDbType.Decimal).Value = putusHeader.NET_BAYAR;
                    command.Parameters.Add("@statusHeader", SqlDbType.VarChar).Value = putusHeader.STATUS_HEADER;
                    command.Parameters.Add("@dateKirim", SqlDbType.DateTime2).Value = putusHeader.SEND_DATE;
                    command.Parameters.Add("@margin", SqlDbType.Int).Value = putusHeader.MARGIN;
                    command.Parameters.Add("@retur", SqlDbType.VarChar).Value = putusHeader.FRETUR;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = putusHeader.ID;
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

        public void insertPutusDetail(SH_PUTUS_DETAIL putusDetail, string createdBy)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertPutusDetail", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idHeader", putusDetail.ID_BAYAR));
                    command.Parameters.Add(new SqlParameter("@namaCust", putusDetail.KODE_CUST));
                    command.Parameters.Add(new SqlParameter("@kodeCust", putusDetail.KODE));
                    command.Parameters.Add(new SqlParameter("@noBon", putusDetail.NO_BON));
                    command.Parameters.Add(new SqlParameter("@margin", putusDetail.MARGIN));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
                    command.Parameters.Add(new SqlParameter("@retur", putusDetail.FRETUR));
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
        #region "New SO"
        public string updatePutusHeaderNEW(SH_PUTUS_HEADER putusHeader)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_PUTUS_HEADER set TGL_TRANS = @tglTrans, NO_BON = @bon, NET_CASH = @cash, JM_UANG = @jmUang, " +
                    "KEMBALI = @kembali, NET_BAYAR = @bayar, CREATED_DATE = GETDATE(), STATUS_HEADER = @statusHeader, SEND_DATE = @dateKirim, " +
                    " MARGIN = @margin, FRETUR = @retur where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime).Value = putusHeader.TGL_TRANS;
                    command.Parameters.Add("@bon", SqlDbType.VarChar).Value = putusHeader.NO_BON;
                    command.Parameters.Add("@cash", SqlDbType.Decimal).Value = putusHeader.NET_CASH;
                    command.Parameters.Add("@jmUang", SqlDbType.Decimal).Value = putusHeader.JM_UANG;
                    command.Parameters.Add("@kembali", SqlDbType.Decimal).Value = putusHeader.KEMBALI;
                    command.Parameters.Add("@bayar", SqlDbType.Decimal).Value = putusHeader.NET_BAYAR;
                    command.Parameters.Add("@statusHeader", SqlDbType.VarChar).Value = putusHeader.STATUS_HEADER;
                    command.Parameters.Add("@dateKirim", SqlDbType.DateTime).Value = putusHeader.SEND_DATE;
                    command.Parameters.Add("@margin", SqlDbType.Decimal).Value = putusHeader.MARGIN;
                    command.Parameters.Add("@retur", SqlDbType.VarChar).Value = putusHeader.FRETUR;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = putusHeader.ID;
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

        public void insertPutusDetailFromSO(SH_PUTUS_DETAIL putusDetail, string createdBy, String NO_SO)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertPutusDetailFromSO", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@idHeader", putusDetail.ID_BAYAR));
                    command.Parameters.Add(new SqlParameter("@namaCust", putusDetail.KODE_CUST));
                    command.Parameters.Add(new SqlParameter("@kodeCust", putusDetail.KODE));
                    command.Parameters.Add(new SqlParameter("@noBon", putusDetail.NO_BON));
                    command.Parameters.Add(new SqlParameter("@margin", putusDetail.MARGIN));
                    command.Parameters.Add(new SqlParameter("@createdBy", createdBy));
                    command.Parameters.Add(new SqlParameter("@retur", putusDetail.FRETUR));
                    command.Parameters.Add(new SqlParameter("@idHeaderSO", putusDetail.ID_HEADER_SO));
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

        public List<SH_PUTUS_HEADER> getSHPutusHeaderJoinSoOrder(string where)
        {
            List<SH_PUTUS_HEADER> listBayar = new List<SH_PUTUS_HEADER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, KODE_CUST, TGL_TRANS, NO_BON, QTY, NET_BAYAR, CARD, JM_UANG, KEMBALI, " +
                    " NET_CASH, VOUCHER, CREATED_BY, STATUS_STORE, STATUS_HEADER, SEND_DATE, MARGIN, KODE, FRETUR, STATUS_ORDER, NO_SO, NO_SCAN " +
                    " from VW_ListSHPutusHeaderJoinSoOrder {0} ORDER BY ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_PUTUS_HEADER item = new SH_PUTUS_HEADER();
                        item.ID = reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.TGL_TRANS = reader.IsDBNull(reader.GetOrdinal("TGL_TRANS")) ? (DateTime?)null : reader.GetDateTime(2);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(3);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.CARD = reader.IsDBNull(reader.GetOrdinal("CARD")) ? "" : reader.GetString(6);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(7);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(8);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(9);
                        item.VOUCHER = reader.IsDBNull(reader.GetOrdinal("VOUCHER")) ? "" : reader.GetString(10);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(11);
                        item.STATUS_STORE = reader.IsDBNull(reader.GetOrdinal("STATUS_STORE")) ? "" : reader.GetString(12);

                        item.STATUS_HEADER = reader.IsDBNull(reader.GetOrdinal("STATUS_HEADER")) ? "" : reader.GetString(13);
                        item.SEND_DATE = reader.IsDBNull(reader.GetOrdinal("SEND_DATE")) ? (DateTime?)null : reader.GetDateTime(14);
                        item.MARGIN = reader.IsDBNull(reader.GetOrdinal("MARGIN")) ? 0 : reader.GetDecimal(15);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(16);
                        item.FRETUR = reader.IsDBNull(reader.GetOrdinal("FRETUR")) ? "No" : reader.GetString(17);
                        item.STATUS_ORDER = reader.IsDBNull(reader.GetOrdinal("STATUS_ORDER")) ? "-" : reader.GetString(18);
                        item.NO_SO = reader.IsDBNull(reader.GetOrdinal("NO_SO")) ? "-" : reader.GetString(19);
                        item.NO_SCAN = reader.IsDBNull(reader.GetOrdinal("NO_SCAN")) ? "-" : reader.GetString(20);
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

        #endregion
        public string updateQTYPutusDetail(SH_PUTUS_DETAIL putusDetail)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_PUTUS_DETAIL set QTY_ACTUAL = @qtyActual, FSTATUS = @status where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@qtyActual", SqlDbType.Int).Value = putusDetail.QTY_ACTUAL;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = putusDetail.FSTATUS;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = putusDetail.ID;
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

        public string updateStatusPutusHeader(SH_PUTUS_HEADER putusHeader)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SH_PUTUS_HEADER set STATUS_HEADER = @status, SEND_DATE = @sendDate, TGL_TRANS = @tglTrans where ID = @id ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@sendDate", SqlDbType.DateTime2).Value = putusHeader.SEND_DATE;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = putusHeader.STATUS_HEADER;
                    command.Parameters.Add("@tglTrans", SqlDbType.DateTime2).Value = putusHeader.TGL_TRANS;
                    command.Parameters.Add("@id", SqlDbType.BigInt).Value = putusHeader.ID;
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
        public string updateTglTransSOWHOLESALE(SH_PUTUS_HEADER putusHeader, string noso)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SO_WHOLESALES_HEADER set TGL_TRANS = @sendDate where NO_SO = @noSO ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@sendDate", SqlDbType.DateTime2).Value = putusHeader.TGL_TRANS;
                    command.Parameters.Add("@noSO", SqlDbType.VarChar).Value = noso;

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
        public string updateTglKirim_1SOWHOLESALE(SH_PUTUS_HEADER putusHeader , string noso)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SO_WHOLESALES set TGL_REAL_1 = @sendDate, TGL_KIRIM_1 = @sendDate where NO_SO = @noSO ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@sendDate", SqlDbType.DateTime2).Value = putusHeader.SEND_DATE;
                    command.Parameters.Add("@noSO", SqlDbType.VarChar).Value = noso;

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
        public string updateTglKirim_2SOWHOLESALE(SH_PUTUS_HEADER putusHeader, string noso)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("update SO_WHOLESALES set TGL_REAL_2 = @sendDate, TGL_KIRIM_2 = @sendDate where NO_SO = @noSO ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@sendDate", SqlDbType.DateTime2).Value = putusHeader.SEND_DATE;
                    command.Parameters.Add("@noSO", SqlDbType.VarChar).Value = noso;

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

        #endregion
                                                                                                                    
        public List<NO_SALE> getListNoSale(string where)
        {
            List<NO_SALE> listNoSale = new List<NO_SALE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_SHOWROOM, KODE, SHOWROOM, NO_SALE_DATE, CREATED_BY, CREATED_DATE, STATUS " +
                    " from NO_SALE {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        NO_SALE item = new NO_SALE();
                        item.ID = reader.GetInt64(0);
                        item.ID_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("ID_SHOWROOM")) ? 0 : reader.GetInt64(1);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(2);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(3);
                        item.NO_SALE_DATE = reader.IsDBNull(reader.GetOrdinal("NO_SALE_DATE")) ? (DateTime?)null : reader.GetDateTime(4);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(5);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(7);

                        listNoSale.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNoSale;
        }

        public List<NO_SALE> getListNoSaleByKodeAndDate(NO_SALE noSale, string where)
        {
            List<NO_SALE> listNoSale = new List<NO_SALE>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, ID_SHOWROOM, KODE, SHOWROOM, NO_SALE_DATE, CREATED_BY, CREATED_DATE, STATUS " +
                    " from NO_SALE where KODE = @kode AND NO_SALE_DATE = @date {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = noSale.KODE;
                    command.Parameters.Add("@date", SqlDbType.DateTime2).Value = noSale.NO_SALE_DATE;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        NO_SALE item = new NO_SALE();
                        item.ID = reader.GetInt64(0);
                        item.ID_SHOWROOM = reader.IsDBNull(reader.GetOrdinal("ID_SHOWROOM")) ? 0 : reader.GetInt64(1);
                        item.KODE = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "" : reader.GetString(2);
                        item.SHOWROOM = reader.IsDBNull(reader.GetOrdinal("SHOWROOM")) ? "" : reader.GetString(3);
                        item.NO_SALE_DATE = reader.IsDBNull(reader.GetOrdinal("NO_SALE_DATE")) ? (DateTime?)null : reader.GetDateTime(4);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(5);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(6);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(7);

                        listNoSale.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNoSale;
        }

        public List<SH_BAYAR> getSHBayarByKodeAndDate(SH_BAYAR bayar, string where)
        {
            List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select ID, KODE_CUST, TGL_TRANS, NO_BON, QTY, NET_BAYAR, CARD, JM_UANG, KEMBALI, " +
                    " NET_CASH, VOUCHER, CREATED_BY, STATUS_STORE, JM_CARD, JM_VOUCHER " +
                    " from SH_BAYAR where KODE = @kode AND TGL_TRANS = @tgl {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = bayar.KODE;
                    command.Parameters.Add("@tgl", SqlDbType.DateTime2).Value = bayar.TGL_TRANS;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        SH_BAYAR item = new SH_BAYAR();
                        item.ID = reader.GetInt64(0);
                        item.KODE_CUST = reader.IsDBNull(reader.GetOrdinal("KODE_CUST")) ? "" : reader.GetString(1);
                        item.TGL_TRANS = reader.GetDateTime(2);
                        item.NO_BON = reader.IsDBNull(reader.GetOrdinal("NO_BON")) ? "" : reader.GetString(3);
                        item.QTY = reader.IsDBNull(reader.GetOrdinal("QTY")) ? 0 : reader.GetInt32(4);
                        item.NET_BAYAR = reader.IsDBNull(reader.GetOrdinal("NET_BAYAR")) ? 0 : reader.GetDecimal(5);
                        item.CARD = reader.IsDBNull(reader.GetOrdinal("CARD")) ? "" : reader.GetString(6);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(7);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(8);
                        item.NET_CASH = reader.IsDBNull(reader.GetOrdinal("NET_CASH")) ? 0 : reader.GetDecimal(9);
                        item.VOUCHER = reader.IsDBNull(reader.GetOrdinal("VOUCHER")) ? "" : reader.GetString(10);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(11);
                        item.STATUS_STORE = reader.IsDBNull(reader.GetOrdinal("STATUS_STORE")) ? "" : reader.GetString(12);
                        item.JM_CARD = reader.IsDBNull(reader.GetOrdinal("JM_CARD")) ? 0 : reader.GetDecimal(13);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(14);

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

        public string insertNoSale(NO_SALE noSale)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert NO_SALE ( ID_SHOWROOM, KODE, SHOWROOM, NO_SALE_DATE, CREATED_BY, CREATED_DATE, STATUS ) values " +
                    " (@idShow, @kode, @show, @noSaleDate, @createdBy, getdate(), 1) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idShow", SqlDbType.BigInt).Value = noSale.ID_SHOWROOM;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = noSale.KODE;
                    command.Parameters.Add("@show", SqlDbType.VarChar).Value = noSale.SHOWROOM;
                    command.Parameters.Add("@noSaleDate", SqlDbType.DateTime2).Value = noSale.NO_SALE_DATE;
                    command.Parameters.Add("@createdBy", SqlDbType.VarChar).Value = noSale.CREATED_BY;
                    
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

        public void insertUpdatePointMember(string noCard, Int64 idBayar, int point)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertUpdatePointMember", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noCard", noCard));
                    command.Parameters.Add(new SqlParameter("@idBayar", idBayar));
                    command.Parameters.Add(new SqlParameter("@point", point));
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

        public void insertUpdatePointMemberRedeem(string noCard, Int64 idBayar, int point)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_insertUpdatePointMemberRedeem", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@noCard", noCard));
                    command.Parameters.Add(new SqlParameter("@idBayar", idBayar));
                    command.Parameters.Add(new SqlParameter("@point", point));
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

        public string checkVoucher(MS_VOUCHER msVoucher)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_checkVoucher", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@kodeCust", msVoucher.KODE));
                    command.Parameters.Add(new SqlParameter("@noVoucher", msVoucher.NO_VOUCHER));
                    command.Parameters.Add(new SqlParameter("@tglTrans", msVoucher.TGL_TRANS));
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

        public string updateMsVoucherNew(MS_VOUCHER voucher)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_VOUCHER SET STATUS_VOUCHER = @statvoucher, UPDATED_BY = @updBy, UPDATED_DATE = GETDATE() " +
                    " , KODE = @kode, KODE_CREATED = GETDATE() WHERE NO_VOUCHER = @noVCR");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@statvoucher", SqlDbType.VarChar).Value = voucher.STATUS_VOUCHER;
                    command.Parameters.Add("@updBy", SqlDbType.VarChar).Value = voucher.UPDATED_BY;
                    command.Parameters.Add("@noVCR", SqlDbType.VarChar).Value = voucher.NO_VOUCHER;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = voucher.KODE;
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

        public List<VW_REPRINTBON> GetReprintBon(string where)
        {
            List<VW_REPRINTBON> ListVwRprint = new List<VW_REPRINTBON>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select NO_BON, ARTICLE, BON_PRICE, DISC_P, DISC_R, NET_PRICE, TOTAL_NET, JM_VOUCHER, JM_CARD, "+
                    "JM_UANG, KEMBALI, CREATED_DATE, CREATED_BY, TGL_TRANS, KODE, SHOWROOM, LOGO_IMG, ALAMAT, PHONE, QTY, BRAND, VOUCHER, CARD, DPP, PPN," +
                    " ONGKIR, JM_ONGKIR, JM_FREE_ONGKIR" +
                    " from VW_REPRINTBON  {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        VW_REPRINTBON item = new VW_REPRINTBON();
                        item.NO_BON = reader.GetString(0);
                        item.ARTICLE =  reader.GetString(1);
                        item.BON_PRICE = reader.IsDBNull(reader.GetOrdinal("BON_PRICE")) ? 0 : reader.GetDecimal(2);
                        item.DISC_P = reader.IsDBNull(reader.GetOrdinal("DISC_P")) ? 0 : reader.GetDecimal(3);
                        item.DISC_R = reader.IsDBNull(reader.GetOrdinal("DISC_R")) ? 0 : reader.GetDecimal(4);
                        item.NET_PRICE = reader.IsDBNull(reader.GetOrdinal("NET_PRICE")) ? 0 : reader.GetDecimal(5);
                        item.TOTAL_NET = reader.IsDBNull(reader.GetOrdinal("TOTAL_NET")) ? 0 : reader.GetDecimal(6);
                        item.JM_VOUCHER = reader.IsDBNull(reader.GetOrdinal("JM_VOUCHER")) ? 0 : reader.GetDecimal(7);
                        item.JM_CARD = reader.IsDBNull(reader.GetOrdinal("JM_CARD")) ? 0 : reader.GetDecimal(8);
                        item.JM_UANG = reader.IsDBNull(reader.GetOrdinal("JM_UANG")) ? 0 : reader.GetDecimal(9);
                        item.KEMBALI = reader.IsDBNull(reader.GetOrdinal("KEMBALI")) ? 0 : reader.GetDecimal(10);
                        item.CREATED_DATE = reader.GetDateTime(11);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(12);
                        item.TGL_TRANS = reader.GetDateTime(13);
                        item.KODE = reader.GetString(14);
                        item.SHOWROOM = reader.GetString(15);
                        item.LOGO_IMG = reader.GetString(16);
                        item.ALAMAT = reader.GetString(17);
                        item.PHONE = reader.GetString(18);
                        item.QTY = reader.GetInt32(19);
                        item.BRAND = reader.GetString(20);
                        item.VOUCHER = reader.GetString(21);
                        item.CARD = reader.GetString(22);
                        item.DPP = reader.GetDecimal(23);
                        item.PPN = reader.GetDecimal(24);
                        item.ONGKIR = reader.GetString(25);
                        item.JM_ONGKIR = reader.GetDecimal(26);
                        item.JM_FREE_ONGKIR = reader.GetDecimal(27);
                        ListVwRprint.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListVwRprint;
        }

    }
}