using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Laporan
{
    public partial class LapStatistikDokumen : System.Web.UI.Page
    {
        #region "METHOD"
        protected void GetLastLock()
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            MS_PARAMETER listParam = new MS_PARAMETER();
            MS_SHOWROOM show = new MS_SHOWROOM();

            show = showDA.getShowRoom(" WHERE KODE = '" + ddlStore.SelectedValue + "'").FirstOrDefault(); ;
            if (show.STATUS_SHOWROOM == "FSS")
            {
                listParam = loginDA.getListParam(" where NAME in ('lockFSS')").FirstOrDefault();
            }
            else if (show.STATUS_SHOWROOM == "SIS")
            {
                listParam = loginDA.getListParam(" where NAME in ('lockSIS')").FirstOrDefault();
            }
            else
            {
                listParam = loginDA.getListParam(" where NAME in ('lockHO')").FirstOrDefault();
            }
            lblLastClosingValue.Text = listParam.VALUE;
        }
        protected void bindStore()
        {
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            string sShow = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> allListShow = new List<MS_SHOWROOM>();
            List<MS_SHOWROOM> listShow = new List<MS_SHOWROOM>();

            allListShow = showDA.getShowRoom(" WHERE STATUS_SHOWROOM !='SUP' ORDER BY SHOWROOM");
            if (sLevel == "Sales" || sLevel == "Store Manager")
            {
                listShow = allListShow.Where(item => item.KODE == sKode).ToList<MS_SHOWROOM>();
                ddlStore.Enabled = false;
            }
            else
            {
                if (sLevel == "Admin Sales")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "FSS").ToList<MS_SHOWROOM>();
                }
                else if (sLevel == "Admin Counter" && sKode != "HO-001")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "SIS" && item.KODE == sKode).ToList<MS_SHOWROOM>();
                }
                else if (sLevel == "Admin Counter" && sKode == "HO-001")
                {
                    listShow = allListShow.Where(item => item.STATUS_SHOWROOM == "SIS").ToList<MS_SHOWROOM>();
                }
                else
                {
                    listShow = allListShow;
                }
            }
            ddlStore.DataSource = listShow;
            ddlStore.DataValueField = "KODE";
            ddlStore.DataTextField = "SHOWROOM";

            ddlStore.DataBind();

        }
        protected void BindGridMain()
        {
            REPORT_DA rptDA = new REPORT_DA();
            DataSet dsGridMain = new DataSet();
            if (tbDate.Text != "" || tbDate.Text == null)
            {
                //DateTime dtRpt = Convert.ToDateTime(tbDate.Text);
                string year = "20" + tbDate.Text.Remove(2);
                string month = tbDate.Text.Remove(0, 2);
                string bulanAwal = "01-" + month + "-" + year;
                DateTime startDate = DateTime.Now;
                if (!string.IsNullOrEmpty(bulanAwal))
                {
                    DateTime.TryParseExact(bulanAwal, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                }
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                hdnDtStart.Value = Convert.ToString(startDate);
                hdnDtEnd.Value = Convert.ToString(endDate);
                hdnKodeShr.Value = ddlStore.SelectedValue;
                #region "RadioButton"
                if (rbtADJUSTMENT.Checked == true)
                {
                    rbtvalue.Value = "ADJUSTMENT";
                    hdnExcelName.Value = "Statistik_Adjustment";
                }
                else if (rbtIN_PINJAM.Checked == true)
                {
                    rbtvalue.Value = "IN_PINJAM";
                    hdnExcelName.Value = "Statistik_In_Pinjam";
                }
                else if (rbtJL_PUTUS.Checked == true)
                {
                    rbtvalue.Value = "JL_PUTUS";
                    hdnExcelName.Value = "Statistik_Jual_Putus";
                }
                else if (rbtOUT_PINJAM.Checked == true)
                {
                    rbtvalue.Value = "OUT_PINJAM";
                    hdnExcelName.Value = "Statistik_Out_Pinjam";
                }
                else if (rbtPO_GR.Checked == true)
                {
                    rbtvalue.Value = "PO_GR";
                    hdnExcelName.Value = "Statistik_Beli_Terima_dr_Supplier";
                }
                else if (rbtRTR_PUTUS.Checked == true)
                {
                    rbtvalue.Value = "RTR_PUTUS";
                    hdnExcelName.Value = "Statistik_Retur_Putus";
                }
                else if (rbtSALES.Checked == true)
                {
                    rbtvalue.Value = "SALES";
                    hdnExcelName.Value = "Statistik_Sales";
                }
                else if (rbtSTOCK_OP.Checked == true)
                {
                    rbtvalue.Value = "STOCK_OP";
                    hdnExcelName.Value = "Statistik_Out_Pinjam";
                }
                else if (rbtTRF_KIRIM.Checked == true)
                {
                    rbtvalue.Value = "TRF_KIRIM";
                    hdnExcelName.Value = "Statistik_Trf_Kirim";
                }
                else if (rbtTRF_TERIMA.Checked == true)
                {
                    rbtvalue.Value = "TRF_TERIMA";
                    hdnExcelName.Value = "Statistik_Trf_Terima";
                }
                else
                {
                    rbtvalue.Value = "";
                }
                #endregion
                if (rbtvalue.Value == "")
                {
                    DivMessage.InnerText = "Pilih Jenis Dokumen!";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                    rbtPO_GR.Focus();
                }
                else
                {
                    dsGridMain = rptDA.GetDataTf_GetStatistikDokumen_Report(rbtvalue.Value, startDate, endDate, ddlStore.SelectedValue);
                    gvMain.DataSource = dsGridMain;
                    gvMain.DataBind();

                    object sumObjectQty, sumObjectDoc;
                    sumObjectQty = dsGridMain.Tables[0].Compute("Sum(TTL_QTY)", string.Empty);
                    sumObjectDoc = dsGridMain.Tables[0].Compute("Sum(TTL_DOK)", string.Empty);
                    lblttlqtyValue.Text = sumObjectQty.ToString();
                    lblttldokvalue.Text = sumObjectDoc.ToString();

                    DivMessage.Visible = false;

                }
            }
            else
            {
                DivMessage.InnerText = "Pilih Bulan!";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
                tbDate.Focus();
            }
        }
        protected void BindGridDetail()
        {
            REPORT_DA rptDA = new REPORT_DA();
            DataSet dsGridMain = new DataSet();
            dsGridMain = rptDA.GetDataTf_Dtl_GetStatistikDokumen_Report(rbtvalue.Value, hdnIdHeader.Value, Convert.ToDateTime(hdnDtStart.Value), Convert.ToDateTime(hdnDtEnd.Value), hdnKodeShr.Value);
            GvDetail.DataSource = dsGridMain.Tables[0];
            GvDetail.DataBind();
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindStore();
            }
        }

        protected void btnProses_Click(object sender, EventArgs e)
        {
            BindGridMain();
            GetLastLock();
        }

        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                gvMain.Columns[0].Visible = false;
                //gvMain.Columns[8].Visible = false;
                gvMain.AllowPaging = false;
                String FileName = hdnExcelName.Value;
                BindGridMain();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                HtmlForm frm = new HtmlForm();

                this.gvMain.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(this.gvMain);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                gvMain.Columns[0].Visible = true;
                //gvMain.Columns[8].Visible = true;
                gvMain.AllowPaging = true;
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            BindGridMain();
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["id"].ToString();
                    //string NoDok = gvMain.DataKeys[rowIndex]["No_Dok"].ToString();
                    //string NoDok = gvMain.Rows[rowIndex].Cells[2].Text;
                    //GridViewRow row = gvMain.SelectedRow;
                    GridViewRow row = gvMain.Rows[rowIndex];
                    String NoDok = row.Cells[2].Text;
                    if (e.CommandName == "SelectRow")
                    {
                        hdnIdHeader.Value = id;
                        //hdnNoDoc.Value = NoDok;
                        BindGridDetail();
                        mpe.Show();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDetail.PageIndex = e.NewPageIndex;
            BindGridDetail();
            mpe.Show();

        }

        protected void btnPrintDetail_Click(object sender, EventArgs e)
        {
            try
            {
                //GvDetail.Columns[0].Visible = false;
                //GvDetail.Columns[8].Visible = false;
                GvDetail.AllowPaging = false;
                String FileName = "Dtl_" + hdnExcelName.Value;
                BindGridDetail();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                HtmlForm frm = new HtmlForm();

                this.GvDetail.Parent.Controls.Add(frm);
                this.EnableViewState = false;
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(this.GvDetail);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                //gvMain.Columns[0].Visible = true;
                //gvMain.Columns[8].Visible = true;
                GvDetail.AllowPaging = true;
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}