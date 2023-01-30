using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace Data
{
    public class APCoreDataSQLCE
    {
        public static string chuoiKetNoi = @"Data Source=E:\Libs\Drive\Libs\Data\Data\system.sdf;Password=123456";
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
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, chuoiKetNoi);
                adapter.FillSchema(result, SchemaType.Source);
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }
        public static DataTable read(string sql, string namePro, string[,] arrParameters, string strPara)
        {
            string _sql = "";
            _sql="declare "+ strPara;
            DataTable result = new DataTable();
            if (!sql.ToUpper().Contains("SELECT"))
            {
                sql = "SELECT * FROM " + sql;
            }
            try
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, chuoiKetNoi);
                adapter.FillSchema(result, SchemaType.Source);
                adapter.Fill(result);
                foreach (DataColumn col in result.Columns)
                    col.ReadOnly = false;
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
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
            SqlCeCommand cmd = new SqlCeCommand(namePro);
            SqlCeConnection sqlConnection1 = new SqlCeConnection(Data.APCoreData.chuoiKetNoi);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            for (int i = 0; i < arrParameters.Length / arrParameters.Rank; i++)
            {
                cmd.Parameters.AddWithValue("@" + arrParameters[i, 0], arrParameters[i, 1]);
            }
            sqlConnection1.Open();
            SqlCeDataAdapter adapt = new SqlCeDataAdapter(cmd);
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
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, chuoiKetNoi);
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
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, chuoiKetNoi);

                SqlCeCommandBuilder cmdBuilder = new SqlCeCommandBuilder(adapter);
                result = adapter.Update(dt);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return result;
        }

        public static int ExcuteSQL(string sql)
        {
            int result = 0;
            using (SqlCeConnection connection = new SqlCeConnection(chuoiKetNoi))
            {
                try
                {
                    SqlCeCommand command = new SqlCeCommand(sql, connection);
                    connection.Open();
                    result=Convert.ToInt16( command.ExecuteScalar());
                     
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    // Always call Close when done reading.
                    connection.Close();
                }
            }
            return result;
        }
    }
}
