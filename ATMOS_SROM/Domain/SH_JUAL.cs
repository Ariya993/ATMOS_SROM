using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class SH_JUAL
    {
        public Int64 ID { get; set; }//ID dari setiap item
        public Int64 ID_BAYAR { get; set; }//ID dari Table SH_Bayar
        public Int64 ID_KDBRG { get; set; }//ID dari Table MS_KDBRG
        public string KODE_CUST { get; set; }//Toko SA jualan
        public string KODE { get; set; }//--Unused
        public DateTime TGL_TRANS { get; set; }//Tanggal Terjadinya Transaksi
        public string NO_BON { get; set; }//No Bon di table SH_Bayar
        public string JUAL_RETUR { get; set; }//Bila di Isi = Retur(Pengembalian)
        public string ITEM_CODE { get; set; }//Code Item yang di jual
        public int QTY { get; set; }//Berapa banyak Banyak barang yang di jual per item code per bon
        public decimal TAG_PRICE { get; set; }//Harga di Tag
        public decimal BON_PRICE { get; set; }//Harga di Bon
        public decimal FTRF { get; set; }//??
        public string JNS_DISC { get; set; }//Tipe Discount
        public decimal DISC_P { get; set; }//Discount Percent
        public decimal DISC_R { get; set; }//Discount Rupiah
        public decimal GVOUCHER { get; set; }//??
        public decimal DISCPAKH { get; set; }//??
        public decimal NILAI_BYR { get; set; }//??Nilai Yang di bayarkan 
        public decimal DLM_TRIMA { get; set; }//??
        public decimal DPP { get; set; }//Nilai sebelum di PPN
        public decimal PPN { get; set; }//Pajak 10%
        public string ALASAN { get; set; }//alasan pengembalian barang
        public string NO_REFF { get; set; }//??
        public string FORMULA { get; set; }//??
        public string BATAL { get; set; }//??
        public int FEDT { get; set; }//??
        public string FJAM { get; set; }//??
        public string FID { get; set; }//??
        public string FOP { get; set; }//??
        public decimal DISC_P2 { get; set; }//??
        public decimal DISC_R2 { get; set; }//??
        public string FBS { get; set; }//??
        public string FPROMO { get; set; }//??
        public string NO_UNIK { get; set; }//??
        public string FADD { get; set; }//??
        public string FEDT1 { get; set; }//??
        public string FDEL { get; set; }//??
        public string FNOACARA { get; set; }//??
        public string FNOUNIK { get; set; }//??
        public string BARCODE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }

        //Addition
        //k.FBRAND, k.FART_DESC, k.FCOL_DESC, k.FSIZE_DESC
        public string FBRAND { get; set; }
        public string FART_DESC { get; set; }
        public string FCOL_DESC { get; set; }
        public string FSIZE_DESC { get; set; }
    }
}