using ATMOS_SROM.Domain;
using ATMOS_SROM.Domain.CustomObj;
using ATMOS_SROM.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Sales
{
    public partial class WholeSalesWareHouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindAllWholeSale();
            }
        }
        #region "Method BindGrid"
        protected void bindAllWholeSale()
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<SH_PUTUS_HEADER> putusHeaderList = new List<SH_PUTUS_HEADER>();
            string where = tbBONSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%'", ddlBONSearch.SelectedValue, tbBONSearch.Text);

            putusHeaderList = bayarDA.getSHPutusHeaderJoinSoOrder(where);
            gvBON.DataSource = putusHeaderList;
            gvBON.DataBind();
            
        }
        protected void bindGridSO()
        {
            List<SO_WHOLESALES> listTemp = new List<SO_WHOLESALES>();
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            //string user = Session["UName"] == null ? "" : Session["UName"].ToString();
            if (txtSearchSO.Text != "")
            {
                listTemp = tstDA.GetSOToShPutusList(" where NO_SO = '" + txtSearchSO.Text + "'");
            }
            else
            {
                listTemp = tstDA.GetSOToShPutusList("");
            }
            gvSo.DataSource = listTemp;
            gvSo.DataBind();

        }
        protected void bindGridSODetail()
        {
            List<SO_WHOLESALES> listTemp = new List<SO_WHOLESALES>();
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            //string user = Session["UName"] == null ? "" : Session["UName"].ToString();

            listTemp = tstDA.GetSODetail(" where NO_SO = '" + lblnoso.Text + "'");


            gvDetailSO.DataSource = listTemp;
            gvDetailSO.DataBind();

        }
        protected void bindGridCloseSO()
        {
            List<vw_CloseSO> listTemp = new List<vw_CloseSO>();
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            //string user = Session["UName"] == null ? "" : Session["UName"].ToString();
            if (txtSearchCloseSO.Text != "")
            {
                listTemp = tstDA.GetCloseSOList(" where NO_SO = '" + txtSearchCloseSO.Text + "'");
            }
            else
            {
                listTemp = tstDA.GetCloseSOList("");
            }
            gvCloseSo.DataSource = listTemp;
            gvCloseSo.DataBind();

        }
        protected void bindItem()
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
            string where = string.Format(" where a.ID_BAYAR like {0}", hdnIReturIDHeader.Value);

            //putusDetailList = bayarDA.getSHPutusDetail(where);
            string tableName = "SH_PUTUS_DETAIL";
            string selectTable = "a.ID, a.ID_BAYAR, a.ID_KDBRG, a.KODE_CUST, a.KODE, a.TGL_TRANS, a.NO_BON, a.ITEM_CODE, " +
                    " a.TAG_PRICE, a.BON_PRICE, a.NILAI_BYR, a.MARGIN, a.QTY, a.JUAL_RETUR, a.QTY_ACTUAL, a.FSTATUS, a.FRETUR, ";
            putusDetailList = bayarDA.getSHPutusDetailWithSP(tableName, selectTable, where);
            gvIRetur.DataSource = putusDetailList;
            gvIRetur.DataBind();

            if (tbIReturStatus.Text.ToLower() == "prepare")
            {
                gvIRetur.Columns[18].Visible = false;
                gvIRetur.Columns[19].Visible = true;
            }
            else
            {
                gvIRetur.Columns[18].Visible = true;
                gvIRetur.Columns[19].Visible = false;
            }

            btnIReturSave.Enabled = gvIRetur.PageIndex == gvIRetur.PageCount - 1 ? true : false;

            btnIReturPrintDeliveryOrder.Visible = tbIReturStatus.Text.ToLower() == "prepare" ? true : false;
            //btnIReturPrintPackingList.Visible = tbIReturStatus.Text.ToLower() == "prepare" ? false : true;
            divIReturMessage.Visible = false;
            ModalReturItem.Show();
        }
        protected void bindItemWholeSale()
        {
            divIReturMessage.Visible = false;
            if (updateStockPrepare())
            {
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
                string where = string.Format(" where a.ID_BAYAR like {0}", hdnIReturIDHeader.Value);

                //putusDetailList = bayarDA.getSHPutusDetail(where);
                string tableName = "SH_PUTUS_DETAIL";
                string selectTable = "a.ID, a.ID_BAYAR, a.ID_KDBRG, a.KODE_CUST, a.KODE, a.TGL_TRANS, a.NO_BON, a.ITEM_CODE, " +
                        " a.TAG_PRICE, a.BON_PRICE, a.NILAI_BYR, a.MARGIN, a.QTY, a.JUAL_RETUR, a.QTY_ACTUAL, a.FSTATUS, a.FRETUR, ";
                putusDetailList = bayarDA.getSHPutusDetailWithSP(tableName, selectTable, where);
                gvIRetur.DataSource = putusDetailList;
                gvIRetur.DataBind();

                if (tbIReturStatus.Text.ToLower() == "prepare")
                {
                    gvIRetur.Columns[18].Visible = false;
                    gvIRetur.Columns[19].Visible = true;
                }
                else
                {
                    gvIRetur.Columns[18].Visible = true;
                    gvIRetur.Columns[19].Visible = false;
                }

                btnIReturSave.Enabled = gvIRetur.PageIndex == gvIRetur.PageCount - 1 ? true : false;
            }
            btnIReturPrintDeliveryOrder.Visible = tbIReturStatus.Text.ToLower() == "prepare" ? true : false;
            //btnIReturPrintPackingList.Visible = tbIReturStatus.Text.ToLower() == "prepare" ? false : true;
            ModalReturItem.Show();
        }
        #endregion
        #region "method Ins & Upd & PRINT"
        protected bool insertStock(MS_STOCK stock)
        {
            bool ret = true;
            try
            {
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                string insert = stockDA.insertDataStock(stock);

                ret = insert == "Berhasil!" ? true : false;
                DivMessage.InnerText = insert;
                DivMessage.Attributes["class"] = insert == "Berhasil!" ? "success" : "error";
                DivMessage.Visible = true;
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
                ret = false;
            }
            return ret;
        }

        protected bool updateStock(MS_STOCK stock)
        {
            bool ret = true;
            try
            {
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                string insert = stockDA.updateDataStockWithID(stock);

                ret = insert == "Berhasil!" ? true : false;
                DivMessage.InnerText = insert;
                DivMessage.Attributes["class"] = insert == "Berhasil!" ? "success" : "error";
                DivMessage.Visible = true;
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
                ret = false;
            }
            return ret;
        }

        protected void printDeliveryNotes(string noBukti, List<SH_PUTUS_DETAIL> listPutus, MS_SHOWROOM show)
        {
            Response.Clear();           // Already have this
            Response.ClearContent();    // Add this line
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Packing_List_" + noBukti + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
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
            PdfPCell header = new PdfPCell(new Phrase("Packing List Wholesale", title1));
            header.Colspan = 2;
            header.Border = 0;
            header.PaddingBottom = 20f;
            header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(header);
            PdfPCell call3 = new PdfPCell(new Phrase(new Chunk("PT.Sembilan Ohm Sembilan", regular)));
            call3.BorderWidth = 0;
            call3.BorderWidthTop = 1;
            call3.BorderWidthLeft = 1;
            call3.BorderWidthRight = 1;
            call3.BorderWidthBottom = 0;
            table.AddCell(call3);
            //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
            PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Kepada, Yth " + show.SHOWROOM, regular)));
            call1.BorderWidthTop = 1;
            call1.BorderWidthLeft = 1;
            call1.BorderWidthRight = 1;
            call1.BorderWidthBottom = 0;
            table.AddCell(call1);
            //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

            //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
            PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Jl Ciniru iV no 16", regular)));
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

            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124", regular)));
            callCIF.BorderWidthTop = 0;
            callCIF.BorderWidthLeft = 1;
            callCIF.BorderWidthRight = 1;
            callCIF.BorderWidthBottom = 0;
            table.AddCell(callCIF);

            //table.AddCell(new Phrase(new Chunk("INDUSTRY : " + Lbl_Industry.Text + "", regular)));
            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("", regular)));
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

            PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("", regular)));
            callAcNo.BorderWidthTop = 0;
            callAcNo.BorderWidthLeft = 1;
            callAcNo.BorderWidthRight = 1;
            callAcNo.BorderWidthBottom = 0;
            table.AddCell(callAcNo);

            PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("", regular)));
            callCreatedBy.BorderWidthTop = 0;
            callCreatedBy.BorderWidthLeft = 1;
            callCreatedBy.BorderWidthRight = 1;
            callCreatedBy.BorderWidthBottom = 0;
            table.AddCell(callCreatedBy);

            //table.AddCell(new Phrase(new Chunk("GCIF : " + Lbl_Gcif.Text + "", regular)));
            PdfPCell callIndustry = new PdfPCell(new Phrase(new Chunk("", regular)));
            callIndustry.BorderWidthTop = 0;
            callIndustry.BorderWidthLeft = 1;
            callIndustry.BorderWidthRight = 1;
            callIndustry.BorderWidthBottom = 0;
            table.AddCell(callIndustry);

            PdfPCell callRelOfficer = new PdfPCell(new Phrase(new Chunk("", regular)));
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
            PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : " + noBukti, regular)));
            bottom1.Colspan = 2;
            bottom1.PaddingBottom = 20f;
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            table.AddCell(bottom1);
            pdfDoc.Add(table);

            string gridHtml = sw.ToString().Replace("class=\"viewdata\"", "");
            gridHtml = gridHtml.Replace("style=\"border-collapse:collapse;\"", "style=\"border-collapse:collapse;font-size:1;font-family:Times-Roman,serif,georgia;\"");
            StringReader sr = new StringReader(gridHtml);
            /*Show DataGrid*/
            htmlparser.Parse(sr);

            //Masukin Table

            PdfPTable table4 = new PdfPTable(8);
            float[] width4 = new float[] { 0.2f, 0.8f, 0.8f, 1f, 0.5f, 0.4f, 0.4f, 0.4f };
            table4.TotalWidth = 575f;
            table4.LockedWidth = true;
            table4.SetWidths(width4);
            table4.HorizontalAlignment = 0;
            table4.SpacingBefore = 0f;
            table4.SpacingAfter = 20f;

            PdfPCell no = new PdfPCell(new Phrase(new Chunk("NO", regular)));
            no.BorderWidth = 1;
            no.HorizontalAlignment = 1;
            table4.AddCell(no);
            PdfPCell barcode = new PdfPCell(new Phrase(new Chunk("BARCODE", regular)));
            barcode.BorderWidth = 1;
            barcode.HorizontalAlignment = 1;
            table4.AddCell(barcode);
            PdfPCell itemCode = new PdfPCell(new Phrase(new Chunk("ITEM CODE", regular)));
            itemCode.BorderWidth = 1;
            itemCode.HorizontalAlignment = 1;
            table4.AddCell(itemCode);
            PdfPCell art = new PdfPCell(new Phrase(new Chunk("ARTICLE", regular)));
            art.BorderWidth = 1;
            art.HorizontalAlignment = 1;
            table4.AddCell(art);
            PdfPCell col = new PdfPCell(new Phrase(new Chunk("COLOR", regular)));
            col.BorderWidth = 1;
            col.HorizontalAlignment = 1;
            table4.AddCell(col);
            PdfPCell size = new PdfPCell(new Phrase(new Chunk("SIZE", regular)));
            size.BorderWidth = 1;
            size.HorizontalAlignment = 1;
            table4.AddCell(size);
            PdfPCell qty = new PdfPCell(new Phrase(new Chunk("QTY", regular)));
            qty.BorderWidth = 1;
            qty.HorizontalAlignment = 1;
            table4.AddCell(qty);
            PdfPCell qtyReal = new PdfPCell(new Phrase(new Chunk("QTY REAL", regular)));
            qtyReal.BorderWidth = 1;
            qtyReal.HorizontalAlignment = 1;
            table4.AddCell(qtyReal);

            int Total = 0;
            for (int i = 0; i < listPutus.Count; i++)
            {
                iTextSharp.text.Font font = regular;

                PdfPCell cell1 = new PdfPCell(new Phrase(Convert.ToString(i + 1), font));
                cell1.BorderWidth = 1;
                cell1.HorizontalAlignment = 1;
                table4.AddCell(cell1);
                PdfPCell cellBarcode = new PdfPCell(new Phrase(listPutus[i].FBARCODE, font));
                cellBarcode.BorderWidth = 1;
                cellBarcode.HorizontalAlignment = 0;
                table4.AddCell(cellBarcode);
                PdfPCell cell2 = new PdfPCell(new Phrase(listPutus[i].FITEM_CODE, font));
                cell2.BorderWidth = 1;
                cell2.HorizontalAlignment = 0;
                table4.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase(listPutus[i].FART_DESC, font));
                cell3.BorderWidth = 1;
                cell3.HorizontalAlignment = 0;
                table4.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase(listPutus[i].FCOL_DESC, font));
                cell4.BorderWidth = 1;
                cell4.HorizontalAlignment = 0;
                table4.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase(listPutus[i].FSIZE_DESC, font));
                cell5.BorderWidth = 1;
                cell5.HorizontalAlignment = 0;
                table4.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase(listPutus[i].QTY.ToString(), font));
                cell6.BorderWidth = 1;
                cell6.HorizontalAlignment = 0;
                table4.AddCell(cell6);

                PdfPCell cell7 = new PdfPCell(new Phrase("...", font));
                cell7.BorderWidth = 1;
                cell7.HorizontalAlignment = 0;
                table4.AddCell(cell7);

                Total = Total + listPutus[i].QTY;
            }

            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 6;
            cellNamaTotalQty.BorderWidth = 1;
            cellNamaTotalQty.HorizontalAlignment = 1;
            table4.AddCell(cellNamaTotalQty);

            PdfPCell cellTotalQty = new PdfPCell(new Phrase(Total.ToString(), regular));
            cellTotalQty.BorderWidth = 1;
            cellTotalQty.HorizontalAlignment = 0;
            table4.AddCell(cellTotalQty);

            PdfPCell cellTotalQtyReal = new PdfPCell(new Phrase("...", regular));
            cellTotalQtyReal.BorderWidth = 1;
            cellTotalQtyReal.HorizontalAlignment = 0;
            table4.AddCell(cellTotalQtyReal);

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

        protected void inputIntoTempStruckRetur()
        {

        }
        
        protected bool checkQTY()
        {
            bool check = true;
            try
            {
                TextBox tbIReturQtyActual;
                for (int i = 0; i < gvIRetur.Rows.Count; i++)
                {
                    tbIReturQtyActual = (TextBox)gvIRetur.Rows[i].FindControl("tbIReturQtyActual");
                    int qtyPrepare = Convert.ToInt32(gvIRetur.Rows[i].Cells[13].Text.ToString());
                    int qtyActual = Convert.ToInt32(tbIReturQtyActual.Text == "" ? "0"
                        : tbIReturQtyActual.Text.Contains("nbsp") ? "0" : tbIReturQtyActual.Text);
                    if (qtyPrepare < qtyActual || tbIReturQtyActual.Text == "")
                    {
                        check = false;
                        i = i + gvIRetur.Rows.Count;

                        divIReturMessage.InnerText = qtyPrepare < qtyActual ? "Quantity barang yang di kirim tidak boleh melebihi yang di prepare. Silakan buat SO yang baru."
                            : "Harap Textbox di isi semua dengan pengiriman nilai actualnya.";
                        divIReturMessage.Attributes["class"] = "warning";
                        divIReturMessage.Visible = true;

                        ModalReturItem.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                check = false;
                divIReturMessage.InnerText = "Error : " + ex.Message;
                divIReturMessage.Attributes["class"] = "error";
                divIReturMessage.Visible = true;
            }
            return check;
        }

        protected bool updateStockPrepare()
        {
            bool check = true;
            try
            {
                TextBox tbIReturQtyActual;
                string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
                List<MS_PO_DETAIL> poList = new List<MS_PO_DETAIL>();
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                bool update = false;

                foreach (GridViewRow item in gvIRetur.Rows)
                {
                    tbIReturQtyActual = (TextBox)item.FindControl("tbIReturQtyActual");

                    if (!(tbIReturQtyActual.Text == "") && !(tbIReturQtyActual.Text.Contains("nbsp")) && tbIReturQtyActual.Enabled)
                    {
                        SH_PUTUS_DETAIL putusDetail = new SH_PUTUS_DETAIL();
                        string id = gvIRetur.DataKeys[item.RowIndex].Value.ToString();
                        putusDetail.ID = Convert.ToInt64(id);
                        putusDetail.QTY_ACTUAL = Convert.ToInt32(tbIReturQtyActual.Text);
                        putusDetail.FSTATUS = "Done";

                        bayarDA.updateQTYPutusDetail(putusDetail);

                        //GET STOCK ITEM
                        List<MS_STOCK> listStock = new List<MS_STOCK>();
                        MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        MS_STOCK stock = new MS_STOCK();
                        stock.KODE = "WARE-001";
                        listStock = stockDA.getStock(string.Format(" where LTRIM(RTRIM(BARCODE)) = '{0}' and KODE = '{1}'", item.Cells[4].Text.Trim().ToString(), stock.KODE));
                        if (listStock.Count > 0)
                        {
                            stock = listStock.First();
                        }
                        stock.ITEM_CODE = item.Cells[5].Text.Trim().ToString();
                        stock.BARCODE = item.Cells[4].Text.Trim().ToString();
                        stock.WAREHOUSE = "Main Warehouse";
                        stock.KODE = "WARE-001";
                        stock.STOCK = tbIReturRetur.Text.ToLower().Trim() == "no" ? Convert.ToInt32(tbIReturQtyActual.Text) * -1 : Convert.ToInt32(tbIReturQtyActual.Text);
                        stock.RAK = "000";
                        stock.CREATED_BY = Session["UName"].ToString();
                        stock.UPDATED_BY = Session["UName"].ToString();

                        bool statusStock = listStock.Count == 0 ? insertStock(stock) : updateStock(stock);
                        update = true;
                        //string wherePO = string.Format(" where ID in ({0})", hdnDetail.Value);
                        //poDA.updateStatusPO(wherePO, "Receive");
                    }
                }

                divIReturMessage.InnerText = "Berhasil!";
                divIReturMessage.Attributes["class"] = "success";
                divIReturMessage.Visible = update;
            }
            catch (Exception ex)
            {
                check = false;
                divIReturMessage.InnerText = "Error : " + ex.Message;
                divIReturMessage.Attributes["class"] = "error";
                divIReturMessage.Visible = true;
            }
            return check;
        }

        protected void printNoBukti(string noBukti, List<SH_PUTUS_DETAIL> listPutus, MS_SHOWROOM show)
        {
            Response.Clear();           // Already have this
            Response.ClearContent();    // Add this line
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Packing_List_" + noBukti + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
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
            PdfPCell header = new PdfPCell(new Phrase("Packing List Wholesale", title1));
            header.Colspan = 2;
            header.Border = 0;
            header.PaddingBottom = 20f;
            header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(header);
            PdfPCell call3 = new PdfPCell(new Phrase(new Chunk("PT.Sembilan Ohm Sembilan", regular)));
            call3.BorderWidth = 0;
            call3.BorderWidthTop = 1;
            call3.BorderWidthLeft = 1;
            call3.BorderWidthRight = 1;
            call3.BorderWidthBottom = 0;
            table.AddCell(call3);
            //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
            PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Kepada, Yth " + show.SHOWROOM, regular)));
            call1.BorderWidthTop = 1;
            call1.BorderWidthLeft = 1;
            call1.BorderWidthRight = 1;
            call1.BorderWidthBottom = 0;
            table.AddCell(call1);
            //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

            //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
            //PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Jl Ciniru iV no 16", regular)));
            PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Wisma Nugra Santana Lantai 7", regular)));
            call5.BorderWidthTop = 0;
            call5.BorderWidthLeft = 1;
            call5.BorderWidthRight = 1;
            call5.BorderWidthBottom = 0;
            table.AddCell(call5);

            PdfPCell call2 = new PdfPCell(new Phrase(new Chunk("", regular)));
            //PdfPCell call2 = new PdfPCell(new Phrase(new Chunk("Jl. Jend. Sudirman Kav 7-8 Jakarta Pusat 10220", regular)));
            call2.BorderWidthTop = 0;
            call2.BorderWidthLeft = 1;
            call2.BorderWidthRight = 1;
            call2.BorderWidthBottom = 0;
            table.AddCell(call2);
            //table.AddCell(new Phrase(new Chunk("VENUE OF MEETING : " + Lbl_Venue.Text + "", regular)));

            PdfPCell callAddr = new PdfPCell(new Phrase(new Chunk("Jl. Jend. Sudirman Kav 7-8 Jakarta Pusat 10220", regular)));
            callAddr.BorderWidthTop = 0;
            callAddr.BorderWidthLeft = 1;
            callAddr.BorderWidthRight = 1;
            callAddr.BorderWidthBottom = 0;
            table.AddCell(callAddr);

            PdfPCell callAddr2 = new PdfPCell(new Phrase(new Chunk("", regular)));
            //PdfPCell call2 = new PdfPCell(new Phrase(new Chunk("Jl. Jend. Sudirman Kav 7-8 Jakarta Pusat 10220", regular)));
            callAddr2.BorderWidthTop = 0;
            callAddr2.BorderWidthLeft = 1;
            callAddr2.BorderWidthRight = 1;
            callAddr2.BorderWidthBottom = 0;
            table.AddCell(callAddr2);


            //  PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124", regular)));
            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-5704777", regular)));

            callCIF.BorderWidthTop = 0;
            callCIF.BorderWidthLeft = 1;
            callCIF.BorderWidthRight = 1;
            callCIF.BorderWidthBottom = 0;
            table.AddCell(callCIF);

            //table.AddCell(new Phrase(new Chunk("INDUSTRY : " + Lbl_Industry.Text + "", regular)));
            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("", regular)));
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

            PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("", regular)));
            callAcNo.BorderWidthTop = 0;
            callAcNo.BorderWidthLeft = 1;
            callAcNo.BorderWidthRight = 1;
            callAcNo.BorderWidthBottom = 0;
            table.AddCell(callAcNo);

            PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("", regular)));
            callCreatedBy.BorderWidthTop = 0;
            callCreatedBy.BorderWidthLeft = 1;
            callCreatedBy.BorderWidthRight = 1;
            callCreatedBy.BorderWidthBottom = 0;
            table.AddCell(callCreatedBy);

            //table.AddCell(new Phrase(new Chunk("GCIF : " + Lbl_Gcif.Text + "", regular)));
            PdfPCell callIndustry = new PdfPCell(new Phrase(new Chunk("", regular)));
            callIndustry.BorderWidthTop = 0;
            callIndustry.BorderWidthLeft = 1;
            callIndustry.BorderWidthRight = 1;
            callIndustry.BorderWidthBottom = 0;
            table.AddCell(callIndustry);

            PdfPCell callRelOfficer = new PdfPCell(new Phrase(new Chunk("", regular)));
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
            PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : " + noBukti, regular)));
            bottom1.Colspan = 2;
            bottom1.PaddingBottom = 20f;
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            table.AddCell(bottom1);
            pdfDoc.Add(table);

            string gridHtml = sw.ToString().Replace("class=\"viewdata\"", "");
            gridHtml = gridHtml.Replace("style=\"border-collapse:collapse;\"", "style=\"border-collapse:collapse;font-size:1;font-family:Times-Roman,serif,georgia;\"");
            StringReader sr = new StringReader(gridHtml);
            /*Show DataGrid*/
            htmlparser.Parse(sr);

            //Masukin Table

            PdfPTable table4 = new PdfPTable(7);
            float[] width4 = new float[] { 0.2f, 1f, 1f, 1f, 0.5f, 0.4f, 0.4f };
            table4.TotalWidth = 575f;
            table4.LockedWidth = true;
            table4.SetWidths(width4);
            table4.HorizontalAlignment = 0;
            table4.SpacingBefore = 0f;
            table4.SpacingAfter = 20f;

            PdfPCell no = new PdfPCell(new Phrase(new Chunk("NO", regular)));
            no.BorderWidth = 1;
            no.HorizontalAlignment = 1;
            table4.AddCell(no);
            PdfPCell barcode = new PdfPCell(new Phrase(new Chunk("BARCODE", regular)));
            barcode.BorderWidth = 1;
            barcode.HorizontalAlignment = 1;
            table4.AddCell(barcode);
            PdfPCell itemCode = new PdfPCell(new Phrase(new Chunk("ITEM CODE", regular)));
            itemCode.BorderWidth = 1;
            itemCode.HorizontalAlignment = 1;
            table4.AddCell(itemCode);
            PdfPCell art = new PdfPCell(new Phrase(new Chunk("ARTICLE", regular)));
            art.BorderWidth = 1;
            art.HorizontalAlignment = 1;
            table4.AddCell(art);
            PdfPCell col = new PdfPCell(new Phrase(new Chunk("COLOR", regular)));
            col.BorderWidth = 1;
            col.HorizontalAlignment = 1;
            table4.AddCell(col);
            PdfPCell size = new PdfPCell(new Phrase(new Chunk("SIZE", regular)));
            size.BorderWidth = 1;
            size.HorizontalAlignment = 1;
            table4.AddCell(size);
            PdfPCell qty = new PdfPCell(new Phrase(new Chunk("QTY", regular)));
            qty.BorderWidth = 1;
            qty.HorizontalAlignment = 1;
            table4.AddCell(qty);

            int Total = 0;
            for (int i = 0; i < listPutus.Count; i++)
            {
                iTextSharp.text.Font font = regular;

                PdfPCell cell1 = new PdfPCell(new Phrase(Convert.ToString(i + 1), font));
                cell1.BorderWidth = 1;
                cell1.HorizontalAlignment = 1;
                table4.AddCell(cell1);
                PdfPCell cellBarcode = new PdfPCell(new Phrase(listPutus[i].FBARCODE, font));
                cellBarcode.BorderWidth = 1;
                cellBarcode.HorizontalAlignment = 0;
                table4.AddCell(cellBarcode);
                PdfPCell cell2 = new PdfPCell(new Phrase(listPutus[i].FITEM_CODE, font));
                cell2.BorderWidth = 1;
                cell2.HorizontalAlignment = 0;
                table4.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase(listPutus[i].FART_DESC, font));
                cell3.BorderWidth = 1;
                cell3.HorizontalAlignment = 0;
                table4.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase(listPutus[i].FCOL_DESC, font));
                cell4.BorderWidth = 1;
                cell4.HorizontalAlignment = 0;
                table4.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase(listPutus[i].FSIZE_DESC, font));
                cell5.BorderWidth = 1;
                cell5.HorizontalAlignment = 0;
                table4.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase(listPutus[i].QTY_ACTUAL.ToString(), font));
                cell6.BorderWidth = 1;
                cell6.HorizontalAlignment = 0;
                table4.AddCell(cell6);

                Total = Total + listPutus[i].QTY_ACTUAL;
            }

            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 6;
            cellNamaTotalQty.BorderWidth = 1;
            cellNamaTotalQty.HorizontalAlignment = 0;
            table4.AddCell(cellNamaTotalQty);

            PdfPCell cellTotalQty = new PdfPCell(new Phrase(Total.ToString(), regular));
            cellTotalQty.BorderWidth = 1;
            cellTotalQty.HorizontalAlignment = 0;
            table4.AddCell(cellTotalQty);

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

        #endregion
        protected void btnInsToShPutus_Click(object sender, EventArgs e)
        {
            ModalPopupSoToShPutusSrch.Show();
            bindGridSO();
        }

        protected void gvBON_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBON.PageIndex = e.NewPageIndex;
            bindAllWholeSale();
        }

        protected void gvBON_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvBON.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        hdnIReturIDHeader.Value = id;
                        tbIReturNoBon.Text = gvBON.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[4].Text;
                        tbIReturStore.Text = gvBON.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[3].Text;

                        tbIReturTglTrans.Text = gvBON.Rows[rowIndex].Cells[5].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[5].Text;
                        tbIReturTglKirim.Text = gvBON.Rows[rowIndex].Cells[12].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[12].Text;
                        tbIReturStatus.Text = gvBON.Rows[rowIndex].Cells[11].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[11].Text;
                        tbIReturKode.Text = gvBON.Rows[rowIndex].Cells[2].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[2].Text;
                        //tbIReturMargin.Text = gvBON.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[7].Text;
                        tbIReturRetur.Text = gvBON.Rows[rowIndex].Cells[13].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[13].Text;
                        lblnoordSO.Text = gvBON.Rows[rowIndex].Cells[14].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[14].Text;
                        lblNoScanOrd.Text = gvBON.Rows[rowIndex].Cells[15].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[15].Text;
                        bindItem();
                    }
                    else if (e.CommandName.ToLower() == "printrow")
                    {
                        string noBukti = gvBON.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[4].Text;
                        hdnIReturIDHeader.Value = id;
                        SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                        List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
                        string where = string.Format(" where a.ID_BAYAR like {0}", hdnIReturIDHeader.Value);

                        //putusDetailList = bayarDA.getSHPutusDetail(where);
                        string tableName = "SH_PUTUS_DETAIL";
                        string selectTable = "a.ID, a.ID_BAYAR, a.ID_KDBRG, a.KODE_CUST, a.KODE, a.TGL_TRANS, a.NO_BON, a.ITEM_CODE, " +
                                " a.TAG_PRICE, a.BON_PRICE, a.NILAI_BYR, a.MARGIN, a.QTY, a.JUAL_RETUR, a.QTY_ACTUAL, a.FSTATUS, a.FRETUR, ";
                        putusDetailList = bayarDA.getSHPutusDetailWithSP(tableName, selectTable, where);

                        MS_SHOWROOM show = new MS_SHOWROOM();
                        show.SHOWROOM = gvBON.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvBON.Rows[rowIndex].Cells[3].Text; //tbIReturStore.Text;

                        printNoBukti(noBukti, putusDetailList, show);
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

        protected void btnBONSearch_Click(object sender, EventArgs e)
        {
            bindAllWholeSale();
        }

        protected void btnCloseSO_Click(object sender, EventArgs e)
        {
            bindGridCloseSO();
            ModalPopupCloseSO.Show();
        }

        protected void gvDetailSO_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDetailSO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailSO.PageIndex = e.NewPageIndex;
            bindGridSODetail();
        }

        protected void btnSearchSo_Click(object sender, EventArgs e)
        {
            bindGridSO();
            ModalPopupSoToShPutusSrch.Show();
        }

        protected void btnSearchSoCancel_Click(object sender, EventArgs e)
        {
            ModalPopupSoToShPutusSrch.Hide();
        }

        protected void gvSo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    Int64 IdSo = Convert.ToInt64(gvSo.DataKeys[rowIndex]["ID"].ToString());

                    if (e.CommandName.ToLower() == "saverow")
                    {
                        string NoScan = gvSo.Rows[rowIndex].Cells[3].Text;
                        string NoSo = gvSo.Rows[rowIndex].Cells[2].Text;
                        SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
                        if (NoScan.Contains("2"))
                        {
                            string cek = tstDA.CekSoInSHPutus("WHERE NO_SO = '" + NoSo + "'");
                            //int cekcount = Convert.ToInt32(cek);
                            if (cek != "")//(cekcount > 0)
                            {
                                List<SO_WHOLESALES_HEADER> ListHeaderSO = new List<SO_WHOLESALES_HEADER>();
                                string where = string.Format("WHERE NO_SO = '" + NoSo + "'");
                                ListHeaderSO = tstDA.getSoHeader(where);
                                lblnopo.Text = ListHeaderSO.FirstOrDefault().NO_PO;
                                lblnoso.Text = ListHeaderSO.FirstOrDefault().NO_SO;
                                lblKodeCust.Text = ListHeaderSO.FirstOrDefault().KODE_CUST;
                                lblKode.Text = ListHeaderSO.FirstOrDefault().KODE;
                                lblTglTrans.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().TGL_TRANS);
                                lblTglKirim.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().SEND_DATE);
                                lblMargin.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().MARGIN);
                                lblFRetur.Text = ListHeaderSO.FirstOrDefault().FRETUR;
                                lblIdHeader.Text = Convert.ToString(IdSo);
                                lblNoScan.Text = NoScan;
                                bindGridSODetail();
                                Dsearch.Visible = false;
                                dShPutus.Visible = true;
                            }
                            else
                            {
                                dMsgSO.InnerText = "Proses No Scan X01 Terlebih Dahulu!";
                                dMsgSO.Attributes["class"] = "error";
                                dMsgSO.Visible = true;
                            }

                            ModalPopupSoToShPutusSrch.Show();
                        }
                        else
                        {
                            List<SO_WHOLESALES_HEADER> ListHeaderSO = new List<SO_WHOLESALES_HEADER>();
                            string where = string.Format("WHERE NO_SO = '" + NoSo + "'");
                            ListHeaderSO = tstDA.getSoHeader(where);
                            lblnopo.Text = ListHeaderSO.FirstOrDefault().NO_PO;
                            lblnoso.Text = ListHeaderSO.FirstOrDefault().NO_SO;
                            lblKodeCust.Text = ListHeaderSO.FirstOrDefault().KODE_CUST;
                            lblKode.Text = ListHeaderSO.FirstOrDefault().KODE;
                            lblTglTrans.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().TGL_TRANS);
                            lblTglKirim.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().SEND_DATE);
                            lblMargin.Text = Convert.ToString(ListHeaderSO.FirstOrDefault().MARGIN);
                            lblFRetur.Text = ListHeaderSO.FirstOrDefault().FRETUR;
                            lblNoScan.Text = NoScan;
                            lblIdHeader.Text = Convert.ToString(IdSo);
                            Dsearch.Visible = false;
                            dShPutus.Visible = true;
                            bindGridSODetail();
                            ModalPopupSoToShPutusSrch.Show();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                dMsgSO.InnerText = "Error : " + ex.Message;
                dMsgSO.Attributes["class"] = "error";
                dMsgSO.Visible = true;
            }
        }

        protected void gvSo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string langId2 = DataBinder.Eval(e.Row.DataItem, "TGL_KIRIM_1").ToString();
                DateTime dtlang = Convert.ToDateTime(langId2);
                string dtcompare = dtlang.ToShortDateString();
                //if (dtcompare != "01/01/0001")
                //{
                //    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgSearchSave");
                //    imgBtn.Visible = false;
                //}

                string langIdStat = DataBinder.Eval(e.Row.DataItem, "STATUS_HEADER").ToString();

                if (langIdStat == "DONE")
                {
                    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgSearchSave");
                    imgBtn.Visible = false;
                }
            }
        }

        protected void gvSo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSo.PageIndex = e.NewPageIndex;
            bindGridSO();
            ModalPopupSoToShPutusSrch.Show();
        }

        protected void btnInsShPutus_Click(object sender, EventArgs e)
        {
            Decimal totalPrice = 0;
            int totalQty = 0;
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            SH_PUTUS_HEADER putusHeader = new SH_PUTUS_HEADER();
            string user = Session["UName"] == null ? "" : Session["UName"].ToString();
            gvDetailSO.AllowPaging = false;
            SO_SHOWLESALES_DA wholesaleDa = new SO_SHOWLESALES_DA();
            //Bug
            //Tambahkan GridviewRow.Paging = false;
            //After hitung tambahkan GridViewRow.Paging - true;

            if (lblNoScan.Text.Contains("1"))
            {
                foreach (GridViewRow item in gvDetailSO.Rows)
                {
                    totalPrice = totalPrice + (Convert.ToDecimal(item.Cells[4].Text) * Convert.ToInt32(item.Cells[5].Text));
                    totalQty = totalQty + Convert.ToInt32(item.Cells[5].Text);
                }
            }
            else
            {
                foreach (GridViewRow item in gvDetailSO.Rows)
                {
                    totalPrice = totalPrice + (Convert.ToDecimal(item.Cells[4].Text) * Convert.ToInt32(item.Cells[8].Text));
                    totalQty = totalQty + Convert.ToInt32(item.Cells[8].Text);
                }
            }
            decimal totalPenjualan = Convert.ToDecimal(totalPrice * Convert.ToDecimal(Convert.ToDecimal(100 - Convert.ToDecimal(lblMargin.Text)) / 100));
            #region "Insert SH_PUTUS_HEADER"
            putusHeader.KODE_CUST = lblKodeCust.Text;
            putusHeader.KODE = lblKode.Text;
            putusHeader.STATUS_STORE = "WHOLESALE";
            putusHeader.QTY = totalQty;
            putusHeader.CREATED_BY = user;
            putusHeader.NO_SO = lblnoso.Text;
            putusHeader.NO_SCAN = lblNoScan.Text;
            string idHeader = bayarDA.insertPutusHeaderRetIDNEW(putusHeader);
            #endregion
            if (!(idHeader.Contains("ERROR")))
            {
                #region "Insert To SH_PUTUS_DETAIL"
                SH_PUTUS_DETAIL putusDetail = new SH_PUTUS_DETAIL();
                string id = idHeader.Length > 5 ? idHeader.Remove(0, idHeader.Length - 5) : idHeader.PadLeft(5, '0');
                DateTime dt = DateTime.Now;
                string tgl = string.Format("{0:yyddMM}", dt);
                string noBon = tgl + id;
                //int margin = int.Parse(lblMargin.Text);
                decimal margin = Convert.ToDecimal(lblMargin.Text);
                putusDetail.ID_BAYAR = Convert.ToInt64(idHeader);
                putusDetail.KODE_CUST = lblKodeCust.Text;
                putusDetail.KODE = lblKode.Text;
                putusDetail.NO_BON = "SO" + noBon;
                //putusDetail.MARGIN = int.Parse(lblMargin.Text);
                putusDetail.MARGIN = Convert.ToDecimal(lblMargin.Text);

                putusDetail.FRETUR = lblFRetur.Text;
                putusDetail.ID_HEADER_SO = Convert.ToInt64(lblIdHeader.Text);
                bayarDA.insertPutusDetailFromSO(putusDetail, user, lblnoso.Text); //penambahan insert putus detail #07082024 - Felix
                //bayarDA.insertPutusDetailFromSO(putusDetail, user);
                #endregion
                //decimal totalPenjualan = Convert.ToDecimal(totalPrice * Convert.ToDecimal(Convert.ToDecimal(100 - margin) / 100));

                DateTime tglTrans = Convert.ToDateTime(lblTglTrans.Text); //DateTime.Now;
                string date = lblTglTrans.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                }

                DateTime tglKirim = DateTime.Now;
                string dateKirim = tglKirim.ToString();
                if (!string.IsNullOrEmpty(dateKirim))
                {
                    DateTime.TryParseExact(dateKirim, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglKirim);
                }
                #region "update OR Insert MS_Stock"
                //foreach (GridViewRow item in gvDetailSO.Rows)
                //{
                //    int qtyact = 0;
                //    if (lblNoScan.Text.Contains("1"))
                //    {
                //        qtyact = Convert.ToInt32(item.Cells[5].Text);
                //    }
                //    else
                //    {
                //        qtyact = Convert.ToInt32(item.Cells[8].Text);
                //    }
                //    SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
                //    SH_PUTUS_DETAIL putusD = new SH_PUTUS_DETAIL();
                //    //string iddetail = gvIRetur.DataKeys[item.RowIndex].Value.ToString();
                //    putusD.ID = tstDA.getIDSHPutusDetail(" WHERE BARCODE = '" + item.Cells[3].Text.Trim().ToString() + "' AND ID_BAYAR = " + Convert.ToInt64(idHeader));//Convert.ToInt64(iddetail);
                //    putusD.QTY_ACTUAL = qtyact;
                //    putusD.FSTATUS = "Done";

                //    bayarDA.updateQTYPutusDetail(putusD);

                //    //GET STOCK ITEM
                //    List<MS_STOCK> listStock = new List<MS_STOCK>();
                //    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                //    MS_STOCK stock = new MS_STOCK();
                //    stock.KODE = "WARE-001";
                //    listStock = stockDA.getStock(string.Format(" where LTRIM(RTRIM(BARCODE)) = '{0}' and KODE = '{1}'", item.Cells[3].Text.Trim().ToString(), stock.KODE));
                //    if (listStock.Count > 0)
                //    {
                //        stock = listStock.First();
                //    }
                //    stock.ITEM_CODE = item.Cells[11].Text.Trim().ToString();
                //    stock.BARCODE = item.Cells[3].Text.Trim().ToString();
                //    stock.WAREHOUSE = "Main Warehouse";
                //    stock.KODE = "WARE-001";
                //    stock.STOCK = lblFRetur.Text.ToLower().Trim() == "no" ? qtyact * -1 : qtyact;
                //    stock.RAK = "000";
                //    stock.CREATED_BY = Session["UName"].ToString();
                //    stock.UPDATED_BY = Session["UName"].ToString();

                //    bool statusStock = listStock.Count == 0 ? insertStock(stock) : updateStock(stock);
                //}
                #endregion
                #region "Update SH_PUTUS_HEADER"
                putusHeader.TGL_TRANS = Convert.ToDateTime(date);//DateTime.Now;//tglTrans;
                putusHeader.NO_BON = "SO" + noBon;
                putusHeader.NET_CASH = totalPenjualan;
                putusHeader.JM_UANG = totalPenjualan;
                putusHeader.KEMBALI = 0;
                putusHeader.NET_BAYAR = totalPenjualan;
                putusHeader.ID = Convert.ToInt64(idHeader);
                putusHeader.STATUS_HEADER = "delivered";//"prepare";
                putusHeader.SEND_DATE = DateTime.Now; //tglKirim;
                //putusHeader.MARGIN = Convert.ToInt32(lblMargin.Text);
                putusHeader.MARGIN = Convert.ToDecimal(lblMargin.Text);

                putusHeader.FRETUR = lblFRetur.Text;

                bayarDA.updatePutusHeaderNEW(putusHeader);
                #endregion

                dShPutus.Visible = false;
                Dsearch.Visible = true;
                lbDONEBON.Text = "SO" + noBon;
                lblDONEChange.Text = string.Format("{0:0,0.00}", Convert.ToDouble(totalPenjualan));

                ModalChange.Show();
            }
            else
            {
                dMsgSO.InnerText = idHeader;
                dMsgSO.Attributes["class"] = "error";
                dMsgSO.Visible = true;
                ModalChange.Show();
            }
            //dShPutus.Visible = false;
            //Dsearch.Visible = true;
            ////lbDONEBON.Text = "SO" + noBon;
            //lblDONEChange.Text = string.Format("{0:0,0.00}", Convert.ToDouble(totalPenjualan));

            ModalChange.Show();
            gvDetailSO.AllowPaging = true;
        }

        protected void btnInsShPutusCancel_Click(object sender, EventArgs e)
        {
            dShPutus.Visible = false;
            Dsearch.Visible = true;
            ModalPopupSoToShPutusSrch.Show();
        }

        protected void bDONEClose_Click(object sender, EventArgs e)
        {
            DivMessage.InnerText = "Pembuatan Sales Order Berhasil!";
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true;
        }

        protected void btnSearchCloseSO_Click(object sender, EventArgs e)
        {
            bindGridCloseSO();
            ModalPopupCloseSO.Show();
        }

        protected void btnSearchCloseSOCancel_Click(object sender, EventArgs e)
        {
            ModalPopupCloseSO.Hide();
        }

        protected void gvCloseSo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCloseSo.PageIndex = e.NewPageIndex;
            bindGridCloseSO();
            ModalPopupCloseSO.Show();
        }

        protected void gvCloseSo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string langId2 = DataBinder.Eval(e.Row.DataItem, "STATUS_HEADER").ToString();

                if (langId2 == "DONE")
                {
                    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgSearchSave");
                    imgBtn.Visible = false;
                }
            }
        }

        protected void gvCloseSo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    String noso = gvCloseSo.DataKeys[rowIndex]["NO_SO"].ToString();

                    if (e.CommandName.ToLower() == "closeso")
                    {
                        SO_SHOWLESALES_DA tstDa = new SO_SHOWLESALES_DA();
                        //tstDa.UpdateSoWholesalesHeaderStatus(noso);
                        bindGridCloseSO();
                        ModalPopupCloseSO.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Div2.InnerText = "Error : " + ex.Message;
                Div2.Attributes["class"] = "error";
                Div2.Visible = true;
            }

        }

        protected void btnIReturPrintDeliveryOrder_Click(object sender, EventArgs e)
        {
            string noBukti = tbIReturNoBon.Text;

            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
            string where = string.Format(" where a.ID_BAYAR like {0}", hdnIReturIDHeader.Value);

            //putusDetailList = bayarDA.getSHPutusDetail(where);
            string tableName = "SH_PUTUS_DETAIL";
            string selectTable = "a.ID, a.ID_BAYAR, a.ID_KDBRG, a.KODE_CUST, a.KODE, a.TGL_TRANS, a.NO_BON, a.ITEM_CODE, " +
                    " a.TAG_PRICE, a.BON_PRICE, a.NILAI_BYR, a.MARGIN, a.QTY, a.JUAL_RETUR, a.QTY_ACTUAL, a.FSTATUS, a.FRETUR, ";
            putusDetailList = bayarDA.getSHPutusDetailWithSP(tableName, selectTable, where);

            MS_SHOWROOM show = new MS_SHOWROOM();
            show.SHOWROOM = tbIReturStore.Text;

            printDeliveryNotes(noBukti, putusDetailList, show);
        }

        protected void btnIReturSave_Click(object sender, EventArgs e)
        {
            //if (checkQTY())
            //{
            LOGIN_DA loginDA = new LOGIN_DA();
            DateTime tgltrns = Convert.ToDateTime(tbIReturTglTrans.Text);
            DateTime tglSenChange = Convert.ToDateTime(tbIReturTglKirim.Text);
            string whereLock = " where NAME = 'lockHO'";
            int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
            int dateSend = Convert.ToInt32(string.Format("{0:yyMM}", tglSenChange));

            if (dateSend <= lockMove)
            {
                DivMessage.InnerText = "Bulan telah Di LOCK!";//"Pengiriman barang penjualan wholesale berhasil!";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            else
            {
                if (tglSenChange < tgltrns)
                {
                    DivMessage.InnerText = "Tanggal pengiriman tidak boleh lebih kecil dari tanggal transaksi!";//"Pengiriman barang penjualan wholesale berhasil!";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                else
                {
                    SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                    SH_PUTUS_HEADER putusHeader = new SH_PUTUS_HEADER();

                    DateTime tglSend = DateTime.Now;
                    string date = tbIReturTglKirim.Text.ToString();
                    if (!string.IsNullOrEmpty(date))
                    {
                        DateTime.TryParseExact(date, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out tglSend);
                    }
                    DateTime tglTrans = DateTime.Now;
                    string dateTrans = tbIReturTglTrans.Text.ToString();
                    if (!string.IsNullOrEmpty(dateTrans))
                    {
                        DateTime.TryParseExact(dateTrans, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                    }
                    putusHeader.ID = Convert.ToInt64(hdnIReturIDHeader.Value);
                    putusHeader.SEND_DATE = tglSend;
                    putusHeader.STATUS_HEADER = "delivered";
                    putusHeader.TGL_TRANS = tglTrans;//tgltrns;

                    bayarDA.updateStatusPutusHeader(putusHeader);
                    if (lblnoordSO.Text != "" && lblNoScanOrd.Text != "")
                    {
                        bayarDA.updateTglTransSOWHOLESALE(putusHeader, lblnoordSO.Text);
                        if (lblNoScanOrd.Text.ToLower() == "x01")
                        {
                            bayarDA.updateTglKirim_1SOWHOLESALE(putusHeader, lblnoordSO.Text);
                        }
                        else
                        {
                            bayarDA.updateTglKirim_2SOWHOLESALE(putusHeader, lblnoordSO.Text);
                        }
                    }
                    bindAllWholeSale();
                    //updateStockPrepare();
                    //bindGrid();
                    //clearGrid();
                    DivMessage.InnerText = "Perubahan Send Date Berhasil";//"Pengiriman barang penjualan wholesale berhasil!";
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            //}
        }

        protected void btnVoid_Click(object sender, EventArgs e)
        {
            //Check NoBOn
            string noBon = tbIReturNoBon.Text;

            //Insert ulang semua tapi stock nya minus
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            string berhasil = stockDA.insertVoidWholesale(noBon);

            if (!berhasil.Contains("ERROR"))
            {
                //Update Stock
                stockDA.updateStockVoidWholesale(noBon);

                DivMessage.InnerText = "Void Berhasil";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            else
            {
                DivMessage.InnerText = "Void " + berhasil;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;

            }
        }

        protected void gvIRetur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIRetur.PageIndex = e.NewPageIndex;
            bindItemWholeSale();
        }

        protected void gvIRetur_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton imgCommand = (ImageButton)e.Row.FindControl("imgCommand");
                TextBox tbIReturQtyActual = (TextBox)e.Row.FindControl("tbIReturQtyActual");

                if (e.Row.Cells[20].Text.Trim().ToLower() == "done")
                {
                    tbIReturQtyActual.Text = e.Row.Cells[21].Text;
                    tbIReturQtyActual.Enabled = false;
                }
            }
        }

        protected void gvIRetur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvIRetur.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        //Insert ke Temp Retur retur
                        inputIntoTempStruckRetur();
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

        protected void bIReturClose_Click(object sender, EventArgs e)
        {
            bindAllWholeSale();
        }

    }
}