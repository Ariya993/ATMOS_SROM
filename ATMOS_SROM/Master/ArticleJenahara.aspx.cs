using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;
using System.IO;
using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop.Excel; 

namespace ATMOS_SROM.Master
{
    public partial class ArticleJenahara : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                viewGrid();
                bindGrid();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            divMain.Visible = true;

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
                    bool ret = upload(dir, FileUploadName, source);
                    if (ret)
                    {
                        //DivMessage.InnerText = "Upload Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        bindGrid();

                        File.Delete(source);
                    }
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

        protected void viewGrid()
        {
            divUpload.Visible = true;
            gvMain.Columns[0].Visible = true;
            //string uLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            //if (uLevel == "Buyer")
            //{
            //    divUpload.Visible = false;
            //    gvMain.Columns[0].Visible = false;
            //}
        }

        protected void bindGrid()
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();

            string where = tbSearch.Text == "" ? "" : String.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            //kdbrgList = kdbrgDA.getMsKdbrg(where);
            kdbrgList = kdbrgDA.getMsKdbrgArticleJenahara(where);
            gvMain.DataSource = kdbrgList;
            gvMain.DataBind();
        }

        protected void bindGridPrice()
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_PRICE> priceist = new List<MS_PRICE>();

            string where = String.Format(" where ID_KDBRG = {0}", lblinfoIdkdbrg.Text);

            //kdbrgList = kdbrgDA.getMsKdbrg(where);
            priceist = kdbrgDA.getPriceInfo(where);
            gvPrice.DataSource = priceist;
            gvPrice.DataBind();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
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
                        MS_KDBRG kbrginfo = new MS_KDBRG();
                        MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

                        kbrginfo = kdbrgDA.GetInfoKdbrg(" Where ID = " + id);
                        lblinfoIdkdbrg.Text = Convert.ToString(kbrginfo.ID);
                        lblinfobarcode.Text = kbrginfo.BARCODE;
                        lblInfoItemCode.Text = kbrginfo.ITEM_CODE;
                        lblInfoColor.Text = kbrginfo.FCOL_DESC;
                        lblInfoSize.Text = kbrginfo.FSIZE_DESC;
                        lblInfoDesc.Text = kbrginfo.FART_DESC;
                        lblInfoProduct.Text = kbrginfo.FPRODUK;
                        bindGridPrice();
                        txtpriceArticle.Text = "";
                        tbDate.Text = "";
                        DivPriceMsg.Visible = false;
                        ModalPopupExtenderPrice.Show();
                    }
                    else if (e.CommandName.ToLower() == "blockrow")
                    {
                        MS_PO_DA PODA = new MS_PO_DA();
                        List<MS_PO_DETAIL> LPoDtl = new List<MS_PO_DETAIL>();
                        MS_KDBRG kbrginfo = new MS_KDBRG();
                        MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

                        kbrginfo = kdbrgDA.GetInfoKdbrg(" Where ID = " + id);
                        LPoDtl = PODA.getDetailPO("where BARCODE = '" + kbrginfo.BARCODE + "'");
                        if (LPoDtl.Count > 0)
                        {
                            DivMessage.InnerText = "Barcode : " + kbrginfo.BARCODE + " Telah memiliki PO dan tidak dapat di block";
                            DivMessage.Attributes["class"] = "error";
                            DivMessage.Visible = true;
                        }
                        else
                        {
                            kdbrgDA.updateBlockStatMS_KDBRG(Convert.ToInt64(id));
                            DivMessage.InnerText = "Barcode : " + kbrginfo.BARCODE + " Telah di block";
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                            bindGrid();
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

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected bool upload(string dir, string fileName, string source)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;
            string error = "";
            string idUpload = "";
            string BrcdExst ="";
            MS_KDBRG kdbrg = new MS_KDBRG();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
            //msKdbrgList = kdbrgDA.getMsKdbrg("");
            //msKdbrgList = kdbrgDA.getMsKdbrgArticle("");
            msKdbrgList = kdbrgDA.getMsKdbrgJENAHARA("");
            try
            {
                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 12.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom

                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);
                //lblSrc.Text = source;
                DataSet dsOle = new DataSet();
                cnn.Open();
                System.Data.DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                int dbschmCount = dbSchema.Rows.Count;
                //lblSrc.Text = dbschmCount.ToString();
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }
                //string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                int i = 1;

                int cnt = dsOle.Tables[0].Rows.Count;
                //lblInfo.Text = cnt.ToString();
                foreach (DataRow item in dsOle.Tables[0].Rows)
                {
                    if (item[1].ToString() != "" && item[2].ToString() != "" && item[4].ToString() != "" && item[5].ToString() != "" && item[8].ToString() != "" && item[11].ToString() != "")
                    {
                        //if (msKdbrgList.Where(items => items.ITEM_CODE == item[1].ToString() && items.FCOL_DESC == item[4].ToString() &&
                        //    items.FSIZE_DESC == item[5].ToString()).ToList<MS_KDBRG>().Count == 0)
                        if (msKdbrgList.Where(items => items.BARCODE.Trim().ToLower() == item[2].ToString().Trim().ToLower()).ToList<MS_KDBRG>().Count == 0)
                        {
                            DateTime dt = new DateTime();
                            kdbrg.FBRAND = item[0].ToString().Trim();// item[0].ToString();
                            kdbrg.ITEM_CODE = item[1].ToString().Trim();
                            kdbrg.BARCODE = item[2].ToString().Trim() == "" ? String.Format("{0:yMMddHHmmssff}", dt) : item[2].ToString().Trim();
                            kdbrg.FART_DESC = item[3].ToString().Trim();
                            kdbrg.FCOL_DESC = item[4].ToString().Trim();
                            kdbrg.FSIZE_DESC = item[5].ToString().Trim();
                            kdbrg.FPRODUK = item[6].ToString().Trim();
                            kdbrg.COGS = Convert.ToDecimal(item[7].ToString() == "" ? "0" : item[7].ToString());
                            kdbrg.PRICE = Convert.ToDecimal(item[8].ToString() == "" ? "0" : item[8].ToString());
                            kdbrg.FGROUP = item[9].ToString().Trim();
                            kdbrg.FSEASON = item[10].ToString().Trim();
                            kdbrg.DATE_START = Convert.ToDateTime(item[11]);
                            kdbrg.CREATED_BY = Session["UName"].ToString();
                            string idNew = kdbrgDA.insertMsKdbrgJenahara(kdbrg);

                            idUpload = idNew.ToLower().Contains("error") ? idUpload : idUpload + "," + idNew;
                            error = idNew.ToLower().Contains("error") ? error + "," + i.ToString() : error;

                            msKdbrgList.Add(kdbrg);
                            insert++;
                        }
                        else
                        {
                            doubel++;
                            if (BrcdExst == "")
                            {
                                BrcdExst = item[2].ToString().Trim();
                            }
                            else
                            {
                                BrcdExst = BrcdExst + ", " + item[2].ToString().Trim();
                            }
                        }
                        //else
                        //{
                        //    //kdbrg = msKdbrgList.Where(items => items.ITEM_CODE == item[1].ToString() && items.FCOL_DESC == item[4].ToString() && 
                        //    //    items.FSIZE_DESC == item[5].ToString()).ToList<MS_KDBRG>().First();
                        //    kdbrg = msKdbrgList.Where(items => items.BARCODE == item[2].ToString()).First();
                        //    kdbrg.PRICE = Convert.ToDecimal(item[8].ToString() == "" ? "0" : item[8].ToString());
                        //    kdbrg.COGS = Convert.ToDecimal(item[7].ToString() == "" ? "0" : item[7].ToString());
                        //    kdbrg.UPDATED_BY = Session["UName"].ToString();

                        //    kdbrg.FBRAND = item[0].ToString().Trim();
                        //    kdbrg.FPRODUK = item[6].ToString().Trim();
                        //    kdbrg.FGROUP = item[9].ToString().Trim();
                        //    kdbrg.FART_DESC = item[3].ToString().Trim();
                        //    kdbrg.FCOL_DESC = item[4].ToString().Trim();
                        //    kdbrg.FSIZE_DESC = item[5].ToString().Trim();
                        //    kdbrg.DATE_START = Convert.ToDateTime(item[11]);

                        //    kdbrgDA.updateMsKdbrg(kdbrg);
                        //    kdbrgDA.updateHarga(kdbrg);

                        //    idUpload = idUpload + "," + kdbrg.ID.ToString();
                        //    doubel++;
                        //}
                        data++;
                    }
                    i++;
                }

                if (insert > 0 || doubel > 0)
                {
                    if (idUpload != "")
                    {
                        insertHarga(idUpload.Remove(0, 1));
                    }
                }
                //string message = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
                string message = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Exists Data : " + doubel.ToString() + " | Barcode Exists : " + BrcdExst;

                message = error == "" ? message : message + " | Error line " + error.Remove(0, 1);
                DivMessage.InnerText = message;
                
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

        protected bool uploadData(string dir, string fileName, string source)
        {
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            string str;
            int rCnt = 0;
            int cCnt = 0;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(source, 0, true, 5, "", "", 
                true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            for (rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
            {
                for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                {
                    str = (string)(range.Cells[rCnt, cCnt] as Range).Value2;
                    
                }
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            return true;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public void insertHarga(string idUpload)
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            kdbrgDA.insertHarga(idUpload);
        }

        protected void btnaddprice_Click(object sender, EventArgs e)
        {
            if (tbDate.Text != "" && txtpriceArticle.Text != "")
            {
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_PRICE msprice = new MS_PRICE();
                string price = txtpriceArticle.Text.Replace(",", "");
                msprice.ID_KDBRG = Convert.ToInt64(lblinfoIdkdbrg.Text);
                msprice.ITEM_CODE = lblInfoItemCode.Text;
                msprice.START_DATE = Convert.ToDateTime(tbDate.Text);
                msprice.PRICE = Convert.ToDecimal(price.Substring(0, price.Length - 3));
                msprice.CREATED_BY = Session["UName"].ToString();
                //kdbrgDA.updateHargaChangePrice(msprice);
                //string res = kdbrgDA.insertHargaChangePrice(msprice);
                bindGridPrice();
                bindGrid();
                DivPriceMsg.InnerText = "Harga : " + msprice.PRICE + " Berlaku Mulai Tanggal : " + tbDate.Text;
                DivPriceMsg.Attributes["class"] = "success";
                DivPriceMsg.Visible = true;
            }
            else 
            {
                DivPriceMsg.InnerText = "Harga dan Tanggal tidak boleh kosong";
                DivPriceMsg.Attributes["class"] = "error";
                DivPriceMsg.Visible = true;
            }
            ModalPopupExtenderPrice.Show();
        }

        protected void gvPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrice.PageIndex = e.NewPageIndex;
            bindGridPrice();
        }

        protected void gvPrice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvPrice.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "editprice")
                    {
                        lblpriceNow.Text = gvPrice.Rows[rowIndex].Cells[4].Text.ToString();
                        lblTglStart.Text = gvPrice.Rows[rowIndex].Cells[5].Text.ToString();
                        lblidprice.Text = id;
                        Div3.Visible = false;
                        ModalPopupExtenderEditPrice.Show();
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

        protected void gvPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dtStart = Convert.ToDateTime( DataBinder.Eval(e.Row.DataItem, "START_DATE").ToString());
                DateTime dtnow = DateTime.Now.Date;
                if (dtStart <= dtnow)
                {
                    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgEdit");
                    imgBtn.Visible = false;
                }
            }
        }

        protected void btncloesprice_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderPrice.Hide();
        }

        protected void btneditPrice_Click(object sender, EventArgs e)
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();  
            MS_PRICE prc = new MS_PRICE();
            if (txtPriceNew.Text != "")
            {
                string price = txtPriceNew.Text.Replace(",", "");
                prc.PRICE = Convert.ToDecimal(price.Substring(0, price.Length - 3));
                prc.ID = Convert.ToInt64(lblidprice.Text);

                kdbrgDA.EditHargaChangePrice(prc);
                bindGridPrice();
                Div3.Visible = false;
                DivPriceMsg.InnerText = "Harga : " + prc.PRICE + " Berlaku Mulai Tanggal : " + lblTglStart.Text;
                DivPriceMsg.Attributes["class"] = "success";
                DivPriceMsg.Visible = true;

                ModalPopupExtenderPrice.Show();
            }
            else
            {
                Div3.InnerText = "Mohon Isi Kolom Harga";
                Div3.Attributes["class"] = "warning";
                Div3.Visible = true;
                ModalPopupExtenderEditPrice.Show();
            }
            
        }

        protected void btneditpriceclose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderEditPrice.Hide();
            ModalPopupExtenderPrice.Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string statItem = DataBinder.Eval(e.Row.DataItem, "STAT_KDBRG").ToString();
                if (statItem == "BLOCKED") //Database field BLOCK_STAT = 0
                {
                    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgBlock");
                    ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                    imgBtn.Visible = false;
                    imgBtnEdit.Visible = false;
                }
            }
        }
    }
}