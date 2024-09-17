using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ATMOS_SROM.Model;
using ATMOS_SROM.Domain;
using System.Threading;

namespace ATMOS_SROM
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //try
                //{
                //    string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                //    SqlConnection Connection = new SqlConnection(conString);
                //    string query = String.Format("Select * from MsUser where userName = 'william'");
                //    Connection.Open();
                //    using (SqlCommand command = new SqlCommand(query, Connection))
                //    {
                //        command.CommandType = CommandType.Text;
                //        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                //        reader.Read();
                //        reader.Close();
                //    }
                //    string script = "<script type=\"text/javascript\">alert('Connect Berhasil!!!');</script>";
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                //}
                //catch (Exception ex)
                //{
                //    string script = "<script type=\"text/javascript\">alert('"+ex.Message+"');</script>";
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                //}
            }
        }

        protected void btnDoubleClick(object sender, EventArgs e)
        {
            try
            {
                MS_USER_DA userDA = new MS_USER_DA();
                MS_USER user = new MS_USER();

                user.userName = "testingDouble";
                user.idStore = 1;
                userDA.insertMsUser(user);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
