using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Domain.Report;
using ATMOS_SROM.Model;
using Microsoft.Reporting.WebForms;

namespace ATMOS_SROM.Report
{
    public partial class RptLapHarianThn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindStore();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string start = tbStartDate.Text.ToString();
                string end = tbEndDate.Text.ToString();
                string KdCust = "";
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
                    endDate = endDate.AddDays(0);
                }

                KdCust = ddlStore.SelectedValue;
                ReportViewer.LocalReport.ReportPath = string.Format(@"Report\{0}", "RptLapHarianTHn.rdlc");
                ReportViewer.Visible = true;
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.Landscape = true;
                ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1169, 1653);
                ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                ReportViewer.SetPageSettings(ps);
                REPORT_DA rptDA = new REPORT_DA();
                List<tf_lapHarianPerthn> total = rptDA.GetTfLapHarianPerThn(startDate, endDate, KdCust);

                ReportDataSource dataSrcReport = new ReportDataSource();
                dataSrcReport = new ReportDataSource("RptLapHarianTHn.rdlc", total);
                dataSrcReport.Name = "DT_TFReportSalesThn";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

                ReportViewer.LocalReport.Refresh();
                ReportViewer.Visible = true;
                divReport.Visible = true;

                //Session["log"] = sJam + sMenit;
                DivMessage.Visible = false;

            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Show Report Failed : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }

        }
        protected void bindStore()
        {
            ddlStore.Enabled = true;

            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            MS_SHOWROOM showRoom = new MS_SHOWROOM();

            showRoom.SHOWROOM = "--Pilih Showroom--";
            showRoom.KODE = "";
            listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM != 'SUP'");
            listStore.Insert(0, showRoom);
            ddlStore.DataSource = listStore;
            ddlStore.DataBind();
        }

    }
}