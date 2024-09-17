using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Configuration
{
    public partial class Configuration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindConfiguration();
            }
        }

        protected void bindConfiguration()
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();

            listParam = loginDA.getListParam(" where NAME in ('lockFSS','lockSIS','lockHO')");

            tbLock.Text = listParam.Where(item => item.NAME.ToLower() == "lockfss").First().VALUE;
            tbLockSIS.Text = listParam.Where(item => item.NAME.ToLower() == "locksis").First().VALUE;
            tbLockHO.Text = listParam.Where(item => item.NAME.ToLower() == "lockho").First().VALUE;
            string maxVal = listParam.Max(item => item.VALUE);
            if (tbLock.Text == maxVal && tbLockSIS.Text == maxVal && tbLockHO.Text == maxVal)
            {
                lblmaxfbln.Text = maxVal;
                btnLockSldPeriode.Enabled = true;
            }
            else
            {
                lblmaxfbln.Text = "";
                btnLockSldPeriode.Enabled = false;
            }
        }

        protected void btnLockClick(object sender, CommandEventArgs e)
        {
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sName = Session["UName"] == null ? "" : Session["UName"].ToString();
            string tbBulan = e.CommandName == "FSS" ? tbLock.Text : e.CommandName == "SIS" ? tbLockSIS.Text : tbLockHO.Text;
            string tbLockBulan = e.CommandName == "FSS" ? tbLockDate.Text : e.CommandName == "SIS" ? tbLockDateSIS.Text : tbLockDateHO.Text;
            string statusShow1 = e.CommandName == "FSS" ? "FSS" : e.CommandName == "SIS" ? "SIS" : "WHU";
            string statusShow2 = e.CommandName;

            string year = "20" + tbBulan.Remove(2);
            string month = tbBulan.Remove(0, 2);
            //1601
            //Yang di butuhin 01-01-2016, 02-01-2016, 01-31-2016
            //format dd-MM-yyyy
            string bulanAwal = "01-" + month + "-" + year;
            int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));

            DateTime startDate = DateTime.Now;
            if (!string.IsNullOrEmpty(bulanAwal))
            {
                DateTime.TryParseExact(bulanAwal, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            }

            if (tbLockBulan == string.Format("{0:yyMM}", startDate.AddDays(daysInMonth)))
            {
                CONFIG_DA configDA = new CONFIG_DA();
                LOGIN_DA loginDA = new LOGIN_DA();
                MS_PARAMETER param = new MS_PARAMETER();

                DateTime endDate = startDate.AddDays(daysInMonth - 1);
                DateTime cutOff = startDate.AddDays(daysInMonth);
                param.VALUE = tbLockBulan;
                param.NAME = e.CommandName == "FSS" ? "lockFSS" : e.CommandName == "SIS" ? "lockSIS" : "lockHO";
                //usp_insertKartuStock('05-01-2016','06-01-2016', '1605', '05-01-2016', '05-31-2016','system','FSS','FSS')

                string a = configDA.insertTRFStock(startDate, cutOff, tbLockBulan, startDate, endDate, sName, statusShow1, statusShow2);
                if (!a.Contains("ERROR"))
                {
                    loginDA.updateValueParam(param);

                    bindConfiguration();

                    string message = e.CommandName == "FSS" ? "FSS" : e.CommandName == "SIS" ? "SIS" : "Warehouse dan Head Office";
                    DivMessage.InnerText = string.Format("Data Stock untuk Bulan {0} {1} sudah berhasil di kunci!", tbLockBulan, message);
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    tbLockDate.Text = "";
                    tbLockDateSIS.Text = "";
                    tbLockDateHO.Text = "";
                }
                else
                {
                    DivMessage.InnerText = string.Format("{0}", a);
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = string.Format("Bulan Harus +1 dari bulan lock terakhir!");
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnUnlockClick(object sender, CommandEventArgs e)
        {
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sName = Session["UName"] == null ? "" : Session["UName"].ToString();
            string tbBulan = e.CommandName == "FSS" ? tbLock.Text : e.CommandName == "SIS" ? tbLockSIS.Text : tbLockHO.Text;
            string tbLockBulan = e.CommandName == "FSS" ? tbLockDate.Text : e.CommandName == "SIS" ? tbLockDateSIS.Text : tbLockDateHO.Text;
            string statusShow1 = e.CommandName == "FSS" ? "FSS" : e.CommandName == "SIS" ? "SIS" : "WHU";
            string statusShow2 = e.CommandName;

            string year = "20" + tbBulan.Remove(2);
            string month = tbBulan.Remove(0, 2);
            //1601
            //Yang di butuhin 01-01-2016, 02-01-2016, 01-31-2016
            //format dd-MM-yyyy
            string bulanAwal = "01-" + month + "-" + year;
            int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));

            DateTime startDate = DateTime.Now;
            if (!string.IsNullOrEmpty(bulanAwal))
            {
                DateTime.TryParseExact(bulanAwal, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            }

            if (tbLockBulan == string.Format("{0:yyMM}", startDate.AddMonths(-1)))
            {
                CONFIG_DA configDA = new CONFIG_DA();
                LOGIN_DA loginDA = new LOGIN_DA();
                MS_PARAMETER param = new MS_PARAMETER();

                DateTime endDate = startDate.AddDays(daysInMonth - 1);
                DateTime cutOff = startDate.AddDays(daysInMonth);
                param.VALUE = tbLockBulan;
                param.NAME = e.CommandName == "FSS" ? "lockFSS" : e.CommandName == "SIS" ? "lockSIS" : "lockHO";
                //usp_insertKartuStock('05-01-2016','06-01-2016', '1605', '05-01-2016', '05-31-2016','system','FSS','FSS')

                string a = configDA.unlockData(tbLockBulan, tbBulan, param.NAME, statusShow1, statusShow2);
                if (!a.Contains("ERROR"))
                {
                    //loginDA.updateValueParam(param);

                    bindConfiguration();

                    string message = e.CommandName == "FSS" ? "FSS" : e.CommandName == "SIS" ? "SIS" : "Warehouse dan Head Office";
                    DivMessage.InnerText = string.Format("Data Stock untuk Bulan {0} {1} sudah berhasil di buka!", tbLockBulan, message);
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    tbLockDate.Text = "";
                    tbLockDateSIS.Text = "";
                    tbLockDateHO.Text = "";
                }
                else
                {
                    DivMessage.InnerText = string.Format("{0}", a);
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = string.Format("Bulan Harus -1 dari bulan lock terakhir!");
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnLockSldPeriode_Click(object sender, EventArgs e)
        {
            if (lblmaxfbln.Text != "")
            {
                DateTime yeramonthMAxNow;
                DateTime yearMonth = DateTime.ParseExact(lblmaxfbln.Text, "yyMM", null);
                TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();
                string maxFbln = tfmutasiStockPOSDA.getMaxFblnSldPeriode("");
                if (maxFbln == "")
                {
                    yeramonthMAxNow = DateTime.ParseExact(lblmaxfbln.Text, "yyMM", null);
                }
                else
                {
                   yeramonthMAxNow = DateTime.ParseExact(maxFbln, "yyMM", null);
                }
                String mindt = yearMonth.ToString();//yearMonth.ToString("yyyy-MM-dd");
                String maxdt = yearMonth.AddMonths(1).AddDays(-1).ToString();
                int tm = yearMonth.Month - yeramonthMAxNow.Month ;
                //if (tm > 1)
                //{
                //    DivMessage.InnerText = "Ada Bulan yang belum di closing antara : " + maxFbln + " dan " + lblmaxfbln.Text;
                //    DivMessage.Attributes["class"] = "success";
                //    DivMessage.Visible = true;
                //}
                //else
                //{
                    string res = tfmutasiStockPOSDA.InsLoopSldPeriode(yearMonth, yearMonth.AddMonths(1).AddDays(-1));
                    if (res.Contains("ERROR"))
                    {
                        DivMessage.InnerText = "Closing SLD PERIODE Berhasil : " + res;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = "Closing SLD PERIODE Berhasil untuk Showroom Bulan : " + lblmaxfbln.Text;
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                        //btnLockSldPeriode.Enabled = false;
                    }
                //}
            }

        }
    }
}