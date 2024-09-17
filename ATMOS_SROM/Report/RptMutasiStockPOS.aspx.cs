using ATMOS_SROM.Domain;
using ATMOS_SROM.Domain.CustomObj;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Report
{
    public partial class RptMutasiStockPOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();
                bindStore();
                closing.Enabled = false;
                //string wheremaxfbln = " where KODE = '" + lblKode.Text + "'";
                lbllastclose.Text = tfmutasiStockPOSDA.getMaxFblnSldPeriode("");
            }
        }
        protected void bindStore()
        {
            ddlStore.Enabled = true;
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            MS_SHOWROOM showRoom = new MS_SHOWROOM();

            showRoom.SHOWROOM = "--Pilih Showroom--";
            showRoom.KODE = "";
            listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' ORDER BY SHOWROOM");
            listStore.Insert(0, showRoom);
            ddlStore.DataSource = listStore;
            ddlStore.DataBind();
            int index = 0;

            ddlStore.SelectedIndex = index;
        }
        protected void BindGrid()
        {
            TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();

            DateTime yearMonth = DateTime.ParseExact(tbBulanStock.Text, "yyMM", null);
            mindt.Text = yearMonth.ToString();//yearMonth.ToString("yyyy-MM-dd");
            maxdt.Text = yearMonth.AddMonths(1).AddDays(-1).ToString();//yearMonth.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            lblKode.Text = ddlStore.SelectedValue;
            //string where = "'" + mindt.Text + "','" + maxdt.Text + "','','" + ddlStore.SelectedValue + "'";
            string wherefbln = " where FBLN ='" + tbBulanStock.Text + "' AND KODE = '" + lblKode.Text + "'";
            string res = "";
            res = tfmutasiStockPOSDA.getFblnSldPeriode(wherefbln);
            string wheremaxfbln = " where KODE = '" + lblKode.Text + "'";
            lbllastclose.Text = tfmutasiStockPOSDA.getMaxFblnSldPeriode(wheremaxfbln);
            if (!res.Contains("ERROR"))
            {

                List<SLD_PERIODE> listsldPeriode = new List<SLD_PERIODE>();
                listsldPeriode = tfmutasiStockPOSDA.getListSldPeriode(wherefbln);
                gvMain.DataSource = listsldPeriode;
                gvMain.DataBind();
            }
            else
            {
                List<TF_MUTASI_STOCK_POS> listtfmutasiStockPOS = new List<TF_MUTASI_STOCK_POS>();
                listtfmutasiStockPOS = tfmutasiStockPOSDA.getTfMutasiStockPOS(yearMonth, yearMonth.AddMonths(1).AddDays(-1), "", ddlStore.SelectedValue);//(where);
                gvMain.DataSource = listtfmutasiStockPOS;
                gvMain.DataBind();
            }

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (ddlStore.SelectedValue == "" || tbBulanStock.Text == "")
            {
                DivMessage.InnerText = "Showroom Harus Dipilih & Bulan Harus di isi !";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
                closing.Enabled = false;
            }
            else
            {
                BindGrid();
                closing.Enabled = true;
                DivMessage.Visible = false;
            }
        }

        protected void closing_Click(object sender, EventArgs e)
        {
            if (lblKode.Text == ddlStore.SelectedValue)
            {
                DateTime yearMonth = DateTime.ParseExact(tbBulanStock.Text, "yyMM", null);
                if (mindt.Text == yearMonth.ToString())
                {
                    TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();
                    string res = tfmutasiStockPOSDA.InsSldPeriode(yearMonth, yearMonth.AddMonths(1).AddDays(-1), "", ddlStore.SelectedValue);
                    if (res.Contains("ERROR"))
                    {
                        DivMessage.InnerText = res;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = "Closing Berhasil untuk Showroom : " + ddlStore.SelectedValue + ", Bulan : " + yearMonth;
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                        closing.Enabled = false;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Bulan Closing dengan Bulan Hasil Pencarian Berdeda !";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Showroom yang di Close dengan Showroom Hasil Pencarian Berdeda !";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGnr_Click(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            if (ddlStore.SelectedItem.Value == "")
            {
                DivMessage.InnerText = "Silahkan Pilih Showroom !";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
            else if (tbBulanStock.Text == "")
            {
                DivMessage.InnerText = "Silahkan Masukan Bulan Yang Diinginkan !";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
            else
            {
                string fileName = ddlStore.SelectedItem.Text + "_" + tbBulanStock.Text;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                GridView db = new GridView();

                //DataSet searchData = cd.SearchData(tabel, field, "");
                //DataSet searchData = new DataSet();
                DateTime sDate = DateTime.Now;

                TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();
                List<SLD_PERIODE> listsldPeriode = new List<SLD_PERIODE>();
                string wherefbln = " where FBLN ='" + tbBulanStock.Text + "' AND KODE = '" + ddlStore.SelectedItem.Value + "'";
                listsldPeriode = tfmutasiStockPOSDA.getListSldPeriode(wherefbln);

                if (listsldPeriode.Count > 0)
                {

                    string strStyle = @"<style>.text { mso-number-format:\@; } runat=server </style>";
                    hw.WriteLine(strStyle);

                    Response.Clear();
                    db.DataSource = listsldPeriode;
                    db.DataBind();

                    for (int i = 0; i < listsldPeriode.Count; i++)
                    {
                        db.Rows[i].Cells[0].Attributes.Add("class", "text");
                        db.Rows[i].Cells[1].Attributes.Add("class", "text");
                        db.Rows[i].Cells[2].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[3].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[4].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[5].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[6].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[7].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[8].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[9].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[10].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[11].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[12].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[13].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[14].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[15].Attributes.Add("class", "text");
                    }

                    db.RenderControl(hw);
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.xls";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Write(tw.ToString());
                    Response.End();

                }
                else
                {
                    DivMessage.InnerText = "Data Tidak ditemukan untuk Showroom : " + ddlStore.SelectedItem.Text + ", Bulan : " + tbBulanStock.Text;
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
        }

        protected void btnGnrAll_Click(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            if (tbBulanStock.Text == "")
            {
                DivMessage.InnerText = "Silahkan Masukan Bulan Yang Diinginkan !";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
            else
            {
                string fileName = "ALLSHR_" + tbBulanStock.Text;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                GridView db = new GridView();

                //DataSet searchData = cd.SearchData(tabel, field, "");
                //DataSet searchData = new DataSet();
                DateTime sDate = DateTime.Now;

                TF_MUTASI_STOCK_POS_DA tfmutasiStockPOSDA = new TF_MUTASI_STOCK_POS_DA();
                List<SLD_PERIODE> listsldPeriode = new List<SLD_PERIODE>();
                string wherefbln = " where FBLN ='" + tbBulanStock.Text + "'";
                listsldPeriode = tfmutasiStockPOSDA.getListSldPeriode(wherefbln);
                if (listsldPeriode.Count > 0)
                {
                    DivMessage.Visible = false;
                    string strStyle = @"<style>.text { mso-number-format:\@; } runat=server </style>";
                    hw.WriteLine(strStyle);

                    Response.Clear();
                    db.DataSource = listsldPeriode;
                    db.DataBind();

                    for (int i = 0; i < listsldPeriode.Count; i++)
                    {
                        db.Rows[i].Cells[0].Attributes.Add("class", "text");
                        db.Rows[i].Cells[1].Attributes.Add("class", "text");
                        db.Rows[i].Cells[2].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[3].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[4].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[5].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[6].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[7].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[8].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[9].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[10].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[11].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[12].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[13].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[14].Attributes.Add("class", "numeric");
                        db.Rows[i].Cells[15].Attributes.Add("class", "text");
                    }
                    DivMessage.Visible = false;
                    db.RenderControl(hw);
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.xls";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Write(tw.ToString());
                    Response.End();

                }
                else
                {
                    DivMessage.InnerText = "Data Tidak ditemukan untuk Bulan : " + tbBulanStock.Text;
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
        }
    }
}