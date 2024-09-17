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
    public class MS_EMPLOYEE_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static string conStringDcard = ConfigurationManager.ConnectionStrings["ConnectionStringDCard"].ConnectionString;

        public int cekvipmemberup(String flPath, String source, string fileType, string usr)
        {
            string res = "Succes";
           
            int jml = 0;
            MS_EMPLOYEE sku = new MS_EMPLOYEE();
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
                if (dsOle.Tables[0].Rows[i][0].ToString() == null || dsOle.Tables[0].Rows[i][1].ToString() == null || dsOle.Tables[0].Rows[i][2].ToString() == null
                    || dsOle.Tables[0].Rows[i][0].ToString() == "" || dsOle.Tables[0].Rows[i][1].ToString() == "" || dsOle.Tables[0].Rows[i][2].ToString() == "")
                {
                    jml = jml + 1;
                }

            }

            return jml;
        }

        public String upMSEmployee(String flPath, String source, string fileType, string usr)
        {
            string res = "Upload Berhasil";
            String test;
            int jml = 0;
            MS_EMPLOYEE emp = new MS_EMPLOYEE();
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

                emp.NIK = dsOle.Tables[0].Rows[i][0].ToString();
                emp.NAMA = dsOle.Tables[0].Rows[i][1].ToString();
                emp.LIMIT = Convert.ToInt32(dsOle.Tables[0].Rows[i][2].ToString());
                emp.SISA_LIMIT = Convert.ToInt32(dsOle.Tables[0].Rows[i][2].ToString());

                test = insertMSEmployee(emp, usr);
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

        public String insertMSEmployee(MS_EMPLOYEE emp, string usr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsMsEmploye", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nik", emp.NIK));
                    command.Parameters.Add(new SqlParameter("@nama", emp.NAMA));
                    command.Parameters.Add(new SqlParameter("@limit", emp.LIMIT));
                    command.Parameters.Add(new SqlParameter("@sisalimit", emp.SISA_LIMIT));
                    command.Parameters.Add(new SqlParameter("@user", usr));
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
        public String updSisaLimtEmpl(MS_EMPLOYEE emp)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("UPDATE MS_EMPLOYEE SET SISA_LIMIT = SISA_LIMIT - @sisalimit, UPDATED_BY = @nama, UPDATED_DATE = GETDATE() WHERE NIK = @nik", Connection))
                {
                    //command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nik", emp.NIK));
                    command.Parameters.Add(new SqlParameter("@nama", emp.UPDATED_BY));
                    command.Parameters.Add(new SqlParameter("@sisalimit", emp.SISA_LIMIT));

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
        #region "Save Ke DCARD"
        public string insertMsEPC(MS_EMPLOYEE msEmployee)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conStringDcard);
            try
            {
                string query = String.Format("Insert MS_EPC ( NO_EPC, NAMA, EMP, LIMIT, Company, Discount, STATUS_EPC, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, STATUS,FKET, JOIN_DT  ) values " +
                    " (@NIK, @NAMA, '-', @LIMIT, @STATEPC, 40, 'ACTIVE', @CREATED_BY, getdate(), null, null, 1,'', @JOINDT); SELECT CAST(scope_identity() AS int)  ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@NIK", SqlDbType.VarChar).Value = msEmployee.NIK;
                    command.Parameters.Add("@NAMA", SqlDbType.VarChar).Value = msEmployee.NAMA;
                    command.Parameters.Add("@LIMIT", SqlDbType.Decimal).Value = msEmployee.LIMIT;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = msEmployee.CREATED_BY;
                    command.Parameters.Add("@JOINDT", SqlDbType.Date).Value = msEmployee.JOIN_DATE;
                    command.Parameters.Add("@STATEPC", SqlDbType.Date).Value = msEmployee.STATUS_EMPLOYEE;

                    newId = command.ExecuteScalar().ToString();
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
        public string insertMsLIMIT(MS_EMPLOYEE msEmployee, String idepc)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conStringDcard);
            try
            {
                string query = String.Format("Insert MS_LIMIT ( ID_EPC, NO_EPC, LIMIT) values " +
                    " (@idEpc, @noEpc, @limit) ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@idEpc", SqlDbType.BigInt).Value = Convert.ToInt64(idepc);
                    command.Parameters.Add("@noEpc", SqlDbType.VarChar).Value = msEmployee.NIK;
                    command.Parameters.Add("@limit", SqlDbType.Decimal).Value = msEmployee.LIMIT;
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
        public string updateEPC(MS_EMPLOYEE msEmployee)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conStringDcard);
            try
            {
                string query = String.Format("UPDATE MS_EPC SET NAMA = @nama, UPDATED_BY = @updatedBy, UPDATED_DATE = getdate(), "
                    + " STATUS_EPC = @status where NO_EPC = @noepc");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@nama", SqlDbType.VarChar).Value = msEmployee.NAMA;
                    command.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = msEmployee.UPDATED_BY;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = msEmployee.STATUS_EMPLOYEE;
                    command.Parameters.Add("@noepc", SqlDbType.VarChar).Value = msEmployee.NIK;
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
        public string DeleteEPC(MS_EMPLOYEE msEmployee)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conStringDcard);
            try
            {
                string query = String.Format("UPDATE MS_EPC SET STATUS = 0, UPDATED_BY = @updatedBy, UPDATED_DATE = getdate(), "
                    + " STATUS_EPC = @status where NO_EPC = @noepc");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@updatedBy", SqlDbType.VarChar).Value = msEmployee.UPDATED_BY;
                    command.Parameters.Add("@status", SqlDbType.VarChar).Value = msEmployee.STATUS_EMPLOYEE;
                    command.Parameters.Add("@noepc", SqlDbType.VarChar).Value = msEmployee.NIK;
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

        public string InsSldAwalEPC(DateTime dt, string noepc)
        {
            string newId = "";
            SqlConnection Connection = new SqlConnection(conStringDcard);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsSaldoAwal", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ftgl", dt));
                    command.Parameters.Add(new SqlParameter("@noEpc", noepc));
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

        #endregion

        #region "Upload EPC"
        public int cekEPCMemberUp(String flPath, String source, string fileType, string usr)
        {
            string resup = "Succes";

            int jml = 0;
            MS_EMPLOYEE sku = new MS_EMPLOYEE();
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
                if (dsOle.Tables[0].Rows[i][0].ToString() == null || dsOle.Tables[0].Rows[i][1].ToString() == null || dsOle.Tables[0].Rows[i][2].ToString() == null || dsOle.Tables[0].Rows[i][3].ToString() == null || dsOle.Tables[0].Rows[i][4].ToString() == null || dsOle.Tables[0].Rows[i][5].ToString() == null || dsOle.Tables[0].Rows[i][6].ToString() == null
                    || dsOle.Tables[0].Rows[i][0].ToString() == "" || dsOle.Tables[0].Rows[i][1].ToString() == "" || dsOle.Tables[0].Rows[i][2].ToString() == "" || dsOle.Tables[0].Rows[i][3].ToString() == "" || dsOle.Tables[0].Rows[i][4].ToString() == "" || dsOle.Tables[0].Rows[i][5].ToString() == "" || dsOle.Tables[0].Rows[i][6].ToString() == "")
                {
                    jml = jml + 1;
                }
            }

            return jml;
        }
        public String upMSEPC(String flPath, String source, string fileType, string usr)
        {
            string res = "Upload Berhasil";
            String test;
            string msepc;
            string mslimit;
            int jml = 0;
            MS_EMPLOYEE emp = new MS_EMPLOYEE();
            OleDbConnection cnn = new OleDbConnection();
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + @";Extended Properties='Excel 8.0;HDR=YES;FMT=Delimited;IMEX=1;'";//C:\appli\new\srom
            //string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + source + "; Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";
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
            test = InsertMSEMPLOYEEwithSP(dsOle.Tables[0], usr);

            //for (int i = 1; i < dsOle.Tables[0].Rows.Count; i++)
            //{

            //    emp.NIK = dsOle.Tables[0].Rows[i][0].ToString();
            //    emp.NAMA = dsOle.Tables[0].Rows[i][1].ToString();
            //    emp.LIMIT = Convert.ToInt32(dsOle.Tables[0].Rows[i][2].ToString());
            //    emp.SISA_LIMIT = Convert.ToInt32(dsOle.Tables[0].Rows[i][2].ToString());
            //    emp.JABATAN = dsOle.Tables[0].Rows[i][3].ToString();
            //    emp.TIPE = dsOle.Tables[0].Rows[i][4].ToString();
            //    emp.LIMIT_DELAMI = Convert.ToInt32(dsOle.Tables[0].Rows[i][5].ToString());
            //    emp.JOIN_DATE = Convert.ToDateTime(dsOle.Tables[0].Rows[i][6].ToString());
            //    emp.CREATED_BY = usr;

            //    //test = insertMSEPCUP(emp, usr);
            //    test = InsertMSEMPLOYEEwithSP
            //    //msepc = insertMsEPC(emp);
            //    //mslimit = insertMsLIMIT(emp,msepc);
            //    if (test == "Berhasil!")// && !msepc.Contains("ERROR") && !mslimit.Contains("ERROR"))
            //    {
            //        jml = jml + 1;
            //    }
            //    //data++;
            //    //sku.insertDetailPO(detailPO);

            //}
            //res = "JUMLAH DATA BERHASIL DI UPLOAD : " + jml;
            return res;
        }
        public string InsertMSEMPLOYEEwithSP(DataTable dt, string usr)
        {
            string newId = "Berhasil";
            SqlConnection Connection = new SqlConnection(conString);

            try
            {
                Connection.Open();
                SqlCommand cmdProc = new SqlCommand("SP_INSERT_MS_EMP", Connection);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@dt", dt);
                cmdProc.Parameters.AddWithValue("@usr", usr);
                cmdProc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
            return newId;
        }
        public String insertMSEPCUP(MS_EMPLOYEE emp, string usr)
        {
            string newId = "Berhasil!";
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                using (SqlCommand command = new SqlCommand("usp_InsMsEmployeUP", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nik", emp.NIK));
                    command.Parameters.Add(new SqlParameter("@nama", emp.NAMA));
                    command.Parameters.Add(new SqlParameter("@limit", emp.LIMIT));
                    command.Parameters.Add(new SqlParameter("@sisalimit", emp.SISA_LIMIT));
                    command.Parameters.Add(new SqlParameter("@user", usr));
                    command.Parameters.Add(new SqlParameter("@jabatan", emp.JABATAN));
                    command.Parameters.Add(new SqlParameter("@tipe", emp.TIPE));
                    command.Parameters.Add(new SqlParameter("@limitdelami", emp.LIMIT_DELAMI));
                    command.Parameters.Add(new SqlParameter("@joindt", emp.JOIN_DATE));
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

        #endregion
    }
}