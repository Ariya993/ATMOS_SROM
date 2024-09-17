using ATMOS_SROM.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Model
{
    public class BRAND_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public List<BRAND> getBRAND(string where)
        {
            List<BRAND> listBRAND = new List<BRAND>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select  FBRAND, FKD_BRN, NOMOR, ID, Consignment,SUPER_BRAND FROM BRAND {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        BRAND item = new BRAND();
                        item.FBRAND = reader.GetString(0);
                        item.FKD_BRN = reader.GetString(1);
                        item.NOMOR  = reader.GetString(2);
                        //item.ID = reader.GetInt32(3);
                        item.Consignment = reader.IsDBNull(reader.GetOrdinal("Consignment")) ? "" : reader.GetString(4);
                        item.SUPER_BRAND = reader.IsDBNull(reader.GetOrdinal("SUPER_BRAND")) ? "" : reader.GetString(5);
                      
                        listBRAND.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBRAND;
        }
        public List<BRAND> getBRANDJual(string where)
        {
            List<BRAND> listBRAND = new List<BRAND>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                //using (SqlCommand command = new SqlCommand(string.Format("select  FBRAND, FKD_BRN, NOMOR, ID, Consignment, SUPER_BRAND FROM BRAND {0}", where), Connection))

                using (SqlCommand command = new SqlCommand(string.Format("select SUPER_BRAND FROM BRAND {0} GROUP BY SUPER_BRAND", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        BRAND item = new BRAND();

                        item.SUPER_BRAND = reader.IsDBNull(reader.GetOrdinal("SUPER_BRAND")) ? "" : reader.GetString(0);

                        listBRAND.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBRAND;
        }

        public string updateBrandShowroom(string kodeShowroom, string brandkode)
        {
            string res = "Update Data Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("Update MS_SHOWROOM set BRAND_JUAL = '{0}' where KODE = '{1}' ", brandkode, kodeShowroom);
                Connection.Open();
                SqlCommand command = new SqlCommand(query, Connection);
                command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                res = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return res;
        }

        public String getBRANDNameAsString(string where)
        {
            string brandstr = "";
            //BRAND item = new BRAND();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select  FBRAND FROM BRAND {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        
                       // item.FBRAND = reader.GetString(0);
                        if (brandstr == "")
                        {
                            brandstr = reader.GetString(0);
                        }
                        else
                        {
                            brandstr = brandstr + "," + reader.GetString(0);
                        }

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return brandstr;
        }

        public List<String> getDefaultBrandByTipe(string where)
        {
            List<string> ListDefaultBrand = new List<string>();

            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select DISTINCT BRAND from MS_DEFAULT_DISC_EPC {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        ListDefaultBrand.Add(reader["BRAND"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListDefaultBrand;
        }

        public List<String> getDefaultTypeDiscByBrand(string where)
        {
            List<string> ListDefaultTypeDisc = new List<string>();

            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select DISTINCT TIPE_DISCOUNT from MS_DEFAULT_DISC_EPC {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        ListDefaultTypeDisc.Add(reader["TIPE_DISCOUNT"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListDefaultTypeDisc;
        }

        public int getDefaultDisc(string where)
        {
            int discEPC = 0;

            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select DISCOUNT from MS_DEFAULT_DISC_EPC {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        discEPC = Convert.ToInt32(reader["DISCOUNT"]);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return discEPC;
        }

        public List<DEFAULT_DISC_EPC> getDefaultDiscEPC(string where)
        {
            List<DEFAULT_DISC_EPC> ListDefaultDiscEPC = new List<DEFAULT_DISC_EPC>();

            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select TIPE_DISCOUNT, BRAND, DISCOUNT from MS_DEFAULT_DISC_EPC {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        DEFAULT_DISC_EPC DDE = new DEFAULT_DISC_EPC();
                        DDE.TIPE_DISCOUNT = reader["TIPE_DISCOUNT"].ToString();
                        DDE.BRAND = reader["BRAND"].ToString();
                        DDE.DISCOUNT = Convert.ToInt32(reader["DISCOUNT"]);
                        ListDefaultDiscEPC.Add(DDE);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListDefaultDiscEPC;
        }

        public string SetDiscEPC(string TIPE, string TIPEDISCOUNT, string BRAND, int DISCOUNT, string UPDATEDBY)
        {
            string res = "Update Data Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string sp = "SetDisc_EPC";
                Connection.Open();
                SqlCommand command = new SqlCommand(sp, Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 180;
                command.Parameters.AddWithValue("TIPE", TIPE);
                command.Parameters.AddWithValue("TIPE_DISCOUNT", TIPEDISCOUNT);
                command.Parameters.AddWithValue("BRAND", BRAND);
                command.Parameters.AddWithValue("DISCOUNT", DISCOUNT);
                command.Parameters.AddWithValue("UPDATEDBY", UPDATEDBY);
                command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                res = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return res;
        }

        public string CreateDiscEPC(string TIPE, string TIPEDISCOUNT, string BRAND, int DISCOUNT, string UPDATEDBY)
        {
            string res = "Insert Data Berhasil!";
            try
            {
                string BRANDIN = string.Empty;
                if (BRAND == "DENIM")
                    BRANDIN = "BRAND IN ('707','DENIM','SDS','STANDARD DENIM')";
                else
                    BRANDIN = string.Format("BRAND = '{0}'", BRAND);

                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(String.Format("select ID, KODE, SHOWROOM from MS_SHOWROOM where [STATUS] = 'OPEN' and {0}", BRANDIN), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    SqlConnection Connection2 = new SqlConnection(conString);
                    Connection2.Open();
                    while (reader.Read())
                    {
                        try
                        {
                            string sp = "CreateDisc_EPC";
                            SqlCommand command2 = new SqlCommand(sp, Connection2);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.CommandTimeout = 180;
                            command2.Parameters.AddWithValue("ID_SHR", reader["ID"]);
                            command2.Parameters.AddWithValue("KODE", reader["KODE"]);
                            command2.Parameters.AddWithValue("SHOWROOM", reader["SHOWROOM"]);
                            command2.Parameters.AddWithValue("TIPE", TIPE);
                            command2.Parameters.AddWithValue("TIPE_DISCOUNT", TIPEDISCOUNT);
                            command2.Parameters.AddWithValue("DISCOUNT", DISCOUNT);
                            command2.Parameters.AddWithValue("UPDATEDBY", UPDATEDBY);
                            command2.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            res = "ERROR : " + ex.Message;
                        }
                    }
                    Connection2.Close();

                    reader.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        #region "ADD & Edit Showroom"
        public MS_SHOWROOM getShowRoomAllInfo(string where)
        {
            MS_SHOWROOM item = new MS_SHOWROOM();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE, SHOWROOM, STORE, BRAND, ALAMAT, PHONE, STATUS, STATUS_SHOWROOM, LUAS, JUM_SPG" +
                    ", SEWA,SERVICE, SALARY, INTERNET, LISTRIK, TELEPON, SUSUT, BY_LAIN2, STATUS_AWAL, KODE_PT, BRAND_JUAL, LOGO_IMG" +
                    " from MS_SHOWROOM {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        item.ID = reader.GetInt64(0);
                        item.KODE = reader.GetString(1);
                        item.SHOWROOM = reader.GetString(2);
                        item.STORE = reader.GetString(3);
                        item.BRAND = reader.GetString(4);
                        item.ALAMAT = reader.GetString(5);
                        item.PHONE = reader.GetString(6);
                        item.STATUS = reader.GetString(7);
                        item.STATUS_SHOWROOM = reader.GetString(8);
                        item.LUAS = reader.IsDBNull(reader.GetOrdinal("LUAS")) ? 0 : reader.GetDecimal(9);
                        item.JUM_SPG = reader.IsDBNull(reader.GetOrdinal("JUM_SPG")) ? 0 : reader.GetInt32(10);
                        item.SEWA = reader.IsDBNull(reader.GetOrdinal("SEWA")) ? 0 : reader.GetDecimal(11);
                        item.SERVICE = reader.IsDBNull(reader.GetOrdinal("SERVICE")) ? 0 : reader.GetDecimal(12);
                        item.SALARY = reader.IsDBNull(reader.GetOrdinal("SALARY")) ? 0 : reader.GetDecimal(13);
                        item.INTERNET = reader.IsDBNull(reader.GetOrdinal("INTERNET")) ? 0 : reader.GetDecimal(14);
                        item.LISTRIK = reader.IsDBNull(reader.GetOrdinal("LISTRIK")) ? 0 : reader.GetDecimal(15);
                        item.TELEPON = reader.IsDBNull(reader.GetOrdinal("TELEPON")) ? 0 : reader.GetDecimal(16);
                        item.SUSUT = reader.IsDBNull(reader.GetOrdinal("SUSUT")) ? 0 : reader.GetDecimal(17);
                        item.BY_LAIN2 = reader.IsDBNull(reader.GetOrdinal("BY_LAIN2")) ? 0 : reader.GetDecimal(18);
                        item.STATUS_AWAL = reader.GetString(19);
                        item.KODE_PT = reader.GetString(20);
                        item.BRAND_JUAL = reader.IsDBNull(reader.GetOrdinal("BY_LAIN2")) ? "-" : reader.GetString(21);
                        item.LOGO_IMG = reader.IsDBNull(reader.GetOrdinal("LOGO_IMG")) ? "" : reader.GetString(22);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public string updateShowroom(MS_SHOWROOM shr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_SHOWROOM SET BRAND=@brand, ALAMAT=@alamat, PHONE=@phone, STATUS=@Statshr, LUAS=@luas, " +
                    "JUM_SPG=@JumSpg, SEWA=@sewa, SERVICE=@service, SALARY=@salary, INTERNET=@internet, LISTRIK=@listrik, " +
                    "TELEPON=@biayaTelp, SUSUT=@susut, BY_LAIN2=@bylain, KODE_PT=@kdPT, BRAND_JUAL=@brandjual, LOGO_IMG = @LOGO_IMG " +
                    "WHERE ID = @idshr");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = shr.BRAND;
                    command.Parameters.Add("@alamat", SqlDbType.VarChar).Value = shr.ALAMAT;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = shr.PHONE;
                    command.Parameters.Add("@Statshr", SqlDbType.VarChar).Value = shr.STATUS;//shr.STATUS_SHOWROOM;
                    command.Parameters.Add("@luas", SqlDbType.Decimal).Value = shr.LUAS;
                    command.Parameters.Add("@JumSpg", SqlDbType.Int).Value = shr.JUM_SPG;
                    command.Parameters.Add("@sewa", SqlDbType.Decimal).Value = shr.SEWA;
                    command.Parameters.Add("@service", SqlDbType.Decimal).Value = shr.SERVICE;
                    command.Parameters.Add("@salary", SqlDbType.Decimal).Value = shr.SALARY;
                    command.Parameters.Add("@internet", SqlDbType.Decimal).Value = shr.INTERNET;
                    command.Parameters.Add("@listrik", SqlDbType.Decimal).Value = shr.LISTRIK;
                    command.Parameters.Add("@biayaTelp", SqlDbType.Decimal).Value = shr.TELEPON;
                    command.Parameters.Add("@susut", SqlDbType.Decimal).Value = shr.SUSUT;
                    command.Parameters.Add("@bylain", SqlDbType.Decimal).Value = shr.BY_LAIN2;
                    command.Parameters.Add("@kdPT", SqlDbType.VarChar).Value = shr.KODE_PT;
                    command.Parameters.Add("@brandjual", SqlDbType.VarChar).Value = shr.BRAND_JUAL;//"-";
                    command.Parameters.Add("@idshr", SqlDbType.BigInt).Value = shr.ID;
                    command.Parameters.Add("@LOGO_IMG", SqlDbType.VarChar).Value = shr.LOGO_IMG;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }
        public string AddShowroom(MS_SHOWROOM shr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("INSERT INTO MS_SHOWROOM(KODE,SHOWROOM,STORE,BRAND,ALAMAT,PHONE,STATUS,STATUS_SHOWROOM,LUAS,JUM_SPG,SEWA,SERVICE,SALARY,INTERNET,LISTRIK,TELEPON,SUSUT,BY_LAIN2,STATUS_AWAL,KODE_PT,BRAND_JUAL, VENDOR, LOGO_IMG) " +
                    " VALUES (@kode,@showroom,@store,@brand,@alamat,@phone,@Stat,@Statshr,@luas,@JumSpg,@sewa,@service,@salary,@internet,@listrik,@biayaTelp,@susut,@bylain,@Statawal,@kdPT,@brandjual,@vendor, @LOGO_IMG); Select ID FROM MS_SHOWROOM WHERE KODE = @kode");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = shr.KODE;
                    command.Parameters.Add("@showroom", SqlDbType.VarChar).Value = shr.SHOWROOM;
                    command.Parameters.Add("@store", SqlDbType.VarChar).Value = shr.STORE;
                    command.Parameters.Add("@brand", SqlDbType.VarChar).Value = shr.BRAND;
                    command.Parameters.Add("@alamat", SqlDbType.VarChar).Value = shr.ALAMAT;
                    command.Parameters.Add("@phone", SqlDbType.VarChar).Value = shr.PHONE;
                    command.Parameters.Add("@Stat", SqlDbType.VarChar).Value = shr.STATUS;
                    command.Parameters.Add("@Statshr", SqlDbType.VarChar).Value = shr.STATUS_SHOWROOM;
                    command.Parameters.Add("@luas", SqlDbType.Decimal).Value = shr.LUAS;
                    command.Parameters.Add("@JumSpg", SqlDbType.Int).Value = shr.JUM_SPG;
                    command.Parameters.Add("@sewa", SqlDbType.Decimal).Value = shr.SEWA;
                    command.Parameters.Add("@service", SqlDbType.Decimal).Value = shr.SERVICE;
                    command.Parameters.Add("@salary", SqlDbType.Decimal).Value = shr.SALARY;
                    command.Parameters.Add("@internet", SqlDbType.Decimal).Value = shr.INTERNET;
                    command.Parameters.Add("@listrik", SqlDbType.Decimal).Value = shr.LISTRIK;
                    command.Parameters.Add("@biayaTelp", SqlDbType.Decimal).Value = shr.TELEPON;
                    command.Parameters.Add("@susut", SqlDbType.Decimal).Value = shr.SUSUT;
                    command.Parameters.Add("@bylain", SqlDbType.Decimal).Value = shr.BY_LAIN2;
                    command.Parameters.Add("@Statawal", SqlDbType.VarChar).Value = shr.STATUS_AWAL;
                    command.Parameters.Add("@kdPT", SqlDbType.VarChar).Value = shr.KODE_PT;
                    command.Parameters.Add("@brandjual", SqlDbType.VarChar).Value = shr.BRAND_JUAL;//"-";
                    command.Parameters.Add("@vendor", SqlDbType.VarChar).Value = "";
                    command.Parameters.Add("@LOGO_IMG", SqlDbType.VarChar).Value = shr.LOGO_IMG;
                    newId = command.ExecuteScalar().ToString() ;
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }
        #endregion

        #region "Add & Edit Brand"
        public string updateBrand(BRAND brn)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE BRAND SET Consignment=@Consignment, SUPER_BRAND = @SUPER_BRAND " +
                    "WHERE FKD_BRN = @FKD_BRN");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@Consignment", SqlDbType.VarChar).Value = brn.Consignment;
                    command.Parameters.Add("@SUPER_BRAND", SqlDbType.VarChar).Value = brn.SUPER_BRAND;
                    command.Parameters.Add("@FKD_BRN", SqlDbType.VarChar).Value = brn.FKD_BRN;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }
        public String upBrand(String flPath, String source, string fileType, string usr)
        {
            string res = "Upload Berhasil";
            String test;
            int jml = 0;
            string CekKodeRes = "0";

            BRAND brnd = new BRAND();
            OleDbConnection cnn = new OleDbConnection();
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=NO;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
            cnn = new OleDbConnection(connetionString);

            DataSet dsOle = new DataSet();
            cnn.Open();

            DataTable dbSchema = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dbSchema == null || dbSchema.Rows.Count < 1)
            {
                throw new Exception("Error: Could not determine the name of the first worksheet.");
            }

            int rowTable = fileType == ".xls" ? 0 : dbSchema.Rows.Count - 1;
            string firstSheetName = dbSchema.Rows[dbSchema.Rows.Count - 1]["TABLE_NAME"].ToString();//dbSchema.Rows[rowTable]["TABLE_NAME"].ToString();
            cnn.Close();
            OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + firstSheetName + "]", cnn);
            //OleDbDataAdapter oAdapter = new OleDbDataAdapter("SELECT * FROM [" + "Sheet1" + "]", cnn);
            oAdapter.Fill(dsOle, "Sheet1");
            CekKodeRes = CekInsertBrand(dsOle.Tables[0]);
            if (CekKodeRes == "0")
            {
                for (int i = 1; i < dsOle.Tables[0].Rows.Count; i++)
                {

                    brnd.FBRAND = dsOle.Tables[0].Rows[i][0].ToString();
                    brnd.FKD_BRN = dsOle.Tables[0].Rows[i][1].ToString();
                    //brnd.NOMOR = dsOle.Tables[0].Rows[i][2].ToString();
                    brnd.Consignment = dsOle.Tables[0].Rows[i][2].ToString();
                    brnd.SUPER_BRAND = dsOle.Tables[0].Rows[i][3].ToString();

                    test = insertBrand(brnd, usr);
                    if (test == "Berhasil!")
                    {
                        jml = jml + 1;
                    }
                    //data++;
                    //sku.insertDetailPO(detailPO);

                }
                res = "JUMLAH DATA BERHASIL DI UPLOAD : " + jml;
            }
            else if (CekKodeRes == "1")
            {
                res = "KODE BRAND HARUS ANGKA!";
            }
            else if (CekKodeRes.Contains("2"))
            {
                res = "KODE BRAND: "+ CekKodeRes.Substring(CekKodeRes.LastIndexOf('_') + 1) + " SUDAH ADA!";
            }
            return res;
        }
        public String insertBrand(BRAND item, string usr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsBrand", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@brand", item.FBRAND));
                    command.Parameters.Add(new SqlParameter("@kdbrand", item.FKD_BRN));
                    //command.Parameters.Add(new SqlParameter("@nomor", item.NOMOR));
                    command.Parameters.Add(new SqlParameter("@consignment", item.Consignment));
                    command.Parameters.Add(new SqlParameter("@sprbrand", item.SUPER_BRAND));

                    command.Parameters.Add(new SqlParameter("@User", usr));
                    command.CommandTimeout = 3600;
                    Connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newId;

        }
        public String CekInsertBrand(DataTable dTab)
        {
            String CekKodeRes = "0";
            List<BRAND> listBrand = new List<BRAND>();
            listBrand = getBRAND("");

            bool check = true;
            for (int i = 1; i < dTab.Rows.Count; i++)
            {
                if (dTab.Rows[i][0].ToString().Length > 5)
                {
                    string KDBRand = dTab.Rows[i][1].ToString();
                    try
                    {
                        int KD = Convert.ToInt32(KDBRand);

                    }
                    catch (Exception ex)
                    {
                        CekKodeRes = "1";
                    }
                    if (CekKodeRes != "1" )
                    {
                        if (listBrand.Where(item => item.FKD_BRN == dTab.Rows[i][1].ToString()).Count() > 0)
                        {
                            CekKodeRes = "2_" + dTab.Rows[i][1].ToString();// ;
                        }
                    }
                }
            }
            return CekKodeRes;

        }
        #endregion

        #region "Add Edit Disc Showroom"
        public string InsMsDiscountShr(MS_DISCOUNT_SHR item)
        {
            string newid ="";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("INSERT INTO MS_DISCOUNT_SHR(ID_SHR, KODE,SHOWROOM,DISCOUNT,CREATED_BY,CREATED_DATE,STATUS,TIPE,TIPE_DISCOUNT) " +
                    " VALUES (@idshr, @kode, @showroom, @discount,@usr,GETDATE(),1,@tipe,@tipedisc)");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idshr", SqlDbType.BigInt).Value = item.ID_SHR;
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = item.KODE;
                    command.Parameters.Add("@showroom", SqlDbType.VarChar).Value = item.SHOWROOM;
                    command.Parameters.Add("@discount", SqlDbType.Int).Value = item.DISCOUNT;
                    command.Parameters.Add("@usr", SqlDbType.VarChar).Value = item.CREATED_BY;
                    command.Parameters.Add("@tipe", SqlDbType.VarChar).Value = item.TIPE;
                    command.Parameters.Add("@tipedisc", SqlDbType.VarChar).Value = item.TIPE_DISCOUNT;
                    

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newid = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newid;
        }
        public string UpdMsDiscountShr(MS_DISCOUNT_SHR item)
        {
            string newid = "";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("UPDATE MS_DISCOUNT_SHR SET STATUS = @status, UPDATED_DATE = GETDATE(), UPDATED_BY = @usr" +
                    " WHERE KODE = @kode AND TIPE = @tipe ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = item.STATUS;
                    
                    command.Parameters.Add("@usr", SqlDbType.VarChar).Value = item.UPDATED_BY;
                    command.Parameters.Add("@tipe", SqlDbType.VarChar).Value = item.TIPE;
                   
                    command.Parameters.Add("@kode", SqlDbType.VarChar).Value = item.KODE;

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newid = "ERROR : " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return newid;
        }
        public List<MS_DISCOUNT_SHR> getListMsDiscountShr(string where)
        {
            List<MS_DISCOUNT_SHR> listItem = new List<MS_DISCOUNT_SHR>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select ID, ID_SHR, KODE, SHOWROOM, DISCOUNT, STATUS, TIPE, TIPE_DISCOUNT FROM MS_DISCOUNT_SHR {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_DISCOUNT_SHR item = new MS_DISCOUNT_SHR();
                        item.ID = reader.GetInt64(0);
                        item.ID_SHR = reader.GetInt64(1);
                        item.KODE = reader.GetString(2);
                        item.SHOWROOM = reader.GetString(3);
                        item.DISCOUNT = reader.GetInt32(4);
                        item.STATUS = reader.GetBoolean(5);
                        item.TIPE = reader.GetString(6);
                        item.TIPE_DISCOUNT = reader.GetString(7);
                        listItem.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listItem;
        }
        #endregion
    }
}