using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class CARD_PAY_POS_TO_EDC
    {
        public Int64 ID { get; set; }
        public Int64 ID_SH_BAYAR { get; set; }
        public decimal CardPay { get; set; }
        public string Bank { get; set; }
        public string EDC { get; set; }
        public string KODE_CUST { get; set; }
        public string KODE_CT { get; set; }
        public DateTime? CRT_DT { get; set; }
        public string CRT_BY { get; set; }
        public string STAT_TRANS { get; set; }
        public DateTime? UPD_DT { get; set; }
        public string UPD_BY { get; set; }
        public string Response { get; set; }
        public string Response_2 { get; set; }
        public string STX { get; set; }
        public string msglenght { get; set; }
        public string version { get; set; }
        public string TRANSTYPE { get; set; }
        public string TRANSAMT { get; set; }
        public string otheramt { get; set; }
        public string PAN { get; set; }
        public string expirydt { get; set; }
        public string respCode { get; set; }
        public string RRN { get; set; }
        public string ApvCode { get; set; }
        public string dte { get; set; }
        public string tme { get; set; }
        public string merchantID { get; set; }
        public string terminalID { get; set; }
        public string offlineFlag { get; set; }
        public string cardholderName { get; set; }
        public string PANCshCard { get; set; }
        public string Invoiceno { get; set; }
        public string batchNo { get; set; }
        public string IssuerID { get; set; }
        public string InstFlag { get; set; }
        public string DCCFlag { get; set; }
        public string redeemFlag { get; set; }
        public string infoamt { get; set; }
        public string dccdecimalplace { get; set; }
        public string dcccurrencyname { get; set; }
        public string dccexchangerate { get; set; }
        public string couponflag { get; set; }
        public string filler { get; set; }
        public string etx { get; set; }
        public string lrc { get; set; }
   }
}