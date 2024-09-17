using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain.Report;
using ATMOS_SROM.Model;
using Microsoft.Reporting.WebForms;

namespace ATMOS_SROM.TestDummy
{
    public partial class TestRptDeisBI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                string start = tbStartDate.Text.ToString();
                DateTime startDate = SqlDateTime.MinValue.Value;

                if (!string.IsNullOrEmpty(start))
                {
                    DateTime.TryParseExact(start, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                }

                ReportViewer.LocalReport.ReportPath = string.Format(@"Report\{0}", "Rpt_DeisBI.rdlc");
                ReportViewer.Visible = true;
                
                REPORT_DA rptDA = new REPORT_DA();
                List<TF_DeisBIF> total = rptDA.GetTF_DeisBIF(startDate);

                ReportDataSource dataSrcReport = new ReportDataSource();
                dataSrcReport = new ReportDataSource("Rpt_DeisBI.rdlc", total);
                dataSrcReport.Name = "DataSet1";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);

                ReportViewer.LocalReport.Refresh();
                ReportViewer.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
}