using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Threading;

namespace ATMOS_SROM.Master
{
    public partial class Acara : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                bindGrid();
                bindGroup();
            }
        }

        protected void bindGrid()
        {
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            string where = tbSearch.Text == "" ? " where STATUS_ACARA = 1" : string.Format(" where STATUS_ACARA = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);

            //listAcara = acaraDA.getAcara(where);
            listAcara = acaraDA.getvw_acaraGrid(where);
            gvMain.DataSource = listAcara.OrderByDescending(item => item.ID).ToList<MS_ACARA>();//listAcara;
            gvMain.DataBind();
        }

        protected void bindViewItem()
        {
            List<MS_ITEM_ACARA> listItemAcara = new List<MS_ITEM_ACARA>();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            string where = tbVISearch.Text == "" ? string.Format(" where ID_ACARA = {0} and STATUS_ACARA = 1", hdnIDVI.Value) :
                string.Format(" where ID_ACARA = {0} and STATUS_ACARA = 1 and {1} like '%{2}%'", hdnIDVI.Value, ddlVISearch.SelectedValue, tbVISearch.Text);

            listItemAcara = acaraDA.getItemAcara(where);
            gvVI.DataSource = listItemAcara;
            gvVI.DataBind();

            ModalViewItem.Show();

            //ModalPopupExtender1.Show();
            //ModalUpload.Show();
        }

        protected void bindGroup()
        {
            List<MS_ACARA_STATUS> listGroup = new List<MS_ACARA_STATUS>();
            MS_ACARA_STATUS acaraStatus = new MS_ACARA_STATUS();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            string where = " where STATUS = 1";
            acaraStatus.ACARA_STATUS = "--Pilih Group--";
            acaraStatus.ID = 0;
            acaraStatus.DESC_DISC = "";

            listGroup = acaraDA.getAcaraStatus(where);
            listGroup.Insert(0, acaraStatus);

            ddlPUGroup.DataSource = listGroup;
            ddlPUGroup.DataBind();

            ddlPUNamaGroup.DataSource = listGroup;
            ddlPUNamaGroup.DataBind();
        }

        protected void bindShowroom()
        {
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> showroomList = new List<MS_SHOWROOM>();
            //showroomList = showDA.getShowRoom(string.Format(" where STATUS_SHOWROOM in ('FSS', 'SIS') and STATUS = 'OPEN' and {0} like '%{1}%' order by KODE ", ddlVSSearch.SelectedValue, tbVSSearch.Text));
            showroomList = showDA.getShowRoom(string.Format(" where STATUS_SHOWROOM in ('FSS', 'SIS') and STATUS = 'OPEN' and {0} like '%{1}%' order by BRAND ,STATUS_SHOWROOM ", ddlVSSearch.SelectedValue, tbVSSearch.Text));
            gvVS.DataSource = showroomList;
            gvVS.DataBind();
            ModalViewShowroom.Show();
        }
        protected void bindShowroomAddBerjalan()
        {
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> showroomList = new List<MS_SHOWROOM>();
            //showroomList = showDA.getShowRoom(string.Format(" where STATUS_SHOWROOM in ('FSS', 'SIS') and STATUS = 'OPEN' and {0} like '%{1}%' order by KODE ", ddlVSSearch.SelectedValue, tbVSSearch.Text));
            showroomList = showDA.getShowRoom(string.Format(" where STATUS_SHOWROOM in ('FSS', 'SIS') and STATUS = 'OPEN' order by BRAND ,STATUS_SHOWROOM"));
            GridView2.DataSource = showroomList;
            GridView2.DataBind();
            ModalPopupExtender2.Show();
        }
        protected void bindAcaraShr()
        {
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            List<MS_ACARA_SHOWROOM> listacaraSHR = new List<MS_ACARA_SHOWROOM>();
            string AcaraShrKode = "";
            string IDAcaraShr = "";
            string AcaraShr = "";
            listacaraSHR = acaraDA.GetListMsAcaraShowroom(" Where ID_ACARA = " + tbVIIdAcara.Text);
            foreach (MS_ACARA_SHOWROOM item in listacaraSHR)
            {
                if (AcaraShrKode == "")
                {
                    AcaraShrKode = item.KODE;
                    IDAcaraShr = Convert.ToString(item.ID_SHOWROOM);
                }
                else
                {
                    AcaraShrKode = AcaraShrKode + ", " + item.KODE;
                    IDAcaraShr = IDAcaraShr + ", " + Convert.ToString(item.ID_SHOWROOM);

                }
            }
            txtShrBerjalan.Text = AcaraShrKode;
            txtIDShrBerjalan.Text = IDAcaraShr;
        }
        protected void btnUpload_ClickNew(object sender, EventArgs e)
        {
            //bool itemAcara = cbPUAllItem.Checked;
            bool itemAcara = rblPUAllIem.SelectedValue == "ALL" ? true : false;

            if (itemAcara)//Acara yang di discount untuk semua item 
            {

            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            String resID = InsertAcara();

            if (resID != "")
            {
                bindGrid();
            }
        }
        protected string InsertAcara()
        {
            string IDAcaraRes = "";
            if (ddlPUGroup.SelectedItem.Value != "" && tbPUShowroom.Text != "")
            {
                string warn = "";
                bool cek = true;
                //if (ddlPUGroup.SelectedItem.Text == "AS024" && (tbPUDisc.Text != "100" || rblPUDisc.SelectedValue != "D"))
                if (ddlPUGroup.SelectedItem.Text == "AS024")
                {
                    //if (!(tbPUDisc.Text == "100" || tbPUDisc.Text == "100,00") || rblPUDisc.SelectedValue != "D")
                    if (!(tbPUDisc.Text == "100" || tbPUDisc.Text == "100.00") || rblPUDisc.SelectedValue != "D")
                    {
                        warn = "Grup Acara AS024 (GWP) harus di isi dengan jenis acara Diskon dan Diskon 100%";
                        cek = false;
                    }
                }
                else if (ddlPUGroup.SelectedItem.Text == "AS025" && rblPUDisc.SelectedValue != "D")
                {
                    warn = "Grup Acara AS025 (PWP Get Discount) harus di isi dengan jenis acara Diskon";
                    cek = false;
                }
                else if (ddlPUGroup.SelectedItem.Text == "AS026" && rblPUDisc.SelectedValue != "P")
                {
                    warn = "Grup Acara AS026 (PWP Get Special Price) harus di isi dengan jenis acara Special Price";
                    cek = false;
                }
                else
                {
                    cek = true;
                }
                if (cek)
                {
                    List<MS_ACARA> listAcara = new List<MS_ACARA>();
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    LOGIN_DA loginDA = new LOGIN_DA();
                    MS_PARAMETER param = new MS_PARAMETER();
                    param = loginDA.getParam(" where NAME = 'Acara'");
                    string acaraValue = "ACR-" + param.VALUE.PadLeft(3, '0');

                    //listAcara = acaraDA.getAcara(string.Format(" where STATUS_ACARA = 1 and ACARA_VALUE = '{0}'", tbPUAcaraValue.Text));
                    listAcara = acaraDA.getAcara(string.Format(" where STATUS_ACARA = 1 and ACARA_VALUE = '{0}'", acaraValue));

                    if (listAcara.Count == 0)
                    {
                        //Insert Header Acara
                        MS_ACARA acara = new MS_ACARA();

                        DateTime startDate = SqlDateTime.MinValue.Value;
                        DateTime endDate = SqlDateTime.MaxValue.Value;
                        string sdate = tbPUStartAcara.Text.ToString();
                        string edate = tbPUEndAcara.Text.ToString();

                        if (!string.IsNullOrEmpty(sdate))
                        {
                            DateTime.TryParseExact(sdate, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            DateTime.TryParseExact(edate, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                        }

                        acara.NAMA_ACARA = tbPUNamaAcara.Text;
                        acara.ACARA_DESC = tbPUDesc.Text;
                        acara.ACARA_VALUE = acaraValue;
                        acara.START_DATE = startDate;
                        acara.END_DATE = endDate;
                        acara.CREATED_BY = Session["UName"].ToString();
                        //acara.ARTICLE = cbPUAllItem.Checked ? "ALL" : "SOME";
                        //acara.ARTICLE = rblPUAllIem.SelectedValue == "ALL" ? "ALL" : "SOME";
                        acara.ARTICLE = rblPUAllIem.SelectedValue == "N" ? "ALL" : "SOME";

                        acara.ITEM_ACARA = rblPUAllIem.SelectedValue == "N" ? "N" : "S";
                        string idAcara = acaraDA.insertMsAcara(acara);

                        if (!idAcara.Contains("ERROR"))
                        {
                            //Insert Disc Status
                            MS_ACARA_DISC acaraDisc = new MS_ACARA_DISC();
                            if (rblPUDisc.SelectedValue == "D" && tbPUDisc.Text.Contains(";"))
                            {
                                List<string> listdisc = tbPUDisc.Text.Split(';').ToList<string>();
                                int nourut = 1;
                                foreach (string item in listdisc)
                                {
                                    acaraDisc.ID_ACARA = Convert.ToInt64(idAcara);
                                    acaraDisc.ID_ACARA_STATUS = Convert.ToInt64(ddlPUGroup.SelectedValue);
                                    acaraDisc.NO_URUT = nourut;
                                    acaraDisc.ACARA_VALUE = acaraValue;
                                    acaraDisc.ACARA_STATUS = ddlPUGroup.SelectedItem.Text;
                                    //acaraDisc.DISC = rblPUDisc.SelectedValue == "D" ? Convert.ToInt32(listdisc[listdisc.IndexOf(item)]) : (int?)null;
                                    acaraDisc.DISC = rblPUDisc.SelectedValue == "D" ? Convert.ToDecimal(listdisc[listdisc.IndexOf(item)]) : (Decimal?)null;
                                    acaraDisc.SPCL_PRICE = rblPUDisc.SelectedValue == "P" ? Convert.ToDecimal(tbPUDisc.Text) : (Decimal?)null;
                                    acaraDisc.CREATED_BY = Session["UName"].ToString();
                                    acaraDisc.MIN_PURCHASE = Convert.ToDecimal(tbMinPurch.Text);
                                    acaraDA.insertMsAcaraDisc(acaraDisc);
                                    nourut = nourut + 1;
                                }
                            }
                            else
                            {
                                acaraDisc.ID_ACARA = Convert.ToInt64(idAcara);
                                acaraDisc.ID_ACARA_STATUS = Convert.ToInt64(ddlPUGroup.SelectedValue);
                                acaraDisc.NO_URUT = 1;
                                acaraDisc.ACARA_VALUE = acaraValue;
                                acaraDisc.ACARA_STATUS = ddlPUGroup.SelectedItem.Text;
                                //acaraDisc.DISC = rblPUDisc.SelectedValue == "D" ? Convert.ToInt32(tbPUDisc.Text) : (int?)null;
                                acaraDisc.DISC = rblPUDisc.SelectedValue == "D" ? Convert.ToDecimal(tbPUDisc.Text) : (Decimal?)null;
                                acaraDisc.SPCL_PRICE = rblPUDisc.SelectedValue == "P" ? Convert.ToDecimal(tbPUDisc.Text) : (Decimal?)null;
                                acaraDisc.CREATED_BY = Session["UName"].ToString();
                                acaraDisc.MIN_PURCHASE = Convert.ToDecimal(tbMinPurch.Text);
                                acaraDA.insertMsAcaraDisc(acaraDisc);
                            }
                            //Insert Acara Showroom
                            List<string> listKode = tbPUShowroom.Text.Split(',').ToList<string>();
                            List<string> listIdKode = tbPUIDShow.Text.Split(',').ToList<string>();
                            List<string> listShow = tbPUShow.Text.Split(',').ToList<string>();

                            foreach (string item in listIdKode)
                            {
                                MS_ACARA_SHOWROOM acaraShow = new MS_ACARA_SHOWROOM();
                                acaraShow.ID_ACARA = Convert.ToInt64(idAcara);
                                acaraShow.ID_SHOWROOM = Convert.ToInt64(item);
                                acaraShow.ACARA_VALUE = acaraValue;
                                //acaraShow.KODE = listKode[listIdKode.IndexOf(item)];
                                //acaraShow.SHOWROOM = listShow[listIdKode.IndexOf(item)];
                                acaraShow.KODE = listKode[listIdKode.IndexOf(item)].Trim();
                                acaraShow.SHOWROOM = listShow[listIdKode.IndexOf(item)].Trim();
                                acaraShow.CREATED_BY = Session["UName"].ToString();
                                acaraDA.insertMsAcaraShow(acaraShow);
                            }
                            bool ret = true;

                            if (ret)
                            {
                                param.NAME = "Acara";
                                param.VALUE = Convert.ToString(Convert.ToInt32(param.VALUE) + 1);
                                loginDA.updateValueParam(param);
                                IDAcaraRes = idAcara;
                                HfKodeAcara.Value = acaraValue;
                            }
                        }
                        else
                        {
                            DivUploadMessage.InnerText = idAcara;
                            DivUploadMessage.Visible = true;
                            ModalUpload.Show();
                        }
                    }
                    else
                    {
                        DivUploadMessage.InnerText = "Kode Acara Sudah terdaftar. Silakan ubah melalui View Item atau Hapus Kode yang lama terlebih dahulu atau Buat Kode Baru!";
                        DivUploadMessage.Attributes["class"] = "warning";
                        DivUploadMessage.Visible = true;
                        ModalUpload.Show();
                    }
                }
                else
                {
                    DivUploadMessage.InnerText = warn;
                    DivUploadMessage.Attributes["class"] = "warning";
                    DivUploadMessage.Visible = true;
                    ModalUpload.Show();
                }
            }
            else
            {
                DivUploadMessage.InnerText = ddlPUGroup.SelectedIndex == 0 ? "Pilih Group Acara" : "Pilih Showroom";
                DivUploadMessage.Attributes["class"] = "warning";
                DivUploadMessage.Visible = true;
                ModalUpload.Show();
            }
            return IDAcaraRes;
        }

        protected void btnVIUploadClick(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyMMddHHmms") + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    //if (ddlPUGroup.SelectedIndex > 0 && tbPUShowroom.Text != "")
                    //{
                    //List<MS_ACARA> listAcara = new List<MS_ACARA>();
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    //listAcara = acaraDA.getAcara(string.Format(" where STATUS_ACARA = 1 and ACARA_VALUE = '{0}'", tbVIKodeAcara.Text));

                    //if (listAcara.Count > 0)
                    if (!(tbVIIdAcara.Text.Trim() == ""))
                    {
                        string idAcara = tbVIIdAcara.Text.Trim();
                        FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                        source = Server.MapPath("../Upload\\" + FileUploadName);

                        string dir = Path.GetDirectoryName(source);
                        string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                        bool ret = true;
                        if (!cbVIStatusArticle.Checked)
                        //if (!(cbbPUAllItem.SelectedValue == "ALL"))
                        {
                            acaraDA.clearTempItemAcara(Convert.ToInt64(idAcara), tbVIKodeAcara.Text);
                            ret = SaveFileToDatabase(source, Convert.ToInt64(idAcara), tbVIKodeAcara.Text);
                            //ret = upload(dir, FileUploadName, source, idAcara);
                            File.Delete(source);
                        }

                        if (ret)
                        {
                            //bindGrid();
                            bindViewItem();
                        }
                    }
                    else
                    {
                        DivUploadMessage.InnerText = "Kode Acara Sudah Tidak Aktif. Silakan Aktifkan kembali terlebih dahulu!";
                        DivUploadMessage.Attributes["class"] = "warning";
                        DivUploadMessage.Visible = true;
                        ModalUpload.Show();
                    }
                    //}
                    //else
                    //{
                    //    DivUploadMessage.InnerText = ddlPUGroup.SelectedIndex == 0 ? "Pilih Group Acara" : "Pilih Showroom";
                    //    DivUploadMessage.Attributes["class"] = "warning";
                    //    DivUploadMessage.Visible = true;
                    //    ModalUpload.Show();
                    //}
                }
                else
                {
                    DivUploadMessage.InnerText = "File Harus Bertipe xls ataus xlsx.";
                    DivUploadMessage.Attributes["class"] = "warning";
                    DivUploadMessage.Visible = true;
                    ModalUpload.Show();
                }
            }
            else
            {
                DivUploadMessage.InnerText = "Pilih File Yang Akan Diupload.";
                DivUploadMessage.Attributes["class"] = "warning";
                DivUploadMessage.Visible = true;
                ModalUpload.Show();
            }
            //ModalViewItem.Hide();
        }

        protected void btnUploadPromo_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_Promo" + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    List<MS_ACARA> listAcara = new List<MS_ACARA>();
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    listAcara = acaraDA.getAcara(string.Format(" where STATUS_ACARA = 1 and ACARA_VALUE = '{0}'", tbPUAcaraValue.Text));

                    //Insert Header Acara
                    MS_ACARA acara = new MS_ACARA();

                    DateTime startDate = SqlDateTime.MinValue.Value;
                    DateTime endDate = SqlDateTime.MaxValue.Value;
                    string sdate = tbPUStartAcara.Text.ToString();
                    string edate = tbPUEndAcara.Text.ToString();

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        DateTime.TryParseExact(sdate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        DateTime.TryParseExact(edate, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    }

                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);

                    #region UploadPromo
                    bool ret = true;
                    int data = 0;
                    int insert = 0;
                    int doubel = 0;

                    MS_ITEM_ACARA itemAcara = new MS_ITEM_ACARA();
                    MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

                    try
                    {
                        List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
                        //msKdbrgList = kdbrgDA.getMsKdbrg("");
                        msKdbrgList = kdbrgDA.getMsKdbrgArticle("");

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
                        string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                        cnn.Close();
                        OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                        oAdapter.Fill(dsOle, "Sheet1");
                        foreach (DataRow item in dsOle.Tables[0].Rows)
                        {
                            string brand = item[1].ToString();
                            itemAcara.ID_ACARA = Convert.ToInt64(0);
                            itemAcara.VALUE_ACARA = tbPUAcaraValue.Text;
                            itemAcara.ITEM_CODE = item[2].ToString();
                            itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.ITEM_CODE == itemAcara.ITEM_CODE && items.FBRAND.Contains(brand)).First().ID;
                            acaraDA.insertMsItemAcara(itemAcara);

                            data++;
                        }
                        //insertHarga(idUpload.Remove(0, 1));
                        DivMessage.InnerText = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
                    }
                    catch (Exception ex)
                    {
                        ret = false;

                        DivMessage.InnerText = "Error : " + ex.Message;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                    func.addLog("Article > Upload > " + DivMessage.InnerText, Session["UName"].ToString());

                    #endregion

                    File.Delete(source);

                    if (ret)
                    {
                        bindGrid();
                    }
                }
                else
                {
                    DivUploadMessage.InnerText = "File Harus Bertipe xls ataus xlsx.";
                    DivUploadMessage.Attributes["class"] = "warning";
                    DivUploadMessage.Visible = true;
                    ModalUpload.Show();
                }
            }
            else
            {
                DivUploadMessage.InnerText = "Pilih File Yang Akan Diupload.";
                DivUploadMessage.Attributes["class"] = "warning";
                DivUploadMessage.Visible = true;
                ModalUpload.Show();
            }
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnVISearchClick(object sender, EventArgs e)
        {
            bindViewItem();
        }

        protected void btnNewAcaraClick(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            clearform();
            lblJdlPopUp.Text = "Create New Event";

            ModalUpload.Show();
            btnUpload.Visible = true;
            btnDuplicate.Visible = false;
        }

        protected void btnPUShowroomClick(object sender, EventArgs e)
        {
            bindShowroom();
            #region "Cek SelectedShowroom"
            MS_ACARA_DA AcaraDA = new MS_ACARA_DA();
            DataSet DsShowroomAcara = new DataSet();
            string KdShrAcara = tbPUShowroom.Text;
            string IdShrAcara = tbPUIDShow.Text;
            string NamaShrAcara = tbPUShow.Text;
            if (!(KdShrAcara == "" || IdShrAcara == "" || NamaShrAcara == ""))
            {
                DsShowroomAcara = AcaraDA.GetDataSeMsAcaraShowroom(String.Format(" WHERE ID_SHOWROOM IN ({0})", IdShrAcara));
                foreach (DataRow row in DsShowroomAcara.Tables[0].Rows)
                {
                    foreach (GridViewRow Gvrow in gvVS.Rows)
                    {
                        //if using TemplateField columns then you may need to use FindControl method
                        CheckBox cb = (CheckBox)Gvrow.FindControl("cbShowroom");
                        string KdShrGridview = Gvrow.Cells[3].Text; //Where Cells is the column. Just changed the index of cells
                        string KdShrDataSet = row["KODE"].ToString();
                        if (KdShrGridview == KdShrDataSet)
                        {
                            cb.Checked = true;
                            break;

                        }
                    }
                }
            }
            #endregion

        }

        protected void bVSCloseShowClick(object sender, EventArgs e)
        {
            ModalUpload.Show();
        }

        protected void btnVSSaveClick(object sender, EventArgs e)
        {
            string kodeShow = "";
            string idShow = "";
            string showroom = "";
            foreach (GridViewRow item in gvVS.Rows)
            {
                CheckBox cbShowroom = (CheckBox)item.FindControl("cbShowroom");
                if (cbShowroom.Checked)
                {
                    kodeShow = kodeShow + item.Cells[3].Text.ToString() + ",";
                    showroom = showroom + item.Cells[4].Text.ToString() + ",";
                    idShow = idShow + gvVS.DataKeys[item.RowIndex]["ID"].ToString() + ",";
                }
            }
            tbPUShowroom.Text = kodeShow.Remove(kodeShow.Count() - 1, 1);
            tbPUShow.Text = showroom.Remove(showroom.Count() - 1, 1);
            tbPUIDShow.Text = idShow.Remove(idShow.Count() - 1, 1);
            ModalUpload.Show();
        }

        protected void btnVSCheckAllClick(object sender, EventArgs e)
        {
            int total = gvVS.Rows.Count;
            for (int i = 0; i < total; i++)
            {
                CheckBox cbShowroom = (CheckBox)gvVS.Rows[i].FindControl("cbShowroom");
                cbShowroom.Checked = true;
            }
            ModalViewShowroom.Show();
        }

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        //string StatusAcara = gvMain.Rows[rowIndex].Cells[7].Text;
                        string DiscBertingkat = "";
                        int jmldisc = acaraDA.CountAcaraDisc(" WHERE ID_ACARA = " + gvMain.Rows[rowIndex].Cells[3].Text);
                        cbVIStatusArticle.Checked = gvMain.Rows[rowIndex].Cells[14].Text.ToLower().Contains("all");
                        tbVIIdAcara.Text = gvMain.Rows[rowIndex].Cells[3].Text;
                        tbVIKodeAcara.Text = gvMain.Rows[rowIndex].Cells[6].Text;
                        List<MS_ACARA_DISC> listDisc = new List<MS_ACARA_DISC>();
                        listDisc = acaraDA.GetListMsAcaraDisc(" WHERE ID_ACARA = " + gvMain.Rows[rowIndex].Cells[3].Text);
                        string grpAcara = gvMain.Rows[rowIndex].Cells[7].Text;
                        if (grpAcara == "AS024" || grpAcara == "AS025" || grpAcara == "AS026")
                        {
                            trhide.Visible = true;
                            divItemGWP.Visible = true;
                            bindViewGWP();
                        }
                        else
                        {
                            trhide.Visible = false;
                            divItemGWP.Visible = false;
                        }
                        if (jmldisc == 1)
                        {
                            if (listDisc.FirstOrDefault().DISC != 0 && listDisc.FirstOrDefault().SPCL_PRICE == 0)
                            {
                                tbVIDisc.Text = gvMain.Rows[rowIndex].Cells[8].Text.Contains("nbsp") ? gvMain.Rows[rowIndex].Cells[18].Text : gvMain.Rows[rowIndex].Cells[8].Text;
                            }
                            else if (listDisc.FirstOrDefault().DISC == 0 && listDisc.FirstOrDefault().SPCL_PRICE != 0)
                            {
                                tbVIDisc.Text = listDisc.FirstOrDefault().SPCL_PRICE.ToString();
                            }
                            else if (listDisc.FirstOrDefault().DISC == 0 && listDisc.FirstOrDefault().SPCL_PRICE == 0)
                            {
                                tbVIDisc.Text = "0";
                            }
                        }
                        else if (jmldisc > 1)
                        {

                            foreach (MS_ACARA_DISC item in listDisc)
                            {
                                if (DiscBertingkat == "")
                                {
                                    DiscBertingkat = Convert.ToString(item.DISC);
                                }
                                else
                                {
                                    DiscBertingkat = DiscBertingkat + ";" + Convert.ToString(item.DISC);
                                }
                            }
                            tbVIDisc.Text = DiscBertingkat;
                        }
                        lblacaraval.Text = listDisc.FirstOrDefault().ACARA_VALUE;
                        bindAcaraShr();
                        btnUpload.Enabled = !cbVIStatusArticle.Checked;
                        //hdnIDVI.Value = id;
                        hdnIDVI.Value = gvMain.Rows[rowIndex].Cells[3].Text.Trim();
                        bindViewItem();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_ACARA acara = new MS_ACARA();
                        acara.ID = Convert.ToInt64(gvMain.Rows[rowIndex].Cells[3].Text);

                        acaraDA.deleteMsAcara(acara);

                        bindGrid();
                    }
                    else if (e.CommandName.ToLower() == "duplicate")
                    {
                        DataSet DsAcrDuplicate = new DataSet();
                        String KdAcaraStatus = "";
                        HfIdToDuplicate.Value = gvMain.Rows[rowIndex].Cells[3].Text;
                        DsAcrDuplicate = acaraDA.GetDataAcaraDuplicate(" Where ID = " + HfIdToDuplicate.Value.ToString());
                        KdAcaraStatus = DsAcrDuplicate.Tables[0].Rows[0]["ACARA_STATUS"].ToString();
                        tbPUShowroom.Text = DsAcrDuplicate.Tables[0].Rows[0]["KODE_SHR_ACARA"].ToString();
                        tbPUIDShow.Text = DsAcrDuplicate.Tables[0].Rows[0]["ID_SHR_ACARA"].ToString();
                        tbPUShow.Text = DsAcrDuplicate.Tables[0].Rows[0]["SHOWROOM_ACARA"].ToString();
                        tbPUGroupDesc.Text = DsAcrDuplicate.Tables[0].Rows[0]["DESC_DISC"].ToString();
                        ddlPUGroup.ClearSelection();
                        if (ddlPUGroup.Items.FindByValue(DsAcrDuplicate.Tables[0].Rows[0]["ID_ACARA_STATUS"].ToString()) != null)
                        {
                            ddlPUGroup.Items.FindByValue(DsAcrDuplicate.Tables[0].Rows[0]["ID_ACARA_STATUS"].ToString()).Selected = true;
                        }
                        tbPUNamaAcara.Text = DsAcrDuplicate.Tables[0].Rows[0]["NAMA_ACARA"].ToString();
                        tbPUDesc.Text = DsAcrDuplicate.Tables[0].Rows[0]["ACARA_DESC"].ToString();
                        if (!(DsAcrDuplicate.Tables[0].Rows[0]["ACR_DISC"].ToString() == "0.00"))
                        {
                            //tbPUDisc.Text = DsAcrDuplicate.Tables[0].Rows[0]["ACR_DISC"].ToString().Replace(".", ",");
                            tbPUDisc.Text = DsAcrDuplicate.Tables[0].Rows[0]["ACR_DISC"].ToString();
                            rblPUDisc.SelectedValue = "D";

                        }
                        else if (!(DsAcrDuplicate.Tables[0].Rows[0]["ACR_SPCL_PRICE"].ToString() == "0.00"))
                        {
                            //tbPUDisc.Text = DsAcrDuplicate.Tables[0].Rows[0]["ACR_SPCL_PRICE"].ToString().Replace(".", ",");
                            tbPUDisc.Text = DsAcrDuplicate.Tables[0].Rows[0]["ACR_SPCL_PRICE"].ToString();
                            rblPUDisc.SelectedValue = "P";

                        }
                        if (KdAcaraStatus == "AS023" || KdAcaraStatus == "AS024" || KdAcaraStatus == "AS025" || KdAcaraStatus == "AS026")
                        {
                            tbMinPurch.Enabled = true;
                        }
                        else
                        {
                            tbMinPurch.Enabled = false;
                        }
                        if (DsAcrDuplicate.Tables[0].Rows[0]["ARTICLE"].ToString() == "ALL")
                        {
                            rblPUAllIem.SelectedValue = "N";
                        }
                        else
                        {
                            rblPUAllIem.SelectedValue = "S";
                        }
                        DateTime DtStart = Convert.ToDateTime(DsAcrDuplicate.Tables[0].Rows[0]["START_DATE"].ToString());
                        DateTime EndStart = Convert.ToDateTime(DsAcrDuplicate.Tables[0].Rows[0]["END_DATE"].ToString());

                        tbPUStartAcara.Text = DtStart.ToString("dd-MM-yyyy");
                        tbPUEndAcara.Text = EndStart.ToString("dd-MM-yyyy");

                        lblJdlPopUp.Text = "Duplicate Acara";
                        btnUpload.Visible = false;
                        btnDuplicate.Visible = true;
                        ModalUpload.Show();
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

        protected bool upload(string dir, string fileName, string source, string idAcara)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;

            MS_ITEM_ACARA itemAcara = new MS_ITEM_ACARA();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            try
            {
                List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
                //msKdbrgList = kdbrgDA.getMsKdbrg("");
                msKdbrgList = kdbrgDA.getMsKdbrgArticle("");

                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                //connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\appli\\new\\srom;Extended Properties='dBASE IV;Exclusive=false;';";
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                //cnn.Open();
                //System.Data.DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //if (dbSchema == null || dbSchema.Rows.Count < 1)
                //{
                //    throw new Exception("Error: Could not determine the name of the first worksheet.");
                //}
                string firstSheetName = "";
                //try
                //{
                //    firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                //}
                //catch
                //{
                //    firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                //}

                firstSheetName = "Item_Acara$";
                //cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                bool cek = true;
                List<MS_ITEM_GWP_PWP> lItemAcaraGWP = new List<MS_ITEM_GWP_PWP>();
                lItemAcaraGWP = acaraDA.getItemAcaraGWP(" Where ID_ACARA = " + idAcara);
                if (lItemAcaraGWP.Count > 0)
                {
                    cek = cek && cekItemAcara(dsOle.Tables[0], lItemAcaraGWP);
                    if (cek)
                    {
                        foreach (DataRow item in dsOle.Tables[0].Rows)
                        {
                            string brand = item[2].ToString();
                            itemAcara.ID_ACARA = Convert.ToInt64(idAcara);
                            itemAcara.VALUE_ACARA = tbVIKodeAcara.Text;
                            itemAcara.ITEM_CODE = item[4].ToString().Trim();
                            itemAcara.BARCODE = item[3].ToString().Trim();
                            //itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString() && items.FBRAND.Contains(brand)).First().ID;
                            itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString().Trim()).First().ID;
                            itemAcara.CREATED_BY = Session["UName"].ToString();
                            acaraDA.insertMsItemAcara(itemAcara);

                            data++;
                        }
                    }
                    //insertHarga(idUpload.Remove(0, 1));
                    dmsgUpdItem.InnerText = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
                }
                else
                {
                    foreach (DataRow item in dsOle.Tables[0].Rows)
                    {
                        string brand = item[2].ToString();
                        itemAcara.ID_ACARA = Convert.ToInt64(idAcara);
                        itemAcara.VALUE_ACARA = tbVIKodeAcara.Text;
                        itemAcara.ITEM_CODE = item[4].ToString().Trim();
                        itemAcara.BARCODE = item[3].ToString().Trim();
                        //itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString() && items.FBRAND.Contains(brand)).First().ID;
                        itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString().Trim()).First().ID;
                        itemAcara.CREATED_BY = Session["UName"].ToString();
                        acaraDA.insertMsItemAcara(itemAcara);

                        data++;
                    }
                    //insertHarga(idUpload.Remove(0, 1));

                    dmsgUpdItem.InnerText = "Upload Berhasil! | Data : " + data.ToString() + " | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
                }
            }
            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Article > Upload > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
        }

        protected void clearform()
        {
            tbPUAcaraValue.Text = "";
            tbPUNamaAcara.Text = "";
            tbPUDesc.Text = "";
            tbPUStartAcara.Text = "";
            tbPUEndAcara.Text = "";
            ddlPUGroup.ClearSelection();
            tbPUGroupDesc.Text = "";
            tbPUShowroom.Text = "";
            tbPUIDShow.Text = "";
            tbPUShow.Text = "";
            tbPUDisc.Text = "";
            DivUploadMessage.Visible = false;
        }

        #region "Tambah Showroom di Acara Berjalan"
        protected void btnDiscShowroom_Click(object sender, EventArgs e)
        {
            bindShowroomAddBerjalan();
            #region "Cek SelectedShowroom"
            MS_ACARA_DA AcaraDA = new MS_ACARA_DA();
            DataSet DsShowroomAcara = new DataSet();
            string IdShrAcara = txtIDShrBerjalan.Text;
            if (!(IdShrAcara == ""))
            {
                DsShowroomAcara = AcaraDA.GetDataSeMsAcaraShowroom(String.Format(" WHERE ID_SHOWROOM IN ({0})", IdShrAcara));
                foreach (DataRow row in DsShowroomAcara.Tables[0].Rows)
                {
                    foreach (GridViewRow Gvrow in GridView2.Rows)
                    {
                        //if using TemplateField columns then you may need to use FindControl method
                        CheckBox cb = (CheckBox)Gvrow.FindControl("cbShowroom");
                        string KdShrGridview = Gvrow.Cells[3].Text; //Where Cells is the column. Just changed the index of cells
                        string KdShrDataSet = row["KODE"].ToString();
                        if (KdShrGridview == KdShrDataSet)
                        {
                            cb.Checked = true;
                            cb.Enabled = false;
                            break;

                        }
                    }
                }
            }
            #endregion

        }
        #endregion

        protected void Button9_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
            ModalViewItem.Show();
        }

        protected void btnShowroomAddSave_Click(object sender, EventArgs e)
        {
            string kodeShow = "";
            string idShow = "";
            string showroom = "";
            //string AcraVal = 
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            DataSet DsShowroomAcara = new DataSet();
            foreach (GridViewRow item in GridView2.Rows)
            {
                CheckBox cbShowroom = (CheckBox)item.FindControl("cbShowroom");
                if (cbShowroom.Checked)
                {
                    DsShowroomAcara = acaraDA.GetDataSeMsAcaraShowroom(String.Format(" WHERE ID_SHOWROOM IN ({0}) AND ID_ACARA = {1}", Convert.ToInt64(GridView2.DataKeys[item.RowIndex]["ID"].ToString()), tbVIIdAcara.Text));
                    if (DsShowroomAcara.Tables[0].Rows.Count == 0)
                    {
                        MS_ACARA_SHOWROOM itemshr = new MS_ACARA_SHOWROOM();
                        itemshr.ID_SHOWROOM = Convert.ToInt64(GridView2.DataKeys[item.RowIndex]["ID"].ToString());
                        itemshr.KODE = item.Cells[3].Text.ToString();
                        itemshr.SHOWROOM = item.Cells[4].Text.ToString();
                        itemshr.ACARA_VALUE = lblacaraval.Text;
                        itemshr.ID_ACARA = Convert.ToInt64(tbVIIdAcara.Text);
                        itemshr.CREATED_BY = Session["UName"].ToString();
                        acaraDA.insertMsAcaraShow(itemshr);
                    }
                }
            }
            bindAcaraShr();
            ModalViewItem.Show();
        }

        protected void ddlPUGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlPUGroup.SelectedItem.Text == "AS023")
            //{
            //    tbMinPurch.ReadOnly = false;
            //    rblPUDisc.SelectedValue = "P";
            //    rblPUDisc.Enabled = true;
            //}
            //else if (ddlPUGroup.SelectedItem.Text == "AS024")
            //{
            //    tbMinPurch.ReadOnly = false;
            //    rblPUDisc.SelectedValue = "D";
            //    tbPUDisc.Text = "0";
            //    tbPUDisc.Enabled = false;
            //    rblPUDisc.Enabled = false;
            //}
            //else
            //{
            //    tbMinPurch.ReadOnly = true;
            //    rblPUDisc.SelectedValue = "D";
            //    rblPUDisc.Enabled = true;
            //}
            ModalUpload.Show();
        }

        protected void gvVI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVI.PageIndex = e.NewPageIndex;
            bindViewItem();
            ModalViewItem.Show();
        }

        #region "GWP"

        protected void btnFileUploadGWP_Click(object sender, EventArgs e)
        {
            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FlUp2.PostedFile.ContentType.ToLower();//FileUpload2.PostedFile.ContentType.ToLower();
            ExcelFileName = FlUp2.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyMMddHHmms") + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    if (!(tbVIIdAcara.Text.Trim() == ""))
                    {
                        string idAcara = tbVIIdAcara.Text.Trim();
                        FlUp2.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                        source = Server.MapPath("../Upload\\" + FileUploadName);

                        string dir = Path.GetDirectoryName(source);
                        string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                        bool ret = true;
                        if (!cbVIStatusArticle.Checked)
                        {
                            acaraDA.clearTempItemAcaraGWP(Convert.ToInt64(idAcara), tbVIKodeAcara.Text);
                            ret = SaveFileToDatabaseGWP(source, Convert.ToInt64(idAcara), tbVIKodeAcara.Text);

                            //ret = uploadGWP(dir, FileUploadName, source, idAcara);
                            File.Delete(source);
                        }

                        if (ret)
                        {
                            //bindGrid();
                            bindViewItem();
                            //dmsgUpdItem.Visible = false;
                        }
                    }
                    else
                    {
                        dmsgUpdItem.InnerText = "Kode Acara Sudah Tidak Aktif. Silakan Aktifkan kembali terlebih dahulu!";
                        dmsgUpdItem.Attributes["class"] = "warning";
                        dmsgUpdItem.Visible = true;
                        //ModalUpload.Show();
                    }
                }
                else
                {
                    dmsgUpdItem.InnerText = "File GWP Harus Bertipe xls ataus xlsx.";
                    dmsgUpdItem.Attributes["class"] = "warning";
                    dmsgUpdItem.Visible = true;
                    //ModalUpload.Show();
                }
            }
            else
            {
                dmsgUpdItem.InnerText = "Pilih File GWP Yang Akan Diupload.";
                dmsgUpdItem.Attributes["class"] = "warning";
                dmsgUpdItem.Visible = true;
                //ModalUpload.Show();
            }
            bindViewGWP();
            ModalViewItem.Show();
            //ModalViewItem.Hide();
        }
        protected bool uploadGWP(string dir, string fileName, string source, string idAcara)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;

            MS_ITEM_GWP_PWP itemAcara = new MS_ITEM_GWP_PWP();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            try
            {
                List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
                msKdbrgList = kdbrgDA.getMsKdbrgArticle("");
                bool cek = true;
                string connetionString = null;
                OleDbConnection cnn = new OleDbConnection();
                connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
                cnn = new OleDbConnection(connetionString);

                DataSet dsOle = new DataSet();
                string firstSheetName = "";
                firstSheetName = "Item_GWP_PWP$";
                //cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                //Region Cek 
                List<MS_ITEM_ACARA> lItemAcara = new List<MS_ITEM_ACARA>();
                lItemAcara = acaraDA.getItemAcara(" Where ID_ACARA = " + idAcara);
                cek = cek && cekGWP(dsOle.Tables[0], lItemAcara);
                if (cek)
                {
                    foreach (DataRow item in dsOle.Tables[0].Rows)
                    {
                        string brand = item[2].ToString();
                        itemAcara.ID_ACARA = Convert.ToInt64(idAcara);
                        itemAcara.VALUE_ACARA = tbVIKodeAcara.Text;
                        itemAcara.ITEM_CODE = item[4].ToString().Trim();
                        itemAcara.BARCODE = item[3].ToString().Trim();
                        //itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString() && items.FBRAND.Contains(brand)).First().ID;
                        itemAcara.ID_KDBRG = msKdbrgList.Where(items => items.BARCODE == item[3].ToString().Trim()).First().ID;
                        itemAcara.CREATED_BY = Session["UName"].ToString();
                        itemAcara.ITEM_PRICE_ACARA = 0;// Convert.ToDecimal(item[8].ToString().Trim());
                        acaraDA.insertMsItemAcaraGWP(itemAcara);
                        dmsgUpdItem.Visible = false;
                        data++;
                    }
                }
                dmsgUpdItem.InnerText = "Upload Berhasil! | Data : " + data.ToString();// +" | Insert Data : " + insert.ToString() + " | Update Data : " + doubel.ToString();
            }

            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Article GWP> Upload > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
        }
        protected bool cekGWP(DataTable dTab, List<MS_ITEM_ACARA> listitem)
        {
            bool check = true;
            if (check)
            {
                for (int i = 0; i < dTab.Rows.Count; i++)
                {
                    string barcode = dTab.Rows[i][3].ToString();
                    int count = listitem.Where(item => item.BARCODE == dTab.Rows[i][3].ToString()).Count();
                    if (count > 0)//(listitem.Where(item => item.BARCODE == dTab.Rows[i][3].ToString()).Count() > 0)
                    {
                        check = false;
                        dmsgUpdItem.InnerText = "Barcode Sudah di daftarkan sebagai Item Acara Tidak dapat didaftarkan sebagai Item GWP, harap di cek kembali di Line " + (i + 1);
                        dmsgUpdItem.Attributes["class"] = "warning";
                        dmsgUpdItem.Visible = true;

                        i = dTab.Rows.Count + 1;
                    }

                }
            }

            return check;
        }
        protected void Gv_GWP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_GWP.PageIndex = e.NewPageIndex;
            bindViewGWP();
            ModalViewItem.Show();
        }
        protected void bindViewGWP()
        {
            List<MS_ITEM_GWP_PWP> listItemgwp = new List<MS_ITEM_GWP_PWP>();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            string where = string.Format(" where ID_ACARA = {0} and STATUS_ACARA = 1", tbVIIdAcara.Text);

            listItemgwp = acaraDA.getItemAcaraGWP(where);
            Gv_GWP.DataSource = listItemgwp;
            Gv_GWP.DataBind();

            ModalViewItem.Show();

            //ModalPopupExtender1.Show();
            //ModalUpload.Show();
        }
        protected bool cekItemAcara(DataTable dTab, List<MS_ITEM_GWP_PWP> listitem)
        {
            bool check = true;
            if (check)
            {
                for (int i = 0; i < dTab.Rows.Count; i++)
                {
                    string barcode = dTab.Rows[i][3].ToString();
                    int count = listitem.Where(item => item.BARCODE == dTab.Rows[i][3].ToString()).Count();
                    if (count > 0)//(listitem.Where(item => item.BARCODE == dTab.Rows[i][3].ToString()).Count() > 0)
                    {
                        check = false;
                        dmsgUpdItem.InnerText = "Barcode Sudah di daftarkan sebagai Item GWP Tidak dapat didaftarkan sebagai Item Acara, harap di cek kembali di Line " + (i + 1);
                        dmsgUpdItem.Attributes["class"] = "warning";
                        dmsgUpdItem.Visible = true;

                        i = dTab.Rows.Count + 1;
                    }

                }
            }

            return check;
        }

        #endregion

        #region "Search Group Acara"
        protected void BindSrchGrp()
        {
            List<MS_ACARA_STATUS> listGroup = new List<MS_ACARA_STATUS>();
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();

            string where = " where STATUS = 1";
            if (tbSearchGrp.Text != "")
            {
                where = where + " And DESC_DISC like '%" + tbSearchGrp.Text + "%'";
            }
            listGroup = acaraDA.getAcaraStatus(where);

            gvGrp.DataSource = listGroup;
            gvGrp.DataBind();
        }
        protected void btnSrchGrpAcara_Click(object sender, EventArgs e)
        {
            BindSrchGrp();
            ModalSearchGroupAcara.Show();
        }

        protected void btnsearchGrp_Click(object sender, EventArgs e)
        {
            BindSrchGrp();
            ModalSearchGroupAcara.Show();
        }

        protected void btnsearcGrpClose_Click(object sender, EventArgs e)
        {

        }

        protected void gvGrp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    //string id = gvShowroom.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName == "SelectRow")
                    {
                        List<MS_ACARA_STATUS> listGroup = new List<MS_ACARA_STATUS>();
                        MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                        string kdacara = gvGrp.Rows[rowIndex].Cells[3].Text.ToString();
                        listGroup = acaraDA.getAcaraStatus(" Where ACARA_STATUS = '" + kdacara + "'");
                        ddlPUGroup.DataSource = listGroup;
                        ddlPUGroup.DataBind();
                        ddlPUGroup.Enabled = false;
                        //ddlPUGroup.SelectedIndex = 1;
                        tbPUGroupDesc.Text = listGroup.FirstOrDefault().DESC_DISC;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            ModalUpload.Show();
        }

        protected void gvGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGrp.PageIndex = e.NewPageIndex;
            BindSrchGrp();
            ModalSearchGroupAcara.Show();
        }
        #endregion

        #region "Insert Item ACara Bulk Insert"
        protected bool SaveFileToDatabase(string filePath, Int64 idacr, string acrvalue)
        {
            bool ret = true;
            try
            {
                MS_ACARA_DA AcrDa = new MS_ACARA_DA();
                string resbulk = AcrDa.BulkInsItemAcara(filePath);
                string res = "";
                if (!resbulk.Contains("ERROR"))
                {
                    res = AcrDa.insertMsItemAcaraBulk(Session["UName"].ToString(), idacr, acrvalue);
                    if (res.Contains("GWP"))
                    {
                        ret = false;
                        DivMessage.InnerText = "Error : " + res;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = res;
                        DivMessage.Attributes["class"] = "Success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    ret = false;
                    DivMessage.InnerText = "Error : " + resbulk;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                //lblinfo.Text = lblinfo.Text + " : " + res;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        protected bool SaveFileToDatabaseGWP(string filePath, Int64 idacr, string acrvalue)
        {
            bool ret = true;
            try
            {
                MS_ACARA_DA AcrDa = new MS_ACARA_DA();
                string resbulk = AcrDa.BulkInsItemAcaraGWP(filePath);
                string res = "";
                if (!resbulk.Contains("ERROR"))
                {
                    res = AcrDa.insertMsItemAcaraBulkGWP(Session["UName"].ToString(), idacr, acrvalue);
                    if (res.Contains("Acara"))
                    {
                        ret = false;
                        DivMessage.InnerText = "Error : " + res;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = res;
                        DivMessage.Attributes["class"] = "Success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    ret = false;
                    DivMessage.InnerText = "Error : " + resbulk;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
                //lblinfo.Text = lblinfo.Text + " : " + res;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        #endregion
        protected void gvVI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvVI.DataKeys[rowIndex]["ID"].ToString();
                    MS_ACARA_DA AcrDa = new MS_ACARA_DA();
                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        AcrDa.SoftdeleteMsItemAcara(Convert.ToInt64(id));
                        bindViewItem();
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

        protected void Gv_GWP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = Gv_GWP.DataKeys[rowIndex]["ID"].ToString();
                    MS_ACARA_DA AcrDa = new MS_ACARA_DA();
                    if (e.CommandName.ToLower() == "deleterow")
                    {
                        AcrDa.SoftdeleteMsItemGWPPWPAcara(Convert.ToInt64(id));
                        bindViewItem();
                        bindViewGWP();
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

        protected void btnDuplicate_Click(object sender, EventArgs e)
        {
            MS_ACARA_DA AcrDA = new MS_ACARA_DA();
            List<MS_ITEM_ACARA> listItemAcara = new List<MS_ITEM_ACARA>();
            List<MS_ITEM_GWP_PWP> listItemAcaraGWP = new List<MS_ITEM_GWP_PWP>();
            int CountItemAcara = 0;
            int CountItemAcaraGWP = 0;
            
            String resID = InsertAcara();
            String UserName = Session["UName"].ToString();
            if (resID != "")
            {
                listItemAcara = AcrDA.getItemAcara("WHERE ID_ACARA = " + HfIdToDuplicate.Value.ToString());
                listItemAcaraGWP = AcrDA.getItemAcaraGWP("WHERE ID_ACARA = " + HfIdToDuplicate.Value.ToString());
                CountItemAcara = listItemAcara.Count();
                CountItemAcaraGWP = listItemAcaraGWP.Count();
                if (CountItemAcara > 0)
                {
                    AcrDA.DuplicateMsItemAcara(Convert.ToInt64(HfIdToDuplicate.Value), Convert.ToInt64(resID), HfKodeAcara.Value, UserName);
                }
                if (CountItemAcaraGWP > 0)
                {
                    AcrDA.DuplicateMsItemAcaraGWP_PWP(Convert.ToInt64(HfIdToDuplicate.Value), Convert.ToInt64(resID), HfKodeAcara.Value, UserName);
                }
                bindGrid();
            }
        }
    }
}