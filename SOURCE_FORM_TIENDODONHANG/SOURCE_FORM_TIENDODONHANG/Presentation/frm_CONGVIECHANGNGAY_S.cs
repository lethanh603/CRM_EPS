using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using Function;

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_CONGVIECHANGNGAY_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_CONGVIECHANGNGAY_S()
        {
            InitializeComponent();
        }
        #endregion

        #region Var

        public bool _insert = false;
        public bool call = false;     
        public bool statusForm;    
        public delegate void PassData(bool value);
        public PassData passData;
        public delegate void strPassData(string value);
        public strPassData strpassData;
        public string _sign = "IF";
        public string ID = "";
        public string manhiemvu = "";

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            // statusForm = true;
            
            if (statusForm == true)
            {
                Function.clsFunction.Save_sysControl(this, this);
                try
                {
                    Function.clsFunction.CreateTable(this, this);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Error " + MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txt_giobatdau_I2.Text = "08:00";
                txt_gioketthuc_I2.Text = "17:00";
                loadGridLookupEmployee();
                loadGridLookupNhiemVu();
                loadGridLookupQuanly();
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadInfo();
            }
            txt_idngay_IK1.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            txt_idngay_IK1.Focus();
        }

        private void frm_DMAREA_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                bbi_allow_insert.PerformClick();
            }
            else
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
        }


        #endregion

        #region ButtonEvent

        private void bbi_allow_insert_Click(object sender, EventArgs e)
        {
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                this.Close();
            }
            
        }

        private void btn_exit_S_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_insert_allow_insert_Click(object sender, EventArgs e)
        {
           
        }



        private void InitData()
        {
            _insert = true;
            LoadInfo();
        }

        #endregion
        
        #region Event

        // enter next control
        
        private void txt_sign_I2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        #endregion

        #region GridEvent



        #endregion

        #region Methods

        private void save()
        {
            if (!checkInput()) return;
            if (_insert == true)
            {
                Function.clsFunction.Insert_data(this, this.Name);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idngay_IK1.Name) + " = '" + txt_idngay_IK1.Text + "'"));
                //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_id_IK1.Text, txt_area_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idngay_IK1.Text);
                }
                else
                {
                    LoadInfo();
                    passData(true);
                }
                
                //this.Hide();  
                dxEp_error_S.ClearErrors();
            }
            else
            {
                DataTable dt = APCoreProcess.APCoreProcess.Read("select sogioketthuc from STARTRENTDEVICE where id ='"+ txt_idngay_IK1.Text +"'");
                if (dt.Rows.Count > 0)
                {
                    if (!checkAdmin() && Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                        return;
                    }
                }
                Function.clsFunction.Edit_data(this, this.Name,"idngay",ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idngay_IK1.Name) + " = '" + txt_idngay_IK1.Text + "'"));
                //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_id_IK1.Text, txt_area_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                dxEp_error_S.ClearErrors();
                passData(true);
            }
        }
        private bool checkAdmin()
        {
            bool flag = false;
            DataTable dt = APCoreProcess.APCoreProcess.Read("select * from sysUser where root = 1 AND userid = '" + clsFunction._iduser + "'");
            if (dt.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idngay_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idngay_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idngay_IK1);
                chk_status_I6.Checked = true;
                txt_giobatdau_I2.Text = "08:00";
                txt_gioketthuc_I2.Text = "17:00";
            } 
            else
            {
                txt_idngay_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idngay_IK1);                             
            }                     
        }

        private bool checkInput()
        {
            if (dte_ngay_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngay_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngay_I5.Focus();
                return false;
            }

           
            if (spe_ngaycong_I4.Value == 0)
            {
                dxEp_error_S.SetError(spe_ngaycong_I4, Function.clsFunction.transLateText("Không được rỗng"));
                spe_ngaycong_I4.Focus();
                return false;
            }

           
            if (_insert == true)
            {
                DataTable dtcheck = APCoreProcess.APCoreProcess.Read("select * from congviechangngay where manhanvienphutrach ='"+ glue_manhanvienphutrach_I1.EditValue.ToString() +"' and ngay =cast('"+ dte_ngay_I5.EditValue +"' as date)");
                if (dtcheck.Rows.Count > 0)
                {
                    clsFunction.MessageInfo("Đã nhập công việc hàng ngày cho nhân viên " + glue_manhanvienphutrach_I1.EditValue.ToString() , "Thông báo");
                    return false;
                }
            }
            else
            {

            }

            return true;
        }

        private void loadGridLookupEmployee()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienthuchien_I1, "select IDEMP, StaffName from EMPLOYEES where status = 1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienthuchien_I1.Width);
            }
            catch { }
        }

        private void loadGridLookupQuanly()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Tên NV" };
                string[] fieldname = new string[] { "IDEMP", "StaffName" };
                string[] col_visible = new string[] { "False", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhanvienphutrach_I1, "select IDEMP, StaffName from EMPLOYEES  where status = 1", "StaffName", "IDEMP", caption, fieldname, this.Name, glue_manhanvienphutrach_I1.Width);
            }
            catch { }
        }


        private void loadGridLookupNhiemVu()
        {
            try
            {
                string[] caption = new string[] { "Mã NV", "Nhiệm vụ" };
                string[] fieldname = new string[] { "manhiemvu", "congviecduocgiao" };
                string[] col_visible = new string[] { "True", "True" };
                ControlDev.FormatControls.LoadGridLookupEdit(glue_manhiemvu_I1, "select distinct N.manhiemvu, N.congviecduocgiao from NHIEMVU N Left join TIENDOCONGVIEC T ON N.manhiemvu = T.manhiemvu WHERE T.matinhtrangcongviec NOT IN('TT000004, TT000005')", "congviecduocgiao", "manhiemvu", caption, fieldname, this.Name, glue_manhiemvu_I1.Width);
            }
            catch { }
        }

        #endregion

    }
}