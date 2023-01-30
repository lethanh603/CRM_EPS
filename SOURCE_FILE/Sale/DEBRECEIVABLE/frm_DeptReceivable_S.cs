using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DEBTRECEIVABLE.Presentation
{
    public partial class frm_DeptReceivable_S : DevExpress.XtraEditors.XtraForm
    {

        #region VAR
        public string idemp = string.Empty;
        public string idexport = string.Empty;
        public string iddebtreceivable = string.Empty;
        public decimal amount = 0;
        public decimal rate = 0;
        public decimal numdate = 0;
        public decimal rest = 0;
        public decimal amountrate = 0;     
        public DateTime datedebt;
        public string dauma = "";
        public delegate void PassData(bool value);
        public PassData passData;
        #endregion

        #region Contructer

        public frm_DeptReceivable_S()
        {
            InitializeComponent();
        }

        #endregion        

        #region FormEvent

        private void frm_DeptReceivable_S_Load(object sender, EventArgs e)
        {
            initLoad();
        }

        #endregion

        #region ButtonEvent

        private void btn_allow_insert_S_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void btn_allow_print_S_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Event

        private void dte_datecreate_I2_EditValueChanged(object sender, EventArgs e)
        {
            decimal songay = 0;
            TimeSpan diff = Convert.ToDateTime(dte_datecreate_I2.EditValue) - Convert.ToDateTime(dte_datedebt_S.EditValue);
            songay = diff.Days;
            cal_amountrate_S.Value = Convert.ToDecimal(cal_rest_S.Value) * Convert.ToDecimal(rate) / 100 / 30 * songay;
        }

        #endregion

        #region Methods

        private void initLoad()
        {
            loadGridLookupEmployee();
            dte_datecreate_I2.EditValue = DateTime.Now;
            dte_datecreate_I2.EditValue = datedebt;
            txt_idexport_I1.Text = idexport;
            glue_IDEMP_I1.EditValue = Function.clsFunction._iduser;
            cal_amountrate_S.Value = amountrate;
            cal_moneydebt_S.Value = amount;
            cal_numdate_S.Value = numdate;
            cal_pay_I4.Value = rest;
            cal_rate_S.Value = rate;
            cal_rest_S.Value = rest;
            cal_debtrest_S.Value = rest - cal_pay_I4.Value;
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

        private void insert()
        {
            if (!checkInput()) return;
            save();
        }

        private void save()
        {

            DataTable dt = new DataTable();
            dt = APCoreProcess.APCoreProcess.Read("debtreceivable where iddebtreceivable='"+iddebtreceivable+"'");
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["iddebtreceivable"] = lbl_iddebtreceivable_IK1.Text;
                dr["idexport"] = txt_idexport_I1.Text;
                dr["idemp"] = glue_IDEMP_I1.EditValue.ToString();
                dr["moneydebt"] = cal_pay_I4.Value;
                dr["amountrate"] = cal_amountrate_S.Value;
                dr["datedebt"] = Convert.ToDateTime(dte_datecreate_I2.EditValue);
                dr["statusdel"] = false;
                dr["statusreceivable"] = false;
                dr["reason"] = mmo_reason_I7.Text;
                dr["numdatelate"] = cal_numdate_S.Value;
                dt.Rows.Add(dr);       
            }
            else
            {
                dr = dt.Rows[0];          
                dr["idexport"] = txt_idexport_I1.Text;
                dr["idemp"] = glue_IDEMP_I1.EditValue.ToString();
                dr["moneydebt"] = cal_pay_I4.Value;
                dr["amountrate"] = cal_amountrate_S.Value;
                dr["datedebt"] = Convert.ToDateTime(dte_datecreate_I2.EditValue);
                dr["statusdel"] = false;
                dr["statusreceivable"] = false;
                dr["reason"] = mmo_reason_I7.Text;
                dr["numdatelate"] = cal_numdate_S.Value ;
                dt.EndInit();              
            }
            APCoreProcess.APCoreProcess.Save(dr);         
            passData(true);
        }

        private bool checkInput()
        {
            if (cal_pay_I4.Value == 0)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Nhập số tiền");
                cal_pay_I4.Focus();
                return false;
            }
            if (glue_IDEMP_I1.EditValue == null)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Chọn nhân viên");
                glue_IDEMP_I1.Focus();
                return false;
            }
            if (cal_pay_I4.Value > cal_rest_S.Value)
            {
                Function.clsFunction.MessageInfo("Thông báo", "Sai giá trị");
                cal_pay_I4.Focus();
                return false;
            }
            return true;
        }

        #endregion      



    }
}