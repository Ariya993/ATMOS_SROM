using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data.SqlTypes;
using System.Globalization;
using Microsoft.PointOfService;
using System.Drawing.Printing;
using ATMOS_SROM.Model.CustomObj;
using ATMOS_SROM.Domain.CustomObj;

namespace ATMOS_SROM.Sales
{
    public partial class Sales : System.Web.UI.Page
    {
        string globalNoBon = string.Empty;
        int slhpin;
        DateTime DtIReturTrans;
        DateTime NewPPNDate = Convert.ToDateTime("2022-04-01");
        //DateTime NewPPNDate = Convert.ToDateTime("2022-03-25");

        private class CardName
        {
            public string Nama { get; set; }
            public string Kode { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //lbl1.Text = "NO MEMBER : "+ hdnMemberNumber.Value;
            if (!Page.IsPostBack)
            {
                ////MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                ////kdbrgDA.testing();
                //CultureInfo ci = CultureInfo.InvariantCulture;
                //Session.Remove("IJual");
                //Session.Remove("UStock");
                //firstDelete();
                //deleteTempAcara();
                //bindGrid();
                //bindStore();
                //clearSession();
                //bindddlCard();
                ////slhpin = 0;
                //lblWrongPin.Text = "0";
                //DateTime dt = DateTime.Now;
                ////string sDate = dt.ToString("hh-mm-ss", ci);
                //tbDate.Text = dt.ToString("dd'-'MM'-'yyyy");
                ////hdncountwrongpin.Value = "";
                //lblFlag.Text = "";
                //#region "COMMENT"
                ////String pkInstalledPrinters;
                ////for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                ////{
                ////    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                ////}

                ////MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                ////MS_SHOWROOM show = new MS_SHOWROOM();

                ////string sUser = Session["UName"].ToString();
                ////string sKode = Session["UKode"].ToString();
                ////show = showDA.getShowRoom(String.Format(" where KODE = '{0}'", sKode)).First();

                ////PRINT_DA printDA = new PRINT_DA("000", show, sUser);

                ////string szPrinterName = @"NPID95464\HP LaserJet 400 M401 PCL 6";

                ////StreamReader sr = new StreamReader(@"F:\a.txt");
                ////string line = (char)27 + "*c32545D";
                ////line += sr.ReadToEnd();
                ////line += (char)27 + "*c5F";

                ////PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);
                ////PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");

                ////szPrinterName = @"\\NPID95464\HP LaserJet 400 M401 PCL 6";
                ////PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);
                ////PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");

                ////szPrinterName = @"\\192.168.2.171";
                ////PrintRaw.RawFilePrint.SendStringToPrinter(szPrinterName, line);


                ////szPrinterName = @"HP LaserJet 400 M401 PCL 6";
                ////PrintRaw.RawFilePrint.SendFileToPrinter(szPrinterName, @"F:\a.pdf");
                //#endregion
            }
        }
        protected void bindddlCard()
        {
            List<CardName> listCardname = new List<CardName>()  {
                new CardName() { Nama = "OTHER BANK", Kode = "OTHER BANK" },
                new CardName() { Nama = "BANK MEGA", Kode = "BANK MEGA" },
                new CardName() { Nama = "BCA", Kode = "BCA" },
                new CardName() { Nama = "BNI", Kode = "BNI" },
                new CardName() { Nama = "BRI", Kode = "BRI" },
                new CardName() { Nama = "CIMB NIAGA", Kode = "CIMB NIAGA" },
                new CardName() { Nama = "CITIBANK", Kode = "CITIBANK" },
                new CardName() { Nama = "COMMONWEALTH", Kode = "COMMONWEALTH" },
                new CardName() { Nama = "DANAMON", Kode = "DANAMON" },
                new CardName() { Nama = "HSBC", Kode = "HSBC" },
                new CardName() { Nama = "ICBC", Kode = "ICBC" },
                new CardName() { Nama = "MANDIRI", Kode = "MANDIRI" },
                new CardName() { Nama = "MEGA", Kode = "MEGA" },
                new CardName() { Nama = "OCBS NISP", Kode = "OCBS NISP" },
                new CardName() { Nama = "PANIN", Kode = "PANIN" },
                new CardName() { Nama = "STANDARD CHARTERED", Kode = "STANDARD CHARTERED" },
                new CardName() { Nama = "UOB", Kode = "UOB" },
                 new CardName() { Nama = "MEMBERSHIP", Kode = "MEMBERSHIP" }
            };

            ddlCardName.DataSource = listCardname;
            ddlCardName.DataBind();
        }
        protected void bindGrid()
        {
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            string user = Session["UName"] == null ? "" : Session["UName"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();

            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + user + "'");
            gvMain.DataSource = struckList;
            gvMain.DataBind();

            btnSave.Enabled = struckList.Where(item => item.RETUR == "No").ToList<TEMP_STRUCK>().Count > 0 ? true : false;
            btnVoid.Visible = sLevel.ToLower().Contains("admin") ? true : false;

            DivMessage.Visible = false;
        }

        protected void bindStore()
        {
            ddlStore.Enabled = true;
            string store = Session["UStore"] == null ? "" : Session["UStore"].ToString();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            divStore.Visible = store.ToLower() == "head office" ? true : false;
            lbJudul.Text = "Sales Page (Showroom)";
            if (divStore.Visible)
            {
                //ddlStore.Enabled = false;
                //btnsearchstore1.Visible = true;
                #region "SIS / FSS"
                if (sLevel.ToLower() == "admin counter")
                {
                    lbJudul.Text = "Sales Page (SIS)";

                    MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                    List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                    MS_SHOWROOM showRoom = new MS_SHOWROOM();

                    showRoom.SHOWROOM = "--Pilih Showroom--";
                    showRoom.KODE = "";
                    listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS'");
                    listStore.Insert(0, showRoom);
                    ddlStore.DataSource = listStore;
                    ddlStore.DataBind();
                    int index = 0;
                    int.TryParse(hdnIDStore.Value, out index);
                    ddlStore.SelectedIndex = index;
                }
                else if (sLevel.ToLower() == "admin sales")
                {
                    lbJudul.Text = "Sales Page Admin (Showroom)";

                    MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                    List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                    MS_SHOWROOM showRoom = new MS_SHOWROOM();

                    showRoom.SHOWROOM = "--Pilih Showroom--";
                    showRoom.KODE = "";
                    listStore = showRoomDA.getShowRoom(" where STATUS = 'OPEN' and STATUS_SHOWROOM = 'FSS'");
                    listStore.Insert(0, showRoom);
                    ddlStore.DataSource = listStore;
                    ddlStore.DataBind();
                    int index = 0;
                    int.TryParse(hdnIDStore.Value, out index);
                    ddlStore.SelectedIndex = index;
                    trMarginSIS.Visible = false;
                }
                #endregion
            }
        }

        protected void firstDelete()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            kdbrgDA.deleteStruck(namaKasir);
            bayarDA.deleteTempDoc(namaKasir);
        }

        protected void deleteTempAcara()
        {
            string namaKasir = Session["UName"] == null ? "" : Session["UName"].ToString();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            kdbrgDA.deleteTempAcara(namaKasir);
        }

        protected void bindSearch()
        {
            List<MS_KDBRG> kdbrgList = new List<MS_KDBRG>();
            List<MS_KDBRG> kdbrgListwithsprbrand = new List<MS_KDBRG>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            MS_KDBRG kdbrg = new MS_KDBRG();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            //sKode = sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter" ? ddlStore.SelectedValue : sKode;
            //string search = " where " + ddlSearchBy.SelectedValue + " like '%" + tbSearchBy.Text + "%' and STOCK is not null";
            string search = " where brg." + ddlSearchBy.SelectedValue + " like '%" + tbSearchBy.Text + "%'";
            #region Article by Brand
            String superbrand = "";
            if (sLevel.ToLower() == "admin sales")
            {
                sKode = ddlStore.SelectedValue.ToLower();
            }
            else if (sLevel.ToLower() == "admin counter")
            {
                if (ddlStore.SelectedValue.ToLower() != "")
                {
                    sKode = ddlStore.SelectedValue.ToLower();
                }
                else
                {
                    sKode = Session["UKode"].ToString().ToLower();
                }
            }
            else
            { sKode = Session["UKode"].ToString().ToLower(); }
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> lShr = new List<MS_SHOWROOM>();
            lShr = showRoomDA.getShowRoom("WHERE KODE = '" + sKode + "'");
            //superbrand = lShr.FirstOrDefault().BRAND_JUAL;
            List<string> namessprbrand = lShr.FirstOrDefault().BRAND_JUAL.Split(',').ToList<string>();
            foreach (String sbrand in namessprbrand)
            {
                if (superbrand == "")
                {
                    superbrand = "'" + sbrand + "'";
                }
                else
                {
                    superbrand = superbrand + ",'" + sbrand + "'";
                }
            }
            kdbrgList = kdbrgDA.getMsKdbrgSearch100(search, sKode);
            kdbrgListwithsprbrand = kdbrgDA.getMsKdbrgTablebyBrand(search, superbrand);
            if (kdbrgListwithsprbrand.Count <= 0)
            {
                kdbrgList = kdbrgDA.getMsKdbrgTable(search);
                kdbrg.ID = 0;
                kdbrg.BARCODE = "SERVICE";
                kdbrg.ITEM_CODE = "SERVICE";
                kdbrg.ART_DESC = "SERVICE";
                kdbrg.PRICE = 0;
                kdbrgList.Add(kdbrg);
                gvSearch.DataSource = kdbrgList;
                gvSearch.DataBind();
            }
            else
            {
                kdbrg.ID = 0;
                kdbrg.BARCODE = "SERVICE";
                kdbrg.ITEM_CODE = "SERVICE";
                kdbrg.ART_DESC = "SERVICE";
                kdbrg.PRICE = 0;
                kdbrgListwithsprbrand.Add(kdbrg);
                gvSearch.DataSource = kdbrgListwithsprbrand;
                gvSearch.DataBind();
            }
            #endregion
            closeAllModal();
            //kdbrgList = kdbrgDA.getMsKdbrgTable(search);
            //kdbrg.ID = 0;
            //kdbrg.BARCODE = "SERVICE";
            //kdbrg.ITEM_CODE = "SERVICE";
            //kdbrg.ART_DESC = "SERVICE";
            //kdbrg.PRICE = 0;
            //kdbrgList.Add(kdbrg);
            //gvSearch.DataSource = kdbrgList;
            //gvSearch.DataBind();
            ModalSearchItemCode.Show();
        }

        protected void bindAcaraOLD()
        {
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            MS_ACARA acara = new MS_ACARA();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString().ToLower();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString().ToLower();

            acara.ID = 0;
            acara.NAMA_ACARA = "Normal";
            acara.ACARA_VALUE = "NORMAL";

            listAcara = acaraDA.getAcara(" where STATUS_ACARA = 1");
            listAcara.Insert(0, acara);

            if (!(sLevel == "admin counter"))//Sales, Admin sales, admin wholesale
            {
                listAcara.RemoveAll(item => item.ACARA_VALUE == "CNTRL" || item.ACARA_VALUE == "LV40" || item.ACARA_VALUE == "2M1S");
            }

            if (!(sLevel == "admin sales"))//Sales, Admin counter, admin wholesale
            {
                if (!(sKode == "sosfss04" || sKode == "sosfss01"))
                {
                    listAcara.RemoveAll(item => item.ACARA_VALUE == "AE12");
                }
                else if (sKode == "sosfss01")//MEL-GI-001 sosfss01
                {
                    listAcara.RemoveAll(item => item.ACARA_VALUE == "SP40");
                }

                if (!(sKode == "sosfss05" || sKode == "sosfss07"))
                {
                    listAcara.RemoveAll(item => item.ACARA_VALUE == "SP30S");
                    listAcara.RemoveAll(item => item.ACARA_VALUE == "UNI50");
                }

                if (!(sKode == "sosfss02" || sKode == "sosfss03"))
                {
                    listAcara.RemoveAll(item => item.ACARA_VALUE == "7S20S");
                }
            }

            if (sLevel == "admin wholesale")//Admin Wholesale
            {
                listAcara.RemoveAll(item => item.ACARA_VALUE != "NORMAL");
            }

            ddlBYRAcara.DataSource = listAcara;
            ddlBYRAcara.DataBind();

            trBYRACaraNama.Visible = false;
            trBYRNoID.Visible = false;
        }

        //protected void bindAcara()
        //{
        //    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
        //    List<MS_ACARA> listAcara = new List<MS_ACARA>();
        //    MS_ACARA acara = new MS_ACARA();
        //    string sLevel = Session["ULevel"].ToString().ToLower();
        //    string sKode = sLevel == "admin sales" ? ddlStore.SelectedValue.ToLower() : sLevel == "admin counter" ? "SIS" : Session["UKode"].ToString().ToLower();

        //    acara.ID_ACARA = 0;
        //    acara.NAMA_ACARA = "Normal";
        //    acara.ACARA_VALUE = "NORMAL";

        //    DateTime tglTrans = DateTime.Now;
        //    string date = tbDate.Visible ? tbDate.Text.ToString() : string.Format("{0:dd-MM-yyyy}",DateTime.Now);
        //    if (!string.IsNullOrEmpty(date))
        //    {
        //        DateTime.TryParseExact(date, "dd-MM-yyyy",
        //        CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
        //    }

        //    listAcara = acaraDA.getAcaraGroupBy(" and STATUS_ACARA = 1 and ISNULL(KODE, '" + sKode + "') = '" + sKode + "' group by ID_ACARA, ACARA_VALUE, NAMA_ACARA", tglTrans);
        //    listAcara.Insert(0, acara);

        //    if (sLevel == "admin wholesale")//Admin Wholesale
        //    {
        //        listAcara.RemoveAll(item => item.ACARA_VALUE != "NORMAL");
        //    }

        //    ddlBYRAcara.DataSource = listAcara;
        //    ddlBYRAcara.DataBind();

        //    trBYRACaraNama.Visible = false;
        //    trBYRNoID.Visible = false;
        //}

        protected void bindAcara()
        {
            MS_ACARA_DA acaraDA = new MS_ACARA_DA();
            List<MS_ACARA> listAcara = new List<MS_ACARA>();
            MS_ACARA acara = new MS_ACARA();
            string sLevel = Session["ULevel"].ToString().ToLower();
            string Sessionkd = Session["UKode"].ToString().ToLower();
            //string sKode = sLevel == "admin sales" ? ddlStore.SelectedValue.ToLower() : sLevel == "admin counter" ? ddlStore.SelectedValue.ToLower() : Session["UKode"].ToString().ToLower();
            string sKode = "";
            if (sLevel == "admin sales")
            {
                sKode = ddlStore.SelectedValue.ToLower();
            }
            else if (sLevel == "admin counter")
            {
                if (ddlStore.SelectedValue.ToLower() != "")
                {
                    sKode = ddlStore.SelectedValue.ToLower();
                }
                else
                {
                    sKode = Session["UKode"].ToString().ToLower();
                }
            }
            else
            { sKode = Session["UKode"].ToString().ToLower(); }
            acara.ID_ACARA = 0;
            acara.NAMA_ACARA = "Normal";
            acara.ACARA_VALUE = "NORMAL";

            DateTime tglTrans = DateTime.Now;
            string date = tbDate.Visible ? tbDate.Text.ToString() : string.Format("{0:dd-MM-yyyy}", DateTime.Now);
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }
            //tambahkan tanggal dan status yah william masa depan? yang mana william masa lalu?
            listAcara = acaraDA.getAcaraShowroom(string.Format(" where ISNULL(KODE,'') = '{0}' and '{1:yyyy-MM-dd}' between ISNULL(START_DATE,getdate()) and ISNULL(END_DATE,getdate())", sKode, tglTrans));
            listAcara.Insert(0, acara);

            if (sLevel == "admin wholesale")//Admin Wholesale
            {
                listAcara.RemoveAll(item => item.ACARA_VALUE != "NORMAL");
            }

            ddlBYRAcara.DataSource = listAcara;
            ddlBYRAcara.DataBind();

            trBYRACaraNama.Visible = false;
            trBYRNoID.Visible = false;
        }

        protected void bindNoBon()
        {
            List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            string sLevel = Session["ULevel"].ToString().ToLower();
            string sKode = Session["UKode"].ToString().ToLower();
            string where = "";
            if (sLevel.ToLower() == "admin counter" && sKode.ToLower() != "ho-001")
            {
                where = string.Format(" where TGL_TRANS is not null and DATEDIFF(M, TGL_TRANS, getdate()) < 3 and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y' AND KODE = '{1}'", tbBONSearch.Text, sKode);
            }
            else
            {
                where = string.Format(" where TGL_TRANS is not null and DATEDIFF(M, TGL_TRANS, getdate()) < 3 and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y'", tbBONSearch.Text);
            }
            //string where = string.Format(" where TGL_TRANS is not null and DATEDIFF(M, TGL_TRANS, getdate()) < 3 and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y'", tbBONSearch.Text);
            listBayar = bayarDA.getSHBayar(where);

            gvBON.DataSource = listBayar;
            gvBON.DataBind();

            ModalSearchNoBon.Show();
        }

        protected void bindNoBonReprint()
        {
            List<SH_BAYAR> listBayar = new List<SH_BAYAR>();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            string sLevel = Session["ULevel"].ToString().ToLower();
            string sKode = Session["UKode"].ToString().ToLower();
            string where = "";
            if (sLevel.ToLower() == "admin counter" && sKode.ToLower() != "ho-001")
            {
                where = string.Format(" where TGL_TRANS is not null and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y' AND KODE = '{1}'", tbBONSearch.Text, sKode);
            }
            else if (sLevel.ToLower() == "store manager" && sKode.ToLower() != "ho-001")
            {
                where = string.Format(" where TGL_TRANS is not null and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y' AND KODE = '{1}'", tbBONSearch.Text, sKode);
            }
            else if (sLevel.ToLower() == "sales" && sKode.ToLower() != "ho-001")
            {
                where = string.Format(" where TGL_TRANS is not null and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y' AND KODE = '{1}'", tbBONSearch.Text, sKode);
            }
            else
            {
                where = string.Format(" where TGL_TRANS is not null and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y'", tbBONSearch.Text);
            }
            //string where = string.Format(" where TGL_TRANS is not null and DATEDIFF(M, TGL_TRANS, getdate()) < 3 and NO_BON like '%{0}%' and ISNULL(BATAL, '') != 'Y'", tbBONSearch.Text);
            listBayar = bayarDA.getSHBayar(where);
            gvBON.DataSource = listBayar;
            gvBON.DataBind();
            //gvreprint.DataSource = listBayar;
            //gvreprint.DataBind();

            ModalSearchNoBon.Show();
        }

