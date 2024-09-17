using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ATMOS_SROM.Sales
{
    public partial class PrintStruck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            MS_SHOWROOM show = new MS_SHOWROOM();

            string sKode = "SOSFSS04";
            show = showDA.getShowRoom(String.Format(" where KODE = '{0}'", sKode)).First();
            string noBon = "17010370001";
            createStruck(noBon, show);
        }

        protected void createStruck2(string noBon, MS_SHOWROOM show)
        {
            string sUser = "Adit";

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                float hor = 450f + (1 - 1) * 30f;
                Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);

                pdfDoc.Open();
                #region Create PDF

                //Paragraph paragraph = new Paragraph("Getting Started ITextSharp.");
                string imageURL = Server.MapPath("..\\Image\\Logo.png");
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //Resize image depend upon your need
                jpg.ScaleToFit(140f, 100f);
                //Give space before image
                jpg.SpacingBefore = 10f;
                //Give some space after the image
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(jpg);

                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                myPDFWriter.AddJavaScript(jAction);
                #endregion
                /*Template Done*/
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();

                using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\" + noBon + ".pdf")))
                //using(FileStream fs = File.Create(Request.PhysicalApplicationPath + "1.pdf"))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                iframePDF.Attributes["src"] = "..\\Bon\\" + noBon + ".pdf";
                string url = HttpContext.Current.Request.Url.Authority.ToString();
            }
        }

        protected void createStruck3(string noBon, MS_SHOWROOM show)
        {
            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            string pdfFilePath = Server.MapPath(".") + "/PDFFiles";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath + "/Default.pdf", FileMode.Create));
            doc.Open();
            try
            {
                Paragraph paragraph = new Paragraph("Getting Started ITextSharp.");
                string imageURL = Server.MapPath(".") + "/Logo.jpg";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //Resize image depend upon your need
                jpg.ScaleToFit(140f, 120f);
                //Give space before image
                jpg.SpacingBefore = 10f;
                //Give some space after the image
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_LEFT;

                doc.Add(paragraph);
                doc.Add(jpg);

            }
            catch (Exception ex)
            { }
            finally
            {
                doc.Close();
            }
        }

        protected void createStruck(string noBon, MS_SHOWROOM show)
        {
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Outbond_Delivery_" + "" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string sUser = "Adit";
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            SH_BAYAR bayar = new SH_BAYAR();

            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + sUser + "'");
            bayar = bayarDA.getSHBayar(" where NO_BON = '" + noBon + "'").First();

            using (MemoryStream myMemoryStream = new MemoryStream())
            {

                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);

                //Document pdfDoc = new Document(PageSize._11X17, 10f, 10f, 10f, 0f);
                float hor = 450f + (struckList.Count - 1) * 30f;
                Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);

                //Document pdfTest = new Document(PageSize.
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                pdfDoc.Open();
                //PdfWriter.GetInstance(pdfDoc, "");
                #region Create PDF

                string imageURL = "";
                switch (show.BRAND.ToLower())
                {
                    case "melissa":
                        imageURL = Server.MapPath("..\\Image\\Logo_Melissa.png");
                        break;
                    case "denim":
                        imageURL = Server.MapPath("..\\Image\\Logo_SDS.png");
                        break;
                    case "fred perry":
                        imageURL = Server.MapPath("..\\Image\\Logo_FP.png");
                        break;
                    case "707":
                        imageURL = Server.MapPath("..\\Image\\Logo_707.png");
                        break;
                    case "superga":
                        imageURL = Server.MapPath("..\\Image\\Logo_Superga.png");
                        break;
                    default:
                        imageURL = Server.MapPath("..\\Image\\Logo_SOS.png");
                        break;
                }
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //Resize image depend upon your need
                jpg.ScaleToFit(100f, 80f);
                //Give space before image
                jpg.SpacingBefore = 50f;
                //Give some space after the image
                jpg.SpacingAfter = 20f;
                jpg.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(jpg);

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                /**********************************CREATE TEMPLATE**********************************/
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 205f;
                table.LockedWidth = true;
                float[] widths = new float[] { 2f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                //table.SpacingBefore = 1f;
                //table.SpacingAfter = 0f;
                /*Create The First Table */
                PdfPCell header = new PdfPCell(new Phrase(show.SHOWROOM, title1));
                header.Border = 0;
                header.PaddingBottom = 2f;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT + " \n" + show.PHONE, regular)));
                call3.BorderWidth = 0;
                call3.HorizontalAlignment = 1;
                call3.PaddingBottom = 15f;
                table.AddCell(call3);

                //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
                PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) +
                    "\n" + "Cashier : " + "Adit", regular)));
                call1.BorderWidth = 0;
                call1.BorderWidthTop = 0.5f;
                call1.PaddingTop = 7f;
                call1.PaddingBottom = 7f;
                table.AddCell(call1);
                //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

                //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
                PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article \nPrice     Discount     Net Discount\nNet Price", regular)));
                call5.BorderWidth = 0;
                call5.BorderWidthTop = 0.5f;
                call5.BorderWidthBottom = 0.5f;
                call5.PaddingTop = 7f;
                call5.PaddingBottom = 7f;
                table.AddCell(call5);

                decimal total = 0;
                foreach (TEMP_STRUCK item in struckList)
                {
                    PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ART_DESC + " " + item.WARNA + " " + item.SIZE + " \n" +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.PRICE)) + "     " + item.DISCOUNT.ToString() + "     " +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_DISCOUNT)) + "\n" + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE * item.QTY)), regular)));
                    call2.BorderWidth = 0;
                    call2.PaddingTop = 4f;
                    call2.PaddingBottom = 4f;
                    table.AddCell(call2);
                    total = total + (item.NET_PRICE * item.QTY);
                }

                PdfPCell callTotal = new PdfPCell(new Phrase(new Chunk("Total Net : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(total)), regular)));
                callTotal.BorderWidth = 0;
                callTotal.BorderWidthTop = 0.5f;
                callTotal.BorderWidthBottom = 0.5f;
                callTotal.PaddingTop = 7f;
                callTotal.PaddingBottom = 7f;
                table.AddCell(callTotal);

                if (bayar.VOUCHER == "Yes")
                {
                    PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Voucher : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_VOUCHER)), regular)));
                    callCIF.BorderWidth = 0;
                    table.AddCell(callCIF);
                }

                if (bayar.CARD == "Yes")
                {
                    PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("Total Card : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_CARD)), regular)));
                    callCP.BorderWidth = 0;
                    callCP.PaddingTop = 4f;
                    callCP.PaddingBottom = 4f;
                    table.AddCell(callCP);
                }

                PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Total Cash : Rp " + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_UANG)), regular)));
                callAcNo.BorderWidth = 0;
                callAcNo.PaddingTop = 5f;
                callAcNo.PaddingBottom = 5f;
                table.AddCell(callAcNo);

                PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Change : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.KEMBALI)), regular)));
                callCreatedBy.BorderWidth = 0;
                callCreatedBy.PaddingBottom = 5f;
                table.AddCell(callCreatedBy);

                //create a black line
                PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                black.BackgroundColor = new BaseColor(0, 0, 0);
                black.BorderColor = new BaseColor(0, 0, 0);
                //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                black.BorderWidth = 0.5f;
                table.AddCell(black);
                ///////////////////////////
                PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \nExchange Non-discount items within 7 days. \n" +
                    "    Term & condition apply.", regularTerm)));
                bottom1.PaddingTop = 3f;
                bottom1.BorderWidth = 0.5f;
                table.AddCell(bottom1);

                pdfDoc.Add(table);


                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                myPDFWriter.AddJavaScript(jAction);
                #endregion
                /*Template Done*/
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();

                using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\" + noBon + ".pdf")))
                //using(FileStream fs = File.Create(Request.PhysicalApplicationPath + "1.pdf"))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                iframePDF.Attributes["src"] = "..\\Bon\\" + noBon + ".pdf";
                string url = HttpContext.Current.Request.Url.Authority.ToString();
                //bDoneLinkStruck.HRef = "http://112.78.148.18:8137/Bon/" + noBon + ".pdf";
                //Response.Write(pdfDoc);
                //Response.Redirect("PopUp.aspx?noBukti=" + noBukti);
                //Response.End();
                //Response.Redirect(@"E:\New folder\test.pdf");
                //string pageurl = "~/webpages/frmCrystalReportViewer.aspx?VoucherNo=" + txtVoucherNo.Text + "&VoucherDate=" + txtVoucherDate.Text + " &strUserCode=" + strUserCode.ToString() + "&strCompanyCode=" + strCompanyCode.ToString() + "&formName=frmPaymentVoucher";
                //string pageurl = @"~/Upload/test.pdf";

                //Response.Write("<script>");
                ////Response.Write("window.open('" + pageurl + "','_blank')");
                //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(pageurl)));
                //Response.Write("</script>");

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pdfwindow", "window.open('E://New folder//test.pdf','_blank','width=250,height=200');", true);
            }
        }
    }
}