using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SOURCE_FORM_CRM.Presentation
{
    public partial class frm_DisCus : DevExpress.XtraEditors.XtraForm
    {
        public frm_DisCus()
        {
            InitializeComponent();
        }
        #region Var
        public string idcustomer;// CUS000001@CUS000002@
        public string idempdis;
        public delegate void strPassData(string value);// IDEMP
        public strPassData strpassData;
      
        #endregion 
        #region FormEvent
        private void frm_DisCus_Load(object sender, EventArgs e)
        {
            loadGridLookupCustomer();
            loadGridLookupEmployee();
            glue_idempdis_S.EditValue = idempdis;
            glue_IDEMP_I1.EditValue = idempdis;
            glue_idempdis_S.Properties.ReadOnly = true;
            glue_IDEMP_I1.Focus();
        }

        private void frm_DisCus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                bbi_allow_insert.PerformClick();
            }
            else
            {
                if (e.KeyCode == Keys.F9)
                {
                    btn_exit_S.PerformClick();
                }
            }
        }

        #endregion
        #region Method

        private void loadGridLookupCustomer()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_idempdis_S, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);

            }
            catch { }
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_IDEMP_I1, "select IDEMP, StaffName from EMPLOYEES", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_IDEMP_I1.Width);
            }
            catch { }
        }

        #endregion

        #region ButtonEvent

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            if (idempdis == glue_IDEMP_I1.EditValue.ToString())
            {
                Function.clsFunction.MessageInfo("Thông báo", "Bạn phải chọn nhân viên khác");
                return;
            }
            string[] arrCus = idcustomer.Split('@');
            for (int i = 0; i < arrCus.Length; i++)
            {
                APCoreProcess.APCoreProcess.ExcuteSQL("update empcus set idemp='" + glue_IDEMP_I1.EditValue.ToString() + "' where idcustomer='" + arrCus[i] + "' and idemp='" + idempdis + "' ");

                saveEmpcushis(glue_IDEMP_I1.EditValue.ToString(), arrCus[i], true);
                saveEmpcushis(glue_idempdis_S.EditValue.ToString(), arrCus[i], false);

            }
            strpassData(glue_IDEMP_I1.EditValue.ToString());
            Function.clsFunction.MessageInfo("Thông báo", "Chuyển khách hàng hoàn tất");
            this.Close();
        }

        private void saveEmpcushis(String idemp , String idcustomer,bool status)
        {
            if (idcustomer == "")
                return;
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from empcushis");
            DataRow dr = dt.NewRow();
            dr["idemp"] = idemp;
            dr["idcustomer"] = idcustomer;
            dr["status"] = status;
            dr["datecn"] = DateTime.Now;
            dt.Rows.Add(dr);
            APCoreProcess.APCoreProcess.Save(dr);
        }

        #endregion

 

    }
}