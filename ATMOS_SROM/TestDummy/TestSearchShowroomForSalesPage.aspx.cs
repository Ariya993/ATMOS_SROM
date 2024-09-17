using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.TestDummy
{
    public partial class TestSearchShowroomForSalesPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void BindGridShowroom()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            if (tbSearchShowRoom.Text != null)
            {
                listStore = showRoomDA.getShowRoom(" where Showroom like '%" + tbSearchShowRoom.Text + "%'");
                gvShowroom.DataSource = listStore;
                gvShowroom.DataBind();
                dGrid.Visible = true;
                gvShowroom.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGridShowroom();
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
                        MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                        //ddlStore.Text = gvShowroom.Rows[rowIndex].Cells[2].Text.ToString();
                        ////string store = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();
                        ////lblBrand.Text = gvShowroom.Rows[rowIndex].Cells[4].Text.ToString();
                        ////lblAlamat.Text = gvShowroom.Rows[rowIndex].Cells[6].Text.ToString();
                        ////lblPhone.Text = gvShowroom.Rows[rowIndex].Cells[5].Text.ToString();
                        //ddlStore.SelectedValue = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();
                        MS_SHOWROOM showRoom = new MS_SHOWROOM();
                        List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                        string kdshowroom = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();

                      
                        listStore = showRoomDA.getShowRoom(" where KODE = '" + kdshowroom + "'");
                        //listStore.Insert(0, showRoom);
                        ddlStore.DataSource = listStore;
                        ddlStore.DataBind();
                        int index = 0;
                        //int.TryParse(hdnIDStore.Value, out index);
                        //ddlStore.SelectedIndex = index;
                        gvShowroom.Visible = false;
                        //dShowRoomData.Visible = true;
                        //BindGridShowroom();
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

        protected void gvShowroom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShowroom.PageIndex = e.NewPageIndex;
            BindGridShowroom();
        }

        protected void gvShowroom_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvShowroom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}