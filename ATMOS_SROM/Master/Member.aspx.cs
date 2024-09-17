using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;

namespace ATMOS_SROM.Master
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

            string where = String.Format(" where {0} like '%{1}%' and status_member in ('active','wait')", ddlSearch.SelectedValue, tbSearch.Text);

            //kdbrgList = kdbrgDA.getMsKdbrg(where);
            memberList = memberDA.getMember(where);
            gvMain.DataSource = memberList;
            gvMain.DataBind();

            DivMessage.Visible = false;

            btnMember.Visible = sLevel.ToLower() == "sales" ? false : true;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

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

        protected void btnMemberClick(object sender, EventArgs e)
        {
            tbCFirstName.Text = "";
            tbCLastName.Text = "";
            tbCPhone1.Text = "62";
            tbCPhone.Text = "";
            tbCEmail.Text = "";
            tbCAlamat.Text = "";
            tbCBrand.Text = "";
            divCMessage.Visible = false;

            ModalCreateMember.Show();
        }

        protected void btnCSaveClick(object sender, EventArgs e)
        {
            try
            {
                string firstName = tbCFirstName.Text;
                string lastName = tbCLastName.Text;
                string email = tbCEmail.Text;
                string phone1 = tbCPhone1.Text;
                string phone = tbCPhone.Text;
                string alamat = tbCAlamat.Text;
                string noBon = tbCNoBon.Text;
                string createBY = Session["UName"].ToString();
                string uKode = Session["UKode"].ToString();
                
                List<MS_MEMBER> listMember = new List<MS_MEMBER>();
                MS_MEMBER_DA memberDA = new MS_MEMBER_DA();
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                List<SH_BAYAR> listBayar = new List<SH_BAYAR>();

                listMember = memberDA.getMember(string.Format(" where PHONE = '{0}' or EMAIL = '{1}' ", phone, email));
                listBayar = bayarDA.getSHBayar(string.Format(" where NO_BON = '{0}' and KODE = '{1}'", noBon, uKode));
                bool valid = listBayar.Count > 0 ? listBayar.First().NET_BAYAR > 4999999 : false;
                if (listMember.Count == 0)
                {
                    if (valid)
                    {
                        MS_MEMBER member = new MS_MEMBER();
                        member.FIRST_NAME = firstName;
                        member.LAST_NAME = lastName;
                        member.PHONE = phone1 + phone;
                        member.EMAIL = email;
                        member.ALAMAT = tbCAlamat.Text;
                        member.BRAND = "";
                        //member.STATUS_MEMBER = "active";
                        member.STATUS_MEMBER = "wait";
                        member.CREATED_BY = createBY;
                        string newID = memberDA.insertMember(member);

                        if (!(newID.ToLower().Contains("error")))
                        {
                            bindGrid();

                            DivMessage.InnerText = "Success Create New Member!";
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                        else
                        {
                            DivMessage.InnerText = "ERROR : " + newID;
                            DivMessage.Attributes["class"] = "error";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        divCMessage.InnerText = "No Bon tidak Valid, Cek kembali NO bon yang di input!";
                        divCMessage.Attributes["class"] = "warning";
                        divCMessage.Visible = true;

                        ModalCreateMember.Show();
                    }
                }
                else
                {
                    divCMessage.InnerText = "Member Telah terdaftar, silakan menggunakan telepon atau email yang berbeda!";
                    divCMessage.Attributes["class"] = "warning";
                    divCMessage.Visible = true;

                    ModalCreateMember.Show();
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }
    }
}