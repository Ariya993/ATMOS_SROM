using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace ATMOS_SROM
{
    public partial class PopUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string noBukti = Request["noBukti"].ToString();
            List<TRF_STOCK_DETAIL> listStock = getListStock();
            MS_SHOWROOM show = getShowRoom();

            printNoBukti(noBukti, listStock, show);
            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        protected List<TRF_STOCK_DETAIL> getListStock()
        {
            string idHeader = Session["PDFIdHeader"].ToString();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();

            listStock = stockDA.getDetailTrfStock(" where ID_HEADER = '" + idHeader + "' ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC");

            return listStock;
        }

        protected MS_SHOWROOM getShowRoom()
        {
            string kode = Session["PDFKode"].ToString();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            MS_SHOWROOM show = new MS_SHOWROOM();

            show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", kode)).FirstOrDefault();

            return show;
        }

        protected void printNoBukti(string noBukti, List<TRF_STOCK_DETAIL> listStock, MS_SHOWROOM show)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Packing_List_PL" + noBukti.Remove(0, 2) + ".pdf");
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
            PdfPCell header = new PdfPCell(new Phrase("Packing List", title1));
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
            PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Jl Raya Bekasi km 6,Narogong", regular)));
             
            call5.BorderWidthTop = 0;
            call5.BorderWidthLeft = 1;
            call5.BorderWidthRight = 1;
            call5.BorderWidthBottom = 0;
            table.AddCell(call5);

            PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT, regular)));
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
            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk(show.PHONE, regular)));
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
            PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : PL" + noBukti.Remove(0, 2), regular)));
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
            /*
            StyleSheet st = new StyleSheet();
            st.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTSIZE, "10");
            st.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTFAMILY, BaseFont.TIMES_ROMAN);
             * */
            /*End Create DataGrid*/

            //Create table
            /*
            PdfPTable table3 = new PdfPTable(2);
            float[] widths3 = new float[] { 1f, 1f };
            table3.TotalWidth = 575f;
            table3.LockedWidth = true;
            table3.SetWidths(widths3);
            table3.HorizontalAlignment = 0;
            table3.SpacingBefore = 20f;
            table3.SpacingAfter = 20f;

            PdfPCell bottom3 = new PdfPCell(new Phrase(new Chunk("FINANCIAL UPDATE", regular)));
            bottom3.PaddingBottom = 3f;
            bottom3.PaddingTop = 3f;
            bottom3.Colspan = 2;
            bottom3.Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom3.BorderWidth = 1;
            table3.AddCell(bottom3);
            PdfPCell bottom35 = new PdfPCell(new Phrase(new Chunk("", regular)));
            bottom35.PaddingBottom = 20f;
            bottom35.PaddingTop = 2f;
            bottom35.Colspan = 2;
            bottom35.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom35.BorderWidth = 1;
            table3.AddCell(bottom35);
            PdfPCell bottom4 = new PdfPCell(new Phrase(new Chunk("LAST COMPANY PERFORMANCE", regular)));
            bottom4.PaddingBottom = 3f;
            bottom4.PaddingTop = 3f;
            bottom4.Colspan = 2;
            bottom4.Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom4.BorderWidth = 1;
            table3.AddCell(bottom4);
            PdfPCell bottom45 = new PdfPCell(new Phrase(new Chunk("", regular)));
            bottom45.PaddingBottom = 20f;
            bottom45.PaddingTop = 2f;
            bottom45.Colspan = 2;
            bottom45.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom45.BorderWidth = 1;
            table3.AddCell(bottom45);
            table3.AddCell(black);
            PdfPCell bottom5 = new PdfPCell(new Phrase(new Chunk("DESCRIPTION OF MEETING ", regular)));
            bottom5.PaddingBottom = 3f;
            bottom5.PaddingTop = 3f;
            bottom5.Colspan = 2;
            bottom5.Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom5.BorderWidth = 1;
            table3.AddCell(bottom5);
            PdfPCell bottom55 = new PdfPCell(new Phrase(new Chunk("", regular)));
            bottom55.PaddingBottom = 20f;
            bottom55.PaddingTop = 2f;
            bottom55.Colspan = 2;
            bottom55.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom55.BorderWidth = 1;
            table3.AddCell(bottom55);
            PdfPCell bottom6 = new PdfPCell(new Phrase(new Chunk("EVALUATION / CONCLUSION", regular)));
            bottom6.PaddingBottom = 3f;
            bottom6.PaddingTop = 3f;
            bottom6.Colspan = 2;
            bottom6.Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom6.BorderWidth = 1;
            table3.AddCell(bottom6);
            PdfPCell bottom65 = new PdfPCell(new Phrase(new Chunk("", regular)));
            bottom65.PaddingBottom = 20f;
            bottom65.PaddingTop = 2f;
            bottom65.Colspan = 2;
            bottom65.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom65.BorderWidth = 1;
            table3.AddCell(bottom65);
            PdfPCell bottom7 = new PdfPCell(new Phrase(new Chunk("ACTION TO BE TAKEN", regular)));
            bottom7.PaddingBottom = 3f;
            bottom7.PaddingTop = 3f;
            bottom7.Colspan = 2;
            bottom7.Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            bottom7.BorderWidth = 1;
            table3.AddCell(bottom7);
            PdfPCell bottom75 = new PdfPCell(new Phrase(new Chunk("", regular)));
            bottom75.PaddingBottom = 20f;
            bottom75.PaddingTop = 2f;
            bottom75.Colspan = 2;
            bottom75.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            bottom75.BorderWidth = 1;
            table3.AddCell(bottom75);
            //PdfPCell bottom8 = new PdfPCell(new Phrase(new Chunk("COMMENT : ", regular)));
            //bottom8.PaddingBottom = 3f;
            //bottom8.PaddingTop = 3f;
            //bottom8.Colspan = 2;
            //bottom8.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //bottom8.BorderWidth = 1;
            //table3.AddCell(bottom8);
            pdfDoc.Add(table3);
            */
            //Masukin Table

            PdfPTable table4 = new PdfPTable(6);
            float[] width4 = new float[] { 0.3f, 1.2f, 1.2f, 0.8f, 0.5f, 0.5f };
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
            for (int i = 0; i < listStock.Count; i++)
            {
                iTextSharp.text.Font font = regular;

                PdfPCell cell1 = new PdfPCell(new Phrase(Convert.ToString(i + 1), font));
                cell1.BorderWidth = 1;
                cell1.HorizontalAlignment = 1;
                table4.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase(listStock[i].ITEM_CODE, font));
                cell2.BorderWidth = 1;
                cell2.HorizontalAlignment = 0;
                table4.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase(listStock[i].FART_DESC, font));
                cell3.BorderWidth = 1;
                cell3.HorizontalAlignment = 0;
                table4.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase(listStock[i].FCOL_DESC, font));
                cell4.BorderWidth = 1;
                cell4.HorizontalAlignment = 0;
                table4.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase(listStock[i].FSIZE_DESC, font));
                cell5.BorderWidth = 1;
                cell5.HorizontalAlignment = 0;
                table4.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase(listStock[i].QTY_KIRIM.ToString(), font));
                cell6.BorderWidth = 1;
                cell6.HorizontalAlignment = 0;
                table4.AddCell(cell6);

                Total = Total + listStock[i].QTY_KIRIM;
            }

            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 5;
            cellNamaTotalQty.BorderWidth = 1;
            cellNamaTotalQty.HorizontalAlignment = 1;
            table4.AddCell(cellNamaTotalQty);

            PdfPCell cellTotalQty = new PdfPCell(new Phrase(Total.ToString(), regular));
            cellTotalQty.BorderWidth = 1;
            cellTotalQty.HorizontalAlignment = 1;
            table4.AddCell(cellTotalQty);

            pdfDoc.Add(table4);


            /*Show Comment As Table*/
            StringWriter sw2 = new StringWriter();
            HtmlTextWriter hw2 = new HtmlTextWriter(sw2);

            gridHtml = sw2.ToString().Replace("class=\"viewdata\"", "");
            gridHtml = gridHtml.Replace("style=\"border-collapse:collapse;\"", "style=\"border-collapse:collapse;font-size:9;font-family:Times-Roman,serif,georgia;\"");
            StringReader sr2 = new StringReader(gridHtml);
            htmlparser.Parse(sr2);

            //Create new page
            //pdfDoc.NewPage();
            /*Create the second table in second page*/
            /*
            PdfPTable table2 = new PdfPTable(4);
            //table2.TotalWidth = 3000f;
            //table2.LockedWidth = true;
            float[] widths2 = new float[] { 0.5f, 0.5f, 0.5f, 0.5f };
            table2.SetWidths(widths2);
            table2.HorizontalAlignment = 0;
            table2.SpacingBefore = 10f;
            table2.SpacingAfter = 10f;

            PdfPCell header2 = new PdfPCell(new Phrase("PREPARED BY", title2));
            header2.BackgroundColor = new BaseColor(0, 0, 0);
            header2.BorderColor = new BaseColor(0, 0, 0);
            header2.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            header2.BorderWidth = 1;
            header2.Colspan = 4;
            header2.HorizontalAlignment = 1;
            header2.PaddingBottom = 5f;
            header2.PaddingTop = 5f;
            table2.AddCell(header2);

            PdfPCell createdate = new PdfPCell(new Phrase("Diserahkan Oleh,", regular));
            createdate.HorizontalAlignment = 1;
            createdate.PaddingBottom = 2f;
            createdate.PaddingTop = 3f;
            createdate.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            createdate.BorderWidth = 1;
            table2.AddCell(createdate);

            PdfPCell approvedate = new PdfPCell(new Phrase("Dikirim Oleh,", regular));
            approvedate.HorizontalAlignment = 1;
            approvedate.PaddingBottom = 1f;
            approvedate.PaddingTop = 3f;
            approvedate.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            approvedate.BorderWidth = 1;
            table2.AddCell(approvedate);

            PdfPCell createdate1 = new PdfPCell(new Phrase("Mengetahui,", regular));
            createdate1.HorizontalAlignment = 1;
            createdate1.PaddingBottom = 1f;
            createdate1.PaddingTop = 3f;
            createdate1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            createdate1.BorderWidth = 1;
            table2.AddCell(createdate1);

            PdfPCell approvedate1 = new PdfPCell(new Phrase("Diterima Oleh,", regular));
            approvedate1.HorizontalAlignment = 1;
            approvedate1.PaddingBottom = 2f;
            approvedate1.PaddingTop = 3f;
            approvedate1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            approvedate1.BorderWidth = 1;
            table2.AddCell(approvedate1);

            /*
            iTextSharp.text.Font signatureComment = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLUE);
            PdfPCell signCmt1 = new PdfPCell(new Phrase("[ This report does not required signature as it is generated by computer ]", signatureComment));
            signCmt1.HorizontalAlignment = 1;
            signCmt1.PaddingBottom = 6f;
            signCmt1.PaddingTop = 3f;
            signCmt1.Colspan = 2;
            signCmt1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            signCmt1.BorderWidth = 1;
            table2.AddCell(signCmt1);
            ****

            PdfPCell AO = new PdfPCell(new Phrase("", regular));
            AO.HorizontalAlignment = 1;
            AO.PaddingBottom = 40f;
            AO.PaddingTop = 5f;
            AO.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            AO.BorderWidth = 1;
            table2.AddCell(AO);

            PdfPCell spv = new PdfPCell(new Phrase("Bag. Expedisi", regular));
            spv.HorizontalAlignment = 1;
            spv.PaddingBottom = 40f;
            spv.PaddingTop = 5f;
            spv.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            spv.BorderWidth = 1;
            table2.AddCell(spv);

            PdfPCell AO1 = new PdfPCell(new Phrase("Ka. Gudang", regular));
            AO1.HorizontalAlignment = 1;
            AO1.PaddingBottom = 40f;
            AO1.PaddingTop = 5f;
            AO1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            AO1.BorderWidth = 1;
            table2.AddCell(AO1);

            PdfPCell spv1 = new PdfPCell(new Phrase("", regular));
            spv1.HorizontalAlignment = 1;
            spv1.PaddingBottom = 40f;
            spv1.PaddingTop = 5f;
            spv1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            spv1.BorderWidth = 1;
            table2.AddCell(spv1);

            PdfPCell empptyline = new PdfPCell(new Phrase("(_______________)", regular));
            empptyline.HorizontalAlignment = 1;
            empptyline.PaddingBottom = 2f;
            empptyline.PaddingTop = 3f;
            empptyline.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            empptyline.BorderWidth = 1;
            table2.AddCell(empptyline);

            PdfPCell lvl = new PdfPCell(new Phrase("(______________)", regular));
            lvl.HorizontalAlignment = 1;
            lvl.PaddingBottom = 2f;
            lvl.PaddingTop = 3f;
            lvl.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            lvl.BorderWidth = 1;
            table2.AddCell(lvl);

            PdfPCell empptyline1 = new PdfPCell(new Phrase("(_______________)", regular));
            empptyline1.HorizontalAlignment = 1;
            empptyline1.PaddingBottom = 2f;
            empptyline1.PaddingTop = 3f;
            empptyline1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            empptyline1.BorderWidth = 1;
            table2.AddCell(empptyline1);

            PdfPCell lvl1 = new PdfPCell(new Phrase("(______________)", regular));
            lvl1.HorizontalAlignment = 1;
            lvl1.PaddingBottom = 2f;
            lvl1.PaddingTop = 3f;
            lvl1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            lvl1.BorderWidth = 1;
            table2.AddCell(lvl1);

            PdfPCell empptyline2 = new PdfPCell(new Phrase("", regular));
            empptyline2.HorizontalAlignment = 1;
            empptyline2.PaddingBottom = 5f;
            empptyline2.PaddingTop = 3f;
            empptyline2.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            empptyline2.BorderWidth = 1;
            table2.AddCell(empptyline2);
            PdfPCell branchname = new PdfPCell(new Phrase("", regular));
            branchname.HorizontalAlignment = 1;
            branchname.PaddingBottom = 5f;
            branchname.PaddingTop = 3f;
            branchname.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            branchname.BorderWidth = 1;
            table2.AddCell(branchname); 
            PdfPCell empptyline21 = new PdfPCell(new Phrase("", regular));
            empptyline21.HorizontalAlignment = 1;
            empptyline21.PaddingBottom = 5f;
            empptyline21.PaddingTop = 3f;
            empptyline21.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            empptyline21.BorderWidth = 1;
            table2.AddCell(empptyline21);
            PdfPCell branchname1 = new PdfPCell(new Phrase("", regular));
            branchname1.HorizontalAlignment = 1;
            branchname1.PaddingBottom = 5f;
            branchname1.PaddingTop = 3f;
            branchname1.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            branchname.BorderWidth = 1;
            table2.AddCell(branchname1);


            pdfDoc.Add(table2);
            */
            /*Template Done*/

            pdfDoc.Close();
            Response.Write(pdfDoc);
            //Response.End();
            //Response.Close();
            Response.Flush(); Response.Close();
        }
    }
}