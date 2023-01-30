using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
//using System.Data.OleDb;

namespace Data
{
    public class APCoreData
    {
        public static string chuoiKetNoi = @"Data Source=THANHLM\SQLEXPRESS2016;Initial Catalog=system_2020;Persist Security Info=True;User ID=sa;Password=SA@123456";
        //public static string chuoiKetNoi = @"Data Source=103.28.38.251;Initial Catalog=LOAN_MANAGER;Persist Security Info=True;User ID=sa;Password=%*xpof0!7QQ6D^TL";
        public static DataTable read(string sql)
        {
     
            DataTable result = new DataTable();
            if (!sql.ToUpper().Contains("SELECT"))
            {
                sql = "SELECT * FROM " + sql;
            }
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, chuoiKetNoi);
                adapter.FillSchema(result, SchemaType.Source);
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                // DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }
        public static DataTable read(string sql, string namePro, string[,] arrParameters, string strPara)
        {
            DataTable result = new DataTable();                 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(namePro);
                    SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection1;
                    for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
                    }
                    sqlConnection1.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    //adapt.SelectCommand = cmd;
                    DataSet dt = new DataSet();
                    adapt.Fill(dt);
                    result = dt.Tables[0];
                    sqlConnection1.Close();
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                    if (!sql.ToUpper().Contains("SELECT"))
                    {
                        sql = "SELECT * FROM " + sql;
                    }
                    APCoreData.ExcuteSQL(" create proc " + namePro + " " + (strPara !="" ? "("+ strPara + ")" : " ") + "  as begin " + sql + " end ");
                }
            }
            return result;
        }
        /*
         * @todo read database table 's structure
         * */
        private static bool checkExitsStorePro(string storeName)
        {
            bool flag = false;
            if (ExcuteProc("checkExistProc", new string[1, 2] { { "storeName", storeName } }).Rows[0][0].ToString() == "0")
                flag = false;
            else
                flag = true;
            return flag;
        }

        private static DataTable ExcuteProc(string namePro, string[,] arrParameters)
        {
            SqlCommand cmd = new SqlCommand(namePro);
            SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
            {
                cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
            }
            sqlConnection1.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            //adapt.SelectCommand = cmd;
            DataSet dt = new DataSet();
            adapt.Fill(dt);
            sqlConnection1.Close();
            return dt.Tables[0];
        }

        public static DataTable readStructure(string tableName)
        {
            DataTable result = new DataTable();
            string sql = "SELECT * FROM " + tableName;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, chuoiKetNoi);
                adapter.FillSchema(result, SchemaType.Source);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }
        /*
         * @todo write to database
         * */
        public static int write(DataRow dr)
        {
            int result = 0;
            string sql = "SELECT * FROM " + dr.Table.TableName;
            DataTable dt = dr.Table;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, chuoiKetNoi);
                
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                result = adapter.Update(dt);
                writeLogSQL("Success", sql);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                writeLogSQL("Error", sql);
            }
            return result;
        }

        public static int ExcuteSQL(string sql)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(chuoiKetNoi))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    result = Convert.ToInt16(command.ExecuteScalar());
                    writeLogSQL("Success",sql);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                    writeLogSQL("Error", sql);
                }
                finally
                {
                    // Always call Close when done reading.
                    connection.Close();
                }
            }
            return result;
        }

        public static void writeLogSQL(String status ,String sql)
        {
            try
            {
                String path = Application.StartupPath + "\\log\\log.log";
               

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(status + " : Time :" + DateTime.Now + " : " + sql, true);
                }	
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông báo", "Error " + ex.Message);
            }

        }

        ////////////////////////////////////////////////f
        //public static OleDbConnection conn;
        //public static string chuoiKetNoi1 = "Provider=Microsoft.JET.OLEDB.4.0; Data Source =" + APCoreData.read("sysconfig").Rows[0]["pathAccess"].ToString() + "\\WiseEye2010_V3.mdb;Jet OLEDB:Database Password=12113009;";

        //public static void ketnoi()
        //{
        //    //string chuoiKetNoi1 = "Provider=Microsoft.JET.OLEDB.4.0; Data Source = " + KhaiBao.database2 + "";
        //    conn = new OleDbConnection(chuoiKetNoi1);
        //    conn.Open();
        //}

        //public static DataTable read1(string OleDb)
        //{

        //    DataTable result = new DataTable();
        //    if (!OleDb.ToUpper().Contains("SELECT"))
        //    {
        //        OleDb = "SELECT * FROM " + OleDb;
        //    }
        //    try
        //    {
        //        OleDbDataAdapter adapter = new OleDbDataAdapter(OleDb, chuoiKetNoi1);
        //        adapter.FillSchema(result, SchemaType.Source);
        //        adapter.Fill(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        //DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        //    }
        //    return result;
        //}

        //public static DataTable readStructure1(string tableName)
        //{


        //    DataTable result = new DataTable();
        //    string OleDb = "SELECT * FROM " + tableName;

        //    try
        //    {
        //        OleDbDataAdapter adapter = new OleDbDataAdapter(OleDb, chuoiKetNoi1);
        //        adapter.FillSchema(result, SchemaType.Source);

        //    }
        //    catch (Exception ex)
        //    {
        //        //DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        //    }
        //    return result;
        //}
    }
}
