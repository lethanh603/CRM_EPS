using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;

namespace LoyalHRM.Presentation
{
    public partial class frm_BKRT_S : DevExpress.XtraEditors.XtraForm
    {
        private static Server srvSql;

        public frm_BKRT_S()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        bool _connect=false;
        private DevExpress.Utils.WaitDialogForm waitDialog = null;
        ProgressBarControl progressBarControl1 = new ProgressBarControl();
        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a DataTable where we enumerate the available servers 
            DataTable dtServers = SmoApplication.EnumAvailableSqlServers(true);
            khoanut(true);
            // If there are any servers at all 
            //if (dtServers.Rows.Count > 0)
            //{
            //    // Loop through each server in the DataTable 
            //    foreach (DataRow drServer in dtServers.Rows)
            //    {
            //        // Add the name to the combobox 
            //        cmbServer.Items.Add(drServer["Name"]);
            //    }
            //}
            Function.clsFunction.TranslateForm(this, this.Name);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {           
          
            if (checkConnect()==true)
            {        
      
                _connect=true;
                khoanut(false);
            }
            else
            {
                // A server was not selected, show an error message 
                MessageBox.Show("Please select a server first", "Server Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool checkConnect()
        {
            bool flag = false;
            try
            {
             
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["issqlserver"]) == true)
                {
                    Function.clsFunction.DataBaseName = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["DBName"].ToString());
                    Data.APCoreData.chuoiKetNoi = "Data Source=" + (ds.Tables[0].Rows[0]["server"].ToString()) + ";Database=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["DBName"].ToString()) + "; User ID=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["account"].ToString()) + ";Password=" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["password"].ToString()) + ";";
                    //Data.APCoreData.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0][0].ToString() + ";Initial Catalog=" + ds.Tables[0].Rows[0][3].ToString() + ";Integrated Security=True";
                    //Data.APCoreData.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0][0].ToString() + ",1433;Network Library=DBMSSOCN;Initial Catalog=" + ds.Tables[0].Rows[0][3].ToString() + ";User ID=" + ds.Tables[0].Rows[0][1].ToString() + ";Password=" + ds.Tables[0].Rows[0][2].ToString() + ";";
                    APCoreProcess.APCoreProcess.IssqlCe = false;
                    SqlConnection sqlConnection1 = new SqlConnection(Data.APCoreData.chuoiKetNoi);
                    sqlConnection1.Open();
                    if (sqlConnection1.State == ConnectionState.Open)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    Function.clsFunction.DataBaseName = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["dbnameCE"].ToString());
                    Data.APCoreDataSQLCE.chuoiKetNoi = "Data Source=" + ds.Tables[0].Rows[0]["path"].ToString() + ";Password='" + Function.clsFunction.giaima(ds.Tables[0].Rows[0]["passCE"].ToString()) + "';";
                    //Data Source=E:\Libs\Drive\Libs\Data\Data\system.sdf;Password=***********
                    APCoreProcess.APCoreProcess.IssqlCe = true;
                    SqlCeConnection sqlConnectionCE = new SqlCeConnection(Data.APCoreDataSQLCE.chuoiKetNoi);
                    sqlConnectionCE.Open();
                    if (sqlConnectionCE.State == ConnectionState.Open)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch { }
            return flag;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("config.xml");
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
            {
                backUPSqlServer(ds);
            }
            else
            {
                backUPSqlCe(ds);
            }
        }

