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
    public partial class frmThongBaoBaoTri : DevExpress.XtraEditors.XtraForm
    {
        public frmThongBaoBaoTri()
        {
            InitializeComponent();
        }

        public DataTable dt;

        private void frmThongBaoBaoTri_Load(object sender, EventArgs e)
        {
            try
            {
                gct_history_C.DataSource = dt;
                DataTable dtKH = APCoreProcess.APCoreProcess.Read("select * from v_thongbao_kehoach");
                gct_kehoach_C.DataSource = dtKH;
            }
            catch (Exception ex)
            {
                Function.clsFunction.MessageInfo("Thông báo", ex.Message);
            }
        }
    }
}