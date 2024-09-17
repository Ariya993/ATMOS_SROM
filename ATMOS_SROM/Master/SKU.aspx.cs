using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class SKU : System.Web.UI.Page
    {
        string src;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void bindPU()
        {
            string where = String.Format("where FILE_UP LIKE '%{0}%'", src);
            List<MS_SKU> ListSKU = new List<MS_SKU>();
            MS_SKU_DA skuDA = new MS_SKU_DA();

            ListSKU = skuDA.getListku(where);
            gvPU.DataSource = ListSKU;
            gvPU.DataBind();
            gvPU.Visible = true;
            gvPUSearch.Visible = false;
        }
        protected void bindPUSearch()
        {
            string where = String.Format("where {0} LIKE '%{1}%'", ddlSearch.SelectedValue, txtSearch.Text);
            List<MS_SKU> ListSKU = new List<MS_SKU>();
            MS_SKU_DA skuDA = new MS_SKU_DA();

            ListSKU = skuDA.getListku(where);
            gvPUSearch.DataSource = ListSKU;
            gvPUSearch.DataBind();
            gvPU.Visible = false;
            gvPUSearch.Visible = true;
            lblInfo.Visible = false;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";
            string filePath = string.Empty;
            MS_SKU_DA MsSKUDa = new MS_SKU_DA();

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);
            if (ExcelFileName != "")
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(FileUpload.FileName);


                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FileUpload.PostedFile.SaveAs(filePath);
                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    string user = Session["UName"].ToString(); //"SYSTEM";
                    string res = MsSKUDa.upMSSKU(FileUploadName, source, FileType, user);
                    lblInfo.Text = res;
                    lblInfo.Visible = true;
                    src = FileUploadName;
                    bindPU();
                }
            }
        }
        protected void gvPUPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvPU.PageIndex = e.NewPageIndex;
            bindPU();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindPUSearch();
        }

        protected void gvPUSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPUSearch.PageIndex = e.NewPageIndex;
            bindPUSearch();
        }
    }
}