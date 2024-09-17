using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ATMOS_SROM.Domain;
using System.Data.OleDb;
using System.Data;
using ATMOS_SROM.Model;
using System.Data.SqlTypes;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;

namespace ATMOS_SROM.Master
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)  
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
            }
        }

        protected void bindGrid()
        {
            List<MS_PO> listPO = new List<MS_PO>();
            MS_PO_DA poDA = new MS_PO_DA();
            string where = tbSearch.Text == "" ? " where STATUS_PO = 1" : string.Format(" where STATUS_PO = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);
            listPO = poDA.getPO(where);
            gvMain.DataSource = listPO.OrderByDescending(item => item.ID).ToList<MS_PO>();
            gvMain.DataBind();
        }

        protected void bindPU()
        {
            string id = hdnIDPU.Value;
            string where = tbPUSearch.Text == "" ? string.Format("where STATUS_PO = 1 and ID_PO = {0}", id) : String.Format("where STATUS_PO = 1 and ID_PO = {0} and {1} = '{2}'", id, ddlPUSearch.SelectedValue, tbPUSearch.Text);
            List<MS_PO_DETAIL> detailListPO = new List<MS_PO_DETAIL>();
            MS_PO_DA poDA = new MS_PO_DA();

            detailListPO = poDA.getDetailPO(where);
            gvPU.DataSource = detailListPO.OrderByDescending(x=>x.ID_PO);
            gvPU.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);

                    UPRhdnSource.Value = source;
                    UPRhdnFileType.Value = FileType.ToLower();

                    UPRlbFileName.Text = ExcelFileName;
                    ModalUploadReady.Show();
                    //bool ret = upload(source, FileType.ToLower());
                    //File.Delete(source);

                    //if (ret)
                    //{
                    //    bindGrid();
                    //}
                }
                else
                {
                    DivMessage.InnerText = "File Harus Bertipe xls.";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Pilih File Yang Akan Diupload.";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected bool upload(string source, string fileType)
        {
            bool ret = true;
            int data = 0;
            int total = 0;
            bool cek = true;
            
            string newId = "";
            MS_PO po = new MS_PO();
            MS_PO_DETAIL detailPO = new MS_PO_DETAIL();
            MS_PO_DA poDA = new MS_PO_DA();

            List<MS_KDBRG> listKdbrg = new List<MS_KDBRG>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            try
            {
                listKdbrg = kdbrgDA.getMsKdbrgArticle("");
                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=NO;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                cnn.Open();
                DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }

                int rowTable = fileType == ".xls" ? 0 : dbSchema.Rows.Count - 1;
                //string firstSheetName = dbSchema.Rows[rowTable]["TABLE_NAME"].ToString(); //Production
                ////string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString(); //Development
                //firstSheetName = firstSheetName.ToLower().Contains("print_titles") || firstSheetName.ToLower().Contains("print_area") ||
                //    firstSheetName.ToLower().Contains("filterdatabase") ? dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString() : firstSheetName;
                string firstSheetName = "MASTER_PO$";

                //firstSheetName = "PO$";
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                DateTime? poDate;
                string dtpo = dsOle.Tables[0].Rows[2][1].ToString();
                try
                {
                    poDate = dsOle.Tables[0].Rows[2][1].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dsOle.Tables[0].Rows[2][1].ToString());
                }
                catch (Exception ex)
                {
                    poDate = SqlDateTime.MinValue.Value;
                }

                //Check MS_SHOWROOM untuk Kode Supplier
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();

                listStore = showRoomDA.getShowRoom(" where KODE = '" + dsOle.Tables[0].Rows[10][1].ToString().ToUpper() + "' AND STATUS_SHOWROOM = 'SUP'");

                //if (listStore.Count > 0 && listStore.First().STATUS_SHOWROOM != "SUP")
                //{
                //    DivMessage.InnerText = "Format Kode Supplier Salah, Silakan rubah Kode Supplier terlebih dahulu!";
                //    DivMessage.Attributes["class"] = "warning";
                //    DivMessage.Visible = true;

                //    cek = false;
                //}
                if (listStore.Count == 0)
                {
                    DivMessage.InnerText = "Supplier belum didaftarkan, Silakan Daftarkan Supplier terlebih dahulu!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;

                    cek = false;
                }
                //Check data
                cek = cek && cekHeader(dsOle.Tables[0], listKdbrg) ;
                if (cek)
                {
                    //Insert Header PO
                    po.NO_PO = "";
                    po.PO_REFF = dsOle.Tables[0].Rows[1][1].ToString();
                    po.DATE = poDate;
                    po.BRAND = dsOle.Tables[0].Rows[3][1].ToString();
                    po.CONTACT = dsOle.Tables[0].Rows[4][1].ToString();
                    po.POSITION = dsOle.Tables[0].Rows[5][1].ToString();
                    po.EMAIL = dsOle.Tables[0].Rows[6][1].ToString();
                    po.ADDRESS = dsOle.Tables[0].Rows[7][1].ToString();
                    po.PHONE = dsOle.Tables[0].Rows[8][1].ToString();
                    po.STATUS = "Release";
                    po.CREATED_BY = Session["UName"].ToString();
                    po.SUPPLIER = dsOle.Tables[0].Rows[9][1].ToString();
                    po.KODE_SUPPLIER = dsOle.Tables[0].Rows[10][1].ToString().ToUpper();
                    
                    newId = poDA.insertPORetID(po);

                    //Insert Detail PO
                    for (int i = 12; i < dsOle.Tables[0].Rows.Count; i++)
                    {
                        if (dsOle.Tables[0].Rows[i][0].ToString().Length > 5)
                        {
                            detailPO.ID_PO = Convert.ToInt64(newId);
                            detailPO.BARCODE = dsOle.Tables[0].Rows[i][0].ToString();
                            detailPO.ITEM_CODE = dsOle.Tables[0].Rows[i][1].ToString();
                            detailPO.COGS = Convert.ToDecimal(dsOle.Tables[0].Rows[i][9].ToString());
                            detailPO.PRICE = Convert.ToDecimal(dsOle.Tables[0].Rows[i][10].ToString());
                            detailPO.QTY = Convert.ToInt32(dsOle.Tables[0].Rows[i][7].ToString());
                            detailPO.STATUS = "Send";
                            detailPO.ID_KDBRG = listKdbrg.Where(item => item.BARCODE == detailPO.BARCODE).Count() > 0 ?
                                listKdbrg.Where(item => item.BARCODE == detailPO.BARCODE).First().ID : 0;

                            poDA.insertDetailPO(detailPO);
                            total = total + Convert.ToInt32(dsOle.Tables[0].Rows[i][7].ToString());
                            data++;
                        }
                    }

                    //Update Header for No PO dan QTY
                    DateTime dt = DateTime.Now;
                    string tgl = string.Format("{0:yyddMM}", dt);
                    string id = newId.Length > 4 ? newId.Remove(0, newId.Length - 4) : newId.PadLeft(4, '0');

                    po.ID = Convert.ToInt64(newId);
                    po.NO_PO = tgl + id;
                    po.QTY = total;
                    poDA.updateHeaderPO(po);

                    if (listStore.Count == 0)
                    {
                        //Insert Into MS_SHOWROOM Kode Supplier yang Baru
                        MS_SHOWROOM showroom = new MS_SHOWROOM();
                        showroom.KODE = po.KODE_SUPPLIER;
                        showroom.SHOWROOM = "";
                        showroom.STORE = "";
                        showroom.BRAND = po.BRAND;
                        showroom.ALAMAT = po.ADDRESS;
                        showroom.PHONE = po.PHONE;
                        showroom.STATUS = "";
                        showroom.STATUS_SHOWROOM = "SUP";

                        showRoomDA.insertShowRoom(showroom);
                    }

                    func.addLog("PurchaseOrder.aspx > Upload PO > NO PO : " + tgl + id + " | Jumlah Barang : " + total.ToString(), Session["UName"].ToString());

                    DivMessage.InnerText = "Upload Berhasil! | Data : " + data.ToString();
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
                //else
                //{
                //    DivMessage.InnerText = "Data Excel tidak lengkap, harap di cek kembali";
                //    DivMessage.Attributes["class"] = "warning";
                //    DivMessage.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Article > Upload > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
        }

        protected bool cekHeader(DataTable dTab, List<MS_KDBRG> listKdbrg)
        {
            bool check = true;
            for (int j = 1; j < 10; j++)
            {
                if (dTab.Rows[j][1].ToString() == "")
                {
                    check = false;

                    DivMessage.InnerText = "Data Header Excel tidak lengkap, harap di cek kembali";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            if (check)
            {
                for (int i = 12; i < dTab.Rows.Count; i++)
                {
                    if (dTab.Rows[i][0].ToString().Length > 5)
                    {
                        string qty = dTab.Rows[i][7].ToString();
                        if (dTab.Rows[i][1].ToString() == "" || dTab.Rows[i][7].ToString() == "" || dTab.Rows[i][9].ToString() == "" || dTab.Rows[i][10].ToString() == "")
                        {
                            check = false;

                            DivMessage.InnerText = "Data Detail Excel tidak lengkap, harap di cek kembali di Line " + (i + 1);
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;

                            i = dTab.Rows.Count + 1;
                        }
                        else  if (listKdbrg.Where(item => item.BARCODE == dTab.Rows[i][0].ToString()).Count() == 0)
                        {
                            check = false;

                            DivMessage.InnerText = "Barcode tidak ditemukan di Master Artikel di Line " + (i + 1);
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;

                            i = dTab.Rows.Count + 1;
                        }
                        else if ( Convert.ToInt32(qty) < 0 && ChkReturPO.Checked == false)
                        {
                            check = false;

                            DivMessage.InnerText = "Qty Tidak Boleh minus jika bukan PO Retur, Cek Kembali Di Line " + (i + 1);
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;

                            i = dTab.Rows.Count + 1;
                        }
                        else if (Convert.ToInt32(qty) > 0 && ChkReturPO.Checked == true)
                        {
                            check = false;

                            DivMessage.InnerText = "Qty Tidak Boleh Plus jika PO Retur, Cek Kembali Di Line " + (i + 1);
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;

                            i = dTab.Rows.Count + 1;
                        }
                    }
                }
            }

            return check;
        }

        protected void gvMainDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgDel = (ImageButton)e.Row.FindControl("imgDel");

                if (e.Row.Cells[7].Text == "Receive")
                {
                    imgDel.Visible = false;
                }
            }
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
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        tbPUSearch.Text = "";
                        hdnIDPU.Value = id;
                        tbPUNoPO.Text = gvMain.Rows[rowIndex].Cells[3].Text.ToString();
                        bindPU();
                        gvPU.Columns[0].Visible = gvMain.Rows[rowIndex].Cells[7].Text.ToString() == "Receive" ? false : true;
                        ModalPopupExtender0.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_PO_DA poDA = new MS_PO_DA();
                        poDA.softDelete(id, "MS_PO");

                        func.addLog("PurchaseOrder.aspx > Delete Detail PO > Delete MS_PO_DETAIL | ID : " + id, Session["UName"].ToString());
                        DivMessage.InnerText = "Delete Berhasil! ";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        bindGrid();
                    }
                    else if (e.CommandName.ToLower() == "printrow")
                    {
                        string noPO = gvMain.Rows[rowIndex].Cells[3].Text;

                        printPO(noPO);
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

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            DivMessage.Visible = false;
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvPUCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvPU.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnTRMIdTrfStock.Value = id;
                        tbTRMNoPO.Text = tbPUNoPO.Text;
                        tbTRMItemCode.Text = gvPU.Rows[rowIndex].Cells[3].Text.ToString();
                        tbTRMQTYBeli.Text = gvPU.Rows[rowIndex].Cells[6].Text.ToString();
                        hdnTRMQty.Value = gvPU.Rows[rowIndex].Cells[6].Text.ToString();
                        //bindPU();
                        //ModalPopupExtender0.Show();

                        ModalPopupExtender1.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_PO_DA poDA = new MS_PO_DA();
                        poDA.softDelete(id, "MS_PO_DETAIL");

                        func.addLog("PurchaseOrder.aspx > Delete PO > Delete MS_PO | ID : " + id, Session["UName"].ToString());
                        DivMessage.InnerText = "Delete Berhasil! ";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        bindPU();
                        ModalPopupExtender0.Show();
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

        protected void gvPUPageChanging(object sender, GridViewPageEventArgs e)
        {
            DivMessage.Visible = false;
            gvPU.PageIndex = e.NewPageIndex;
            bindPU();
            ModalPopupExtender0.Show();
        }

        protected void btnTRMSaveClick(object sender, EventArgs e)
        {
            if (!(hdnTRMQty.Value.Trim() == tbTRMQTYBeli.Text.Trim()))
            {
                MS_PO_DA poDA = new MS_PO_DA();
                MS_PO_DETAIL detailPO = new MS_PO_DETAIL();

                detailPO.ID = Convert.ToInt64(hdnTRMIdTrfStock.Value);
                detailPO.QTY = Convert.ToInt32(tbTRMQTYBeli.Text);
                poDA.updateQtyDetailPO(detailPO);

                func.addLog("PurchaseOrder.aspx > Update Qty PO > Update Qty Item Code : " + tbTRMItemCode.Text + " | ID : " + hdnTRMIdTrfStock.Value + " | QTY : " + hdnTRMQty.Value + " => " + tbTRMQTYBeli.Text, Session["UName"].ToString());
                DivMessage.InnerText = "Edit Berhasil! | QTY Beli Lama : " + hdnTRMQty.Value + " => " + tbTRMQTYBeli.Text;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
                ModalPopupExtender0.Show();
                bindPU();
            }
            else
            {
                DivMessage.InnerText = "Data Qty Sama!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
                ModalPopupExtender1.Show();
            }
        }

        protected void UPRbtnUploadClick(object sender, EventArgs e)
        {
            string source = UPRhdnSource.Value;
            string fileType = UPRhdnFileType.Value;
            bool ret = upload(source, fileType);
            File.Delete(source);

            if (ret)
            {
                bindGrid();
            }
        }

        protected void printPO(string noPO)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Purchase_Order_" + noPO + ".pdf");
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
            PdfPCell header = new PdfPCell(new Phrase("Purchase Order", title1));
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
            PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Kepada, Yth ", regular)));
            call1.BorderWidthTop = 1;
            call1.BorderWidthLeft = 1;
            call1.BorderWidthRight = 1;
            call1.BorderWidthBottom = 0;
            table.AddCell(call1);
            //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

            //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
           // PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Jl Ciniru iV no 16", regular)));
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


            //PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124", regular)));
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
            PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : ", regular)));
            bottom1.Colspan = 2;
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            bottom1.BorderWidthBottom = 0;
            table.AddCell(bottom1);

            PdfPCell bottom2 = new PdfPCell(new Phrase(new Chunk("No Refference : PL", regular)));
            bottom2.Colspan = 2;
            bottom2.PaddingBottom = 20f;
            bottom2.PaddingTop = 3f;
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

            
            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 5;
            cellNamaTotalQty.BorderWidth = 1;
            cellNamaTotalQty.HorizontalAlignment = 0;
            table4.AddCell(cellNamaTotalQty);

            PdfPCell cellTotalQty = new PdfPCell(new Phrase("", regular));
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

            /*Template Done*/
            pdfDoc.Close();
            Response.Write(pdfDoc);
            //Response.Redirect("PopUp.aspx?noBukti=" + noBukti);
            //Response.End();
        }

        protected void gvMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnPUSearch_Click(object sender, EventArgs e)
        {
            bindPU();
            ModalPopupExtender0.Show();
        }

        protected void btnPUClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtender0.Hide();
        }
    }
}