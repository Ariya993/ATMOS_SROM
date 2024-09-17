using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain.Report;
using ATMOS_SROM.Model;
using Microsoft.Reporting.WebForms;

namespace ATMOS_SROM.TestDummy
{
    public partial class ReportPrintDummy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
               
                ReportViewer.LocalReport.ReportPath = string.Format(@"Reports\{0}", "PackingList_GoodReceive.rdlc");
                ReportViewer.Visible = true;
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                //ps.Landscape = true;
                //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1169, 1653);
                //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                //ReportViewer.SetPageSettings(ps);
                REPORT_DA rptDA = new REPORT_DA();
                //List<TF_RptPackingListTrfStock> total = rptDA.GetTF_RptPackingListTrfStock(txtNoPO.Text);
                DataSet dsSrcRpt = rptDA.GetDataTF_ReportPackingListGR(txtNoPO.Text);
                ReportDataSource dataSrcReport = new ReportDataSource();
                dataSrcReport = new ReportDataSource("PackingList_GoodReceive.rdlc", dsSrcRpt);
                dataSrcReport.Name = "DataSet1";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

                ReportViewer.LocalReport.Refresh();
                ReportViewer.Visible = true;
                //divReport.Visible = true;

                ////Session["log"] = sJam + sMenit;
                //DivMessage.Visible = false;
            }
            catch (Exception ex)
            { }
        }

        protected void btnDirectPrint_Click(object sender, EventArgs e)
        {
            LoadRDLCData();
            exportRpt();
            //PrintDirect();
        }
        protected void LoadRDLCData()
        {
            REPORT_DA rptDA = new REPORT_DA();
            DataSet dsRptData = new DataSet();
            ReportViewer.ProcessingMode = ProcessingMode.Local;
            ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/PackingList_GoodReceive.rdlc");
            dsRptData = rptDA.GetDataTF_ReportPackingListGR(txtNoPO.Text);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dsRptData.Tables[0]);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(datasource);
        }
        protected void exportRpt()
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension = "PDF";

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void PrintDirect()
        {
            REPORT_DA rptDA = new REPORT_DA();
            LocalReport report = new LocalReport();
            report.ReportPath = "DeliveryNotes_TrfStock.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file
            rds.Value = rptDA.GetDataTf_ReportPusrchaseOrder(txtNoPO.Text); //EmployeeRepository.GetAllEmployees();
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("WORD");
            //Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            using (FileStream fs = File.Create(@"D:\SalSlip.doc"))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }
        }
    }
}