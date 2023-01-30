using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;

namespace LoyalHRM.Presentation
{
    public partial class frmConfigDatabase : DevExpress.XtraEditors.XtraForm
    {
        public frmConfigDatabase()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();

        private void btn_thoat_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("server", typeof(string));
            dt.Columns.Add("account", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("DBName", typeof(string));
            dt.Columns.Add("path", typeof(string));
        
            dt.Columns.Add("passCE", typeof(string));
            dt.Columns.Add("dbnameCE", typeof(string));
            dt.Columns.Add("passaccess", typeof(string));
            dt.Columns.Add("isSqlServer", typeof(bool));
            DataRow dr = dt.NewRow();
            dr["server"] =( txt_maychu_S.Text);
            dr["account"] = Function.clsFunction.mahoapw(txt_taikhoan_S.Text);
            dr["password"] = Function.clsFunction.mahoapw(txt_matkhau_S.Text);
            dr["DBName"] = Function.clsFunction.mahoapw(txt_dbname_S.Text);

            dr["path"] = (txt_path_S.Text);

            dr["passCE"] = Function.clsFunction.mahoapw(txt_passCE_S.Text);
            dr["dbnameCE"] = Function.clsFunction.mahoapw(txt_dbnameCE_S.Text);
            dr["passaccess"] = Function.clsFunction.mahoapw(txt_passaccess_I2.Text);
            dr["isSqlServer"] = rad_group_C.EditValue.ToString() == "0" ? true : false;
            
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml("config.xml");

            Application.Restart();
        }

        private void frmConfigDatabase_Load(object sender, EventArgs e)
        {
            try
            {
                ds.ReadXml("config.xml");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txt_maychu_S.Text=(ds.Tables[0].Rows[0]["server"].ToString());
                    txt_taikhoan_S.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["account"].ToString());
                    txt_matkhau_S.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["password"].ToString());
                    txt_dbname_S.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["DBName"].ToString());

                    txt_path_S.Text = (ds.Tables[0].Rows[0]["path"].ToString());

