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

namespace ATMOS_SROM.Sales
{
    public partial class Whole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                firstDelete();
                deleteTempAcara();
                bindGrid();
                //bindStore();
            }
        }

        protected void bindGrid()
        {
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            string user = Session["UName"] == null ? "" : Session["UName"].ToString();
            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + user + "'");
            gvMain.DataSource = struckList;
            gvMain.DataBind();

            btnSave.Enabled = struckList.Count > 0 ? true : false;

            DivMessage.Visible = false;
        }

        protected void bindStore()
        {
            string store = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            divStore.Visible = store.ToLower() == "head office" ? true : false;
            if (divStore.Visible)
            {
                MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                MS_SHOWROOM showRoom = new MS_SHOWROOM();

                showRoom.SHOWROOM = "--Pilih Showroom--";
                listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS'");
                listStore.Insert(0, showRoom);
                //ddlStore.DataSource = listStore;
                //ddlStore.DataBind();
                int index = 0;
                int.TryParse(hdnIDStore.Value, out index);
                //ddlStore.SelectedIndex = index;
            }
        }

        protected void bindSearch()
        {
            //DateTime tglTrans = DateTime.Now;
            //string date = tbDate.Text.ToString();
            //if (!string.IsNullOrEmpty(date))
            //{
            //    DateTime.TryParseExact(date, "dd-MM-yyyy",
            //    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            //}

            //List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();
            //MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            //string search = ddlSearchBy.SelectedValue + " like '%" + tbSearchBy.Text + "%'";

            //string sKode = "WARE-001";//Session["UKode"] == null ? "" : Session["UKode"].ToString();
            ////kdbrgList = kdbrgDA.getMsKdbrg(search, "WARE-001");
            //kdbrgList = kdbrgDA.getMsKdbrgWholesale(tglTrans, sKode, " where KODE = '" + sKode + "' and " + search);
            //gvSearch.DataSource = kdbrgList;
            //gvSearch.DataBind();

            //ModalSearchItemCode.Show();
        }

        protected void bindAllWholeSale()
        {
            //SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            //List<SH_PUTUS_HEADER> putusHeaderList = new List<SH_PUTUS_HEADER>();
            //string where = tbBONSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%'", ddlBONSearch.SelectedValue, tbBONSearch.Text);

            //putusHeaderList = bayarDA.getSHPutusHeader(where);
            //gvBON.DataSource = putusHeaderList;
            //gvBON.DataBind();

            //ModalSearchNoBon.Show();
        }

        protected void bindItemWholeSale()
        {
            //SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            //List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
            //string where = string.Format(" where ID_BAYAR like {0}", hdnIDBayarRetur.Value);

            //putusDetailList = bayarDA.getSHPutusDetail(where);
            //gvIRetur.DataSource = putusDetailList;
            //gvIRetur.DataBind();

            //ModalItemRetur.Show();
        }

        protected void firstDelete()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            kdbrgDA.deleteStruck(namaKasir);
        }

        protected void deleteTempAcara()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            kdbrgDA.deleteTempAcara(namaKasir);
        }

        protected void gvSearcRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName.ToLower() != "page")
                //{
                //    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //    int rowIndex = grv.RowIndex;

                //    if (e.CommandName.ToLower() == "saverow")
                //    {
                //        string barcode = gvSearch.Rows[rowIndex].Cells[2].Text.ToString();
                //        tbBarcode.Text = barcode;
                //        inputIntoTempStruck();
                //    }
                //}
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
            //gvSearch.PageIndex = e.NewPageIndex;
            //bindSearch();
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName.ToLower() != "page")
                //{
                //    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //    int rowIndex = grv.RowIndex;
                //    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();
                //    if (e.CommandName.ToLower() == "editrow")
                //    {
                //        hdnId.Value = id;
                //        tbPUBarcode.Text = gvMain.Rows[rowIndex].Cells[3].Text;
                //        tbPUItemCode.Text = gvMain.Rows[rowIndex].Cells[4].Text;
                //        tbPUDesc.Text = gvMain.Rows[rowIndex].Cells[5].Text;
                //        tbPUSize.Text = gvMain.Rows[rowIndex].Cells[7].Text;
                //        tbPUQty.Text = gvMain.Rows[rowIndex].Cells[8].Text;
                //        tbPUPrice.Text = gvMain.Rows[rowIndex].Cells[9].Text.Replace(".00", "");
                //        ModalChangeQty.Show();
                //    }
                //}
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void gvBONCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName.ToLower() != "page")
                //{
                //    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //    int rowIndex = grv.RowIndex;
                //    string id = gvBON.DataKeys[rowIndex]["ID"].ToString();
                //    if (e.CommandName.ToLower() == "saverow")
                //    {
                //        hdnIDBayarRetur.Value = id;
                //        tbIReturNoBon.Text = gvBON.Rows[rowIndex].Cells[3].Text;
                //        tbIReturStore.Text = gvBON.Rows[rowIndex].Cells[2].Text;

                //        bindItemWholeSale();
                //    }
                //}
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void gvBONPageChanging(object sender, GridViewPageEventArgs e)
        {
            //gvBON.PageIndex = e.NewPageIndex;
            //bindAllWholeSale();
        }

        protected void gvIReturCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName.ToLower() != "page")
                //{
                //    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //    int rowIndex = grv.RowIndex;
                //    string id = gvIRetur.DataKeys[rowIndex]["ID"].ToString();
                //    if (e.CommandName.ToLower() == "saverow")
                //    {
                //        //Insert ke Temp Retur retur
                //        inputIntoTempStruckRetur();
                //    }
                //}
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void gvIReturPageChanging(object sender, GridViewPageEventArgs e)
        {
            //gvIRetur.PageIndex = e.NewPageIndex;
            //bindItemWholeSale();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            //tbSearchBy.Text = tbBarcode.Text;
            //bindSearch();
        }

        protected void btnSearchSearchClick(object sender, EventArgs e)
        {
            bindSearch();
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            inputIntoTempStruck();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Decimal totalPrice = 0;
            int totalQty = 0;
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            SH_PUTUS_HEADER putusHeader = new SH_PUTUS_HEADER();
            string user = Session["UName"] == null ? "" : Session["UName"].ToString();

            foreach (GridViewRow item in gvMain.Rows)
            {
                totalPrice = totalPrice + Convert.ToDecimal(item.Cells[12].Text);
                totalQty = totalQty + Convert.ToInt32(item.Cells[8].Text);
            }

            putusHeader.KODE_CUST = tbNama.Text;
            putusHeader.KODE = tbKode.Text;
            putusHeader.STATUS_STORE = "WHOLESALE";
            putusHeader.QTY = totalQty;
            putusHeader.CREATED_BY = user;
            string idHeader = bayarDA.insertPutusHeaderRetID(putusHeader);

            if (!(idHeader.Contains("ERROR")))
            {
                SH_PUTUS_DETAIL putusDetail = new SH_PUTUS_DETAIL();
                string id = idHeader.Length > 5 ? idHeader.Remove(0, idHeader.Length - 5) : idHeader.PadLeft(5, '0');
                DateTime dt = DateTime.Now;
                string tgl = string.Format("{0:yyddMM}", dt);
                string noBon = tgl + id;
                int margin = int.Parse(tbMargin.Text);

                putusDetail.ID_BAYAR = Convert.ToInt64(idHeader);
                putusDetail.KODE_CUST = tbNama.Text;
                putusDetail.KODE = tbKode.Text;
                putusDetail.NO_BON = "SO" + noBon;
                //putusDetail.MARGIN = int.Parse(tbMargin.Text);
                putusDetail.MARGIN = Convert.ToDecimal(tbMargin.Text);
                bayarDA.insertPutusDetail(putusDetail, user);

                decimal totalPenjualan = Convert.ToDecimal(totalPrice * Convert.ToDecimal(Convert.ToDecimal(100 - margin) / 100));

                DateTime tglTrans = DateTime.Now;
                string date = tbDate.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                }
                putusHeader.TGL_TRANS = tglTrans;
                putusHeader.NO_BON = "SO" + noBon;
                putusHeader.NET_CASH = totalPenjualan;
                putusHeader.JM_UANG = totalPenjualan;
                putusHeader.KEMBALI = 0;
                putusHeader.NET_BAYAR = totalPenjualan;
                putusHeader.ID = Convert.ToInt64(idHeader);

                bayarDA.updatePutusHeader(putusHeader);

                //lbDONEBON.Text = "SO" + noBon;
                //lblDONEChange.Text = string.Format("{0:0,0.00}", Convert.ToDouble(totalPenjualan)); ;
                //ModalChange.Show();
            }
            else
            {
                DivMessage.InnerText = idHeader;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }
        
        protected void btnPUSave_Click(object sender, EventArgs e)
        {
            //MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            //int qty = int.Parse(tbPUQty.Text);
            //int price = int.Parse(tbPUPrice.Text.Replace(",", ""));
            //Int64 id = Convert.ToInt64(hdnId.Value);
            //kdbrgDA.updateQtyAndPrice(qty, price, id);

            //bindGrid();

            //DivMessage.InnerText = "Update Berhasil";
            //DivMessage.Attributes["class"] = "success";
            //DivMessage.Visible = true;
        }

        protected void btnAllWholeSaleClick(object sender, EventArgs e)
        {
            bindAllWholeSale();
        }

        protected void btnBONSearchClick(object sender, EventArgs e)
        {
            bindAllWholeSale();
        }

        protected void bIReturCloseClick(object sender, EventArgs e)
        {
            bindAllWholeSale();
        }

        protected void btnReturClick(object sender, EventArgs e)
        {

        }

        protected void bDONEClose_Click(object sender, EventArgs e)
        {
            firstDelete();
            clearForm();
            bindGrid();
            bindStore();

            DivMessage.InnerText = "Pembuatan Sales Order Berhasil!";
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true;
        }

        protected void inputIntoTempStruck()
        {
            //Search Data Item Code ada atau tidak
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            string itemCode = tbBarcode.Text;

            DateTime tglTrans = DateTime.Now;
            string date = tbDate.Text.ToString();
            string sKode = "WARE-001";//Session["UKode"] == null ? "" : Session["UKode"].ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }

            if (Session["UStore"].ToString() == "Head Office")
            {
                //listKdBrg = kdbrgDA.getMsKdbrgSIS(tglTrans, " where BARCODE = '" + itemCode + "'");
            }
            else
            {
                //listKdBrg = kdbrgDA.getMsKdbrg(" where BARCODE = '" + itemCode + "'");
            }
            listKdBrg = kdbrgDA.getMsKdbrgWholesale(tglTrans, "WARE-001", " where BARCODE = '" + itemCode + "' and KODE = '" + sKode + "'");

            //Jika Item Code Terdaftar
            if (listKdBrg.Count > 0)
            {
                MS_KDBRG kdbrg = new MS_KDBRG();
                kdbrg = listKdBrg.First();

                if (kdbrg.STOCK > 0)
                {
                    MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                    MS_SHOWROOM show = new MS_SHOWROOM();

                    show = showDA.getShowRoom(" where KODE = 'WARE-001'").First();
                    string kasir = Session["UName"].ToString();

                    kdbrg.ART_PRICE = kdbrg.PRICE;
                    kdbrg.NET_PRICE = kdbrg.PRICE;
                    kdbrg.BON_PRICE = kdbrg.PRICE;
                    kdbrg.RETUR = "No";

                    string insert = kdbrgDA.insertTempStruckWholeSale(kdbrg, show, kasir);

                    if (insert == "Berhasil!")
                    {
                        DivMessage.InnerText = "Insert Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        tbBarcode.Text = "";
                        bindGrid();

                        tbBarcode.Focus();
                    }
                    else
                    {
                        DivMessage.InnerText = "Error : " + insert;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Tidak Terdapat Stock Di Sistem, Harap Hubungi Admin.";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Barcode Tidak Ditemukan";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void inputIntoTempStruckRetur()
        {

        }

        protected string changeCurr(string curr)
        {
            for (int i = 0; i < Math.Floor(Convert.ToDecimal((curr.Length - (1 + i)) / 3)); i++)
            {
                curr = curr.Substring(0, curr.Length - (4 * i + 3)) + "," + curr.Substring(curr.Length - (4 * i + 3));
            }
            return curr;
        }

        protected void clearForm()
        {
            tbDate.Text = "";
            tbNama.Text = "";
            tbKode.Text = "";
            tbMargin.Text = "";
        }
    }
}