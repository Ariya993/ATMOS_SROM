using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;
using Microsoft.Reporting.WebForms;
using System.Data.SqlTypes;
using System.Globalization;

namespace ATMOS_SROM.Report
{
    public partial class RptStock : System.Web.UI.Page
    {
        string sJam = ("000" + DateTime.Now.Hour.ToString()).Substring(("000" + DateTime.Now.Hour.ToString()).Length - 2);
        string sMenit = ("000" + DateTime.Now.Minute.ToString()).Substring(("000" + DateTime.Now.Minute.ToString()).Length - 2);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindReport();
            }
        }

        protected void bindReport()
        {
            try
            {
                ReportViewer.LocalReport.ReportPath = string.Format(@"Report\{0}", "rptStock.rdlc");
                ReportViewer.Visible = true;

                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                string where = " where KODE in ( select KODE from MS_SHOWROOM where STORE = 'Fred Perry')";
                List<MS_STOCK> stockList = stockDA.getStock(where);

                ReportDataSource dataSrcReport = new ReportDataSource();
                dataSrcReport = new ReportDataSource("rptStock.rdlc", stockList);
                dataSrcReport.Name = "dsStock";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

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