using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using ATMOS_SROM.Model.Article;
using ATMOS_SROM.Services;
using log4net;
using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class Article : System.Web.UI.Page
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Article));
        GLOBALCODE func = new GLOBALCODE();

        private readonly IArticleService _articleService;

        public Article()
        {
            _articleService = Global.Resolve<IArticleService>();
        }

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
            string uLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            if (uLevel == "Buyer")
            {
                divUpload.Visible = false;
                gvMain.Columns[0].Visible = false;
            }
        }

        protected void bindGrid()
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();

            string where = tbSearch.Text == "" ? "" : String.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            //kdbrgList = kdbrgDA.getMsKdbrg(where);
            kdbrgList = kdbrgDA.getMsKdbrgArticle_1000(where);
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
            int dataUploaded = 0;
            int dataHasSpclChar = 0;
            int dataDuplicate = 0;

            try
            {
                IWorkbook workbook;

                using (FileStream fileStream = new FileStream(source, FileMode.Open, FileAccess.Read))
                {
                    workbook = Path.GetExtension(source).Equals(".xls") ?
                        (IWorkbook)new HSSFWorkbook(fileStream) :
                        new XSSFWorkbook(fileStream);
                }

                ISheet sheet = workbook.GetSheetAt(0);

                List<ArticleExcelRowModel> excelArticles = Enumerable
                    .Range(1, sheet.LastRowNum + 1)
                    .Select(i => sheet.GetRow(i))
                    .Where(r => r != null)
                    .Select(r => new ArticleExcelRowModel(r))
                    .ToList();

                dataUploaded = _articleService.ProcessExcelArticleRow(excelArticles, Session["UName"].ToString(), ref dataDuplicate, ref dataHasSpclChar);

                string message = ($"Upload Berhasil! | Data: { excelArticles.Count() } | Data Uploaded: { dataUploaded } | " +
                    $"Data duplicate: {dataDuplicate} | Failed contains special characters: { dataHasSpclChar }");

                SetMessage(message, "success");
            }
            catch (Exception ex)
            {
                _log.Error($"Error uploading master article: { ex.Message }. Inner exception: { ex.InnerException }. Stack trace: { ex.StackTrace }");

                SetMessage(ex.Message, "error");

                return false;
            }

            func.addLog("Article > Upload > " + DivMessage.InnerText, Session["UName"].ToString());

            return true;
        }

        private void SetMessage(string message, string classType)
        {
            DivMessage.InnerText = message;
            DivMessage.Attributes["class"] = classType;
            DivMessage.Visible = true;
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
                DateTime DtStartPrice = Convert.ToDateTime(tbDate.Text);
                //string price = txtpriceArticle.Text.Replace(",", "");
                msprice.ID_KDBRG = Convert.ToInt64(lblinfoIdkdbrg.Text);
                msprice.ITEM_CODE = lblInfoItemCode.Text;
                msprice.START_DATE = Convert.ToDateTime(tbDate.Text);
                msprice.PRICE = Convert.ToDecimal(txtpriceArticle.Text);//Convert.ToDecimal(price.Substring(0, price.Length - 3));
                msprice.CREATED_BY = Session["UName"].ToString();
                //kdbrgDA.updateHargaChangePrice(msprice);
                //string res = kdbrgDA.insertHargaChangePrice(msprice);
                kdbrgDA.updateHargaChangePrice(msprice, DtStartPrice.ToString("yyyy-MM-dd"));
                string res = kdbrgDA.insertHargaChangePrice(msprice, DtStartPrice.ToString("yyyy-MM-dd")); bindGridPrice();
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
    }
}