                    txt_passCE_S.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["passCE"].ToString());
                    txt_dbnameCE_S.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["dbnameCE"].ToString());
                    txt_passaccess_I2.Text = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["passaccess"].ToString());
                    txt_repearpass_I2.Text = txt_passaccess_I2.Text;
                    rad_group_C.SelectedIndex = Convert.ToBoolean(ds.Tables[0].Rows[0]["isSqlServer"]) ==true ? 0:1 ;

                }
                //Function.clsFunction.TranslateForm(this, this.Name);
            }
            catch
            {
 
            }

        }

        private void btn_checkconect_S_Click(object sender, EventArgs e)
        {
            try
            {
                string sConn = "";
                if (rad_group_C.EditValue.ToString() == "0")
                {
                    sConn = "Data Source=" + txt_maychu_S.Text + ";Initial Catalog=" + txt_dbname_S.Text + ";Persist Security Info=True;User ID=" + txt_taikhoan_S.Text + ";Password=" + txt_matkhau_S.Text + "";
                    SqlConnection sqlConnection1 = new SqlConnection(sConn);
                    sqlConnection1.Open();
                    if (sqlConnection1.State == ConnectionState.Open)
                    {
                        Function.clsFunction.MessageInfo("Thông báo", "Kết nối thành công đến SQL Server");
                    }
                    else
                    {
                        XtraMessageBox.Show("Kết nối thất bại đến SQL Server", "Thông báo");
                    }
                }
                else
                {
                    //Data Source=E:\Libs\Drive\Libs\Data\Data\system.sdf;Password=***********
                    sConn = "Data Source="+ txt_path_S.Text +";Password='"+ txt_passCE_S.Text +"';";
                    SqlCeConnection sqlConnectionCE = new SqlCeConnection(sConn);
                    if (Function.clsFunction.checkFileExist(txt_path_S.Text) == false)
                    {                        
                        SqlCeEngine en = new SqlCeEngine(sConn);
                        en.CreateDatabase();
                    }                  
                    sqlConnectionCE.Open();
                    if (sqlConnectionCE.State == ConnectionState.Open)
                    {
                        XtraMessageBox.Show( "Kết nối thành công đến SQL Compact","Thông báo");
                    }
                    else
                    {
                        XtraMessageBox.Show("Kết nối thất bại đến SQL Compact", "Thông báo");
                    }
                }

            }
            catch (Exception ex)
            { 
       
                    XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_path_S_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "SDF files (*.sdf)|*.sdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            txt_path_S.Text = openFileDialog1.FileName;
                            txt_dbnameCE_S.Text = Path.GetFileName( openFileDialog1.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ptr_change_S_Click(object sender, EventArgs e)
        {
            if (rad_group_C.EditValue.ToString() == "1")
            {
                convertSqlServerToSqlCE();
            }
        }

        private void convertSqlServerToSqlCE()
        {
            try
            {

                Data.APCoreData.chuoiKetNoi = "Data Source=" + txt_maychu_S.Text + ";Initial Catalog=" + txt_dbname_S.Text + ";Persist Security Info=True;User ID=" + txt_taikhoan_S.Text + ";Password=" + txt_matkhau_S.Text + "";
                Data.APCoreDataSQLCE.chuoiKetNoi = "Data Source=" + txt_path_S.Text + ";Password='" + txt_passCE_S.Text + "';";
                APCoreProcess.APCoreProcess.IssqlCe = false;// sqlSv
                System.Data.DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' order by table_name"); // lay danh sach table 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // tao cau truc table cho sqlce
                    DataTable dtCol=new DataTable();
                    APCoreProcess.APCoreProcess.IssqlCe = false;// sqlSv
                    dtCol = APCoreProcess.APCoreProcess.Read("SELECT column_name,data_type, case when character_maximum_length is not null then case when character_maximum_length = -1 then '(4000)' else  '('+ convert(nvarchar, character_maximum_length) + ')' end else '' end as max_length FROM   information_schema.columns WHERE  table_name = '"+dt.Rows[i]["table_name"].ToString()+"' ORDER  BY ordinal_position ");
       
                    DataTable dtSQL = new DataTable();
                    dtSQL = APCoreProcess.APCoreProcess.Read(dt.Rows[i]["table_name"].ToString());
                    APCoreProcess.APCoreProcess.IssqlCe = true;// sqlCE
                    string sql = "";
                    //CREATE TABLE MyCustomers (CustID int IDENTITY (100,1) PRIMARY KEY, CompanyName nvarchar (50))
                    string primarykey = "";
                    sql = " Create table  " + dt.Rows[i]["table_name"].ToString() + " ( ";
                    for (int j = 0; j < dtCol.Rows.Count; j++)
                    {
                        if (j < dtCol.Rows.Count - 1)
                        {
                            if (GetPrimaryKeys(dtSQL, dtCol.Rows[j]["column_name"].ToString())) // la khoa 
                            {
                                primarykey += dtCol.Rows[j]["column_name"].ToString() + ",";
                            }
                            sql += dtCol.Rows[j]["column_name"].ToString() + " " + dtCol.Rows[j]["data_type"].ToString() + " " + dtCol.Rows[j]["max_length"];
                            sql += " , ";                           
                        }
                        else
                        {
                            sql += dtCol.Rows[j]["column_name"].ToString() + " " + dtCol.Rows[j]["data_type"].ToString() + " " + dtCol.Rows[j]["max_length"];
                            if (primarykey.Length>0)
                                sql += " , PRIMARY KEY ("+ primarykey.Substring(0,primarykey.Length-1) +" )";
                            sql += ")";
                        }
                    }
                    Data.APCoreDataSQLCE.chuoiKetNoi = "Data Source=" + txt_path_S.Text + ";Password='" + txt_passCE_S.Text + "';";
                    if (APCoreProcess.APCoreProcess.ExcuteSQL("SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + dt.Rows[i]["table_name"].ToString() + "'") ==1)
                    {
                        APCoreProcess.APCoreProcess.ExcuteSQL("drop table " + dt.Rows[i]["table_name"].ToString() + "");
                    }
                    //if (dt.Rows[i]["table_name"].ToString() == "EXPORTDETAIL")
                    //    MessageBox.Show("");
                    APCoreProcess.APCoreProcess.ExcuteSQL(sql); // tao cau truc sqlce
                    // import data to sql ce
                    
                    APCoreProcess.APCoreProcess.IssqlCe = true;// sqlCE
                    DataTable dtCE = new DataTable();
                    dtCE = APCoreProcess.APCoreProcess.Read(dt.Rows[i]["table_name"].ToString());
                 
                    if (dtSQL.Rows.Count > 0)
                    {                      
                        DataRow dr;
                        for (int k = 0; k < dtSQL.Rows.Count; k++)
                        {
                            dr = dtCE.NewRow();
                            for (int h = 0; h < dtSQL.Columns.Count; h++)
                            {
                                try
                                {
                                    dr[dtSQL.Columns[h].ColumnName] = dtSQL.Rows[k][h].ToString();
                                }
                                catch { }
                            }
                                dtCE.Rows.Add(dr);
                            APCoreProcess.APCoreProcess.Save(dr);
                        }
                    }
                }
            }
            catch { }
        }

        private bool GetPrimaryKeys(DataTable table, string fieldName)
        {
            bool flag=false;
            // Create the array for the columns.
            DataColumn[] columns;
            columns = table.PrimaryKey;
            // Get the number of elements in the array.
            Console.WriteLine("Column Count: " + columns.Length);
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].ColumnName == fieldName)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

   
    }
}