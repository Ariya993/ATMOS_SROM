using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.TestDummy
{
    public partial class DummyRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
               
                //string start = tbStartDate.Text.ToString();
                //DateTime startDate = SqlDateTime.MinValue.Value;

                //if (!string.IsNullOrEmpty(start))
                //{
                //    DateTime.TryParseExact(start, "dd-MM-yyyy",
                //    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                //}


                //ReportViewer.LocalReport.ReportPath = string.Format(@"TestDummy\{0}", "Report2.rdlc");
                //ReportViewer.Visible = true;

                ////REPORT_DA rptDA = new REPORT_DA();
                ////List<USP_SRPTADJUSMENTMANUAL> total = rptDA.getSrptAdjustmentManual(startDate, endDate, "Fred Perry");

                

                //ReportDataSource dataSrcReport = new ReportDataSource();
                ////dataSrcReport = new ReportDataSource("Report2.rdlc", total);
                //dataSrcReport.Name = "DataSet1";

                //ReportViewer.LocalReport.DataSources.Clear();
                //ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

                //ReportViewer.LocalReport.Refresh();
                //ReportViewer.Visible = true;
                //divReport.Visible = true;

            //    DivMessage.Visible = false;

            }
            catch (Exception ex)
            {
                //DivMessage.InnerText = "Show Report Failed : " + ex.Message;
                //DivMessage.Attributes["class"] = "error";
                //DivMessage.Visible = true;
            }
        }

    }
}