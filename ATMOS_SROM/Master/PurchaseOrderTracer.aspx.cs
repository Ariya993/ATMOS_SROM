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
    public partial class PurchaseOrderTracer : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        protected void bindGrid()
        {
            PO_TRACER_H POTRACE_H = new PO_TRACER_H();
            List<PO_TRACER_D> listPoTracerD = new List<PO_TRACER_D>();
            PO_TRACER_DA poTraceDA = new PO_TRACER_DA();
            if (tbSearch.Text != "")
            {
                string where = string.Format(" where poh.STATUS_PO = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

                //POTRACE_H = poTraceDA.GetPoTracerHeader(where);
                listPoTracerD = poTraceDA.GetPoTracerDetail(where);

                lblPoNo.Text = POTRACE_H.NO_PO;
                lblPoReff.Text = POTRACE_H.PO_REFF;
                lblGrNo.Text = POTRACE_H.NO_GR;
                lblAddr.Text = POTRACE_H.Addr;
                lblBrand.Text = POTRACE_H.BRAND;
                lblContact.Text = POTRACE_H.CONTACT;
                lblPhnEmail.Text = POTRACE_H.PHONE + " / " + POTRACE_H.EMAIL;
                lblPoDt.Text = Convert.ToString(POTRACE_H.PO_DATE);
                lblSuppl.Text = POTRACE_H.SUPPLIER;
                lblTtlQty.Text = Convert.ToString(POTRACE_H.TotalPOQty);
                lblGrStat.Text = POTRACE_H.Status_GR;
                lblShowroom.Text = POTRACE_H.Kode;

                gvMain.DataSource = listPoTracerD;
                gvMain.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PO_TRACER_H POTRACE_H = new PO_TRACER_H();
            List<PO_TRACER_D> listPoTracerD = new List<PO_TRACER_D>();
            PO_TRACER_DA poTraceDA = new PO_TRACER_DA();
            if (tbSearch.Text != "")
            {
                string where = string.Format(" where poh.STATUS_PO = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

                //POTRACE_H = poTraceDA.GetPoTracerHeader(where);
                listPoTracerD = poTraceDA.GetPoTracerDetail(where);

                lblPoNo.Text = POTRACE_H.NO_PO;
                lblPoReff.Text = POTRACE_H.PO_REFF;
                lblGrNo.Text = POTRACE_H.NO_GR;
                lblAddr.Text = POTRACE_H.Addr;
                lblBrand.Text = POTRACE_H.BRAND;
                lblContact.Text = POTRACE_H.CONTACT;
                lblPhnEmail.Text = POTRACE_H.PHONE + " / " + POTRACE_H.EMAIL;
                lblPoDt.Text = Convert.ToString(POTRACE_H.PO_DATE);
                lblSuppl.Text = POTRACE_H.SUPPLIER;
                lblTtlQty.Text = Convert.ToString(POTRACE_H.TotalPOQty);
                lblGrStat.Text = POTRACE_H.Status_GR;
                lblShowroom.Text = POTRACE_H.Kode;

                gvMain.DataSource = listPoTracerD;
                gvMain.DataBind();
                dGrid.Visible = true;
                TracerHeader.Visible = true;
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DivMessage.Visible = false;
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}