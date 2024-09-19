using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;

namespace ATMOS_SROM.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var a ="";
                bindUserName();
            }
        }

        protected void bindUserName()
        {
            string userName = Session["UName"] == null ? "" : Session["UName"].ToString();
            tbUserName.Text = userName;
        }

        protected void btnSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (tbNewPass.Text.Trim() == tbConfirmPass.Text.Trim())
                {
                    tbUserName.Text = tbUserName.Text.TrimEnd().TrimStart();

                    List<MS_USER> userList = new List<MS_USER>();
                    MS_USER user = new MS_USER();
                    MS_USER_DA userDA = new MS_USER_DA();
                    LOGIN_DA loginDA = new LOGIN_DA();

                    string userName = tbUserName.Text;
                    string passWord = func.Encrypt(tbOldPass.Text);

                    userList = loginDA.getMsUserLogin(userName, passWord);
                    if (userList.Count > 0)
                    {
                        user = userList.First();
                        user.password = func.Encrypt(tbNewPass.Text.Trim());
                        user.lastChangePass = DateTime.Now;
                        loginDA.changePass(user);

                        tbOldPass.Text = "";
                        tbNewPass.Text = "";
                        tbConfirmPass.Text = "";
                        Session["UChangePass"] = "0";

                        DivMessage.InnerText = "Perubahaan Password Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = "Password Salah!";
                        DivMessage.Attributes["class"] = "warning";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Password Baru tidak sama!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }
    }
}
