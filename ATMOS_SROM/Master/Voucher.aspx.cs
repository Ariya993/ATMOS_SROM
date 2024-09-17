using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Master
{
    public partial class Voucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
                bindBrand();
            }
        }

        protected void bindGrid()
        {
            MS_ACARA_DA masterDA = new MS_ACARA_DA();
            List<MS_VOUCHER> listVoucher = new List<MS_VOUCHER>();

            //string uName = Session["UName"] == null ? "" : Session["UName"].ToString();
            string where = string.Format(" where {0} like '%{1}%'", ddlFilter.SelectedValue, tbFilterText.Text);

            listVoucher = masterDA.getListVoucher(where);

            gvMain.DataSource = listVoucher;
            gvMain.DataBind();

            DivMessage.Visible = false;
        }

        protected void bindBrand()
        {
            //MASTER_DA masterDA = new MASTER_DA();
            //List<MS_BRAND> listBrand = new List<MS_BRAND>();

            //string uName = Session["UName"] == null ? "" : Session["UName"].ToString();
            //string where = string.Format(" where USERNAME like '{0}'", uName);

            //listBrand = masterDA.getListBrand(where);

            //ddlVDBrand.DataSource = listBrand;
            //ddlVDBrand.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnAddClick(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = fileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = fileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_Acara" + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    fileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    bool ret = true;

                    ret = upload(dir, FileUploadName, source);
                    File.Delete(source);

                    if (ret)
                    {
                        bindGrid();
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "File Harus Bertipe xls ataus xlsx.";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Pilih File Yang Akan Diupload.";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnAddVoucherClick(object sender, EventArgs e)
        {
            hdnVDID.Value = "";
            tbVDNoVoucher.Text = "";
            tbVDNilai.Text = "";
            ddlVDJenis.SelectedIndex = 0;
            tbVDStartDates.Text = "";
            tbVDEndDates.Text = "";
            ddlVDStatus.SelectedValue = "ACTIVE";
            ModalViewData.Show();
        }

        protected void btnVDSaveClick(object sender, EventArgs e)
        {
            if (checkTextBox())
            {
                MS_VOUCHER msVoucher = new MS_VOUCHER();
                MS_ACARA_DA masterDA = new MS_ACARA_DA();
                string startDate = Request.Form[tbVDStartDates.UniqueID];
                string endDate = Request.Form[tbVDEndDates.UniqueID];

                DateTime from = DateTime.Now;
                string fromDate = tbVDStartDates.Visible ? startDate : string.Format("{0:dd-MM-yyyy}", DateTime.Now);
                if (!string.IsNullOrEmpty(fromDate))
                {
                    DateTime.TryParseExact(fromDate, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out from);
                }

                DateTime until = DateTime.Now;
                string untilDate = tbVDEndDates.Visible ? endDate : string.Format("{0:dd-MM-yyyy}", DateTime.Now);
                if (!string.IsNullOrEmpty(untilDate))
                {
                    DateTime.TryParseExact(untilDate, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out until);
                }

                msVoucher.NO_VOUCHER = tbVDNoVoucher.Text.Trim();
                msVoucher.NILAI = Convert.ToDecimal(tbVDNilai.Text);
                msVoucher.JENIS = ddlVDJenis.SelectedValue;
                msVoucher.VALID_FROM = from;
                msVoucher.VALID_UNTIL = until;
                msVoucher.STATUS_VOUCHER = ddlVDStatus.SelectedValue;

                msVoucher.CREATED_BY = Session["UName"].ToString();
                msVoucher.UPDATED_BY = Session["UName"].ToString();

                if (hdnVDID.Value == "")
                {
                    //Insert Data
                    masterDA.insertMsVoucher(msVoucher);
                }
                else
                {
                    //Update Data
                    msVoucher.ID = Convert.ToInt64(hdnVDID.Value);
                    masterDA.updateMsVoucher(msVoucher);
                }

                bindGrid();
                DivMessage.InnerText = "Berhasil!";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            else
            {
                divVDMessage.InnerText = "Cek Semua Field!";
                divVDMessage.Attributes["class"] = "warning";
                divVDMessage.Visible = true;
                ModalViewData.Show();
            }
        }

        protected void btnExcelClick(object sender, EventArgs e)
        {
            MS_ACARA_DA masterDA = new MS_ACARA_DA();
            string where = string.Format(" where {0} like '%{1}%'", ddlFilter.SelectedValue, tbFilterText.Text);
            DataSet ds = new DataSet();

            ds = masterDA.GeneratVoucher(where);
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //GridView1.Columns[1].Visible = false;

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Voucher.xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvMainCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName == "EditRow")
                    {
                        hdnVDID.Value = id;
                        tbVDNoVoucher.Text = gvMain.Rows[rowIndex].Cells[2].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[2].Text.ToString();
                        tbVDNilai.Text = gvMain.Rows[rowIndex].Cells[3].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[3].Text.ToString();
                        ddlVDJenis.SelectedValue = gvMain.Rows[rowIndex].Cells[4].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[4].Text.ToString();
                        tbVDStartDates.Text = gvMain.Rows[rowIndex].Cells[5].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[5].Text.ToString();
                        tbVDEndDates.Text = gvMain.Rows[rowIndex].Cells[6].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[6].Text.ToString();
                        ddlVDStatus.SelectedValue = gvMain.Rows[rowIndex].Cells[7].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[7].Text.ToString();
                        //tbVDEvent.Text = gvMain.Rows[rowIndex].Cells[13].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[13].Text.ToString();
                        //ddlVDBrand.SelectedValue = gvMain.Rows[rowIndex].Cells[14].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[14].Text.ToString();

                        if (ddlVDStatus.SelectedIndex == 2)
                        {
                            tbVDNoVoucher.Enabled = false;
                            tbVDNilai.Enabled = false;
                            ddlVDJenis.Enabled = false;
                            tbVDStartDates.Enabled = false;
                            tbVDEndDates.Enabled = false;
                            ddlVDStatus.Enabled = false;

                            ddlVDStatus.Items[2].Attributes["style"] = "block";
                        }
                        else
                        {
                            tbVDNoVoucher.Enabled = true;
                            tbVDNilai.Enabled = true;
                            ddlVDJenis.Enabled = true;
                            tbVDStartDates.Enabled = true;
                            tbVDEndDates.Enabled = true;
                            ddlVDStatus.Enabled = true;

                            ddlVDStatus.Items[2].Attributes["style"] = "none";
                        }
                        ModalViewData.Show();
                    }
                    else if (e.CommandName == "DeleteRow")
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected bool upload(string dir, string fileName, string source)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;

            try
            {
                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                cnn.Open();
                System.Data.DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }

                //string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                //string lastSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                string firstSheetName = "Voucher$";
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                MS_ACARA_DA masterDA = new MS_ACARA_DA();
                List<MS_VOUCHER> listVoucher = new List<MS_VOUCHER>();
                bool cek = true;
                cek = cek && cekUploadFile(dsOle.Tables[0]);
                if (cek)
                {
                    string uName = Session["UName"] == null ? "" : Session["UName"].ToString();
                    listVoucher = masterDA.getListVoucher("");
                    masterDA.clearTempVoucher(Session["UName"].ToString());
                    foreach (DataRow item in dsOle.Tables[0].Rows)
                    {
                        if (item[0].ToString().Trim() != "")
                        {
                            if (listVoucher.Where(itemw => itemw.NO_VOUCHER.ToString() == item[0].ToString()).Count() == 0)
                            {
                                MS_VOUCHER msVoucher = new MS_VOUCHER();
                                msVoucher.NO_VOUCHER = item[0].ToString();
                                msVoucher.NILAI = Convert.ToDecimal(item[1]);
                                msVoucher.JENIS = item[2].ToString();
                                msVoucher.VALID_FROM = Convert.ToDateTime(item[3]);
                                msVoucher.VALID_UNTIL = Convert.ToDateTime(item[4]);
                                msVoucher.STATUS_VOUCHER = "ACTIVE";
                                msVoucher.CREATED_BY = Session["UName"].ToString();

                                string berhasill = masterDA.insertTempVoucher(msVoucher);

                                if (berhasill.Contains("ERROR"))
                                    doubel++;
                                else
                                {
                                    insert++;
                                    listVoucher.Add(msVoucher);
                                }
                                data++;
                            }
                        }
                    }

                    masterDA.insertVoucherFromTemp(Session["UName"].ToString());
                    //insertHarga(idUpload.Remove(0, 1));
                    DivMessage.InnerText = "Upload Berhasil! | Data : " + "|" + data.ToString() + " | Insert Data : " + insert.ToString() + " | Error Data : " + doubel.ToString();
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            return ret;
        }
        protected bool cekUploadFile(DataTable dTab)
        {
            bool check = true;
            if (check)
            {
                for (int i = 0; i < dTab.Rows.Count; i++)
                {

                    DateTime dtFrom = Convert.ToDateTime(dTab.Rows[i][3].ToString());
                    DateTime dtUntil = Convert.ToDateTime(dTab.Rows[i][4].ToString());
                    if (dtFrom > dtUntil)
                    {
                        check = false;

                        DivMessage.InnerText = "Tanggal Valid From Tidak Boleh Lebih Besar Dari Tanggal Valid Until, harap di cek kembali di Line " + (i + 1);
                        DivMessage.Attributes["class"] = "warning";
                        DivMessage.Visible = true;

                        i = dTab.Rows.Count + 1;
                    }
                }
            }

            return check;
        }

        protected bool checkTextBox()
        {
            string startDate = Request.Form[tbVDStartDates.UniqueID];
            string endDate = Request.Form[tbVDEndDates.UniqueID];

            if (tbVDNoVoucher.Text == "" || tbVDNilai.Text == "" || startDate == "" || endDate == "")
            {
                return false;
            }
            else
                return true;
        }
    }
}