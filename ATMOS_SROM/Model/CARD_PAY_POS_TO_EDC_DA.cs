using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATMOS_SROM.Domain;

namespace ATMOS_SROM.Model
{
    public class CARD_PAY_POS_TO_EDC_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public string insertCARD_PAY_POS_TO_EDC(CARD_PAY_POS_TO_EDC tempEDC)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Insert CARD_PAY_POS_TO_EDC (CardPay, Bank, EDC, KODE_CUST, KODE_CT, CRT_DT, CRT_BY, STAT_TRANS, ID_SH_BAYAR) values " +
                    " (@CardPay, @Bank, @EDC, @KODE_CUST, @KODE_CT, GETDATE(),@user, 'NEW', @idSHBayar) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@CardPay", SqlDbType.Decimal).Value = tempEDC.CardPay;
                    command.Parameters.Add("@Bank", SqlDbType.VarChar).Value = tempEDC.Bank;
                    command.Parameters.Add("@EDC", SqlDbType.VarChar).Value = tempEDC.EDC;
                    command.Parameters.Add("@KODE_CUST", SqlDbType.VarChar).Value = tempEDC.KODE_CUST;
                    command.Parameters.Add("@KODE_CT", SqlDbType.VarChar).Value = tempEDC.KODE_CT;
                    command.Parameters.Add("@user", SqlDbType.VarChar).Value = tempEDC.CRT_BY;
                    command.Parameters.Add("@idSHBayar", SqlDbType.BigInt).Value = tempEDC.ID_SH_BAYAR;

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

        public CARD_PAY_POS_TO_EDC getEDCResult(string where)
        {
            CARD_PAY_POS_TO_EDC item = new CARD_PAY_POS_TO_EDC();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("SELECT top 1 ID, ID_SH_BAYAR, CardPay, Bank, EDC, KODE_CUST, KODE_CT, CRT_DT, CRT_BY, STAT_TRANS, UPD_DT, UPD_BY, Response, Response_2, STX, msglenght, version, TRANSTYPE, TRANSAMT, " +
                    " otheramt, PAN, expirydt, respCode, RRN, ApvCode, dte, tme, merchantID, terminalID, offlineFlag, cardholderName, PANCshCard, Invoiceno, batchNo, IssuerID, InstFlag, DCCFlag, redeemFlag, infoamt,"+
                    " dccdecimalplace, dcccurrencyname, dccexchangerate, couponflag, filler, etx, lrc FROM CARD_PAY_POS_TO_EDC {0} order by ID DESC", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        item.ID = reader.GetInt64(0);
                        item.ID_SH_BAYAR = reader.GetInt64(1);
                        item.CardPay = reader.GetDecimal(2);
                        item.Bank = reader.GetString(3);
                        item.EDC = reader.GetString(4);
                        item.KODE_CUST = reader.GetString(5);
                        item.KODE_CT = reader.GetString(6);
                        item.CRT_DT = reader.GetDateTime(7);
                        item.CRT_BY = reader.GetString(8);
                        item.STAT_TRANS = reader.GetString(9);
                        item.UPD_DT = reader.IsDBNull(reader.GetOrdinal("UPD_DT")) ? Convert.ToDateTime("01-01-0001") : reader.GetDateTime(10);
                        item.UPD_BY = reader.IsDBNull(reader.GetOrdinal("UPD_BY")) ? "" : reader.GetString(11);
                        item.Response = reader.IsDBNull(reader.GetOrdinal("Response")) ? "" : reader.GetString(12);
                        item.Response_2 = reader.IsDBNull(reader.GetOrdinal("Response_2")) ? "" : reader.GetString(13);
                        item.STX = reader.IsDBNull(reader.GetOrdinal("STX")) ? "" : reader.GetString(14);
                        item.msglenght = reader.IsDBNull(reader.GetOrdinal("msglenght")) ? "" : reader.GetString(15);
                        item.version = reader.IsDBNull(reader.GetOrdinal("version")) ? "" : reader.GetString(16);
                        item.TRANSTYPE = reader.IsDBNull(reader.GetOrdinal("TRANSTYPE")) ? "" : reader.GetString(17);
                        item.TRANSAMT = reader.IsDBNull(reader.GetOrdinal("TRANSAMT")) ? "" : reader.GetString(18);
                        item.otheramt = reader.IsDBNull(reader.GetOrdinal("otheramt")) ? "" : reader.GetString(19);
                        item.PAN = reader.IsDBNull(reader.GetOrdinal("PAN")) ? "" : reader.GetString(20);
                        item.expirydt = reader.IsDBNull(reader.GetOrdinal("expirydt")) ? "" : reader.GetString(21);
                        item.respCode = reader.IsDBNull(reader.GetOrdinal("respCode")) ? "" : reader.GetString(22);
                        item.RRN = reader.IsDBNull(reader.GetOrdinal("RRN")) ? "" : reader.GetString(23);
                        item.ApvCode = reader.IsDBNull(reader.GetOrdinal("ApvCode")) ? "" : reader.GetString(24);
                        item.dte = reader.IsDBNull(reader.GetOrdinal("dte")) ? "" : reader.GetString(25);
                        item.tme = reader.IsDBNull(reader.GetOrdinal("tme")) ? "" : reader.GetString(26);
                        item.merchantID = reader.IsDBNull(reader.GetOrdinal("merchantID")) ? "" : reader.GetString(27);
                        item.terminalID = reader.IsDBNull(reader.GetOrdinal("terminalID")) ? "" : reader.GetString(28);
                        item.offlineFlag = reader.IsDBNull(reader.GetOrdinal("offlineFlag")) ? "" : reader.GetString(29);
                        item.cardholderName = reader.IsDBNull(reader.GetOrdinal("cardholderName")) ? "" : reader.GetString(30);
                        item.PANCshCard = reader.IsDBNull(reader.GetOrdinal("PANCshCard")) ? "" : reader.GetString(31);
                        item.Invoiceno = reader.IsDBNull(reader.GetOrdinal("Invoiceno")) ? "" : reader.GetString(32);
                        item.batchNo = reader.IsDBNull(reader.GetOrdinal("batchNo")) ? "" : reader.GetString(33);
                        item.IssuerID = reader.IsDBNull(reader.GetOrdinal("IssuerID")) ? "" : reader.GetString(34);
                        item.InstFlag = reader.IsDBNull(reader.GetOrdinal("InstFlag")) ? "" : reader.GetString(35);
                        item.DCCFlag = reader.IsDBNull(reader.GetOrdinal("DCCFlag")) ? "" : reader.GetString(36);
                        item.redeemFlag = reader.IsDBNull(reader.GetOrdinal("redeemFlag")) ? "" : reader.GetString(37);
                        item.infoamt = reader.IsDBNull(reader.GetOrdinal("infoamt")) ? "" : reader.GetString(38);
                        item.dccdecimalplace = reader.IsDBNull(reader.GetOrdinal("dccdecimalplace")) ? "" : reader.GetString(39);
                        item.dcccurrencyname = reader.IsDBNull(reader.GetOrdinal("dcccurrencyname")) ? "" : reader.GetString(40);
                        item.dccexchangerate = reader.IsDBNull(reader.GetOrdinal("dccexchangerate")) ? "" : reader.GetString(41);
                        item.couponflag = reader.IsDBNull(reader.GetOrdinal("couponflag")) ? "" : reader.GetString(42);
                        item.filler = reader.IsDBNull(reader.GetOrdinal("filler")) ? "" : reader.GetString(43);
                        item.etx = reader.IsDBNull(reader.GetOrdinal("etx")) ? "" : reader.GetString(44);
                        item.lrc = reader.IsDBNull(reader.GetOrdinal("lrc")) ? "" : reader.GetString(45);

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public string deleteCARD_PAY_POS_TO_EDC(Int64 idBayar)
        {
            SqlConnection Connection = new SqlConnection(conString);
            string ret = "Berhasil!";
            try
            {
                string query = String.Format("UPDATE CARD_PAY_POS_TO_EDC set STAT_TRANS = 'FAILED'  where STAT_TRANS = 'NEW' AND ID_SH_BAYAR = @id ");
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

    }
}