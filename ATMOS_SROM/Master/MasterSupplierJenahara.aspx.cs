using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using ATMOS_SROM.Model.CustomObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class MasterSupplierJenahara : System.Web.UI.Page
    {
        private class vendor
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<vendor> listvendor = new List<vendor>()  {  
                new vendor() { Nama = "Import", Kode = "Import" },
                new vendor() { Nama = "Local", Kode = "Local" }
                      };
                ddlAddVendor.DataSource = listvendor;
                ddlAddVendor.DataBind();

                ddlEditVendor.DataSource = listvendor;
                ddlEditVendor.DataBind();
                BindGridShowroom();
            }
        }
        private void BindGridShowroom()
        {
            MS_SUPPLIER_DA showRoomDA = new MS_SUPPLIER_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            if (tbSearchSuppl.Text != "")
            {
                string where = string.Format(" where STATUS_SHOWROOM ='SUP' AND {0} like '%{1}%' order by ID DESC", ddlSearch.SelectedValue, tbSearchSuppl.Text);

                listStore = showRoomDA.getListSuppl(where);
                gvSupplier.DataSource = listStore;
                gvSupplier.DataBind();
                dGrid.Visible = true;
            }
            else
            {
                string where = string.Format(" where STATUS_SHOWROOM ='SUP' order by ID DESC");

                listStore = showRoomDA.getListSuppl(where);
                gvSupplier.DataSource = listStore;
                gvSupplier.DataBind();
                dGrid.Visible = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridShowroom();
        }

        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            PopUpAddSppl.Show();
        }

        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        MS_SUPPLIER_DA showRoomDA = new MS_SUPPLIER_DA();
                        List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                        listStore = showRoomDA.getListSuppl(" WHERE KODE = '" + gvSupplier.Rows[rowIndex].Cells[5].Text.ToString() + "'");
                        txtEditKode.Text = listStore.FirstOrDefault().KODE;
                        txtEditBrand.Text = listStore.FirstOrDefault().BRAND;
                        txtEditAlamat.Text = listStore.FirstOrDefault().ALAMAT;
                        txtEditPhone.Text = listStore.FirstOrDefault().PHONE;
                        if (listStore.FirstOrDefault().VENDOR != "")
                        {
                            ddlEditVendor.SelectedIndex = ddlEditVendor.Items.IndexOf(ddlEditVendor.Items.FindByText(listStore.FirstOrDefault().VENDOR.ToString()));
                            //ddlEditVendor.SelectedItem.Text = listStore.FirstOrDefault().VENDOR;
                            //ddlEditVendor.SelectedItem.Value = listStore.FirstOrDefault().VENDOR;
                        }
                        else
                        {
                            ddlEditVendor.SelectedIndex = 0;
                        }
                        PopUpEditSppl.Show();
                    }
                }
            }
            catch(Exception ex)
            { }
        }

        protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplier.PageIndex = e.NewPageIndex;
            BindGridShowroom();
        }

        protected void btnAddSuppl_Click(object sender, EventArgs e)
        {
            MS_SHOWROOM shritemAdd = new MS_SHOWROOM();
            List<MS_SHOWROOM> ListSHR = new List<MS_SHOWROOM>();
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            MS_SUPPLIER_DA supplDA = new MS_SUPPLIER_DA();
            shritemAdd.KODE = txtAddKode.Text;
            shritemAdd.BRAND = txtAddBrand.Text;
            shritemAdd.ALAMAT = txtAddAlamat.Text;
            shritemAdd.PHONE = txtAddPhone.Text;
            shritemAdd.VENDOR = ddlAddVendor.SelectedItem.Text;
            ListSHR = showRoomDA.getShowRoom(" WHERE KODE = '" + txtAddKode.Text + "'");
            if (ListSHR.Count() > 0)
            {
                dAddSpplMsg.InnerText = "Supplier Atau Showroom Dengan Kode : " + txtAddKode.Text + " Sudah Ada";
                dAddSpplMsg.Attributes["class"] = "warning";
                dAddSpplMsg.Visible = true;
                PopUpAddSppl.Show();
            }
            else
            {
                supplDA.AddSuppl(shritemAdd);
                dSupplierMSG.InnerText = "Supplier : " + txtAddKode.Text + " Berhasil Di Tambahkan";
                dSupplierMSG.Attributes["class"] = "success";
                dSupplierMSG.Visible = true;
            }
            BindGridShowroom();
        }

        protected void btnEditSuppl_Click(object sender, EventArgs e)
        {
            MS_SHOWROOM shritemAdd = new MS_SHOWROOM();
            MS_SUPPLIER_DA supplDA = new MS_SUPPLIER_DA();
            shritemAdd.KODE = txtEditKode.Text;
            //shritemAdd.BRAND = txtEditBrand.Text;
            shritemAdd.ALAMAT = txtEditAlamat.Text;
            shritemAdd.PHONE = txtEditPhone.Text;
            shritemAdd.VENDOR = ddlEditVendor.SelectedItem.Text;
            supplDA.updateSuppl(shritemAdd);
            BindGridShowroom();
        }

        protected void btnAddCancelSuppl_Click(object sender, EventArgs e)
        {
            PopUpAddSppl.Hide();
        }

        protected void btnEditCancelSuppl_Click(object sender, EventArgs e)
        {
            PopUpEditSppl.Hide();
        }
    }
}