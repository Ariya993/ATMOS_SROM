using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Domain.Laporan;
using ATMOS_SROM.Model;
using Microsoft.Reporting.WebForms;

namespace ATMOS_SROM.Laporan
{
    public partial class LapStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tbBulanStock.Text = string.Format("{0:yyMM}", DateTime.Now);
                bindStore();
            }
        }

        protected void bindgrid(string kode)
        {
            GLOBALCODE gc = new GLOBALCODE();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();

            //Check sudah dimasukin ke table SLD_AWAL
            string countSld = gc.countData("SLD_AWAL", string.Format("where KODE = '{0}' and FBULAN = '{1}'", sKode, tbBulanStock.Text));
            if (int.Parse(countSld) == 0)
            {
                string year = "20" + tbBulanStock.Text.Remove(2);
                string month = tbBulanStock.Text.Remove(0, 2);
                //1601
                //Yang di butuhin 01-01-2016, 02-01-2016, 01-31-2016
                //format dd-MM-yyyy
                string bulanAwal = "01-" + month + "-" + year;
                int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));

                DateTime startDate = DateTime.Now;
                if (!string.IsNullOrEmpty(bulanAwal))
                {
                    DateTime.TryParseExact(bulanAwal, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                }

                DateTime endDate = startDate.AddDays(daysInMonth - 1);
                DateTime cutOff = startDate.AddDays(daysInMonth);

                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                List<MS_KARTU_STOCK_HEADER> listKartuStock = stockDA.getKartuStockHeader(startDate, cutOff, endDate, " where KODE = '" + kode + "'");
                if (listKartuStock.Count > 0)
                {
                    MS_KARTU_STOCK_HEADER kartuStock = listKartuStock.First();

                    tbKode.Text = kode;
                    tbShowroom.Text = ddlShowroom.SelectedItem.Text;

                    tbAwal.Text = kartuStock.SALDO_AWAL.ToString();
                    tbJual.Text = kartuStock.SALE.ToString();
                    tbBeli.Text = kartuStock.BELI.ToString();
                    tbTerima.Text = kartuStock.TERIMA.ToString();
                    tbKirim.Text = kartuStock.KIRIM.ToString();
                    tbAdjustment.Text = kartuStock.ADJUSTMENT.ToString();
                    tbAkhir.Text = kartuStock.SALDO_AKHIR.ToString();
                    divStock.Visible = true;
                }
                else
                {

                }
            }
            else
            {
                string where = string.Format("where KODE = '{0}' and FBULAN = '{1}'", kode, tbBulanStock.Text);
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                List<MS_KARTU_STOCK_HEADER> listKartuStock = stockDA.getKartuStockHeaderSldAwal(where);
                if (listKartuStock.Count > 0)
                {
                    MS_KARTU_STOCK_HEADER kartuStock = listKartuStock.First();

                    tbKode.Text = kode;
                    tbShowroom.Text = ddlShowroom.SelectedItem.Text;

                    tbAwal.Text = kartuStock.SALDO_AWAL.ToString();
                    tbJual.Text = kartuStock.SALE.ToString();
                    tbBeli.Text = kartuStock.BELI.ToString();
                    tbTerima.Text = kartuStock.TERIMA.ToString();
                    tbKirim.Text = kartuStock.KIRIM.ToString();
                    tbAdjustment.Text = kartuStock.ADJUSTMENT.ToString();
                    tbAkhir.Text = kartuStock.SALDO_AKHIR.ToString();
                    divStock.Visible = true;
                }
                else
                {

                }
            }
        }

        protected void bindStore()
        {
            ddlShowroom.Enabled = true;
            string store = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();

            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();

            if (sLevel.ToLower() == "store manager" || sLevel.ToLower() == "sales")
            {
                ddlShowroom.Enabled = false;
                MS_SHOWROOM show = new MS_SHOWROOM();
                show.KODE = sKode;
                show.SHOWROOM = store;
                listStore.Add(show);
            }
            else
            {
                ddlShowroom.Enabled = true;
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                MS_SHOWROOM showRoom = new MS_SHOWROOM();

                showRoom.SHOWROOM = "--Pilih Showroom--";
                showRoom.KODE = "";
                listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM != 'SUP'");
                listStore.Insert(0, showRoom);
            }

            ddlShowroom.DataSource = listStore;
            ddlShowroom.DataBind();
        }

        protected void btnSearchStockClick(object sender, EventArgs e)
        {
            if (tbBulanStock.Text.Trim() != "" && (ddlShowroom.Enabled == false || ddlShowroom.SelectedIndex > 0))
            {
                bindgrid(ddlShowroom.SelectedValue);
                divStock.Visible = true;
                DivMessage.Visible = false;
            }
            else
            {
                divStock.Visible = false;

                DivMessage.InnerText = "Pilih Bulan dan Showroom!";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnDetailClick(object sender, EventArgs e)
        {
            try
            {
                //string start = tbStartDate.Text.ToString();
                //string end = tbEndDate.Text.ToString();
                //DateTime startDate = SqlDateTime.MinValue.Value;
                //DateTime endDate = SqlDateTime.MaxValue.Value;
                //DateTime endLog = SqlDateTime.MaxValue.Value;
                //if (!string.IsNullOrEmpty(start))
                //{
                //    DateTime.TryParseExact(start, "dd-MM-yyyy",
                //    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                //}

                //if (!string.IsNullOrEmpty(end))
                //{
                //    DateTime.TryParseExact(end, "dd-MM-yyyy",
                //    CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                //    endLog = endDate;
                //    endDate = endDate.AddDays(1);
                //}

                string where = string.Format(" where KODE = '{0}' and FBULAN = '{1}'", tbKode.Text, tbBulanStock.Text);

                ReportViewer.LocalReport.ReportPath = string.Format(@"Laporan\{0}", "LapStock.rdlc");
                ReportViewer.Visible = true;

                MS_STOCK_DA stcDA = new MS_STOCK_DA();
                List<LAP_SLD_STOCK> total = stcDA.getKartuStockDetail(where);

                ReportParameter rp = new ReportParameter("Kode", tbKode.Text);
                ReportParameter rp2 = new ReportParameter("Showroom", tbShowroom.Text);

                ReportDataSource dataSrcReport = new ReportDataSource();
                dataSrcReport = new ReportDataSource("LapStock.rdlc", total);
                dataSrcReport.Name = "DataSet1";

                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(dataSrcReport);
                ReportViewer.LocalReport.SetParameters(new ReportParameter[] { rp });
                ReportViewer.LocalReport.SetParameters(new ReportParameter[] { rp2 });
                ReportViewer.LocalReport.Refresh();
                ReportViewer.Visible = true;
                divDetail.Visible = true;

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