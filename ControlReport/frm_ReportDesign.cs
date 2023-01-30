using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace ControlReport
{
    public partial class frm_ReportDesign : DevExpress.XtraEditors.XtraForm
    {
        public frm_ReportDesign()
        {
            InitializeComponent();
        }

        private void bbi_config_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ControlDev.frmConfigRePort frm = new ControlDev.frmConfigRePort();
            frm.sql = "select * from thongtin; select * from nhapphongban;select * from nhapnhanvien";
            frm.langues = "_VI";
            frm.group = 1;
            frm.formname = this.Name;
            frm.ShowDialog();
        }

        private void bbi_call_report_S_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
       
            try
            {
                XtraReport report = new XtraReport();// = CreateReport();
                report.LoadLayout("C:\\LoyaltyData\\nhanvien.repx");
                DataSet ds = new DataSet();
                DataTable dtthong = new DataTable();
                dtthong = APCoreProcess.APCoreProcess.Read("thongtin");
                ds.Tables.Add(dtthong);
                DataTable dtPhongban = new DataTable();
                dtPhongban = APCoreProcess.APCoreProcess.Read("nhapphongban");
                ds.Tables.Add(dtPhongban);
                DataTable dtNhanvien = new DataTable();
                dtNhanvien = APCoreProcess.APCoreProcess.Read("nhapnhanvien");
                ds.Tables.Add(dtNhanvien);          
                report.DataSource = ds;                
                report.ShowPreview();            
            }
            catch (Exception ex)
            {
      
            }
        }

        private void frm_ReportDesign_Load(object sender, EventArgs e)
        {

        }
    }
}