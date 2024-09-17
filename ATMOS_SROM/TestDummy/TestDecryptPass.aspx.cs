using ATMOS_SROM.Model;
using ATMOS_SROM.Model.CustomObj;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.TestDummy
{
    public partial class TestDecryptPass : System.Web.UI.Page
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || Session["end_t"] == null)
            {
                DateTime start_time = DateTime.Now;
                DateTime end_time = start_time.AddMinutes(1);
                Session["end_t"] = end_time;
                //sendMailSignUp("natasha.angelica2212@gmail.com");
            }
        }
        public static string sendMailSignUp( string to)
        {
            string htmlBody = "<html><body>Dear William <br/><br/><label align=center text=testing middle /></body></html></body></html>";
            MailMessage mail = new MailMessage("admin.delamibrandsreward@delamibrands.com", to);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "mail.delamibrands.com";//smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("admin.delamibrandsreward@delamibrands.com", "Admin1234");//("test.decard@delamibrands.com", "Test1234");//new System.Net.NetworkCredential("testng462", "1401100273");
            //smtp.EnableSsl = true;
            mail.Subject = "Welcome to the list";
            //mail.Body = string.Format(htmlBody);
            mail.IsBodyHtml = true;
            //Attachment attach = new Attachment(pathFile);
            //mail.Attachments.Add(attach);
            //mail.AlternateViews.Add(getEmbeddeImage(pathFile));
            smtp.Send(mail);

            return "Berhasil";
        }
        protected void btndcrypt_Click(object sender, EventArgs e)
        {
            GLOBALCODE globalcode = new GLOBALCODE();
            string getpas = getMsUserPass(txtusername.Text);
            string showpas = globalcode.Decrypt(txtusername.Text);

            //string showpas = globalcode.Encrypt(txtusername.Text);
            lblpass.Text = showpas;
        }

        private String getMsUserPass(string userName)
        {
            String pass = "";
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("Select password " +
                    "from vw_user where userName = @userName and status = 1", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@userName", userName));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        pass = reader.GetString(0);
                        
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pass;
        }

        protected void btnhitpoint_Click(object sender, EventArgs e)
        {
            int pointhit = Convert.ToInt32(txtprice.Text) * 10 / 100;//Convert.ToInt32(txtprice.Text) * 10 / 100;

            Label1.Text = Convert.ToString(pointhit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendMail_DA mailDA = new SendMail_DA();
            mailDA.sendMailPoint(TextBox1.Text);
        }

        //protected void tmr1_Tick(object sender, EventArgs e)
        //{
        //    DateTime dt = (DateTime)Session["end_t"];
        //    DateTime dt_curr = DateTime.Now;
        //    TimeSpan ts = dt - dt_curr;
        //    lbltimer.Text = ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
        //    //if (ts.Minutes == 0)
        //    //{
        //    //    tmr1.Enabled = false;
        //    //    Response.Redirect("/TestDummy/TestSearchShowroomForSalesPage.aspx");
        //    //}

        //}


    }
}