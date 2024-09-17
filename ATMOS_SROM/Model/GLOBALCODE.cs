using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ATMOS_SROM.Domain;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;

namespace ATMOS_SROM.Model
{
    public class GLOBALCODE
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private string sJam = ("000" + DateTime.Now.Hour.ToString()).Substring(("000" + DateTime.Now.Hour.ToString()).Length - 2);
        private string sMenit = ("000" + DateTime.Now.Minute.ToString()).Substring(("000" + DateTime.Now.Minute.ToString()).Length - 2);

        protected string GetHost()
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ip;
        }

        public virtual string countData(string table, string where)
        {
            string count = string.Empty;

            try
            {
                SqlConnection CnLocal = new SqlConnection(conn);
                SqlCommand command = new SqlCommand("Select count(*) from dbo." + table + " " + where, CnLocal);
                CnLocal.Open();
                count = command.ExecuteScalar().ToString();
                CnLocal.Close();
            }
            catch (Exception ex)
            {
                count = ex.Message;
            }

            return count;
        }

        public void addLog(string description, string username)
        {
            MS_LOG log = new MS_LOG();
            log.description = description;
            log.userName = username;
            log.ipAddress = GetHost();
            log.logDate = DateTime.Now;
            new MS_LOG_DA().addMsLog(log);
        }

        public void download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public virtual string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public virtual string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}