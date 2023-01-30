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
    public partial class frmKey : DevExpress.XtraEditors.XtraForm
    {
        public frmKey()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void frmKey_Load(object sender, EventArgs e)
        {
            load();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt =((DataTable)gridControl1.DataSource);
            DataRow dr = dt.Rows[0];
            dr["A002"] = SECURITY.clsSecurity.EC_CL(dr["A002"].ToString());
            dr["A003"] = SECURITY.clsSecurity.EC_CL(dr["A003"].ToString());
            dr["A004"] = SECURITY.clsSecurity.EC_CL(dr["A004"].ToString());
            dr["A005"] = SECURITY.clsSecurity.EC_CL(dr["A005"].ToString());
            dr["A006"] = SECURITY.clsSecurity.EC_CL(dr["A006"].ToString());
            dr.EndEdit();
            APCoreProcess.APCoreProcess.Save(dr);
            load();
        
        }
        private void load()
        {
            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("AAA");
            DataRow dr = dt.Rows[0];
            dr["A002"] = SECURITY.clsSecurity.DC_CL(dr["A002"].ToString());
            dr["A003"] = SECURITY.clsSecurity.DC_CL(dr["A003"].ToString());
            dr["A004"] = SECURITY.clsSecurity.DC_CL(dr["A004"].ToString());
            dr["A005"] = SECURITY.clsSecurity.DC_CL(dr["A005"].ToString());
            dr["A006"] = SECURITY.clsSecurity.DC_CL(dr["A006"].ToString());
            dr.EndEdit();
            gridControl1.DataSource = dt;
        }

   
    }
}