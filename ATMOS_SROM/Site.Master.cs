using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            string SN = Convert.ToString(Session["UName"]);
            if (SN == "")
            {
                //Response.Redirect("~/Account/Login.aspx"); 
            }
            else
            {
                string[] urlContent = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                string url = urlContent[urlContent.Count() - 1].ToLower();

                string user = Session["UName"].ToString();
                string uLevel = Session["ULevel"].ToString();
                string uStore = Session["UStore"].ToString();

                lbUName.Text = user;
                lbULevel.Text = uLevel;
                lbUStore.Text = uStore;

                LOGIN_DA loginDA = new LOGIN_DA();
                List<MS_MENU> listMenu = new List<MS_MENU>();
                //string where = " where ISNULL(USER_LEVEL, '') = '" + uLevel + "' or ISNULL(USERNAME, '') = '" + user + "' order by ID_MENU";
                string where = " where ISNULL(USER_LEVEL, '') = '" + uLevel + "' or ISNULL(USERNAME, '') = '" + user + "' AND STATUS = '1' order by ID_MENU";
                listMenu = uLevel.ToLower() == "admin" ? loginDA.getMenuAdmin(" WHERE STATUS = 1 order by ID") : loginDA.getMenu(where);
                
                //tambahkan menu member
                foreach (MS_MENU menu in listMenu)
                {
                    MenuItem newMenuItem = new MenuItem(menu.MENU_NAME, "", "", menu.MENU_PATH);
                    NavigationMenu.Items.Add(newMenuItem);
                    //menu.MENU_NAME == "Wholesale (Manual Input)" || menu.MENU_NAME == "Change Password" ||
                    if (menu.MENU_NAME == "About" || menu.MENU_NAME == "Home" || menu.MENU_NAME == "Wholesale (Manual Input)" || menu.MENU_NAME == "Change Password")
                    {
                        MenuItem rmvMenuItem = new MenuItem(menu.MENU_NAME, "", "", menu.MENU_PATH);
                        NavigationMenu.Items.Remove(newMenuItem);
                    }
                }
                int chg = Convert.ToInt32(Session["UChangePass"]);
                if (url.ToLower() == "changepass.aspx" || (Convert.ToInt32(Session["UChangePass"] == null ? "0" : Session["UChangePass"].ToString()) < 4))
                {
                    if (listMenu.Where(itemu => itemu.MENU_PATH.ToString().Split('/').Last().ToLower() == url.ToLower()).ToList().Count == 0 && !(url.ToLower() == "changepass.aspx"))
                    {
                        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                    }
                }
                else
                {
                    Response.Redirect("~/ChangePass.aspx");
                }

                if (!Page.IsPostBack)
                {
                    func.addLog("Show Page: " + url, user);
                }
            }
        }

        protected string getHost()
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ip;
        }

        protected void lbLogOutClick(object sender, EventArgs e)
        {
            MS_USER user = new MS_USER();
            LOGIN_DA loginDA = new LOGIN_DA();

            user.idUser = Convert.ToInt64(Session["UId"].ToString());
            user.online = "offline";
            user.lastLogin = DateTime.Now;
            user.lastIP = getHost();
            loginDA.updateLoginStatus(user);
            func.addLog("LogOut > Success > " + Session["UName"].ToString(), Session["UName"].ToString());

            Session.Clear();
            Response.Redirect("~/Account/Login.aspx");
        }

        protected void lbChangePassClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangePass.aspx");
        }

        protected void ibHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void lbMember_Click(object sender, EventArgs e)
        {
            GLOBALCODE gc = new GLOBALCODE();
            string[] urlContent = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            string encrptUser = gc.Encrypt(Session["UName"].ToString());

            string urlHost = HttpContext.Current.Request.Url.Host;
            string urlPort = urlHost == "localhost" ? "15676" : "9283";
            //string url = "http://localhost:15676/Redirect/Login.aspx?user=" + encrptUser;
            string url = "http://" + urlHost + ":" + urlPort + "/Redirect/Login.aspx?user=" + encrptUser;
            string redirect = "<script>window.open('" + url + "');</script>";
            Response.Write(redirect);
        }
    }
}
