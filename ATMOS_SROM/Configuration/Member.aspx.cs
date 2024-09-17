using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Configuration
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
            }
        }

        protected void bindGrid()
        {
            MS_MEMBER_DA memberDA = new MS_MEMBER_DA();
            List<MS_MEMBER> memberList = new List<MS_MEMBER>();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            string where = String.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            //kdbrgList = kdbrgDA.getMsKdbrg(where);
            memberList = memberDA.getMember(where);
            gvMain.DataSource = memberList;
            gvMain.DataBind();

            DivMessage.Visible = false;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();

                    string firstName = gvMain.Rows[rowIndex].Cells[3].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[3].Text;
                    string lastName = gvMain.Rows[rowIndex].Cells[4].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[4].Text;
                    string phone = gvMain.Rows[rowIndex].Cells[5].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[5].Text;
                    string email = gvMain.Rows[rowIndex].Cells[6].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[6].Text;
                    string alamat = gvMain.Rows[rowIndex].Cells[7].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[7].Text;
                    string status = gvMain.Rows[rowIndex].Cells[9].Text.Contains("nbsp") ? "" : gvMain.Rows[rowIndex].Cells[9].Text;

                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnIdMember.Value = id;
                        tbCFirstName.Text = firstName;
                        tbCLastName.Text = lastName;
                        tbCPhone.Text = phone;
                        tbCEmail.Text = email;
                        tbCAlamat.Text = alamat;
                        tbCStatus.Text = status;
                        divCMessage.Visible = false;

                        ModalCreateMember.Show();
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

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnCSaveClick(object sender, EventArgs e)
        {
            MS_MEMBER_DA memberDA = new MS_MEMBER_DA();
            MS_MEMBER member = new MS_MEMBER();

            member.STATUS_MEMBER = "active";
            member.UPDATED_BY = Session["UName"].ToString();
            member.ID = Convert.ToInt64(hdnIdMember.Value);

            memberDA.updateStatus(member);

            bindGrid();
            DivMessage.InnerText = "Member Berhasil Di Activasi | Nama : " + tbCFirstName.Text + " " + tbCLastName.Text + " | HP : +" + tbCPhone.Text;
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true;
        }

        protected void btnCBlockClick(object sender, EventArgs e)
        {
            MS_MEMBER_DA memberDA = new MS_MEMBER_DA();
            MS_MEMBER member = new MS_MEMBER();

            member.STATUS_MEMBER = "block";
            member.UPDATED_BY = Session["UName"].ToString();
            member.ID = Convert.ToInt64(hdnIdMember.Value);

            memberDA.updateStatus(member);

            bindGrid();
            DivMessage.InnerText = "Member Berhasil Di Block | Nama : " + tbCFirstName.Text + " " + tbCLastName.Text + " | HP : +" + tbCPhone.Text;
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true;
        }
    }
}