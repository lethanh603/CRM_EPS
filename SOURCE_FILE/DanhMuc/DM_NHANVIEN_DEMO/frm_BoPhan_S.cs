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

namespace SOURCE_FORM_DMNHANVIEN.Presentation
{
    public partial class frm_BOPHAN_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_BOPHAN_S()
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
        public string _sign = "NV";
        public string ID = "";  

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
                loadGridLookupKV();
                LoadInfo();
            }
            txt_MANV_20_I1.Focus();
        }

        private void frm_DMAREA_S_Activated(object sender, EventArgs e)
        {
            txt_MANV_20_I1.Focus();
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
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                InitData();
                txt_MANV_20_I1.Focus();
            }
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

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                callForm();
            }
            catch 
            { }
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_NVID_IK1.Name) + " = '" + txt_NVID_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_NVID_IK1.Text, txt_TENNV_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_NVID_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,"NVID",ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_NVID_IK1.Name) + " = '" + txt_NVID_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_NVID_IK1.Text, txt_TENNV_100_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_NVID_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_NVID_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_NVID_IK1);
                    
            } 
            else
            {
                txt_NVID_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_NVID_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            if (txt_MANV_20_I1.Text == "")
            {
                txt_MANV_20_I1.Focus();
                dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được rỗng"));
                return false;
            }
            if (txt_TENNV_100_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_TENNV_100_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_TENNV_100_I2.Focus();
                return false;
            }
            if (glue_BPID_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_BPID_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_BPID_I1.Focus();
                return false;
            }


            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN where MANV='" + txt_MANV_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_MANV_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select MANV from DMNHANVIEN where NVID='" + txt_NVID_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from DMNHANVIEN where MANV='" + txt_MANV_20_I1.Text + "' and MANV <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_MANV_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_MANV_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }
        
        private void loadGridLookupKV()
        {
            string[] caption = new string[] { "Mã BP", "Tên BP" };
            string[] fieldname = new string[] { "BPID", "TENBP" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_BPID_I1,"select BPID, TENBP from BOPHAN","TENBP","BPID",caption,fieldname, this.Name, glue_BPID_I1.Width);
        }

        private void callForm()
        {
            SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S frm = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S();
            frm.passData = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S.PassData(getValue);
            frm.strpassData = new SOURCE_FORM_DMAREA.Presentation.frm_DMAREA_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "KV";
            frm.ShowDialog();
        }

        private void getValue(bool value)
        {
            if (value == true)
            {

            }
        }

        private void getStringValue(string value)
        {
            if (value != "")
            {
                glue_BPID_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select BPID,TENBP from BOPHAN");
                glue_BPID_I1.EditValue = value;
            }
        }

        #endregion        









    }
}