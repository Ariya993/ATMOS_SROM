using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Account
{
    public partial class Login : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////Session.Remove("IBayar");
            //MS_USER_DA userDA = new MS_USER_DA();
            //MS_USER user = new MS_USER();

            //user.userName = "a";
            //user.idStore = null;

            //userDA.insertMsUser(user);
        }

        protected void btnLoginClick(object sender, EventArgs e)
        {
            LoginCheck();
        }

        protected string getHost()
        {
            //string ipAddress;
            //if (!String.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"]))
            //    ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            //else
            //    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //return ipAddress;
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ip;
        }
        protected void LoginCheck()
        {
            Boolean pass = false;
            string redirect = "~/Account/Login.aspx";
            try
            {
                UserName.Text = UserName.Text.TrimEnd().TrimStart();

                List<MS_USER> userList = new List<MS_USER>();
                MS_USER user = new MS_USER();
                MS_USER_DA userDA = new MS_USER_DA();
                LOGIN_DA loginDA = new LOGIN_DA();

                string userName = UserName.Text;
                string passWord = func.Encrypt(Password.Text);
                string host = getHost();
                String USerCheck;
                USerCheck = loginDA.usp_checkLoginUser(userName, passWord, host);
                if (USerCheck == "-3")
                {
                    func.addLog("Login > Error > " + UserName.Text + " - Incorrect Password.", UserName.Text);
                    DivMessage.InnerText = "Incorrect Password.";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                else if (USerCheck == "-1")
                {
                    func.addLog("Login > Error > " + UserName.Text + " - User are not registered in this Application or Password Incorrect.", UserName.Text);
                    DivMessage.InnerText = "User are not registered in this Application. For registration please contact MIS.";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                else if (USerCheck == "-2")
                {
                    func.addLog("Login > Error > " + UserName.Text + " - User Still Active in another Session.", UserName.Text);
                    DivMessage.InnerText = "User Still Active in another Session.";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                else
                {
                    func.addLog("Login > Success > " + UserName.Text, UserName.Text);
                    userList = loginDA.getMsUserLogin(userName, passWord);
                    user = userList.First();

                    user.lastLogin = DateTime.Now;
                    user.online = "online";
                    user.lastIP = getHost();
                    loginDA.updateLoginStatus(user);
                    redirect = setLoginFormAuth(user);
                    pass = true;
                }
            }
            catch (Exception ex)
            {
                func.addLog("Login > Error > " + ex.Message, UserName.Text);
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            if (pass)
            {
                Response.Redirect("~/Sales/Sales.aspx");
                //Response.Redirect(redirect);
            }
        }

        #region "Comment"
        //protected void LoginCheck()
        //{
        //    Boolean pass = false;
        //    string redirect = "~/Account/Login.aspx";
        //    try
        //    {
        //        UserName.Text = UserName.Text.TrimEnd().TrimStart();

        //        List<MS_USER> userList = new List<MS_USER>();
        //        MS_USER user = new MS_USER();
        //        MS_USER_DA userDA = new MS_USER_DA();
        //        LOGIN_DA loginDA = new LOGIN_DA();

        //        string userName = UserName.Text;
        //        string passWord = func.Encrypt(Password.Text);

        //        userList = loginDA.getMsUserLogin(userName, passWord);

        //        if (userList.Count > 0)
        //        {
        //            user = userList.First();

        //            if (user.status == false)
        //            {
        //                func.addLog("Login > Error > " + UserName.Text + " - User Inactive. Please contact Admin Head Office to activate this user.", UserName.Text);
        //                DivMessage.InnerText = "User Inactive. Please contact Admin Head Office to activate this user.";
        //                DivMessage.Attributes["class"] = "error";
        //                DivMessage.Visible = true;
        //            }
        //            else if (user.online.ToLower() == "online")
        //            {
        //                string host = getHost();
        //                if (user.lastIP != host)
        //                {
        //                    if (user.lastLongOff > 10)
        //                    {
        //                        func.addLog("Login > Success > " + UserName.Text, UserName.Text);
        //                        user.lastLogin = DateTime.Now;
        //                        user.online = "online";
        //                        user.lastIP = getHost();
        //                        loginDA.updateLoginStatus(user);
        //                        redirect = setLoginFormAuth(user);
        //                        pass = true;
        //                    }
        //                    else
        //                    {
        //                        func.addLog("Login > Error > " + UserName.Text + " - User Still Active in another Session.", UserName.Text);
        //                        DivMessage.InnerText = "User Still Active in another Session.";
        //                        DivMessage.Attributes["class"] = "error";
        //                        DivMessage.Visible = true;
        //                    }
        //                }
        //                else
        //                {
        //                    func.addLog("Login > Success > " + UserName.Text, UserName.Text);
        //                    user.lastLogin = DateTime.Now;
        //                    user.online = "online";
        //                    user.lastIP = getHost();
        //                    loginDA.updateLoginStatus(user);
        //                    redirect = setLoginFormAuth(user);
        //                    pass = true;
        //                }
        //            }
        //            else
        //            {
        //                func.addLog("Login > Success > " + UserName.Text, UserName.Text);
        //                user.lastLogin = DateTime.Now;
        //                user.online = "online";
        //                user.lastIP = getHost();
        //                loginDA.updateLoginStatus(user);
        //                redirect = setLoginFormAuth(user);
        //                pass = true;
        //            }
        //        }
        //        else
        //        {
        //            func.addLog("Login > Error > " + UserName.Text + " - User are not registered in this Application or Password Incorrect.", UserName.Text);
        //            DivMessage.InnerText = "User are not registered in this Application. For registration please contact MIS.";
        //            DivMessage.Attributes["class"] = "error";
        //            DivMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        func.addLog("Login > Error > " + ex.Message, UserName.Text);
        //        DivMessage.InnerText = ex.Message;
        //        DivMessage.Attributes["class"] = "error";
        //        DivMessage.Visible = true;
        //    }
        //    if (pass)
        //    {
        //        Response.Redirect(redirect);
        //    }
        //}
        #endregion
        protected string setLoginFormAuth(MS_USER userSession)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            Session["UId"] = userSession.idUser.ToString();
            Session["UName"] = userSession.userName.ToString();
            Session["URealname"] = userSession.realName.ToString();
            Session["ULevel"] = userSession.userLevel.ToString();
            Session["UEmail"] = userSession.email.ToString();
            Session["UStore"] = userSession.store.ToString();
            Session["ULogin"] = userSession.lastLogin.ToString();
            Session["UKode"] = userSession.kodeCust.ToString();
            Session["UIdStore"] = userSession.idStore.ToString();
            Session["UStoreShow"] = userSession.STORE_SHOWROOM.ToString();
            Session["UBrand"] = userSession.BRAND.ToString();
            Session["UStatusShow"] = userSession.STATUS_SHOWROOM.ToString();
            Session["UVersion"] = loginDA.getParam(" where NAME = 'version'").VALUE;
            Session["UChangePass"] = userSession.timeLastChangePass.ToString();

            //List<MsMenu> menu = new List<MsMenu>();
            //menu = new LoginDA().getMsMenuByQry("where uLevelName like '" + userSession.uLevelName.ToString() + "'");

            //string redirect = userSession.timeLastChangePass > 3 ? "~/ChangePass.aspx" : "~/Default.aspx";//menu.First().url.ToString();
            string redirect = userSession.timeLastChangePass > 1 ? "~/ChangePass.aspx" : "~/Default.aspx";//menu.First().url.ToString();
            return redirect;
        }
    }
}
