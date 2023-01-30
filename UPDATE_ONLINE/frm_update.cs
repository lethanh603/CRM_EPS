//anhthanh@vnfaith.com
//thanh@123
//173.254.28.102
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;

namespace UPDATE_ONLINE
{
    public partial class frm_update : DevExpress.XtraEditors.XtraForm
    {
        public frm_update(string pathApp1)
        {
            InitializeComponent();      
         
        }
        string pathApp = "";
        private void frm_update_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dt1 = new DataSet();
                dt1.ReadXml("path.xml");
                Data.APCoreData.chuoiKetNoi = dt1.Tables[0].Rows[0][1].ToString();
                pathApp = dt1.Tables[0].Rows[0][0].ToString();
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysVar");
                string pathftp = "", username = "", pass = "", pathout = "";
                pathftp = dt.Rows[2]["value"].ToString();
                username = dt.Rows[0]["value"].ToString();
                pass = dt.Rows[1]["value"].ToString();
                pathout = dt.Rows[3]["value"].ToString();
                string[] filePaths = Directory.GetFiles(@"" + pathout);
                lbl_show.Text = "0 / " + (filePaths.Length);
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {      
            try
            {       
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysVar");
                string pathftp = "", username = "", pass = "", pathout = "";
                pathftp = dt.Rows[2]["value"].ToString();
                username = dt.Rows[0]["value"].ToString();
                pass = dt.Rows[1]["value"].ToString();
                pathout = dt.Rows[3]["value"].ToString();
                string[] filePaths = Directory.GetFiles(@"" + pathout);
                for (int i = 0; i < filePaths.Length;i++)
                {
                    File.Copy(@"" + filePaths[i], @"" + pathApp+"\\" + filePaths[i].Split('\\')[filePaths[i].Split('\\').Length-1], true);
                    lbl_show.Text = i+" / "+ (filePaths.Length-1);
                }
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@""+pathout);
                string[] filePathDelete = Directory.GetFiles(@""+pathout);
                foreach (string filePath in filePathDelete)
                    File.Delete(filePath);
                this.Hide();
                Process process = new Process();
                // Configure the process using the StartInfo properties.
                process.StartInfo.FileName = Application.StartupPath + "\\LoyalHRM.exe";
                process.StartInfo.Arguments = "-n";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.Start();
                process.WaitForExit();// Waits here for the process to exit.

                foreach (var process1 in Process.GetProcessesByName("UPDATE_ONLINE.exe"))
                {
                    process1.Kill();
                }
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}