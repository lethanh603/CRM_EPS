using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LoyalHRM.Presentation
{
    public partial class frnConfigFTP : DevExpress.XtraEditors.XtraForm
    {
        public frnConfigFTP()
        {
            InitializeComponent();
        }

        private void frnConfigFTP_Load(object sender, EventArgs e)
        {
            Function.clsFunction.TranslateForm(this, this.Name);
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("sysVar");
            if (dt.Rows.Count > 0)
            {
                txt_address_I2.Text = SECURITY.clsSecurity.DC_CL( dt.Rows[2]["value"].ToString());
                txt_account_I2.Text = SECURITY.clsSecurity.DC_CL(dt.Rows[0]["value"].ToString());
                txt_password_I2.Text = SECURITY.clsSecurity.DC_CL(dt.Rows[1]["value"].ToString());
                txt_path_I2.Text = dt.Rows[3]["value"].ToString();
            }
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_S_Click(object sender, EventArgs e)
        {
            try
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("truncate table sysVar");
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("sysVar");
                DataRow dr1 = dt.NewRow();
                dr1["name"] = "usernameftp";
                dr1["value"] = SECURITY.clsSecurity.EC_CL(txt_account_I2.Text);
                dt.Rows.Add(dr1);
                APCoreProcess.APCoreProcess.Save(dr1);
                DataRow dr2 = dt.NewRow();
                dr2["name"] = "passftp";
                dr2["value"] = SECURITY.clsSecurity.EC_CL(txt_password_I2.Text);
                dt.Rows.Add(dr2);
                APCoreProcess.APCoreProcess.Save(dr2);
                DataRow dr3 = dt.NewRow();
                dr3["name"] = "pathftp";
                dr3["value"] = SECURITY.clsSecurity.EC_CL(txt_address_I2.Text);
                dt.Rows.Add(dr3);
                APCoreProcess.APCoreProcess.Save(dr3);
                DataRow dr4 = dt.NewRow();
                dr4["name"] = "pathtempftp";
                dr4["value"] = (txt_path_I2.Text);
                dt.Rows.Add(dr4);
                APCoreProcess.APCoreProcess.Save(dr4);
                this.Close();
            }
            catch { }
        }

        private void frnConfigFTP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                btn_save_S.PerformClick();
            else
                btn_exit_S.PerformClick();
        }
    }
}