        protected void bindItemRetur()
        {
            List<SH_JUAL> listJual = new List<SH_JUAL>();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            string sLevel = Session["ULevel"].ToString().ToLower();

            string where = string.Format(" where ID_BAYAR like {0}", hdnIDBayarRetur.Value);
            listJual = bayarDA.getSHJual(where);

            btnIReturVoid.Visible = sLevel.Contains("admin") ? true : false;

            gvIRetur.DataSource = listJual;
            gvIRetur.DataBind();

            divIReturMessage.Visible = false;
            ModalItemRetur.Show();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            string sLevel = Session["ULevel"].ToString();

            if (!(ddlStore.SelectedIndex == 0 && sLevel.ToLower() == "admin sales"))
            {
                tbSearchBy.Text = tbBarcode.Text;
                bindSearch();
                clearhiddenfieldmember();
            }
            else
            {
                DivMessage.InnerText = "Pilih Showroom terlebih dahulu!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void btnSearchSearchClick(object sender, EventArgs e)
        {
            bindSearch();
        }

        protected void gvSearchPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearch.PageIndex = e.NewPageIndex;
            bindSearch();
        }

        protected void gvSearcRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;

                    if (e.CommandName.ToLower() == "saverow")
                    {
                        string barcode = gvSearch.Rows[rowIndex].Cells[2].Text.ToString();
                        if (!(barcode == "SERVICE"))
                        {
                            tbBarcode.Text = barcode;
                            inputIntoTempStruck();
                        }
                        else
                        {
                            tbSBarcode.Text = barcode;
                            tbSDesc.Text = "";
                            tbSPrice.Text = "";
                            cbSMinus.Checked = false;
                            ModalService.Show();
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

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string sLevel = Session["ULevel"].ToString();

            if (!(ddlStore.SelectedIndex == 0 && sLevel.ToLower() == "admin sales"))
            {
                inputIntoTempStruck();
            }
            else
            {
                DivMessage.InnerText = "Pilih Showroom terlebih dahulu!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void inputIntoTempStruck()
        {
            //Search Data Item Code ada atau tidak
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            string itemCode = tbBarcode.Text;
            string sLevel = Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();

            //if (Session["UStore"].ToString().ToLower() == "head office" && sLevel.ToLower() == "admin counter")
            //{
            //    DateTime tglTrans = DateTime.Now;
            //    string date = tbDate.Text.ToString();
            //    if (!string.IsNullOrEmpty(date))
            //    { 
            //        DateTime.TryParseExact(date, "dd-MM-yyyy",
            //        CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            //    }
            //    sKode = sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter" ? ddlStore.SelectedValue : sKode;
            //    listKdBrg = kdbrgDA.getMsKdbrgSIS(sKode, tglTrans, " where BARCODE = '" + itemCode + "'");
            //}
            if (sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter")
            {
                DateTime tglTrans = DateTime.Now;
                string date = tbDate.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                }
                sKode = sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter" ? ddlStore.SelectedValue : sKode;
                listKdBrg = kdbrgDA.getMsKdbrgChangeDate(" where BARCODE = '" + itemCode + "'", sKode, tglTrans);
            }
            else
            {
                sKode = sLevel.ToLower() == "admin sales" ? ddlStore.SelectedValue : sKode;
                listKdBrg = kdbrgDA.getMsKdbrg(" where BARCODE = '" + itemCode + "'", sKode);
            }

            //Jika Item Code Terdaftar
            if (listKdBrg.Count > 0)
            {
                //if (listKdBrg.First().STOCK > 0 || listKdBrg.First().STATUS_BRG != "")
                if (listKdBrg.First().STATUS_BRG != "" || listKdBrg.First().STATUS_BRG == "")
                {
                    MS_KDBRG kdbrg = new MS_KDBRG();
                    kdbrg = listKdBrg.First();

                    MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                    MS_SHOWROOM show = new MS_SHOWROOM();

                    show = showDA.getShowRoom(" where KODE = '" + sKode + "'").First();
                    string kasir = Session["UName"].ToString();

                    //Cek Promo
                    if (kdbrg.ID_PROMO > 0)//Ada Promo
                    {
                        if (kdbrg.FLAG == "P")//Promo Special Price
                        {
                            kdbrg.ART_PRICE = kdbrg.PRICE;
                            //kdbrg.PRICE = kdbrg.SPCL_PRICE;
                            kdbrg.NET_PRICE = kdbrg.SPCL_PRICE;
                            kdbrg.BON_PRICE = kdbrg.PRICE;
                            kdbrg.DISCOUNT = 0;
                        }
                        else if (kdbrg.FLAG == "D")//Promo Discount
                        {
                            //if (kdbrg.SPCL_PRICE == 0)//Tidak menaikan harga retail
                            //{
                            kdbrg.ART_PRICE = kdbrg.PRICE;
                            kdbrg.NET_PRICE = (kdbrg.PRICE * (100 - kdbrg.DISCOUNT)) / 100;
                            kdbrg.BON_PRICE = kdbrg.PRICE;
                            //}
                            //else//Menaikan harga retail
                            //{
                            //    kdbrg.ART_PRICE = kdbrg.PRICE;
                            //    kdbrg.PRICE = kdbrg.SPCL_PRICE;
                            //    kdbrg.BON_PRICE = kdbrg.SPCL_PRICE;
                            //    kdbrg.NET_PRICE = (kdbrg.PRICE * (100 - kdbrg.DISCOUNT)) / 100;
                            //}
                        }
                    }
                    else
                    {
                        kdbrg.ART_PRICE = kdbrg.PRICE;
                        kdbrg.NET_PRICE = kdbrg.PRICE;
                        kdbrg.BON_PRICE = kdbrg.PRICE;
                    }
                    kdbrg.RETUR = "No";
                    kdbrg.MEMBER = "No";
                    string insert = kdbrgDA.insertTempStruck(kdbrg, show, kasir);
                    ddlStore.Enabled = false;

                    if (insert == "Berhasil!")
                    {
                        DivMessage.InnerText = "Insert Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        tbBarcode.Text = "";
                        bindGrid();

                        tbBarcode.Focus();
                    }
                    else
                    {
                        DivMessage.InnerText = "Error : " + insert;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Tidak Terdapat Stock Di Sistem, Harap Hubungi Admin.";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Barcode Tidak Ditemukan";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void inputIntoTempStruckRetur(string barcode)
        {
            //Search Data Item Code ada atau tidak
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR_DA SHBayarDA = new SH_BAYAR_DA();
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            List<SH_JUAL> listShJual = new List<SH_JUAL>();
            string itemCode = tbBarcode.Text;
            string NOBON = tbReturNoBon.Text;

            string sLevel = Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            sKode = sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter" ? ddlStore.SelectedValue : sKode;
            listKdBrg = kdbrgDA.getMsKdbrg(" where BARCODE = '" + barcode + "'", sKode);

            //Jika Item Code Terdaftar
            if (listKdBrg.Count > 0)
            {
                MS_KDBRG kdbrg = new MS_KDBRG();
                kdbrg = listKdBrg.First();

                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                MS_SHOWROOM show = new MS_SHOWROOM();

                show = showDA.getShowRoom(" where KODE = '" + sKode + "'").First();

                string kasir = Session["UName"].ToString();

                //Cek Promo
                if (kdbrg.ID_PROMO > 0)//Ada Promo
                {
                    if (kdbrg.FLAG == "P")//Promo Special Price
                    {
                        kdbrg.ART_PRICE = kdbrg.PRICE;
                        kdbrg.NET_PRICE = kdbrg.SPCL_PRICE;
                        kdbrg.BON_PRICE = kdbrg.PRICE;
                    }
                    else if (kdbrg.FLAG == "D")//Promo Discount
                    {
                        kdbrg.ART_PRICE = kdbrg.PRICE;
                        kdbrg.NET_PRICE = (kdbrg.PRICE * (100 - kdbrg.DISCOUNT)) / 100;
                        kdbrg.BON_PRICE = kdbrg.PRICE;
                    }
                }
                else
                {
                    string whereShJual = string.Format(" where NO_BON = '{0}' AND BARCODE = '{1}'", NOBON, barcode);

                    listShJual = SHBayarDA.getSHJual(whereShJual);

                    kdbrg.ART_PRICE = listShJual.FirstOrDefault().TAG_PRICE;
                    kdbrg.NET_PRICE = listShJual.FirstOrDefault().NILAI_BYR;
                    kdbrg.BON_PRICE = listShJual.FirstOrDefault().TAG_PRICE;
                    kdbrg.FLAG = listShJual.FirstOrDefault().JNS_DISC;
                    kdbrg.DISCOUNT = listShJual.FirstOrDefault().DISC_P;
                }
                kdbrg.RETUR = "Yes";
                kdbrg.MEMBER = "No";
                kdbrg.NET_PRICE = kdbrg.NET_PRICE;

                string insert = kdbrgDA.insertTempStruck(kdbrg, show, kasir);

                if (insert == "Berhasil!")
                {
                    bindGrid();

                    DivMessage.InnerText = "Insert Berhasil!";
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    tbBarcode.Text = "";

                    tbBarcode.Focus();
                }
                else
                {
                    DivMessage.InnerText = "Error : " + insert;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Barcode Tidak Ditemukan";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void inputServiceTempStruck()
        {
            //Search Data Item Code ada atau tidak
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            List<MS_KDBRG> listKdBrg = new List<MS_KDBRG>();
            string barcode = tbSBarcode.Text;

            string sLevel = Session["ULevel"].ToString();
            string sKode = Session["UKode"] == null ? "" : Session["UKode"].ToString();
            sKode = sLevel.ToLower() == "admin sales" ? ddlStore.SelectedValue : sKode;
            listKdBrg = kdbrgDA.getMsKdbrgArticle(" where BARCODE = '" + barcode + "'");

            //Jika Item Code Terdaftar
            if (listKdBrg.Count > 0)
            {
                MS_KDBRG kdbrg = new MS_KDBRG();
                kdbrg = listKdBrg.First();

                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                MS_SHOWROOM show = new MS_SHOWROOM();

                show = showDA.getShowRoom(" where KODE = '" + sKode + "'").First();
                string kasir = Session["UName"].ToString();

                //Cek Promo
                kdbrg.FART_DESC = tbSDesc.Text;
                kdbrg.PRICE = Convert.ToDecimal(tbSPrice.Text.Replace(",", ""));
                kdbrg.PRICE = cbSMinus.Checked ? kdbrg.PRICE * -1 : kdbrg.PRICE;
                kdbrg.SPCL_PRICE = 0;
                kdbrg.DISCOUNT = 0;
                kdbrg.FLAG = "";
                kdbrg.ART_PRICE = kdbrg.PRICE;
                kdbrg.NET_PRICE = kdbrg.PRICE;
                kdbrg.BON_PRICE = kdbrg.PRICE;

                kdbrg.RETUR = "No";
                kdbrg.MEMBER = "No";
                string insert = kdbrgDA.insertTempStruck(kdbrg, show, kasir);

                if (insert == "Berhasil!")
                {
                    DivMessage.InnerText = "Insert Berhasil!";
                    DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    tbBarcode.Text = "";
                    bindGrid();

                    tbBarcode.Focus();
                }
                else
                {
                    DivMessage.InnerText = "Error : " + insert;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Barcode Tidak Ditemukan";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvMain.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "editrow")
                    {
                        hdnId.Value = id.ToString();
                        tbPUBarcode.Text = gvMain.Rows[rowIndex].Cells[13].Text;
                        tbPUItemCode.Text = gvMain.Rows[rowIndex].Cells[22].Text;
                        tbPUDesc.Text = gvMain.Rows[rowIndex].Cells[4].Text;
                        tbPUSize.Text = gvMain.Rows[rowIndex].Cells[6].Text;
                        tbPUPrice.Text = gvMain.Rows[rowIndex].Cells[8].Text;
                        tbPUQty.Text = gvMain.Rows[rowIndex].Cells[7].Text;
                        ModalChangeQty.Show();
                    }
                    else if (e.CommandName.ToLower() == "deleterow")
                    {
                        MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                        Int64 idTemp = new Int64();
                        idTemp = Convert.ToInt64(id);
                        string del = kdbrgDA.deleteTempStruck(idTemp);

                        if (del == "Berhasil!")
                        {
                            DivMessage.InnerText = "Delete Berhasil!";
                            DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;

                            bindGrid();
                        }
                        else
                        {
                            DivMessage.InnerText = "Error : " + del;
                            DivMessage.Attributes["class"] = "error";
                            DivMessage.Visible = true;
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

        protected void gvBONCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvBON.DataKeys[rowIndex]["ID"].ToString();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        List<SH_BAYAR> bayarList = new List<SH_BAYAR>();
                        SH_BAYAR bayar = new SH_BAYAR();
                        SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                        hdnIDBayarRetur.Value = id;
                        hdnIReturIDBayar.Value = id;
                        //DateTime dtransBon = Convert.ToDateTime(gvBON.Rows[rowIndex].Cells[4].Text.Substring(0, gvBON.Rows[rowIndex].Cells[4].Text.IndexOf(" ")));
                        //hdnIReturTgl.Value = dtransBon.ToString("dd'/'MM'/'yyyy");
                        tbIReturNoBon.Text = gvBON.Rows[rowIndex].Cells[3].Text;
                        tbIReturStore.Text = gvBON.Rows[rowIndex].Cells[2].Text;
                        bayarList = bayarDA.getSHBayar(" where ID = '" + hdnIReturIDBayar.Value.Trim() + "'");
                        bayar = bayarList.First();
                        DtIReturTrans = bayar.TGL_TRANS;

                        txtIReturTglTrans.Text = DtIReturTrans.ToString("dd'/'MM'/'yyyy");
                        hdnIReturTgl.Value = DtIReturTrans.ToString("dd'/'MM'/'yyyy");
                        bindItemRetur();
                        //if (lblVoidNoVoid.Text == "VOID")
                        //{
                        //    lblVoidNoVoid.Text = "-";
                        //}
                        //else
                        //{
                        //    bindItemRetur();
                        //}
                    }
                    //Reprint existing PDF
                    else if (e.CommandName.ToLower() == "reprint")
                    {
                        String BONNMR = gvBON.Rows[rowIndex].Cells[3].Text;
                        RecreateStruck(BONNMR);
                        //string urlHost = HttpContext.Current.Request.Url.Host;
                        //string urlPort = urlHost == "localhost" ? "23963" : "5793";
                        //string url = "http://" + urlHost + ":" + urlPort + "/Bon/" + BONNMR + ".pdf";
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                        bindNoBonReprint();
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

        protected void gvBONPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvBON.PageIndex = e.NewPageIndex;
            bindNoBon();
        }

        protected void gvIReturCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string id = gvIRetur.DataKeys[rowIndex]["ID"].ToString();
                    string sLevel = Session["ULevel"].ToString().ToLower();
                    string Sessionkd = Session["UKode"].ToString().ToLower();
                    string sKode = "";
                    string sSHR = "";
                    SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                    SH_BAYAR shBayar = new SH_BAYAR();

                    MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                    MS_SHOWROOM show = new MS_SHOWROOM();
                    if (e.CommandName.ToLower() == "saverow")
                    {
                        shBayar = shBayarDA.getSHBayar(" WHERE ID = '" + hdnIReturIDBayar.Value + "'").FirstOrDefault();

                        //DateTime dt = Convert.ToDateTime(txtIReturTglTrans.Text);
                        string tgl = string.Format("{0:yyMM}", shBayar.TGL_TRANS);

                        //string whereLock = show.STATUS_SHOWROOM == "SIS" ? "lockSIS" : sLevel == "FSS" ? "lockFSS" : "lockFSS";
                        string whereLock = shBayar.STATUS_STORE == "SIS" ? "lockSIS" : sLevel == "FSS" ? "lockFSS" : "lockFSS";

                        if (cekLock(whereLock, tgl))
                        {
                            //Save to Temp_Struck
                            hdnIReturIDKdbrg.Value = gvIRetur.Rows[rowIndex].Cells[3].Text;
                            string barcode = gvIRetur.Rows[rowIndex].Cells[4].Text.Trim();
                            tbReturNoBon.Text = tbIReturNoBon.Text;
                            inputIntoTempStruckRetur(barcode);
                        }
                        else
                        {
                            DivMessage.InnerText = "Bulan Sudah Di Lock!";
                            DivMessage.Attributes["class"] = "warning";
                            DivMessage.Visible = true;
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

        protected void gvMain_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton imgCommand = (ImageButton)e.Row.FindControl("imgCommand");

                //if (e.Row.Cells[2].Text == "0")
                //{
                //    imgEdit.Visible = false;
                //    imgCommand.Visible = false;
                //}
            }
        }

        protected void btnPUSave_Click(object sender, EventArgs e)
        {
            string update = "Berhasil!";

            for (int i = 1; i < Convert.ToInt32(tbPUQty.Text); i++)
            {
                tbBarcode.Text = tbPUBarcode.Text;
                inputIntoTempStruck();
            }
            tbBarcode.Text = "";

            //MS_KDBRG_DA kdBrgDA = new MS_KDBRG_DA();
            //Int64 idStruck = Convert.ToInt64(hdnId.Value);
            //int qty = Convert.ToInt32(tbPUQty.Text);
            //string update = kdBrgDA.updateQty(qty, idStruck);

            if (update == "Berhasil!")
            {
                DivMessage.InnerText = "Update Berhasil!";
                DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                clearForm();
                bindGrid();

                tbBarcode.Focus();
            }
            else
            {
                DivMessage.InnerText = "Error : " + update;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            DateTime tglTrans = DateTime.Now;
            DateTime dtnow = DateTime.Now;
            string date = divStore.Visible ? tbDate.Text.ToString() : string.Format("{0:dd-MM-yyyy}", tglTrans);
            string sLevel = Session["ULevel"].ToString().ToLower();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }
            string whereLock = sLevel == "admin counter" ? " where NAME = 'lockSIS'" : sLevel == "admin sales" ? " where NAME = 'lockFSS'" : " where NAME = 'lockFSS'";
            int lockMove = Convert.ToInt32(loginDA.getParam(whereLock).VALUE);
            int dateMove = Convert.ToInt32(string.Format("{0:yyMM}", tglTrans));
            if (tglTrans > dtnow)
            {
                DivMessage.InnerText = "Tanggal Transaksi Tidak boleh lebih besar dari tanggal saat ini";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            else if (dateMove > lockMove || !divStore.Visible)
            {
                bool check = true;
                lbWarningSIS.Visible = false;
                if (divStore.Visible)
                {
                    if (sLevel == "admin counter")
                    {
                        if (tbDate.Text == "" || ddlStore.SelectedIndex == 0 || tbMarginSIS.Text == "")
                        {
                            lbWarningSIS.Visible = true;
                            check = false;
                        }
                    }
                    else if (sLevel == "admin sales")
                    {
                        if (tbDate.Text == "" || ddlStore.SelectedIndex == 0)
                        {
                            lbWarningSIS.Visible = true;
                            check = false;
                        }
                    }
                }

                if (check)
                {
                    hdnNoUrutBon.Value = "";
                    Session.Remove("INoBon");
                    hdnMemberDisc.Value = "0";
                    hdnEmpDisc.Value = "0";
                    btnMemberSave.Enabled = true;
                    Decimal totalPrice = 0;
                    int totalQty = 0;
                    string store = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedItem.Text : Session["UStore"].ToString();
                    string kodeStore = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedValue : Session["UKode"].ToString();

                    hdnIDStore.Value = cbStoreChange.Checked ? ddlStore.SelectedIndex.ToString() : "";
                    SH_BAYAR shBayar = new SH_BAYAR();
                    SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();

                    //List<NO_DOC> listNoDoc = new List<NO_DOC>();
                    //string whereDoc = string.Format(" where KODE = '{0}' and FLAG = 'SALE'", kodeStore);
                    //listNoDoc = shBayarDA.getNoDoc(whereDoc);

                    //hdnNoUrutBon.Value = listNoDoc.Count == 0 ? "1" : listNoDoc.Last().DIFF_YEAR == 1 ? "1" : (listNoDoc.Last().NO_URUT + 1).ToString();

                    foreach (GridViewRow item in gvMain.Rows)
                    {
                        totalPrice = totalPrice + Convert.ToDecimal(item.Cells[12].Text);
                        totalQty = totalQty + Convert.ToInt32(item.Cells[7].Text);
                    }
                    lbOtherIncome.Text = totalPrice < 0 ? totalPrice.ToString() : "0";
                    totalPrice = totalPrice < 0 ? 0 : totalPrice;

                    shBayar.KODE_CUST = store;
                    shBayar.KODE = kodeStore;
                    //shBayar.NET_BAYAR = totalPrice;
                    shBayar.STATUS_STORE = Session["ULevel"].ToString().ToLower() == "admin counter" ? "SIS" : "FSS";
                    shBayar.QTY = totalQty;
                    shBayar.CREATED_BY = Session["UName"].ToString();
                    shBayar.JM_CARD = 0;
                    shBayar.JM_VOUCHER = 0;
                    string newID = "";
                    string session = Session["IBayar"] == null ? "" : Session["IBayar"].ToString();
                    if (Session["IBayar"] == null)
                    {
                        Session["IBayar"] = "Insert";
                        newID = shBayarDA.insertBayarRetID(shBayar);
                        Session["IBayar"] = "Done";
                    }
                    if (!(newID.Contains("ERROR")))
                    {
                        tbBYRPrice.Text = string.Format("{0:0,0.00}", totalPrice); //changeCurr(totalPrice.ToString().Remove(totalPrice.ToString().Length - 3));
                        tbBYRQty.Text = totalQty.ToString();
                        hdnIdBYR.Value = newID;
                        DivMessage.Visible = false;

                        bindAcara();
                        DivBYRMessage.Visible = false;
                        ModalPaymentMethodAndAcara.Show();
                        hdnMemberStatus.Value = "";
                    }
                    else
                    {
                        DivMessage.InnerText = newID;
                        DivMessage.Attributes["class"] = "error";
                        DivMessage.Visible = true;
                    }
                }
            }
            else
            {
                DivMessage.InnerText = "Tanggal Sudah Di Lock Silakan Hubungi Administrator untuk penjelasaan lebih lanjut";
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            clearhiddenfieldmember();
        }

        protected void btnCancelClick(object sender, EventArgs e)
        {
            Session.Remove("INoBon");
            firstDelete();
            clearForm();
            bindGrid();
            deleteTempAcara();
            bindStore();
            clearSession();
            //clearhiddenfieldmember();
            Response.Redirect(Request.RawUrl);
            //Session.Remove("IJual");
            DivMessage.Visible = false;
        }

        protected void btnBYRSave_Click(object sender, EventArgs e)
        {
            hdnIdPAY.Value = hdnIdBYR.Value;
            hdnNPayIDBayar.Value = hdnIdBYR.Value;

            tbPAYQuantity.Text = tbBYRQty.Text;
            tbNPayQty.Text = tbBYRQty.Text;
            tbPAYChange.Text = "0";

            if (checkAcara(ddlBYRAcara.SelectedValue))
            {
                string sLevel = Session["ULevel"].ToString().ToLower();
                //if (sLevel != "admin counter")
                //{
                tbPAYPrice.Text = ddlBYRAcara.SelectedIndex == 0 ? tbBYRPrice.Text : string.Format("{0:0,0}", Convert.ToDouble(countTotalPrice().ToString()));

                #region oldPayment
                //if (rbBYRPay.SelectedValue == "Yes")//Bayar Menggunakan Kartu
                //{
                //    clearBank();
                //    divPAYCard.Visible = true;
                //    tbPAYPAY.ReadOnly = true;

                //    tbPAYPAY.Text = tbPAYPrice.Text.Replace(".00", "");
                //}
                //else//Bayar Menggunakan Cash
                //{
                //    inputBank();
                //    divPAYCard.Visible = false;
                //    tbPAYPAY.ReadOnly = false;

                //    tbPAYPAY.Text = "";
                //}
                #endregion
                tbNPayTotalPrice.Text = string.Format("{0:0,0.00}", Convert.ToDouble(countTotalPrice().ToString()));
                hdnNPayNilaiVoucher.Value = "0";
                DivNPayMessage.Visible = false;
                ModalNewInputPayment.Show();
                //}
                //else
                //{
                //    string noBon = updateSHBayar(Convert.ToString(0));
                //    //Insert SH_JUAL
                //    insertIntoSHJUAL();
                //    printStruck(noBon);
                //    Session.Remove("IBayar");
                //}
                //hdnPAYNilaiVoucher.Value = "0";
                //ModalInputPayment.Show();

                Session.Remove("IJual");
                Session.Remove("UStock");
                Session.Remove("Voucher");
                Session.Remove("ICard");
                Session.Remove("UCard");
            }
            else
            {
                ModalPaymentMethodAndAcara.Show();
            }
        }

        protected void bBYRClose_Click(object sender, EventArgs e)
        {
            SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
            Int64 idTemp = new Int64();
            idTemp = Convert.ToInt64(hdnIdBYR.Value);
            string del = string.Empty;
            if (!(Session["IBayar"] == null))
            {
                del = shBayarDA.deleteBayar(idTemp);
            }

            if (del == "Berhasil!")
            {
                clearMember();
                bindGrid();
            }
            else
            {
                DivMessage.InnerText = "Error : " + del;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            txtBiayaOngkir.Text = string.Empty;
            txtFreeOngkir.Text = string.Empty;
            HdnBiayaOngkir.Value = string.Empty;
            HdnFreeOngkir.Value = string.Empty;
            Session.Remove("IBayar");
            ModalPaymentMethodAndAcara.Hide();
        }

        protected void bPAYClose_Click(object sender, EventArgs e)
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            bayarDA.deleteVoucher(Convert.ToInt64(hdnIdPAY.Value));
            bayarDA.deleteAcaraValue(Convert.ToInt64(hdnIdPAY.Value));
            CARD_PAY_POS_TO_EDC_DA edcDA = new CARD_PAY_POS_TO_EDC_DA();
            edcDA.deleteCARD_PAY_POS_TO_EDC(Convert.ToInt64(hdnIdPAY.Value));

            ModalInputPayment.Hide();
            ModalPaymentMethodAndAcara.Show();
            //Cancel Mebatalkan Transaksi
            Session.Remove("INoBon");
            firstDelete();
            clearForm();
            bindGrid();
            deleteTempAcara();
            bindStore();
            clearSession();
            txtBiayaOngkir.Text = string.Empty;
            txtFreeOngkir.Text = string.Empty;
            HdnBiayaOngkir.Value = string.Empty;
            HdnFreeOngkir.Value = string.Empty;
            //clearhiddenfieldmember();
            Response.Redirect(Request.RawUrl);
            //Session.Remove("IJual");
            DivMessage.Visible = false;
        }

        protected void btnPAYVoucherClick(object sender, EventArgs e)
        {
            //if (!(tbPAYNoVoucher.Text == "") && !(tbPAYNilaiVoucher.Text == ""))
            if (!(tbPAYNoVoucher.Text == ""))
            {
                //if (addVoucher())
                if (newAddVoucherNoNilaiInput())
                {
                    tbPAYNoVoucher.Text = "";
                    tbPAYNilaiVoucher.Text = "";
                }
            }
            else
            {
                DivPAYMessage.InnerText = "Harap isi No Voucher dan Nilai Voucher!";
                DivPAYMessage.Attributes["class"] = "warning";
                DivPAYMessage.Visible = true;
            }
            ModalInputPayment.Show();
        }

        protected void btnPAYSave_Click(object sender, EventArgs e)
        {
            decimal harga = Convert.ToDecimal(tbPAYPrice.Text.Replace(",", ""));
            decimal bayar = Convert.ToDecimal(tbPAYPAY.Text.Replace(",", ""));
            decimal nilVoucher = tbPAYNilaiVoucher.Text == "" ? 0 : Convert.ToDecimal(tbPAYNilaiVoucher.Text.Replace(",", ""));
            decimal voucher = Convert.ToDecimal(hdnPAYNilaiVoucher.Value) + nilVoucher;
            decimal Ongkir = Convert.ToDecimal(HdnBiayaOngkir.Value);
            decimal FreeOngkir = Convert.ToDecimal(HdnFreeOngkir.Value);
            decimal finalOngkir;
            if (Ongkir - FreeOngkir > 0)
            {
                finalOngkir = Ongkir - FreeOngkir;
            }
            else
            {
                finalOngkir = 0;
            }
            bayar = bayar + voucher;
            harga = harga + finalOngkir;

            if (harga > bayar)
            {
                DivPAYMessage.InnerText = "Pembayaran lebih kecil daripada harga!";
                DivPAYMessage.Attributes["class"] = "warning";
                DivPAYMessage.Visible = true;

                ModalInputPayment.Show();
            }
            else
            {
                bool status = true;
                if (tbPAYNilaiVoucher.Text != "" && tbPAYNoVoucher.Text != "")
                {
                    status = addVoucher();
                }
                else if (!(tbPAYNilaiVoucher.Text == "" && tbPAYNoVoucher.Text == ""))
                {
                    status = false;
                    ModalInputPayment.Show();
                }

                if (status)
                {
                    string change = (bayar - harga).ToString();
                    paid(change);
                    DivPAYMessage.Visible = false;
                }
            }
        }

        protected void btnReturClick(object sender, EventArgs e)
        {
            divRetur.Visible = true;
            lblFlag.Text = "";
            bindNoBon();
        }

        protected void btnBONSearchClick(object sender, EventArgs e)
        {
            if (lblFlag.Text != "")
            {
                bindNoBonReprint();
            }
            else
            {
                bindNoBon();
            }
        }

        protected void btnNPayVoucherClick(object sender, EventArgs e)
        {
            //if (!(tbNPayNoVou.Text == "") && !(tbNPayNilaiVou.Text == ""))
            if (!(tbNPayNoVou.Text == ""))
            {
                //if (newAddVoucher())
                if (newAddVoucherNoNilaiInput())
                {
                    tbNPayNoVou.Text = "";
                    tbNPayNilaiVou.Text = "";
                }
            }
            else
            {
                DivNPayMessage.InnerText = "Harap isi No Voucher dan Nilai Voucher!";
                DivNPayMessage.Attributes["class"] = "warning";
                DivNPayMessage.Visible = true;
            }
            ModalNewInputPayment.Show();
        }

        protected void btnNPaySaveClick(object sender, EventArgs e)
        {
            if ((tbNPayNilaiVou.Text == "" || tbNPayNilaiVou.Text == "0") && tbNPayNoVou.Text == "")
            {
                string nPay = tbNPayPay.Text == "" ? "0" : tbNPayPay.Text.Replace(",", "");

                decimal totalPrice = Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", ""));
                decimal bayar = Convert.ToDecimal(nPay);
                string nOngkir = txtBiayaOngkir.Text == "" ? "0" : txtBiayaOngkir.Text.Replace(",", "");
                string nFreeOngkir = txtFreeOngkir.Text == "" ? "0" : txtFreeOngkir.Text.Replace(",", "");
                decimal ttlOngkir = Convert.ToDecimal(nOngkir);
                decimal ttlFreeOngkir = Convert.ToDecimal(nFreeOngkir);
                HdnBiayaOngkir.Value = nOngkir;
                HdnFreeOngkir.Value = nFreeOngkir;
                decimal finalOngkir;
                if (ttlOngkir - ttlFreeOngkir > 0)
                {
                    finalOngkir = ttlOngkir - ttlFreeOngkir;
                }
                else
                {
                    finalOngkir = 0;
                }
                decimal ttlPriceOngkir = totalPrice + finalOngkir;
                //nPay
                //var d = Convert.ToDecimal(nPay, new CultureInfo("id-ID"));
                //if (((bayar < totalPrice) && cbNPayCard.Checked) || (!(bayar < totalPrice) && !(cbNPayCard.Checked)))
                if (((bayar < ttlPriceOngkir) && cbNPayCard.Checked) || (!(bayar < ttlPriceOngkir) && !(cbNPayCard.Checked)))
                {
                    if (cbNPayCard.Checked)//Pembayaran Cash Lebih Kecil dan Ada Pembayaran dengan Kartu
                    {
                        //double a = 0.125;
                        ////decimal separator will always be '.'
                        //string txt = a.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        ////back to a double
                        // double a2 = double.Parse(txt, System.Globalization.CultureInfo.InvariantCulture);

                        //decimal payDec = totalPrice - bayar;
                        decimal payDec = ttlPriceOngkir - bayar;

                        //string pay = Convert.ToString(totalPrice - bayar);
                        string pay = Convert.ToString(ttlPriceOngkir - bayar);
                        //pay = changeCurr(pay.Replace(".00", ""));
                        pay = string.Format("{0:0,0.00}", payDec);
                        hdnNCardValueCash.Value = bayar.ToString();
                        hdnNCardValueVoucher.Value = hdnNPayNilaiVoucher.Value;

                        tbNCardPrice.Text = pay;
                        tbNCardPay.Text = pay;
                        tbNCardQty.Text = tbNPayQty.Text;

                        DivNCardMessage.Visible = false;
                        ModalNewCard.Show();
                    }
                    else
                    {
                        //Update SH_Bayar
                        //string noBon = updateSHBayar(Convert.ToString(bayar - totalPrice));
                        string noBon = updateSHBayar(Convert.ToString(bayar - ttlPriceOngkir));
                        //Insert SH_JUAL
                        insertIntoSHJUAL();
                        printStruck(noBon);
                        Session.Remove("IBayar");
                    }
                }
                else
                {
                    DivNPayMessage.InnerText = "Harap Periksa Kembali Metode Pembayaran dan Jumlah Pembayaran!";
                    DivNPayMessage.Attributes["class"] = "warning";
                    DivNPayMessage.Visible = true;
                    ModalNewInputPayment.Show();
                }
            }
            else
            {
                DivNPayMessage.InnerText = "Harap Simpan Voucher Terlebih Dahulu!";
                DivNPayMessage.Attributes["class"] = "warning";
                DivNPayMessage.Visible = true;
                ModalNewInputPayment.Show();
            }
        }

        protected void btnNCardCardClick(object sender, EventArgs e)
        {
            decimal totalPrice = Convert.ToDecimal(tbNCardPrice.Text.Replace(",", ""));
            decimal payCard = Convert.ToDecimal(tbNCardPay.Text.Replace(",", ""));
            if (payCard < totalPrice)
            {
                //Masukin ke SH_Card
                insertIntoSHCard("card", "card");
                decimal payDec = totalPrice - payCard;
                string pay = Convert.ToString(totalPrice - payCard);
                pay = string.Format("{0:0,0.00}", payDec);
                tbNCardPrice.Text = pay;
                tbNCardNoCard.Text = "";
                ddlNCardKodeCard.SelectedIndex = 0;
                tbNCardVLCard.Text = "";
                //tbNCardBank.Text = "";
                ddlCardName.SelectedIndex = 0;
                tbNCardPay.Text = tbNCardPrice.Text;
                tbNCardPay.Enabled = false;
                btnNCardCard.Enabled = false;
                DivNCardMessage.Visible = false;
            }
            else
            {
                DivNCardMessage.InnerText = "Pembayaran dengan Kartu tambahan, Kartu pertama harus lebih kecil dari total pembayaran!";
                DivNCardMessage.Attributes["class"] = "warning";
                DivNCardMessage.Visible = true;
            }
            ModalNewCard.Show();
        }

        protected void btnNCardSaveClick(object sender, EventArgs e)
        {
            decimal totalPrice = Convert.ToDecimal(tbNCardPrice.Text.Replace(",", ""));
            decimal payCard = Convert.ToDecimal(tbNCardPay.Text.Replace(",", ""));
            //Update SH_Bayar
            if (ddlNCardEdc.SelectedValue == "Member" || ddlCardName.SelectedItem.Text == "MEMBERSHIP" || ddlNCardKodeCard.SelectedValue == "Membership")
            {
                if (ddlNCardEdc.SelectedValue == "Member" && ddlCardName.SelectedItem.Text == "MEMBERSHIP" && ddlNCardKodeCard.SelectedValue == "Membership")
                {
                    if (payCard == totalPrice)
                    {
                        insertIntoSHCard("save", "save");

                        string noBon = updateSHBayar(Convert.ToString(payCard - totalPrice));

                        //Insert SH_JUAL
                        insertIntoSHJUAL();
                        printStruck(noBon);
                        Session.Remove("IBayar");
                        DivNCardMessage.Visible = false;
                    }
                    else
                    {
                        DivNCardMessage.InnerText = "Pembayaran dengan Kartu tidak boleh melebihi atau kurang dari total harga!";
                        DivNCardMessage.Attributes["class"] = "warning";
                        DivNCardMessage.Visible = true;
                        ModalNewCard.Show();
                        btnNCardSave.Enabled = true;
                    }
                }
                else
                {
                    DivNCardMessage.InnerText = "Pembayarn Menggunakan Membership Mohon Cek Kembali Data Yang di Input!";
                    DivNCardMessage.Attributes["class"] = "warning";
                    DivNCardMessage.Visible = true;
                    ModalNewCard.Show();
                    btnNCardSave.Enabled = true;
                }
            }
            else
            {
                if (payCard == totalPrice)
                {
                    insertIntoSHCard("save", "save");

                    string noBon = updateSHBayar(Convert.ToString(payCard - totalPrice));

                    //Insert SH_JUAL
                    insertIntoSHJUAL();
                    printStruck(noBon);
                    Session.Remove("IBayar");
                    DivNCardMessage.Visible = false;
                }
                else
                {
                    DivNCardMessage.InnerText = "Pembayaran dengan Kartu tidak boleh melebihi atau kurang dari total harga!";
                    DivNCardMessage.Attributes["class"] = "warning";
                    DivNCardMessage.Visible = true;
                    ModalNewCard.Show();
                    btnNCardSave.Enabled = true;
                }
            }
        }

        protected void btnBYRMemberClick(object sender, EventArgs e)
        {
            tbMemberNumber.Text = "";
            ModalMember.Show();
        }

        protected void btnBYREmployeeClick(object sender, EventArgs e)
        {
            DivEmpMessage.Visible = false;
            tbEmpNoCard.Text = "";
            tbEmpName.Text = "";
            tbEmpPosition.Text = "";
            ModalEmployee.Show();
        }

        protected void bMemberCloseClick(object sender, EventArgs e)
        {
            hdncountwrongpin.Value = "";
            ModalPaymentMethodAndAcara.Show();
        }

        protected void btnMemberSaveClick_OLD(object sender, EventArgs e)
        {
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<MS_MEMBER> listMember = new List<MS_MEMBER>();

            string nama = "";//tbMemberFirst.Text;
            string namaLast = "";//tbMemberLast.Text;
            string phone = "";//tbMemberPhone.Text + tbMemberPhone2.Text;
            listMember = bayarDA.getMember(string.Format(" where (FIRST_NAME = '{0}' and LAST_NAME = '{2}') or (PHONE = '{1}' and PHONE != '')", nama, phone, namaLast));
            Int64 idTemp = Convert.ToInt64(hdnIdBYR.Value);
            decimal totalPrice = Convert.ToDecimal(tbBYRPrice.Text.Replace(",", "").Replace(".00", ""));

            if (listMember.Count == 0)
            {
                divMember.InnerText = "Member tidak terdaftar!";
                divMember.Attributes["class"] = "warning";
                divMember.Visible = true;
                ModalMember.Show();
            }
            else if (listMember.First().STATUS_MEMBER != "active")
            {
                divMember.InnerText = "Member Belum Terverifikasi!";
                divMember.Attributes["class"] = "warning";
                divMember.Visible = true;
                ModalMember.Show();
            }
            else
            {
                hdnMemberDisc.Value = "15";
                hdnEmpDisc.Value = "0";
                //decimal totalPrice = Convert.ToDecimal(tbBYRPrice.Text.Replace(",", "").Replace(".00",""));
                //decimal discTotalMember = 0;
                //int discMember = int.Parse(hdnMemberDisc.Value == "" ? "0" : hdnMemberDisc.Value);
                //discTotalMember = (totalPrice * discMember) / 100;
                //totalPrice = totalPrice - discTotalMember;
                //tbBYRPrice.Text = changeCurr(totalPrice.ToString());
                //btnMemberSave.Enabled = false;

                //foreach (GridViewRow item in gvMain.Rows)
                //{
                //    //yang di rubah DISCOUNT, NET_PRICE, MEMBER, RETUR yang bukan retur
                //    if (item.Cells[21].Text == "No")
                //    {
                //        string id = gvMain.DataKeys[item.RowIndex]["ID"].ToString();
                //        Int64 IDTemp = Convert.ToInt64(id);
                //        bayarDA.updateMember(IDTemp);
                //    }
                //}
                string user = Session["UName"] == null ? "" : Session["UName"].ToString();
                TEMP_STRUCK struck = new TEMP_STRUCK();
                struck.CREATED_BY = user;
                struck.MEMBER = "Yes";
                struck.EPC = "No";
                bayarDA.updateMember(struck);

                clearMember();

                SH_MEMBER shMember = new SH_MEMBER();
                shMember.ID_BAYAR = idTemp;
                shMember.ID_MEMBER = listMember.First().ID;
                shMember.NAMA = listMember.First().FIRST_NAME;
                shMember.PHONE = listMember.First().PHONE;
                shMember.NO_BON = "";
                shMember.NET_BAYAR = 0;
                //shMember.DISC_RATE = divStore.Visible ? ddlStore.SelectedValue.Contains("LWI") ? 15 : 10 : Session["UKode"].ToString().Contains("LWI") ? 15 : 10;
                //shMember.DISC_PRICE = divStore.Visible ? ddlStore.SelectedValue.Contains("LWI") ? totalPrice * 15 / 100 : totalPrice * 10 / 100 :
                //    Session["UKode"].ToString().Contains("LWI") ? totalPrice * 15 / 100 : totalPrice * 10 / 100;
                shMember.DISC_RATE = 10;
                shMember.DISC_PRICE = totalPrice * 10 / 100;
                shMember.CREATED_BY = user;

                bayarDA.insertShMember(shMember);

                ModalPaymentMethodAndAcara.Show();
                bindGrid();
                tbBYRPrice.Text = string.Format("{0:0,0.00}", Convert.ToDouble(countTotalPrice().ToString()));
            }
        }

        protected void btnMemberSaveClick(object sender, EventArgs e)
        {
            //Check dulu ini untuk get poin atau redeem
            #region "Cek Blocked Pin, Masa Tenggang & Expired Member"
            MS_MEMBER itemmember = new MS_MEMBER();
            MEMBER_DA memberDA = new MEMBER_DA();
            DateTime datetrx = Convert.ToDateTime(tbDate.Text);
            itemmember = memberDA.getStatusAktifMember(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
            if (itemmember.WRONG_PIN == 3)
            {
                divMember.InnerText = "Pin TerBlokir! silakan Unblock malalui website 909 Membership!";
                divMember.Attributes["class"] = "warning";
                divMember.Visible = true;
                //btnMemberSave.Enabled = false;
                ModalMember.Show();
                
            }
            else if (datetrx.Date > itemmember.TGL_MASA_TENGGANG && datetrx.Date < itemmember.TGL_EXPIRED)
            {
                divMember.InnerText = "Kartu Member Sudah Memasuki Masa Tenggang! silakan Perbaharui Kartu malalui website 909 Membership!";
                divMember.Attributes["class"] = "warning";
                divMember.Visible = true;
                btnMemberSave.Enabled = false;
                ModalMember.Show();
            }
            else if (datetrx.Date > itemmember.TGL_MASA_TENGGANG && datetrx.Date > itemmember.TGL_EXPIRED)
            {
                divMember.InnerText = "Kartu Member Sudah Tidak Dapat Digunakan Karena Masa Berlaku Telah Habis! silakan Melakukan Registrasi Kartu Baru malalui website 909 Membership!";
                divMember.Attributes["class"] = "warning";
                divMember.Visible = true;
                btnMemberSave.Enabled = false;
                ModalMember.Show();
            }
            #endregion
            else
            {
                #region " get poin Point"
                if (tbMemberPoin.Text.Trim() == "")
                {
                    //Hanya untuk get poin
                    //Check valid atau ngak membernya

                    List<MS_MEMBER> listMember = new List<MS_MEMBER>();

                    listMember = memberDA.getMember(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
                    if (listMember.Count > 0)
                    {
                        MS_MEMBER member = new MS_MEMBER();
                        member = listMember.First();
                        GLOBALCODE gcFunc = new GLOBALCODE();
                        string pin = gcFunc.Encrypt(tbMemberPIN.Text.Trim());
                        

                        if (member.PIN == pin)
                        {
                            hdnMemberID.Value = member.ID.ToString();
                            hdnMemberNumber.Value = member.NO_CARD.ToString();
                            hdnMemberStatus.Value = "RP";
                            //slhpin = 0;
                            lblWrongPin.Text = "0";
                            ModalPaymentMethodAndAcara.Show();
                            hdncountwrongpin.Value = "";
                        }
                        else
                        {
                            //No member tidak terdaftar
                            divMember.InnerText = "Pin Salah!";
                            divMember.Attributes["class"] = "warning";
                            divMember.Visible = true;
                            ModalMember.Show();
                            lblWrongPin.Text = Convert.ToString(Convert.ToInt32(lblWrongPin.Text) + 1);
                            //lblWrongPin.Text = Convert.ToString(slhpin);
                            //if (hdncountwrongpin.Value == "")
                            //{
                            //    hdncountwrongpin.Value = "1";
                            //}
                            //else
                            //{
                            //    hdncountwrongpin.Value = Convert.ToString(Convert.ToInt32(hdncountwrongpin.Value) + 1);//hdncountwrongpin.Value + 1;
                            //}
                            //int slhpin = Convert.ToInt32(hdncountwrongpin.Value);
                            if (lblWrongPin.Text == "3")
                            {
                                
                                string res = memberDA.UpCustCardBlockPIN(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
                                //btnMemberSave.Enabled = false;
                                ModalMember.Hide();
                                DivBYRMessage.InnerText = "Pin TerBlokir! silakan Unblock malalui website 909 Membership!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ModalPaymentMethodAndAcara.Show();
                            }
                        }
                    }
                    else
                    {
                        //No member tidak terdaftar
                        divMember.InnerText = "Member Tidak Terdaftar!";
                        divMember.Attributes["class"] = "warning";
                        divMember.Visible = true;
                        ModalMember.Show();
                    }
                }
                #endregion
                #region " Redeem Point"
                else
                {
                    //Untuk getpoint dan Redeem Point
                    //Check valid atau tidak nya nomer member beserta dengan PIN member                
                    //MEMBER_DA memberDA = new MEMBER_DA();
                    List<MS_MEMBER> listMember = new List<MS_MEMBER>();
                    listMember = memberDA.getMember(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
                    if (listMember.Count > 0) 
                    {
                        MS_MEMBER member = new MS_MEMBER();
                        member = listMember.First();
                        GLOBALCODE gcFunc = new GLOBALCODE();
                        string noMember = tbMemberNumber.Text.Trim();
                        string pin = gcFunc.Encrypt(tbMemberPIN.Text.Trim());
                        string point = tbMemberPoin.Text.Trim();
                        if (member.PIN == pin)
                        {
                            //Valid redeem
                            hdnMemberNumber.Value = noMember;
                            hdnMemberNilai.Value = point;
                            hdnMemberStatus.Value = "RD";
                            ModalPaymentMethodAndAcara.Show();
                            bindGrid();
                            tbBYRPrice.Text = string.Format("{0:0,0.00}", Convert.ToDouble(countTotalPrice().ToString()));
                        }
                        else
                        {
                            //No member tidak terdaftar
                            divMember.InnerText = "Pin Salah!";
                            divMember.Attributes["class"] = "warning";
                            divMember.Visible = true;
                            ModalMember.Show();
                            lblWrongPin.Text = Convert.ToString(Convert.ToInt32(lblWrongPin.Text) + 1);
                            //lblWrongPin.Text = Convert.ToString(slhpin);
                            //if (hdncountwrongpin.Value == "")
                            //{
                            //    hdncountwrongpin.Value = "1";
                            //}
                            //else
                            //{
                            //    hdncountwrongpin.Value = Convert.ToString(Convert.ToInt32(hdncountwrongpin.Value) + 1);//hdncountwrongpin.Value + 1;
                            //}
                            //int slhpin = Convert.ToInt32(hdncountwrongpin.Value);
                            if (lblWrongPin.Text == "3")
                            {

                                string res = memberDA.UpCustCardBlockPIN(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
                                //btnMemberSave.Enabled = false;
                                ModalMember.Hide();
                                DivBYRMessage.InnerText = "Pin TerBlokir! silakan Unblock malalui website 909 Membership!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ModalPaymentMethodAndAcara.Show();
                            }
                        }
                    }
                    
                    #region "OLD"
                    //Harus ada checking dia tidak sedang dalam status redeem

                    //int checkMember = memberDA.checkCard(noMember, pin, Convert.ToInt32(point), Session["UKode"].ToString(), Session["UName"].ToString());
                    //if (checkMember == 1)
                    //{
                    //    //Valid redeem
                    //    hdnMemberNumber.Value = noMember;
                    //    hdnMemberNilai.Value = point;
                    //    hdnMemberStatus.Value = "RD";

                    //    ModalPaymentMethodAndAcara.Show();
                    //    bindGrid();
                    //    tbBYRPrice.Text = string.Format("{0:0,0.00}", Convert.ToDouble(countTotalPrice().ToString()));
                    //    hdncountwrongpin.Value = "";
                    //}
                    //else if (checkMember == -1)
                    //{

                    //    //if (hdncountwrongpin.Value == "")
                    //    //{
                    //    //    hdncountwrongpin.Value = "1";
                    //    //}
                    //    //else
                    //    //{
                    //    //    hdncountwrongpin.Value = Convert.ToString(Convert.ToInt32(hdncountwrongpin.Value) + 1);//hdncountwrongpin.Value + 1;
                    //    //}
                    //    //int slhpin = Convert.ToInt32(hdncountwrongpin.Value);
                    //    //if (slhpin == 3)
                    //    //{
                    //    //    divMember.InnerText = "Pin TerBlokir! silakan Unblock malalui website 909 Membership!";
                    //    //    divMember.Attributes["class"] = "warning";
                    //    //    divMember.Visible = true;
                    //    //    string res = memberDA.UpCustCardBlockPIN(" where STATUS_VALIDASI = 'DONE' and NO_CARD = '" + tbMemberNumber.Text.Trim() + "'");
                    //    //    btnMemberSave.Enabled = false;
                    //    //    ModalMember.Show();
                    //    //}
                    //    //else
                    //    //{
                    //        divMember.InnerText = "Pin atau No Member Salah!";
                    //        divMember.Attributes["class"] = "warning";
                    //        divMember.Visible = true;
                    //        ModalMember.Show();
                    //    //}
                        
                    //}
                    //else if (checkMember == -2)
                    //{
                    //    divMember.InnerText = "Point yang diredeem tidak mencukupi!";
                    //    divMember.Attributes["class"] = "warning";
                    //    divMember.Visible = true;
                    //    ModalMember.Show();
                    //}
                    //else if (checkMember == -3)
                    //{
                    //    divMember.InnerText = "Data member sedang melakukan transaksi redeem point! silakan Clear data Transaksi malalui website 909 Membership!";
                    //    divMember.Attributes["class"] = "warning";
                    //    divMember.Visible = true;
                    //    ModalMember.Show();
                    //}
                    //else if (checkMember == -4)
                    //{
                    //    divMember.InnerText = "No Member Sudah diblockir, Harap hubungi card center.";
                    //    divMember.Attributes["class"] = "warning";
                    //    divMember.Visible = true;
                    //    ModalMember.Show();
                    //}
                    #endregion
                }
                #endregion
            }
            
        }

        protected void bEmpCloseClick(object sender, EventArgs e)
        {
            ModalPaymentMethodAndAcara.Show();
        }

        protected void btnEmpSaveClick(object sender, EventArgs e)
        {
            try
            {
                //Clear Staff
                DivEmpMessage.Visible = false;
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                SH_EMPLOYEE emp = new SH_EMPLOYEE();
                List<MS_DISCOUNT_SHR> listDisc = new List<MS_DISCOUNT_SHR>();
                MS_DISCOUNT_SHR discShr = new MS_DISCOUNT_SHR();
                string sLevel = Session["ULevel"].ToString();
                string sKode = sLevel.ToLower() == "sales" || sLevel.ToLower() == "store manager" ? Session["UKode"].ToString() : ddlStore.SelectedValue;

                //if ((tbEmpPosition.Text == "General Manager" || tbEmpPosition.Text == "Konsultan" || tbEmpPosition.Text == "Direktur") && tbEmpDisc.Text.Trim() == "")
                if (1 == 0)
                {
                    DivEmpMessage.InnerText = "Untuk Position ini harap isi Discount!";
                    DivEmpMessage.Visible = true;
                    DivEmpMessage.Attributes["class"] = "warning";
                    ModalEmployee.Show();
                }
                else
                {
                    listDisc = bayarDA.getListDiscShr(" where status = 1 and KODE = '" + sKode + "' and TIPE = '" + tbEmpTipe.Text + "' and TIPE_DISCOUNT = '" + hdnEmpEpc.Value.ToString().Trim() + "'");
                    if (listDisc.Count > 0)
                    {
                        discShr = listDisc.First();
                        //tbEmpDisc.Text = tbEmpPosition.Text == "General Manager" || tbEmpPosition.Text == "Konsultan" || tbEmpPosition.Text == "Direktur" ?
                        //    tbEmpDisc.Text : discShr.DISCOUNT.ToString();
                        tbEmpDisc.Text = discShr.DISCOUNT.ToString();

                        string nama = tbEmpName.Text;
                        string position = tbEmpPosition.Text;

                        Int64 idTemp = Convert.ToInt64(hdnIdBYR.Value);
                        decimal totalPrice = Convert.ToDecimal(tbBYRPrice.Text.Replace(",", "").Replace(".00", ""));

                        hdnMemberDisc.Value = "0";
                        //hdnEmpDisc.Value = "15";
                        //Direktur Konsultan General Manager

                        //hdnEmpDisc.Value = Session["UName"].ToString().ToLower() == "ridwan_admin" && string.Format("{0:dd/MM/yyyy}", DateTime.Now) == "09/05/2016"
                        //    ? tbEmpDisc.Text : "30";
                        hdnEmpDisc.Value = hdnEmpStatus.Value == "1"
                            ? tbEmpDisc.Text : "30";
                        //hdnEmpDisc.Value = tbEmpDisc.Text;

                        string user = Session["UName"] == null ? "" : Session["UName"].ToString();
                        TEMP_STRUCK struck = new TEMP_STRUCK();
                        struck.CREATED_BY = user;
                        struck.MEMBER = "No";
                        struck.EPC = "Yes";

                        //struck.DISC_TEMP = Session["UName"].ToString().ToLower() == "ridwan_admin" && string.Format("{0:dd/MM/yyyy}", DateTime.Now) == "09/05/2016"
                        //    ? Convert.ToInt32(tbEmpDisc.Text == "" ? "0" : tbEmpDisc.Text) : 30;
                        struck.DISC_TEMP = hdnEmpStatus.Value == "1"
                            ? Convert.ToInt32(tbEmpDisc.Text == "" ? "0" : tbEmpDisc.Text) : 30;

                        //struck.DISC_TEMP = Convert.ToInt32(tbEmpDisc.Text == "" ? "0" : tbEmpDisc.Text);
                        bayarDA.updateMember(struck);

                        clearMember();

                        SH_EMPLOYEE shEmployee = new SH_EMPLOYEE();
                        //ID_BAYAR, NAME, POSITION, CREATED_BY, CREATED_DATE
                        shEmployee.ID_BAYAR = idTemp;
                        shEmployee.NAME = tbEmpName.Text;
                        shEmployee.POSITION = tbEmpPosition.Text;
                        shEmployee.CREATED_BY = user;

                        //CHANGE HERE
                        shEmployee.NIK = tbEmpNoCard.Text;
                        //END HERE

                        bayarDA.insertShEmployee(shEmployee);

                        ModalPaymentMethodAndAcara.Show();
                        bindGrid();
                        tbBYRPrice.Text = string.Format("{0:0,0.00}", Convert.ToDouble(countTotalPrice().ToString()));


                        //
                    }
                    else
                    {
                        DivEmpMessage.InnerText = "Kode Showroom Tidak Memiliki master Discount!";
                        DivEmpMessage.Visible = true;
                        DivEmpMessage.Attributes["class"] = "warning";
                        ModalEmployee.Show();
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSSaveClick(object sender, EventArgs e)
        {
            inputServiceTempStruck();
        }

        protected void bDoneGiftReciptClick(object sender, EventArgs e)
        {
            string noBon = lbDONEBON.Text.Trim();
            createGiftStruck(noBon);
            ModalChange.Show();
        }

        protected void bDONEClose_Click(object sender, EventArgs e)
        {
            firstDelete();
            clearForm();
            bindGrid();
            deleteTempAcara();
            bindStore();
            clearSession();
            //Response.Redirect(Request.RawUrl);
            DivMessage.InnerText = "Pembayaran Berhasil!";
            DivMessage.Attributes["class"] = "success";
            DivMessage.Visible = true; 
        }

        protected void btnNoSaleClick(object sender, EventArgs e)
        {
            string sName = Session["UName"].ToString();

            string sLevel = Session["ULevel"].ToString();
            string sKode = Session["UKode"].ToString();
            string sStore = Session["UStore"].ToString();
            string sIdStore = Session["UIdStore"].ToString();
            DateTime sDate = DateTime.Now;
            Boolean boolStore = true;

            string date = tbDate.Text.ToString() == "" ? string.Format("{0:dd-MM-yyyy}", DateTime.Now) : tbDate.Text.ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate);
            }

            if (sLevel.ToLower() == "admin sales" || sLevel.ToLower() == "admin counter")
            {
                //Change Kode & Nama Store
                sKode = ddlStore.SelectedValue;

                List<MS_SHOWROOM> listShow = new List<MS_SHOWROOM>();
                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

                listShow = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).ToList<MS_SHOWROOM>();
                if (listShow.Count > 0)
                {
                    MS_SHOWROOM show = new MS_SHOWROOM();
                    show = listShow.First();

                    sStore = show.SHOWROOM;
                    sIdStore = show.ID.ToString();
                }
                else
                {
                    boolStore = false;

                    DivMessage.InnerText = "Error : Showroom tidak ditemukan!";
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                }               
            }

            if (boolStore)
            {
                List<NO_SALE> listNoSale = new List<NO_SALE>();
                List<String> listTanggalSale = new List<String>();
                List<SH_BAYAR> listSale = new List<SH_BAYAR>();
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();

                NO_SALE noSale = new NO_SALE();
                noSale.KODE = sKode;
                noSale.NO_SALE_DATE = sDate;

                listNoSale = bayarDA.getListNoSaleByKodeAndDate(noSale, "");

                if (listNoSale.Count == 0)
                {
                    SH_BAYAR bayar = new SH_BAYAR();
                    bayar.KODE = sKode;
                    bayar.TGL_TRANS = sDate;

                    listSale = bayarDA.getSHBayarByKodeAndDate(bayar, "");

                    if (listSale.Count == 0)
                    {
                        //Insert Data
                        //ID_SHOWROOM, SHOWROOM, CREATED_BY, CREATED_DATE, STATUS
                        noSale.ID_SHOWROOM = Convert.ToInt64(sIdStore);
                        noSale.SHOWROOM = sStore;
                        noSale.CREATED_BY = sName;

                        bayarDA.insertNoSale(noSale);

                        DivMessage.InnerText = string.Format("Penambahan No Sale untuk tanggal {0:dd-MM-yyyy} berhasil", sDate);
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                    else
                    {
                        DivMessage.InnerText = string.Format("Tanggal {0: dd-MM-yyyy} sudah ada penjualan", sDate);
                        DivMessage.Attributes["class"] = "warning";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Data No Sale sudah ada di sistem!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
        }

        protected void btnVoidClick(object sender, EventArgs e)
        {
            lblFlag.Text = "";
            //lblVoidNoVoid.Text = "VOID";
            divVoid.Visible = true;
            bindNoBon();
        }
        protected bool cekLock(string lockParam, string time)
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_PARAMETER> listParam = new List<MS_PARAMETER>();
            bool ret = true;

            listParam = loginDA.getListParam(string.Format(" where name in ('{0}')", lockParam));

            //time = 1601 ; param1 = 1510 ; param2 = 1601
            foreach (MS_PARAMETER item in listParam)
            {

                int nowTime = Convert.ToInt32(time);
                int value = Convert.ToInt32(item.VALUE);
                if (!(nowTime > value))
                {
                    ret = false;
                }
            }

            return ret;
        }
        protected void btnIReturVoidClick(object sender, EventArgs e)
        {
            if (ddlStore.SelectedIndex > 0)
            {
                List<SH_BAYAR> bayarList = new List<SH_BAYAR>();
                SH_BAYAR bayar = new SH_BAYAR();
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                MS_SHOWROOM show = new MS_SHOWROOM();

                string sLevel = Session["ULevel"].ToString().ToLower();

                string whereLock = sLevel == "admin counter" ? "lockSIS" : sLevel == "admin sales" ? "lockFSS" : "lockFSS";
                bayarList = bayarDA.getSHBayar(" where ID = '" + hdnIReturIDBayar.Value.Trim() + "'");
                bayar = bayarList.First();
                //DateTime dt = DateTime.Now;\
                 DateTime dt = Convert.ToDateTime(bayar.TGL_TRANS);

                //string tgl = string.Format("{0:yyMM}", dt);
                string tgl = string.Format("{0:yyMM}", dt);


                if (cekLock(whereLock, tgl))
                {
                    string noBon = tbIReturNoBon.Text;
                    string sUser = Session["UName"].ToString();
                    string sKode = ddlStore.SelectedValue;
                    string idStore = "";

                    //bayarList = bayarDA.getSHBayar(" where NO_BON = '" + noBon + "'");

                    bayar.VOID_BY = sUser;
                    sKode = bayar.KODE;

                    //Update No Bon yang baru dari Document dan Update Doc
                    List<NO_DOC> listNoDoc = new List<NO_DOC>();
                    string whereDoc = string.Format(" where KODE = '{0}' and FLAG = 'SALE'", sKode);
                    listNoDoc = bayarDA.getNoDoc(whereDoc);

                    string noUrutID = listNoDoc.Count == 0 ? "1" : listNoDoc.Last().DIFF_YEAR == 1 ? "1" : (listNoDoc.Last().NO_URUT + 1).ToString();
                    string id = noUrutID.Length > 4 ? noUrutID.Remove(0, noUrutID.Length - 4) : noUrutID.PadLeft(4, '0');

                    show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).First();
                    idStore = show.ID.ToString();
                    idStore = idStore.Length > 3 ? idStore.Remove(0, idStore.Length - 3) : idStore.PadLeft(3, '0');

                    string newNoBon = tgl + idStore + id;

                    bayar.NO_BON = newNoBon;
                    string newID = bayarDA.updateVoidHeader(bayar);

                    Int64 newIdBayar = 0;
                    if (Int64.TryParse(newID, out newIdBayar))
                    {
                        NO_DOC noDoc = listNoDoc.Last();
                        noDoc.NO_URUT = int.Parse(noUrutID);
                        string ret = noUrutID == "1" ? bayarDA.insertNoDoc(noDoc) : bayarDA.updateNoDoc(noDoc);

                        bayarDA.updateVoidDetail(bayar, newIdBayar);

                        DivMessage.InnerText = "Void Data : " + noBon + " Berhasil!";
                        DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Bulan Sudah Di Lock!";
                    DivMessage.Attributes["class"] = "warning";
                    DivMessage.Visible = true;
                }
            }
            else
            {
                DivMessage.InnerText = "Pilih Showroom terlebih dahulu untuk void data!";
                DivMessage.Attributes["class"] = "warning";
                DivMessage.Visible = true;
            }
        }

        protected void bDoneReceiptClick(object sender, EventArgs e)
        {
            string urlStruck = "..\\Bon\\" + lbDONEBON.Text + ".pdf";
            urlStruck = "http://localhost:46151/Bon/15300900038.pdf";
            String js = "window.open('" + urlStruck + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open Signature.aspx", js, true);
        }

        protected void tbEmpNoCard_TextChanged(object sender, EventArgs e)
        {
            DivEmpMessage.Visible = false;
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<MS_EMPLOYEE> listEmployee = new List<MS_EMPLOYEE>();
            MS_EMPLOYEE employee = new MS_EMPLOYEE();

            listEmployee = bayarDA.getEmployee(" where NIK = '" + tbEmpNoCard.Text + "' and STATUS = 1");
            if (listEmployee.Count > 0)
            {
                decimal nilaiLimit = countNilaiPrice() * Convert.ToDecimal(0.6);
                employee = listEmployee.First();
                decimal sisaLimit = employee.SISA_LIMIT;

                if (nilaiLimit > sisaLimit)
                {
                    DivEmpMessage.InnerText = "Sisa limit EPC tidak mencukupi untuk melakukan pembelian barang ini!!!";
                    DivEmpMessage.Visible = true;
                    DivEmpMessage.Attributes["class"] = "warning";
                }
                else
                {
                    //if (employee.DAY_JOIN >= 90)
                    //{
                    tbEmpName.Text = employee.NAMA;
                    tbEmpPosition.Text = employee.JABATAN;
                    tbEmpTipe.Text = employee.TIPE;
                    hdnEmpStatus.Value = employee.STATUS_CARD.ToString();
                    hdnEmpEpc.Value = employee.STATUS_EPC.ToString();
                    hdnEmpSisaLimit.Value = employee.SISA_LIMIT.ToString();
                    //}
                    //else
                    //{
                    //    DivEmpMessage.InnerText = "Employee belum memasuki masa 3 bulan bekerja";
                    //    DivEmpMessage.Visible = true;
                    //    DivMessage.Attributes["class"] = "warning";
                    //}
                }
            }
            else
            {
                DivEmpMessage.InnerText = "No Kartu tidak di temukan atau No Kartu sudah tidak valid!";
                DivEmpMessage.Visible = true;
                DivEmpMessage.Attributes["class"] = "warning";
            }
            ModalEmployee.Show();
        }

        protected string changeCurr(string curr)
        {
            for (int i = 0; i < Math.Floor(Convert.ToDecimal((curr.Length - (1 + i)) / 3)); i++)
            {
                curr = curr.Substring(0, curr.Length - (4 * i + 3)) + "," + curr.Substring(curr.Length - (4 * i + 3));
            }
            return curr;
        }

        protected bool addVoucher()
        {
            bool ret = true;
            string session = Session["Voucher"] == null ? "" : Session["Voucher"].ToString();
            try
            {
                Decimal nilaiVoucher = Convert.ToDecimal(hdnPAYNilaiVoucher.Value);

                SH_VOUCHER voucher = new SH_VOUCHER();
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();

                voucher.ID_BAYAR = Convert.ToInt64(hdnIdPAY.Value);
                voucher.KODE_CUST = Session["UStore"].ToString();
                voucher.NO_VCR = tbPAYNoVoucher.Text;
                voucher.NILAI = Convert.ToDecimal(tbPAYNilaiVoucher.Text.Replace(",", ""));
                voucher.NO_CARD = "";
                voucher.CREATED_BY = Session["UName"].ToString();
                string berhasil = "";
                if (session == "" || session == "voucher")
                {
                    Session["Voucher"] = "voucher";
                    berhasil = bayarDA.insertSHVoucher(voucher);
                }

                if (berhasil == "Berhasil!")
                {
                    hdnPAYNilaiVoucher.Value = Convert.ToString(nilaiVoucher + voucher.NILAI);
                    tbPAYPrice.Text = changeCurr(Convert.ToString(Convert.ToDecimal(tbPAYPrice.Text.Replace(",", "")) - voucher.NILAI));
                    
                    if (divPAYCard.Visible)
                    {
                        tbPAYPAY.Text = tbPAYPrice.Text;
                    }
                }
                else
                {
                    ret = false;

                    DivPAYMessage.InnerText = "Error : " + berhasil;
                    DivPAYMessage.Attributes["class"] = "error";
                    DivPAYMessage.Visible = true;
                }
                Session["Voucher"] = "Done";
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            return ret;
        }

        protected bool newAddVoucher()
        {
            bool ret = true;
            try
            {
                //Check voucher dan nilai voucher di sistem
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_VOUCHER msVoucher = new MS_VOUCHER();
                msVoucher.KODE = Session["UKode"].ToString();
                msVoucher.NO_VOUCHER = tbNPayNoVou.Text.Trim();
                msVoucher.TGL_TRANS = DateTime.Now;
                string nilai = bayarDA.checkVoucher(msVoucher);

                if (nilai == "-1")
                {
                    DivNPayMessage.InnerText = "No Voucher tidak ditemukan";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else if (nilai == "-2")
                {
                    DivNPayMessage.InnerText = "No Voucher tidak valid";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else if (nilai == "-3")
                {
                    DivNPayMessage.InnerText = "No Voucher sudah digunakan";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else
                {
                    //Decimal nilaiVoucher = Convert.ToDecimal(hdnNPayNilaiVoucher.Value);
                    Decimal nilaiVoucher = Convert.ToDecimal(nilai);
                    if (Convert.ToDecimal(tbNPayNilaiVou.Text.Replace(",", "")) > nilaiVoucher)
                    {
                        ret = false;

                        DivNPayMessage.InnerText = "NIlai Voucher yang di masukan lebih besar dari nilai voucher sebenarnya";
                        DivNPayMessage.Attributes["class"] = "error";
                        DivNPayMessage.Visible = true;
                    }
                    else
                    {
                        SH_VOUCHER voucher = new SH_VOUCHER();

                        voucher.ID_BAYAR = Convert.ToInt64(hdnNPayIDBayar.Value);
                        voucher.KODE_CUST = Session["UStore"].ToString();
                        voucher.NO_VCR = tbNPayNoVou.Text;
                        voucher.NILAI = Convert.ToDecimal(tbNPayNilaiVou.Text.Replace(",", ""));
                        voucher.NO_CARD = "";
                        voucher.CREATED_BY = Session["UName"].ToString();
                        string berhasil = bayarDA.insertSHVoucher(voucher);

                        if (berhasil == "Berhasil!")
                        {
                            SH_BAYAR bayar = new SH_BAYAR();
                            bayar.JM_VOUCHER = voucher.NILAI;
                            bayar.ID = voucher.ID_BAYAR;
                            bayarDA.updateBayarVoucher(bayar);
                            //hdnNPayNilaiVoucher.Value = Convert.ToString(voucher.NILAI);
                            hdnNPayNilaiVoucher.Value = Convert.ToString(nilaiVoucher + voucher.NILAI);
                            tbNPayTotalPrice.Text = string.Format("{0:0,0.00}", Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")) - voucher.NILAI);
                            //tbNPayTotalPrice.Text = changeCurr(Convert.ToString(Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")) - voucher.NILAI));
                            //Penjagaan hanya bisa menggunakan 1 voucher 1 transaksi
                            btnNPayVoucher.Enabled = false;
                            DivNPayMessage.Visible = false;
                            tbNPayPay.Text = "";
                        }
                        else
                        {
                            ret = false;

                            DivNPayMessage.InnerText = "Error : " + berhasil;
                            DivNPayMessage.Attributes["class"] = "error";
                            DivNPayMessage.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DivNPayMessage.InnerText = "Error : " + ex.Message;
                DivNPayMessage.Attributes["class"] = "error";
                DivNPayMessage.Visible = true;
            }
            return ret;
        }

        protected bool newAddVoucherNoNilaiInput()
        {
            bool ret = true;
            try
            {
                //Check voucher dan nilai voucher di sistem
                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                MS_VOUCHER msVoucher = new MS_VOUCHER();
                msVoucher.KODE = Session["UKode"].ToString();
                msVoucher.NO_VOUCHER = tbNPayNoVou.Text.Trim();
                msVoucher.TGL_TRANS = DateTime.Now;
                string nilai = bayarDA.checkVoucher(msVoucher);

                if (nilai == "-1")
                {
                    DivNPayMessage.InnerText = "No Voucher tidak ditemukan";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else if (nilai == "-2")
                {
                    DivNPayMessage.InnerText = "No Voucher tidak valid";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else if (nilai == "-3")
                {
                    DivNPayMessage.InnerText = "No Voucher sudah digunakan";
                    DivNPayMessage.Attributes["class"] = "error";
                    DivNPayMessage.Visible = true;

                    return false;
                }
                else
                {
                    //Decimal nilaiVoucher = Convert.ToDecimal(hdnNPayNilaiVoucher.Value);
                    Decimal nilaiVoucher = Convert.ToDecimal(nilai);
                    lblNoVoucher.Text = tbNPayNoVou.Text.Trim();
                    //lblOriTotalPrice.Text = tbNPayTotalPrice.Text;


                    //if (berhasil == "Berhasil!")
                    //{
                    lblNilaiVoucher.Text = nilai;
                    if (Convert.ToDecimal(lblNilaiVoucher.Text.Replace(",", "")) > Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")))
                    {
                        SH_BAYAR bayar = new SH_BAYAR();
                        bayar.JM_VOUCHER =  Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")); //Convert.ToDecimal(nilai);
                        bayar.ID = Convert.ToInt64(hdnNPayIDBayar.Value);
                        bayarDA.updateBayarVoucher(bayar);
                        hdnNPayNilaiVoucher.Value = tbNPayTotalPrice.Text.Replace(",", ""); 
                        tbNPayTotalPrice.Text = string.Format("{0:0,0.00}", 0);
                    }
                    else
                    {
                        tbNPayTotalPrice.Text = string.Format("{0:0,0.00}", Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")) - Convert.ToDecimal(lblNilaiVoucher.Text.Replace(",", "")));
                        SH_BAYAR bayar = new SH_BAYAR();
                        bayar.JM_VOUCHER = Convert.ToDecimal(nilai);
                        bayar.ID = Convert.ToInt64(hdnNPayIDBayar.Value);
                        bayarDA.updateBayarVoucher(bayar);
                        hdnNPayNilaiVoucher.Value = lblNilaiVoucher.Text.Replace(",", "");//nilai;
                    }
                    btnNPayVoucher.Enabled = false;
                    DivNPayMessage.Visible = false;
                    tbNPayPay.Text = "";
                }
            }
            catch (Exception ex)
            {
                DivNPayMessage.InnerText = "Error : " + ex.Message;
                DivNPayMessage.Attributes["class"] = "error";
                DivNPayMessage.Visible = true;
            }
            return ret;
        }


        protected bool checkAcaraOLD(string acaraValue)
        {
            bool ret = true;
            try
            {
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                kdbrgDA.deleteTempAcara(Session["UName"].ToString());

                string nilaiMember = hdnMemberDisc.Value;
                string nilaiEpc = hdnEmpDisc.Value;
                string user = Session["UName"].ToString();

                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                TEMP_STRUCK struck = new TEMP_STRUCK();
                bool update = false;

                update = nilaiMember == "0" || nilaiEpc == "0" ? false : true;
                struck.MEMBER = nilaiMember == "0" ? "No" : "Yes";
                struck.EPC = nilaiEpc == "0" ? "No" : "Yes";
                
                if (update)
                {
                    struck.CREATED_BY = user;
                    bayarDA.updateMember(struck);
                }

                if (!(acaraValue == "0") && (nilaiMember == "0") && (nilaiEpc == "0"))
                {
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    List<MS_ITEM_ACARA> listItemAcara = new List<MS_ITEM_ACARA>();

                    kdbrgDA = new MS_KDBRG_DA();
                    List<TEMP_STRUCK> listTempStruck = new List<TEMP_STRUCK>();
                    //listItemAcara = acaraDA.getItemAcara(string.Format(" where ID = '{0}' and ID_KDBRG in ( " +
                    //    "select ID_KDBRG from TEMP_STRUCK where CREATED_BY = '{1}')", acaraValue, Session["UName"].ToString()));
                    listTempStruck = kdbrgDA.getTempStruck(string.Format(" where ID_KDBRG in ( select ID_KDBRG from MS_ITEM_ACARA " +
                        "where ID_ACARA = '{0}' ) and CREATED_BY = '{1}'", acaraValue, Session["UName"].ToString()));

                    #region acara 1 dan 2 Buy One Get One Selected Item Only
                    if (listTempStruck.Count > 1 && (acaraValue == "1" || acaraValue == "2"))//Acara bisa di dapat
                    {
                        if (acaraValue == "1")
                        {
                            //Update data Temp Struck
                            List<TEMP_STRUCK> listTempOrder = new List<TEMP_STRUCK>();
                            listTempOrder = listTempStruck.OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();
                            for (int i = 0; i < 2; i++)
                            {
                                TEMP_STRUCK temp = new TEMP_STRUCK();
                                temp = listTempOrder[i];

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                //tempAcara.NET_PRICE = i == 0 ? Convert.ToDecimal(0) : temp.ART_PRICE;
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = 0;
                                tempAcara.DISC = i == 0 ? 100 : 0;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = i == 0 ? Convert.ToDecimal(0) : temp.ART_PRICE;

                                kdbrgDA.updateTempStruckAcara(temp);
                            }
                        }
                        else if (acaraValue == "2")
                        {
                            //Update data Temp Struck
                            List<TEMP_STRUCK> listTempOrder = new List<TEMP_STRUCK>();
                            listTempOrder = listTempStruck.OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();
                            for (int i = 0; i < 2; i++)
                            {
                                TEMP_STRUCK temp = new TEMP_STRUCK();
                                temp = listTempOrder[i];

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = i == 0 ? Convert.ToDecimal(temp.ART_PRICE / 2) : temp.ART_PRICE;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = i == 0 ? Convert.ToDecimal(temp.ART_PRICE / 2) : temp.ART_PRICE;

                                kdbrgDA.updateTempStruckAcara(temp);
                            }
                        }
                    }
                    #endregion

                    #region acara 3 Discount Card Permata
                    else if (listTempStruck.Count > 0 && acaraValue == "3")
                    {
                        foreach (TEMP_STRUCK item in listTempStruck)
                        {
                            TEMP_STRUCK temp = new TEMP_STRUCK();
                            temp = item;

                            TEMP_ACARA tempAcara = new TEMP_ACARA();
                            tempAcara.ID_TEMP = temp.ID;
                            tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                            tempAcara.ID_KDBRG = temp.ID_KDBRG;
                            tempAcara.ITEM_CODE = temp.ITEM_CODE;
                            tempAcara.VALUE_ACARA = "";
                            tempAcara.NET_PRICE = temp.ART_PRICE;
                            tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                            tempAcara.DISC = 10;
                            tempAcara.CREATED_BY = Session["UName"].ToString();

                            kdbrgDA.insertTempAcara(tempAcara);

                            temp.ID_ACARA = Convert.ToInt64(acaraValue);
                            temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.9));

                            kdbrgDA.updateTempStruckAcara(temp);
                        }
                    }
                    #endregion

                    #region acara 4 Discount Central 10%
                    else if (acaraValue == "4")
                    {
                        MS_SHOWROOM show = new MS_SHOWROOM();
                        MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                        string sLevel = Session["ULevel"].ToString().ToLower();
                        string sKode = ddlStore.SelectedValue;
                        show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).First();

                        if (show.STORE.ToLower() == "central")
                        {
                            List<TEMP_STRUCK> listTempStruckCentral = new List<TEMP_STRUCK>();
                            listTempStruckCentral = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));
                            foreach (TEMP_STRUCK item in listTempStruckCentral)
                            {
                                TEMP_STRUCK temp = new TEMP_STRUCK();
                                temp = item;

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = 10;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.9));

                                kdbrgDA.updateTempStruckAcara(temp);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi brand untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }
                    #endregion

                    #region acara 5 Buy one 10% buy two 20% Melisa AEON
                    //else if (acaraValue == "5" && DateTime.Now > DateTime.ParseExact("2015-12-22 22:00:00,000","yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture))
                    else if (acaraValue == "5") //&& Session["ULevel"].ToString().ToLower() == "admin counter")
                    {
                        string sLevel = Session["ULevel"].ToString();
                        string sKode = Session["UKode"].ToString();

                        if (((ddlStore.SelectedValue.ToLower() == "sosfss04" || ddlStore.SelectedValue.ToLower() == "sosfss01") && sLevel.ToLower() == "admin sales") || sKode.ToString() == "SOSFSS01" || sKode.ToString() == "SOSFSS04")
                        {
                            //DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture)
                            List<TEMP_STRUCK> listTempStruckAEON = new List<TEMP_STRUCK>();
                            listTempStruckAEON = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));
                            int i = 0;
                            foreach (TEMP_STRUCK item in listTempStruckAEON)
                            {
                                TEMP_STRUCK temp = new TEMP_STRUCK();
                                temp = item;

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = i == 0 ? 10 : 20;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = i == 0 ? (temp.ART_PRICE * Convert.ToDecimal(0.9)) : (temp.ART_PRICE * Convert.ToDecimal(0.8));

                                kdbrgDA.updateTempStruckAcara(temp);
                                i++;
                            }
                        }
                    }
                    #endregion

                    #region acara 6 Disc 40% Bazzar Lotte
                    else if (acaraValue == "6" && Session["ULevel"].ToString().ToLower() == "admin counter")
                    {
                        string sLevel = Session["ULevel"].ToString();
                        string ddlKode = ddlStore.SelectedValue;

                        if (ddlKode == "SOSSIS15")
                        {
                            //DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",System.Globalization.CultureInfo.InvariantCulture)
                            List<TEMP_STRUCK> listTempStruckLOVE = new List<TEMP_STRUCK>();
                            listTempStruckLOVE = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));
                            int i = 0;
                            foreach (TEMP_STRUCK item in listTempStruckLOVE)
                            {
                                TEMP_STRUCK temp = new TEMP_STRUCK();
                                temp = item;

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = 40;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.6));

                                kdbrgDA.updateTempStruckAcara(temp);
                                i++;
                            }
                        }
                    }
                    #endregion

                    #region acara 7 Buy One Get 40% for Second Purchase AEON Melissa
                    else if (acaraValue == "7" && gvMain.Rows.Count == 2 && listTempStruck.Count == 1)
                    {
                        listTempStruck.OrderBy(item => item.ID);
                        
                        TEMP_STRUCK temp = new TEMP_STRUCK();
                        temp = listTempStruck.First();

                        TEMP_ACARA tempAcara = new TEMP_ACARA();
                        tempAcara.ID_TEMP = temp.ID;
                        tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                        tempAcara.ID_KDBRG = temp.ID_KDBRG;
                        tempAcara.ITEM_CODE = temp.ITEM_CODE;
                        tempAcara.VALUE_ACARA = "";
                        tempAcara.NET_PRICE = temp.ART_PRICE;
                        tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                        tempAcara.DISC = 40;
                        tempAcara.CREATED_BY = Session["UName"].ToString();

                        kdbrgDA.insertTempAcara(tempAcara);

                        temp.ID_ACARA = Convert.ToInt64(acaraValue);
                        temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.6));

                        kdbrgDA.updateTempStruckAcara(temp);
                    }
                    #endregion

                    #region acara 8 Buy One Get 30% for Second Purchase SUPERGA
                    else if ((acaraValue == "8" || acaraValue == "13") && gvMain.Rows.Count > 1)
                    {
                        List<TEMP_STRUCK> listTempStruckSP = new List<TEMP_STRUCK>();
                        TEMP_STRUCK temp = new TEMP_STRUCK();
                        listTempStruckSP = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString())).OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();

                        temp = listTempStruckSP.OrderBy(item => item.ART_PRICE).First();

                        TEMP_ACARA tempAcara = new TEMP_ACARA();
                        tempAcara.ID_TEMP = temp.ID;
                        tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                        tempAcara.ID_KDBRG = temp.ID_KDBRG;
                        tempAcara.ITEM_CODE = temp.ITEM_CODE;
                        tempAcara.VALUE_ACARA = "";
                        tempAcara.NET_PRICE = temp.ART_PRICE;
                        tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                        tempAcara.DISC = acaraValue == "8" ? 30 : 40;
                        tempAcara.CREATED_BY = Session["UName"].ToString();

                        kdbrgDA.insertTempAcara(tempAcara);

                        temp.ID_ACARA = Convert.ToInt64(acaraValue);
                        temp.NET_ACARA = acaraValue == "8" ? (temp.ART_PRICE * Convert.ToDecimal(0.7)) : (temp.ART_PRICE * Convert.ToDecimal(0.6));

                        kdbrgDA.updateTempStruckAcara(temp);
                    }
                    #endregion

                    #region acara 9 Buy One Get 20% for Second Purchase SDS dan 707 Store
                    else if (acaraValue == "9" && gvMain.Rows.Count == 2 && listTempStruck.Count == 2)
                    {
                        //List<TEMP_STRUCK> listTempStruckSP = new List<TEMP_STRUCK>();
                        TEMP_STRUCK temp = new TEMP_STRUCK();
                        //listTempStruckSP = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString())).OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();

                        temp = listTempStruck.OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>().First();

                        TEMP_ACARA tempAcara = new TEMP_ACARA();
                        tempAcara.ID_TEMP = temp.ID;
                        tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                        tempAcara.ID_KDBRG = temp.ID_KDBRG;
                        tempAcara.ITEM_CODE = temp.ITEM_CODE;
                        tempAcara.VALUE_ACARA = "";
                        tempAcara.NET_PRICE = temp.ART_PRICE;
                        tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                        tempAcara.DISC = 20;
                        tempAcara.CREATED_BY = Session["UName"].ToString();

                        kdbrgDA.insertTempAcara(tempAcara);

                        temp.ID_ACARA = Convert.ToInt64(acaraValue);
                        temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.8));

                        kdbrgDA.updateTempStruckAcara(temp);
                    }
                    #endregion

                    #region acara 10 Get 20% for Melissa & 10% for Superga
                    else if (acaraValue == "10")
                    {
                        MS_SHOWROOM show = new MS_SHOWROOM();
                        MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                        string sLevel = Session["ULevel"].ToString().ToLower();
                        string sKode = ddlStore.SelectedValue;
                        show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).First();

                        if (show.STORE.ToLower() == "central")
                        {                            
                            List<TEMP_STRUCK> listTempStruckCntrl = new List<TEMP_STRUCK>();
                            listTempStruckCntrl = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));

                            foreach (TEMP_STRUCK temp in listTempStruckCntrl)
                            {
                                if (temp.BRAND.ToLower() == "melissa" || temp.BRAND.ToLower() == "mel" || temp.BRAND.ToLower() == "superga")
                                {
                                    TEMP_ACARA tempAcara = new TEMP_ACARA();
                                    tempAcara.ID_TEMP = temp.ID;
                                    tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                    tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                    tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                    tempAcara.VALUE_ACARA = "";
                                    tempAcara.NET_PRICE = temp.ART_PRICE;
                                    tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                    tempAcara.DISC = temp.BRAND.ToLower() == "superga" ? 10 : 20;
                                    tempAcara.CREATED_BY = Session["UName"].ToString();

                                    kdbrgDA.insertTempAcara(tempAcara);

                                    temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                    temp.NET_ACARA = temp.BRAND.ToLower() == "superga" ? (temp.ART_PRICE * Convert.ToDecimal(0.9)) : (temp.ART_PRICE * Convert.ToDecimal(0.8));

                                    kdbrgDA.updateTempStruckAcara(temp);
                                }
                            }
                        }
                    }
                    #endregion

                    #region acara 11 Get 10% for Fred Perry Student and Employee
                    else if (acaraValue == "11")
                    {
                        MS_SHOWROOM show = new MS_SHOWROOM();
                        MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                        string sLevel = Session["ULevel"].ToString().ToLower();
                        string sKode = sLevel == "admin sales" ? ddlStore.SelectedValue : Session["UKode"].ToString();
                        show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).First();

                        if (show.STORE.ToLower() == "fred perry")
                        {
                            List<TEMP_STRUCK> listTempStruckFP = new List<TEMP_STRUCK>();
                            listTempStruckFP = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));

                            foreach (TEMP_STRUCK temp in listTempStruckFP)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = 10;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.9));

                                kdbrgDA.updateTempStruckAcara(temp);                                
                            }

                            SH_ACARA_VALUE shNama = new SH_ACARA_VALUE();
                            shNama.ID_BAYAR = Convert.ToInt64(hdnIdBYR.Value);
                            shNama.ID_ACARA = Convert.ToInt64(acaraValue);
                            shNama.PARAM = "Nama";
                            shNama.VALUE = tbBYRAcaraNama.Text;
                            shNama.CREATED_BY = Session["UName"].ToString();
                            bayarDA.insertSHAcaraValue(shNama);

                            shNama.PARAM = "NO ID";
                            shNama.VALUE = tbBYRNoID.Text;
                            bayarDA.insertSHAcaraValue(shNama);
                        }
                    }
                    #endregion

                    #region acara 12 Get 50% for UNION Superga
                    else if (acaraValue == "12")
                    {
                        MS_SHOWROOM show = new MS_SHOWROOM();
                        MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                        string sLevel = Session["ULevel"].ToString().ToLower();
                        string sKode = sLevel == "admin sales" ? ddlStore.SelectedValue : Session["UKode"].ToString();
                        show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", sKode)).First();

                        if (show.KODE.ToLower() == "sosfss07")
                        {
                            //List<TEMP_STRUCK> listTempStruckFP = new List<TEMP_STRUCK>();
                            //listTempStruckFP = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));

                            foreach (TEMP_STRUCK temp in listTempStruck)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = temp.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = temp.ID_KDBRG;
                                tempAcara.ITEM_CODE = temp.ITEM_CODE;
                                tempAcara.VALUE_ACARA = "";
                                tempAcara.NET_PRICE = temp.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = 50;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                temp.ID_ACARA = Convert.ToInt64(acaraValue);
                                temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.5));

                                kdbrgDA.updateTempStruckAcara(temp);
                            }
                        }
                    }
                    #endregion

                    #region acara 14 Buy One Get 50% for Second Purchase SDS
                    else if (acaraValue == "14" && gvMain.Rows.Count == 2 )
                    {
                        TEMP_STRUCK temp = new TEMP_STRUCK();
                        List<TEMP_STRUCK> listTempStruckSDS = new List<TEMP_STRUCK>();

                        listTempStruckSDS = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString())).OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();
                        for (int i = 0; i < listTempStruckSDS.Count; i++)
                        {
                            //temp = listTempStruckSDS.OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>().First();
                            temp = listTempStruckSDS[i];

                            TEMP_ACARA tempAcara = new TEMP_ACARA();
                            tempAcara.ID_TEMP = temp.ID;
                            tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                            tempAcara.ID_KDBRG = temp.ID_KDBRG;
                            tempAcara.ITEM_CODE = temp.ITEM_CODE;
                            tempAcara.VALUE_ACARA = "";
                            tempAcara.NET_PRICE = temp.ART_PRICE;
                            tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                            tempAcara.DISC = i == 0 ? 50 : 0;
                            tempAcara.CREATED_BY = Session["UName"].ToString();

                            kdbrgDA.insertTempAcara(tempAcara);

                            temp.ID_ACARA = Convert.ToInt64(acaraValue);
                            temp.NET_ACARA = i == 0 ? (temp.ART_PRICE * Convert.ToDecimal(0.5)) : (temp.ART_PRICE * Convert.ToDecimal(1));

                            kdbrgDA.updateTempStruckAcara(temp);
                        }
                    }
                    #endregion

                    #region acara 15 Voucher 40% for MDreams Melissa
                    else if (acaraValue == "15" && string.Format("{0:ddMMyy}", DateTime.Now) == "260416"
                        && Convert.ToInt32(string.Format("{0:HHmm}", DateTime.Now)) > 1300 && Convert.ToInt32(string.Format("{0:HHmm}", DateTime.Now)) < 1500)
                    {
                        TEMP_STRUCK temp = new TEMP_STRUCK();
                        List<TEMP_STRUCK> listTempStruckSDS = new List<TEMP_STRUCK>();

                        listTempStruckSDS = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString())).OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>();
                        for (int i = 0; i < listTempStruckSDS.Count; i++)
                        {
                            //temp = listTempStruckSDS.OrderBy(item => item.ART_PRICE).ToList<TEMP_STRUCK>().First();
                            temp = listTempStruckSDS[i];

                            TEMP_ACARA tempAcara = new TEMP_ACARA();
                            tempAcara.ID_TEMP = temp.ID;
                            tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                            tempAcara.ID_KDBRG = temp.ID_KDBRG;
                            tempAcara.ITEM_CODE = temp.ITEM_CODE;
                            tempAcara.VALUE_ACARA = "";
                            tempAcara.NET_PRICE = temp.ART_PRICE;
                            tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                            tempAcara.DISC = 40;
                            tempAcara.CREATED_BY = Session["UName"].ToString();

                            kdbrgDA.insertTempAcara(tempAcara);

                            temp.ID_ACARA = Convert.ToInt64(acaraValue);
                            temp.NET_ACARA = (temp.ART_PRICE * Convert.ToDecimal(0.6));

                            kdbrgDA.updateTempStruckAcara(temp);
                        }
                    }
                    #endregion
                    else
                    {
                        DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                        DivBYRMessage.Attributes["class"] = "warning";
                        DivBYRMessage.Visible = true;
                        ret = false;
                    }
                }
                bindGrid();
            }
            catch (Exception ex)
            {
                DivBYRMessage.InnerText = "ERROR : " + ex.Message;
                DivBYRMessage.Attributes["class"] = "error";
                DivBYRMessage.Visible = true;
                ret = false;
            }
            return ret;
        }

        protected bool checkAcara(string acaraValue)
        {
            bool ret = true;
            try
            {
                MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
                kdbrgDA.deleteTempAcara(Session["UName"].ToString());

                string nilaiMember = hdnMemberDisc.Value;
                string nilaiEpc = hdnEmpDisc.Value;
                string user = Session["UName"].ToString();

                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                TEMP_STRUCK struck = new TEMP_STRUCK();
                bool update = false;

                update = (nilaiMember == "0" && nilaiEpc == "0") || (nilaiMember != "0" && nilaiEpc != "0") ? true : false;
                struck.MEMBER = nilaiMember == "0" ? "No" : "Yes";
                struck.EPC = nilaiEpc == "0" ? "No" : "Yes";

                if (update)
                {
                    struck.CREATED_BY = user;
                    bayarDA.updateMember(struck);
                }

                if (!(acaraValue == "0") && (nilaiMember == "0") && (nilaiEpc == "0"))
                {
                    MS_ACARA_DA acaraDA = new MS_ACARA_DA();
                    List<MS_ACARA> listAcara = new List<MS_ACARA>();
                    List<MS_ITEM_ACARA> listItemAcara = new List<MS_ITEM_ACARA>();
                    List<TEMP_STRUCK> listTempStruckAcara = new List<TEMP_STRUCK>();
                    List<TEMP_STRUCK> listTempStruck = new List<TEMP_STRUCK>();
                    MS_ACARA acara = new MS_ACARA();

                    listTempStruckAcara = kdbrgDA.getTempStruck(string.Format(" where ID_KDBRG in ( select ID_KDBRG from MS_ITEM_ACARA " +
                        "where ID_ACARA = '{0}' ) and CREATED_BY = '{1}'", acaraValue, Session["UName"].ToString()));

                    listTempStruck = kdbrgDA.getTempStruck(string.Format(" where CREATED_BY = '{0}'", Session["UName"].ToString()));

                    listAcara = acaraDA.getAcara(" where STATUS_ACARA = 1 and ID_ACARA = " + acaraValue);
                    acara = listAcara.First();

                    //Yang sudah AS001 AS002 AS003 AS009 AS010 AS004 AS005 AS006 AS007
                    #region Discount pembelian barang ke-2 berlaku untuk barang tertentu atau semuanya
                    if (acara.ACARA_STATUS == "AS001" || acara.ACARA_STATUS == "AS002")
                    {
                        //if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 1) || (acara.ARTICLE == "ALL" && listTempStruck.Count > 1))
                        if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 0) || (acara.ARTICLE == "ALL" && listTempStruck.Count > 1))
                        {
                            List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();

                            listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;
                            listTempLooping = listTempLooping.OrderBy(item => item.ART_PRICE).ToList();

                            int limit = acara.ACARA_STATUS == "AS001" ? 1 : listTempLooping.Count - 1;
                            int i = 0;

                            //if (listTempLooping.Count > 1)
                            if (listTempLooping.Count > 0)
                            {
                                foreach (TEMP_STRUCK item in listTempLooping)
                                {
                                    //if (i > 0)
                                    //{
                                        TEMP_ACARA tempAcara = new TEMP_ACARA();
                                        tempAcara.ID_TEMP = item.ID;
                                        tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                        tempAcara.ID_KDBRG = item.ID_KDBRG;
                                        tempAcara.ITEM_CODE = item.ITEM_CODE;
                                        tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                        tempAcara.NET_PRICE = item.ART_PRICE;
                                        tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                        //tempAcara.DISC = i < limit ? i > 0 ? acara.DISC : 0 : 0;
                                        tempAcara.DISC = i < limit ? acara.DISC : 0;
                                        tempAcara.CREATED_BY = Session["UName"].ToString();

                                        kdbrgDA.insertTempAcara(tempAcara);

                                        item.ID_ACARA = Convert.ToInt64(acaraValue);
                                        //item.NET_ACARA = i < limit ? i > 0 ? (item.ART_PRICE * Convert.ToDecimal(acara.DISC)) : (item.ART_PRICE) : (item.ART_PRICE);
                                        item.NET_ACARA = i < limit ? (item.ART_PRICE * Convert.ToDecimal(acara.DISC)) : (item.ART_PRICE);

                                        kdbrgDA.updateTempStruckAcara(item);
                                    //}
                                    i++;
                                }
                            }
                            else
                            {
                                DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ret = false;
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }
                    #endregion

                    #region Discount Article
                    else if (acara.ACARA_STATUS == "AS003" || acara.ACARA_STATUS == "AS009" || acara.ACARA_STATUS == "AS010")
                    {
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        int limit = acara.ACARA_STATUS == "AS003" ? 0 : acara.ACARA_STATUS == "AS009" ? 1 : 2;
                        listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;

                        if (listTempLooping.Count > limit)
                        {
                            foreach (TEMP_STRUCK item in listTempLooping)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = acara.DISC;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcara(item);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }
                    #endregion

                    #region Discount pembelian 1 barang, discount pembelian 2 barang tertentu atau semuanya
                    else if (acara.ACARA_STATUS == "AS004" || acara.ACARA_STATUS == "AS005")
                    {
                        if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 0) || (acara.ARTICLE == "ALL"))
                        {
                            List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                            int limit = acara.ACARA_STATUS == "AS004" ? 2 : 999;
                            int i = 0;
                            //int discacara = 0;
                            decimal discacara = 0;

                            listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;
                            acara = listTempLooping.Count > 1 ? listAcara.Last() : acara;
                            #region "Get Discount yg digunakan"
                            if (listTempLooping.Count == 1)
                            {
                                discacara =  listAcara[i].DISC;
                            }
                            else 
                            {
                                discacara = listAcara[1].DISC;
                            }
                            #endregion
                            foreach (TEMP_STRUCK item in listTempLooping)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                             
                                //tempAcara.DISC = i < limit ? acara.DISC : 0;
                                tempAcara.DISC = i < limit ? discacara : 0;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = i < limit ? (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100))) : item.ART_PRICE;

                                kdbrgDA.updateTempStruckAcara(item);
                                i++;
                            }
                        }
                    }
                    #endregion

                    #region Discount pembelian 1 barang, discount pembelian 2 barang, discount pembelian 3 barang tertentu atau semuanya
                    else if (acara.ACARA_STATUS == "AS006" || acara.ACARA_STATUS == "AS007")
                    {
                        if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 0) || (acara.ARTICLE == "ALL"))
                        {
                            List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                            int limit = acara.ACARA_STATUS == "AS006" ? 3 : 999;
                            int i = 0;
                            decimal discacara = 0;
                            //int discacara = 0;
                            listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;
                            acara = listTempLooping.Count > 2 ? listAcara.Last() : listTempLooping.Count == 2 ? listAcara[1] : acara;
                            #region "Get Discount yg digunakan"
                            if (listTempLooping.Count == 1)
                            {
                                discacara = listAcara[i].DISC;
                            }
                            else if (listTempLooping.Count == 2)
                            {
                                discacara = listAcara[1].DISC;
                            }
                            else
                            {
                                discacara = listAcara[2].DISC; ;
                            }
                            #endregion
                            foreach (TEMP_STRUCK item in listTempLooping)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                               
                                //tempAcara.DISC = i < limit ? acara.DISC : 0;
                                tempAcara.DISC = i < limit ? discacara : 0;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = i < limit ? (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100))) : item.ART_PRICE;

                                kdbrgDA.updateTempStruckAcara(item);
                                i++;
                            }
                        }
                    }
                    #endregion

                    #region Special Price Article
                    else if (acara.ACARA_STATUS == "AS013" || acara.ACARA_STATUS == "AS014" || acara.ACARA_STATUS == "AS015")
                    {
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        int limit = acara.ACARA_STATUS == "AS013" ? 0 : acara.ACARA_STATUS == "AS014" ? 1 : 2;
                        listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;

                        if (listTempLooping.Count > limit)
                        {
                            foreach (TEMP_STRUCK item in listTempLooping)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(acara.SPCL_PRICE);
                                tempAcara.DISC = acara.DISC;
                                tempAcara.CREATED_BY = Session["UName"].ToString();
                                tempAcara.DISC_PRICE = item.ART_PRICE - acara.SPCL_PRICE;

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcara(item);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }
                    #endregion

                    #region Discount for selected item With Minimun Purchase (exclude that selected item)
                    else if (acara.ACARA_STATUS == "AS017" || acara.ACARA_STATUS == "AS018" || acara.ACARA_STATUS == "AS019" || acara.ACARA_STATUS == "AS020")
                    {
                        decimal minPurchase = acara.ACARA_STATUS == "AS017" ? 500000 : acara.ACARA_STATUS == "AS018" ? 1000000 :
                            acara.ACARA_STATUS == "AS019" ? 1500000 : 2000000;
                        decimal allPay = listTempStruck.Sum(item => item.NET_PRICE);
                        decimal minPay = listTempStruckAcara.Sum(item => item.NET_PRICE);

                        if ((allPay - minPay) >= minPurchase)
                        {
                            foreach (TEMP_STRUCK item in listTempStruckAcara)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                tempAcara.DISC = acara.DISC;
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcara(item);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }

                    }
                    #endregion
                    #region Discount Price With Minimum Purchase "
                    else if (acara.ACARA_STATUS == "AS023" )
                    {
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        decimal minPurchase = acara.MIN_PURCHASE;
                        decimal allPay = listTempStruck.Sum(item => item.NET_PRICE);
                        decimal minPay;
                        if (acara.ARTICLE == "SOME")
                        {
                            listTempLooping = listTempStruckAcara;
                            minPay = listTempStruckAcara.Sum(item => item.NET_PRICE);
                        }
                        else
                        {
                            listTempLooping = listTempStruck;
                            minPay = listTempStruck.Sum(item => item.NET_PRICE);
                        }
                        
                        decimal totalDiscPrice = 0;
                        int jmlartAcara = listTempLooping.Count();
                        if (allPay >= minPurchase)
                        {
                            //foreach (TEMP_STRUCK item in listTempStruckAcara)
                            for (int i = 0; i < jmlartAcara; i++ )
                            {

                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = listTempLooping[i].ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = listTempLooping[i].ID_KDBRG;
                                tempAcara.ITEM_CODE = listTempLooping[i].ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = listTempLooping[i].ART_PRICE;
                                if (i == (jmlartAcara-1))
                                {
                                    tempAcara.DISC_PRICE = acara.SPCL_PRICE - totalDiscPrice;
                                }
                                else
                                {
                                    tempAcara.DISC_PRICE = decimal.Round(Convert.ToDecimal((listTempLooping[i].ART_PRICE * acara.SPCL_PRICE) / minPay));
                                    totalDiscPrice = totalDiscPrice + tempAcara.DISC_PRICE;
                                }
                                tempAcara.DISC = acara.DISC;
                                
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                listTempLooping[i].ID_ACARA = Convert.ToInt64(acaraValue);
                                listTempLooping[i].NET_ACARA = (listTempLooping[i].ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcara(listTempLooping[i]);
                                
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }

                    }
                    #endregion
                    #region "GWP AS024"
                    else if (acara.ACARA_STATUS == "AS024")
                    {
                        List<MS_ITEM_GWP_PWP> listItemAcaraGWP = new List<MS_ITEM_GWP_PWP>();
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        listItemAcaraGWP = acaraDA.getItemAcaraGWP(" where ID_ACARA = " + acaraValue);

                        decimal minPurchase = acara.MIN_PURCHASE;
                        decimal allPay = listTempStruck.Sum(item => item.NET_PRICE);
                        decimal minPay;
                        if (acara.ARTICLE == "SOME")
                        {
                            listTempLooping = listTempStruckAcara;
                            minPay = listTempStruckAcara.Sum(item => item.NET_PRICE);
                        }
                        else
                        {
                            listTempLooping = listTempStruck;
                            minPay = listTempStruck.Sum(item => item.NET_PRICE);
                        }

                        //int jmlartAcara = listTempLooping.Count();
                        if (minPay >= minPurchase)
                        {
                            List<TEMP_STRUCK_GWP_PWP> LtStrck = new List<TEMP_STRUCK_GWP_PWP>();
                            LtStrck = acaraDA.getTempStruckGWP(String.Format(" WHERE b.ID_ACARA = {0} AND a.CREATED_BY = '{1}' order by a.ID ASC", acaraValue, Session["UName"].ToString()));
                            if (LtStrck.Count == 0)
                            {
                                DivBYRMessage.InnerText = "Barang GWP Harus di Input Dulu kedalam Struk!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ret = false;
                            }
                            else
                            {
                                TEMP_STRUCK_GWP_PWP tStrck = new TEMP_STRUCK_GWP_PWP();
                                tStrck = LtStrck.FirstOrDefault();
                                //tStrck = listTempStruck.Where(item => item.ID_KDBRG == listItemAcaraGWP.FirstOrDefault().ID_KDBRG).FirstOrDefault();
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = tStrck.ID;//listTempLooping[i].ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = tStrck.ID_KDBRG;//listTempLooping[i].ID_KDBRG;
                                tempAcara.ITEM_CODE = tStrck.ITEM_CODE;//listTempLooping[i].ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = tStrck.ART_PRICE;//listTempLooping[i].ART_PRICE;
                                tempAcara.DISC_PRICE = 0 ;//acara.SPCL_PRICE - totalDiscPrice;
                                tempAcara.DISC = acara.DISC;

                                tempAcara.SPCL_PRICE = 0;//tStrck.ITEM_PRICE_ACARA; //Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                tStrck.ID_ACARA = Convert.ToInt64(acaraValue);
                                tStrck.NET_ACARA = 0; //(tStrck.ITEM_PRICE_ACARA * Convert.ToDecimal(1 - (tempAcara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcaraGWP(tStrck);
                            }
                            #region COMMENT
                            //foreach (TEMP_STRUCK item in listTempStruckAcara)
                            //for (int i = 0; i < jmlartAcara; i++)
                            //{

                            //    TEMP_ACARA tempAcara = new TEMP_ACARA();
                            //    tempAcara.ID_TEMP = listTempLooping[i].ID;
                            //    tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                            //    tempAcara.ID_KDBRG = listTempLooping[i].ID_KDBRG;
                            //    tempAcara.ITEM_CODE = listTempLooping[i].ITEM_CODE;
                            //    tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                            //    tempAcara.NET_PRICE = listTempLooping[i].ART_PRICE;
                            //    if (i == (jmlartAcara - 1))
                            //    {
                            //        tempAcara.DISC_PRICE = acara.SPCL_PRICE - totalDiscPrice;
                            //    }
                            //    else
                            //    {
                            //        tempAcara.DISC_PRICE = decimal.Round(Convert.ToDecimal((listTempLooping[i].ART_PRICE * acara.SPCL_PRICE) / minPay));
                            //        totalDiscPrice = totalDiscPrice + tempAcara.DISC_PRICE;
                            //    }
                            //    tempAcara.DISC = acara.DISC;

                            //    tempAcara.SPCL_PRICE = Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                            //    tempAcara.CREATED_BY = Session["UName"].ToString();

                            //    kdbrgDA.insertTempAcara(tempAcara);

                            //    listTempLooping[i].ID_ACARA = Convert.ToInt64(acaraValue);
                            //    listTempLooping[i].NET_ACARA = (listTempLooping[i].ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                            //    kdbrgDA.updateTempStruckAcara(listTempLooping[i]);

                            //}
                            #endregion
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini Atau Minimumpurchase lebih kecil dari " + acara.MIN_PURCHASE + ", silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }

                    }
                    #endregion
                    #region "PWP DISC AS025"
                    else if (acara.ACARA_STATUS == "AS025")
                    {
                        List<MS_ITEM_GWP_PWP> listItemAcaraGWP = new List<MS_ITEM_GWP_PWP>();
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        listItemAcaraGWP = acaraDA.getItemAcaraGWP(" where ID_ACARA = " + acaraValue);

                        decimal minPurchase = acara.MIN_PURCHASE;
                        decimal allPay = listTempStruck.Sum(item => item.NET_PRICE);
                        decimal minPay;
                        if (acara.ARTICLE == "SOME")
                        {
                            listTempLooping = listTempStruckAcara;
                            minPay = listTempStruckAcara.Sum(item => item.NET_PRICE);
                        }
                        else
                        {
                            listTempLooping = listTempStruck;
                            minPay = listTempStruck.Sum(item => item.NET_PRICE);
                        }

                        //int jmlartAcara = listTempLooping.Count();
                        if (minPay >= minPurchase)
                        {
                            List<TEMP_STRUCK_GWP_PWP> LtStrck = new List<TEMP_STRUCK_GWP_PWP>();
                            LtStrck = acaraDA.getTempStruckGWP(String.Format(" WHERE b.ID_ACARA = {0} AND a.CREATED_BY = '{1}' order by a.ID ASC", acaraValue, Session["UName"].ToString()));
                            if (LtStrck.Count == 0)
                            {
                                DivBYRMessage.InnerText = "Barang PWP Harus di Input Dulu kedalam Struk!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ret = false;
                            }
                            else
                            {
                                TEMP_STRUCK_GWP_PWP tStrck = new TEMP_STRUCK_GWP_PWP();
                                tStrck = LtStrck.FirstOrDefault();
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = tStrck.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = tStrck.ID_KDBRG;
                                tempAcara.ITEM_CODE = tStrck.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = tStrck.ART_PRICE;
                                tempAcara.DISC_PRICE = acara.SPCL_PRICE;
                                tempAcara.DISC = acara.DISC;

                                tempAcara.SPCL_PRICE = 0;//tStrck.ITEM_PRICE_ACARA; //Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                tStrck.ID_ACARA = Convert.ToInt64(acaraValue);
                                tStrck.NET_ACARA = (tStrck.ART_PRICE * (1-(tempAcara.DISC / 100))); //(tStrck.ITEM_PRICE_ACARA * Convert.ToDecimal(1 - (tempAcara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcaraGWP(tStrck);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini Atau Minimumpurchase lebih kecil dari " + acara.MIN_PURCHASE + ", silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }

                    }
                    #endregion
                    #region "PWP SPECIAL PRICE AS026"
                    else if (acara.ACARA_STATUS == "AS026")
                    {
                        List<MS_ITEM_GWP_PWP> listItemAcaraGWP = new List<MS_ITEM_GWP_PWP>();
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        listItemAcaraGWP = acaraDA.getItemAcaraGWP(" where ID_ACARA = " + acaraValue);

                        decimal minPurchase = acara.MIN_PURCHASE;
                        decimal allPay = listTempStruck.Sum(item => item.NET_PRICE);
                        decimal minPay;
                        if (acara.ARTICLE == "SOME")
                        {
                            listTempLooping = listTempStruckAcara;
                            minPay = listTempStruckAcara.Sum(item => item.NET_PRICE);
                        }
                        else
                        {
                            listTempLooping = listTempStruck;
                            minPay = listTempStruck.Sum(item => item.NET_PRICE);
                        }

                        //int jmlartAcara = listTempLooping.Count();
                        if (minPay >= minPurchase)
                        {
                            List<TEMP_STRUCK_GWP_PWP> LtStrck = new List<TEMP_STRUCK_GWP_PWP>();
                            LtStrck = acaraDA.getTempStruckGWP(String.Format(" WHERE b.ID_ACARA = {0} AND a.CREATED_BY = '{1}' order by a.ID ASC", acaraValue, Session["UName"].ToString()));
                            if (LtStrck.Count == 0)
                            {
                                DivBYRMessage.InnerText = "Barang PWP Harus di Input Dulu kedalam Struk!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ret = false;
                            }
                            else
                            {
                                TEMP_STRUCK_GWP_PWP tStrck = new TEMP_STRUCK_GWP_PWP();
                                tStrck = LtStrck.FirstOrDefault();
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = tStrck.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = tStrck.ID_KDBRG;
                                tempAcara.ITEM_CODE = tStrck.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = tStrck.ART_PRICE;
                                tempAcara.DISC_PRICE = (tStrck.ART_PRICE - acara.SPCL_PRICE);
                                tempAcara.DISC = acara.DISC;

                                tempAcara.SPCL_PRICE = 0;//tStrck.ITEM_PRICE_ACARA; //Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                tStrck.ID_ACARA = Convert.ToInt64(acaraValue);
                                tStrck.NET_ACARA = acara.SPCL_PRICE; //(tStrck.ITEM_PRICE_ACARA * Convert.ToDecimal(1 - (tempAcara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcaraGWP(tStrck);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini Atau Minimumpurchase lebih kecil dari " + acara.MIN_PURCHASE + ", silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }

                    }
                    #endregion
                    
                    #region Discount pembelian barang ke-3 berlaku untuk barang tertentu atau semuanya (Buy 2 get 1)
                    else if (acara.ACARA_STATUS == "AS021" || acara.ACARA_STATUS == "AS022")
                    {
                        //if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 1) || (acara.ARTICLE == "ALL" && listTempStruck.Count > 1))
                        if ((acara.ARTICLE == "SOME" && listTempStruckAcara.Count > 1) || (acara.ARTICLE == "ALL" && listTempStruck.Count > 2))
                        {
                            List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();

                            listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;
                            listTempLooping = listTempLooping.OrderBy(item => item.ART_PRICE).ToList();

                            int limit = acara.ACARA_STATUS == "AS021" ? 1 : listTempLooping.Count - 2;
                            int i = 0;

                            //if (listTempLooping.Count > 1)
                            if (listTempLooping.Count > 0)
                            {
                                foreach (TEMP_STRUCK item in listTempLooping)
                                {
                                    //if (i > 0)
                                    //{
                                    TEMP_ACARA tempAcara = new TEMP_ACARA();
                                    tempAcara.ID_TEMP = item.ID;
                                    tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                    tempAcara.ID_KDBRG = item.ID_KDBRG;
                                    tempAcara.ITEM_CODE = item.ITEM_CODE;
                                    tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                    tempAcara.NET_PRICE = item.ART_PRICE;
                                    tempAcara.SPCL_PRICE = Convert.ToDecimal(0.0);
                                    //tempAcara.DISC = i < limit ? i > 0 ? acara.DISC : 0 : 0;
                                    tempAcara.DISC = i < limit ? acara.DISC : 0;
                                    tempAcara.CREATED_BY = Session["UName"].ToString();

                                    kdbrgDA.insertTempAcara(tempAcara);

                                    item.ID_ACARA = Convert.ToInt64(acaraValue);
                                    //item.NET_ACARA = i < limit ? i > 0 ? (item.ART_PRICE * Convert.ToDecimal(acara.DISC)) : (item.ART_PRICE) : (item.ART_PRICE);
                                    item.NET_ACARA = i < limit ? (item.ART_PRICE * Convert.ToDecimal(acara.DISC)) : (item.ART_PRICE);

                                    kdbrgDA.updateTempStruckAcara(item);
                                    //}
                                    i++;
                                }
                            }
                            else
                            {
                                DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                                DivBYRMessage.Attributes["class"] = "warning";
                                DivBYRMessage.Visible = true;
                                ret = false;
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }
                    #endregion

                    else if (acara.ACARA_STATUS == "AS016")
                    {
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        //int limit = acara.ACARA_STATUS == "AS016" ? 0 : acara.ACARA_STATUS == "AS014" ? 1 : 2;
                        //listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;

                        int limit = 0;
                        listTempLooping = listTempStruck;

                        decimal totalDisc = acara.SPCL_PRICE;
                        decimal totalBelanja = listTempLooping.Sum(item => item.ART_PRICE);
                        totalBelanja = totalBelanja > totalDisc ? totalBelanja : totalDisc;

                        if (listTempLooping.Count > limit)
                        {
                            foreach (TEMP_STRUCK item in listTempLooping)
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.DISC_PRICE = Convert.ToDecimal((item.ART_PRICE * acara.SPCL_PRICE) / totalBelanja);
                                tempAcara.DISC = acara.DISC;

                                tempAcara.SPCL_PRICE = Convert.ToDecimal(tempAcara.NET_PRICE - tempAcara.DISC_PRICE);
                                tempAcara.CREATED_BY = Session["UName"].ToString();

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = (item.ART_PRICE * Convert.ToDecimal(1 - (acara.DISC / 100)));

                                kdbrgDA.updateTempStruckAcara(item);
                            }
                        }
                        else
                        {
                            DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                            DivBYRMessage.Attributes["class"] = "warning";
                            DivBYRMessage.Visible = true;
                            ret = false;
                        }
                    }

                    else if (acara.ACARA_STATUS == "AS008")
                    {
                        List<TEMP_STRUCK> listTempLooping = new List<TEMP_STRUCK>();
                        listTempLooping = acara.ARTICLE == "SOME" ? listTempStruckAcara : listTempStruck;

                        foreach (TEMP_STRUCK item in listTempLooping)
                        {
                            if (!(item.JENIS_DISCOUNT.ToLower() == "p"))
                            {
                                TEMP_ACARA tempAcara = new TEMP_ACARA();
                                tempAcara.ID_TEMP = item.ID;
                                tempAcara.ID_ACARA = Convert.ToInt64(acaraValue);
                                tempAcara.ID_KDBRG = item.ID_KDBRG;
                                tempAcara.ITEM_CODE = item.ITEM_CODE;
                                tempAcara.VALUE_ACARA = acara.ACARA_VALUE;
                                tempAcara.NET_PRICE = item.ART_PRICE;
                                tempAcara.SPCL_PRICE = Convert.ToDecimal(item.NET_PRICE * (100 - acara.DISC) / 100);
                                tempAcara.DISC = 0;
                                tempAcara.CREATED_BY = Session["UName"].ToString();
                                tempAcara.DISC_PRICE = item.ART_PRICE - tempAcara.SPCL_PRICE;

                                kdbrgDA.insertTempAcara(tempAcara);

                                item.ID_ACARA = Convert.ToInt64(acaraValue);
                                item.NET_ACARA = tempAcara.SPCL_PRICE;

                                kdbrgDA.updateTempStruckAcara(item);
                            }
                        }
                    }
                    else
                    {
                        DivBYRMessage.InnerText = "Barang yang dibeli tidak bisa untuk acara ini, silakan hubungi administator untuk penjelasan lanjut!";
                        DivBYRMessage.Attributes["class"] = "warning";
                        DivBYRMessage.Visible = true;
                        ret = false;
                    }
                }
                bindGrid();
            }
            catch (Exception ex)
            {
                DivBYRMessage.InnerText = "ERROR : " + ex.Message;
                DivBYRMessage.Attributes["class"] = "error";
                DivBYRMessage.Visible = true;
                ret = false;
            }
            return ret;
        }

        protected bool paid(string change)
        {
            bool isSuccess = true;
            SH_BAYAR shBayar = new SH_BAYAR();
            SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
            MS_STOCK_DA stockDA = new MS_STOCK_DA();
            string idTemp = hdnIdPAY.Value;
            string toko = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedItem.Text : Session["UStore"].ToString();
            string kodeToko = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedValue : Session["UKode"].ToString();
            string id = idTemp.Length > 5 ? idTemp.Remove(0, idTemp.Length - 5) : idTemp.PadLeft(5, '0');
            decimal bayar = Convert.ToDecimal(tbPAYPAY.Text.Replace(",", ""));
            string sLevel = Session["ULevel"].ToString();

            DateTime tglTrans = SqlDateTime.MaxValue.Value;
            string date = tbDate.Text.ToString();
            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
            }

            DateTime dt = DateTime.Now;
            string tgl = string.Format("{0:yyddMM}", dt);

            string noBon = tgl + id;
            string retCard = string.Empty;
            if (divPAYCard.Visible)//Bayar pake kartu
            {
                SH_CARD shCard = new SH_CARD();
                shCard.ID_BAYAR = Convert.ToInt64(idTemp);
                shCard.NET_CARD = Convert.ToDecimal(tbPAYPAY.Text.Replace(".00", "").Replace(",", ""));
                shCard.KD_CCARD = tbPAYKDCard.Text;
                shCard.NO_CCARD = tbPAYNoCard.Text;
                shCard.VL_CCARD = tbPAYVLCard.Text;
                shCard.BANK = tbPAYBank.Text;
                shCard.EDC = tbPAYEdc.Text;

                retCard = shBayarDA.insertSHCard(shCard);
                if (retCard != "Berhasil!")
                {
                    DivMessage.InnerText = "Error : " + retCard;
                    DivMessage.Attributes["class"] = "error";
                    DivMessage.Visible = true;
                    isSuccess = false;
                }
            }
            shBayar.ID = Convert.ToInt64(idTemp);
            shBayar.NO_BON = noBon;
            shBayar.CARD = rbBYRPay.SelectedValue;
            shBayar.JM_UANG = bayar;
            shBayar.KEMBALI = Convert.ToDecimal(change);
            shBayar.NET_CASH = rbBYRPay.SelectedValue == "Yes" ? 0 : bayar;
            shBayar.NET_BAYAR = Convert.ToDecimal(tbPAYPrice.Text.Replace(",", ""));
            shBayar.VOUCHER = hdnPAYNilaiVoucher.Value == "0" ? "No" : "Yes";
            shBayar.ONGKIR = HdnOngkir.Value == "0" ? "No" : "Yes";
            if (HdnBiayaOngkir.Value == "" || HdnBiayaOngkir.Value == null)
            {
                shBayar.JM_ONGKIR = 0;
            }
            else
            {
                shBayar.JM_ONGKIR = Convert.ToDecimal(HdnBiayaOngkir.Value);
            }
            if (HdnFreeOngkir.Value == "" || HdnFreeOngkir.Value == null)
            {
                shBayar.JM_FREE_ONGKIR = 0;
            }
            else
            {
                shBayar.JM_FREE_ONGKIR = Convert.ToDecimal(HdnFreeOngkir.Value);
            }
            string ret = shBayarDA.updateBayar(shBayar);
            if (ret == "Berhasil!" && (retCard == "Berhasil!" || string.IsNullOrEmpty(retCard)))
            {
                //Insert Barang ke SH_JUAL
                foreach (GridViewRow item in gvMain.Rows)
                {
                    SH_JUAL jual = new SH_JUAL();
                    jual.ID_BAYAR = Convert.ToInt64(idTemp);
                    jual.ID_KDBRG = Convert.ToInt64(item.Cells[20].Text.ToString());
                    jual.KODE_CUST = toko;
                    jual.KODE = kodeToko;
                    jual.NO_BON = noBon;
                    jual.ITEM_CODE = item.Cells[22].Text.ToString();
                    jual.QTY = Convert.ToInt32(item.Cells[7].Text.ToString());
                    jual.TAG_PRICE = Convert.ToDecimal(item.Cells[14].Text.ToString());//Article Price
                    jual.BON_PRICE = Convert.ToDecimal(item.Cells[16].Text.ToString());//Bon Price
                    jual.JNS_DISC = item.Cells[17].Text.ToString().Contains("&nbsp") ? "" : item.Cells[17].Text.ToString();//Jenis Discount
                    jual.DISC_P = Convert.ToInt32(item.Cells[9].Text.ToString());//Discount Rate
                    jual.NILAI_BYR = Convert.ToDecimal(item.Cells[12].Text.ToString().Replace(",",""));//Total bayar per item
                    if (tglTrans < NewPPNDate)
                    {
                        jual.DPP = Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(1.1));
                        jual.PPN = Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(11));
                    }
                    else
                    {
                        jual.DPP = Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(1.11));
                        jual.PPN = jual.NILAI_BYR - Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(1.11));//Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(11));
                    }
                    jual.ALASAN = "";
                    jual.FNOACARA = item.Cells[18].Text.ToString();
                    jual.JUAL_RETUR = item.Cells[21].Text.ToString();
                    jual.DISC_R = jual.JNS_DISC.Contains("P") ? jual.TAG_PRICE - Convert.ToDecimal(item.Cells[15].Text.ToString())
                        : jual.JNS_DISC.Contains("D") ? Convert.ToDecimal(item.Cells[8].Text.ToString().Replace(",", "")) - Convert.ToDecimal(item.Cells[11].Text.ToString().Replace(",", "")) : 0;//Discount Price
                    jual.BARCODE = item.Cells[3].Text.ToString();
                    string idJual = shBayarDA.insertSHJual(jual);

                    //Update Stock
                    List<MS_STOCK> listStock = new List<MS_STOCK>();
                    MS_STOCK stock = new MS_STOCK();

                    listStock = stockDA.getStock(string.Format(" where ITEM_CODE like '{0}' and WAREHOUSE like '{1}'", jual.ITEM_CODE.Trim(), toko));
                    if (listStock.Count > 0)
                    {
                        stock = listStock.First();
                        stock.STOCK = jual.QTY * -1;
                        stock.UPDATED_BY = Session["UName"].ToString();
                        stockDA.updateDataStockWithID(stock);
                    }
                    else
                    {
                        stock.ITEM_CODE = jual.ITEM_CODE;
                        stock.WAREHOUSE = toko;
                        stock.STOCK = jual.QTY * -1;
                        stock.CREATED_BY = Session["UName"].ToString();
                        stockDA.insertDataStock(stock);
                    }

                    //Insert Margin jika SIS
                    if (sLevel.ToLower() == "admin counter" && !(idJual.Contains("ERROR")))
                    {
                        string margin = tbMarginSIS.Text == "" ? "0" : tbMarginSIS.Text;
                        shBayarDA.insertNilaiMargin(Convert.ToInt64(idJual), Convert.ToDecimal(margin));
                    }
                }

                if (divStore.Visible)//SIS
                {
                    shBayar.TGL_TRANS = tglTrans;
                    shBayarDA.updateTglTrans(shBayar);

                    SH_JUAL jual = new SH_JUAL();
                    jual.ID_BAYAR = shBayar.ID;
                    jual.TGL_TRANS = tglTrans;
                    shBayarDA.updateJualTglTrans(jual);
                }
                else//FSS
                {
                    //printStruck();
                    PRINT_DA printDA = new PRINT_DA();
                    //printDA.print();
                }

                lbDONEBON.Text = noBon;
                lblDONEChange.Text = string.Format("{0:0,0.00}", Convert.ToDouble(change));
                bDoneGiftRecipt.Visible = kodeToko == "SOSFSS01" || kodeToko == "SOSFSS04" || kodeToko == "SOSFSS05" || kodeToko == "SOSFSS06" || kodeToko == "SOSFSS07" ? false : true;
                //bDoneLinkStruck.Visible = bDoneGiftRecipt.Visible ? false : true;
                bDoneLinkStruck.Visible = true;
               ModalChange.Show();

            }
            else
            {
                DivMessage.InnerText = "Error : " + ret == "Berhasil!" ? retCard : ret;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
                isSuccess = false;
            }

            return isSuccess;
        }

        protected void printStruck(string noBon)
        {
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
            MS_SHOWROOM show = new MS_SHOWROOM();
            
            string sUser = Session["UName"].ToString();
            string sKode = Session["UKode"].ToString();
            show = showDA.getShowRoom(String.Format(" where KODE = '{0}'", sKode)).First();

            PRINT_DA printDA = new PRINT_DA(noBon, show, sUser);
            iframePDF.Visible = true;
            createStruck(noBon, show);

            //firstDelete();
            deleteTempAcara();
            bindGrid();
            //lbTestPrint.Text = printDA.print();
            //PrintReceipt();
        }

        protected void clearForm()
        {
            hdnMemberNumber.Value = "";
            hdnId.Value = "";
            tbPAYBank.Text = "";
            tbPAYChange.Text = "";
            tbPAYEdc.Text = "";
            tbPAYKDCard.Text = "";
            tbPAYNoCard.Text = "";
            tbPAYVLCard.Text = "";
            tbPAYNoVoucher.Text = "";
            tbPAYNilaiVoucher.Text = "";
            rbBYRPay.SelectedIndex = -1;
            txtNoBonManual.Text = "";
            tbNPayPay.Text = "";
            TxtFRemark.Text = "";
            cbNPayCard.Checked = false;

            DivMessage.Visible = false;
            DivNCardMessage.Visible = false;
            DivNPayMessage.Visible = false;
            DivPAYMessage.Visible = false;

            iframePDF.Visible = false;

            divRetur.Visible = false;

            clearBank();
        }

        protected void clearSession()
        {
            Session.Remove("IBayar");
            Session.Remove("Voucher");
            Session.Remove("ICard");
            Session.Remove("UCard");
            Session.Remove("IJual");
            Session.Remove("UStock");
            lblWrongPin.Text = "0";
        }

        protected void clearBank()
        {
            //tbNCardBank.Text = "";
            ddlCardName.SelectedIndex = 0;
            ddlNCardKodeCard.SelectedIndex = 0;
            tbNCardNoCard.Text = "";
            tbNCardPay.Text = "";
            tbNCardPrice.Text = "";
            tbNCardQty.Text = "";
            tbNCardVLCard.Text = "";

            tbNPayNilaiVou.Text = "";
            tbNPayNoVou.Text = "";

            tbPAYNoCard.Text = "";
            tbPAYKDCard.Text = "";
            tbPAYVLCard.Text = "";
            tbPAYBank.Text = "";
            tbPAYEdc.Text = "";
        }

        protected void inputBank()
        {
            tbPAYNoCard.Text = "1";
            tbPAYKDCard.Text = "1";
            tbPAYVLCard.Text = "1";
            tbPAYBank.Text = "1";
            tbPAYEdc.Text = "1";
        }

        protected string insertIntoSHJUAL()
        {
            string isSuccess = "Berhasil!";
            try
            {
                SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                MS_STOCK_DA stockDA = new MS_STOCK_DA();
                string idTemp = hdnNPayIDBayar.Value;
                string toko = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedItem.Text : Session["UStore"].ToString();
                string kodeToko = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedValue : Session["UKode"].ToString(); 
                string sName = Session["UName"].ToString();
                string sLevel = Session["ULevel"].ToString();

                string noUrutID = hdnNoUrut.Value;
                string id = noUrutID.Length > 4 ? noUrutID.Remove(0, noUrutID.Length - 4) : noUrutID.PadLeft(4, '0');
                string idStore = Session["UIdStore"].ToString();
                idStore = idStore.Length > 3 ? idStore.Remove(0, idStore.Length - 3) : idStore.PadLeft(3, '0');

                DateTime dt = DateTime.Now;
                string tgl = string.Format("{0:yyMM}", dt);

                string date = tbDate.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                }

                //string noBon = tgl + id;
                //string noBon = tgl + idStore + id;
                string noBon = globalNoBon;

                foreach (GridViewRow item in gvMain.Rows)
                {
                    SH_JUAL jual = new SH_JUAL();
                    jual.ID_BAYAR = Convert.ToInt64(idTemp);
                    jual.ID_KDBRG = Convert.ToInt64(item.Cells[20].Text.ToString());
                    jual.KODE_CUST = toko;
                    jual.KODE = kodeToko;
                    jual.NO_BON = noBon;
                    jual.ITEM_CODE = item.Cells[22].Text.ToString();
                    jual.QTY = Convert.ToInt32(item.Cells[7].Text.ToString());
                    jual.TAG_PRICE = Convert.ToDecimal(item.Cells[14].Text.ToString());//Article Price
                    jual.BON_PRICE = Convert.ToDecimal(item.Cells[16].Text.ToString());//Bon Price
                    jual.JNS_DISC = item.Cells[17].Text.ToString().Contains("&nbsp") ? "" : item.Cells[17].Text.ToString();//Jenis Discount
                    //jual.DISC_P = Convert.ToInt32(item.Cells[9].Text.ToString());//Discount Rate
                    jual.DISC_P = Convert.ToDecimal(item.Cells[9].Text.ToString());//Discount Rate

                    jual.NILAI_BYR = Convert.ToDecimal(item.Cells[12].Text.ToString().Replace(",", ""));//Total bayar per item
                    if (dt < NewPPNDate)
                    {
                        jual.DPP = jual.NILAI_BYR - (jual.NILAI_BYR / Convert.ToDecimal(11));//jual.NILAI_BYR / Convert.ToDecimal(1.1);
                        jual.PPN = jual.NILAI_BYR / Convert.ToDecimal(11);
                    }
                    else
                    {
                        jual.DPP = Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(1.11));
                        jual.PPN = jual.NILAI_BYR - Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(1.11));//Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(11));
                    }
                    jual.ALASAN = "";
                    jual.FNOACARA = item.Cells[18].Text.ToString();
                    jual.JUAL_RETUR = item.Cells[21].Text.ToString();
                    jual.DISC_R = jual.JNS_DISC.Contains("P") ? (jual.TAG_PRICE - Convert.ToDecimal(item.Cells[15].Text.ToString())) * jual.QTY
                        : jual.JNS_DISC.Contains("D") || jual.JNS_DISC.Contains("A") ? (Convert.ToDecimal(item.Cells[8].Text.ToString().Replace(",", "")) - Convert.ToDecimal(item.Cells[11].Text.ToString().Replace(",", ""))) * jual.QTY
                        : jual.JNS_DISC.Contains("M") ? jual.TAG_PRICE * 10 / 100 * jual.QTY
                        : jual.JNS_DISC.Contains("E") ? jual.TAG_PRICE * jual.DISC_P / 100 : 0 * jual.QTY;//Discount Price
                    jual.BARCODE = item.Cells[3].Text.ToString();
                    jual.TGL_TRANS = dt;
                    jual.CREATED_BY = sName;
                    if (item.Cells[21].Text.ToString().ToLower() == "yes")
                    {
                        jual.NO_REFF = tbReturNoBon.Text;
                    }
                    else
                    {
                        jual.NO_REFF = "";
                    }
                    string idJual = "";

                    //Update Stock
                    List<MS_STOCK> listStock = new List<MS_STOCK>();
                    MS_STOCK stock = new MS_STOCK();

                    listStock = stockDA.getStock(string.Format(" where BARCODE like '{0}' and KODE like '{1}'", item.Cells[3].Text.Trim(), kodeToko));
                    if (listStock.Count > 0)
                    {
                        stock = listStock.First();
                        stock.STOCK = jual.QTY * -1;
                        stock.UPDATED_BY = Session["UName"].ToString();

                        int sJual = Session["IJual"] == null ? 0 : int.Parse(Session["IJual"].ToString());
                        if ((sJual == item.RowIndex && Session["IJual2"] == null) || item.RowIndex > sJual)
                        {
                            Session["IJual2"] = "Insert";
                            idJual = shBayarDA.insertSHJual(jual);
                            Session.Remove("IJual2");
                            Session["IJual"] = Convert.ToString(item.RowIndex + 1);
                        }

                        int sStock = Session["UStock"] == null ? 0 : int.Parse(Session["UStock"].ToString());
                        if ((sStock == item.RowIndex && Session["UStock2"] == null) || item.RowIndex > sStock)
                        {
                            Session["UStock2"] = "Update";
                            stockDA.updateDataStockWithID(stock);
                            Session.Remove("UStock2");
                            Session["UStock"] = Convert.ToString(item.RowIndex + 1);
                        }
                    }
                    else
                    {
                        stock.ITEM_CODE = jual.ITEM_CODE;
                        stock.BARCODE = item.Cells[3].Text.Trim();
                        stock.WAREHOUSE = toko;
                        stock.KODE = kodeToko;
                        stock.STOCK = jual.QTY * -1;
                        stock.RAK = "000";
                        stock.CREATED_BY = Session["UName"].ToString();

                        stockDA.insertDataStock(stock);

                        int sJual = Session["IJual"] == null ? 0 : int.Parse(Session["IJual"].ToString());
                        if ((sJual == item.RowIndex && Session["IJual2"] == null) || item.RowIndex > sJual)
                        {
                            Session["IJual2"] = "Insert";
                            idJual = shBayarDA.insertSHJual(jual);
                            Session.Remove("IJual2");
                            Session["IJual"] = Convert.ToString(item.RowIndex + 1);
                        }
                    }

                    //Insert Margin jika SIS
                    if (sLevel.ToLower() == "admin counter" && !(idJual.Contains("ERROR")))
                    {
                        string margin = tbMarginSIS.Text == "" ? "0" : tbMarginSIS.Text;
                        shBayarDA.insertNilaiMargin(Convert.ToInt64(idJual), Convert.ToDecimal(margin));
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = "Error : " + ex.Message;
            }
            return isSuccess;
        }

        protected string updateSHBayar(string change)
        {
            string isSuccess = "Berhasil!";
            try
            {
                string sVersion = Session["UVersion"].ToString();
                GLOBALCODE func = new GLOBALCODE();
                DateTime tglTrans = DateTime.Now;
                string date = tbDate.Text.ToString();
                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParseExact(date, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out tglTrans);
                }

                SH_BAYAR shBayar = new SH_BAYAR();
                SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                string kodeStore = Session["UStore"].ToString().ToLower() == "head office" ? ddlStore.SelectedValue : Session["UKode"].ToString();

                List<NO_DOC> listNoDoc = new List<NO_DOC>();
                string whereDoc = string.Format(" where KODE = '{0}' and FLAG = 'SALE'", kodeStore);
                listNoDoc = shBayarDA.getNoDoc(whereDoc);

                //Insert Temp Doc
                TEMP_DOC tempDOC = new TEMP_DOC();
                tempDOC.KODE = kodeStore;
                tempDOC.CREATED_BY = Session["UName"].ToString();
                tempDOC.FLAG = "SALE";
                shBayarDA.insertTempDoc(tempDOC);

                hdnNoUrutBon.Value = listNoDoc.Count == 0 ? "1" : listNoDoc.Last().DIFF_YEAR == 1 ? "1" : (listNoDoc.Last().NO_URUT + 1).ToString();

                int noUrut = Convert.ToInt32(hdnNoUrutBon.Value == "" ? "0" : hdnNoUrutBon.Value); //listNoDoc.Count == 0 ? 1 : listNoDoc.First().DIFF_YEAR == 1 ? 1 : listNoDoc.First().NO_URUT + 1;
                Int64 iDNoDoc = listNoDoc.Count == 0 ? 0 : listNoDoc.Last().ID;
                hdnNoUrut.Value = noUrut.ToString();

                NO_DOC noDoc = new NO_DOC();
                noDoc.ID = iDNoDoc;
                noDoc.NO_URUT = int.Parse(hdnNoUrutBon.Value);
                noDoc.KODE = kodeStore;
                noDoc.CREATED_BY = Session["UName"].ToString();
                noDoc.FLAG = "SALE";

                Session["INoBon"] = "noBon";
                string retNoDoc = hdnNoUrutBon.Value == "1" ? shBayarDA.insertNoDoc(noDoc) : shBayarDA.updateNoDoc(noDoc);

                //DELETE TEMP_DOC

                SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                bayarDA.deleteTempDoc(Session["UName"].ToString());

                string idTemp = hdnNPayIDBayar.Value;
                string noUrutID = hdnNoUrut.Value;
                string id = noUrutID.Length > 4 ? noUrutID.Remove(0, noUrutID.Length - 4) : noUrutID.PadLeft(4, '0');
                string idStore = Session["UIdStore"].ToString();
                
                decimal bayar = Convert.ToDecimal(tbNPayPay.Text == "" ? "0" : tbNPayPay.Text.Replace(",", ""));
                

                MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();
                MS_SHOWROOM show = new MS_SHOWROOM();
                show = showDA.getShowRoom(string.Format(" where KODE = '{0}'", kodeStore)).First();

                idStore = !divStore.Visible ? idStore : show.ID.ToString();
                idStore = idStore.Length > 3 ? idStore.Remove(0, idStore.Length - 3) : idStore.PadLeft(3, '0');

                DateTime dt = DateTime.Now;
                string tgl = string.Format("{0:yyMM}", dt);

                string noBon = tgl + idStore + id;

                shBayar.ID = Convert.ToInt64(idTemp);
                shBayar.NO_BON = noBon;
                shBayar.CARD = cbNPayCard.Checked ? "Yes" : "No";
                shBayar.JM_UANG = bayar;
                shBayar.KEMBALI = Convert.ToDecimal(change);
                shBayar.NET_CASH = bayar - shBayar.KEMBALI;
                shBayar.NET_BAYAR = Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", "")) + Convert.ToDecimal(hdnNPayNilaiVoucher.Value);
                shBayar.VOUCHER = hdnNPayNilaiVoucher.Value == "0" ? "No" : "Yes";
                shBayar.EPC = hdnEmpDisc.Value == "0" ? "No" : "Yes";
                shBayar.MEMBER = hdnMemberDisc.Value == "0" ? "No" : "Yes";
                shBayar.TGL_TRANS = tglTrans;
                shBayar.FVER = sVersion;
                if (tglTrans < NewPPNDate)
                {
                    shBayar.DPP = shBayar.NET_BAYAR / Convert.ToDecimal(1.1);
                    shBayar.PPN = shBayar.NET_BAYAR / Convert.ToDecimal(11);
                }
                else
                {
                    shBayar.DPP = Convert.ToDecimal(shBayar.NET_BAYAR / Convert.ToDecimal(1.11));
                    shBayar.PPN = shBayar.NET_BAYAR - Convert.ToDecimal(shBayar.NET_BAYAR / Convert.ToDecimal(1.11));//Convert.ToDecimal(jual.NILAI_BYR / Convert.ToDecimal(11));
                }
                shBayar.OTHER = Convert.ToDecimal(lbOtherIncome.Text);
                shBayar.NO_MEMBER = hdnMemberNumber.Value;
                shBayar.NO_BONM = txtNoBonManual.Text.Trim();
                shBayar.FREMARK = TxtFRemark.Text.Trim();
                shBayar.ONGKIR = HdnOngkir.Value == "0" ? "No" : "Yes";
                if (HdnBiayaOngkir.Value == "" || HdnBiayaOngkir.Value == null)
                {
                    shBayar.JM_ONGKIR = 0;
                }
                else
                {
                    shBayar.JM_ONGKIR = Convert.ToDecimal(HdnBiayaOngkir.Value);
                }
                if (HdnFreeOngkir.Value == "" || HdnFreeOngkir.Value == null)
                {
                    shBayar.JM_FREE_ONGKIR = 0;
                }
                else
                {
                    shBayar.JM_FREE_ONGKIR = Convert.ToDecimal(HdnFreeOngkir.Value);
                }
                string ret = shBayarDA.updateBayar(shBayar);

                if (shBayar.VOUCHER == "Yes")
                {
                    #region "SH_VOUCHER"
                    SH_VOUCHER voucher = new SH_VOUCHER();
                    voucher.ID_BAYAR = Convert.ToInt64(hdnNPayIDBayar.Value);
                    voucher.KODE_CUST = Session["UStore"].ToString();
                    voucher.NO_VCR = lblNoVoucher.Text;
                    voucher.NILAI = Convert.ToDecimal(lblNilaiVoucher.Text.Replace(",", "")); //Convert.ToDecimal(tbNPayNilaiVou.Text.Replace(",", ""));
                    voucher.NO_CARD = "";
                    voucher.NO_BON = noBon;
                    voucher.CREATED_BY = Session["UName"].ToString();
                    string berhasil = bayarDA.insertSHVoucherNew(voucher);
                    #endregion

                    #region "MS_VOUCHER"
                    MS_VOUCHER msvoucher = new MS_VOUCHER();
                    msvoucher.NO_VOUCHER = lblNoVoucher.Text;
                    msvoucher.KODE = Session["UStore"].ToString();
                    msvoucher.STATUS_VOUCHER = "DONE";
                    msvoucher.UPDATED_BY = Session["UName"].ToString();
                    bayarDA.updateMsVoucherNew(msvoucher);
                    #endregion
                }
                #region "EPC"
                if (shBayar.EPC == "Yes")
                {
                    MS_EMPLOYEE_DA msEmpDA = new MS_EMPLOYEE_DA();
                    MS_EMPLOYEE MsEmp = new MS_EMPLOYEE();
                    MsEmp.NIK = tbEmpNoCard.Text;
                    MsEmp.SISA_LIMIT = Convert.ToDecimal(tbNPayTotalPrice.Text.Replace(",", ""));
                    MsEmp.UPDATED_BY = Session["UName"].ToString();
                    msEmpDA.updSisaLimtEmpl(MsEmp);
                }
                #endregion
                //NO_DOC noDoc = new NO_DOC();
                //noDoc.ID = iDNoDoc;
                //noDoc.NO_URUT = int.Parse(noUrutID);
                //noDoc.KODE = kodeStore;
                //noDoc.CREATED_BY = Session["UName"].ToString();
                //noDoc.FLAG = "SALE";
                
                //if (Session["INoBon"] == null)
                //{
                //    Session["INoBon"] = "noBon";
                //    ret = noUrutID == "1" ? shBayarDA.insertNoDoc(noDoc) : shBayarDA.updateNoDoc(noDoc);
                 
                //    //DELETE TEMP_DOC

                //    SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
                //    bayarDA.deleteTempDoc(Session["UName"].ToString());
                //}
                lbDONEBON.Text = noBon;
                globalNoBon = noBon;
                lblDONEChange.Text = string.Format("{0:0,0.00}", Convert.ToDouble(change));
                isSuccess = noBon;

                bDoneGiftRecipt.Visible = kodeStore == "SOSFSS01" || kodeStore == "SOSFSS04" || kodeStore == "SOSFSS05" || kodeStore == "SOSFSS06" || kodeStore == "SOSFSS07" ? false : true;
                //bDoneLinkStruck.Visible = bDoneGiftRecipt.Visible ? false : true;
                bDoneLinkStruck.Visible = true;
               bDoneLinkReprint.Visible = hdnEmpDisc.Value == "0" ? false : true;

                //Jika menggunakan member
                //Insert poin 
                if (hdnMemberNumber.Value != "")
                {
                    SendMail_DA mailDA = new SendMail_DA();
                    
                    //Update insert point
                    if (hdnMemberStatus.Value == "RP") //Redeem Only
                    {
                        bayarDA.insertUpdatePointMember(hdnMemberNumber.Value, shBayar.ID, Convert.ToInt32(shBayar.NET_BAYAR * 10 / 100));// Convert.ToInt32(shBayar.NET_BAYAR * Convert.ToDecimal(0.1))
                    }
                    else if (hdnMemberStatus.Value == "RD")//Redeem + Add Point
                    {
                        //Update status Wait Jadi Done
                        bayarDA.insertUpdatePointMemberRedeem(hdnMemberNumber.Value, shBayar.ID, Convert.ToInt32(hdnMemberNilai.Value));// Convert.ToInt32(shBayar.NET_BAYAR * Convert.ToDecimal(0.1))

                        bayarDA.insertUpdatePointMember(hdnMemberNumber.Value, shBayar.ID, Convert.ToInt32(shBayar.NET_BAYAR * 10 / 100));// Convert.ToInt32(shBayar.NET_BAYAR * Convert.ToDecimal(0.1))
                    }
                    ////Mail sender bwlum diketahui
                    //mailDA.sendMailPoint(hdnMemberNumber.Value);
                }
                PnlOngkir.Visible = false;
                txtBiayaOngkir.Text = "";
                txtFreeOngkir.Text = "";
                HdnOngkir.Value = "0";
                HdnBiayaOngkir.Value = "0";
                HdnFreeOngkir.Value = "0";
                CbOngkir.Checked = false;
                ModalChange.Show();
                func.addLog("Sales > Sale > NO BON : " + noBon, Session["UName"].ToString());
            }
            catch (Exception ex)
            {
                isSuccess = "Error : " + ex.Message;
                DivNPayMessage.InnerText = isSuccess;
                DivNPayMessage.Attributes["class"] = "warning";
                DivNPayMessage.Visible = true;
                ModalNewInputPayment.Show();
            }
            return isSuccess;
        }

        protected string insertIntoSHCard(string session, string sessionU)
        {
            string isSuccess = "Berhasil!";
            try
            {
                SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
                string idTemp = hdnNPayIDBayar.Value;

                SH_CARD shCard = new SH_CARD();
                shCard.ID_BAYAR = Convert.ToInt64(idTemp);
                shCard.NET_CARD = Convert.ToDecimal(tbNCardPay.Text.Replace(".00", "").Replace(",", ""));
                shCard.KD_CCARD = ddlNCardKodeCard.SelectedValue;
                shCard.NO_CCARD = tbNCardNoCard.Text;
                shCard.VL_CCARD = tbNCardVLCard.Text;
                shCard.BANK = ddlCardName.SelectedItem.Text;//tbNCardBank.Text;
                shCard.EDC = ddlNCardEdc.SelectedValue;

                SH_BAYAR bayar = new SH_BAYAR();
                bayar.ID = shCard.ID_BAYAR;
                bayar.JM_CARD = shCard.NET_CARD;

                string sInsert = Session["ICard"] == null ? "" : Session["ICard"].ToString();
                if(sInsert != session)
                {
                    Session["ICard"] = session;
                    shBayarDA.insertSHCard(shCard);
                }

                string sUpdate = Session["UCard"] == null ? "" : Session["UCard"].ToString();
                if (sUpdate != sessionU)
                {
                    Session["UCard"] = sessionU;
                    shBayarDA.updateBayarCard(bayar);
                }
            }
            catch (Exception ex)
            {
                isSuccess = "Error : " + ex.Message;
                DivMessage.InnerText = "Error : " + ex.Message;
                DivMessage.Attributes["class"] = "error";
                DivMessage.Visible = true;
            }
            return isSuccess;
        }

        protected decimal countTotalPrice()
        {
            Decimal totalPrice = 0;
            foreach (GridViewRow item in gvMain.Rows)
            {
                totalPrice = totalPrice + Convert.ToDecimal(item.Cells[12].Text);
            }
            totalPrice = totalPrice < 0 ? 0 : totalPrice;

            if (hdnMemberStatus.Value == "RD")
            {
                //decimal point = Convert.ToDecimal(hdnMemberNilai.Value) * 1000;
                decimal point = Convert.ToDecimal(hdnMemberNilai.Value);
                totalPrice = totalPrice - point;
            }

            //decimal discTotalMember = 0;
            //int discMember = int.Parse(hdnMemberDisc.Value == "" ? "0" : hdnMemberDisc.Value);
            //discTotalMember = (totalPrice * discMember) / 100;
            //totalPrice = totalPrice - discTotalMember;
            return totalPrice;
        }

        protected decimal countTotalBayar()
        {
            Decimal totalPrice = 0;
            foreach (GridViewRow item in gvMain.Rows)
            {
                totalPrice = totalPrice + Convert.ToDecimal(item.Cells[12].Text);
                totalPrice = totalPrice + Convert.ToDecimal(item.Cells[10].Text);
            }
            totalPrice = totalPrice < 0 ? 0 : totalPrice;

            //decimal discTotalMember = 0;
            //int discMember = int.Parse(hdnMemberDisc.Value == "" ? "0" : hdnMemberDisc.Value);
            //discTotalMember = (totalPrice * discMember) / 100;
            //totalPrice = totalPrice - discTotalMember;
            return totalPrice;
        }

        protected decimal countNilaiPrice()
        {
            Decimal totalPrice = 0;
            foreach (GridViewRow item in gvMain.Rows)
            {
                totalPrice = totalPrice + Convert.ToDecimal(item.Cells[8].Text);
            }
            totalPrice = totalPrice < 0 ? 0 : totalPrice;

            //decimal discTotalMember = 0;
            //int discMember = int.Parse(hdnMemberDisc.Value == "" ? "0" : hdnMemberDisc.Value);
            //discTotalMember = (totalPrice * discMember) / 100;
            //totalPrice = totalPrice - discTotalMember;
            return totalPrice;
        }

        protected void clearMember()
        {
            SH_BAYAR_DA shBayarDA = new SH_BAYAR_DA();
            Int64 idTemp = new Int64();
            idTemp = Convert.ToInt64(hdnIdBYR.Value);

            shBayarDA.deleteShMember(idTemp);
            shBayarDA.deleteShEmployee(idTemp);
        }

        private void PrintReceipt()
        {
            PosPrinter printer = GetReceiptPrinter();
            try
            {
                ConnectToPrinter(printer);

                PrintReceiptHeader(printer, "ABCDEF Pte. Ltd.", "123 My Street, My City,", "My State, My Country", "012-3456789", DateTime.Now, "ABCDEF");

                PrintLineItem(printer, "Item 1", 10, 99.99);
                PrintLineItem(printer, "Item 2", 101, 0.00);
                PrintLineItem(printer, "Item 3", 9, 0.1);
                PrintLineItem(printer, "Item 4", 1000, 1);

                PrintReceiptFooter(printer, 1, 0.1, 0.1, "THANK YOU FOR CHOOSING ABC Ptr. Ltd.");
            }
            finally
            {
                DisconnectFromPrinter(printer);
            }
        }

        private void DisconnectFromPrinter(PosPrinter printer)
        {
            printer.Release();
            printer.Close();
        }

        private void ConnectToPrinter(PosPrinter printer)
        {
            printer.Open();
            printer.Claim(10000);
            printer.DeviceEnabled = true;
        }

        private PosPrinter GetReceiptPrinter()
        {
            PosExplorer posExplorer = new PosExplorer();
            PosExplorer myPosExplorer = new PosExplorer();
            DeviceCollection myDevices = myPosExplorer.GetDevices(DeviceType.PosPrinter);
            foreach (DeviceInfo devInfo in myDevices)
            {
                string a = "";
            }
            DeviceInfo receiptPrinterDevice = myPosExplorer.GetDevice(DeviceType.PosPrinter, "mainPrinter");
            return (PosPrinter)posExplorer.CreateInstance(receiptPrinterDevice);
        }

        private void PrintReceiptFooter(PosPrinter printer, int subTotal, double tax, double discount, string footerText)
        {
            string offSetString = new string(' ', printer.RecLineChars / 2);

            PrintTextLine(printer, new string('-', (printer.RecLineChars / 3) * 2));
            PrintTextLine(printer, offSetString + String.Format("SUB-TOTAL     {0}", subTotal.ToString("#0.00")));
            PrintTextLine(printer, offSetString + String.Format("TAX           {0}", tax.ToString("#0.00")));
            PrintTextLine(printer, offSetString + String.Format("DISCOUNT      {0}", discount.ToString("#0.00")));
            PrintTextLine(printer, offSetString + new string('-', (printer.RecLineChars / 3)));
            PrintTextLine(printer, offSetString + String.Format("TOTAL         {0}", (subTotal - (tax + discount)).ToString("#0.00")));
            PrintTextLine(printer, offSetString + new string('-', (printer.RecLineChars / 3)));
            PrintTextLine(printer, String.Empty);

            //Embed 'center' alignment tag on front of string below to have it printed in the center of the receipt.
            PrintTextLine(printer, System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' }) + footerText);

            //Added in these blank lines because RecLinesToCut seems to be wrong on my printer and
            //these extra blank lines ensure the cut is after the footer ends.
            PrintTextLine(printer, String.Empty);
            PrintTextLine(printer, String.Empty);
            PrintTextLine(printer, String.Empty);
            PrintTextLine(printer, String.Empty);
            PrintTextLine(printer, String.Empty);

            //Print 'advance and cut' escape command.
            PrintTextLine(printer, System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'1', (byte)'0', (byte)'0', (byte)'P', (byte)'f', (byte)'P' }));
        }

        private void PrintLineItem(PosPrinter printer, string itemCode, int quantity, double unitPrice)
        {
            PrintText(printer, TruncateAt(itemCode.PadRight(9), 9));
            PrintText(printer, TruncateAt(quantity.ToString("#0.00").PadLeft(9), 9));
            PrintText(printer, TruncateAt(unitPrice.ToString("#0.00").PadLeft(10), 10));
            PrintTextLine(printer, TruncateAt((quantity * unitPrice).ToString("#0.00").PadLeft(10), 10));
        }

        private void PrintReceiptHeader(PosPrinter printer, string companyName, string addressLine1, string addressLine2, string taxNumber, DateTime dateTime, string cashierName)
        {
            PrintTextLine(printer, companyName);
            PrintTextLine(printer, addressLine1);
            PrintTextLine(printer, addressLine2);
            PrintTextLine(printer, taxNumber);
            PrintTextLine(printer, new string('-', printer.RecLineChars / 2));
            PrintTextLine(printer, String.Format("DATE : {0}", dateTime.ToShortDateString()));
            PrintTextLine(printer, String.Format("CASHIER : {0}", cashierName));
            PrintTextLine(printer, String.Empty);
            PrintText(printer, "item      ");
            PrintText(printer, "qty       ");
            PrintText(printer, "Unit Price ");
            PrintTextLine(printer, "Total      ");
            PrintTextLine(printer, new string('=', printer.RecLineChars));
            PrintTextLine(printer, String.Empty);

        }

        private void PrintText(PosPrinter printer, string text)
        {
            if (text.Length <= printer.RecLineChars)
                printer.PrintNormal(PrinterStation.Receipt, text); //Print text
            else if (text.Length > printer.RecLineChars)
                printer.PrintNormal(PrinterStation.Receipt, TruncateAt(text, printer.RecLineChars)); //Print exactly as many characters as the printer allows, truncating the rest.
        }

        private void PrintTextLine(PosPrinter printer, string text)
        {
            if (text.Length < printer.RecLineChars)
                printer.PrintNormal(PrinterStation.Receipt, text + Environment.NewLine); //Print text, then a new line character.
            else if (text.Length > printer.RecLineChars)
                printer.PrintNormal(PrinterStation.Receipt, TruncateAt(text, printer.RecLineChars)); //Print exactly as many characters as the printer allows, truncating the rest, no new line character (printer will probably auto-feed for us)
            else if (text.Length == printer.RecLineChars)
                printer.PrintNormal(PrinterStation.Receipt, text + Environment.NewLine); //Print text, no new line character, printer will probably auto-feed for us.
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        protected void createStruckOld(string noBon, MS_SHOWROOM show)
        {
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Outbond_Delivery_" + "" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string sUser = Session["UName"].ToString();
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            SH_BAYAR bayar = new SH_BAYAR();

            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + sUser + "'");
            bayar = bayarDA.getSHBayar(" where NO_BON = '" + noBon + "'").First();

            using (MemoryStream myMemoryStream = new MemoryStream())
            {

                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);

                //Document pdfDoc = new Document(PageSize._11X17, 10f, 10f, 10f, 0f);
                float hor = 450f + (struckList.Count - 1) * 30f;
                Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);

                //Document pdfTest = new Document(PageSize.
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                pdfDoc.Open();
                //PdfWriter.GetInstance(pdfDoc, "");
                #region Create PDF

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                /**********************************CREATE TEMPLATE**********************************/
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 205f;
                table.LockedWidth = true;
                float[] widths = new float[] { 2f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                table.SpacingBefore = 25f;
                table.SpacingAfter = 0f;
                /*Create The First Table */
                PdfPCell header = new PdfPCell(new Phrase(show.SHOWROOM, title1));
                header.Border = 0;
                header.PaddingBottom = 2f;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT + " \n" + show.PHONE, regular)));
                call3.BorderWidth = 0;
                call3.HorizontalAlignment = 1;
                call3.PaddingBottom = 15f;
                table.AddCell(call3);

                //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
                PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) +
                    "\n \n" + "Cashier : " + Session["UName"].ToString(), regular)));
                call1.BorderWidth = 0;
                call1.BorderWidthTop = 0.5f;
                call1.PaddingTop = 10f;
                call1.PaddingBottom = 10f;
                table.AddCell(call1);
                //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

                //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
                PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article \nPrice     Discount     Net Discount\nNet Price", regular)));
                call5.BorderWidth = 0;
                call5.BorderWidthTop = 0.5f;
                call5.BorderWidthBottom = 0.5f;
                call5.PaddingTop = 7f;
                call5.PaddingBottom = 7f;
                table.AddCell(call5);

                decimal total = 0;
                foreach (TEMP_STRUCK item in struckList)
                {
                    PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ART_DESC + " " + item.WARNA + " " + item.SIZE + " \n" +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.PRICE)) + "     " + item.DISCOUNT.ToString() + "     " +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_DISCOUNT)) + "\n" + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE * item.QTY)), regular)));
                    call2.BorderWidth = 0;
                    call2.PaddingTop = 4f;
                    call2.PaddingBottom = 4f;
                    table.AddCell(call2);
                    total = total + (item.NET_PRICE * item.QTY);
                }

                PdfPCell callTotal = new PdfPCell(new Phrase(new Chunk("Total Net : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(total)), regular)));
                callTotal.BorderWidth = 0;
                callTotal.BorderWidthTop = 0.5f;
                callTotal.BorderWidthBottom = 0.5f;
                callTotal.PaddingTop = 7f;
                callTotal.PaddingBottom = 7f;
                table.AddCell(callTotal);

                if (bayar.VOUCHER == "Yes")
                {
                    PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Voucher : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_VOUCHER)), regular)));
                    callCIF.BorderWidth = 0;
                    table.AddCell(callCIF);
                }

                if (bayar.CARD == "Yes")
                {
                    PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("Total Card : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_CARD)), regular)));
                    callCP.BorderWidth = 0;
                    callCP.PaddingTop = 4f;
                    callCP.PaddingBottom = 4f;
                    table.AddCell(callCP);
                }

                PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Total Cash : Rp " + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_UANG)), regular)));
                callAcNo.BorderWidth = 0;
                callAcNo.PaddingTop = 5f;
                callAcNo.PaddingBottom = 5f;
                table.AddCell(callAcNo);

                PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Change : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.KEMBALI)), regular)));
                callCreatedBy.BorderWidth = 0;
                table.AddCell(callCreatedBy);

                //create a black line
                PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                black.BackgroundColor = new BaseColor(0, 0, 0);
                black.BorderColor = new BaseColor(0, 0, 0);
                //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                black.BorderWidth = 0.5f;
                table.AddCell(black);
                ///////////////////////////
                PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \nExchange Non-discount items within 7 days. \n" +
                    "Term & condition apply.", regularTerm)));
                bottom1.PaddingTop = 3f;
                bottom1.BorderWidth = 0.5f;
                table.AddCell(bottom1);
                
                pdfDoc.Add(table);


                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                myPDFWriter.AddJavaScript(jAction);
                #endregion
                /*Template Done*/
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();

                using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\" + noBon + ".pdf")))
                //using(FileStream fs = File.Create(Request.PhysicalApplicationPath + "1.pdf"))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                iframePDF.Attributes["src"] = "..\\Bon\\" + noBon + ".pdf";
                string url = HttpContext.Current.Request.Url.Authority.ToString();
                bDoneLinkStruck.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
                bDoneLinkReprint.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
                //bDoneLinkStruck.HRef = "http://112.78.148.18:8137/Bon/" + noBon + ".pdf";
                //Response.Write(pdfDoc);
                //Response.Redirect("PopUp.aspx?noBukti=" + noBukti);
                //Response.End();
                //Response.Redirect(@"E:\New folder\test.pdf");
                //string pageurl = "~/webpages/frmCrystalReportViewer.aspx?VoucherNo=" + txtVoucherNo.Text + "&VoucherDate=" + txtVoucherDate.Text + " &strUserCode=" + strUserCode.ToString() + "&strCompanyCode=" + strCompanyCode.ToString() + "&formName=frmPaymentVoucher";
                //string pageurl = @"~/Upload/test.pdf";

                //Response.Write("<script>");
                ////Response.Write("window.open('" + pageurl + "','_blank')");
                //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(pageurl)));
                //Response.Write("</script>");

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pdfwindow", "window.open('E://New folder//test.pdf','_blank','width=250,height=200');", true);
            }
        }

        protected void createStruck(string noBon, MS_SHOWROOM show)
        {
            string sUser = Session["UName"].ToString(); ;

            #region NEW
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<VW_REPRINTBON> struckList = new List<VW_REPRINTBON>();
            struckList = bayarDA.GetReprintBon(" where NO_BON = '" + noBon + "'");
            decimal ttlOngkir = struckList.FirstOrDefault().JM_ONGKIR;
            decimal ttlFreeOngkir = struckList.FirstOrDefault().JM_FREE_ONGKIR;
            decimal FinalOngkir = ttlOngkir - ttlFreeOngkir;
            String NmPT = "";
            DateTime dtChangeName = Convert.ToDateTime("2023-01-01");
            try
            {
                using (MemoryStream myMemoryStream = new MemoryStream())
                {

                    float hor = 450f + (struckList.Count - 1) * 30f;
                    Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                    PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);
                    pdfDoc.Open();
                    #region Create PDF

                    string imageURL = "";
                    if (struckList.FirstOrDefault().LOGO_IMG == "" || struckList.FirstOrDefault().LOGO_IMG == null)
                    {
                        imageURL = Server.MapPath("..\\Image\\Logo_Atmos_Jakarta.png");
                    }
                    else
                    {
                        imageURL = Server.MapPath(struckList.FirstOrDefault().LOGO_IMG);
                    }

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    jpg.ScaleToFit(100f, 80f);
                    jpg.SpacingBefore = 50f;
                    jpg.SpacingAfter = 20f;
                    jpg.Alignment = Element.ALIGN_CENTER;

                    pdfDoc.Add(jpg);

                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 6, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                    iTextSharp.text.Font regularTerm2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                    /**********************************CREATE TEMPLATE**********************************/
                    PdfPTable table = new PdfPTable(1);
                    table.TotalWidth = 205f;
                    table.LockedWidth = true;
                    float[] widths = new float[] { 2f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = 0;
                    //leave a gap before and after the table
                    //table.SpacingBefore = 1f;
                    //table.SpacingAfter = 0f;
                    /*Create The First Table */
                    PdfPCell header = new PdfPCell(new Phrase(struckList.FirstOrDefault().SHOWROOM, title1));
                    header.Border = 0;
                    header.PaddingBottom = 2f;
                    header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(header);

                    PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(struckList.FirstOrDefault().ALAMAT + " \n" + struckList.FirstOrDefault().PHONE, regular)));
                    call3.BorderWidth = 0;
                    call3.HorizontalAlignment = 1;
                    call3.PaddingBottom = 15f;
                    table.AddCell(call3);
                    //if (DateTime.Now < dtChangeName)
                    //{
                    //    NmPT = "PT Atmos Isvara Ritelindo";
                    //}
                    //else
                    //{
                        NmPT = "PT Atomica Isvara Ritelindo";
                    //}
                    //Penambahan informasi NPWP
                    //PdfPCell callNMPT = new PdfPCell(new Phrase(new Chunk("PT Atmos Isvara Ritelindo", regular)));
                    PdfPCell callNMPT = new PdfPCell(new Phrase(new Chunk(NmPT, regular)));
                    callNMPT.BorderWidth = 0;
                    callNMPT.PaddingBottom = 2f;
                    table.AddCell(callNMPT);

                    PdfPCell callNONPWP = new PdfPCell(new Phrase(new Chunk("No NPWP : 92.941.243.5-022.000", regular)));
                    callNONPWP.BorderWidth = 0;
                    callNONPWP.PaddingBottom = 5f;
                    table.AddCell(callNONPWP);

                    //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
                    PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", struckList.FirstOrDefault().CREATED_DATE) +
                        "\n" + "Cashier : " + struckList.FirstOrDefault().CREATED_BY, regular)));
                    call1.BorderWidth = 0;
                    call1.BorderWidthTop = 0.5f;
                    call1.PaddingTop = 7f;
                    call1.PaddingBottom = 7f;
                    table.AddCell(call1);
                    //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

                    //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
                    PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article \nPrice            Discount          Net Discount           Net Price", regular)));
                    call5.BorderWidth = 0;
                    call5.BorderWidthTop = 0.5f;
                    call5.BorderWidthBottom = 0.5f;
                    call5.PaddingTop = 7f;
                    call5.PaddingBottom = 7f;
                    table.AddCell(call5);

                    decimal total = 0;
                    decimal totalDPP = 0;
                    decimal totalPPN = 0;
                    foreach (VW_REPRINTBON item in struckList)
                    {
                        PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ARTICLE + " \n" +
                            string.Format("{0:0,0.00}", Convert.ToDouble(item.BON_PRICE)) + "       " + item.DISC_P.ToString("G29") + "         " +
                            string.Format("{0:0,0.00}", Convert.ToDouble(item.DISC_R)) + "          " + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE)), regular)));
                        call2.BorderWidth = 0;
                        call2.PaddingTop = 6f;
                        call2.PaddingBottom = 10f;
                        table.AddCell(call2);
                        total = total + item.NET_PRICE;
                        totalDPP = totalDPP + item.DPP;
                        totalPPN = totalPPN + item.PPN;

                    }

                    PdfPCell callTotal = new PdfPCell(new Phrase(new Chunk("Total Net : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(total)), regular)));
                    callTotal.BorderWidth = 0;
                    callTotal.BorderWidthTop = 0.5f;
                    callTotal.BorderWidthBottom = 0.5f;
                    callTotal.PaddingTop = 7f;
                    callTotal.PaddingBottom = 7f;
                    table.AddCell(callTotal);
                    if (struckList.FirstOrDefault().ONGKIR == "Yes")
                    {
                        PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Ongkir : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(FinalOngkir)), regular)));
                        callCIF.BorderWidth = 0;
                        table.AddCell(callCIF);
                    }
                    if (struckList.FirstOrDefault().VOUCHER == "Yes")
                    {
                        PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Voucher : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_VOUCHER)), regular)));
                        callCIF.BorderWidth = 0;
                        table.AddCell(callCIF);
                    }

                    if (struckList.FirstOrDefault().CARD == "Yes")
                    {
                        PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("Total Card : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_CARD)), regular)));
                        callCP.BorderWidth = 0;
                        callCP.PaddingTop = 4f;
                        callCP.PaddingBottom = 4f;
                        table.AddCell(callCP);
                    }

                    PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Total Cash : Rp " + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_UANG)), regular)));
                    callAcNo.BorderWidth = 0;
                    callAcNo.PaddingTop = 5f;
                    callAcNo.PaddingBottom = 5f;
                    table.AddCell(callAcNo);

                    PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Change : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().KEMBALI)), regular)));
                    callCreatedBy.BorderWidth = 0;
                    callCreatedBy.PaddingBottom = 5f;
                    table.AddCell(callCreatedBy);

                    //Menampilkan DPP Dan PPN
                    PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalDPP)) + " PPN : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalPPN)), regular)));
                    //PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : " + totalDPP.ToString("c0") + " PPN : " + totalPPN.ToString("c0"), regular)));
                    callDPPN.BorderWidth = 0;
                    callDPPN.PaddingBottom = 5f;
                    table.AddCell(callDPPN);

                    if (struckList.FirstOrDefault().BRAND.ToLower() == "melissa")
                    {
                        PdfPCell callOLShop = new PdfPCell(new Phrase(new Chunk("shop easy 24/7 at melissa.co.id", regular)));
                        callOLShop.BorderWidth = 0;
                        callOLShop.PaddingBottom = 5f;
                        table.AddCell(callOLShop);
                    }

                    //create a black line
                    PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    black.BackgroundColor = new BaseColor(0, 0, 0);
                    black.BorderColor = new BaseColor(0, 0, 0);
                    //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    black.BorderWidth = 0.5f;
                    table.AddCell(black);
                    ///////////////////////////
                    PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \nExchange Non-discount items within 7 days. \n" +
                        "Term & condition apply.", regularTerm2)));
                    bottom1.PaddingTop = 3f;
                    bottom1.BorderWidth = 0.5f;
                    table.AddCell(bottom1);

                    pdfDoc.Add(table);


                    PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                    myPDFWriter.AddJavaScript(jAction);
                    #endregion
                    /*Template Done*/
                    pdfDoc.Close();
                    byte[] content = myMemoryStream.ToArray();

                    using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\" + noBon + ".pdf")))
                    {
                        fs.Write(content, 0, (int)content.Length);
                    }

                    iframePDF.Attributes["src"] = "..\\Bon\\" + noBon + ".pdf";
                    string url = HttpContext.Current.Request.Url.Authority.ToString();
                    bDoneLinkStruck.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
                    bDoneLinkReprint.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
                }
            }
            catch (Exception Ex)
            { }
            #endregion

            #region COMMENT
            //try
            //{
            //    //Response.ContentType = "application/pdf";
            //    //Response.AddHeader("content-disposition", "attachment;filename=Outbond_Delivery_" + "" + ".pdf");
            //    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    string sUser = Session["UName"].ToString(); ;
            //    List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            //    MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            //    SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            //    SH_BAYAR bayar = new SH_BAYAR();
            //    decimal ttlOngkir = bayar.JM_ONGKIR;
            //    decimal ttlFreeOngkir = bayar.JM_FREE_ONGKIR;
            //    decimal FinalOngkir = ttlOngkir - ttlFreeOngkir;
            //    struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + sUser + "'");
            //    bayar = bayarDA.getSHBayar(" where NO_BON = '" + noBon + "'").First();

            //    using (MemoryStream myMemoryStream = new MemoryStream())
            //    {

            //        //StringWriter sw = new StringWriter();
            //        //HtmlTextWriter hw = new HtmlTextWriter(sw);

            //        //Document pdfDoc = new Document(PageSize._11X17, 10f, 10f, 10f, 0f);
            //        float hor = 450f + (struckList.Count - 1) * 30f;
            //        Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
            //        PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);

            //        //Document pdfTest = new Document(PageSize.
            //        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            //        pdfDoc.Open();
            //        //PdfWriter.GetInstance(pdfDoc, "");
            //        #region Create PDF

            //        string imageURL = "";
            //        if (show.LOGO_IMG == "" || show.LOGO_IMG == null || show.LOGO_IMG == "-")
            //        {
            //            imageURL = Server.MapPath("..\\Image\\Logo_Atmos_Jakarta.png");
            //        }
            //        else
            //        {
            //            imageURL = Server.MapPath(show.LOGO_IMG);
            //        }
            //        #region "Comment"
            //        //switch (show.BRAND.ToLower())
            //        //{
            //        //    case "melissa":
            //        //        imageURL = Server.MapPath(show.LOGO_IMG); // Server.MapPath("..\\Image\\Logo_Melissa.png");
            //        //        break;
            //        //    case "denim":
            //        //        imageURL = Server.MapPath("..\\Image\\Logo_SDS.png");
            //        //        break;
            //        //    case "fred perry":
            //        //        imageURL = Server.MapPath("..\\Image\\Logo_FP.png");
            //        //        break;
            //        //    case "707":
            //        //        imageURL = Server.MapPath("..\\Image\\Logo_707.png");
            //        //        break;
            //        //    case "superga":
            //        //        imageURL = Server.MapPath("..\\Image\\Logo_Superga.png");
            //        //        break;
            //        //    default:
            //        //        imageURL = Server.MapPath("..\\Image\\Logo_SOS.png");
            //        //        break;
            //        //}
            //        #endregion
            //        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            //        //Resize image depend upon your need
            //        jpg.ScaleToFit(100f, 80f);
            //        //Give space before image
            //        jpg.SpacingBefore = 50f;
            //        //Give some space after the image
            //        jpg.SpacingAfter = 20f;
            //        jpg.Alignment = Element.ALIGN_CENTER;

            //        pdfDoc.Add(jpg);

            //        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            //        iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
            //        iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
            //        iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 6, iTextSharp.text.Font.NORMAL);
            //        iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD);
            //        iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            //        iTextSharp.text.Font regularTerm2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
            //        /**********************************CREATE TEMPLATE**********************************/
            //        PdfPTable table = new PdfPTable(1);
            //        table.TotalWidth = 205f;
            //        table.LockedWidth = true;
            //        float[] widths = new float[] { 2f };
            //        table.SetWidths(widths);
            //        table.HorizontalAlignment = 0;
            //        //leave a gap before and after the table
            //        //table.SpacingBefore = 1f;
            //        //table.SpacingAfter = 0f;
            //        /*Create The First Table */
            //        PdfPCell header = new PdfPCell(new Phrase(show.SHOWROOM, title1));
            //        header.Border = 0;
            //        header.PaddingBottom = 2f;
            //        header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //        table.AddCell(header);

            //        PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT + " \n" + show.PHONE, regular)));
            //        call3.BorderWidth = 0;
            //        call3.HorizontalAlignment = 1;
            //        call3.PaddingBottom = 15f;
            //        table.AddCell(call3);

            //        //Penambahan informasi NPWP
            //        PdfPCell callNMPT = new PdfPCell(new Phrase(new Chunk("PT Atmos Isvara Ritelindo", regular)));
            //        callNMPT.BorderWidth = 0;
            //        callNMPT.PaddingBottom = 2f;
            //        table.AddCell(callNMPT);

            //        PdfPCell callNONPWP = new PdfPCell(new Phrase(new Chunk("No NPWP : 92.941.243.5-022.000", regular)));
            //        callNONPWP.BorderWidth = 0;
            //        callNONPWP.PaddingBottom = 5f;
            //        table.AddCell(callNONPWP);
            //        //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
            //        PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) +
            //            "\n" + "Cashier : " + Session["UName"].ToString(), regular)));
            //        call1.BorderWidth = 0;
            //        call1.BorderWidthTop = 0.5f;
            //        call1.PaddingTop = 7f;
            //        call1.PaddingBottom = 7f;
            //        table.AddCell(call1);
            //        //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

            //        //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
            //        PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article \nPrice            Discount          Net Discount           Net Price", regular)));
            //        call5.BorderWidth = 0;
            //        call5.BorderWidthTop = 0.5f;
            //        call5.BorderWidthBottom = 0.5f;
            //        call5.PaddingTop = 7f;
            //        call5.PaddingBottom = 7f;
            //        table.AddCell(call5);

            //        decimal total = 0;
            //        decimal totalDPP = 0;
            //        decimal totalPPN = 0;
            //        foreach (TEMP_STRUCK item in struckList)
            //        {
            //            decimal DPP = 0;
            //            decimal PPN = 0;
            //            decimal CalTotal = 0;
            //            PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ART_DESC + " " + item.WARNA + " " + item.SIZE + " \n" +
            //                string.Format("{0:0,0.00}", Convert.ToDouble(item.PRICE)) + "       " + item.DISCOUNT.ToString() + "         " +
            //                string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_DISCOUNT)) + "          " + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE * item.QTY)), regular)));
            //            call2.BorderWidth = 0;
            //            call2.PaddingTop = 6f;
            //            call2.PaddingBottom = 10f;
            //            table.AddCell(call2);
            //            //if (item.RETUR.ToLower() == "yes")
            //            //{
            //            //    total = total + (-1 * (item.NET_PRICE * item.QTY));
            //            //}
            //            //else
            //            //{
            //                total = total + (item.NET_PRICE * item.QTY);
            //            //}
            //            if (bayar.TGL_TRANS < NewPPNDate)
            //            {
            //                DPP = item.NET_PRICE - (item.NET_PRICE / Convert.ToDecimal(11));//jual.NILAI_BYR / Convert.ToDecimal(1.1);
            //                PPN = item.NET_PRICE / Convert.ToDecimal(11);
            //            }
            //            else
            //            {
            //                DPP = item.NET_PRICE / Convert.ToDecimal(1.11);//jual.NILAI_BYR / Convert.ToDecimal(1.1);
            //                PPN = item.NET_PRICE - (item.NET_PRICE / Convert.ToDecimal(1.11));
            //            }
            //            totalDPP = totalDPP + DPP;
            //            totalPPN = totalPPN + PPN;
            //        }

            //        PdfPCell callTotal = new PdfPCell(new Phrase(new Chunk("Total Net : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(total)), regular)));
            //        callTotal.BorderWidth = 0;
            //        callTotal.BorderWidthTop = 0.5f;
            //        callTotal.BorderWidthBottom = 0.5f;
            //        callTotal.PaddingTop = 7f;
            //        callTotal.PaddingBottom = 7f;
            //        table.AddCell(callTotal);
            //        if (bayar.ONGKIR == "Yes")
            //        {
            //            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Ongkir : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(FinalOngkir)), regular)));
            //            callCIF.BorderWidth = 0;
            //            table.AddCell(callCIF);
            //        }
            //        if (bayar.VOUCHER == "Yes")
            //        {
            //            PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Voucher : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_VOUCHER)), regular)));
            //            callCIF.BorderWidth = 0;
            //            table.AddCell(callCIF);
            //        }

            //        if (bayar.CARD == "Yes")
            //        {
            //            PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("Total Card : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_CARD)), regular)));
            //            callCP.BorderWidth = 0;
            //            callCP.PaddingTop = 4f;
            //            callCP.PaddingBottom = 4f;
            //            table.AddCell(callCP);
            //        }

            //        PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Total Cash : Rp " + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.JM_UANG)), regular)));
            //        callAcNo.BorderWidth = 0;
            //        callAcNo.PaddingTop = 5f;
            //        callAcNo.PaddingBottom = 5f;
            //        table.AddCell(callAcNo);

            //        PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Change : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(bayar.KEMBALI)), regular)));
            //        callCreatedBy.BorderWidth = 0;
            //        callCreatedBy.PaddingBottom = 5f;
            //        table.AddCell(callCreatedBy);

            //        //Menampilkan DPP Dan PPN
            //        PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalDPP)) + " PPN : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalPPN)), regular)));
            //        //PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : " + totalDPP.ToString("c0") + " PPN : " + totalPPN.ToString("c0"), regular)));
            //        callDPPN.BorderWidth = 0;
            //        callDPPN.PaddingBottom = 5f;
            //        table.AddCell(callDPPN);



            //        if (show.BRAND.ToLower() == "melissa")
            //        {
            //            PdfPCell callOLShop = new PdfPCell(new Phrase(new Chunk("shop easy 24/7 at melissa.co.id", regular)));
            //            callOLShop.BorderWidth = 0;
            //            callOLShop.PaddingBottom = 5f;
            //            table.AddCell(callOLShop);
            //        }

            //        //create a black line
            //        PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            //        black.BackgroundColor = new BaseColor(0, 0, 0);
            //        black.BorderColor = new BaseColor(0, 0, 0);
            //        //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            //        black.BorderWidth = 0.5f;
            //        table.AddCell(black);
            //        ///////////////////////////
            //        PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \n1. Change normal product max 7 days. \n" +
            //            "    Term & condition apply. \n2. Sale product & Special Price \n    Can not be exchanged or returned", regularTerm2)));
            //        bottom1.PaddingTop = 3f;
            //        bottom1.BorderWidth = 0.5f;
            //        table.AddCell(bottom1);

            //        pdfDoc.Add(table);


            //        PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
            //        myPDFWriter.AddJavaScript(jAction);
            //        #endregion
            //        /*Template Done*/
            //        pdfDoc.Close();
            //        byte[] content = myMemoryStream.ToArray();

            //        using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\" + noBon + ".pdf")))
            //        {
            //            fs.Write(content, 0, (int)content.Length);
            //        }

            //        iframePDF.Attributes["src"] = "..\\Bon\\" + noBon + ".pdf";
            //        string url = HttpContext.Current.Request.Url.Authority.ToString();
            //        bDoneLinkStruck.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
            //        bDoneLinkReprint.HRef = "http://" + url + "/Bon/" + noBon + ".pdf";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DivMessage.InnerText = "Error : " + ex.Message;
            //    DivMessage.Attributes["class"] = "error";
            //    DivMessage.Visible = true;
            //}
            #endregion

        }

        protected void createGiftStruck(string noBon)
        {
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Outbond_Delivery_" + "" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string sUser = Session["UName"].ToString();
            MS_SHOWROOM show = new MS_SHOWROOM();
            MS_SHOWROOM_DA showDA = new MS_SHOWROOM_DA();

            string sKode = Session["UKode"].ToString();
            show = showDA.getShowRoom(String.Format(" where KODE = '{0}'", sKode)).First();
            List<TEMP_STRUCK> struckList = new List<TEMP_STRUCK>();
            MS_KDBRG_DA kdbrgDA = new MS_KDBRG_DA();
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            SH_BAYAR bayar = new SH_BAYAR();

            struckList = kdbrgDA.getTempStruck(" where CREATED_BY = '" + sUser + "'");
            bayar = bayarDA.getSHBayar(" where NO_BON = '" + noBon + "'").First();

            using (MemoryStream myMemoryStream = new MemoryStream())
            {

                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);

                //Document pdfDoc = new Document(PageSize._11X17, 10f, 10f, 10f, 0f);
                float hor = 450f + (struckList.Count - 1) * 30f;
                Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);

                //Document pdfTest = new Document(PageSize.
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                pdfDoc.Open();
                //PdfWriter.GetInstance(pdfDoc, "");
                #region Create PDF

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                /**********************************CREATE TEMPLATE**********************************/
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 205f;
                table.LockedWidth = true;
                float[] widths = new float[] { 2f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                table.SpacingBefore = 25f;
                table.SpacingAfter = 0f;
                /*Create The First Table */
                PdfPCell header = new PdfPCell(new Phrase(show.SHOWROOM, title1));
                header.Border = 0;
                header.PaddingBottom = 2f;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(show.ALAMAT + " \n" + show.PHONE, regular)));
                call3.BorderWidth = 0;
                call3.HorizontalAlignment = 1;
                call3.PaddingBottom = 15f;
                table.AddCell(call3);

                //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
                PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) +
                    "\n \n" + "Cashier : " + Session["UName"].ToString(), regular)));
                call1.BorderWidth = 0;
                call1.BorderWidthTop = 0.5f;
                call1.PaddingTop = 10f;
                call1.PaddingBottom = 10f;
                table.AddCell(call1);
                //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

                //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
                PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article", regular)));
                call5.BorderWidth = 0;
                call5.BorderWidthTop = 0.5f;
                call5.BorderWidthBottom = 0.5f;
                call5.PaddingTop = 7f;
                call5.PaddingBottom = 7f;
                table.AddCell(call5);

                decimal total = 0;
                foreach (TEMP_STRUCK item in struckList)
                {
                    PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ART_DESC + " " + item.WARNA + " " + item.SIZE, regular)));
                    call2.BorderWidth = 0;
                    call2.PaddingTop = 4f;
                    call2.PaddingBottom = 4f;
                    table.AddCell(call2);
                    total = total + item.NET_PRICE;
                }
                
                //create a black line
                PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                black.BackgroundColor = new BaseColor(0, 0, 0);
                black.BorderColor = new BaseColor(0, 0, 0);
                //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                black.BorderWidth = 0.5f;
                table.AddCell(black);
                ///////////////////////////
                PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \nExchange Non-discount items within 7 days. \n" +
                    "Term & condition apply.", regularTerm)));
                bottom1.PaddingTop = 3f;
                bottom1.BorderWidth = 0.5f;
                table.AddCell(bottom1);

                pdfDoc.Add(table);


                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                myPDFWriter.AddJavaScript(jAction);
                #endregion
                /*Template Done*/
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();

                using (FileStream fs = File.Create(Server.MapPath("..\\Bon\\GIFT_" + noBon + ".pdf")))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                iframePDF.Attributes["src"] = "..\\Bon\\GIFT_" + noBon + ".pdf";
            }
        }

        protected void closeAllModal()
        {
            ModalChange.Hide();
            ModalChangeQty.Hide();
            ModalEmployee.Hide();
            ModalInputPayment.Hide();
            ModalItemRetur.Hide();
            ModalMember.Hide();
            ModalNewCard.Hide();
            ModalNewInputPayment.Hide();
            ModalPaymentMethodAndAcara.Hide();
            //ModalSearchItemCode.Hide();
            ModalSearchNoBon.Hide();
            ModalService.Hide();
        }

        protected void ddlBYRAcara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBYRAcara.SelectedValue == "11")
            {
                trBYRNoID.Visible = true;
                trBYRACaraNama.Visible = true;
            }
            else
            {
                trBYRNoID.Visible = false;
                trBYRACaraNama.Visible = false;
            }
            ModalPaymentMethodAndAcara.Show();
        }

        protected void tbMemberNumber_TextChanged(object sender, EventArgs e)
        {
            MEMBER_DA memberDA = new MEMBER_DA();
            int pointget = 0;

            pointget = memberDA.getPointMember("WHERE NO_CARD = '" + tbMemberNumber.Text + "'");
            lblTotalPoint.Text = (pointget).ToString("N0", new CultureInfo("is-IS"));//String.Format("#.##0", pointget);
            lblredeemablePoint.Text = (pointget-25000).ToString("N0", new CultureInfo("is-IS"));//String.Format("#.##0", (pointget - 25000));
            ModalMember.Show();
        }

        protected void clearhiddenfieldmember()
        {
            hdnMemberNumber.Value = string.Empty;
            hdnMemberID.Value = string.Empty;
            //hdnMemberDisc.Value = "0";
            hdnMemberNilai.Value = string.Empty;
            hdnMemberStatus.Value = string.Empty;
            //hdncountwrongpin.Value = string.Empty;
        }

        #region "SearchShowroom"
        protected void btnFindStore_Click(object sender, EventArgs e)
        {
            ModalSearchStore.Show();
        }
        private void BindGridShowroom()
        {
            MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
            List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
            string sLevel = Session["ULevel"] == null ? "" : Session["ULevel"].ToString();
            if (tbSearchShowRoom.Text != null)
            {
                if (sLevel.ToLower() == "admin counter")
                {
                    lbJudul.Text = "Sales Page (SIS)";
                    listStore = showRoomDA.getShowRoom(" where Showroom like '%" + tbSearchShowRoom.Text + "%' AND STATUS = 'OPEN' and STATUS_SHOWROOM = 'SIS'");
                }
                else if (sLevel.ToLower() == "admin sales")
                {
                    lbJudul.Text = "Sales Page Admin (Showroom)";
                    listStore = showRoomDA.getShowRoom(" where Showroom like '%" + tbSearchShowRoom.Text + "%' AND STATUS = 'OPEN' and STATUS_SHOWROOM = 'FSS'");
                }

                gvShowroom.DataSource = listStore;
                gvShowroom.DataBind();
                ModalSearchStore.Show();
                //gvShowroom.Visible = true;
            }
        }

        protected void btnsearchStore_Click(object sender, EventArgs e)
        {
            BindGridShowroom();
        }
        protected void btnsearchStoreClose_Click(object sender, EventArgs e)
        {
            ModalSearchStore.Hide();
        }
        protected void gvShowroom_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        MS_SHOWROOM_DA showRoomDA = new MS_SHOWROOM_DA();
                        //ddlStore.Text = gvShowroom.Rows[rowIndex].Cells[2].Text.ToString();
                        ////string store = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();
                        ////lblBrand.Text = gvShowroom.Rows[rowIndex].Cells[4].Text.ToString();
                        ////lblAlamat.Text = gvShowroom.Rows[rowIndex].Cells[6].Text.ToString();
                        ////lblPhone.Text = gvShowroom.Rows[rowIndex].Cells[5].Text.ToString();
                        //ddlStore.SelectedValue = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();
                        MS_SHOWROOM showRoom = new MS_SHOWROOM();
                        List<MS_SHOWROOM> listStore = new List<MS_SHOWROOM>();
                        string kdshowroom = gvShowroom.Rows[rowIndex].Cells[3].Text.ToString();

                        showRoom.SHOWROOM = "--Pilih Showroom--";
                        showRoom.KODE = "";
                        listStore = showRoomDA.getShowRoom(" where KODE = '" + kdshowroom + "'");
                        listStore.Insert(0, showRoom);
                        ddlStore.DataSource = listStore;
                        ddlStore.DataBind();
                        ddlStore.Enabled = false;
                        //int index = 0;
                        //int.TryParse(hdnIDStore.Value, out index);
                        ddlStore.SelectedIndex = 1;
                        //gvShowroom.Visible = false;
                        //dShowRoomData.Visible = true;
                        //BindGridShowroom();
                        //dSearchStore.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //DivMessage.InnerText = "Error : " + ex.Message;
                //DivMessage.Attributes["class"] = "error";
                //DivMessage.Visible = true;
            }
        }

        protected void gvShowroom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShowroom.PageIndex = e.NewPageIndex;
            BindGridShowroom();
        }
        #endregion

        #region "Penjagaan Retur Cross Chanel"
        protected void gvIRetur_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sLevel = Session["ULevel"].ToString().ToLower();
            string Sessionkd = Session["UKode"].ToString().ToLower();
            string sKode = "";
            string sSHR = "";
            if (sLevel == "admin sales")
            {
                sKode = ddlStore.SelectedValue.ToLower();
            }
            else if (sLevel == "admin counter")
            {
                if (ddlStore.SelectedValue.ToLower() != "")
                {
                    sKode = ddlStore.SelectedValue.ToLower();
                    sSHR = ddlStore.SelectedItem.Text.ToLower();
                }
                else
                {
                    sKode = Session["UKode"].ToString().ToLower();
                    sSHR = Session["UStore"].ToString().ToLower();
                }
            }
            else
            {
                sKode = Session["UKode"].ToString().ToLower();
                sSHR = Session["UStore"].ToString().ToLower();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string langId2 = DataBinder.Eval(e.Row.DataItem, "StatusTest").ToString();
                string selectedshr = sSHR;//ddlStore.SelectedItem.Text;
                string shrRetured = tbIReturStore.Text.ToLower();
                if (shrRetured.Contains("online"))
                {
                    if (!selectedshr.Contains("online"))
                    {
                        string res = selectedshr;
                        ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgSearchSave");
                        imgBtn.Visible = false;
                        lblInfoRetur.Text = "Barang yang di beli melalui Online Tidak Dapat di retur di toko ";
                        lblInfoRetur.Visible = true;
                    }
                    else
                    { lblInfoRetur.Visible = false; }
                }
                else if (!shrRetured.Contains("online"))
                {
                    if (selectedshr.Contains("online"))
                    {
                        string res = selectedshr;
                        ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgSearchSave");
                        imgBtn.Visible = false;
                        lblInfoRetur.Text = "Barang yang di beli di toko Tidak Dapat di retur di toko Online";
                        lblInfoRetur.Visible = true;
                    }
                    else
                    { lblInfoRetur.Visible = false; }
                }
                else
                { lblInfoRetur.Visible = false; }
            }
        }
        #endregion

        protected void gvIRetur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIRetur.PageIndex = e.NewPageIndex;
            bindItemRetur();
        }
        #region "REPRINT BON"
        protected void btnReprintStruck_Click(object sender, EventArgs e)
        {
            lblFlag.Text = "reprint";
            bindNoBonReprint();
            //ModalPopupReprint.Show();
            //bindNoBon();
        }

        protected void gvBON_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblFlag.Text != "")
                {
                    ImageButton imgBtnsv = (ImageButton)e.Row.FindControl("imgSearchSave");
                    ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgReprint");
                    imgBtn.Visible = true;
                    imgBtnsv.Visible = false;
                }
            }
        }

        protected void RecreateStruck(string noBon)
        {
            string sUser = Session["UName"].ToString(); ;

            #region NEW
            SH_BAYAR_DA bayarDA = new SH_BAYAR_DA();
            List<VW_REPRINTBON> struckList = new List<VW_REPRINTBON>();
            struckList = bayarDA.GetReprintBon(" where NO_BON = '" + noBon + "'");
            decimal ttlOngkir = struckList.FirstOrDefault().JM_ONGKIR;
            decimal ttlFreeOngkir = struckList.FirstOrDefault().JM_FREE_ONGKIR;
            decimal FinalOngkir = ttlOngkir - ttlFreeOngkir;
            String NmPT = "";
            DateTime dtChangeName = Convert.ToDateTime("2023-01-01");
            #endregion

            using (MemoryStream myMemoryStream = new MemoryStream())
            {

                float hor = 450f + (struckList.Count - 1) * 30f;
                Document pdfDoc = new Document(new Rectangle(215f, hor), 10f, 0f, 0f, 0f);
                PdfWriter myPDFWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);
                pdfDoc.Open();
                #region Create PDF

                string imageURL = "";
                if (struckList.FirstOrDefault().LOGO_IMG == "" || struckList.FirstOrDefault().LOGO_IMG == null)
                {
                    imageURL = Server.MapPath("..\\Image\\Logo_Atmos_Jakarta.png");
                }
                else
                {
                    imageURL = Server.MapPath(struckList.FirstOrDefault().LOGO_IMG );
                }

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(100f, 80f);
                jpg.SpacingBefore = 50f;
                jpg.SpacingAfter = 20f;
                jpg.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(jpg);

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font title1 = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font regular = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regularTerm = new iTextSharp.text.Font(bfTimes, 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font regular2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font title2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font regularTerm2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL);
                /**********************************CREATE TEMPLATE**********************************/
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 205f;
                table.LockedWidth = true;
                float[] widths = new float[] { 2f };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                //leave a gap before and after the table
                //table.SpacingBefore = 1f;
                //table.SpacingAfter = 0f;
                /*Create The First Table */
                PdfPCell header = new PdfPCell(new Phrase(struckList.FirstOrDefault().SHOWROOM, title1));
                header.Border = 0;
                header.PaddingBottom = 2f;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                PdfPCell call3 = new PdfPCell(new Phrase(new Chunk(struckList.FirstOrDefault().ALAMAT + " \n" + struckList.FirstOrDefault().PHONE, regular)));
                call3.BorderWidth = 0;
                call3.HorizontalAlignment = 1;
                call3.PaddingBottom = 15f;
                table.AddCell(call3);

                ////Penambahan informasi NPWP
                //if (DateTime.Now < dtChangeName)
                //{
                //    NmPT = "PT Atmos Isvara Ritelindo";
                //}
                //else
                //{
                    NmPT = "PT Atomica Isvara Ritelindo";
                //}
                //PdfPCell callNMPT = new PdfPCell(new Phrase(new Chunk("PT Atmos Isvara Ritelindo", regular)));
                PdfPCell callNMPT = new PdfPCell(new Phrase(new Chunk(NmPT, regular)));
                callNMPT.BorderWidth = 0;
                callNMPT.PaddingBottom = 2f;
                table.AddCell(callNMPT);

                PdfPCell callNONPWP = new PdfPCell(new Phrase(new Chunk("No NPWP : 92.941.243.5-022.000", regular)));
                callNONPWP.BorderWidth = 0;
                callNONPWP.PaddingBottom = 5f;
                table.AddCell(callNONPWP);

                //table.AddCell(new Phrase(new Chunk("DEBTOR NAME  : " + Lbl_DebtorName.Text + "", regular)));
                PdfPCell call1 = new PdfPCell(new Phrase(new Chunk("Receipt #: " + noBon + " \n" + "Ticket Date :" + string.Format("{0:dd-MM-yyyy HH:mm:ss}", struckList.FirstOrDefault().CREATED_DATE) +
                    "\n" + "Cashier : " + struckList.FirstOrDefault().CREATED_BY, regular)));
                call1.BorderWidth = 0;
                call1.BorderWidthTop = 0.5f;
                call1.PaddingTop = 7f;
                call1.PaddingBottom = 7f;
                table.AddCell(call1);
                //table.AddCell(new Phrase(new Chunk("VISIT DATE   : " + Lbl_VisitDate.Text + "", regular)));

                //table.AddCell(new Phrase(new Chunk("AC NO.   : ", regular)));
                PdfPCell call5 = new PdfPCell(new Phrase(new Chunk("Article \nPrice            Discount          Net Discount           Net Price", regular)));
                call5.BorderWidth = 0;
                call5.BorderWidthTop = 0.5f;
                call5.BorderWidthBottom = 0.5f;
                call5.PaddingTop = 7f;
                call5.PaddingBottom = 7f;
                table.AddCell(call5);

                decimal total = 0;
                decimal totalDPP = 0;
                decimal totalPPN = 0;
                foreach (VW_REPRINTBON item in struckList)
                {
                    PdfPCell call2 = new PdfPCell(new Phrase(new Chunk(item.ARTICLE + " \n" +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.BON_PRICE)) + "       " + item.DISC_P.ToString("G29") + "         " +
                        string.Format("{0:0,0.00}", Convert.ToDouble(item.DISC_R)) + "          " + string.Format("{0:0,0.00}", Convert.ToDouble(item.NET_PRICE)), regular)));
                    call2.BorderWidth = 0;
                    call2.PaddingTop = 6f;
                    call2.PaddingBottom = 10f;
                    table.AddCell(call2);
                    total = total + item.NET_PRICE;
                    totalDPP = totalDPP + item.DPP;
                    totalPPN = totalPPN + item.PPN;

                }

                PdfPCell callTotal = new PdfPCell(new Phrase(new Chunk("Total Net : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(total)), regular)));
                callTotal.BorderWidth = 0;
                callTotal.BorderWidthTop = 0.5f;
                callTotal.BorderWidthBottom = 0.5f;
                callTotal.PaddingTop = 7f;
                callTotal.PaddingBottom = 7f;
                table.AddCell(callTotal);
                if (struckList.FirstOrDefault().ONGKIR == "Yes")
                {
                    PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Ongkir : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(FinalOngkir)), regular)));
                    callCIF.BorderWidth = 0;
                    table.AddCell(callCIF);
                }
                if (struckList.FirstOrDefault().VOUCHER == "Yes")
                {
                    PdfPCell callCIF = new PdfPCell(new Phrase(new Chunk("Total Voucher : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_VOUCHER)), regular)));
                    callCIF.BorderWidth = 0;
                    table.AddCell(callCIF);
                }

                if (struckList.FirstOrDefault().CARD == "Yes")
                {
                    PdfPCell callCP = new PdfPCell(new Phrase(new Chunk("Total Card : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_CARD)), regular)));
                    callCP.BorderWidth = 0;
                    callCP.PaddingTop = 4f;
                    callCP.PaddingBottom = 4f;
                    table.AddCell(callCP);
                }

                PdfPCell callAcNo = new PdfPCell(new Phrase(new Chunk("Total Cash : Rp " + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().JM_UANG)), regular)));
                callAcNo.BorderWidth = 0;
                callAcNo.PaddingTop = 5f;
                callAcNo.PaddingBottom = 5f;
                table.AddCell(callAcNo);

                PdfPCell callCreatedBy = new PdfPCell(new Phrase(new Chunk("Change : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(struckList.FirstOrDefault().KEMBALI)), regular)));
                callCreatedBy.BorderWidth = 0;
                callCreatedBy.PaddingBottom = 5f;
                table.AddCell(callCreatedBy);

                //Menampilkan DPP Dan PPN
                PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalDPP)) + " PPN : Rp." + string.Format("{0:0,0.00}", Convert.ToDouble(totalPPN)), regular)));
                //PdfPCell callDPPN = new PdfPCell(new Phrase(new Chunk("DPP : " + totalDPP.ToString("c0") + " PPN : " + totalPPN.ToString("c0"), regular)));
                callDPPN.BorderWidth = 0;
                callDPPN.PaddingBottom = 5f;
                table.AddCell(callDPPN);

                if (struckList.FirstOrDefault().BRAND.ToLower() == "melissa")
                {
                    PdfPCell callOLShop = new PdfPCell(new Phrase(new Chunk("shop easy 24/7 at melissa.co.id", regular)));
                    callOLShop.BorderWidth = 0;
                    callOLShop.PaddingBottom = 5f;
                    table.AddCell(callOLShop);
                }

                //create a black line
                PdfPCell black = new PdfPCell(new Phrase("black 2", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 0.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                black.BackgroundColor = new BaseColor(0, 0, 0);
                black.BorderColor = new BaseColor(0, 0, 0);
                //black.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                black.BorderWidth = 0.5f;
                table.AddCell(black);
                ///////////////////////////
                PdfPCell bottom1 = new PdfPCell(new Phrase(new Chunk("Thank you very much. Please come again ! \nExchange Non-discount items within 7 days. \n" +
                    "Term & condition apply.", regularTerm2)));
                bottom1.PaddingTop = 3f;
                bottom1.BorderWidth = 0.5f;
                table.AddCell(bottom1);

                pdfDoc.Add(table);


                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", myPDFWriter);
                myPDFWriter.AddJavaScript(jAction);
                #endregion
                /*Template Done*/
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();

                using (FileStream fs = File.Create(Server.MapPath("..\\BonReprint\\" + noBon + ".pdf")))
                {
                    fs.Write(content, 0, (int)content.Length);
                }

                iframePDF.Attributes["src"] = "..\\BonReprint\\" + noBon + ".pdf";
                //string url = HttpContext.Current.Request.Url.Authority.ToString();
                //bDoneLinkStruck.HRef = "http://" + url + "/BonReprint/" + noBon + ".pdf";
                //bDoneLinkReprint.HRef = "http://" + url + "/BonReprint/" + noBon + ".pdf";

                string urlHost = HttpContext.Current.Request.Url.Host;
                string urlPort = urlHost == "localhost" ? "23963" : "5793";
                string url = "http://" + urlHost + ":" + urlPort + "/BonReprint/" + noBon + ".pdf";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
        }

        #endregion
        #region "ECR_EDC_BCA"
        protected void btnSendToEDC_Click(object sender, EventArgs e)
        {
            string idTemp = hdnIdBYR.Value;
            CARD_PAY_POS_TO_EDC_DA TempEDCDa = new CARD_PAY_POS_TO_EDC_DA();
            if (ddlNCardEdc.SelectedValue == "BCA")
            {
                CARD_PAY_POS_TO_EDC tempEDC = new CARD_PAY_POS_TO_EDC();

                tempEDC.CardPay = Convert.ToDecimal(tbNCardPay.Text);
                tempEDC.ID_SH_BAYAR = Convert.ToInt64(idTemp);
                tempEDC.Bank = ddlCardName.SelectedValue;
                tempEDC.EDC = ddlNCardEdc.SelectedValue;
                tempEDC.KODE_CUST = Session["UKode"].ToString();
                tempEDC.CRT_BY = Session["UName"].ToString();
                tempEDC.KODE_CT = "-";

                TempEDCDa.insertCARD_PAY_POS_TO_EDC(tempEDC);
                DivNCardMessage.Visible = false;
            }
            else
            {
                DivNCardMessage.InnerText = "Penggunaan Tombol 'TO EDC' hanya dapat di gunakan untuk mesin EDC BCA!";
                DivNCardMessage.Attributes["class"] = "warning";
                DivNCardMessage.Visible = true;
            }
            ModalNewCard.Show();
        }

        protected void btnReceiveFromEDC_Click(object sender, EventArgs e)
        {
            string idTemp = hdnIdBYR.Value;

            CARD_PAY_POS_TO_EDC_DA TempEDCDa = new CARD_PAY_POS_TO_EDC_DA();
            CARD_PAY_POS_TO_EDC tempEDC = new CARD_PAY_POS_TO_EDC();
            tempEDC = TempEDCDa.getEDCResult(" Where STAT_TRANS  = 'DONE' AND ID_SH_BAYAR = " + Convert.ToInt64(idTemp));
            if (tempEDC.PAN != null || tempEDC.PAN != "")
            {
                string cardno = tempEDC.PAN;
                tbNCardNoCard.Text = cardno.Replace("*", "0");
                tbNCardVLCard.Text = tempEDC.Invoiceno;
                DivNCardMessage.Visible = false;
            }
            else
            {
                DivNCardMessage.InnerText = "Tidak Ada Respon yang di Terima Dari EDC! Silahkan Cek Transaksi Melalui Mesin EDC!";
                DivNCardMessage.Attributes["class"] = "warning";
                DivNCardMessage.Visible = true;
            }
            ModalNewCard.Show();
        }
        #endregion
        protected void CbOngkir_CheckedChanged(object sender, EventArgs e)
        {
            if (CbOngkir.Checked == true)
            {
                PnlOngkir.Visible = true;
                HdnOngkir.Value = "1";
            }
            else
            {
                PnlOngkir.Visible = false;
                HdnOngkir.Value = "0";
            }
            ModalNewInputPayment.Show();
        }

    }
}