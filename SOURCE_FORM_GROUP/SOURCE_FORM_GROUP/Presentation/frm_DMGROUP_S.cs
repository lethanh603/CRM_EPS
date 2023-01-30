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

namespace SOURCE_FORM_DMGROUP.Presentation
{
    public partial class frm_DMGROUP_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_DMGROUP_S()
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
        public string _sign = "NH";
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
                loadGridLookupTABLE();
                loadGridLookupEMP();
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
            save();
            if (dxEp_error_S.HasErrors == false)
            {
                InitData();
                txt_sign_20_I1.Focus();
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
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idgroup_IK1.Name) + " = '" + txt_idgroup_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idgroup_IK1.Text, txt_groupname_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    
                if (call == true)
                {
                    strpassData(txt_idgroup_IK1.Text);                    
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
                Function.clsFunction.Edit_data(this, this.Name,Function.clsFunction.getNameControls(txt_idgroup_IK1.Name),ID);
                DataSet ds = new DataSet();
                ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idgroup_IK1.Name) + " = '" + txt_idgroup_IK1.Text + "'"));
                Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idgroup_IK1.Text, txt_groupname_50_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                 
                passData(true);
            }
        }

        private void LoadInfo()
        {
            this.Focus();
            if (_insert==true)
            {
                txt_idgroup_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idgroup_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idgroup_IK1);
                chk_status_I6.Checked = true;
                txt_sign_20_I1.Text = txt_idgroup_IK1.Text;
            } 
            else
            {
                txt_idgroup_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idgroup_IK1);
                             
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
            if (txt_groupname_50_I2.Text == "")
            {
                dxEp_error_S.SetError(txt_groupname_50_I2, Function.clsFunction.transLateText("Không được rỗng"));
                txt_groupname_50_I2.Focus();
                return false;
            }
            if (glue_idsector_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idsector_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idsector_I1.Focus();
                return false;
            }


            if (_insert == true)
            {
                if (APCoreProcess.APCoreProcess.Read("select * from "+ Function.clsFunction.getNameControls(this.Name) +" where sign='" + txt_sign_20_I1.Text + "'").Rows.Count > 0)
                {
                    dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                    txt_sign_20_I1.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select sign from " + Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idgroup_IK1.Name) + "='" + txt_idgroup_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (APCoreProcess.APCoreProcess.Read("select * from " + Function.clsFunction.getNameControls(this.Name) + " where sign='" + txt_sign_20_I1.Text + "' and sign <>'" + dt.Rows[0][0].ToString() + "'").Rows.Count > 0)
                    {
                        dxEp_error_S.SetError(txt_sign_20_I1, Function.clsFunction.transLateText("Không được trùng"));
                        txt_sign_20_I1.Focus();
                        return false;
                    }
                } 
            }     

            return true;
        }
        
        private void loadGridLookupTABLE()
        {
            string[] caption = new string[] { "Mã Ngành", "Ngành hàng" };
            string[] fieldname = new string[] { "idsector", "sector" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idsector_I1,"select idsector, sector from dmsectors","sector","idsector",caption,fieldname, this.Name, glue_idsector_I1.Width);
        }

        private void callForm()
        {
            SOURCE_FORM_DMSTORE.Presentation.frm_DMSTORE_S frm = new SOURCE_FORM_DMSTORE.Presentation.frm_DMSTORE_S();
            frm.passData = new SOURCE_FORM_DMSTORE.Presentation.frm_DMSTORE_S.PassData(getValue);
            frm.strpassData = new SOURCE_FORM_DMSTORE.Presentation.frm_DMSTORE_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "NH";
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
                glue_idsector_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idstore,store from dmstore where status=1");
                glue_idsector_I1.EditValue = value;
            }
        }

        private void loadGridLookupEMP()
        {
            string[] caption = new string[] { "Mã NV", "Tên nhân viên" };
            string[] fieldname = new string[] { "idemp", "staffname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idemp_I1, "select IDEMP as idemp, StaffName As staffname from employees", "staffname", "idemp", caption, fieldname, this.Name, glue_idemp_I1.Width);

        }

        #endregion        



    }
}