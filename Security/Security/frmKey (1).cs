using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SECURITY
{
    public partial class frmKey : DevExpress.XtraEditors.XtraForm
    {
        public frmKey()
        {
            InitializeComponent();
        }

        private void frmKey_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("AAA");
            gridControl1.DataSource = dt;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}