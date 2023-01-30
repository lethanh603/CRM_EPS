using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DMNHANVIENKH.Presentation
{
    public partial class frmConfig : DevExpress.XtraEditors.XtraForm
    {
        public frmConfig()
        {
            InitializeComponent();
        }
     

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_S_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("Configmail");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                dr["smtp"] = txt_smtp_I2.Text;
                dr["emailaddress"] = txt_mailEddress_I2.Text;
                dr["username"] = txt_username_I2.Text;
                dr["password"] =Function.clsFunction.mahoapw( txt_pass_I2.Text);
                dr["port"] = txt_port_S.Text;
                APCoreProcess.APCoreProcess.Save(dr);
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["smtp"] = txt_smtp_I2.Text;
                dr["emailaddress"] = txt_mailEddress_I2.Text;
                dr["username"] = txt_username_I2.Text;
                dr["password"] =Function.clsFunction.mahoapw( txt_pass_I2.Text);
                dr["port"] = txt_port_S.Text;
                dt.Rows.Add(dr);
                APCoreProcess.APCoreProcess.Save(dr);
            }
            XtraMessageBox.Show("Save successfull","Message");
            this.Close();
        }
        private bool checkInput()
        {
            if (txt_mailEddress_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Mail Address not null","Message");
                txt_mailEddress_I2.Focus();
                return false;
            }
            if (txt_pass_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Password not null", "Message");
                txt_pass_I2.Focus();
                return false;
            }
     
            if (txt_smtp_I2.Text == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Smtp not null", "Message");
                txt_smtp_I2.Focus();
                return false;
            }
            if (txt_username_I2.Text != "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Username not null", "Message");
                txt_username_I2.Focus();
                return false;
            }

    
            return true;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("Configmail");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
               txt_smtp_I2.Text = dr["smtp"].ToString() ;
               txt_mailEddress_I2.Text = dr["emailaddress"].ToString();
                txt_username_I2.Text =dr["username"].ToString() ;
               txt_pass_I2.Text= Function.clsFunction.giaima( dr["password"].ToString()) ;
               txt_port_S.Text = dr["port"].ToString();
            }
        }
    }
}