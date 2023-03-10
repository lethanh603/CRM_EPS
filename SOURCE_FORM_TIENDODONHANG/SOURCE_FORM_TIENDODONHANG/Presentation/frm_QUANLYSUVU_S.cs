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

namespace SOURCE_FORM_TIENDODONHANG.Presentation
{
    public partial class frm_QUANLYSUVU_S : DevExpress.XtraEditors.XtraForm
    {

        #region Contructor
        public frm_QUANLYSUVU_S()
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
        public string _sign = "HH";
        public string ID = "";
        public string idquotation = "";

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
                loadGridLookupUnit();
                loadGridLookupKind();
                loadGridLookupGroup();
                LoadInfo();
            }
            //chk_status_I6.Enabled = false;
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

        private void glue_id_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glue_idkind_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glue_idgroup_I1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }


        #endregion

        #region GridEvent



        #endregion

        #region Methods

        private void save()
        {
            try
            {
                //chk_quantity_I6.Checked = true;
                if (!checkInput()) return;
                if (_insert == true)
                {
                    Function.clsFunction.Insert_data(this, this.Name);
                    APCoreProcess.APCoreProcess.ExcuteSQL("update dmrentdevice set tenkhongdau = dbo.fChuyenCoDauThanhKhongDauNew(commodity) where idcommodity ='" + txt_idproblem_IK1.Text + "'");
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblem_IK1.Name) + " = '" + txt_idproblem_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcommodity_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);

                    if (call == true)
                    {
                        strpassData(txt_idproblem_IK1.Text);
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
                    Function.clsFunction.Edit_data(this, this.Name, Function.clsFunction.getNameControls(txt_idproblem_IK1.Name), ID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(APCoreProcess.APCoreProcess.Read(Function.clsFunction.getNameControls(this.Name) + " where " + Function.clsFunction.getNameControls(txt_idproblem_IK1.Name) + " = '" + txt_idproblem_IK1.Text + "'"));
                    //Function.clsFunction.sysTrace(this.Name, Function.clsFunction.transLate(btn_insert_allow_insert.Text), txt_idcommodity_IK1.Text, txt_commodity_500_I2.Text, SystemInformation.ComputerName.ToString(), "0", ds.GetXml(), Assembly.GetAssembly(this.GetType()).ToString().Split(',')[0]);
                    dxEp_error_S.ClearErrors();
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
                txt_idproblem_IK1.Text = Function.clsFunction.layMa(_sign, Function.clsFunction.getNameControls(txt_idproblem_IK1.Name), Function.clsFunction.getNameControls(this.Name));
                Function.clsFunction.Data_XoaText(this, txt_idproblem_IK1);
                chk_status_I6.Checked = true;
            } 
            else
            {
                txt_idproblem_IK1.Text = ID;
                Function.clsFunction.Data_Binding1(this, txt_idproblem_IK1);
                             
            }                     
        }

        private bool checkInput()
        {
            dxEp_error_S.ClearErrors();
            if (glue_idgroup_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idgroup_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idgroup_I1.Focus();
                return false;
            }
           
            if (glue_idkind_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idkind_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idkind_I1.Focus();
                return false;
            }
            if (glue_idunit_I1.Text == "")
            {
                dxEp_error_S.SetError(glue_idunit_I1, Function.clsFunction.transLateText("Không được rỗng"));
                glue_idunit_I1.Focus();
                return false;
            }
            if (mmo_spec_I3.Text == "")
            {
                dxEp_error_S.SetError(mmo_spec_I3, Function.clsFunction.transLateText("Không được rỗng"));
                mmo_spec_I3.Focus();
                return false;
            }

            if (_insert == true)
            {       
            }
            else
            {
                DataTable dt = new DataTable();
                dt = APCoreProcess.APCoreProcess.Read("select commodity, equipmentinfo, model, spec from DMRENTDEVICE where idcommodity='" + txt_idproblem_IK1.Text + "'");
                if (dt.Rows.Count > 0)
                {
                }
            }   

            return true;
        }
        
        private void loadGridLookupUnit()
        {
            string[] caption = new string[] { "Mã ĐVT", "ĐVT" };
            string[] fieldname = new string[] { "idunit", "unit" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idunit_I1, "select idunit, unit from dmunit where status=1", "unit", "idunit", caption, fieldname, this.Name, glue_idunit_I1.Width);
        }

        private void callForm()
        {
            SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S frm = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S();
            //frm.passData = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S.PassData(getValue);
            frm.strpassData = new SOURCE_FORM_DMUNIT.Presentation.frm_DMUNIT_S.strPassData(getStringValue);
            frm._insert = true;
            frm.call = true;
            frm._sign = "DV";
            frm.ShowDialog();
        }
        
        private void getStringValue(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idunit_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idunit,unit from dmunit where status=1");
                    glue_idunit_I1.EditValue = value;
                }
            }
            catch { }
        }

        private void loadGridLookupGroup()
        {
            string[] caption = new string[] { "Mã nhóm", "Nhóm hàng" };
            string[] fieldname = new string[] { "idgroup", "groupname" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idgroup_I1, "select idgroup, groupname from dmgroup where status=1", "groupname", "idgroup", caption, fieldname, this.Name, glue_idgroup_I1.Width);
        }

        private void callFormGroup()
        {
            SOURCE_FORM_DMGROUP.Presentation.frm_DMGROUP_S frm = new SOURCE_FORM_DMGROUP.Presentation.frm_DMGROUP_S();  
            frm.strpassData = new SOURCE_FORM_DMGROUP.Presentation.frm_DMGROUP_S.strPassData(getStringValueGroup);
            frm._insert = true;
            frm.call = true;
            frm._sign = "NH";
            frm.ShowDialog();
        }
        
        private void getStringValueGroup(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idgroup_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idgroup,groupname from dmgroup where status=1");
                    glue_idgroup_I1.EditValue = value;
                }
            }
            catch { }
        }

        private void loadGridLookupKind()
        {
            string[] caption = new string[] { "Mã loại", "Loại hàng" };
            string[] fieldname = new string[] { "idkind", "kind" };
            ControlDev.FormatControls.LoadGridLookupEdit(glue_idkind_I1, "select idkind, kind from dmkind where status=1", "kind", "idkind", caption, fieldname, this.Name, glue_idkind_I1.Width);
        }

        private void callFormKind()
        {
            SOURCE_FORM_DMKIND.Presentation.frm_DMKIND_S frm = new SOURCE_FORM_DMKIND.Presentation.frm_DMKIND_S();
            frm.strpassData = new SOURCE_FORM_DMKIND.Presentation.frm_DMKIND_S.strPassData(getStringValueKind);
            frm._insert = true;
            frm.call = true;
            frm._sign = "LH";
            frm.ShowDialog();
        }

        private void getStringValueKind(string value)
        {
            try
            {
                if (value != "")
                {
                    glue_idkind_I1.Properties.DataSource = APCoreProcess.APCoreProcess.Read("select idkind,kind from dmkind where status=1");
                    glue_idkind_I1.EditValue = value;
                }
            }
            catch { }
        }      

        #endregion   

        private void mmo_spec_I3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl16_Click(object sender, EventArgs e)
        {

        }
        
    }
}