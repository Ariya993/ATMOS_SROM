using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace ATMOS_SROM.Warehouse
{
    public partial class Warehouse : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        string noBuktiGlobal = string.Empty;
        int ttlTrfQty;
        string ttlTrfQtyFromExcel = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
                bindStore();
                tbDownloadDate.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Now);
                ScriptManager1.RegisterPostBackControl(this.btnDownloadStock);
                //ScriptManager1.RegisterPostBackControl(this.btnUpdStock);
            }
        }

        protected void bindGrid()
        {
            string where = tbSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            List<MS_STOCK> listStock = new List<MS_STOCK>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            if (sULevel.ToLower() == "sales" || sULevel.ToLower() == "store manager")
            {
                lbIdJudul.Text = "Penerimaan dan Pengiriman Barang di Showroom";
                //listStock = listStock.Where(item => item.WAREHOUSE.ToLower() == sStore.ToLower()).ToList<MS_STOCK>();
                where = where == "" ? " where KODE = '" + sKode + "'" : where + " and KODE = '" + sKode + "'";
                ddlSearch.Items[0].Enabled = false;
            }

            listStock = stockDA.getStock_1000(String.Format(where));

            gvMain.DataSource = listStock;
            gvMain.DataBind();

            List<MS_SHOWROOM> listShowRoom = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            MS_SHOWROOM show = new MS_SHOWROOM();
            listShowRoom = showDA.getShowRoom(" WHERE STATUS_SHOWROOM != 'SUP' AND STATUS = 'OPEN' ORDER BY 2");//(" where STATUS_SHOWROOM != 'SUP' AND (STATUS != 'CLOSED' OR STATUS != 'CLOSE')");
            //listShowRoom = showDA.getShowRoom(" where STATUS_SHOWROOM != 'SUP'");
            //if (!(session.ToLower() == "admin") && !(session.ToLower() == "warehouse"))
            //{
            //    show.SHOWROOM = "MAIN WAREHOUSE";
            //    listShowRoom.Add(show);
            //}

            MS_SHOWROOM showDel = new MS_SHOWROOM();
            showDel = listShowRoom.Where(itemd => itemd.SHOWROOM == sStore).FirstOrDefault();
            listShowRoom.Remove(showDel);

            ddlQTYKE.DataSource = listShowRoom;
            ddlQTYKE.DataTextField = "SHOWROOM";
            ddlQTYKE.DataValueField = "KODE";
            ddlQTYKE.DataBind();

            ddlStoreUpload.DataSource = listShowRoom;
            ddlStoreUpload.DataTextField = "SHOWROOM";
            ddlStoreUpload.DataValueField = "KODE";
            ddlStoreUpload.DataBind();

            if (sULevel.ToLower() == "sales" || sULevel.ToLower() == "ic head")//sULevel.ToLower() == "inventory")
            {
                btnCreateTrfStock.Visible = false;
                if (sULevel.ToLower() == "ic head")//(sULevel.ToLower() == "inventory")
                {
                    btnAdjustment.Visible = true;
                    btnUploadAdjustment.Visible = true;
                }
                else
                {
                    btnCreatePinjam.Visible = false;
                }
            }

            if (sULevel.ToLower() == "ic head")//(sULevel.ToLower() == "inventory")
            {
                gvMain.Columns[0].Visible = true;
                divTotal.Visible = false;
            }
            else if (sULevel.ToLower() == "buyer")
            {
                divBtnMain.Visible = false;
                btnTrfStock.Visible = false;
                btnListPeminjaman.Visible = false;
            }

            //Get Total Stock
            List<string> listTotalStock = new List<string>();
            listTotalStock = stockDA.getTotalStock(sKode);

            tbTotalStock.Text = listTotalStock.Count == 0 ? "0" : listTotalStock.First();
        }

        protected void bindPU()
        {
            string id = hdnIDPU.Value;
            string sLevel = Session["ULevel"].ToString();
            string where = tbPUSearch.Text == "" ? " where STATUS is not null" : String.Format("where STATUS is not null and {1} LIKE '%{2}%'", id, ddlPUSearch.SelectedValue, tbPUSearch.Text);
            List<TRF_STOCK_HEADER> listTrfStock = new List<TRF_STOCK_HEADER>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();

            if (!(sLevel.ToLower() == "admin counter"))
            {
                listTrfStock = stockDA.getTrfStockHeader(where);
                string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString().ToLower();

                if (Session["ULevel"].ToString().ToLower() == "sales" || Session["ULevel"].ToString().ToLower() == "store manager")
                {
                    listTrfStock = listTrfStock.Where(item => item.DARI.ToLower() == sStore || item.KE.ToLower() == sStore).ToList<TRF_STOCK_HEADER>();
                }
            }
            else
            {
                where = string.Format(" where STATUS is not null and (KODE_DARI in ( select KODE from MS_SHOWROOM where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS') " +
                    " or KODE_KE in ( select KODE from MS_SHOWROOM where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS')) and {0} like '%{1}%'", ddlPUSearch.SelectedValue, tbPUSearch.Text);
                listTrfStock = stockDA.getTrfStockHeader(where);
                string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString().ToLower();
                if (sStore != "head office")
                {
                    if (sLevel.ToLower() == "admin counter")
                    {

                        listTrfStock = listTrfStock.Where(item => item.KE.ToLower() == sStore).ToList<TRF_STOCK_HEADER>();
                    }
                }
            }

            listTrfStock = listTrfStock.OrderByDescending(item => item.ID).ToList<TRF_STOCK_HEADER>();
            gvPU.DataSource = listTrfStock;
            gvPU.DataBind();

            divPUMessage.Visible = false;
            ModalPopupTrfHeader.Show();
        }

        protected void bindNoBukti()
        {
            List<TRF_STOCK_DETAIL> listTrfStock = new List<TRF_STOCK_DETAIL>();
            List<TEMP_KDBRG> listTempKdbrg = new List<TEMP_KDBRG>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
            string user = Session["UName"].ToString();

            //string where = String.Format(" where ID_HEADER = {0}", hdnIDHeader.Value); //OLD
            string where = String.Format(" where CREATED_BY = '{0}'", user); //NEW

            //listTrfStock = stockDA.getTrfStock(where); //OLD
            listTempKdbrg = wareDA.getTempTrf(where); //NEW

            gvStock.DataSource = listTempKdbrg;
            gvStock.DataBind();

            if (btnStockGenerate.Enabled && panelCStockHeader.Visible)
            {
                btnStockGetItemCode.Enabled = false;
            }
            else
            {
                btnStockGetItemCode.Enabled = true;
            }

            DivMessage.Visible = false;
        }

        protected void bindItemCode()
        {
            List<MS_STOCK> listStock = new List<MS_STOCK>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            string kode = Session["UKode"].ToString(); //lbStockKodeDari.Text; //Session["UKode"].ToString();
            string where = tbItemSearch.Text == "" ? string.Format(" where KODE like '%{0}%'", kode) : string.Format(" where KODE like '%{0}%' and {1} like '%{2}%'", kode, ddlItemSearch.SelectedValue, tbItemSearch.Text);
            listStock = stockDA.getStock(where);

            gvItem.DataSource = listStock;
            gvItem.DataBind();

            divItemMessage.Visible = false;

            ModalPopupItemCode.Show();
        }

        protected void bindItemKirim()
        {
            string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();

            List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

            string where = tbIKirimSearch.Text == "" ? string.Format(" where ID_HEADER = {0}", hdnIKirimIDHeader.Value) 
                : string.Format(" where ID_HEADER = {0} and {1} like '%{2}%'", hdnIKirimIDHeader.Value, ddlIKirimSearch.SelectedValue, tbIKirimSearch.Text);

            listStock = stockDA.getTrfStock(where);
            gvIKirim.DataSource = listStock;
            gvIKirim.DataBind();
            List<TRF_STOCK_DETAIL> listStockCek = new List<TRF_STOCK_DETAIL>();
            string NoBukti = "'" +tbIKirimNoBukti.Text+"'";
            listStockCek = listStock.Where(item => item.NO_BUKTI != tbIKirimNoBukti.Text).ToList<TRF_STOCK_DETAIL>();
            if (listStockCek.Count > 0)
            {
                lblinfoNoBukti.Text = "Nomor Bukti Header dan Detail Berbeda !";
                btnIKirimReceiveAll.Enabled = false;
            }
            else
            {

                lblinfoNoBukti.Text = "";
                btnIKirimReceiveAll.Enabled = true;
            }
            listShowroom = showDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS'");

            //if (tbIKirimWaktuTerima.Text == "" && (sStore.ToLower().Contains("warehouse") || sStore.ToLower().Contains("head office")))
            //{
            //    btnIKirimGenerateWaktuTerima.Visible = true;
            //}
            //else
            //{
            //    btnIKirimGenerateWaktuTerima.Visible = false;
            //}

            btnIKirimGenerateWaktuTerima.Visible = false;
            //tbIKirimWaktuTerima.ReadOnly = tbIKirimWaktuTerima.Text == "" ? false : true;
            CalendarWaktuTerima.Enabled = tbIKirimWaktuTerima.Text == "" ? true : false;
            lbIKirimWaktuTerima.Visible = false;

            //gvIKirim.Columns[0].Visible = tbIKirimDari.Text.ToLower() == sStore.ToLower() ? false : true;
            //if (sULevel.ToLower() == "admin counter")
            //{
            //    gvIKirim.Columns[0].Visible = listShowroom.Where(item => item.KODE == tbIKirimKodeKe.Text).Count() > 0 && sULevel.ToLower() == "admin counter" ? true : false;
            //}

            btnIKirimReceiveAll.Visible = tbIKirimDari.Text.ToLower() == sStore.ToLower() ? false : true;
            if (sULevel.ToLower() == "admin counter")
            {
                btnIKirimReceiveAll.Visible = listShowroom.Where(item => item.KODE == tbIKirimKodeKe.Text).Count() > 0 && sULevel.ToLower() == "admin counter" ? true : false;
            }

            ModalItemKirim.Show();
        }

        protected void bindAdjustment()
        {
            List<TR_ADJUSTMENT> listAdjustment = new List<TR_ADJUSTMENT>();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            string where = string.Format(" where {0} like '%{1}%'", ddlLAdjSearch.SelectedValue, tbLAdjSearch.Text);
            listAdjustment = showDA.getTrAdjustment(where);

            gvLAdj.DataSource = listAdjustment;
            gvLAdj.DataBind();
            
            ModalListAdj.Show();
        }

        protected void bindPeminjaman()
        {
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
            List<TRF_PINJAM_HEADER> listPinjamHeader = new List<TRF_PINJAM_HEADER>();

            string where = string.Format(" where {0} like '%{1}%'", ddlLPSearch.SelectedValue, tbLPSearch.Text);
            listPinjamHeader = wareDA.getPinjamHeader(where);

            gvLPeminjaman.DataSource = listPinjamHeader;
            gvLPeminjaman.DataBind();

            ModalListPeminjaman.Show();
        }

        protected void bindPinjamDetail()
        {
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
            List<TRF_PINJAM_DETAIL> listPinjamDetail = new List<TRF_PINJAM_DETAIL>();
            string idHeader = hdnIdHeaderPinjam.Value;

            string where = string.Format(" where ID_HEADER = {0} and {1} like '%{2}%'", idHeader, ddlLPSearch.SelectedValue, tbLPSearch.Text);
            listPinjamDetail = wareDA.getPinjamDetail(where);

            gvPinjamDetail.DataSource = listPinjamDetail;
            gvPinjamDetail.DataBind();

            divDetailMessage.Visible = false;
            tbPDWaktuSelesai.ReadOnly = tbPDWaktuSelesai.Text == "" ? false : true;
            //tbPDWaktuSelesai.Enabled = tbPDWaktuSelesai.Text == "" ? true : false;

            ModalPeminjamanDetail.Show();
        }

        protected void bindStoreKirim()
        {
            string store = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            if (sLevel.ToLower() == "admin counter")
            {
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                MS_SHOWROOM showRoom = new MS_SHOWROOM();

                showRoom.SHOWROOM = "--Pilih Showroom--";
                showRoom.KODE = "";
                listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS'");
                listStore.Insert(0, showRoom);
                ddlQTYDari.DataSource = listStore;
                ddlQTYDari.DataBind();
            }
            else if (sLevel.ToLower() == "admin sales")
            {
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                MS_SHOWROOM showRoom = new MS_SHOWROOM();

                showRoom.SHOWROOM = "--Pilih Showroom--";
                showRoom.KODE = "";
                listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'FSS'");
                listStore.Insert(0, showRoom);
                ddlQTYDari.DataSource = listStore;
                ddlQTYDari.DataBind();
            }
            else if (sLevel.ToLower() == "supply control")
            {
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                MS_SHOWROOM showRoom = new MS_SHOWROOM();

                showRoom.SHOWROOM = "--Pilih Showroom--";
                showRoom.KODE = "";
                listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM != 'WHU'");
                listStore.Insert(0, showRoom);
                ddlQTYDari.DataSource = listStore;
                ddlQTYDari.DataBind();
            }
        }

        protected void bindStore()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            MS_SHOWROOM showRoom = new MS_SHOWROOM();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();

            showRoom.SHOWROOM = "--Pilih Showroom--";
            showRoom.KODE = "";
            string where = "";
            if (sLevel.ToLower() == "store manager" || sLevel.ToLower() == "sales")
            {
                where = " where STATUS = 'OPEN' and KODE = '" + sKode + "'";
                listStore = showRoomDA.getShowRoom(where);
            }
            else if (sLevel.ToLower() == "admin counter" && sKode.ToLower() != "ho-001")
            {
                where = " where STATUS = 'OPEN' and KODE = '" + sKode + "'";
                listStore = showRoomDA.getShowRoom(where);
            }
            else
            {
                where = " where STATUS = 'OPEN'";
                listStore = showRoomDA.getShowRoom(where);
                listStore.Insert(0, showRoom);
            }
            //string where = sLevel.ToLower() == "store manager" || sLevel.ToLower() == "sales" ? " where STATUS = 'OPEN' and KODE = '" + sKode + "'" : " where STATUS = 'OPEN'";
            
            ddlDownloadStore.DataSource = listStore;
            ddlDownloadStore.DataBind();
        }
        
        protected void gvItemPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItem.PageIndex = e.NewPageIndex;
            bindItemCode();
        }

        protected void gvItemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
                    string user = Session["UName"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvItem.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "saverow")
                    {
                        //List<TRF_STOCK_DETAIL> listTrfStock = new List<TRF_STOCK_DETAIL>();
                        List<TEMP_KDBRG> listTrfStock = new List<TEMP_KDBRG>();
                        MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();

                        //string where = String.Format(" where ID_HEADER = {0} and BARCODE like '{1}'", hdnIDHeader.Value, gvItem.Rows[rowIndex].Cells[3].Text.ToString());
                        //listTrfStock = stockDA.getTrfStock(where);

                        string where = String.Format(" where CREATED_BY = '{0}' and BARCODE = '{1}'", user, gvItem.Rows[rowIndex].Cells[3].Text.ToString()); //NEW

                        //listTrfStock = stockDA.getTrfStock(where); //OLD
                        listTrfStock = wareDA.getTempTrf(where); //NEW

                        if (listTrfStock.Count == 0)
                        {
                            hdnKRMIdKdbrg.Value = gvItem.Rows[rowIndex].Cells[11].Text.ToString();
                            hdnKRMStat.Value = "insert";
                            hdnKRMIdTemp.Value = "0";
                            tbKRMNoBukti.Text = tbStockNoBukti.Text;
                            tbKRMDari.Text = panelCStockHeader.Visible ? tbStockDari.Text : tbPinjamDari.Text;
                            tbKRMKe.Text = panelCStockHeader.Visible ? tbStockKe.Text : tbPinjamKe.Text;
                            tbKRMStock.Text = gvItem.Rows[rowIndex].Cells[10].Text.ToString();
                            tbKRMBarcode.Text = gvItem.Rows[rowIndex].Cells[3].Text.ToString();
                            tbKRMItemCode.Text = gvItem.Rows[rowIndex].Cells[4].Text.ToString();
                            tbKRMDesc.Text = gvItem.Rows[rowIndex].Cells[6].Text.ToString() + " " + gvItem.Rows[rowIndex].Cells[7].Text.ToString() 
                                + " " + gvItem.Rows[rowIndex].Cells[8].Text.ToString();
                            tbKRMQTYKirim.Text = "";
                            lbKRMBrand.Text = gvItem.Rows[rowIndex].Cells[5].Text.ToString();
                            lbKRMDesc.Text = gvItem.Rows[rowIndex].Cells[6].Text.ToString();
                            lbKRMColor.Text = gvItem.Rows[rowIndex].Cells[7].Text.ToString();
                            lbKRMSize.Text = gvItem.Rows[rowIndex].Cells[8].Text.ToString();
                            ModalInputQtyKirim.Show();
                        }
                        else
                        {
                            //bindItemCode();

                            //divItemMessage.InnerText = "Item Code sudah di pilih, silakan di hapus terlebih dahulu!";
                            //divItemMessage.Attributes["class"] = "warning";
                            //divItemMessage.Visible = true;
                            //ModalPopupItemCode.Show();

                            hdnKRMIdKdbrg.Value = listTrfStock.First().ID_KDBRG.ToString();
                            hdnKRMStat.Value = "update";
                            hdnKRMIdTemp.Value = listTrfStock.First().ID.ToString();
                            tbKRMNoBukti.Text = tbStockNoBukti.Text;
                            tbKRMDari.Text = panelCStockHeader.Visible ? tbStockDari.Text : tbPinjamDari.Text;
                            tbKRMKe.Text = panelCStockHeader.Visible ? tbStockKe.Text : tbPinjamKe.Text;
                            tbKRMStock.Text = gvItem.Rows[rowIndex].Cells[10].Text.ToString();
                            tbKRMBarcode.Text = gvItem.Rows[rowIndex].Cells[3].Text.ToString();
                            tbKRMItemCode.Text = gvItem.Rows[rowIndex].Cells[4].Text.ToString();
                            tbKRMDesc.Text = gvItem.Rows[rowIndex].Cells[6].Text.ToString() + " " + gvItem.Rows[rowIndex].Cells[7].Text.ToString()
                                + " " + gvItem.Rows[rowIndex].Cells[8].Text.ToString();
                            tbKRMQTYKirim.Text = "";

                            lbKRMBrand.Text = gvItem.Rows[rowIndex].Cells[6].Text.ToString();
                            lbKRMDesc.Text = gvItem.Rows[rowIndex].Cells[6].Text.ToString();
                            lbKRMColor.Text = gvItem.Rows[rowIndex].Cells[7].Text.ToString();
                            lbKRMSize.Text = gvItem.Rows[rowIndex].Cells[8].Text.ToString();
                            lbKRMKodeKe.Text = "";
                            ModalInputQtyKirim.Show();
                        }
                        divKRMMessage.Visible = false;
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
                        //hdnIdQTY.Value = id;
                        ////bindPU();
                        //tbQTYStockAwal.Text = gvMain.Rows[rowIndex].Cells[5].Text.ToString();
                        //tbQTYItemCode.Text = gvMain.Rows[rowIndex].Cells[4].Text.ToString();
                        //tbQTYDari.Text = gvMain.Rows[rowIndex].Cells[3].Text.ToString();
                        //tbQTYQTY.Text = "";
                        //ddlQTYKE.SelectedIndex = hdnIdTujuan.Value == "" ? 0 : Convert.ToInt32(hdnIdTujuan.Value);
                        //ModalPopupExtender1.Show();

                        hdnAdjIdStock.Value = id;
                        hdnAdjIdKdbrg.Value = gvMain.Rows[rowIndex].Cells[14].Text.ToString();
                        hdnAdjStock.Value = gvMain.Rows[rowIndex].Cells[5].Text.ToString();

                        tbAdjWarehouse.Text = gvMain.Rows[rowIndex].Cells[3].Text.ToString();
                        tbAdjBarcode.Text = gvMain.Rows[rowIndex].Cells[6].Text.ToString();
                        tbAdjItemCode.Text = gvMain.Rows[rowIndex].Cells[4].Text.ToString();
                        tbAdjDesc.Text = gvMain.Rows[rowIndex].Cells[10].Text.ToString() + " " + gvMain.Rows[rowIndex].Cells[11].Text.ToString() + " " + gvMain.Rows[rowIndex].Cells[12].Text.ToString();
                        //tbAdjStockAwal.Text = gvMain.Rows[rowIndex].Cells[5].Text.ToString();
                        tbAdjPerbedaan.Text = "";
                        tbAdjAlasan.Text = "";
                        
                        lbAdjKode.Text = gvMain.Rows[rowIndex].Cells[15].Text.ToString();
                        lbAdjBrand.Text = gvMain.Rows[rowIndex].Cells[7].Text.ToString();
                        lbAdjColor.Text = gvMain.Rows[rowIndex].Cells[11].Text.ToString();
                        lbAdjDesc.Text = gvMain.Rows[rowIndex].Cells[10].Text.ToString();
                        lbAdjSize.Text = gvMain.Rows[rowIndex].Cells[12].Text.ToString();

                        ModalAdjustment.Show();
                    }
                    else
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

        protected void gvMainPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        //protected void gvPUDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

        //        if (e.Row.Cells[8].Text == "Done")
        //        {
        //            imgEdit.Visible = false;
        //        }
        //    }
        //}

        protected void gvPUCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sUName = Session["UName"] == null ? "" : Session["UName"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvPU.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnIKirimIDHeader.Value = id;
                        tbIKirimNoBukti.Text = gvPU.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[3].Text;
                        tbIKirimDari.Text = gvPU.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[4].Text;
                        tbIKirimKodeDari.Text = gvPU.Rows[rowIndex].Cells[9].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[9].Text;
                        tbIKirimKodeKe.Text = gvPU.Rows[rowIndex].Cells[10].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[10].Text;
                        tbIKirimKe.Text = gvPU.Rows[rowIndex].Cells[5].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[5].Text;
                        tbIKirimWaktuKirim.Text = gvPU.Rows[rowIndex].Cells[6].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[6].Text;
                        tbIKirimWaktuTerima.Text = gvPU.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[7].Text;

                        bindItemKirim();
                    }
                    else if (e.CommandName.ToLower() == "printrow")
                    {
                        #region "OLD"
                        //MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        //MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

                        //List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();
                        //MS_SHOWROOM show = new MS_SHOWROOM();

                        //listStock = stockDA.getDetailTrfStock(" where ID_HEADER = '" + id + "' ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC");
                        //show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", gvPU.Rows[rowIndex].Cells[10].Text)).FirstOrDefault();

                        //string noBukti = gvPU.Rows[rowIndex].Cells[3].Text;
                        //Session["PDFIdHeader"] = id;
                        //Session["PDFNoBukti"] = noBukti;
                        //Session["PDFKode"] = gvPU.Rows[rowIndex].Cells[10].Text;
                        ////printNoBukti(noBukti, listStock, show);
                        ////ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../PopUp.aspx?noBukti=" + noBukti + "');", true);
                        ////string url = "PopUp.aspx?noBukti=" + noBukti;
                        ////string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
                        ////ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                        ////Response.Redirect("PopUp.aspx?noBukti=" + noBukti);

                        //printDeliveryNotes(noBukti, listStock, show);
                        #endregion
                        string noPO = gvPU.Rows[rowIndex].Cells[3].Text;
                        LoadRDLCData(id);
                        exportRpt(noPO);
                        bindPU();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        string noBukti = gvPU.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[3].Text;
                        string kode = gvPU.Rows[rowIndex].Cells[9].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[9].Text;
                        string wktTerima = gvPU.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[7].Text;
                        if (wktTerima == "")
                        {
                            LOGIN_DA loginDA = new LOGIN_DA();
                            string whereLock = " where NAME = 'lock'";
                            int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
                            int date = Convert.ToInt32(string.Format("{0:yyMM}", DateTime.Now));
                            List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
                            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                            listShowroom = showDA.getShowRoom(string.Format(" where KODE in('{0}')", kode));
                            if (cekLock(listShowroom.First().STATUS_SHOWROOM, listShowroom.Last().STATUS_SHOWROOM, Convert.ToString(date)))
                            {
                                string a = stockDA.cancelDelivery(noBukti, kode, sUName);

                                if (!a.Contains("ERROR"))
                                {
                                    bindPU();

                                    divPUMessage.InnerText =  "Berhasil Di Cancel!";
                                    divPUMessage.Attributes["class"] = "success";
                                    divPUMessage.Visible = true;
                                }
                                else
                                {
                                    divPUMessage.InnerText = a;
                                    divPUMessage.Attributes["class"] = "error";
                                    divPUMessage.Visible = true;
                                    ModalPopupTrfHeader.Show();
                                }
                            }
                            else
                            {
                                divPUMessage.InnerText = "Tanggal Sudah Di lock";
                                divPUMessage.Attributes["class"] = "error";
                                divPUMessage.Visible = true;
                                ModalPopupTrfHeader.Show();
                            }
                        }
                        else
                        {
                            divPUMessage.InnerText = "TS sudah di terima, Tidak dapat di batalkan";
                            divPUMessage.Attributes["class"] = "error";
                            divPUMessage.Visible = true;
                            ModalPopupTrfHeader.Show();
                        }
                    }
                    else
                    {
                        #region "OLD"
                        //MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        //MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

                        //List<TRF_STOCK_DETAIL> listStock = new List<TRF_STOCK_DETAIL>();
                        //MS_SHOWROOM show = new MS_SHOWROOM();

                        //listStock = stockDA.getDetailTrfStock(" where ID_HEADER = '" + id + "' ORDER BY FART_DESC, FCOL_DESC, FSIZE_DESC");
                        //show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", gvPU.Rows[rowIndex].Cells[10].Text)).FirstOrDefault();

                        //string noBukti = gvPU.Rows[rowIndex].Cells[3].Text;
                        //Session["PDFIdHeader"] = id;
                        //Session["PDFNoBukti"] = noBukti;
                        //Session["PDFKode"] = gvPU.Rows[rowIndex].Cells[10].Text;

                        //string tglKirim = gvPU.Rows[rowIndex].Cells[6].Text;
                        //string tglTerima = gvPU.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvPU.Rows[rowIndex].Cells[7].Text;
                        //printNoBukti(noBukti, listStock, show, tglKirim, tglTerima);

                        ////ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../PopUp.aspx?noBukti=" + noBukti + "');", true);
                        ////string url = "PopUp.aspx?noBukti=" + noBukti;
                        ////string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
                        ////ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                        ////Response.Redirect("PopUp.aspx?noBukti=" + noBukti);

                        //string pageurl = "~/Warehouse/PopUp.aspx?noBukti=" + noBukti;

                        ////printDeliveryNotes(noBukti, listStock, show);
                        //Response.Write("<script>");
                        ////Response.Write("window.open('" + pageurl + "','_blank')");
                        //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(pageurl)));
                        //Response.Write("</script>");
                        #endregion
                        string noPO = gvPU.Rows[rowIndex].Cells[3].Text;
                        DirectPrinPackingList(noPO, id);
                        //LoadRDLCDataPackingList(id);
                        //exportRptPackingList(noPO);
                        bindPU();
                        bindPU();
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
            gvPU.PageIndex = e.NewPageIndex;
            bindPU();
        }

        protected void gvPUDataBound(object sender, GridViewRowEventArgs e)
        {
            string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgPdf = (ImageButton)e.Row.FindControl("imgPdf");
                ImageButton imgPdfPL = (ImageButton)e.Row.FindControl("imgPdfPL");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                DateTime DtKirim = Convert.ToDateTime(e.Row.Cells[6].Text.ToString().ToLower());
                String YYMMDtKirim = DtKirim.ToString("yyMM");// DtKirim.Year.ToString() + DtKirim.Month.ToString();
                String YYMMDtNow = DateTime.Now.ToString("yyMM");// DtKirim.Year.ToString() + DtKirim.Month.ToString();
                ScriptManager1.RegisterPostBackControl(imgPdf);
                ScriptManager1.RegisterPostBackControl(imgPdfPL);

                if (sULevel.ToLower() == "inventory" && e.Row.Cells[8].Text.ToString().ToLower() == "sedang di kirim")
                {
                    //imgDelete.Visible = true;
                    if (YYMMDtKirim == YYMMDtNow)
                    {
                        imgDelete.Visible = true;
                    }
                    else
                    {
                        imgDelete.Visible = false;
                    }
                }
                else
                {
                    imgDelete.Visible = false;
                }
                //imgDelete.Visible = sULevel.ToLower() == "inventory" && e.Row.Cells[8].Text.ToString().ToLower() == "sedang di kirim" ? true : false;


                //AjaxControlToolkit.ToolkitScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(imgPdf);
                //AjaxControlToolkit.ToolkitScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(imgPdfPL);

            }
        }

        protected void gvStockCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
                    string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
                    string sName = Session["UName"] == null ? "" : Session["UName"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvStock.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        MS_STOCK stock = new MS_STOCK();

                        stock.BARCODE = gvStock.Rows[rowIndex].Cells[3].Text.ToString();
                        stock.KODE = sKode;
                        stock.UPDATED_BY = sName;
                        stock.STOCK = int.Parse(gvStock.Rows[rowIndex].Cells[5].Text);
                        string ret = stockDA.updateDataStockWithOutID(stock);

                        if (ret == "Berhasil!")
                        {
                            Int64 idBig = Convert.ToInt64(id);
                            stockDA.deleteTRFStock(idBig);
                        }
                        bindNoBukti();
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

        protected void gvStockPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvStock.PageIndex = e.NewPageIndex;
            bindNoBukti();
        }

        protected void gvIKirimDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                if (e.Row.Cells[7].Text.ToLower().Contains("done"))
                {
                    imgEdit.Visible = false;
                }
            }
        }

        protected void gvIKirimRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(tbIKirimWaktuTerima.Text == ""))
            {
                try
                {
                    if (e.CommandName.ToLower() != "page")
                    {
                        string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                        string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                        GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        int rowIndex = grv.RowIndex;
                        string id = gvIKirim.DataKeys[rowIndex]["ID"].ToString();

                        if (e.CommandName.ToLower() == "editrow")
                        {
                            hdnTRMIdTrfStock.Value = id;
                            tbTRMNoBukti.Text = tbIKirimNoBukti.Text;
                            tbTRMItemCode.Text = gvIKirim.Rows[rowIndex].Cells[3].Text;
                            tbTRMBarcode.Text = gvIKirim.Rows[rowIndex].Cells[8].Text;
                            tbTRMDari.Text = tbIKirimDari.Text;
                            tbTRMKe.Text = tbIKirimKe.Text;
                            tbTRMQTYKirim.Text = gvIKirim.Rows[rowIndex].Cells[4].Text;
                            tbTRMQTYTerima.Text = "";
                            tbTRMAlasan.Text = "";

                            ModalInputQtyTerima.Show();
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
            else
            {
                bindItemKirim();
                lbIKirimWaktuTerima.Visible = true;
            }
        }

        protected void gvIKirimPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvIKirim.PageIndex = e.NewPageIndex;
            bindItemKirim();
        }

        protected void gvLAdjPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLAdj.PageIndex = e.NewPageIndex;
            bindAdjustment();
        }

        protected void gvLPeminjamanCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                    string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvLPeminjaman.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "saverow")
                    {
                        hdnIdHeaderPinjam.Value = id;
                        tbPDNoBukti.Text = gvLPeminjaman.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[3].Text;
                        tbPDDari.Text = gvLPeminjaman.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[4].Text;
                        tbPDEmail.Text = gvLPeminjaman.Rows[rowIndex].Cells[10].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[10].Text;
                        tbPDKe.Text = gvLPeminjaman.Rows[rowIndex].Cells[5].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[5].Text;
                        tbPDKodeKe.Text = gvLPeminjaman.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[7].Text;
                        tbPDNama.Text = gvLPeminjaman.Rows[rowIndex].Cells[8].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[8].Text;
                        tbPDPhone.Text = gvLPeminjaman.Rows[rowIndex].Cells[9].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[9].Text;
                        tbPDWaktuKembali.Text = gvLPeminjaman.Rows[rowIndex].Cells[12].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[12].Text;
                        tbPDWaktuKirim.Text = gvLPeminjaman.Rows[rowIndex].Cells[11].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[11].Text;
                        tbPDWaktuSelesai.Text = gvLPeminjaman.Rows[rowIndex].Cells[13].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[13].Text;
                        
                        tbPDStatus.Text = gvLPeminjaman.Rows[rowIndex].Cells[14].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[14].Text;

                        lbPDKodeDari.Text = gvLPeminjaman.Rows[rowIndex].Cells[6].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[6].Text;
                        bindPinjamDetail();
                    }
                    else if (e.CommandName.ToLower() == "printrow")
                    {
                        hdnIdHeaderPinjam.Value = id;
                        String No_Bukti = gvLPeminjaman.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvLPeminjaman.Rows[rowIndex].Cells[3].Text;
                        LoadRDLCDataPeminjaman(hdnIdHeaderPinjam.Value);
                        exportRpt2(No_Bukti);

                        ModalListPeminjaman.Show();

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

        protected void gvLPeminjamanPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvLPeminjaman.PageIndex = e.NewPageIndex;
            bindPeminjaman();
        }

        protected void gvPinjamDetailCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(tbPDWaktuSelesai.Text == ""))
            {
                try
                {
                    if (e.CommandName.ToLower() != "page")
                    {
                        string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                        string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

                        GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        int rowIndex = grv.RowIndex;
                        string id = gvPinjamDetail.DataKeys[rowIndex]["ID"].ToString();

                        if (e.CommandName.ToLower() == "editrow")
                        {
                            if (tbPDWaktuSelesai.Text.Trim() != "")
                            {
                                DateTime WktSelesai = Convert.ToDateTime(tbPDWaktuSelesai.Text.Trim());
                                DateTime WktPinjam = Convert.ToDateTime(tbPDWaktuKirim.Text.Trim());
                                if (WktSelesai >= WktPinjam)
                                {
                                    if (cekLockPeminjaman(lbPDKodeDari.Text, WktSelesai))
                                    {
                                        hdnIdPPDetail.Value = id;
                                        hdnIdPPHeader.Value = hdnIdHeaderPinjam.Value;
                                        hdnIdPPKdbrg.Value = gvPinjamDetail.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[3].Text;

                                        tbPPAlasan.Text = "";
                                        tbPPBarcode.Text = gvPinjamDetail.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[4].Text;
                                        tbPPDari.Text = tbPDDari.Text;
                                        tbPPItemCode.Text = gvPinjamDetail.Rows[rowIndex].Cells[5].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[5].Text;
                                        tbPPKe.Text = tbPDKe.Text;
                                        tbPPQtyKembali.Text = "";
                                        tbPPQtyKirim.Text = gvPinjamDetail.Rows[rowIndex].Cells[10].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[10].Text;

                                        lbPPBrand.Text = gvPinjamDetail.Rows[rowIndex].Cells[6].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[6].Text;
                                        lbPPColor.Text = gvPinjamDetail.Rows[rowIndex].Cells[8].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[8].Text;
                                        lbPPDariKode.Text = lbPDKodeDari.Text;
                                        lbPPDesc.Text = gvPinjamDetail.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[7].Text;
                                        lbPPSize.Text = gvPinjamDetail.Rows[rowIndex].Cells[9].Text.Contains("nbsp") ? "" : gvPinjamDetail.Rows[rowIndex].Cells[9].Text;

                                        tbPPDesc.Text = lbPPDesc.Text + " " + lbPPColor.Text + " " + lbPPSize.Text;

                                        divMessagePP.Visible = false;
                                        ModalPengembalianPinjam.Show();
                                    }
                                    else
                                    {
                                        divDetailMessage.InnerText = "Tanggal Pengembalian Sudah Di lock : " + tbPDWaktuSelesai.Text.Trim();
                                        //tbPDWaktuSelesai.Text = "";
                                        bindPinjamDetail();
                                        divDetailMessage.Attributes["class"] = "error";
                                        divDetailMessage.Visible = true;
                                    }
                                }
                                else
                                {
                                    divDetailMessage.InnerText = "Waktu Pengembalian Tidak Boleh Lebih Kecil Dari Waktu Peminjaman: " + tbPDWaktuSelesai.Text.Trim();
                                    tbPDWaktuSelesai.Text = "";
                                    bindPinjamDetail();
                                    divDetailMessage.Attributes["class"] = "error";
                                    divDetailMessage.Visible = true;
                                }
                            }
                            else
                            {
                                tbPDWaktuSelesai.Text = "";
                                bindPinjamDetail();
                                divDetailMessage.InnerText = "Waktu Pengembalian Harus Di Isi";
                                divDetailMessage.Attributes["class"] = "error";
                                divDetailMessage.Visible = true;
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
            else
            {
                bindPinjamDetail();

                divDetailMessage.InnerText = "Pilih Tanggal Waktu Pengembalian Barang";
                divDetailMessage.Attributes["class"] = "error";
                divDetailMessage.Visible = true;
            }
        }

        protected void gvPinjamDetailPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvPinjamDetail.PageIndex = e.NewPageIndex;
            bindPinjamDetail();
        }

        protected void gvPinjamDetailDataBound(object sender, GridViewRowEventArgs e)
        {
            string status = tbPDStatus.Text;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                if ((e.Row.Cells[12].Text.ToLower().Contains("keluar")) && status == "kembali")
                {
                    imgEdit.Visible = true;
                }
            }
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnTrfStockClick(object sender, EventArgs e)
        {
            bindPU();
        }

        protected void btnCreateTrfStockClick(object sender, EventArgs e)
        {
            panelCStockHeader.Visible = true;
            panelReturHeader.Visible = false;
            hdnCreateStock.Value = "Transfer";
            panelPinjamHeader.Visible = false;
            btnStockSave.Visible = true;
            btnPinjamSave.Visible = false;
            btnPinjamGetItemCode.Visible = false;
            btnStockGetItemCode.Visible = true;

            if (divStock.Visible) //Awal
            {
                divCreateStock.Attributes.Add("style", "display: block;"); ;
                divStock.Visible = false;
                btnViewTrfStock.Visible = true;
                divBtnMain.Visible = false;

                hdnIDHeader.Value = "0";
                btnStockGenerate.Enabled = true;
                tbStockNoBukti.Text = "";
                tbStockDari.Text = "";
                tbStockKe.Text = "";

                deleteTempTrfStock();
                bindNoBukti();
            }
            else
            {
                divCreateStock.Attributes.Add("style", "display: none;"); ;
                divStock.Visible = true;
                UpdStock.Enabled = false;
                btnViewTrfStock.Visible = false;
                divBtnMain.Visible = true;
            }
        }

        protected void btnCreateReturSingClick(object sender, EventArgs e)
        {
            hdnCreateStock.Value = "Retur";
            divBtnMain.Visible = false;
            btnViewTrfStock.Visible = true;
            divStock.Visible = false;

            panelCStockHeader.Visible = false;
            panelPinjamHeader.Visible = false;
            panelReturHeader.Visible = true;

            btnStockGetItemCode.Visible = false;
            btnPinjamGetItemCode.Visible = false;
            btnReturGetItemCode.Visible = true;

            btnStockSave.Visible = false;
            btnPinjamSave.Visible = false;
            btnReturSave.Visible = true;

            deleteTempTrfStock();
            bindNoBukti();
        }

        protected void btnStockGenerateClick(object sender, EventArgs e)
        {
            string sLevel = Session["ULevel"].ToString().ToLower();
            if (!(sLevel == "admin counter") && !(sLevel == "admin sales") && !(sLevel == "supply control"))
            {
                tbQTYDari.Text = Session["UStore"].ToString();
                tbQTYWaktu.Text = "";

                ddlQTYDari.Visible = false;
                tbQTYDari.Visible = true;
            }
            else
            {
                tbQTYWaktu.Text = "";

                ddlQTYDari.Visible = true;
                tbQTYDari.Visible = false;
                bindStoreKirim();
            }
            ModalGenerateNoBukti.Show();
        }

        protected void btnStockGetItemCodeClick(object sender, EventArgs e)
        {
            Session.Remove("INoBon");
            bindItemCode();
        }

        protected void btnItemSearchClick(object sender, EventArgs e)
        {
            bindItemCode();
        }

        protected void bIKirimCloseClick(object sender, EventArgs e)
        {
            bindPU();
            ModalItemKirim.Hide();
            ModalPopupTrfHeader.Show();
        }

        protected void btnQTYSaveClick(object sender, EventArgs e) //Update No Bukti yang lama
        {
            //TRF_STOCK_DETAIL show = new TRF_STOCK_DETAIL();
            TRF_STOCK_HEADER showHeader = new TRF_STOCK_HEADER();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA(); 
            DateTime dt = DateTime.Now;
            DateTime timeNow = DateTime.Now;

            string date = tbQTYWaktu.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbQTYWaktu.Text.ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out timeNow);
            }

            date = string.Format("{0:yyMM}", timeNow);

            //Check Lock
            LOGIN_DA loginDA = new LOGIN_DA();
            string whereLock = " where NAME = 'lock'";
            int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
            int dateMove = Convert.ToInt32(string.Format("{0:yyMM}", timeNow));

            if (dateMove > lockMove)
            {
                List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
                string kodeDari = tbQTYDari.Visible ? Session["UKode"].ToString() : ddlQTYDari.SelectedValue.ToString().Split('#')[1].ToString();
                string kodeKe = ddlQTYKE.SelectedValue;
                listShowroom = showDA.getShowRoom(string.Format(" where KODE in('{0}')", kodeDari));
                //listShowroom = showDA.getShowRoom(string.Format(" where KODE in('{0}','{1}')", kodeDari, kodeKe));

                if (cekLock(listShowroom.First().STATUS_SHOWROOM, listShowroom.Last().STATUS_SHOWROOM, date))
                {
                    tbQTYDari.Text = tbQTYDari.Visible ? tbQTYDari.Text : ddlQTYDari.SelectedItem.Text;
                    showHeader.DARI = tbQTYDari.Text;
                    showHeader.KE = ddlQTYKE.SelectedItem.Text;
                    showHeader.CREATED_BY = Session["UName"].ToString();
                    showHeader.KODE_DARI = kodeDari;
                    showHeader.KODE_KE = kodeKe;

                    string idNew = showDA.insertHeaderRetID(showHeader);
                    //string NoBukti = idNew.Length > 5 ? idNew.Remove(0, idNew.Length - 6) : idNew.PadLeft(6, '0');
                    string tgl = string.Format("{0:yyddMM}", dt);
                    string kodeMove = tbQTYDari.Text.ToLower().Contains("main warehouse") || tbQTYDari.Text.ToLower().Contains("head office")
                        ? "KT" : ddlQTYKE.SelectedValue.Contains("WARE-001") || ddlQTYKE.SelectedValue.Contains("HO-001")
                        ? "DT" : ddlQTYKE.SelectedValue.Contains("SING-001")
                        ? "KS" : "ST";
                    //NoBukti = kodeMove + tgl + NoBukti;
                    hdnKodeMove.Value = kodeMove;
                    hdnIDShowroomKirim.Value = tbQTYDari.Visible ? Session["UIdStore"].ToString() : ddlQTYDari.SelectedValue.ToString().Split('#')[0].ToString(); ;

                    //string ret = showDA.updateNoBuktiHeader(NoBukti, idNew, kodeMove);
                    //hdnIdTujuan.Value = ddlQTYKE.SelectedIndex.ToString();
                    string ret = "Berhasil!";
                    if (ret == "Berhasil!")
                    {
                        if (ret == "Berhasil!")
                        {
                            btnStockGenerate.Enabled = false;
                            btnStockGetItemCode.Enabled = true;
                            UpdStock.Enabled = true;

                            //tbStockNoBukti.Text = NoBukti;
                            tbStockDari.Text = tbQTYDari.Text;
                            lbStockKodeDari.Text = tbQTYDari.Visible ? Session["UKode"].ToString() : ddlQTYDari.SelectedValue.ToString().Split('#')[1].ToString();
                            lbStockIdDari.Text = tbQTYDari.Visible ? Session["UIdStore"].ToString() : ddlQTYDari.SelectedValue.ToString().Split('#')[0].ToString();
                            tbStockKe.Text = ddlQTYKE.SelectedItem.Text;
                            tbStockKodeKe.Text = ddlQTYKE.SelectedValue;
                            tbStockWaktuKirim.Text = tbQTYWaktu.Text;
                            hdnIDHeader.Value = idNew;

                            DivMessage.InnerText = "Generate No Bukti Sukses!";
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                        else
                        {
                            DivMessage.InnerText = ret;
                            DivMessage.Attributes["class"] = "error";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        DivMessage.InnerText = ret;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
                else 
                {
                    DivMessage.InnerText = "Tanggal Di Showroom Pengirim Sudah Di Lock!";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Tanggal Sudah Di Lock!";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void btnTRMSaveClick(object sender, EventArgs e)
        {
            int kirim = int.Parse(tbTRMQTYKirim.Text);
            int terima = int.Parse(tbTRMQTYTerima.Text);

            if (terima <= kirim)
            {
                string sStore = Session["UStore"] == null ? "" : Session["UStore"].ToString();
                string sULevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
                string sUName = Session["UName"] == null ? "" : Session["UName"].ToString();
                string ret = string.Empty;

                List<MS_STOCK> stockList = new List<MS_STOCK>();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                MS_STOCK stock = new MS_STOCK();
                TRF_STOCK_DETAIL trfStock = new TRF_STOCK_DETAIL();

                stockList = stockDA.getStock(String.Format(" where BARCODE = '{0}' and WAREHOUSE = '{1}'", tbTRMBarcode.Text, tbTRMKe.Text));

                stock.ITEM_CODE = tbTRMItemCode.Text;
                stock.WAREHOUSE = tbTRMKe.Text;
                stock.KODE = tbIKirimKodeKe.Text;
                stock.STOCK = Convert.ToInt32(tbTRMQTYTerima.Text);
                stock.RAK = "000";
                stock.CREATED_BY = sUName;
                stock.UPDATED_BY = sUName;
                stock.BARCODE = tbTRMBarcode.Text;

                if (stockList.Count > 0)//Update
                {
                    ret = stockDA.updateDataStockWithOutID(stock);
                }
                else//Insert
                {
                    ret = stockDA.insertDataStock(stock);
                }

                //Update Item Detail Transfer Stock
                if (ret == "Berhasil!")
                {
                    trfStock.ID = Convert.ToInt64(hdnTRMIdTrfStock.Value);
                    trfStock.QTY_TERIMA = Convert.ToInt32(tbTRMQTYTerima.Text);
                    trfStock.USER_TERIMA = sUName;
                    trfStock.ALASAN = tbTRMAlasan.Text;
                    trfStock.REFF = "Done";

                    ret = stockDA.updateTerimaTRFStock(trfStock);
                }

                //Update Header
                if (ret == "Berhasil!")
                {
                    TRF_STOCK_HEADER header = new TRF_STOCK_HEADER();

                    header = stockDA.getTrfStockHeader(String.Format(" where ID = {0}", hdnIKirimIDHeader.Value)).FirstOrDefault();

                    //Bila di terima pertama kali
                    if (header.WAKTU_TERIMA == (DateTime?)null)
                    {
                        //Update Waktu Terima & Ubah Status Header menjadi Sedang Menerima
                        header.STATUS = "Sedang Menerima";
                        header.UPDATED_BY = Session["UName"].ToString();

                        DateTime tglTerima = DateTime.Now;
                        string date = tbIKirimWaktuTerima.Text.ToString();
                        if (!string.IsNullOrEmpty(date))
                        {
                            DateTime.TryParseExact(date, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTerima);
                        }
                        header.WAKTU_TERIMA = tglTerima;

                        ret = stockDA.updateFirstTrfHeader(header);
                    }
                    if (ret == "Berhasil!")
                    {
                        List<TRF_STOCK_DETAIL> listTrfStock = new List<TRF_STOCK_DETAIL>();
                        listTrfStock = stockDA.getTrfStock(String.Format(" where ID_HEADER = {0} and REFF not like 'Done'", hdnIKirimIDHeader.Value));

                        //Bila semua status item transfer stock done
                        if (listTrfStock.Count == 0)
                        {
                            //Update Header Waktu Done Time & ubah status Header menjadi Done
                            header.STATUS = "Done";
                            header.UPDATED_BY = Session["UName"].ToString();

                            ret = stockDA.updateDoneTrfHeader(header);
                        }
                    }
                }

                if (ret == "Berhasil!")
                {
                    divIKirimMessage.InnerText = "Update Sukses!";
                    divIKirimMessage.Attributes["class"] = "success";
                    divIKirimMessage.Visible = true;
                }
                else if (ret != "")
                {
                    divIKirimMessage.InnerText = "Error : " + ret;
                    divIKirimMessage.Attributes["class"] = "error";
                    divIKirimMessage.Visible = true;
                }

                bindItemKirim();
            }
            else
            {
                divTRMMessage.InnerText = "Quantity tidak boleh lebih besar dari yang dikirim!";
                divTRMMessage.Attributes["class"] = "warning";
                divTRMMessage.Visible = true;
                ModalInputQtyTerima.Show();
            }
        }

        protected void btnAddAdjustmentClick(object sender, EventArgs e)
        {

        }

        protected void bTRMCloseClick(object sender, EventArgs e)
        {
            bindItemKirim();
        }

        protected void btnKRMSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(tbKRMStock.Text) == int.Parse(tbKRMQTYKirim.Text) && int.Parse(tbKRMStock.Text) != int.Parse(tbKRMQTYKirim.Text))//(int.Parse(tbKRMStock.Text) < int.Parse(tbKRMQTYKirim.Text))
                {
                    divKRMMessage.InnerText = "Jumlah kirim melebihi jumlah stock!";
                    divKRMMessage.Attributes["class"] = "warning";
                    divKRMMessage.Visible = true;

                    ModalInputQtyKirim.Show();
                }
                else
                {
                    int diff = 0 - int.Parse(tbKRMQTYKirim.Text);
                    TRF_STOCK_DETAIL stock = new TRF_STOCK_DETAIL();
                    TEMP_KDBRG tempKdbrg = new TEMP_KDBRG();
                    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                    MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();

                    if (hdnKRMStat.Value.ToLower() == "insert")
                    {
                        string idShow = panelCStockHeader.Visible ? lbStockIdDari.Text : tbPinjamDariID.Text;
                        string kode = panelCStockHeader.Visible ? lbStockKodeDari.Text : tbPinjamDariKode.Text;
                        string store = panelCStockHeader.Visible ? tbStockDari.Text : tbPinjamDari.Text;
                        string user = Session["UName"].ToString();

                        tempKdbrg.ID_KDBRG = Convert.ToInt64(hdnKRMIdKdbrg.Value);
                        tempKdbrg.ID_HEADER = Convert.ToInt64(hdnIDHeader.Value);
                        tempKdbrg.ID_SHOWROOM = Convert.ToInt64(idShow);
                        tempKdbrg.SHOWROOM = store;
                        tempKdbrg.KODE = kode;
                        tempKdbrg.BARCODE = tbKRMBarcode.Text.Trim();
                        tempKdbrg.ITEM_CODE = tbKRMItemCode.Text.Trim();
                        tempKdbrg.QTY = Convert.ToInt32(tbKRMQTYKirim.Text);
                        tempKdbrg.CREATED_BY = user;
                        tempKdbrg.FLAG = panelCStockHeader.Visible ? "transfer" : "pinjam";
                        tempKdbrg.STAT = "insert";
                        tempKdbrg.BRAND = lbKRMBrand.Text;
                        tempKdbrg.ART_DESC = lbKRMDesc.Text;
                        tempKdbrg.FCOLOR = lbKRMColor.Text;
                        tempKdbrg.FSIZE = lbKRMSize.Text;

                        wareDA.insertTempTrfStock(tempKdbrg);
                    }
                    else
                    {
                        tempKdbrg.ID = Convert.ToInt64(hdnKRMIdTemp.Value);
                        tempKdbrg.QTY = Convert.ToInt32(tbKRMQTYKirim.Text);
                        wareDA.updateTempTrfStock(tempKdbrg);
                    }

                    //stock.ID_HEADER = Convert.ToInt64(hdnIDHeader.Value);
                    //stock.ID_KDBRG = Convert.ToInt64(hdnKRMIdKdbrg.Value);
                    //stock.NO_BUKTI = tbKRMNoBukti.Text;
                    //stock.ITEM_CODE = tbKRMItemCode.Text;
                    //stock.BARCODE = tbKRMBarcode.Text;
                    //stock.USER_KIRIM = Session["UName"].ToString();
                    //stock.QTY_KIRIM = Convert.ToInt32(tbKRMQTYKirim.Text);
                    //stock.STOCK_AKHIR_KIRIM = int.Parse(tbKRMStock.Text) + diff;
                    //stock.REFF = "Sedang Di Kirim";
                    //stockDA.insertTrfStock(stock);

                    ////Update Stock

                    //MS_STOCK msStock = new MS_STOCK();
                    //msStock.ITEM_CODE = stock.ITEM_CODE;
                    //msStock.BARCODE = stock.BARCODE;
                    //msStock.WAREHOUSE = tbKRMDari.Text;
                    //msStock.KODE = Session["UKode"].ToString();
                    //msStock.UPDATED_BY = Session["UName"].ToString();
                    //msStock.STOCK = diff;
                    //stockDA.updateDataStockWithOutID(msStock);

                    bindNoBukti();
                }

            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void btnStockSaveClick(object sender, EventArgs e)
        {
            //Cek Stock after upload dan insert

            List<MS_STOCK> stockList = new List<MS_STOCK>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            string kode = lbStockKodeDari.Text; //Session["UKode"].ToString();
            string sName = Session["UName"].ToString();
            //stockList = stockDA.getStock(string.Format(" where KODE = '{0}' and STOCK < 0", kode));
            stockList = stockDA.getStock(string.Format(" where ID_KDBRG in ( select ID_KDBRG from temp_kdbrg where CREATED_BY = '{0}') and KODE = '{1}' and STOCK < 0", sName, kode));

            if (stockList.Count == 0 || stockList.Count > 0)
            {
                DateTime tglKirim = SqlDateTime.MaxValue.Value;
                string date = tbStockWaktuKirim.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglKirim);
                }

                TRF_STOCK_HEADER header = new TRF_STOCK_HEADER();
                MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
                string user = Session["UName"].ToString();


                //Update & Insert Header Transfer Stock
                
                //Insert
                TRF_STOCK_HEADER showHeader = new TRF_STOCK_HEADER();
                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                DateTime dt = DateTime.Now;
                //showHeader.DARI = tbStockDari.Text;
                //showHeader.KE = tbStockKe.Text;
                //showHeader.CREATED_BY = Session["UName"].ToString();
                //showHeader.KODE_DARI = lbStockKodeDari.Text;
                //showHeader.KODE_KE = tbStockKodeKe.Text;

                //string idNew = showDA.insertHeaderRetID(showHeader);
                //int a = 0;
                //if (int.TryParse(idNew, out a))
                //{
                    //Update Header
                //hdnIDHeader.Value = idNew;
                List<NO_DOC> listNoDoc = new List<NO_DOC>();
                SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                string whereDoc = string.Format(" where KODE = '{0}' and FLAG = 'TRANSFER'", hdnKodeMove.Value);
                listNoDoc = shBayarDA.getNoDoc(whereDoc);

                string noUrut = listNoDoc.Count == 0 ? "1" : listNoDoc.Last().DIFF_YEAR == 1 ? "1" : (listNoDoc.Last().NO_URUT + 1).ToString();
                noUrut = noUrut.Length > 5 ? noUrut.Remove(0, noUrut.Length - 6) : noUrut.PadLeft(6, '0');

                string NoBukti = noUrut;
                string tgl = string.Format("{0:yyddMM}", dt);
                string kodeMove = ddlQTYKE.SelectedValue.Contains("SING-001")
                    ? "KS" : tbQTYDari.Text.ToLower().Contains("main warehouse") || tbQTYDari.Text.ToLower().Contains("head office")
                    ? "KT" : ddlQTYKE.SelectedValue.Contains("WARE-001") || ddlQTYKE.SelectedValue.Contains("HO-001")
                    ? "DT" : "ST";
                NoBukti = kodeMove + tgl + NoBukti;
                hdnKodeMove.Value = kodeMove;
                hdnIDShowroomKirim.Value = tbQTYDari.Visible ? Session["UIdStore"].ToString() : ddlQTYDari.SelectedValue.ToString().Split('#')[0].ToString(); ;

                string ret = "";// showDA.updateNoBuktiHeader(NoBukti, hdnIDHeader.Value, kodeMove);

                // Update No Bon
                NO_DOC noDoc = new NO_DOC();
                noDoc.ID = listNoDoc.Count == 0 ? 0 : listNoDoc.Last().ID;
                noDoc.NO_URUT = int.Parse(noUrut);
                noDoc.KODE = hdnKodeMove.Value;
                noDoc.CREATED_BY = Session["UName"].ToString();
                noDoc.FLAG = "TRANSFER";

                ret = noDoc.NO_URUT.ToString() == "1" ? shBayarDA.insertNoDoc(noDoc) : shBayarDA.updateNoDoc(noDoc);

                    
                header.STATUS = "Sedang Di Kirim";
                header.CREATED_BY = Session["UName"].ToString();
                header.ID = Convert.ToInt64(hdnIDHeader.Value);
                header.WAKTU_KIRIM = tglKirim;

                //stockDA.updateKirimTrfHeader(header);
                //wareDA.insertTRFStock(user, hdnKodeMove.Value);
                wareDA.insertTRFStockNEW(user, hdnKodeMove.Value, NoBukti, Convert.ToInt64(hdnIDHeader.Value), tglKirim, "Sedang Di Kirim");

                clearFinish();
                bindNoBukti();

                DivMessage.InnerText = "Pengiriman sudah disimpan | No Bukti : " + NoBukti;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                lblDONEChange.Text = NoBukti;

                ttlTrfQty = 0;
                ttlTrfQtyFromExcel = string.Empty;
                lblTtlQtyExcel.Text = "0";
                lblTtlQtyUpd.Text = "0";
                ModalFinish.Show();
                //}
                //else
                //{
                //    DivMessage.InnerText = idNew;
                //    DivMessage.Attributes["class"] = "error";
                //    DivMessage.Visible = true;
                //}
            }
            else
            {
                DivMessage.InnerText = "Cek Stock Kembali, Terdapat Stock Minus setelah di Update\nHubungin Admin untuk penjelasan lebih lanjut.";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnUploadClick(object sender, EventArgs e)
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

        protected void btnUpdStockClick(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            //string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = UpdStock.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "" || UpdStock.Enabled)
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    UpdStock.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);

                    deleteTempTrfStock();
                    ttlTrfQty = 0;
                    ttlTrfQtyFromExcel = string.Empty;
                    lblTtlQtyExcel.Text = "0";
                    lblTtlQtyUpd.Text = "0";
                    bool ret = uploadNew(dir, FileUploadName, source, FileType);
                    if (ret)
                    {
                        bindNoBukti();
                        //DivMessage.InnerText = "Upload Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
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
                DivMessage.InnerText = ExcelFileName != "" ? "Pilih File Yang Akan Diupload." : "Generate No Bukti Terlebih Dahulu.";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnCreatePinjamClick(object sender, EventArgs e)
        {
            string sidShow = Session["UIdStore"].ToString();
            string skode = Session["UKode"].ToString();
            string sLevel = Session["ULevel"].ToString();
            string sStore = Session["UStore"].ToString();

            tbPinjamDari.Text = sStore;
            tbPinjamDariKode.Text = skode;
            tbPinjamDariID.Text = sidShow;
            hdnCreateStock.Value = "Pinjam";

            panelCStockHeader.Visible = false;
            panelPinjamHeader.Visible = true;
            panelReturHeader.Visible = false;

            btnStockSave.Visible = false;
            btnPinjamSave.Visible = true;
            btnPinjamGetItemCode.Visible = true;
            btnStockGetItemCode.Visible = false;

            divCreateStock.Attributes.Add("style", "display: block;"); ;
            divStock.Visible = false;
            btnViewTrfStock.Visible = true;
            divBtnMain.Visible = false;

            hdnIDHeader.Value = "0";
            btnStockGenerate.Enabled = true;
            tbStockNoBukti.Text = "";
            tbStockDari.Text = "";
            tbStockKe.Text = "";

            ddlPinjamStatus.SelectedIndex = 0;
            ddlPinjamStatus.Enabled = sLevel.ToLower() == "warehouse" ? true : false;

            deleteTempTrfStock();
            bindNoBukti();
        }

        protected void btnPinjamSaveClick(object sender, EventArgs e)
        {
            string tglKembali = tbPinjamKembali.Text;
            if (tglKembali == "" && ddlPinjamStatus.SelectedIndex == 0)
            {
                DivMessage.InnerText = "Harap isi tanggal kembali barang";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
            else
            {
                //Insert Header Pinjam
                SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                string kodeStore = Session["UKode"].ToString();
                string sName = Session["UName"].ToString();
                ////No Urut
                List<NO_DOC> listNoDoc = new List<NO_DOC>();
                string whereDoc = string.Format(" where KODE = '{0}' and FLAG = 'pinjam'", kodeStore);
                listNoDoc = shBayarDA.getNoDoc(whereDoc);

                int noUrut = listNoDoc.Count == 0 ? 1 : listNoDoc.Last().DIFF_YEAR == 1 ? 1 : listNoDoc.Last().NO_URUT + 1;
                Int64 idNoDoc = listNoDoc.Count == 0 ? 0 : listNoDoc.Last().ID;
                string noUrutID = noUrut.ToString();
                string id = noUrutID.Length > 5 ? noUrutID.Remove(0, noUrutID.Length - 5) : noUrutID.PadLeft(5, '0');

                ////Tanggal
                DateTime dt = DateTime.Now;
                string tgl = string.Format("{0:yyMM}", dt);
                
                ////ID Store
                string idStore = Session["UIdStore"].ToString();
                idStore = idStore.Length > 2 ? idStore.Remove(0, idStore.Length - 2) : idStore.PadLeft(2, '0');

                string noBukti = "PJ" + tgl + idStore + id;

                DateTime tglKirim = DateTime.Now;
                string date = tbPinjamAmbil.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglKirim);
                }

                DateTime tglPinjamKembali = DateTime.MaxValue;
                date = tbPinjamKembali.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglPinjamKembali);
                }

                MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
                TRF_PINJAM_HEADER pinjamHeader = new TRF_PINJAM_HEADER();
                pinjamHeader.NO_BUKTI = noBukti;
                pinjamHeader.DARI = tbPinjamDari.Text;
                pinjamHeader.KE = tbPinjamKe.Text;
                pinjamHeader.KODE_DARI = kodeStore;
                pinjamHeader.KODE_KE = tbPinjamKode.Text;
                pinjamHeader.NAMA = tbPinjamNama.Text;
                pinjamHeader.PHONE = tbPinjamPhone.Text;
                pinjamHeader.EMAIL = tbPinjamEmail.Text;
                pinjamHeader.WAKTU_KIRIM = tglKirim;
                pinjamHeader.WAKTU_KEMBALI = tglPinjamKembali;
                pinjamHeader.STATUS = ddlPinjamStatus.SelectedIndex == 0 ? "kembali" : "tidak";
                pinjamHeader.CREATED_BY = sName;
                string idHeader = wareDA.insertPinjamHeaderRetID(pinjamHeader);

                if (!idHeader.Contains("ERROR"))
                {
                    pinjamHeader.ID = Convert.ToInt64(idHeader);
                    wareDA.insertPinjamDetail(pinjamHeader, ddlPinjamStatus.SelectedIndex);

                    insertUpdateNoDoc(idNoDoc, noUrutID, "pinjam");

                    clearFinishPinjam();
                    bindNoBukti();

                    DivMessage.InnerText = "Peminjaman sudah disimpan | No Bukti : " + pinjamHeader.NO_BUKTI;
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
                else
                {
                    DivMessage.InnerText = idHeader;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
        }

        protected void btnAdjSaveClick(object sender, EventArgs e)
        {
            if (tbAdjAlasan.Text.Length < 6)
            {
                divMessageAdj.InnerText = "Alasan minimal harus 5 karakter";
                divMessageAdj.Attributes["class"] = "error";
                divMessageAdj.Visible = true;
                ModalAdjustment.Show();
                return;
            }

            if (tbAdjPerbedaan.Text.Length == 0)
            {
                divMessageAdj.InnerText = "Jumlah perbedaan harus diisi";
                divMessageAdj.Attributes["class"] = "error";
                divMessageAdj.Visible = true;
                ModalAdjustment.Show();
            }
            else
            {
                if (Convert.ToInt32(tbAdjPerbedaan.Text) <= 0)
                {
                    divMessageAdj.InnerText = "Jumlah perbedaan harus lebih besar dari nol";
                    divMessageAdj.Attributes["class"] = "error";
                    divMessageAdj.Visible = true;
                    ModalAdjustment.Show();
                    return;
                }

                string sName = Session["UName"].ToString();
                List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
                MS_SHOWROOM show = new MS_SHOWROOM();
                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

                listShowroom = showDA.getShowRoom(string.Format(" where KODE = '{0}'", lbAdjKode.Text.Trim()));
                show = listShowroom.First();

                DateTime tanggalTrans = DateTime.Now;
                string sTanggal = tbAdjTanggal.Text;
                if (!string.IsNullOrEmpty(sTanggal))
                {
                    DateTime.TryParseExact(sTanggal, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tanggalTrans);
                }


                //Check Lock
                LOGIN_DA loginDA = new LOGIN_DA();
                string whereLock = " where NAME = 'lock'";
                int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
                int dateMove = Convert.ToInt32(string.Format("{0:yyMM}", tanggalTrans));

                if (dateMove > lockMove)
                {

                    TR_ADJUSTMENT adj = new TR_ADJUSTMENT();
                    adj.ID_KDBRG = Convert.ToInt64(hdnAdjIdKdbrg.Value);
                    adj.ID_WAREHOUSE = show.ID;
                    adj.KODE = lbAdjKode.Text;
                    adj.SHOWROOM = tbAdjWarehouse.Text;
                    adj.BARCODE = tbAdjBarcode.Text;
                    adj.ITEM_CODE = tbAdjItemCode.Text;
                    adj.BRAND = lbAdjBrand.Text;
                    adj.ART_DESC = lbAdjDesc.Text;
                    adj.COLOR = lbAdjColor.Text;
                    adj.SIZE = lbAdjSize.Text;
                    adj.STOCK_AWAL = Convert.ToInt32("0");
                    adj.STOCK_AKHIR = Convert.ToInt32("0");
                    adj.ADJUSTMENT = cbAdjMinus.Checked ? Convert.ToInt32(tbAdjPerbedaan.Text) * -1 : Convert.ToInt32(tbAdjPerbedaan.Text);
                    adj.CREATED_DATE = tanggalTrans;
                    adj.ALASAN = tbAdjAlasan.Text;
                    adj.CREATED_BY = sName;
                    string newID = showDA.insertTrAdjusment(adj);

                    if (!newID.Contains("ERROR"))
                    {
                        MS_STOCK_DA stockDA = new MS_STOCK_DA();
                        MS_STOCK stock = new MS_STOCK();

                        stock.ID = Convert.ToInt64(hdnAdjIdStock.Value);
                        //stock.STOCK = adj.STOCK_AKHIR;
                        stock.STOCK = adj.ADJUSTMENT;
                        stock.UPDATED_BY = sName;
                        stockDA.updateDataDiffkWithID(stock);

                        bindGrid();
                        DivMessage.InnerText = "Adjustment Manual Berhasil Dilakukan";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = newID;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Tanggal Sudah Di Lock!";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
        }

        protected void btnAdjustmentClick(object sender, EventArgs e)
        {
            bindAdjustment();
        }

        protected void btnUploadAdjustmentClick(object sender, EventArgs e)
        {
            ModalUploadAdjust.Show();
        }

        protected void btnListPeminjamanClick(object sender, EventArgs e)
        {
            bindPeminjaman();
        }

        protected void btnPDSearchClick(object sender, EventArgs e)
        {
            bindPinjamDetail();
        }

        protected void btnPPSaveClick(object sender, EventArgs e)
        {
            int qtyKirim = int.Parse(tbPPQtyKirim.Text);
            int qtyKembali = int.Parse(tbPPQtyKembali.Text);

            if (!(qtyKirim < qtyKembali))
            {
                List<MS_STOCK> listStock = new List<MS_STOCK>();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();

                if (!tbPDWaktuSelesai.ReadOnly)
                {
                    TRF_PINJAM_HEADER pinjamHeader = new TRF_PINJAM_HEADER();
                    DateTime waktuSelesai = DateTime.Now;
                    string selesai = tbPDWaktuSelesai.Text;
                    if (!string.IsNullOrEmpty(selesai))
                    {
                        DateTime.TryParseExact(selesai, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out waktuSelesai);
                    }
                    pinjamHeader.WAKTU_SELESAI = waktuSelesai;
                    pinjamHeader.ID = Convert.ToInt64(hdnIdHeaderPinjam.Value);
                    pinjamHeader.UPDATED_BY = Session["UName"].ToString();

                    wareDA.updateWaktuSelesaiPinjamHeader(pinjamHeader);
                }

                listStock = stockDA.getStock(string.Format(" where KODE = '{0}' and BARCODE = '{1}'", lbPPDariKode.Text, tbPPBarcode.Text));

                //Update Stock
                
                MS_STOCK stock = new MS_STOCK();
                if (listStock.Count > 0)
                {
                    stock = listStock.First();
                    stock.STOCK = qtyKembali;
                    stockDA.updateDataStockWithID(stock);
                }

                TRF_PINJAM_DETAIL pinjamDetail = new TRF_PINJAM_DETAIL();
                pinjamDetail.QTY_TERIMA = qtyKembali;
                pinjamDetail.ID = Convert.ToInt64(hdnIdPPDetail.Value);
                pinjamDetail.USER_TERIMA = Session["UName"].ToString();
                pinjamDetail.ALASAN = tbPPAlasan.Text;
                pinjamDetail.FLAG = "kembali";
                pinjamDetail.STOCK_AKHIR_TERIMA = listStock.Count == qtyKembali ? -1 : listStock.First().STOCK + qtyKembali;

                wareDA.updatePinjamDetail(pinjamDetail);
                bindPinjamDetail();

                divDetailMessage.InnerText = "Pengembalian barang peminjaman berhasil";
                divDetailMessage.Attributes["class"] = "success";
                divDetailMessage.Visible = true;
            }
            else
            {
                divMessagePP.InnerText = "Qty Kirim Tidak boleh lebih Kecil dari Qty Kembali!";
                divMessagePP.Attributes["class"] = "warning";
                divMessagePP.Visible = true;

                ModalPengembalianPinjam.Show();
            }
        }

        protected void btnDownloadStockClick(object sender, EventArgs e)
        {
            if (ddlDownloadStore.SelectedItem.Text != "")//(ddlDownloadStore.SelectedIndex > 0)
            {
                string fileName = ddlDownloadStore.SelectedItem.Text + "_" + tbDownloadDate.Text;
                if (File.Exists(Server.MapPath("\\Download\\" + fileName + ".xls")))
                {
                    File.Delete(Server.MapPath("\\Download\\" + fileName + ".xls"));
                }
                string connectionString = GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    MS_STOCK_DA stockDA = new MS_STOCK_DA();
                    List<MS_STOCK> listStock = new List<MS_STOCK>();
                    DateTime sDate = DateTime.Now;

                    string date = tbDownloadDate.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbDownloadDate.Text.ToString();
                    if (!string.IsNullOrEmpty(date))
                    {
                        DateTime.TryParseExact(date, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate);
                    }

                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "CREATE TABLE ["+ddlDownloadStore.SelectedItem.Text+"] " +
                        "(KODE varchar, SHOWROOM varchar, BARCODE TEXT, ITEM_CODE TEXT, BRAND varchar, ARTICLE varchar, WARNA varchar, UKURAN varchar, STOCK int );";
                    cmd.ExecuteNonQuery();

                    //listStock = stockDA.getStockCutOff("", DateTime.Now, ddlDownloadStore.SelectedValue);
                    listStock = stockDA.getStockCutOff("", sDate, ddlDownloadStore.SelectedValue);
                    foreach (MS_STOCK item in listStock)
                    {
                        cmd.CommandText = "INSERT INTO [" + ddlDownloadStore.SelectedItem.Text + "]" +
                            "(KODE, SHOWROOM, BARCODE, ITEM_CODE, BRAND, ARTICLE, WARNA, UKURAN, STOCK) " +
                            "VALUES('" + ddlDownloadStore.SelectedValue + "','" + ddlDownloadStore.SelectedItem.Text + "','" + item.BARCODE + "', '" + item.ITEM_CODE + "', " +
                            "'" + item.BRAND + "', '" + item.ART_DESC + "', '" + item.WARNA + "', '" + item.SIZE + "', " + item.STOCK + ");";
                        cmd.ExecuteNonQuery();
                    }
                    //dsCBYR = dBase.SearchData("JLSC5001 group by NO_BON", "NO_BON", "");
                    //foreach (DataRow item in dsCBYR.Tables[0].Rows)
                    //{
                    //    cmd.CommandText = "INSERT INTO [SIS](No_Transaction,Description) VALUES('" + noBon + "','" + tulis + "');";
                    //    cmd.ExecuteNonQuery();
                    //}

                    conn.Close();

                    func.download(fileName + ".xls", "\\Download\\" + fileName + ".xls");
                }
            }
            else
            {
                DivMessage.InnerText = "Pilih Showroom yang akan di download!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnDownloadStockClickNew(object sender, EventArgs e)
        {
            if (ddlDownloadStore.SelectedItem.Text != "")//(ddlDownloadStore.SelectedIndex > 0)
            {
                string fileName = ddlDownloadStore.SelectedItem.Text + "_" + tbDownloadDate.Text;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                GridView db = new GridView();

                //DataSet searchData = cd.SearchData(tabel, field, "");
                //DataSet searchData = new DataSet();
                DateTime sDate = DateTime.Now;

                string date = tbDownloadDate.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbDownloadDate.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate);
                }
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                List<EXCEL_DOWNLOAD_STOCK> soDetail = new List<EXCEL_DOWNLOAD_STOCK>();
                //soDetail = stockDA.getDownloadStockCutOff(" WHERE STOCK != 0 ", sDate, ddlDownloadStore.SelectedValue);//("", sDate, ddlDownloadStore.SelectedValue);
                soDetail = stockDA.getDownloadStockCutOffWithPrice(" WHERE STOCK != 0 ", sDate, ddlDownloadStore.SelectedValue, DateTime.Now);//("", sDate, ddlDownloadStore.SelectedValue);

                string strStyle = @"<style>.text { mso-number-format:\@; } runat=server </style>";
                hw.WriteLine(strStyle);

                Response.Clear();
                db.DataSource = soDetail;
                db.DataBind();

                for (int i = 0; i < soDetail.Count; i++)
                {
                    db.Rows[i].Cells[0].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[1].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[2].Attributes.Add("class", "text");
                    db.Rows[i].Cells[3].Attributes.Add("class", "text");
                    db.Rows[i].Cells[4].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[5].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[6].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[7].Attributes.Add("class", "varchar");
                    db.Rows[i].Cells[8].Attributes.Add("class", "numeric");
                    db.Rows[i].Cells[9].Attributes.Add("class", "numeric");
                }

                db.RenderControl(hw);
                Response.AddHeader("content-disposition", "attachment;filename=" + ddlDownloadStore.SelectedItem.Text + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(tw.ToString());
                Response.End();
            }
            else
            {
                DivMessage.InnerText = "Pilih Showroom yang akan di download!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnIDeleteClick(object sender, EventArgs e)
        {

        }

        protected void btnIKirimReceiveAllClick(object sender, EventArgs e)
        {
            if (!(tbIKirimWaktuTerima.Text.Trim() == ""))
            {
                #region "Cek Tanggal"
                TRF_STOCK_HEADER showHeader = new TRF_STOCK_HEADER();
                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                DateTime dt = DateTime.Now;
                DateTime timeNow = DateTime.Now;
                DateTime dtterima = Convert.ToDateTime(tbIKirimWaktuTerima.Text);
                DateTime dtToday = Convert.ToDateTime(string.Format("{0:dd-MM-yyyy}", DateTime.Now));
                //DateTime dtkirim1 = Convert.ToDateTime(tbIKirimWaktuKirim.Text);
                string datetrima = tbIKirimWaktuTerima.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbIKirimWaktuTerima.Text.ToString();
                if (!string.IsNullOrEmpty(datetrima))
                {
                    DateTime.TryParseExact(datetrima, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out timeNow);
                }
                string datekirim = tbIKirimWaktuKirim.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbIKirimWaktuKirim.Text.ToString();

                datetrima = string.Format("{0:yyMM}", timeNow);
                DateTime dtkirim = Convert.ToDateTime(datekirim);
                //Check Lock
                LOGIN_DA loginDA = new LOGIN_DA();
                string whereLock = " where NAME = 'lock'";
                int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
                int dateMove = Convert.ToInt32(string.Format("{0:yyMM}", timeNow));

                if (dateMove > lockMove)
                {
                    List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
                    string kodeKe = tbIKirimKodeKe.Text;//tbIKirimKe.Text;
                    listShowroom = showDA.getShowRoom(string.Format(" where KODE in('{0}')", kodeKe.Trim()));//(" where SHOWROOM in('{0}')", kodeKe.Trim()));

                    if (cekLock(listShowroom.First().STATUS_SHOWROOM, listShowroom.Last().STATUS_SHOWROOM, datetrima))
                    {
                        if (timeNow < dtkirim)
                        {
                            divIKirimMessage.InnerText = "Tanggal Terima Lebih Kecil Dari Tanggal Kirim! Tanggal Kirim : " + string.Format("{0:dd-MMMM-yyyy}", dtkirim) + ", Tanggal Terima : " + string.Format("{0:dd-MMMM-yyyy}", dtterima);
                            divIKirimMessage.Attributes["class"] = "warning";
                            divIKirimMessage.Visible = true;
                            ModalItemKirim.Show();
                        }
                        else if (timeNow > dtToday)
                        {
                            divIKirimMessage.InnerText = "Tanggal Terima Lebih Besar Dari Tanggal Hari Ini!";
                            divIKirimMessage.Attributes["class"] = "warning";
                            divIKirimMessage.Visible = true;
                            ModalItemKirim.Show();
                        }
                        else
                        {
                            #region "Rcv All (Existing" 
                            divIKirimMessage.Visible = false;
                            MS_STOCK_DA stockDA = new MS_STOCK_DA();
                            string sUName = Session["UName"] == null ? "" : Session["UName"].ToString();
                            string noBukti = tbIKirimNoBukti.Text.Trim();

                            DateTime tglTerima = DateTime.Now;
                            string date = tbIKirimWaktuTerima.Text.ToString();
                            if (!string.IsNullOrEmpty(date))
                            {
                                DateTime.TryParseExact(date, "dd-MM-yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTerima);
                            }

                            stockDA.receiveAllTransferStock(noBukti, tglTerima, sUName);

                            bindItemKirim();

                            divIKirimMessage.InnerText = "Receive Berhasil!";
                            divIKirimMessage.Attributes["class"] = "success";
                            divIKirimMessage.Visible = true;
                            #endregion
                        }
                        
                    }
                    else
                    {
                        divIKirimMessage.InnerText = "Tanggal Di Showroom Penerima Sudah Di Lock!";
                        divIKirimMessage.Attributes["class"] = "error";
                        divIKirimMessage.Visible = true;
                        ModalItemKirim.Show();
                    }
                }
               
                #endregion
                
            }
            else
            {
                divIKirimMessage.InnerText = "Isi Tanggal Terima!";
                divIKirimMessage.Attributes["class"] = "warning";
                divIKirimMessage.Visible = true;

                ModalItemKirim.Show();
            }
        }

        protected string GetConnectionString()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            string fileName = ddlDownloadStore.SelectedItem.Text + "_" + tbDownloadDate.Text;
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = Server.MapPath("\\Download\\" + fileName + ".xls");//"D:\\SIS.xlsx";

            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.12.0";
            //props["Extended Properties"] = "Excel 12.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        protected bool uploadNew(string dir, string fileName, string source, string fileType)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;
            int qty = 0;
            string idShow = lbStockIdDari.Text;//Session["UIdStore"].ToString();
            string kode = lbStockKodeDari.Text; //Session["UKode"].ToString();
            string store = tbQTYDari.Text; //Session["UStore"].ToString();
            string error = "";
            string notFound = "";
            string idUpload = "";
            string barcodeWrong = "";
            string user = Session["UName"].ToString();
            MS_KDBRG kdbrg = new MS_KDBRG();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();

            List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
            List<MS_STOCK> stockList = new List<MS_STOCK>();
            List<String> listBarcode = new List<String>();
            List<TEMP_KDBRG> listInsertKdbrg = new List<TEMP_KDBRG>();
            //msKdbrgList = kdbrgDA.getMsKdbrg("");
            msKdbrgList = kdbrgDA.getMsKdbrgArticle("");
            //stockList = stockDA.getStock(""); #Felix - 12082024
            try
            {
                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                cnn.Open();
                System.Data.DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }
                int rowTable = fileType == ".xls" ? 0 : dbSchema.Rows.Count - 1;
                string firstSheetName = dbSchema.Rows[rowTable]["TABLE_NAME"].ToString();
                //string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                //string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                bool check = true;
                check = cekTS(dsOle.Tables[0]);
                int i = 2;
                if (check)
                {

                    ttlTrfQtyFromExcel = dsOle.Tables[0].Rows[0]["TotalQty"].ToString();
                    foreach (DataRow item in dsOle.Tables[0].Rows)
                    {
                        List<MS_KDBRG> listKdbrg = new List<MS_KDBRG>();
                        List<MS_STOCK> listStock = new List<MS_STOCK>();
                        listKdbrg = msKdbrgList.Where(items => items.BARCODE.Trim().ToLower() == item[0].ToString().ToLower()).ToList<MS_KDBRG>();
                        listStock = stockDA.getStock(" WHERE BARCODE = '" + item[0].ToString().Trim().ToLower() + "' AND KODE = '" + kode + "'"); //#Felix - 12082024
                        //listStock = stockList.Where(items => items.BARCODE.Trim().ToLower() == item[0].ToString().Trim().ToLower() && items.KODE == kode).ToList<MS_STOCK>();
                        if ((item[0].ToString() != "" || item[5].ToString() != ""))
                        {
                            if (listStock.Count > 0)
                            {
                                //Cek barcode nya udah masuk atau belum
                                TEMP_KDBRG tempKdbrg = new TEMP_KDBRG();
                                if (listBarcode.Where(itemb => itemb.ToLower() == item[0].ToString().ToLower()).ToList<String>().Count == 0)
                                {
                                    //Insert
                                    Int64 idKDBRG = listKdbrg.Count == 0 ? 0 : listKdbrg.First().ID;
                                    tempKdbrg.ID_KDBRG = idKDBRG;
                                    tempKdbrg.ID_HEADER = Convert.ToInt64(hdnIDHeader.Value);
                                    tempKdbrg.ID_SHOWROOM = Convert.ToInt64(idShow);
                                    tempKdbrg.SHOWROOM = store;
                                    tempKdbrg.KODE = kode;
                                    tempKdbrg.BARCODE = item[0].ToString();
                                    tempKdbrg.ITEM_CODE = item[1].ToString();
                                    tempKdbrg.QTY = Convert.ToInt32(item[5].ToString());
                                    tempKdbrg.CREATED_BY = user;
                                    tempKdbrg.FLAG = "transfer";
                                    tempKdbrg.STAT = "upload";
                                    tempKdbrg.BRAND = listKdbrg.Count == 0 ? "" : listKdbrg.First().FBRAND;
                                    tempKdbrg.ART_DESC = listKdbrg.Count == 0 ? "" : listKdbrg.First().FART_DESC;
                                    tempKdbrg.FCOLOR = listKdbrg.Count == 0 ? "" : listKdbrg.First().FCOL_DESC;
                                    tempKdbrg.FSIZE = listKdbrg.Count == 0 ? "" : listKdbrg.First().FSIZE_DESC;
                                    string idNew = wareDA.insertTempTrfStock(tempKdbrg);
                                    error = idNew.ToLower().Contains("error") ? error + "," + i.ToString() : error;
                                    if (!(idNew.ToLower().Contains("error")))
                                    {
                                        listBarcode.Add(item[0].ToString());
                                        tempKdbrg.ID = Convert.ToInt64(idNew);
                                        listInsertKdbrg.Add(tempKdbrg);
                                    }
                                    insert++;
                                    ttlTrfQty = ttlTrfQty + tempKdbrg.QTY;
                                }
                                else
                                {
                                    //Update Jumlah Kirim
                                    tempKdbrg.ID = listInsertKdbrg.Where(itembt => itembt.BARCODE.ToLower() == item[0].ToString().ToLower()).First().ID;
                                    tempKdbrg.QTY = Convert.ToInt32(item[5].ToString());
                                    wareDA.updateTempTrfStock(tempKdbrg);
                                    doubel++;
                                }
                            }
                            else
                            {
                                //Barcode tidak ditemukan
                                barcodeWrong = barcodeWrong + "," + i.ToString();
                                //notFound = listKdbrg.Count == 0 ? notFound + "," + i.ToString() : notFound;
                            }
                            i++;
                        }
                        else if ((item[0].ToString() != "" || item[5].ToString() != "") && listStock.Count == 0)
                        {
                            barcodeWrong = barcodeWrong + "," + i.ToString();
                        }
                    }

                    string message = "Upload Berhasil! | Data : " + (i - 1).ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
                    message = barcodeWrong == "" ? message : message + " | Barcode Not Found in Stock : " + barcodeWrong.Remove(0, 1);
                    message = notFound == "" ? message : message + " | Barcode Not Found : " + notFound.Remove(0, 1);
                    message = error == "" ? message : message + " | Error line : " + error.Remove(0, 1);
                    DivMessage.InnerText = message;

                    btnStockSave.Enabled = true;
                    lblTtlQtyExcel.Text = ttlTrfQtyFromExcel;
                    lblTtlQtyUpd.Text = ttlTrfQty.ToString();
                }
                else 
                {
                    DivMessage.InnerText = "Qty tidak boleh minus";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                    ret = false;
                    btnStockSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Warehouse > Upload_TRF_Stock > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
        }

        protected void printNoBukti(string noBukti, List<TRF_STOCK_DETAIL> listStock, MS_SHOWROOM show, string tglKirim, string tglTerima)
        {
            Response.Clear();           // Already have this
            Response.ClearContent();    // Add this line
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Packing_List_PL" + noBukti.Remove(0, 2) + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            Document pdfDoc = new Document(new Rectangle(float.Parse(Convert.ToString(8.5 * 72)), float.Parse((10.9d * 72d).ToString())), 10f, 10f, 10f, 10f);
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
          //  PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Office : Jl Ciniru iV no 16", regular)));
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

           // PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124" , regular)));
            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-82404445 ext 133", regular)));
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
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            bottom1.BorderWidthBottom = 0;
            //bottom1.PaddingBottom = 20f;
            table.AddCell(bottom1);

            PdfPCell bottom2 = new PdfPCell(new Phrase(new Chunk("Tanggal Kirim :" + tglKirim, regular)));
            bottom2.Colspan = 2;
            bottom2.BorderWidth = 1;
            bottom2.BorderWidthTop = 0;
            bottom2.BorderWidthBottom = 0;
            
            //bottom2.PaddingBottom = 20f;
            //bottom2.PaddingTop = 3f;
            table.AddCell(bottom2);

            PdfPCell bottom3 = new PdfPCell(new Phrase(new Chunk("Tanggal Terima :" + tglTerima, regular)));
            bottom3.Colspan = 2;
            bottom3.PaddingBottom = 20f;
            //bottom3.PaddingTop = 3f;
            bottom3.BorderWidth = 1;
            bottom3.BorderWidthTop = 0;
            table.AddCell(bottom3);

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

        protected void printDeliveryNotes(string noBukti, List<TRF_STOCK_DETAIL> listStock, MS_SHOWROOM show)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Delivery_Note" + noBukti + ".pdf");
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
            PdfPCell header = new PdfPCell(new Phrase("Delivery Notes", title1));
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

           // PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-87546124", regular)));
            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Telepon : 021-82404445 ext 133", regular)));
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
            PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("No Bukti : " + noBukti, regular)));
            bottom1.Colspan = 2;
            bottom1.PaddingTop = 3f;
            bottom1.BorderWidth = 1;
            bottom1.BorderWidthBottom = 0;
            table.AddCell(bottom1);

            PdfPCell bottom2 = new PdfPCell(new Phrase(new Chunk("No Refference : PL" + noBukti.Remove(0, 2), regular)));
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
                Total = Total + listStock[i].QTY_KIRIM;
            }

            PdfPCell cellNamaTotalQty = new PdfPCell(new Phrase("Total Quantity :", regular));
            cellNamaTotalQty.Colspan = 5;
            cellNamaTotalQty.BorderWidth = 1;
            cellNamaTotalQty.HorizontalAlignment = 0;
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
            */

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

        protected void clearFinish()
        {
            hdnIDHeader.Value = "0";
            tbStockNoBukti.Text = "";
            tbStockKe.Text = "";
            tbStockDari.Text = "";
            tbStockWaktuKirim.Text = "";

            btnStockGenerate.Enabled = true;
            btnStockGetItemCode.Enabled = false;

            deleteTempTrfStock();
        }

        protected void clearFinishPinjam()
        {
            tbPinjamAmbil.Text = "";
            tbPinjamEmail.Text = "";
            tbPinjamKe.Text = "";
            tbPinjamKembali.Text = "";
            tbPinjamKode.Text = "";
            tbPinjamNama.Text = "";
            tbPinjamPhone.Text = "";
            ddlPinjamStatus.SelectedIndex = 0;

            deleteTempTrfStock();
        }

        protected bool checkStock()
        {
            bool check = true;
            int stockAwal = Convert.ToInt32(hdnTRMStock.Value);
            int stockBaru = Convert.ToInt32(tbTRMQTYKirim.Text);
            int diff = stockAwal - stockBaru;
                                                                                                                                        
            if (stockAwal < stockBaru)
            {
                int dif = stockBaru - stockAwal;
                MS_STOCK stock = new MS_STOCK();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();

                stock = stockDA.getStock(String.Format(" where BARCODE = '{0}' and WAREHOUSE = '{1}'", tbTRMBarcode.Text, tbTRMDari.Text)).First();
                if (stock.STOCK < dif)
                {
                    DivMessage.InnerText = "Quantity Stock tidak mencukupi untuk mengupdate data. Silakan cek kembali stock anda.";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                    
                    check = false;
                }

                if (check)
                {
                    stock.STOCK = diff;
                    stockDA.updateDataStockWithOutID(stock);
                }
            }
            return check;
        }

        protected void deleteTempTrfStock()
        {
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
            string user = Session["UName"].ToString();
            wareDA.deleteTempKdbrg(user);
        }

        protected bool upload(string dir, string fileName, string source, string idAcara)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;

            MS_ITEM_ACARA itemAcara = new MS_ITEM_ACARA();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            try
            {
                List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
                //msKdbrgList = kdbrgDA.getMsKdbrg("");
                msKdbrgList = kdbrgDA.getMsKdbrgArticle("");

                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                cnn.Open();
                System.Data.DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }
                //string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                foreach (DataRow item in dsOle.Tables[0].Rows)
                {
                    string brand = item[1].ToString();
                    itemAcara.ID_ACARA = Convert.ToInt64(idAcara);
                    //itemAcara.VALUE_ACARA = tbPUCodeAcara.Text;
                    itemAcara.ITEM_CODE = item[2].ToString();
                    itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.ITEM_CODE == itemAcara.ITEM_CODE && items.FBRAND.Contains(brand)).First().ID;
                    acaraDA.insertMsItemAcara(itemAcara);

                    data++;
                }
                //insertHarga(idUpload.Remove(0, 1));
                DivMessage.InnerText = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
            }
            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Warehouse > Upload > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
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
                StreamReader objReader;
                TextReader txtReader;
                string sLine1 = "";
                string createdBy = Session["UName"].ToString();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                List<MS_STOCK> listStock = new List<MS_STOCK>();
                string noBukti = string.Empty;

                path = source;
                objReader = new StreamReader(path);
                txtReader = new StreamReader(path);

                listStock = stockDA.getStock(string.Format(" where KODE = '{0}'", ddlStoreUpload.SelectedValue));

                while (txtReader.Peek() != -1)
                {
                    sLine1 = txtReader.ReadLine();
                    if (i > 0)//Insert Detail
                    {
                        if (sLine1.Contains(",") && !(sLine1 == ""))//Detail
                        {
                            string[] line = sLine1.Split(',');
                            int stock_akhir_old = 0;
                            int stockPDT = int.Parse(line[1]);
                            int bedaStock = 0;
                            List<MS_STOCK> stockListDetail = new List<MS_STOCK>();
                            stockListDetail = listStock.Where(items => items.BARCODE == line[0]).ToList<MS_STOCK>();

                            List<MS_KDBRG> kdbrgListSelected = new List<MS_KDBRG>();
                            kdbrgListSelected = listKDBRG.Where(itemk => itemk.BARCODE == line[0]).ToList<MS_KDBRG>();
                            if (kdbrgListSelected.Count > 0)
                            {
                                MS_KDBRG kdbrg = new MS_KDBRG();
                                kdbrg = kdbrgListSelected.First();

                                if (stockListDetail.Count > 0)//Update Data Stock
                                {
                                    MS_STOCK stock = new MS_STOCK();
                                    stock = stockListDetail.FirstOrDefault();
                                    if (!(stock.STOCK == stockPDT))//Bila stock pdt tidak sama dengan stock di sistem
                                    {
                                        stock_akhir_old = stock.STOCK;
                                        bedaStock = stockPDT - stock_akhir_old;
                                        stock.STOCK = bedaStock;

                                        stockDA.updateDataStockWithID(stock);

                                        //Insert ke TRF_Detail
                                    }
                                }
                                else //Insert Data Stock
                                {
                                    MS_STOCK stock = new MS_STOCK();

                                    stock.ITEM_CODE = kdbrg.ITEM_CODE;
                                    stock.BARCODE = sLine1[0].ToString();
                                    stock.WAREHOUSE = ddlStoreUpload.SelectedItem.Text;
                                    stock.KODE = ddlStoreUpload.SelectedValue;
                                    stock.STOCK = stockPDT;
                                    stock.RAK = rak;
                                    stock.CREATED_BY = Session["UName"].ToString();
                                    stockDA.insertDataStock(stock);
                                }

                                TRF_STOCK_DETAIL trfStock = new TRF_STOCK_DETAIL();
                                trfStock.ID_HEADER = Convert.ToInt64(newID);
                                trfStock.ID_KDBRG = kdbrg.ID;
                                trfStock.NO_BUKTI = noBukti;
                                trfStock.ITEM_CODE = kdbrg.ITEM_CODE;
                                trfStock.BARCODE = kdbrg.BARCODE;
                                trfStock.QTY_KIRIM = bedaStock;
                                trfStock.QTY_TERIMA = bedaStock;
                                trfStock.STOCK_AKHIR_KIRIM = stockPDT;
                                trfStock.STOCK_AKHIR_TERIMA = stockPDT;
                                trfStock.USER_KIRIM = Session["UName"].ToString();
                                trfStock.USER_TERIMA = Session["UName"].ToString();
                                trfStock.ALASAN = "Stock Opname";
                                trfStock.REFF = "Done";
                                stockDA.insertTrfStockOpname(trfStock);
                            }
                        }
                        else if (!(sLine1 == ""))//RAK
                        {
                            rak = sLine1;
                        }
                    }
                    else //Insert Header
                    {
                        DateTime dt = SqlDateTime.MaxValue.Value;
                        string[] line = sLine1.Split(',');
                        tanggalSO = line[1];
                        tanggal = tanggalSO.Remove(3) + "20" + tanggalSO.Remove(0, 4);
                        if (!string.IsNullOrEmpty(tanggal))
                        {
                            DateTime.TryParseExact(tanggal, "ddMMyyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                        }

                        TRF_STOCK_HEADER stockHeader = new TRF_STOCK_HEADER();
                        stockHeader.DARI = "STOCK OPNAME";
                        stockHeader.KE = ddlStoreUpload.SelectedItem.Text;
                        stockHeader.KODE_DARI = "SO-SO-001";
                        stockHeader.KODE_KE = ddlStoreUpload.SelectedValue;
                        stockHeader.WAKTU_KIRIM = dt;
                        stockHeader.WAKTU_TERIMA = dt;
                        stockHeader.CREATED_BY = Session["UName"].ToString();
                        newID = stockDA.insertHeaderOpnameRetID(stockHeader);

                        noBukti = "SO" + tanggalSO + (newID.Length > 5 ? newID.Remove(0, newID.Length - 6) : newID.PadLeft(6, '0'));
                    }
                    i++;
                }
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

        protected void insertUpdateNoDoc(Int64 idNoDoc, string noUrut, string flag)
        {
            SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
            string kodeStore = Session["UKode"].ToString();
            NO_DOC noDoc = new NO_DOC();

            noDoc.ID = idNoDoc;
            noDoc.NO_URUT = int.Parse(noUrut);
            noDoc.KODE = kodeStore;
            noDoc.CREATED_BY = Session["UName"].ToString();
            noDoc.FLAG = flag;
            if (Session["INoBon"] == null)
            {
                Session["INoBon"] = "noBon";
                string ret = noUrut == "1" ? shBayarDA.insertNoDoc(noDoc) : shBayarDA.updateNoDoc(noDoc);
                //ret = noUrut == "1" ? shBayarDA.insertNoDoc(noDoc) : shBayarDA.updateNoDoc(noDoc);
            }
        }

        protected bool cekLock(string statusKode1, string statusKode2, string time)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            bool ret = true;

            string name1 = statusKode1 == "FSS" ? "lockFSS" : statusKode1 == "SIS" ? "lockSIS" : "lockHO";
            string name2 = statusKode2 == "FSS" ? "lockFSS" : statusKode2 == "SIS" ? "lockSIS" : "lockHO";

            listParam = loginDA.getListParam(string.Format(" where name in ('{0}','{1}')", name1, name2));

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

        protected void btnAMSave_Click(object sender, EventArgs e)
        {

        }

        protected bool cekTS(DataTable dTab)
        {
            bool check = true;
          
                for (int i = 0; i < dTab.Rows.Count; i++)
                {
                    //if (dTab.Rows[i][0].ToString().Length > 5)
                    //{
                        string qty = dTab.Rows[i][5].ToString();
                       
                         if (Convert.ToInt32(qty) < 0 )
                        {
                            check = false;

                            DivMessage.InnerText = "Qty Tidak Boleh minus";
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;

                            i = dTab.Rows.Count + 1;
                        }
                       
                    }
                //}
            return check;
        }

        protected void tbItemSearch_TextChanged(object sender, EventArgs e)
        {
            bindItemCode();
        }
        #region "Direct Print PDF From RDLC"
        protected void LoadRDLCData(string IDPO)
        {
            //REPORT_DA rptDA = new REPORT_DA();
            //LocalReport report = new LocalReport();
            //report.ReportEmbeddedResource = "~/Reports/DeliveryNotes_TrfStock.rdlc";
            //report.DataSources.Add(new ReportDataSource("DataSet1", rptDA.GetDataTF_RptDeliveryNotesTrfStock(IDPO)));
            //report.PrintToPrinter();
            REPORT_DA rptDA = new REPORT_DA();
            DataSet dsRptData = new DataSet();
            ReportViewer.ProcessingMode = ProcessingMode.Local;
            ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/DeliveryNotes_TrfStock.rdlc");
            dsRptData = rptDA.GetDataTF_RptDeliveryNotesTrfStock(IDPO);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dsRptData.Tables[0]);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(datasource);
        }
        protected void exportRpt(string NO_PO)
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension = "PDF";
            string FileName = "DeliveryNotes_" + NO_PO + "." + extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);

            Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event
        }
        protected void LoadRDLCDataPackingList(string IDPO)
        {
            //REPORT_DA rptDA = new REPORT_DA();
            //LocalReport report = new LocalReport();
            //report.ReportEmbeddedResource = "~/Reports/DeliveryNotes_TrfStock.rdlc";
            //report.DataSources.Add(new ReportDataSource("DataSet1", rptDA.GetDataTF_RptDeliveryNotesTrfStock(IDPO)));
            //report.PrintToPrinter();
            REPORT_DA rptDA = new REPORT_DA();
            DataSet dsRptData = new DataSet();
            string path = string.Format(@"Reports\{0}", "PackingList_TrfStock.rdlc");
            ReportViewerPackingList.ProcessingMode = ProcessingMode.Local;
            ReportViewerPackingList.LocalReport.ReportPath = Server.MapPath("~/Reports/PackingList_TrfStock.rdlc");
            dsRptData = rptDA.GetDataTF_RptPackingListTrfStock(IDPO);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dsRptData.Tables[0]);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(datasource);
        }
        protected void exportRptPackingList(string NO_PO)
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension = "PDF";
            string FileName = "PackingList" + NO_PO + "." + extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewerPackingList.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);

            Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event
        }
        public void DirectPrinPackingList(string NO_PO, string IDPO)
        {
            REPORT_DA rptDA = new REPORT_DA();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = "pdf";// string.Empty;
            DataSet dsGrpSum, dsActPlan, dsProfitDetails,
                dsProfitSum, dsSumHeader, dsDetailsHeader, dsBudCom = null;

          
            DataSet dsData = rptDA.GetDataTF_RptPackingListTrfStock(IDPO);
            ReportDataSource rdsAct = new ReportDataSource("DataSet1", dsData.Tables[0]);
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = "Reports/PackingList_TrfStock.rdlc"; //This is your rdlc name.
            //viewer.LocalReport.SetParameters(param);
            viewer.LocalReport.DataSources.Add(rdsAct); // Add  datasource here         
            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
            // System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= PackingList_TransferStock_" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }
        protected void LoadRDLCDataPeminjaman(string ID_HEADER)
        {
            LAPORAN_DA rptDA = new LAPORAN_DA();
            DataSet dsRptData = new DataSet();
            ReportViewer.ProcessingMode = ProcessingMode.Local;
            ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/PrintPeminjaman.rdlc");
            dsRptData = rptDA.GetDatavw_PrintPeminjaman(ID_HEADER);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dsRptData.Tables[0]);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(datasource);
        }
        protected void exportRpt2(string NO_Bukti)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string FileName = "Peminjaman" + NO_Bukti;

            byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");
            Response.BinaryWrite(bytes); // create the file    
            Response.Flush();
        }

        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void gvLPeminjaman_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgPdf = e.Row.FindControl("imgPdf") as ImageButton;
                //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(imgPdf);
                ScriptManager.GetCurrent(this).RegisterPostBackControl(imgPdf);
            }
        }
        protected bool cekLockPeminjaman(string KodeShr, DateTime time)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            List<MS_SHOWROOM> listShowroom = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            listShowroom = showDA.getShowRoom(string.Format(" where KODE in('{0}')", KodeShr));
            int date = Convert.ToInt32(string.Format("{0:yyMM}", time));

            bool ret = true;

            string name1 = listShowroom.First().STATUS_SHOWROOM == "FSS" ? "lockFSS" : listShowroom.First().STATUS_SHOWROOM == "SIS" ? "lockSIS" : "lockHO";

            listParam = loginDA.getListParam(string.Format(" where name in ('{0}')", name1));

            //time = 1601 ; param1 = 1510 ; param2 = 1601
            foreach (MS_PARAMETER item in listParam)
            {

                int nowTime = Convert.ToInt32(date);
                int value = Convert.ToInt32(item.VALUE);
                if (!(nowTime > value))
                {
                    ret = false;
                }
            }

            return ret;
        }

        protected void btnFupAdjStock_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";
            string filePath = string.Empty;

            MS_SHOWROOM_DA MSShrDA = new MS_SHOWROOM_DA();

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);
            if (ExcelFileName != "")
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(FileUpload.FileName);

                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FileUpload.PostedFile.SaveAs(filePath);
                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    string Kode_SHR = ddlStoreUpload.SelectedValue;
                    string SHR = ddlStoreUpload.SelectedItem.ToString();
                    string user = Session["UName"].ToString();

                    DateTime tanggalTrans = DateTime.Now;
                    string sTanggal = txTanggalAdj.Text;
                    if (!string.IsNullOrEmpty(sTanggal))
                    {
                        DateTime.TryParseExact(sTanggal, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out tanggalTrans);
                    }

                    int rescek = MSShrDA.cekAdjustmentUp(FileUploadName, source, FileType, user);
                    if (rescek > 0)
                    {
                        dMsgUpAdjust.InnerText = "Error : Data yang Di Upload Tidak Lengkap";
                        dMsgUpAdjust.Attributes["class"] = "error";
                        dMsgUpAdjust.Visible = true;
                    }
                    else
                    {
                        string res = MSShrDA.upTrAdjustment(FileUploadName, source, FileType, Kode_SHR, SHR, user, tanggalTrans);
                        dMsgUpAdjust.InnerText = res;
                        dMsgUpAdjust.Attributes["class"] = "success";
                        dMsgUpAdjust.Visible = true;
                    }

                    ModalUploadAdjust.Show();
                }
            }
        }
    }
}