using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Warehouse
{
    public partial class ViewStock : System.Web.UI.Page
    {
        protected void bindGrid()
        {
            string where = "";
            string whereShr = "";

            if (ddlStoreSrchStore.SelectedItem.Value != "")
            {
                whereShr = " AND KODE = '" + ddlStoreSrchStore.SelectedItem.Value.Trim() + "'";
                where = tbSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%' {2} ORDER BY STOCK DESC", ddlSearch.SelectedValue, tbSearch.Text, whereShr);

            }
            else
            {
                where = tbSearch.Text == "" ? "" : string.Format(" where {0} like '%{1}%' ORDER BY STOCK DESC", ddlSearch.SelectedValue, tbSearch.Text);
            }

            List<MS_STOCK> listStock = new List<MS_STOCK>();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();

            listStock = stockDA.getStock(String.Format(where));

            gvMain.DataSource = listStock;
            gvMain.DataBind();
        }
        protected void bindStore()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            MS_SHOWROOM showRoom = new MS_SHOWROOM();
            showRoom.SHOWROOM = "--All Showroom--";
            showRoom.KODE = "";
            listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN'");
            listStore.Insert(0, showRoom);
            ddlStoreSrchStore.DataSource = listStore;
            ddlStoreSrchStore.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindStore();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text == "" || tbSearch.Text == "&nbsp" || tbSearch.Text == null)
            {
                DivMessage.InnerText = "Mohon Isi Barcode / Item Code / Item Description";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            else
            {
                bindGrid();
                DivMessage.Visible = false;

            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }
    }
}