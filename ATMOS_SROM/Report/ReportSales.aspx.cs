using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Globalization;
using ATMOS_SROM.Domain;
using Microsoft.Reporting.WebForms;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Report
{
    public partial class ReportSales : System.Web.UI.Page
    {
        string sJam = ("000" + DateTime.Now.Hour.ToString()).Substring(("000" + DateTime.Now.Hour.ToString()).Length - 2);
        string sMenit = ("000" + DateTime.Now.Minute.ToString()).Substring(("000" + DateTime.Now.Minute.ToString()).Length - 2);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerateClick(object sender, EventArgs e)
        {
            try
            {
                string start = tbStartDate.Text.ToString();
                string end = tbEndDate.Text.ToString();
                DateTime startDate = SqlDateTime.MinValue.Value;
                DateTime endDate = SqlDateTime.MaxValue.Value;
                DateTime endLog = SqlDateTime.MaxValue.Value;
                if (!string.IsNullOrEmpty(start))
                {
                    DateTime.TryParseExact(start, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                }

                if (!string.IsNullOrEmpty(end))
                {
                    DateTime.TryParseExact(end, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    endLog = endDate;
                    endDate = endDate.AddDays(1);
                }

                //ReportViewer.LocalReport.ReportPath = string.Format(@"Report\{0}", "rptPenjualan.rdlc");
                ReportViewer.LocalReport.ReportPath = string.Format(@"Report\{0}", "rptTotalMargin.rdlc");
                ReportViewer.Visible = true;

                REPORT_DA rptDA = new REPORT_DA();
                //List<USP_RPTPENJUALAN> total = rptDA.getRptPenjualan(startDate, endDate);
                List<USP_TOTALMARGIN> total = rptDA.getRptTotalMargin(startDate, endDate);
                //List<totalCard> total = new ReportController().report(startDate, endDate, id,
                //    region, area, reportType);

                ReportDataSource dataSrcReport = new ReportDataSource();
                //dataSrcReport = new ReportDataSource("rptPenjualan.rdlc", total);
                dataSrcReport = new ReportDataSource("rptTotalMargin.rdlc", total);
                //dataSrcReport.Name = "DataSet1";
                dataSrcReport.Name = "dsReport2";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

                //ReportParameter param = new ReportParameter();
                //param = new ReportParameter("paramJudul", "Reporting");
                //ReportViewer.LocalReport.SetParameters(param);

                ReportViewer.LocalReport.Refresh();
                ReportViewer.Visible = true;
                
                divReport.Visible = true;


                Session["log"] = sJam + sMenit;
                DivMessage.Visible = false;

            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Show Report Failed : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }
    }
}