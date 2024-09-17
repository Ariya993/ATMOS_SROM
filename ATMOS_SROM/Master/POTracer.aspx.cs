using ATMOS_SROM.Domain.CustomObj;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class POTracer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridPoTracerH();
            }
        }
        private void BindGridPoTracerH()
        {
            PO_TRACER_DA POTracerDA = new PO_TRACER_DA();
            List<PO_TRACER_H> listPoTracerH = new List<PO_TRACER_H>();
            string where = string.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);
            if (tbSearch.Text != "")
            {
                listPoTracerH = POTracerDA.GetListPoTracerHeader(where);
                gvMain.DataSource = listPoTracerH;
                gvMain.DataBind();
                //dGrid.Visible = true;
            }
            else
            {
                listPoTracerH = POTracerDA.GetListPoTracerHeader("");
                gvMain.DataSource = listPoTracerH;
                gvMain.DataBind();
            }
        }
        private void BindGridPoTracerD()
        {
            PO_TRACER_DA POTracerDA = new PO_TRACER_DA();
            List<PO_TRACER_D> listPoTracerD = new List<PO_TRACER_D>();
            string where = "";
            if (txtDetailSrch.Text == "")
            {
                where = " where ID = " + lblPOID.Text;
            }
            else
            {
                where = " where ID = " + lblPOID.Text + " AND " + ddlDetailSrch.SelectedItem.Value + " like '%" + txtDetailSrch.Text + "%'";
            }
            listPoTracerD = POTracerDA.GetPoTracerDetail(where);
            gvPoTracerD.DataSource = listPoTracerD;
            gvPoTracerD.DataBind();
            ModalPopupPOTracerD.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridPoTracerH();
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
                    if (e.CommandName == "SelectRow")
                    {
                        txtDetailSrch.Text = "";
                        lblPOID.Text = id;
                        string where = " where ID = " + id;
                        PO_TRACER_DA POTracerDA = new PO_TRACER_DA();
                        List<PO_TRACER_H> listPoTracerH = new List<PO_TRACER_H>();
                        listPoTracerH = POTracerDA.GetListPoTracerHeader(where);
                        lblPoNo.Text = listPoTracerH.FirstOrDefault().NO_PO;
                        lblGrNo.Text = listPoTracerH.FirstOrDefault().NO_GR;
                        lblPoReff.Text = listPoTracerH.FirstOrDefault().PO_REFF;
                        lblPoDt.Text = listPoTracerH.FirstOrDefault().PO_DATE.ToString();
                        lblBrand.Text = listPoTracerH.FirstOrDefault().BRAND;
                        lblSuppl.Text = listPoTracerH.FirstOrDefault().SUPPLIER;
                        lblContact.Text = listPoTracerH.FirstOrDefault().CONTACT;
                        lblTtlQty.Text = listPoTracerH.FirstOrDefault().TotalPOQty.ToString();
                        lblPhnEmail.Text = listPoTracerH.FirstOrDefault().PHONE + " / " + listPoTracerH.FirstOrDefault().EMAIL;
                        lblAddr.Text = listPoTracerH.FirstOrDefault().Addr;
                        lblShowroom.Text = listPoTracerH.FirstOrDefault().Kode;
                        BindGridPoTracerD();
                        ModalPopupPOTracerD.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                lblPOID.Text = ex.Message;
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            BindGridPoTracerH();
        }

        protected void gvPoTracerD_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvPoTracerD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPoTracerD.PageIndex = e.NewPageIndex;
            BindGridPoTracerD();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ModalPopupPOTracerD.Hide();
        }

        protected void btnDetailSrch_Click(object sender, EventArgs e)
        {
            BindGridPoTracerD(); 
        }
    }
}