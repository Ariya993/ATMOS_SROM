using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Sales
{
    public partial class WholeSalesManualInput : System.Web.UI.Page
    {
        #region "BindGridMethod"
        protected void bindSearch()
        {
            DateTime tglTrans = DateTime.Now;
            string date = tbDate.Text.ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }

            List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            string search = ddlSearchBy.SelectedValue + " like '%" + tbSearchBy.Text + "%'";

            string sKode = "WARE-001";//Session["UKode"] == null ? "" : Session["UKode"].ToString();
            //kdbrgList = kdbrgDA.getMsKdbrg(search, "WARE-001");
            kdbrgList = kdbrgDA.getMsKdbrgWholesale(tglTrans, sKode, " where KODE = '" + sKode + "' and " + search);
            gvSearch.DataSource = kdbrgList;
            gvSearch.DataBind();

            ModalSearchItemCode.Show();
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
        protected void bindHeader()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            TEMP_WHOLESALE tempWhole = new TEMP_WHOLESALE();
            List<TEMP_WHOLESALE> listTempWhole = new List<TEMP_WHOLESALE>();
            listTempWhole = kdbrgDA.getTempWholesale(" where CREATED_BY = '" + namaKasir + "'");

            if (listTempWhole.Count > 0)
            {
                tempWhole = listTempWhole.First();

                tbDate.Text = string.Format("{0:dd-MM-yyyy}", tempWhole.TGL_PENJUALAN);
                lbTglTrans.Text = tbDate.Text;

                tbDateKirim.Text = string.Format("{0:dd-MM-yyyy}", tempWhole.TGL_PENGIRIMAN);
                lbTglKirim.Text = tbDateKirim.Text;

                //tbNama.Text = tempWhole.NAMA_PEMBELI;
                //lbNamaPembeli.Text = tbNama.Text;

                //tbKode.Text = tempWhole.KODE_PEMBELI;
                //lbKodePembeli.Text = tbKode.Text;

                tbMargin.Text = tempWhole.MARGIN.ToString();
                lbMargin.Text = tbMargin.Text;
            }
        }
        protected void bindCustSO()
        {
            MS_CUST_WHOLESALE_DA CustSoDA = new MS_CUST_WHOLESALE_DA();
            DataSet DsCustSO = new DataSet();

            //DsBank.Tables[0].Rows.Add(workRow);
            DsCustSO = CustSoDA.GetMS_CUST_WHOLESALE("Where BLOCK = 0");
            DataRow workRow = DsCustSO.Tables[0].NewRow();
            workRow["KD_PEMBELI"] = "-";
            workRow["NM_PEMBELI"] = "-";
            DsCustSO.Tables[0].Rows.InsertAt(workRow, 0);
            ddlCustSO.DataSource = DsCustSO;
            ddlCustSO.DataBind();
        }
        protected void bindHeaderSOEdit()
        {
            SO_SHOWLESALES_DA tstDa = new SO_SHOWLESALES_DA();
            List<SO_WHOLESALES_HEADER> ListSOHeader = new List<SO_WHOLESALES_HEADER>();
            string where = string.Format("WHERE ID = '" + idSOHeaderEdit.Text + "'");
            ListSOHeader = tstDa.getSoHeader(where);
            DateTime dtTrans = Convert.ToDateTime(ListSOHeader.FirstOrDefault().TGL_TRANS);
            DateTime dtSend = Convert.ToDateTime(ListSOHeader.FirstOrDefault().SEND_DATE);
            tbDate.Text = dtTrans.ToString("dd-MM-yyyy");
            tbDateKirim.Text = dtSend.ToString("dd-MM-yyyy");
            txtNoPo.Text = ListSOHeader.FirstOrDefault().NO_PO;
            tbMargin.Text = Convert.ToString(ListSOHeader.FirstOrDefault().MARGIN);
            txtNoSO.Text = ListSOHeader.FirstOrDefault().NO_SO;
            lblNoPOEdit.Text = ListSOHeader.FirstOrDefault().NO_PO;
            ddlCustSO.ClearSelection();
            if (ddlCustSO.Items.FindByValue(ListSOHeader.FirstOrDefault().KODE) != null)
            {
                ddlCustSO.Items.FindByValue(ListSOHeader.FirstOrDefault().KODE).Selected = true;
            }
            if (ListSOHeader.FirstOrDefault().FRETUR == "No")
            {
                cbRetur.Checked = false;
            }
            else
            {
                cbRetur.Checked = true;
            }

            tdNoSO.Visible = true;
            tdNoSOValue.Visible = true;
            btnSave.Enabled = true;

        }
        protected void BindEditSoDetail()
        {
            SO_SHOWLESALES_DA tstDa = new SO_SHOWLESALES_DA();
            List<SO_WHOLESALES> listSoDetail = new List<SO_WHOLESALES>();
            string where = string.Format("WHERE NO_SO = '" + txtNoSO.Text + "'");

            //listSoDetail = tstDa.GetSODetail(where);
            //gvMain.DataSource = listSoDetail;
            //gvMain.DataBind();
            tstDa.CopySoDetailtoTempStruck(where);
            bindGrid();
        }

        #endregion
        #region Method"
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

                if (kdbrg.STOCK != 0 || kdbrg.STOCK == 0)
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

                    if (gvMain.Rows.Count == 0)
                    {
                        DateTime tglTransHeader = DateTime.Now;
                        string dateTrans = tbDate.Text.ToString();
                        if (!string.IsNullOrEmpty(dateTrans))
                        {
                            DateTime.TryParseExact(date, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTransHeader);
                        }

                        DateTime tglKirim = DateTime.Now;
                        string dateKirim = tbDateKirim.Text.ToString();
                        if (!string.IsNullOrEmpty(dateKirim))
                        {
                            DateTime.TryParseExact(date, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out tglKirim);
                        }

                        TEMP_WHOLESALE temp = new TEMP_WHOLESALE();
                        temp.TGL_PENJUALAN = tglTransHeader;
                        temp.TGL_PENGIRIMAN = tglKirim;
                        temp.NAMA_PEMBELI = ddlCustSO.SelectedItem.Text;
                        temp.KODE_PEMBELI = ddlCustSO.SelectedItem.Value;

                        //temp.NAMA_PEMBELI = tbNama.Text;
                        //temp.KODE_PEMBELI = tbKode.Text;
                        //temp.MARGIN = Convert.ToInt32(tbMargin.Text);
                        temp.MARGIN = Decimal.Parse(tbMargin.Text);

                        temp.CREATED_BY = kasir;

                        kdbrgDA.insertTempHeaderWholesale(temp);
                    }
                    else if (!checkHeader())
                    {
                        DateTime tglTransHeader = DateTime.Now;
                        string dateTrans = tbDate.Text.ToString();
                        if (!string.IsNullOrEmpty(dateTrans))
                        {
                            DateTime.TryParseExact(date, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTransHeader);
                        }

                        DateTime tglKirim = DateTime.Now;
                        string dateKirim = tbDateKirim.Text.ToString();
                        if (!string.IsNullOrEmpty(dateKirim))
                        {
                            DateTime.TryParseExact(date, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out tglKirim);
                        }

                        TEMP_WHOLESALE temp = new TEMP_WHOLESALE();
                        temp.TGL_PENJUALAN = tglTransHeader;
                        temp.TGL_PENGIRIMAN = tglKirim;
                        //temp.NAMA_PEMBELI = tbNama.Text;
                        //temp.KODE_PEMBELI = tbKode.Text;
                        temp.NAMA_PEMBELI = ddlCustSO.SelectedItem.Text;
                        temp.KODE_PEMBELI = ddlCustSO.SelectedItem.Value;
                        //temp.MARGIN = Convert.ToInt32(tbMargin.Text);
                        temp.MARGIN = Decimal.Parse(tbMargin.Text);

                        temp.CREATED_BY = kasir;

                        kdbrgDA.updateTempHeaderWholesale(temp);

                        lbTglTrans.Text = tbDate.Text;
                        lbTglKirim.Text = tbDateKirim.Text;
                        lbNamaPembeli.Text = ddlCustSO.SelectedItem.Text;
                        lbKodePembeli.Text = ddlCustSO.SelectedItem.Value;
                        //lbNamaPembeli.Text = tbNama.Text;
                        //lbKodePembeli.Text = tbKode.Text;
                        lbMargin.Text = tbMargin.Text;
                    }

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
        protected bool checkHeader()
        {
            bool ret = true;
            //tbNama.Text.ToLower() != lbNamaPembeli.Text.ToLower() || tbKode.Text.ToLower() != lbKodePembeli.Text.ToLower()
            if (tbDate.Text.ToLower() != lbTglTrans.Text.ToLower() || tbDateKirim.Text.ToLower() != lbTglKirim.Text.ToLower()
               || tbMargin.Text.ToLower() != lbMargin.Text.ToLower() || ddlCustSO.SelectedItem.Value == "-")
            {
                ret = false;
            }
            return ret;
        }
        protected void deleteTempAcara()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            kdbrgDA.deleteTempAcara(namaKasir);
        }
        protected void ClearTxtHeader()
        {
            tbDate.Text = "";
            tbDateKirim.Text = "";
            txtNoPo.Text = "";
            //tbNama.Text = "";
            //tbKode.Text = "";
            tbMargin.Text = "";
            cbRetur.Checked = false;
            ddlCustSO.SelectedIndex = 0;
            txtNoSO.Text = "";
            tdNoSO.Visible = false;
            tdNoSOValue.Visible = false;
        }
        protected void firstDelete()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            kdbrgDA.deleteStruck(namaKasir);
            kdbrgDA.deleteHeaderWholesale(namaKasir);
        }
        protected void clearForm()
        {
            tbDate.Text = "";
            //tbNama.Text = "";
            //tbKode.Text = "";
            tbMargin.Text = "";
            tbDateKirim.Text = "";
            ddlCustSO.SelectedIndex = 0;
            txtNoSO.Text = "";
            tdNoSO.Visible = false;
            tdNoSOValue.Visible = false;
        }
        protected void clearGrid()
        {
            List<SH_PUTUS_DETAIL> putusDetailList = new List<SH_PUTUS_DETAIL>();
            //gvIRetur.DataSource = putusDetailList;
            //gvIRetur.DataBind();
        }
        protected void UpdateSOWholesale(string namaKasir)
        {
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            SO_WHOLESALES_HEADER SoHeader = new SO_WHOLESALES_HEADER();
            int cekpo = 0;
            int QtySO = 0;
            foreach (GridViewRow item in gvMain.Rows)
            {
                QtySO = QtySO + Convert.ToInt32(item.Cells[8].Text);
            }

            SoHeader.ID = Convert.ToInt64(idSOHeaderEdit.Text);
            SoHeader.QTY = QtySO;
            SoHeader.NO_SO = txtNoSO.Text;
            SoHeader.KODE_CUST = ddlCustSO.SelectedItem.Text;
            SoHeader.KODE = ddlCustSO.SelectedItem.Value;
            SoHeader.TGL_TRANS = Convert.ToDateTime(tbDate.Text);
            SoHeader.SEND_DATE = Convert.ToDateTime(tbDateKirim.Text);
            SoHeader.MARGIN = Decimal.Parse(tbMargin.Text);
            SoHeader.FRETUR = cbRetur.Checked ? "Yes" : "No";
            SoHeader.UPDATED_BY = namaKasir;
            //SoHeader.NO_PO = txtNoPo.Text.Trim();
            if (lblNoPOEdit.Text.Trim() != txtNoPo.Text.Trim())
            {
                cekpo = Convert.ToInt32(tstDA.CekNoPOInSOWholeSales(" WHERE NO_PO = '" + txtNoPo.Text.Trim() + "'"));
                if (cekpo == 0)
                {
                    SoHeader.NO_PO = txtNoPo.Text.Trim();
                }
                else
                {
                    DivMessage.InnerText = "NO PO Sudah pernah digunakan untuk membuat SO!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                SoHeader.NO_PO = txtNoPo.Text.Trim();
            }
            if (cekpo == 0)
            {
                tstDA.UpdateSoWholesalesHeader(SoHeader);
                tstDA.UpdateSoWholesalesDetail(SoHeader);
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                kdbrgDA.deleteStruck(namaKasir);
                bindGrid();
                DivMessage.InnerText = "WholeSale dengan No : " + txtNoSO.Text + " Berahsil di Update";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                ClearTxtHeader();
            }
        }
        protected void InsertSOWholesale(string namaKasir)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            MS_PARAMETER param = new MS_PARAMETER();
            MS_PARAMETER paramnew = new MS_PARAMETER();
            DateTime dt = DateTime.Now;
            int cekpo = Convert.ToInt32(tstDA.CekNoPOInSOWholeSales(" WHERE NO_PO = '" + txtNoPo.Text.Trim() + "'"));

            if (cekpo == 0)
            {
                string tgl = string.Format("{0:yyddMM}", dt);

                int totalQty = 0;

                gvMain.AllowPaging = false;
                param = loginDA.getParam(" where NAME = 'Wholesales'");
                string nourut = "ORD" + tgl + param.VALUE;
                int QtySO = 0;
                foreach (GridViewRow item in gvMain.Rows)
                {
                    QtySO = QtySO + Convert.ToInt32(item.Cells[8].Text);
                }
                SO_WHOLESALES_HEADER SoHeader = new SO_WHOLESALES_HEADER();
                SoHeader.NO_PO = txtNoPo.Text.Trim();
                SoHeader.NO_SO = nourut;
                SoHeader.KODE_CUST = ddlCustSO.SelectedItem.Text;
                SoHeader.KODE = ddlCustSO.SelectedItem.Value;
                SoHeader.TGL_TRANS = Convert.ToDateTime(tbDate.Text);
                SoHeader.SEND_DATE = Convert.ToDateTime(tbDateKirim.Text);
                SoHeader.QTY = QtySO;// Convert.ToInt32(lblSumQty.Text);//totalQty;
                                     //SoHeader.MARGIN = int.Parse(tbMargin.Text);
                SoHeader.MARGIN = Decimal.Parse(tbMargin.Text);
                SoHeader.FRETUR = cbRetur.Checked ? "Yes" : "No";

                tstDA.insertFromStagingWholesaleToSOWholeSales(namaKasir, SoHeader);
                kdbrgDA.deleteStruck(namaKasir);
                kdbrgDA.deleteStagingWholesale(namaKasir);

                paramnew.NAME = "Wholesales";
                paramnew.VALUE = Convert.ToString(Convert.ToInt32(param.VALUE) + 1);
                loginDA.updateValueParam(paramnew);
                bindGrid();
                ClearTxtHeader();

                DivMessage.InnerText = "WholeSale Berhasil Diupload dengan No : " + nourut + ".";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
                gvMain.AllowPaging = true;
            }
            else
            {
                DivMessage.InnerText = "NO PO Sudah pernah digunakan untuk membuat SO!";
                DivMessage.Attributes["class"] = "warning";
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            String myvalue = Request.QueryString["IdSO"];
            idSOHeaderEdit.Text = myvalue;
            //scriptManager.RegisterPostBackControl(this.btnIReturPrintPackingList);
            //scriptManager.RegisterPostBackControl(this.btnIReturPrintDeliveryOrder);
            if (!Page.IsPostBack)
            {
                bindCustSO();
                if (myvalue == "")
                {
                    //firstDelete();
                    bindHeader();
                    deleteTempAcara();
                    //bindGrid();
                    //bindStore();
                    clearGrid();
                    kdbrgDA.deleteStruck(namaKasir);
                    ClearTxtHeader();
                }
                else
                {
                    bindHeaderSOEdit();
                    //BindEditSoDetail();
                    bindGrid();
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            firstDelete();
            bindGrid();
            clearForm();
            clearGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SO_SHOWLESALES_DA tstDA = new SO_SHOWLESALES_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            LOGIN_DA loginDA = new LOGIN_DA();
            MS_PARAMETER param = new MS_PARAMETER();
            MS_PARAMETER paramnew = new MS_PARAMETER();
            DateTime dt = DateTime.Now;
            DateTime dtTrans = Convert.ToDateTime(tbDate.Text);
            string tgl = string.Format("{0:yyMM}", dtTrans);
            string whereLock = "lockHO";
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            string wherecek = "WHERE CREATED_BY = '" + namaKasir + "'";
            string brcdDouble = tstDA.CekDoubleBarcode(wherecek);
            if (cekLock(whereLock, tgl))
            {
                if (brcdDouble != "")
                {
                    DivMessage.InnerText = "Ada Item Yang Ter upload lebih dari 1 baris dengan Barcode : " + brcdDouble + ". Mohon Cek Kembali File yang Di upload!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
                else
                {

                    if (idSOHeaderEdit.Text == "" || (tdNoSO.Visible == false && tdNoSOValue.Visible == false))
                    {
                        InsertSOWholesale(namaKasir);
                    }
                    else
                    {
                        UpdateSOWholesale(namaKasir);
                    }

                }
            }
            else
            {
                DivMessage.InnerText = "Bulan Sudah Di Lock!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
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
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnId.Value = id;
                        tbPUBarcode.Text = gvMain.Rows[rowIndex].Cells[3].Text;
                        tbPUItemCode.Text = gvMain.Rows[rowIndex].Cells[4].Text;
                        tbPUDesc.Text = gvMain.Rows[rowIndex].Cells[5].Text;
                        tbPUSize.Text = gvMain.Rows[rowIndex].Cells[7].Text;
                        tbPUQty.Text = gvMain.Rows[rowIndex].Cells[8].Text;
                        tbPUPrice.Text = gvMain.Rows[rowIndex].Cells[9].Text.Replace(".00", "");
                        ModalChangeQty.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                        Int64 idTemp = new Int64();
                        idTemp = Convert.ToInt64(id);
                        string del = kdbrgDA.deleteTempStruck(idTemp);

                        if (del == "Berhasil!")
                        {
                            DivMessage.InnerText = "Delete Berhasil!";
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;

                            bindGrid();
                        }
                        else
                        {
                            DivMessage.InnerText = "Error : " + del;
                            DivMessage.Attributes["class"] = "error";
                            DivMessage.Visible = true;
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

        protected void btnInput_Click(object sender, EventArgs e)
        {
            inputIntoTempStruck();
        }
        #region "Search Barang"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            tbSearchBy.Text = tbBarcode.Text;
            bindSearch();
        }

        protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearch.PageIndex = e.NewPageIndex;
            bindSearch();
        }

        protected void gvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;

                    if (e.CommandName.ToLower() == "saverow")
                    {
                        string barcode = gvSearch.Rows[rowIndex].Cells[2].Text.ToString();
                        tbBarcode.Text = barcode;
                        inputIntoTempStruck();
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

        protected void btnSearchSearch_Click(object sender, EventArgs e)
        {
            bindSearch();
        }
        #endregion

        protected void btnPUSave_Click(object sender, EventArgs e) //Update Barang Wholesale
        {
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            string price1 = tbPUPrice.Text;
            int qty = int.Parse(tbPUQty.Text);
            int price = int.Parse(tbPUPrice.Text.Replace(",", ""));
            Int64 id = Convert.ToInt64(hdnId.Value);
            kdbrgDA.updateQtyAndPrice(qty, price, id);

            bindGrid();

            DivMessage.InnerText = "Update Berhasil";
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true;
        }

        protected void btnFileUploadIns_Click(object sender, EventArgs e)
        {
            Context.Response.Redirect("WholeSalesNew.aspx");
        }

    }
}