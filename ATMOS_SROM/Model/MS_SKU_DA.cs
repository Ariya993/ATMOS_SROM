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
    public class MS_SKU_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_SKU> getListku(string where)
        {
            List<MS_SKU> Listku = new List<MS_SKU>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("Select MS_SKU_ID, KD_SKU, KD_BRAND, GRUP, DISC_P, MARGIN, BBN_DEPT,KETERANGAN,FADD,FUPDT,USR_ADD,USR_UPD,FILE_UP from MS_SKU {0}", where), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_SKU item = new MS_SKU();
                        item.MS_SKU_ID = reader.GetDecimal(0);
                        item.KD_SKU = reader.GetString(1);
                        item.KD_BRAND = reader.GetString(2);
                        item.GRUP = reader.GetString(3);
                        item.DISC_P = reader.GetDecimal(4);
                        item.MARGIN = reader.GetDecimal(5);
                        item.BBN_DEPT = reader.IsDBNull(reader.GetOrdinal("BBN_DEPT")) ? 0 : reader.GetDecimal(6);

                        item.KETERANGAN = reader.GetString(7);
                        item.FADD = reader.GetDateTime(8);
                        item.FUPDT = reader.GetDateTime(9);
                        item.USR_ADD = reader.GetString(10);
                        item.USR_UPD = reader.GetString(11);
                        item.FILE_UP = reader.GetString(12);
                        Listku.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Listku;
        }


        public String upMSSKU(String flPath, String source, string fileType, string usr)
        {
            string res = "Upload Berhasil";
            String test;
            int jml = 0;
            MS_SKU sku = new MS_SKU();
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

             for (int i = 1; i < dsOle.Tables[0].Rows.Count; i++)
                    {
                        
                            sku.KD_SKU = dsOle.Tables[0].Rows[i][0].ToString();
                            sku.GRUP = dsOle.Tables[0].Rows[i][1].ToString();
                            sku.KD_BRAND = dsOle.Tables[0].Rows[i][2].ToString();
                            sku.KETERANGAN = dsOle.Tables[0].Rows[i][3].ToString();
                            sku.DISC_P = Convert.ToInt32(dsOle.Tables[0].Rows[i][4].ToString());
                            sku.MARGIN = Convert.ToInt32(dsOle.Tables[0].Rows[i][5].ToString());
                            sku.BBN_DEPT = Convert.ToInt32(dsOle.Tables[0].Rows[i][6].ToString());
                            //sku.FADD = DateTime.Now;
                            sku.FILE_UP = source;
                            test = insertMSSku(sku, usr);
                            if (test == "Berhasil!")
                            {
                                jml = jml + 1;
                            }
                            //data++;
                            //sku.insertDetailPO(detailPO);
                      
                    }
             res = "JUMLAH DATA BERHASIL DI UPLOAD : " + jml;
            return res;
        }

        public String insertMSSku(MS_SKU sku, string usr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                //string query = String.Format("Insert MS_SKU (KD_SKU, KD_BRAND, GRUP, DISC_P, MARGIN, BBN_DEPT, KETERANGAN, FADD, FILE_UP) values " +
                //    " (@kdsku, @kdbrand, @grup, @discP, @margin, @bbnDept, @ket, GETDATE(), @fileup) ");
                //Connection.Open();
                //using (SqlCommand command = new SqlCommand(query, Connection))
                using (SqlCommand command = new SqlCommand("usp_InsMsSKU", Connection))
                {
                    //command.Parameters.Add("@kdsku", SqlDbType.VarChar).Value = sku.KD_SKU;
                    //command.Parameters.Add("@kdbrand", SqlDbType.VarChar).Value = sku.KD_BRAND;
                    //command.Parameters.Add("@grup", SqlDbType.VarChar).Value = sku.GRUP;
                    //command.Parameters.Add("@discP", SqlDbType.Decimal).Value = sku.DISC_P;
                    //command.Parameters.Add("@margin", SqlDbType.Decimal).Value = sku.MARGIN;
                    //command.Parameters.Add("@bbnDept", SqlDbType.Decimal).Value = sku.BBN_DEPT;
                    //command.Parameters.Add("@ket", SqlDbType.VarChar).Value = sku.KETERANGAN;
                    //command.Parameters.Add("@fileup", SqlDbType.VarChar).Value = sku.FILE_UP;
                    //command.Parameters.Add("@User", SqlDbType.VarChar).Value = usr;
                    
                    //command.ExecuteScalar().ToString();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@kdsku", sku.KD_SKU));
                    command.Parameters.Add(new SqlParameter("@kdbrand", sku.KD_BRAND));
                    command.Parameters.Add(new SqlParameter("@grup", sku.GRUP));
                    command.Parameters.Add(new SqlParameter("@discP", sku.DISC_P));
                    command.Parameters.Add(new SqlParameter("@margin", sku.MARGIN));
                    command.Parameters.Add(new SqlParameter("@bbnDept", sku.BBN_DEPT));
                    command.Parameters.Add(new SqlParameter("@ket", sku.KETERANGAN));
                    command.Parameters.Add(new SqlParameter("@fileup", sku.FILE_UP));
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
    }
}