using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;
using System.Data.SqlTypes;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using AjaxControlToolkit;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace ATMOS_SROM.Warehouse
{
    public partial class GoodReceiveJenahara : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                firstDelete();
                bindGrid();
            }
        }

        protected void firstDelete()
        {
            string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_PO_DA poDA = new MS_PO_DA();
            poDA.deleteTempGR(nama);
        }

        protected void bindGrid()
        {
            string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
            List<MS_PO> poList = new List<MS_PO>();
            MS_PO_DA poDA = new MS_PO_DA();

            string where = String.Format(" where CREATED_BY_TEMP = '{0}'", nama);
            poList = poDA.getPOGR(where);
            gvMain.DataSource = poList;
            gvMain.DataBind();

            btnSave.Enabled = tbNoGR.Text == "" ? false : true;
        }

        protected void bindGridSearch()
        {
            string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
            List<MS_PO> poList = new List<MS_PO>();
            MS_PO_DA poDA = new MS_PO_DA();

            string where = tbSearchBy.Text == "" ? " where STATUS_PO = 1 ORDER BY ID DESC" : string.Format("  where STATUS_PO = 1 and {0} like '%{1}%' ORDER BY ID DESC", ddlSearchBy.SelectedValue, tbSearchBy.Text);
            poList = poDA.getPO(where);
            gvSearch.DataSource = poList;
            gvSearch.DataBind();

            ModalPopupExtender0.Show();
        }

        protected void bindDetail()
        {
            TextBox tbQty;
            DropDownList ddlWarehouse;
            string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
            List<MS_PO_DETAIL> poList = new List<MS_PO_DETAIL>();
            MS_PO_DA poDA = new MS_PO_DA();

            foreach (GridViewRow item in gvDetail.Rows)
            {
                tbQty = (TextBox)item.FindControl("tbInputQty");
                ddlWarehouse = (DropDownList)item.FindControl("ddlWarehouse");

                if (!(tbQty.Text == "") && !(tbQty.Text == "0") && !(tbQty.Text.Contains("nbsp")))
                {
                    string id = gvDetail.DataKeys[item.RowIndex].Value.ToString();
                    TR_BELI_DETAIL receivePO = new TR_BELI_DETAIL();
                    receivePO.ID_DETAIL_PO = Convert.ToInt64(id);
                    receivePO.NO_GR = tbNoGR.Text;
                    receivePO.QTY_TIBA = Convert.ToInt32(tbQty.Text);
                    receivePO.KODE = ddlWarehouse.SelectedValue;
                    receivePO.SHOWROOM = ddlWarehouse.SelectedItem.Text.ToUpper();
                    receivePO.RECEIVED_BY = Session["UName"].ToString();
                    receivingGoods(receivePO);

                    string status = tbQty.Text == item.Cells[9].Text.ToString() ? "Done" : "Sending";
                    int stockAkhir = int.Parse(tbQty.Text) + Convert.ToInt32(item.Cells[12].Text.ToString());
                    poDA.updateStatusDetailPO(id, status, stockAkhir);

                    //GET STOCK ITEM
                    List<MS_STOCK> listStock = new List<MS_STOCK>();
                    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                    MS_STOCK stock = new MS_STOCK();
                    stock.KODE = ddlWarehouse.SelectedValue;
                    listStock = stockDA.getStock(string.Format(" where LTRIM(RTRIM(BARCODE)) = '{0}' and KODE = '{1}'", item.Cells[5].Text.Trim().ToString(), stock.KODE));
                    if (listStock.Count > 0)
                    {
                        stock = listStock.First();
                    }
                    stock.ITEM_CODE = item.Cells[4].Text.Trim().ToString();
                    stock.BARCODE = item.Cells[5].Text.Trim().ToString();
                    stock.WAREHOUSE = ddlWarehouse.SelectedItem.Text.ToUpper();
                    stock.KODE = ddlWarehouse.SelectedValue;
                    stock.STOCK = Convert.ToInt32(tbQty.Text);
                    stock.RAK = "000";
                    stock.CREATED_BY = Session["UName"].ToString();
                    stock.UPDATED_BY = Session["UName"].ToString();

                    bool statusStock = listStock.Count == 0 ? insertStock(stock) : updateStock(stock);

                    string wherePO =  string.Format(" where ID in ({0})", hdnDetail.Value);
                    poDA.updateStatusPO(wherePO, "Receive");
                }
            }

            string where = string.Format(" where ID_PO in ({0}) and STATUS_PO = 1", hdnDetail.Value);
            where = tbDetailSearch.Text == "" ? where : where + string.Format(" and {0} like '%{1}%'", ddlDetailSearch.SelectedValue, tbDetailSearch.Text);
            poList = poDA.getDetailPOGR(where);
            gvDetail.DataSource = poList;
            gvDetail.DataBind();

            panelMain.Visible = false;
            panelDetail.Visible = true;
            btnSearch.Visible = false;
            btnDetailSave.Enabled = gvDetail.PageIndex == gvDetail.PageCount - 1 ? tbDetailSearch.Text == "" ? true : false : false;
        }

        protected void bindNoGR()
        {
            List<TR_BELI_HEADER> listGRHeader = new List<TR_BELI_HEADER>();
            MS_PO_DA poDA = new MS_PO_DA();

            string where = string.Format(" where {0} like '%{1}%'", ddlGRHSearch.SelectedValue, tbGRHSearch.Text);
            listGRHeader = poDA.getGRHeaderGroupBy(where);

            gvGRH.DataSource = listGRHeader;
            gvGRH.DataBind();

            ModalListGRHeader.Show();
        }

        protected void bindGRDetail()
        {
            string noGR = hdnGRDNoGR.Value;
            List<TR_BELI_DETAIL> listGRDetail = new List<TR_BELI_DETAIL>();
            MS_PO_DA poDA = new MS_PO_DA();

            string where = string.Format(" where NO_GR = '{0}' and {1} like '%{2}%'", noGR, ddlGRDSearch.SelectedValue, tbGRDSearch.Text);
            listGRDetail = poDA.getGRDetail(where);
            gvGRD.DataSource = listGRDetail;
            gvGRD.DataBind();

            ModalListGRDetail.Show();
        }

        protected void btnDetailSearchClick(object sender, EventArgs e)
        {
            bindDetail();
        }

        protected void receivingGoods(TR_BELI_DETAIL receivePO)
        {
            MS_PO_DA poDA = new MS_PO_DA();
            poDA.insertReceivePO(receivePO);
        }

        protected void btnDetailSaveClick(object sender, EventArgs e)
        {
            if (cekNilaiQty())
            {
                bindDetail();
                firstDelete();
                bindGrid();
                clearForm();

                DivMessage.InnerText = "Penerimaan Berhasil!";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGridSearch();
        }

        protected void btnSaveClick(object sender, EventArgs e)
        {
            string idChoose = "";
            MS_PO_DA poDA = new MS_PO_DA();

            foreach (GridViewRow item in gvMain.Rows)
            {
                idChoose = idChoose + "," + gvMain.DataKeys[item.RowIndex].Value.ToString();
            }
            hdnDetail.Value = idChoose.Remove(0, 1);

            DateTime tglTrans = SqlDateTime.MaxValue.Value;
            string date = tbTglTrans.Text.ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }
            string dateyymm = string.Format("{0:yyMM}", tglTrans);
            if (cekLock("lockHO", dateyymm))
            {
                poDA.insertGr(tbNoGR.Text.Trim(), Session["UName"].ToString(), tglTrans);
                bindDetail();
                DivMessage.Visible = false;
            }
            else 
            {
                DivMessage.InnerText = "Bulan Sudah Di Lock";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
           
            
        }
        protected bool cekLock(string lockParam, string time)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            bool ret = true;

            listParam = loginDA.getListParam(string.Format(" where name in ('{0}')", lockParam));

            //time = 1601 ; param1 = 1510 ; param2 = 1601
            foreach (MS_PARAMETER item in listParam)
            {

                int nowTime = Convert.ToInt32(time);
                int value = Convert.ToInt32(item.VALUE);
                if (!(nowTime > value))
                {
                    ret = false;
                }
            }

            return ret;
        }
        protected void btnSearchSearchClick(object sender, EventArgs e)
        {
            bindGridSearch();
        }

        protected void btnListGRClick(object sender, EventArgs e)
        {
            bindNoGR();
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_PO_DA poDA = new MS_PO_DA();
                        TEMP_GR tempGR = new TEMP_GR();

                        tempGR.ID_PO = Convert.ToInt64(id);
                        tempGR.CREATED_BY = Session["UName"].ToString();

                        poDA.deleteOneTempGR(tempGR);
                        bindGrid();
                        clearForm();
                        DivMessage.InnerText = "Hapus Data Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
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

        protected void gvSearcRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvSearch.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        if (tbKodeSupplier.Text.ToLower() == gvSearch.Rows[rowIndex].Cells[6].Text.ToString().ToLower() || tbKodeSupplier.Text == "")
                        {
                            MS_PO_DA poDA = new MS_PO_DA();
                            TEMP_GR gr = new TEMP_GR();

                            gr.ID_PO = Convert.ToInt64(id);
                            gr.KODE_SUPPLIER = gvSearch.Rows[rowIndex].Cells[6].Text.Contains("nbsp") ? "" : gvSearch.Rows[rowIndex].Cells[6].Text;
                            gr.SUPPLIER = gvSearch.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvSearch.Rows[rowIndex].Cells[7].Text;
                            gr.CREATED_BY = Session["UName"].ToString();

                            string idTemp = poDA.insertTempGrRetID(gr);
                            if (tbNoGR.Text == "")//Create No GR
                            {
                                string idGR = idTemp.Length > 4 ? idTemp.Remove(0, idTemp.Length - 4) : idTemp.PadLeft(4, '0');

                                DateTime dt = DateTime.Now;
                                string tgl = string.Format("{0:yyddMM}", dt);

                                tbNoGR.Text = "GR" + tgl + idGR;
                                tbKodeSupplier.Text = gvSearch.Rows[rowIndex].Cells[6].Text.Contains("&nbsp") ? "None" : gvSearch.Rows[rowIndex].Cells[6].Text.ToString();
                                tbNamaSupplier.Text = gvSearch.Rows[rowIndex].Cells[7].Text.Contains("&nbsp") ? "None" : gvSearch.Rows[rowIndex].Cells[7].Text.ToString();
                            }
                            bindGrid();
                        }
                        else
                        {
                            divSearchMessage.InnerText = "Harap pilih Supplier PO yang lain!";
                            divSearchMessage.Attributes["class"] = "warning";
                            divSearchMessage.Visible = true;
                            ModalPopupExtender0.Show();
                        }
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

        protected void gvDetailPageChanging(object sender, GridViewPageEventArgs e)
        {
            if (cekNilaiQty())
            {
                gvDetail.PageIndex = e.NewPageIndex;
                bindDetail();
            }
        }

        protected void gvPUCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvSearch.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        
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

        protected void gvSearchPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearch.PageIndex = e.NewPageIndex;
            bindGridSearch();
        }

        protected void gvPUPageChanging(object sender, GridViewPageEventArgs e)
        {
            if (cekNilaiQty())
            {
                gvDetail.PageIndex = e.NewPageIndex;
                bindDetail();
            }
        }

        protected void gvGRHCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string noGR = gvGRH.DataKeys[rowIndex]["NO_GR"].ToString();
                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        //Delete GR
                        MS_PO_DA poDA = new MS_PO_DA();
                        poDA.cancelGR(noGR);

                        DivMessage.InnerText = "GR NO : " + noGR + " sudah dihapus!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                    else if (e.CommandName.ToLower() == "saverow")
                    {
                        hdnGRDNoGR.Value = noGR;

                        bindGRDetail();
                        ModalListGRDetail.Show();
                    }
                    else if (e.CommandName.ToLower() == "printrow")
                    {
                        #region Old"
                        //MS_SHOWROOM show = new MS_SHOWROOM();
                        //MS_PO_DA poDA = new MS_PO_DA();

                        //List<TR_BELI_DETAIL> listGRDetail = new List<TR_BELI_DETAIL>();
                        //List<MS_PO> listPO = new List<MS_PO>();

                        //string kodeSupplier = gvGRH.Rows[rowIndex].Cells[3].Text.Contains("&nbsp") ? "" : gvGRH.Rows[rowIndex].Cells[3].Text;
                        //listPO = poDA.getPO(string.Format(" where KODE_SUPPLIER = '{0}'", kodeSupplier));

                        //show.SHOWROOM = listPO.First().KODE_SUPPLIER + " - " + listPO.First().SUPPLIER;
                        //show.ALAMAT = listPO.First().ADDRESS;
                        //show.PHONE = listPO.First().PHONE;

                        //string where = string.Format(" where NO_GR = '{0}'", noGR);
                        //listGRDetail = poDA.getGRDetail(where);

                        //printNoBukti(noGR, listGRDetail, show);
                        #endregion
                        DirectPrinPackingList(noGR);
                    }
                }
            }
            catch (Exception ex)
            {
                divMessageGRHeader.InnerText = "Error : " + ex.Message;
                divMessageGRHeader.Attributes["class"] = "error";
                divMessageGRHeader.Visible = true;

                ModalListGRHeader.Show();
            }
        }

        protected void gvGRHPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvGRH.PageIndex = e.NewPageIndex;
            bindNoGR();
        }

        protected void gvGRDCommand(object sender, GridViewCommandEventArgs e)
        {
            try 
            {
                
            }
            catch (Exception ex)
            {
                divMessageGRDetail.InnerText = "Error : " + ex.Message;
                divMessageGRDetail.Attributes["class"] = "error";
                divMessageGRDetail.Visible = true;
            }
        }

        protected void gvGRDPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvGRD.PageIndex = e.NewPageIndex;
            bindGRDetail();
        }

        protected void printNoBukti(string noBukti, List<TR_BELI_DETAIL> beliDetail, MS_SHOWROOM show)
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
            PdfPCell header = new PdfPCell(new Phrase("Packing List Good Receive", title1));
            header.Colspan = 2;
            header.Border = 0;
            header.PaddingBottom = 20f;
            header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(header);

            //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
            PdfPCell call1 = new PdfPCell(new Phrase(new Chunk(show.SHOWROOM, regular)));
            call1.BorderWidthTop = 1;
            call1.BorderWidthLeft = 1;
            call1.BorderWidthRight = 1;
            call1.BorderWidthBottom = 0;
            table.AddCell(call1);

            PdfPCell call3 = new PdfPCell(new Phrase(new Chunk("PT.Sembilan Ohm Sembilan", regular)));
            call3.BorderWidth = 0;
            call3.BorderWidthTop = 1;
            call3.BorderWidthLeft = 1;
            call3.BorderWidthRight = 1;
            call3.BorderWidthBottom = 0;
            table.AddCell(call3);
            //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));


            PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT, regular)));
            call2.BorderWidthTop = 0;
            call2.BorderWidthLeft = 1;
            call2.BorderWidthRight = 1;
            call2.BorderWidthBottom = 0;
            table.AddCell(call2);

            //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
            PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Jl Ciniru iV no 16", regular)));
            call5.BorderWidthTop = 0;
            call5.BorderWidthLeft = 1;
            call5.BorderWidthRight = 1;
            call5.BorderWidthBottom = 0;
            table.AddCell(call5);

            //table.AddCell(new Phrase(new Chunk("VENUE OF MEETING : " + Lbl_Venue.Text + "", regular)));



            //table.AddCell(new Phrase(new Chunk("INDUSTRY : " + Lbl_Industry.Text + "", regular)));
            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk(show.PHONE, regular)));
            callCP.BorderWidthTop = 0;
            callCP.BorderWidthLeft = 1;
            callCP.BorderWidthRight = 1;
            callCP.BorderWidthBottom = 0;
            table.AddCell(callCP);

            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124", regular)));
            callCIF.BorderWidthTop = 0;
            callCIF.BorderWidthLeft = 1;
            callCIF.BorderWidthRight = 1;
            callCIF.BorderWidthBottom = 0;
            table.AddCell(callCIF);
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
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            bottom1.BorderWidthBottom = 0;
            table.AddCell(bottom1);

            PdfPCell bottom2 = new PdfPCell(new Phrase(new Chunk("Tanggal Transaksi : " + beliDetail.First().TGL_TRANS, regular)));
            bottom2.Colspan = 2;
            bottom2.PaddingBottom = 20f;
            bottom2.BorderWidth = 1;
            bottom2.BorderWidthTop = 0;
            table.AddCell(bottom2);

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
            for (int i = 0; i < beliDetail.Count; i++)
            {
                iTextSharp.text.Font font = regular;

                PdfPCell cell1 = new PdfPCell(new Phrase(Convert.ToString(i + 1), font));
                cell1.BorderWidth = 1;
                cell1.HorizontalAlignment = 1;
                table4.AddCell(cell1);
                PdfPCell cellBar = new PdfPCell(new Phrase(beliDetail[i].BARCODE, font));
                cellBar.BorderWidth = 1;
                cellBar.HorizontalAlignment = 0;
                table4.AddCell(cellBar);
                PdfPCell cell2 = new PdfPCell(new Phrase(beliDetail[i].ITEM_CODE, font));
                cell2.BorderWidth = 1;
                cell2.HorizontalAlignment = 0;
                table4.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase(beliDetail[i].FART_DESC, font));
                cell3.BorderWidth = 1;
                cell3.HorizontalAlignment = 0;
                table4.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase(beliDetail[i].FCOL_DESC, font));
                cell4.BorderWidth = 1;
                cell4.HorizontalAlignment = 0;
                table4.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase(beliDetail[i].FSIZE_DESC, font));
                cell5.BorderWidth = 1;
                cell5.HorizontalAlignment = 0;
                table4.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase(beliDetail[i].QTY_TIBA.ToString(), font));
                cell6.BorderWidth = 1;
                cell6.HorizontalAlignment = 0;
                table4.AddCell(cell6);

                Total = Total + beliDetail[i].QTY_TIBA;
            }

            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 6;
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

            /*Template Done*/

            pdfDoc.Close();
            Response.Write(pdfDoc);
            //Response.End();
            //Response.Close();
            Response.Flush(); Response.Close();
        }

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

        protected bool cekNilaiQty()
        {
            bool ret = true;
            try
            {
                TextBox tbQtyTiba;
                for (int i = 0; i < gvDetail.Rows.Count; i++)
                {
                    tbQtyTiba = (TextBox)gvDetail.Rows[i].FindControl("tbInputQty");
                    int qtyWait = Convert.ToInt32(gvDetail.Rows[i].Cells[9].Text.ToString());
                    int qtyTiba = Convert.ToInt32(tbQtyTiba.Text == "" ? "0" : tbQtyTiba.Text.Contains("nbsp") ? "0" : tbQtyTiba.Text);
                    int qtyunderover = (qtyWait * 5 )/ 100;// qtyWait - (qtyWait * (5 / 100)); 
                    //int QtyOver = qtyWait + (qtyWait * (5 / 100));
                    int QtyOver = qtyWait + qtyunderover;
                    int qtyunder = qtyWait - qtyunderover;

                    if (qtyTiba > QtyOver)
                    {
                        ret = false;
                        i = i + gvDetail.Rows.Count;

                        DivMessage.InnerText = "Quantity barang yang tiba tidak boleh melebihi dari batas toleransi 5% Qty yang dipesan. Silakan buat PO yang baru.";
                        DivMessage.Attributes["class"] = "warning";
                        DivMessage.Visible = true;
                    }
                    else if (qtyTiba < qtyunder)
                    {
                        ret = false;
                        i = i + gvDetail.Rows.Count;

                        DivMessage.InnerText = "Quantity barang yang tiba tidak boleh Kurang dari batas toleransi 5% Qty yang dipesan. Silakan buat PO yang baru.";
                        DivMessage.Attributes["class"] = "warning";
                        DivMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            return ret;
        }

        protected void clearForm()
        {
            panelMain.Visible = true;
            panelDetail.Visible = false;

            tbNoGR.Text = "";
            tbKodeSupplier.Text = "";
            tbNamaSupplier.Text = "";
            hdnDetail.Value = "";
            tbTglTrans.Text = "";
            btnSearch.Visible = true;
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string langId2 = DataBinder.Eval(e.Row.DataItem, "QTY").ToString();
                
                if (Convert.ToInt32(langId2) < 0)
                {
                    FilteredTextBoxExtender fltr = (FilteredTextBoxExtender)e.Row.FindControl("FilteredTextBoxExtender1");
                    fltr.FilterType = FilterTypes.Custom;
                    fltr.ValidChars = "-0123456789";
                }
                else
                {
                    FilteredTextBoxExtender fltr = (FilteredTextBoxExtender)e.Row.FindControl("FilteredTextBoxExtender1");
                    fltr.FilterType = FilterTypes.Numbers;
                }
            }
        }
        public void DirectPrinPackingList(string NO_PO)
        {
            REPORT_DA rptDA = new REPORT_DA();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = "pdf";// string.Empty;
            DataSet dsGrpSum, dsActPlan, dsProfitDetails,
                dsProfitSum, dsSumHeader, dsDetailsHeader, dsBudCom = null;


            DataSet dsData = rptDA.GetDataTF_ReportPackingListGR(NO_PO);
            ReportDataSource rdsAct = new ReportDataSource("DataSet1", dsData.Tables[0]);
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = "Reports/PackingList_GoodReceive.rdlc"; //This is your rdlc name.
            //viewer.LocalReport.SetParameters(param);
            viewer.LocalReport.DataSources.Add(rdsAct); // Add  datasource here         
            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
            // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= PackingList_GoodReceive_" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }
    }
}