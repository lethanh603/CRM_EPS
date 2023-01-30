using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
namespace Data
{
    public class ConnectAccess
    {
        public static OleDbConnection conn;
        //public static string chuoiKetNoi1 = "Provider=Microsoft.JET.OLEDB.4.0; Data Source =" + APCoreData.read("sysconfig").Rows[0]["pathAccess"].ToString() + "\\att2000.mdb;";
        public static string chuoiKetNoi1 = "Provider=Microsoft.JET.OLEDB.4.0; Data Source =" + Application.StartupPath + "\\att2000.mdb;";
        public static void ketnoi()
        {
            //string chuoiKetNoi1 = "Provider=Microsoft.JET.OLEDB.4.0; Data Source = " + KhaiBao.database2 + "";
            conn = new OleDbConnection(chuoiKetNoi1);
            conn.Open();
        }

        public static DataTable read1(string OleDb)
        {

            DataTable result = new DataTable();
            if (!OleDb.ToUpper().Contains("SELECT"))
            {
                OleDb = "SELECT * FROM " + OleDb;
            }
            try
            {
               
                OleDbDataAdapter adapter = new OleDbDataAdapter(OleDb, chuoiKetNoi1);                
                adapter.FillSchema(result, SchemaType.Source);
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }

        public static DataTable readStructure1(string tableName)
        {


            DataTable result = new DataTable();
            string OleDb = "SELECT * FROM " + tableName;

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(OleDb, chuoiKetNoi1);
                adapter.FillSchema(result, SchemaType.Source);

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }
    }
}
