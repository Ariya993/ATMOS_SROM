using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class ShowRoomEdit : System.Web.UI.Page
    {
        private class brandStatus
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        private class brandname
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        private class StatusShowroom
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        private class StatusOpenClosed
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        private class SuperBrand
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                createDataTable();
                if (dShowroom.Visible == true)
                {
                    BindDDLBrand();
                    btndShowroomShow.Enabled = false;
                    lblJudul.Text = "MASTER SHOWROOM";
                }
                BindGridShowroom();
            }
        }
        #region "Method"
        private void BindDDLBrand()
        {
            BRAND_DA BrandDA = new BRAND_DA();
            //List<BRAND> listBrand = new List<BRAND>();
            #region "ATMOS"
            List<brandname> listbrandname = new List<brandname>()  {
                new brandname() { Nama = "ATMOS", Kode = "60" }
            };
            List<SuperBrand> listSuperBrand = new List<SuperBrand>()  {
                new SuperBrand() { Nama = "ATMOS", Kode = "ATMOS" }
            };
            #endregion

            //listBrand = BrandDA.getBRAND(" WHERE KD_BRAND_SHR IS NOT NULL OR KD_BRAND_SHR != '' ");
            ddlBrandShr.DataSource = listbrandname;
            ddlBrandShr.DataBind();
            //ddlBrandJual.DataSource = listSuperBrand;
            //ddlBrandJual.DataBind();
            ddlSuperBrand.DataSource = listSuperBrand;
            ddlSuperBrand.DataBind();
            List<brandStatus> listStatus = new List<brandStatus>()  {  
                new brandStatus() { Nama = "STORE", Kode = "01" },
                new brandStatus() { Nama = "COUNTER", Kode = "02" },
                new brandStatus() { Nama = "BAZAAR", Kode = "03" }
                                         };
            ddlStatusBrandShr.DataSource = listStatus;
            ddlStatusBrandShr.DataBind();

            List<StatusShowroom> listStatusShr = new List<StatusShowroom>()  {  
                new StatusShowroom() { Nama = "FSS", Kode = "FSS" },
                new StatusShowroom() { Nama = "SIS", Kode = "SIS" }
                      };
            ddlstatusShowroom.DataSource = listStatusShr;
            ddlstatusShowroom.DataBind();

            List<StatusOpenClosed> listStatusOpenClosed = new List<StatusOpenClosed>()  {  
                new StatusOpenClosed() { Nama = "OPEN", Kode = "OPEN" },
                new StatusOpenClosed() { Nama = "CLOSE", Kode = "CLOSE" }
                      };
            ddlStatOpenClose.DataSource = listStatusOpenClosed;
            ddlStatOpenClose.DataBind();
            ddlStatOpenCloseEdit.DataSource = listStatusOpenClosed;
            ddlStatOpenCloseEdit.DataBind();
         //   Div1.Visible = true;

        }
        private DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FBRAND");
            dt.Columns.Add("FKD_BRN");
            return dt;
        }
        protected string checkNoUrut()
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> Param = new List<MS_PARAMETER>();
            string nilai = "";
            string brand = ddlBrandShr.SelectedValue;
            string status = ddlStatusBrandShr.SelectedValue;
            //if (ddlStatusBrandShr.SelectedItem.Text.ToLower() == "counter")
            //{
            //    ddlStatusShr.SelectedIndex = ddlStatusShr.Items.IndexOf(ddlStatusShr.Items.FindByText("SIS"));
            //}
            //else
            //{
            //    ddlStatusShr.SelectedIndex = ddlStatusShr.Items.IndexOf(ddlStatusShr.Items.FindByText("FSS")); 
            //}
            //string where = string.Format(" where NAME = '{0}'", brand + status);
            string where = string.Format(" where NAME = '{0}'", brand);

            Param = loginDA.getListParam(where);
            if (Param.Count > 0)
            {
                nilai = Param.First().VALUE.ToString().PadLeft(4, '0');
            }
            else
            {
                nilai = "";
            }
            return nilai;
        }
        private void BindGridShowroom()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            string where = "WHERE STATUS_SHOWROOM !='SUP'";
            
            if (tbSearchShowRoom.Text != null)
            {
                where = where + string.Format(" and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearchShowRoom.Text);
            }
            where = where + "  order by ID Desc";
            listStore = showRoomDA.getShowRoom(where);
            gvShowroom.DataSource = listStore;
            gvShowroom.DataBind();
            dGrid.Visible = true;
        }
        private void BindGridBrand()
        {
            BRAND_DA BrandDA = new BRAND_DA();
            List<BRAND> listBrand = new List<BRAND>();

            String Where = "";
            if (txtSearchBrnd.Text != "")
            {
                Where = string.Format(" where {0} like '%{1}%'", DropDownList2.SelectedValue, txtSearchBrnd.Text);
            }
            listBrand = BrandDA.getBRANDJual(Where);
            GVBrand.DataSource = listBrand;
            GVBrand.DataBind();
            Div1.Visible = true;

        }
        private void BindGridBrandJualSHR()
        {
            BRAND_DA BrandDA = new BRAND_DA();
            List<BRAND> listBrand = new List<BRAND>();

            String Where = string.Format(" where {0} like '%{1}%'", DropDownList3.SelectedValue, txtBrandsrch.Text);
            listBrand = BrandDA.getBRANDJual(Where);
            GVBrandJual.DataSource = listBrand;
            GVBrandJual.DataBind();


        }
        private void BindGridBrnd()
        {
            BRAND_DA BrandDA = new BRAND_DA();
            List<BRAND> listBrand = new List<BRAND>();
            String Where = "";

            if (txtSearchBrnd.Text != "")
            {
                Where = string.Format(" where {0} like '%{1}%' ORDER BY ID DESC", DropDownList2.SelectedValue, txtSearchBrnd.Text);
            }

            listBrand = BrandDA.getBRAND(Where);
            GvBrnd.DataSource = listBrand;
            GvBrnd.DataBind();
            Div1.Visible = true;

        }
        #endregion
        protected void dShowroomShow_Click(object sender, EventArgs e)
        {
            dShowroom.Visible = true;
            dBrand.Visible = false;
            btndShowroomShow.Enabled = false;
            btndBrandShow.Enabled = true;
            lblJudul.Text = "MASTER SHOWROOM";
        }

        protected void dBrandShow_Click(object sender, EventArgs e)
        {
            BindGridBrnd();
            dShowroom.Visible = false;
            dBrand.Visible = true;
            btndShowroomShow.Enabled = true;
            btndBrandShow.Enabled = false;
            lblJudul.Text = "MASTER BRAND";
            BindGridBrnd();

        }

        protected void btnSearchBrand_Click(object sender, EventArgs e)
        {
            BindGridBrand();
            //gvShowroom.Visible = false;
            GVBrand.Visible = true;
            ModalPopupExtender1.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridShowroom();
            //GVBrand.Visible = false;
            gvShowroom.Visible = true;
        }

        #region "SHOWROOM"
        private void BindGridShowroomDiscEdit(String kdshr)
        {
            BRAND_DA brandDA = new BRAND_DA();
            MS_SHOWROOM shritem = new MS_SHOWROOM();
            List<MS_DISCOUNT_SHR> listdiscSHR = new List<MS_DISCOUNT_SHR>();
            shritem = brandDA.getShowRoomAllInfo(" Where KODE = '" + kdshr + "'");
            libIDSHR.Text = Convert.ToString(shritem.ID);
            lblNamaSHR.Text = shritem.SHOWROOM;
            lblKodeSHR.Text = shritem.KODE;
            lblBrandSHR.Text = shritem.BRAND;

            listdiscSHR = brandDA.getListMsDiscountShr(" Where KODE = '" + kdshr + "'");
            GridView1.DataSource = listdiscSHR;
            GridView1.DataBind();

            ChckDiscVIPMemberEdit.Checked = false;
            ChckDiscKaryawanEdit.Checked = false;
            ChckDiscRelasiEdit.Checked = false;

            foreach (MS_DISCOUNT_SHR ITEM in listdiscSHR)
            {
                if (ITEM.TIPE == "ATMOS VIP MEMBER" && ITEM.STATUS == true)
                {
                    ChckDiscVIPMemberEdit.Checked = true;
                }
                if (ITEM.TIPE == "Relasi" && ITEM.STATUS == true)
                {
                    ChckDiscRelasiEdit.Checked = true;
                }
                if (ITEM.TIPE == "Karyawan" && ITEM.STATUS == true)
                {
                    ChckDiscKaryawanEdit.Checked = true;
                }
            }
        }

        protected void gvShowroom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    //string id = gvShowroom.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName == "SelectRow")
                    {
                        string kdshr = gvShowroom.Rows[rowIndex].Cells[7].Text.ToString();
                        BRAND_DA brandDA = new BRAND_DA();
                        MS_SHOWROOM shritem = new MS_SHOWROOM();
                        List<StatusShowroom> listStatusShr = new List<StatusShowroom>()  {  
                        new StatusShowroom() { Nama = "FSS", Kode = "FSS" },
                        new StatusShowroom() { Nama = "SIS", Kode = "SIS" }
                              };
                        ddlStatusShr.DataSource = listStatusShr;
                        ddlStatusShr.DataBind();

                        shritem = brandDA.getShowRoomAllInfo(" Where KODE = '" + kdshr + "'");
                        string str = shritem.LOGO_IMG;
                        string str2 = str.Substring(str.LastIndexOf('\\') + 1);
                        var pos = str2.IndexOf('.');
                        var subText = str2.Substring(0, pos );
                        //string str3 = 
                        lblIDShowroom.Text = Convert.ToString(shritem.ID);
                        txtshowroom.Text = shritem.SHOWROOM;
                        txtKdShowroom.Text = shritem.KODE;
                        txtstore.Text = shritem.STORE;
                        txtAlamat.Text = shritem.ALAMAT;
                        txtPhone.Text = shritem.PHONE;
                        txtPT.Text = shritem.KODE_PT;
                        txtluas.Text = Convert.ToString(shritem.LUAS);//shritem.LUAS.ToString("N");//Convert.ToString(shritem.LUAS);
                        txtJmSpg.Text = Convert.ToString(shritem.JUM_SPG);
                        txtSalary.Text = Convert.ToString(shritem.SALARY);
                        txtinternet.Text = Convert.ToString(shritem.INTERNET);
                        txtbiayatelp.Text = Convert.ToString(shritem.TELEPON);
                        txtsewa.Text = Convert.ToString(shritem.SEWA);
                        txtsusut.Text = Convert.ToString(shritem.SUSUT);
                        txtbiayalain.Text = Convert.ToString(shritem.BY_LAIN2);
                        txtservice.Text = Convert.ToString(shritem.SERVICE);
                        txtLogoImgUpd.Text = subText;
                        //txtstatus.Text = shritem.STATUS;
                        //txtStatusShr.Text = shritem.STATUS_SHOWROOM;
                        ddlStatusShr.SelectedIndex = ddlStatusShr.Items.IndexOf(ddlStatusShr.Items.FindByText(shritem.STATUS_SHOWROOM));//.Text = shritem.STATUS_SHOWROOM;
                        //ddlStatusShr.SelectedValue = shritem.STATUS_SHOWROOM; 
                        ddlStatOpenCloseEdit.SelectedIndex = ddlStatOpenCloseEdit.Items.IndexOf(ddlStatOpenClose.Items.FindByText(shritem.STATUS));
                        txtStatusAwal.Text = shritem.STATUS_AWAL;
                        txtBrand.Text = shritem.BRAND;
                        txtlistrik.Text = Convert.ToString(shritem.LISTRIK);
                        if (shritem.BRAND_JUAL == null)
                        {
                            txtBrandjual.Text = shritem.BRAND;
                        }
                        else if (shritem.BRAND_JUAL == "")
                        {
                            txtBrandjual.Text = "-";
                        }
                        else if (shritem.BRAND_JUAL == "-")
                        {
                            txtBrandjual.Text = "-";
                        }
                        else
                        {
                            txtBrandjual.Text = shritem.BRAND_JUAL; //brandDA.getBRANDNameAsString("where SUPER_BRAND IN (" + shritem.BRAND_JUAL + ")");
                        }
                        
                        ModalPopupExtender0.Show();
                        
                    }
                    else if (e.CommandName == "EditRow")
                    {
                        string kdshr = gvShowroom.Rows[rowIndex].Cells[7].Text.ToString();
                        lblKdShrEditDisc.Text = kdshr;
                        BindGridShowroomDiscEdit(lblKdShrEditDisc.Text);
                        ModalPopupExtender3.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                //DivMessage.InnerText = "Error : " + ex.Message;
                //DivMessage.Attributes["class"] = "error";
                //DivMessage.Visible = true;
            }
            #region "COMMENT"
            //int rowIndex = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            ////GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            ////int rowIndex = grv.RowIndex;
            ////string id = gvShowroom.DataKeys[rowIndex]["ID"].ToString();
            ////int rowIndex = Convert.ToInt32(e.CommandArgument);
            //if (e.CommandName == "SelectRow")
            //{
            //    lblShowroom.Text = gvShowroom.Rows[rowIndex].Cells[2].Text.ToString();
            //    string store = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();
            //    lblBrand.Text = gvShowroom.Rows[rowIndex].Cells[4].Text.ToString();
            //    lblAlamat.Text = gvShowroom.Rows[rowIndex].Cells[6].Text.ToString();
            //    lblPhone.Text = gvShowroom.Rows[rowIndex].Cells[5].Text.ToString();

            //    gvShowroom.DataSource = null;
            //    gvShowroom.DataBind();
            //}
            #endregion
        }

        protected void gvShowroom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShowroom.PageIndex = e.NewPageIndex;
            BindGridShowroom();
        }

        protected void GVBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    //string id = gvShowroom.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName == "SelectBrand")
                    {
                        if (lblselectedBrand.Text == null || lblselectedBrand.Text == "" )
                        {
                            lblselectedBrand.Text = GVBrand.Rows[rowIndex].Cells[2].Text.ToString();
                           // lblselectedKDBrand.Text = GVBrand.Rows[rowIndex].Cells[3].Text.ToString();
                            dsave.Visible = true;
                        }
                        else
                        {
                            lblselectedBrand.Text = lblselectedBrand.Text + "," + GVBrand.Rows[rowIndex].Cells[2].Text.ToString();
                            //lblselectedKDBrand.Text = lblselectedKDBrand.Text + "," + GVBrand.Rows[rowIndex].Cells[3].Text.ToString();
                        }
                        ModalPopupExtender1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GVBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVBrand.PageIndex = e.NewPageIndex;
            ModalPopupExtender1.Show();
            BindGridBrand();
        }

        protected void GVBrand_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GVBrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnupd_Click(object sender, EventArgs e)
        {
            BRAND_DA brandDA = new BRAND_DA();
            MS_SHOWROOM shritemUpd = new MS_SHOWROOM();
            string sal = txtSalary.Text.Replace(",", "");
            string internt = txtinternet.Text.Replace(",", "");
            string listrik = txtlistrik.Text.Replace(",", "");
            string telp = txtbiayatelp.Text.Replace(",", "");
            string sewa = txtsewa.Text.Replace(",", "");
            string susut = txtsusut.Text.Replace(",", "");
            string bylain = txtbiayalain.Text.Replace(",", "");
            string service = txtservice.Text.Replace(",", "");

            shritemUpd.ID = Convert.ToInt64(lblIDShowroom.Text);
            shritemUpd.ALAMAT = txtAlamat.Text;
            shritemUpd.PHONE = txtPhone.Text;
            shritemUpd.KODE_PT = txtPT.Text;
            shritemUpd.LUAS = Convert.ToDecimal(txtluas.Text);
            shritemUpd.JUM_SPG = Convert.ToInt32(txtJmSpg.Text);
            shritemUpd.SALARY = Convert.ToDecimal(txtSalary.Text); //Convert.ToDecimal(sal.Substring(0, sal.Length - 3));
            shritemUpd.INTERNET = Convert.ToDecimal(txtinternet.Text); //Convert.ToDecimal(internt.Substring(0, internt.Length - 3));
            shritemUpd.TELEPON = Convert.ToDecimal(txtbiayatelp.Text); //Convert.ToDecimal(telp.Substring(0, telp.Length - 3));
            shritemUpd.SEWA = Convert.ToDecimal(txtsewa.Text); //Convert.ToDecimal(sewa.Substring(0, sewa.Length - 3));
            shritemUpd.SUSUT = Convert.ToDecimal(txtsusut.Text); // Convert.ToDecimal(susut.Substring(0, susut.Length - 3));
            shritemUpd.BY_LAIN2 = Convert.ToDecimal(txtbiayalain.Text); // Convert.ToDecimal(bylain.Substring(0, bylain.Length - 3));
            shritemUpd.SERVICE = Convert.ToDecimal(txtservice.Text); // Convert.ToDecimal(service.Substring(0, service.Length - 3));
            shritemUpd.LISTRIK = Convert.ToDecimal(txtlistrik.Text); // Convert.ToDecimal(listrik.Substring(0, listrik.Length - 3));
            shritemUpd.STATUS = ddlStatOpenCloseEdit.SelectedItem.Value;//txtstatus.Text;
            shritemUpd.STATUS_SHOWROOM = ddlStatusShr.SelectedItem.Value;//txtStatusShr.Text;
            shritemUpd.BRAND = txtBrand.Text;
            shritemUpd.LOGO_IMG = "..\\Image\\" + txtLogoImgUpd.Text.Trim() + ".png";

            //if (lblbrandjualkd.Text != "" || lblbrandjualkd.Text != null)
            //{
            //    shritemUpd.BRAND_JUAL = lblbrandjualkd.Text;
            //}
            //else
            //{
            //    shritemUpd.BRAND_JUAL = null;
            //}
            shritemUpd.BRAND_JUAL = txtBrandjual.Text;
            brandDA.updateShowroom(shritemUpd);
            BindGridShowroom();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblselectedBrand.Text = "";
            lblselectedKDBrand.Text = "";
            ModalPopupExtender0.Show();
        }

        protected void btnCancelclose_Click(object sender, EventArgs e)
        {
            ModalPopupExtender0.Hide();
        }

        protected void btnSrchBrand_Click1(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
            BindGridBrand();
            //lblselectedKDBrand.Visible = true;
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            ModalPopupExtender0.Show();
        }

        protected void btnsaveselected_Click(object sender, EventArgs e)
        {
            txtBrandjual.Text = lblselectedBrand.Text;
            //lblbrandjualkd.Text = lblselectedKDBrand.Text;
            //lblbrandjualkd.Visible = true;
            ModalPopupExtender1.Hide();
            ModalPopupExtender0.Show();
        }
       
        protected void btnAddShowroom_Click(object sender, EventArgs e)
        {
            PopUpAddSHR.Show();
            BindDDLBrand();
            ClearAddShowroomPopUp();
        }

        protected void btnSetDiscEPC_Click(object sender, EventArgs e)
        {
            BRAND_DA _da = new BRAND_DA();
            PopUpSetDiscEPC.Show();
            ddlBrandEPC.DataSource = _da.getDefaultBrandByTipe("where TIPE = 'Karyawan'");
            ddlBrandEPC.DataBind();
            ddlTipeDiscEPC.DataSource = _da.getDefaultTypeDiscByBrand(string.Format("where TIPE = 'Karyawan' and BRAND = '{0}'", ddlBrandEPC.SelectedValue));
            ddlTipeDiscEPC.DataBind();
            txDiscount.Text = _da.getDefaultDisc(string.Format("where TIPE = 'Karyawan' and BRAND = '{0}' and TIPE_DISCOUNT = '{1}'", ddlBrandEPC.SelectedValue, ddlTipeDiscEPC.SelectedValue)).ToString();
        }

        protected void btnSaveBrndJualSHR_Click(object sender, EventArgs e)
        {
            TxtBrndJualShr.Text = lblBrndJualSHR.Text;
            //lblKDBrandJualSHR.Text = lblKDBrndJualSHR.Text;
            //lblbrandjualkd.Visible = true;
            ModalPopupExtender2.Hide();
            PopUpAddSHR.Show();
        }

        protected void btnCancelBrndJualSHR_Click(object sender, EventArgs e)
        {
            lblBrndJualSHR.Text = "";
            lblKDBrndJualSHR.Text = "";
            ModalPopupExtender2.Show();
        }

        protected void btnCloseBrndJualSHR_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }

        protected void GVBrandJual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVBrand.PageIndex = e.NewPageIndex;
            ModalPopupExtender2.Show();
            BindGridBrand();
        }
        protected void GVBrandJual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    //string id = gvShowroom.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName == "SelectBrand")
                    {
                        if (lblBrndJualSHR.Text == null || lblBrndJualSHR.Text == "" )
                        {
                            lblBrndJualSHR.Text = GVBrandJual.Rows[rowIndex].Cells[2].Text.ToString();
                            //lblKDBrndJualSHR.Text = GVBrandJual.Rows[rowIndex].Cells[3].Text.ToString();

                        }
                        else
                        {
                            lblBrndJualSHR.Text = lblBrndJualSHR.Text + "," + GVBrandJual.Rows[rowIndex].Cells[2].Text.ToString();
                            //lblKDBrndJualSHR.Text = lblKDBrndJualSHR.Text + "," + GVBrandJual.Rows[rowIndex].Cells[3].Text.ToString();
                        }

                        ModalPopupExtender2.Show();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSrchBrandJual_Click(object sender, EventArgs e)
        {
            BindGridBrandJualSHR();
            ModalPopupExtender2.Show();
        }
        protected void btnbrandjualSHRsearch_Click(object sender, EventArgs e)
        {
            BindGridBrandJualSHR();
            ModalPopupExtender2.Show();
        }

        private void InsertMSDiscountSHR(string tipe, string brand, string res, string kd_shr, string shr)
        {
            BRAND_DA brandDA = new BRAND_DA();
            List<DEFAULT_DISC_EPC> ldde = brandDA.getDefaultDiscEPC(string.Format("where TIPE = '{0}' and BRAND = '{1}'", tipe, brand));

            foreach (DEFAULT_DISC_EPC dde in ldde)
            {
                MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();

                itehdiscshr.ID_SHR = Convert.ToInt64(res);
                itehdiscshr.KODE = kd_shr;
                itehdiscshr.SHOWROOM = shr;
                itehdiscshr.DISCOUNT = dde.DISCOUNT;

                itehdiscshr.TIPE_DISCOUNT = dde.TIPE_DISCOUNT;
                itehdiscshr.TIPE = tipe;
                itehdiscshr.CREATED_BY = Session["UName"].ToString();
                brandDA.InsMsDiscountShr(itehdiscshr);
            }
        }

        protected void btnaddSHR_Click(object sender, EventArgs e)
        {
            BRAND_DA brandDA = new BRAND_DA();
            MS_SHOWROOM shritemAdd = new MS_SHOWROOM();
            List<MS_SHOWROOM> ListSHR = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            string sal = txtSalarySHR.Text.Replace(",", "");
            string internt = txtInternetSHR.Text.Replace(",", "");
            string listrik = txtListrikSHR.Text.Replace(",", "");
            string telp = txtTeleponSHR.Text.Replace(",", "");
            string sewa = txtSewaSHR.Text.Replace(",", "");
            string susut = TxtSusutSHR.Text.Replace(",", "");
            string bylain = txtBiayalainSHR.Text.Replace(",", "");
            string service = txtServiceSHR.Text.Replace(",", "");
            shritemAdd.KODE = txtkdSHR.Text;
            shritemAdd.SHOWROOM = txtShrName.Text;
            shritemAdd.STORE = txtStoreShr.Text;
            shritemAdd.ALAMAT = txtAlamatShr.Text;
            shritemAdd.PHONE = txtPhoneShr.Text;
            shritemAdd.KODE_PT = TxtPTShr.Text;
            shritemAdd.LUAS = TxtLuasSHR.Text == "" ? 0 : Convert.ToDecimal(TxtLuasSHR.Text);//Convert.ToDecimal(TxtLuasSHR.Text);
            shritemAdd.JUM_SPG = txtJmlSpgSHR.Text == "" ? 0 : Convert.ToInt32(txtJmlSpgSHR.Text);
            shritemAdd.SALARY = txtSalarySHR.Text == "" ? 0 : Convert.ToDecimal(sal.Substring(0, sal.Length - 3));//(txtSalarySHR.Text.Replace(",", ""));//Convert.ToDecimal(txtSalarySHR.Text);
            shritemAdd.INTERNET = txtInternetSHR.Text == "" ? 0 : Convert.ToDecimal(internt.Substring(0, internt.Length - 3));//Convert.ToDecimal(txtInternetSHR.Text);
            shritemAdd.LISTRIK = txtListrikSHR.Text == "" ? 0 : Convert.ToDecimal(listrik.Substring(0, listrik.Length - 3));//Convert.ToDecimal(txtListrikSHR.Text);
            shritemAdd.TELEPON = txtTeleponSHR.Text == "" ? 0 : Convert.ToDecimal(telp.Substring(0, telp.Length - 3));//Convert.ToDecimal(txtTeleponSHR.Text);
            shritemAdd.SEWA = txtSewaSHR.Text == "" ? 0 : Convert.ToDecimal(sewa.Substring(0, sewa.Length - 3));//Convert.ToDecimal(txtSewaSHR.Text);
            shritemAdd.SUSUT = TxtSusutSHR.Text == "" ? 0 : Convert.ToDecimal(susut.Substring(0, susut.Length - 3));//Convert.ToDecimal(TxtSusutSHR.Text);
            shritemAdd.BY_LAIN2 = txtBiayalainSHR.Text == "" ? 0 : Convert.ToDecimal(bylain.Substring(0, bylain.Length - 3));//Convert.ToDecimal(txtBiayalainSHR.Text);
            shritemAdd.SERVICE = txtServiceSHR.Text == "" ? 0 : Convert.ToDecimal(service.Substring(0, service.Length - 3));//Convert.ToDecimal(txtServiceSHR.Text);
            shritemAdd.STATUS = ddlStatOpenClose.SelectedItem.Value;//txtStatSHR.Text;
            shritemAdd.STATUS_SHOWROOM = ddlstatusShowroom.SelectedItem.Value;//txtStatusShowroomSHR.Text;
            shritemAdd.STATUS_AWAL = txtStatusAwalSHR.Text;
            shritemAdd.BRAND = ddlBrandShr.SelectedItem.Text;
            shritemAdd.LOGO_IMG = "..\\Image\\" + TxtLogoImg.Text.Trim() + ".png";
            //if (lblKDBrandJualSHR.Text != "" || lblKDBrandJualSHR.Text != null)
            //{
            //    shritemAdd.BRAND_JUAL = lblKDBrandJualSHR.Text;
            //}
            //else
            //{
            //    shritemAdd.BRAND_JUAL = null;
            //}
            shritemAdd.BRAND_JUAL = TxtBrndJualShr.Text;
            ListSHR = showRoomDA.getShowRoom(" WHERE KODE = '" + txtkdSHR.Text + "'");
            if (shritemAdd.STATUS_SHOWROOM.ToLower() != "sis" && ddlStatusBrandShr.SelectedItem.Text.ToLower() == "counter")
            {
                dAddShrMsg.InnerText = "Counter Harus Menggunakan Status SIS";
                dAddShrMsg.Attributes["class"] = "warning";
                dAddShrMsg.Visible = true;
                PopUpAddSHR.Show();
            }
            else if (shritemAdd.STATUS_SHOWROOM.ToLower() != "fss" && (ddlStatusBrandShr.SelectedItem.Text.ToLower() == "store" || ddlStatusBrandShr.SelectedItem.Text.ToLower() == "bazaar"))
            {
                dAddShrMsg.InnerText = "Showroom atau bazaar Harus Menggunakan Status FSS";
                dAddShrMsg.Attributes["class"] = "warning";
                dAddShrMsg.Visible = true;
                PopUpAddSHR.Show();
            }
            else
            {
                if (ListSHR.Count() > 0)
                {
                    dAddShrMsg.InnerText = "Showroom Dengan Kode : " + txtkdSHR.Text + " Sudah Ada";
                    dAddShrMsg.Attributes["class"] = "warning";
                    dAddShrMsg.Visible = true;
                    PopUpAddSHR.Show();
                }
                else
                {
                    string res = brandDA.AddShowroom(shritemAdd);

                    if (res.Contains("ERROR"))
                    {
                        dAddShrMsg.InnerText = "Cek Kembali Data yang di masukan !";
                        dAddShrMsg.Attributes["class"] = "error";
                        dAddShrMsg.Visible = true;
                        PopUpAddSHR.Show();
                    }
                    else
                    {
                        if (chkDiscKaryawan.Checked == true)
                        {
                            #region hardcode_mode
                            /*
                            MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                            MS_DISCOUNT_SHR itehdiscshrDKB = new MS_DISCOUNT_SHR();
                            MS_DISCOUNT_SHR itehdiscshrMRA = new MS_DISCOUNT_SHR();
                            MS_DISCOUNT_SHR itehdiscshrATM = new MS_DISCOUNT_SHR();
                            MS_DISCOUNT_SHR itehdiscshrWIP = new MS_DISCOUNT_SHR();
                            //DISC ATM
                            itehdiscshrATM.ID_SHR = Convert.ToInt64(res);
                            itehdiscshrATM.KODE = txtkdSHR.Text;
                            itehdiscshrATM.SHOWROOM = txtShrName.Text;
                            if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                            {
                                itehdiscshrATM.DISCOUNT = 25;
                            }
                            itehdiscshrATM.TIPE = "Karyawan";
                            itehdiscshrATM.TIPE_DISCOUNT = "ATM";
                            itehdiscshrATM.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshrATM);

                            //DISC SOS
                            itehdiscshr.ID_SHR = Convert.ToInt64(res);
                            itehdiscshr.KODE = txtkdSHR.Text;
                            itehdiscshr.SHOWROOM = txtShrName.Text;
                            if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                            {
                                itehdiscshr.DISCOUNT = 25;
                            }
                            itehdiscshr.TIPE = "Karyawan";
                            itehdiscshr.TIPE_DISCOUNT = "SOS";
                            itehdiscshr.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshr);

                            //DISC DKB
                            itehdiscshrDKB.ID_SHR = Convert.ToInt64(res);
                            itehdiscshrDKB.KODE = txtkdSHR.Text;
                            itehdiscshrDKB.SHOWROOM = txtShrName.Text;
                            if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                            {
                                itehdiscshrDKB.DISCOUNT = 25;
                            }
                            itehdiscshrDKB.TIPE = "Karyawan";
                            itehdiscshrDKB.TIPE_DISCOUNT = "DKB";
                            itehdiscshrDKB.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshrDKB);

                            //DISC MRA
                            itehdiscshrMRA.ID_SHR = Convert.ToInt64(res);
                            itehdiscshrMRA.KODE = txtkdSHR.Text;
                            itehdiscshrMRA.SHOWROOM = txtShrName.Text;
                            if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                            {
                                itehdiscshrMRA.DISCOUNT = 25;
                            }
                            itehdiscshrMRA.TIPE = "Karyawan";
                            itehdiscshrMRA.TIPE_DISCOUNT = "MRA";
                            itehdiscshrMRA.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshrMRA);

                            //DISC WIP
                            itehdiscshrWIP.ID_SHR = Convert.ToInt64(res);
                            itehdiscshrWIP.KODE = txtkdSHR.Text;
                            itehdiscshrWIP.SHOWROOM = txtShrName.Text;
                            if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                            {
                                itehdiscshrWIP.DISCOUNT = 30;
                            }
                            itehdiscshrWIP.TIPE = "Karyawan";
                            itehdiscshrWIP.TIPE_DISCOUNT = "WIP";
                            itehdiscshrWIP.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshrWIP);
                            */
                            #endregion

                            InsertMSDiscountSHR("Karyawan", ddlBrandShr.SelectedItem.Text, res, txtkdSHR.Text, txtShrName.Text);
                        }
                        if (chkDiscRelasi.Checked == true)
                        {
                            #region hardcode_mode
                            /*
                            MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                            itehdiscshr.ID_SHR = Convert.ToInt64(res);
                            itehdiscshr.KODE = txtkdSHR.Text;
                            itehdiscshr.SHOWROOM = txtShrName.Text;
                            itehdiscshr.DISCOUNT = 15;
                            itehdiscshr.TIPE_DISCOUNT = "SOS";
                            itehdiscshr.TIPE = "Relasi";
                            itehdiscshr.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshr);

                            MS_DISCOUNT_SHR itehdiscshrATM = new MS_DISCOUNT_SHR();
                            itehdiscshrATM.ID_SHR = Convert.ToInt64(res);
                            itehdiscshrATM.KODE = txtkdSHR.Text;
                            itehdiscshrATM.SHOWROOM = txtShrName.Text;
                            itehdiscshrATM.DISCOUNT = 15;
                            itehdiscshrATM.TIPE_DISCOUNT = "ATM";
                            itehdiscshrATM.TIPE = "Relasi";
                            itehdiscshrATM.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshrATM);

                            //MS_DISCOUNT_SHR itehdiscshrMRA = new MS_DISCOUNT_SHR();
                            //itehdiscshrMRA.ID_SHR = Convert.ToInt64(res);
                            //itehdiscshrMRA.KODE = txtkdSHR.Text;
                            //itehdiscshrMRA.SHOWROOM = txtShrName.Text;
                            //itehdiscshrMRA.DISCOUNT = 20;
                            //itehdiscshrMRA.TIPE_DISCOUNT = "MRA";
                            //itehdiscshrMRA.TIPE = "Relasi";
                            //itehdiscshrMRA.CREATED_BY = Session["UName"].ToString();
                            //brandDA.InsMsDiscountShr(itehdiscshrMRA);
                            */
                            #endregion

                            InsertMSDiscountSHR("Relasi", ddlBrandShr.SelectedItem.Text, res, txtkdSHR.Text, txtShrName.Text);
                        }
                        if (chkDiscVIPMember.Checked == true)
                        {
                            MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                            itehdiscshr.ID_SHR = Convert.ToInt64(res);
                            itehdiscshr.KODE = txtkdSHR.Text;
                            itehdiscshr.SHOWROOM = txtShrName.Text;
                            itehdiscshr.DISCOUNT = 15;
                            itehdiscshr.TIPE = "ATMOS VIP MEMBER";
                            itehdiscshr.TIPE_DISCOUNT = "ATM";
                            itehdiscshr.CREATED_BY = Session["UName"].ToString();
                            brandDA.InsMsDiscountShr(itehdiscshr);
                        }
                        LOGIN_DA loginDA = new LOGIN_DA();
                        MS_PARAMETER param = new MS_PARAMETER();
                        //param.NAME = ddlBrandShr.SelectedValue.ToString() + ddlStatusBrandShr.SelectedValue.ToString();
                        param.NAME = ddlBrandShr.SelectedValue.ToString();
                        param.VALUE = Convert.ToString(Convert.ToInt32(lblNoUrutSHR.Text) + 1);
                        loginDA.updateValueParam(param);
                        dShowroomMSG.InnerText = "Showroom : " + txtkdSHR.Text + " Berhasil Di Tambahkan";
                        dShowroomMSG.Attributes["class"] = "success";
                        dShowroomMSG.Visible = true;
                    }
                }
            }

            BindGridShowroom();
            //brandDA.updateShowroom(shritemAdd);
        }

        protected void btnCloseAddShr_Click(object sender, EventArgs e)
        {
            PopUpAddSHR.Hide();
        }
        protected void txtShrName_TextChanged(object sender, EventArgs e)
        {
            string res_cek = checkNoUrut();
            if (res_cek == "")
            {
                dAddShrMsg.InnerText = "Showroom Baru Belum bisa dibuat untuk Brand Ini silahkan hubungi MIS!";
                dAddShrMsg.Attributes["class"] = "error";
                dAddShrMsg.Visible = true;
                btnaddSHR.Enabled = false;
            }
            else
            {
                //txtkdSHR.Text = ddlBrandShr.SelectedValue.ToString() + ddlStatusBrandShr.SelectedValue.ToString() + res_cek;
                txtkdSHR.Text = ddlBrandShr.SelectedValue.ToString() + res_cek;
                lblNoUrutSHR.Text = res_cek;
                dAddShrMsg.Visible = false;
                btnaddSHR.Enabled = true;
            }
            PopUpAddSHR.Show();
        }
        #endregion

        #region "BRAND"
        protected void GvBrnd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() != "page")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowIndex = grv.RowIndex;
                //string KD_BRN = gvShowroom.DataKeys[rowIndex]["FKD_BRN"].ToString();
                if (e.CommandName == "EditBrand")
                {
                    hfAddEditBrand.Value = "edit";
                    btnAddBrand.Visible = false;
                    btnUpdBrnd.Visible = true;
                    string csg = GvBrnd.Rows[rowIndex].Cells[4].Text.ToString();
                    string sprbrnd = GvBrnd.Rows[rowIndex].Cells[5].Text.ToString();
                    txtFbrand.Text = GvBrnd.Rows[rowIndex].Cells[2].Text.ToString();
                    txtFKdBrand.Text = GvBrnd.Rows[rowIndex].Cells[3].Text.ToString();
                    if (csg == "&nbsp;")
                    {
                        txtConsignment.Text = "";
                    }
                    else
                    {
                        txtConsignment.Text = csg;
                    }
                    //if (sprbrnd == "&nbsp;")
                    //{
                    //    BindDDLBrand.Text = "";
                    //}
                    //else
                    //{
                  
                    ddlSuperBrand.SelectedIndex = ddlSuperBrand.Items.IndexOf(ddlSuperBrand.Items.FindByText(sprbrnd.ToString()));
                    //}
                    
                    PopUpEditBrnd.Show();
                }
            }
        }

        protected void GvBrnd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBrnd.PageIndex = e.NewPageIndex;
            BindGridBrnd();
        }

        protected void btnSrcBrand_Click(object sender, EventArgs e)
        {
            BindGridBrnd();
        }

        protected void btnUpdBrnd_Click(object sender, EventArgs e)
        {
            BRAND_DA brandDA = new BRAND_DA();
            BRAND brnditemUpd = new BRAND();

            brnditemUpd.Consignment = txtConsignment.Text;

            brnditemUpd.SUPER_BRAND = ddlSuperBrand.SelectedItem.Text;
            brnditemUpd.FKD_BRN = txtFKdBrand.Text;
            brandDA.updateBrand(brnditemUpd);
            BindGridBrnd();
        }

        protected void btnClosePopUpEditBrnd_Click(object sender, EventArgs e)
        {
            PopUpEditBrnd.Hide();
        }
        protected int GenerateNextKdBrand()
        {
            int NoUrut = 0;
            BRAND_DA BrandDA = new BRAND_DA();
            BRAND Brnd = new BRAND();
            List<BRAND> listBrand = new List<BRAND>();
            String Where = string.Format(" ORDER BY ID DESC");
            listBrand = BrandDA.getBRAND(Where);
            NoUrut = Convert.ToInt32(listBrand.FirstOrDefault().FKD_BRN) + 1;

            return NoUrut;
        }
        protected void btnAddBrandManual_Click(object sender, EventArgs e)
        {
            int GnrtNo = 0;
            String KDBrand, NoUrutBrand;

            hfAddEditBrand.Value = "add";
            btnUpdBrnd.Visible = false;
            btnAddBrand.Visible = true;
            GnrtNo = GenerateNextKdBrand();
            KDBrand = Convert.ToString(GnrtNo);
            NoUrutBrand = KDBrand.PadLeft(7, '0');
            txtFKdBrand.Text = KDBrand;
            txtFNoBrand.Text = NoUrutBrand;
            txtFbrand.ReadOnly = false;
            txtFbrand.Text = "";
            txtConsignment.Text = "";
            PopUpEditBrnd.Show();
        }
        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            try
            {
                BRAND brnd = new BRAND();
                BRAND_DA brndDA = new BRAND_DA();
                string user = Session["UName"].ToString(); //"SYSTEM";

                brnd.FBRAND = txtFbrand.Text;
                brnd.FKD_BRN = txtFKdBrand.Text;
                brnd.NOMOR = txtFNoBrand.Text;
                brnd.Consignment = txtConsignment.Text;
                brnd.SUPER_BRAND = ddlSuperBrand.SelectedItem.Text; ;
                brndDA.insertBrand(brnd, user);

                BindGridBrnd();
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region "Add Edit Disc Shr"
        protected void btneditDiscSHR_Click(object sender, EventArgs e)
        {
            BRAND_DA brandDA = new BRAND_DA();
            List<MS_DISCOUNT_SHR> listdiscSHRKaryawan = new List<MS_DISCOUNT_SHR>();
            List<MS_DISCOUNT_SHR> listdiscSHRRelasi = new List<MS_DISCOUNT_SHR>();
            List<MS_DISCOUNT_SHR> listdiscSHRVIPMember = new List<MS_DISCOUNT_SHR>();
            listdiscSHRKaryawan = brandDA.getListMsDiscountShr(" Where KODE = '" + lblKodeSHR.Text + "' AND Tipe ='Karyawan'");
            listdiscSHRRelasi = brandDA.getListMsDiscountShr(" Where KODE = '" + lblKodeSHR.Text + "' AND Tipe ='Relasi'");
            listdiscSHRVIPMember = brandDA.getListMsDiscountShr(" Where KODE = '" + lblKodeSHR.Text + "' AND Tipe ='ATMOS VIP MEMBER'");
            #region "DiscKaryawan"
            string SUPER_BRAND = string.Empty;

            if (lblBrandSHR.Text == "707" || lblBrandSHR.Text == "DENIM" || lblBrandSHR.Text == "SDS" || lblBrandSHR.Text == "STANDARD DENIM")
                SUPER_BRAND = "DENIM";
            else
                SUPER_BRAND = lblBrandSHR.Text;

            if (ChckDiscKaryawanEdit.Checked == true)
            {
                if (listdiscSHRKaryawan.Count > 0)
                {
                    if (listdiscSHRKaryawan.First().STATUS == false)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = true;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "Karyawan";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
                else
                {
                    #region hardcode_mode
                    /*
                    MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                    MS_DISCOUNT_SHR itehdiscshrDKB = new MS_DISCOUNT_SHR();
                    MS_DISCOUNT_SHR itehdiscshrMRA = new MS_DISCOUNT_SHR();
                    MS_DISCOUNT_SHR itehdiscshrATM = new MS_DISCOUNT_SHR();
                    MS_DISCOUNT_SHR itehdiscshrWIP = new MS_DISCOUNT_SHR();
                    //DISC SOS
                    itehdiscshr.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshr.KODE = lblKodeSHR.Text; //txtkdSHR.Text;
                    itehdiscshr.SHOWROOM = lblNamaSHR.Text;// txtShrName.Text;
                    if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                    {
                        itehdiscshr.DISCOUNT = 25;
                    }
                    itehdiscshr.TIPE_DISCOUNT = "SOS";
                    itehdiscshr.TIPE = "Karyawan";
                    itehdiscshr.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshr);

                    //DISC ATM
                    itehdiscshrATM.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshrATM.KODE = lblKodeSHR.Text; //txtkdSHR.Text;
                    itehdiscshrATM.SHOWROOM = lblNamaSHR.Text;// txtShrName.Text;
                    if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                    {
                        itehdiscshrATM.DISCOUNT = 25;
                    }
                    itehdiscshrATM.TIPE_DISCOUNT = "ATM";
                    itehdiscshrATM.TIPE = "Karyawan";
                    itehdiscshrATM.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshrATM);

                    //DISC DKB
                    itehdiscshrDKB.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshrDKB.KODE = lblKodeSHR.Text; //txtkdSHR.Text;
                    itehdiscshrDKB.SHOWROOM = lblNamaSHR.Text;//  txtShrName.Text;
                    if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                    {
                        itehdiscshrDKB.DISCOUNT = 25;
                    }
                    itehdiscshrDKB.TIPE = "Karyawan";
                    itehdiscshrDKB.TIPE_DISCOUNT = "DKB";
                    itehdiscshrDKB.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshrDKB);

                    //DISC MRA
                    itehdiscshrMRA.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshrMRA.KODE = lblKodeSHR.Text; //txtkdSHR.Text;
                    itehdiscshrMRA.SHOWROOM = lblNamaSHR.Text;//  txtShrName.Text;
                    if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                    {
                        itehdiscshrMRA.DISCOUNT = 25;
                    }
                    itehdiscshrMRA.TIPE = "Karyawan";
                    itehdiscshrMRA.TIPE_DISCOUNT = "MRA";
                    itehdiscshrMRA.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshrMRA);
                    //DISC WIP
                    itehdiscshrWIP.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshrWIP.KODE = lblKodeSHR.Text; //txtkdSHR.Text;
                    itehdiscshrWIP.SHOWROOM = lblNamaSHR.Text;// txtShrName.Text;
                    if (ddlBrandShr.SelectedItem.Text == "ATMOS")
                    {
                        itehdiscshrMRA.DISCOUNT = 30;
                    }
                    itehdiscshrWIP.TIPE_DISCOUNT = "WIP";
                    itehdiscshrWIP.TIPE = "Karyawan";
                    itehdiscshrWIP.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshrWIP);
                    */
                    #endregion

                    InsertMSDiscountSHR("Karyawan", SUPER_BRAND, libIDSHR.Text, lblKodeSHR.Text, lblNamaSHR.Text);
                }
            }
            else
            {
                if (listdiscSHRKaryawan.Count > 0)
                {
                    if (listdiscSHRKaryawan.First().STATUS == true)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = false;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "Karyawan";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
            }
            #endregion
            #region "DiscRelasi"
            if (ChckDiscRelasiEdit.Checked == true)
            {
                if (listdiscSHRRelasi.Count > 0)
                {
                    if (listdiscSHRRelasi.First().STATUS == false)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = true;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "Relasi";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
                else
                {
                    #region hardcode_mode
                    /*
                    MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                    itehdiscshr.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshr.KODE = lblKodeSHR.Text;
                    itehdiscshr.SHOWROOM = lblNamaSHR.Text;
                    itehdiscshr.DISCOUNT = 15;
                    itehdiscshr.TIPE = "Relasi";
                    itehdiscshr.TIPE_DISCOUNT = "ATM";
                    itehdiscshr.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshr);

                    MS_DISCOUNT_SHR itehdiscshrSOS = new MS_DISCOUNT_SHR();
                    itehdiscshrSOS.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshrSOS.KODE = lblKodeSHR.Text;
                    itehdiscshrSOS.SHOWROOM = lblNamaSHR.Text;
                    itehdiscshrSOS.DISCOUNT = 15;
                    itehdiscshrSOS.TIPE = "Relasi";
                    itehdiscshrSOS.TIPE_DISCOUNT = "SOS";
                    itehdiscshrSOS.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshr);
                    */
                    #endregion

                    InsertMSDiscountSHR("Relasi", SUPER_BRAND, libIDSHR.Text, lblKodeSHR.Text, lblNamaSHR.Text);
                }
            }
            else
            {
                if (listdiscSHRRelasi.Count > 0)
                {
                    if (listdiscSHRRelasi.First().STATUS == true)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = false;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "Relasi";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
            }
            #endregion
            #region "DiscVIPMember"
            if (ChckDiscVIPMemberEdit.Checked == true)
            {
                if (listdiscSHRVIPMember.Count > 0)
                {
                    if (listdiscSHRVIPMember.First().STATUS == false)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = true;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "ATMOS VIP MEMBER";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
                else
                {
                    MS_DISCOUNT_SHR itehdiscshr = new MS_DISCOUNT_SHR();
                    itehdiscshr.ID_SHR = Convert.ToInt64(libIDSHR.Text);
                    itehdiscshr.KODE = lblKodeSHR.Text;
                    itehdiscshr.SHOWROOM = lblNamaSHR.Text;
                    itehdiscshr.DISCOUNT = 15;
                    itehdiscshr.TIPE = "ATMOS VIP MEMBER";
                    itehdiscshr.TIPE_DISCOUNT = "ATM";
                    itehdiscshr.CREATED_BY = Session["UName"].ToString();
                    brandDA.InsMsDiscountShr(itehdiscshr);
                }
            }
            else
            {
                if (listdiscSHRVIPMember.Count > 0)
                {
                    if (listdiscSHRVIPMember.First().STATUS == true)
                    {
                        MS_DISCOUNT_SHR updDiscSHR = new MS_DISCOUNT_SHR();
                        updDiscSHR.STATUS = false;
                        updDiscSHR.UPDATED_BY = Session["UName"].ToString();
                        updDiscSHR.KODE = lblKodeSHR.Text;
                        updDiscSHR.TIPE = "ATMOS VIP MEMBER";
                        brandDA.UpdMsDiscountShr(updDiscSHR);
                    }
                }
            }
            #endregion
            BindGridShowroomDiscEdit(lblKodeSHR.Text);
            ModalPopupExtender3.Show();
        }

        protected void btneditDiscSHRClose_Click(object sender, EventArgs e)
        {
            lblKdShrEditDisc.Text = "";
            ModalPopupExtender3.Hide();
        }
        #endregion

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";
            string filePath = string.Empty;
            BRAND_DA brandDA = new BRAND_DA();

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
                    string user = Session["UName"].ToString(); //"SYSTEM";
                    string res = brandDA.upBrand(FileUploadName, source, FileType, user);
                    if (res.Contains("BERHASIL"))
                    {
                        dBrandMSG.InnerText = res;
                        dBrandMSG.Attributes["class"] = "success";
                        dBrandMSG.Visible = true;
                    }
                    else
                    {
                        dBrandMSG.InnerText = res;
                        dBrandMSG.Attributes["class"] = "error";
                        dBrandMSG.Visible = true;
                    }
                    BindGridBrnd();
                }
            }
        }

        protected void ddlStatusBrandShr_SelectedIndexChanged(object sender, EventArgs e)
        {
            string res_cek = checkNoUrut();
            if (res_cek == "")
            {
                dAddShrMsg.InnerText = "Showroom Baru Belum bisa dibuat untuk Brand Ini silahkan hubungi MIS!";
                dAddShrMsg.Attributes["class"] = "error";
                dAddShrMsg.Visible = true;
                btnaddSHR.Enabled = false;
            }
            else
            {
                //txtkdSHR.Text = ddlBrandShr.SelectedValue.ToString() + ddlStatusBrandShr.SelectedValue.ToString() + res_cek;
                txtkdSHR.Text = ddlBrandShr.SelectedValue.ToString() + res_cek;
                lblNoUrutSHR.Text = res_cek;
                dAddShrMsg.Visible = false;
                btnaddSHR.Enabled = true;
            }
            PopUpAddSHR.Show();
        }

        protected void ClearAddShowroomPopUp()
        {
            txtSalarySHR.Text = "";
            txtInternetSHR.Text = "";
            txtListrikSHR.Text = "";
            txtTeleponSHR.Text = "";
            txtSewaSHR.Text = "";
            TxtSusutSHR.Text = "";
            txtBiayalainSHR.Text = "";
            txtServiceSHR.Text = "";
            txtkdSHR.Text = "";
            txtShrName.Text = "";
            txtStoreShr.Text = "";
            txtAlamatShr.Text = "";
            txtPhoneShr.Text = "";
            TxtPTShr.Text = "";
            TxtLuasSHR.Text = "";
            txtJmlSpgSHR.Text = "";
            ddlStatOpenClose.SelectedIndex = 0;
            ddlstatusShowroom.SelectedIndex = 0;
            txtStatusAwalSHR.Text = "";
            ddlBrandShr.SelectedIndex = 0;
            TxtLogoImg.Text = "";
            TxtBrndJualShr.Text = "";
            lblBrndJualSHR.Text = "";
            chkDiscKaryawan.Checked = false;
            chkDiscVIPMember.Checked = false;
            chkDiscRelasi.Checked = false;
        }

        protected void optDiscKaryawanEPC_OnCheckedChanged(object sender, EventArgs e)
        {
            BRAND_DA _da = new BRAND_DA();
            ddlBrandEPC.DataSource = _da.getDefaultBrandByTipe(string.Format("where TIPE = 'Karyawan'"));
            ddlBrandEPC.DataBind();
            ddlTipeDiscEPC.DataSource = _da.getDefaultTypeDiscByBrand(string.Format(string.Format("where TIPE = 'Karyawan' and BRAND = '{0}'", ddlBrandEPC.SelectedValue)));
            ddlTipeDiscEPC.DataBind();
            txDiscount.Text = _da.getDefaultDisc(string.Format("where TIPE = 'Karyawan' and BRAND = '{0}' and TIPE_DISCOUNT = '{1}'", ddlBrandEPC.SelectedValue, ddlTipeDiscEPC.SelectedValue)).ToString();
            PopUpSetDiscEPC.Show();
        }

        protected void optDiscRelasiEPC_OnCheckedChanged(object sender, EventArgs e)
        {
            BRAND_DA _da = new BRAND_DA();
            ddlBrandEPC.DataSource = _da.getDefaultBrandByTipe(string.Format("where TIPE = 'Relasi'"));
            ddlBrandEPC.DataBind();
            ddlTipeDiscEPC.DataSource = _da.getDefaultTypeDiscByBrand(string.Format(string.Format("where TIPE = 'Relasi' and BRAND = '{0}'", ddlBrandEPC.SelectedValue)));
            ddlTipeDiscEPC.DataBind();
            txDiscount.Text = _da.getDefaultDisc(string.Format("where TIPE = 'Relasi' and BRAND = '{0}' and TIPE_DISCOUNT = '{1}'", ddlBrandEPC.SelectedValue, ddlTipeDiscEPC.SelectedValue)).ToString();
            PopUpSetDiscEPC.Show();
        }

        protected void ddlBrandEPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = string.Empty;
            if (optDiscKaryawanEPC.Checked == true)
                type = "Karyawan";
            else
                type = "Relasi";

            BRAND_DA _da = new BRAND_DA();
            ddlTipeDiscEPC.DataSource = _da.getDefaultTypeDiscByBrand(string.Format(string.Format("where TIPE = '{0}' and BRAND = '{1}'", type, ddlBrandEPC.SelectedValue)));
            ddlTipeDiscEPC.DataBind();
            txDiscount.Text = _da.getDefaultDisc(string.Format("where TIPE = '{0}' and BRAND = '{1}' and TIPE_DISCOUNT = '{2}'", type, ddlBrandEPC.SelectedValue, ddlTipeDiscEPC.SelectedValue)).ToString();
            PopUpSetDiscEPC.Show();
        }

        protected void ddlTipeDiscEPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = string.Empty;
            if (optDiscKaryawanEPC.Checked == true)
                type = "Karyawan";
            else
                type = "Relasi";

            BRAND_DA _da = new BRAND_DA();
            txDiscount.Text = _da.getDefaultDisc(string.Format("where TIPE = '{0}' and BRAND = '{1}' and TIPE_DISCOUNT = '{2}'", type, ddlBrandEPC.SelectedValue, ddlTipeDiscEPC.SelectedValue)).ToString();
            PopUpSetDiscEPC.Show();
        }

        protected void btnUpdateEPC_Click(object sender, EventArgs e)
        {
            dSetDiscMsg.Visible = false;

            if (txDiscount.Text.Length == 0)
            {
                dSetDiscMsg.InnerText = "Diskon harus diisi !";
                dSetDiscMsg.Attributes["class"] = "error";
                dSetDiscMsg.Visible = true;
                PopUpSetDiscEPC.Show();
            }
            else
            {
                if (Convert.ToInt32(txDiscount.Text) <= 0)
                {
                    dSetDiscMsg.InnerText = "Diskon harus lebih besar dari 0 !";
                    dSetDiscMsg.Attributes["class"] = "error";
                    dSetDiscMsg.Visible = true;
                    PopUpSetDiscEPC.Show();
                }
                else
                {
                    string type = string.Empty;
                    if (optDiscKaryawanEPC.Checked == true)
                        type = "Karyawan";
                    else
                        type = "Relasi";

                    BRAND_DA _da = new BRAND_DA();
                    string res = _da.SetDiscEPC(type, ddlTipeDiscEPC.SelectedValue, ddlBrandEPC.SelectedValue, Convert.ToInt32(txDiscount.Text), Session["UName"].ToString());

                    if (res.Contains("ERROR"))
                    {
                        dSetDiscMsg.InnerText = res;
                        dSetDiscMsg.Attributes["class"] = "error";
                        dSetDiscMsg.Visible = true;
                        PopUpSetDiscEPC.Show();
                    }
                    else
                    {
                        res = _da.CreateDiscEPC(type, ddlTipeDiscEPC.SelectedValue, ddlBrandEPC.SelectedValue, Convert.ToInt32(txDiscount.Text), Session["UName"].ToString());

                        if (res.Contains("ERROR"))
                        {
                            dSetDiscMsg.InnerText = res;
                            dSetDiscMsg.Attributes["class"] = "error";
                            dSetDiscMsg.Visible = true;
                            PopUpSetDiscEPC.Show();
                        }
                        else
                        {
                            dSetDiscMsg.InnerText = res;
                            dSetDiscMsg.Attributes["class"] = "success";
                            dSetDiscMsg.Visible = true;
                            PopUpSetDiscEPC.Show();
                        }
                    }
                }
            }
        }

        protected void btnCloseEPC_Click(object sender, EventArgs e)
        {
            PopUpSetDiscEPC.Hide();
        }
    }
}