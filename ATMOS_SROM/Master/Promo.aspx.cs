using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Data.SqlTypes;
using System.IO;

namespace ATMOS_SROM.Master
{
    public partial class Promo : System.Web.UI.Page
    {
        GLOBALCODE func = new GLOBALCODE();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
                bindStore();
                firstDelete();
            }
        }

        protected void bindGrid()
        {
            List<MS_PROMO> listPromo = new List<MS_PROMO>();
            MS_PROMO_DA promoDA = new MS_PROMO_DA();

            string where = tbSearch.Text == "" ? " where STATUS = 1" : String.Format(" where STATUS = 1 and {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);
            listPromo = promoDA.getPromo(where);

            gvMain.DataSource = listPromo;
            gvMain.DataBind();

            btnDivUploadShr.Visible = divUploadPromo.Visible ? true : false;
            btnDivUploadAll.Visible = divUploadPromo.Visible ? false : true;

            DivMessage.Visible = false;
        }

        protected void bindStore()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            MS_SHOWROOM showRoom = new MS_SHOWROOM();

            showRoom.SHOWROOM = "--Pilih Showroom--";
            showRoom.KODE = "";
            listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN'");
            listStore.Insert(0, showRoom);
            ddlUploadShr.DataSource = listStore;
            ddlUploadShr.DataBind();
        }

        protected void bindPromoShr()
        {
            List<MS_PROMO_SHR> listPromo = new List<MS_PROMO_SHR>();
            MS_PROMO_DA promoDA = new MS_PROMO_DA();

            string where = tbSearch.Text == "" ? "" : String.Format(" where {0} like '%{1}%'", ddlSearch.SelectedValue, tbSearch.Text);
            listPromo = promoDA.getPromoSHR(where);

            gvPromoShr.DataSource = listPromo;
            gvPromoShr.DataBind();

            btnDivUploadShr.Visible = divUploadPromo.Visible ? true : false;
            btnDivUploadAll.Visible = divUploadPromo.Visible ? false : true;

            DivMessage.Visible = false;
        }

        protected void firstDelete()
        {
            MS_WAREHOUSE_DA wareDA = new MS_WAREHOUSE_DA();
            string user =  Session["UName"] == null ? "" : Session["UName"].ToString();
            wareDA.deleteTempKdbrg(user);
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
            bindPromoShr();
        }

        protected void btnUploadPromo_Click(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            divMain.Visible = true;

            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUpload.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUpload.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_PROMO" + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FileUpload.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    UPRhdnSource.Value = source;
                    UPRlbFileName.Text = ExcelFileName;
                    ModalUploadReady.Show();
                    //bool ret = upload(dir, FileUploadName, source);
                    //if (ret)
                    //{
                    //    //DivMessage.InnerText = "Upload Berhasil!";
                    //    DivMessage.Attributes["class"] = "success";
                    //    DivMessage.Visible = true;

                    //    bindGrid();

                    //    File.Delete(source);
                    //}
                }
                else
                {
                    DivMessage.InnerText = "File Harus Bertipe xls.";
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

        protected void btnUploadPromoShrClick(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            divMain.Visible = true;

            string ExcelFileName = "";
            string FileUploadName = "";
            string source = "";

            string ExcelType = FileUploadShr.PostedFile.ContentType.ToLower();
            ExcelFileName = FileUploadShr.FileName;
            string FileType = Path.GetExtension(ExcelFileName).ToString();
            FileUploadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_PROMO" + Path.GetExtension(ExcelFileName);

            if (ExcelFileName != "")
            {
                if (FileType.ToLower() == ".xls" || FileType.ToLower() == ".xlsx")
                {
                    FileUploadShr.PostedFile.SaveAs(Server.MapPath("../Upload\\" + FileUploadName));
                    source = Server.MapPath("../Upload\\" + FileUploadName);

                    string dir = Path.GetDirectoryName(source);
                    string dir2 = Server.MapPath("~/Excel/" + FileUploadName);
                    UPRhdnSource.Value = source;
                    UPRlbFileName.Text = ExcelFileName;
                    ModalUploadReady.Show();
                    //bool ret = upload(dir, FileUploadName, source);
                    //if (ret)
                    //{
                    //    //DivMessage.InnerText = "Upload Berhasil!";
                    //    DivMessage.Attributes["class"] = "success";
                    //    DivMessage.Visible = true;

                    //    bindGrid();

                    //    File.Delete(source);
                    //}
                }
                else
                {
                    DivMessage.InnerText = "File Harus Bertipe xls.";
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

        protected void btnDivUploadClick(object sender, EventArgs e)
        {
            if (btnDivUploadAll.Visible)// Yang di Click Tombol untuk melihat semua promo
            {
                btnDivUploadAll.Visible = false;
                btnDivUploadShr.Visible = true;

                divUploadPromo.Visible = true;
                divUploadPromoShowroom.Visible = false;

                lbJudul.Text = "Promo All Article";
                btnDivPromoShr.Visible = true;
                btnDivAllPromo.Visible = false;

                divAllPromo.Visible = true;
                divPromoShr.Visible = false;

                gvMain.PageIndex = 0;
                bindGrid();
            }
            else
            {
                btnDivUploadAll.Visible = true;
                btnDivUploadShr.Visible = false;

                divUploadPromo.Visible = false;
                divUploadPromoShowroom.Visible = true;

                lbJudul.Text = "Promo per Showroom";
                btnDivPromoShr.Visible = false;
                btnDivAllPromo.Visible = true;

                divAllPromo.Visible = false;
                divPromoShr.Visible = true;
                bindPromoShr();
            }
        }

        protected void divPromoClick(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "allPromo")// Yang di Click Tombol untuk melihat semua promo
            {
                btnDivUploadAll.Visible = false;
                btnDivUploadShr.Visible = true;

                divUploadPromo.Visible = true;
                divUploadPromoShowroom.Visible = false;

                lbJudul.Text = "Promo All Article";
                btnDivPromoShr.Visible = true;
                btnDivAllPromo.Visible = false;

                divAllPromo.Visible = true;
                divPromoShr.Visible = false;

                gvMain.PageIndex = 0;
                bindGrid();
            }
            else if (e.CommandName == "promoSHR")
            {
                btnDivUploadAll.Visible = true;
                btnDivUploadShr.Visible = false;

                divUploadPromo.Visible = false;
                divUploadPromoShowroom.Visible = true;

                lbJudul.Text = "Promo per Showroom";
                btnDivPromoShr.Visible = false;
                btnDivAllPromo.Visible = true;

                divAllPromo.Visible = false;
                divPromoShr.Visible = true;
                bindPromoShr();
            }
        }

        protected void UPRbtnUploadClick(object sender, EventArgs e)
        {
            bool ret = true;
            if (divUploadPromo.Visible)
            {
                ret = upload(UPRhdnSource.Value);
            }
            else
            {
                ret = uploadShowroom(UPRhdnSource.Value);
            }

            if (ret)
            {
                bindGrid();
                bindPromoShr();
                //DivMessage.InnerText = "Upload Berhasil!";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                firstDelete();
                File.Delete(UPRhdnSource.Value);
            }
        }

        protected bool upload(string source)
        {
            bool ret = true;
            int data = 0;
            int insert = 0;
            int doubel = 0;
            string idKDBRG = "";
            MS_PROMO promo = new MS_PROMO();
            MS_PROMO_DA promoDA = new MS_PROMO_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            List<MS_PROMO> msPromoList = new List<MS_PROMO>();
            List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
            msPromoList = promoDA.getPromo(" where STATUS = 1 ");
            //msKdbrgList = kdbrgDA.getMsKdbrg("");
            msKdbrgList = kdbrgDA.getMsKdbrgArticle("");
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
                string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                firstSheetName = firstSheetName.ToLower().Contains("print_titles") || firstSheetName.ToLower().Contains("print_area") ||
                    firstSheetName.ToLower().Contains("filterdatabase") ? dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString() : firstSheetName;
                cnn.Close();

                //string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();
                //firstSheetName = firstSheetName.ToLower().Contains("print_titles") || firstSheetName.ToLower().Contains("print_area") ||
                //    firstSheetName.ToLower().Contains("filterdatabase") ? dbSchema.Rows[0]["TABLE_NAME"].ToString() : firstSheetName;

                //string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                foreach (DataRow item in dsOle.Tables[0].Rows)
                {
                    if (item[0].ToString() != "" && item[3].ToString() != "" && item[5].ToString() != "")
                    {
                        string start = item[3].ToString().Remove(item[3].ToString().IndexOf(' '));
                        string end = item[4].ToString() == "" ? "" : item[4].ToString().Remove(item[4].ToString().IndexOf(' '));

                        DateTime dt = DateTime.Now;
                        dt = Convert.ToDateTime(item[3]);
                        DateTime endDT = SqlDateTime.MaxValue.Value;
                        endDT = item[4].ToString() == "" ? endDT : Convert.ToDateTime(item[4]);

                        //if (!string.IsNullOrEmpty(start))
                        //{
                        //    DateTime.TryParseExact(start, "dd/MM/yyyy",
                        //    CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                        //}

                        //if (!string.IsNullOrEmpty(end))
                        //{
                        //    DateTime.TryParseExact(end, "dd/MM/yyyy",
                        //    CultureInfo.InvariantCulture, DateTimeStyles.None, out endDT);
                        //}

                        List<MS_PROMO> listMsPromoCheck = new List<MS_PROMO>();
                        listMsPromoCheck = msPromoList.Where(items => items.BARCODE == item[0].ToString() && items.END_DATE >= dt).ToList<MS_PROMO>();

                        if (msKdbrgList.Where(itemk => itemk.BARCODE == item[0].ToString()).ToList<MS_KDBRG>().Count > 0)
                        {
                            if (listMsPromoCheck.Count > 0)
                            {
                                promo = listMsPromoCheck.OrderByDescending(itemo => itemo.END_DATE).ToList<MS_PROMO>().First();

                                promo.END_DATE = dt.AddDays(-1);
                                promo.UPDATED_BY = Session["UName"].ToString();
                                promoDA.updateEndDateMsPromo(promo);

                                doubel++;
                            }

                            idKDBRG = msKdbrgList.Where(itemk => itemk.BARCODE == item[0].ToString()).ToList<MS_KDBRG>().First().ID.ToString();

                            promo.ID_KDBRG = Convert.ToInt64(idKDBRG);
                            promo.BARCODE = item[0].ToString();
                            promo.ITEM_CODE = msKdbrgList.Where(itemi => itemi.BARCODE == item[0].ToString()).ToList<MS_KDBRG>().First().ITEM_CODE.ToString();
                            promo.SPCL_PRICE = item[1].ToString() == "" ? 0 : Convert.ToDecimal(item[1].ToString());
                            promo.DISCOUNT = item[2].ToString() == "" ? (int?)null : Convert.ToInt32(item[2].ToString());
                            promo.START_DATE = dt;
                            promo.END_DATE = endDT;
                            promo.FLAG = item[5].ToString();
                            promo.CATATAN = item[6].ToString();
                            promo.CREATED_BY = Session["UName"].ToString();

                            promoDA.insertMsPromo(promo);
                            //idUpload = idUpload + "," + kdbrgDA.insertMsKdbrg(kdbrg);

                            insert++;
                        }
                        data++;
                    }
                }
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
            return ret;
        }

        protected bool uploadShowroom(string source)
        {
            bool ret = true;
            int data = 0;
            string error = "";
            int insert = 0;
            int doubel = 0;
            int line = 2;
            string barcodeNotFound = "";
            string dataSalah = "";
            string kode = ddlUploadShr.SelectedValue.Split('#')[1];
            string idStore = ddlUploadShr.SelectedValue.Split('#')[0];
            string user = Session["UName"].ToString();
            MS_PROMO_SHR promo = new MS_PROMO_SHR();
            MS_PROMO_DA promoDA = new MS_PROMO_DA();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();

            List<MS_PROMO_SHR> msPromoList = new List<MS_PROMO_SHR>();
            List<MS_KDBRG> msKdbrgList = new List<MS_KDBRG>();
            msPromoList = promoDA.getPromoSHR(" where KODE = '" + kode + "'"); ;
            //msKdbrgList = kdbrgDA.getMsKdbrg("");
            msKdbrgList = kdbrgDA.getMsKdbrgArticle("");
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
                string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
                firstSheetName = firstSheetName.ToLower().Contains("print_titles") || firstSheetName.ToLower().Contains("print_area") ||
                    firstSheetName.ToLower().Contains("filterdatabase") ? dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString() : firstSheetName;
                cnn.Close();
                OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);

                oAdapter.Fill(dsOle, "Sheet1");
                if (dsOle.Tables[0].Rows[0][2].ToString() != "")
                {
                    foreach (DataRow item in dsOle.Tables[0].Rows)
                    {
                        if (item[0].ToString().Trim() != "" || item[4].ToString().Trim() != "" || item[5].ToString().Trim() != "")
                        {
                            if (item[7].ToString().Trim().ToLower() == "p" || item[7].ToString().Trim().ToLower() == "d")
                            {
                                List<MS_KDBRG> listBrg = new List<MS_KDBRG>();
                                listBrg = msKdbrgList.Where(itemb => itemb.BARCODE.Trim().ToLower() == item[0].ToString().ToLower()).ToList<MS_KDBRG>();

                                DateTime sDt = DateTime.Now;
                                //string startDate = item[5].ToString().Remove(10); //String.Format("{0:dd/MM/yyyy}", item[5].ToString());
                                sDt = Convert.ToDateTime(item[5]);
                                //string startDate = item[5].ToString().Remove(item[5].ToString().IndexOf(' '));
                                //if (!string.IsNullOrEmpty(startDate))
                                //{
                                //    DateTime.TryParseExact(startDate, "M/d/yyyy",
                                //       CultureInfo.InvariantCulture, DateTimeStyles.None, out sDt);

                                //    DateTime.TryParseExact(startDate, "dd/MM/yyyy",
                                //        CultureInfo.InvariantCulture, DateTimeStyles.None, out sDt);
                                //}

                                DateTime eDt = DateTime.MaxValue;
                                eDt = item[6].ToString() == "" ? eDt : Convert.ToDateTime(item[6]);
                                //string endDate = item[6].ToString() == "" ? "31/12/2999" : item[6].ToString().Remove(item[6].ToString().IndexOf(' ')); ;
                                //if (!string.IsNullOrEmpty(endDate))
                                //{
                                //    DateTime.TryParseExact(endDate, "M/d/yyyy",
                                //       CultureInfo.InvariantCulture, DateTimeStyles.None, out eDt);

                                //    DateTime.TryParseExact(endDate, "dd/MM/yyyy",
                                //        CultureInfo.InvariantCulture, DateTimeStyles.None, out eDt);
                                //}


                                if (listBrg.Count > 0)
                                {
                                    TEMP_KDBRG tempKDBRG = new TEMP_KDBRG();
                                    tempKDBRG.ID_KDBRG = listBrg.First().ID;//
                                    tempKDBRG.ID_SHOWROOM = Convert.ToInt64(idStore);//
                                    tempKDBRG.KODE = kode;//
                                    tempKDBRG.SHOWROOM = ddlUploadShr.SelectedItem.Text;//
                                    tempKDBRG.BARCODE = item[0].ToString();//
                                    tempKDBRG.ITEM_CODE = listBrg.First().ITEM_CODE;//
                                    tempKDBRG.CREATED_BY = user;//
                                    //tempKDBRG.CREATED_DATE = DateTime.Now;
                                    tempKDBRG.FLAG = "promo";//
                                    tempKDBRG.STAT = item[7].ToString();//
                                    tempKDBRG.BRAND = listBrg.First().FBRAND;//
                                    tempKDBRG.ART_DESC = listBrg.First().FART_DESC;//
                                    tempKDBRG.FCOLOR = listBrg.First().FCOL_DESC;//
                                    tempKDBRG.FSIZE = listBrg.First().FSIZE_DESC;//
                                    tempKDBRG.PRICE = listBrg.First().PRICE;
                                    tempKDBRG.DISCOUNT = item[7].ToString().ToLower().Trim() == "d" ? Convert.ToInt32(item[4].ToString()) : 0;
                                    tempKDBRG.PRICE_AKHIR = item[7].ToString().ToLower().Trim() == "d" ? tempKDBRG.PRICE * tempKDBRG.DISCOUNT / 100 : Convert.ToDecimal(item[4].ToString());
                                    tempKDBRG.START_DATE = sDt;
                                    tempKDBRG.END_DATE = eDt;

                                    string idNew = promoDA.insertTempPromoShr(tempKDBRG);
                                    error = idNew.ToLower().Contains("error") ? error + "," + line.ToString() : error;
                                }
                                else
                                {
                                    barcodeNotFound = barcodeNotFound + "," + line.ToString();
                                }
                                line++;
                                data++;
                            }
                            else
                            {
                                dataSalah = dataSalah + "," + line.ToString();
                            }
                            //btnTest.Text = sDt.ToString();
                        }
                    }
                    promoDA.insertPromoSHR(ddlUploadShr.SelectedItem.Text, user);

                    DivMessage.InnerText = "Upload Berhasil! | Data : " + data.ToString();
                    DivMessage.InnerText = barcodeNotFound == "" ? DivMessage.InnerText : DivMessage.InnerText + " | Barcode Not Found in Line : " + barcodeNotFound.Remove(0, 1);
                    DivMessage.InnerText = dataSalah == "" ? DivMessage.InnerText : DivMessage.InnerText + " | Data Salah in Line : " + dataSalah.Remove(0, 1);
                    DivMessage.InnerText = error == "" ? DivMessage.InnerText : DivMessage.InnerText + " | Error in Line : " + error.Remove(0, 1);

                }
            }
            catch (Exception ex)
            {
                ret = false;

                DivMessage.InnerText = "Error Gagal : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            func.addLog("Promo > Upload > " + DivMessage.InnerText, Session["UName"].ToString());
            return ret;
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

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void gvPromoShrRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvPromoShr.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName.ToLower() == "deleterow")
                    {

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

        protected void gvPromoShrPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvPromoShr.PageIndex = e.NewPageIndex;
            bindGrid();
        }
    }
}