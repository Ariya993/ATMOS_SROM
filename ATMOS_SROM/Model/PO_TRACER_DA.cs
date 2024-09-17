using ATMOS_SROM.Domain.CustomObj;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Model
{
    public class PO_TRACER_DA
    {
        private static string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public List<PO_TRACER_H> GetListPoTracerHeader(string whereCon)
        {
            List<PO_TRACER_H> Listitem = new List<PO_TRACER_H>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select NO_PO, PO_REFF, DATE, BRAND, CONTACT, " +
                        "PHONE, EMAIL,  ADDRESS, SUPPLIER, NO_GR, ID from vw_POTracerHeader {0} ", whereCon), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        PO_TRACER_H item = new PO_TRACER_H();
                        item.NO_PO = reader.GetString(0);
                        item.PO_REFF = reader.GetString(1);
                        item.PO_DATE = reader.GetDateTime(2);
                        item.BRAND = reader.GetString(3);
                        item.CONTACT = reader.GetString(4);
                        item.PHONE = reader.GetString(5);
                        item.EMAIL = reader.GetString(6);
                        item.Addr = reader.GetString(7);
                        item.SUPPLIER = reader.GetString(8);
                        item.POSITION = reader.GetString(9);
                        item.ID = reader.GetInt64(10);
                        //item.TotalPOQty = reader.GetInt32(11);
                        //item.Kode = reader.IsDBNull(reader.GetOrdinal("KODE")) ? "-" : reader.GetString(12);//reader.GetString(12);
                        //item.Status_GR = reader.IsDBNull(reader.GetOrdinal("STATUS_GR")) ? "-" : reader.GetString(13);//reader.GetString(13);
                        //item.ID = reader.GetInt64(14);
                        Listitem.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Listitem;
        }

        public List<PO_TRACER_D> GetPoTracerDetail(string whereCon)
        {
            List<PO_TRACER_D> listPO = new List<PO_TRACER_D>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand(string.Format("select ID_PO, ID_PO_DTL, QTY, ID_KDBRG, BARCODE, ITEM_CODE, FSize_DESC, " +
                    "FART_DESC, FCOL_DESC, FPRODUK, NO_GR, QTY_TIBA, Selisih, NO_PO from vw_POTraceDetail {0}" +
                    " ORDER BY Selisih DESC", whereCon), Connection))
                {
                    command.CommandType = CommandType.Text;
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        PO_TRACER_D item = new PO_TRACER_D();
                        item.ID_PO = reader.GetInt64(0);
                        item.ID_PO_DTL = reader.GetInt64(1);// reader.GetInt32(1);
                        item.QTY = reader.GetInt32(2);
                        item.ID_KDBRG = reader.GetInt64(3);
                        item.BARCODE = reader.GetString(4);
                        item.ITEM_CODE = reader.GetString(5);
                        item.FSize_DESC = reader.GetString(6);
                        item.FART_DESC = reader.GetString(7);
                        item.FCOL_DESC = reader.GetString(8);//reader.GetString(8);
                        item.FPRODUK = reader.GetString(9);
                        item.NO_GR = reader.GetString(10);
                        item.QTY_TIBA = reader.GetInt32(11);
                        item.Selisih = reader.GetInt32(12);
                        item.NO_PO = reader.GetString(11);
                        listPO.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listPO;
        }
        public virtual DataSet GetDataPoTracerDetail(string whereCon)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("select ID_PO, ID_PO_DTL, QTY, ID_KDBRG, BARCODE, ITEM_CODE, FSize_DESC, " +
                     "FART_DESC, FCOL_DESC, FPRODUK, NO_GR, QTY_TIBA, Selisih, NO_PO, TGL_GR from vw_POTraceDetail {0}" +
                     " ORDER BY Selisih DESC", whereCon), Connection))
            {
                command.CommandType = CommandType.Text;
                //command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }
        public virtual DataSet GetDatavw_POTraceALL(string whereCon)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            //using (SqlCommand command = new SqlCommand(string.Format("select ID_PO, ID_PO_DTL, QTY, ID_KDBRG, BARCODE, ITEM_CODE, FSize_DESC, FART_DESC, FCOL_DESC, FPRODUK, NO_GR, QTY_TIBA, Selisih, STATUS_PO, NO_PO, PO_REFF, DATE, " +
            //         "BRAND, CONTACT, PHONE, EMAIL, ADDRESS, SUPPLIER, TGL_GR from vw_POTraceALL {0}" +
            //         " order by NO_PO", whereCon), Connection))
            //using (SqlCommand command = new SqlCommand(string.Format("select ID_PO, ID_PO_DTL, QTY, ID_KDBRG, BARCODE, ITEM_CODE, FSize_DESC, FART_DESC, FCOL_DESC, FPRODUK, NO_GR, QTY_TIBA, Selisih, STATUS_PO, NO_PO, PO_REFF, DATE, " +
            //         "BRAND, CONTACT, PHONE, EMAIL, ADDRESS, SUPPLIER, TGL_GR from vw_POTraceALL {0}" +
            //         " order by NO_PO", whereCon), Connection))
            using (SqlCommand command = new SqlCommand(string.Format("select ID_PO, NO_GR, QTY_TIBA, QTY, Selisih, STATUS_PO, NO_PO, PO_REFF, DATE, BRAND, CONTACT," +
                "PHONE, EMAIL, ADDRESS, SUPPLIER from vw_POTraceALL {0}" +
                    " order by NO_PO", whereCon), Connection))
            {
                command.CommandType = CommandType.Text;
                //command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }
        public virtual DataSet GetDataVw_PoTracerQtyDiff(string whereCon)
        {
            SqlConnection Connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_PO, NO_PO, ID_KDBRG, BARCODE, ITEM_CODE, FSIZE_DESC, FART_DESC, FCOL_DESC, FPRODUK, QTY, QTY_TIBA " +
                     "from Vw_PoTracerQtyDiff {0} order by NO_PO", whereCon), Connection))
            {
                command.CommandType = CommandType.Text;
                //command.Parameters.Add("@NoGR", NoGR);
                Connection.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "DataSet1");
                Connection.Close();
            }
            return dataSet;
        }
    }
}