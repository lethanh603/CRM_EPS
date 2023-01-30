using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LoyalHRM.Presentation
{
    public partial class frmSMS : DevExpress.XtraEditors.XtraForm
    {
        public frmSMS()
        {
            InitializeComponent();
        }
   
        public string langues = "_VI";
        public delegate void PassData(string value);
        public PassData passData;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_luu_S_Click(object sender, EventArgs e)
        {
            APCoreProcess.APCoreProcess.ExcuteSQL("update sysconfig set message=N'" + memoEdit1.Text + "' ");
            passData(memoEdit1.Text);
            this.Close();
        }

        private void frmSMS_Load(object sender, EventArgs e)
        {
            try
            {
                memoEdit1.Text = APCoreProcess.APCoreProcess.Read("select message from sysConfig").Rows[0][0].ToString();
                //if (passData != null)
                Function.clsFunction.TranslateForm(this, this.Name);
                this.Text = Function.clsFunction.transLateText(this.Text);
            }
            catch { }
        }
    }
}