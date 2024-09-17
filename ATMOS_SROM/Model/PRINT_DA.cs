using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Printing;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ATMOS_SROM.Domain;
using System.Security.Principal;

namespace ATMOS_SROM.Model
{
    public class PRINT_DA
    {
        PrintDocument pdoc = null;
        Document thisPdf = null;
        int ticketNo;
        string noBON;
        string user;
        DateTime TicketDate;
        String Source, Destination,DrawnBy;
        float Amount;

        MS_SHOWROOM show = new MS_SHOWROOM();

        public int TicketNo
        {
            //set the person name
            set { this.ticketNo = value; }
            //get the person name 
            get { return this.ticketNo; }
        }

        public DateTime ticketDate
        {
            //set the person name
            set { this.TicketDate = value; }
            //get the person name 
            get { return this.TicketDate; }
        }

        public String source
        {
            //set the person name
            set { this.Source = value; }
            //get the person name 
            get { return this.Source; }
        }

        public String destination
        {
            //set the person name
            set { this.Destination = value; }
            //get the person name 
            get { return this.Destination; }
        }

        public float amount
        {
            //set the person name
            set { this.Amount = value; }
            //get the person name 
            get { return this.Amount; }
        }

        public String drawnBy
        {
            //set the person name
            set { this.DrawnBy = value; }
            //get the person name 
            get { return this.DrawnBy; }
        }

        public PRINT_DA()
        {

        }

        public PRINT_DA(int ticketNo, DateTime TicketDate, String Source, String Destination, float Amount, String DrawnBy)
        {
            this.ticketNo = ticketNo;
            this.TicketDate = TicketDate;
            this.Source = Source;
            this.Destination = Destination;
            this.Amount = Amount;
            this.DrawnBy = DrawnBy;
        }

        public PRINT_DA(string noBon, MS_SHOWROOM show, string user)
        {
            this.noBON = noBon;
            this.show = show;
            this.user = user;
            print();
        }

        public string print()
        {
            //PrintDialog pd = new PrintDialog();
            using (WindowsImpersonationContext wic = WindowsIdentity.Impersonate(IntPtr.Zero))
            {
                Document pdf = new Document();
                pdoc = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                System.Drawing.Font font = new System.Drawing.Font("Courier New", 15);
                pdoc.PrinterSettings.PrinterName = @"HP_LASERJET";

                PaperSize psize = new PaperSize("Custom", 100, 200);
                //ps.DefaultPageSettings.PaperSize = psize;

                //pd.Document = pdoc;
                //pd.Document.DefaultPageSettings.PaperSize = psize;
                //pdoc.DefaultPageSettings.PaperSize.Height = 10f;
                //pdoc.DefaultPageSettings.PaperSize.Height = 820;

                //pdoc.DefaultPageSettings.PaperSize.Width = 520;
                pdoc.DefaultPageSettings.PaperSize = psize;
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

                //DialogResult result = pd.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //PrintPreviewDialog pp = new PrintPreviewDialog();
                //pp.Document = pdoc;
                //result = pp.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                ////pdoc.Print();
                //}
                //}
            }
            return "Print Berhasil";
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR bayar = new SH_BAYAR();
            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + user + "'");
            decimal totalPrice = 0;

            Graphics graphics = e.Graphics;
            System.Drawing.Font font = new System.Drawing.Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to 909Group Store", new System.Drawing.Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(show.SHOWROOM, new System.Drawing.Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(show.ALAMAT + " - " + show.PHONE, new System.Drawing.Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("BON No:" + this.noBON, new System.Drawing.Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket Date :" + DateTime.Now.ToString(), new System.Drawing.Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Source= this.source; 
            graphics.DrawString("Article", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Price \t Discount\tNet Discount \t Net Price", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString(underLine, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            foreach (TEMP_STRUCK item in struckList)
            {
                Offset = Offset + 20;
                graphics.DrawString(item.ART_DESC + item.WARNA + item.SIZE, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(string.Format("{0:0,0.00}", Convert.ToDouble(item.PRICE)) + "\t" + item.DISCOUNT.ToString() + "\t" + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_DISCOUNT)) + "\t" + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE))
                    , new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                totalPrice = totalPrice + item.NET_PRICE;
            }

            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Grosstotal = "Total Amount to Pay = Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalPrice));
            graphics.DrawString(Grosstotal , new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            
            
            if (bayar.VOUCHER == "Yes")
            {
                Offset = Offset + 20;
                graphics.DrawString("Voucher \t \t : Rp.", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            }

            if (bayar.CARD == "Yes")
            {
                Offset = Offset + 20;
                graphics.DrawString("Card \t \t : Rp.", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            }

            Offset = Offset + 20;
            graphics.DrawString("Cash \t \t : Rp." + bayar.JM_UANG.ToString(), new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Kembali \t \t : Rp." + bayar.KEMBALI.ToString(), new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thank you very much. Please come again !", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("1. Penukaran Barang Normal max 2 hari.", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("   Syarat & ketentuan berlaku.", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("2. Barang sale & Special Price", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("   TIDAK BISA DITUKAR ATAU DIKEMBALIKAN", new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            
            //String DrawnBy = this.drawnBy;
            //graphics.DrawString("Conductor - "+DrawnBy, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
        }

        void pdoc_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            System.Drawing.Font font = new System.Drawing.Font("Courier New", 10);

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            System.Drawing.Font reg = new System.Drawing.Font(BaseFont.TIMES_ROMAN, 14, System.Drawing.FontStyle.Bold);
            System.Drawing.Font reg2 = new System.Drawing.Font(BaseFont.TIMES_ROMAN, 12, System.Drawing.FontStyle.Bold);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to 707 STORE", reg, new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("No BON:" + this.TicketNo, reg, new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ticket Date :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), reg, new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, reg, new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String Source = this.source;
            graphics.DrawString("From " + Source + " To " + Destination, reg2, new SolidBrush(Color.Black), startX, startY + Offset);
            
            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            
            Offset = Offset + 20;
            String Grosstotal = "Total Amount to Pay = " + this.amount;

            graphics.DrawString(Grosstotal, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String DrawnBy = this.drawnBy;
            graphics.DrawString("Conductor - " + DrawnBy, new System.Drawing.Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
        }
    }
}