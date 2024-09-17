using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Globalization;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;

namespace ATMOS_SROM.Laporan
{
    public partial class LapSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindStore();
                bindHarian();
                bindStatistic();
            }
        }

        protected void bindgrid()
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
                    //endDate = endDate.AddDays(1);
                }

                string kodeToko = Session["UKode"] == null ? "" : Session["UKode"].ToString();
                kodeToko = ddlShowroom.SelectedValue;
                string and = string.Format(" and KODE like '{0}' and ISNULL(BATAL, 'N') = 'N'", kodeToko);
                List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
                LAPORAN_DA lapDA = new LAPORAN_DA();

                listBayar = lapDA.getLaporanSHBayar(startDate, endDate, and);

                gvMain.DataSource = listBayar;
                gvMain.DataBind();
                panelView.Visible = true;
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
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            string sShow = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> allListShow = new List<MS_SHOWROOM>();
            List<MS_SHOWROOM> listShow = new List<MS_SHOWROOM>();

            allListShow = showDA.getShowRoom("");
            if (sLevel == "Sales" || sLevel == "Store Manager")
            {
                listShow = allListShow.Where(item => item.KODE == sKode).ToList<MS_SHOWROOM>();
                ddlShowroom.Enabled = false;
            }
            else
            {
                if (sLevel == "Admin Sales")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "FSS").ToList<MS_SHOWROOM>();
                }
                else if (sLevel == "Admin Counter" && sKode != "HO-001")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "SIS" && item.KODE == sKode).ToList<MS_SHOWROOM>();
                }
                else if (sLevel == "Admin Counter" && sKode == "HO-001")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "SIS").ToList<MS_SHOWROOM>();
                }
                else
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM != "SUP").ToList<MS_SHOWROOM>();
                    //listShow = allListShow;
                }
                //listShow = sLevel == "Admin Counter" ? allListShow.Where(item => item.STATUS_SHOWROOM == "SIS").ToList<MS_SHOWROOM>() :
                //    sLevel == "Admin Sales" ? allListShow.Where(item => item.STATUS_SHOWROOM == "FSS").ToList<MS_SHOWROOM>() : 
                //    allListShow;

                divPrintScoot.Visible = sLevel == "Admin Counter" && sKode == "HO-001" ? true : false; //sLevel == "Admin Counter" ? true : false;
            }
            ddlShowroom.DataSource = listShow;
            ddlShowroom.DataValueField = "KODE";
            ddlShowroom.DataTextField = "SHOWROOM";
            ddlShowroom.DataBind();
        }

        protected void bindHarian()
        {
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            string sShow = Session["UStore"] == null ? "" : Session["UStore"].ToString();

            tbTglTrans.Text = tbTglTrans.Text == "" || tbTglTrans.Text.ToLower().Contains("nbsp") ? DateTime.Now.ToString("dd-MM-yyyy") : tbTglTrans.Text;
            tbTglEndTrans.Text = tbTglEndTrans.Text == "" || tbTglEndTrans.Text.ToLower().Contains("nbsp") ? DateTime.Now.ToString("dd-MM-yyyy") : tbTglEndTrans.Text;
            tbShowroom.Text = sShow;
            lbKode.Text = sKode;
            sKode = ddlShowroom.SelectedValue;

            DateTime tglTrans = DateTime.Now;
            if (!string.IsNullOrEmpty(tbTglTrans.Text))
            {
                DateTime.TryParseExact(tbTglTrans.Text, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }

            DateTime tglEndTrans = DateTime.Now;
            if (!string.IsNullOrEmpty(tbTglEndTrans.Text))
            {
                DateTime.TryParseExact(tbTglEndTrans.Text, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglEndTrans);
            }

            LAPORAN_DA lapDA = new LAPORAN_DA();
            List<LAPORAN_HARIAN> listHarian = new List<LAPORAN_HARIAN>();
            LAPORAN_HARIAN lapHarian = new LAPORAN_HARIAN();

            listHarian = lapDA.getLaporanHarian(sKode, tglTrans, "", tglEndTrans);
            lapHarian = listHarian.FirstOrDefault();

            tbNilBayar.Text = listHarian.Count == 0 ? "0" : lapHarian.NILAI_BYR.ToString("0,0.00");
            tbDPP.Text = listHarian.Count == 0 ? "0" : lapHarian.DPP.ToString("0,0.00");
            tbPPN.Text = listHarian.Count == 0 ? "0" : lapHarian.PPN.ToString("0,0.00");
            tbCustBayar.Text = listHarian.Count == 0 ? "0" : lapHarian.NILAI_BYR.ToString("0,0.00");
            tbCash.Text = listHarian.Count == 0 ? "0" : lapHarian.NET_CASH.ToString("0,0.00");
            tbCard.Text = listHarian.Count == 0 ? "0" : lapHarian.JM_CARD.ToString("0,0.00");
            tbVoucher.Text = listHarian.Count == 0 ? "0" : lapHarian.JM_VOUCHER.ToString("0,0.00");
            tbDebit.Text = listHarian.Count == 0 ? "0" : lapHarian.NET_CARD_DEBIT.ToString("0,0.00");
            tbKredit.Text = listHarian.Count == 0 ? "0" : lapHarian.NET_CARD_KREDIT.ToString("0,0.00");
            tbTotalQty.Text = listHarian.Count == 0 ? "0" : lapHarian.QTY.ToString();
            tbDisc.Text = listHarian.Count == 0 ? "0" : lapHarian.DISC_R.ToString("0,0.00");
            tbBruto.Text = listHarian.Count == 0 ? "0" : lapHarian.TAG_PRICE.ToString("0,0.00");
            tbMargin.Text = listHarian.Count == 0 ? "0" : lapHarian.MARGIN.ToString("0,0.00");
            tbOthers.Text = listHarian.Count == 0 ? "0" : lapHarian.OTHERS.ToString("0,0.00");

        }

        protected void bindView()
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<SH_JUAL> listJual = new List<SH_JUAL>();

            string where = string.Format(" where ID_BAYAR = {0} and {1} like '%{2}%'", hdnIDBayarView.Value, ddlViewBy.SelectedValue, tbViewBy.Text.ToString().Trim());
            listJual = bayarDA.getSHJual(where);

            gvView.DataSource = listJual;
            gvView.DataBind();

            divVMessage.Visible = false;
            ModalViewDetail.Show();
        }

        protected void bindStatistic()
        {
            LAPORAN_DA laporanDA = new LAPORAN_DA();
            List<TF_STATISTIC> listStatistic = new List<TF_STATISTIC>();

            DateTime tglTrans = DateTime.Now;
            if (!string.IsNullOrEmpty(tbTglTrans.Text))
            {
                DateTime.TryParseExact(tbTglTrans.Text, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }


            DateTime tglEndTrans = DateTime.Now;
            if (!string.IsNullOrEmpty(tbTglEndTrans.Text))
            {
                DateTime.TryParseExact(tbTglEndTrans.Text, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglEndTrans);
            }
            string kode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            kode = ddlShowroom.SelectedValue;


            listStatistic = laporanDA.getTfStatistic(" where KODE = '" + kode + "'", kode, tglTrans, tglEndTrans);

            if (listStatistic.Count > 0)
            {
                TF_STATISTIC statistic = listStatistic.First();
                tbStockAwal.Text = statistic.STOCK_AWAL.ToString();
                tbStockAkhir.Text = statistic.STOCK_AKHIR.ToString();
                tbDiff.Text = statistic.DIFF.ToString();
                tbPenjualan.Text = statistic.PENJUALAN.ToString();
                tbTerimaBarang.Text = statistic.TERIMA_BRG.ToString();
                tbKeluarBarang.Text = statistic.KELUAR_BRG.ToString();
                tbTerimaPinjam.Text = statistic.TERIMA_PINJAM.ToString();
                tbKeluarPinjam.Text = statistic.KELUAR_PINJAM.ToString();
                tbAdjManual.Text = statistic.ADJ_MANUAL.ToString();
                tbAdjSO.Text = statistic.ADJUSTMENT_SO.ToString();
            }
        }

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindHarian();
            bindgrid();
        }

        protected void gvViewPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvMain.PageIndex = e.NewPageIndex;
            gvView.PageIndex = e.NewPageIndex;
            bindView();
        }

        protected void gvMainCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        hdnIDBayarView.Value = id;
                        bindView();
                    }
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void btnGenerateClick(object sender, EventArgs e)
        {
            bindHarian();
            bindgrid();
        }

        protected void btnPrintClick(object sender, EventArgs e)
        {
            gvMain.Columns[0].Visible = false;
            gvMain.AllowPaging = false;
            bindgrid();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Laporan_Penjualan.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();

            this.gvMain.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(this.gvMain);
            frm.RenderControl(htw);

            Response.Write(sw.ToString());

            gvMain.Columns[0].Visible = true;
            gvMain.AllowPaging = true;
            Response.End();
        }

        protected void btnRefreshHeaderClick(object sender, EventArgs e)
        {
            bindHarian();
            bindStatistic();
        }

        protected void btnPrintHarianClick(object sender, EventArgs e)
        {
            int month = int.Parse(tbTglTrans.Text.Substring(3,2));
            MS_SHOWROOM show = new MS_SHOWROOM();
            show.KODE = ddlShowroom.SelectedValue;
            show.SHOWROOM = ddlShowroom.SelectedItem.Text;

            LAPORAN_DA lapDA = new LAPORAN_DA();
            List<VW_LAPORAN_HARIAN> listVwLaporan = new List<VW_LAPORAN_HARIAN>();
            //listVwLaporan = lapDA.getPenjualanHarian("");
            listVwLaporan = lapDA.getPenjualanHarian(" where KODE = '" + show.KODE + "' and DATEPART(MONTH,TGL_TRANS) = " + month.ToString());

            laporanHarian(show, listVwLaporan);
        }

        protected void btnViewSearchClick(object sender, EventArgs e)
        {
            bindView();
        }

        protected void laporanHarian(MS_SHOWROOM show, List<VW_LAPORAN_HARIAN> listVwLaporan)
        {
            #region calculate
            
            var listGroupBy = listVwLaporan.GroupBy(u => u.POTONGAN).Select(grp => new { Potongan = grp.Key, listVwLaporan = grp.ToList() }).ToList();

            string tgl = tbTglTrans.Text.Remove(2);
            string bln = tbTglTrans.Text.Substring(3, 2);
            string thn = tbTglTrans.Text.Remove(0, 6);
            string month = string.Format("{0:MMMM}", new DateTime(int.Parse(thn), int.Parse(bln), int.Parse(tgl)));
            #endregion

            Response.Clear();           // Already have this
            Response.ClearContent();    // Add this line
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=LAP_HARIAN_" + show.KODE + "_" + month + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            /**********************************CREATE TEMPLATE**********************************/
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            float[] widths = new float[] { 1f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 25f;
            table.SpacingAfter = 0f;

            /*Create The First Table */
            //PdfPCell header = new PdfPCell(new Phrase("Packing List Wholesale", title1));
            //header.Colspan = 2;
            //header.Border = 0;
            //header.PaddingBottom = 20f;
            //header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //table.AddCell(header);


            PdfPCell call3 = new PdfPCell(new Phrase(new Chunk("PT.Sembilan Ohm Sembilan", regular)));
            call3.BorderWidth = 0;
            call3.BorderWidthTop = 1;
            call3.BorderWidthLeft = 1;
            call3.BorderWidthRight = 1;
            call3.BorderWidthBottom = 0;
            table.AddCell(call3);
            //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
            PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("", regular)));
            call1.BorderWidthTop = 1;
            call1.BorderWidthLeft = 1;
            call1.BorderWidthRight = 1;
            call1.BorderWidthBottom = 0;
            table.AddCell(call1);
            //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

            //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
            PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("PENJUALAN HARIAN", regular)));
            call5.BorderWidthTop = 0;
            call5.BorderWidthLeft = 1;
            call5.BorderWidthRight = 1;
            call5.BorderWidthBottom = 0;
            table.AddCell(call5);

            PdfPCell call2 = new PdfPCell(new Phrase(new Chunk("", regular)));
            call2.BorderWidthTop = 0;
            call2.BorderWidthLeft = 1;
            call2.BorderWidthRight = 1;
            call2.BorderWidthBottom = 0;
            table.AddCell(call2);
            //table.AddCell(new Phrase(new Chunk("VENUE OF MEETING : " + Lbl_Venue.Text + "", regular)));

            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Kode : " + show.KODE, regular)));
            callCIF.BorderWidthTop = 0;
            callCIF.BorderWidthLeft = 1;
            callCIF.BorderWidthRight = 1;
            callCIF.BorderWidthBottom = 0;
            table.AddCell(callCIF);

            //table.AddCell(new Phrase(new Chunk("INDUSTRY : " + Lbl_Industry.Text + "", regular)));
            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("No. ", regular)));
            callCP.BorderWidthTop = 0;
            callCP.BorderWidthLeft = 1;
            callCP.BorderWidthRight = 1;
            callCP.BorderWidthBottom = 0;
            table.AddCell(callCP);
            //table.AddCell(new Phrase(new Chunk("PERSON CONTACTED (NAME & DESIGNATION)", regular)));
            /*
            PdfPCell call8 = new PdfPCell(new Phrase(new Chunk(Lbl_Cp.Text, regular)));
            call8.BorderWidth = 1;
            table.AddCell(call8);
            //table.AddCell(new Phrase(new Chunk(": " + Lbl_Cp.Text + "", regular)));
            */

            PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Customer/Toko : " + show.SHOWROOM, regular)));
            callAcNo.BorderWidthTop = 0;
            callAcNo.BorderWidthLeft = 1;
            callAcNo.BorderWidthRight = 1;
            callAcNo.BorderWidthBottom = 0;
            table.AddCell(callAcNo);

            PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Tgl. SJ : ", regular)));
            callCreatedBy.BorderWidthTop = 0;
            callCreatedBy.BorderWidthLeft = 1;
            callCreatedBy.BorderWidthRight = 1;
            callCreatedBy.BorderWidthBottom = 0;
            table.AddCell(callCreatedBy);

            //table.AddCell(new Phrase(new Chunk("GCIF : " + Lbl_Gcif.Text + "", regular)));
            PdfPCell callIndustry = new PdfPCell(new Phrase(new Chunk("Periode : 01" + tbTglTrans.Text.Remove(0, 2) + " s/d " + DateTime.DaysInMonth(int.Parse(tbTglTrans.Text.Remove(0, 6)), int.Parse(tbTglTrans.Text.Substring(3, 2))) + tbTglTrans.Text.Remove(0, 2), regular)));
            callIndustry.BorderWidthTop = 0;
            callIndustry.BorderWidthLeft = 1;
            callIndustry.BorderWidthRight = 1;
            callIndustry.BorderWidthBottom = 0;
            table.AddCell(callIndustry);

            PdfPCell callRelOfficer = new PdfPCell(new Phrase(new Chunk(string.Format("{0:dd-MM-yy hh:mm:ss}",DateTime.Now), regular)));
            callRelOfficer.BorderWidthTop = 0;
            callRelOfficer.BorderWidthLeft = 1;
            callRelOfficer.BorderWidthRight = 1;
            callRelOfficer.BorderWidthBottom = 0;
            table.AddCell(callRelOfficer);

            PdfPCell callDebtName = new PdfPCell(new Phrase(new Chunk("", regular)));
            callDebtName.BorderWidthTop = 0;
            callDebtName.BorderWidthLeft = 1;
            callDebtName.BorderWidthRight = 1;
            callDebtName.BorderWidthBottom = 0;
            table.AddCell(callDebtName);
            //table.AddCell(new Phrase(new Chunk("GROUP DEBTOR NAME", regular)));

            PdfPCell call10 = new PdfPCell(new Phrase(new Chunk("", regular)));
            call10.BorderWidthTop = 0;
            call10.BorderWidthLeft = 1;
            call10.BorderWidthRight = 1;
            call10.BorderWidthBottom = 0;
            table.AddCell(call10);

            //table.AddCell(new Phrase(new Chunk(":", regular)));
            PdfPCell call11 = new PdfPCell(new Phrase(new Chunk("", regular)));
            call11.BorderWidthTop = 0;
            call11.BorderWidthLeft = 1;
            call11.BorderWidthRight = 1;
            call11.BorderWidthBottom = 1;
            table.AddCell(call11);
            //table.AddCell(new Phrase(new Chunk("MEMBER OF THE GROUP", regular)));
            PdfPCell call12 = new PdfPCell(new Phrase(new Chunk("", regular)));
            call12.BorderWidthTop = 0;
            call12.BorderWidthLeft = 1;
            call12.BorderWidthRight = 1;
            call12.BorderWidthBottom = 1;
            table.AddCell(call12);
            //table.AddCell(new Phrase(new Chunk(":", regular)));
            //create a black line
            PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            black.BackgroundColor = new BaseColor(0, 0, 0);
            black.BorderColor = new BaseColor(0, 0, 0);
            black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            black.BorderWidth = 1;
            black.Colspan = 2;
            table.AddCell(black);
            ///////////////////////////

            //PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : ", regular)));
            //bottom1.Colspan = 2;
            //bottom1.PaddingBottom = 20f;
            //bottom1.PaddingTop = 3f;
            //bottom1.BorderWidth = 1;
            //table.AddCell(bottom1);
            pdfDoc.Add(table);

            string gridHtml = sw.ToString().Replace("class=\"viewdata\"", "");
            gridHtml = gridHtml.Replace("style=\"border-collapse:collapse;\"", "style=\"border-collapse:collapse;font-size:1;font-family:Times-Roman,serif,georgia;\"");
            StringReader sr = new StringReader(gridHtml);
            /*Show DataGrid*/
            htmlparser.Parse(sr);

            //Masukin Table

            PdfPTable table4 = new PdfPTable(11);
            float[] width4 = new float[] { 0.5f, 0.2f, 0.6f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0.3f, 0.4f, 0.2f };
            table4.TotalWidth = 575f;
            table4.LockedWidth = true;
            table4.SetWidths(width4);
            table4.HorizontalAlignment = 0;
            table4.SpacingBefore = 0f;
            table4.SpacingAfter = 20f;

            PdfPCell tglCell = new PdfPCell(new Phrase(new Chunk("Tanggal", regular)));
            tglCell.BorderWidth = 1;
            tglCell.HorizontalAlignment = 1;
            table4.AddCell(tglCell);
            PdfPCell qtyCell = new PdfPCell(new Phrase(new Chunk("Qty", regular)));
            qtyCell.BorderWidth = 1;
            qtyCell.HorizontalAlignment = 1;
            table4.AddCell(qtyCell);
            PdfPCell tagCell = new PdfPCell(new Phrase(new Chunk("Nilai Tag", regular)));
            tagCell.BorderWidth = 1;
            tagCell.HorizontalAlignment = 1;
            table4.AddCell(tagCell);
            PdfPCell bonCell = new PdfPCell(new Phrase(new Chunk("Nilai Bon", regular)));
            bonCell.BorderWidth = 1;
            bonCell.HorizontalAlignment = 1;
            table4.AddCell(bonCell);
            PdfPCell discAcaraCell = new PdfPCell(new Phrase(new Chunk("Disc. Acara", regular)));
            discAcaraCell.BorderWidth = 1;
            discAcaraCell.HorizontalAlignment = 1;
            table4.AddCell(discAcaraCell);
            PdfPCell custBayarCell = new PdfPCell(new Phrase(new Chunk("Cust Bayar", regular)));
            custBayarCell.BorderWidth = 1;
            custBayarCell.HorizontalAlignment = 1;
            table4.AddCell(custBayarCell);
            PdfPCell terimaCell = new PdfPCell(new Phrase(new Chunk("Uang Terima", regular)));
            terimaCell.BorderWidth = 1;
            terimaCell.HorizontalAlignment = 1;
            table4.AddCell(terimaCell);

            PdfPCell percDiscCell = new PdfPCell(new Phrase(new Chunk("Disc ac", regular)));
            percDiscCell.BorderWidth = 1;
            percDiscCell.HorizontalAlignment = 1;
            table4.AddCell(percDiscCell);

            PdfPCell marginCell = new PdfPCell(new Phrase(new Chunk("Margin", regular)));
            marginCell.BorderWidth = 1;
            marginCell.HorizontalAlignment = 1;
            table4.AddCell(marginCell);
            PdfPCell partCell = new PdfPCell(new Phrase(new Chunk("Partisp", regular)));
            partCell.BorderWidth = 1;
            partCell.HorizontalAlignment = 1;
            table4.AddCell(partCell);
            PdfPCell statCell = new PdfPCell(new Phrase(new Chunk("", regular)));
            statCell.BorderWidth = 1;
            statCell.HorizontalAlignment = 1;
            table4.AddCell(statCell);

            int Total = 0;
            decimal totalTag = 0;
            decimal totalBon = 0;
            decimal totalDiscAcara = 0;
            decimal totalCustBayar = 0;
            decimal totalTerima = 0;
            foreach (var item in listGroupBy)
            {
                List<VW_LAPORAN_HARIAN> listLaporanHarianGroup = new List<VW_LAPORAN_HARIAN>();
                listLaporanHarianGroup = listVwLaporan.Where(itemw => itemw.POTONGAN == item.Potongan).OrderBy(itemo => itemo.TGL_TRANS).ToList();

                int subTotal = 0;
                decimal subTag = 0;
                decimal subBon = 0;
                decimal subDiscAcara = 0;
                decimal subCustBayar = 0;
                decimal subTerima = 0;

                foreach (VW_LAPORAN_HARIAN vwLaporan in listLaporanHarianGroup)
                {
                    iTextSharp.text.Font font = regular;

                    PdfPCell cell1 = new PdfPCell(new Phrase(string.Format("{0:dd-MM-yyyy}", vwLaporan.TGL_TRANS), font));
                    cell1.BorderWidth = 1;
                    cell1.HorizontalAlignment = 1;
                    table4.AddCell(cell1);
                    PdfPCell cell2 = new PdfPCell(new Phrase(vwLaporan.QTY.ToString(), font));
                    cell2.BorderWidth = 1;
                    cell2.HorizontalAlignment = 0;
                    table4.AddCell(cell2);
                    PdfPCell cell3 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.TAG_PRICE), font));
                    cell3.BorderWidth = 1;
                    cell3.HorizontalAlignment = 0;
                    table4.AddCell(cell3);
                    PdfPCell cell4 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.BON_PRICE), font));
                    cell4.BorderWidth = 1;
                    cell4.HorizontalAlignment = 0;
                    table4.AddCell(cell4);
                    PdfPCell cell5 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.DISC_ACARA), font));
                    cell5.BorderWidth = 1;
                    cell5.HorizontalAlignment = 0;
                    table4.AddCell(cell5);
                    PdfPCell cell6 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.CUST_BAYAR), font));
                    cell6.BorderWidth = 1;
                    cell6.HorizontalAlignment = 0;
                    table4.AddCell(cell6);
                    PdfPCell cell7 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.UANG_TERIMA), font));
                    cell7.BorderWidth = 1;
                    cell7.HorizontalAlignment = 0;
                    table4.AddCell(cell7);

                    PdfPCell cell8 = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", vwLaporan.POTONGAN), font));
                    cell8.BorderWidth = 1;
                    cell8.HorizontalAlignment = 0;
                    table4.AddCell(cell8);
                    PdfPCell cell9 = new PdfPCell(new Phrase(vwLaporan.MARGIN.ToString(), font));
                    cell9.BorderWidth = 1;
                    cell9.HorizontalAlignment = 0;
                    table4.AddCell(cell9);
                    PdfPCell cell10 = new PdfPCell(new Phrase("...", font));
                    cell10.BorderWidth = 1;
                    cell10.HorizontalAlignment = 0;
                    table4.AddCell(cell10);
                    PdfPCell cell11 = new PdfPCell(new Phrase("A", font));
                    cell11.BorderWidth = 1;
                    cell11.HorizontalAlignment = 0;
                    table4.AddCell(cell11);

                    subTotal = subTotal + vwLaporan.QTY;
                    subTag = subTag + vwLaporan.TAG_PRICE;
                    subBon = subBon + vwLaporan.BON_PRICE;
                    subDiscAcara = subDiscAcara + vwLaporan.DISC_ACARA;
                    subCustBayar = subCustBayar + vwLaporan.CUST_BAYAR;
                    subTerima = subTerima + vwLaporan.UANG_TERIMA;
                }

                Total = Total + subTotal;
                totalTag = totalTag + subTag;
                totalBon = totalBon + subBon;
                totalDiscAcara = totalDiscAcara + subDiscAcara;
                totalCustBayar = totalCustBayar + subCustBayar;
                totalTerima = totalTerima + subTerima;

                PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Sub Total :", regular));
                cellNamaTotalQty.BorderWidth = 1;
                cellNamaTotalQty.HorizontalAlignment = 1;
                table4.AddCell(cellNamaTotalQty);

                PdfPCell cellTotalQty = new PdfPCell(new Phrase(subTotal.ToString(), regular));
                cellTotalQty.BorderWidth = 1;
                cellTotalQty.HorizontalAlignment = 0;
                table4.AddCell(cellTotalQty);

                PdfPCell cellTotaltagPrice = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", subTag), regular));
                cellTotaltagPrice.BorderWidth = 1;
                cellTotaltagPrice.HorizontalAlignment = 0;
                table4.AddCell(cellTotaltagPrice);

                PdfPCell cellTotalBonPrice = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", subBon), regular));
                cellTotalBonPrice.BorderWidth = 1;
                cellTotalBonPrice.HorizontalAlignment = 0;
                table4.AddCell(cellTotalBonPrice);

                PdfPCell cellTotalDiscAcara = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", subDiscAcara), regular));
                cellTotalDiscAcara.BorderWidth = 1;
                cellTotalDiscAcara.HorizontalAlignment = 0;
                table4.AddCell(cellTotalDiscAcara);

                PdfPCell cellTotalCustBayar = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", subCustBayar), regular));
                cellTotalCustBayar.BorderWidth = 1;
                cellTotalCustBayar.HorizontalAlignment = 0;
                table4.AddCell(cellTotalCustBayar);

                PdfPCell cellTotalDlmTerima = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", subTerima), regular));
                cellTotalDlmTerima.BorderWidth = 1;
                cellTotalDlmTerima.HorizontalAlignment = 0;
                table4.AddCell(cellTotalDlmTerima);

                PdfPCell cellTotalDiscPercAcara = new PdfPCell(new Phrase("", regular));
                cellTotalDiscPercAcara.BorderWidth = 1;
                cellTotalDiscPercAcara.HorizontalAlignment = 0;
                table4.AddCell(cellTotalDiscPercAcara);

                PdfPCell cellTotalMargin = new PdfPCell(new Phrase("", regular));
                cellTotalMargin.BorderWidth = 1;
                cellTotalMargin.HorizontalAlignment = 0;
                table4.AddCell(cellTotalMargin);

                PdfPCell cellTotalPart = new PdfPCell(new Phrase("", regular));
                cellTotalPart.BorderWidth = 1;
                cellTotalPart.HorizontalAlignment = 0;
                table4.AddCell(cellTotalPart);

                PdfPCell cellTotalStatus = new PdfPCell(new Phrase("", regular));
                cellTotalStatus.BorderWidth = 1;
                cellTotalStatus.HorizontalAlignment = 0;
                table4.AddCell(cellTotalStatus);

                PdfPCell cellSpasi = new PdfPCell(new Phrase("", regular));
                cellSpasi.Colspan = 11;
                cellSpasi.BorderWidth = 1;
                cellSpasi.HorizontalAlignment = 0;
                table4.AddCell(cellSpasi);
            }

            PdfPCell cellTotalAll = new PdfPCell(new Phrase("Total :", regular));
            cellTotalAll.BorderWidth = 1;
            cellTotalAll.HorizontalAlignment = 1;
            table4.AddCell(cellTotalAll);

            PdfPCell cellTotal = new PdfPCell(new Phrase(Total.ToString(), regular));
            cellTotal.BorderWidth = 1;
            cellTotal.HorizontalAlignment = 0;
            table4.AddCell(cellTotal);

            PdfPCell cellTotalAlltagPrice = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", totalTag), regular));
            cellTotalAlltagPrice.BorderWidth = 1;
            cellTotalAlltagPrice.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAlltagPrice);

            PdfPCell cellTotalAllBonPrice = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", totalBon), regular));
            cellTotalAllBonPrice.BorderWidth = 1;
            cellTotalAllBonPrice.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllBonPrice);

            PdfPCell cellTotalAllDiscAcara = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", totalDiscAcara), regular));
            cellTotalAllDiscAcara.BorderWidth = 1;
            cellTotalAllDiscAcara.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllDiscAcara);

            PdfPCell cellTotalAllCustBayar = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", totalCustBayar), regular));
            cellTotalAllCustBayar.BorderWidth = 1;
            cellTotalAllCustBayar.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllCustBayar);

            PdfPCell cellTotalAllDlmTerima = new PdfPCell(new Phrase(String.Format("{0:0,0.00}", totalTerima), regular));
            cellTotalAllDlmTerima.BorderWidth = 1;
            cellTotalAllDlmTerima.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllDlmTerima);

            PdfPCell cellTotalAllDiscPercAcara = new PdfPCell(new Phrase("", regular));
            cellTotalAllDiscPercAcara.BorderWidth = 1;
            cellTotalAllDiscPercAcara.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllDiscPercAcara);

            PdfPCell cellAllTotalMargin = new PdfPCell(new Phrase("", regular));
            cellAllTotalMargin.BorderWidth = 1;
            cellAllTotalMargin.HorizontalAlignment = 0;
            table4.AddCell(cellAllTotalMargin);

            PdfPCell cellTotalAllPart = new PdfPCell(new Phrase("", regular));
            cellTotalAllPart.BorderWidth = 1;
            cellTotalAllPart.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllPart);

            PdfPCell cellTotalAllStatus = new PdfPCell(new Phrase("", regular));
            cellTotalAllStatus.BorderWidth = 1;
            cellTotalAllStatus.HorizontalAlignment = 0;
            table4.AddCell(cellTotalAllStatus);

            pdfDoc.Add(table4);

            /*Show Comment As Table*/
            StringWriter sw2 = new StringWriter();
            HtmlTextWriter hw2 = new HtmlTextWriter(sw2);

            gridHtml = sw2.ToString().Replace("class=\"viewdata\"", "");
            gridHtml = gridHtml.Replace("style=\"border-collapse:collapse;\"", "style=\"border-collapse:collapse;font-size:9;font-family:Times-Roman,serif,georgia;\"");
            StringReader sr2 = new StringReader(gridHtml);
            htmlparser.Parse(sr2);

            /*Template Done*/

            pdfDoc.Close();
            Response.Write(pdfDoc);
            //Response.End();
            //Response.Close();
            Response.Flush(); Response.Close();
        }
    }
}