        private void backUPSqlServer(DataSet ds)
        {
             // Create a new connection to the selected server name 
                ServerConnection srvConn = new ServerConnection(Function.clsFunction.giaima(ds.Tables[0].Rows[0]["server"].ToString()));
                // Log in using SQL authentication instead of Windows authentication 
                srvConn.LoginSecure = false;
                // Give the login username 
                srvConn.Login = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["account"].ToString());
                // Give the login password 
                srvConn.Password = Function.clsFunction.giaima(ds.Tables[0].Rows[0]["password"].ToString());
                // Create a new SQL Server object using the connection we created 
                srvSql = new Server(srvConn);
                // If there was a SQL connection created 
                if (srvSql != null && _connect)
                {
                    // If the user has chosen a path where to save the backup file 
                    if (saveBackupDialog.ShowDialog() == DialogResult.OK)
                    {
                        waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                        // Create a new backup operation 
                        Backup bkpDatabase = new Backup();
                        // Set the backup type to a database backup 
                        bkpDatabase.Action = BackupActionType.Database;
                        // Set the database that we want to perform a backup on 
                        bkpDatabase.Database = ds.Tables[0].Rows[0]["DBName"].ToString();

                        // Set the backup device to a file 
                        BackupDeviceItem bkpDevice = new BackupDeviceItem(saveBackupDialog.FileName, DeviceType.File);
                        // Add the backup device to the backup 
                        bkpDatabase.Devices.Add(bkpDevice);
                        // Perform the backup 
                        bkpDatabase.SqlBackup(srvSql);
                        waitDialog.Close();
                        this.Close();
                    }
                }
                else
                {
                    waitDialog.Close();
                    // There was no connection established; probably the Connect button was not clicked 
                    MessageBox.Show("A connection to a SQL server was not established.", "Not Connected to Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }

        private void backUPSqlCe(DataSet ds)
        {
            if (saveBackupDialog.ShowDialog() == DialogResult.OK)
                {
                    waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
                    File.Copy(ds.Tables[0].Rows[0]["path"].ToString(), saveBackupDialog.FileName);
                    waitDialog.Close();
                    Function.clsFunction.MessageInfo("Thông báo","Sao lưu thành công");
                    this.Close();
                }            
            else
            {
                waitDialog.Close();
                // There was no connection established; probably the Connect button was not clicked 
                MessageBox.Show("A connection to a SQL server was not established.", "Not Connected to ServerCe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
 
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (APCoreProcess.APCoreProcess.IssqlCe == false)
                restoreSqlServer();
            else
                 restoreSqlCe();
        }

        private void restoreSqlCe()
        {
            waitDialog = new DevExpress.Utils.WaitDialogForm("Đang sao lưu dữ liệu", "Xin vui lòng chờ trong giây lát");
            File.Copy( openBackupDialog.FileName,ds.Tables[0].Rows[0]["path"].ToString());
            waitDialog.Close();
            Function.clsFunction.MessageInfo("Thông báo", "Sao lưu thành công");
            this.Close();
        }

        private void restoreSqlServer()
        {
            try
            {
                // If there was a SQL connection created 
                if (srvSql != null && _connect)
                {
                    // If the user has chosen the file from which he wants the database to be restored 
                    if (openBackupDialog.ShowDialog() == DialogResult.OK)
                    {
                        waitDialog = new DevExpress.Utils.WaitDialogForm("Đang khôi phục  dữ liệu", "Xin vui lòng chờ trong giây lát");
                        // Create a new database restore operation 
                        Restore rstDatabase = new Restore();
                        // Set the restore type to a database restore 
                        rstDatabase.Action = RestoreActionType.Database;
                        // Set the database that we want to perform the restore on 
                        rstDatabase.Database = Function.clsFunction.giaima(ds.Tables[0].Rows[0][3].ToString());
                        srvSql.KillDatabase(Function.clsFunction.giaima(ds.Tables[0].Rows[0][3].ToString()));
                        // Set the backup device from which we want to restore, to a file 
                        BackupDeviceItem bkpDevice = new BackupDeviceItem(openBackupDialog.FileName, DeviceType.File);
                        // Add the backup device to the restore type 
                        rstDatabase.Devices.Add(bkpDevice);
                        // If the database already exists, replace it 
                        rstDatabase.ReplaceDatabase = true;
                        // Perform the restore 
                        rstDatabase.SqlRestore(srvSql);
                        srvSql.Refresh();
                        waitDialog.Close();
                        this.Close();
                    }
                }
                else
                {
                    waitDialog.Close();
                    // There was no connection established; probably the Connect button was not clicked 
                    MessageBox.Show("A connection to a SQL server was not established.", "Not Connected to Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch { }
        }

        private void khoanut(bool khoa)
        {
            btnConnect.Enabled=khoa;
            btnCreate.Enabled=!khoa;
            btnRestore.Enabled=!khoa;
        }
    }
}