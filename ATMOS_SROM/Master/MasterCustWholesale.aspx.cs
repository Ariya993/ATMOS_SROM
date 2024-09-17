using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Master
{
    public partial class MasterCustWholesale : System.Web.UI.Page
    {
        MS_CUST_WHOLESALE_DA MsCustWholesaleDA = new MS_CUST_WHOLESALE_DA();
        private class BlockStat
        {
            public string Nama { get; set; }
            public string Value { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridCustWholesale();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridCustWholesale();
        }
        protected void BindGridCustWholesale()
        {
            DataSet DsGridCust = new DataSet();
            String Where = "";
            Where = String.Format(" Where {0} like '%{1}%'", ddlSearch.SelectedItem.Value, tbSearch.Text.Trim());
            DsGridCust = MsCustWholesaleDA.GetTf_View_Wholesale_Customer(Where);

            GvCustWholesale.DataSource = DsGridCust;
            GvCustWholesale.DataBind();

        }
        protected void GvCustWholesale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvCustWholesale.PageIndex = e.NewPageIndex;
            BindGridCustWholesale();
        }
        protected void btnAddNewCust_Click(object sender, EventArgs e)
        {
            PopUpAddCst.Show();
            lblJudulPopUp.Text = "Add New Wholesale Customer";
            btnAddCust.Text = "Add New";
            ClearPopUp();
        }
        protected void btnAddCancelCust_Click(object sender, EventArgs e)
        {
            PopUpAddCst.Hide();
            ClearPopUp();
        }
        protected void ClearPopUp()
        {
            lblIDCustEdit.Text = "";
            txtAddKode.Text = "";
            txtNmCust.Text = "";
            txtAddKode.ReadOnly = false;
            txtNmCust.ReadOnly = false;
        }
        protected void btnAddCust_Click(object sender, EventArgs e)
        {
            String Res = "";
            DataSet DSCustExist = new DataSet();
            MS_CUST_WHOLESALE custWholesale = new MS_CUST_WHOLESALE();


            if (lblIDCustEdit.Text == "")
            {
                DSCustExist = MsCustWholesaleDA.GetMS_CUST_WHOLESALE(String.Format(" WHERE KD_PEMBELI = '{0}'", txtAddKode.Text));
                if (DSCustExist.Tables[0].Rows.Count > 0)
                {
                    dAddCustMsg.InnerText = "Customer Wholesale Dengan Kode : " + txtAddKode.Text + " Sudah Ada";
                    dAddCustMsg.Attributes["class"] = "warning";
                    dAddCustMsg.Visible = true;
                    PopUpAddCst.Show();
                }
                else
                {
                    dAddCustMsg.Visible = false;
                    custWholesale.KD_PEMBELI = txtAddKode.Text;
                    custWholesale.NM_PEMBELI = txtNmCust.Text;
                    if (CBBlockStat.Checked == true)
                    {
                        custWholesale.BLOCK = true;
                    }
                    else
                    {
                        custWholesale.BLOCK = false;
                    }
                    custWholesale.CREATED_BY = Session["UName"].ToString();
                    Res = MsCustWholesaleDA.InsMS_CUST_WHOLESALE(custWholesale);
                    if (Res.Contains("ERROR"))
                    {
                        dAddCustMsg.InnerText = "Add New Customer Wholesale Failed!, " + Res;
                        dAddCustMsg.Attributes["class"] = "error";
                        dAddCustMsg.Visible = true;
                        PopUpAddCst.Show();
                    }
                }
            }
            else
            {
                custWholesale.ID = Convert.ToInt64(lblIDCustEdit.Text);
                custWholesale.KD_PEMBELI = txtAddKode.Text;
                custWholesale.NM_PEMBELI = txtNmCust.Text;
                if (CBBlockStat.Checked == true)
                {
                    custWholesale.BLOCK = true;
                }
                else
                {
                    custWholesale.BLOCK = false;
                }
                custWholesale.UPDATED_BY = Session["UName"].ToString();
                Res = MsCustWholesaleDA.UpdMS_CUST_WHOLESALE(custWholesale);
                if (Res.Contains("ERROR"))
                {
                    dAddCustMsg.InnerText = "Update Customer Wholesale Failed!, " + Res;
                    dAddCustMsg.Attributes["class"] = "error";
                    dAddCustMsg.Visible = true;
                    PopUpAddCst.Show();
                }
            }
            BindGridCustWholesale();
        }

        protected void GvCustWholesale_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = GvCustWholesale.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName == "EditRow")
                    {
                        DataSet DsCustWholesale = new DataSet();
                        DsCustWholesale = MsCustWholesaleDA.GetTf_View_Wholesale_Customer(" WHERE ID = '" + id + "'");
                        int JmlSO = Convert.ToInt32(DsCustWholesale.Tables[0].Rows[0]["JML_SO"].ToString());
                        lblIDCustEdit.Text = DsCustWholesale.Tables[0].Rows[0]["ID"].ToString();
                        txtAddKode.Text = DsCustWholesale.Tables[0].Rows[0]["KD_PEMBELI"].ToString();
                        txtNmCust.Text = DsCustWholesale.Tables[0].Rows[0]["NM_PEMBELI"].ToString();
                        Boolean statBlock = Convert.ToBoolean(DsCustWholesale.Tables[0].Rows[0]["BLOCK"].ToString());
                        if (statBlock == true)
                        {
                            CBBlockStat.Checked = true;
                        }
                        //if (ddlBlockStat.Items.FindByValue(DsCustWholesale.Tables[0].Rows[0]["BLOCK"].ToString()) != null)
                        //{
                        //    ddlBlockStat.Items.FindByValue(DsCustWholesale.Tables[0].Rows[0]["BLOCK"].ToString()).Selected = true;
                        //}
                        if (JmlSO > 0)
                        {
                            txtAddKode.ReadOnly = true;
                            txtNmCust.ReadOnly = true;
                        }
                        PopUpAddCst.Show();
                        btnAddCust.Text = "Update";

                    }
                }
            }
            catch (Exception ex)
            { }

        }
    }
}