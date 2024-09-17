using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace _707SROM
{
    public partial class TestPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
  
        }

        protected void print()
        {
            //String pkInstalledPrinters;
            //for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            //{
            //    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
            //}

            //MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            //MS_SHOWROOM show = new MS_SHOWROOM();

            //string sUser = Session["UName"].ToString();
            //string sKode = Session["UKode"].ToString();
            //show = showDA.getShowRoom(String.Format(" where KODE = '{0}'", sKode)).First();

            //PRINT_DA printDA = new PRINT_DA("000", show, sUser);

            //string szPrinterName = @"NPID95464\HP LaserJet 400 M401 PCL 6";

            //StreamReader sr = new StreamReader(@"F:\a.txt");
            //string line = (char)27 + "*c32545D";
            //line += sr.ReadToEnd();
            //line += (char)27 + "*c5F";

            //PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);
            //PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");

            //szPrinterName = @"\\NPID95464\HP LaserJet 400 M401 PCL 6";
            //PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);
            //PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");

            //szPrinterName = @"\\192.168.2.171";
            //PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);


            //szPrinterName = @"HP LaserJet 400 M401 PCL 6";
            //PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");

            string alamatIP = tbIP.Text.Trim();
            string bon = Server.MapPath("Bon\\15300900038.pdf");

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.NoDelay = true;

            IPAddress ip = IPAddress.Parse(alamatIP);
            IPEndPoint ipep = new IPEndPoint(ip,9100);
            clientSocket.Connect(ipep);

            byte[] fileBytes = File.ReadAllBytes(bon);

            clientSocket.SendFile(bon);
            //clientSocket.Send(fileBytes);
            clientSocket.Close();
        }

        protected void btnPrintClick(object sender, EventArgs e)
        {
            print();
        }
    }
}