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

namespace SOURCE_FORM_QUOTATION.Presentation
{
    public partial class frm_PHIEUTHU_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_PHIEUTHU_S()
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
        public string _sign = "PT";
        public string ID = "";
        public string idexport = "";  

        #endregion

        #region FormEvent

        private void frm_AREA_S_Load(object sender, EventArgs e)
        {
            
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
                loadGridNhanVien();
                LoadInfo();
            }
            chk_quantity_I6.Checked = true;
            cal_sotien_12_I4.Focus();
            chk_status_I6.Enabled = false;
            cal_sotien_12_I4.Value = cal_conlai_S.Value;
            txt_idexport_I2.Text = idexport;
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            try
            {
                txt_idexport_I2.Focus();
            }
            catch { }
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
            try
            {
                save();
                if (dxEp_error_S.HasErrors == false)
                {
                    InitData();
                    txt_idexport_I2.Focus();
                    passData(true);
                }
            }
            catch { }
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
            if (e.KeyCode == Keys.Enter && ! sender.GetType().ToString().Contains("MemoEdit"))
                SendKeys.Send("{Tab}");
        }  

        #endregion

        #region GridEvent



        #endregion

        #region Methods

        private void save()
        {
            try
            {
                chk_quantity_I6.Checked = true;
                if (!checkInput()) return;
                if (_insert == true)
                {
                    Function.clsFunction.Insert_data(this, this.Name);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_maphieuthu_IK1.Name) + " = '" + txt_maphieuthu_IK1.Text + "'"));

                    if (call == true)
                    {
                        strpassData(txt_maphieuthu_IK1.Text);
                    }
                    else
                    {
                        LoadInfo();
                    }
                    passData(true);
                    //this.Hide();  
                    dxEp_error_S.ClearErrors();
                }
                else
                {
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_maphieuthu_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_maphieuthu_IK1.Name) + " = '" + txt_maphieuthu_IK1.Text + "'"));

                    passData(true);
                }
            }
            catch
            {
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_maphieuthu_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_maphieuthu_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_maphieuthu_IK1);
                chk_status_I6.Checked = true;
                dte_ngaythu_I5.EditValue = DateTime.Now;
               
            } 
            else
            {
                txt_maphieuthu_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_maphieuthu_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            dxEp_error_S.ClearErrors();
            if (txt_idexport_I2.Text == "")
            {
                txt_idexport_I2.Focus();
                dxEp_error_S.SetError(txt_idexport_I2, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (_insert == true)
            {               

                if (cal_sotien_12_I4.Value <=0 || cal_sotien_12_I4.Value >  cal_conlai_S.Value)
                {
                    dxEp_error_S.SetError(cal_sotien_12_I4, Function.clsFunction.transLateText("Số tiền phải lớn hơn 0 và phải nhỏ hơn số tiền cần thu"));
                    return false;
                }          
            }
            else
            {
                if (cal_sotien_12_I4.Value <= 0 || cal_sotien_12_I4.Value >  cal_conlai_S.Value)
                {
                    dxEp_error_S.SetError(cal_sotien_12_I4, Function.clsFunction.transLateText("Số tiền phải lớn hơn 0 và phải nhỏ hơn số tiền cần thu"));
                    return false;
                }
            }   

            return true;
        }
        
        private void loadGridNhanVien()
        {
            string[] caption = new string[] { "Mã NV", "Nhân viên" };
            string[] fieldname = new string[] { "idemp", "staffname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1, "select idemp, staffname from employees where status=1", "staffname", "idemp", caption, fieldname, this.Name, glue_idemp_I1.Width);
        }

       

        #endregion       
        
    }
}