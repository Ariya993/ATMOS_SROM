using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.Globalization;
using System.Data.SqlTypes;
using System.Data;
using System.Data.Common;
using System.Reflection;
using ATMOS_SROM.TestDummy;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text.pdf;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System.Drawing;

namespace ATMOS_SROM.Warehouse
{
    public partial class StockOpname : System.Web.UI.Page
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                firstDelete();
                bindShowroom();
            }
        }

        protected void bindGrid()
        {
            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            List<TEMP_SO_DETAIL> tempSODetailList = new List<TEMP_SO_DETAIL>();
            string sName = Session["UName"] == null ? "" : Session["UName"].ToString();
            //string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            string sKode = ddlStoreUpload.SelectedValue;
            //string where = string.Format(" where CREATED_BY = '{0}' or KODE = '{1}'", sName, tbKodeWarehouse.Text);
            string where = //tbSearch.Text == "" ? string.Format(" where CREATED_BY = '{0}' or KODE = '{1}'", sName, tbKodeWarehouse.Text) : 
                string.Format(" where (CREATED_BY = '{0}' or KODE = '{1}') and {2} like '%{3}%' " +
                " order by (CASE WHEN DIFF_STOCK != 0 THEN 1 ELSE 0 END) desc, DIFF_STOCK desc", sName, tbKodeWarehouse.Text, ddlSearch.SelectedValue, tbSearch.Text);

            DateTime dt = SqlDateTime.MaxValue.Value;
            if (!string.IsNullOrEmpty(tbTglSO.Text))
            {
                DateTime.TryParseExact(tbTglSO.Text, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            }

            tempSODetailList = stockOpnameDA.getCompare(where, dt, sKode, tbNoBukti.Text);
            int total = 0;
            foreach (TEMP_SO_DETAIL item in tempSODetailList)
            {
                total = total + item.STOCK;
            }

            gvMain.DataSource = tempSODetailList;
            gvMain.DataBind();
            tbAllStock.Text = total.ToString();

            List<EXCELL_ALL_RAK_TEMP_SO> allRakList = new List<EXCELL_ALL_RAK_TEMP_SO>();
            allRakList = stockOpnameDA.getTempSO(string.Format(" where CREATED_BY = '{0}'", sName));
            ddlRakSO.DataSource = allRakList;
            ddlRakSO.DataBind();

            DivMessage.Visible = false;
        }

        protected void bindPU()
        {
            STOCK_OPNAME_DA stockOpanameDA = new STOCK_OPNAME_DA();
            List<TR_OP_HEADER> listTRHeader = new List<TR_OP_HEADER>();

            string where = tbPUSearch.Text == "" ? " ORDER BY ID DESC" : string.Format(" where {0} like '%{1}%' ORDER BY ID DESC", ddlPUSearch.SelectedValue, tbPUSearch.Text);
            listTRHeader = stockOpanameDA.getOPHeader(where);

            gvPU.DataSource = listTRHeader;
            gvPU.DataBind();
            ModalPopupTrfHeader.Show();
        }

        protected void bindIKirim()
        {
            STOCK_OPNAME_DA stockOpanameDA = new STOCK_OPNAME_DA();
            List<TR_OP_DETAIL> listTRDetail = new List<TR_OP_DETAIL>();

            string where = tbIKirimSearch.Text == "" ? string.Format(" where ID_HEADER = {0}", hdnIKirimIDHeader.Value) : 
                string.Format(" where {0} like '%{1}%' and ID_HEADER = {2}", ddlIKirimSearch.SelectedValue, tbIKirimSearch.Text, hdnIKirimIDHeader.Value);
            listTRDetail = stockOpanameDA.getOPDetail(where);

            gvIKirim.DataSource = listTRDetail;
            gvIKirim.DataBind();
            ModalItemKirim.Show();
        }

        protected void bindTemp()
        {
            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            List<TEMP_SO_DETAIL> listTemp = new List<TEMP_SO_DETAIL>();

            string where = tbTempSearch.Text == "" ? string.Format(" where NO_BUKTI = '{0}'", tbNoBukti.Text) 
                : string.Format(" where NO_BUKTI = '{0}' and {1} like '%{2}%'", tbNoBukti.Text, ddlTempSearch.SelectedValue, tbTempSearch.Text);

            listTemp = stockOpnameDA.getTempOPDetail(where);

            gvTemp.DataSource = listTemp;
            gvTemp.DataBind();

            ModalTempDetail.Show();
        }

        protected void firstDelete()
        {
            string nama = Session["UName"] == null ? "" : Session["UName"].ToString();
            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            stockOpnameDA.deleteTempSO(nama);
        }

        protected void bindShowroom()
        {
            List<MS_SHOWROOM> listShowRoom = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            listShowRoom = showDA.getShowRoom(" where STATUS = 'OPEN'");
            
            ddlStoreUpload.DataSource = listShowRoom;
            ddlStoreUpload.DataTextField = "SHOWROOM";
            ddlStoreUpload.DataValueField = "KODE";
            ddlStoreUpload.DataBind();
        }

        protected void bindBrand(int value)
        {
            if (value == 1) //New Brand, Pertama kali klik edit
            {
                List<MS_KDBRG> allListKdbrg = new List<MS_KDBRG>();
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                allListKdbrg = kdbrgDA.getMsKdbrgArticleBRAND(" GROUP BY FBRAND");
                kdbrg.FBRAND = "-Pilih Brand-";
                allListKdbrg.Insert(0, kdbrg);

                ddlUpdBrand.DataSource = allListKdbrg;
                ddlUpdBrand.DataBind();

                ddlUpdBrand.Enabled = true;
                ddlUpdProduk.Enabled = false;
                ddlUpdArticle.Enabled = false;
                ddlUpdColor.Enabled = false;
                ddlUpdSize.Enabled = false;
            }
            else if (value == 2) //New Produk, setelah pilih brand
            {
                List<MS_KDBRG> allListKdbrg = new List<MS_KDBRG>();
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                allListKdbrg = kdbrgDA.getMsKdbrgArticlePRODUK(string.Format(" where FBRAND = '{0}' GROUP BY FPRODUK", ddlUpdBrand.SelectedValue));
                kdbrg.FPRODUK = "-Pilih Produk-";
                allListKdbrg.Insert(0, kdbrg);

                ddlUpdProduk.DataSource = allListKdbrg;
                ddlUpdProduk.DataBind();

                ddlUpdBrand.Enabled = true;
                ddlUpdProduk.Enabled = true;
                ddlUpdArticle.Enabled = false;
                ddlUpdColor.Enabled = false;
                ddlUpdSize.Enabled = false;

                ModalUpdate.Show();
            }
            else if (value == 3) //New Article, setelah pilih Produk
            {
                List<MS_KDBRG> allListKdbrg = new List<MS_KDBRG>();
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                allListKdbrg = kdbrgDA.getMsKdbrgArticleARTICLE(string.Format(" where FBRAND = '{0}' and FPRODUK = '{1}' GROUP BY FART_DESC", ddlUpdBrand.SelectedValue, ddlUpdProduk.SelectedValue));
                kdbrg.FART_DESC = "-Pilih Produk-";
                allListKdbrg.Insert(0, kdbrg);

                ddlUpdArticle.DataSource = allListKdbrg;
                ddlUpdArticle.DataBind();

                ddlUpdBrand.Enabled = true;
                ddlUpdProduk.Enabled = true;
                ddlUpdArticle.Enabled = true;
                ddlUpdColor.Enabled = false;
                ddlUpdSize.Enabled = false;

                ModalUpdate.Show();
            }
            else if (value == 4) //New Color, setelah pilih Article
            {
                List<MS_KDBRG> allListKdbrg = new List<MS_KDBRG>();
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                allListKdbrg = kdbrgDA.getMsKdbrgArticleCOLOR(
                    string.Format(" where FBRAND = '{0}' and FPRODUK = '{1}' and FART_DESC = '{2}' GROUP BY FCOL_DESC", 
                    ddlUpdBrand.SelectedValue, ddlUpdProduk.SelectedValue, ddlUpdArticle.SelectedValue));

                kdbrg.FCOL_DESC = "-Pilih Produk-";
                allListKdbrg.Insert(0, kdbrg);

                ddlUpdColor.DataSource = allListKdbrg;
                ddlUpdColor.DataBind();

                ddlUpdBrand.Enabled = true;
                ddlUpdProduk.Enabled = true;
                ddlUpdArticle.Enabled = true;
                ddlUpdColor.Enabled = true;
                ddlUpdSize.Enabled = false;

                ModalUpdate.Show();
            }
            else if (value == 5) //New Size, setelah pilih Color
            {
                List<MS_KDBRG> allListKdbrg = new List<MS_KDBRG>();
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                allListKdbrg = kdbrgDA.getMsKdbrgArticleSIZE(
                    string.Format(" where FBRAND = '{0}' and FPRODUK = '{1}' and FART_DESC = '{2}' and FCOL_DESC = '{3}' GROUP BY FSIZE_DESC",
                    ddlUpdBrand.SelectedValue, ddlUpdProduk.SelectedValue, ddlUpdArticle.SelectedValue, ddlUpdColor.SelectedValue));

                kdbrg.FSIZE_DESC = "-Pilih Produk-";
                allListKdbrg.Insert(0, kdbrg);

                ddlUpdSize.DataSource = allListKdbrg;
                ddlUpdSize.DataBind();

                ddlUpdBrand.Enabled = true;
                ddlUpdProduk.Enabled = true;
                ddlUpdArticle.Enabled = true;
                ddlUpdColor.Enabled = true;
                ddlUpdSize.Enabled = true;

                ModalUpdate.Show();
            }
        }

        protected void gvMainCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "editrow")
                    {
                        clearForm();
                        hdnId.Value = id;
                        hdnIdKdbrg.Value = gvMain.Rows[rowIndex].Cells[4].Text;
                        hdnIdStock.Value = gvMain.Rows[rowIndex].Cells[5].Text;
                        tbUpdBarcode.Text = gvMain.Rows[rowIndex].Cells[7].Text;
                        tbUpdItemCode.Text = gvMain.Rows[rowIndex].Cells[8].Text;
                        tbUpdRak.Text = gvMain.Rows[rowIndex].Cells[6].Text;
                        tbUpdStock.Text = gvMain.Rows[rowIndex].Cells[12].Text;

                        lbUpdBarcode.Text = gvMain.Rows[rowIndex].Cells[7].Text;
                        lbUpdStock.Text = gvMain.Rows[rowIndex].Cells[12].Text;
                        lbUpdDiffStock.Text = gvMain.Rows[rowIndex].Cells[18].Text;
                        lbUpdArticle.Text = gvMain.Rows[rowIndex].Cells[9].Text;
                        lbUpdColor.Text = gvMain.Rows[rowIndex].Cells[10].Text;
                        lbUpdSize.Text = gvMain.Rows[rowIndex].Cells[11].Text;

                        ModalUpdate.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        STOCK_OPNAME_DA stockODA = new STOCK_OPNAME_DA();
                        Int64 idSO = Convert.ToInt64(id);
                        stockODA.deleteOneTempSODetail(idSO);
                        bindGrid();
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

        protected void gvMainPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvMainDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton imgDel = (ImageButton)e.Row.FindControl("imgDel");

                if (e.Row.Cells[2].Text == "0")
                {
                    imgEdit.Visible = false;
                    imgDel.Visible = false;
                }
            }
        }

        protected void gvPUPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvPU.PageIndex = e.NewPageIndex;
            bindPU();
        }

        protected void gvPUCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvPU.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnIKirimIDHeader.Value = id;
                        tbIKirimNoBukti.Text = gvPU.Rows[rowIndex].Cells[3].Text;
                        tbIKirimKe.Text = gvPU.Rows[rowIndex].Cells[5].Text;
                        tbIKirimWaktuKirim.Text = gvPU.Rows[rowIndex].Cells[6].Text;

                        bindIKirim();
                    }
                }
            }
            catch (Exception ex)
            {
                DivMessageHeader.InnerText = "Error : " + ex.Message;
                DivMessageHeader.Attributes["class"] = "error";
                DivMessageHeader.Visible = true;
            }
        }

        protected void gvIKirimPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvIKirim.PageIndex = e.NewPageIndex;
            bindIKirim();
        }

        protected void gvTempCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvTemp.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "editrow")
                    {
                        clearForm();
                        hdnTEditID.Value = id;
                        hdnTEditIDKdbrg.Value = gvTemp.Rows[rowIndex].Cells[4].Text;

                        tbTEditBarcode.Text = gvTemp.Rows[rowIndex].Cells[6].Text;
                        tbTEditBrand.Text = gvTemp.Rows[rowIndex].Cells[8].Text;
                        tbTEditGroup.Text = gvTemp.Rows[rowIndex].Cells[9].Text;
                        tbTEditArticle.Text = gvTemp.Rows[rowIndex].Cells[10].Text;
                        tbTEditWarna.Text = gvTemp.Rows[rowIndex].Cells[11].Text;
                        tbTEditSize.Text = gvTemp.Rows[rowIndex].Cells[12].Text;
                        tbTEditRak.Text = gvTemp.Rows[rowIndex].Cells[5].Text;
                        tbTEditStock.Text = gvTemp.Rows[rowIndex].Cells[13].Text;

                        lbTEditBarcode.Text = gvTemp.Rows[rowIndex].Cells[6].Text;
                        lbTEditItemCode.Text = gvTemp.Rows[rowIndex].Cells[7].Text;
                        lbTEditStock.Text = gvTemp.Rows[rowIndex].Cells[13].Text;

                        ModalTempEdit.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        STOCK_OPNAME_DA stockODA = new STOCK_OPNAME_DA();
                        Int64 idSO = Convert.ToInt64(id);
                        stockODA.deleteOneTempSODetail(idSO);
                        bindTemp();
                    }
                }
            }
            catch (Exception ex)
            {
                DivMessageHeader.InnerText = "Error : " + ex.Message;
                DivMessageHeader.Attributes["class"] = "error";
                DivMessageHeader.Visible = true;
            }
        }

        protected void btnTrfStockClick(object sender, EventArgs e)
        {
            bindPU();
        }

        protected void btnIKirimSearchClick(object sender, EventArgs e)
        {
            bindIKirim();
        }

        protected void btnViewTempSODetailClick(object sender, EventArgs e)
        {
            bindTemp();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnUploadClick(object sender, EventArgs e)
        {
            firstDelete();
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".txt")
                {
                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();
                    MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                    //kdbrgList = kdbrgDA.getMsKdbrg("");
                    kdbrgList = kdbrgDA.getMsKdbrgArticle("");
                    bool ret = readData(source, kdbrgList);
                    File.Delete(source);

                    if (ret)
                    {
                        bindGrid();

                        DivMessage.InnerText = "Upload Berhasil";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "File Harus Bertipe txt.";
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

        protected void btnAddClick(object sender, EventArgs e)
        {
            clearForm();
            lblTitleSubPage.Text = "Add New Stock Opname";
            bindBrand(1);
            ModalUpdate.Show();
        }

        protected void btnUpdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string idKdbrg = string.Empty;
                ////string idStock = string.Empty;
                string itemCode = string.Empty;
                string artDesc = string.Empty;
                string warna = string.Empty;
                string size = string.Empty;
                ////int bedaStock = Convert.ToInt32(tbUpdStock.Text);
                bool check = true;
                if (tbUpdBarcode.Text == lbUpdBarcode.Text)
                {
                    idKdbrg = hdnIdKdbrg.Value;
                    ////idStock = hdnIdStock.Value;
                    itemCode = tbUpdItemCode.Text;
                    artDesc = lbUpdArticle.Text;
                    warna = lbUpdColor.Text;
                    size = lbUpdSize.Text;
                }
                else
                {
                    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                    MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                    List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();

                    //kdbrgList = kdbrgDA.getMsKdbrg(string.Format(" where BARCODE = '{0}'", tbUpdBarcode.Text));
                    kdbrgList = kdbrgDA.getMsKdbrgArticle(string.Format(" where BARCODE = '{0}'", tbUpdBarcode.Text));
                    if (kdbrgList.Count > 0)
                    {
                        idKdbrg = kdbrgList.First().ID.ToString();
                        itemCode = kdbrgList.First().ITEM_CODE;
                        artDesc = kdbrgList.First().ART_DESC;
                        warna = kdbrgList.First().FCOL_DESC;
                        size = kdbrgList.First().FSIZE_DESC;

                        List<MS_STOCK> stockList = new List<MS_STOCK>();
                        //stockList = stockDA.getStock(string.Format(" where KODE = '{0}' and BARCODE = '{1}'", tbKodeWarehouse.Text, kdbrgList.First().BARCODE));

                        DateTime dt = SqlDateTime.MaxValue.Value;
                        string tanggal = tbTglSO.Text;
                        if (!string.IsNullOrEmpty(tanggal))
                        {
                            DateTime.TryParseExact(tanggal, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                        }

                        stockList = stockDA.getStockCutOff(string.Format(" where KODE = '{0}' and BARCODE = '{1}'", tbKodeWarehouse.Text, kdbrgList.First().BARCODE), 
                            dt, tbKodeWarehouse.Text);
                        ////idStock = stockList.Count == 0 ? "0" : stockList.First().ID.ToString();
                        ////bedaStock = stockList.Count == 0 ? bedaStock - 0 : bedaStock - stockList.First().STOCK;
                    }
                    else
                    {
                        DivUpdMessage.InnerText = "Barcode tidak di temukan!";
                        DivUpdMessage.Attributes["class"] = "warning";
                        DivUpdMessage.Visible = true;
                        check = false;
                        ModalUpdate.Show();
                    }
                }
                if (check)
                {
                    STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
                    TEMP_SO_DETAIL soDetail = new TEMP_SO_DETAIL();

                    if (lblTitleSubPage.Text.ToLower().Contains("add"))
                    {
                        soDetail.ID_HEADER = Convert.ToInt64(hdnIdHeader.Value);
                        soDetail.ID_KDBRG = Convert.ToInt64(idKdbrg);
                        ////soDetail.ID_STOCK = Convert.ToInt64(idStock);
                        soDetail.NO_BUKTI = tbNoBukti.Text;
                        soDetail.RAK = tbUpdRak.Text;
                        soDetail.ITEM_CODE = itemCode;
                        soDetail.BARCODE = tbUpdBarcode.Text;
                        ////soDetail.DIFF_STOCK = bedaStock;
                        soDetail.BRAND = ddlUpdBrand.SelectedValue;
                        soDetail.PRODUK = ddlUpdProduk.SelectedValue;
                        soDetail.ART_DESC = artDesc;
                        soDetail.WARNA = warna;
                        soDetail.SIZE = size;
                        soDetail.STOCK = Convert.ToInt32(tbUpdStock.Text == "" ? "0" : tbUpdStock.Text);
                        soDetail.CREATED_BY = Session["UName"].ToString();

                        stockOpnameDA.insertTempSoDetail(soDetail);

                        DivMessage.InnerText = "Penambahan Data Stock Berhasil";
                    }
                    else
                    {
                        soDetail.ID = Convert.ToInt64(hdnId.Value);
                        soDetail.ID_KDBRG = Convert.ToInt64(idKdbrg);
                        ////soDetail.ID_STOCK = Convert.ToInt64(idStock);
                        soDetail.RAK = tbUpdRak.Text;
                        soDetail.ITEM_CODE = itemCode;
                        soDetail.BARCODE = tbUpdBarcode.Text;
                        soDetail.ART_DESC = artDesc;
                        soDetail.WARNA = warna;
                        soDetail.SIZE = size;
                        soDetail.STOCK = Convert.ToInt32(tbUpdStock.Text == "" ? "0" : tbUpdStock.Text);
                        soDetail.DIFF_STOCK = int.Parse(lbUpdDiffStock.Text) + soDetail.STOCK - int.Parse(lbUpdStock.Text);

                        stockOpnameDA.updateTempSODetail(soDetail);

                        DivMessage.InnerText = "Update Data Stock Berhasil";
                    }

                    bindGrid();

                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                DivUpdMessage.InnerText = "Error : " + ex.Message;
                DivUpdMessage.Attributes["class"] = "error";
                DivUpdMessage.Visible = true;
                ModalUpdate.Show();
            }
        }

        protected void btnPrintByRakClick(object sender, EventArgs e)
        {
            createExcelByRak();
        }

        protected void btnDoneClick(object sender, EventArgs e)
        {
            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            Int64 idHeaderTemp = Convert.ToInt64(hdnIdHeader.Value);
            string idOPHeader = stockOpnameDA.insertOPHeaderRetID(idHeaderTemp);
            if (!(idOPHeader.Contains("ERROR")))
            {
                stockOpnameDA.insertOPDetail(Convert.ToInt64(idOPHeader), idHeaderTemp);

                string idHeader = stockOpnameDA.insertSOHeaderRetID(Convert.ToInt64(hdnIdHeader.Value));
                if (!(idHeader.Contains("ERROR")))
                {
                    string createdBy = Session["UName"].ToString();
                    string kode = tbKodeWarehouse.Text;
                    string warehouse = tbNamaWarehouse.Text;
                    string noBukti = tbNoBukti.Text;
                    DateTime dt = SqlDateTime.MaxValue.Value;
                    string tanggal = tbTglSO.Text;
                    if (!string.IsNullOrEmpty(tanggal))
                    {
                        DateTime.TryParseExact(tanggal, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    }

                    string check = stockOpnameDA.insertSODetail(Convert.ToInt64(idHeader), idHeaderTemp, createdBy, kode, warehouse, noBukti, dt);
                    if (!(check.Contains("ERROR")))
                    {

                        clearForm();
                        firstDelete();
                        panelCompare.Visible = false;

                        DivMessage.InnerText = "Stock Opname Berhasil";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = "Insert Failed 3 : ID Header : " + idHeader + " | " + check;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Insert Failed 2 : " + idHeader;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Insert Failed 1 : " + idOPHeader;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void btnTEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = true;
                string idKdbrg = string.Empty;
                ////string idStock = string.Empty;
                string itemCode = string.Empty;
                string brand = string.Empty;
                string group = string.Empty;
                string artDesc = string.Empty;
                string warna = string.Empty;
                string size = string.Empty;
                if ((tbTEditBarcode.Text == lbTEditBarcode.Text) && (tbTEditStock.Text == lbTEditStock.Text))
                {
                    check = false;
                }
                else if (tbUpdBarcode.Text == lbUpdBarcode.Text)
                {
                    idKdbrg = hdnTEditIDKdbrg.Value;
                    ////idStock = hdnIdStock.Value;
                    itemCode = lbTEditItemCode.Text;
                    brand = tbTEditBrand.Text;
                    group = tbTEditGroup.Text;
                    artDesc = tbTEditArticle.Text;
                    warna = tbTEditWarna.Text;
                    size = tbTEditSize.Text;
                }
                else
                {
                    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                    MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                    List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();

                    //kdbrgList = kdbrgDA.getMsKdbrg(string.Format(" where BARCODE = '{0}'", tbUpdBarcode.Text));
                    kdbrgList = kdbrgDA.getMsKdbrgArticle(string.Format(" where BARCODE = '{0}'", tbTEditBarcode.Text));
                    if (kdbrgList.Count > 0)
                    {
                        idKdbrg = kdbrgList.First().ID.ToString();
                        itemCode = kdbrgList.First().ITEM_CODE;
                        brand = kdbrgList.First().FBRAND;
                        group = kdbrgList.First().FGROUP;
                        artDesc = kdbrgList.First().ART_DESC;
                        warna = kdbrgList.First().FCOL_DESC;
                        size = kdbrgList.First().FSIZE_DESC;
                    }
                    else
                    {
                        DivUpdMessage.InnerText = "Barcode tidak di temukan!";
                        DivUpdMessage.Attributes["class"] = "warning";
                        DivUpdMessage.Visible = true;
                        check = false;
                        ModalUpdate.Show();
                    }
                }
                if (check)
                {
                    STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
                    TEMP_SO_DETAIL soDetail = new TEMP_SO_DETAIL();

                    soDetail.ID = Convert.ToInt64(hdnTEditID.Value);
                    soDetail.ID_KDBRG = Convert.ToInt64(idKdbrg);
                    ////soDetail.ID_STOCK = Convert.ToInt64(idStock);
                    soDetail.RAK = tbTEditRak.Text;
                    soDetail.ITEM_CODE = itemCode;
                    soDetail.BARCODE = tbTEditBarcode.Text;
                    soDetail.ART_DESC = artDesc;
                    soDetail.WARNA = warna;
                    soDetail.SIZE = size;
                    soDetail.FGROUP = group;
                    soDetail.BRAND = brand;
                    soDetail.STOCK = Convert.ToInt32(tbTEditStock.Text == "" ? "0" : tbTEditStock.Text);

                    stockOpnameDA.updateTempSODetail(soDetail);

                    DivMessage.InnerText = "Update Data Stock Berhasil";

                    bindTemp();
                }
            }
            catch
            {
                throw;
            }
        }

        protected void ddlUpdBrandChange(object sender, EventArgs e)
        {
            if (ddlUpdBrand.SelectedIndex > 0)
            {
                bindBrand(2);
            }
            else
            {
                bindBrand(1);
            }
        }

        protected void ddlUpdProdukChange(object sender, EventArgs e)
        {
            if (ddlUpdProduk.SelectedIndex > 0)
            {
                bindBrand(3);
            }
            else
            {
                bindBrand(2);
            }
        }

        protected void ddlUpdArticleChange(object sender, EventArgs e)
        {
            if (ddlUpdArticle.SelectedIndex > 0)
            {
                bindBrand(4);
            }
            else
            {
                bindBrand(3);
            }
        }

        protected void ddlUpdColorChange(object sender, EventArgs e)
        {
            if (ddlUpdColor.SelectedIndex > 0)
            {
                bindBrand(5);
            }
            else
            {
                bindBrand(4);
            }
        }

        protected void ddlUpdSizeChange(object sender, EventArgs e)
        {
            if (ddlUpdSize.SelectedIndex > 0)
            {
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                MS_KDBRG kdbrg = new MS_KDBRG();

                kdbrg = kdbrgDA.getMsKdbrgArticle(
                    string.Format(" where FBRAND = '{0}' and FPRODUK = '{1}' and FART_DESC = '{2}' and FCOL_DESC = '{3}' and FSIZE_DESC = '{4}'",
                    ddlUpdBrand.SelectedValue, ddlUpdProduk.SelectedValue, ddlUpdArticle.SelectedValue, ddlUpdColor.SelectedValue, ddlUpdSize.SelectedValue)).First();

                tbUpdBarcode.Text = kdbrg.BARCODE;
                tbUpdItemCode.Text = kdbrg.ITEM_CODE;

                ModalUpdate.Show();
            }
            else
            {
                bindBrand(5);
            }
        }

        protected void printStock(object sender, CommandEventArgs e)
        {
            createExcel();
        }

        protected bool readData(string source, List<MS_KDBRG> listKDBRG)
        {
            bool a = true;
            int i = 0;
            try
            {
                string path = string.Empty;
                string tanggalSO = string.Empty;
                string tanggal = string.Empty;
                string newID = string.Empty;
                string rak = string.Empty;
                string[] strdata = new string[5];
                //StreamReader objReader;
                TextReader txtReader;
                string sLine1 = "";
                string createdBy = Session["UName"].ToString();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
                List<MS_STOCK> listStock = new List<MS_STOCK>();
                string noBukti = string.Empty;

                path = source;
                //objReader = new StreamReader(path);
                //txtReader = new StreamReader(path);

                //listStock = stockDA.getStock(string.Format(" where KODE = '{0}'", ddlStoreUpload.SelectedValue));
                //Added By VAV
                DataTable dtTemp = new DataTable();
                dtTemp = CreateDataTable(typeof(TEMP_SO_DETAIL));
                using (txtReader = new StreamReader(path))
                {
                    while (txtReader.Peek() != -1)
                    {
                        sLine1 = txtReader.ReadLine();
                        if (i > 0)//Insert Detail
                        {
                            if (sLine1.Contains(",") && !(sLine1 == ""))//Detail
                            {
                                string[] line = sLine1.Split(',');
                                ////int stock_akhir_old = 0;
                                int stockPDT = int.Parse(line[1]);
                                ////int bedaStock = 0;
                                ////List<MS_STOCK> stockListDetail = new List<MS_STOCK>();
                                ////stockListDetail = listStock.Where(items => items.BARCODE == line[0]).ToList<MS_STOCK>();

                                List<MS_KDBRG> kdbrgListSelected = new List<MS_KDBRG>();
                                kdbrgListSelected = listKDBRG.Where(itemk => itemk.BARCODE == line[0]).ToList<MS_KDBRG>();

                                ////stock_akhir_old = stockListDetail.Count == 0 ? 0 : stockListDetail.First().STOCK;
                                ////bedaStock = stockPDT - stock_akhir_old;

                                //Insert into Temp_SO_Detail
                                TEMP_SO_DETAIL soDetail = new TEMP_SO_DETAIL();
                                soDetail.ID_HEADER = Convert.ToInt64(newID);
                                soDetail.ID_KDBRG = kdbrgListSelected.Count == 0 ? 0 : kdbrgListSelected.First().ID;
                                ////soDetail.ID_STOCK = stockListDetail.Count == 0 ? 0 : stockListDetail.First().ID;
                                soDetail.NO_BUKTI = noBukti;
                                soDetail.RAK = rak;
                                soDetail.ITEM_CODE = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().ITEM_CODE;
                                soDetail.BARCODE = line[0];
                                soDetail.BRAND = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().FBRAND;
                                soDetail.PRODUK = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().FPRODUK;
                                soDetail.ART_DESC = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().FART_DESC;
                                soDetail.WARNA = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().FCOL_DESC;
                                soDetail.SIZE = kdbrgListSelected.Count == 0 ? "" : kdbrgListSelected.First().FSIZE_DESC;
                                ////soDetail.DIFF_STOCK = bedaStock;
                                soDetail.STOCK = stockPDT;
                                soDetail.CREATED_BY = Session["UName"].ToString();
                             
                                DataRow newRow = dtTemp.Rows.Add(); 
                                newRow["ID_HEADER"] = soDetail.ID_HEADER;
                                newRow["ID_KDBRG"] = soDetail.ID_KDBRG;
                                newRow["NO_BUKTI"] = soDetail.NO_BUKTI;
                                newRow["RAK"] = soDetail.RAK;
                                newRow["ITEM_CODE"] = soDetail.ITEM_CODE;
                                newRow["BARCODE"] = soDetail.BARCODE;
                                newRow["BRAND"] = soDetail.BRAND;
                                newRow["PRODUK"] = soDetail.PRODUK;
                                newRow["ART_DESC"] = soDetail.ART_DESC;
                                newRow["WARNA"] = soDetail.WARNA;
                                newRow["SIZE"] = soDetail.SIZE;
                                newRow["STOCK"] = soDetail.STOCK;
                                newRow["CREATED_BY"] = soDetail.CREATED_BY;

                               
                                //  stockOpnameDA.insertTempSoDetail(soDetail); 'REMARK BY VAV
                            }
                            else if (!(sLine1 == ""))//RAK
                            {
                                rak = sLine1;
                            }
                        }
                        else //Insert Header
                        {
                          
                            DateTime tglTrans = DateTime.Now;
                            
                            string date = tbDate.Text.ToString();
                            if (!string.IsNullOrEmpty(date))
                            {
                                DateTime.TryParseExact(date, "dd-MM-yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                            }
                            String tglSO = string.Format("{0:ddMMyy}", tglTrans);
                            //DateTime.TryParseExact(tanggal, "ddMMyyyy",
                            //    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglSO);
                            DateTime dt = SqlDateTime.MaxValue.Value;
                            string[] line = sLine1.Split(',');
                            //tanggalSO = line[1];
                            //tanggal = tanggalSO.Remove(4) + "20" + tanggalSO.Remove(0, 4);
                            //if (!string.IsNullOrEmpty(tanggal))
                            //{
                            //    DateTime.TryParseExact(tanggal, "ddMMyyyy",
                            //    CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            //}
                            tbTglSO.Text = tbDate.Text;// String.Format("{0:dd-MM-yyyy}", dt);

                            TEMP_SO tempSO = new TEMP_SO();
                            tempSO.KE = ddlStoreUpload.SelectedItem.Text;
                            tempSO.KODE_KE = ddlStoreUpload.SelectedValue;
                            tempSO.UPLOAD_TIME = tglTrans;// dt;
                            tempSO.STATUS = "Done";
                            tempSO.CREATED_BY = Session["UName"].ToString();
                            newID = stockOpnameDA.insertTempSORetID(tempSO);

                            noBukti = "SO" + tglSO + (newID.Length > 5 ? newID.Remove(0, newID.Length - 6) : newID.PadLeft(6, '0'));
                            tempSO.NO_BUKTI = noBukti;
                            tempSO.ID = Convert.ToInt64(newID);
                            stockOpnameDA.updateNoBuktiTempSO(tempSO);

                            listStock = stockDA.getStockCutOff("", dt, ddlStoreUpload.SelectedValue);
                        }
                        i++;
                    }
                }
                //Added By VAV
                SqlConnection Connection = new SqlConnection(conString);
                if (Connection.State==ConnectionState.Closed)
                {
                    Connection.Open();
                }
               
                var bulkCopy = new SqlBulkCopy(Connection)
                {
                    DestinationTableName = "[dbo].[TEMP_SO_DETAIL]",
                    BatchSize = 1000
                }; 

                bulkCopy.ColumnMappings.Add("ID_HEADER", "ID_HEADER");
                bulkCopy.ColumnMappings.Add("ID_KDBRG", "ID_KDBRG");
                bulkCopy.ColumnMappings.Add("NO_BUKTI", "NO_BUKTI");
                bulkCopy.ColumnMappings.Add("RAK", "RAK");
                bulkCopy.ColumnMappings.Add("ITEM_CODE", "ITEM_CODE");
                bulkCopy.ColumnMappings.Add("BARCODE", "BARCODE");
                bulkCopy.ColumnMappings.Add("BRAND", "BRAND");
                bulkCopy.ColumnMappings.Add("PRODUK", "PRODUK");
                bulkCopy.ColumnMappings.Add("ART_DESC", "ART_DESC");
                bulkCopy.ColumnMappings.Add("WARNA", "WARNA");
                bulkCopy.ColumnMappings.Add("SIZE", "SIZE");
                bulkCopy.ColumnMappings.Add("STOCK", "STOCK");
                bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                
                bulkCopy.WriteToServer(dtTemp);
                bulkCopy.Close();

                tbNamaWarehouse.Text = ddlStoreUpload.SelectedItem.Text;
                tbKodeWarehouse.Text = ddlStoreUpload.SelectedValue;
                tbNoBukti.Text = noBukti;
                hdnIdHeader.Value = newID;
                panelCompare.Visible = true;
                txtReader.Close();
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Read File Failed : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;

                a = false;
            }

            return a;
        }

        protected void createExcel()
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView db = new GridView();

            //DataSet searchData = cd.SearchData(tabel, field, "");
            //DataSet searchData = new DataSet();

            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            List<EXCELL_ALL_RAK_TEMP_SO> soDetail = new List<EXCELL_ALL_RAK_TEMP_SO>();
            soDetail = stockOpnameDA.getTempSO(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));
            string strStyle = @"<style>.text { mso-number-format:\@; } runat=server </style>";
            hw.WriteLine(strStyle);

            Response.Clear();
            db.DataSource = soDetail;
            db.DataBind();

            for (int i = 0; i < soDetail.Count; i++)
            {
                db.Rows[i].Cells[0].Attributes.Add("class", "text");
                db.Rows[i].Cells[1].Attributes.Add("class", "numeric");
            }

            db.RenderControl(hw);
            Response.AddHeader("content-disposition", "attachment;filename=AllRak.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write("AllRak");
            Response.Write(" Print Date : ");
            Response.Write(DateTime.Now.ToString("dd-MM-yyyy-hh:mm:ss"));
            Response.Write(tw.ToString());
            Response.Write("Total Qty : " + tbAllStock.Text);
            Response.End();
        }

        protected void createExcelByRak()
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView db = new GridView();

            //DataSet searchData = cd.SearchData(tabel, field, "");
            //DataSet searchData = new DataSet();

            STOCK_OPNAME_DA stockOpnameDA = new STOCK_OPNAME_DA();
            List<EXCELL_RAK_TEMP_SO> soDetail = new List<EXCELL_RAK_TEMP_SO>();
            soDetail = stockOpnameDA.getTempSOByRak(string.Format(" where CREATED_BY = '{0}' and RAK = '{1}'", Session["UName"].ToString(), ddlRakSO.SelectedValue));
            string strStyle = @"<style>.text { mso-number-format:\@; } runat=server </style>";
            hw.WriteLine(strStyle);

            Response.Clear();
            db.DataSource = soDetail;
            db.DataBind();

            int total = 0;
            for (int i = 0; i < soDetail.Count; i++)
            {
                db.Rows[i].Cells[0].Attributes.Add("class", "text");
                db.Rows[i].Cells[1].Attributes.Add("class", "text");
                db.Rows[i].Cells[2].Attributes.Add("class", "text");
                db.Rows[i].Cells[3].Attributes.Add("class", "text");
                db.Rows[i].Cells[4].Attributes.Add("class", "text");
                db.Rows[i].Cells[5].Attributes.Add("class", "text");
                db.Rows[i].Cells[6].Attributes.Add("class", "numeric");
                total = total + soDetail[i].STOCK;
            }

            db.RenderControl(hw);
            Response.AddHeader("content-disposition", "attachment;filename=RAK_" + ddlRakSO.SelectedValue + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write("RAK :" + ddlRakSO.SelectedValue);
            Response.Write(" Print Date : ");
            Response.Write(DateTime.Now.ToString("dd-MM-yyyy-hh:mm:ss"));
            Response.Write(tw.ToString());
            Response.Write("Total Qty : " + total.ToString());
            Response.End();
        }

        protected void clearForm()
        {
            hdnId.Value = "";
            hdnIdKdbrg.Value = "";
            hdnIdStock.Value = "";

            tbUpdBarcode.Text = "";
            tbUpdItemCode.Text = "";
            tbUpdStock.Text = "";
            tbUpdRak.Text = "";

            lblTitleSubPage.Text = "Update Stock/Barcode";
            lbUpdBarcode.Text = "";
        }
        
        //Tambahan Untuk Haspus Data Stock Opname
        protected void bindStockOPData()
        {
            STOCK_OPNAME_DA stockOpanameDA = new STOCK_OPNAME_DA();
            TR_OP_HEADER TRHeader = new TR_OP_HEADER();

            string where = tbPUSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%'", ddlPUSearch.SelectedValue, tbPUSearch.Text);
            //TRHeader = stockOpanameDA.getOPHeader(where);
            //pnlDataSearchStockOP.Visible = true;
            //gvPU.DataSource = listTRHeader;
            //gvPU.DataBind();
            //ModalPopupTrfHeader.Show();
        }

        protected void gvSockOPData_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvSockOPData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void DownloadFormatSO_Click(object sender, EventArgs e)
        {
            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Format_Stock_Opname.txt");
            Response.TransmitFile(Server.MapPath("~/Upload/Format_Stock_Opname.txt"));
            Response.End();

        }
        public static DataTable CreateDataTable(Type temp)
        {
            DataTable return_Datatable = new DataTable();
            foreach (PropertyInfo info in temp.GetProperties())
            {
                return_Datatable.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }


            return return_Datatable;
        }
    }
}