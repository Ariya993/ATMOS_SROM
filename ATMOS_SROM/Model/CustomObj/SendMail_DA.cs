using ATMOS_SROM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ATMOS_SROM.Model.CustomObj
{
    public class SendMail_DA
    {
        public void sendMailPoint(string CardNo)
        {
            #region Get Data
            MEMBER_DA memberDA = new MEMBER_DA();
            List<MS_MEMBER> Lmember = new List<MS_MEMBER>();
            Lmember = memberDA.getMemberForEmail("WHERE NO_CARD = '" + CardNo + "'");
            string sendTo = Lmember.FirstOrDefault().EMAIL;
            string Name = Lmember.FirstOrDefault().FIRST_NAME + " " + Lmember.FirstOrDefault().LAST_NAME;
            string poinRP = memberDA.getLastpoinRP("AND NO_CARD = '" + CardNo + "'");
            if (poinRP == "")
            { poinRP = "0,00"; }
            string poinRD = memberDA.getLastpoinRD("AND NO_CARD = '" + CardNo + "'");
            if (poinRD == "")
            { poinRD = "0,00"; }
            string pointBalance = memberDA.getLastpoinBalance("WHERE NO_CARD = '" + CardNo + "'");
            if (pointBalance == "")
            { pointBalance = "0,00"; }
            #endregion
            //string htmlBody = "<html><body>Dear William <br/><br/><label align=center text=testing middle /></body></html></body></html>";
            MailMessage mail = new MailMessage("natasha.angelica2212@gmail.com", sendTo);
            StringBuilder body = new StringBuilder();
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("natasha.angelica2212@gmail.com", "sunouyuki88");//new System.Net.NetworkCredential("testng462", "1401100273");
            mail.Subject = "707 Point rewards " + CardNo;
            body.AppendLine(" ");
            body.AppendLine("Dear " + Name + ",");
            body.AppendLine(" ");
            body.AppendLine("Thank you for using 707 Membership Card");
            body.AppendLine("Below is summary of the membership card transaction that you have performed :");
            body.AppendLine(" ");
            body.AppendLine("Date                 : ");
            body.AppendLine("Transaction Type     : Purchase / Reedem ");
            body.AppendLine("Poin Add / Deduction :  " + poinRP + " / " + poinRD);
            body.AppendLine("Poin Balance         : " + pointBalance);
            body.AppendLine(" ");
            body.AppendLine("We advise that you keep this email as a reference of your membership Card transaction.");
            body.AppendLine(" ");
            body.AppendLine("Thank You ");
            body.AppendLine("Warm regards, ");
            body.AppendLine(" ");
            body.AppendLine("Card Center ");
            body.AppendLine("The 707 Group Of Company ");
            mail.Body = body.ToString();
            mail.IsBodyHtml = false;

            smtp.Send(mail);
            
        }

    }
}