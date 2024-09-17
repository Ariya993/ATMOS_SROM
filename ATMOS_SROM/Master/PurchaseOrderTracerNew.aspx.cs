using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain.CustomObj;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Master
{
    public partial class PurchaseOrderTracerNew : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGridGvMAIN();
            }
        }
        protected void bindGridGvMAIN()
        {
            string where;
            PO_TRACER_DA poTraceDA = new PO_TRACER_DA();
            DataSet ds = new DataSet();
            if (tbSearch.Text != "")
            {
                where = string.Format(" where STATUS_PO = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

                ds = poTraceDA.GetDatavw_POTraceALL(where);
                gvMain.DataSource = ds;
                gvMain.DataBind();
            }
            else
            {
                where = " where STATUS_PO = 1 ";
                ds = poTraceDA.GetDatavw_POTraceALL(where);
                gvMain.DataSource = ds;
                gvMain.DataBind();
            }
        }
        protected void bindGrid()
        {
            PO_TRACER_H POTRACE_H = new PO_TRACER_H();
            List<PO_TRACER_D> listPoTracerD = new List<PO_TRACER_D>();
            List<PO_TRACER_H> listPoTracerH = new List<PO_TRACER_H>();
            PO_TRACER_DA poTraceDA = new PO_TRACER_DA();
            string wherehdr;
            string wheredtl;
            DataSet ds = new DataSet();
            if (tbSearch.Text != "")
            {
                wherehdr = string.Format(" where STATUS_PO = 1 and ID = {2} and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text, hdnIDVI.Value);
                wheredtl = string.Format(" where STATUS_PO = 1 and ID_PO = {2} and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text, hdnIDVI.Value);

                listPoTracerH = poTraceDA.GetListPoTracerHeader(wherehdr);
                //listPoTracerD = poTraceDA.GetPoTracerDetail(where);

                lblPoNo.Text = listPoTracerH.FirstOrDefault().NO_PO;
                lblPoReff.Text = listPoTracerH.FirstOrDefault().PO_REFF;
                lblAddr.Text = listPoTracerH.FirstOrDefault().Addr;
                lblBrand.Text = listPoTracerH.FirstOrDefault().BRAND;
                lblContact.Text = listPoTracerH.FirstOrDefault().CONTACT;
                lblPhnEmail.Text = listPoTracerH.FirstOrDefault().PHONE + " / " + listPoTracerH.FirstOrDefault().EMAIL;
                lblPoDt.Text = Convert.ToString(listPoTracerH.FirstOrDefault().PO_DATE);
                lblSuppl.Text = listPoTracerH.FirstOrDefault().SUPPLIER;

                ds = poTraceDA.GetDataPoTracerDetail(wheredtl);
                GvDetailTracer.DataSource = ds;
                GvDetailTracer.DataBind();
            }
            else
            {
                wherehdr = string.Format(" where STATUS_PO = 1 and ID = {0}", hdnIDVI.Value);
                wheredtl = string.Format(" where STATUS_PO = 1 and ID_PO = {0} ", hdnIDVI.Value);

                listPoTracerH = poTraceDA.GetListPoTracerHeader(wherehdr);
                //listPoTracerD = poTraceDA.GetPoTracerDetail(where);

                lblPoNo.Text = listPoTracerH.FirstOrDefault().NO_PO;
                lblPoReff.Text = listPoTracerH.FirstOrDefault().PO_REFF;
                lblAddr.Text = listPoTracerH.FirstOrDefault().Addr;
                lblBrand.Text = listPoTracerH.FirstOrDefault().BRAND;
                lblContact.Text = listPoTracerH.FirstOrDefault().CONTACT;
                lblPhnEmail.Text = listPoTracerH.FirstOrDefault().PHONE + " / " + listPoTracerH.FirstOrDefault().EMAIL;
                lblPoDt.Text = Convert.ToString(listPoTracerH.FirstOrDefault().PO_DATE);
                lblSuppl.Text = listPoTracerH.FirstOrDefault().SUPPLIER;

                ds = poTraceDA.GetDataPoTracerDetail(wheredtl);
                GvDetailTracer.DataSource = ds;
                GvDetailTracer.DataBind();
            }
            ModalShowDtlTracer.Show();
        }
        protected void bindGridDiff()
        {
            PO_TRACER_H POTRACE_H = new PO_TRACER_H();
            List<PO_TRACER_D> listPoTracerD = new List<PO_TRACER_D>();
            List<PO_TRACER_H> listPoTracerH = new List<PO_TRACER_H>();
            PO_TRACER_DA poTraceDA = new PO_TRACER_DA();
            string where;
            DataSet ds = new DataSet();
            if (tbSearch.Text != "")
            {
                where = string.Format(" where ID_PO = {2} and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text, hdnIDVI.Value);

                ds = poTraceDA.GetDataVw_PoTracerQtyDiff(where);
                GvDtlDiff.DataSource = ds;
                GvDtlDiff.DataBind();
            }
            else
            {
                where = string.Format(" where ID_PO = {0} ", hdnIDVI.Value);

                ds = poTraceDA.GetDataVw_PoTracerQtyDiff(where);
                GvDtlDiff.DataSource = ds;
                GvDtlDiff.DataBind();
            }
            ModalShowDtlTracer.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridGvMAIN();
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID_PO"].ToString();
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnIDVI.Value = id;

                        bindGrid();
                        bindGridDiff();
                        ModalShowDtlTracer.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                //DivMessage.InnerText = "Error : " + ex.Message;
                //DivMessage.Attributes["class"] = "error";
                //DivMessage.Visible = true;
            }
        }


        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGridGvMAIN();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GvDetailTracer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GvDetailTracer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDetailTracer.PageIndex = e.NewPageIndex;
            bindGrid();
            ModalShowDtlTracer.Show();
        }

        protected void bVIClose_Click(object sender, EventArgs e)
        {
            ModalShowDtlTracer.Hide();
        }

        protected void GvDtlDiff_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDtlDiff.PageIndex = e.NewPageIndex;
            bindGridDiff();
        }
    }
}