using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Master
{
    public partial class EPC : System.Web.UI.Page
    {
        #region "ATMOS"
        private string KdEPC = "09-";
        #endregion
        private string KdEPCMRA = "08-";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
            }
        }

        protected void bindGrid()
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<MS_EMPLOYEE> employee = new List<MS_EMPLOYEE>();
            string where = string.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            employee = bayarDA.getEmployee(where);
            gvMain.DataSource = employee;
            gvMain.DataBind();

            DivMessage.Visible = false;
        }

        protected void btnAddEPCClick(object sender, EventArgs e)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            MS_PARAMETER param = new MS_PARAMETER();

            param = loginDA.getParam(" where NAME = 'EPC'");
            //lbPUNoUrut.Text = param.VALUE.PadLeft(3, '0');
            string nmrEPC = param.VALUE.PadLeft(4, '0');
            lbPUNoUrut.Text = nmrEPC.Substring(nmrEPC.Length - 4); ;
            hdnId.Value = "";
            ModalAddEdit.Show();
            ClearAddEPCPopUp();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void tbPUDateJoinChange(object sender, EventArgs e)
        {
            //string joinDate = tbPUDateJoin.Text.Replace("-", "").Remove(4, 2);
            //tbPUNIK.Text = "08-" + joinDate + "-" + lbPUNoUrut.Text;
            //tbPUNIK.Text = "70-" + joinDate + "-" + lbPUNoUrut.Text;
            generateEPCNo();

            ModalAddEdit.Show();
        }
        protected void generateEPCNo()
        {
            string joinDate = "";
            DateTime dtnow = DateTime.Now;
            String dt = DateTime.Now.ToString("MM-dd-yyyy");// dtnow.ToShortDateString();
            if (tbPUDateJoin.Text != "")
            {
                joinDate = tbPUDateJoin.Text.Replace("-", "").Remove(4, 2);
            }
            else
            {
                joinDate = dt.Replace("-", "").Remove(4, 2);
            }
            if (ChkMRA.Checked == true)
            {
                tbPUNIK.Text = KdEPCMRA + joinDate + "-" + lbPUNoUrut.Text;
            }
            else
            {
                tbPUNIK.Text = KdEPC + joinDate + "-" + lbPUNoUrut.Text;
            }
        }

        protected void bPUCloseClick(object sender, EventArgs e)
        {
            ModalAddEdit.Hide();
        }

        protected void btnPUSaveClick(object sender, EventArgs e)
        {
            MS_EMPLOYEE_DA empDA = new MS_EMPLOYEE_DA();
            if (hdnId.Value.Trim() == "")
            {
                //Hitung limit proposional per hari
                string joinDate = tbPUDateJoin.Text.Trim();

                double limit = Convert.ToDouble(tbPULimitSOS.Text.Trim());
                double limitDelami = Convert.ToDouble(tbPULimitDelami.Text.Trim());
                DateTime dateJoin = DateTime.Now;
                DateTime.TryParseExact(joinDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateJoin);

                DateTime dateYear = new DateTime(dateJoin.Year, 12, 31);
                double sisaDay = dateYear.Subtract(dateJoin).TotalDays;

                decimal limitProp = Convert.ToDecimal(limit * (sisaDay / dateYear.DayOfYear));

                //Insert ke EPC
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                
                MS_EMPLOYEE employee = new MS_EMPLOYEE();
                employee.NIK = tbPUNIK.Text.Trim();
                employee.NAMA = tbPUNama.Text.Trim();
                employee.JABATAN = tbPUJabatan.Text.Trim();
                employee.JOIN_DATE = dateJoin;
                employee.LIMIT = Convert.ToDecimal(limit);
                employee.STATUS_EMPLOYEE = "active";
                employee.CREATED_BY = Session["UName"].ToString();
                employee.TIPE = cbPURelasi.Checked ? "Relasi" : "Karyawan";
                employee.LIMIT_DELAMI = Convert.ToDecimal(limitDelami);
                employee.STATUS_EPC = ChkMRA.Checked ? "MRA" : "ATM";
                employee.SISA_LIMIT = Convert.ToDecimal(limit);

              string resInsEmp = bayarDA.insertMsEmployee(employee);
                //Update DCard
                string idepc = empDA.insertMsEPC(employee);
               string resDcard=  empDA.insertMsLIMIT(employee, idepc);
               #region "Insert Update SALDO AWAL EPC DCARD"
               //DateTime firstDayCurrentYear = new DateTime(dateJoin.Year, 1, 1);
               //empDA.InsSldAwalEPC(firstDayCurrentYear, employee.NIK);
               #endregion
               //End Update DCard

                //Update parameter

                LOGIN_DA loginDA = new LOGIN_DA();
                MS_PARAMETER param = new MS_PARAMETER();
                param.NAME = "EPC";
                param.VALUE = Convert.ToString(Convert.ToInt32(lbPUNoUrut.Text) + 1);
                loginDA.updateValueParam(param);

                bindGrid();

                DivMessage.InnerText = "Insert New EPC berhasil ! No EPC : " + employee.NIK;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            else
            {
                string joinDate = tbPUDateJoin.Text.Trim();
                DateTime dateJoin = DateTime.Now;
                DateTime.TryParseExact(joinDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateJoin);

                double limit = Convert.ToDouble(tbPULimitSOS.Text.Trim());
                double limitDelami = Convert.ToDouble(tbPULimitDelami.Text.Trim());

                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_EMPLOYEE employee = new MS_EMPLOYEE();
                employee.NIK = tbPUNIK.Text.Trim();
                employee.NAMA = tbPUNama.Text.Trim();
                employee.JABATAN = tbPUJabatan.Text.Trim();
                employee.JOIN_DATE = dateJoin;
                employee.LIMIT = Convert.ToDecimal(limit);
                employee.STATUS_EMPLOYEE = "active";
                employee.UPDATED_BY = Session["UName"].ToString(); ;
                employee.TIPE = cbPURelasi.Checked ? "Relasi" : "Karyawan";
                employee.LIMIT_DELAMI = Convert.ToDecimal(limitDelami);
                employee.STATUS_EPC = ChkMRA.Checked ? "MRA" : "ATM";
                employee.ID = Convert.ToInt64( hdnId.Value);
                bayarDA.updateMsEmployee(employee);
                string res = empDA.updateEPC(employee);
                bindGrid();

                DivMessage.InnerText = "Update EPC berhasil ! No EPC : " + employee.NIK;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            ClearAddEPCPopUp();

        }

        protected void btnGenerateEPCClick(object sender, EventArgs e)
        {
            try
            {
                gvMain.Columns[0].Visible = false;
                gvMain.Columns[8].Visible = false;
                gvMain.AllowPaging = false;
                bindGrid();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Epc.xls");
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
                gvMain.Columns[8].Visible = true;
                gvMain.AllowPaging = true;
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvMainPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                        MS_EMPLOYEE employee = new MS_EMPLOYEE();
                        MS_EMPLOYEE_DA empDA = new MS_EMPLOYEE_DA();
                        employee.NIK = gvMain.Rows[rowIndex].Cells[3].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[3].Text;
                        employee.UPDATED_BY = Session["UName"].ToString();
                        employee.STATUS_EMPLOYEE = "resign";
                        employee.ID = Convert.ToInt64(id);

                        bayarDA.deleteMsEmployee(employee);

                        bindGrid();
                        //Untuk Delete VIP Member
                        if (gvMain.Rows[rowIndex].Cells[3].Text.Contains("707-02-"))
                        {
                            DivMessage.InnerText = "Delete VIP Member berhasil ! No Member : " + employee.NIK;
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                        // Untuk Delete VIP Member
                        else
                        {
                            empDA.DeleteEPC(employee);
                            DivMessage.InnerText = "Delete EPC berhasil ! No EPC : " + employee.NIK;
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }

                    }
                    else if (e.CommandName.ToLower() == "editrow")
                    {
                        //Edit VIP Member
                        if (gvMain.Rows[rowIndex].Cells[3].Text.Contains("707-02-"))
                        {
                            txtvipNo.Text = gvMain.Rows[rowIndex].Cells[3].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[3].Text.Substring(7);
                            txtNamaVip.Text = gvMain.Rows[rowIndex].Cells[4].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[4].Text;
                            txtJabatanVIP.Text = gvMain.Rows[rowIndex].Cells[5].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[5].Text;
                            txtjoindtvip.Text = gvMain.Rows[rowIndex].Cells[6].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[6].Text;
                            txtlimitsosVip.Text = gvMain.Rows[rowIndex].Cells[7].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[7].Text;
                            txtlimitdelamivip.Text = gvMain.Rows[rowIndex].Cells[8].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[8].Text;
                            string tipe = gvMain.Rows[rowIndex].Cells[16].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[16].Text; ;
                            //cbPURelasi.Checked = tipe.ToLower() == "relasi" ? true : false;

                            lbPUJudul.Text = "Edit VIP Member";
                            hdnVipId.Value = id;
                            ModalPopupAddVip.Show();
                        }
                        //End Edit VIP Member
                        else
                        {
                            tbPUNIK.Text = gvMain.Rows[rowIndex].Cells[3].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[3].Text;
                            tbPUNama.Text = gvMain.Rows[rowIndex].Cells[4].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[4].Text;
                            tbPUJabatan.Text = gvMain.Rows[rowIndex].Cells[5].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[5].Text;
                            tbPUDateJoin.Text = gvMain.Rows[rowIndex].Cells[6].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[6].Text;
                            tbPULimitSOS.Text = gvMain.Rows[rowIndex].Cells[7].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[7].Text;
                            tbPULimitDelami.Text = gvMain.Rows[rowIndex].Cells[8].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[8].Text;
                            string tipe = gvMain.Rows[rowIndex].Cells[16].Text.Contains("&nbsp") ? "" : gvMain.Rows[rowIndex].Cells[16].Text; ;
                            cbPURelasi.Checked = tipe.ToLower() == "relasi" ? true : false;

                            lbPUJudul.Text = "Edit EPC";
                            hdnId.Value = id;
                            ModalAddEdit.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        #region "VIP MEMBER & EPC UPLOAD"
        protected void btnUploadVIPMEMBER_Click(object sender, EventArgs e)
        {
            modalUploadVipMember.Show();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";
            string filePath = string.Empty;
            MS_EMPLOYEE_DA MsEmpDA = new MS_EMPLOYEE_DA();

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
                    int rescek = MsEmpDA.cekvipmemberup(FileUploadName, source, FileType, user);
                    if (rescek > 0)
                    {
                        dMsgVipMember.InnerText = "Error : Data yang Di Upload Tidak Lengkap";
                        dMsgVipMember.Attributes["class"] = "error";
                        dMsgVipMember.Visible = true;
                    }
                    else
                    {
                        string res = MsEmpDA.upMSEmployee(FileUploadName, source, FileType, user);
                        dMsgVipMember.InnerText = res;
                        dMsgVipMember.Attributes["class"] = "success";
                        dMsgVipMember.Visible = true;
                    }
                    modalUploadVipMember.Show();
                    //string res = MsEmpDA.upMSEmployee(FileUploadName, source, FileType, user);
                    //lblInfo.Text = res;
                    //lblInfo.Visible = true;
                    //src = FileUploadName;
                    //bindPU();
                }
            }
        }

        protected void btnupVipMemberClose_Click(object sender, EventArgs e)
        {
            modalUploadVipMember.Hide();
        }

        protected void btnVIPMemberSave_Click(object sender, EventArgs e)
        {
            if (hdnVipId.Value == "")
            {
                //Hitung limit proposional per hari
                string joinDate = txtjoindtvip.Text.Trim();

                double limit = Convert.ToDouble(txtlimitsosVip.Text.Trim());
                double limitDelami = Convert.ToDouble(txtlimitdelamivip.Text.Trim());
                DateTime dateJoin = DateTime.Now;
                DateTime.TryParseExact(joinDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateJoin);


                //Insert ke EPC
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_EMPLOYEE employee = new MS_EMPLOYEE();
                employee.NIK = lblvipnoH.Text + txtvipNo.Text.Trim();
                employee.NAMA = txtNamaVip.Text.Trim();
                employee.JABATAN = txtJabatanVIP.Text.Trim();
                employee.JOIN_DATE = dateJoin;
                employee.LIMIT = Convert.ToDecimal(limit);
                employee.STATUS_EMPLOYEE = "active";
                employee.CREATED_BY = Session["UName"].ToString();
                employee.TIPE = "SOS VIP MEMBER";//cbPURelasi.Checked ? "Relasi" : "Karyawan";
                employee.LIMIT_DELAMI = Convert.ToDecimal(limitDelami);
                employee.STATUS_EPC = "ATM";
                employee.SISA_LIMIT = Convert.ToDecimal(limit);

                bayarDA.insertMsEmployee(employee);
                bindGrid();

                DivMessage.InnerText = "Insert New VIP Member berhasil ! No Member : " + employee.NIK;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            else
            {
                //Hitung limit proposional per hari
                string joinDate = txtjoindtvip.Text.Trim();

                double limit = Convert.ToDouble(txtlimitsosVip.Text.Trim());
                double limitDelami = Convert.ToDouble(txtlimitdelamivip.Text.Trim());
                DateTime dateJoin = DateTime.Now;
                DateTime.TryParseExact(joinDate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateJoin);


                //Insert ke EPC
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_EMPLOYEE employee = new MS_EMPLOYEE();
                employee.NIK = lblvipnoH.Text + txtvipNo.Text.Trim();
                employee.NAMA = txtNamaVip.Text.Trim();
                employee.JABATAN = txtJabatanVIP.Text.Trim();
                employee.JOIN_DATE = dateJoin;
                employee.LIMIT = Convert.ToDecimal(limit);
                employee.STATUS_EMPLOYEE = "active";
                employee.UPDATED_BY = Session["UName"].ToString(); ;
                employee.TIPE = "SOS VIP MEMBER";//cbPURelasi.Checked ? "Relasi" : "Karyawan";
                employee.LIMIT_DELAMI = Convert.ToDecimal(limitDelami);
                employee.STATUS_EPC = "ATM";
                employee.SISA_LIMIT = Convert.ToDecimal(limit);
                employee.ID = Convert.ToInt64(hdnVipId.Value);
                bayarDA.updateMsEmployee(employee);
                bindGrid();

                DivMessage.InnerText = "update New VIP Member berhasil ! No Member : " + employee.NIK;
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
            ClearVIPPopUp();
        }

        protected void btnAddVipMember_Click(object sender, EventArgs e)
        {
            ModalPopupAddVip.Show();
            ClearVIPPopUp();

        }

        protected void btnUploadEPC_Click(object sender, EventArgs e)
        {
            modalUpEPC.Show();
        }

        protected void btnFupEPC_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";
            string filePath = string.Empty;
            MS_EMPLOYEE_DA MsEmpDA = new MS_EMPLOYEE_DA();

            string ExcelType = FupEPC.PostedFile.ContentType.ToLower();
            ExcelFileName = FupEPC.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(ExcelFileName);
            if (ExcelFileName != "")
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(FupEPC.FileName);


                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FupEPC.PostedFile.SaveAs(filePath);
                    FupEPC.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    string user = Session["UName"].ToString(); //"SYSTEM";
                    int rescek = MsEmpDA.cekEPCMemberUp(FileUploadName, source, FileType, user);
                    if (rescek > 0)
                    {
                        dMsgUpEPC.InnerText = "Error : Data yang Di Upload Tidak Lengkap";
                        dMsgUpEPC.Attributes["class"] = "error";
                        dMsgUpEPC.Visible = true;
                    }
                    else
                    {
                        string res = MsEmpDA.upMSEPC(FileUploadName, source, FileType, user);
                        dMsgUpEPC.InnerText = res;
                        dMsgUpEPC.Attributes["class"] = "success";
                        dMsgUpEPC.Visible = true;
                    }
                    modalUpEPC.Show();
                }
            }
        }

        protected void btnFupEPCClose_Click(object sender, EventArgs e)
        {
            modalUpEPC.Hide();
        }
        #endregion
        protected void ClearVIPPopUp()
        {
            txtvipNo.Text = "";
            txtNamaVip.Text = "";
            txtJabatanVIP.Text = "";
            txtjoindtvip.Text = "";
            txtlimitsosVip.Text = "";
        }
        protected void ClearAddEPCPopUp()
        {
            tbPUNIK.Text = "";
            tbPUNama.Text = "";
            tbPUJabatan.Text = "";
            tbPUDateJoin.Text = "";
            tbPULimitSOS.Text = "";
            tbPULimitDelami.Text = "";
            cbPURelasi.Checked = false;
        }
        protected void ChkMRA_CheckedChanged(object sender, EventArgs e)
        {
            generateEPCNo();
            ModalAddEdit.Show();
        }
    }
}