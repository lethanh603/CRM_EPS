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

namespace SOURCE_FORM_DEVICEFORRENT.Presentation
{
    public partial class frm_STARTRENTDEVICE_S: DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_STARTRENTDEVICE_S()
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
        public string _sign = "KH";
        public string ID = "";
        public string idThuexe = "";

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            //statusForm = true;
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
                Function.clsFunction.TranslateForm(this, this.Name);
                LoadInfo();
            }
            txt_sign_20_I1.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            txt_sign_20_I1.Focus();
        }

        private void frm_DMAREA_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btn_insert_allow_insert.PerformClick();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_id_IK1.Name) + " = '" + txt_id_IK1.Text + "'"));
                //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_id_IK1.Text, txt_area_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_id_IK1.Text);                    
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
                DataTable dt = APCoreProcess.APCoreProcess.Read("select sogioketthuc from STARTRENTDEVICE where id ='"+ txt_id_IK1.Text +"'");
                if (dt.Rows.Count > 0)
                {
                    if (!checkAdmin() && Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        clsFunction.MessageInfo("Thông báo", "Chỉ có admin mới có quyền xóa hoặc sửa thông tin này");
                        return;
                    }
                }
                Function.clsFunction.Edit_data(this, this.Name,"id",ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_id_IK1.Name) + " = '" + txt_id_IK1.Text + "'"));
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
                txt_id_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_id_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_id_IK1);
                chk_status_I6.Checked = true;
                txt_sign_20_I1.Text = txt_id_IK1.Text;
                txt_idthuexe_I1.Text = idThuexe;
                dte_ngayketthuc_I5.EditValue = Convert.ToDateTime(dte_ngaybatdau_I5.EditValue).AddMonths(1);
            } 
            else
            {
                txt_id_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_id_IK1);                             
            }                     
        }

        private bool checkInput()
        {
            if (txt_sign_20_I1.Text == "")
            {
                txt_sign_20_I1.Focus();
                dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (dte_ngaybatdau_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngaybatdau_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngaybatdau_I5.Focus();
                return false;
            }

            if (dte_ngayketthuc_I5.Text == "")
            {
                dxEp_error_S.SetError(dte_ngayketthuc_I5, Function.clsFunction.transLateText("Không được rỗng"));
                dte_ngayketthuc_I5.Focus();
                return false;
            }

            if (spe_sogiobatdau_I4.Value == 0)
            {
                dxEp_error_S.SetError(spe_sogiobatdau_I4, Function.clsFunction.transLateText("Không được rỗng"));
                spe_sogiobatdau_I4.Focus();
                return false;
            }

            if (spe_sogioketthuc_I4.Value != 0 && spe_sogiobatdau_I4.Value > spe_sogioketthuc_I4.Value)
            {
                dxEp_error_S.SetError(spe_sogioketthuc_I4, Function.clsFunction.transLateText("Số giờ kết thúc phải lớn hơn số giờ bắt đầu"));
                spe_sogioketthuc_I4.Focus();
                return false;
            }

            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from STARTRENTDEVICE where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from STARTRENTDEVICE where id='" + txt_id_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from STARTRENTDEVICE where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }

       

        #endregion        

        private void spe_sogioketthuc_I4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (spe_sogioketthuc_I4.Value > spe_sogiobatdau_I4.Value)
                spe_sogiochenlech_S.Value = spe_sogioketthuc_I4.Value - spe_sogiobatdau_I4.Value;
            }
            catch (Exception ex)
            { }
        }

        private void dte_ngaybatdau_I5_EditValueChanged(object sender, EventArgs e)
        {
            dte_ngayketthuc_I5.EditValue = Convert.ToDateTime(dte_ngaybatdau_I5.EditValue).AddMonths(1).AddDays(-1);
        }
               

    }
}